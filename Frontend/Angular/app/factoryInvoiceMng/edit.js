//
// SUPPORT
//
//jQuery('#main-form').keypress(function (e) {
//    if (e.keyCode == 13) {
//        e.preventDefault();
//        return false;
//    }
//});

//
// APP START
//
jQuery('#grdProduct').scrollableTable2();
jQuery('#grdSparepart').scrollableTable2();
jQuery('#grdExtra').scrollableTable2();

var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ui.select2']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;
    $scope.factoryOrderDetails = null;
    $scope.factoryOrderSparepartDetails = null;

    $scope.selectedFactoryOrderDetailID = null;
    $scope.selectedFactoryOrderSparepartDetailID = null;

    $scope.newid = 0;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                context.bookingId,
                context.supplierId,
                context.season,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.factoryOrderDetails = data.data.factoryOrderDetails;
                    $scope.factoryOrderSparepartDetails = data.data.factoryOrderSparepartDetails;
                    $scope.$apply();

                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.factoryOrderDetails = null;
                    $scope.factoryOrderSparepartDetails = null;
                    $scope.$apply();
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryInvoiceID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', context.error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', errMsg);
            }
        },
        delete: function () {
            if (confirm('Are you sure ?')) {
                jsonService.delete(context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        window.location = context.backUrl;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        approve: function () {
            if (confirm('Approve the current invoice ? (data can not be changed after approval)')) {
                jsonService.approve(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryInvoiceID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        reset: function () {
            if (confirm('Reset approval status for the current invoice ?')) {
                jsonService.reset(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryInvoiceID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },


        // product
        addProduct: function () {
            if ($scope.selectedFactoryOrderDetailID == null) {
                return;
            }

            if ($scope.method.productExists($scope.selectedFactoryOrderDetailID)) {
                alert('Selected product is already exists in the current invoice');
            }
            else {
                factoryOrderDetailItem = $scope.method.getFactoryOrderDetail($scope.selectedFactoryOrderDetailID);
                if (factoryOrderDetailItem != null) {
                    $scope.data.factoryInvoiceDetails.push({
                        factoryInvoiceDetailID: $scope.method.getNewID(),
                        factoryOrderDetailID: factoryOrderDetailItem.factoryOrderDetailID,
                        quantity: parseInt(factoryOrderDetailItem.totalQuantity),
                        unitPrice: parseFloat(factoryOrderDetailItem.purchasingPrice),
                        subTotal: parseInt(factoryOrderDetailItem.totalQuantity) * parseFloat(factoryOrderDetailItem.purchasingPrice),
                        remark: '',
                        articleCode: factoryOrderDetailItem.articleCode,
                        description: factoryOrderDetailItem.description,
                        factoryOrderUD: factoryOrderDetailItem.factoryOrderUD,
                        clientUD: factoryOrderDetailItem.clientUD
                    });
                }
            }

            // reset selection
            $scope.selectedFactoryOrderDetailID = null;
            jQuery('#factoryOrderDetailSelector').select2('val', '');
        },
        addAllProduct: function () {
            angular.forEach($scope.factoryOrderDetails, function (value, key) {
                if (!$scope.method.productExists(value.factoryOrderDetailID)) {
                    factoryOrderDetailItem = $scope.method.getFactoryOrderDetail(value.factoryOrderDetailID);
                    if (factoryOrderDetailItem != null) {
                        $scope.data.factoryInvoiceDetails.push({
                            factoryInvoiceDetailID: $scope.method.getNewID(),
                            factoryOrderDetailID: factoryOrderDetailItem.factoryOrderDetailID,
                            quantity: parseInt(factoryOrderDetailItem.totalQuantity),
                            unitPrice: parseFloat(factoryOrderDetailItem.purchasingPrice),
                            subTotal: parseInt(factoryOrderDetailItem.totalQuantity) * parseFloat(factoryOrderDetailItem.purchasingPrice),
                            remark: '',
                            articleCode: factoryOrderDetailItem.articleCode,
                            description: factoryOrderDetailItem.description,
                            factoryOrderUD: factoryOrderDetailItem.factoryOrderUD,
                            clientUD: factoryOrderDetailItem.clientUD
                        });
                    }
                }
            }, null);

            // reset selection
            $scope.selectedFactoryOrderDetailID = null;
            jQuery('#factoryOrderDetailSelector').select2('val', '');
        },
        removeProduct: function (item) {
            $scope.data.factoryInvoiceDetails.splice($scope.data.factoryInvoiceDetails.indexOf(item), 1);
        },


        // sparepart
        addSparepart: function () {
            if ($scope.selectedFactoryOrderSparepartDetailID == null) {
                return;
            }

            if ($scope.method.sparepartExists($scope.selectedFactoryOrderSparepartDetailID)) {
                alert('Selected sparepart is already exists in the current invoice');
            }
            else {
                factoryOrderSparepartDetailItem = $scope.method.getFactoryOrderSparepartDetail($scope.selectedFactoryOrderSparepartDetailID);
                if (factoryOrderSparepartDetailItem != null) {
                    $scope.data.factoryInvoiceSparepartDetails.push({
                        factoryInvoiceSparepartDetailID: $scope.method.getNewID(),
                        factoryOrderSparepartDetailID: factoryOrderSparepartDetailItem.factoryOrderSparepartDetailID,
                        quantity: parseInt(factoryOrderSparepartDetailItem.totalQuantity),
                        unitPrice: parseFloat(factoryOrderSparepartDetailItem.purchasingPrice),
                        subTotal: parseInt(factoryOrderSparepartDetailItem.totalQuantity) * parseFloat(factoryOrderSparepartDetailItem.purchasingPrice),
                        remark: '',
                        articleCode: factoryOrderSparepartDetailItem.articleCode,
                        description: factoryOrderSparepartDetailItem.description,
                        factoryOrderUD: factoryOrderSparepartDetailItem.factoryOrderUD,
                        clientUD: factoryOrderSparepartDetailItem.clientUD
                    });
                }
            }

            // reset selection
            $scope.selectedFactoryOrderSparepartDetailID = null;
            jQuery('#factoryOrderSparepartDetailSelector').select2('val', '');
        },
        addAllSparepart: function () {
            angular.forEach($scope.factoryOrderSparepartDetails, function (value, key) {
                if (!$scope.method.sparepartExists(value.factoryOrderSparepartDetailID)) {
                    factoryOrderSparepartDetailItem = $scope.method.getFactoryOrderSparepartDetail(value.factoryOrderSparepartDetailID);
                    if (factoryOrderSparepartDetailItem != null) {
                        $scope.data.factoryInvoiceSparepartDetails.push({
                            factoryInvoiceSparepartDetailID: $scope.method.getNewID(),
                            factoryOrderSparepartDetailID: factoryOrderSparepartDetailItem.factoryOrderSparepartDetailID,
                            quantity: parseInt(factoryOrderSparepartDetailItem.totalQuantity),
                            unitPrice: parseFloat(factoryOrderSparepartDetailItem.purchasingPrice),
                            subTotal: parseInt(factoryOrderSparepartDetailItem.totalQuantity) * parseFloat(factoryOrderSparepartDetailItem.purchasingPrice),
                            remark: '',
                            articleCode: factoryOrderSparepartDetailItem.articleCode,
                            description: factoryOrderSparepartDetailItem.description,
                            factoryOrderUD: factoryOrderSparepartDetailItem.factoryOrderUD,
                            clientUD: factoryOrderSparepartDetailItem.clientUD
                        });
                    }
                }
            }, null);

            // reset selection
            $scope.selectedFactoryOrderSparepartDetailID = null;
            jQuery('#factoryOrderSparepartDetailSelector').select2('val', '');
        },
        removeSparepart: function (item) {
            $scope.data.factoryInvoiceSparepartDetails.splice($scope.data.factoryInvoiceSparepartDetails.indexOf(item), 1);
        },

        // extra
        addExtra: function () {
            console.log('asdas');
            $scope.data.factoryInvoiceExtras.push({
                factoryInvoiceExtraID: $scope.method.getNewID(),
                quantity: 0,
                unitPrice: 0,
                subTotal: 0,
                remark: '',
                description: ''
            });
        },
        removeExtra: function (item) {
            $scope.data.factoryInvoiceExtras.splice($scope.data.factoryInvoiceExtras.indexOf(item), 1);
        },

        // file event
        changeFile: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.fileLocation = img.fileURL;
                        scope.data.friendlyName = img.filename;
                        scope.data.scanFile_NewFile = img.filename;
                        scope.data.scanFile_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeFile: function () {
            $scope.data.friendlyName = '';
            $scope.data.fileLocation = '';
            $scope.data.scanFile_NewFile = '';
            $scope.data.scanFile_HasChange = true;
        },
        downloadFile: function () {
            if ($scope.data.fileLocation) {
                window.open($scope.data.fileLocation);
            }
        }
    };

    //
    // method
    //
    $scope.method = {
        getNewID: function(){
            $scope.newid--;
            return $scope.newid;
        },
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        },

        // product method
        productExists: function (factoryOrderDetailID) {
            var result = false;
            angular.forEach($scope.data.factoryInvoiceDetails, function (value, key) {
                if (value.factoryOrderDetailID == factoryOrderDetailID) {
                    result = true;
                }
            }, null);
            return result;
        },
        getFactoryOrderDetail: function (id) {
            var result = false;
            angular.forEach($scope.factoryOrderDetails, function (value, key) {
                if (value.factoryOrderDetailID == id) {
                    result = value;
                }
            }, null);
            return result;
        },
        getProductSubTotal: function (item) {
            return parseFloat(item.unitPrice) * parseInt(item.quantity);
        },
        getProductSumSubTotal: function () {
            if ($scope.data == null) {
                return 0;
            }

            var result = 0;
            angular.forEach($scope.data.factoryInvoiceDetails, function (value, key) {
                result += parseFloat(value.unitPrice) * parseInt(value.quantity);
            }, null);
            return result;
        },
        getProductSumQnt: function () {
            if ($scope.data == null) {
                return 0;
            }

            var result = 0;
            angular.forEach($scope.data.factoryInvoiceDetails, function (value, key) {
                result += parseInt(value.quantity);
            }, null);
            return result;
        },

        // sparepart method
        sparepartExists: function (factoryOrderSparepartDetailID) {
            var result = false;
            angular.forEach($scope.data.factoryInvoiceSparepartDetails, function (value, key) {
                if (value.factoryOrderSparepartDetailID == factoryOrderSparepartDetailID) {
                    result = true;
                }
            }, null);
            return result;
        },
        getFactoryOrderSparepartDetail: function (id) {
            var result = false;
            angular.forEach($scope.factoryOrderSparepartDetails, function (value, key) {
                if (value.factoryOrderSparepartDetailID == id) {
                    result = value;
                }
            }, null);
            return result;
        },
        getSparepartSubTotal: function (item) {
            return parseFloat(item.unitPrice) * parseInt(item.quantity);
        },
        getSparepartSumSubTotal: function () {
            if ($scope.data == null) {
                return 0;
            }

            var result = 0;
            angular.forEach($scope.data.factoryOrderSparepartDetails, function (value, key) {
                result += parseFloat(value.unitPrice) * parseInt(value.quantity);
            }, null);
            return result;
        },
        getSparepartSumQnt: function () {
            if ($scope.data == null) {
                return 0;
            }

            var result = 0;
            angular.forEach($scope.data.factoryOrderSparepartDetails, function (value, key) {
                result += parseInt(value.quantity);
            }, null);
            return result;
        },

        // extra method
        getExtraSubTotal: function (item) {
            return parseFloat(item.unitPrice) * parseInt(item.quantity);
        },
        getExtraSumSubTotal: function () {
            if ($scope.data == null) {
                return 0;
            }

            var result = 0;
            angular.forEach($scope.data.factoryInvoiceExtras, function (value, key) {
                result += parseFloat(value.unitPrice) * parseInt(value.quantity);
            }, null);
            return result;
        },
        getExtraSumQnt: function () {
            if ($scope.data == null) {
                return 0;
            }

            var result = 0;
            angular.forEach($scope.data.factoryInvoiceExtras, function (value, key) {
                result += parseInt(value.quantity);
            }, null);
            return result;
        },

        // invoice method
        getSubTotal: function () {
            return $scope.method.getExtraSumSubTotal() + $scope.method.getSparepartSumSubTotal() + $scope.method.getProductSumSubTotal();
        },
        getTotal: function () {
            if ($scope.data == null) {
                return 0;
            }

            if ($scope.data.deductedAmount == null) {
                return $scope.method.getSubTotal();
            }
            else {
                return $scope.method.getSubTotal() - parseFloat($scope.data.deductedAmount);
            }            
        }
    };

    //
    // init
    //    
    $scope.event.init();
}]);