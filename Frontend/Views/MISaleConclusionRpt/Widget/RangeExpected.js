// object name  : miSaleConclusionRpt
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
angular.module('tilsoftApp').controller('MiSaleConclusionRpt_RangeExpectedController', ['$scope', '$rootScope', '$timeout', '$filter', function ($scope, $rootScope, $timeout, $filter) {
    $scope.miSaleConclusionRpt = {};
    //
    // property
    //
    $scope.miSaleConclusionRpt.data = null;
    $scope.miSaleConclusionRpt.param = miSaleConclusionRpt_RangeExpected_Context.param;

    $scope.miSaleConclusionRpt.filter = {            
    };

    $scope.miSaleConclusionRpt.sortValue = {
        colName: '-totalClient'
    };

    //
    // events
    //
    $scope.miSaleConclusionRpt.event = {
        init: function () {
            $scope.miSaleConclusionRpt.method.getData(
                $scope.miSaleConclusionRpt.param.season,
                function (data) {
                    $scope.miSaleConclusionRpt.data = data.data;
                    $scope.$apply();

                    //pie chart
                    var params = {
                        data: []
                    };
                    angular.forEach($scope.miSaleConclusionRpt.data, function (value, key) {
                        this.data.push({ name: $filter('currency')(value.totalClient, '', 0) + ' clients (' + $filter('currency')(value.totalAmount, '', 0) + ')', y: value.totalAmount });
                    }, params);
                    jQuery('#miSaleConclusionRpt_RangeExpected').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'pie'
                        },
                        credits: {
                            enabled: false
                        },
                        title: {
                            text: ''
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: true,
                                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                    style: {
                                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                                    }
                                }
                            }
                        },
                        series: [{
                            name: 'Percent',
                            colorByPoint: true,
                            data: params.data
                        }]
                    });
                
                
                },
                function (error) {

                });
        }
    };


    $scope.miSaleConclusionRpt.method = {
        getData: function (season, successCallBack, errorCallBack) {
            $.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + miSaleConclusionRpt_RangeExpected_Context.token
                },
                type: "POST",
                dataType: 'json',
                url: miSaleConclusionRpt_RangeExpected_Context.serviceUrl + 'get-range-expected?season=' + season + '&saleID=-1',
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

        sortData: function (key, field) {
            var currentSetting = $scope.miSaleConclusionRpt.sortValue[key];
            if (currentSetting.replace('-', '') === field) {
                if (currentSetting.indexOf('-') < 0) {
                    $scope.miSaleConclusionRpt.sortValue[key] = '-' + field;
                }
                else {
                    $scope.miSaleConclusionRpt.sortValue[key] = field;
                }
            }
            else {
                $scope.miSaleConclusionRpt.sortValue[key] = field;
            }
        }
    };

    //
    // angular init
    //
    $timeout(function () {
        $scope.miSaleConclusionRpt.event.init();
    }, 0);
    
}]);