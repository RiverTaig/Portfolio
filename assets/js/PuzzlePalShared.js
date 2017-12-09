function GetColorCounts(piecesPerPage) {
    var count1Slider = parseInt($("#sldColor1").val());
    var count2Slider = parseInt($("#sldColor2").val());
    var count3Slider = parseInt($("#sldColor3").val());
    var count4Slider = parseInt($("#sldColor4").val());
    var count5Slider = parseInt($("#sldColor5").val());
    if (count1Slider + count2Slider + count3Slider + count4Slider + count5Slider === 0) {
        count1Slider = 1;
    }
    var totalSliderCount = count1Slider + count2Slider + count3Slider + count4Slider + count5Slider;
    var count1SliderPercent = count1Slider / totalSliderCount;
    var count2SliderPercent = count2Slider / totalSliderCount;
    var count3SliderPercent = count3Slider / totalSliderCount;
    var count4SliderPercent = count4Slider / totalSliderCount;
    var count5SliderPercent = count5Slider / totalSliderCount;

    //console.log(totalSliderCount);
    var color1Count = Math.round((count1SliderPercent * piecesPerPage));
    var color2Count = Math.round((count2SliderPercent * piecesPerPage));
    var color3Count = Math.round((count3SliderPercent * piecesPerPage));
    var color4Count = Math.round((count4SliderPercent * piecesPerPage));
    var color5Count = Math.round((count5SliderPercent * piecesPerPage));
    var colorCountArray = [color1Count, color2Count, color3Count, color4Count, color5Count];
    var maxCount = Math.max(color1Count, color2Count, color3Count, color4Count, color5Count);
    //console.log("max = " + maxCount);
    var index = colorCountArray.indexOf(maxCount);
    var bump = color1Count + color2Count + color3Count + color4Count + color5Count > piecesPerPage ? -1 : 1;
    //console.log("index,bump,ppp = " + index + "," + bump + "," + piecesPerPage);
    var bail = 0;
    while (color1Count + color2Count + color3Count + color4Count + color5Count != piecesPerPage) {
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
        if (index === 4) {
            color4Count += bump;
        }
        bail++;
        index++;
        if (index == 5) {
            index = 0;
        }
        if (bail > 10) {
            break;
        }
    }
    return {
        color1Count: color1Count,
        color2Count: color2Count,
        color3Count: color3Count,
        color4Count: color4Count,
        color5Count: color5Count,
    }
}
function CreatePrintTemplate(margin, size, pageWidth, pageHeight, colorPercent1, colorPercent2, colorPercent3, colorPercent4, colorPercent5, start, end, preview, div) {
    //HEIGHT: td = size + (border*2)  , tr = td + 2, table= (tr*rows)+( (rows+1)*cellspacingg) = 212 + 36
    var scale = 1.0;
    var textHeight = 12;
    var htmlString = "";
    if (preview) {
        scale = div.width() / pageWidth;
    }
    textHeight = parseInt(textHeight * scale);
    margin = margin * scale;
    var cellspacing = parseInt( 4 * scale);
    size = parseInt(scale * size);
    var maxTableWidth = pageWidth * scale;
    var maxTableHeight = pageHeight * scale;
    var maxUsableWidth = maxTableWidth - (2 * margin);
    var maxUsableHeight = maxTableHeight - (2 * margin);
    var borderSize = 3;
    if (preview) {
        borderSize = 1;
    }
    var tdSize = size + (2 * borderSize ) +textHeight;
    var trSize = 2 + tdSize;
    var numberOfRows = 0;
    var calculatedTableHeight = 0;
    var testTableHeight = 0
    while (testTableHeight < maxUsableHeight) {
        numberOfRows = numberOfRows + 1;
        // table= (tr*rows)+( (rows+1)*cellspacingg) 
        testTableHeight = (trSize * numberOfRows) + ((numberOfRows + 1) * cellspacing);
        if(testTableHeight < maxUsableHeight)
        {
            calculatedTableHeight = testTableHeight;
        }
        
    }
    var rows = numberOfRows - 1;
    var cols = parseInt( maxUsableWidth / trSize);
    var colorCounts = GetColorCounts(rows * cols);
    var ccs = [colorCounts.color1Count, colorCounts.color1Count + colorCounts.color2Count, colorCounts.color1Count + colorCounts.color2Count + colorCounts.color3Count,
        colorCounts.color1Count + colorCounts.color2Count + colorCounts.color3Count + colorCounts.color4Count,
        colorCounts.color1Count + colorCounts.color2Count + colorCounts.color3Count + colorCounts.color4Count + colorCounts.color5Count];
    var pieceCounter = start;
    var piecesPerPage = rows * cols;
    var totalPieces = end - start + 1;
    var requiredPages = 1 + parseInt((totalPieces / piecesPerPage));
    if (preview) {
        requiredPages = 1;
    }
    
    var colorIndex = 0;


    var colorArray = ["#FFFFFF", "#FF0000", "#00FF00", "#0000FF","#000000"];
    for (var p = 0; p < requiredPages; p++) {
        var colorCounter = 0;
        htmlString += "<table id='table" + p + "' cellspacing=" + cellspacing + " class='pagebreak' style='margin:" + margin + "px;'>"
        for (var r = 0; r < rows ; r++) {
            htmlString += "<tr id='tr" + r +"'>";
            for (var c = 0; c < cols ; c++) {
                //var colorRemainder = colorCounter % ccs[4];//sum of the color counts
                if (colorCounter < ccs[4]) { colorIndex = 4; }
                if (colorCounter < ccs[3]) { colorIndex = 3; }
                if (colorCounter < ccs[2]) { colorIndex = 2; }
                if (colorCounter < ccs[1]) { colorIndex = 1; }
                if (colorCounter < ccs[0]) { colorIndex = 0; }
                var colNoQuote = colorArray[colorIndex ];
                var divwidth = size + "px";
                var divheight = size + "px";
                var divborder = borderSize + "px solid #000";
                var divstyle = 'width:WIDTH;height:HEIGHT;border:BORDER;background:BACKGROUND;';
                divstyle = divstyle.replace("WIDTH", divwidth);
                divstyle = divstyle.replace("HEIGHT", divheight);
                divstyle = divstyle.replace("BORDER", divborder);
                divstyle = divstyle.replace("BACKGROUND", colNoQuote);
                var rc = r + "-" + c;
                var pieceCounterDiv = "<div id='divPieceNumber'" + pieceCounter +  " style='font-size:" +textHeight + "px;height:" + textHeight + "px;'>" + pieceCounter + "</div>";
                //htmlString += "<td id='td" + rc + "' style='height:" + tdHeight +  "px;'>" + pieceCounter + "<div id='div" + rc + "' style='" + divstyle + "'></div></td>";
                htmlString += "<td id='td" + rc + "' >" + pieceCounterDiv + "<div id='div" + rc + "' style='" + divstyle + "'></div></td>";
                colorCounter++;
                pieceCounter++;
            }
            htmlString += "</tr>";
        }
        htmlString += "</table>";
    }
    div.html(htmlString);
    //window.print();
}