
angular.module('tilsoftApp').controller('deltaChartController', ['$scope', '$rootScope', '$timeout', '$filter', '$interval', function ($scope, $rootScope, $timeout, $filter, $interval) {
    $scope.deltaChart = {};
    //
    // property
    //    
    $scope.deltaChart.data = [];
    $scope.deltaChart.season = jsHelper.getCurrentSeason();
    //
    // event
    //
    $scope.deltaChart.event = {
        init: function () {
            $timeout(function () {
                $scope.deltaChart.event.generateHtml();
            }, 0);
        },
        generateHtml: function () {
            //get data from server

            $scope.deltaChart.method.getDataReport(
                $scope.deltaChart.season,
                function (data) {
                    $scope.deltaChart.data = data.data;

                    jQuery('#chartDetal').highcharts({
                        chart: {
                            type: "spline"
                        },
                        title: {
                            text: 'DELTA CHART ' + $scope.deltaChart.season
                        },
                        xAxis: {
                            categories: $scope.deltaChart.data.months
                            //categories: ['Oct', 'Nov', 'Dec', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep']
                        },
                        yAxis: {
                            title: {
                                text: 'Delta ' + $scope.deltaChart.season
                            }
                        },
                        tooltip: {
                            valueSuffix: " USD"
                            //formatter: function () {
                            //    return '<b>' + this.x + ' - ' + this.series.name + '</b>: &euro;' + $filter('currency')(this.y, '', 0);
                            //},
                            //useHTML: true
                        },
                        series: $scope.deltaChart.data.data
                        //series: [
                        //    {
                        //        name: 'Tokyo',
                        //        data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]
                        //    },
                        //    {
                        //        name: 'New York',
                        //        data: [-0.2, 0.8, 5.7, 11.3, 17.0, 22.0, 24.8, 24.1, 20.1, 14.1, 8.6, 2.5]
                        //    },
                        //    {
                        //        name: 'Berlin',
                        //        data: [-0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0]
                        //    },
                        //    {
                        //        name: 'London',
                        //        data: [3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
                        //    }
                        //]
                    });

                    $('#widget-deltaChart-loading').hide();
                    $('#widget-deltaChart-container').show();
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    console.log(error);
                    $scope.deltaChart.data = null;
                    $scope.$apply();
                }
            );
        }
    };

    //
    // method
    //
    $scope.deltaChart.method = {

        getHeader: function () {
            var result = [];
            var indexs = [];
            angular.forEach($scope.deltaChart.totalCapacityAndOrderByWeekDatas, function (item) {
                if (indexs.indexOf(item.weekIndex) < 0) {
                    indexs.push(item.weekIndex);
                    result.push({
                        weekIndex: item.weekIndex,
                        weekStart: item.weekStart,
                        weekEnd: item.weekEnd,
                    });
                }
            });
            return result;
        },

        getDataReport: function (season, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + deltaChartContext.token
                },
                type: "POST",
                dataType: 'json',
                url: deltaChartContext.serviceUrl + 'dashboarddetal?season=' + season,
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
        }
    };


    //auto load after 15'
    $interval(function () {
        $scope.deltaChart.event.init();
    }, 900000); //15' refesh auto

    $scope.deltaChart.event.init();


}]);