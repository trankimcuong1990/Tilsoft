(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', inventoryRptController);
    inventoryRptController.$inject = ['$scope', '$timeout', 'dataService'];

    function inventoryRptController($scope, $timeout, inventoryRptService) {
        /// variable for filters
        $scope.filters = {
            factoryWarehouses: null,
            productionTeamID: null,
            startDate: null,
            endDate: null,
        };

        $scope.data = [];
        $scope.viewData = [];

        /// list support
        $scope.support = {
            factoryWarehouses: [],
            productionTeams: [],
        };

        $scope.isFactoryWarehouse = false; /// end-user click filter Factory Warehouse
        $scope.isProductionTeam = false; /// end-user click filter Production Team

        $scope.nameFilter = null; /// Factory Warehouse (or Production Team)

        /// event controller
        $scope.event = {
            init: init,
            selectLoaded: selectLoaded,
            search: search,
            search2: search2,
            visibleDetail: visibleDetail,
            changeProductionTeam: changeProductionTeam,
            exportFile: exportFile,
        };

        /// define event controller
        function init() {
            inventoryRptService.serviceUrl = context.serviceUrl;
            inventoryRptService.token = context.token;

            inventoryRptService.getInitCustom(
                context.branchID,
                function (data) {
                    $scope.support.factoryWarehouses = data.data.factoryWarehouse;
                    $scope.support.productionTeams = data.data.productionTeam;

                    $scope.filters.startDate = data.data.startDate;
                    $scope.filters.endDate = data.data.endDate;

                    $scope.isFactoryWarehouse = true; /// default all selected factory warehouse

                    jQuery('#content').show();
                },
                function (error) {
                });
        };

        function selectLoaded(isFactoryWarehouse, isProductionTeam) {
            if (isFactoryWarehouse) {
                angular.forEach($scope.support.factoryWarehouses, function (item) {
                    if (item.isSelected) {
                        item.isSelected = false;
                    }
                });

                return $scope.nameFilter = 'Factory Warehouse';
            } else {
                $scope.data = [];
                $scope.filters.factoryWarehouses = '';
            }

            if (isProductionTeam) {
                return $scope.nameFilter = 'Production Team';
            }
        };

        function search() {
            $scope.filters.factoryWarehouses = '';
            $scope.form = 0;
            if ($scope.isFactoryWarehouse) {
                angular.forEach($scope.support.factoryWarehouses, function (item) {
                    if (item.isSelected) {
                        if ($scope.filters.factoryWarehouses != null && $scope.filters.factoryWarehouses != '') {
                            $scope.filters.factoryWarehouses += ',';
                        }

                        $scope.filters.factoryWarehouses += item.factoryWarehouseID.toString();
                    }
                });
            } else {
                $scope.filters.factoryWarehouses = '';
            }

            $scope.filters.startDate = jQuery('#startDate').val();
            $scope.filters.endDate = jQuery('#endDate').val();

            inventoryRptService.getDataFilterCustom(
                $scope.filters.factoryWarehouses,
                $scope.filters.productionTeamID,
                $scope.filters.startDate,
                $scope.filters.endDate,
                function (success) {
                    if (success.message.type == 0) {
                        $scope.data = success.data;
                        $scope.$apply();
                    } else {
                        jsHelper.showMessage('warning', success.message.message);
                    }
                },
                function (error) {
                });
        };

        function search2() {
            $scope.filters.factoryWarehouses = '';
            $scope.form = 1;
            debugger;
            if ($scope.isFactoryWarehouse) {
                angular.forEach($scope.support.factoryWarehouses, function (item) {
                    if (item.isSelected) {
                        if ($scope.filters.factoryWarehouses != null && $scope.filters.factoryWarehouses != '') {
                            $scope.filters.factoryWarehouses += ',';
                        }

                        $scope.filters.factoryWarehouses += item.factoryWarehouseID.toString();
                    }
                });
            } else {
                $scope.filters.factoryWarehouses = '';
            }

            $scope.filters.startDate = jQuery('#startDate').val();
            $scope.filters.endDate = jQuery('#endDate').val();

            inventoryRptService.getDataFilterCustom(
                $scope.filters.factoryWarehouses,
                $scope.filters.productionTeamID,
                $scope.filters.startDate,
                $scope.filters.endDate,
                function (success) {
                    if (success.message.type == 0) {
                        $scope.data = success.data;
                        $scope.$apply();
                    } else {
                        jsHelper.showMessage('warning', success.message.message);
                    }
                },
                function (error) {
                });
            
            
        };

        function visibleDetail(item) {
            if (item.isHideDetail == true) {
                item.isHideDetail = false;
            } else {
                if ($scope.method.countDetailByItem(item) > 0) {
                    item.isHideDetail = true;
                }
            }
        };

        function changeProductionTeam() {
            if ($scope.filters.productionTeamID == null) {
                $scope.data = [];
            }
        };

        function exportFile() {
            $scope.filters.factoryWarehouses = '';

            if ($scope.isFactoryWarehouse) {
                angular.forEach($scope.support.factoryWarehouses, function (item) {
                    if (item.isSelected) {
                        if ($scope.filters.factoryWarehouses != null && $scope.filters.factoryWarehouses != '') {
                            $scope.filters.factoryWarehouses += ',';
                        }

                        $scope.filters.factoryWarehouses += item.factoryWarehouseID.toString();
                    }
                });
            } else {
                $scope.filters.factoryWarehouses = '';
            }

            $scope.filters.startDate = jQuery('#startDate').val();
            $scope.filters.endDate = jQuery('#endDate').val();

            inventoryRptService.exportDataFilterCustom(
                $scope.filters.factoryWarehouses,
                $scope.filters.productionTeamID,
                $scope.filters.startDate,
                $scope.filters.endDate,
                function (success) {
                    if (success.message.type == 0) {
                        window.location = context.backendReportUrl + success.data;
                    } else {
                        jsHelper.showMessage('warning', success.message.message);
                    }
                },
                function (error) {
                });
        };

        /// method controller
        $scope.method = {
            parseView: parseView,
            totalStocked: totalStocked,
            totalReceived: totalReceived,
            totalDelivered: totalDelivered,
            totalEnded: totalEnded,
            totalStockedCont: totalStockedCont,
            totalReceivedCont: totalReceivedCont,
            totalDeliveredCont: totalDeliveredCont,
            totalEndedCont: totalEndedCont,
            countDetailByItem: countDetailByItem,
        };

        /// define method controller
        function parseView() {
            var kProductionItemID = null;
            var index = null;

            $scope.viewData = [];

            angular.forEach($scope.data, function (item) {
                if (kProductionItemID == null || kProductionItemID != item.productionItemID) {
                    var pItem = {
                        productionItemID: item.productionItemID,
                        productionItemUD: item.productionItemUD,
                        productionItemNM: item.productionItemNM,
                        factoryWarehouseID: item.factoryWarehouseID,
                        factoryWarehouseNM: item.factoryWarehouseNM,
                        unitNM: item.unitNM,
                        clientUD: item.clientUD,
                        frameWeight: item.frameWeight,
                        frameMaterialColorNM: item.frameMaterialColorNM,
                        stockedQnt: item.stockedQnt,
                        productID: item.productID,
                        receivedQnt: 0,
                        deliveredQnt: 0,
                        documentNotes: [],
                        isHideDetail: false,
                        productionTeamID: item.productionTeamID,
                        productionTeamNM: item.productionTeamNM
                    };

                    $scope.viewData.push(pItem);

                    kProductionItemID = item.productionItemID;
                    index = $scope.viewData.indexOf(pItem);
                }

                var dItem = {
                    documentNoteID: item.documentNoteID,
                    workOrderID: item.workOrderID,
                    documentNoteTypeID: item.documentNoteTypeID,
                    workOrderUD: item.workOrderUD,
                    proformaInvoiceNo: item.proformaInvoiceNo,
                    documentNoteUD: item.documentNoteUD,
                    documentNoteDate: item.documentNoteDate,
                    documentNoteDescription: item.documentNoteDescription,
                    viewName: item.viewName,
                    orderQnt: item.orderQnt,
                    receivedQnt: item.receivedQnt,
                    deliveredQnt: item.deliveredQnt,
                    articleCode: item.articleCode,
                    description: item.description,
                    clientUD: item.clientUD,
                    productID: item.productID,
                };

                $scope.viewData[index].documentNotes.push(dItem);
            });

            angular.forEach($scope.viewData, function (item) {
                var receivedQnt = 0;
                var deliveredQnt = 0;

                angular.forEach(item.documentNotes, function (dItem) {
                    receivedQnt = receivedQnt + dItem.receivedQnt;
                    deliveredQnt = deliveredQnt + dItem.deliveredQnt;
                });

                item.receivedQnt = receivedQnt;
                item.deliveredQnt = deliveredQnt;
            });
        };

        function totalStocked() {
            var totalStocked = 0;

            angular.forEach($scope.data.inventory, function (item) {
                totalStocked = totalStocked + item.stockedQnt;
            });

            return totalStocked;
        };

        function totalReceived() {
            var totalReceived = 0;

            angular.forEach($scope.data.inventory, function (item) {
                totalReceived = totalReceived + item.receivedQnt;
            });

            return totalReceived;
        };

        function totalDelivered() {
            var totalDelivered = 0;

            angular.forEach($scope.data.inventory, function (item) {
                totalDelivered = totalDelivered + item.deliveredQnt;
            });

            return totalDelivered;
        };

        function totalEnded() {
            var totalEnded = 0;

            angular.forEach($scope.data.inventory, function (item) {
                totalEnded = totalEnded + (item.stockedQnt + item.receivedQnt - item.deliveredQnt);
            });

            return totalEnded;
        };

        function totalStockedCont() {
            var totalStockedCont = 0;

            angular.forEach($scope.data.inventory, function (item) {
                totalStockedCont = totalStockedCont + item.stockedCont;
            });

            return totalStockedCont;
        };

        function totalReceivedCont() {
            var totalReceivedCont = 0;

            angular.forEach($scope.data.inventory, function (item) {
                totalReceivedCont = totalReceivedCont + item.receivedCont;
            });

            return totalReceivedCont;
        };

        function totalDeliveredCont() {
            var totalDeliveredCont = 0;

            angular.forEach($scope.data.inventory, function (item) {
                totalDeliveredCont = totalDeliveredCont + item.deliveredCont;
            });

            return totalDeliveredCont;
        };

        function totalEndedCont() {
            var totalEndedCont = 0;

            angular.forEach($scope.data.inventory, function (item) {
                totalEndedCont = totalEndedCont + item.endedCont;
            });

            return totalEndedCont;
        };

        function countDetailByItem(item) {
            var count = 0;
            angular.forEach($scope.data.inventoryDetail, function (dItem) {
                if (dItem.productionItemID == item.productionItemID && dItem.factoryWarehouseID == item.factoryWarehouseID && dItem.productionTeamID == item.productionTeamID) {
                    count = count + 1;
                }
            });
            return count;
        };

        /// set time-out to load page
        $timeout(function () {
            $scope.event.init();
        }, 0);
    };
})();