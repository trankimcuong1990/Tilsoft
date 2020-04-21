(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', productBreakDownDefaultPALController);
    productBreakDownDefaultPALController.$inject = ['$scope', '$timeout', '$cookieStore', 'dataService'];

    function productBreakDownDefaultPALController($scope, $timeout, $cookieStore, productBreakDownDefaultPALService) {
        $scope.data = null;

        $scope.support = {
            calculationType: [],
            optionPrice: [],
            optionQuantity: []
        };

        $scope.event = {
            init: function () {
                productBreakDownDefaultPALService.serviceUrl = context.serviceUrl;
                productBreakDownDefaultPALService.token = context.token;

                productBreakDownDefaultPALService.getInit(
                    function (data) {
                        $scope.support.calculationType = data.data.supportProductBreakDownCalculationTypePAL;
                        $scope.support.optionPrice = data.data.supportProductBreakDownOptionPricePAL;
                        $scope.support.optionQuantity = data.data.supportProductBreakDownOptionQuantityPAL;

                        $scope.event.load(context.id);
                    },
                    function (error) {
                    });
            },
            load: function (id) {
                productBreakDownDefaultPALService.load(
                    id,
                    null,
                    function (data) {
                        $scope.data = data.data.productBreakDownDefaultCategoryPAL;

                        jQuery('#content').show();
                    },
                    function (error) {
                    });
            },
            update: function () {
                productBreakDownDefaultPALService.update(
                    $scope.data.productBreakDownDefaultCategoryID,
                    $scope.data,
                    function (data) {
                        window.location = context.refreshUrl + data.data.productBreakDownDefaultCategoryPAL.productBreakDownDefaultCategoryID;
                    },
                    function (error) {
                    });
            },
            delete: function (id) {
                productBreakDownDefaultPALService.delete(
                    id,
                    null,
                    function (data) {
                        if (data.message.type === 0) {
                            jsHelper.processMessage(data.message);

                            window.location = context.backUrl;
                        }
                    },
                    function (error) {
                    });
            },
            changeCalculationType: function (calculationTypeID) {
                if (calculationTypeID !== 2) {
                    $scope.data.quantityPercent = "";
                } else {
                    $scope.data.optionTotalID = null;
                    $scope.data.optionToGetPriceID = null;
                }
            },
            changeOptionPrice: function (optionPriceID) {
                if (optionPriceID !== null) {
                    $scope.data.unitPrice = "";
                }
            },
            back: function () {
                window.location = context.backUrl;
            }
        };

        $timeout(function () {
            $scope.event.init();
        }, 0);
    };
})();