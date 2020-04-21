jQuery('.search-filter').keypress(
    function (event) {
        if (event.keyCode === 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.search();
        }
    }
);

(function () {
    'use strict';
    var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ngCookies']);
    tilsoftApp.controller('tilsoftController', stockStatusQntRptController);
    stockStatusQntRptController.$inject = ['$scope', '$timeout', '$cookieStore', 'dataService'];

    function stockStatusQntRptController($scope, $timeout, $cookieStore, stockStatusQntRptService) {
        // variable
        $scope.data = [];
        $scope.StockStatusData = [];

        $scope.filters = {
            AlertLevel: '',
            FactoryWarehouseID: '',
            MaterialGroupID: ''
        };

        $scope.gridHandler = {
            loadMore: function () {
                //$scope.event.search(true);
            },
            sort: function (sortedBy, sortedDirection) {
                //$scope.event.search();
            },
            isTriggered: false
        };

        // event
        $scope.event = {
            init: init
            ,search: search
        };

        $scope.alertLevels = [
            { name: "All", value: 1 },
            { name: "Minimum", value: 2 },
            { name: "Average", value: 3 }
        ];

        function init() {
            stockStatusQntRptService.serviceUrl = context.serviceUrl;
            stockStatusQntRptService.token = context.token;
            stockStatusQntRptService.supportServiceUrl = context.supportServiceUrl;
            stockStatusQntRptService.getInit(
                context.branchID,
                function (data) {
                    $scope.productionItemGroups = data.data.productionItemGroups;
                    $scope.factoryWarehouseDtos = data.data.factoryWarehouseDtos;

                    $scope.filters.AlertLevel = 1;
                    $scope.$apply();
                    jQuery('#content').show();
                },
                function (error) {
                }
            );
        };

        function search() {
            stockStatusQntRptService.getDataWithFilters(
                {
                    filters: {
                        AlertLevel: $scope.filters.AlertLevel,
                        FactoryWarehouseID: $scope.filters.FactoryWarehouseID,
                        MaterialGroupID: $scope.filters.MaterialGroupID
                    }
                },
                function (data) {
                    $scope.StockStatusData = data.data.stockStatusQntDTOs;

                    if ($scope.filters.FactoryWarehouseID == "" || $scope.filters.FactoryWarehouseID == null) {
                        angular.forEach($scope.StockStatusData, function (item) {
                            item.factoryWarehouseNM = null;
                        });
                    }

                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        // default event
        $scope.event.init();
    }
})();