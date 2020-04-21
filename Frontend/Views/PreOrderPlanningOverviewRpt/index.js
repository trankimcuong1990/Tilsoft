(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
    angular.module('tilsoftApp').controller('tilsoftController', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
        dataService.searchFilter.pageSize = context.pageSize;
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;

        //
        // property
        //
        $scope.data = null;

        //
        // events
        //
        $scope.event = {
            init: function () {
                dataService.gethtmldata(
                    function (data) {
                        $scope.data = data.data;

                        $timeout(function () {
                            $scope.method.drawChartBeforeTet();
                            $scope.method.drawChartAfterTet();
                        }, 100);
                    },
                    function (error) {
                        $scope.data = null;
                    }
                );

                $('#content').show();
            },
            search: function () {
                dataService.gethtmldata(
                    function (data) {
                        $scope.data = data.data;

                        $timeout(function () {
                            $scope.method.drawChartBeforeTet();
                            $scope.method.drawChartAfterTet();
                        }, 100);
                    },
                    function (error) {
                        $scope.data = null;
                    }
                );
            }
        }

        //
        // method
        //
        $scope.method = {
            drawChartBeforeTet: function () {
                var beforeChart = Highcharts.chart('preOrderPlanningBeforeTet', {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: 'Pre order planning before Tet holiday'
                    },
                    subtitle: {
                        text: ''
                    },
                    xAxis: {
                        type: 'category',
                        labels: {
                            rotation: -90
                        }
                    },
                    yAxis: {
                        title: {
                            text: 'Delta'
                        },
                        labels: {
                            format: '{value:,.0f}'
                        }
                    },
                    legend: {
                        enabled: false
                    },
                    plotOptions: {
                        series: {
                            borderWidth: 0,
                            dataLabels: {
                                enabled: true
                            }
                        }
                    },
                    tooltip: {
                        headerFormat: '',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:,.2f}</b>'
                    },
                    series: [{
                        name: '',
                        data: []
                    }]
                });

                var beforeChartSeriData = [];
                angular.forEach($scope.data.reportDetailDTOs, function (item) {
                    // processing seri data
                    beforeChartSeriData.push({ name: item.factoryUD, y: item.deltaB, color: (item.deltaB < 0) ? '#ff0000' : '#0373fc' });
                });
                beforeChart.series[0].setData(beforeChartSeriData, true);
                beforeChart.redraw();
            },
            drawChartAfterTet: function () {
                var afterChart = Highcharts.chart('preOrderPlanningAfterTet', {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: 'Pre order planning after Tet holiday'
                    },
                    subtitle: {
                        text: ''
                    },
                    xAxis: {
                        type: 'category',
                        labels: {
                            rotation: -90
                        }
                    },
                    yAxis: {
                        title: {
                            text: 'Delta'
                        },
                        labels: {
                            format: '{value:,.0f}'
                        }
                    },
                    legend: {
                        enabled: false
                    },
                    plotOptions: {
                        series: {
                            borderWidth: 0,
                            dataLabels: {
                                enabled: true
                            }
                        }
                    },
                    tooltip: {
                        headerFormat: '',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:,.2f}</b>'
                    },
                    series: [{
                        name: '',
                        data: []
                    }]
                });

                var afterChartSeriData = [];
                angular.forEach($scope.data.reportDetailDTOs, function (item) {
                    // processing seri data
                    afterChartSeriData.push({ name: item.factoryUD, y: item.deltaA, color: (item.deltaA < 0) ? '#ff0000' : '#0373fc' });
                });
                afterChart.series[0].setData(afterChartSeriData, true);
                afterChart.redraw();
            }
        };


        //
        // angular init
        //
        $timeout(function () {
            $scope.event.init();
        }, 0);
    }]);

})();