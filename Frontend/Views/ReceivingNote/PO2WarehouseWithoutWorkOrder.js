function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            description: '',
            id: item.id,
            label: item.label,
            value: item.value,

            purchaseOrderID: item.purchaseOrderID,
            purchaseOrderUD: item.purchaseOrderUD,
            factoryRawMaterialID: item.factoryRawMaterialID,
            factoryRawMaterialUD: item.factoryRawMaterialUD,
            factoryRawMaterialOfficialNM: item.factoryRawMaterialOfficialNM,
            factoryRawMaterialShortNM: item.factoryRawMaterialShortNM,
            address: item.address,
            totalQntWithPurchaseOrder: item.totalQntWithPurchaseOrder,
            purchaseOrderDetailDTOs: item.purchaseOrderDetailDTOs,
        };
    });
}

function formatDataSupplier(data) {
    return $.map(data.data, function (item) {
        return {
            description: item.label,
            id: item.id,
            label: item.value,
            value: item.label,

            subSupplierFullNM: item.subSupplierFullNM,
            address: item.address,

        };
    });
}

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
            unitID: item.unitID,
            factoryWarehouseID: item.factoryWarehouseID,
            unitPrice: item.unitPrice,
            productionItemUnits: item.productionItemUnits,
            totalReceive: item.totalReceive,
            stockQnt: item.stockQnt,
        };
    });
}

function formatFactorySaleOrder(data) {
    return $.map(data.data, function (item) {
        return {
            description: '',
            id: item.factorySaleOrderID,
            label: item.factorySaleOrderUD,
            value: item.factorySaleOrderUD,

            factorySaleOrderID: item.factorySaleOrderID,
            factorySaleOrderUD: item.factorySaleOrderUD            
        };
    });
}

$(document).keypress(function (e) {
    if ($(e.target).is('input, textarea')) {
        return;
    }
    if (e.which === 83) { // 83: S
        var scope = angular.element(jQuery('body')).scope();
        scope.event.update();
    }
});

