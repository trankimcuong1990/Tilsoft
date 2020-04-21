function formatProductionItem(data) {
    return $.map(data.data, function (item) {
        return {
            description: item.productionItemUD,
            id: item.productionItemID,
            label: item.productionItemNM,
            value: item.productionItemUD,

            productionItemID: item.productionItemID,
            productionItemUD: item.productionItemUD,
            productionItemNM: item.productionItemNM,
            unitNM: item.unitNM,
            factoryWarehouseID: item.factoryWarehouseID,
            productionItemTypeID: item.productionItemTypeID
        };
    });
}
tilsoftApp.controller('_GeneralController', ['$scope', 'dataService', function ($scope, dataService) {
    //
    //property
    //

    //
    //init controller
    //
    $scope.initController = function () {
        //
        // init service
        //
        dataService.serviceUrl = context.serviceUrl;
        dataService.supportServiceUrl = context.supportServiceUrl;
        dataService.token = context.token;
        //
        //        
        jQuery('#content').show();
    };

    $scope.supportData.spCurrency = [
        { sname: 'VND', name: 'VND' },
        { sname: 'USD', name: 'USD' }
    ];

    $scope.supportData.spcheckPercentVAT = [
        { id: 0, name: '0%' },
        { id: 5, name: '5%' },
        { id: 10, name: '10%' }

    ];
    $scope.newID = -1;
    $scope.supportAutomaticData = {
        dataPO: null,
        flagUpdatePO: false,
        dataReceiving: null,
        flagUpdateReceiving: false
    };
    
    //
    //autocomplete textbox
    //
    $scope.productionBox = {
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
            unitNM: null,
            factoryWarehouseID: null,
            productionItemTypeID: null
        },
        selectedItem: function (data, item) {
            if (data !== null) {
                item.productionItemID = data.productionItemID;
                item.productionItemUD = data.productionItemUD;
                item.productionItemNM = data.productionItemNM;
                item.unitNM = data.unitNM;
                item.factoryWarehouseID = data.factoryWarehouseID;
                if (data.productionItemTypeID === 7) {
                    item.quantity = 1;
                }
                else {
                    item.quantity = 0;
                }
                item.productionItemTypeID = data.productionItemTypeID;
            }
            $scope.$apply();
        }
    };

    $scope.addItem = function () {
        var pItem = {
            purchaseInvoiceDetailID: $scope.event.getNewID(),
            type:0
        };
        $scope.data.purchaseInvoiceDetailDTOs.push(pItem);
    };
    $scope.addItemCost = function () {
        var pItem = {
            purchaseInvoiceDetailID: $scope.event.getNewID(),
            productionItemTypeID: 7
        };
        $scope.data.purchaseInvoiceDetailDTOs.push(pItem);
    };

    $scope.deleteFromProductionItem = function (item) {
        var index = $scope.data.purchaseInvoiceDetailDTOs.indexOf(item);
        $scope.data.purchaseInvoiceDetailDTOs.splice(index, 1);

        $scope.removeIteminListNote(item);
    };

    $scope.removeIteminListNote = function (item) {
        angular.forEach($scope.data.listNoteStatuses, function (noteItem) {
            if (item.purchaseOrderID === noteItem.purchaseOrderID && item.productionItemID === noteItem.productionItemID && item.type === noteItem.type) {
                var index = $scope.data.listNoteStatuses.indexOf(noteItem);
                $scope.data.listNoteStatuses.splice(index, 1);
            }
        });
    };

    $scope.event = {
        changeSupplier: function (factoryRawMaterialID) {
            if (factoryRawMaterialID !== null) {
                for (let i = 0; i < $scope.supportData.factoryRawMaterialDTOs.length; i++) {
                    let item = $scope.supportData.factoryRawMaterialDTOs[i];
                    if (factoryRawMaterialID === item.factoryRawMaterialID) {
                        $scope.data.factoryRawMaterialOfficialNM = item.factoryRawMaterialOfficialNM;
                    }
                }

                dataService.getSupplierPaymentTerm(
                    factoryRawMaterialID,
                    function (data) {
                        $scope.supportData.paymentTerms = data.data;
                    },
                    function (errorData) {

                    }
                );
            }
        },
        //file
        changeFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.friendlyName = fileUploader2.selectedFriendlyName;
                    $scope.data.fileLocation = fileUploader2.selectedFileUrl;
                    $scope.data.file_NewFile = fileUploader2.selectedFileName;
                    $scope.data.file_HasChange = true;
                });
            };
            fileUploader2.open();
        },
        removeFile: function () {
            $scope.data.friendlyName = '';
            $scope.data.fileLocation = '';
            $scope.data.file_NewFile = '';
            $scope.data.file_HasChange = true;
        },
        downloadFile: function () {
            if ($scope.data.fileLocation) {
                window.open($scope.data.fileLocation);
            }
        },

        //Amount
        itemAmount: function () {
            let amount = 0;
            if ($scope.data.purchaseInvoiceDetailDTOs.length > 0) {
                for (let i = 0; i < $scope.data.purchaseInvoiceDetailDTOs.length; i++) {
                    let item = $scope.data.purchaseInvoiceDetailDTOs[i];
                    if (item.productionItemTypeID !== 7) {
                        if (item.unitPrice !== null && item.quantity !== null) {
                            amount += (item.unitPrice * item.quantity);
                        } else {
                            amount += 0;
                        }
                    }
                }
            }
            else {
                amount = 0;
            }
            return amount;
        },
        costAmount: function () {
            let amount = 0;
            if ($scope.data.purchaseInvoiceDetailDTOs.length > 0) {
                for (let i = 0; i < $scope.data.purchaseInvoiceDetailDTOs.length; i++) {
                    let item = $scope.data.purchaseInvoiceDetailDTOs[i];
                    if (item.productionItemTypeID === 7) {
                        if (item.unitPrice !== null && item.quantity !== null) {
                            amount += (item.unitPrice * item.quantity);
                        } else {
                            amount += 0;
                        }
                    }
                }
            }
            else {
                amount = 0;
            }
            return amount;
        },
        vatAmount: function () {
            let itemAmount = 0;
            let costAmount = 0;
            if ($scope.data.purchaseInvoiceDetailDTOs.length > 0) {
                for (let i = 0; i < $scope.data.purchaseInvoiceDetailDTOs.length; i++) {
                    let item = $scope.data.purchaseInvoiceDetailDTOs[i];
                    if (item.productionItemTypeID === 7) {
                        if (item.unitPrice !== null && item.quantity !== null) {
                            costAmount += (item.unitPrice * item.quantity);
                        } else {
                            costAmount += 0;
                        }
                    }

                    if (item.productionItemTypeID !== 7) {
                        if (item.unitPrice !== null && item.quantity !== null) {
                            itemAmount += (item.unitPrice * item.quantity);
                        } else {
                            itemAmount += 0;
                        }
                    }
                }
            }
            else {
                costAmount = 0;
                itemAmount = 0;
            }
            $scope.data.vatAmount = (itemAmount + costAmount) * $scope.data.vat / 100;
            $scope.data.totalAmount = costAmount + itemAmount + parseInt($scope.data.vatAmount);
        },
        totalAmount: function () {
            let itemAmount = 0;
            let costAmount = 0;
            let vatAmount = 0;
            if ($scope.data.purchaseInvoiceDetailDTOs.length > 0) {
                for (let i = 0; i < $scope.data.purchaseInvoiceDetailDTOs.length; i++) {
                    let item = $scope.data.purchaseInvoiceDetailDTOs[i];
                    if (item.productionItemTypeID === 7) {
                        if (item.unitPrice !== null && item.quantity !== null) {
                            costAmount += (item.unitPrice * item.quantity);
                        } else {
                            costAmount += 0;
                        }
                    }

                    if (item.productionItemTypeID !== 7) {
                        if (item.unitPrice !== null && item.quantity !== null) {
                            itemAmount += (item.unitPrice * item.quantity);
                        } else {
                            itemAmount += 0;
                        }
                    }
                }
            }
            else {
                costAmount = 0;
                itemAmount = 0;
            }
            $scope.data.totalAmount = costAmount + itemAmount + parseInt($scope.data.vatAmount);
        },

        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },

        checkInList: function (fItem, list) {
            if (list.length === 0) {
                return -1;
            }

            for (let e = 0; e < list.length; e++) {
                var eItem = list[e];
                if (fItem.productionItemID === eItem.productionItemID) {
                    return list.indexOf(eItem);
                }
            }

            return -1;
        },

        getTotalOrder: function (productionItemID, purchaseOrderDetail) {
            var totalOrderQnt = 0;

            angular.forEach(purchaseOrderDetail, function (itemDetail) {
                if (itemDetail.productionItemID === productionItemID) {
                    totalOrderQnt += parseFloat(itemDetail.quantity);
                }
            });

            return totalOrderQnt;
        },

        resetDetail: function () {
            $scope.data.purchaseInvoiceDetailDTOs = [];
        }
    };

    //
    //Status
    //
    $scope.purchaseInvoiceStatus = {
        setPurchaseInvoiceStatus: function () {
            const statusName = $scope.supportData.purchaseInvoiceStatusDTOs.filter(o => o.purchaseInvoiceStatusID === $scope.data.purchaseInvoiceStatusID)[0].purchaseInvoiceStatusNM;
            bootbox.confirm("Are you sure you want to " + statusName, function (result) {
                if (result) {
                    dataService.setPurchaseInvoiceStatus($scope.data.purchaseInvoiceID, $scope.data.purchaseInvoiceStatusID,
                        function (data) {
                            jsHelper.processMessage(data.message);
                            if (data.message.type === 0) {
                                window.location = context.refreshUrl + $scope.data.purchaseInvoiceID + '#/';
                            }
                            if ($scope.data.purchaseInvoiceStatusID === 2) {
                                if ($scope.data.purchaseInvoiceTypeID === 2) {
                                    $scope.eventPO.getDataPO();
                                }
                            }
                        },
                        function (errorData) {
                            $scope.data.purchaseInvoiceStatusID = errorData.data.data;
                        });
                }
            });
        }
    };

    //
    //Automatic create PO and ReceivingPO
    //
    $scope.eventPO = {
        getDataPO: function () {
            dataService.getPurchaseOrderFromInvoice(
                context.purchaseOrderAPI,
                0,
                null,
                function (data) {
                    $scope.supportAutomaticData.dataPO = data.data.data;
                    //push data for PO
                    $scope.supportAutomaticData.dataPO.remark = "From Purchasing Invoice - Purchasing Order";
                    $scope.supportAutomaticData.dataPO.purchaseOrderDate = $scope.data.postingDate;
                    $scope.supportAutomaticData.dataPO.eta = $scope.data.postingDate;
                    $scope.supportAutomaticData.dataPO.currency = $scope.data.currency;
                    $scope.supportAutomaticData.dataPO.attachedFile = $scope.data.attachedFile;
                    $scope.supportAutomaticData.dataPO.vat = $scope.data.vat;
                    $scope.supportAutomaticData.dataPO.factoryRawMaterialID = $scope.data.factoryRawMaterialID;
                    $scope.supportAutomaticData.dataPO.factoryRawMaterialUD = $scope.data.factoryRawMaterialUD;
                    $scope.supportAutomaticData.dataPO.purchaseOrderStatusID = 4;
                    $scope.supportAutomaticData.dataPO.season = jsHelper.getCurrentSeason();
                    //push data for PO Detail
                    if ($scope.data.purchaseInvoiceDetailDTOs !== undefined) {
                        for (let i = 0; i < $scope.data.purchaseInvoiceDetailDTOs.length; i++) {
                            let item = $scope.data.purchaseInvoiceDetailDTOs[i];
                            let newItem = {
                                purchaseOrderDetailID: $scope.event.getNewID(),
                                orderQnt: item.quantity,
                                vat: $scope.data.vat,
                                unitPrice: item.unitPrice,
                                productionItemID: item.productionItemID,
                                eta: $scope.data.postingDate,
                                productionItemTypeID: item.productionItemTypeID
                            };
                            $scope.supportAutomaticData.dataPO.purchaseOrderDetailDTOs.push(newItem);
                        }
                    }

                    //Tinh chi phi cho Item trong PO
                    var totalAmountCost = 0;
                    var totalAmountItem = 0;
                    var checkHasItemCost = false;
                    for (let j = 0; j < $scope.supportAutomaticData.dataPO.purchaseOrderDetailDTOs.length; j++) {
                        let jItem = $scope.supportAutomaticData.dataPO.purchaseOrderDetailDTOs[j];
                        if (jItem.UnitPrice !== null && jItem.OrderQnt !== null) {
                            if (jItem.ProductionItemTypeID === 7) {
                                totalAmountCost = totalAmountCost + (jItem.UnitPrice * jItem.OrderQnt);
                                //checkHasItemCost = true;
                            }
                            else {
                                totalAmountItem = totalAmountItem + (jItem.UnitPrice * jItem.OrderQnt);
                            }
                        }
                        else {
                            totalAmountCost = 0;
                            totalAmountItem = 0;
                        }
                    }

                    //if (checkHasItemCost) {
                    //    for (let x = 0; j < $scope.supportAutomaticData.dataPO.purchaseOrderDetailGeneralDTOs.length; x++) {
                    //        let xItem = $scope.supportAutomaticData.dataPO.purchaseOrderDetailGeneralDTOs[x];
                    //        var Amount = 0;
                    //        var freeCostOfItem = 0;
                    //        if (xItem.UnitPrice !== null && xItem.OrderQnt !== null) {
                    //            Amount = xItem.UnitPrice * xItem.OrderQnt;
                    //        }
                    //        else {
                    //            Amount = 0;
                    //        }
                    //        if (xItem.ProductionItemTypeID !== 7) {
                    //            freeCostOfItem = Amount / totalAmountItem * totalAmountCost;
                    //            if (xItem.OrderQnt !== null && xItem.OrderQnt !== 0) {
                    //                xItem.Cost = freeCostOfItem / xItem.OrderQnt;
                    //            }
                    //            else {
                    //                xItem.Cost = 0;
                    //            }
                    //        }
                    //    }
                    //}

                    //create PO
                    if ($scope.supportAutomaticData.flagUpdatePO === false) {
                        $scope.eventPO.updatePO($scope.supportAutomaticData.dataPO);
                    }
                },
                function (error) {
                    // do nothing
                });
        },
        updatePO: function (dataPO) {
            dataService.updatePurchaseOrderFromInvoice(
                context.updatePurchaseOrderAPI,
                0,
                dataPO,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {

                        $scope.supportAutomaticData.dataPO = data.data;
                        $scope.supportAutomaticData.flagUpdatePO = true;
                        $scope.eventPO.getDataReceiving();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        },

        getDataReceiving: function () {
            var param = {
                typeID: 0
            };
            dataService.getReceivingFromInvoice(
                context.receivingAPI,
                0,
                0,
                0,
                0,
                param,
                function (data) {
                    $scope.supportAutomaticData.dataReceiving = data.data.data;
                    //push data for PO
                    $scope.supportAutomaticData.dataReceiving.description = "From Purchasing Invoice - Receiving PO";
                    $scope.supportAutomaticData.dataReceiving.receivingDate = $scope.data.postingDate;
                    $scope.supportAutomaticData.dataReceiving.eta = $scope.data.postingDate;
                    $scope.supportAutomaticData.dataReceiving.receivingReceiptTypeID = 2;
                    $scope.supportAutomaticData.dataReceiving.purchasingOrderNo = $scope.supportAutomaticData.dataPO.purchaseOrderUD;
                    $scope.supportAutomaticData.dataReceiving.purchaseOrderUD = $scope.supportAutomaticData.dataPO.purchaseOrderUD;
                    $scope.supportAutomaticData.dataReceiving.purchaseOrderID = $scope.supportAutomaticData.dataPO.purchaseOrderID;
                    $scope.supportAutomaticData.dataReceiving.isConfirm = true;
                    $scope.supportAutomaticData.dataReceiving.viewName = "PO2WarehouseWithoutWorkOrder";
                    $scope.supportAutomaticData.dataReceiving.deliverName = $scope.supportAutomaticData.dataPO.factoryRawMaterialOfficialNM;
                    $scope.supportAutomaticData.dataReceiving.deliverAddress = $scope.supportAutomaticData.dataPO.address;
                    for (let i = 0; i < $scope.supportAutomaticData.dataPO.purchaseOrderDetailDTOs.length; i++) {
                        let item = $scope.supportAutomaticData.dataPO.purchaseOrderDetailDTOs[i];
                        let generalItem = {
                            receivingPOItemID: $scope.event.getNewID(),
                            productionItemID: item.productionItemID,
                            quantity: item.orderQnt,
                            orderQnt: item.orderQnt,
                            qtyByUnit: item.orderQnt,
                            unitID: item.unitID,
                            unitNM: item.unitNM,
                            purchaseOrderID: item.purchaseOrderID,
                            toFactoryWarehouseID: item.factoryWarehouseID,
                            unitPrice: item.unitPrice,
                            purchaseOrderDetailID: item.purchaseOrderDetailID
                        };
                        for (let i = 0; i < $scope.data.purchaseInvoiceDetailDTOs.length; i++) {
                            let invoiceItem = $scope.data.purchaseInvoiceDetailDTOs[i];
                            if (item.productionItemID === invoiceItem.productionItemID) {
                                generalItem.toFactoryWarehouseID = invoiceItem.factoryWarehouseID;
                            }
                        }
                        $scope.supportAutomaticData.dataReceiving.receivingNoteDetailDTOs.push(generalItem);
                    }

                    //create Receiving PO
                    if ($scope.supportAutomaticData.flagUpdateReceiving === false) {
                        $scope.eventPO.updateReceiving($scope.supportAutomaticData.dataReceiving);
                    }
                },
                function (error) {
                    // do nothing
                });
        },

        updateReceiving: function (dataReceiving) {
            dataService.updateReceivingFromInvoice(
                context.updateReceivingAPI,
                0,
                dataReceiving,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        $scope.supportAutomaticData.dataReceiving = data.data;
                        $scope.supportAutomaticData.flagUpdateReceiving = true;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        },
    };

    //
    //init
    //
    $scope.initController();


}]);
