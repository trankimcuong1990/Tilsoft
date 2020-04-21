tilsoftApp.controller('_GeneralCtrl', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
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
        addReceiptNoteClient: function () {
            $scope.data.receiptNoteClientResults.push({
                receiptNoteClientID: $scope.generalCtrlMethod.getnewID(),
                clientID: null,
                amount: null,
                remark: null,
                clientUD: null,
                clientNM: null
            });
        },
        removeReceiptNoteClient: function (item) {
            var index = $scope.data.receiptNoteClientResults.indexOf(item);
            $scope.data.receiptNoteClientResults.splice(index, 1);

            if ($scope.data.receiptNoteClientResults.length > 0) {
                if ($scope.data.totalByCurrency !== null && $scope.data.totalByCurrency !== '') {
                    $scope.data.totalByCurrency = parseFloat($scope.data.totalByCurrency) - parseFloat(item.amount);
                }
            } else {
                $scope.data.totalByCurrency = null;
            }
        },

        //For Other Zone
        addReceiptNoteOther: function () {
            $scope.data.receiptNoteOtherResults.push({
                receiptNoteOtherID: $scope.generalCtrlMethod.getnewID(),
                noteItemID: null,
                clientID: null,
                amount: null,
                clientUD: null,
                clientNM: null,
                remark: null
            });
        },
        removeReceiptNoteOther: function (item) {
            var index = $scope.data.receiptNoteOtherResults.indexOf(item);
            $scope.data.receiptNoteOtherResults.splice(index, 1);
        },

        //For Purchasing Invoice
        removeReceiptNotePurchasing: function (item) {
            var index = $scope.data.receiptNoteInvoiceResults.indexOf(item);
            $scope.data.receiptNoteInvoiceResults.splice(index, 1);
        },

        pushClientID: function () {
            context.supplierID = $scope.data.supplierID;
        },

        removeReceiptNoteEcommercial: function (item) {
            var index = $scope.data.receiptNoteInvoiceResults.indexOf(item);
            $scope.data.receiptNoteInvoiceResults.splice(index, 1);
        },

        pushFactoryRawMaterialID: function () {
            context.factoryRawMaterialID = $scope.data.factoryRawMaterialID;
        },

        removeReceiptNoteSaleInvoice: function (item) {
            var index = $scope.data.receiptNoteSaleInvoiceResults.indexOf(item);
            $scope.data.receiptNoteSaleInvoiceResults.splice(index, 1);
        },

        setstatus: function () {
            if (context.id !== 0) {
                const statusNM = $scope.status.filter(o => o.statusID === $scope.data.statusID)[0].statusNM;
                bootbox.confirm("Are you sure you want set Receipt Note to " + statusNM, function (result) {
                    if (result) {
                        dataService.setStatus(
                            $scope.data.receiptNoteID,
                            $scope.data.statusID,
                            $scope.data.receiptNoteTypeID,
                            function (data) {
                                jsHelper.processMessage(data.message);
                                if (data.message.type === 0) {
                                    //window.location = context.refreshUrl + $scope.data.receiptNoteID + '#/';
                                    $scope.event.load();
                                }
                            },
                            function (error) {
                                //
                            });
                    }
                });
            }
        },

        changeAmountByCurrency: function () {
            if ($scope.data.currency === 'VND') {
                var sumTotal = 0;
                angular.forEach($scope.data.receiptNoteInvoiceResults, function (item) {
                    if (item.amountByCurrency !== null && item.amountByCurrency !== '' && item.amountByCurrency !== undefined) {
                        sumTotal += parseFloat(item.amountByCurrency);
                    }
                });
                angular.forEach($scope.data.receiptNoteClientResults, function (item) {
                    if (item.amount !== null && item.amount !== '' && item.amount !== undefined) {
                        sumTotal += parseFloat(item.amount);
                    }
                });
                angular.forEach($scope.data.receiptNoteOtherResults, function (item) {
                    if (item.amount !== null && item.amount !== '' && item.amount !== undefined) {
                        sumTotal += parseFloat(item.amount);
                    }
                });
                angular.forEach($scope.data.receiptNoteSaleInvoiceResults, function (item) {
                    if (item.amountByCurrency !== null && item.amountByCurrency !== '' && item.amountByCurrency !== undefined) {
                        sumTotal += parseFloat(item.amountByCurrency);
                    }
                });
                $scope.data.totalByCurrency = sumTotal;
            } else {
                $scope.data.totalByCurrency = null;
            }
        }
    };

    $scope.generalCtrlMethod = {
        getnewID: function () {
            $scope.getID--;
            return $scope.getID;
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

        autocompleteClientDetail: function () {
            if ($scope.data.receiptNoteTypeID === 2) {
                angular.forEach($scope.data.receiptNoteClientResults, function (item) {
                    $('#supplierAutoCompleteReceiptNoteClient' + item.receiptNoteClientID).autocomplete({
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
                                item.factoryRawMaterialID = ui.item.id;
                                item.factoryRawMaterialOfficialNM = ui.item.label;
                                item.factoryRawMaterialUD = ui.item.code;
                            });
                        },
                        change: function (event, ui) {
                            if (!ui.item) {
                                scope.$apply(function () {
                                    item.factoryRawMaterialID = null;
                                    item.factoryRawMaterialOfficialNM = '';
                                    item.factoryRawMaterialUD = '';
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
                });
            }
            else if ($scope.data.receiptNoteTypeID === 3) {
                angular.forEach($scope.data.receiptNoteOtherResults, function (item) {
                    $("#clientAutoCompleteOther" + item.receiptNoteOtherID).autocomplete({
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
                                url: context.serviceUrl + 'query-employee?param=' + request.term,
                                success: function (data) {
                                    response($.map(data, function (value, key) {
                                        return {
                                            id: value.employeeID,
                                            label: value.employeeNM,
                                            value: value.employeeNM,
                                            description: 'code: ' + value.employeeUD
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
                                item.employeeID = ui.item.id;
                                item.employeeNM = ui.item.label;
                                item.employeeUD = ui.item.value;
                            });
                        },
                        change: function (event, ui) {
                            if (!ui.item) {
                                scope.$apply(function () {
                                    item.employeeID = null;
                                    item.employeeNM = '';
                                    item.employeeUD = '';
                                });
                                $('#clientAutoCompleteOther').val('');
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

        },

         autocompleteProductionItemDetail: function () {
            if ($scope.data.receiptNoteTypeID === 3) {
                angular.forEach($scope.data.receiptNoteOtherResults, function (item) {
                    $("#productionItemAutoCompleteOther" + item.receiptNoteOtherID).autocomplete({
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
                                url: context.serviceUrl + 'query-productionitem?param=' + request.term,
                                success: function (data) {
                                    response($.map(data, function (value, key) {
                                        return {
                                            id: value.productionItemID,
                                            label: value.productionItemNM,
                                            value: value.productionItemUD,
                                            description: 'code: ' + value.productionItemUD
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
                                item.productionItemID = ui.item.id;
                                item.productionItemNM = ui.item.label;
                                item.productionItemUD = ui.item.value;
                            });
                        },
                        change: function (event, ui) {
                            if (!ui.item) {
                                scope.$apply(function () {
                                    item.productionItemID = null;
                                    item.productionItemNM = '';
                                    item.productionItemUD = '';
                                });
                                $('#productionItemAutoCompleteOther').val('');
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

    /*
     *initialization controller
     */
    $timeout(function () { $scope.initController(); }, 0);

}]);