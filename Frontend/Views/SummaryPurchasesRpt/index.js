function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.value,
            description: item.label
        };
    });
}
(function () {
    'use strict';
    var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive', 'ngCookies']);

    tilsoftApp.filter('factoryRawMaterialFilter', function () {
        return function (data, factoryRawMaterialID) {
            var result = [];
            angular.forEach(data, function (item) {
                if (parseInt(item.factoryRawMaterialID) === parseInt(factoryRawMaterialID)) {
                    result.push(item);
                }
            });
            return result;
        };
    });

    tilsoftApp.controller('tilsoftController', summaryPurchasesRptController);
    summaryPurchasesRptController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function summaryPurchasesRptController($scope, $cookieStore, summaryPurchasesRptService) {
        // variable
        $scope.data = [];
        $scope.dataGroupOfSupplier = [];
        $scope.subSuppliers = [];
        $scope.workOrders = [];
        $scope.filter = {
            factoryRawMaterialID: null,
            productionItemUD: '',
            startDate: '',
            endDate: '',
            factoryRawMaterialFullName: ''
        };     

        // event
        $scope.event = {
            init: init,
            generateExcel: generateExcel,
            search: search,
            changeSubSupplier: changeSubSupplier
        };

        // method
        $scope.method = {
            totalAmountSupplier: function (item) {
                var total = 0;
                var totalVAT = 0;
                for (var i = 0; i < $scope.data.length; i++) {
                    var listReceivingNote = $scope.data[i];
                    if (listReceivingNote.factoryRawMaterialID === item.factoryRawMaterialID) {
                        total += listReceivingNote.totalAmount;
                        totalVAT += (listReceivingNote.purchaseValue + ((listReceivingNote.purchaseValue * 10) / 100));
                    }
                }
                item.totalAmountOfSupplier = total;
                item.totalValue = totalVAT;
            }
        };

        function init() {
            summaryPurchasesRptService.serviceUrl = context.serviceUrl;
            summaryPurchasesRptService.token = context.token;
            summaryPurchasesRptService.supportServiceUrl = context.supportServiceUrl;
            summaryPurchasesRptService.getInit(
                function (data) {
                    $scope.subSuppliers = data.data.supportSubSuppliers;
                    
                    jQuery('#content').show();
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.$apply();
                }
            );
        };

        function generateExcel() {
            if (jQuery('#startDate').val() === null || jQuery('#startDate').val() === '') {
                jsHelper.showMessage('warning', 'Please input From Date.');
                return false;
            }

            if (jQuery('#endDate').val() === null || jQuery('#endDate').val() === '') {
                jsHelper.showMessage('warning', 'Please input To Date.');
                return false;
            }
            if ($scope.filter.productionItemUD === undefined) {
                $scope.filter.productionItemUD = "";
            }
            summaryPurchasesRptService.generateExcel(
                $scope.filter.factoryRawMaterialID,
                $scope.filter.productionItemUD,
                $scope.filter.startDate,
                $scope.filter.endDate,
                $scope.filter.factoryRawMaterialFullName,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function search() {   
            $scope.data = [];
            if (jQuery('#startDate').val() === null || jQuery('#startDate').val() === '') {
                jsHelper.showMessage('warning', 'Please input From Date.');
                return false;
            }

            if (jQuery('#endDate').val() === null || jQuery('#endDate').val() === '') {
                jsHelper.showMessage('warning', 'Please input To Date.');
                return false;
            }         
            summaryPurchasesRptService.getDataWithFilters(
                {
                    filters: {
                        factoryRawMaterialID: $scope.filter.factoryRawMaterialID,
                        productionItemUD: $scope.filter.productionItemUD,
                        startDate: $scope.filter.startDate,
                        endDate: $scope.filter.endDate,
                        factoryRawMaterialFullName: $scope.filter.factoryRawMaterialFullName
                    }
                },
                function (data) {
                    $scope.data = data.data.receivingNoteReportDatas;
                    $scope.dataGroupOfSupplier = data.data.supplierOfReceivingDatas;
                    $scope.$apply();                   
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });                    
        };

        function changeSubSupplier(selectedItem) {
            if (selectedItem === null || selectedItem === '') {
                $scope.filter.factoryRawMaterialFullName = '';
                return;
            }

            for (var i = 0; i < $scope.subSuppliers.length; i++) {
                var eleItem = $scope.subSuppliers[i];

                if (eleItem.factoryRawMaterialID === selectedItem) {
                    $scope.filter.factoryRawMaterialFullName = eleItem.factoryRawMaterialOfficialNM;
                    return;
                }
            }
        };

        // default event
        $scope.event.init();
    }
})();