var $;
$(document).ready(function () {

    $(function () {
        updateCounts();
 
    });



    $("#color1,#color2,#color3,#color4,#color5").bind('change',
      function () {
          updateCounts();
      });


    $('input[type=range]').on('input', function () {
        $(this).trigger('change');
        updateCounts();
    });

    //numPageWidth
    $('#numMargin,#numPageWidth,#numPageWidth,#numPatchSize').on("mousewheel keyup mouseup", function (event, delta) {
        drawPreview();
    });

    $("#start,#end").bind('keyup mouseup mousewheel', function () {
        var startVal = parseInt($("#start").val());
        if (startVal < 1 || startVal > 6560) {
            $("#start").val(1);
            $("#end").val(1026);
        }
        updateCounts();
    });
    /*$('#start,#end').on("mousewheel", function (event, delta) {
        updateCounts(event.target, delta);
    });*/

    //(int startNumber, int endNumber, int rows, int columns, int colorCount1, int colorCount2, int colorCount3, int colorCount4, 
    //    string color1, string color2, string color3, string color4)
    // Validating Form Fields.....
    $("#btnCreateTemplate").click(function (e) {
        e.preventDefault();
    });
});



var _startNumber = 1;

function getStartNumber() {
    return _startNumber;
}

function updateCounts(target, delta) {
    var start = parseInt($("#start").val());
    var end = parseInt($("#end").val());

    if (typeof target != 'undefined') {
        if (target.id === "start") {
            start = delta + start;
            _startNumber = start;
        } else {
            end = delta + end;
        }
    }
    log("start,end", [start, end]);

    var totalPieces = end - start + 1;
    var totalPiecesText = totalPieces > 1 ? " Pieces" : " Piece";
    piecesPerPage = 100;//100%
    var colorCounts = GetColorCounts(piecesPerPage);
   

    $("#lblColor1Count").text(colorCounts.color1Count);
    $("#lblColor2Count").text(colorCounts.color2Count);
    $("#lblColor3Count").text(colorCounts.color3Count);
    $("#lblColor4Count").text(colorCounts.color4Count);
    $("#lblColor5Count").text(colorCounts.color5Count);

    drawPreview();
    
}

function addLink() {
    var aHrefString = "PuzzlePalPrint.html?size=SIZE&pageHeight=HEIGHT&pageWidth=WIDTH&rows=ROWS&cols=COLS&start=START&end=END&pages=PAGES&colorCount1=COLORCOUNT1&color1='COLOR1'&colorCount2=COLORCOUNT2&color2='COLOR2'&colorCount3=COLORCOUNT3&color3='COLOR3'&colorCount4=COLORCOUNT4&color4='COLOR4'&margin=MARGIN";
    var height = parseInt($('#numPageHeight').val());
    var width = parseInt($('#numPageWidth').val());
    var size = parseInt($('#numPatchSize').val());
    var margin = parseInt($('#numMargin').val());

    var start = $('#start').val();
    var end = $('#end').val();

    var cc1 = $("#lblColor1Count").text();
    var cc2 = $("#lblColor2Count").text();
    var cc3 = $("#lblColor3Count").text();
    var cc4 = $("#lblColor4Count").text();
    var color1 = $('#color1').val() ;
    var color2 = $('#color2').val() ;
    var color3 = $('#color3').val() ;
    var color4 = $('#color4').val();
    $('#aPrintYourTemplates').text("Print Templates");
    aHrefString = aHrefString.replace("START", start);
    aHrefString = aHrefString.replace("END", end);
    aHrefString = aHrefString.replace("COLORCOUNT1", cc1);
    aHrefString = aHrefString.replace("COLORCOUNT2", cc2);
    aHrefString = aHrefString.replace("COLORCOUNT3", cc3);
    aHrefString = aHrefString.replace("COLORCOUNT4", cc4);
    aHrefString = aHrefString.replace("COLOR1", color1);
    aHrefString = aHrefString.replace("COLOR2", color2);
    aHrefString = aHrefString.replace("COLOR3", color3);
    aHrefString = aHrefString.replace("COLOR4", color4);
    aHrefString = aHrefString.replace("HEIGHT", height);
    aHrefString = aHrefString.replace("WIDTH", width);
    aHrefString = aHrefString.replace("SIZE", size);
    aHrefString = aHrefString.replace("MARGIN", margin);

    $('#aPrintYourTemplates').attr('href', aHrefString);
}
var convertBase = function (val, base1, base2) {
    if (typeof (val) == "number") {
        return parseInt(String(val)).toString(base2);
    } else {
        return parseInt(val.toString(), base1).toString(base2);
    }
};

function pad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}

function log(message, params) {
    var details = "";
    if (params !== undefined) {
        details = ": " + params.join();
    }
    console.log("LOG: " + message + details);
}


function drawPreview() {
    try {
        //margin, size, pageWidth, pageHeight, 
        //colorPercent1, colorPercent2, colorPercent3, colorPercent4, colorPercent5, 
        //start, end, preview, div
        var count1Slider = parseInt($("#lblColor1Count").text());
        var count2Slider = parseInt($("#lblColor2Count").text());
        var count3Slider = parseInt($("#lblColor3Count").text());
        var count4Slider = parseInt($("#lblColor4Count").text());
        var count5Slider = parseInt($("#lblColor5Count").text());
        var pageHeight = parseInt($('#numPageHeight').val());
        var pageWidth = parseInt($('#numPageWidth').val());
        var size = parseInt($('#numPatchSize').val());
        var margin = parseInt($('#numMargin').val());
        var start = parseInt($("#start").val());
        var end = parseInt($("#end").val());
        var div = $("#divPreview");
        div.height( div.width() * (pageHeight / pageWidth));
        CreatePrintTemplate(margin, size, pageWidth, pageHeight, count1Slider, count2Slider, count3Slider, count4Slider, count5Slider, start, end, true, div);
        addLink();
 
    } catch (Err) {
        log("Error in DrawPreview! " + Err.toString());
    }
}
function rotateAndPaintImage(context, image, angleInRad, positionX, positionY, axisX, axisY) {
    context.translate(positionX, positionY);
    context.rotate(angleInRad);
    context.drawImage(image, -axisX, -axisY);
    context.rotate(-angleInRad);
    context.translate(-positionX, -positionY);
}