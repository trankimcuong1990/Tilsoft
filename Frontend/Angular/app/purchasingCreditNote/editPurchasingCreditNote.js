//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ng-currency']);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.editData = null;
    $scope.newID = -1;
    //
    // total function
    //
    $scope.calTotalAmount = function () {
        if ($scope.editData == null) return;
        var total = 0;
        angular.forEach($scope.editData.purchasingCreditNoteDetails, function (item) {
            total = total + item.unitPrice * item.quantity;
        })
        return total;
    }

    $scope.calTotalQuantity = function () {
        if ($scope.editData == null) return;
        var total = 0;
        angular.forEach($scope.editData.purchasingCreditNoteDetails, function (item) {
            total = total + item.quantity;
        })
        return total;
    }

    $scope.calTotalAmountSparepart = function () {
        if ($scope.editData == null) return;
        var total = 0;
        angular.forEach($scope.editData.purchasingCreditNoteSparepartDetails, function (item) {
            total = total + item.unitPrice * item.quantity;
        })
        return total;
    }

    $scope.calTotalQuantitySparepart = function () {
        if ($scope.editData == null) return;
        var total = 0;
        angular.forEach($scope.editData.purchasingCreditNoteSparepartDetails, function (item) {
            total = total + item.quantity;
        })
        return total;
    }
    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.getEditData(
                context.id, context.creditNoteType, context.purchasingInvoiceID, context.supplierID,
                function (data) {
                    $scope.editData = data.data;
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                    if (data.message.type == 2) //error
                    {
                        jsHelper.processMessage(data.message);
                    }
                    setTimeout(function () { runAllForms() }, 200);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.editData = null;
                    $scope.$apply();
                }
            );
        },
        update: function ($event) {
            $event.preventDefault();

            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.editData,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.purchasingCreditNoteID);
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
        delete: function ($event) {
            $event.preventDefault();

            if (confirm('Are you sure you want to delete ?')) {
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

        setStatus: function ($event,id, status) {
            $event.preventDefault();

            $event.preventDefault();
            askmessage = (status == true ? 'Are you sure you want to confirmed ?' : 'Are you sure you want to un-confirmed ?')
            if (confirm(askmessage)) {
                jsonService.setStatus(id, status,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {
                        $scope.method.refresh(id);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            )
            };
        },

        removeProduct: function ($event,index) {
            $event.preventDefault();
            $scope.editData.purchasingCreditNoteDetails.splice(index, 1);
        },
        removeSparepart: function ($event, index) {
            $event.preventDefault();
            $scope.editData.purchasingCreditNoteSparepartDetails.splice(index, 1);
        },
        addCreditNoteExtend: function ($event) {
            $event.preventDefault();
            $scope.editData.purchasingCreditNoteExtendDetails.push({
                purchasingCreditNoteExtendDetailID: $scope.method.getNewID(),
            });
            setTimeout(function () { runAllForms() }, 200);
        },
        removeCreditNoteExtend: function ($event, index) {
            $event.preventDefault();
            $scope.editData.purchasingCreditNoteExtendDetails.splice(index, 1);
        },
        print : function($event){
            jsonService.getPrintoutCreditNote($scope.editData.purchasingCreditNoteID,
                function (data) {
                    if (data.message.type != 2) {
                        window.location = context.backendReportUrl + data.data + '.xlsm';
                    }
                    else {
                        jsHelper.processMessage(data.message);
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
        refresh: function (id) {
            window.location = context.refreshUrl + id
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        }
    };

    $scope.addPurchasingInvoiceForm = {
        invoiceData: null,
        event: {
            load: function ($event){
                $event.preventDefault();
                jsonService.getPurchasingInvoice(
                    $scope.editData.purchasingInvoiceID,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.addPurchasingInvoiceForm.invoiceData = data.data;
                            $scope.$apply();
                            jsHelper.showPopup('addPurchasingInvoiceForm', function () { });
                        }
                        else {
                            jsHelper.processMessage(data.message);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            },

            addGoods: function ($event) {
                $event.preventDefault();
                //add product
                angular.forEach($scope.addPurchasingInvoiceForm.invoiceData.purchasingInvoiceDetails, function (item) {
                    if (item.isSelected)
                    {
                        var arr = $scope.editData.purchasingCreditNoteDetails.filter(function (o) { return o.purchasingInvoiceDetailID == item.purchasingInvoiceDetailID })
                        if (arr == null || arr.length == 0)
                        {
                            $scope.editData.purchasingCreditNoteDetails.push({
                                purchasingCreditNoteDetailID: $scope.method.getNewID(),
                                purchasingInvoiceDetailID: item.purchasingInvoiceDetailID,
                                clientUD: item.clientUD,
                                proformaInvoiceNo: item.proformaInvoiceNo,
                                loadingPlanUD: item.loadingPlanUD,
                                containerNo: item.containerNo,
                                sealNo: item.sealNo,
                                containerTypeNM: item.containerTypeNM,
                                articleCode: item.articleCode,
                                description: item.description,
                                factoryPrice : item.factoryPrice
                            });
                        }
                    }
                });

                //add sparepart
                angular.forEach($scope.addPurchasingInvoiceForm.invoiceData.purchasingInvoiceSparepartDetails, function (item) {
                    if (item.isSelected) {
                        var arr = $scope.editData.purchasingCreditNoteSparepartDetails.filter(function (o) { return o.purchasingInvoiceSparepartDetailID == item.purchasingInvoiceSparepartDetailID })
                        if (arr == null || arr.length == 0) {
                            $scope.editData.purchasingCreditNoteSparepartDetails.push({
                                purchasingCreditNoteSparepartDetailID: $scope.method.getNewID(),
                                purchasingInvoiceSparepartDetailID: item.purchasingInvoiceSparepartDetailID,
                                clientUD: item.clientUD,
                                proformaInvoiceNo: item.proformaInvoiceNo,
                                loadingPlanUD: item.loadingPlanUD,
                                containerNo: item.containerNo,
                                sealNo: item.sealNo,
                                containerTypeNM: item.containerTypeNM,
                                articleCode: item.articleCode,
                                description: item.description,
                                factoryPrice: item.factoryPrice
                            });
                        }
                    }
                });

                //
                //$scope.$apply();
                jsHelper.hidePopup('addPurchasingInvoiceForm', function () { });
            },

            cancel: function ($event) {
                $event.preventDefault();
                jsHelper.hidePopup('addPurchasingInvoiceForm', function () { });
            }


        }

    }

    //
    // init
    //
    $scope.event.load();
}]);