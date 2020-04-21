//
// APP START
//
function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.label,
            description: item.value,
            defaultFactoryWarehouse: item.factoryWarehouseID,
            unitID: item.unitID,
            unitNM: item.unitNM
        };
    });
}

function formatFactorySaleQuotation(data) {
    return $.map(data.data, function (item) {
        return {
            description: '',
            id: item.id,
            label: item.label,
            value: item.value,

            factorySaleQuotationID: item.factorySaleQuotationID,
            factorySaleQuotationUD: item.factorySaleQuotationUD,
            factoryRawMaterialID: item.factoryRawMaterialID,
            factoryRawMaterialUD: item.factoryRawMaterialUD,
            factoryRawMaterialOfficialNM: item.factoryRawMaterialOfficialNM,
            factoryRawMaterialDocumentRefNo: item.factoryRawMaterialDocumentRefNo,
            factoryRawMaterialContactPersonNM: item.factoryRawMaterialContactPersonNM,
            factoryShippingMethodID: item.factoryShippingMethodID,
            factoryPaymentTermID: item.factoryPaymentTermID,
            currency: item.currency,
            factorySaleOrderStatusID: item.factorySaleOrderStatusID,
            shippingAddress: item.shippingAddress,
            billingAddress: item.billingAddress,
            employeeNM: item.employeeNM,
            remark: item.remark,
            factorySaleUserID: item.factorySaleUserID,
            factoryRawMaterialShortNM: item.factoryRawMaterialShortNM,
            supplierContactQuickInfoId: item.supplierContactQuickInfoId,
            fullName: item.fullName,
            factorySaleQuotationDetailDTOs: item.factorySaleQuotationDetailDTOs,
            factorySaleQuotationAttachedFileDTOs: item.factorySaleQuotationAttachedFileDTOs
        };
    });
}

