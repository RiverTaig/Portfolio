var $;
$(document).ready(function () {

    $(function () {
        $("#dialog").dialog({
            width: 600,
            autoOpen: true
        });
        updateCounts();
    });

    $("#cboLayout").change(function () {
        updateCounts();
    });

    $("#color1,#color2,#color3,#color4").bind('change',
      function () {
          //console.log("color change");
          updateCounts();
      });


    $('input[type=range]').on('input', function () {
        $(this).trigger('change');
        updateCounts();
    });


    $('#numMargin').on("mousewheel keyup mouseup", function (event, delta) {
        var cnt1 = parseInt($("#lblColor1Count").text());
        var cnt2 = parseInt($("#lblColor2Count").text());
        var cnt3 = parseInt($("#lblColor3Count").text());
        var cnt4 = parseInt($("#lblColor4Count").text());
        drawPreview(cnt1, cnt2, cnt3, cnt4);
    });

    $("#start,#end").bind('keyup mouseup', function () {
        var startVal = parseInt($("#start").val());
        if (startVal < 1 || startVal > 6560) {
            $("#start").val(1);
            $("#end").val(1026);
        }
        updateCounts();
    });
    $('#start,#end').on("mousewheel", function (event, delta) {
        updateCounts(event.target, delta);
    });

    //(int startNumber, int endNumber, int rows, int columns, int colorCount1, int colorCount2, int colorCount3, int colorCount4, 
    //    string color1, string color2, string color3, string color4)
    // Validating Form Fields.....
    $("#btnCreateTemplate").click(function (e) {
        //alert("asdf");
        e.preventDefault();
        //debugger;
        //var aHrefString = "PuzzlePalPrint.html?rows=ROWS&cols=COLS&start=START&end=END&pages=PAGES&colorCount1=COLORCOUNT1&color1='COLOR1'&colorCount2=COLORCOUNT2&color2='COLOR2'&colorCount3=COLORCOUNT3&color3='COLOR3'&colorCount4=COLORCOUNT4&color4='COLOR4'&margin='MARGIN'";
        //var start = $('start').val;
        //var end = $('end').val;
        //var piecesPerPage = parseInt($("#cboLayout").val());
        //var cc1 = $("#lblColor1Count").text;
        //var cc2 = $("#lblColor2Count").text;
        //var cc3 = $("#lblColor3Count").text;
        //var cc4 = $("#lblColor4Count").text;
        //$('#aPrintYourTemplates').text("Print Templates");
        //aHrefString = aHrefString.replace("ROWS", 5);
        //aHrefString = aHrefString.replace("COLS", 6);
        //aHrefString = aHrefString.replace("START", start);
        //aHrefString = aHrefString.replace("END", end);
        //aHrefString = aHrefString.replace("COLORCOUNT1", cc1);
        //aHrefString = aHrefString.replace("COLORCOUNT2", cc2);
        //aHrefString = aHrefString.replace("COLORCOUNT3", cc3);
        //aHrefString = aHrefString.replace("COLORCOUNT4", cc4);
        //$('#aPrintYourTemplates').attr('href', aHrefString);
        /*var data = {
            startNumber: 1,
            endNumber: 1024,
            rows: 4,
            columns: 5,
            colorCount1: 5,
            colorCount2: 5,
            colorCount3: 5,
            colorCount4: 5,
            color1: "#ffffff",
            color2: "#ffffff",
            color3: "#ffffff",
            color4: "#ffffff"
        };
        e.preventDefault();
        $.ajax({
            data: data,
            method: "post",
            url: "WebService.asmx/MakeTemplates",
            dataType: "xml",
            success: function (msg) {
                alert(msg);
            },
            error: function (e) {
                alert("error " + e);
            }
        });*/
    });
});

