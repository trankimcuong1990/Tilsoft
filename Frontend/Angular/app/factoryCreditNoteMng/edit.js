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
                            $scope.method.refresh(data.data.factoryCreditNoteID);
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
                jsonService.approve(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryCreditNoteID);
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
                            $scope.method.refresh(data.data.factoryCreditNoteID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },


        // product
        addInvoice: function () {
            if ($scope.invoiceSearchResult.invoiceID == null) {
                return;
            }

            if ($scope.method.invoiceExists($scope.invoiceSearchResult.invoiceID)) {
                alert('Selected invoice is already exists in the current credit note');
            }
            else {
                $scope.data.factoryCreditNoteDetails.push({
                    factoryCreditNoteDetailID: $scope.method.getNewID(),
                    factoryInvoiceID: $scope.invoiceSearchResult.invoiceID,
                    creditAmount: 0,
                    invoiceNo: $scope.invoiceSearchResult.invoice.invoiceNo,
                    invoiceDate: $scope.invoiceSearchResult.invoice.invoiceDate,
                    invoiceAmount: parseFloat($scope.invoiceSearchResult.invoice.totalAmount),
                    remark: ''
                });
            }

            // reset selection
            $scope.invoiceSearchResult.invoiceID = null;
            $scope.invoiceSearchResult.invoice = null;
            jQuery('#invoice-autocomplete').val('');
        },
        removeInvoice: function (item) {
            $scope.data.factoryCreditNoteDetails.splice($scope.data.factoryCreditNoteDetails.indexOf(item), 1);
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
            angular.forEach($scope.data.factoryCreditNoteDetails, function (value, key) {
                if (value.factoryInvoiceID == id) {
                    result = true;
                }
            }, null);
            return result;
        },
        getTotal: function () {
            if ($scope.data == null) {
                return 0;
            }

            var result = 0;
            angular.forEach($scope.data.factoryCreditNoteDetails, function (value, key) {
                result += parseFloat(value.creditAmount);
            }, null);

            return result;
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
jQuery(document).ready(function () {
    jQuery('#invoice-autocomplete').devbridgeAutocomplete({
        serviceUrl: jsonService.serviceUrl + 'quicksearchinvoice',
        dataType: 'json',
        minChars: 3,
        ajaxSettings: {
            cache: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jsonService.token
            },
            type: "GET"
        },
        params: { supplierid: function () { return scope.data.supplierID; }, season: function () { return scope.data.season; } },
        onSelect: function (suggestion) {
            scope.invoiceSearchResult.invoiceID = suggestion.data.factoryInvoiceID;
            scope.invoiceSearchResult.invoice = suggestion.data;
            scope.$apply();
        },
        transformResult: function (response) {
            return {
                suggestions: jQuery.map(response, function (item) {
                    return { value: item.invoiceNo, data: item };
                })
            };
        },
        onInvalidateSelection: function () {
            scope.invoiceSearchResult.invoiceID = null;
            scope.invoiceSearchResult.invoice = null;
            scope.$apply();
            jQuery('#invoice-autocomplete').val('');
        },
        showNoSuggestionNotice: true,
        noCache: true,
        onSearchStart: function (query) {
            jsHelper.loadingSwitch(true);
        },
        onSearchComplete: function (query, suggestions) {
            jsHelper.loadingSwitch(false);
        }
    });
});