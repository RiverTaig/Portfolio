var $;
$(document).ready(function () {

    $(function () {
        //debugger;
        var params = getUrlParams();
        var htmlString = "";
        var pieceCounter = parseInt(params["start"]);
        var start = parseInt(params["start"]);
        var end = parseInt(params["end"]);
        var rows = parseInt(params["rows"]);
        var cols = parseInt(params["cols"]);
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
        var color1 = params["color1"];
        var color2 = params["color2"];
        var color3 = params["color3"];
        var color4 = params["color4"];
        for (var p = 0; p < requiredPages; p++) {

            htmlString +=  "<table class='pagebreak'>"
            for (var r = 0; r < rows ; r++) {
                htmlString += "<tr>";
                for (var c = 0; c < cols ; c++) {
                    var colorRemainder = colorCounter % sumCcs;
                    if (colorRemainder < ccs[3]) { colorIndex = 4; }
                    if (colorRemainder < ccs[2]) { colorIndex = 3; }
                    if (colorRemainder < ccs[1]) { colorIndex = 2; }
                    if (colorRemainder < ccs[0]) { colorIndex = 1; }
                    htmlString += "<td>" + pieceCounter + "<div class='puzzleCell" + colorIndex + "'></div></td>"
                    colorCounter++;
                    pieceCounter++;
                }
                htmlString += "</tr>";
            }
            htmlString += "</table>";
        }
        $('body').html(htmlString);
        //<tr>
        //    <td>
        //        123
        //        <div class="color1">
        //        </div>
        //    </td>
        //</tr>
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