var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', function ($scope, $timeout) {
    //
    // property
    //
    $scope.data = null;
    $scope.factoryWarehousePallets = [];
    $scope.factoryWarehouses = [];
    $scope.productionTeams = [];
    $scope.productionItemTypes = [];
    $scope.listProdutionItemUnits = [];
    $scope.newID = -1;
    $scope.statusTypes = [];
    $scope.poDetailTemp = [];
    $scope.workCenters = [];
    $scope.productionTeams = [];

    $scope.isNullPurchaseOrderID = null;

    $scope.subSupplierID = null;
    $scope.purchaseOrderSearch = null;

    $scope.purchaseOrderAPI = {
        url: jsonService.serviceUrl + 'get-purchase-order',
        token: context.token
    };

    $scope.delieveryAPI = {
        url: jsonService.serviceUrl + 'getsubsupplier',
        token: context.token
    };

    $scope.purchaseOrder = {
        purchaseOrderID: null,
        purchaseOrderUD: null,
        factoryRawMaterialID: null,
        factoryRawMaterialUD: null,
        factoryRawMaterialOfficialNM: null,
        factoryRawMaterialShortNM: null,
        address: null,
        totalQntWithPurchaseOrder: null,
        purchaseOrderDetailDTOs: [],
    };

    $scope.delievery = {
        subSupplierFullNM: null,
        address: null,
        id: null,
        label: null,
        value: null
    };

    //
    //autocomplete textbox
    //
    $scope.productionItemBox = {
        request: {
            url: context.getProductionItemUrl,
            token: context.token
        },
        data: {
            description: null,
            id: null,
            label: null,
            value: null,

            productionItemID: null,
            productionItemUD: null,
            productionItemNM: null,
            unitID: null,
            factoryWarehouseID: null,
            productionItemUnits: null,
            unitPrice: null,
            totalReceive: null,
            stockQnt: null
        }
    };

    $scope.factorySaleOrderBox = {
        request: {
            url: context.getFactorySaleOrderUrl,
            token: context.token
        },
        data: {
            description: null,
            id: null,
            label: null,
            value: null,

            factorySaleOrderID: null,
            factorySaleOrderUD: null        
        },
        selectedItem: function (data) {
            if (data !== null) {
                $scope.data.factorySaleOrderID = data.factorySaleOrderID;
                $scope.data.factorySaleOrderUD = data.factorySaleOrderUD;

                //get product
                $scope.data.receivingNoteDetailDTOs = [];
                jsonService.getFactorySaleOrderDetail(
                    $scope.data.factorySaleOrderID,
                    $scope.data.receivingNoteDate,
                    function (data) {
                        var itemResult = data.data;
                        angular.forEach(itemResult, function (item) {
                            $scope.data.receivingNoteDetailDTOs.push({
                                receivingNoteDetailID: $scope.method.getNewID(),
                                productionItemID: item.productionItemID,
                                productionItemUD: item.productionItemUD,
                                productionItemNM: item.productionItemNM,
                                unitID: item.unitID,
                                unitNM: item.unitNM,
                                toFactoryWarehouseID: item.factoryWarehouseID,
                                toFactoryWarehouseUD: item.factoryWarehouseUD,
                                toFactoryWarehouseNM: item.factoryWarehouseNM,
                                unitPrice: item.unitPrice,
                                qtyByUnit: item.quantity,
                                quantity: item.quantity,
                                productionItemUnits: item.productionItemUnits
                            });
                        });
                        $scope.$apply();
                    }
                    , function () {

                    });
            }
           
        }
    };

    //
    // event
    //
    $scope.event = {

        load: function () {
            jsonService.getData(
                context.id,
                context.workOrderIDs,
                context.branchID,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.factoryWarehousePallets = data.data.factoryWarehousePallets;
                    $scope.factoryWarehouses = data.data.factoryWarehouses;
                    $scope.productionTeams = data.data.productionTeams;
                    $scope.productionItemTypes = data.data.productionItemTypes;
                    $scope.statusTypes = data.data.statusTypes;

                    $scope.productionTeams = data.data.productionTeams;
                    $scope.workCenters = data.data.workCenters;

                    if ($scope.data.statusTypeID === null) {
                        $scope.data.statusTypeID = 2; // From purchasing
                    }

                    $scope.data.viewName = 'PO2WarehouseWithoutWorkOrder';

                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },

        update: function () {
            if ($scope.editForm.$valid) {
                $scope.data.receivingNoteDate = jQuery('#receivingNoteDate').val();

                // Get base url
                $scope.data.baseUrl = window.location.href.substring(0, window.location.href.length - 1);

                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            $scope.method.refresh(data.data.receivingNoteID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', context.errMsg);
            }
        },

        reset: function () {
            if (confirm('Are you sure you want to reset?')) {
                jsonService.reset(
                    context.id,
                    $scope.data,
                    function (data) {
                        window.location = context.refreshUrl + data.data.receivingNoteID;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
        },

        delete: function ($event) {
            $event.preventDefault();
            //jsHelper.showMessage('warning', 'cannot delete.');
            if (confirm('Are you sure you want to delete receiving note ?')) {
                jsonService.deleteWithPermission(
                    context.id,
                    $scope.data.createdBy,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.backUrl;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },

        removeLine: function ($event, index) {
            $event.preventDefault();
            $scope.data.receivingNoteDetailDTOs.splice(index, 1);
        },

        addNewLineManual: function ($event) {
            $event.preventDefault();

            // Case note type Purchasing and data PurchaseOrder empty
            if ($scope.data.statusTypeID === 2 && $scope.data.purchaseOrderID === null) {
                jsHelper.showMessage('warning', 'Please choose one PurchaseOrder to create/edit ReceivingNote!');
                return;
            }

            if ($scope.data.statusTypeID === 6 && $scope.data.factorySaleOrderID === null) {
                jsHelper.showMessage('warning', 'Please select sale order !!!');
                return;
            }

            $scope.data.receivingNoteDetailDTOs.push({
                receivingNoteDetailID: $scope.method.getNewID(),
                productionItemID: null,
                productionItemUD: null,
                productionItemNM: null,
                unitID: null,
                quantity: 0,
                qtyByUnit: null,
                unitPrice: null,
                receivingNoteDetailSubUnitDTOs: [],
            });
        },

        changeQtyByUnit: function (item) {
            var inputQnt = item.qtyByUnit;
            for (var i = 0; i < item.receivingNoteWorkOrderDetailDTOs.length; i++) {
                var wItem = item.receivingNoteWorkOrderDetailDTOs[i];
                if (inputQnt <= wItem.totalNorm) {
                    wItem.receivingQnt = inputQnt;
                    inputQnt = inputQnt - wItem.receivingQnt;
                }
                else {
                    wItem.receivingQnt = wItem.totalNorm;
                    inputQnt = inputQnt - wItem.totalNorm;
                }
            }
        },

        getReceivingNotePrintout: function () {
            jsonService.getReceivingNotePrintout(
                context.id,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type !== 2) {
                        window.open(context.reportUrl + data.data, '');
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        getPurchaseOrderApprove: function (purchaseOrderItem) {
            //$scope.poDetailTemp = [];
            if (purchaseOrderItem !== null) {
                $scope.data.purchaseOrderID = purchaseOrderItem.purchaseOrderID;
                $scope.data.deliverName = purchaseOrderItem.factoryRawMaterialOfficialNM;
                $scope.data.deliverAddress = purchaseOrderItem.address;
                $scope.data.totalQntWithPurchaseOrder = purchaseOrderItem.totalQntWithPurchaseOrder;

                jQuery('#receivingNoteDetailLoading').show();
                jQuery('#receivingNoteDetail').hide();

                $scope.$apply();

                jQuery('#purchase-order-auto-complete').blur();

                jsonService.getPurchaseOrderDetail(
                    $scope.data.purchaseOrderID,
                    context.branchID,
                    $scope.data.receivingNoteDate,
                    function (data) {
                        angular.forEach(data.data, function (pItem) {
                            var receivingNoteDetailItem = {
                                receivingNoteDetailID: $scope.method.getNewID(),
                                purchaseOrderDetailID: pItem.purchaseOrderDetailID,
                                productionItemID: pItem.productionItemID,
                                unitID: pItem.unitID,
                                toFactoryWarehouseID: pItem.factoryWarehouseID,
                                orderQnt: pItem.orderQnt,
                                qtyByUnit: null,
                                unitPrice: pItem.unitPrice,
                                totalReceive: pItem.totalReceive,
                                stockQnt: pItem.stockQnt,
                                productionItemUD: pItem.productionItemUD,
                                productionItemNM: pItem.productionItemNM,
                                receivingNoteWorkOrderDetailDTOs: [],
                                receivingNoteDetailSubUnitDTOs: [],
                                productionItemUnits: pItem.productionItemUnits,
                                subUnitID: null,
                                conversionFactorMainUnit: pItem.conversionFactorMainUnit,
                                conversionFactorSubUnit: null
                            };

                            angular.forEach(pItem.purchaseOrderWorkOrderDetailDTOs, function (wItem) {
                                receivingNoteDetailItem.receivingNoteWorkOrderDetailDTOs.push({
                                    receivingNoteWorkOrderDetailID: $scope.method.getNewID(),
                                    purchaseOrderDetailID: wItem.purchaseOrderDetailID,
                                    workOrderID: wItem.workOrderID,
                                    receivingQnt: wItem.purchaseOrderWorkOrderQnt,
                                    workOrderUD: wItem.workOrderUD,
                                    totalNorm: wItem.totalNorm,
                                    finishDate: wItem.finishDate
                                });
                            });

                            //distribute receiving qnt for every workOrder
                            $scope.event.changeQtyByUnit(receivingNoteDetailItem);

                            //add to receiving note detail
                            $scope.data.receivingNoteDetailDTOs.push(receivingNoteDetailItem);

                            jQuery('#receivingNoteDetailLoading').hide();
                            jQuery('#receivingNoteDetail').show();

                            $scope.$apply();
                            //add to PO detail temp
                            //$scope.poDetailTemp.push(receivingNoteDetailItem);
                        });
                    },
                    function (error) {
                    });
            }
        },

        selectDelivery: function (delievery) {
            $scope.data.deliverAddress = delievery.address;
            $scope.data.deliverName = delievery.label;
            $scope.$apply();
        },

        selectedProductionItem: function (data, item) {
            if (data !== null) {
                var isExist = $scope.method.isExistProductionItemInReceivingProduction(data.productionItemID);

                if (!isExist) {
                    item.productionItemID = data.productionItemID;
                    item.productionItemUD = data.productionItemUD;
                    item.productionItemNM = data.productionItemNM;
                    item.unitID = data.unitID;
                    item.productionItemUnits = data.productionItemUnits;
                    item.toFactoryWarehouseID = data.factoryWarehouseID;
                    item.conversionFactorMainUnit = 1;

                    item.unitPrice = data.unitPrice;
                    if ($scope.data.statusTypeID === 3) { // Case note type is Other
                        item.unitPrice = null;
                    }

                    item.totalReceive = data.totalReceive;
                    item.stockQnt = data.stockQnt;

                    if ($scope.poDetailTemp.length > 0) {
                        angular.forEach($scope.poDetailTemp, function (dItem) {
                            if (item.productionItemID === dItem.productionItemID) {
                                item.unitPrice = dItem.unitPrice;
                                item.orderQnt = dItem.orderQnt;
                                item.qtyByUnit = dItem.qtyByUnit;
                                item.purchaseOrderDetailID = dItem.purchaseOrderDetailID;
                                item.purchaseOrderID = dItem.purchaseOrderID;
                            }
                        });
                    }
                } else {
                    jsHelper.showMessage('warning', 'ProductionItem ' + data.productionItemNM + ' is existed!');
                    return;
                }
            }

            $scope.$apply();
        },

        removePurchaseOrderUD: function () {
            $scope.method.resetPurchaseOrderLinked();
        },

        changeStatusType: function () {
            $scope.method.resetPurchaseOrderLinked();
        },

        changeProductionItem: function (item) {
            if (item !== null) {
                item.productionItemID = null;
                item.stockQnt = null;
                item.totalReceive = null;
                item.unitPrice = null;
                item.unitID = null;
                item.qtyByUnit = null;
                item.toFactoryWarehouseID = null;
                item.factoryWarehousePalletID = null;
            }
        },

        changeSubUnit: function (item) {
            var itemSubUnit = item.productionItemUnits.filter(o => o.unitID === item.subUnitID);
            if (itemSubUnit.length > 0) {
                item.conversionFactorSubUnit = parseFloat(itemSubUnit[0].conversionFactor);
                item.conversionFactorAffter = parseFloat(item.conversionFactorSubUnit) / parseFloat(item.conversionFactorMainUnit);
            }
        },

        changeUnit: function (item) {
            var itemSubUnit = item.productionItemUnits.filter(o => o.unitID === item.unitID);
            var conversionFactor = parseFloat(0);
            if (itemSubUnit.length > 0) {
                item.conversionFactorMainUnit = parseFloat(itemSubUnit[0].conversionFactor);
                item.conversionFactorAffter = parseFloat(item.conversionFactorSubUnit) / parseFloat(item.conversionFactorMainUnit);
                conversionFactor = itemSubUnit[0].conversionFactor;
            }
            jsonService.getProductionItemByUD(
                item.productionItemUD,
                context.branchID,
                $scope.data.receivingNoteDate,
                function (data) {
                    var xData = data.data;
                    item.stockQnt = xData[0].stockQnt / conversionFactor;
                    $scope.$apply();
                },
                function (error) {

                }
            );
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            window.location = context.refreshUrl + id;
        },

        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },

        resetPurchaseOrderLinked: function () {
            $scope.data.purchaseOrderID = null;
            $scope.data.deliverName = null;
            $scope.data.deliverAddress = null;
            $scope.data.totalQntWithPurchaseOrder = null;

            $scope.data.receivingNoteDetailDTOs = [];
        },

        isExistProductionItemInReceivingProduction: function (productionItemID) {
            var isExist = false;

            if ($scope.data != null) {
                if ($scope.data.receivingNoteDetailDTOs.length == 0) {
                    return isExist;
                }

                angular.forEach($scope.data.receivingNoteDetailDTOs, function (item) {
                    if (item.productionItemID == productionItemID) {
                        isExist = true;
                    }
                });

                return isExist;
            }
        },

        caculatorDelta: function (item) {
            var result = parseFloat(0);
            result = (((parseFloat(item.qtyByUnit) / parseFloat(item.conversionFactorAffter)) - parseFloat(item.subQnt)) / (parseFloat(item.qtyByUnit) / parseFloat(item.conversionFactorAffter))) * 100;
            return result;
        }
    };

    //
    // receiving by purcahse order detail workorder form
    //
    $scope.workOrderDetailForm = {
        selectedReceivingNoteDetailItem: null,
        receivingNoteDetailItem: null,
        workOrderItems: [],
        productionItemUD: null,

        load: function (receivingNoteDetailItem) {
            $scope.workOrderDetailForm.selectedReceivingNoteDetailItem = receivingNoteDetailItem;
            $scope.workOrderDetailForm.receivingNoteDetailItem = angular.copy(receivingNoteDetailItem);
            jQuery("#frmReceivingWorkOrderDetail").modal();
        },

        open: function (receivingNoteDetailItem) {
            jsonService.getWorkOrderItems(
                receivingNoteDetailItem.productionItemID,
                function (data) {
                    if (data.message.type == 0) {
                        $scope.workOrderDetailForm.productionItemUD = receivingNoteDetailItem.productionItemUD;
                        $scope.workOrderDetailForm.workOrderItems = data.data;
                    } else if (data.message.type == 2) {
                        console.log(data);
                    }

                    $scope.$apply();
                    jQuery("#frmReceivingWorkOrderDetail").modal();
                },
                function (error) {
                });
        },

        ok: function () {
            //update edit value
            var mainItem = $scope.workOrderDetailForm.selectedReceivingNoteDetailItem;
            var editedItem = $scope.workOrderDetailForm.receivingNoteDetailItem;

            var totalReceiving = 0;
            angular.forEach(editedItem.receivingNoteWorkOrderDetailDTOs, function (item) {
                totalReceiving += (item.receivingQnt === null ? parseFloat(0) : parseFloat(item.receivingQnt));
            });
            if (editedItem.qtyByUnit < totalReceiving) {
                bootbox.alert("Total receiving on every workorder is greater than quantity. Please check again");
                return;
            }
            else {
                angular.forEach(mainItem.receivingNoteWorkOrderDetailDTOs, function (item) {
                    var x = editedItem.receivingNoteWorkOrderDetailDTOs.filter(o => o.receivingNoteWorkOrderDetailID === item.receivingNoteWorkOrderDetailID);
                    item.receivingQnt = x[0].receivingQnt;
                });
            }
            jQuery("#frmReceivingWorkOrderDetail").modal('hide');
        }
    };

    //
    //add item by purchase order detail
    //
    $scope.purchaseOrderForm = {
        purchaseOrderUD: null,
        data: null,
        load: function () {
            $scope.purchaseOrderForm.purchaseOrderUD = $scope.data.purchaseOrderUD;
            jsonService.getPurchaseOrder(
                $scope.purchaseOrderForm.purchaseOrderUD,
                function (data) {
                    $scope.purchaseOrderForm.data = data.data[0];
                    jQuery("#frmPurchaseOrder").modal();
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        ok: function () {
            var purchaseOrderItem = $scope.purchaseOrderForm.data;
            var totalReceived_Temp = 0
            //add to receiving note detail
            //$scope.data.receivingNoteDetailDTOs = [];
            angular.forEach($scope.purchaseOrderForm.data.purchaseOrderDetailDTOs, function (pItem) {
                //check item is exist in dto
                if (pItem.isSelected) {
                    var x = $scope.data.receivingNoteDetailDTOs.filter(o => o.purchaseOrderDetailID === pItem.purchaseOrderDetailID);
                    if (x === null || x.length === 0) {
                        var receivingNoteDetailItem = {
                            receivingNoteDetailID: $scope.method.getNewID(),
                            purchaseOrderDetailID: pItem.purchaseOrderDetailID,
                            productionItemID: pItem.productionItemID,
                            unitID: pItem.unitID,
                            toFactoryWarehouseID: pItem.factoryWarehouseID,
                            orderQnt: pItem.orderQnt,
                            qtyByUnit: pItem.orderQnt - pItem.totalReceived,
                            unitPrice: pItem.unitPrice,
                            totalReceived: pItem.totalReceived,

                            productionItemUD: pItem.productionItemUD,
                            productionItemNM: pItem.productionItemNM,
                            receivingNoteWorkOrderDetailDTOs: [],
                            receivingNoteDetailSubUnitDTOs: [],
                            productionItemUnits: pItem.productionItemUnitPurchaseOrderDTOs,
                            subUnitID: null
                        };
                        angular.forEach(pItem.purchaseOrderWorkOrderDetailDTOs, function (wItem) {
                            receivingNoteDetailItem.receivingNoteWorkOrderDetailDTOs.push({
                                receivingNoteWorkOrderDetailID: $scope.method.getNewID(),
                                purchaseOrderDetailID: wItem.purchaseOrderDetailID,
                                workOrderID: wItem.workOrderID,
                                receivingQnt: wItem.purchaseOrderWorkOrderQnt,
                                workOrderUD: wItem.workOrderUD,
                                totalNorm: wItem.totalNorm,
                                finishDate: wItem.finishDate,
                            });
                        });

                        //distribute receiving qnt for every workOrder
                        $scope.event.changeQtyByUnit(receivingNoteDetailItem);

                        //add to receiving note detail
                        $scope.data.receivingNoteDetailDTOs.push(receivingNoteDetailItem);
                    }
                }
            });
            jQuery("#frmPurchaseOrder").modal('hide');
        },
        checkPurchaseOrderDetailExist: function (purchaseOrderDetail) {

        }
    };

    //
    // receiving by sub Unit form
    //
    $scope.receivingNoteSubUnitForm = {
        receivingNoteDetail: null,
        productionItemUD: null,

        listSubUnit: function (item, productionItemUnits) {
            $scope.receivingNoteSubUnitForm.productionItemUD = item.productionItemUD;
            $scope.receivingNoteSubUnitForm.addSubUnit(item, productionItemUnits);
            jQuery("#frmSubUnitProductiItem").modal();
        },
        addSubUnit: function (receivingNoteDetail, productionItemUnits) {
            $scope.receivingNoteSubUnitForm.receivingNoteDetail = receivingNoteDetail;

            for (var i = 0; i < $scope.receivingNoteSubUnitForm.receivingNoteDetail.receivingNoteDetailSubUnitDTOs.length; i++) {
                var subUnitItem = $scope.receivingNoteSubUnitForm.receivingNoteDetail.receivingNoteDetailSubUnitDTOs[i];
                if ($scope.receivingNoteSubUnitForm.receivingNoteDetail.unitID === subUnitItem.unitID) {
                    var index = $scope.receivingNoteSubUnitForm.receivingNoteDetail.receivingNoteDetailSubUnitDTOs.indexOf(subUnitItem);
                    $scope.receivingNoteSubUnitForm.receivingNoteDetail.receivingNoteDetailSubUnitDTOs.splice(index, 1);
                }
            }
            // Add receiving note detail sub unit

            // Add production item sub unit
            angular.forEach($scope.receivingNoteSubUnitForm.receivingNoteDetail.productionItemUnits, function (productionUnitItem) {
                if (!$scope.receivingNoteSubUnitForm.checkUnitExist(productionUnitItem, $scope.receivingNoteSubUnitForm.receivingNoteDetail.receivingNoteDetailSubUnitDTOs)) {
                    if (productionUnitItem.unitID !== $scope.receivingNoteSubUnitForm.receivingNoteDetail.unitID) {
                        $scope.receivingNoteSubUnitForm.receivingNoteDetail.receivingNoteDetailSubUnitDTOs.push({
                            receivingNoteDetailSubUnitID: $scope.method.getNewID(),
                            receivingNoteDetailID: $scope.receivingNoteSubUnitForm.receivingNoteDetail.receivingNoteDetailID,
                            unitID: productionUnitItem.unitID,
                            unitNM: productionUnitItem.unitNM,
                            quantity: 0,
                            remark: null
                        });
                    }
                }
            });
        },
        checkUnitExist: function (productionUnitItem, receivingNoteDetailSubUnitDTOs) {
            for (var i = 0; i < receivingNoteDetailSubUnitDTOs.length; i++) {
                var subUnitItem = receivingNoteDetailSubUnitDTOs[i];
                if (productionUnitItem.unitID === subUnitItem.unitID) {
                    return true;
                }
            }

            return false;
        },
    };

    //
    // init
    //
    $timeout(function () {
        $scope.event.load();
    }, 0);
}]);