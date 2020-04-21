var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getReportData(
                context.season,
                function (data) {
                    $scope.data = data.data;

                    $scope.$apply();
                    jQuery('#content').show();

                    //
                    // render chart
                    //

                    //TOTAL CONTAINER CONFIRMED (By Season)
                    var param = {
                        chartData: { showInLegend: false, name: '', data: [] },
                        charSeries: []
                    }
                    angular.forEach($scope.data.confirmedContainerPerSeasons, function (value, key) {
                        this.charSeries.push(value.season);
                        this.chartData.data.push(value.totalCont);
                    }, param);
                    jQuery('#chartConfirmedContainerBySeason').highcharts({
                        chart: {
                            type: 'column'
                        },
                        title: {
                            text: 'TOTAL CONTAINER OF PROFORMA INVOICE CONFIRMED BY SEASON'
                        },
                        xAxis: {
                            categories: param.charSeries
                        },
                        yAxis: {
                            allowDecimals: false,
                            min: 0,
                            title: {
                                text: 'Quantity'
                            },
                            labels: {
                                formatter: function () {
                                    return $filter('currency')(this.value, '', 0);
                                }
                            }
                        },
                        tooltip: {
                            formatter: function () {
                                return $filter('currency')(this.point.stackTotal, '', 1) + ' container';
                            }
                        },
                        plotOptions: {
                            column: {
                                stacking: 'normal'
                            }
                        },
                        series: [param.chartData]
                    });

                    //TOTAL CONTAINER CONFIRMED BY MONTH
                    var param = {
                        chartData: [],
                        charSeries: []
                    }
                    angular.forEach($scope.data.confirmedContainerPerMonths, function (value, key) {
                        this.chartData.push(
                            {
                                name: value.season, data: [
                                  parseFloat(value.october),
                                  parseFloat(value.november),
                                  parseFloat(value.december),
                                  parseFloat(value.january),
                                  parseFloat(value.febuary),
                                  parseFloat(value.march),
                                  parseFloat(value.april),
                                  parseFloat(value.may),
                                  parseFloat(value.june),
                                  parseFloat(value.july),
                                  parseFloat(value.august),
                                  parseFloat(value.september)
                                ]
                            }
                        );
                    }, param);
                    jQuery('#chartConfirmedContainerByMonth').highcharts({
                        title: {
                            text: 'TOTAL CONTAINER OF PROFORMA INVOICE CONFIRMED BY MONTH'
                        },
                        xAxis: {
                            categories: ['Oct', 'Nov', 'Dec', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep']
                        },
                        yAxis: {
                            title: {
                                text: 'Container'
                            }
                        },
                        tooltip: {
                            formatter: function () {
                                return '<b>' + this.x + ' - ' + this.series.name + '</b>:' + $filter('currency')(this.y, '', 1) + ' Cont';
                            },
                            useHTML: true
                        },
                        series: param.chartData
                    });

                    //all proforma by month
                    var param = {
                        chartData: [],
                        charSeries: []
                    }
                    angular.forEach($scope.data.allProformaByMonths, function (value, key) {
                        this.chartData.push(
                            {
                                name: value.season, data: [
                                  parseFloat(value.october),
                                  parseFloat(value.november),
                                  parseFloat(value.december),
                                  parseFloat(value.january),
                                  parseFloat(value.febuary),
                                  parseFloat(value.march),
                                  parseFloat(value.april),
                                  parseFloat(value.may),
                                  parseFloat(value.june),
                                  parseFloat(value.july),
                                  parseFloat(value.august),
                                  parseFloat(value.september)
                                ]
                            }
                        );
                    }, param);
                    jQuery('#chartAllProformaByMonth').highcharts({
                        title: {
                            text: 'PROFORMA INVOICE'
                        },                       
                        xAxis: {
                            categories: ['Oct', 'Nov', 'Dec', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep']
                        },
                        yAxis: {
                            title: {
                                text: 'Amount in Euro'
                            }
                        },
                        tooltip: {
                            formatter: function () {
                                return '<b>' + this.x + ' - ' + this.series.name + '</b>: &euro;' + $filter('currency')(this.y, '', 0);
                            },
                            useHTML: true
                        },
                        series: param.chartData
                    });

                    //confirmed proforma by month
                    var param = {
                        chartData: [],
                        charSeries: []
                    }
                    angular.forEach($scope.data.confirmedProformaByMonths, function (value, key) {
                        this.chartData.push(
                            {
                                name: value.season, data: [
                                  parseFloat(value.october),
                                  parseFloat(value.november),
                                  parseFloat(value.december),
                                  parseFloat(value.january),
                                  parseFloat(value.febuary),
                                  parseFloat(value.march),
                                  parseFloat(value.april),
                                  parseFloat(value.may),
                                  parseFloat(value.june),
                                  parseFloat(value.july),
                                  parseFloat(value.august),
                                  parseFloat(value.september)
                                ]
                            }
                        );
                    }, param);
                    jQuery('#chartConfirmedProformaByMonth').highcharts({
                        title: {
                            text: 'PROFORMA INVOICE CONFIRMED'
                        },
                        xAxis: {
                            categories: ['Oct', 'Nov', 'Dec', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep']
                        },
                        yAxis: {
                            title: {
                                text: 'Amount in Euro'
                            }
                        },
                        tooltip: {
                            formatter: function () {
                                return '<b>' + this.x + ' - ' + this.series.name + '</b>: &euro;' + $filter('currency')(this.y, '', 0);
                            },
                            useHTML: true
                        },
                        series: param.chartData
                    });


                    //Eurofar Invoiced by month
                    var param = {
                        chartData: [],
                        charSeries: []
                    }
                    angular.forEach($scope.data.eurofarInvoicedPerMonths, function (value, key) {
                        this.chartData.push(
                            {
                                name: value.season, data: [
                                  parseFloat(value.october),
                                  parseFloat(value.november),
                                  parseFloat(value.december),
                                  parseFloat(value.january),
                                  parseFloat(value.febuary),
                                  parseFloat(value.march),
                                  parseFloat(value.april),
                                  parseFloat(value.may),
                                  parseFloat(value.june),
                                  parseFloat(value.july),
                                  parseFloat(value.august),
                                  parseFloat(value.september)
                                ]
                            }
                        );
                    }, param);
                    jQuery('#chartEurofarInvoicedByMonth').highcharts({
                        title: {
                            text: 'COMMERCIAL INVOICE'
                        },
                        xAxis: {
                            categories: ['Oct', 'Nov', 'Dec', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep']
                        },
                        yAxis: {
                            title: {
                                text: 'Amount in Euro'
                            }
                        },
                        tooltip: {
                            formatter: function () {
                                return '<b>' + this.x + ' - ' + this.series.name + '</b>: &euro;' + $filter('currency')(this.y, '', 0);
                            },
                            useHTML: true
                        },
                        series: param.chartData
                    });

                    //CUMMULATIVE ALL PROFORMA
                    var param = {
                        chartData: [],
                        charSeries: []
                    }
                    angular.forEach($scope.data.allCummulatives, function (value, key) {
                        this.chartData.push(
                            {
                                name: value.season, data: [
                                  parseFloat(value.october),
                                  parseFloat(value.november),
                                  parseFloat(value.december),
                                  parseFloat(value.january),
                                  parseFloat(value.febuary),
                                  parseFloat(value.march),
                                  parseFloat(value.april),
                                  parseFloat(value.may),
                                  parseFloat(value.june),
                                  parseFloat(value.july),
                                  parseFloat(value.august),
                                  parseFloat(value.september)
                                ]
                            }
                        );
                    }, param);
                    jQuery('#chartCumulativeAllProforma').highcharts({
                        title: {
                            text: 'CUMMULATIVE PROFORMA INVOICE'
                        },
                        xAxis: {
                            categories: ['Oct', 'Nov', 'Dec', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep']
                        },
                        yAxis: {
                            title: {
                                text: 'Amount in Euro'
                            }
                        },
                        tooltip: {
                            formatter: function () {
                                return '<b>' + this.x + ' - ' + this.series.name + '</b>: &euro;' + $filter('currency')(this.y, '', 0);
                            },
                            useHTML: true
                        },
                        series: param.chartData
                    });

                    //CUMMULATIVE CONFIRMED PROFORMA
                    var param = {
                        chartData: [],
                        charSeries: []
                    }
                    angular.forEach($scope.data.confirmedCummulatives, function (value, key) {
                        this.chartData.push(
                            {
                                name: value.season, data: [
                                  parseFloat(value.october),
                                  parseFloat(value.november),
                                  parseFloat(value.december),
                                  parseFloat(value.january),
                                  parseFloat(value.febuary),
                                  parseFloat(value.march),
                                  parseFloat(value.april),
                                  parseFloat(value.may),
                                  parseFloat(value.june),
                                  parseFloat(value.july),
                                  parseFloat(value.august),
                                  parseFloat(value.september)
                                ]
                            }
                        );
                    }, param);
                    jQuery('#chartCumulativeConfirmedProforma').highcharts({
                        title: {
                            text: 'CUMMULATIVE PROFORMA INVOICE CONFIRMED'
                        },
                        xAxis: {
                            categories: ['Oct', 'Nov', 'Dec', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep']
                        },
                        yAxis: {
                            title: {
                                text: 'Amount in Euro'
                            }
                        },
                        tooltip: {
                            formatter: function () {
                                return '<b>' + this.x + ' - ' + this.series.name + '</b>: &euro;' + $filter('currency')(this.y, '', 0);
                            },
                            useHTML: true
                        },
                        series: param.chartData
                    });


                    //CUMMULATIVE COMMERCIAL INVOICE
                    var param = {
                        chartData: [],
                        charSeries: []
                    }
                    angular.forEach($scope.data.eurofarInvoicedCummulatives, function (value, key) {
                        this.chartData.push(
                            {
                                name: value.season, data: [
                                  parseFloat(value.october),
                                  parseFloat(value.november),
                                  parseFloat(value.december),
                                  parseFloat(value.january),
                                  parseFloat(value.febuary),
                                  parseFloat(value.march),
                                  parseFloat(value.april),
                                  parseFloat(value.may),
                                  parseFloat(value.june),
                                  parseFloat(value.july),
                                  parseFloat(value.august),
                                  parseFloat(value.september)
                                ]
                            }
                        );
                    }, param);
                    jQuery('#chartCumulativeEurofarInvoiced').highcharts({
                        title: {
                            text: 'CUMMULATIVE COMMERCIAL INVOICE'
                        },
                        xAxis: {
                            categories: ['Oct', 'Nov', 'Dec', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep']
                        },
                        yAxis: {
                            title: {
                                text: 'Amount in Euro'
                            }
                        },
                        tooltip: {
                            formatter: function () {
                                return '<b>' + this.x + ' - ' + this.series.name + '</b>: &euro;' + $filter('currency')(this.y, '', 0);
                            },
                            useHTML: true
                        },
                        series: param.chartData
                    });





                },
                function (error) {
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        }
    }

    //
    // method
    //
    $scope.method = {
        formatCurrency: function (text) {
            return formatCurrency(parseFloat(text), 0);
        },
        formatContainerNumber: function (text) {
            return formatCurrency(parseFloat(text), 1);
        },
        formatNumber: function (text, decimal) {
            return formatCurrency(parseFloat(text), decimal);
        },
        totalConfirmedContainer: function (item) {
            return $scope.method.formatContainerNumber(parseFloat(item.january) + parseFloat(item.febuary) + parseFloat(item.march) + parseFloat(item.april) + parseFloat(item.may)
                + parseFloat(item.june) + parseFloat(item.july) + parseFloat(item.august) + parseFloat(item.september) + parseFloat(item.october)
                + parseFloat(item.november) + parseFloat(item.december));
        }
    }


    //
    // init
    //
    $scope.event.init();
}]);