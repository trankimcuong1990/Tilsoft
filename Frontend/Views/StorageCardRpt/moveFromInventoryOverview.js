function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.label,
            description: item.value,
            defaultFactoryWarehouse: item.factoryWarehouseID
        }
    });
}

(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', storageCardRptController);
    storageCardRptController.$inject = ['$scope', '$timeout', 'dataService'];

    function storageCardRptController($scope, $timeout, storageCardRptService) {
        $scope.factoryWarehouses = [];
        $scope.data = [];
        $scope.totalBeginning = null;

        $scope.ngInitProductionItem = null;
        $scope.ngItemProductionItem = {
            id: null,
            label: null,
            description: null,
            defaultFactoryWarehouse: null
        };
        $scope.ngRequestProductionItem = {
            url: context.supportServiceUrl + 'getProductionItemWithFilters',
            token: context.token,
        };
        $scope.ngSelectedProductionItem = {
            id: null,
            label: null,
            description: null
        };

        $scope.filters = {
            productionItemID: null,
            factoryWarehouseID: null,
            startDate: null,
            endDate: null,
        };

        $scope.event = {
            init: init,
            report: report,
            search: search,
            select: select
        };

        $scope.method = {
            sumImport: sumImport,
            sumExport: sumExport,
            getEndingLatest: getEndingLatest,
            getBeginning: getBeginning,
        };

        function select(productionItem) {
            debugger;
            if (productionItem !== null) {
                $scope.filters.factoryWarehouseID = productionItem.defaultFactoryWarehouse;
                $scope.filters.productionItemID = productionItem.id;

                $timeout(function () {
                    jQuery('#factoryWarehouse').trigger('change.select2');
                });

                $scope.$apply();
            }
        }

        function init() {
            storageCardRptService.serviceUrl = context.serviceUrl;
            storageCardRptService.token = context.token;

            storageCardRptService.getInitFromInventoryOverview(
                context.productionItemID,
                context.factoryWarehouseID,
                context.startDate,
                context.endDate,
                context.branchID,
                function (data) {
                    // default current to date.
                    var currentDate = new Date();
                    Number.prototype.pad = function (size) {
                        var s = String(this);
                        while (s.length < (size || 2)) { s = "0" + s; }
                        return s;
                    }
                    $scope.filters.endDate = currentDate.getDate().pad(2) + '/' + (currentDate.getMonth() + 1).pad(2) + '/' + currentDate.getFullYear();

                    // set value for filters.
                    $scope.filters.factoryWarehouseID = parseInt(context.factoryWarehouseID);
                    $scope.filters.startDate = context.startDate;
                    $scope.filters.endDate = context.endDate;

                    // set ngItemProductionItem.
                    $scope.ngItemProductionItem.id = data.data.data.productionItemID;
                    $scope.ngInitProductionItem = data.data.data.productionItemNM;

                    $scope.filters.productionItemID = $scope.ngItemProductionItem.id;

                    $scope.factoryWarehouses = data.data.factoryWarehouses;

                    $timeout(function () {
                        jQuery('#factoryWarehouse').trigger('change.select2');
                    });

                    $scope.$apply();

                    $scope.event.search($scope.filters.productionItemID, $scope.filters.factoryWarehouseID, $scope.filters.startDate, $scope.filters.endDate);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    jQuery('#content').show();
                });
        };

        function report() {
            $scope.filters.startDate = jQuery('#startDate').val();
            $scope.filters.endDate = jQuery('#endDate').val();

            if ($scope.ngInitProductionItem !== null && $scope.ngInitProductionItem !== '') {
                $scope.filters.productionItemID = $scope.ngItemProductionItem.id;
            }

            storageCardRptService.exportExcel(
                $scope.filters.productionItemID,
                $scope.filters.factoryWarehouseID,
                $scope.filters.startDate,
                $scope.filters.endDate,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function search(productionItemID, factoryWarehouseID, startDate, endDate) {
            startDate = jQuery('#startDate').val();
            endDate = jQuery('#endDate').val();

            if (startDate === null || startDate === '') {
                jsHelper.showMessage('warning', context.errorValid);
                return false;
            }

            if (endDate === null || endDate === '') {
                jsHelper.showMessage('warning', context.errorValid);
                return false;
            }

            if (factoryWarehouseID === null) {
                factoryWarehouseID = 0;
            }

            if ($scope.ngInitProductionItem !== null && $scope.ngInitProductionItem !== '') {
                productionItemID = $scope.ngItemProductionItem.id;
            }

            storageCardRptService.getDataWithFilters(
                productionItemID,
                factoryWarehouseID,
                startDate,
                endDate,
                function (data) {
                    $scope.data = data.data.storageCards;
                    $scope.totalBeginning = data.data.finalBeginning;

                    for (var i = 0; i < $scope.data.length; i++) {
                        if ($scope.data[i].importDocumentNo !== null) {
                            if (i === 0) {
                                $scope.data[i].ending = $scope.totalBeginning + $scope.data[i].receiving;
                                $scope.data[i].beginning = $scope.totalBeginning;
                            } else {
                                $scope.data[i].ending = $scope.data[i - 1].ending + $scope.data[i].receiving;
                            }
                        }

                        if ($scope.data[i].exportDocumentNo !== null) {
                            if (i === 0) {
                                $scope.data[i].ending = $scope.totalBeginning - $scope.data[i].delivering;
                                $scope.data[i].beginning = $scope.totalBeginning;
                            } else {
                                $scope.data[i].ending = $scope.data[i - 1].ending - $scope.data[i].delivering;
                            }
                        }
                    }

                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function sumImport() {
            var sum = 0;

            for (var i = 0; i < $scope.data.length; i++) {
                sum = sum + $scope.data[i].receiving;
                //if ($scope.data[i].importDocumentNo !== null) {
                //    sum = sum + $scope.data[i].receiving
                //}
            }

            return sum;
        };

        function sumExport() {
            var sum = 0;

            for (var i = 0; i < $scope.data.length; i++) {
                sum = sum + $scope.data[i].delivering;
                //if ($scope.data[i].exportDocumentNo !== null) {
                //    sum = sum + $scope.data[i].delivering
                //}
            }

            return sum;
        };

        function getEndingLatest() {
            var sum = 0;

            if ($scope.data.length > 0) {
                sum = $scope.data[$scope.data.length - 1].ending;
            }

            return sum;
        };

        function getBeginning() {
            var sum = 0;

            if ($scope.totalBeginning !== null) {
                sum = $scope.totalBeginning;
            }

            return sum;
        };

        $timeout(function () {
            $scope.event.init();
        }, 0);
    };
})();