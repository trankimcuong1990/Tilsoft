'use strict';

tilsoftApp.controller('_GeneralCtrl', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
    //
    //init controller
    //

    $scope.oldValue = {
        totalPayment: 0,
        remain: 0,
        amount: 0,
        totalPayDepositAmount: 0,
        remainDepositAmount: 0
    };

    $scope.initController = function () {
        //
        // init service
        //
        dataService.serviceUrl = context.serviceUrl;
        dataService.supportServiceUrl = context.supportServiceUrl;
        dataService.token = context.token;

    };

    $scope.generalCtrlEvent = {

        //File Zone
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

        //For Clent Zone
        addPaymentNoteSupplier: function () {
            $scope.data.paymentNoteSupplierResults.push({
                paymentNoteClientID: $scope.generalCtrlMethod.getnewID(),
                factoryRawMaterialID: null,
                amount: null,
                remark: null,
                factoryRawMaterialUD: null,
                factoryRawMaterialOfficialNM: null,
                amountDeposit: null,
                paymentByDeposit: null,
                totalDeposit: null
            });
        },
        removePaymentNoteSupplier: function (item) {
            var index = $scope.data.paymentNoteSupplierResults.indexOf(item);
            $scope.data.paymentNoteSupplierResults.splice(index, 1);
        },

        //For Other Zone
        addPaymentNoteOther: function () {
            $scope.data.paymentNoteOtherResults.push({
                paymentNoteOtherID: $scope.generalCtrlMethod.getnewID(),
                NoteItemID: null,
                factoryRawMaterialID: null,
                amount: null,
                factoryRawMaterialUD: null,
                factoryRawMaterialOfficialNM: null,
                remark: null
            });
        },
        removePaymentNoteOther: function (item) {
            var index = $scope.data.paymentNoteOtherResults.indexOf(item);
            $scope.data.paymentNoteOtherResults.splice(index, 1);
        },

        //For Purchase Invoice
        removePaymentNotePurchase: function (item) {
            var index = $scope.data.paymentNoteInvoiceResults.indexOf(item);
            $scope.data.paymentNoteInvoiceResults.splice(index, 1);
        },

        pushFactoryRawMaterialID: function () {
            context.factoryRawMaterialID = $scope.data.factoryRawMaterialID;
        },

        setstatus: function () {
            if (context.id !== 0) {
                if ($scope.data.paymentNoteTypeID == 1 && $scope.data.statusID == 2 && $scope.DepositAfterPayment < 0) {
                    $scope.data.statusID = 1;
                    jsHelper.showMessage('warning', 'Deposit after payment can not lower than 0, please check Deposit');
                }
                else {
                    const statusNM = $scope.status.filter(o => o.statusID === $scope.data.statusID)[0].statusNM;
                    bootbox.confirm("Are you sure you want set Payment Note to " + statusNM, function (result) {
                        if (result) {
                            dataService.setStatus(
                                $scope.data.paymentNoteID,
                                $scope.data.statusID,
                                $scope.data.paymentNoteTypeID,
                                function (data) {
                                    jsHelper.processMessage(data.message);
                                    if (data.message.type === 0) {
                                        //window.location = context.refreshUrl + $scope.data.paymentNoteID + '#/';
                                        $scope.event.load();
                                    }
                                },
                                function (error) {
                                    //
                                });
                        }
                    });
                }
            }
        },

        depositDetail: function (item) {
            item.isShow = !item.isShow;

            if (item.isShow) {
                dataService.getDepositPODronInvoice(
                    item.purchaseInvoiceID,
                    function (data) {
                        var responData = data.data;
                        if (item.paymentNotePODepositResults.length <= 0) {
                            angular.forEach(responData, function (itemx) {
                                item.paymentNotePODepositResults.push({
                                    paymentNotePODepositID: dataService.getIncrementId(),
                                    purchaseOrderID: itemx.purchaseOrderID,
                                    depositAmount: null,
                                    remark: '',
                                    purchaseOrderUD: itemx.purchaseOrderUD,
                                    purchaseOrderDate: itemx.purchaseOrderDate,
                                    paymentByPO: itemx.paymentByPO,
                                    totalPurchaseOrderAmount: itemx.totalPurchaseOrderAmount,
                                    totalDepositAmount: itemx.totalDepositAmount,
                                    remainDepositAmount: itemx.remainDepositAmount,
                                    remainPurchaseOrderAmount: itemx.remainPurchaseOrderAmount,
                                    totalPayDepositAmount: itemx.totalPayDepositAmount,
                                    currency: itemx.currency
                                });
                            });
                        }
                        else
                        {
                            angular.forEach(responData, function (itemx) {
                                var checkdata = item.paymentNotePODepositResults.filter(x => x.purchaseOrderID === itemx.purchaseOrderID);
                                if (checkdata.length === 0) {
                                    item.paymentNotePODepositResults.push({
                                        paymentNotePODepositID: dataService.getIncrementId(),
                                        purchaseOrderID: itemx.purchaseOrderID,
                                        depositAmount: null,
                                        remark: '',
                                        purchaseOrderUD: itemx.purchaseOrderUD,
                                        purchaseOrderDate: itemx.purchaseOrderDate,
                                        paymentByPO: itemx.paymentByPO,
                                        totalPurchaseOrderAmount: itemx.totalPurchaseOrderAmount,
                                        totalDepositAmount: itemx.totalDepositAmount,
                                        remainDepositAmount: itemx.remainDepositAmount,
                                        remainPurchaseOrderAmount: itemx.remainPurchaseOrderAmount,
                                        totalPayDepositAmount: itemx.totalPayDepositAmount,
                                        currency: itemx.currency
                                    });
                                }
                            });
                        }
                        
                    },
                    function (error) {

                    }
                );
            }
        },

        calDepositInvoice: function (itemiv) {
            itemiv.depositAmount = 0;
            angular.forEach(itemiv.paymentNotePODepositResults, function (itemdps) {
                itemiv.depositAmount = parseFloat(itemiv.depositAmount) + parseFloat(itemdps.depositAmount === null ? 0 : itemdps.depositAmount);
            });
            $scope.detailEvent.calTotalInvoice();
            return itemiv;
        }
    };

    $scope.generalCtrlMethod = {
        getnewID: function () {
            $scope.getID--;
            return $scope.getID;
        },

        calTotal: function () {
            var total = 0;
            if ($scope.data.paymentNoteInvoiceResults.length > 0) {
                angular.forEach($scope.data.paymentNoteInvoiceResults, function (item) {

                });
            }
            return total;
        },

        calTotalForSupplier: function () {
            $scope.data.totalByCurrency = 0;
            if ($scope.data.paymentNoteTypeID === 2) {
                if ($scope.data.currency === 'USD') {
                    if ($scope.data.paymentNoteSupplierResults.length > 0) {
                        angular.forEach($scope.data.paymentNoteSupplierResults, function (item) {
                            $scope.data.totalByCurrency = $scope.data.totalByCurrency + parseFloat(item.depositAmount) * parseFloat($scope.data.exchangeRate);
                        });
                    }
                }
                if ($scope.data.currency === 'VND') {
                    if ($scope.data.paymentNoteSupplierResults.length > 0) {
                        angular.forEach($scope.data.paymentNoteSupplierResults, function (item) {
                            $scope.data.totalByCurrency = $scope.data.totalByCurrency + parseFloat(item.depositAmount) / parseFloat($scope.data.exchangeRate);
                        });
                    }
                }
            }
            return $scope.data.totalByCurrency;
        },

        //AutoComplete
        autocompleteSupplier: function () {
            $('#supplierAutoComplete').autocomplete({
                source: function (request, response) {
                    jQuery.ajax({
                        url: context.serviceUrl + 'query-supplier?param=' + request.term,
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json',
                            'Authorization': 'Bearer ' + context.token
                        },
                        dataType: "json",
                        type: "POST",
                        success: function (data) {
                            response($.map(data, function (value, key) {
                                return {
                                    id: value.factoryRawMaterialID,
                                    label: value.factoryRawMaterialOfficialNM,
                                    value: value.factoryRawMaterialOfficialNM,
                                    description: 'Code: ' + value.factoryRawMaterialUD,
                                    code: value.factoryRawMaterialUD
                                };
                            }));
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log(jqXHR);
                            console.log(textStatus);
                            console.log(errorThrown);
                        }
                    });
                },
                minLength: 3,
                select: function (event, ui) {
                    scope.$apply(function () {
                        $scope.data.factoryRawMaterialID = ui.item.id;
                        $scope.data.factoryRawMaterialOfficialNM = ui.item.label;
                        $scope.data.factoryRawMaterialUD = ui.item.code;
                    });
                },
                change: function (event, ui) {
                    if (!ui.item) {
                        scope.$apply(function () {
                            $scope.data.factoryRawMaterialID = null;
                            $scope.data.factoryRawMaterialOfficialNM = '';
                            $scope.data.factoryRawMaterialUD = '';
                        });
                        $('#supplierAutoComplete').val('');
                    }
                }
            })
                .data("ui-autocomplete")._renderItem = function (ul, item) {
                    return jQuery("<li>")
                        .data('item.autocomplete', item)
                        .append('<a href="javascript:void(0)"><strong style="text-decoration: underline; text-transform: uppercase;">' + item.label + '</strong><br>' + item.description + '</a>')
                        .appendTo(ul);
                };
        },

        autocompleteSupplierDetail: function () {
            if ($scope.data.paymentNoteTypeID === 2) {
                angular.forEach($scope.data.paymentNoteSupplierResults, function (item) {
                    $("#supplierAutoCompleteDetail" + item.paymentNoteClientID).autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                cache: false,
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json',
                                    'Authorization': 'Bearer ' + context.token
                                },
                                type: "POST",
                                dataType: 'JSON',
                                url: context.serviceUrl + 'query-supplier?param=' + request.term,
                                success: function (data) {
                                    response($.map(data, function (value, key) {
                                        return {
                                            id: value.factoryRawMaterialID,
                                            label: value.factoryRawMaterialOfficialNM,
                                            value: value.factoryRawMaterialUD,
                                            description: 'code: ' + value.factoryRawMaterialUD
                                        };
                                    }));
                                },
                                error: function (jqXHR, textStatus, errorThrown) {
                                    console.log(jqXHR);
                                    console.log(textStatus);
                                    console.log(errorThrown);
                                }
                            });
                        },
                        minLength: 3,
                        select: function (event, ui) {
                            scope.$apply(function () {
                                item.factoryRawMaterialID = ui.item.id;
                                item.factoryRawMaterialOfficialNM = ui.item.label;
                                item.factoryRawMaterialUD = ui.item.value;
                            });
                            $scope.event.getPaymentByDeposit($scope.data, item);
                            $scope.event.getTotalDeposit($scope.data, item);
                        },
                        change: function (event, ui) {
                            if (!ui.item) {
                                scope.$apply(function () {
                                    item.factoryRawMaterialID = null;
                                    item.factoryRawMaterialOfficialNM = '';
                                    item.factoryRawMaterialUD = '';
                                });
                                $('#supplierAutoCompleteDetail').val('');
                            }
                        }

                    }).data("ui-autocomplete")._renderItem = function (ul, item) {
                        return jQuery("<li>")
                            .data('item.autocomplete', item)
                            .append('<a href="javascript:void(0)"><strong style="text-decoration: underline; text-transform: uppercase;">' + item.label + '</strong><br>' + item.description + '</a>')
                            .appendTo(ul);
                    };
                });
            }
            else if ($scope.data.paymentNoteTypeID === 3) {
                angular.forEach($scope.data.paymentNoteOtherResults, function (item) {
                    $("#supplierAutoCompleteOther" + item.paymentNoteOtherID).autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                cache: false,
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json',
                                    'Authorization': 'Bearer ' + context.token
                                },
                                type: "POST",
                                dataType: 'JSON',
                                url: context.serviceUrl + 'query-supplier?param=' + request.term,
                                success: function (data) {
                                    response($.map(data, function (value, key) {
                                        return {
                                            id: value.factoryRawMaterialID,
                                            label: value.factoryRawMaterialOfficialNM,
                                            value: value.factoryRawMaterialUD,
                                            description: 'code: ' + value.factoryRawMaterialUD
                                        };
                                    }));
                                },
                                error: function (jqXHR, textStatus, errorThrown) {
                                    console.log(jqXHR);
                                    console.log(textStatus);
                                    console.log(errorThrown);
                                }
                            });
                        },
                        minLength: 3,
                        select: function (event, ui) {
                            scope.$apply(function () {
                                item.factoryRawMaterialID = ui.item.id;
                                item.factoryRawMaterialOfficialNM = ui.item.label;
                                item.factoryRawMaterialUD = ui.item.value;
                            });
                        },
                        change: function (event, ui) {
                            if (!ui.item) {
                                scope.$apply(function () {
                                    item.factoryRawMaterialID = null;
                                    item.factoryRawMaterialOfficialNM = '';
                                    item.factoryRawMaterialUD = '';
                                });
                                $('#supplierAutoCompleteOther').val('');
                            }
                        }

                    }).data("ui-autocomplete")._renderItem = function (ul, item) {
                        return jQuery("<li>")
                            .data('item.autocomplete', item)
                            .append('<a href="javascript:void(0)"><strong style="text-decoration: underline; text-transform: uppercase;">' + item.label + '</strong><br>' + item.description + '</a>')
                            .appendTo(ul);
                    };
                });
            }

        }
    };

    $scope.detailEvent = {
        //To purchasing invoice
        calTotalInvoice: function () {
            $scope.data.totalByCurrency = 0;
            if ($scope.data.paymentNoteTypeID === 1) {
                if ($scope.data.currency === 'USD') {
                    if ($scope.data.paymentNoteInvoiceResults.length > 0) {
                        angular.forEach($scope.data.paymentNoteInvoiceResults, function (item) {
                            $scope.data.totalByCurrency = $scope.data.totalByCurrency + ((parseFloat(item.amount) + (item.depositAmount === null ? 0 : parseFloat(item.depositAmount))) * (parseFloat($scope.data.exchangeRate)));
                        });
                    }
                }
                if ($scope.data.currency === 'VND') {
                    if ($scope.data.paymentNoteInvoiceResults.length > 0) {
                        angular.forEach($scope.data.paymentNoteInvoiceResults, function (item) {
                            $scope.data.totalByCurrency = $scope.data.totalByCurrency + ((parseFloat(item.amount) + (item.depositAmount === null ? 0 : parseFloat(item.depositAmount))) / (parseFloat($scope.data.exchangeRate)));
                        });
                    }
                }
            }
            return $scope.data.totalByCurrency;
            //item.total = parseFloat(item.amountByCurrency) + parseFloat(item.deposit);
            //$scope.generalCtrlMethod.calTotal();
            //$scope.event.getDepositThisNote();
        },

        calTotalPaymentInInvoice: function (item) {

            //if ($scope.oldValue.totalPayment === 0 && $scope.oldValue.remain === 0) {
            //    $scope.oldValue.totalPayment = item.totalPayment;
            //    $scope.oldValue.remain = item.remain; 
            //    $scope.oldValue.amount = item.amount;
            //}
            //var minimusAmount = 0;
            //if (item.currency === 'USD' && $scope.data.currency === 'VND') {
            //    minimusAmount = parseFloat($scope.oldValue.amount) / parseFloat($scope.data.exchangeRate);
            //    item.totalPayment = 0;
            //    item.totalPayment = parseFloat($scope.oldValue.totalPayment) + (parseFloat(item.amount) / parseFloat($scope.data.exchangeRate)) - minimusAmount;
            //    //Remain
            //    item.remain = 0;
            //    item.remain = parseFloat(item.totalPurchaseInvoice) - (parseFloat($scope.oldValue.totalPayment) + (parseFloat(item.amount) / parseFloat($scope.data.exchangeRate))) - minimusAmount;
            //}
            //else if (item.currency === 'VND' && $scope.data.currency === 'USD') {
            //    minimusAmount = parseFloat($scope.oldValue.amount) * parseFloat($scope.data.exchangeRate);
            //    item.totalPayment = 0;
            //    item.totalPayment = parseFloat($scope.oldValue.totalPayment) + (parseFloat(item.amount) * parseFloat($scope.data.exchangeRate)) - minimusAmount;
            //    //Remain
            //    item.remain = 0;
            //    item.remain = parseFloat(item.totalPurchaseInvoice) - (parseFloat($scope.oldValue.totalPayment) + (parseFloat(item.amount) * parseFloat($scope.data.exchangeRate))) - minimusAmount;
            //}
            //else {
            //    minimusAmount = parseFloat($scope.oldValue.amount);
            //    item.totalPayment = 0;
            //    item.totalPayment = parseFloat($scope.oldValue.totalPayment) + (parseFloat(item.amount)) - minimusAmount;
            //    //Remain
            //    item.remain = 0;
            //    item.remain = parseFloat(item.totalPurchaseInvoice) - (parseFloat($scope.oldValue.totalPayment) + parseFloat(item.amount)) - minimusAmount;
            //}

            $scope.detailEvent.calTotalInvoice();
            //return item;
        },

        changeCurrency: function () {
            if ($scope.data.paymentNoteTypeID === 1) {
                $scope.detailEvent.calTotalInvoice();
            }
            if ($scope.data.paymentNoteTypeID === 2) {
                $scope.generalCtrlMethod.calTotalForSupplier();
            }
        },

        getTotalByInvoice: function (item) {
            var results = 0;
            results = parseFloat(item.depositAmount === null ? 0 : item.depositAmount) + parseFloat(item.amount === null ? 0 : item.amount);
            return results;
        }
    };
    /*
     *initialization controller
     */
    $timeout(function () { $scope.initController(); }, 0);

}]);