function formatProductionItem(data) {
    return $.map(data.data, function (item) {
        return {
            description: item.productionItemUD,
            id: item.productionItemID,
            label: item.productionItemNM,
            value: item.productionItemNM,

            productionItemID: item.productionItemID,
            productionItemUD: item.productionItemUD,
            productionItemNM: item.productionItemNM,
            unitNM: item.unitNM,
            orderQnt: item.orderQnt,
            stockQnt: item.stockQnt,
            isApproved: item.isApproved,
            workOrderID: item.workOrderID,
            workOrderUD: item.workOrderUD,
            proformaInvoiceNo: item.proformaInvoiceNo,
            workOrderByItems: item.workOrderDTOs,
            productionItemGroupID: item.productionItemGroupID,
        };
    });
}

(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', purchasingRequestMngController);
    purchasingRequestMngController.$inject = ['$scope', '$timeout', 'dataService'];

    function purchasingRequestMngController($scope, $timeout, dataService) {
        $scope.data = null;
        $scope.purchaseRequestDetails = [];
        $scope.clients = [];
        $scope.saleOrders = [];
        $scope.support = {
            purchasingRequestStatuses: [], // support status purchasing request
            productionItemGroups: [], // support material group
        };

        $scope.detailID = 0;
        $scope.isApproved = null;
        $scope.requestStatusNM = null;

        $scope.quickSearchItem = {
            apiService: {
                url: context.serviceUrl + 'get-production-item-base-on',
                token: context.token,
            },
            dataTemp: {
                productionItemID: null,
                productionItemUD: null,
                productionItemNM: null,
                unitNM: null,
                orderQnt: null,
                stockQnt: null,
                isApproved: null,
                workOrderID: null,
                workOrderUD: null,
                proformaInvoiceNo: null,
                requestQnt: 0,
                workOrderByItems: [],
                productionItemGroupID: null,
            },
        };

        $scope.event = {
            init: init,
            load: load,
            update: update,
            remove: remove,
            addItem: addItem,
            removeItem: removeItem,
            selectedItem: selectedItem,
            totalRequest: totalRequest,
            filterItemGroup: filterItemGroup,
        };

        function init() {
            dataService.token = context.token;
            dataService.serviceUrl = context.serviceUrl;

            dataService.getInit(
                function (success) {
                    $scope.support.purchasingRequestStatuses = success.data.purchaseRequestStatuses;
                    $scope.support.productionItemGroups = success.data.productionItemGroups;
                    $scope.event.load();
                },
                function (error) {
                });
        };

        function load() {
            dataService.load(
                context.id,
                {
                    workOrderIDs: context.workOrderIDs,
                    branchID: context.branchID,
                },
                function (success) {
                    $scope.data = success.data.data;

                    if ($scope.data.purchaseRequestStatusID != null && ($scope.data.purchaseRequestStatusID == 2 || $scope.data.purchaseRequestStatusID == 3)) {
                        $scope.isApproved = true;
                    }

                    angular.forEach($scope.support.purchasingRequestStatuses, function (sItem) {
                        if (sItem.purchaseRequestStatusID == $scope.data.purchaseRequestStatusID) {
                            $scope.requestStatusNM = sItem.purchaseRequestStatusNM;
                        }
                    });

                    angular.copy(success.data.clientByWorkOrders, $scope.clients);
                    angular.copy(success.data.proformaInvoiceByWorkOrders, $scope.saleOrders);

                    $scope.method.parseView($scope.data.purchaseRequestDetailDTOs);

                    angular.forEach($scope.purchaseRequestDetails, function (mItem) {
                        angular.forEach($scope.support.productionItemGroups, function (tItem) {
                            if (mItem.productionItemGroupID == tItem.productionItemGroupID) {
                                tItem.isSelected = true;
                            }
                        });
                    });

                    jQuery('#content').show();
                },
                function (error) {
                });
        };

        function update() {
            // Process detail before transfer backend
            $scope.data.purchaseRequestDetailDTOs = [];

            if ($scope.data.workOrderIDs == null || $scope.data.workOrderIDs == '') {
                angular.forEach($scope.purchaseRequestDetails, function (item) {
                    $scope.data.purchaseRequestDetailDTOs.push(item);
                });
            } else {
                angular.forEach($scope.purchaseRequestDetails, function (item) {
                    if (item.productionItemID != null) {
                        angular.forEach(item.workOrderByItems, function (sItem) {
                            $scope.data.purchaseRequestDetailDTOs.push({
                                purchaseRequestDetailID: $scope.method.createDetailID(),
                                productionItemID: item.productionItemID,
                                productionItemUD: item.productionItemUD,
                                productionItemNM: item.productionItemNM,
                                unitNM: item.unitNM,
                                eta: item.eta,
                                isApproved: item.isApproved,
                                orderQnt: item.orderQnt,
                                stockQnt: item.stockQnt,
                                workOrderID: sItem.workOrderID,
                                requestQnt: sItem.requestQnt,
                            });
                        });
                    }
                });
            }

            // Call api update
            dataService.update(
                context.id,
                $scope.data,
                function (success) {
                    if (success.message.type == 0) {
                        window.location = context.refreshUrl + success.data.purchaseRequestID;
                    }
                },
                function (error) {
                });
        };

        function remove() {
        };

        function addItem(typeID) {
            if (typeID == 0) { // Free item
                $scope.method.addFreeItem();
            } else { // WorkOrder item
                $scope.method.addWorkOrderItem();
            }
        };

        function removeItem(item) {
            var index = $scope.purchaseRequestDetails.indexOf(item);
            $scope.purchaseRequestDetails.splice(index, 1);
        };

        function selectedItem(data, item, workOrderIDs) {
            $timeout(function () {
                if (workOrderIDs == null || workOrderIDs == '') {
                    if (data != null && !$scope.method.isExistProductionItem(data.productionItemID)) {
                        item.productionItemID = data.productionItemID;
                        item.productionItemUD = data.productionItemUD;
                        item.productionItemNM = data.productionItemNM;
                        item.unitNM = data.unitNM;
                        item.orderQnt = data.orderQnt;
                        item.stockQnt = data.stockQnt;
                        item.isApproved = data.isApproved;
                        item.workOrderID = data.workOrderID;
                        item.workOrderUD = data.workOrderUD;
                        item.proformaInvoiceNo = data.proformaInvoiceNo;
                        item.eta = $scope.data.eta;
                    }
                } else {
                    if (data != null && !$scope.method.isExistProductionItem(data.productionItemID)) {
                        item.productionItemID = data.productionItemID;
                        item.productionItemUD = data.productionItemUD;
                        item.productionItemNM = data.productionItemNM;
                        item.unitNM = data.unitNM;
                        item.orderQnt = data.orderQnt;
                        item.stockQnt = data.stockQnt;
                        item.isApproved = data.isApproved;
                        item.eta = $scope.data.eta;

                        angular.forEach(data.workOrderByItems, function (wItem) {
                            var sItem = {
                                requestQnt: wItem.requestingQnt,
                                workOrderID: wItem.workOrderID,
                                workOrderUD: wItem.workOrderUD,
                                proformaInvoiceNo: wItem.proformaInvoiceNo,
                            };
                            item.workOrderByItems.push(sItem);
                        });

                        angular.forEach($scope.support.productionItemGroups, function (sItem) {
                            if (sItem.productionItemGroupID == data.productionItemGroupID) {
                                sItem.isSelected = true;
                            }
                        });
                    }
                }
            }, 0);
        };

        function totalRequest(item) {
            var totalRequest = 0;

            angular.forEach(item.workOrderByItems, function (sItem) {
                if (sItem.requestQnt != null) {
                    totalRequest = totalRequest + parseFloat(sItem.requestQnt);
                }
            });

            return totalRequest;
        };

        function filterItemGroup() {
            var tmpPurchaseRequestDetails = [];
            var hasChecked = false;

            var productionItemIDs = '';
            var workOrderIDs = $scope.data.workOrderIDs;
            var stickGroupIDs = '';
            var branchID = context.branchID;

            angular.forEach($scope.purchaseRequestDetails, function (item) {
                if (productionItemIDs != '') {
                    productionItemIDs = productionItemIDs + ',';
                }
                productionItemIDs = productionItemIDs + item.productionItemID.toString();
            });

            angular.forEach($scope.support.productionItemGroups, function (sItem) {
                if (sItem.isSelected) {
                    if (stickGroupIDs != '') {
                        stickGroupIDs = stickGroupIDs + ',';
                    }
                    stickGroupIDs = stickGroupIDs + sItem.productionItemGroupID.toString();
                }
            });

            if (stickGroupIDs != '') {
                /// add progress call api to get item have material group not display on screen
                dataService.reloadItemMaterialGroups(
                    productionItemIDs,
                    workOrderIDs,
                    stickGroupIDs,
                    branchID,
                    function (success) {
                        if (success.message.type == 0) {
                            angular.forEach(success.data, function (item) {
                                if (!$scope.method.isExistProductionItem(item.productionItemID)) {
                                    $scope.data.purchaseRequestDetailDTOs.push(item);
                                }
                            });

                            angular.forEach($scope.support.productionItemGroups, function (tItem) {
                                if (tItem.isSelected != null && !tItem.isSelected) {
                                    for (var i = $scope.data.purchaseRequestDetailDTOs.length-1; i >= 0; i--) {
                                        var mItem = $scope.data.purchaseRequestDetailDTOs[i];
                                        if (mItem.productionItemGroupID == tItem.productionItemGroupID) {
                                            $scope.data.purchaseRequestDetailDTOs.splice(i, 1);
                                        }
                                    }
                                }
                            });

                            angular.forEach($scope.support.productionItemGroups, function (tItem) {
                                if (tItem.isSelected) {
                                    angular.forEach($scope.data.purchaseRequestDetailDTOs, function (mItem) {
                                        if (mItem.productionItemGroupID == tItem.productionItemGroupID) {
                                            tmpPurchaseRequestDetails.push(mItem);
                                        }
                                    });
                                }
                            });

                            $scope.method.parseView(tmpPurchaseRequestDetails);
                        } else {
                            jsHelper.showMessage('warning', success.message.message);
                        }
                    },
                    function (error) {
                    });
            } else {
                $scope.method.parseView(tmpPurchaseRequestDetails);
            }
        };

        $scope.method = {
            parseView: parseView,
            addFreeItem: addFreeItem,
            addWorkOrderItem: addWorkOrderItem,
            createDetailID: createDetailID,
            isExistProductionItem: isExistProductionItem,
        };

        function parseView(purchaseRequestDetailDTOs) {
            $scope.purchaseRequestDetails = [];

            if ($scope.data.workOrderIDs == null || $scope.data.workOrderIDs == '') {
                angular.copy(purchaseRequestDetailDTOs, $scope.purchaseRequestDetails);
            } else {
                var tmpPurchaseRequestDetails = [];
                angular.copy(purchaseRequestDetailDTOs, tmpPurchaseRequestDetails);

                var kProductionItemID = null; // Keep value production item
                var kIndex = null; // Keep value index of production item

                angular.forEach(tmpPurchaseRequestDetails, function (tmpItem) {
                    if (tmpItem.productionItemGroupID != null) {
                        if ($scope.purchaseRequestDetails.length == 0 || kProductionItemID != tmpItem.productionItemID) { // material(only)
                            kProductionItemID = tmpItem.productionItemID;

                            var mItem = {
                                purchaseRequestDetailID: tmpItem.purchaseRequestDetailID,
                                productionItemID: tmpItem.productionItemID,
                                productionItemUD: tmpItem.productionItemUD,
                                productionItemNM: tmpItem.productionItemNM,
                                unitNM: tmpItem.unitNM,
                                eta: tmpItem.eta,
                                isApproved: tmpItem.isApproved,
                                orderQnt: tmpItem.orderQnt,
                                stockQnt: tmpItem.stockQnt,
                                workOrderByItems: [],
                                productionItemGroupID: tmpItem.productionItemGroupID,
                            }

                            mItem.workOrderByItems.push({
                                requestQnt: tmpItem.requestQnt,
                                workOrderID: tmpItem.workOrderID,
                                workOrderUD: tmpItem.workOrderUD,
                                proformaInvoiceNo: tmpItem.proformaInvoiceNo,
                            });

                            $scope.purchaseRequestDetails.push(mItem);
                            kIndex = $scope.purchaseRequestDetails.indexOf(mItem);
                        } else {
                            $scope.purchaseRequestDetails[kIndex].workOrderByItems.push({
                                requestQnt: tmpItem.requestQnt,
                                workOrderID: tmpItem.workOrderID,
                                workOrderUD: tmpItem.workOrderUD,
                                proformaInvoiceNo: tmpItem.proformaInvoiceNo,
                            });
                        }
                    }
                });
            }
        };

        function addFreeItem() {
            var purchaseRequestDetail = {
                purchaseRequestDetailID: $scope.method.createDetailID(),
                productionItemID: null,
                productionItemUD: null,
                productionItemNM: null,
                unitNM: null,
                eta: null,
                isApproved: null,
                requestQnt: null,
                orderQnt: null,
                stockQnt: null,
            };

            $scope.purchaseRequestDetails.push(purchaseRequestDetail);
        };

        function addWorkOrderItem() {
            var purchaseRequestDetail = {
                purchaseRequestDetailID: $scope.method.createDetailID(),
                productionItemID: null,
                productionItemUD: null,
                productionItemNM: null,
                unitNM: null,
                eta: null,
                isApproved: null,
                orderQnt: null,
                stockQnt: null,
                workOrderByItems: [],
            };

            $scope.purchaseRequestDetails.push(purchaseRequestDetail);
        };

        function createDetailID() {
            return $scope.detailID = $scope.detailID - 1;
        };

        function isExistProductionItem(id) {
            var isExisted = false;

            angular.forEach($scope.purchaseRequestDetails, function (mItem) {
                if (mItem.productionItemID == id) {
                    isExisted = true;
                }
            });

            return isExisted;
        };

        $timeout(function () {
            $scope.event.init();
        }, 0);
    };
})();