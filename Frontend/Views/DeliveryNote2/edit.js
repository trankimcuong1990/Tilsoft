$(document).keypress(function (e) {
    if ($(e.target).is('input, textarea')) {
        return;
    }
    if (e.which === 83) { // 83: S
        var scope = angular.element(jQuery('body')).scope();
        scope.event.update();
    }
});

function formatProductionItem(data) {
    return $.map(data.data, function (item) {
        return {
            description: item.label,
            id: item.id,
            value: item.label,
            label: item.value,

            productionItemID: item.productionItemID,
            productionItemUD: item.value,
            productionItemNM: item.label,
            unitID: item.unitID,
            defaultFactoryWarehouseID: item.defaultFactoryWarehouseID,
            productionItemTypeID: item.productionItemTypeID,
            stockQnt: item.stockQnt,
            productionItemUnits: item.productionItemUnits
        };
    });
}

var tilsoftApp = angular.module('tilsoftApp', ['ui.bootstrap', 'furnindo-directive', 'avs-directives']);
tilsoftApp.filter('productionTeamByWorkCenter', function () {
    return function (productionTeams, workCenterID) {
        var result = [];
        angular.forEach(productionTeams, function (item) {
            if (item.workCenterID == workCenterID) {
                result.push(item);
            }
            else if (item.workCenterID == null) {
                result.push(item);
            }
        });
        return result;
    };
});
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'confirmDialogFactory', function ($scope, $timeout, confirmDialogFactory) {
    //
    // property
    //
    $scope.data = null;
    $scope.semiReceiptData = null;
    $scope.deliveryNoteProducts = [];
    $scope.deliveryNoteSemiProducts = [];

    $scope.factoryWarehousePallets = [];
    $scope.factoryWarehouses = [];
    $scope.productionTeams = [];
    $scope.productionItemTypes = [];
    $scope.workCenters = [];
    $scope.statusTypes = [];
    $scope.transportationServiceDTOs = [];
    $scope.outsourcingCostDTOs = [];
    $scope.newID = -1;
    $scope.bomdtOs = null;    
    $scope.workOrderList = [];
    $scope.reasonOtherPrices = [];

    $scope.includeMaterial = false;
    $scope.includeComponent = false;
    $scope.isBlockMaterial = null;

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
            defaultFactoryWarehouseID: null,
            productionItemTypeID: null,
            stockQnt: null,
            productionItemUnits: null
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
                    $scope.workCenters = data.data.workCenters;
                    $scope.workOrderList = data.data.workOrderList;
                    $scope.statusTypes = data.data.statusTypes;
                    $scope.transportationServiceDTOs = data.data.transportationServiceDTOs;
                    $scope.outsourcingCostDTOs = data.data.outsourcingCostDTOs;
                    $scope.reasonOtherPrices = data.data.reasonOtherPrices;
                    $scope.data.viewName = context.viewName;
                    $scope.data.statusTypeNM = $scope.statusTypes.filter(o => o.statusTypeID === $scope.data.statusTypeID)[0].statusTypeNM;                    
                    //
                    //show data on view
                    //
                    $scope.deliveryNoteProducts = $scope.method.parseDataToView($scope.data);

                    //assign select2
                    $("#workCenterID").select2();
                    //$("#fromProductionTeamID").select2();
                    $("#toProductionTeamID").select2();
                    
                    //assing confirm dialog for combobox
                    confirmDialogFactory.setSelected('workCenterID', $scope.data.workCenterID);
                    $scope.$on('popupCloseEvent_workCenterID', function (event, data) {
                        $scope.data.workCenterID = data.selectedItem;
                        // Check work center is block material
                        angular.forEach($scope.workCenters, function (workCenter) {
                            if (workCenter !== null && workCenter.workCenterID === $scope.data.workCenterID) {
                                $scope.isBlockMaterial = workCenter.isBlocked;
                            }
                        });
                        $timeout(function () {
                            $scope.$apply(function () {
                                $('#workCenterID').trigger('change.select2');
                            });
                        }, 100);
                        if (data.result) {
                            $scope.event.initializeItem($scope.data.workCenterID, true, true);
                        }
                    });

                    confirmDialogFactory.setSelected('fromProductionTeamID', $scope.data.fromProductionTeamID);
                    $scope.$on('popupCloseEvent_fromProductionTeamID', function (event, data) {
                        $scope.data.fromProductionTeamID = data.selectedItem;
                        $timeout(function () {
                            $scope.$apply(function () {
                                $('#fromProductionTeamID').trigger('change.select2');
                            });
                        }, 100);
                        if (data.result) {
                            $scope.method.resetDeliveryDetail();
                        }
                    });

                    //
                    //load semi product receipt
                    //
                    var semiReceiptID = $scope.data.semiReceiptID !== null ? $scope.data.semiReceiptID : 0;
                    var workOrderIDs = $scope.data.workOrderIDs;
                    jsonService.getData(
                        semiReceiptID,
                        workOrderIDs,
                        context.branchID,
                        function (data) {
                            $scope.semiReceiptData = data.data.data;
                            $scope.semiReceiptData.viewName = context.viewName;
                            $scope.deliveryNoteSemiProducts = $scope.method.parseDataToView($scope.semiReceiptData);
                            $scope.$apply();
                            $('#toProductionTeamID').trigger('change.select2');
                            $('#workCenterID').trigger('change.select2');
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                            $scope.semiReceiptData = null;
                            $scope.$apply();
                        });
                    
                    //
                    //apply data
                    //                    
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
                $scope.data.branchID = context.branchID;
                $scope.data.deliveryNoteDate = jQuery('#deliveryNoteDate').val();
                //update data
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {                            
                            var deliveryNoteID = data.data.deliveryNoteID;
                            var x = $scope.semiReceiptData.deliveryNoteDetailDTOs.filter(o => o.qtyByUnit !== null && o.qtyByUnit !== '' && parseFloat(o.qtyByUnit) !== 0);
                            if (x.length > 0) {
                                //assign info for semi-receipt
                                $scope.semiReceiptData.toProductionTeamID = $scope.data.toProductionTeamID;
                                $scope.semiReceiptData.workCenterID = $scope.data.workCenterID;
                                $scope.semiReceiptData.parentID = data.data.deliveryNoteID;
                                $scope.semiReceiptData.branchID = context.branchID;
                                //update data for semi rceipt
                                var semiReceiptID = $scope.data.semiReceiptID !== null ? $scope.data.semiReceiptID : 0;
                                jsonService.update(
                                    semiReceiptID,
                                    $scope.semiReceiptData,
                                    function (data) {
                                        jsHelper.processMessage(data.message);
                                        //refresh receipt
                                        $scope.method.refresh(deliveryNoteID);
                                    },
                                    function (error) {
                                        jsHelper.showMessage('warning', error);
                                    }
                                );
                            }
                            else {
                                //refresh receipt
                                $scope.method.refresh(deliveryNoteID);
                            }                                                        
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
                        window.location = context.refreshUrl + data.data.deliveryNoteID;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
        },

        delete: function ($event) {
            $event.preventDefault();
            //jsHelper.showMessage('warning', 'cannot delete.');
            if (confirm('Are you sure you want to delete delivery note ?')) {
                jsonService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }            
        },

        removeLine: function ($event, item) {
            $event.preventDefault();
            angular.forEach(item.pieces, function (pItem) {
                $scope.event.removeDeliveryNoteDetailByID(pItem.deliveryNoteDetailID);
            });
            $scope.deliveryNoteProducts = $scope.method.parseDataToView($scope.data);
        },

        addNewLine: function ($event) {
            $event.preventDefault();
            $scope.deliveryNoteProducts.push({
                rowID: $scope.method.getNewID(),
                productionItemID: null,
                productionItemUD: null,
                productionItemNM: null,
                productionItemTypeID: null,
                unitID: null,
                isNew: true
            });
        },

        getDeliveryNotePrintout: function () {
            jsonService.getDeliveryNotePrintout(
                context.id,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type != 2) {
                        window.open(context.reportUrl + data.data, '');
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        removeDeliveryNoteDetailByID: function (deliveryNoteDetailID) {
            var j = -1;
            for (var i = 0; i < $scope.data.deliveryNoteDetailDTOs.length; i++) {
                if ($scope.data.deliveryNoteDetailDTOs[i].deliveryNoteDetailID == deliveryNoteDetailID) {
                    j = i;
                    break;
                }
            }
            if (j >= 0) {
                $scope.data.deliveryNoteDetailDTOs.splice(j, 1);
            }
        },

        initializeItem: function (workCenterID, includeMaterial, includeComponent) {
            $scope.method.getBOM(function () {
                $scope.event.initItems(workCenterID, includeMaterial, includeComponent);
                $scope.event.initSemiItems(workCenterID, false, true);
            });
        },

        initItems: function (workCenterID, includeMaterial, includeComponent) {
            /*
                reset product detail dto
            */
            $scope.data.deliveryNoteDetailDTOs = [];

            /*
                get bom items base on selected workcenter
            */
            var bomItems = [];
            if ($scope.data.viewName == 'Warehouse2Team' || $scope.data.viewName == 'AmendWarehouse2Team') {
                bomItems = $scope.bomdtOs.filter(function (o) { return o.parentWorkCenterID == workCenterID && !o.isExceptionAtConfirmFrameStatus });
            } else if ($scope.data.viewName == 'Team2Team' || $scope.data.viewName == 'AmendTeam2Team') {
                bomItems = $scope.bomdtOs.filter(function (o) { return o.workCenterID == workCenterID && !o.isExceptionAtConfirmFrameStatus });
            }

            /*
            /   filter production item type base on selected of user
            /   this case is only apply on delivery note from warehouse to team
            */
            if (!includeMaterial) {
                //productionItemTypeID=2: Material
                bomItems = bomItems.filter(function (o) { return o.productionItemTypeID != 2 });
            }
            if (!includeComponent) {
                //productionItemTypeID=1: Component, 3:Piece
                bomItems = bomItems.filter(function (o) { return o.productionItemTypeID != 1 && o.productionItemTypeID != 3 });
            }

            // With work center is packing only work order confirm all
            if (workCenterID == 11) {
                bomItems = bomItems.filter(function (o) { return o.workOrderStatusID == 2 });
            }

            /*
                push to production detail dto
            */
            angular.forEach(bomItems, function (bItem) {
                $scope.data.deliveryNoteDetailDTOs.push({
                    deliveryNoteDetailID: $scope.method.getNewID(),
                    productionItemID: bItem.productionItemID,
                    productionItemUD: bItem.productionItemUD,
                    productionItemNM: bItem.productionItemNM,
                    productionItemTypeID: bItem.productionItemTypeID,
                    qty: 0,
                    qtyByUnit: null,
                    unitID: bItem.unitID,
                    unitNM: bItem.unitNM,
                    productionItemUnits: bItem.productionItemUnits,
                    fromFactoryWarehouseID: bItem.defaultFactoryWarehouseID,
                    bomid: bItem.bomid,
                    bomQnt: bItem.bomQnt,
                    bomQntTotal: bItem.bomQntTotal,
                    workOrderUD: bItem.workOrderUD,
                    workOrderID: bItem.workOrderID,
                    totalDelivery: bItem.totalDelivery,
                    stockQnt: bItem.stockQnt,
                    parentProductionItemNM: bItem.parentProductionItemNM,
                    parentProductionItemUD: bItem.parentProductionItemUD,
                    parentProductionItemID: bItem.parentProductionItemID,
                    unitPrice: bItem.unitPrice,
                    totalDeliverWorkOrder: (bItem.totalDeliverWorkOrder == null) ? 0 : bItem.totalDeliverWorkOrder,
                });
            });

            /*
                get default production team for FromProductionTeamID base on selected workcenterID
            */
            var team = $scope.productionTeams.filter(function (o) { return o.workCenterID == workCenterID && o.isDefault == true; });
            if (team.length > 0) {
                $scope.data.fromProductionTeamID = team[0].productionTeamID;
            }
            else {
                $scope.data.fromProductionTeamID = null;
            }
            $timeout(function () {
                $('#fromProductionTeamID').trigger('change.select2');
            });

            /*
                get default production team for ToProductionTeamID
            */
            if ($scope.data.viewName == 'Warehouse2Team' || $scope.data.viewName == 'AmendWarehouse2Team') {
                //get default production item base on selected workcenterID
                if (team.length > 0) {
                    $scope.data.toProductionTeamID = team[0].productionTeamID;
                }
                else {
                    $scope.data.toProductionTeamID = null;
                }
            }
            else if ($scope.data.viewName == 'Team2Team' || $scope.data.viewName == 'AmendTeam2Team') {
                /*
                / we only can find default production team for "toProductionTeamID" when we make delivery note team 2 team from 1 workOrder
                / we can not find prodcution team when we make delveiry note from many workOrder
                */
                var nextWorkCenterID = $scope.method.getNextWorkCenter(workCenterID);
                var nextTeam = $scope.productionTeams.filter(function (o) { return o.workCenterID == nextWorkCenterID && o.isDefault == true });
                if (nextTeam.length > 0) {
                    $scope.data.toProductionTeamID = nextTeam[0].productionTeamID;
                }
                else {
                    $scope.data.toProductionTeamID = null;
                }
            }

            /*
                trigger changed
            */
            $timeout(function () {
                $('#toProductionTeamID').trigger('change.select2');
            });

            /*
                parse in view
            */
            $scope.deliveryNoteProducts = $scope.method.parseDataToView($scope.data);
        },

        initSemiItems: function (workCenterID, includeMaterial, includeComponent) {
            if ($scope.semiReceiptData === null) return;
            /*
                reset product detail dto
            */
            $scope.semiReceiptData.deliveryNoteDetailDTOs = [];

            /*
                get bom items base on selected workcenter
            */
            var bomItems = [];
            bomItems = $scope.bomdtOs.filter(function (o) { return o.workCenterID == workCenterID && !o.isExceptionAtConfirmFrameStatus });

            /*
            /   filter production item type base on selected of user
            /   this case is only apply on delivery note from warehouse to team
            */
            if (!includeMaterial) {
                //productionItemTypeID=2: Material
                bomItems = bomItems.filter(function (o) { return o.productionItemTypeID != 2 });
            }
            if (!includeComponent) {
                //productionItemTypeID=1: Component, 3:Piece
                bomItems = bomItems.filter(function (o) { return o.productionItemTypeID != 1 && o.productionItemTypeID != 3 });
            }

            // With work center is packing only work order confirm all
            if (workCenterID == 11) {
                bomItems = bomItems.filter(function (o) { return o.workOrderStatusID == 2 });
            }

            /*
                push to production detail dto
            */
            angular.forEach(bomItems, function (bItem) {
                $scope.semiReceiptData.deliveryNoteDetailDTOs.push({
                    deliveryNoteDetailID: $scope.method.getNewID(),
                    productionItemID: bItem.productionItemID,
                    productionItemUD: bItem.productionItemUD,
                    productionItemNM: bItem.productionItemNM,
                    productionItemTypeID: bItem.productionItemTypeID,
                    qty: 0,
                    qtyByUnit: null,
                    unitID: bItem.unitID,
                    unitNM: bItem.unitNM,
                    productionItemUnits: bItem.productionItemUnits,
                    fromFactoryWarehouseID: bItem.defaultFactoryWarehouseID,
                    bomid: bItem.bomid,
                    bomQnt: bItem.bomQnt,
                    bomQntTotal: bItem.bomQntTotal,
                    workOrderUD: bItem.workOrderUD,
                    workOrderID: bItem.workOrderID,
                    totalDelivery: bItem.totalDelivery,
                    stockQnt: bItem.stockQnt,
                    parentProductionItemNM: bItem.parentProductionItemNM,
                    parentProductionItemUD: bItem.parentProductionItemUD,
                    parentProductionItemID: bItem.parentProductionItemID,
                    unitPrice: bItem.unitPrice,
                    totalDeliverWorkOrder: (bItem.totalDeliverWorkOrder == null) ? 0 : bItem.totalDeliverWorkOrder,
                });
            });
            /*
                parse in view
            */
            $scope.deliveryNoteSemiProducts = $scope.method.parseDataToView($scope.semiReceiptData);
        },

        filterMaterial: function () {
            if (!$scope.includeMaterial) {
                $scope.includeMaterial = true;
            } else {
                $scope.includeMaterial = false;
            }

            $scope.event.initializeItem($scope.data.workCenterID, $scope.includeMaterial, $scope.includeComponent);
        },

        filterComponent: function () {
            if (!$scope.includeComponent) {
                $scope.includeComponent = true;
            } else {
                $scope.includeComponent = false;
            }

            $scope.event.initializeItem($scope.data.workCenterID, $scope.includeMaterial, $scope.includeComponent);
        },

        changeFactoryWarehouse: function (item) {
            angular.forEach(item.pieces, function (dItem) {
                dItem.fromFactoryWarehouseID = item.fromFactoryWarehouseID;
            });
        },

        changeUnit: function (item) {
            var itemUnit = item.productionItemUnits.filter(o => o.unitID === item.unitID);
            if (itemUnit.length === 0) {
                bootbox.alert("Could not find sub unit. Please fill-in sub unit for this item");
                return;
            }
            var conversionFactor = item.productionItemUnits.filter(o => o.unitID === item.unitID)[0].conversionFactor;
            if (conversionFactor === null || conversionFactor === 0) {
                bootbox.alert("Missing convertion factor. Please fill-in convertion factory");
                return;
            }
            conversionFactor = parseFloat(conversionFactor);

            //update unitID and stock info, bominfo qnt
            jsonService.getProductionItem(
                item.productionItemID,
                $scope.data.deliveryNoteDate,
                function (data) {
                   
                    //stock info
                    var x = data.data;
                    if (x !== null && x.length > 0) {
                        item.stockQnt = parseFloat(x[0].stockQnt) / conversionFactor;
                    }

                    //update unitID for items that same productionItemID and bom info
                    $scope.method.getBOM(function () {
                        angular.forEach(item.pieces, function (pItem) {
                            //unit
                            pItem.unitID = item.unitID;
                            //bom qnt info
                            var bItems = $scope.bomdtOs.filter(o => o.bomid === pItem.bomid);
                            if (bItems.length > 0) {
                                var bItem = bItems[0];
                                pItem.bomQnt = parseFloat(bItem.bomQuantity) / conversionFactor;
                                pItem.bomQntTotal = parseFloat(bItem.bomQuantityTotal) / conversionFactor;
                                pItem.totalDelivery = parseFloat(bItem.totalDelivery) / conversionFactor;
                            }
                        });
                    });

                    //apply data
                    $timeout(function () {
                        $scope.$apply();
                    });
                },
                function (error) {
                }
            );
        },

        changeFactoryWarehousePallet: function (item) {
            angular.forEach(item.pieces, function (dItem) {
                dItem.factoryWarehousePalletID = item.factoryWarehousePalletID;
            });
        },

        changeOutsourcingCost: function (item) {
            angular.forEach(item.pieces, function (dItem) {
                dItem.outsourcingCostID = item.outsourcingCostID;
            });
        },

        selectedProductionItem: function (data, item) {
            if (data !== null) {
                var funcAsignItem = function () {
                    /*
                        apply selected product
                    */
                    item.productionItemID = data.productionItemID;
                    item.productionItemUD = data.productionItemUD;
                    item.productionItemNM = data.productionItemNM;
                    item.unitID = data.unitID;
                    item.unitNM = data.unitNM;
                    item.productionItemTypeID = data.productionItemTypeID;
                    item.productionItemUnits = data.productionItemUnits;
                    item.defaultFactoryWarehouseID = data.defaultFactoryWarehouseID;
                    item.stockQnt = data.stockQnt;

                    /*
                        push item to deliveryNoteDetailDTOs
                    */
                    //get work center
                    var workCenterID = $scope.data.workCenterID;

                    //filter bom dto
                    var bomItems = [];
                    if ($scope.data.viewName === 'Warehouse2Team' || $scope.data.viewName === 'AmendWarehouse2Team') {
                        bomItems = $scope.bomdtOs.filter(function (o) { return o.parentWorkCenterID === workCenterID && !o.parentIsExceptionAtConfirmFrameStatus });
                    }
                    else if ($scope.data.viewName === 'Team2Team' || $scope.data.viewName === 'AmendTeam2Team') {
                        bomItems = $scope.bomdtOs.filter(function (o) { return o.workCenterID === workCenterID && !o.isExceptionAtConfirmFrameStatus; });
                    }
                    //push to dto detail from filtered bom dto
                    for (var i = 0; i < bomItems.length; i++) {
                        var bomItem = bomItems[i];
                        if (bomItem.productionItemID === item.productionItemID) {
                            var x = $scope.data.deliveryNoteDetailDTOs.filter(o => o.bomid == bomItem.bomid);
                            if (x.length === 0) {
                                $scope.data.deliveryNoteDetailDTOs.push({
                                    deliveryNoteDetailID: $scope.method.getNewID(),
                                    productionItemID: bomItem.productionItemID,
                                    productionItemUD: item.productionItemUD,
                                    productionItemNM: item.productionItemNM,
                                    productionItemTypeID: item.productionItemTypeID,
                                    qty: 0,
                                    qtyByUnit: null,
                                    unitID: bomItem.unitID,
                                    unitNM: bomItem.unitNM,
                                    productionItemUnits: bomItem.productionItemUnits,
                                    fromFactoryWarehouseID: item.defaultFactoryWarehouseID,
                                    bomid: bomItem.bomid,
                                    bomQnt: bomItem.bomQnt,
                                    bomQntTotal: bomItem.bomQntTotal,
                                    workOrderUD: bomItem.workOrderUD,
                                    workOrderID: bomItem.workOrderID,
                                    totalDelivery: bomItem.totalDelivery,
                                    stockQnt: bomItem.stockQnt,
                                    parentProductionItemNM: bomItem.parentProductionItemNM,
                                    parentProductionItemUD: bomItem.parentProductionItemUD,
                                    parentProductionItemID: bomItem.parentProductionItemID,
                                    unitPrice: bomItem.unitPrice,
                                });
                            }
                        }
                    }

                    //push to dto detail free item (not exist in bom)
                    var bom = bomItems.filter(function (o) { return o.productionItemID === item.productionItemID; });
                    if (bom.length === 0) {
                        var workOrderIds = $scope.workOrderList;
                        angular.forEach(workOrderIds, function (woItem) {
                            var x = $scope.data.deliveryNoteDetailDTOs.filter(o => o.productionItemID === item.productionItemID && o.workOrderID === woItem.workOrderID);
                            if (x.length === 0) {
                                $scope.data.deliveryNoteDetailDTOs.push({
                                    deliveryNoteDetailID: $scope.method.getNewID(),
                                    productionItemID: item.productionItemID,
                                    productionItemUD: item.productionItemUD,
                                    productionItemNM: item.productionItemNM,
                                    productionItemTypeID: item.productionItemTypeID,
                                    qty: 0,
                                    qtyByUnit: null,
                                    unitID: item.unitID,
                                    unitNM: item.unitNM,
                                    productionItemUnits: item.productionItemUnits,
                                    fromFactoryWarehouseID: item.defaultFactoryWarehouseID,
                                    workOrderID: woItem.workOrderID,
                                    workOrderUD: woItem.workOrderUD,
                                    stockQnt: item.stockQnt,
                                });
                            }
                        });
                    }

                    /*
                         parse data to show on view
                     */
                    $scope.deliveryNoteProducts = $scope.method.parseDataToView($scope.data);
                    $scope.$apply();
                };
            }

            //get bom items
            $scope.method.getBOM(function () {
                funcAsignItem();
            });
        },

        changeSelectWorkCenter: function (item) {
            if (item !== 9) {
                $scope.data.transportationServiceID = null;
            }
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

        getNextWorkCenter: function (workCenterID) {
            //only effected when select from 1 workorder, not effected when select many workOrder
            var nextWorkCenterID = -1;
            if ($scope.bomdtOs !== null) {
                var _bomItem = $scope.bomdtOs.filter(function (o) { return o.workCenterID == workCenterID });
                if (_bomItem.length > 0) {
                    nextWorkCenterID = _bomItem[0].parentWorkCenterID;
                }
            }
            return nextWorkCenterID;
        },

        resetDeliveryDetail: function () {
            $scope.data.deliveryNoteDetailDTOs = [];
            $scope.deliveryNoteProducts = $scope.method.parseDataToView($scope.data);
        },

        parseDataToView: function (data) {
            var deliveryNoteProducts = [];
            var indexs = [];
            for (var i = 0; i < data.deliveryNoteDetailDTOs.length; i++) {
                var item = data.deliveryNoteDetailDTOs[i];
                if (item.productionItemID !== null) {
                    keyID = item.productionItemID.toString();
                    if (indexs.indexOf(keyID) < 0) {
                        indexs.push(keyID);
                        var pItem = {
                            deliveryNoteDetailID: $scope.method.getNewID(),
                            productionItemID: item.productionItemID,
                            productionItemUD: item.productionItemUD,
                            productionItemNM: item.productionItemNM,
                            unitID: item.unitID,
                            productionItemUnits: item.productionItemUnits,
                            stockQnt: item.stockQnt,
                            fromFactoryWarehouseID: item.fromFactoryWarehouseID,
                            factoryWarehousePalletID: item.factoryWarehousePalletID,
                            isNew: false,
                            pieces: [],
                            totalDelivery: item.totalDelivery,
                            unitPrice: item.unitPrice,
                            qtyByUnit: item.qtyByUnit,
                            unitNM: item.unitNM,
                            fromFactoryWarehouseNM: item.fromFactoryWarehouseNM,
                            factoryWarehousePalletNM: item.factoryWarehousePalletNM,
                            workOrderID: item.workOrderID,
                            bomid: item.bomid,
                            productionItemTypeID: item.productionItemTypeID,
                            totalDeliverWorkOrder: item.totalDeliverWorkOrder,
                            outsourcingCostID: item.outsourcingCostID,
                            outsourcingCostNM: item.outsourcingCostNM
                        };

                        angular.forEach(data.deliveryNoteDetailDTOs, function (sItem) {
                            if (pItem.productionItemID == sItem.productionItemID) {
                                sItem.qtyByUnit_Old = sItem.qtyByUnit;
                                pItem.pieces.push(sItem);
                            }
                        });

                        deliveryNoteProducts.push(pItem);
                    }
                }
            }
            return deliveryNoteProducts;
        },

        getBOM: function (successCallBack) {
            if ($scope.bomdtOs !== null) {
                successCallBack();
            }
            else {
                var workOrderIDs = $scope.data.workOrderIDs;
                jsonService.getBOM(
                    workOrderIDs,
                    context.branchID,
                    $scope.data.deliveryNoteDate,
                    function (data) {
                        //get bomdtos before init items
                        $scope.bomdtOs = data.data;
                        $timeout(function () {
                            $scope.$apply();
                        });
                        successCallBack();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.bomdtOs = null;
                    }
                );
            }
        },

        getFactoryWarehouseNM: function (id) {
            var name = null;

            if (id !== null) {
                angular.forEach($scope.factoryWarehouses, function (item) {
                    if (item.factoryWarehouseID === id) {
                        name = item.factoryWarehouseNM;
                    }
                });
            }

            return name;
        },

        getReasonOtherPriceName: function (reasonOtherPriceID) {
            var x = $scope.reasonOtherPrices.filter(o => o.reasonOtherPriceID === reasonOtherPriceID);
            if (x.length > 0) {
                return x.filter(o => o.reasonOtherPriceID === reasonOtherPriceID)[0].reasonOtherPriceName;
            }
            return '';
        }
    };

    //
    //edit or addd production item
    //
    $scope.productionItemForm = {
        editingNotde: null,

        productionItemID: null,
        data: null,
        event: {
            load: function (productionItemID, copyOfcurrentNode) {
                $scope.productionItemForm.editingNotde = copyOfcurrentNode;
                $scope.productionItemForm.productionItemID = productionItemID;
                $scope.productionItemForm.event.getProductionItem(productionItemID);
            },
            getProductionItem: function (id) {
                BOMService.getProductionItem(
                    id,
                    function (data) {
                        $scope.productionItemForm.data = data.data;
                        if ($scope.productionItemForm.data.productionItemID == null) $scope.productionItemForm.data.productionItemTypeID = 2; //Type of Material
                        $('#frmProductInfo').modal('show');
                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.productionItemForm.data = null;
                        $scope.$apply();
                    }
                );
            },
            updateProductionItem: function () {
                BOMService.updateProductionItem(
                    $scope.productionItemForm.productionItemID,
                    $scope.productionItemForm.data,
                    function (data) {
                        if (data.message.type === 0) {
                            //adjust data for editing node
                            var productItemData = data.data;
                            $scope.productionItemForm.editingNotde.productionItemID = productItemData.productionItemID;
                            $scope.productionItemForm.editingNotde.productionItemUD = productItemData.productionItemUD;
                            $scope.productionItemForm.editingNotde.productionItemNM = productItemData.productionItemNM;
                            $scope.productionItemForm.editingNotde.unit = productItemData.unit;
                            $scope.productionItemForm.editingNotde.productThumbnailImageUrl = productItemData.thumbnailLocation;
                            $scope.productionItemForm.editingNotde.productFullSizeImageUrl = productItemData.fileLocation;
                            $scope.$apply();
                            $('#frmProductInfo').modal('hide');
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },

            cancel: function () {
                $('#frmProductInfo').modal('hide');
            },

            changeImage: function ($event) {
                $event.preventDefault();
                masterUploader.multiSelect = true;
                masterUploader.imageOnly = false;
                masterUploader.callback = function () {
                    var scope = angular.element(jQuery('body')).scope();
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            scope.productionItemForm.data.thumbnailLocation = img.fileURL;
                            scope.productionItemForm.data.productImage_NewFile = img.filename;
                            scope.productionItemForm.data.productImage_HasChange = true;
                        }, null);
                    });
                };
                masterUploader.open();
            },
            removeImage: function ($event) {
                $scope.data.showroomItemThumbnailImage = '';
                $scope.data.imageFile_NewFile = '';
                $scope.data.imageFile_HasChange = true;
            }

        }
    };

    //
    // init
    //
    $scope.event.load();
}]);