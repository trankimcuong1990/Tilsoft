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
            description: item.productionItemUD,
            id: item.productionItemID,
            label: item.productionItemNM,
            value: item.productionItemNM,

            productionItemID: item.productionItemID,
            productionItemUD: item.productionItemUD,
            productionItemNM: item.productionItemNM,
            unitID: item.unitID,
            productionItemTypeID: item.productionItemTypeID,
            toFactoryWarehouseID: item.factoryWarehouseID,
            productionItemUnits: item.productionItemUnits,
            frameWeight: item.frameWeight,
            isHasFrameWeight: item.isHasFrameWeight,
            unitPrice: item.unitPrice,
            stockQnt: item.stockQnt,
            totalReceive: item.totalReceive,

            bomid: item.bomid,
            workOrderID: item.workOrderID,
            workOrderUD: item.workOrderUD,
            parentProductionItemID: item.parentProductionItemID,
            parentProductionItemUD: item.parentProductionItemUD,
            parentProductionItemNM: item.parentProductionItemNM,
            bomQnt: item.bomQnt,
            bomQntTotal: item.bomQntTotal,
        };
    });
}

var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives']);
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
    $scope.data = [];
    $scope.factoryWarehousePallets = [];
    $scope.factoryWarehouses = [];
    $scope.productionTeams = [];
    $scope.workCenters = [];
    $scope.productionItemTypes = [];
    $scope.newID = -1;    
    $scope.workOrderList = [];
    $scope.bomdtOs = null;
    $scope.statusTypes = [];
    $scope.data.receivingNoteDate = "";
    $scope.transportationServiceDTOs = [];
    $scope.receivingNoteProducts = [];
    $scope.receivingNoteSemiProducts = [];

    //
    //data for semi product receipt
    //
    $scope.semiReceiptData = null;

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
            toFactoryWarehouseID: null,
            productionItemUnits: null,
            unitPrice: null,
            stockQnt: null,
            totalReceive: null,

            // New fields
            bomid: null,
            workOrderID: null,
            workOrderUD: null,
            parentProductionItemID: null,
            parentProductionItemUD: null,
            parentProductionItemNM: null,
            bomQnt: null,
            bomQntTotal: null,
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
                    $scope.workCenters = data.data.workCenters;
                    $scope.productionItemTypes = data.data.productionItemTypes;
                    $scope.workOrderList = data.data.workOrderList;
                    $scope.statusTypes = data.data.statusTypes;
                    $scope.transportationServiceDTOs = data.data.transportationServiceDTOs;
                    $scope.reasonOtherPrices = data.data.reasonOtherPrices;
                    $scope.data.statusTypeNM = $scope.statusTypes.filter(o => o.statusTypeID === $scope.data.statusTypeID)[0].statusTypeNM;
                    $scope.data.viewName = context.viewName;

                    //assign select2
                    $("#workCenterID").select2();
                    $("#fromProductionTeamID").select2();
                    confirmDialogFactory.setSelected('workCenterID', $scope.data.workCenterID);
                    $scope.$on('popupCloseEvent_workCenterID', function (event, data) {
                        $scope.data.workCenterID = data.selectedItem;
                        $timeout(function () {
                            $scope.$apply(function () {
                                $('#workCenterID').trigger('change.select2');
                            });
                        }, 100);
                        if (data.result) {
                            //$scope.event.initializeItem($scope.data.workCenterID, true, true);
                        }
                    });
                   
                    //
                    //show data on view
                    //
                    $scope.receivingNoteProducts = $scope.method.parseDataToView($scope.data);

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
                            $scope.receivingNoteSemiProducts = $scope.method.parseDataToView($scope.semiReceiptData);
                            $scope.$apply();
                            $('#workCenterID').trigger('change.select2');
                            $('#fromProductionTeamID').trigger('change.select2');
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
                });
        },

        update: function () {
            if ($scope.editForm.$valid) {
                $scope.data.receivingNoteDate = jQuery('#receivingNoteDate').val();
                //assing value & check data
                for (let i = 0; i < $scope.receivingNoteProducts.length; i++) {
                    let item = $scope.receivingNoteProducts[i];
                    if (item.pieces.filter(o => o.qtyByUnit !== null && o.qtyByUnit !== '' && parseFloat(o.qtyByUnit) !== 0).length > 0 && item.toFactoryWarehouseID === null) {
                        bootbox.alert('Please select warehouse');
                        return;
                    }
                    angular.forEach(item.pieces, function (pItem) {
                        pItem.unitPrice = item.unitPrice === 0 ? null : item.unitPrice;
                        pItem.frameWeight = item.frameWeight;
                        pItem.toFactoryWarehouseID = item.toFactoryWarehouseID;
                    });
                }

                //assign value & check factory of semi receipt
                for (let i = 0; i < $scope.receivingNoteSemiProducts.length; i++) {
                    let item = $scope.receivingNoteSemiProducts[i];
                    if (item.pieces.filter(o => o.qtyByUnit !== null && o.qtyByUnit !== '' && parseFloat(o.qtyByUnit) !== 0).length > 0 && item.toFactoryWarehouseID === null) {
                        bootbox.alert('Please select warehouse for semi item');
                        return;
                    }
                    angular.forEach(item.pieces, function (pItem) {
                        pItem.unitPrice = item.unitPrice === 0 ? null : item.unitPrice;
                        pItem.frameWeight = item.frameWeight;
                        pItem.toFactoryWarehouseID = item.toFactoryWarehouseID;
                    });
                }
                //update data
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            var receivingNoteID = data.data.receivingNoteID;
                            var workOrderIDs = data.data.workOrderIDs;
                            var x = $scope.semiReceiptData.receivingNoteDetailDTOs.filter(o => o.qtyByUnit !== null && o.qtyByUnit !== '' && parseFloat(o.qtyByUnit) !== 0);
                            if (x.length > 0) {
                                //assign info for semi-receipt
                                $scope.semiReceiptData.fromProductionTeamID = $scope.data.fromProductionTeamID;
                                $scope.semiReceiptData.workCenterID = $scope.data.workCenterID;
                                $scope.semiReceiptData.parentID = data.data.receivingNoteID;
                                //update data for semi rceipt
                                var semiReceiptID = $scope.data.semiReceiptID !== null ? $scope.data.semiReceiptID : 0;
                                jsonService.update(
                                    semiReceiptID,
                                    $scope.semiReceiptData,
                                    function (data) {
                                        jsHelper.processMessage(data.message);
                                        //refresh receipt
                                        $scope.method.refresh(receivingNoteID, workOrderIDs);
                                    },
                                    function (error) {
                                        jsHelper.showMessage('warning', error);
                                    }
                                );
                            }
                            else {
                                //refresh receipt
                                $scope.method.refresh(receivingNoteID, workOrderIDs);
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
                $scope.event.removeReceivingNoteDetailByID(pItem.receivingNoteDetailID);
            });
            $scope.receivingNoteProducts = $scope.method.parseDataToView($scope.data);
        },

        addNewLine: function ($event) {
            $event.preventDefault();
            $scope.receivingNoteProducts.push({
                rowID: $scope.method.getNewID(),
                productionItemID: null,
                productionItemUD: null,
                productionItemNM: null,
                unitID: null,
                isAddNew: true,
                price: null
            });
        },

        getReceivingNotePrintout: function () {
            jsonService.getReceivingNotePrintout(
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

        checkPrintOut: function (item, branchID) {
            window.open(context.commonURL);
            if (item == 9 && branchID == 1) {
                window.open(context.vanphatURL);
            }
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
            var stockQnt = parseFloat(0);
            angular.forEach(item.pieces, function (pItem) {
                pItem.unitID = item.unitID;
                //bom qnt info
                //$scope.method.getBOM(function () {
                //    var bItems = $scope.bomdtOs.filter(o => o.bomid === pItem.bomid);
                //    if (bItems.length > 0) {
                //        var bItem = bItems[0];
                //        pItem.bomQnt = parseFloat(bItem.bomQnt) / conversionFactor;
                //        pItem.totalReceive = parseFloat(bItem.totalReceive) / conversionFactor;
                //        stockQnt = parseFloat(bItem.stockQnt);
                //        //apply data
                //        $timeout(function () {
                //            $scope.$apply();
                //        });
                //    }
                //});
            });
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
        },

        changeFactoryWarehouse: function (item) {
            angular.forEach(item.pieces, function (kItem) {
                kItem.toFactoryWarehouseID = item.toFactoryWarehouseID;
            });
        },

        removeReceivingNoteDetailByID: function (receivingNoteDetailID) {
            var j = -1;
            for (var i = 0; i < $scope.data.receivingNoteDetailDTOs.length; i++) {
                if ($scope.data.receivingNoteDetailDTOs[i].receivingNoteDetailID == receivingNoteDetailID) {
                    j = i;
                    break;
                }
            }
            if (j >= 0) {
                $scope.data.receivingNoteDetailDTOs.splice(j, 1);
            }
        },

        initializeItem: function (workCenterID, fromProductionTeamID) {
            $scope.method.getBOM(function () {
                $scope.event.initItems(workCenterID, fromProductionTeamID);
                $scope.event.initSemiItems(workCenterID, fromProductionTeamID);
            });
        },

        initItems: function (workCenterID, fromProductionTeamID) {
            /*
                reset product detail dto
            */
            $scope.data.receivingNoteDetailDTOs = [];

            /*
                get bom items base on workcenter
            */
            var bomItems = $scope.bomdtOs.filter(function (o) { return o.workCenterID === workCenterID && !o.isExceptionAtConfirmFrameStatus;});

            /*
                push to production detail dto
            */
            angular.forEach(bomItems, function (bItem) {
                $scope.data.receivingNoteDetailDTOs.push({
                    receivingNoteDetailID: $scope.method.getNewID(),
                    productionItemID: bItem.productionItemID,
                    productionItemUD: bItem.productionItemUD,
                    productionItemNM: bItem.productionItemNM,
                    unitID: bItem.unitID,
                    unitNM: bItem.unitNM,
                    productionItemUnits: bItem.productionItemUnits,
                    toFactoryWarehouseID: bItem.defaultFactoryWarehouseID,
                    bomid: bItem.bomid,
                    bomQnt: bItem.bomQnt,
                    workOrderID: bItem.workOrderID,
                    workOrderUD: bItem.workOrderUD,
                    totalReceiving: bItem.totalReceiving,
                    parentProductionItemNM: bItem.parentProductionItemNM,
                    frameWeight: bItem.frameWeight,
                    productionItemTypeID: bItem.productionItemTypeID,
                    isHasFrameWeight: bItem.isHasFrameWeight,
                    unitPrice: bItem.unitPrice,
                    parentProductionItemUD: bItem.parentProductionItemUD,
                    stockQnt: bItem.stockQnt,
                    totalReceive: bItem.totalReceive,
                    bomQntTotal: bItem.bomQntTotal,
                    totalReceiveWorkOrder: bItem.totalReceiveWorkOrder,
                    quantity: 0,
                    qtyByUnit: null
                });
            });

            /*
                get default production team for FromProductionTeamID base on selected workcenterID
            */
            var team = $scope.productionTeams.filter(function (o) { return o.workCenterID == workCenterID && o.isDefault == true });
            if (team.length > 0) {
                $scope.data.fromProductionTeamID = team[0].productionTeamID;
            }
            else {
                $scope.data.fromProductionTeamID = null;
            }

            /*
                trigger changed
            */
            $timeout(function () {
                $('#fromProductionTeamID').trigger('change.select2');
            });

            /*
                parse in view
            */
            $scope.receivingNoteProducts = $scope.method.parseDataToView($scope.data);            
        },

        initSemiItems: function (workCenterID, fromProductionTeamID) {
            if ($scope.semiReceiptData === null) return;
            /*
                reset product detail dto
            */
            $scope.semiReceiptData.receivingNoteDetailDTOs = [];

            /*
                get bom items base on workcenter
            */
            var bomItems = $scope.bomdtOs.filter(function (o) { return o.workCenterID === workCenterID && !o.isExceptionAtConfirmFrameStatus; });

            /*
                push to production detail dto
            */
            angular.forEach(bomItems, function (bItem) {
                $scope.semiReceiptData.receivingNoteDetailDTOs.push({
                    receivingNoteDetailID: $scope.method.getNewID(),
                    productionItemID: bItem.productionItemID,
                    productionItemUD: bItem.productionItemUD,
                    productionItemNM: bItem.productionItemNM,
                    unitID: bItem.unitID,
                    unitNM: bItem.unitNM,
                    productionItemUnits: bItem.productionItemUnits,
                    toFactoryWarehouseID: null,
                    bomid: bItem.bomid,
                    bomQnt: bItem.bomQnt,
                    workOrderID: bItem.workOrderID,
                    workOrderUD: bItem.workOrderUD,
                    totalReceiving: bItem.totalReceiving,
                    parentProductionItemNM: bItem.parentProductionItemNM,
                    frameWeight: bItem.frameWeight,
                    productionItemTypeID: bItem.productionItemTypeID,
                    isHasFrameWeight: bItem.isHasFrameWeight,
                    unitPrice: bItem.unitPrice,
                    parentProductionItemUD: bItem.parentProductionItemUD,
                    stockQnt: bItem.stockQnt,
                    totalReceive: bItem.totalReceive,
                    bomQntTotal: bItem.bomQntTotal,
                    totalReceiveWorkOrder: bItem.totalReceiveWorkOrder,
                    quantity: 0,
                    qtyByUnit: null
                });
            });
            /*
                parse in view
            */            
            $scope.receivingNoteSemiProducts = $scope.method.parseDataToView($scope.semiReceiptData);
        },

        selectedProductionItem: function (data, item) {
            if (data !== null) {
                var isExist = $scope.method.isExistProductionItemInReceivingProduct(data.productionItemID);

                if (!isExist) {
                    var funcAsignItem = function () {
                        //apply selected product
                        item.productionItemID = data.productionItemID;
                        item.productionItemUD = data.productionItemUD;
                        item.productionItemNM = data.productionItemNM;
                        item.unitID = data.unitID;
                        item.unitNM = data.unitNM;
                        item.productionItemTypeID = data.productionItemTypeID;
                        item.productionItemUnits = data.productionItemUnits;
                        item.toFactoryWarehouseID = data.toFactoryWarehouseID;
                        item.frameWeight = data.frameWeight;
                        item.isHasFrameWeight = data.isHasFrameWeight;
                        item.unitPrice = data.unitPrice;
                        item.stockQnt = data.stockQnt;
                        item.totalReceive = data.totalReceive;

                        jsonService.getBOMProductionItem(
                            data.productionItemUD,
                            $scope.data.workOrderIDs,
                            $scope.data.workCenterID,
                            context.branchID,
                            $scope.data.receivingNoteDate,
                            function (data) {
                                angular.forEach(data.data, function (sItem) {
                                    if (item.productionItemID === sItem.productionItemID) {
                                        // New fields
                                        item.bomid = sItem.bomid;
                                        item.workOrderID = sItem.workOrderID;
                                        item.workOrderUD = sItem.workOrderUD;
                                        item.parentProductionItemID = sItem.parentProductionItemID;
                                        item.parentProductionItemUD = sItem.parentProductionItemUD;
                                        item.parentProductionItemNM = sItem.parentProductionItemNM;
                                        item.bomQnt = sItem.bomQnt;
                                        item.bomQntTotal = sItem.bomQntTotal;

                                        //push item to receivingNoteDetailDTOs
                                        var workCenterID = $scope.data.workCenterID;
                                        var bomItems = $scope.bomdtOs.filter(function (o) { return o.workCenterID === workCenterID && !o.isExceptionAtConfirmFrameStatus; });
                                        angular.forEach(bomItems, function (bomItem) {
                                            if (bomItem.productionItemID === item.productionItemID) {
                                                var x = $scope.data.receivingNoteDetailDTOs.filter(o => o.bomid == bomItem.bomid);
                                                if (x.length === 0) {
                                                    $scope.data.receivingNoteDetailDTOs.push({
                                                        receivingNoteDetailID: $scope.method.getNewID(),
                                                        productionItemID: bomItem.productionItemID,
                                                        productionItemUD: bomItem.productionItemUD,
                                                        productionItemNM: item.productionItemNM,
                                                        quantity: 0,
                                                        qtyByUnit: null,
                                                        unitID: item.unitID,
                                                        unitNM: item.unitNM,
                                                        productionItemUnits: bomItem.productionItemUnits,
                                                        toFactoryWarehouseID: item.toFactoryWarehouseID,
                                                        workOrderID: bomItem.workOrderID,
                                                        workOrderUD: bomItem.workOrderUD,
                                                        totalReceiving: bomItem.totalReceiving,
                                                        parentProductionItemNM: item.parentProductionItemNM,
                                                        productionItemTypeID: bomItem.productionItemTypeID,
                                                        frameWeight: item.frameWeight,
                                                        isHasFrameWeight: item.isHasFrameWeight,
                                                        unitPrice: item.unitPrice,
                                                        stockQnt: item.stockQnt,
                                                        totalReceive: item.totalReceive,
                                                        parentProductionItemUD: item.parentProductionItemUD,
                                                        parentProductionItemID: item.parentProductionItemID,
                                                        bomQnt: item.bomQnt,
                                                        bomQntTotal: item.bomQntTotal,
                                                        bomid: item.bomid,
                                                    });
                                                }
                                            }
                                        });

                                        ////push to dto detail free item (not exist in bom)
                                        var bom = bomItems.filter(function (o) { return o.productionItemID == item.productionItemID; });
                                        if (bom.length === 0) {
                                            var workOrderIds = $scope.workOrderList;
                                            angular.forEach(workOrderIds, function (woItem) {
                                                var x = $scope.data.receivingNoteDetailDTOs.filter(o => o.productionItemID === item.productionItemID && o.workOrderID === sItem.workOrderID);
                                                if (x.length === 0 && woItem.workOrderID == sItem.workOrderID) {
                                                    $scope.data.receivingNoteDetailDTOs.push({
                                                        receivingNoteDetailID: $scope.method.getNewID(),
                                                        productionItemID: item.productionItemID,
                                                        productionItemUD: item.productionItemUD,
                                                        productionItemNM: item.productionItemNM,
                                                        quantity: 0,
                                                        qtyByUnit: null,
                                                        unitID: item.unitID,
                                                        unitNM: item.unitNM,
                                                        productionItemUnits: item.productionItemUnits,
                                                        toFactoryWarehouseID: item.toFactoryWarehouseID,
                                                        workOrderID: item.workOrderID,
                                                        workOrderUD: item.workOrderUD,
                                                        productionItemTypeID: item.productionItemTypeID,
                                                        frameWeight: item.frameWeight,
                                                        isHasFrameWeight: item.isHasFrameWeight,
                                                        unitPrice: item.unitPrice,
                                                        stockQnt: item.stockQnt,
                                                        totalReceive: item.totalReceive,
                                                        parentProductionItemID: item.parentProductionItemID,
                                                        parentProductionItemUD: item.parentProductionItemUD,
                                                        parentProductionItemNM: item.parentProductionItemNM,
                                                        bomQnt: item.bomQnt,
                                                        bomQntTotal: item.bomQntTotal,
                                                        bomid: item.bomid,
                                                    });
                                                }
                                            });
                                        }
                                    }
                                    else {
                                        // New fields
                                        item.bomid = sItem.bomid;
                                        item.workOrderID = sItem.workOrderID;
                                        item.workOrderUD = sItem.workOrderUD;
                                        item.parentProductionItemID = sItem.parentProductionItemID;
                                        item.parentProductionItemUD = sItem.parentProductionItemUD;
                                        item.parentProductionItemNM = sItem.parentProductionItemNM;
                                        item.bomQnt = sItem.bomQnt;
                                        item.bomQntTotal = sItem.bomQntTotal;

                                        //push item to receivingNoteDetailDTOs
                                        workCenterID = $scope.data.workCenterID;
                                        bomItems = $scope.bomdtOs.filter(function (o) { return o.workCenterID === workCenterID && !o.isExceptionAtConfirmFrameStatus; });
                                        angular.forEach(bomItems, function (bomItem) {
                                            if (bomItem.productionItemID === item.productionItemID) {
                                                var x = $scope.data.receivingNoteDetailDTOs.filter(o => o.bomid == bomItem.bomid);
                                                if (x.length === 0) {
                                                    $scope.data.receivingNoteDetailDTOs.push({
                                                        receivingNoteDetailID: $scope.method.getNewID(),
                                                        productionItemID: bomItem.productionItemID,
                                                        productionItemUD: bomItem.productionItemUD,
                                                        productionItemNM: item.productionItemNM,
                                                        quantity: 0,
                                                        qtyByUnit: null,
                                                        unitID: item.unitID,
                                                        unitNM: item.unitNM,
                                                        productionItemUnits: bomItem.productionItemUnits,
                                                        toFactoryWarehouseID: item.toFactoryWarehouseID,
                                                        workOrderID: bomItem.workOrderID,
                                                        workOrderUD: bomItem.workOrderUD,
                                                        totalReceiving: bomItem.totalReceiving,
                                                        parentProductionItemNM: item.parentProductionItemNM,
                                                        productionItemTypeID: bomItem.productionItemTypeID,
                                                        frameWeight: item.frameWeight,
                                                        isHasFrameWeight: item.isHasFrameWeight,
                                                        unitPrice: item.unitPrice,
                                                        stockQnt: item.stockQnt,
                                                        totalReceive: item.totalReceive,
                                                        parentProductionItemUD: item.parentProductionItemUD,
                                                        parentProductionItemID: item.parentProductionItemID,
                                                        //bomQnt: item.bomQnt,
                                                        //bomQntTotal: item.bomQntTotal,
                                                        //bomid: item.bomid,
                                                    });
                                                }
                                            }
                                        });

                                        ////push to dto detail free item (not exist in bom)
                                        bom = bomItems.filter(function (o) { return o.productionItemID == item.productionItemID; });
                                        if (bom.length === 0) {
                                            workOrderIds = $scope.workOrderList;
                                            angular.forEach(workOrderIds, function (woItem) {
                                                var x = $scope.data.receivingNoteDetailDTOs.filter(o => o.productionItemID === item.productionItemID && o.workOrderID === woItem.workOrderID);
                                                if (x.length === 0) {
                                                    $scope.data.receivingNoteDetailDTOs.push({
                                                        receivingNoteDetailID: $scope.method.getNewID(),
                                                        productionItemID: item.productionItemID,
                                                        productionItemUD: item.productionItemUD,
                                                        productionItemNM: item.productionItemNM,
                                                        quantity: 0,
                                                        qtyByUnit: null,
                                                        unitID: item.unitID,
                                                        unitNM: item.unitNM,
                                                        productionItemUnits: item.productionItemUnits,
                                                        toFactoryWarehouseID: item.toFactoryWarehouseID,
                                                        workOrderID: item.workOrderID,
                                                        workOrderUD: item.workOrderUD,
                                                        productionItemTypeID: item.productionItemTypeID,
                                                        frameWeight: item.frameWeight,
                                                        isHasFrameWeight: item.isHasFrameWeight,
                                                        unitPrice: item.unitPrice,
                                                        stockQnt: item.stockQnt,
                                                        totalReceive: item.totalReceive,
                                                        parentProductionItemID: item.parentProductionItemID,
                                                        parentProductionItemUD: item.parentProductionItemUD,
                                                        parentProductionItemNM: item.parentProductionItemNM,
                                                        //bomQnt: item.bomQnt,
                                                        //bomQntTotal: item.bomQntTotal,
                                                        //bomid: item.bomid,
                                                    });
                                                }
                                            });
                                        }
                                    }
                                });

                                //parse data to show on view
                                $scope.receivingNoteProducts = $scope.method.parseDataToView($scope.data);
                                $scope.$apply();
                            },
                            function (error) {
                            });
                    };

                    //get bom items
                    $scope.method.getBOM(function () {
                        funcAsignItem();
                    });
                } else {
                    jsHelper.showMessage('warning', 'ProductionItem ' + data.productionItemNM + ' is existed!');
                    return;
                }
            }
        },

        setContentDetail: function (item) {
            //jsonService.setContentDetail(
            //    item.productionItemID,
            //    item.toFactoryWarehouseID,
            //    function (data) { 
            //        item.stockQnt = data.data.stockQnt;
            //        item.totalReceive = data.data.totalReceive;
            //        $scope.$apply();
            //    },
            //    function (error) {
            //    });
        },

        changeSelectWorkCenter: function (item) {
            if (item !== 9) {
                $scope.data.transportationServiceID = null;
            }
        },

        changeUnitItem: function (item) {
            angular.forEach(item.pieces, function (subItem) {
                subItem.unitID = item.unitID;
            });
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

        parseDataToView: function (data) {
            var receivingNoteProducts = [];
            var indexs = [];
            angular.forEach(data.receivingNoteDetailDTOs, function (item) {
                if (item.productionItemID !== null) {
                    keyID = item.productionItemID.toString();
                    if (indexs.indexOf(keyID) < 0) {
                        indexs.push(keyID);
                        var pItem = {
                            isAddNew: false,
                            rowID: $scope.method.getNewID(),
                            productionItemID: item.productionItemID,
                            productionItemUD: item.productionItemUD,
                            productionItemNM: item.productionItemNM,
                            unitID: item.unitID,
                            unitNM: item.unitNM,
                            productionItemUnits: item.productionItemUnits,
                            toFactoryWarehouseID: item.toFactoryWarehouseID,
                            isNew: false,
                            pieces: [],
                            productionItemTypeID: item.productionItemTypeID,
                            unitPrice: (item.unitPrice === null) ? 0 : item.unitPrice,
                            stockQnt: item.stockQnt,
                            totalReceive: item.totalReceive,
                            qtyByUnit: item.qtyByUnit,
                            toFactoryWarehouseNM: item.toFactoryWarehouseNM,
                            factoryWarehousePalletNM: item.factoryWarehousePalletNM,
                            frameWeight: item.frameWeight,
                            parentProductionItemID: item.parentProductionItemID,
                            parentProductionItemUD: item.parentProductionItemUD,
                            parentProductionItemNM: item.parentProductionItemNM,
                            bomQnt: item.bomQnt,
                            bomQntTotal: item.bomQntTotal,
                            isHasFrameWeight: item.isHasFrameWeight,
                            totalReceiveWorkOrder: item.totalReceiveWorkOrder,
                        };
                        receivingNoteProducts.push(pItem);
                        angular.forEach(data.receivingNoteDetailDTOs, function (sItem) {
                            if (pItem.productionItemID === sItem.productionItemID) {
                                pItem.pieces.push(sItem);
                            }
                        });
                    }
                }
            });
            return receivingNoteProducts;
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
                    $scope.data.receivingNoteDate,
                    $scope.data.workCenterID,
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

        getWorkCenterName: function (workCenterID, workCenters) {
            var workCenterNM = '';

            angular.forEach(workCenters, function (item) {
                if (item.workCenterID == workCenterID) {
                    workCenterNM = item.workCenterNM;
                }
            });

            return workCenterNM;
        },

        getTeamName: function (fromProductionTeamID, productionTeams) {
            var teamName = '';

            angular.forEach(productionTeams, function (item) {
                if (item.productionTeamID == fromProductionTeamID) {
                    teamName = item.productionTeamNM;
                }
            });

            return teamName;
        },

        isExistProductionItemInReceivingProduct: function (productionItemID) {
            var isExist = false;

            if ($scope.receivingNoteProducts.length == 0) {
                return isExist;
            }

            angular.forEach($scope.receivingNoteProducts, function (item) {
                if (item.productionItemID == productionItemID) {
                    isExist = true;
                }
            });

            return isExist;
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
    // init
    //
    $scope.event.load();
}]);