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

                    //console.log(data);
                    //var weekIndexArr = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 51, 53, 54];

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
                            if(i == 1){
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
                        this.weekIndex.push(value.weekIndex);
                    }, param);

                    // combine
                    Highcharts.chart('chartTotalUser', {
                        title: {
                            text: 'TOTAL USER PER WEEK'
                        },
                        xAxis: {
                            categories: param.weekIndex
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
                            headerFormat: 'Week <b>{point.x}</b><br/>',
                            pointFormat: 'Total User: {point.total} <br/> {series.name}: {point.y} '
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
                    var param = {
                        chartData: { name: 'Total Hit Count', data: [] },
                        chartTarget: { name: 'Target', data: [] },
                        charSeries: []
                    }

                    var target = 0;
                    var keepGoing = true;
                    var i = 0;

                    angular.forEach($scope.data.hitCountDataRpt, function (value, key) {
                        i++;
                        if (keepGoing) {
                            target = parseInt(value.totalHit);
                            if (i == 1) {
                                keepGoing = false;
                            }
                        }
                    });     

                    angular.forEach($scope.data.hitCountDataRpt, function (value, key) {
                        this.charSeries.push(value.weekIndex);
                        this.chartData.data.push(parseInt(value.totalHit));
                        this.chartTarget.data.push(
                            target = target + (parseInt(target * 0.0125))
                        );
                    }, param);
                    
                    jQuery('#chartTotalHitCount').highcharts({
                        chart: {
                            type: 'column'
                        },
                        title: {
                            text: 'TOTAL HIT PER WEEK'
                        },
                        xAxis: {
                            categories: param.charSeries
                        },
                        yAxis: {
                            allowDecimals: false,
                            min: 0,
                            title: {
                                text: 'Total Hit Count Per Week'
                            },
                        },
                        tooltip: {
                            headerFormat: 'Week <b>{point.x}</b><br/>',
                            pointFormat: 'Hit Count: {point.y}'
                        },
                        plotOptions: {
                            column: {
                                stacking: 'normal'
                            }
                        },
                        series: [
                            param.chartData,
                        {
                            type: 'spline',
                            color: '#FF0000',
                            name: 'Target',
                            data: param.chartTarget.data,   
                            marker: {
                                lineWidth: 2,
                                lineColor: Highcharts.getOptions().colors[3],
                                fillColor: 'white'
                            }
                        }
                        ]
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
        sumDDCExpected: function (data) {
            var total = 0;
            if ($scope.data != null) {
                angular.forEach(data, function (value, key) {
                    total += value.ddcAmountUSD + value.ddcAmountEUR;
                }, null);
            }
            return total;
        }
    }


    //
    // init
    //
    $scope.event.init();
}]);