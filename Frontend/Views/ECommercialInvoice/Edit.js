var ecommercialinvoiceApp = angular.module('tilsoftApp', ['ng-currency']);
ecommercialinvoiceApp.filter('clientCostItemFilter', function () {
    return function (clientCostItems, polid) {
        var result = [];
        angular.forEach(clientCostItems, function (item) {
            if (item.polid == polid || item.polid == null) {
                result.push(item);
            }
        });
        return result;
    };
});
ecommercialinvoiceApp.controller('tilsoftController', ['$scope', function ($scope) {
    eCommercialInvoiceService.serviceUrl = context.serviceUrl;
    eCommercialInvoiceService.token = context.token;
    $scope.internalCompanyID = null;
    $scope.ecommercialinvoice = null;
    $scope.newID = -1;
    $scope.printOption = {
        printOptionSelected: 1
    };
    $scope.printOptions = [
        { id: 1, type: 'invoice', formatType: 'PDF', name: 'Eurofar / PDF', templateName: 'InvoicePrint', typeOfInvoice: 'all', companyID: 4 },
        { id: 2, type: 'invoice', formatType: 'PDF', name: 'Eurofar / PDF / FRANCE', templateName: 'InvoicePrint_ActionFrance', typeOfInvoice: 'all', companyID: 4 },
        { id: 3, type: 'invoice', formatType: 'Excel', name: 'Orange Pine / Excel', templateName: 'InvoicePrint_OrangePine', typeOfInvoice: 'all', companyID: 13 },
        //{ id: 4, type: 'invoice', formatType: 'PDF', name: 'Orange Pine / PDF', templateName: 'InvoicePrint_OrangePine', typeOfInvoice: 'all' },
        { id: 5, type: 'invoice', formatType: 'Excel', name: 'Orange Pine / Excel / Transport', templateName: 'InvoicePrint_OrangePine_Transport', companyID: 13, typeOfInvoice: 'transport' },
        { id: 6, type: 'invoice', formatType: 'Excel', name: 'Orange Pine / Excel / Transport / Client PO Number', templateName: 'InvoicePrint_ClientPONumber_OrangePine_Transport', companyID: 13, typeOfInvoice: 'transport' },
        { id: 7, type: 'packinglist', formatType: 'PDF', name: 'Eurofar / PDF', templateName: 'PackingListPrint', typeOfInvoice: 'all', companyID: 4 },
        { id: 8, type: 'packinglist', formatType: 'Excel', name: 'Orange Pine', templateName: 'PackingListPrint_OrangePine', typeOfInvoice: 'all', companyID: 13 },
        { id: 9, type: 'packinglist', formatType: 'Excel', name: 'Eurofar / Excel', templateName: 'PackingListPrint', typeOfInvoice: 'all', companyID: 4 },
        { id: 10, type: 'packinglist', formatType: 'PDF', name: 'Eurofar / PDF / Per container', templateName: 'PackingListPrint_PerCont', typeOfInvoice: 'all', companyID: 4 },
        { id: 11, type: 'packinglist', formatType: 'Excel', name: 'Eurofar / Excel / Per container', templateName: 'PackingListPrint_PerCont', typeOfInvoice: 'all', companyID: 4 }
    ];
    $scope.printToFile = function ($event) {
        //alert($scope.printOption.printOptionSelected);
        var printItem = $scope.printOptions.filter(o => o.id === $scope.printOption.printOptionSelected)[0];
        if (printItem.type === 'invoice') {
            $scope.printInvoice($event, printItem.formatType, printItem.templateName, $scope.ecommercialinvoice.eCommercialInvoiceData.typeOfInvoice);
        }
        else if (printItem.type === 'packinglist') {
            $scope.printPackingList($event, printItem.formatType, printItem.templateName);
        }
    };

    $scope.calTotalAmount = function () {
        if ($scope.ecommercialinvoice == null) return;
        var total = 0;
        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDetails, function (item) {
            total = total + item.unitPrice * item.quantity;
        });
        return total;
    };

    $scope.calTotalQuantity = function () {
        if ($scope.ecommercialinvoice == null) return;
        var total = 0;
        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDetails, function (item) {
            total = total + item.quantity;
        })
        return total;
    }

    $scope.calTotalQuantitySparepart = function () {
        if ($scope.ecommercialinvoice == null) return;
        var total = 0;
        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSparepartDetails, function (item) {
            total = total + item.quantity;
        })
        return total;
    }

    $scope.calTotalQuantitySample = function () {
        if ($scope.ecommercialinvoice == null) return;
        var total = 0;
        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSampleDetails, function (item) {
            total = total + item.quantity;
        })
        return total;
    }

    $scope.calTotalAmountSparepart = function () {
        if ($scope.ecommercialinvoice == null) return;
        var total = 0;
        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSparepartDetails, function (item) {
            total = total + item.unitPrice * item.quantity;
        })
        return total;
    }

    $scope.calTotalAmountSample = function () {
        if ($scope.ecommercialinvoice == null) return;
        var total = 0;
        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSampleDetails, function (item) {
            total = total + item.unitPrice * item.quantity;
        })
        return total;
    }

    $scope.calTotalQuantityWarehouseProduct = function () {
        if ($scope.ecommercialinvoice == null) return;
        var total = 0;
        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.warehouseInvoiceProductDetails, function (item) {
            total = total + item.quantity;
        })
        return total;
    }

    $scope.calTotalAmountWarehouseProduct = function () {
        if ($scope.ecommercialinvoice == null) return;
        var total = 0;
        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.warehouseInvoiceProductDetails, function (item) {
            total = total + item.unitPrice * item.quantity;
        })
        return total;
    }

    $scope.calTotalQuantityWarehouseSparepart = function () {
        if ($scope.ecommercialinvoice == null) return;
        var total = 0;
        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.warehouseInvoiceSparepartDetails, function (item) {
            total = total + item.quantity;
        })
        return total;
    }

    $scope.calTotalAmountWarehouseSparepart = function () {
        if ($scope.ecommercialinvoice == null) return;
        var total = 0;
        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.warehouseInvoiceSparepartDetails, function (item) {
            total = total + item.unitPrice * item.quantity;
        })
        return total;
    }

    $scope.reCal = function (netCallBack, vatCallBack, totalCalBack) {

        if ($scope.ecommercialinvoice == null) return;

        var net_amount = 0;
        var vat_amount = 0;

        var discount_and_warehouse_cost = 0;
        var remaining_cost = 0;

        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDetails, function (item) {
            net_amount += item.unitPrice * item.quantity;
        })

        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSparepartDetails, function (item) {
            net_amount += item.unitPrice * item.quantity;
        })

        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSampleDetails, function (item) {
            net_amount += item.unitPrice * item.quantity;
        })

        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceExtDetails, function (item) {
            net_amount += item.totalAmount;
        })

        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceContainerTransports, function (item) {
            net_amount += item.totalAmount;
        })

        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.warehouseInvoiceProductDetails, function (item) {
            net_amount += item.unitPrice * item.quantity;
        })

        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.warehouseInvoiceSparepartDetails, function (item) {
            net_amount += item.unitPrice * item.quantity;
        })

        angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDescriptions, function (item) {
            if (item.costType == 'D' || item.costType == 'W') // discount and warehouse cost
            {
                discount_and_warehouse_cost += item.totalAmount;
            }
            else {
                remaining_cost += item.totalAmount;
            }
        })

        net_amount = net_amount + discount_and_warehouse_cost;
        vat_amount = net_amount * ($scope.ecommercialinvoice.eCommercialInvoiceData.vatRate == null || $scope.ecommercialinvoice.eCommercialInvoiceData.vatRate == 'undefined' ? 0 : $scope.ecommercialinvoice.eCommercialInvoiceData.vatRate) / 100;

        //$scope.ecommercialinvoice.eCommercialInvoiceData.netAmount = net_amount;
        //$scope.ecommercialinvoice.eCommercialInvoiceData.vatAmount = vat_amount;
        //$scope.ecommercialinvoice.eCommercialInvoiceData.totalAmount = (net_amount + vat_amount + remaining_cost);

        if (netCallBack != null) netCallBack(net_amount);
        if (vatCallBack != null) vatCallBack(vat_amount);
        if (totalCalBack != null) totalCalBack(net_amount + vat_amount + remaining_cost);
    }

    $scope.netRecal = function () {
        amount = 0;
        $scope.reCal(function (net_amount) { amount = net_amount; }, null, null);
        if ($scope.ecommercialinvoice != null) {
            $scope.ecommercialinvoice.eCommercialInvoiceData.netAmount = amount;
        }
        
        return amount;
    }

    $scope.vatRecal = function () {
        $scope.reCal(null, function (vatAmount) { amount = vatAmount; }, null);
        if ($scope.ecommercialinvoice != null) {
            $scope.ecommercialinvoice.eCommercialInvoiceData.vatAmount = amount;
        }
        return amount;
    }

    $scope.totalRecal = function () {
        $scope.reCal(null, null, function (totalAmount) { amount = totalAmount; });
        if ($scope.ecommercialinvoice != null) {
            $scope.ecommercialinvoice.eCommercialInvoiceData.totalAmount = amount;
        }
        return amount;
    }

    //function define
    $scope.load = function () {
        $scope.internalCompanyID = context.internalCompanyID;
        eCommercialInvoiceService.load(context.id, context.invoiceTypeID, context.internalCompanyID, context.clientID, context.parentID,
            function (data) {
                $scope.ecommercialinvoice = data.data;
                $scope.$apply();

                jQuery('#content').show();
                $scope.$watch('ecommercialinvoice', function () {
                    jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                });

                //load form select product
                if (context.id === 0) {
                    if ($scope.ecommercialinvoice.eCommercialInvoiceData.typeOfInvoice == 1) //fob invoice
                    {
                        $scope.addFobProduct();
                    }
                    else if ($scope.ecommercialinvoice.eCommercialInvoiceData.typeOfInvoice == 2) //warehouse invoice
                    {
                        $scope.addWarehouseGoods();
                    }
                    else if ($scope.ecommercialinvoice.eCommercialInvoiceData.typeOfInvoice == 3) //other invoice
                    {
                        //do nothing
                    }
                }

                //assign auto complete for B/L No incase typeOfInvoice = 4 (Container Transport Invoice)
                $("#bookingID").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            cache: false,
                            headers: {
                                'Accept': 'application/json',
                                'Content-Type': 'application/json',
                                'Authorization': 'Bearer ' + eCommercialInvoiceService.token
                            },
                            type: "POST",
                            data: JSON.stringify({
                                filters: {
                                    searchQuery: request.term,
                                    clientID: $scope.ecommercialinvoice.eCommercialInvoiceData.clientID,
                                }
                            }),
                            dataType: 'json',
                            url: eCommercialInvoiceService.serviceUrl + 'getBillTransport',
                            beforeSend: function () {
                                jsHelper.loadingSwitch(true);
                            },
                            success: function (data) {
                                jsHelper.loadingSwitch(false);
                                response(data.data);
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                jsHelper.loadingSwitch(false);
                                errorCallBack(xhr.responseJSON.exceptionMessage);
                            }
                        });
                    },
                    minLength: 2,
                    select: function (event, ui) {
                        $scope.ecommercialinvoice.eCommercialInvoiceData.bookingID = ui.item.id;
                        $scope.ecommercialinvoice.eCommercialInvoiceData.polName = ui.item.polName;
                        $scope.ecommercialinvoice.eCommercialInvoiceData.polid = ui.item.polid
                        $scope.ecommercialinvoice.containerTransports = ui.item.containerTransports;
                        $scope.ecommercialinvoice.clientOfferCostItems = ui.item.clientOfferCostItems;
                        $scope.$apply();
                    }
                });

                //angular refresh
                setTimeout(function () { runAllForms() }, 200);
            },
            function (error) {
                jsHelper.showMessage('warning', error);
                $scope.ecommercialinvoice = null;
                $scope.$apply();
            }
        );
    }

    $scope.update = function ($event) {
        $event.preventDefault();
        if ($scope.editForm.$valid) {
            eCommercialInvoiceService.update(context.id, $scope.ecommercialinvoice.eCommercialInvoiceData,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {
                        window.location = context.refreshUrl + data.data.eCommercialInvoiceID;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        }
        else {
            jsHelper.showMessage('warning', context.errMsg);
        }
    }

    $scope.delete = function ($event) {
        $event.preventDefault();

        if (confirm('Are you sure ?')) {
            eCommercialInvoiceService.delete(context.id,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {
                        window.location = context.indexUrl;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        }
    }

    $scope.showPrintOption = function ($event) {
        $event.preventDefault();
        $('#frmPrintOption').modal('show');
    };

    $scope.printInvoice = function ($event, formatType, reportName, invoiceType) {
        $event.preventDefault();
        eCommercialInvoiceService.printInvoice(context.id, formatType, reportName, invoiceType,
            function (data) {
                if (data.message.type == 2) {
                    jsHelper.processMessage(data.message);
                    return;
                }
                window.open(context.backendReportUrl + data.data, '');
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            }
        );
    };

    $scope.printPackingList = function ($event, formatType, reportname) {
        $event.preventDefault();
        eCommercialInvoiceService.printPackingList(
            context.id,
            formatType,
            reportname,
            function (data) {
                if (data.message.type == 2) {
                    jsHelper.processMessage(data.message);
                    return;
                }
                var fileNames = data.data.split(',');
                if (fileNames.length > 1) {
                    angular.forEach(fileNames, function (item) {
                        window.open(context.backendReportUrl + item, '');
                    });
                }
                else {
                    window.open(context.backendReportUrl + data.data, '');
                }
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            }
        );
    };

    $scope.confirmInvoice = function ($event) {
        $event.preventDefault();

        if (confirm('Are you sure confirm this invoice?')) {

            //update invoice before confirm
            eCommercialInvoiceService.update(context.id, $scope.ecommercialinvoice.eCommercialInvoiceData,
                function (data) {
                    if (data.message.type == 0) {
                        //confirm invoice
                        var eID = data.data.eCommercialInvoiceID;
                        eCommercialInvoiceService.confirmInvoice(eID, true,
                            function (data) {
                                window.location = context.refreshUrl + eID;
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error);
                            }
                        );
                    }
                    else {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        }
    }

    $scope.unConfirmInvoice = function ($event) {
        $event.preventDefault();

        if (confirm('Are you sure un-confirm this invoice?')) {
            eCommercialInvoiceService.confirmInvoice(context.id, false,
                function (data) {
                    jsHelper.processMessage(data.message);
                    window.location = context.refreshUrl + context.id;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    }

    $scope.setDonePayment = function ($event) {
        $event.preventDefault();

        isDonePayment = ($scope.ecommercialinvoice.eCommercialInvoiceData.isDonePayment != null && $scope.ecommercialinvoice.eCommercialInvoiceData.isDonePayment ? true : false);
        isDonePayment = !isDonePayment;
        eCommercialInvoiceService.setDonePayment(context.id, isDonePayment,
            function (data) {
                jsHelper.processMessage(data.message);
                $scope.ecommercialinvoice.eCommercialInvoiceData.isDonePayment = data.data.isDonePayment;
                $scope.ecommercialinvoice.eCommercialInvoiceData.isDonePaymentText = data.data.isDonePaymentText;
                $scope.ecommercialinvoice.eCommercialInvoiceData.isDonePaymentLabel = data.data.isDonePaymentLabel;
                $scope.ecommercialinvoice.eCommercialInvoiceData.concurrencyFlag_String = data.data.concurrencyFlag_String;
                $scope.$apply();
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            }
        );
    }

    $scope.addFobProduct = function () {
        //set input data
        $scope.popupForm.inputData.clientID = $scope.ecommercialinvoice.eCommercialInvoiceData.clientID;
        //call event load
        $scope.popupForm.event.load('product');
    }

    $scope.addFobSparepart = function () {
        //set input data
        $scope.popupForm.inputData.clientID = $scope.ecommercialinvoice.eCommercialInvoiceData.clientID;
        //call event load
        $scope.popupForm.event.load('sparepart');
    }

    $scope.addFobSample = function () {
        //set input data
        $scope.popupForm.inputData.clientID = $scope.ecommercialinvoice.eCommercialInvoiceData.clientID;
        //call event load
        $scope.popupForm.event.load('sample');
    }

    $scope.removeFobProduct = function ($event, eCommercialInvoiceDetailID) {
        $event.preventDefault();

        var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDetails;
        isExist = false;

        for (j = 0; j < arr.length; j++) {
            if (arr[j].eCommercialInvoiceDetailID == eCommercialInvoiceDetailID) {
                isExist = true;
                break;
            }
        }
        if (isExist) {
            arr.splice(j, 1);
        }
    }

    $scope.removeFobSparepart = function ($event, eCommercialInvoiceSparepartDetailID) {
        $event.preventDefault();

        var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSparepartDetails;
        isExist = false;

        for (j = 0; j < arr.length; j++) {
            if (arr[j].eCommercialInvoiceSparepartDetailID == eCommercialInvoiceSparepartDetailID) {
                isExist = true;
                break;
            }
        }
        if (isExist) {
            arr.splice(j, 1);
        }
    }

    $scope.removeFobSample = function ($event, eCommercialInvoiceSampleDetailID) {
        $event.preventDefault();

        var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSampleDetails;
        isExist = false;

        for (j = 0; j < arr.length; j++) {
            if (arr[j].eCommercialInvoiceSampleDetailID == eCommercialInvoiceSampleDetailID) {
                isExist = true;
                break;
            }
        }
        if (isExist) {
            arr.splice(j, 1);
        }
    }

    $scope.addWarehouseGoods = function () {
        //set input data
        $scope.addWarehouseProductForm.inputData.clientID = $scope.ecommercialinvoice.eCommercialInvoiceData.clientID;
        //call event load
        $scope.addWarehouseProductForm.event.load();
    }

    $scope.removeWarehouseGoods = function ($event, cmrNo) {
        $event.preventDefault();

        if (!confirm('When you remove this product then all product that have same CMR.No will be deleted. Are you sure delete?')) {
            return;
        }

        //remove product
        var arr_position = [];
        var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.warehouseInvoiceProductDetails;

        for (j = 0; j < arr.length; j++) {
            if (arr[j].cmrNo == cmrNo) {
                arr_position.push({ position: j });
            }
        }

        for (j = 0; j < arr_position.length; j++) {
            arr.splice(arr_position[j], 1);
        }

        //remove sapreaprt
        arr_position = [];
        arr = $scope.ecommercialinvoice.eCommercialInvoiceData.warehouseInvoiceSparepartDetails;

        for (j = 0; j < arr.length; j++) {
            if (arr[j].cmrNo == cmrNo) {
                arr_position.push({ position: j });
            }
        }

        for (j = 0; j < arr_position.length; j++) {
            arr.splice(arr_position[j], 1);
        }
    }


    $scope.addDescription = function ($event, position) {
        $event.preventDefault();
        //var tops = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDescriptions.filter(function(o){return o.position == 'TOP'});
        //$(tops).each(function(){alert(this.description);});

        var descrip = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDescriptions;
        max_rowIndex = 1;
        if (descrip.length > 0) {
            descrip.sort(function (a, b) { return b.rowIndex - a.rowIndex });
            max_rowIndex = descrip[0].rowIndex + 1;
        }
        $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDescriptions.push({
            eCommercialInvoiceDescriptionID: max_rowIndex * (-1),
            rowIndex: max_rowIndex,
            position: position,
            costType: 'O',
            totalAmount: null,
        });
        setTimeout(function () { runAllForms() }, 200);
    }

    $scope.removeDescription = function ($event, id) {
        $event.preventDefault();

        isExist = false;
        for (j = 0; j < $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDescriptions.length; j++) {
            if ($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDescriptions[j].eCommercialInvoiceDescriptionID == id) {
                isExist = true;
                break;
            }
        }
        if (isExist) {
            $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDescriptions.splice(j, 1);
        }
    }

    $scope.insertDescription = function ($event, position, rowIndex) {
        $event.preventDefault();
        var descrip = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDescriptions;
        for (i = 0; i < descrip.length; i++) {
            if ($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDescriptions[i].rowIndex > rowIndex) {
                $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDescriptions[i].rowIndex = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDescriptions[i].rowIndex + 1;
                if ($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDescriptions[i].eCommercialInvoiceDescriptionID < 0) {
                    $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDescriptions[i].eCommercialInvoiceDescriptionID = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDescriptions[i].eCommercialInvoiceDescriptionID - 1;
                }
            }
        }
        $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDescriptions.push({
            eCommercialInvoiceDescriptionID: (rowIndex + 1) * (-1),
            rowIndex: rowIndex + 1,
            position: position

        });
    }

    $scope.addExtDetail = function ($event) {
        $event.preventDefault();
        //var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceExtDetails.filter(function(o){ return o.eCommercialInvoiceExtDetailID < 0});
        //max_id = -1;
        //if (arr.length > 0)
        //{
        //    arr.sort(function(a,b){return a.eCommercialInvoiceExtDetailID - b.eCommercialInvoiceExtDetailID});
        //    max_id = arr[0].eCommercialInvoiceExtDetailID - 1;
        //}
        $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceExtDetails.push({
            eCommercialInvoiceExtDetailID: $scope.method.getNewID(),
        });
        setTimeout(function () { runAllForms() }, 200);
    }

    $scope.removeExtDetail = function ($event, id) {
        $event.preventDefault();

        isExist = false;
        for (j = 0; j < $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceExtDetails.length; j++) {
            if ($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceExtDetails[j].eCommercialInvoiceExtDetailID == id) {
                isExist = true;
                break;
            }
        }
        if (isExist) {
            $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceExtDetails.splice(j, 1);
        }
    }

    $scope.viewDetailDescription = function ($event, id) {
        $event.preventDefault();
        $scope.detailDesciprtionForm.event.load(id);
    },

    $scope.viewsDetailDescription = function ($event, id) {
        $event.preventDefault();
        $scope.sparepartdetailDesciprtionForm.event.load(id);
    },

    $scope.selectedDelivery = function (id) {
        $($scope.ecommercialinvoice.deliveryTerms).each(function () {
            if (this.deliveryTermID == id) {
                $scope.ecommercialinvoice.eCommercialInvoiceData.deliveryTerm = this.deliveryTermNM;
                $scope.ecommercialinvoice.eCommercialInvoiceData.deliveryTypeNM = this.deliveryTypeNM;
                $scope.$apply();
            }
        });
        },

    $scope.viewSampleDetailDescription = function ($event, id) {
        $event.preventDefault();
        $scope.detailSampleDesciprtionForm.event.load(id);
    },

    $scope.selectedPaymentTerm = function (id) {
        $($scope.ecommercialinvoice.paymentTerms).each(function () {
            if (this.paymentTermID == id) {
                $scope.ecommercialinvoice.eCommercialInvoiceData.paymentTerm = this.paymentTermNM;
                $scope.ecommercialinvoice.eCommercialInvoiceData.paymentTypeNM = this.paymentTypeNM;
                $scope.$apply();
            }
        });
    }

    $scope.createCreditNote = function ($event) {
        typeOfInvoice = 4; // 1:FOB-INVOICE, 2:WAREHOUSE-INVOICE, 3: OTHER-INVOICE, 4: CREDITNOTE-INVOICE
        parentID = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceID;
        window.open(context.refreshUrl + '/0?invoiceTypeID=' + typeOfInvoice + '&parentID=' + parentID, '');
    }

    $scope.selectedPercentCost = function (top_item) {
        top_item.totalAmount = top_item.percent / 100 * ($scope.calTotalAmount() + $scope.calTotalAmountSparepart());
    }

    $scope.selected_Quantity_Price = function (itemExtDetail) {
        itemExtDetail.totalAmount = itemExtDetail.quantity * itemExtDetail.unitPrice;
    }

    $scope.addContainerTransportItem = function () {
        if ($scope.ecommercialinvoice.eCommercialInvoiceData.bookingID == null) {
            bootbox.alert('You should select B/L No before add cost');
            return;
        }
        $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceContainerTransports.push({
            eCommercialInvoiceContainerTransportID: $scope.method.getNewID(),
        });
        setTimeout(function () { runAllForms() }, 200);
    }

    $scope.removeContainerTransportItem = function ($event, id) {
        $event.preventDefault();
        isExist = false;
        for (j = 0; j < $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceContainerTransports.length; j++) {
            if ($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceContainerTransports[j].eCommercialInvoiceContainerTransportID == id) {
                isExist = true;
                break;
            }
        }
        if (isExist) {
            $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceContainerTransports.splice(j, 1);
        }
    }

    $scope.addSpecificContainerTransportItem = function () {
        if ($scope.ecommercialinvoice.eCommercialInvoiceData.bookingID == null) {
            bootbox.alert('You should select B/L No before add cost');
            return;
        }

        if ($scope.ecommercialinvoice.eCommercialInvoiceData.currency == null) {
            bootbox.alert('You should select currency before add cost');
            return;
        }

        $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceContainerTransports = [];
        //add all client cost item for every container
        var currency = $scope.ecommercialinvoice.eCommercialInvoiceData.currency;
        var clientOfferCostItems = $scope.ecommercialinvoice.clientOfferCostItems.filter(function (o) { return o.currency == currency });
        angular.forEach($scope.ecommercialinvoice.containerTransports, function (item) {
            angular.forEach(clientOfferCostItems, function (cItem) {

                //get offer amount
                var offerAmount = null;
                if (item.containerTypeNM == '20DC') {
                    offerAmount = cItem.cost20DC;
                }
                else if (item.containerTypeNM == '40DC') {
                    offerAmount = cItem.cost40DC;
                }
                else if (item.containerTypeNM == '40HC') {
                    offerAmount = cItem.cost40HC;
                }

                //push to dto detail
                $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceContainerTransports.push({
                    eCommercialInvoiceContainerTransportID: $scope.method.getNewID(),
                    clientCostItemID: cItem.clientCostItemID,
                    loadingPlanID: item.loadingPlanID,
                    containerNo: item.containerNo,
                    containerTypeNM: item.containerTypeNM,
                    etd: item.etd,
                    totalAmount: offerAmount,
                });
            });
        });
    }

    //popup form
    $scope.popupForm = {

        inputData: {
            clientID: 0
        },
        purchasingInvoiceSearchResult: null,
        selectedPurchasingInvoiceID: -1,
        goodsType: 'product',

        event: {
            load: function (goodsType) {
                $scope.popupForm.goodsType = goodsType;
                $scope.popupForm.selectedPurchasingInvoiceID = -1;
                $scope.popupForm.method.search_purchasingInvoice($scope.popupForm.inputData.clientID)
            },
            ok: function ($event) {
                $event.preventDefault();

                var arr = $scope.popupForm.purchasingInvoiceSearchResult.filter(function (o) { return o.purchasingInvoiceID == $scope.popupForm.selectedPurchasingInvoiceID });

                //INIT bill info & currency & VAT
                if (arr != null && arr.length > 0) {
                    //SET INVOICE INFO
                    arr_booking = $scope.ecommercialinvoice.eCommercialInvoiceData.bookings.filter(function (o) { return o.invoiceNo == arr[0].invoiceNo });
                    if (arr_booking.length == 0) {
                        $scope.ecommercialinvoice.eCommercialInvoiceData.bookings.push({
                            invoiceNo: arr[0].invoiceNo,
                            factoryInvoiceNo: arr[0].factoryInvoiceNo,
                            blNo: arr[0].blNo
                        });
                    }
                    //SET CURRENCY & VAT & Condtions
                    arr_detail = arr[0].purchasingInvoiceDetails.filter(function (o) { return o.isSelected == true && o.clientID == $scope.popupForm.inputData.clientID });
                    if (arr_detail.length > 0) {
                        $scope.ecommercialinvoice.eCommercialInvoiceData.currency = arr_detail[0].currency;
                        $scope.ecommercialinvoice.eCommercialInvoiceData.vatRate = arr_detail[0].vat;
                        $scope.ecommercialinvoice.eCommercialInvoiceData.conditions = arr_detail[0].conditions;

                        $scope.ecommercialinvoice.eCommercialInvoiceData.paymentTermID = arr_detail[0].paymentTermID;
                        $scope.ecommercialinvoice.eCommercialInvoiceData.paymentTerm = arr_detail[0].paymentTermNM;
                        $scope.ecommercialinvoice.eCommercialInvoiceData.deliveryTermID = arr_detail[0].deliveryTermID;
                        $scope.ecommercialinvoice.eCommercialInvoiceData.deliveryTerm = arr_detail[0].deliveryTermNM;

                    }
                    else {
                        //If not exist in table detail then taken from table sparepart
                        arr_detail_sparepart = arr[0].purchasingInvoiceSparepartDetails.filter(function (o) { return o.isSelected == true && o.clientID == $scope.popupForm.inputData.clientID });
                        if (arr_detail_sparepart.length > 0) {
                            $scope.ecommercialinvoice.eCommercialInvoiceData.currency = arr_detail_sparepart[0].currency;
                            $scope.ecommercialinvoice.eCommercialInvoiceData.vatRate = arr_detail_sparepart[0].vat;
                            $scope.ecommercialinvoice.eCommercialInvoiceData.conditions = arr_detail_sparepart[0].conditions;

                            $scope.ecommercialinvoice.eCommercialInvoiceData.paymentTermID = arr_detail_sparepart[0].paymentTermID;
                            $scope.ecommercialinvoice.eCommercialInvoiceData.paymentTerm = arr_detail_sparepart[0].paymentTermNM;
                            $scope.ecommercialinvoice.eCommercialInvoiceData.deliveryTermID = arr_detail_sparepart[0].deliveryTermID;
                            $scope.ecommercialinvoice.eCommercialInvoiceData.deliveryTerm = arr_detail_sparepart[0].deliveryTermNM;
                        }
                        else {
                            arr_detail_sample = arr[0].purchasingInvoiceSampleDetails.filter(function (o) { return o.isSelected == true && o.clientID == $scope.popupForm.inputData.clientID });
                            if (arr_detail_sample.length > 0) {
                                $scope.ecommercialinvoice.eCommercialInvoiceData.currency = arr_detail_sample[0].currency;
                                $scope.ecommercialinvoice.eCommercialInvoiceData.vatRate = arr_detail_sample[0].vat;
                                $scope.ecommercialinvoice.eCommercialInvoiceData.conditions = arr_detail_sample[0].conditions;

                                $scope.ecommercialinvoice.eCommercialInvoiceData.paymentTermID = arr_detail_sample[0].paymentTermID;
                                $scope.ecommercialinvoice.eCommercialInvoiceData.paymentTerm = arr_detail_sample[0].paymentTermNM;
                                $scope.ecommercialinvoice.eCommercialInvoiceData.deliveryTermID = arr_detail_sample[0].deliveryTermID;
                                $scope.ecommercialinvoice.eCommercialInvoiceData.deliveryTerm = arr_detail_sample[0].deliveryTermNM;
                            }
                        }
                    }
                }

                //ADD PRODUCT
                max_id = -1;
                var arr_purchasingInvoiceDetail = [];
                if (arr != null && arr.length > 0)
                    arr_purchasingInvoiceDetail = arr[0].purchasingInvoiceDetails.filter(function (o) { return o.isSelected == true && o.clientID == $scope.popupForm.inputData.clientID });

                $(arr_purchasingInvoiceDetail).each(function () {
                    id = this.purchasingInvoiceDetailID;
                    //check is exist purchasingInvoice item detail in eCommercialInvoiceDetails
                    var arr_purchasinginvoiceID = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDetails.filter(function (o) { return o.purchasingInvoiceDetailID == id });
                    if (arr_purchasinginvoiceID == null || arr_purchasinginvoiceID.length == 0) {
                        var productItem = {
                            eCommercialInvoiceDetailID: max_id,
                            quantity: this.quantity,
                            unitPrice: this.unitPrice,
                            articleCode: this.articleCode,
                            description: this.description,
                            proformaInvoiceNo: this.proformaInvoiceNo,
                            containerNo: this.containerNo,
                            sealNo: this.sealNo,
                            containerType: this.containerType,
                            loadingPlanDetailID: this.loadingPlanDetailID,

                            currency: this.currency,
                            clientArticleCode: this.clientArticleCode,
                            clientArticleName: this.clientArticleName,
                            clientOrderNumber: this.clientOrderNumber,
                            clientEANCode: this.clientEANCode,
                            hsCode: this.hsCode,

                            vatRate: this.vat,
                            conditions: this.conditions,
                            paymentTermID: this.paymentTermID,
                            paymentTermNM: this.paymentTermNM,
                            deliveryTermID: this.deliveryTermID,
                            deliveryTermNM: this.deliveryTermNM,

                            //description of product item
                            eCommercialInvoiceDetailDescriptions: []
                        };

                        //add description line
                        if (productItem.clientArticleCode !== null && productItem.clientArticleCode !== '') {
                            productItem.eCommercialInvoiceDetailDescriptions.push({
                                eCommercialInvoiceDetailDescriptionID: $scope.method.getNewID(),
                                rowIndex: 1,
                                description: 'CLIENT ARTICLE CODE:' + productItem.clientArticleCode
                            });
                        }
                        if (productItem.clientArticleName !== null && productItem.clientArticleName !== '') {
                            productItem.eCommercialInvoiceDetailDescriptions.push({
                                eCommercialInvoiceDetailDescriptionID: $scope.method.getNewID(),
                                rowIndex: 2,
                                description: 'CLIENT ATICLE NAME:' + productItem.clientArticleName
                            });
                        }
                        if (productItem.clientOrderNumber !== null && productItem.clientOrderNumber !== '') {
                            productItem.eCommercialInvoiceDetailDescriptions.push({
                                eCommercialInvoiceDetailDescriptionID: $scope.method.getNewID(),
                                rowIndex: 3,
                                description: 'CLIENT ORDER NUMBER:' + productItem.clientOrderNumber
                            });
                        }
                        if (productItem.clientEANCode !== null && productItem.clientEANCode !== '') {
                            productItem.eCommercialInvoiceDetailDescriptions.push({
                                eCommercialInvoiceDetailDescriptionID: $scope.method.getNewID(),
                                rowIndex: 4,
                                description: 'CLIENT EAN CODE:' + productItem.clientEANCode
                            });
                        }
                        if (productItem.hsCode !== null && productItem.hsCode !== '') {
                            productItem.eCommercialInvoiceDetailDescriptions.push({
                                eCommercialInvoiceDetailDescriptionID: $scope.method.getNewID(),
                                rowIndex: 5,
                                description: 'HS Code:' + productItem.hsCode
                            });
                        }
                        $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDetails.push(productItem);
                    }
                    max_id = max_id - 1;
                });

                //ADD SPAREPART
                max_id = -1;
                var arr_purchasingInvoiceSparepartDetail = [];
                if (arr != null && arr.length > 0)
                    arr_purchasingInvoiceSparepartDetail = arr[0].purchasingInvoiceSparepartDetails.filter(function (o) { return o.isSelected == true && o.clientID == $scope.popupForm.inputData.clientID });
                $(arr_purchasingInvoiceSparepartDetail).each(function () {
                    id = this.purchasingInvoiceSparepartDetailID;
                    //check is exist purchasing invoice sparepart item detail in eCommercialInvoiceDetailSpareparts
                    var arr_purchasinginvoiceSparepartID = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSparepartDetails.filter(function (o) { return o.purchasingInvoiceSparepartDetailID == id });
                    if (arr_purchasinginvoiceSparepartID == null || arr_purchasinginvoiceSparepartID.length == 0) {
                        var sparepartItem = {
                            eCommercialInvoiceSparepartDetailID: max_id,
                            quantity: this.quantity,
                            unitPrice: this.unitPrice,
                            articleCode: this.articleCode,
                            description: this.description,
                            proformaInvoiceNo: this.proformaInvoiceNo,
                            containerNo: this.containerNo,
                            sealNo: this.sealNo,
                            containerType: this.containerType,
                            loadingPlanSparepartDetailID: this.loadingPlanSparepartDetailID,

                            currency: this.currency,
                            clientArticleCode: this.clientArticleCode,
                            clientArticleName: this.clientArticleName,
                            clientOrderNumber: this.clientOrderNumber,
                            clientEANCode: this.clientEANCode,
                            hsCode: this.hsCode,

                            vatRate: this.vat,
                            conditions: this.conditions,
                            paymentTermID: this.paymentTermID,
                            paymentTermNM: this.paymentTermNM,
                            deliveryTermID: this.deliveryTermID,
                            deliveryTermNM: this.deliveryTermNM,

                            eCommercialInvoiceSparepartDetailDescriptions: []
                        };
                        //add description line
                        if (sparepartItem.clientArticleCode !== null && sparepartItem.clientArticleCode !== '') {
                            sparepartItem.eCommercialInvoiceSparepartDetailDescriptions.push({
                                eCommercialInvoiceSparepartDetailDescriptionID: $scope.method.getNewID(),
                                rowIndex: 1,
                                description: 'CLIENT ARTICLE CODE:' + sparepartItem.clientArticleCode
                            });
                        }
                        if (sparepartItem.clientArticleName !== null && sparepartItem.clientArticleName !== '') {
                            sparepartItem.eCommercialInvoiceSparepartDetailDescriptions.push({
                                eCommercialInvoiceSparepartDetailDescriptionID: $scope.method.getNewID(),
                                rowIndex: 2,
                                description: 'CLIENT ATICLE NAME:' + sparepartItem.clientArticleName
                            });
                        }
                        if (sparepartItem.clientOrderNumber !== null && sparepartItem.clientOrderNumber !== '') {
                            sparepartItem.eCommercialInvoiceSparepartDetailDescriptions.push({
                                eCommercialInvoiceSparepartDetailDescriptionID: $scope.method.getNewID(),
                                rowIndex: 3,
                                description: 'CLIENT ORDER NUMBER:' + sparepartItem.clientOrderNumber
                            });
                        }
                        if (sparepartItem.clientEANCode !== null && sparepartItem.clientEANCode !== '') {
                            sparepartItem.eCommercialInvoiceSparepartDetailDescriptions.push({
                                eCommercialInvoiceSparepartDetailDescriptionID: $scope.method.getNewID(),
                                rowIndex: 4,
                                description: 'CLIENT EAN CODE:' + sparepartItem.clientEANCode
                            });
                        }
                        if (sparepartItem.hsCode !== null && sparepartItem.hsCode !== '') {
                            productItem.eCommercialInvoiceDetailDescriptions.push({
                                eCommercialInvoiceDetailDescriptionID: $scope.method.getNewID(),
                                rowIndex: 5,
                                description: 'HS Code:' + sparepartItem.hsCode
                            });
                        }
                        $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSparepartDetails.push(sparepartItem);
                    }
                    max_id = max_id - 1;
                });

                //ADD SAMPLE
                max_id = -1;
                var arr_purchasingInvoiceSampleDetail = [];
                if (arr != null && arr.length > 0)
                    arr_purchasingInvoiceSampleDetail = arr[0].purchasingInvoiceSampleDetails.filter(function (o) { return o.isSelected == true && o.clientID == $scope.popupForm.inputData.clientID });

                $(arr_purchasingInvoiceSampleDetail).each(function () {
                    id = this.purchasingInvoiceSampleDetailID;
                    //check is exist purchasingInvoice item detail in eCommercialInvoiceDetails
                    var arr_purchasinginvoiceID = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSampleDetails.filter(function (o) { return o.purchasingInvoiceDetailID == id });
                    if (arr_purchasinginvoiceID == null || arr_purchasinginvoiceID.length == 0) {
                        var productItem = {
                            eCommercialInvoiceSampleDetailID: max_id,
                            quantity: this.quantity,
                            unitPrice: this.unitPrice,
                            articleCode: this.articleCode,
                            description: this.description,
                            proformaInvoiceNo: this.proformaInvoiceNo,
                            containerNo: this.containerNo,
                            sealNo: this.sealNo,
                            containerType: this.containerType,
                            loadingPlanSampleDetailID: this.loadingPlanSampleDetailID,

                            currency: this.currency,
                            clientArticleCode: this.clientArticleCode,
                            clientArticleName: this.clientArticleName,
                            clientOrderNumber: this.clientOrderNumber,
                            clientEANCode: this.clientEANCode,
                            hsCode: this.hsCode,

                            vatRate: this.vat,
                            conditions: this.conditions,
                            paymentTermID: this.paymentTermID,
                            paymentTermNM: this.paymentTermNM,
                            deliveryTermID: this.deliveryTermID,
                            deliveryTermNM: this.deliveryTermNM,

                            //description of product item
                            eCommercialInvoiceSampleDetailDescriptions: []
                        };

                        //add description line
                        if (productItem.clientArticleCode !== null && productItem.clientArticleCode !== '') {
                            productItem.eCommercialInvoiceSampleDetailDescriptions.push({
                                eCommercialInvoiceSampleDetailDescriptionID: $scope.method.getNewID(),
                                rowIndex: 1,
                                description: 'CLIENT ARTICLE CODE:' + productItem.clientArticleCode
                            });
                        }
                        if (productItem.clientArticleName !== null && productItem.clientArticleName !== '') {
                            productItem.eCommercialInvoiceSampleDetailDescriptions.push({
                                eCommercialInvoiceSampleDetailDescriptionID: $scope.method.getNewID(),
                                rowIndex: 2,
                                description: 'CLIENT ATICLE NAME:' + productItem.clientArticleName
                            });
                        }
                        if (productItem.clientOrderNumber !== null && productItem.clientOrderNumber !== '') {
                            productItem.eCommercialInvoiceSampleDetailDescriptions.push({
                                eCommercialInvoiceSampleDetailDescriptionID: $scope.method.getNewID(),
                                rowIndex: 3,
                                description: 'CLIENT ORDER NUMBER:' + productItem.clientOrderNumber
                            });
                        }
                        if (productItem.clientEANCode !== null && productItem.clientEANCode !== '') {
                            productItem.eCommercialInvoiceSampleDetailDescriptions.push({
                                eCommercialInvoiceSampleDetailDescriptionID: $scope.method.getNewID(),
                                rowIndex: 4,
                                description: 'CLIENT EAN CODE:' + productItem.clientEANCode
                            });
                        }
                        if (productItem.hsCode !== null && productItem.hsCode !== '') {
                            productItem.eCommercialInvoiceSampleDetailDescriptions.push({
                                eCommercialInvoiceSampleDetailDescriptionID: $scope.method.getNewID(),
                                rowIndex: 5,
                                description: 'HS Code:' + productItem.hsCode
                            });
                        }
                        $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSampleDetails.push(productItem);
                    }
                    max_id = max_id - 1;
                });


                //run script format currency
                setTimeout(function () { runAllForms() }, 200);
                //hide form
                jsHelper.hidePopup('popupForm', function () { });
            },
            cancel: function ($event) {
                $event.preventDefault();
                jsHelper.hidePopup('popupForm', function () { });
            },

            selectedPurchasingInvoice: function (purchasingInvoiceID) {
                var arr_selected = $scope.popupForm.purchasingInvoiceSearchResult.filter(function (o) { return o.purchasingInvoiceID == purchasingInvoiceID });
                var arr_unselected = $scope.popupForm.purchasingInvoiceSearchResult.filter(function (o) { return o.purchasingInvoiceID != purchasingInvoiceID });

                //product
                $(arr_selected[0].purchasingInvoiceDetails).each(function () {
                    this.isSelected = true;
                    $("#chk-" + this.purchasingInvoiceDetailID).removeAttr('disabled');
                });

                for (i = 0; i < arr_unselected.length; i++) {
                    $(arr_unselected[i].purchasingInvoiceDetails).each(function () {
                        this.isSelected = false;
                        $("#chk-" + this.purchasingInvoiceDetailID).attr("disabled", "disabled");
                    });
                }
                //sparepart
                $(arr_selected[0].purchasingInvoiceSparepartDetails).each(function () {
                    this.isSelected = true;
                    $("#chk-sparepart-" + this.purchasingInvoiceSparepartDetailID).removeAttr('disabled');
                });

                for (i = 0; i < arr_unselected.length; i++) {
                    $(arr_unselected[i].purchasingInvoiceSparepartDetails).each(function () {
                        this.isSelected = false;
                        $("#chk-sparepart-" + this.purchasingInvoiceSparepartDetailID).attr("disabled", "disabled");
                    });
                }

                //product
                $(arr_selected[0].purchasingInvoiceSampleDetails).each(function () {
                    this.isSelected = true;
                    $("#chk-sample-" + this.purchasingInvoiceSampleDetailID).removeAttr('disabled');
                });

                for (i = 0; i < arr_unselected.length; i++) {
                    $(arr_unselected[i].purchasingInvoiceDetails).each(function () {
                        this.isSelected = false;
                        $("chk-sample-" + this.purchasingInvoiceSampleDetailID).attr("disabled", "disabled");
                    });
                }
            }
        },

        method: {
            search_purchasingInvoice: function (clientID) {
                eCommercialInvoiceService.search_purchasingInvoice(clientID,
                    function (data) {
                        $scope.popupForm.purchasingInvoiceSearchResult = data.data;
                        $scope.$apply();
                        //show popup form
                        jsHelper.showPopup('popupForm', function () {
                            if ($scope.popupForm.goodsType == 'product') {
                                jQuery(".add_product").show();
                                jQuery(".add_sparepart").hide();
                                jQuery(".add_sample").hide();
                            }
                            else if ($scope.popupForm.goodsType == 'sparepart') {
                                jQuery(".add_product").hide();
                                jQuery(".add_sample").hide();
                                jQuery(".add_sparepart").show();
                            } else if ($scope.popupForm.goodsType == 'sample') {
                                jQuery(".add_product").hide();
                                jQuery(".add_sample").show();
                                jQuery(".add_sparepart").hide();
                            }
                        });
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.$apply();
                    }
                );
            },

        }
    }

    //add warehouse product form
    $scope.addWarehouseProductForm = {

        inputData: {
            clientID: 0
        },

        outputData: null,

        event: {
            load: function () {
                $scope.addWarehouseProductForm.method.search_warehousePickingList($scope.addWarehouseProductForm.inputData.clientID)
            },
            ok: function ($event) {
                $event.preventDefault();

                $scope.ecommercialinvoice.eCommercialInvoiceData.warehouseInvoiceProductDetails = [];
                $scope.ecommercialinvoice.eCommercialInvoiceData.warehouseInvoiceSparepartDetails = [];

                max_id = -1;
                var arr = $scope.addWarehouseProductForm.outputData.filter(function (o) { return o.isSelected == true });
                $(arr).each(function () {
                    for (i = 0; i < this.warehousePickingListDetails.length; i++) {
                        if (this.warehousePickingListDetails[i].productType == 'PRODUCT') {
                            $scope.ecommercialinvoice.eCommercialInvoiceData.warehouseInvoiceProductDetails.push({
                                warehouseInvoiceProductDetailID: max_id,
                                productID: this.warehousePickingListDetails[i].goodsID,
                                quantity: this.warehousePickingListDetails[i].quantity,
                                unitPrice: this.warehousePickingListDetails[i].unitPrice,
                                purchasingPrice: this.warehousePickingListDetails[i].purchasingPrice,
                                factoryID: this.warehousePickingListDetails[i].factoryID,
                                factoryUD: this.warehousePickingListDetails[i].factoryUD,
                                articleCode: this.warehousePickingListDetails[i].articleCode,
                                description: this.warehousePickingListDetails[i].description,
                                proformaInvoiceNo: this.warehousePickingListDetails[i].proformaInvoiceNo,
                                warehousePickingListProductDetailID: this.warehousePickingListDetails[i].warehousePickingListDetailID,
                                cmrNo: this.cmrNo,
                                productType: this.warehousePickingListDetails[i].productType,

                                currency: this.warehousePickingListDetails[i].currency,
                                vatRate: this.warehousePickingListDetails[i].vat,
                                conditions: this.warehousePickingListDetails[i].conditions,
                                paymentTermID: this.warehousePickingListDetails[i].paymentTermID,
                                paymentTermNM: this.warehousePickingListDetails[i].paymentTermNM,
                                deliveryTermID: this.warehousePickingListDetails[i].deliveryTermID,
                                deliveryTermNM: this.warehousePickingListDetails[i].deliveryTermNM,
                            });
                        }
                        else if (this.warehousePickingListDetails[i].productType == 'SPAREPART') {
                            $scope.ecommercialinvoice.eCommercialInvoiceData.warehouseInvoiceSparepartDetails.push({
                                warehouseInvoiceSparepartDetailID: max_id,
                                sparepartID: this.warehousePickingListDetails[i].goodsID,
                                quantity: this.warehousePickingListDetails[i].quantity,
                                unitPrice: this.warehousePickingListDetails[i].unitPrice,
                                purchasingPrice: this.warehousePickingListDetails[i].purchasingPrice,
                                factoryID: this.warehousePickingListDetails[i].factoryID,
                                factoryUD: this.warehousePickingListDetails[i].factoryUD,
                                articleCode: this.warehousePickingListDetails[i].articleCode,
                                description: this.warehousePickingListDetails[i].description,
                                proformaInvoiceNo: this.warehousePickingListDetails[i].proformaInvoiceNo,
                                warehousePickingListSparepartDetailID: this.warehousePickingListDetails[i].warehousePickingListDetailID,
                                cmrNo: this.cmrNo,
                                productType: this.warehousePickingListDetails[i].productType,

                                currency: this.warehousePickingListDetails[i].currency,
                                vatRate: this.warehousePickingListDetails[i].vat,
                                conditions: this.warehousePickingListDetails[i].conditions,
                                paymentTermID: this.warehousePickingListDetails[i].paymentTermID,
                                paymentTermNM: this.warehousePickingListDetails[i].paymentTermNM,
                                deliveryTermID: this.warehousePickingListDetails[i].deliveryTermID,
                                deliveryTermNM: this.warehousePickingListDetails[i].deliveryTermNM,
                            });
                        }

                        max_id = max_id - 1;
                    }
                });

                //set currency,vat, conditions... for ecommercial invoice
                var sub_info = null;
                if ($scope.ecommercialinvoice.eCommercialInvoiceData.warehouseInvoiceProductDetails.length > 0) {
                    sub_info = $scope.ecommercialinvoice.eCommercialInvoiceData.warehouseInvoiceProductDetails[0];
                }
                else if ($scope.ecommercialinvoice.eCommercialInvoiceData.warehouseInvoiceSparepartDetails.length > 0) {
                    sub_info = $scope.ecommercialinvoice.eCommercialInvoiceData.warehouseInvoiceSparepartDetails[0];
                }
                if (sub_info != null) {
                    $scope.ecommercialinvoice.eCommercialInvoiceData.currency = sub_info.currency;
                    $scope.ecommercialinvoice.eCommercialInvoiceData.vatRate = sub_info.vat;
                    $scope.ecommercialinvoice.eCommercialInvoiceData.conditions = sub_info.conditions;
                    $scope.ecommercialinvoice.eCommercialInvoiceData.paymentTermID = sub_info.paymentTermID;
                    $scope.ecommercialinvoice.eCommercialInvoiceData.paymentTerm = sub_info.paymentTermNM;
                    $scope.ecommercialinvoice.eCommercialInvoiceData.deliveryTermID = sub_info.deliveryTermID;
                    $scope.ecommercialinvoice.eCommercialInvoiceData.deliveryTerm = sub_info.deliveryTermNM;
                }
                //$scope.$apply();
                jsHelper.hidePopup('addWarehouseProductForm', function () { });
            },
            cancel: function ($event) {
                $event.preventDefault();
                jsHelper.hidePopup('addWarehouseProductForm', function () { });
            }
        },

        method: {
            search_warehousePickingList: function (clientID) {
                eCommercialInvoiceService.search_warehousePickingList(clientID,
                    function (data) {
                        $scope.addWarehouseProductForm.outputData = data.data;
                        $scope.$apply();
                        //show popup form
                        jsHelper.showPopup('addWarehouseProductForm', function () { });
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.$apply();
                    }
                );
            },

        }
    }

    //detail description form
    $scope.detailDesciprtionForm = {
        event: {
            load: function (eCommercialInvoiceDetailID) {
                $("#" + eCommercialInvoiceDetailID).toggle();
                if ($("#icon-detail-description-" + eCommercialInvoiceDetailID).hasClass('fa-plus-square-o')) {
                    $("#icon-detail-description-" + eCommercialInvoiceDetailID).removeClass('fa-plus-square-o');
                    $("#icon-detail-description-" + eCommercialInvoiceDetailID).addClass('fa-minus-square-o');
                }
                else if ($("#icon-detail-description-" + eCommercialInvoiceDetailID).hasClass('fa-minus-square-o')) {
                    $("#icon-detail-description-" + eCommercialInvoiceDetailID).removeClass('fa-minus-square-o');
                    $("#icon-detail-description-" + eCommercialInvoiceDetailID).addClass('fa-plus-square-o');
                }
            },

            add: function ($event, eCommercialInvoiceDetailID) {
                $event.preventDefault();
                $scope.detailDesciprtionForm.method.add(eCommercialInvoiceDetailID);
            },

            addClientInfo: function ($event, eCommercialInvoiceDetailID) {
                $event.preventDefault();
                $scope.detailDesciprtionForm.method.addClientInfo(eCommercialInvoiceDetailID);
            },

            remove: function ($event, eCommercialInvoiceDetailID, eCommercialInvoiceDetailDescriptionID) {
                $event.preventDefault();
                $scope.detailDesciprtionForm.method.remove(eCommercialInvoiceDetailID, eCommercialInvoiceDetailDescriptionID);
            },

            insert: function ($event, eCommercialInvoiceDetailID, rowIndex) {
                $event.preventDefault();
                $scope.detailDesciprtionForm.method.insert(eCommercialInvoiceDetailID, rowIndex);
            }
        },
        method: {
            add: function (eCommercialInvoiceDetailID) {
                var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDetails.filter(function (o) { return o.eCommercialInvoiceDetailID == eCommercialInvoiceDetailID });
                if (arr[0].eCommercialInvoiceDetailDescriptions == null) {
                    arr[0].eCommercialInvoiceDetailDescriptions = [];
                }
                var arr_detaildescription = arr[0].eCommercialInvoiceDetailDescriptions;

                max_rowIndex = 1;
                if (arr_detaildescription.length > 0) {
                    arr_detaildescription.sort(function (a, b) { return b.rowIndex - a.rowIndex });
                    max_rowIndex = arr_detaildescription[0].rowIndex + 1;
                }
                arr_detaildescription.push({
                    eCommercialInvoiceDetailDescriptionID: max_rowIndex * (-1),
                    rowIndex: max_rowIndex
                });
            },

            addClientInfo: function (eCommercialInvoiceDetailID) {
                var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDetails.filter(function (o) { return o.eCommercialInvoiceDetailID == eCommercialInvoiceDetailID });
                if (arr[0].eCommercialInvoiceDetailDescriptions == null) {
                    arr[0].eCommercialInvoiceDetailDescriptions = [];
                }
                var arr_detaildescription = arr[0].eCommercialInvoiceDetailDescriptions;

                max_rowIndex = 1;
                if (arr_detaildescription.length > 0) {
                    arr_detaildescription.sort(function (a, b) { return b.rowIndex - a.rowIndex });
                    max_rowIndex = arr_detaildescription[0].rowIndex + 1;
                }

                //insert client info
                for (var i = 1; i <= 5; i++) {
                    var insertvalue = '';
                    switch (i) {
                        case 1:
                            if (arr[0].clientArticleCode !== null && arr[0].clientArticleCode !== '')
                                insertvalue = 'CLIENT ARTICLE CODE : ' + arr[0].clientArticleCode;
                            break;
                        case 2:
                            if (arr[0].clientArticleName !== null && arr[0].clientArticleName !== '')
                                insertvalue = 'CLIENT ATICLE NAME : ' + arr[0].clientArticleName;
                            break;
                        case 3:
                            if (arr[0].clientOrderNumber !== null && arr[0].clientOrderNumber !== '')
                                insertvalue = 'CLIENT ORDER NUMBER : ' + arr[0].clientOrderNumber;
                            break;
                        case 4:
                            if (arr[0].clientEANCode !== null && arr[0].clientEANCode !== '')
                                insertvalue = 'CLIENT EAN CODE : ' + arr[0].clientEANCode;
                            break;
                        case 5:
                            if (arr[0].hsCode !== null && arr[0].hsCode !== '')
                                insertvalue = 'HS Code : ' + arr[0].hsCode;
                            break;
                    }
                    if (insertvalue !== '') {
                        arr_detaildescription.push({
                            eCommercialInvoiceDetailDescriptionID: -1 * max_rowIndex,
                            rowIndex: max_rowIndex,
                            description: insertvalue
                        });
                    }
                    max_rowIndex++;
                }
            },

            remove: function (eCommercialInvoiceDetailID, eCommercialInvoiceDetailDescriptionID) {
                var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDetails.filter(function (o) { return o.eCommercialInvoiceDetailID == eCommercialInvoiceDetailID });
                var arr_detaildescription = arr[0].eCommercialInvoiceDetailDescriptions;

                isExist = false;
                for (j = 0; j < arr_detaildescription.length; j++) {
                    if (arr_detaildescription[j].eCommercialInvoiceDetailDescriptionID == eCommercialInvoiceDetailDescriptionID) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    arr_detaildescription.splice(j, 1);
                }
            },

            insert: function (eCommercialInvoiceDetailID, rowIndex) {
                var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDetails.filter(function (o) { return o.eCommercialInvoiceDetailID == eCommercialInvoiceDetailID });
                if (arr[0].eCommercialInvoiceDetailDescriptions == null) {
                    arr[0].eCommercialInvoiceDetailDescriptions = [];
                }
                var arr_detaildescription = arr[0].eCommercialInvoiceDetailDescriptions;

                for (i = 0; i < arr_detaildescription.length; i++) {
                    if (arr_detaildescription[i].rowIndex > rowIndex) {
                        arr_detaildescription[i].rowIndex = arr_detaildescription[i].rowIndex + 1;
                        if (arr_detaildescription[i].eCommercialInvoiceDetailDescriptionID < 0) {
                            arr_detaildescription[i].eCommercialInvoiceDetailDescriptionID = arr_detaildescription[i].eCommercialInvoiceDetailDescriptionID - 1;
                        }
                    }
                }
                arr_detaildescription.push({
                    eCommercialInvoiceDetailDescriptionID: (rowIndex + 1) * (-1),
                    rowIndex: rowIndex + 1,
                });
            }
        }

    },

    //detail Sparepart description form
    $scope.sparepartdetailDesciprtionForm = {
        event: {
            load: function (eCommercialInvoiceSparepartDetailID) {
                $("#" + eCommercialInvoiceSparepartDetailID).toggle();
                if ($("#icon-detail-description-" + eCommercialInvoiceSparepartDetailID).hasClass('fa-plus-square-o')) {
                    $("#icon-detail-description-" + eCommercialInvoiceSparepartDetailID).removeClass('fa-plus-square-o');
                    $("#icon-detail-description-" + eCommercialInvoiceSparepartDetailID).addClass('fa-minus-square-o');
                }
                else if ($("#icon-detail-description-" + eCommercialInvoiceSparepartDetailID).hasClass('fa-minus-square-o')) {
                    $("#icon-detail-description-" + eCommercialInvoiceSparepartDetailID).removeClass('fa-minus-square-o');
                    $("#icon-detail-description-" + eCommercialInvoiceSparepartDetailID).addClass('fa-plus-square-o');
                }
            },

            add: function ($event, eCommercialInvoiceSparepartDetailID) {
                $event.preventDefault();
                $scope.sparepartdetailDesciprtionForm.method.add(eCommercialInvoiceSparepartDetailID);
            },

            addClientInfo: function ($event, eCommercialInvoiceSparepartDetailID) {
                $event.preventDefault();
                $scope.sparepartdetailDesciprtionForm.method.addClientInfo(eCommercialInvoiceSparepartDetailID);
            },

            remove: function ($event, eCommercialInvoiceSparepartDetailID, eCommercialInvoiceSparepartDetailDescriptionID) {
                $event.preventDefault();
                $scope.sparepartdetailDesciprtionForm.method.remove(eCommercialInvoiceSparepartDetailID, eCommercialInvoiceSparepartDetailDescriptionID);
            },

            insert: function ($event, eCommercialInvoiceSparepartDetailID, rowIndex) {
                $event.preventDefault();
                $scope.sparepartdetailDesciprtionForm.method.insert(eCommercialInvoiceSparepartDetailID, rowIndex);
            }
        },
        method: {
            add: function (eCommercialInvoiceSparepartDetailID) {
                var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSparepartDetails.filter(function (o) { return o.eCommercialInvoiceSparepartDetailID === eCommercialInvoiceSparepartDetailID });
                if (arr[0].eCommercialInvoiceSparepartDetailDescriptions === null) {
                    arr[0].eCommercialInvoiceSparepartDetailDescriptions = [];
                }
                var arr_detaildescription = arr[0].eCommercialInvoiceSparepartDetailDescriptions;

                max_rowIndex = 1;
                if (arr_detaildescription.length > 0) {
                    arr_detaildescription.sort(function (a, b) { return b.rowIndex - a.rowIndex });
                    max_rowIndex = arr_detaildescription[0].rowIndex + 1;
                }
                arr_detaildescription.push({
                    eCommercialInvoiceSparepartDetailDescriptionID: max_rowIndex * (-1),
                    rowIndex: max_rowIndex
                });
            },

            addClientInfo: function (eCommercialInvoiceSparepartDetailID) {
                var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSparepartDetails.filter(function (o) { return o.eCommercialInvoiceSparepartDetailID === eCommercialInvoiceSparepartDetailID });
                if (arr[0].eCommercialInvoiceSparepartDetailDescriptions === null) {
                    arr[0].eCommercialInvoiceSparepartDetailDescriptions = [];
                }
                var arr_detaildescription = arr[0].eCommercialInvoiceSparepartDetailDescriptions;

                max_rowIndex = 1;
                if (arr_detaildescription.length > 0) {
                    arr_detaildescription.sort(function (a, b) { return b.rowIndex - a.rowIndex });
                    max_rowIndex = arr_detaildescription[0].rowIndex + 1;
                }

                //insert client info
                for (var i = 1; i <= 5; i++) {
                    var insertvalue = '';
                    switch (i) {
                        case 1:
                            if (arr[0].clientArticleCode !== null && arr[0].clientArticleCode !== '')
                                insertvalue = 'CLIENT ARTICLE CODE : ' + arr[0].clientArticleCode;
                            break;
                        case 2:
                            if (arr[0].clientArticleName !== null && arr[0].clientArticleName !== '')
                                insertvalue = 'CLIENT ATICLE NAME : ' + arr[0].clientArticleName;
                            break;
                        case 3:
                            if (arr[0].clientOrderNumber !== null && arr[0].clientOrderNumber !== '')
                                insertvalue = 'CLIENT ORDER NUMBER : ' + arr[0].clientOrderNumber;
                            break;
                        case 4:
                            if (arr[0].clientEANCode !== null && arr[0].clientEANCode !== '')
                                insertvalue = 'CLIENT EAN CODE : ' + arr[0].clientEANCode;
                            break;
                        case 5:
                            if (arr[0].hsCode !== null && arr[0].hsCode !== '')
                                insertvalue = 'HS Code : ' + arr[0].hsCode;
                            break;
                    }
                    if (insertvalue !== '') {
                        arr_detaildescription.push({
                            eCommercialInvoiceSparepartDetailDescriptionID: -1 * max_rowIndex,
                            rowIndex: max_rowIndex,
                            description: insertvalue
                        });
                    }
                    max_rowIndex++;
                }
            },

            remove: function (eCommercialInvoiceSparepartDetailID, eCommercialInvoiceSparepartDetailDescriptionID) {
                var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSparepartDetails.filter(function (o) { return o.eCommercialInvoiceSparepartDetailID == eCommercialInvoiceSparepartDetailID });
                var arr_detaildescription = arr[0].eCommercialInvoiceSparepartDetailDescriptions;

                isExist = false;
                for (j = 0; j < arr_detaildescription.length; j++) {
                    if (arr_detaildescription[j].eCommercialInvoiceSparepartDetailDescriptionID === eCommercialInvoiceSparepartDetailDescriptionID) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    arr_detaildescription.splice(j, 1);
                }
            },

            insert: function (eCommercialInvoiceSparepartDetailID, rowIndex) {
                var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSparepartDetails.filter(function (o) { return o.eCommercialInvoiceSparepartDetailID === eCommercialInvoiceSparepartDetailID });
                if (arr[0].eCommercialInvoiceSparepartDetailDescriptions === null) {
                    arr[0].eCommercialInvoiceSparepartDetailDescriptions = [];
                }
                var arr_detaildescription = arr[0].eCommercialInvoiceSparepartDetailDescriptions;

                for (i = 0; i < arr_detaildescription.length; i++) {
                    if (arr_detaildescription[i].rowIndex > rowIndex) {
                        arr_detaildescription[i].rowIndex = arr_detaildescription[i].rowIndex + 1;
                        if (arr_detaildescription[i].eCommercialInvoiceSparepartDetailDescriptionID < 0) {
                            arr_detaildescription[i].eCommercialInvoiceSparepartDetailDescriptionID = arr_detaildescription[i].eCommercialInvoiceSparepartDetailDescriptionID - 1;
                        }
                    }
                }
                arr_detaildescription.push({
                    eCommercialInvoiceSparepartDetailDescriptionID: (rowIndex + 1) * (-1),
                    rowIndex: rowIndex + 1
                });
            }
        }

    },

    $scope.detailSampleDesciprtionForm = {
        event: {
            load: function (eCommercialInvoiceSampleDetailID) {
                $("#" + eCommercialInvoiceSampleDetailID).toggle();
                if ($("#icon-detail-description-" + eCommercialInvoiceSampleDetailID).hasClass('fa-plus-square-o')) {
                    $("#icon-detail-description-" + eCommercialInvoiceSampleDetailID).removeClass('fa-plus-square-o');
                    $("#icon-detail-description-" + eCommercialInvoiceSampleDetailID).addClass('fa-minus-square-o');
                }
                else if ($("#icon-detail-description-" + eCommercialInvoiceSampleDetailID).hasClass('fa-minus-square-o')) {
                    $("#icon-detail-description-" + eCommercialInvoiceSampleDetailID).removeClass('fa-minus-square-o');
                    $("#icon-detail-description-" + eCommercialInvoiceSampleDetailID).addClass('fa-plus-square-o');
                }
            },

            add: function ($event, eCommercialInvoiceSampleDetailID) {
                $event.preventDefault();
                $scope.detailSampleDesciprtionForm.method.add(eCommercialInvoiceSampleDetailID);
            },

            addClientInfo: function ($event, eCommercialInvoiceSampleDetailID) {
                $event.preventDefault();
                $scope.detailSampleDesciprtionForm.method.addClientInfo(eCommercialInvoiceSampleDetailID);
            },

            remove: function ($event, eCommercialInvoiceSampleDetailID, eCommercialInvoiceSampleDetailDescriptionID) {
                $event.preventDefault();
                $scope.detailSampleDesciprtionForm.method.remove(eCommercialInvoiceSampleDetailID, eCommercialInvoiceSampleDetailDescriptionID);
            },

            insert: function ($event, eCommercialInvoiceSampleDetailID, rowIndex) {
                $event.preventDefault();
                $scope.detailSampleDesciprtionForm.method.insert(eCommercialInvoiceSampleDetailID, rowIndex);
            }
        },
        method: {
            add: function (eCommercialInvoiceSampleDetailID) {
                var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSampleDetails.filter(function (o) { return o.eCommercialInvoiceSampleDetailID == eCommercialInvoiceSampleDetailID });
                if (arr[0].eCommercialInvoiceSampleDetailDescriptions == null) {
                    arr[0].eCommercialInvoiceSampleDetailDescriptions = [];
                }
                var arr_detaildescription = arr[0].eCommercialInvoiceSampleDetailDescriptions;

                max_rowIndex = 1;
                if (arr_detaildescription.length > 0) {
                    arr_detaildescription.sort(function (a, b) { return b.rowIndex - a.rowIndex });
                    max_rowIndex = arr_detaildescription[0].rowIndex + 1;
                }
                arr_detaildescription.push({
                    eCommercialInvoiceSampleDetailDescriptionID: max_rowIndex * (-1),
                    rowIndex: max_rowIndex
                });
            },

            addClientInfo: function (eCommercialInvoiceSampleDetailID) {
                var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSampleDetails.filter(function (o) { return o.eCommercialInvoiceSampleDetailID == eCommercialInvoiceSampleDetailID });
                if (arr[0].eCommercialInvoiceSampleDetailDescriptions == null) {
                    arr[0].eCommercialInvoiceSampleDetailDescriptions = [];
                }
                var arr_detaildescription = arr[0].eCommercialInvoiceSampleDetailDescriptions;

                max_rowIndex = 1;
                if (arr_detaildescription.length > 0) {
                    arr_detaildescription.sort(function (a, b) { return b.rowIndex - a.rowIndex });
                    max_rowIndex = arr_detaildescription[0].rowIndex + 1;
                }

                //insert client info
                for (var i = 1; i <= 5; i++) {
                    var insertvalue = '';
                    switch (i) {
                        case 1:
                            if (arr[0].clientArticleCode !== null && arr[0].clientArticleCode !== '')
                                insertvalue = 'CLIENT ARTICLE CODE : ' + arr[0].clientArticleCode;
                            break;
                        case 2:
                            if (arr[0].clientArticleName !== null && arr[0].clientArticleName !== '')
                                insertvalue = 'CLIENT ATICLE NAME : ' + arr[0].clientArticleName;
                            break;
                        case 3:
                            if (arr[0].clientOrderNumber !== null && arr[0].clientOrderNumber !== '')
                                insertvalue = 'CLIENT ORDER NUMBER : ' + arr[0].clientOrderNumber;
                            break;
                        case 4:
                            if (arr[0].clientEANCode !== null && arr[0].clientEANCode !== '')
                                insertvalue = 'CLIENT EAN CODE : ' + arr[0].clientEANCode;
                            break;
                        case 5:
                            if (arr[0].hsCode !== null && arr[0].hsCode !== '')
                                insertvalue = 'HS Code : ' + arr[0].hsCode;
                            break;
                    }
                    if (insertvalue !== '') {
                        arr_detaildescription.push({
                            eCommercialInvoiceSampleDetailDescriptionID: -1 * max_rowIndex,
                            rowIndex: max_rowIndex,
                            description: insertvalue
                        });
                    }
                    max_rowIndex++;
                }
            },

            remove: function (eCommercialInvoiceSampleDetailID, eCommercialInvoiceSampleDetailDescriptionID) {
                var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSampleDetails.filter(function (o) { return o.eCommercialInvoiceSampleDetailID == eCommercialInvoiceSampleDetailID });
                var arr_detaildescription = arr[0].eCommercialInvoiceSampleDetailDescriptions;

                isExist = false;
                for (j = 0; j < arr_detaildescription.length; j++) {
                    if (arr_detaildescription[j].eCommercialInvoiceSampleDetailDescriptionID == eCommercialInvoiceSampleDetailDescriptionID) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    arr_detaildescription.splice(j, 1);
                }
            },

            insert: function (eCommercialInvoiceSampleDetailID, rowIndex) {
                var arr = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceSampleDetails.filter(function (o) { return o.eCommercialInvoiceSampleDetailID == eCommercialInvoiceSampleDetailID });
                if (arr[0].eCommercialInvoiceSampleDetailDescriptions == null) {
                    arr[0].eCommercialInvoiceSampleDetailDescriptions = [];
                }
                var arr_detaildescription = arr[0].eCommercialInvoiceSampleDetailDescriptions;

                for (i = 0; i < arr_detaildescription.length; i++) {
                    if (arr_detaildescription[i].rowIndex > rowIndex) {
                        arr_detaildescription[i].rowIndex = arr_detaildescription[i].rowIndex + 1;
                        if (arr_detaildescription[i].eCommercialInvoiceSampleDetailDescriptionID < 0) {
                            arr_detaildescription[i].eCommercialInvoiceSampleDetailDescriptionID = arr_detaildescription[i].eCommercialInvoiceSampleDetailDescriptionID - 1;
                        }
                    }
                }
                arr_detaildescription.push({
                    eCommercialInvoiceSampleDetailDescriptionID: (rowIndex + 1) * (-1),
                    rowIndex: rowIndex + 1,
                });
            }
        }

    },

    //add warehouse product form
    $scope.returnGoodsForm = {
        inputData: {
            eCommercialInvoiceID: -1
        },
        returnGoods: null,

        invoiceDetailProducts: [],
        productSetEANCodes: [],

        event: {
            load: function ($event) {
                $event.preventDefault();
                $scope.returnGoodsForm.inputData.eCommercialInvoiceID = $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceID;
                $scope.returnGoodsForm.method.getReturnGoods();
            },
            ok: function ($event) {
                $event.preventDefault();
                $scope.returnGoodsForm.method.returnGoods();
            },
            cancel: function ($event) {
                $event.preventDefault();
                jsHelper.hidePopup('returnGoodsForm', function () { });
            },
        },

        method: {
            getReturnGoods: function () {
                eCommercialInvoiceService.getReturnGoods($scope.returnGoodsForm.inputData.eCommercialInvoiceID,
                    function (data) {
                        $scope.returnGoodsForm.returnGoods = data.data;

                        angular.forEach($scope.returnGoodsForm.returnGoods.returnProducts, function (item) {
                            if (item.productColliID > 0) item.isSelected = true;
                        })
                        angular.forEach($scope.returnGoodsForm.returnGoods.returnSpareparts, function (item) {
                            item.isSelected = true;
                        })

                        //register watch change value
                        $scope.$watch(function ($scope) {
                            return $scope.returnGoodsForm.invoiceDetailProducts.map(function (item) {
                                return { 'returnQnt': item.returnQnt, 'eCommercialInvoiceDetailID': item.eCommercialInvoiceDetailID, 'remaingReturnQnt': item.remaingReturnQnt, 'productID': item.productID };
                            });
                        },
                            function (newValArray, oldValArray) {
                                for (i = 0; i < newValArray.length; i++) {
                                    if (newValArray[i].returnQnt != oldValArray[i].returnQnt) {

                                        if (newValArray[i].returnQnt > oldValArray[i].remaingReturnQnt) {
                                            var x = $scope.returnGoodsForm.invoiceDetailProducts.filter(function (o) { return o.eCommercialInvoiceDetailID == newValArray[i].eCommercialInvoiceDetailID });
                                            x[0].returnQnt = oldValArray[i].returnQnt;
                                            bootbox.alert('Import quanity must be less than remaining quantity!');
                                            return;
                                        }
                                        //hooking return quantity
                                        angular.forEach($scope.returnGoodsForm.returnGoods.returnProducts, function (item) {
                                            if (item.eCommercialInvoiceDetailID == newValArray[i].eCommercialInvoiceDetailID) {
                                                item.returnQnt = newValArray[i].returnQnt;
                                                item.returnColliQnt = newValArray[i].returnQnt;
                                                //un-select if quantity = 0
                                                item.isSelected = (newValArray[i].returnQnt > 0 && item.productColliID > 0);
                                            }
                                        });
                                    }
                                }
                            }, true);

                        //get invoiceDetailProducts
                        var indexs = [];
                        $scope.returnGoodsForm.invoiceDetailProducts = [];
                        angular.forEach($scope.returnGoodsForm.returnGoods.returnProducts, function (value, key) {
                            if (indexs.indexOf(value.eCommercialInvoiceDetailID) < 0) {
                                indexs.push(value.eCommercialInvoiceDetailID);
                                $scope.returnGoodsForm.invoiceDetailProducts.push({
                                    eCommercialInvoiceDetailID: value.eCommercialInvoiceDetailID
                                    , productID: value.productID
                                    , articleCode: value.articleCode
                                    , description: value.description
                                    , remaingReturnQnt: value.remaingReturnQnt
                                    , returnQnt: value.returnQnt
                                    , containerNo: value.containerNo
                                    , clientUD: value.clientUD
                                    , orderType: value.orderType
                                    , factoryOrderDetailID: value.factoryOrderDetailID
                                    , factoryOrderID: value.factoryOrderID
                                    , saleOrderID: value.saleOrderID
                                });
                            }
                        }, null);

                        //get productSetEANCodes
                        indexs = [];
                        $scope.returnGoodsForm.productSetEANCodes = [];
                        angular.forEach($scope.returnGoodsForm.returnGoods.returnProducts, function (value, key) {
                            keyID = value.eCommercialInvoiceDetailID.toString() + '_' + value.productSetEANCodeID.toString();
                            if (indexs.indexOf(keyID) < 0) {
                                indexs.push(keyID);
                                $scope.returnGoodsForm.productSetEANCodes.push({
                                    eCommercialInvoiceDetailID: value.eCommercialInvoiceDetailID
                                    , productSetEANCodeID: value.productSetEANCodeID
                                    , productEANCode: value.productEANCode
                                });
                            }
                        }, null);

                        //apply data
                        $scope.$apply();

                        //show popup form
                        jsHelper.showPopup('returnGoodsForm', function () { });
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.$apply();
                    }
                );
            },

            returnGoods: function () {
                eCommercialInvoiceService.returnGoods($scope.returnGoodsForm.returnGoods,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type != 2) {
                            //reload invoice
                            window.location = '@Url.Action("Edit", "ECommercialInvoice", new { id = UrlParameter.Optional })/' + $scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceID;

                            //redirect to warehouse import receipt
                            window.open('@Url.Action("Return", "WarehouseImport", new { id = UrlParameter.Optional })/' + data.data, '');
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.$apply();
                    }
                );
            },
        }
    }

    $scope.method = {

        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },

        getPurchasingQuantity: function (eCommercialInvoiceDetailID, purchasingQnts) {
            for (var i = 0; i < purchasingQnts.length; i++) {
                var purchasingItem = purchasingQnts[i];
                if (purchasingItem.eCommercialInvoiceDetailID === eCommercialInvoiceDetailID) {
                    if (purchasingItem.eCommercialQnt !== purchasingItem.purchasingQnt) {
                        return purchasingItem.purchasingQnt;
                    }
                }
            }

            return null;
        },

        getSaleOrderPrice: function (eCommercialInvoiceDetailID, saleOrderPrices) {
            for (var i = 0; i < saleOrderPrices.length; i++) {
                var saleOrderPrice = saleOrderPrices[i];
                if (saleOrderPrice.eCommercialInvoiceDetailID === eCommercialInvoiceDetailID) {
                    if (saleOrderPrice.eCommercialPrice !== saleOrderPrice.saleOrderPrice) {
                        return saleOrderPrice.saleOrderPrice;
                    }
                }
            }
            return null;
        }
    };

    $scope.getPurchasingQnt = function (id) {

        eCommercialInvoiceService.getPurchasingQnt(
            id,
            function (data) {
                var purchasingQnts = data.data;
                var saleOrderPrices = data.data;
                angular.forEach($scope.ecommercialinvoice.eCommercialInvoiceData.eCommercialInvoiceDetails, function (item) {
                    var qnt = $scope.method.getPurchasingQuantity(item.eCommercialInvoiceDetailID, purchasingQnts);
                    var price = $scope.method.getSaleOrderPrice(item.eCommercialInvoiceDetailID, saleOrderPrices);
                    if (qnt !== null) {
                        item.quantity = qnt;
                    }

                    if (price !== null) {
                        item.unitPrice = price;
                    }
                });

                $scope.$apply();
            },
            function (error) {
                jsHelper.showMessage('warning', error);
                $scope.$apply();
            });
    };

    //load data
    $scope.load();
}]);