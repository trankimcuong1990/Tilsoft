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
jQuery('#grdInvoice').scrollableTable2();

var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;
    $scope.newid = 0;

    $scope.invoiceSearchResult = {
        invoiceID: null,
        invoice: null
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                context.supplierId,
                context.season,
                function (data) {
                    $scope.data = data.data.data;
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
                            $scope.method.refresh(data.data.factoryPayment2ID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', context.error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Please check your input and try again');
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
                jQuery('#confirmForm').modal('hide');
                jsonService.approve(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryPayment2ID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                $scope.data.confirmedAmount = null;
                $scope.data.confirmedRemark = null;
            }
        },
        cancelApprove: function () {
            $scope.data.confirmedAmount = null;
            $scope.data.confirmedRemark = null;
            jQuery('#confirmForm').modal('hide');
        },
        reset: function () {
            if (confirm('Reset approval status for the current invoice ?')) {
                jsonService.reset(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryPayment2ID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },


        // invoice
        addInvoice: function () {
            if ($scope.invoiceSearchResult.invoiceID == null) {
                return;
            }

            if ($scope.method.invoiceExists($scope.invoiceSearchResult.invoiceID)) {
                alert('Selected invoice is already exists in the current credit note');
            }
            else {
                $scope.data.factoryPaymentDetails.push({
                    factoryPayment2DetailID: $scope.method.getNewID(),
                    factoryInvoiceID: $scope.invoiceSearchResult.invoiceID,
                    dpDeductedAmount: 0,
                    paidAmount: 0,
                    invoiceNo: $scope.invoiceSearchResult.invoice.invoiceNo,
                    invoiceDate: $scope.invoiceSearchResult.invoice.invoiceDate,
                    totalAmount: parseFloat($scope.invoiceSearchResult.invoice.totalAMount),
                    totalPaid: parseFloat($scope.invoiceSearchResult.invoice.totalPaid),
                    remark: ''
                });
            }

            // reset selection
            $scope.invoiceSearchResult.invoiceID = null;
            $scope.invoiceSearchResult.invoice = null;
            jQuery('#invoice-autocomplete').val('');
        },
        removeInvoice: function (item) {
            $scope.data.factoryPaymentDetails.splice($scope.data.factoryPaymentDetails.indexOf(item), 1);
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
        invoiceExists: function (id) {
            var result = false;
            angular.forEach($scope.data.factoryPaymentDetails, function (value, key) {
                if (value.purchasingInvoiceID == id) {
                    result = true;
                }
            }, null);
            return result;
        },
        getDPDeductedTotal: function () {
            if ($scope.data == null) {
                return 0;
            }

            var result = 0;
            angular.forEach($scope.data.factoryPaymentDetails, function (value, key) {
                if (value.dpDeductedAmount != null) {
                    result += parseFloat(value.dpDeductedAmount);
                }
            }, null);

            return result;
        },
        getPaidTotal: function () {
            if ($scope.data == null) {
                return 0;
            }

            var result = 0;
            angular.forEach($scope.data.factoryPaymentDetails, function (value, key) {
                if (value.paidAmount != null) {
                    result += parseFloat(value.paidAmount);
                }
            }, null);

            return result;
        },
        getInvoicePaidTotal: function () {
            if ($scope.data == null) {
                return 0;
            }

            var result = 0;
            angular.forEach($scope.data.factoryPaymentDetails, function (value, key) {
                result += parseFloat(value.totalPaid);
            }, null);

            return result;
        },
        getInvoiceTotal: function () {
            if ($scope.data == null) {
                return 0;
            }

            var result = 0;
            angular.forEach($scope.data.factoryPaymentDetails, function (value, key) {
                if (value.totalAmount != null) {
                    result += parseFloat(value.totalAmount);
                }
            }, null);

            return result;
        },
        getRemainTotal: function () {
            if ($scope.data == null) {
                return 0;
            }

            var result = 0;
            angular.forEach($scope.data.factoryPaymentDetails, function (value, key) {
                if (value.totalAmount != null && value.totalPaid != null) {
                    result += parseFloat(value.totalAmount) - parseFloat(value.totalPaid);
                }
            }, null);

            return result;
        },
        getTotal: function () {
            if ($scope.data == null) {
                return 0;
            }

            var result = 0;
            angular.forEach($scope.data.factoryPaymentDetails, function (value, key) {
                if (value.paidAmount != null) {
                    result += parseFloat(value.paidAmount);
                }
            }, null);

            if ($scope.data.dpAmount != null) {
                return result + parseFloat($scope.data.dpAmount);
            }
            else {
                return result;
            }
        }
    };

    //
    // init
    //    
    $scope.event.init();
}]);

//
// JQUERY EXTENSION DECLARATION
//
$(document).ready(function () {
    $('#invoice-autocomplete').autocomplete({
        source: function (request, response) {
            jsHelper.loadingSwitch(true);
            jQuery.ajax({
                url: jsonService.serviceUrl + 'quicksearchinvoice',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + jsonService.token
                },
                type: 'GET',
                dataType: 'json',
                data: {
                    supplierid: scope.data.supplierID,
                    query: request.term
                },
                success: function (data) {
                    jsHelper.loadingSwitch(false);
                    response(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    jsHelper.loadingSwitch(false);
                    console.log(jqXHR);
                    console.log(textStatus);
                    console.log(errorThrown);
                }
            });
        },
        minLength: 3,
        select: function (event, ui) {
            scope.invoiceSearchResult.invoiceID = ui.item.purchasingInvoiceID;
            scope.invoiceSearchResult.invoice = ui.item;
            scope.$apply();
        },
    })
    .data("ui-autocomplete")._renderItem = function (ul, item) {
	    return jQuery("<li>")
		    .data('item.autocomplete', item)
		    .append('<a href="javascript:void(0)">' + item.invoiceNo + '</a>')
		    .appendTo(ul);
    };
});