"use strict";
var $;
$(document).ready(function () {
    function replaceAll(str, find, replace) {
        return str.replace(new RegExp(find, 'g'), replace);
    }
    $(function () {
        //margin,size,pageWidth,pageHeight,colorPercent1,colorPercent2,colorPercent3,colorPercent4,colorPercent5,start,end,preview,div
        var params = getUrlParams();
        var htmlString = "";
        var rowGap = 10;
        var columnGap = 2;
        //var rows = parseInt(params["rows"]);
        //var cols = parseInt(params["cols"]);
        var width = parseInt(params["width"]);
        var margin = parseInt(params["margin"]);
        var size = parseInt(params["size"]);
        var tableWidth = parseInt(params["pageWidth"]);
        var tableHeight = parseInt(params["pageHeight"]);
        var effectiveWidth = tableWidth - (2 * margin);
        var effectiveHeight = tableHeight - (2 * margin);
        var cols = effectiveWidth / (size+10);
        var rows = effectiveHeight / (size + 10);
        cols = parseInt(cols);
        rows = parseInt(rows);
        var pieceCounter = parseInt(params["start"]);
        var start = parseInt(params["start"]);
        var end = parseInt(params["end"]);

        
        var piecesPerPage = rows * cols;
        var totalPieces = end - start + 1;
        var requiredPages = 1 + parseInt((totalPieces / piecesPerPage));
        var colorCounter = 0;
        var cc1 = parseInt(params["colorCount1"]);
        var threshold = cc1;
        var colorIndex = 0;
        var cc2 = parseInt(params["colorCount2"]);
        var cc3 = parseInt(params["colorCount3"]);
        var cc4 = parseInt(params["colorCount4"]);
        var ccs = [cc1, cc1 + cc2, cc1 + cc2 + cc3, cc1 + cc2 + cc3+cc4];
        var sumCcs = ccs[3];
        var color1 = replaceAll(params["color1"],"'","");
        var color2 = replaceAll(params["color2"], "'", "");
        var color3 = replaceAll(params["color3"], "'", "");
        var color4 = replaceAll(params["color4"], "'", "");
        var colorArray = [color1, color2, color3, color4];
        for (var p = 0; p < requiredPages; p++) {

            htmlString += "<table cellspacing='10' class='pagebreak' style='background:pink;width:" + tableWidth + "height:" + tableHeight + ";margin:0px;'>"
            for (var r = 0; r < rows-1 ; r++) {
                htmlString += "<tr>";
                for (var c = 0; c < cols-1 ; c++) {
                    var colorRemainder = colorCounter % sumCcs;
                    if (colorRemainder < ccs[3]) { colorIndex = 4; }
                    if (colorRemainder < ccs[2]) { colorIndex = 3; }
                    if (colorRemainder < ccs[1]) { colorIndex = 2; }
                    if (colorRemainder < ccs[0]) { colorIndex = 1; }
                    var colNoQuote = colorArray[colorIndex - 1];
                    var divwidth = size + "px";
                    var divheight = size + "px";
                    var divborder = "3px solid #000";
                    var divstyle = 'width:WIDTH;height:HEIGHT;border:BORDER;background:BACKGROUND;';
                    divstyle = divstyle.replace("WIDTH", divwidth);
                    divstyle = divstyle.replace("HEIGHT", divheight);
                    divstyle = divstyle.replace("BORDER", divborder);
                    divstyle = divstyle.replace("BACKGROUND", colNoQuote);
                    htmlString += "<td>" + pieceCounter + "<div style='" + divstyle + "'></div></td>"
                    colorCounter++;
                    pieceCounter++;
                }
                htmlString += "</tr>";
            }
            htmlString += "</table>";
        }
        $('body').html(htmlString);
        window.print();

    });


});
function getUrlParams(prop) {
    var params = {};
    var search = decodeURIComponent(window.location.href.slice(window.location.href.indexOf('?') + 1));
    var definitions = search.split('&');

    definitions.forEach(function (val, key) {
        var parts = val.split('=', 2);
        params[parts[0]] = parts[1];
    });

    return (prop && prop in params) ? params[prop] : params;
}