(function () {
    'use strict';

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', factorySaleOrderController);
    factorySaleOrderController.$inject = ['$scope', 'dataService', '$timeout'];

    function factorySaleOrderController($scope, factorySaleOrderService, $timeout) {
        factorySaleOrderService.searchFilter.pageSize = context.pageSize;
        factorySaleOrderService.serviceUrl = context.serviceUrl;
        factorySaleOrderService.token = context.token;
        /// validation
        $scope.listError = [];

        //
        // property
        //
        ///
        $scope.factorySaleOrder = null;
        $scope.newID = -1;
        $scope.lstStatus = [];
        $scope.lstTypeOfCurrency = [];
        $scope.lstPaymentTerm = [];
        $scope.lstShipmentTypeOption = [];
        $scope.lstShipmentToOption = [];
        $scope.lstWarehouse = [];
        $scope.supplierContactQuickInfo = [];
        $scope.supplierContact = [];

        //------------

        $scope.lstTaxCode = [{
            'vatId': 5,
            'vatPercent': 5
        },
        {
            'vatId': 10,
            'vatPercent': 10
        }];
        $scope.lstTypeOfCurrency = [
            {
                currencyValue: "VND",
                currencyText: "VND"
            },
            {
                currencyValue: "USD",
                currencyText: "USD"
            },
            {
                currencyValue: "EUR",
                currencyText: "EUR"
            },
        ];
        $scope.warehouseNM = "";
        //-------------
        $scope.unitPrice = 1;
        //--------------
        $scope.currentTaskFile = null;
        $scope.newid = 0;
        //--------------------------------------------
        $scope.filter = {
            factoryRawMaterialUD: null,
            factoryRawMaterialContactPersonNM: null,
            factorySaleUserID: null,
            productionitem: null,
            factoryWarehouseID: null,
        };
        //------------------------------------------------------   
        $scope.totaldiscount = parseFloat(0);
        $scope.ngInitProductionItem = null;
        $scope.ngItemProductionItem = {
            id: null,
            label: null,
            description: null,
            defaultFactoryWarehouse: null,
            unitID: null,
            unitNM: null
        };
        $scope.ngRequestProductionItem = {
            url: factorySaleOrderService.serviceUrl + 'getFilterProductItem',
            token: context.token
        };
        $scope.ngSelectedProductionItem = {
            id: null,
            label: null,
            description: null
        };

        $scope.factorySalesQuotationAPI = {
            url: factorySaleOrderService.serviceUrl + 'getfactorysalesquotation',
            token: context.token
        };
        $scope.factorySalesQuotation = {
            factorySaleQuotationID: null,
            factorySaleQuotationUD: null,
            factoryRawMaterialID: null,
            factoryRawMaterialUD: null,
            factoryRawMaterialOfficialNM: null,
            factoryRawMaterialDocumentRefNo: null,
            factoryRawMaterialContactPersonNM: null,
            factoryShippingMethodID: null,
            factoryPaymentTermID: null,
            currency: null,
            factorySaleOrderStatusID: null,
            shippingAddress: null,
            billingAddress: null,
            employeeNM: null,
            remark: null,
            fullName: null,
            supplierContactQuickInfoId: null,
            factorySaleUserID: null,
            factoryRawMaterialShortNM: null,
            factorySaleQuotationDetailDTOs: [],
            factorySaleQuotationAttachedFileDTOs: []
        };
        //
        // event
        //
        $scope.event = {
            changeCurrency: function (item) {
                $scope.factorySaleQuotation.currency = item.currency;
            },
            ////Add a production item
            ////Add a production item
            addProductionItems: function () {
                var item = {
                    factorySaleQuotationDetailID: $scope.method.getNewID(),
                };

                $scope.factorySaleOrder.lstFactorySaleOrderDetail.push(item);
            },

            calTotalByItem: function (item) {
                var totalDiscount = 0;
                var totalPercent = 0;
                var totalPriceBefore = 0;
                if (item === null) return 0;
                if (item.vatPercent === null && item.discountPercent === null) {
                    return parseFloat(item.quantity) * parseFloat(item.unitPrice);
                }
                else if (item.vatPercent === null) {
                    totalPriceBefore = parseFloat(item.quantity) * parseFloat(item.unitPrice);
                    if (item.discountPercent === null || item.discountPercent === '') {
                        totalDiscount = (parseFloat(item.quantity) * parseFloat(item.unitPrice) * parseFloat(0) / 100);
                    }
                    else {
                        totalDiscount = (parseFloat(item.quantity) * parseFloat(item.unitPrice) * parseFloat(item.discountPercent) / 100);
                    }
                    totalPercent = 0;
                    return totalPriceBefore - totalDiscount;
                }
                else if (item.discountPercent === null) {
                    totalPriceBefore = parseFloat(item.quantity) * parseFloat(item.unitPrice);
                    totalDiscount = 0;
                    totalPercent = (parseFloat(item.quantity) * parseFloat(item.unitPrice) * parseFloat(item.vatPercent) / 100);
                    return totalPriceBefore + totalPercent;
                }
                else {
                    totalPriceBefore = parseFloat(item.quantity) * parseFloat(item.unitPrice);
                    if (item.discountPercent === null || item.discountPercent === '') {
                        totalDiscount = totalPriceBefore - (parseFloat(item.quantity) * parseFloat(item.unitPrice) * parseFloat(0) / 100);
                    }
                    else {
                        totalDiscount = totalPriceBefore - (parseFloat(item.quantity) * parseFloat(item.unitPrice) * parseFloat(item.discountPercent) / 100);
                    }
                    totalPercent = (totalDiscount * parseFloat(item.vatPercent) / 100);
                    return totalDiscount + totalPercent;
                    //return (parseFloat(item.quantity) * parseFloat(item.unitPrice) - (parseFloat(item.quantity) * parseFloat(item.unitPrice) * parseFloat(item.discountPercent) / 100)) + (parseFloat(item.quantity) * parseFloat(item.unitPrice) * parseFloat(item.vatPercent) / 100);
                }
            },
            calTotalAmount: function () {
                var result = parseFloat(0);
                if ($scope.factorySaleOrder === null) {
                    return result;
                }
                if ($scope.factorySaleOrder !== null) {
                    if ($scope.factorySaleOrder.lstFactorySaleOrderDetail.length !== 0) {
                        angular.forEach($scope.factorySaleOrder.lstFactorySaleOrderDetail, function (item) {
                            result += $scope.event.calTotalByItem(item);
                        });
                    }
                }

                return result;
            },
            toTalPriceBeforeDiscount: function () {
                var totalprice = parseFloat(0);
                if ($scope.factorySaleOrder !== null) {
                    if ($scope.factorySaleOrder.lstFactorySaleOrderDetail.length !== 0) {
                        angular.forEach($scope.factorySaleOrder.lstFactorySaleOrderDetail, function (item) {
                            if (item === null || item === undefined || item === '')
                                return totalprice;
                            totalprice += parseFloat(item.unitPrice * item.quantity);
                        });
                    }
                }
                return totalprice;
            },
            toTalPriceDiscount: function () {
                var totalPrice = parseFloat(0);
                var discountPercentTotal = parseFloat(0);
                if ($scope.factorySaleOrder !== null) {
                    if ($scope.factorySaleOrder.lstFactorySaleOrderDetail.length !== 0) {
                        angular.forEach($scope.factorySaleOrder.lstFactorySaleOrderDetail, function (item) {
                            if (item === null)
                                return totalPrice;
                            if (item.discountPercent === null || item.discountPercent === '' || item.discountPercent === undefined) {
                                return totalPrice;
                            }
                            if (item.discountPercent !== null) {
                                discountPercentTotal = 0;
                                if (item.discountPercent === null || item.discountPercent === '') {
                                    discountPercentTotal += 0;
                                }
                                else {
                                    discountPercentTotal += parseFloat(item.discountPercent);
                                }
                                totalPrice += (parseFloat(item.unitPrice * item.quantity) * discountPercentTotal / 100);
                            }
                        });
                    }
                }
                return totalPrice;
            },
            calTax: function () {
                var result = parseFloat(0);
                var totalPriceDiscount = parseFloat(0);
                var totalPriceBeforeDiscount = parseFloat(0);
                var discountPercentTotal = parseFloat(0);
                if ($scope.factorySaleOrder !== null) {
                    if ($scope.factorySaleOrder.lstFactorySaleOrderDetail.length !== 0) {
                        angular.forEach($scope.factorySaleOrder.lstFactorySaleOrderDetail, function (item) {
                            if (item === null || item.vatPercent === null || item.vatPercent === '' || item.vatPercent === undefined) return result;
                            discountPercentTotal = 0;
                            totalPriceDiscount = 0;
                            totalPriceBeforeDiscount = 0;
                            if (item.discountPercent === null || item.discountPercent === '') {
                                discountPercentTotal += 0;
                            }
                            else {
                                discountPercentTotal += parseFloat(item.discountPercent);
                            }
                            totalPriceDiscount += (parseFloat(item.unitPrice * item.quantity) * discountPercentTotal / 100);
                            totalPriceBeforeDiscount += parseFloat(item.unitPrice * item.quantity);
                            result += ((totalPriceBeforeDiscount - totalPriceDiscount) * parseFloat(item.vatPercent) / 100);
                        });
                    }
                }
                return result;
            },

            //
            // Other File
            //
            addOtherFile: function () {
                userFileMng.isMultiSelectActivated = true;
                userFileMng.autoResizeImage = false;
                userFileMng.callback = function () {
                    scope.$apply(function () {
                        var d = new Date();
                        var n = d.getMilliseconds();

                        angular.forEach(userFileMng.selectedFiles, function (value, key) {
                            $scope.factorySaleOrder.lstFactorySaleOrderAttachedFile.push({
                                factorySaleOrderAttachedFileID: scope.method.getNewID(),
                                remark: null,
                                otherFileFriendlyName: value.fileName,
                                otherFileUrl: (value.filePath.indexOf('?') < 0) ? value.filePath + '?' + n : value.filePath + n,
                                otherFileHasChange: true,
                                newOtherFile: value.fileName,
                            });
                        }, null);
                    });
                };

                userFileMng.open();
            },

            removeFile: function (item) {
                var index = $scope.factorySaleOrder.lstFactorySaleOrderAttachedFile.indexOf(item);

                $timeout(function () {
                    $scope.factorySaleOrder.lstFactorySaleOrderAttachedFile.splice(index, 1)
                }, 0);
            },

            init: function () {
                factorySaleOrderService.load(
                    context.id,
                    null,
                    function (data) {
                        $scope.factorySaleOrder = data.data.data;
                        $scope.lstStatus = data.data.lstStatus;
                        $scope.lstPaymentTerm = data.data.lstPaymentTerm;
                        $scope.lstShipmentTypeOption = data.data.lstShipmentTypeOption;
                        $scope.lstShipmentToOption = data.data.lstShipmentToOption;
                        $scope.lstWarehouse = data.data.lstWarehouse;
                        $scope.supplierContactQuickInfo = data.data.supplierContactQuickInfo;
                        for (var j = 0; j < $scope.supplierContactQuickInfo.length; j++) {
                            var item2 = $scope.supplierContactQuickInfo[j];
                            if ($scope.factorySaleOrder.factoryRawMaterialID == item2.factoryRawMaterialID) {
                                $scope.supplierContact.push(item2);
                            }
                        }
                      
                        if (context.id === 0) {
                            
                            $scope.factorySaleOrder.expectedPaidDate = jsHelper.getCurrentDateTime().substr(0, 10);
                            $scope.factorySaleOrder.documentDate = jsHelper.getCurrentDateTime().substr(0, 10);
                        }
                        ///Factory Raw Materil
                        jQuery('#factorySaleQuotationID').autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    cache: false,
                                    headers: {
                                        'Accept': 'application/json',
                                        'Content-Type': 'application/json',
                                        'Authorization': 'Bearer ' + context.token
                                    },
                                    type: 'POST',
                                    data: JSON.stringify({
                                        filters: {
                                            searchQuery: request.term
                                        }
                                    }),
                                    dataType: 'json',
                                    url: context.serviceUrl + 'getFilterQuotation',
                                    beforeSend: function () {
                                        jsHelper.loadingSwitch(true);
                                    },
                                    success: function (data) {
                                        jsHelper.loadingSwitch(false);
                                        response(data.data);
                                    },
                                    error: function (xhr, ajaxOptions, throwError) {
                                        jsHelper.loadingSwitch(false);

                                        errorCallBack(xhr.responseJSON.exceptionMessage);
                                    }
                                });
                            },
                            minLength: 3,
                            select: function (event, ui) {
                                $scope.factorySaleOrder.factorySaleQuotationID = ui.item.id;

                                $scope.$apply();
                            }
                        });
                        jQuery('#factoryRawMaterialOfficialNM').autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    cache: false,
                                    headers: {
                                        'Accept': 'application/json',
                                        'Content-Type': 'application/json',
                                        'Authorization': 'Bearer ' + context.token
                                    },
                                    type: 'POST',
                                    data: JSON.stringify({
                                        filters: {
                                            searchQuery: request.term
                                        }
                                    }),
                                    dataType: 'json',
                                    url: context.serviceUrl + 'getFilterRawMaterial',
                                    beforeSend: function () {
                                        jsHelper.loadingSwitch(true);
                                    },
                                    success: function (data) {
                                        jsHelper.loadingSwitch(false);
                                        response(data.data);
                                    },
                                    error: function (xhr, ajaxOptions, throwError) {
                                        jsHelper.loadingSwitch(false);

                                        errorCallBack(xhr.responseJSON.exceptionMessage);
                                    }
                                });
                            },
                            minLength: 3,
                            select: function (event, ui) {
                                $scope.factorySaleOrder.factoryRawMaterialID = ui.item.id;
                                $scope.factorySaleOrder.factoryRawMaterialOfficialNM = ui.item.factoryRawMaterialOfficialNM;
                                $scope.factorySaleOrder.shippingAddress = ui.item.address;
                                $scope.factorySaleOrder.billingAddress = ui.item.address;
                                $scope.$apply();
                                $scope.event.getContactUser();
                            }
                        });
                        jQuery('#factoryRawMaterialContactPersonNM').autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    cache: false,
                                    headers: {
                                        'Accept': 'application/json',
                                        'Content-Type': 'application/json',
                                        'Authorization': 'Bearer ' + context.token
                                    },
                                    type: 'POST',
                                    data: JSON.stringify({
                                        filters: {
                                            searchQuery: request.term
                                        }
                                    }),
                                    dataType: 'json',
                                    url: context.serviceUrl + 'getFilterClientcontact',
                                    beforeSend: function () {
                                        jsHelper.loadingSwitch(true);
                                    },
                                    success: function (data) {
                                        jsHelper.loadingSwitch(false);
                                        response(data.data);
                                    },
                                    error: function (xhr, ajaxOptions, throwError) {
                                        jsHelper.loadingSwitch(false);

                                        errorCallBack(xhr.responseJSON.exceptionMessage);
                                    }
                                });
                            },
                            minLength: 3,
                            select: function (event, ui) {
                                $scope.factorySaleOrder.factoryRawMaterialContactPersonNM = ui.item.value;
                                $scope.$apply();
                                $scope.event.getContactUser();
                            }
                        });
                        //////
                        jQuery('#employeeNM').autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    cache: false,
                                    headers: {
                                        'Accept': 'application/json',
                                        'Content-Type': 'application/json',
                                        'Authorization': 'Bearer ' + context.token
                                    },
                                    type: 'POST',
                                    data: JSON.stringify({
                                        filters: {
                                            searchQuery: request.term
                                        }
                                    }),
                                    dataType: 'json',
                                    url: context.serviceUrl + 'getFilterSaleEmployee',
                                    beforeSend: function () {
                                        jsHelper.loadingSwitch(true);
                                    },
                                    success: function (data) {
                                        jsHelper.loadingSwitch(false);
                                        response(data.data);
                                    },
                                    error: function (xhr, ajaxOptions, throwError) {
                                        jsHelper.loadingSwitch(false);

                                        errorCallBack(xhr.responseJSON.exceptionMessage);
                                    }
                                });
                            },
                            minLength: 3,
                            select: function (event, ui) {
                                $scope.factorySaleOrder.factorySaleUserID = ui.item.id;
                                $scope.$apply();
                            }
                        });
                        debugger;
                        jQuery('#content').show();
                        
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);

                        $scope.data = null;
                    }
                );
            },
            update: function () {
                if (jQuery('#createdDate').val() === null || jQuery('#createdDate').val() === '') {
                    jsHelper.showMessage('warning', 'Please input Posting Date.');
                    return false;
                }
                if (jQuery('#documentDate').val() === null || jQuery('#documentDate').val() === '') {
                    jsHelper.showMessage('warning', 'Please input Document Date.');
                    return false;
                }
                if (jQuery('#expectedPaidDate').val() === null || jQuery('#expectedPaidDate').val() === '') {
                    $scope.factorySaleOrder.expectedPaidDate = "";
                }
                $scope.factorySaleOrder.discountPercent = $scope.event.toTalPriceDiscount();

                if ($scope.editForm.$valid) {
                    factorySaleOrderService.update(
                        context.id,
                        $scope.factorySaleOrder,
                        function (data) {
                            jsHelper.processMessage(data.message);
                            if (data.message.type === 0) {
                                $scope.method.refresh(data.data.factorySaleOrderID);
                            }
                        },
                        function (error) {


                            jsHelper.showMessage('warning', error.data.message.message);

                        }
                    );
                }
                else {
                    jsHelper.showMessage('warning', 'Invalid input data, please check');
                }
            },

            // remove item from list User of ProductitemGroup
            removeProductItem: function (item) {
                var index = $scope.factorySaleOrder.lstFactorySaleOrderDetail.indexOf(item);
                $scope.factorySaleOrder.lstFactorySaleOrderDetail.splice(index, 1);
            },
            select: function (productionItem, item) {
                item.productionItemID = productionItem.id;
                item.productionItemUD = productionItem.description;
                item.productionItemNM = productionItem.label;
                item.factoryWarehouseNM = productionItem.factoryWarehouseNM;
                item.factoryWarehouseID = productionItem.defaultFactoryWarehouse;
                item.unitID = productionItem.unitID;
                item.unitNM = productionItem.unitNM;
                $scope.$apply();
            },
            //==========

            getFactorySaleQuotation: function (factorySalesQuotation) {
                if (factorySalesQuotation !== null) {
                    debugger;
                    $scope.factorySaleOrder.factorySaleQuotationID = factorySalesQuotation.factorySaleQuotationID;
                    $scope.factorySaleOrder.factorySaleQuotationUD = factorySalesQuotation.factorySaleQuotationUD;
                    $scope.factorySaleOrder.factoryRawMaterialID = factorySalesQuotation.factoryRawMaterialID;
                    $scope.factorySaleOrder.factoryRawMaterialUD = factorySalesQuotation.factoryRawMaterialUD;
                    $scope.factorySaleOrder.factoryRawMaterialOfficialNM = factorySalesQuotation.factoryRawMaterialOfficialNM;
                    $scope.factorySaleOrder.factoryRawMaterialDocumentRefNo = factorySalesQuotation.factoryRawMaterialDocumentRefNo;
                    $scope.factorySaleOrder.factoryRawMaterialContactPersonNM = factorySalesQuotation.factoryRawMaterialContactPersonNM;
                    $scope.factorySaleOrder.factoryShippingMethodID = factorySalesQuotation.factoryShippingMethodID;
                    $scope.factorySaleOrder.factoryPaymentTermID = factorySalesQuotation.factoryPaymentTermID;
                    $scope.factorySaleOrder.currency = factorySalesQuotation.currency;
                    $scope.factorySaleOrder.factorySaleOrderStatusID = factorySalesQuotation.factorySaleOrderStatusID;
                    $scope.factorySaleOrder.shippingAddress = factorySalesQuotation.shippingAddress;
                    $scope.factorySaleOrder.billingAddress = factorySalesQuotation.billingAddress;
                    $scope.factorySaleOrder.employeeNM = factorySalesQuotation.employeeNM;
                    $scope.factorySaleOrder.remark = factorySalesQuotation.remark;
                    $scope.factorySaleOrder.factorySaleUserID = factorySalesQuotation.factorySaleUserID;
                    $scope.factorySaleOrder.factoryRawMaterialShortNM = factorySalesQuotation.factoryRawMaterialShortNM;
                    $scope.factorySaleOrder.fullName = factorySalesQuotation.fullName;
                    $scope.factorySaleOrder.supplierContactQuickInfoId = factorySalesQuotation.supplierContactQuickInfoId;

                    //add to factory sale order detail
                    $scope.factorySaleOrder.lstFactorySaleOrderDetail = [];
                    angular.forEach(factorySalesQuotation.factorySaleQuotationDetailDTOs, function (pItem) {
                        var factorySaleOrderItem = {
                            factorySaleOrderDetailID: $scope.method.getNewID(),
                            productionItemID: pItem.productionItemID,
                            unitPrice: pItem.unitPrice,
                            quantity: pItem.quantity,
                            discountPercent: pItem.discountPercent,
                            vatPercent: pItem.vatPercent,
                            factoryWarehouseID: pItem.factoryWarehouseID,
                            factoryWarehouseNM: pItem.factoryWarehouseNM,
                            unitID: pItem.unitID,
                            unitNM: pItem.unitNM,
                            productionItemUD: pItem.productionItemUD,
                            productionItemNM: pItem.productionItemNM,
                            remark: pItem.remark
                        };

                        //add to factory sale order detail
                        $scope.factorySaleOrder.lstFactorySaleOrderDetail.push(factorySaleOrderItem);
                    });
                    //add to factory sale order File detail
                    $scope.factorySaleOrder.lstFactorySaleOrderAttachedFile = [];

                    angular.forEach(factorySalesQuotation.factorySaleQuotationAttachedFileDTOs, function (pItem) {
                        var factorySaleOrderFileItem = {
                            factorySaleOrderAttachedFileID: $scope.method.getNewID(),
                            fileUD: pItem.fileUD,
                            remark: pItem.remark,
                            otherFileUrl: pItem.otherFileUrl,
                            otherFileFriendlyName: pItem.otherFileFriendlyName
                        };

                        //add to factory sale order File detail
                        $scope.factorySaleOrder.lstFactorySaleOrderAttachedFile.push(factorySaleOrderFileItem);
                    });
                }
                $scope.$apply();
            },

            approve: function () {
                if (confirm('Are you sure ?')) {
                    factorySaleOrderService.approve(
                        context.id,
                        $scope.factorySaleOrder,
                        function (data) {
                            window.location = context.refreshUrl + data.data.factorySaleOrderID;
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        });
                }
            },
            cancel: function () {
                if (confirm('Are you sure Cancel ?')) {
                    factorySaleOrderService.cancel(
                        context.id,
                        $scope.factorySaleOrder,
                        function (data) {
                            if (data.message.type === 2) {
                                jsHelper.showMessage('warning', data.message.message);
                            } else {
                                window.location = context.refreshUrl + data.data.factorySaleOrderID;
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        });
                }
            },
            getContactUser: function () {
                debugger;
                for (var j = 0; j < $scope.supplierContactQuickInfo.length; j++) {
                    var item2 = $scope.supplierContactQuickInfo[j];
                    if ($scope.factorySaleOrder.factoryRawMaterialID == item2.factoryRawMaterialID) {
                        $scope.supplierContact.push(item2);
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
                window.location = context.refreshUrl + id;
            },
            getNewID: function () {
                $scope.newid--;
                return $scope.newid;
            },
            assignAutoCompleteProductionItem: function () {
                angular.forEach($scope.factorySaleQuotation.lstFactorySaleQuotationDetail, function (item) {
                    $("#productionItem" + item.factorySaleQuotationDetailID).autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                cache: false,
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json',
                                    'Authorization': 'Bearer ' + context.token
                                },
                                type: "POST",
                                data: JSON.stringify({
                                    filters: {
                                        searchQuery: request.term,
                                    }
                                }),
                                dataType: 'JSON',
                                url: context.serviceUrl + 'getFilterProductItem',
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
                            $scope.$apply();
                        }
                    });

                });
            },
        };

        //
        // init
        //
        $scope.event.init();
    };
})();