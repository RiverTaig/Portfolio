/*
The MIT License (MIT)

Copyright (c) 2014 Chris Wilson

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

/*

Usage:
audioNode = createAudioMeter(audioContext,clipLevel,averaging,clipLag);

audioContext: the AudioContext you're using.
clipLevel: the level (0 to 1) that you would consider "clipping".
   Defaults to 0.98.
averaging: how "smoothed" you would like the meter to be over time.
   Should be between 0 and less than 1.  Defaults to 0.95.
clipLag: how long you would like the "clipping" indicator to show
   after clipping has occured, in milliseconds.  Defaults to 750ms.

Access the clipping through node.checkClipping(); use node.shutdown to get rid of it.
*/

function createAudioMeter(audioContext, clipLevel, averaging, clipLag) {
    var processor = audioContext.createScriptProcessor(512);
    processor.onaudioprocess = volumeAudioProcess;
    processor.clipping = false;
    processor.lastClip = 0;
    processor.volume = 0;
    processor.clipLevel = clipLevel || 0.98;
    processor.averaging = averaging || 0.95;
    processor.clipLag = clipLag || 750;

    // this will have no effect, since we don't copy the input to the output,
    // but works around a current Chrome bug.
    processor.connect(audioContext.destination);

    processor.checkClipping =
		function () {
		    if (!this.clipping)
		        return false;
		    if ((this.lastClip + this.clipLag) < window.performance.now())
		        this.clipping = false;
		    return this.clipping;
		};

    processor.shutdown =
		function () {
		    this.disconnect();
		    this.onaudioprocess = null;
		};

    return processor;
}


var _iterations = 0;
//The inital time of first noise will be reset after the first detection of sound > _tapThreshold
var _timeOfFirstNoise = new Date(2000, 1, 1, 1, 0, 0, 0);
var _heardFirstTap = false;
var _awaitingDoubleTap = false;
var _arrayOfVolumes = [];
var _awaitingQuietAfterDoubleTap = false;
var _iterationsAfterDoubleTapDetected = 0;
var _tapThreshold = .15;
var _sumOfLastSecondVolumes = 10;
var _intervalVolume = .1;
var _msToWaitForItToGetQuiet = 500;
var _iterationsToWaitAfterDoubleNoise = 100;
var _resetAfter = 2000;

function getSum(total, num) {
    return total + num;
}

