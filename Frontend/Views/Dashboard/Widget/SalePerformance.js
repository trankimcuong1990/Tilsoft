    angular.module('tilsoftApp').filter('sumFunc', function () {
        return function (data, field) {
            if (angular.isUndefined(data) || angular.isUndefined(field))
                return 0;
            var sum = parseFloat('0');
            angular.forEach(data, function (v, k) {
                sum = sum + parseFloat(v[field] === null ? 0 : v[field]);
            });
            return sum;
        };
    });
angular.module('tilsoftApp').controller('SalePerformanceController', ['$scope', '$rootScope', '$timeout', '$filter', function ($scope, $rootScope, $timeout, $filter) {
    //
    // property
    //
    $scope.salePerformance = null;
    $scope.param = salePerformanceContext.param;    
    $scope.isLoaded = false;

    //
    // events
    //
    $scope.event = {
        init: function () {
            $scope.method.getSalePerformance(
                $scope.param.season,
                function (data) {
                    $scope.salePerformance = data.data;                   
                    $scope.$apply();

                    //pi confirmed chart
                    $scope.method.buildPIConfirmedChart();

                    //pi total chart
                    $scope.method.buildPITotalChart();
                },
                function (error) {

                });                                 
        },            
    };


    $scope.method = {
        getSalePerformance: function (season, successCallBack, errorCallBack) {
            $.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + salePerformanceContext.token
                },
                type: "POST",
                dataType: 'json',
                url: salePerformanceContext.serviceUrl + 'get-admin-dashboard?season=' + season,
                beforeSend: function () {
                    //jsHelper.loadingSwitch(true);
                },
                success: function (data) {
                    //jsHelper.loadingSwitch(false);
                    successCallBack(data);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    //jsHelper.loadingSwitch(false);
                    errorCallBack(xhr.responseJSON.exceptionMessage);
                }
            });
        },                           

        buildPIConfirmedChart: function () {
            //
            //stack chart data PI Confirmed
            //
            var chartSalePerformanceData = {
                categories: [
                    'P/I CONF IN $',
                    'P/I CONF Δ6 IN $',
                    'P/I CONF Δ6 IN %'
                ],
                series: []
            };
            var chartData = angular.copy($scope.salePerformance);
            chartData = chartData.sort(function (a, b) { return a.piConfirmedSaleAmountThisYear - b.piConfirmedSaleAmountThisYear; });
            angular.forEach(chartData, function (item) {
                chartSalePerformanceData.series.push({
                    name: item.saleUD,
                    color: item.defaultColor,
                    data: [
                        jsHelper.round(item.piConfirmedSaleAmountThisYear, 0),
                        jsHelper.round(item.piConfirmedDelta5AmountThisYear, 0),
                        jsHelper.round(item.piConfirmedDelta5PercentThisYear, 1)
                    ]
                });
            });

            //bind to chart
            Highcharts.chart('chartSalePerformance', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'P/I CONF IN $ - P/I CONF Δ6 IN $ - P/I CONF Δ6 IN %'
                },
                xAxis: {
                    categories: chartSalePerformanceData.categories
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Total Percent'
                    }
                },
                tooltip: {
                    formatter: function () {
                        if (this.x === 'P/I CONF Δ6 IN %') {
                            return '<span>' + this.series.name + ': ' + this.y + '%' + '</span>';
                        }
                        else {
                            return '<span>' + this.series.name + ': ' + $filter('currency')(this.y, '$', 0) + '</span>';
                        }
                    }
                },
                plotOptions: {
                    column: {
                        stacking: 'percent',
                        dataLabels: {
                            enabled: true,
                            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'black',
                            formatter: function () {
                                if (this.x === 'P/I CONF Δ6 IN %') {
                                    return '<span>' + this.series.name + ': ' + this.y + '%' + '</span>';
                                }
                                else {
                                    return '<span>' + this.series.name + ': ' + $filter('currency')(this.y, '$', 0) + ' (' + Highcharts.numberFormat(this.percentage, 1) + '%)' + '</span>';
                                }
                            }
                        }
                    }
                },
                series: chartSalePerformanceData.series
            });
        },

        buildPITotalChart: function () {
            //
            //stack chart data PI Confirmed
            //
            var chartSalePerformanceData = {
                categories: [
                    'P/I IN $',
                    'P/I Δ6 IN $',
                    'P/I Δ6 IN %'
                ],
                series: []
            };
            var chartData = angular.copy($scope.salePerformance);
            chartData = chartData.sort(function (a, b) { return a.piSaleAmountThisYear - b.piSaleAmountThisYear; });
            angular.forEach(chartData, function (item) {
                chartSalePerformanceData.series.push({
                    name: item.saleUD,
                    color: item.defaultColor,
                    data: [
                        jsHelper.round(item.piSaleAmountThisYear, 0),
                        jsHelper.round(item.piDelta5AmountThisYear, 0),
                        jsHelper.round(item.piDelta5PercentThisYear, 1)
                    ]
                });
            });

            //bind to chart
            Highcharts.chart('chartSalePerformanceBaseOnPI', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'P/I IN $ - P/I Δ6 IN $ - P/I Δ6 IN %'
                },
                xAxis: {
                    categories: chartSalePerformanceData.categories
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Total Percent'
                    }
                },
                tooltip: {
                    formatter: function () {
                        if (this.x === 'P/I Δ6 IN %') {
                            return '<span>' + this.series.name + ': ' + this.y + '%' + '</span>';
                        }
                        else {
                            return '<span>' + this.series.name + ': ' + $filter('currency')(this.y, '$', 0) + '</span>';
                        }
                    }
                },
                plotOptions: {
                    column: {
                        stacking: 'percent',
                        dataLabels: {
                            enabled: true,
                            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'black',
                            formatter: function () {
                                if (this.x === 'P/I Δ6 IN %') {
                                    return '<span>' + this.series.name + ': ' + this.y + '%' + '</span>';
                                }
                                else {
                                    return '<span>' + this.series.name + ': ' + $filter('currency')(this.y, '$', 0) + ' (' + Highcharts.numberFormat(this.percentage, 1) + '%)' + '</span>';
                                }
                            }
                        }
                    }
                },
                series: chartSalePerformanceData.series
            });
        }
    };

    //
    // angular init
    //
    $timeout(function () {
        $scope.isLoaded = true;
        $scope.event.init();
    }, 0);

    //$rootScope.$on('saleperformance123', function () {
    //    alert(1);
    //    //$scope.isLoaded = true;
    //    $scope.event.init();
    //});
}]);