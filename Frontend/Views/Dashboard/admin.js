var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);

tilsoftApp.filter('sumFunc', function () {
    return function (data, key) {
        if (angular.isUndefined(data) || angular.isUndefined(key))
            return 0;
        var sum = parseFloat(0);
        angular.forEach(data, function (v, k) {
            sum = sum + parseFloat(v[key] === null ? 0 : v[key]);
        });
        return sum;
    };
});

tilsoftApp.controller('tilsoftController', ['$scope', '$rootScope', '$filter', '$interval', function ($scope, $rootScope, $filter, $interval) {
    //
    // property
    //
    $scope.data = null;
    $scope.MIdata = null;

    //
    //filter property
    //
    $scope.gridHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.data = [];
            dataService.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = scope.pageIndex;
            dataService.searchFilter.sortedBy = sortedBy;
            $scope.event.search();
        },
        isTriggered: false
    };

    // get short season
    var today = new Date();
    $scope.currentSeason = jsHelper.getCurrentSeason(false);
    $scope.nextSeason = jsHelper.getNextSeason(false);

    $scope.shortCurrentSeason = jsHelper.getCurrentSeason(true);
    $scope.shortNextSeason = jsHelper.getNextSeason($scope.currentSeason, true);

    $scope.shortSeason = $scope.shortCurrentSeason;
    $scope.tempSeason = $scope.currentSeason;
    if ((today.getMonth() + 1) === 8) {
        $scope.shortSeason = $scope.shortNextSeason;
        $scope.tempSeason = $scope.nextSeason;
    }
    else {
        $scope.shortSeason = $scope.shortCurrentSeason;
        $scope.tempSeason = $scope.currentSeason;
    }
    $scope.shortPrevSeason = jsHelper.getPrevSeason($scope.tempSeason, true);

    //
    // event
    //
    $scope.event = {
        init: function () {
            //
            // get mi sales proforma overview data
            //
            jsonService.getMIReportData(
                context.season,
                function (data) {
                    $scope.MIdata = data.data;
                    $scope.$apply();

                    //TOTAL CONTAINER CONFIRMED (By Season)
                    var param = {
                        chartData: { showInLegend: false, name: '', data: [] },
                        charSeries: []
                    }
                    angular.forEach($scope.MIdata.confirmedContainerPerSeasons, function (value, key) {
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
                    angular.forEach($scope.MIdata.confirmedContainerPerMonths, function (value, key) {
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
                    angular.forEach($scope.MIdata.allProformaByMonths, function (value, key) {
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
                    angular.forEach($scope.MIdata.confirmedProformaByMonths, function (value, key) {
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
                    angular.forEach($scope.MIdata.eurofarInvoicedPerMonths, function (value, key) {
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
                    angular.forEach($scope.MIdata.allCummulatives, function (value, key) {
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
                    angular.forEach($scope.MIdata.confirmedCummulatives, function (value, key) {
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
                    angular.forEach($scope.MIdata.eurofarInvoicedCummulatives, function (value, key) {
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

                    $('#pnlSalesOverviewLoading').hide();
                    $('#pnlSalesOverview').show();
                },
                function (error) {
                    $scope.MIdata = null;
                    $scope.$apply();
                }
            );
            
            //
            // get admin dashboard data
            //
            jsonService.getReportData(
                context.season,
                function (data) {
                    $scope.data = data.data;
                    $scope.$apply();

                    // CHART USER INCREASEMENT
                    var param = {
                        high: { name: 'High', data: [] },
                        intense: { name: 'Intense', data: [] },
                        aboveAverage: { name: 'Above Average', data: [] },
                        average: { name: 'Average', data: [] },
                        low: { name: 'Low', data: [] },
                        veryLow: { name: 'Very Low', data: [] },
                        none: { name: 'None', data: [] },
                        target: { name: 'Target', data: [] },
                        weekIndex: []
                    }

                    var target = 0;
                    var keepGoing = true;
                    var i = 0;

                    angular.forEach($scope.data.userDataRpt, function (value, key) {
                        i++;
                        if (keepGoing) {
                            target = parseInt(value.cHigh) + parseInt(value.cIntense) + parseInt(value.cAboveAverage) + parseInt(value.cAverage) + parseInt(value.cLow) + parseInt(value.cVeryLow) + parseInt(value.cNone);
                            if (i === 1) {
                                keepGoing = false;
                            }
                        }
                    });

                    angular.forEach($scope.data.userDataRpt, function (value, key) {
                        this.high.data.push(parseInt(value.cHigh));
                        this.intense.data.push(parseInt(value.cIntense));
                        this.aboveAverage.data.push(parseInt(value.cAboveAverage));
                        this.average.data.push(parseInt(value.cAverage));
                        this.low.data.push(parseInt(value.cLow));
                        this.veryLow.data.push(parseInt(value.cVeryLow));
                        this.none.data.push(parseInt(value.cNone));
                        this.target.data.push(
                            target = target + 1
                        );
                        this.weekIndex.push("Week " + value.weekIndex + "<br/>From " + value.weekStart + "<br/>To " + value.weekEnd);
                    }, param);

                    // combine
                    Highcharts.chart('chartTotalUser', {
                        title: {
                            text: 'TOTAL USER PER WEEK'
                        },
                        xAxis: {
                            categories: param.weekIndex,
                            labels: {
                                rotation: -45,
                            }
                        },
                        yAxis: {
                            min: 0,
                            title: {
                                text: 'User Increasement'
                            },
                            stackLabels: {
                                enabled: true,
                                style: {
                                    fontWeight: 'bold',
                                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                                }
                            }
                        },
                        legend: {
                            backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                            shadow: true
                        },
                        tooltip: {
                            headerFormat: '<b>{point.x}</b><br/>',
                            pointFormat: '<b>Total User: {point.total}</b><br/><b>{series.name}: {point.y}</b>'
                        },
                        plotOptions: {
                            column: {
                                stacking: 'normal',
                                dataLabels: {
                                    enabled: true,
                                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                                }
                            }
                        },
                        series: [
                            {
                                type: 'column',
                                color: '#33ff34',
                                name: 'High',
                                data: param.high.data
                            },
                            {
                                type: 'column',
                                color: '#3699fe',
                                name: 'Intense',
                                data: param.intense.data
                            },
                            {
                                type: 'column',
                                color: '#2675bf',
                                name: 'Above Average',
                                data: param.aboveAverage.data
                            },
                            {
                                type: 'column',
                                color: '#b228ed',
                                name: 'Average',
                                data: param.average.data
                            },
                            {
                                type: 'column',
                                color: '#ff8f4f',
                                name: 'Low',
                                data: param.low.data
                            },
                            {
                                type: 'column',
                                color: '#fff435',
                                name: 'Very Low',
                                data: param.veryLow.data
                            },
                            {
                                type: 'column',
                                color: '#969696',
                                name: 'None',
                                data: param.none.data
                            },
                            {
                                type: 'spline',
                                color: '#FF0000 ',
                                name: 'Target',
                                data: param.target.data,
                                marker: {
                                    lineWidth: 2,
                                    lineColor: Highcharts.getOptions().colors[3],
                                    fillColor: 'white'
                                }
                            }]
                    });


                    // CHART TOTAL HIT COUNT
                    var target = 0;
                    var keepGoing = true;
                    var i = 0;
                    angular.forEach($scope.data.hitCountDataRpt, function (item) {
                        i++;
                        if (keepGoing) {
                            target = parseInt(item.totalHit);
                            if (i === 1) {
                                keepGoing = false;
                            }
                        }
                    });

                    var isContinue = false;
                    var firstWeek = 0;
                    var categoryData = [];
                    var seriData = [{ name: 'Last Season', data: [], color: '#969696' }, { name: 'Current Season', data: [], color: '#3699fe' }, {
                                                                                                                                                    name: 'Target',
                                                                                                                                                    type: 'spline',
                                                                                                                                                    color: '#FF0000',
                                                                                                                                                    data: [],
                                                                                                                                                    marker: {
                                                                                                                                                        lineWidth: 2,
                                                                                                                                                        lineColor: Highcharts.getOptions().colors[3],
                                                                                                                                                        fillColor: 'white'
                                                                                                                                                    }
                                                                                                                                                }
                                    ];

                    angular.forEach($scope.data.hitCountDataRpt, function (value) {
                        if (firstWeek === 0) {
                            firstWeek = value.weekIndex;
                        }
                        if (value.weekIndex >= firstWeek && value.weekIndex <= 54) {
                            isContinue = true;
                        }
                        else {
                            isContinue = false;
                        }
                        if (isContinue) {
                            categoryData.push('W-' + value.weekIndex);
                            seriData[0].data.push(value.totalHitLast);
                            seriData[1].data.push(value.totalHit);

                            var index = $scope.data.hitCountDataRpt.indexOf(value);
                            if (index === 0) {
                                target = parseInt(target);
                            } else {
                                target = target + (parseInt(target * 0.0125));
                            }

                            seriData[2].data.push(target);
                        }
                    });

                    jQuery('#chartTotalHitCount').highcharts({
                        chart: {
                            type: 'column'
                        },
                        title: {
                            text: 'TOTAL HIT PER WEEK'
                        },
                        xAxis: {
                            type: 'category',
                            categories: categoryData
                        },
                        yAxis: {
                            allowDecimals: false,
                            min: 0,
                            title: {
                                text: 'Total Hit Count Per Week'
                            }
                        },
                        //tooltip: {
                        //    headerFormat: '<b>{point.x}</b><br/>',
                        //    pointFormat: '<b>Hit Count: {point.y}</b>'
                        //},
                        //plotOptions: {
                        //    column: {
                        //        stacking: 'normal'
                        //    }
                        //},
                        series: seriData
                            //[
                            //    seriData,
                            //    {
                            //        type: 'spline',
                            //        color: '#FF0000',
                            //        name: 'Target',
                            //        data: targetData.data,
                            //        marker: {
                            //            lineWidth: 2,
                            //            lineColor: Highcharts.getOptions().colors[3],
                            //            fillColor: 'white'
                            //        }
                            //    }
                            //]
                    });

                    $('#pnlSystemActivityLoading').hide();
                    $('#pnlSystemActivity').show();
                },
                function (error) {
                    $scope.data = null;
                    $scope.$apply();
                }
            );
                            
        }       
    };

    //
    // method
    //
    $scope.method = {
        sumDDCExpected: function (data) {
            var total = 0;
            if ($scope.data !== null) {
                angular.forEach(data, function (value, key) {
                    total += value.ddcAmountUSD + value.ddcAmountEUR;
                }, null);
            }
            return total;
        },
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
        },

        //
        //function use for list has margin (in this case we only user sale by client)
        //        
        setPanelVisible: function (id) {
            $('#' + id).find('.rpt-content').show();
            $('#' + id).find('.rpt-loading').hide();
        },
        getSum: function (data, field) {
            if (angular.isUndefined(data) || angular.isUndefined(field))
                return 0;
            var sum = parseFloat('0');
            angular.forEach(data, function (item) {
                sum = sum + parseFloat(item[field] === null ? 0 : item[field]);
            });
            return sum;
        },
        getSum2: function (data, field1, field2) {
            if (angular.isUndefined(data) || angular.isUndefined(field1) || angular.isUndefined(field2))
                return 0;
            var sum = parseFloat('0');
            angular.forEach(data, function (item) {
                sum = sum + parseFloat(item[field1] === null ? 0 : item[field1]) + parseFloat(item[field2] === null ? 0 : item[field2]);
            });
            return sum;
        },
        getPercent: function (data, item, field, decimal) {
            if (angular.isUndefined(data) || angular.isUndefined(field))
                return 0;
            if (angular.isUndefined(decimal)) decimal = 0;
            var sum = $scope.method.getSum(data, field);
            if (sum === 0) return 0;
            if (data.indexOf(item) === data.length - 1) {
                var percentSum = parseFloat(0);
                angular.forEach(data, function (sItem) {
                    if (sItem !== item) {
                        percentSum += jsHelper.round(parseFloat(sItem[field] === null ? 0 : sItem[field]) * 100 / sum, decimal);
                    }
                });
                if (100 - percentSum >= 0) {
                    return 100 - percentSum;
                }
                else {
                    return 0;
                }
            }
            else {
                return jsHelper.round(parseFloat(item[field] === null ? 0 : item[field]) * 100 / sum, decimal);
            }
        },
        getPercent2: function (data, item, field1, field2, decimal) {
            if (angular.isUndefined(data) || angular.isUndefined(field1) || angular.isUndefined(field2))
                return 0;
            if (angular.isUndefined(decimal)) decimal = 0;
            var sum = $scope.method.getSum2(data, field1, field2);
            if (sum === 0) return 0;
            if (data.indexOf(item) === data.length - 1) {
                var percentSum = parseFloat(0);
                angular.forEach(data, function (sItem) {
                    if (sItem !== item) {
                        percentSum += jsHelper.round((parseFloat(sItem[field1] === null ? 0 : sItem[field1]) + parseFloat(sItem[field2] === null ? 0 : sItem[field2])) * 100 / sum, decimal);
                    }
                });
                if (100 - percentSum >= 0) {
                    return 100 - percentSum;
                }
                else {
                    return 0;
                }
            }
            else {
                return jsHelper.round((parseFloat(item[field1] === null ? 0 : item[field1]) + parseFloat(item[field2] === null ? 0 : item[field2])) * 100 / sum, decimal);
            }
        },
        getPercent3: function (data, item, field, baseField, decimal) {
            if (angular.isUndefined(data) || angular.isUndefined(field) || angular.isUndefined(baseField))
                return 0;

            if (angular.isUndefined(decimal)) decimal = 0;
            if (parseFloat(item[baseField] === null ? 0 : item[baseField]) === 0) return 0;
            if (data.indexOf(item) === data.length - 1) {
                var percentSum = parseFloat(0);
                angular.forEach(data, function (sItem) {
                    if (sItem !== item && parseFloat(sItem[baseField] === null ? 0 : sItem[baseField]) > 0) {
                        percentSum += jsHelper.round(parseFloat(sItem[field] === null ? 0 : sItem[field]) * 100 / parseFloat(sItem[baseField] === null ? 0 : sItem[baseField]), decimal);
                    }
                });
                if (100 - percentSum >= 0) {
                    return 100 - percentSum;
                }
                else {
                    return 0;
                }
            }
            else {
                return jsHelper.round(parseFloat(item[field] === null ? 0 : item[field]) * 100 / parseFloat(item[baseField] === null ? 0 : item[baseField]), decimal);
            }
        },
        getPercent4: function (data, item, field, baseField1, baseField2, decimal) {
            if (angular.isUndefined(data) || angular.isUndefined(field) || angular.isUndefined(baseField1) || angular.isUndefined(baseField2))
                return 0;
            if (angular.isUndefined(decimal)) decimal = 0;
            if ((parseFloat(item[baseField1] === null ? 0 : item[baseField1]) + parseFloat(item[baseField2] === null ? 0 : item[baseField2])) === 0) return 0;
            if (data.indexOf(item) === data.length - 1) {
                var percentSum = parseFloat(0);
                angular.forEach(data, function (sItem) {
                    if (sItem !== item && (parseFloat(sItem[baseField1] === null ? 0 : sItem[baseField1]) + parseFloat(sItem[baseField2] === null ? 0 : sItem[baseField2])) > 0) {
                        percentSum += jsHelper.round(parseFloat(sItem[field] === null ? 0 : sItem[field]) * 100 / (parseFloat(sItem[baseField1] === null ? 0 : sItem[baseField1]) + parseFloat(sItem[baseField2] === null ? 0 : sItem[baseField2])), decimal);
                    }
                });
                if (100 - percentSum >= 0) {
                    return 100 - percentSum;
                }
                else {
                    return 0;
                }
            }
            else {
                return jsHelper.round(parseFloat(item[field] === null ? 0 : item[field]) * 100 / (parseFloat(item[baseField1] === null ? 0 : item[baseField1]) + parseFloat(item[baseField2] === null ? 0 : item[baseField2])), decimal);
            }
        },

        sortData: function (key, field) {
            var currentSetting = $scope.sortValue[key];
            if (currentSetting.replace('-', '') === field) {
                if (currentSetting.indexOf('-') < 0) {
                    $scope.sortValue[key] = '-' + field;
                }
                else {
                    $scope.sortValue[key] = field;
                }
            }
            else {
                $scope.sortValue[key] = field;
            }
        },        
    };

    //
    // init
    //
    $scope.event.init();    
    
}]);