function formatProductionItem(data) {
    return $.map(data.data.productionItems, function (item) {
        return {
            description: item.productionItemUD,
            id: item.productionItemID,
            label: item.productionItemNM,

            fromFactoryWarehouseID: item.fromFactoryWarehouseID,
            toFactoryWarehouseID: item.toFactoryWarehouseID,
            productionItemID: item.productionItemID,
            productionItemUD: item.productionItemUD,
            productionItemNM: item.productionItemNM,
            unitID: item.unitID,
            defaultFactoryWarehouseID: item.defaultFactoryWarehouseID,
            stockQnt: item.stockQnt,
            productionItemUnits: item.productionItemUnits
        };
    });
}

function selectType(sel) {
  
    if (sel.value>=0) {
        $("#colorred").hide();
        $("#select-type").removeClass("error-cus");
        $('#select-type').prop('required', false);
    }
    else {
        $('#colorred').show();
        $('#select-type').addClass('error-cus');
        $('#select-type').prop('required', true);
    }
}

(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', warehouseTransferControllerController);
    warehouseTransferControllerController.$inject = ['$scope', 'dataService', '$timeout'];

    function warehouseTransferControllerController($scope, warehouseTransferService, $timeout) {
        $scope.data = null;
        $scope.searchString = null;
        $scope.IDX = 0;
        $scope.getIDX = 0;
        $scope.warehouseTransferDetails = [];
        $scope.newID = -1;

        $scope.event = {
            init: init,
            get: get,
            remove: remove,
            update: update,
            scan: scan,
            removeProduct: removeProduct,
            addProduct: addProduct,
            getInitData: getInitData,
            confirmReceiving: confirmReceiving,
            confirmDelivering: confirmDelivering,
            selectedProductionItem: selectedProductionItem,
            calreceivedQnt: calreceivedQnt,
            caldeliveriedQnt: caldeliveriedQnt,
            changeUnit: changeUnit,
            changeFromWarehouse: changeFromWarehouse
        };

        $scope.method = {
            increaseIDX: increaseIDX,
            existList: existList,
            getDefaultFactoryWarehouse: getDefaultFactoryWarehouse,
            getTotalQnt: getTotalQnt,
            getNewID: getNewID
        };

        //
        //autocomplete textbox
        //
        $scope.productionItemBox = {
            request: {
                url: context.serviceUrl + 'get-production-item',
                token: context.token
            },
            data: {
                description: null,
                id: null,
                label: null,
                value: null,

                fromFactoryWarehouseID: null,
                toFactoryWarehouseID: null,
                productionItemID: null,
                productionItemUD: null,
                productionItemNM: null,
                unitID: null,
                defaultFactoryWarehouseID: null,
                stockQnt: null,
                productionItemUnits: null
            }
        };

        function selectedProductionItem(data, item) {
            //apply selected product
            item.fromFactoryWarehouseID = data.fromFactoryWarehouseID;
            item.toFactoryWarehouseID = data.toFactoryWarehouseID;
            item.productionItemID = data.productionItemID;
            item.productionItemUD = data.productionItemUD;
            item.productionItemNM = data.productionItemNM;
            item.unitID = data.unitID;
            item.unitNM = data.unitNM;
            item.productionItemTypeID = data.productionItemTypeID;
            item.productionItemUnits = data.productionItemUnits;
            //item.toFactoryWarehouseID = data.defaultFactoryWarehouseID;
            item.weight = data.weight;
            item.isHasWeight = data.isHasWeight;
            item.stockQnt = data.stockQnt;

            $scope.$apply();
        };

        function calreceivedQnt(item) {
            for (var i = 0; i < item.productionItemUnits.length; i++) {
                if (item.unitID === item.productionItemUnits[i].unitID) {
                    if (item.receivedQnt !== '') {
                        item.receivedQnt = (parseFloat(item.receivedQnt) * parseFloat(item.productionItemUnits[i].conversionFactor)).toFixed(4) + '';
                        break;
                    }
                }
            }
            item.deliveriedQnt = item.receivedQnt;
        };

        function caldeliveriedQnt(item) {
            for (var i = 0; i < item.productionItemUnits.length; i++) {
                if (item.unitID === item.productionItemUnits[i].unitID) {
                    if (item.deliveriedQnt !== '') {
                        item.deliveriedQnt = (parseFloat(item.deliveriedQnt) * parseFloat(item.productionItemUnits[i].conversionFactor)).toFixed(4) + '';
                        break;
                    }
                }
            }
        };

        function changeUnit(item) {
            item.receivedQnt = "";
            item.deliveriedQnt = "";
        };

        function changeFromWarehouse(item) {
            warehouseTransferService.getStockQntFromWarehouse(
                item.fromFactoryWarehouseID,
                item.productionItemID,
                function (data) {
                    if (data.data.length >= 1) {
                        item.stockQnt = data.data[0].stockQnt;
                    }
                    else {
                        item.stockQnt = 0;
                    }
                    $scope.$apply();
                },
                function (error) {
                    item.stockQnt = 0;
                },
            );
        };

        function init() {
            warehouseTransferService.serviceUrl = context.serviceUrl;
            warehouseTransferService.token = context.token;
            warehouseTransferService.supportServiceUrl = context.supportServiceUrl;
            $scope.event.getInitData();
        };

        function getInitData() {
            warehouseTransferService.getInitData(
                function (data) {
                    $scope.factoryWarehouses = data.data.factoryWarehouses;
                    $scope.branches = data.data.branches;

                    $scope.event.get();
                },
                function (error) {
                    //do nothing
                },
            );
        };

        function get() {
            warehouseTransferService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    //$scope.factoryWarehouses = data.data.factoryWarehouses;
                    $scope.warehouseTransferDetails = data.data.data.warehouseTransferDetails;

                    angular.forEach($scope.warehouseTransferDetails, function (item) {
                        if (item.stockQnt == null) {
                            item.stockQnt = 0;
                        }
                    });

                    $timeout(function () { jQuery('#content').show(); }, 0);

                    //$('#fromBranch').trigger('change.select2');
                },
                function (error) {
                    //do nothing
                });
        };

        function remove(id) {
            warehouseTransferService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    //do nothing
                });
        };

        function update($event, data) {
            if ($scope.editForm.$valid) {
                var Continue = true;
                angular.forEach(scope.data.warehouseTransferDetails, function (item) {
                    if (Continue) {
                        if (item.deliveriedQnt > item.stockQnt) {
                            jsHelper.showMessage('warning', "Delivery quanlity must lower than stock quantity (" + item.productionItemNM+")");
                            Continue = false;
                        }
                    }
                });
                if (Continue) {
                    warehouseTransferService.update(
                        context.id,
                        $scope.data,
                        function (data) {
                            jsHelper.processMessage(data.message);
                            window.location = context.refreshUrl + data.data.warehouseTransferID;
                            $scope.data = data.data;
                        },
                        function (error) {
                            //do nothing
                        });
                }
            } else {
                jsHelper.showMessage('warning', context.msgValid);
            }
        };

        function scan() {
            warehouseTransferService.searchProductionItemByScanQRCode(
                $scope.searchString,
                function (data) {
                    if (data.data !== null) {

                        var item = data.data[0]; //debugger;

                        if (item !== undefined || item !== null) {
                            if ($scope.data.warehouseTransferProductDTOs === null) {
                                $scope.data.warehouseTransferProductDTOs = [];
                            }

                            var receivingNoteDetail = {
                                warehouseTransferProductID: $scope.method.increaseIDX(),
                                productionItemID: item.id,
                                productionItemUD: item.value,
                                productionItemNM: item.label,
                                productionItemTypeID: item.productionItemTypeID,
                                productionItemTypeNM: item.productionItemTypeNM,
                                description: null,
                                quantity: 1,
                                toFactoryWarehouseID: $scope.method.getDefaultFactoryWarehouse(),
                            }

                            if ($scope.data.warehouseTransferProductDTOs.length === 0) {
                                $scope.data.warehouseTransferProductDTOs.push(receivingNoteDetail);
                            } else {
                                if ($scope.method.existList(receivingNoteDetail)) {
                                    $scope.data.warehouseTransferProductDTOs[$scope.getIDX].quantity = $scope.data.warehouseTransferProductDTOs[$scope.getIDX].quantity + receivingNoteDetail.quantity;
                                } else {
                                    $scope.data.warehouseTransferProductDTOs.push(receivingNoteDetail);
                                }
                            }

                            $scope.searchString = null;

                            $scope.$apply();
                        }
                    }
                },
                function (error) {
                });
        };

        function removeProduct(item) {
            var index = $scope.data.warehouseTransferDetails.indexOf(item);
            $scope.data.warehouseTransferDetails.splice(index, 1);
        };

        function addProduct($event) {
            if ($scope.data.fromBranchID && $scope.data.toBranchID) {
                if ($event !== null) {
                    $event.preventDefault();
                }
                $scope.warehouseTransferDetails.push({
                    warehouseTransferDetailID: $scope.method.getNewID()
                });
            }
            else {
                jsHelper.showMessage('warning', "Please select branch before add product");
            }
        }


        function confirmReceiving(id) {
            if (confirm("Are you sure?")) {
                warehouseTransferService.confirmReceiving(
                    id,
                    function (data) {
                        window.location = context.refreshUrl + data.data.data.warehouseTransferID;
                    },
                    function (error) {
                        //do nothing
                    }
                );
            }
        };

        function confirmDelivering(id) {
            if (confirm("Are you sure?")) {
                warehouseTransferService.confirmDelivering(
                    id,
                    function (data) {
                        window.location = context.refreshUrl + data.data.data.warehouseTransferID;
                    },
                    function (error) {
                        //do nothing
                    }
                );
            }
        };
        //method
        function getNewID() {
            $scope.newID--;
            return $scope.newID;
        };

        function increaseIDX() {
            $scope.IDX = $scope.IDX - 1;
            return $scope.IDX
        };

        function existList(item) {
            for (var i = 0; i < $scope.data.warehouseTransferProductDTOs.length; i++) {
                if ($scope.data.warehouseTransferProductDTOs[i].productionItemID === item.productionItemID) {
                    $scope.getIDX = i;
                    return true;
                }
            }

            return false;
        };

        function getDefaultFactoryWarehouse() {
            var factoryWarehouseID = null;
            angular.forEach($scope.factoryWarehouses, function (item) {
                if (item.isDefault) {
                    factoryWarehouseID = item.factoryWarehouseID;
                }
            });
            return factoryWarehouseID;
        }

        function getTotalQnt() {
            var totalQnt = 0
            if ($scope.data !== null) {
                angular.forEach($scope.data.warehouseTransferProductDTOs, function (item) {
                    totalQnt += parseInt(item.quantity);
                });
            }
            return totalQnt;
        }

        $timeout(function () {
            $scope.event.init();
        }, 0);
    };
})();