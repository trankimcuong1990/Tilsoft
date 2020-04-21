//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', 'detailService', function ($scope, dataService, detailService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.years = [];
    $scope.filters = {
        year: 2017
    };
    $scope.data = [];

    $scope.detailService = detailService;
    $scope.detailService.parentScope = $scope;

    //
    // event
    //
    $scope.event = {
        init: function () {
            var currentDate = new Date();
            for (var i = 2017; i <= currentDate.getFullYear() ; i++) {
                $scope.years.push({text: i, value: i});
            }
            $scope.filters.year = currentDate.getFullYear();
            $scope.event.load();
        },
        load: function () {
            dataService.getOverview(
                $scope.filters.year,
                function (data) {
                    $scope.data = data.data.overviews;
                    jQuery('#content').show();

                    // re-generate chart
                    var devs = [];
                    var resolveRequests = {
                        name: 'Resolved requests',
                        data: []
                    };
                    var pendingRequests = {
                        name: 'Pending requests',
                        data: []
                    };
                    angular.forEach($scope.data, function (item) {
                        devs.push(item.employeeNM);
                        resolveRequests.data.push(item.totalResolvedRequest);
                        pendingRequests.data.push(item.totalPendingRequest);
                    }, null);

                    Highcharts.chart('chartOverview', {
                        chart: {
                            type: 'column'
                        },
                        title: {
                            text: 'Overview'
                        },
                        xAxis: {
                            categories: devs
                        },
                        yAxis: {
                            min: 0,
                            title: {
                                text: ''
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
                            align: 'right',
                            x: -30,
                            verticalAlign: 'top',
                            y: 25,
                            floating: true,
                            backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                            borderColor: '#CCC',
                            borderWidth: 1,
                            shadow: false
                        },
                        tooltip: {
                            headerFormat: '<b>{point.x}</b><br/>',
                            pointFormat: '{series.name}: {point.y}<br/>Total: {point.stackTotal}'
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
                        series: [resolveRequests, pendingRequests]
                    });
                },
                function (error) {
                    $scope.data = [];
                }
            );
        }
    }

    //
    // method
    //


    //
    // init
    //
    $scope.event.init();
}]);