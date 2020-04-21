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
angular.module('tilsoftApp').controller('MiSaleConclusionRpt_ExpectedByCountryController', ['$scope', '$rootScope', '$timeout', '$filter', function ($scope, $rootScope, $timeout, $filter) {
    $scope.miSaleConclusionRpt = {};
    //
    // property
    //
    $scope.miSaleConclusionRpt.data = null;
    $scope.miSaleConclusionRpt.param = miSaleConclusionRpt_ExpectedByCountry_Context.param;

    $scope.miSaleConclusionRpt.filter = {
        clientCountryUD: '',
        clientCountryNM: ''        
    };

    $scope.miSaleConclusionRpt.sortValue = {
        colName: '-totalExpectedAmount'
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
                    angular.forEach($scope.miSaleConclusionRpt.data, function (item) {
                        if (item.totalExpectedAmount !== 0) {
                            params.data.push({ name: item.clientCountryNM, y: item.totalExpectedAmount });
                        }                        
                    });
                    jQuery('#miSaleConclusionRpt_ExpectedByCountry').highcharts({
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
                            text: 'Expected by country ' + $scope.miSaleConclusionRpt.param.season
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                size: '110%',
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
                    'Authorization': 'Bearer ' + miSaleConclusionRpt_ExpectedByCountry_Context.token
                },
                type: "POST",
                dataType: 'json',
                url: miSaleConclusionRpt_ExpectedByCountry_Context.serviceUrl + 'get-expected-by-country?season=' + season + '&saleID=-1',
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