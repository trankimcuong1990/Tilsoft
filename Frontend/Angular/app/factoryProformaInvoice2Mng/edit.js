jQuery('#grdItem').scrollableTable(
    function (currentPage) {
    },
    function (sortedBy, sortedDirection) {
    }
);
jQuery('#grdSearchResult').scrollableTable(
    function (currentPage) {
    },
    function (sortedBy, sortedDirection) {
    }
);

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ngSanitize']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;
    $scope.items = null;
    $scope.newid = 0;
    $scope.deliveryTerms = null;
    $scope.paymentTerms = null;
    $scope.officeId = context.officeId;

    $scope.filter = {
        clientId: context.clientId,
        factoryId: context.factoryId,
        id: context.id,
        season: context.season,
        description: '',
        factoryOrderUd: '',
        itemType: ''
    };
    $scope.selectAll = false;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                context.clientId,
                context.factoryId,
                context.season,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.deliveryTerms = data.data.deliveryTerms;
                    $scope.paymentTerms = data.data.paymentTerms;
                    $scope.$apply();

                    context.clientId = $scope.data.clientID;
                    context.factoryId = $scope.data.factoryID;
                    context.season = $scope.data.season;
                    $scope.filter = {
                        clientId: context.clientId,
                        factoryId: context.factoryId,
                        id: context.id,
                        season: context.season,
                        description: '',
                        factoryOrderUd: '',
                        itemType: ''
                    };

                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.deliveryTerms = null;
                    $scope.paymentTerms = null;
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
                            $scope.method.refresh(data.data.factoryProformaInvoiceID);
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
        furnindoConfirm: function () {
            if ($scope.editForm.$valid) {
                if (confirm('Confirm the current factory proforma invoice?')) {
                    jsonService.furnindoConfirm(
                        context.id,
                        $scope.data,
                        function (data) {
                            jsHelper.processMessageEx(data.message);
                            if (data.message.type == 0) {
                                $scope.method.refresh(data.data.factoryProformaInvoiceID);
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', context.error);
                        }
                    );
                }
            }
            else {
                jsHelper.showMessage('warning', errMsg);
            }
        },
        furnindoUnConfirm: function () {
            if ($scope.editForm.$valid) {
                if (confirm('Unconfirm the current factory proforma invoice?')) {
                    jsonService.furnindoUnConfirm(
                        context.id,
                        $scope.data,
                        function (data) {
                            jsHelper.processMessageEx(data.message);
                            if (data.message.type == 0) {
                                $scope.method.refresh(data.data.factoryProformaInvoiceID);
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', context.error);
                        }
                    );
                }
            }
            else {
                jsHelper.showMessage('warning', errMsg);
            }
        },
        factoryConfirm: function () {
            if ($scope.editForm.$valid) {
                if (confirm('Confirm the current factory proforma invoice?')) {
                    jsonService.factoryConfirm(
                        context.id,
                        $scope.data,
                        function (data) {
                            jsHelper.processMessageEx(data.message);
                            if (data.message.type == 0) {
                                $scope.method.refresh(data.data.factoryProformaInvoiceID);
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', context.error);
                        }
                    );
                }
            }
            else {
                jsHelper.showMessage('warning', errMsg);
            }
        },
        factoryUnConfirm: function () {
            if ($scope.editForm.$valid) {
                if (confirm('Unconfirm the current factory proforma invoice?')) {
                    jsonService.factoryUnConfirm(
                        context.id,
                        $scope.data,
                        function (data) {
                            jsHelper.processMessageEx(data.message);
                            if (data.message.type == 0) {
                                $scope.method.refresh(data.data.factoryProformaInvoiceID);
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', context.error);
                        }
                    );
                }
            }
            else {
                jsHelper.showMessage('warning', errMsg);
            }
        },

        addFormOpen: function (item) {
            jQuery('#addForm').modal();
            $scope.selectAll = false;

            jsonService.quickSearchItem(
                $scope.filter.clientId,
                $scope.filter.factoryId,
                $scope.filter.id,
                $scope.filter.season,
                $scope.filter.description,
                $scope.filter.factoryOrderUd,
                $scope.filter.itemType,
                function (data) {
                    $scope.items = data.data;
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.items = null;
                    $scope.$apply();
                }
            );
        },
        addFormFilter: function () {
            jsonService.quickSearchItem(
                $scope.filter.clientId,
                $scope.filter.factoryId,
                $scope.filter.id,
                $scope.filter.season,
                $scope.filter.description,
                $scope.filter.factoryOrderUd,
                $scope.filter.itemType,
                function (data) {
                    $scope.items = data.data;
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.items = null;
                    $scope.$apply();
                }
            );
        },
        addFormAdd: function () {
            angular.forEach($scope.items, function (value, key) {
                if (value.isSelected && !$scope.method.isItemExist(value)) {
                    $scope.data.factoryProformaInvoiceDetails.push({
                        factoryProformaInvoiceDetailID: $scope.method.getNewID(),
                        factoryOrderDetailID: value.factoryOrderDetailID < 0 ? null : value.factoryOrderDetailID,
                        factoryOrderSparepartDetailID: value.factoryOrderSparepartDetailID < 0 ? null : value.factoryOrderSparepartDetailID,
                        unitPrice1: value.purchasingPrice,
                        unitPrice2: null,
                        unitPrice3: null,
                        unitPrice4: null,
                        updatedBy: context.userId,
                        updatedDate: $scope.method.getCurrentDateTime(),
                        remark: '',
                        factoryOrderUD: value.factoryOrderUD,
                        lds: value.lds,
                        articleCode: value.articleCode,
                        description: value.description,
                        purchasingPrice: value.purchasingPrice,
                        updatorName: context.userName,
                        itemType: value.itemType,
                        orderQnt: value.orderQnt
                    });
                }
            }, null);

            jQuery('#addForm').modal('hide');
        },
        removeItem: function (item) {
            $scope.data.factoryProformaInvoiceDetails.splice($scope.data.factoryProformaInvoiceDetails.indexOf(item), 1);
        },
        //
        // file functions
        //
        changeFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.friendlyName = fileUploader2.selectedFileName;
                    $scope.data.fileLocation = fileUploader2.selectedFileUrl;
                    $scope.data.attachedFile_NewFile = fileUploader2.selectedFileName;
                    $scope.data.attachedFile_HasChanged = true;
                });
            };
            fileUploader2.open();
        },
        removeFile: function () {
            $scope.data.friendlyName = '';
            $scope.data.fileLocation = '';
            $scope.data.attachedFile_NewFile = '';
            $scope.data.attachedFile_HasChanged = true;
        },
        downloadFile: function () {
            if ($scope.data.fileLocation) {
                window.open($scope.data.fileLocation);
            }
        },
        print: function () {
            jsonService.getReport(
                context.id,
                function (data) {
                    if (data.message.type == 0) {
                        window.location = context.backendReportUrl + data.data;
                    }
                    else {
                        jsHelper.processMessageEx(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
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
        isItemExist: function (item) {
            var result = false;
            angular.forEach($scope.data.factoryProformaInvoiceDetails, function (value, key) {
                if (value.factoryOrderDetailID != null && value.factoryOrderDetailID == this.factoryOrderDetailID) {
                    result = true;
                }
                if (value.factoryOrderSparepartDetailID != null && value.factoryOrderSparepartDetailID == this.factoryOrderSparepartDetailID) {
                    result = true;
                }
            }, item);

            return result;
        },
        getCurrentDateTime: function () {
            var currentdate = new Date();
            return currentdate.getDate() + "/"
                + (currentdate.getMonth() + 1) + "/"
                + currentdate.getFullYear() + " "
                + currentdate.getHours() + ":"
                + currentdate.getMinutes();
        },
        formatRemark: function (remark) {
            if (remark == null) {
                return '';
            }
            return remark.replace(/(?:\r\n|\r|\n)/g, '<br />');
        },
        toAmount: function (item) {
            if (item == null || item.orderQnt == null) {
                return 0;
            }
            if (item.unitPrice4 != null) {
                return parseFloat(item.unitPrice4) * parseInt(item.orderQnt);
            }
            if (item.unitPrice3 != null) {
                return parseFloat(item.unitPrice3) * parseInt(item.orderQnt);
            }
            if (item.unitPrice2 != null) {
                return parseFloat(item.unitPrice2) * parseInt(item.orderQnt);
            }
            if (item.unitPrice1 != null) {
                return parseFloat(item.unitPrice1) * parseInt(item.orderQnt);
            }
            return 0;
        },
        toSubTotalAmount: function () {
            var result = 0;
            if ($scope.data != null && $scope.data.factoryProformaInvoiceDetails != null) {
                angular.forEach($scope.data.factoryProformaInvoiceDetails, function (value, key) {
                    if (value.orderQnt != null) {
                        if (value.unitPrice4 != null) {
                            result = result + parseFloat(value.unitPrice4) * parseInt(value.orderQnt);
                        }
                        else if (value.unitPrice3 != null) {
                            result = result + parseFloat(value.unitPrice3) * parseInt(value.orderQnt);
                        }
                        else if (value.unitPrice2 != null) {
                            result = result + parseFloat(value.unitPrice2) * parseInt(value.orderQnt);
                        }
                        else if (value.unitPrice1 != null) {
                            result = result + parseFloat(value.unitPrice1) * parseInt(value.orderQnt);
                        }
                    }
                }, null);
            }            

            return result;
        },
        toSubTotalQnt: function () {
            var result = 0;
            if ($scope.data != null && $scope.data.factoryProformaInvoiceDetails != null) {
                angular.forEach($scope.data.factoryProformaInvoiceDetails, function (value, key) {
                    if (value.orderQnt != null) {
                        result = result + parseInt(value.orderQnt);
                    }
                }, null);
            }

            return result;
        }
    };

    // on data binding change
    $scope.$watch('selectAll', function () {
        angular.forEach($scope.items, function (value, key) {
            value.isSelected = $scope.selectAll;
        }, null);
    });

    //
    // init
    //    
    $scope.event.init();
}]);