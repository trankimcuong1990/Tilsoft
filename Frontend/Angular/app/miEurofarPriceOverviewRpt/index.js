//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
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
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.seasons = null;
    $scope.selection = {
        season: null
    };

    $scope.data = null;
    $scope.sales = [];

    //Property Admin-DashBoard
    $scope.salePerformance = [];

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getInitData(
                function (data) {
                    $scope.seasons = data.data.seasons;
                    $scope.selection.season = jsHelper.getCurrentSeason();

                    //angular.forEach($scope.data.customerData, function (value, key) {
                    //    var sale = {
                    //        saleNM: value.saleNM,
                    //        orderQnt40HC: value.orderQnt40HC,

                    //    }
                    //}, null);

                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.$apply();
                }
            );
        },
        generateExcel: function () {
            jsonService.getExcelReport(
                $scope.selection.season,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        generateHtml: function () {
            jsonService.getHtmlReport(
                $scope.selection.season,
                function (data) {
                    $scope.data = data.data;
                    $scope.$apply();
                    console.log(data);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
            jsonService.getAdminDashBoard(
                $scope.selection.season,
                function (data) {
                    $scope.salePerformance = data.data;
                    $scope.show = $scope.salePerformance;
                    $scope.subTotalNetGrossMarginAfterCommission = 0;

                    //
                    var totalAcc = $scope.salePerformance.length;
                    var totalPercent = 0;
                    angular.forEach($scope.salePerformance, function (item, i) {
                        var x = i + 1;

                        //fob percent
                        if (x !== totalAcc) {
                            item.fobPercent = jsHelper.round(item.fobAmountUSD / $scope.subtotalFOBAmountUSD * 100, 1);
                            totalPercent += item.fobPercent;
                        }
                        else {
                            item.fobPercent = jsHelper.round(100 - totalPercent, 1);
                        }

                        //net gross margin after commission
                        item.netGrossMarginAfterCommission = jsHelper.round(item.fobAmountUSD - item.fobCostUSD - (item.commissionAmountUSD + item.fobCostUSD * 1.25 / 100 + item.lcCostAmount + item.interestAmount), 0);
                        $scope.subTotalNetGrossMarginAfterCommission += item.netGrossMarginAfterCommission;
                    });

                    //percent of net gross magin after commission
                    totalPercent = 0;
                    angular.forEach($scope.salePerformance, function (item, i) {
                        var x = i + 1;
                        if (x !== totalAcc) {
                            item.netGrossMarginAfterCommissionPercent = jsHelper.round(item.netGrossMarginAfterCommission / $scope.subTotalNetGrossMarginAfterCommission * 100, 1);
                            totalPercent += item.netGrossMarginAfterCommissionPercent;
                        }
                        else {
                            item.netGrossMarginAfterCommissionPercent = jsHelper.round(100 - totalPercent, 1);
                        }
                    });

                    $scope.$apply();
                },

                function (error) {
                    //Noting Else
                }
            );
        }

    }

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },
        getPercentage: function (a,b) {
            var percentage = 0;
            if (b != 0 && b !== null && b != '') {
                percentage = a / b * 100;
            } else {
                percentage = 0;
            }
            return percentage;
        },
        getTotal: function (data,val) {
            var result = 0;
            if (data != null) {
                for (i = 0; i < data.length; ++i) {
                    if (data[i][val] <= 0 || data[i][val] == null){
                        data[i][val] = 0;
                    }
                    result += parseFloat(data[i][val]);
                    if(val === 'lcCostAmount') console.log(result);
                }
                //angular.forEach(data, function (value, key) {
                //    console.log(value[]);
                //    if (value == val) {
                //        result += parseFloat(value);
                //    }
                //}, null);
            }
            return result;
        },
        
        getTotalMargin: function () {
            var toTalMargin = 0;
            if ($scope.data.saleData.length > 0) {
                angular.forEach($scope.data.saleData, function (item) {
                    toTalMargin += item.netGrossMargin === null ? 0 : item.netGrossMargin;
                });
                return toTalMargin;
            }
            
        }

    };


    //
    // init
    //
    $scope.event.init();
}]);