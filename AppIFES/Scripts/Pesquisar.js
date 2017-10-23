$('tr').each(function () {
    /* look for text in this row*/
    var weaveName = $(this).find('td.headerName').text();
    /* no need to trim "weaverset" each time, do it when variable created, saves many function calls*/
    if ($.trim(weaveName) != $.trim(weaverSet)) {
        $(this).hide();
    }
});

$(document).ready(function () {
    $('#grid').filterTable();
});