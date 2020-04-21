(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', inventoryCostRptController);
    inventoryCostRptController.$inject = ['$scope', 'dataService'];

    function inventoryCostRptController($scope, inventoryCostRptService) {
        // variables for filter.
        $scope.filters = {
            factoryWarhouseID: null,
            productionItemTypeID: null,
            startDate: null
        };

        // variables for support.
        $scope.support = {
            factoryWarehouses: [],
            productionItemTypes: [],
        };

        // event.
        $scope.event = {
            init: init,
            report: report,
            search: search,
        };

        // #region Variables for page

        $scope.data = [];

        // #endregion

        // #region Methods for page

        $scope.method = {
            sumBeginning: sumBeginning,
            sumReceiving: sumReceiving,
            sumDelivering: sumDelivering,
            sumEndingInventory: sumEndingInventory,
            sumValue: sumValue
        };

        // #endregion

        // define init.
        // get factory warehouse.
        function init() {
            // initialize service.
            inventoryCostRptService.serviceUrl = context.serviceUrl;
            inventoryCostRptService.token = context.token;

            inventoryCostRptService.getInit(
                function (data) {
                    // set value get from api.
                    $scope.support.factoryWarehouses = data.data.factoryWarehouses;
                    $scope.support.productionItemTypes = data.data.productionItemTypes;

                    jQuery('#content').show();
                },
                function (error) {
                });
        };

        // define report.
        // export excel with:
        // factory warehouse, start date.
        function report(factoryWarehouseID, productionItemTypeID, startDate) {
            //$scope.data = [];
            // fix value when end-user keypress value by keyboard.
            startDate = jQuery('#startDate').val();

            if (startDate === null || startDate === '') {
                jsHelper.showMessage('warning', context.errorValid);
                return false;
            }

            if (factoryWarehouseID === null) {
                jsHelper.showMessage('warning', 'Please select at least one value to search!');
                return false;
            }

            if (factoryWarehouseID === null) {
                factoryWarehouseID = 0;
            }

            inventoryCostRptService.exportInventoryCostReport(
                factoryWarehouseID,
                productionItemTypeID,
                startDate,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };
       
        // define search.
        // search with filter:
        // factory warehouse, start date.
        function search(factoryWarehouseID, productionItemTypeID, startDate) {
            // fix value when end-user keypress value by keyboard.
            startDate = jQuery('#startDate').val();

            if (startDate === null || startDate === '') {
                jsHelper.showMessage('warning', context.errorValid);
                return false;
            }

            if (factoryWarehouseID === null) {
                jsHelper.showMessage('warning', 'Please select at least one value to search!');
                return false;
            }

            if (factoryWarehouseID === null) {
                factoryWarehouseID = 0;
            }


            inventoryCostRptService.getDataWithFilters(
                factoryWarehouseID,
                productionItemTypeID,
                startDate,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        //
        function sumBeginning() {
            var sum = 0;

            for (var i = 0; i < $scope.data.length; i++) {
                sum = sum + $scope.data[i].totalBeginning;
            }

            return sum;
        };

        //
        function sumReceiving() {
            var sum = 0;

            for (var i = 0; i < $scope.data.length; i++) {
                sum = sum + $scope.data[i].totalReceiving;
            }

            return sum;
        };

        //
        function sumDelivering() {
            var sum = 0;

            for (var i = 0; i < $scope.data.length; i++) {
                sum = sum + $scope.data[i].totalDelivering;
            }

            return sum;
        };

        //
        function sumEndingInventory() {
            var sum = 0;

            for (var i = 0; i < $scope.data.length; i++) {
                sum = sum + $scope.data[i].totalEndingInventory;
            }

            return sum;
        };

        //
        function sumValue() {
            var sum = 0;

            for (var i = 0; i < $scope.data.length; i++) {
                sum = sum + $scope.data[i].value;
            }

            return sum;
        };

        // default event call init.
        $scope.event.init();
    };
})();