function volumeAudioProcess(event) {
    var buf = event.inputBuffer.getChannelData(0);
    var bufLength = buf.length;
    var sum = 0;
    var x;

    // Do a root-mean-square on the samples: sum up the squares...
    for (var i = 0; i < bufLength; i++) {
        x = buf[i];
        if (Math.abs(x) >= this.clipLevel) {
            this.clipping = true;
            this.lastClip = window.performance.now();
        }
        sum += x * x;
    }

    // ... then take the square root of the sum.
    var rms = Math.sqrt(sum / bufLength);

    // Now smooth this out with the averaging factor applied
    // to the previous sample - take the max here because we
    // want "fast attack, slow release."
    this.volume = Math.max(rms, this.volume * this.averaging);

    //A double tap event occurs when the following sequence takes place:
    //1) During the last 1000 ms, there is a quiet period (meaning the sum of the collected volumes is less than _sumOfLastSecondVolumes (default of 10)
    //2) There is a noise in excess of _tapThreshold (default of .25)
    //3) There is a moment within the next _msToWaitForItToGetQuiet (default 500 ms) that the volume falls below _intervalVolume (Default .1)
    //4) There is another noise in excess of _tapThreshold
    //5) A number of iterations (_iterationsToWaitAfterDoubleNoise) - and there are about 100 per second - take place
    //6) It is determined that the after _iterationsToWaitAfterDoubleNoise, it was sufficiently quiet that the second noise was likely a tap.

    var end = Date.now();
    var elapsed = (end - _timeOfFirstNoise);
    var awaitingFirstNoise = false;
    //Everything needs to happen in _resetAfter xx seconds.  If not, reset the variables
    if (elapsed > _resetAfter) {
        _awaitingDoubleTap = false;
        _heardFirstTap = false;
        _awaitingDoubleTap = false;
        _awaitingQuietAfterDoubleTap = false;
        awaitingFirstNoise = true;
    }
    _iterations++;
    var vol = this.volume;
    //This procedure is called about 100 times per second, so shifting
    //the array if the length is greater than 100 will detect talking
    _arrayOfVolumes.push(vol);
    if (_arrayOfVolumes.length > 100)
    {
        _arrayOfVolumes.shift();
    }
    var sumOfLastSecondVolumes = _arrayOfVolumes.reduce(getSum);

    if (vol > _tapThreshold && awaitingFirstNoise) {

        //if it has been noisy in the previous second, this "tap" is likely a person talking, so it should be ignored
        if (sumOfLastSecondVolumes > _sumOfLastSecondVolumes) {
            console.log("Detecting talking - first tap ignored - sum of last 1 second volumes was " + sumOfLastSecondVolumes);
        }
        else {
            console.log("Detected first tap  - sum of last 1 second volumes was " + sumOfLastSecondVolumes);
            _heardFirstTap = true;
        }
        _timeOfFirstNoise = Date.now();
    }
    //If we are awaiting a double tap, then that means that the volume has gotten quiet after the first tap,
    //that less than 500 ms have elapsed since the first tap, and that now we suddenly have a noise again.  That
    //means we have a double noise detected, but is it a double-tap? To tell that, we reset an iteration counter
    //to make sure that it is sufficiently quiet after the double-noise event to rule out this being talking. 
    if (vol > _tapThreshold && _awaitingDoubleTap) {
        console.log("Double noise detected - awaiting quiet period. ");
        _awaitingDoubleTap = false;
        _awaitingQuietAfterDoubleTap = true;
        _iterationsAfterDoubleTapDetected = _iterations;
    }
    //next, for up to 500ms, we wait for it to get quiet between the first tap and (hopefully) the second tap
    if (elapsed < _msToWaitForItToGetQuiet && _heardFirstTap && vol < _intervalVolume) {
        console.log("It's getting quiet in here! - setting _awaitingDoubleTap = true");
        _heardFirstTap = false;
        _awaitingDoubleTap = true;
    }

    //After a double tap is detected, we need to wait one additional second (100 iterations). This does two things - 
    //it allows the user to get their hands out of the way so that the rubiks cube can be imaged 
    //and we also collect background volumes during this period.  If the background volumes indicate
    //talking, then the double-tap event is not raise. 
    if (_awaitingQuietAfterDoubleTap && _iterations > _iterationsAfterDoubleTapDetected + _iterationsToWaitAfterDoubleNoise) {
        _awaitingQuietAfterDoubleTap = false;
        if (sumOfLastSecondVolumes > 10) {
            console.log("Detected talking after double tap, second tap ignored - sum of last 1 second volumes was " + sumOfLastSecondVolumes);
        }
        else {
            console.log("DOUBLE TAP EVENT  - sum of last 1 second volumes was " + sumOfLastSecondVolumes);
            _awaitingDoubleTap = false;
            _heardFirstTap = false;
            _awaitingDoubleTap = false;
            _awaitingQuietAfterDoubleTap = false;
            var canvas = document.getElementById('canvas');
            var context = canvas.getContext('2d');
            context.drawImage(video, 0, 0, 640, 480);
            drawArrow(20, 20, 250, 250);
        }
    }


}



function drawArrow(fromx, fromy, tox, toy) {
    //variables to be used when creating the arrow
    var c = document.getElementById("myCanvas");
    var ctx = c.getContext("2d");
    var headlen = 10;

    var angle = Math.atan2(toy - fromy, tox - fromx);

    //starting path of the arrow from the start square to the end square and drawing the stroke
/*
ctx.beginPath();
ctx.moveTo(fromx, fromy);
ctx.lineTo(tox, toy);
ctx.strokeStyle = "rgba(255, 0, 255, 0.5)";
ctx.lineWidth = 22;
ctx.stroke();
*/
    var midX = 0.5 * ((tox - headlen * Math.cos(angle - Math.PI / 7)) + (tox - headlen * Math.cos(angle + Math.PI / 7)));
    var midY = 0.5 * ((toy - headlen * Math.sin(angle - Math.PI / 7)) + (toy - headlen * Math.sin(angle + Math.PI / 7)));
    ctx.beginPath();
    ctx.moveTo(fromx, fromy);
    ctx.lineTo(tox, toy);
    ctx.strokeStyle = "rgba(255, 0, 255, 0.5)";
    ctx.lineWidth = 22;
    ctx.stroke();

    //starting a new path from the head of the arrow to one of the sides of the point
    ctx.beginPath();
    ctx.moveTo(tox, toy);
    ctx.lineTo(tox - headlen * Math.cos(angle - Math.PI / 7), toy - headlen * Math.sin(angle - Math.PI / 7));

    //path from the side point of the arrow, to the other side point
    ctx.lineTo(tox - headlen * Math.cos(angle + Math.PI / 7), toy - headlen * Math.sin(angle + Math.PI / 7));




    //path from the side point back to the tip of the arrow, and then again to the opposite side point
    ctx.lineTo(tox, toy);
    ctx.lineTo(tox - headlen * Math.cos(angle - Math.PI / 7), toy - headlen * Math.sin(angle - Math.PI / 7));

    //draws the paths created above
    ctx.strokeStyle = "rgba(255, 0, 255, 0.5)";
    ctx.lineWidth = 3;
    ctx.stroke();
    //ctx.fillStyle = "rgba(255, 0, 255, 0.5)";
    //ctx.fill();
}