(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive']).controller('tilsoftController', productBreakDownDefaultCategoryMngController);
    productBreakDownDefaultCategoryMngController.$inject = ['$scope', 'dataService'];

    function productBreakDownDefaultCategoryMngController($scope, dataService) {
        $scope.data = [];
        $scope.newID = -1;     
        $scope.productBreakDownCalculationTypes = [];

        $scope.optionToGetQuantity = [];

        // Issue 1360
        $scope.optionToGetPrice = [];

        $scope.event = {
            get: get,
            remove: remove,
            update: update,

            // Issue 1360
            changeType: function (id) {
                if (id !== 2) {
                    $scope.data.quantityPercent = "";
                } else {
                    // to do
                    $scope.data.optionTotalID = null;
                    $scope.data.optionToGetPriceID = null;
                }
            },
            changeOptionPrice: function (id) {
                if (id !== null) {
                    $scope.data.unitPrice = "";
                }
            }
        };

        function get() {
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;

            dataService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.defaultData;
                    $scope.productBreakDownCalculationTypes = data.data.calculationType;
                    $scope.optionToGetQuantity = data.data.optionalQuantity;

                    // Issue 1360
                    $scope.optionToGetPrice = data.data.optionalPrice;

                    jQuery('#content').show();                    

                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function remove(id) {          
            dataService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    //not thing to do
                });
        };

        function update() {
            if ($scope.editForm.$valid) {
                dataService.update(
                    context.id,
                    $scope.data,
                    function (data) {                   
                        jsHelper.processMessage(data.message);                     
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.productBreakDownDefaultCategoryID;
                            $scope.data = data.data.data;
                        }
                    },
                    function (error) {
                        //not thing to do
                    });
            } else {
                jsHelper.showMessage('warning', context.msgValid);
            }
        };

        $scope.event.get();

        function createservices() {
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
            dataService.supportServiceUrl = context.supportServiceUrl;

        };
    };
})();