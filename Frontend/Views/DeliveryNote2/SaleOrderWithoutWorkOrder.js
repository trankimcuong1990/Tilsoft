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
            stockQnt: item.stockQnt,
            totalDelivery: item.totalDelivery,
            unitPrice: item.unitPrice,
            productionItemUnits: item.productionItemUnits
        };
    });
}

function formatFactorySaleOrder(data) {
    return $.map(data.data, function (item) {
        return {
            description: '',
            id: item.id,
            label: item.label,
            value: item.value,

            factorySaleOrderID: item.factorySaleOrderID,
            factorySaleOrderUD: item.factorySaleOrderUD,
            factoryRawMaterialID: item.factoryRawMaterialID,
            factoryRawMaterialUD: item.factoryRawMaterialUD,
            factoryRawMaterialOfficialNM: item.factoryRawMaterialOfficialNM,
            factoryRawMaterialDocumentRefNo: item.factoryRawMaterialDocumentRefNo,
            factoryRawMaterialContactPersonNM: item.factoryRawMaterialContactPersonNM,
            factoryShippingMethodID: item.factoryShippingMethodID,
            factoryPaymentTermID: item.factoryPaymentTermID,
            currency: item.currency,
            factorySaleOrderStatusID: item.factorySaleOrderStatusID,
            shippingAddress: item.shippingAddress,
            billingAddress: item.billingAddress,
            factoryRawMaterialShortNM: item.factoryRawMaterialShortNM,
            factorySaleOrderDetailDTOs: item.factorySaleOrderDetailDTOs
        };
    });
}

