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
    $scope.newID = -1;

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

    $scope.addItemCost = function () {
        var pItem = {
            factorySaleInvoiceDetailID: $scope.event.getNewID(),
            productionItemTypeID: 7
        };
        $scope.data.factorySaleInvoiceDetailDTOs.push(pItem);
    };

    $scope.deleteFromProductionItem = function (item) {
        var index = $scope.data.factorySaleInvoiceDetailDTOs.indexOf(item);
        $scope.data.factorySaleInvoiceDetailDTOs.splice(index, 1);
    };

    $scope.event = {
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },

        changeSupplier: function (factoryRawMaterialID) {
            if (factoryRawMaterialID !== null) {
                for (let i = 0; i < $scope.supportData.factoryRawMaterialDTOs.length; i++) {
                    let item = $scope.supportData.factoryRawMaterialDTOs[i];
                    if (factoryRawMaterialID === item.subSupplierID) {
                        $scope.data.factoryRawMaterialOfficialNM = item.subSupplierFullNM;
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

        //Amount
        itemAmount: function () {
            let amount = 0;
            if ($scope.data.factorySaleInvoiceDetailDTOs.length > 0) {
                for (let i = 0; i < $scope.data.factorySaleInvoiceDetailDTOs.length; i++) {
                    let item = $scope.data.factorySaleInvoiceDetailDTOs[i];
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
            
            //if ($scope.data.purchaseInvoiceID === 0) {
            //    $scope.event.vatAmount();
            //}

            return amount;
        },
        costAmount: function () {
            let amount = 0;
            if ($scope.data.factorySaleInvoiceDetailDTOs.length > 0) {
                for (let i = 0; i < $scope.data.factorySaleInvoiceDetailDTOs.length; i++) {
                    let item = $scope.data.factorySaleInvoiceDetailDTOs[i];
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
            if ($scope.data.factorySaleInvoiceDetailDTOs.length > 0) {
                for (let i = 0; i < $scope.data.factorySaleInvoiceDetailDTOs.length; i++) {
                    let item = $scope.data.factorySaleInvoiceDetailDTOs[i];
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
            return (itemAmount + costAmount) * $scope.data.vat / 100;
            //$scope.data.vatAmount = (itemAmount + costAmount) * $scope.data.vat / 100;
        },
        totalAmount: function () {
            let itemAmount = 0;
            let costAmount = 0;
            let vatAmount = 0;
            if ($scope.data.factorySaleInvoiceDetailDTOs.length > 0) {
                for (let i = 0; i < $scope.data.factorySaleInvoiceDetailDTOs.length; i++) {
                    let item = $scope.data.factorySaleInvoiceDetailDTOs[i];
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
            if ((itemAmount + costAmount) !== 0) {
                vatAmount = (itemAmount + costAmount) * $scope.data.vat / 100;
            }
            else {
                vatAmount = 0;
            }
            return costAmount + itemAmount + parseInt(vatAmount);
        },

        changeFile: function (type) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        $scope.data.friendlyName = img.friendlyName;
                        $scope.data.fileLocation = img.fileURL;
                        $scope.data.file_NewFile = img.filename;
                        $scope.data.file_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
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
    };

    //
    //Status
    //
    $scope.factorySaleInvoiceStatus = {
        setFactorySaleInvoiceStatus: function () {
            const statusName = $scope.supportData.purchaseInvoiceStatusDTOs.filter(o => o.factorySaleInvoiceStatusID === $scope.data.factorySaleInvoiceStatusID)[0].factorySaleInvoiceStatusNM;
            bootbox.confirm("Are you sure you want to " + statusName, function (result) {
                if (result) {
                    dataService.setFactorySaleInvoiceStatus($scope.data.factorySaleInvoiceID, $scope.data.factorySaleInvoiceStatusID,
                        function (data) {
                            jsHelper.processMessage(data.message);
                            if (data.message.type === 0) {
                                window.location = context.refreshUrl + $scope.data.factorySaleInvoiceID + '#/';
                            }
                            if ($scope.data.factorySaleInvoiceStatusID === 2) {
                                if ($scope.data.factorySaleInvoiceTypeID === 2) {
                                    $scope.eventPO.getDataPO();
                                }
                            }
                        },
                        function (errorData) {
                            $scope.data.factorySaleInvoiceStatusID = errorData.data.data;
                        });
                }
            });
        }
    };

    //
    //init
    //
    $scope.initController();
}]);