function clearCanvas() {
    var canvas = document.getElementById("canPreview");
    var ctx = canvas.getContext("2d");
    ctx.font = "10px Arial";
    ctx.fillStyle = '#FFFFFF';
    ctx.clearRect(0, 0, canvas.width, canvas.height);
}

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
    //console.log(start + "," + end + "," + totalPieces);
    var piecesPerPage = parseInt($("#cboLayout").val());
    var requiredPages = 1 + parseInt((totalPieces / piecesPerPage));
    var pageOrPages = requiredPages > 1 ? " Pages" : " Page";
    var count1Slider = parseInt($("#sldColor1").val());
    var count2Slider = parseInt($("#sldColor2").val());
    var count3Slider = parseInt($("#sldColor3").val());
    var count4Slider = parseInt($("#sldColor4").val());
    if (count1Slider + count2Slider + count3Slider + count4Slider === 0) {
        count1Slider = 1;
    }
    var totalSliderCount = count1Slider + count2Slider + count3Slider + count4Slider;
    var count1SliderPercent = count1Slider / totalSliderCount;
    var count2SliderPercent = count2Slider / totalSliderCount;
    var count3SliderPercent = count3Slider / totalSliderCount;
    var count4SliderPercent = count4Slider / totalSliderCount;
    //console.log(totalSliderCount);
    var color1Count = Math.round((count1SliderPercent * piecesPerPage));
    var color2Count = Math.round((count2SliderPercent * piecesPerPage));
    var color3Count = Math.round((count3SliderPercent * piecesPerPage));
    var color4Count = Math.round((count4SliderPercent * piecesPerPage));
    var colorCountArray = [color1Count, color2Count, color3Count, color4Count];

    var maxCount = Math.max(color1Count, color2Count, color3Count, color4Count);
    //console.log("max = " + maxCount);
    var index = colorCountArray.indexOf(maxCount);
    var bump = color1Count + color2Count + color3Count + color4Count > piecesPerPage ? -1 : 1;
    //console.log("index,bump,ppp = " + index + "," + bump + "," + piecesPerPage);
    var bail = 0;
    while (color1Count + color2Count + color3Count + color4Count != piecesPerPage) {
        //console.log("counts = " + color1Count + "," + color2Count + "," + color3Count + "," + color4Count);
        if (index === 0) {
            color1Count += bump;
        }
        if (index === 1) {
            color2Count += bump;
        }
        if (index === 2) {
            color3Count += bump;
        }
        if (index === 3) {
            color4Count += bump;
        }
        bail++;
        if (bail > 10) {
            break;
        }
    }
    $("#lblColor1Count").text(color1Count);
    $("#lblColor2Count").text(color2Count);
    $("#lblColor3Count").text(color3Count);
    $("#lblColor4Count").text(color4Count);
    $('#divPiecesPerPage').text(color1Count + color2Count + color3Count + color4Count + totalPiecesText);
    $("#lblPieceCount").text(totalPieces + totalPiecesText);

    $("#lblNumberOfPages").text(requiredPages + pageOrPages);
    drawPreview(color1Count, color2Count, color3Count, color4Count);
    addLink();
}

