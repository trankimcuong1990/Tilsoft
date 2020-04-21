// object name  : miSaleByItemRpt
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
angular.module('tilsoftApp').controller('MISaleByItemRpt_PiConfirmedController', ['$scope', '$rootScope', '$timeout', '$filter', function ($scope, $rootScope, $timeout, $filter) {
    $scope.miSaleByItemRpt = {};
    //
    // property
    //
    $scope.miSaleByItemRpt.piConfirmeds = null;
    $scope.miSaleByItemRpt.param = miSaleByItemRpt_PiConfirmed_Context.param;

    $scope.miSaleByItemRpt.filter = {
        productCategoryNM: ''
    };

    $scope.miSaleByItemRpt.sortValue = {
        colName: '-saleAmountUSD'
    };

    //
    // events
    //
    $scope.miSaleByItemRpt.event = {
        init: function () {
            $scope.miSaleByItemRpt.method.getPiConfirmedByItemCategory(
                $scope.miSaleByItemRpt.param.season,
                function (data) {
                    $scope.miSaleByItemRpt.piConfirmeds = data.data;
                    $scope.$apply();

                    //chart
                    var params = {
                        data: []
                    };
                    angular.forEach($scope.miSaleByItemRpt.piConfirmeds, function (item) {
                        params.data.push({ name: item.productCategoryNM, y: item.saleAmountUSD });
                    });
                    jQuery('#piConfirmedPieChart').highcharts({
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
                            text: 'ProformaInvoice Confirmed by item category ' + $scope.miSaleByItemRpt.param.season
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


    $scope.miSaleByItemRpt.method = {
        getPiConfirmedByItemCategory: function (season, successCallBack, errorCallBack) {
            $.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + miSaleByItemRpt_PiConfirmed_Context.token
                },
                type: "POST",
                dataType: 'json',
                url: miSaleByItemRpt_PiConfirmed_Context.serviceUrl + 'get-piconfirmed-by-itemcategory?season=' + season,
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
            var currentSetting = $scope.miSaleByItemRpt.sortValue[key];
            if (currentSetting.replace('-', '') === field) {
                if (currentSetting.indexOf('-') < 0) {
                    $scope.miSaleByItemRpt.sortValue[key] = '-' + field;
                }
                else {
                    $scope.miSaleByItemRpt.sortValue[key] = field;
                }
            }
            else {
                $scope.miSaleByItemRpt.sortValue[key] = field;
            }
        }
    };

    //
    // angular init
    //
    $timeout(function () {
        $scope.miSaleByItemRpt.isLoaded = true;
        $scope.miSaleByItemRpt.event.init();
    }, 0);

    //$rootScope.$on('saleperformance123', function () {
    //    alert(1);
    //    //$scope.isLoaded = true;
    //    $scope.event.init();
    //});
}]);