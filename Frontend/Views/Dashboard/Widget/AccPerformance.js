angular.module('tilsoftApp').controller('performanceController', ['$scope', '$rootScope', '$timeout', '$filter', '$http', function ($scope, $rootScope, $timeout, $filter, $http) {
    //
    // property
    //
    $scope.performance = {};
    $scope.performance.saleId = null;
    $scope.performance.supportData = {
        sales: []
    };
    $scope.performance.data = [];
    $scope.performance.delta = {
        data: [],
        seasons: []
    };
    $scope.performance.saleAmount = {
        data: [],
        seasons: []
    };
    $scope.performance.deltaAmount = {
        data: [],
        seasons: []
    };

    //
    // grid handler
    //
    $scope.performance.nullHandler = {
        loadMore: function () { },
        sort: function (sortedBy, sortedDirection) { },
        isTriggered: false
    };

    //
    // listen to parent scope
    //
    //$rootScope.$on('performance', function () {
    //    $scope.performance.method.loadData();
    //});

    //
    // method
    //
    $scope.performance.method = {
        loadData: function () {
            $scope.performance.dataService.getInit(
                function (initData) {
                    $scope.performance.supportData.sales = initData.data.saleDTOs;

                    $scope.performance.data = [];
                    $scope.performance.dataService.getAccPerformance(
                        'pnlPerformance',
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.performance.data = data.data.accManagerPerformanceDTOs;

                                $timeout(function () {
                                    //
                                    // generate chart performance
                                    //
                                    var series = [];
                                    var seasons = [];
                                    var expectationSeri = {
                                        name: 'Expectation',
                                        color: jsHelper.getDefaultColor('expectation'),
                                        data: []
                                    };
                                    var ciSeri = {
                                        name: 'C/I or P/I confirmed',
                                        color: jsHelper.getDefaultColor('ci'),
                                        data: []
                                    };
                                    var piConfirmedDeltaSeri = {
                                        name: 'Delta 6',
                                        color: jsHelper.getDefaultColor('delta'),
                                        data: []
                                    };
                                    var piDeltaSeri = {
                                        name: '% Delta 6',
                                        color: jsHelper.getDefaultColor('%delta'),
                                        type: 'spline',
                                        data: [],
                                        yAxis: 1
                                    };
                                    var piAVGDeltaSeri = {
                                        name: '% AVG Delta 6',
                                        color: jsHelper.getDefaultColor('%avgdelta'),
                                        type: 'spline',
                                        data: [],
                                        yAxis: 1
                                    };

                                    var sortedArray = angular.copy($scope.performance.data).sort(function (a, b) {
                                        var aSeason = a.season;
                                        var bSeason = b.season;
                                        return aSeason < bSeason ? -1 : ((aSeason > bSeason) ? 1 : 0);
                                    });

                                    angular.forEach(sortedArray, function (item) {
                                        seasons.push(item.season);

                                        expectationSeri.data.push(item.expectationTotalEUR);
                                        if (item.season === jsHelper.getCurrentSeason()) {
                                            ciSeri.data.push(item.piConfirmedTotalEUR);
                                        }
                                        else {
                                            ciSeri.data.push(item.ciTotalEUR);
                                        }
                                        piConfirmedDeltaSeri.data.push(item.piConfirmedDelta6EUR);
                                        piDeltaSeri.data.push(item.delta6Percent);
                                        piAVGDeltaSeri.data.push(item.avgDelta6Percent);
                                    });
                                    series.push(expectationSeri);
                                    series.push(ciSeri);
                                    series.push(piConfirmedDeltaSeri);
                                    series.push(piDeltaSeri);
                                    series.push(piAVGDeltaSeri);

                                    $('#chart-performance').highcharts({
                                        chart: {
                                            type: 'column'
                                        },

                                        title: {
                                            text: 'COMMERCIAL INVOICE & DELTA 6'
                                        },
                                        xAxis: {
                                            categories: seasons
                                        },
                                        yAxis: [
                                            {
                                                title: {
                                                    text: 'Amount In EUR'
                                                }
                                            },
                                            {
                                                labels: {
                                                    format: '{value}%'
                                                },
                                                title: {
                                                    text: '% Delta 6'
                                                },
                                                opposite: true
                                            }
                                        ],
                                        tooltip: {
                                            formatter: function () {
                                                if (this.series.name.indexOf('%') >= 0) {
                                                    return '<b>' + this.series.name + ' (' + this.x + '):</b> ' + $filter('number')(this.y, 1) + '%';
                                                }
                                                else {
                                                    return '<b>' + this.series.name + ' (' + this.x + '):</b> ' + $filter('currency')(this.y, '&euro;', 2);
                                                }
                                            },
                                            useHTML: true
                                        },
                                        series: series
                                    });


                                    //
                                    // generate chart delta & delta data detail
                                    //
                                    if ($scope.performance.saleId === -1) {
                                        var deltaSeries = [];
                                        var saleAmountSeries = [];
                                        var deltaAmountSeries = [];
                                        var deltaCat = angular.copy(seasons);
                                        deltaCat.splice(seasons.indexOf('2013/2014'), 1);
                                        angular.forEach($scope.performance.supportData.sales.filter(o => o.userID !== 23), function (sale) {
                                            var deltaSeri = {
                                                name: sale.employeeNM,
                                                data: []
                                            };
                                            var saleAmountSerie = {
                                                name: sale.employeeNM,
                                                data: []
                                            };
                                            var deltaAmountSerie = {
                                                name: sale.employeeNM,
                                                data: []
                                            };
                                            angular.forEach(deltaCat, function (season) {
                                                var itemResults = data.data.accManagerPerformanceDeltaDTOs.filter(o => o.season === season && o.saleID === sale.userID);
                                                if (itemResults.length > 0) {
                                                    deltaSeri.data.push(itemResults[0].delta6Percent);
                                                    saleAmountSerie.data.push(itemResults[0].saleAmountEUR);
                                                    deltaAmountSerie.data.push(itemResults[0].delta6AmountEUR);
                                                }
                                                else {
                                                    deltaSeri.data.push(null);
                                                    saleAmountSerie.data.push(null);
                                                    deltaAmountSerie.data.push(null);
                                                }
                                            });
                                            deltaSeries.push(deltaSeri);
                                            saleAmountSeries.push(saleAmountSerie);
                                            deltaAmountSeries.push(deltaAmountSerie);
                                        });

                                        // delta %
                                        $scope.performance.delta.data = deltaSeries;
                                        $scope.performance.delta.seasons = deltaCat;
                                        $('#chart-delta').highcharts({
                                            chart: {
                                                type: 'spline'
                                            },

                                            title: {
                                                text: 'DELTA 6 (based on P/I confirmed)'
                                            },
                                            xAxis: {
                                                categories: deltaCat
                                            },
                                            yAxis: [
                                                {
                                                    title: {
                                                        text: '% Delta 6'
                                                    }
                                                }
                                            ],
                                            tooltip: {
                                                formatter: function () {
                                                    return '<b>' + this.series.name + ' (' + this.x + '):</b> ' + $filter('number')(this.y, 1) + '%';
                                                },
                                                useHTML: true
                                            },
                                            series: deltaSeries
                                        });
                                        $('#chart-delta').show();
                                        $('#table-delta').show();

                                        //// sale amount EUR
                                        //$scope.performance.saleAmount.data = saleAmountSeries;
                                        //$scope.performance.saleAmount.seasons = deltaCat;
                                        //$('#chart-saleAmountEUR').highcharts({
                                        //    chart: {
                                        //        type: 'spline'
                                        //    },

                                        //    title: {
                                        //        text: 'Sales Amount in EUR (based on P/I confirmed)'
                                        //    },
                                        //    xAxis: {
                                        //        categories: deltaCat
                                        //    },
                                        //    yAxis: [
                                        //        {
                                        //            title: {
                                        //                text: 'Sales Amount in EUR'
                                        //            }
                                        //        }
                                        //    ],
                                        //    tooltip: {
                                        //        formatter: function () {
                                        //            return '<b>' + this.series.name + ' (' + this.x + '):</b> ' + $filter('currency')(this.y, '&euro;', 0);
                                        //        },
                                        //        useHTML: true
                                        //    },
                                        //    series: saleAmountSeries
                                        //});
                                        //$('#chart-saleAmountEUR').show();
                                        //$('#table-saleAmountEUR').show();

                                        //// sale amount EUR
                                        //$scope.performance.deltaAmount.data = deltaAmountSeries;
                                        //$scope.performance.deltaAmount.seasons = deltaCat;
                                        //$('#chart-deltaAmountEUR').highcharts({
                                        //    chart: {
                                        //        type: 'spline'
                                        //    },

                                        //    title: {
                                        //        text: 'Delta Amount in EUR (based on P/I confirmed)'
                                        //    },
                                        //    xAxis: {
                                        //        categories: deltaCat
                                        //    },
                                        //    yAxis: [
                                        //        {
                                        //            title: {
                                        //                text: 'Delta Amount in EUR'
                                        //            }
                                        //        }
                                        //    ],
                                        //    tooltip: {
                                        //        formatter: function () {
                                        //            return '<b>' + this.series.name + ' (' + this.x + '):</b> ' + $filter('currency')(this.y, '&euro;', 0);
                                        //        },
                                        //        useHTML: true
                                        //    },
                                        //    series: deltaAmountSeries
                                        //});
                                        //$('#chart-deltaAmountEUR').show();
                                        //$('#table-deltaAmountEUR').show();
                                    }
                                    else {
                                        $('#chart-delta').hide();
                                        $('#table-delta').hide();
                                        $('#chart-saleAmountEUR').hide();
                                        $('#table-saleAmountEUR').hide();
                                        $('#chart-deltaAmountEUR').hide();
                                        $('#table-deltaAmountEUR').hide();
                                    }
                                }, 100);
                            }
                        },
                        function (error) {
                            console.log(error);
                            $scope.performance.data = [];
                        }
                    );
                },
                function (initError) {
                    console.log(initError);
                }
            );
        }
    };   

    $scope.performance.dataService = {
        getAccPerformance: function (elemId, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitchExt(true, elemId);

            $http({
                method: 'POST',
                url: context.tilsoftServiceUrl + 'acc-manager-performance/get-acc-performance?saleId=' + $scope.performance.saleId,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + context.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitchExt(false, elemId);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage('warning', response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitchExt(false, elemId);

                jsHelper.showMessage('warning', response.data.message);
                iErrorCallback(response);
            });
        },
        getInit: function (iSuccessCallback, iErrorCallback) {
            $http({
                method: 'POST',
                url: context.tilsoftServiceUrl + 'acc-manager-performance/getinitdata',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + context.token
                }
            }).then(function successCallback(response) {
                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage('warning', response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.showMessage('warning', response.data.message);
                iErrorCallback(response);
            });
        }
    };

}]);