function addLink() {
    //alert("add link");
    var aHrefString = "PuzzlePalPrint.html?rows=ROWS&cols=COLS&start=START&end=END&pages=PAGES&colorCount1=COLORCOUNT1&color1='COLOR1'&colorCount2=COLORCOUNT2&color2='COLOR2'&colorCount3=COLORCOUNT3&color3='COLOR3'&colorCount4=COLORCOUNT4&color4='COLOR4'&margin='MARGIN'";
    var start = $('#start').val();
    var end = $('#end').val();
    var piecesPerPage = parseInt($("#cboLayout").val());
    var cc1 = $("#lblColor1Count").text();
    var cc2 = $("#lblColor2Count").text();
    var cc3 = $("#lblColor3Count").text();
    var cc4 = $("#lblColor4Count").text();
    $('#aPrintYourTemplates').text("Print Templates");
    aHrefString = aHrefString.replace("START", start);
    aHrefString = aHrefString.replace("END", end);
    aHrefString = aHrefString.replace("COLORCOUNT1", cc1);
    aHrefString = aHrefString.replace("COLORCOUNT2", cc2);
    aHrefString = aHrefString.replace("COLORCOUNT3", cc3);
    aHrefString = aHrefString.replace("COLORCOUNT4", cc4);
    var rows = getRowsAndColums()["rows"]; 
    var cols = getRowsAndColums()["cols"];
    aHrefString = aHrefString.replace("ROWS", rows);
    aHrefString = aHrefString.replace("COLS", cols);
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
function getRowsAndColums() {
    var rows = parseInt($("#cboLayout option:selected").text().split('x')[0][0]);
    var cols = parseInt($("#cboLayout option:selected").text().split('x')[1].split('=')[0]);
    return {
        rows: rows,
        cols:cols
    }
}
function drawPreview(colorCount1, colorCount2, colorCount3, colorCount4) {
    try {
        clearCanvas();
        var margin = parseInt($('#numMargin').val());
        var rowGap = 10;
        var columnGap = 2;
        var colorChangeArray = [colorCount1, colorCount1 + colorCount2, colorCount1 + colorCount2 + colorCount3];
        var color1 = $('#color1').val();
        var color2 = $('#color2').val();
        var color3 = $('#color3').val();
        var color4 = $('#color4').val();
        var startNum = getStartNumber();
        log("startNum", [startNum]);
        var colorArray = [color1, color2, color3, color4];
        var pieceNumber = 0;
        //var colorChangesIndex =0;
        var color = colorArray[0];
        var canvas = document.getElementById("canPreview");
        var ctx = canvas.getContext("2d");

        ctx.font = "10px Arial";
        ctx.fillStyle = '#FFFFFF';
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        ctx.beginPath();
        ctx.moveTo(0, 0);
        ctx.lineTo(0, 0);
        ctx.stroke();
        var cboRowsColumns = $("#cboLayout");
        var rows = getRowsAndColums()["rows"]; //parseInt($("#cboLayout option:selected").text().split('x')[0][0]);
        var cols = getRowsAndColums()["cols"];  //parseInt($("#cboLayout option:selected").text().split('x')[1].split('=')[0]);

        var paperHeight = canvas.height - (2 * margin);
        var paperWidth = canvas.width - (2 * margin);
        var size1 = (paperWidth / cols) - columnGap;
        var size2 = (paperHeight / rows) - rowGap;
        var size = size1 < size2 ? size1 : size2;

        var offsetC = 4 + size;
        var offsetR = size + rowGap;

        var widthMinusMargins = (canvas.width - (2 * margin));
        var sizeTimesColumns = (size * cols);
        var availableColumnWhiteSpace = widthMinusMargins - sizeTimesColumns;
        var availableColumnWhiteSpaceDividedByGaps = availableColumnWhiteSpace / (cols - 1);

        var heightMinusMargins = (canvas.height - (2 * margin));
        var sizeTimesRows = (size * rows);
        var availableRowWhiteSpace = heightMinusMargins - sizeTimesRows;
        var availableRowWhiteSpaceDividedByGaps = availableRowWhiteSpace / (rows - 1);
        //Log("width,size,margin,widthMinusMargins,sizeTimesColumns,availableWhiteSpace,availableWhiteSpaceDividedByGaps",[canvas.width ,size,margin,widthMinusMargins,sizeTimesColumns,availableWhiteSpace,availableWhiteSpaceDividedByGaps]);

        offsetC = 1 + parseInt(size + availableColumnWhiteSpaceDividedByGaps);
        offsetR = 1 + parseInt(size + availableRowWhiteSpaceDividedByGaps);
        var patchesDrawn = 0;
        for (var r = 0; r < rows; r++) {
            var y = r * offsetR;
            for (var c = 0; c < cols; c++) {
                for (var colorChangeIndex = 0; colorChangeIndex < 4; colorChangeIndex++) {
                    color = colorArray[colorChangeIndex];
                    if (colorChangeArray[colorChangeIndex] > patchesDrawn) {
                        break;
                    }
                }

                patchesDrawn++;
                var x = (c * offsetC);
                ctx.fillStyle = color;
                ctx.fillRect(x + (margin), y + (margin), size, size);
                ctx.rect(x + (margin), y + (margin), size, size);
                ctx.strokeStyle = '#ff00ff';
                //ctx.setLineDash([1,1]);
                ctx.stroke();
                pieceNumber++;
                ctx.fillStyle = "#000000";
                ctx.fillText(pieceNumber + startNum - 1, x + margin, y + (margin - 1)); //10 for font size
            }

        }
        var img = new Image();
        img.onload = function () {
            for (var r = 0; r < rows; r++) {
                var y = (margin + (size * .1)) + (r * offsetR);
                for (var c = 0; c < cols ; c++) {
                    var x = (margin + (size * .1)) + (c * offsetC);
                    //ctx.drawImage(img, (size / 8) + margin, (size / 8) + margin, size * .8, size * .8);
                    img.className += "rotate45";
                    ctx.drawImage(img, x, y, size * .8, size * .8);
                }
            }

            //ctx.rotate(11);
            //rotateAndPaintImage(ctx,img,.5,(size / 8) + margin, (size / 8) + margin,(size / 8) + margin, (size / 8) + margin);

        }
        img.src = "http://localhost/pp.png";
        img.className += "rotate45";
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