function formatPurchaseOrderQuicksearch(purchaseOrder) {
    return $.map(purchaseOrder.data, function (item) {
        return {
            description: '',
            id: item.purchaseOrderID,
            label: item.purchaseOrderUD,
            value: item.purchaseOrderUD,
            purchaseOrderDetails: item.purchaseOrderDetails,
            supplierID: item.supplierID,
            supplierShortNM: item.supplierShortNM,
            supplierFullNM: item.supplierFullNM,
            supplierAddress: item.supplierAddress,

        };
    });
};

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
    $scope.factoryWarehousePallets = [];
    $scope.factoryWarehouses = [];
    $scope.productionTeams = [];
    $scope.productionItemTypes = [];
    $scope.workCenters = [];
    $scope.subSuppliers = [];
    $scope.statusTypes = [];
    $scope.newID = -1;
    $scope.listProductionItemUnits = [];

    $scope.ngInitParam3 = null;

    //
    //autocomplete textbox
    //
    $scope.productionItemBox = {
        request: {
            url: context.getProductionItemUrl,
            token: context.token
        },
        data: {
            description: '',
            id: null,
            label: '',
            value: '',

            productionItemID: null,
            productionItemUD: '',
            productionItemNM: '',
            unitID: null,
            defaultFactoryWarehouseID: null,
            stockQnt: null,
            totalDelivery: null,
            unitPrice: null,
            productionItemUnits: []
        }
    };

    $scope.factorySaleOrderAPI = {
        url: context.getFactorySaleOrder,
        token: context.token
    };
    $scope.factorySaleOrder = {
        factorySaleOrderID: null,
        factorySaleOrderUD: null,
        factoryRawMaterialID: null,
        factoryRawMaterialUD: null,
        factoryRawMaterialOfficialNM: null,
        factoryRawMaterialDocumentRefNo: null,
        factoryRawMaterialContactPersonNM: null,
        factoryShippingMethodID: null,
        factoryPaymentTermID: null,
        currency: null,
        factorySaleOrderStatusID: null,
        shippingAddress: null,
        billingAddress: null,
        factoryRawMaterialShortNM: null,
        factorySaleOrderDetailDTOs: []
    };

    $scope.purchaseOrderQuicksearch = {
        restfulAPIs: {
            url: jsonService.serviceUrl + 'purchaseorderquicksearch',
            token: context.token,
        },
        data: {
            id: null,
            label: null,
            value: null,
            description: null,
            purchaseOrderDetails: [],
            supplierID: null,
            supplierShortNM: null,
            supplierFullNM: null,
            supplierAddress: null,
        },
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

                    //incase add new: initialize  param
                    if (context.id == 0) {
                        $scope.data.viewName = context.viewName;
                    }
                    //get support list
                    $scope.factoryWarehousePallets = data.data.factoryWarehousePallets;
                    $scope.factoryWarehouses = data.data.factoryWarehouses;
                    $scope.productionTeams = data.data.productionTeams;
                    $scope.productionItemTypes = data.data.productionItemTypes;
                    $scope.workCenters = data.data.allWorkCenters;
                    $scope.subSuppliers = data.data.subSuppliers;
                    $scope.listProductionItemUnits = data.data.listProductionItemUnits;

                    $scope.statusTypes = data.data.statusTypes;

                    // Default status type is 2 "From Return"
                    if ($scope.data.statusTypeID === null) {
                        $scope.data.statusTypeID = 2;
                    }

                    $scope.data.viewName = 'SaleOrderWithoutWorkOrder';

                    angular.forEach($scope.subSuppliers, function (item) {
                        if (item.id === $scope.data.subSupplierID) {
                            $scope.data.subSupplierNM = item.subSupplierFullNM;
                        }
                    });

                    $scope.$apply();

                    jQuery('#content').show();
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                    //assign select2
                    $timeout(function () {
                        jQuery("#workCenterID").select2();
                        jQuery("#toProductionTeamID").select2();
                        jQuery('#subSupplier').select2();
                    }, 0);
                    

                    jQuery('#deliveryNoteDetailWithoutWorkOrderLoading').hide();
                    jQuery('#deliveryNoteDetailWithoutWorkOrder').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },

        update: function () {
            // current branch
            $scope.data.branchID = context.branchID;
            $scope.data.deliveryNoteDate = jQuery('#deliveryNoteDate').val();

            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.deliveryNoteID);
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
            if (confirm('Are you sure you want to delete delivery note ?')) {
                jsonService.deleteWithPermission(
                    context.id,
                    $scope.data.createdBy,
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
            var index = $scope.data.deliveryNoteDetailDTOs.indexOf(item);
            $scope.data.deliveryNoteDetailDTOs.splice(index, 1);
        },

        addNewLine: function ($event) {
            $event.preventDefault();

            if ($scope.data.statusTypeID === null) {
                jsHelper.showMessage('warning', 'Please select one status type!');
                return;
            }

            if ($scope.data.statusTypeID === 2 && $scope.data.purchaseOrderID === null) {
                jsHelper.showMessage('warning', 'Please input one purchase order!');
                return;
            }

            if ($scope.data.statusTypeID === 5 && $scope.data.factorySaleOrderID === null) {
                jsHelper.showMessage('warning', 'Please input one factory saleOrder!');
                return;
            }

            $scope.data.deliveryNoteDetailDTOs.push({
                deliveryNoteDetailID: $scope.method.getNewID(),
                productionItemID: null,
                productionItemUD: null,
                productionItemNM: null,
                unitID: null,
                productionItemUnits: [],
                fromFactoryWarehouseID: null,
                stockQnt: null,
                totalDelivery: null,
                unitPrice: null
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

        changeWorkCenter: function (workCenterID) {
            /*
               get default production team for FromProductionTeamID base on selected workcenterID
           */
            var team = $scope.productionTeams.filter(function (o) { return o.workCenterID == workCenterID && o.isDefault == true });
            if (team.length > 0) {
                $scope.data.toProductionTeamID = team[0].productionTeamID;
                $scope.data.receiverName = team[0].productionTeamNM;
            }
            else {
                $scope.data.toProductionTeamID = null;
                $scope.data.receiverName = null;
            }
            /*
                trigger changed
            */
            $timeout(function () {
                $('#toProductionTeamID').trigger('change.select2');
            });

            //receiver address
            var workCenter = $scope.workCenters.filter(function (o) { return o.workCenterID == workCenterID });
            $scope.data.receiverAddress = workCenter[0].workCenterNM;
        },

        changeTeam: function (productionTeamID) {
            var team = $scope.productionTeams.filter(function (o) { return o.productionTeamID == productionTeamID });
            $scope.data.receiverName = team[0].productionTeamNM;
        },

        changeSubSupplier: function (subSupplierID) {
            for (var i = 0; i < $scope.subSuppliers.length; i++) {
                var item = $scope.subSuppliers[i];
                if (item.id === subSupplierID) {
                    $scope.data.receiverName = item.label;
                    $scope.data.receiverAddress = item.address;
                    return false;
                }
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
            //get stock qnt
            jsonService.getProductionItem(
                item.productionItemID,
                $scope.data.deliveryNoteDate,
                function (data) {
                    var x = data.data;
                    if (x !== null && x.length > 0) {
                        item.stockQnt = parseFloat(x[0].stockQnt) / conversionFactor;
                        $timeout(function () {
                            $scope.$apply();
                        });
                    }
                },
                function (error) {
                }
            );
        },

        selectedProductionItem: function (data, item) {
            if (data !== null) {
                var x = $scope.data.deliveryNoteDetailDTOs.filter(o => o.productionItemID === data.productionItemID);
                if (x.length > 0) {
                    /*
                        cancel select product
                    */
                    $timeout(function () {
                        bootbox.alert('This item already exist in this receipt');
                    });
                }
                else {
                    /*
                        apply selected product
                    */
                    item.productionItemID = data.productionItemID;
                    item.productionItemUD = data.productionItemUD;
                    item.productionItemNM = data.productionItemNM;
                    item.unitID = data.unitID;
                    item.unitNM = data.unitNM;
                    item.productionItemUnits = data.productionItemUnits;
                    item.fromFactoryWarehouseID = data.defaultFactoryWarehouseID;
                    item.stockQnt = data.stockQnt;
                    item.totalDelivery = data.totalDelivery;
                    item.unitPrice = data.unitPrice;
                    if ($scope.data.statusTypeID === 3) {
                        item.unitPrice = null;
                    }
                }
                $scope.$apply();
            }
        },

        selectedFactorySaleOrder: function (factorySaleOrder) {
            if (factorySaleOrder !== null) {
                $scope.data.factorySaleOrderID = factorySaleOrder.id;
                $scope.data.factorySaleOrderUD = factorySaleOrder.label;

                $scope.data.subSupplierID = factorySaleOrder.factoryRawMaterialID;
                $scope.data.subSupplierNM = factorySaleOrder.factoryRawMaterialShortNM;
                $scope.data.receiverName = factorySaleOrder.factoryRawMaterialOfficialNM;
                $scope.data.receiverAddress = factorySaleOrder.billingAddress;

                $scope.ngInitParam3 = $scope.data.factorySaleOrderID;

                $timeout(function () {
                    jQuery('#subSupplier').trigger('change.select2');
                }, 0);

                jQuery('#deliveryNoteDetailWithoutWorkOrderLoading').show();
                jQuery('#deliveryNoteDetailWithoutWorkOrder').hide();

                jQuery('#addProductionItem').attr('disabled', 'disabled');

                $scope.$apply();

                jsonService.factorySaleOrderDetailQuicksearch(
                    $scope.data.factorySaleOrderID,
                    context.branchID,
                    $scope.data.deliveryNoteDate,
                    function (data) {
                        var i = -1;
                        angular.forEach(data.data, function (item) {
                            var deliveryNoteDetail = {
                                factorySaleOrderDetailID: item.factorySaleOrderDetailID,
                                deliveryNoteDetailID: i,
                                fromFactoryWarehouseID: item.factoryWarehouseID,
                                productionItemID: item.productionItemID,
                                productionItemUD: item.productionItemUD,
                                productionItemNM: item.productionItemNM,
                                stockQnt: item.stockQnt,
                                unitID: item.unitID,
                                factoryWarehousePalletID: null,
                                totalDelivery: item.totalDelivery,
                                unitPrice: item.unitPrice,
                                quantity: item.quantity,
                                qtyByUnit: null,
                                description: null,
                                productionItemUnits: item.productionItemUnits,
                            };

                            $scope.data.deliveryNoteDetailDTOs.push(deliveryNoteDetail);

                            i = i - 1;
                        });

                        jQuery('#deliveryNoteDetailWithoutWorkOrderLoading').hide();
                        jQuery('#deliveryNoteDetailWithoutWorkOrder').show();

                        jQuery('#addProductionItem').removeAttr('disabled');

                        $scope.$apply();
                    },
                    function (error) {
                    });
            }
        },

        selectedPurchaseOrder: function (purchaseOrder) {
            if (purchaseOrder !== null) {
                $scope.data.purchaseOrderID = purchaseOrder.id;
                $scope.data.purchaseOrderUD = purchaseOrder.label;

                $scope.data.subSupplierID = purchaseOrder.supplierID;
                $scope.data.subSupplierNM = purchaseOrder.supplierShortNM;
                $scope.data.receiverName = purchaseOrder.supplierFullNM;
                $scope.data.receiverAddress = purchaseOrder.supplierAddress;

                $scope.ngInitParam3 = $scope.data.purchaseOrderID;

                $timeout(function () {
                    jQuery('#subSupplier').trigger('change.select2');
                }, 0);

                jQuery('#deliveryNoteDetailWithoutWorkOrderLoading').show();
                jQuery('#deliveryNoteDetailWithoutWorkOrder').hide();

                jQuery('#addProductionItem').attr('disabled', 'disabled');

                $scope.$apply();

                jsonService.purchaseOrderDetailQuicksearch(
                    $scope.data.purchaseOrderID,
                    context.branchID,
                    $scope.data.deliveryNoteDate,
                    function (data) {
                        var i = -1;
                        angular.forEach(data.data, function (item) {
                            var deliveryNoteDetail = {
                                deliveryNoteDetailID: i,
                                fromFactoryWarehouseID: item.factoryWarehouseID,
                                productionItemID: item.productionItemID,
                                productionItemUD: item.productionItemUD,
                                productionItemNM: item.productionItemNM,
                                stockQnt: item.stockQnt,
                                unitID: item.unitID,
                                factoryWarehousePalletID: null,
                                totalDelivery: item.totalDelivery,
                                unitPrice: item.unitPrice,
                                qty: null,
                                qtyByUnit: null,
                                description: null,
                                productionItemUnits: item.productionItemUnits,
                                orderQnt: item.orderQnt,
                                purchaseOrderDetailID: item.purchaseOrderDetailID
                            };

                            $scope.data.deliveryNoteDetailDTOs.push(deliveryNoteDetail);

                            i = i - 1;
                        });

                        jQuery('#deliveryNoteDetailWithoutWorkOrderLoading').hide();
                        jQuery('#deliveryNoteDetailWithoutWorkOrder').show();

                        jQuery('#addProductionItem').removeAttr('disabled');

                        $scope.$apply();
                    },
                    function (error) {
                    });
            }
        },

        changePurchaseOrder: function () {
            /// Clear data link from purchase order
            $scope.method.resetPurchaseOrderLinked();
        },

        changeStatusType: function () {
            /// Clear purchase order linked
            $scope.data.purchaseOrderUD = null;
            $scope.method.resetPurchaseOrderLinked();

            /// Clear factory saleOrder linked
            $scope.data.factorySaleOrderUD = null;
            $scope.method.resetFactorySaleOrderLinked();

            /// Clear outsource linked
            $scope.method.resetOutsourceLinked();

            /// Clear other linked
            $scope.method.resetOtherLinked();

            $scope.ngInitParam3 = null;

            $timeout(function () {
                jQuery("#workCenterID").select2();
                jQuery("#toProductionTeamID").select2();
                jQuery('#subSupplier').select2();
            }, 0);
        },

        changeProductionItem: function (item) {
            if (item !== null) {
                item.productionItemID = null;
                item.stockQnt = null;
                item.totalDelivery = null;
                item.unitPrice = null;
                item.unitID = null;
                item.qtyByUnit = null;
                item.fromFactoryWarehouseID = null;
                item.factoryWarehousePalletID = null;
            }
        },
    };

    //
    // method
    //
    $scope.qnt = 0;

    $scope.method = {
        refresh: function (id) {
            window.location = context.refreshUrl + id
        },

        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },

        resetPurchaseOrderLinked: function () {
            $scope.data.purchaseOrderID = null;
            $scope.data.receiverName = null;
            $scope.data.receiverAddress = null;
            $scope.data.subSupplierID = null;

            $timeout(function () {
                jQuery('#subSupplier').trigger('change.select2');
                jQuery('#subSupplier').select2();
            }, 0);

            $scope.data.deliveryNoteDetailDTOs = [];
        },

        resetFactorySaleOrderLinked: function () {
            $scope.data.factorySaleOrderID = null;
            $scope.data.receiverName = null;
            $scope.data.receiverAddress = null;
            $scope.data.subSupplierID = null;

            $timeout(function () {
                jQuery('#subSupplier').trigger('change.select2');
                jQuery('#subSupplier').select2();
            }, 0);

            $scope.data.deliveryNoteDetailDTOs = [];
        },

        resetOutsourceLinked: function () {
            $scope.data.receiverName = null;
            $scope.data.receiverAddress = null;
            $scope.data.subSupplierID = null;

            $timeout(function () {
                jQuery('#subSupplier').trigger('change.select2');
                jQuery('#subSupplier').select2();
            }, 0);

            $scope.data.deliveryNoteDetailDTOs = [];
        },

        resetOtherLinked: function () {
            $scope.data.workCenterID = null;
            $scope.data.toProductionTeamID = null;
            $scope.data.receiverName = null;
            $scope.data.receiverAddress = null;
            $scope.data.subSupplierID = null;

            $timeout(function () {
                jQuery('#subSupplier').trigger('change.select2');
                jQuery('#subSupplier').select2();
            }, 0);

            $scope.data.deliveryNoteDetailDTOs = [];
        },
    };

    //
    // init
    //
    $scope.event.load();
}]);