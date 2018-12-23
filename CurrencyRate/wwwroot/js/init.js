"use strict";

$(function () {
    $.ajax(
        {
            url: '/api/Rates'
        }).done(function(data) {
            Highcharts.chart('container', eval(data) );
    });
});
