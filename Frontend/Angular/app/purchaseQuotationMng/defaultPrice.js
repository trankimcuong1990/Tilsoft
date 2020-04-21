(function () {
    'use strict';
    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', purchaseQuotationMngController);
    purchaseQuotationMngController.$inject = ['$scope', 'dataService'];
    function purchaseQuotationMngController($scope, dataService) {
        //
        // init service
        //
        $scope.factoryWarehouses = [];
        $scope.subSuppliers = [];
        $scope.statusDefaults = [];

        //
        // property
        //
        $scope.productionItem = [];
        $scope.purchasingDetail = [];
        $scope.purchasingHeader = [];
        $scope.purchasingSupplier = [];
        $scope.countPurchasing = 0;

        $scope.filters = {
            filters: {
                yesNoValue: '',
                factoryRawMaterialID: '',
                productionItemUD: ''
            }
        }

        //
        // event
        //
        $scope.event = {
            load: load,
            setIsDefaultPrice: function () {
                dataService.setDefaultPrice(
                    $scope.filters.filters.productionItemUD,
                    $scope.filters.filters.factoryRawMaterialID,
                    $scope.filters.filters.yesNoValue,
                    $scope.purchasingDetail,
                    function (data) {
                        $scope.productionItem = [];
                        $scope.purchasingDetail = [];
                        $scope.purchasingHeader = [];
                        $scope.purchasingSupplier = [];
                        $scope.countPurchasing = 0;

                        $scope.productionItem = data.data.defaultPriceProductionItem;
                        $scope.countPurchasing = data.data.countPurchasing;

                        // Create header purchasing
                        for (var i = 0; i < $scope.countPurchasing; i++) {
                            var itemHeader = {
                                commonHeader: 'Quotation',
                                priceHeader: 'Price',
                                quantityHeader: 'Order Qnt'
                            };
                            $scope.purchasingHeader.push(itemHeader);
                        }

                        // Create data content purchasing
                        $scope.method.addContentPurchasing();

                        var j = 0;
                        for (var i = 0; i < $scope.purchasingSupplier.length; i++) {
                            var dItem = data.data.defaultPricePurchaseQuotationDetail[j];
                            if (dItem !== undefined) {
                                if (dItem.productionItemID === $scope.purchasingSupplier[i].productionItemID) {
                                    $scope.purchasingSupplier[i].purchaseQuotationDetailID = dItem.purchaseQuotationDetailID;
                                    $scope.purchasingSupplier[i].validFrom = dItem.validFrom;
                                    $scope.purchasingSupplier[i].validTo = dItem.validTo;
                                    $scope.purchasingSupplier[i].factoryRawMaterialUD = dItem.factoryRawMaterialUD;
                                    j = j + 1;
                                }
                            }
                        }

                        j = 0;
                        for (var i = 0; i < $scope.purchasingDetail.length;) {
                            var dItem = data.data.defaultPricePurchaseQuotationDetail[j];
                            if (dItem !== undefined) {
                                if (dItem.productionItemID === $scope.purchasingDetail[i].productionItemID) {
                                    // Price
                                    $scope.purchasingDetail[i].purchaseQuotationDetailID = dItem.purchaseQuotationDetailID;
                                    $scope.purchasingDetail[i].unitPrice = dItem.unitPrice;
                                    $scope.purchasingDetail[i].isDefault = dItem.isDefault;
                                    // Order
                                    $scope.purchasingDetail[i + 1].purchaseQuotationDetailID = dItem.purchaseQuotationDetailID;
                                    $scope.purchasingDetail[i + 1].orderQnt = dItem.orderQnt;
                                    j = j + 1;
                                }
                            }
                            i = i + 2;
                        }

                        $scope.$apply();
                    },
                    function (error) {
                    });
            },
            getInit: getInit,
            searchDefaultPrice: searchDefaultPrice,
            checkedItem: function (wItem) {
                var index = $scope.purchasingDetail.indexOf(wItem);
                for (var i = 0; i < $scope.purchasingDetail.length; i++) {
                    if ($scope.purchasingDetail[i].colName === 'PRICE' && $scope.purchasingDetail[i].productionItemID === wItem.productionItemID) {
                        if (i !== index) {
                            $scope.purchasingDetail[i].isDefault = false;
                        }
                    }
                }
            }
        }

        //
        // method
        //
        $scope.method = {
            getPurchaseQuotationInfo: getPurchaseQuotationInfo,
            countQuotationDetailByProductionItem: countQuotationDetailByProductionItem,
            showAllContents: showAllContents,
            addContentPurchasing: function () {
                for (var i = 0; i < $scope.productionItem.length; i++) {
                    for (var j = 0; j < $scope.countPurchasing; j++) {
                        var nItem = {
                            purchaseQuotationDetailID: 0,
                            productionItemID: $scope.productionItem[i].productionItemID,
                            unitPrice: null,
                            orderQnt: null,
                            isDefault: null,
                            colName: 'PRICE'
                        };
                        $scope.purchasingDetail.push(nItem);

                        nItem = {
                            purchaseQuotationDetailID: 0,
                            productionItemID: $scope.productionItem[i].productionItemID,
                            unitPrice: null,
                            orderQnt: null,
                            isDefault: null,
                            colName: 'ORDER'
                        };
                        $scope.purchasingDetail.push(nItem);

                        nItem = {
                            purchaseQuotationDetailID: 0,
                            productionItemID: $scope.productionItem[i].productionItemID,
                            validFrom: null,
                            validTo: null,
                            factoryRawMaterialUD: null,
                        };
                        $scope.purchasingSupplier.push(nItem);
                    }
                }
            }
        };

        //
        // init
        //
        $scope.event.getInit();


        /*
        /    implement function for event object
        */

        function getInit() {
            dataService.serviceUrl = context.serviceUrl;
            dataService.supportServiceUrl = context.supportServiceUrl;
            dataService.token = context.token;

            dataService.getInitDefaultPrice(
                function (data) {
                    $scope.factoryWarehouses = data.data.factoryWarehouses;
                    $scope.subSuppliers = data.data.subSuppliers;
                    $scope.statusDefaults = data.data.statusDefaults;

                    $scope.$apply();
                    $('#content').show();
                },
                function (error) {
                    // do nothing
                })
        }

        function searchDefaultPrice() {
            if ($scope.filters.filters.factoryRawMaterialID == null || $scope.filters.filters.factoryRawMaterialID == '') {
                jsHelper.showMessage('warning', 'Supplier is not null!');
                return false;
            }

            $scope.productionItem = [];
            $scope.purchasingDetail = [];
            $scope.purchasingHeader = [];
            $scope.purchasingSupplier = [];
            $scope.countPurchasing = 0;

            dataService.searchDefaultPrice(
                $scope.filters,
                function (data) {
                    $scope.productionItem = data.data.defaultPriceProductionItem;
                    $scope.countPurchasing = data.data.countPurchasing;

                    // Create header purchasing
                    for (var i = 0; i < $scope.countPurchasing; i++) {
                        var itemHeader = {
                            commonHeader: 'Quotation',
                            priceHeader: 'Price',
                            quantityHeader: 'Order Qnt'
                        };
                        $scope.purchasingHeader.push(itemHeader);
                    }

                    // Create data content purchasing
                    $scope.method.addContentPurchasing();

                    var j = 0;
                    for (var i = 0; i < $scope.purchasingSupplier.length; i++) {
                        var dItem = data.data.defaultPricePurchaseQuotationDetail[j];
                        if (dItem !== undefined) {
                            if (dItem.productionItemID === $scope.purchasingSupplier[i].productionItemID) {
                                $scope.purchasingSupplier[i].purchaseQuotationDetailID = dItem.purchaseQuotationDetailID;
                                $scope.purchasingSupplier[i].validFrom = dItem.validFrom;
                                $scope.purchasingSupplier[i].validTo = dItem.validTo;
                                $scope.purchasingSupplier[i].factoryRawMaterialUD = dItem.factoryRawMaterialUD;
                                j = j + 1;
                            }
                        }
                    }

                    j = 0;
                    for (var i = 0; i < $scope.purchasingDetail.length;) {
                        var dItem = data.data.defaultPricePurchaseQuotationDetail[j];
                        if (dItem !== undefined) {
                            if (dItem.productionItemID === $scope.purchasingDetail[i].productionItemID) {
                                // Price
                                $scope.purchasingDetail[i].purchaseQuotationDetailID = dItem.purchaseQuotationDetailID;
                                $scope.purchasingDetail[i].unitPrice = dItem.unitPrice;
                                $scope.purchasingDetail[i].isDefault = dItem.isDefault;
                                // Order
                                $scope.purchasingDetail[i + 1].purchaseQuotationDetailID = dItem.purchaseQuotationDetailID;
                                $scope.purchasingDetail[i + 1].orderQnt = dItem.orderQnt;
                                j = j + 1;
                            }
                        }
                        i = i + 2;
                    }
                },
                function (error) {
                })
        }

        function load() {
            dataService.preparingDefaultPriceData(
                function (data) {
                    $scope.defaultPrice_ProductionItemDTOs = data.data.defaultPrice_ProductionItemDTOs;
                    $scope.defaultPrice_PurchaseQuotationDTOs = data.data.defaultPrice_PurchaseQuotationDTOs;
                    $scope.defaultPrice_PurchaseQuotationDetailDTOs = data.data.defaultPrice_PurchaseQuotationDetailDTOs;

                    $scope.method.showAllContents();

                    //$('#content').show();
                },
                function (error) {
                    $scope.defaultPrice_ProductionItemDTOs = [];
                    $scope.defaultPrice_PurchaseQuotationDTOs = [];
                    $scope.defaultPrice_PurchaseQuotationDetailDTOs = [];
                    $scope.defaultPrice_PurchaseQuotationDetailDTOs_x2 = [];
                    $scope.headers_x2 = [];
                });
        }

        //function setIsDefaultPrice(purchaseQuotationDetailID, productionItemID) {
        //dataService.setDefaultPrice(purchaseQuotationDetailID, productionItemID,
        //    function (data) {
        //        bootbox.alert(data.message);

        //        $scope.defaultPrice_ProductionItemDTOs = data.data.defaultPrice_ProductionItemDTOs;
        //        $scope.defaultPrice_PurchaseQuotationDTOs = data.data.defaultPrice_PurchaseQuotationDTOs;
        //        $scope.defaultPrice_PurchaseQuotationDetailDTOs = data.data.defaultPrice_PurchaseQuotationDetailDTOs;

        //        $scope.method.showAllContents();
        //        $scope.$apply();
        //    },
        //    function (error) {
        //        //do nothing
        //    });
        //}


        /*
        /    implement function for method object
        */
        function countQuotationDetailByProductionItem(productionItemID) {
            var countItem = 0;
            angular.forEach($scope.defaultPrice_PurchaseQuotationDetailDTOs, function (item) {
                if (item.productionItemID == productionItemID) {
                    countItem++;
                }
            });
            return countItem;
        }

        function getPurchaseQuotationInfo(purchaseQuotationID) {
            var quotationInfo = null;
            angular.forEach($scope.defaultPrice_PurchaseQuotationDTOs, function (item) {
                if (item.purchaseQuotationID == purchaseQuotationID) {
                    quotationInfo = item;
                }
            });
            return quotationInfo;
        }

        function showAllContents() {
            $scope.defaultPrice_PurchaseQuotationDetailDTOs_x2 = [];
            $scope.defaultPrice_PurchaseQuotationDetailDTOs_tmp = [];
            $scope.headers_x2 = [];

            // draw unit price and order quantity.
            ////create data corresponding with every header (price, qnt, approveQnt)
            var index = 0;
            for (var i = 0; i < $scope.defaultPrice_ProductionItemDTOs.length; i++) {
                for (var j = 0; j < $scope.defaultPrice_PurchaseQuotationDTOs.length; j++) {
                    if (index === 0) {
                        var item = $scope.defaultPrice_PurchaseQuotationDetailDTOs[index];

                        //create colum unit price
                        var x = angular.copy(item);
                        x.columnName = 'UNITPRICE';
                        $scope.defaultPrice_PurchaseQuotationDetailDTOs_x2.push(x);

                        //create columm order qnt
                        var y = angular.copy(item);
                        y.columnName = 'ORDERQNT';
                        $scope.defaultPrice_PurchaseQuotationDetailDTOs_x2.push(y);

                        index++;
                    } else {
                        var curItem = $scope.defaultPrice_PurchaseQuotationDetailDTOs[index];
                        var prevItem = $scope.defaultPrice_PurchaseQuotationDetailDTOs[index - 1];

                        if (j === 0) {
                            //create colum unit price
                            var x = angular.copy(curItem);
                            x.columnName = 'UNITPRICE';
                            $scope.defaultPrice_PurchaseQuotationDetailDTOs_x2.push(x);

                            //create columm order qnt
                            var y = angular.copy(curItem);
                            y.columnName = 'ORDERQNT';
                            $scope.defaultPrice_PurchaseQuotationDetailDTOs_x2.push(y);

                            index++;
                        } else {
                            debugger
                            if (index < $scope.defaultPrice_PurchaseQuotationDetailDTOs.length && curItem.productionItemID === prevItem.productionItemID) {
                                //create colum unit price
                                var x = angular.copy(curItem);
                                x.columnName = 'UNITPRICE';
                                $scope.defaultPrice_PurchaseQuotationDetailDTOs_x2.push(x);

                                //create columm order qnt
                                var y = angular.copy(curItem);
                                y.columnName = 'ORDERQNT';
                                $scope.defaultPrice_PurchaseQuotationDetailDTOs_x2.push(y);

                                index++;
                            } else {
                                //create colum unit price
                                var x = angular.copy(prevItem);
                                x.columnName = 'UNITPRICE';
                                x.orderQnt = null;
                                x.unitPrice = null;
                                $scope.defaultPrice_PurchaseQuotationDetailDTOs_x2.push(x);

                                //create columm order qnt
                                var y = angular.copy(prevItem);
                                y.columnName = 'ORDERQNT';
                                y.orderQnt = null;
                                y.unitPrice = null;
                                $scope.defaultPrice_PurchaseQuotationDetailDTOs_x2.push(y);
                            }
                        }
                    }
                }
            }

            // draw td display factory.
            var index2 = 0;
            for (var i = 0; i < $scope.defaultPrice_ProductionItemDTOs.length; i++) {
                for (var j = 0; j < $scope.defaultPrice_PurchaseQuotationDTOs.length; j++) {
                    if (index2 === 0) {
                        var item = $scope.defaultPrice_PurchaseQuotationDetailDTOs[index2];

                        //create colum unit price
                        var x = angular.copy(item);
                        $scope.defaultPrice_PurchaseQuotationDetailDTOs_tmp.push(x);

                        index2++;
                    } else {
                        var curItem = $scope.defaultPrice_PurchaseQuotationDetailDTOs[index2];
                        var prevItem = $scope.defaultPrice_PurchaseQuotationDetailDTOs[index2 - 1];

                        if (j === 0) {
                            //create colum unit price
                            var x = angular.copy(curItem);
                            $scope.defaultPrice_PurchaseQuotationDetailDTOs_tmp.push(x);

                            index2++;
                        } else {
                            if (index2 < $scope.defaultPrice_PurchaseQuotationDetailDTOs.length && curItem.productionItemID === prevItem.productionItemID) {
                                //create colum unit price
                                var x = angular.copy(curItem);
                                $scope.defaultPrice_PurchaseQuotationDetailDTOs_tmp.push(x);

                                index2++;
                            } else {
                                //create colum unit price
                                var x = angular.copy(prevItem);
                                x.purchaseQuotationID = null;
                                $scope.defaultPrice_PurchaseQuotationDetailDTOs_tmp.push(x);
                            }
                        }
                    }
                }
            }

            ////create header
            angular.forEach($scope.defaultPrice_PurchaseQuotationDTOs, function (item) {
                $scope.headers_x2.push({
                    headerText: 'Price',
                });
                $scope.headers_x2.push({
                    headerText: 'Quo.Qnt',
                });
            });
        }
    };
})();
