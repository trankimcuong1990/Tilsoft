

function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.label,
            description: item.value,
            defaultFactoryWarehouse: item.factoryWarehouseID,
            unitID: item.unitID,
            unitNM: item.unitNM,
        }
    });
}

(function () {
    'use strict';

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', factorySaleQuotationController);
    factorySaleQuotationController.$inject = ['$scope', 'dataService', '$timeout'];

    function factorySaleQuotationController($scope, factorySaleQuotationService, $timeout) {
        factorySaleQuotationService.searchFilter.pageSize = context.pageSize;
        factorySaleQuotationService.serviceUrl = context.serviceUrl;
        factorySaleQuotationService.token = context.token;

        //
        // property
        //
        $scope.factorySaleQuotation = null;
        $scope.newID = -1;

        $scope.lstStatus = [];
        $scope.lstTypeOfCurrency = [];
        $scope.lstPaymentTerm = [];
        $scope.lstShipmentTypeOption = [];
        $scope.lstShipmentToOption = [];

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
            }
        ];

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
            url: context.serviceUrl + 'getFilterProductItem',
            token: context.token
        };
        $scope.ngSelectedProductionItem = {
            id: null,
            label: null,
            description: null
        };

        //-------------------------------
        // event
        //
        $scope.event = {

            changeCurrency: function (item) {
                $scope.factorySaleQuotation.currency = item.currency;
            },

            ////Add a production item
            addProductionItems: function () {
                var item = {
                    factorySaleQuotationDetailID: $scope.method.getNewID(),
                };

                $scope.factorySaleQuotation.lstFactorySaleQuotationDetail.push(item);
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

                if ($scope.factorySaleQuotation === null) {
                    return result;
                }

                if ($scope.factorySaleQuotation !== null) {
                    angular.forEach($scope.factorySaleQuotation.lstFactorySaleQuotationDetail, function (item) {
                        result += $scope.event.calTotalByItem(item);
                    });
                }

                return result;
            },
            toTalPriceBeforeDiscount: function () {
                var totalprice = parseFloat(0);

                if ($scope.factorySaleQuotation === null) {
                    return totalprice;
                }

                if ($scope.factorySaleQuotation !== null || $scope.factorySaleQuotation.lstFactorySaleQuotationDetail !== null) {
                    angular.forEach($scope.factorySaleQuotation.lstFactorySaleQuotationDetail, function (item) {
                        if (item === null || item === undefined || item === '')
                            return totalprice;
                        totalprice += parseFloat(item.unitPrice * item.quantity);
                    });
                }

                return totalprice;
            },
            toTalPriceDiscount: function () {
                var totalPrice = parseFloat(0);
                var discountPercentTotal = parseFloat(0);
                if ($scope.factorySaleQuotation === null || $scope.factorySaleQuotation === undefined) {
                    return totalPrice;
                }

                if ($scope.factorySaleQuotation.lstFactorySaleQuotationDetail !== null || $scope.factorySaleQuotation.lstFactorySaleQuotationDetail !== undefined) {
                    angular.forEach($scope.factorySaleQuotation.lstFactorySaleQuotationDetail, function (item) {
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
                return totalPrice;
            },
            calTax: function () {
                var result = parseFloat(0);
                var totalPriceDiscount = parseFloat(0);
                var totalPriceBeforeDiscount = parseFloat(0);
                var discountPercentTotal = parseFloat(0);

                if ($scope.factorySaleQuotation === null) {
                    return result;
                }

                if ($scope.factorySaleQuotation.lstFactorySaleQuotationDetail !== null || $scope.factorySaleQuotation.lstFactorySaleQuotationDetail !== undefined) {
                    angular.forEach($scope.factorySaleQuotation.lstFactorySaleQuotationDetail, function (item) {
                        if (item === null || item.vatPercent === null || item.vatPercent === '' || item.vatPercent === undefined) return result;
                        //result += parseFloat(item.vatPercent);
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
                return result;
            },
            /// set default ware house
            setDefaultWarehouse: function (item) {
                $cope.warehouseNM = item.factoryWarehouseID;
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
                            $scope.factorySaleQuotation.lstFactorySaleQuotationAttachedFile.push({
                                factorySaleQuotationAttachedFileID: scope.method.getNewID(),
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
                var index = $scope.factorySaleQuotation.lstFactorySaleQuotationAttachedFile.indexOf(item);
                $scope.factorySaleQuotation.lstFactorySaleQuotationAttachedFile.splice(index, 1);
            },
            init: function () {
                //dataService.searchFilter.pageSize = context.pageSize;
                factorySaleQuotationService.serviceUrl = context.serviceUrl;
                factorySaleQuotationService.token = context.token;
                factorySaleQuotationService.load(
                    context.id,
                    null,
                    function (data) {
                        $scope.factorySaleQuotation = data.data.data;
                        $scope.lstStatus = data.data.lstStatus;
                        $scope.lstPaymentTerm = data.data.lstPaymentTerm;
                        $scope.lstShipmentTypeOption = data.data.lstShipmentTypeOption;
                        $scope.lstShipmentToOption = data.data.lstShipmentToOption;
                        $scope.lstWarehouse = data.data.lstWarehouse;

                        //if (context.id === 0) {
                        //    $scope.factorySaleQuotation.validUntil = jsHelper.getCurrentDateTime().substr(0, 10);
                        //    $scope.factorySaleQuotation.createdDate = jsHelper.getCurrentDateTime().substr(0, 10);
                        //    $scope.factorySaleQuotation.expectedPaidDate = jsHelper.getCurrentDateTime().substr(0, 10);
                        //    $scope.factorySaleQuotation.documentDate = jsHelper.getCurrentDateTime().substr(0, 10);
                        //}

                        ///Factory Raw Material
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
                                $scope.factorySaleQuotation.factoryRawMaterialID = ui.item.id;
                                $scope.factorySaleQuotation.factoryRawMaterialOfficialNM = ui.item.factoryRawMaterialOfficialNM;
                                $scope.factorySaleQuotation.shippingAddress = ui.item.address;
                                $scope.factorySaleQuotation.billingAddress = ui.item.address;
                                $scope.$apply();
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
                                $scope.factorySaleQuotation.factoryRawMaterialContactPersonNM = ui.item.value;
                                $scope.$apply();
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
                                $scope.factorySaleQuotation.factorySaleUserID = ui.item.id;
                                $scope.$apply();
                            }
                        });

                        jQuery('#content').show();
                        $scope.method.assignAutoCompleteProductionItem();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);

                        $scope.data = null;
                    }
                );
            },
            update: function () {
                if (jQuery('#createdDate').val() === null || jQuery('#createdDate').val() === '') {
                    $scope.factorySaleQuotation.createdDate = "";
                }
                if (jQuery('#documentDate').val() === null || jQuery('#documentDate').val() === '') {
                    jsHelper.showMessage('warning', 'Please input Document Date.');
                    return false;
                }
                if (jQuery('#validUntil').val() === null || jQuery('#validUntil').val() === '') {
                    $scope.factorySaleQuotation.validUntil = "";
                }
                if (jQuery('#expectedPaidDate').val() === null || jQuery('#expectedPaidDate').val() === '') {
                    $scope.factorySaleQuotation.expectedPaidDate = "";
                }

                $scope.factorySaleQuotation.discountPercent = $scope.event.toTalPriceDiscount();
                for (var i = 0; i < $scope.factorySaleQuotation.lstFactorySaleQuotationDetail.length; i++) {
                    var item = $scope.factorySaleQuotation.lstFactorySaleQuotationDetail[i];
                    if (item.discountPercent === "" || item.discountPercent === null) {
                        $scope.factorySaleQuotation.lstFactorySaleQuotationDetail[i].discountPercent = null;
                    }
                }

                if ($scope.editForm.$valid) {
                    factorySaleQuotationService.update(
                        context.id,
                        $scope.factorySaleQuotation,
                        function (data) {
                            jsHelper.processMessage(data.message);
                            if (data.message.type === 0) {
                                $scope.method.refresh(data.data.factorySaleQuotationID);
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.data.exceptionMessage);
                            jsHelper.showMessage('warning', error.data.message.message);
                        }
                    );
                }
                else {
                    jsHelper.showMessage('warning', 'Invalid input data, please check');
                }
            },
            // 
            removeProductItem: function (item) {
                var index = $scope.factorySaleQuotation.lstFactorySaleQuotationDetail.indexOf(item);
                $scope.factorySaleQuotation.lstFactorySaleQuotationDetail.splice(index, 1);
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

            approve: function () {
                if (confirm('Are you sure ?')) {
                    factorySaleQuotationService.approve(
                        context.id,
                        $scope.factorySaleQuotation,
                        function (data) {
                            window.location = context.refreshUrl + data.data.factorySaleQuotationID;
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        });
                }
            },
            cancel: function () {
                if (confirm('Are you sure Cancel ?')) {
                    factorySaleQuotationService.cancel(
                        context.id,
                        $scope.factorySaleQuotation,
                        function (data) {
                            if (data.message.type === 2) {
                                jsHelper.showMessage('warning', data.message.message);
                            } else {
                                window.location = context.refreshUrl + data.data.factorySaleQuotationID;
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        });
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
                $scope.$apply();
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
                        minLength: 3,
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