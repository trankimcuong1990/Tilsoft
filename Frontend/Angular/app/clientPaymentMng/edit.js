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
var tilsoftApp = angular.module('tilsoftApp', ['ngSanitize', 'ui.select2', 'furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;
    $scope.newid = 0;

    $scope.orders = null;
    $scope.invoices = null;
    $scope.orderForDeductions = null;

    $scope.currentDetail = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                context.clientId,
                context.methodId,
                context.currency,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.invoices = data.data.invoices;
                    $scope.orders = data.data.orders;
                    $scope.orderForDeductions = data.data.orderForDeductions;
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
                    $scope.invoices = null;
                    $scope.orders = null;
                    $scope.orderForDeductions = null;
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
                            $scope.method.refresh(data.data.clientPaymentID);
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
        approve: function () {
            if (confirm('Approve the current payment ? (data can not be changed after approval)')) {
                jsonService.approve(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.clientPaymentID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
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

        // pi
        addPI: function () {
            $scope.event.editPI($scope.method.getBlankDetail());
        },
        editPI: function (item) {
            $scope.currentDetail = JSON.parse(JSON.stringify(item));
            jQuery("#piForm").modal();
        },
        updatePI: function () {
            if ($scope.piForm.$valid) {
                if (!$scope.currentDetail.isDone) {
                    var order = $scope.method.getOrder($scope.currentDetail.saleOrderID);
                    $scope.currentDetail.proformaInvoiceNo = order.proformaInvoiceNo;
                }                

                var index = $scope.method.getDetailIndex($scope.currentDetail.clientPaymentDetailID)
                if (index < 0) {
                    $scope.data.clientPaymentDetails.push($scope.currentDetail);
                }
                else {
                    $scope.data.clientPaymentDetails[index] = $scope.currentDetail;
                }
                $('#piForm').modal('hide');
            }
        },

        // invoice
        addInvoice: function () {
            $scope.event.editInvoice($scope.method.getBlankDetail());
        },
        editInvoice: function (item) {
            $scope.currentDetail = JSON.parse(JSON.stringify(item));
            jQuery("#invoiceForm").modal();
        },
        updateInvoice: function () {
            if ($scope.invoiceForm.$valid) {
                if (!$scope.currentDetail.isDone) {
                    var invoice = $scope.method.getInvoice($scope.currentDetail.eCommercialInvoiceID);
                    $scope.currentDetail.invoiceNo = invoice.invoiceNo;
                }                

                var index = $scope.method.getDetailIndex($scope.currentDetail.clientPaymentDetailID)
                if (index < 0) {
                    $scope.data.clientPaymentDetails.push($scope.currentDetail);
                }
                else {
                    $scope.data.clientPaymentDetails[index] = $scope.currentDetail;
                }
                $('#invoiceForm').modal('hide');
            }
        },
        onDeductedAmountChange: function () {
            // set total deducted amount
            $scope.currentDetail.deductedAmount = 0;
            for (var index = 0; index < $scope.currentDetail.clientPaymentDeductions.length; index++) {
                $scope.currentDetail.deductedAmount += parseFloat($scope.currentDetail.clientPaymentDeductions[index].amount);
            }

            // set remain amount
            $scope.currentDetail.amount = parseFloat($scope.currentDetail.toBePaidAmount - $scope.currentDetail.deductedAmount);
        },

        // other charge
        addOther: function () {
            $scope.event.editOther($scope.method.getBlankDetail());
        },
        editOther: function (item) {
            $scope.currentDetail = JSON.parse(JSON.stringify(item));
            jQuery("#otherForm").modal();
        },
        updateOther: function () {
            if ($scope.otherForm.$valid) {
                var index = $scope.method.getDetailIndex($scope.currentDetail.clientPaymentDetailID)
                if (index < 0) {
                    $scope.data.clientPaymentDetails.push($scope.currentDetail);
                }
                else {
                    $scope.data.clientPaymentDetails[index] = $scope.currentDetail;
                }
                $('#otherForm').modal('hide');
            }
        },

        // detail
        editDetail: function (item) {
            if (item.eCommercialInvoiceID != null && item.eCommercialInvoiceID != '') {
                $scope.event.editInvoice(item);
            }
            else {
                $scope.event.editPI(item);
            }
        },
        deleteDetail: function (item) {
            $scope.data.clientPaymentDetails.splice($scope.data.clientPaymentDetails.indexOf(item), 1);
            $scope.$apply();
        },

        orderChange: function () {
            var order = $scope.method.getOrder($scope.currentDetail.saleOrderID);
            if (order != null) {
                $scope.currentDetail.toBePaidAmount = order.dpAmount;
                $scope.currentDetail.saleOrderDate = order.invoiceDate;
                $scope.currentDetail.paidAmount = order.paidAmount;
            }
        },
        invoiceChange: function () {
            var invoice = $scope.method.getInvoice($scope.currentDetail.eCommercialInvoiceID);
            if (invoice != null) {
                $scope.currentDetail.toBePaidAmount = invoice.totalAmount;
                $scope.currentDetail.invoiceDate = invoice.invoiceDate;
                $scope.currentDetail.paidAmount = invoice.paidAmount;

                // get deduction
                if ($scope.orderForDeductions != null && $scope.orderForDeductions.length > 0) {
                    for (var index = 0; index < $scope.orderForDeductions.length; index++) {
                        if ($scope.orderForDeductions[index].eCommercialInvoiceID == invoice.eCommercialInvoiceID) {
                            $scope.currentDetail.clientPaymentDeductions.push({
                                clientPaymentDeductionID: $scope.method.getNewID(),
                                saleOrderID: $scope.orderForDeductions[index].saleOrderID,
                                amount: 0,
                                proformaInvoiceNo: $scope.orderForDeductions[index].proformaInvoiceNo,
                                invoiceDate: $scope.orderForDeductions[index].invoiceDate,
                                toBePaidAmount: $scope.orderForDeductions[index].toBePaid,
                                paidAmount: $scope.orderForDeductions[index].paidAmount,
                                deductedAmount: $scope.orderForDeductions[index].deductedAmount
                            });
                        }
                    }
                }
            }
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id + '?clientId=0&c=USD&m=0';
        },
        getNewID: function () {
            $scope.newid--;
            return $scope.newid;
        },
        getBlankDetail: function () {
            return {
                clientPaymentDetailID: $scope.method.getNewID(),
                saleOrderID: '',
                eCommercialInvoiceID: '',
                paidDate: '',
                amount: 0,
                updatedBy: '',
                updatedDate: '',
                remark: '',
                proformaInvoiceNo: '',
                invoiceNo: '',
                toBePaidAmount: 0,
                saleOrderDate: '',
                paidAmount: 0,
                deductedAmount: 0,
                clientPaymentDeductions: []
            }
        },
        getDetailIndex: function (id) {
            var isExist = false;
            var index = 0;
            for (var index = 0; index < $scope.data.clientPaymentDetails.length; index++) {
                if ($scope.data.clientPaymentDetails[index].clientPaymentDetailID == id) {
                    isExist = true;
                    break;
                }
            }

            if (!isExist) {
                return -1;
            }
            else {
                return index;
            }
        },
        getOrder: function (id) {
            var result = null;
            for (var index = 0; index < $scope.orders.length; index++) {
                if ($scope.orders[index].saleOrderID == id) {
                    result = $scope.orders[index];
                    break;
                }
            }
            return result;
        },
        getInvoice: function (id) {
            var result = null;
            for (var index = 0; index < $scope.invoices.length; index++) {
                if ($scope.invoices[index].eCommercialInvoiceID == id) {
                    result = $scope.invoices[index];
                    break;
                }
            }
            return result;
        },
        getTotal: function () {
            var result = 0;
            if ($scope.data != null) {
                angular.forEach($scope.data.clientPaymentDetails, function (value, key) {
                    result += parseFloat(value.amount);
                }, null);
            }            
            return result;
        }
    };

    //
    // init
    //    
    $scope.event.init();
}]);