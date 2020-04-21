(function () {
    'use strict';

    angular.module('tilsoftApp').controller('SaleByCountryController', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;

        //
        //property
        //
        $scope.SaleByCountry = [];

        //
        // events
        //
        $scope.event = {
            init: function () {
                dataService.getSaleByCountry(
                    context.season,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.method.setPanelVisible('pnlSaleByCountry');
                            $scope.SaleByCountry = data.data.commercialInvoiceSummaries;                            
                        }
                        else {
                            jsHelper.showMessage('warning', data.message.message);
                        }
                    },
                    function (error) {
                        $scope.SaleByCountry = null;
                    }
                );
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