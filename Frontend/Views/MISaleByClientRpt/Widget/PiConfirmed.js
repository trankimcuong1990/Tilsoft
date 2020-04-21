// object name  : miSaleByClientRpt
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
angular.module('tilsoftApp').controller('PiConfirmedController', ['$scope', '$rootScope', '$timeout', '$filter', function ($scope, $rootScope, $timeout, $filter) {
    $scope.miSaleByClientRpt = {};
    //
    // property
    //
    $scope.miSaleByClientRpt.proformaInvoiceConfirmeds = null;
    $scope.miSaleByClientRpt.param = miSaleByClientContext.param;
    $scope.miSaleByClientRpt.isLoaded = false;

    $scope.miSaleByClientRpt.filter = {
        clientNM: '',
        clientCountryUD: '',
        saleUD: ''
    };

    $scope.miSaleByClientRpt.sortValue = {
        piConfirmedByClient: '-piConfirmedSaleAmountThisYear'
    };

    //
    // events
    //
    $scope.miSaleByClientRpt.event = {
        init: function () {
            $scope.miSaleByClientRpt.method.getProformaInvoiceConfirmed(
                $scope.miSaleByClientRpt.param.season,
                function (data) {
                    $scope.miSaleByClientRpt.proformaInvoiceConfirmeds = data.data;
                    $scope.$apply();                                      
                },
                function (error) {

                });
        }
    };


    $scope.miSaleByClientRpt.method = {
        getProformaInvoiceConfirmed: function (season, successCallBack, errorCallBack) {
            $.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + miSaleByClientContext.token
                },
                type: "POST",
                dataType: 'json',
                url: miSaleByClientContext.serviceUrl + 'get-proforma-invoice-confirmed?season=' + season,
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
            var currentSetting = $scope.miSaleByClientRpt.sortValue[key];
            if (currentSetting.replace('-', '') === field) {
                if (currentSetting.indexOf('-') < 0) {
                    $scope.miSaleByClientRpt.sortValue[key] = '-' + field;
                }
                else {
                    $scope.miSaleByClientRpt.sortValue[key] = field;
                }
            }
            else {
                $scope.miSaleByClientRpt.sortValue[key] = field;
            }
        }
    };

    //
    // angular init
    //
    $timeout(function () {
        $scope.miSaleByClientRpt.isLoaded = true;
        $scope.miSaleByClientRpt.event.init();
    }, 0);

    //$rootScope.$on('saleperformance123', function () {
    //    alert(1);
    //    //$scope.isLoaded = true;
    //    $scope.event.init();
    //});
}]);