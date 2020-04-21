function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.value,
            description: item.label
        };
    });
}
(function () {
    'use strict';
    var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ngCookies']);
    tilsoftApp.controller('tilsoftController', summarySalesRptController);
    summarySalesRptController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function summarySalesRptController($scope, $cookieStore, summarySalesRptService) {
        // variable
        $scope.data = [];
        $scope.dataGroupOfSupplier = [];
        $scope.deliveryNotes = [];
        $scope.customer = [];
        $scope.workOrders = [];
        $scope.filter = {
            factoryRawMaterialID: null,
            salesOrderNo: '',
            startDate: '',
            endDate: ''
        };

        $scope.vatPercents = 0;
        $scope.discountPercents = 0;

        $scope.saleValues = 0;
        $scope.vatValues = 0;
        $scope.discountValues = 0;
        $scope.totalSaleAmounts = 0;
        $scope.totalSaleAmount = 0;

        // event
        $scope.event = {
            init: init,
            search: search,
            generateExcel: generateExcel
        };

        var a = 0;

        // method
        $scope.method = {
            //sale value
            getSaleValue: function (quantity, unitPrice) {
                if (quantity == null || unitPrice == null) {
                    return parseFloat(0);
                }
                return parseFloat(quantity) * parseFloat(unitPrice);
            },
            getSaleValueWithFactorySaleOrderID: function (factorySaleOrderID) {
                var totalWithFactorySaleOrderID = parseFloat(0);
                for (var i = 0; i < $scope.datadetail.length; i++) {
                    var detail = $scope.datadetail[i];
                    if (detail.factorySaleOrderID == factorySaleOrderID) {
                        totalWithFactorySaleOrderID += $scope.method.getSaleValue(detail.quantity, detail.unitPrice);
                    }
                }
                return parseFloat(totalWithFactorySaleOrderID);
            },
            getTotalSaleValue: function () {
                var totalSaleValue = parseFloat(0);
                for (var i = 0; i < $scope.data.length; i++) {
                    var data = $scope.data[i];
                    totalSaleValue += $scope.method.getSaleValueWithFactorySaleOrderID(data.factorySaleOrderID);
                }
                return parseFloat(totalSaleValue);
            },

            //VAT Percent Value
            getVATPercentValueWithFactorySaleOrderID: function (factorySaleOrderID) {
                var totalWithFactorySaleOrderID = parseFloat(0);
                for (var i = 0; i < $scope.datadetail.length; i++) {
                    var detail = $scope.datadetail[i];
                    if (detail.factorySaleOrderID == factorySaleOrderID) {
                        totalWithFactorySaleOrderID += detail.vatPercent;
                    }
                }
                return parseFloat(totalWithFactorySaleOrderID);
            },

            //VAT value
            getVATValue: function (VAT, discount, quantity, unitPrice) {
                if (quantity == null || unitPrice == null) {
                    return parseFloat(0);
                }
                return (((parseFloat(quantity) * parseFloat(unitPrice)) - ((parseFloat(quantity) * parseFloat(unitPrice) * discount) / 100))*VAT)/100;
            },
            getVATValueWithFactorySaleOrderID: function (factorySaleOrderID) {
                var totalWithFactorySaleOrderID = parseFloat(0);
                for (var i = 0; i < $scope.datadetail.length; i++) {
                    var detail = $scope.datadetail[i];
                    if (detail.factorySaleOrderID == factorySaleOrderID) {
                        totalWithFactorySaleOrderID += $scope.method.getVATValue(detail.vatPercent, detail.discountPercent, detail.quantity, detail.unitPrice);
                    }
                }
                return parseFloat(totalWithFactorySaleOrderID);
            },
            getTotalVATValue: function () {
                var totalVATValue = parseFloat(0);
                for (var i = 0; i < $scope.data.length; i++) {
                    var data = $scope.data[i];
                    totalVATValue += $scope.method.getVATValueWithFactorySaleOrderID(data.factorySaleOrderID);
                }
                return parseFloat(totalVATValue);
            },

            //Discount Percent Value
            getDiscountPercentValueWithFactorySaleOrderID: function (factorySaleOrderID) {
                var totalWithFactorySaleOrderID = parseFloat(0);
                for (var i = 0; i < $scope.datadetail.length; i++) {
                    var detail = $scope.datadetail[i];
                    if (detail.factorySaleOrderID == factorySaleOrderID) {
                        totalWithFactorySaleOrderID += detail.discountPercent;
                    }
                }
                return parseFloat(totalWithFactorySaleOrderID);
            },

            //Discount value
            getDiscountValue: function (discount, quantity, unitPrice) {
                if (quantity == null || unitPrice == null) {
                    return parseFloat(0);
                }
                return (parseFloat(quantity) * parseFloat(unitPrice) * discount) / 100;
            },
            getDiscountValueWithFactorySaleOrderID: function (factorySaleOrderID) {
                var totalWithFactorySaleOrderID = parseFloat(0);
                for (var i = 0; i < $scope.datadetail.length; i++) {
                    var detail = $scope.datadetail[i];
                    if (detail.factorySaleOrderID == factorySaleOrderID) {
                        totalWithFactorySaleOrderID += $scope.method.getDiscountValue(detail.discountPercent, detail.quantity, detail.unitPrice);
                    }
                }
                return parseFloat(totalWithFactorySaleOrderID);
            },
            getTotalDiscountValue: function () {
                var totalDiscountValue = parseFloat(0);
                for (var i = 0; i < $scope.data.length; i++) {
                    var data = $scope.data[i];
                    totalDiscountValue += $scope.method.getDiscountValueWithFactorySaleOrderID(data.factorySaleOrderID);
                }
                return parseFloat(totalDiscountValue);
            },

            //Total sale amount
            getSaleAmountValue: function (VAT, discount, quantity, unitPrice) {
                if (quantity == null || unitPrice == null) {
                    return parseFloat(0);
                }
                return (parseFloat(quantity) * parseFloat(unitPrice))-((parseFloat(quantity) * parseFloat(unitPrice) * discount) / 100) + ((((parseFloat(quantity) * parseFloat(unitPrice)) - ((parseFloat(quantity) * parseFloat(unitPrice) * discount) / 100)) * VAT) / 100);
            },
            getSaleAmountValueWithFactorySaleOrderID: function (factorySaleOrderID) {
                var totalWithFactorySaleOrderID = parseFloat(0);
                for (var i = 0; i < $scope.datadetail.length; i++) {
                    var detail = $scope.datadetail[i];
                    if (detail.factorySaleOrderID == factorySaleOrderID) {
                        totalWithFactorySaleOrderID += $scope.method.getSaleAmountValue(detail.vatPercent, detail.discountPercent, detail.quantity, detail.unitPrice);
                    }
                }
                return parseFloat(totalWithFactorySaleOrderID);
            },
            getDeliveryNoteAmountValueWithFactorySaleOrderID: function (factorySaleOrderID) {
                var totalWithFactorySaleOrderID = parseFloat(0);
                for (var i = 0; i < $scope.datadetail.length; i++) {
                    var detail = $scope.datadetail[i];
                    if (detail.factorySaleOrderID == factorySaleOrderID) {
                        totalWithFactorySaleOrderID += $scope.method.getSaleAmountValue(detail.vatPercent, detail.discountPercent, detail.totalQtyDeliveryNote, detail.unitPrice);
                    }
                }
                return parseFloat(totalWithFactorySaleOrderID);
            },
            getTotalSaleAmountValue: function () {
                var totalSaleAmountValue = parseFloat(0);
                for (var i = 0; i < $scope.data.length; i++) {
                    var data = $scope.data[i];
                    totalSaleAmountValue += $scope.method.getSaleAmountValueWithFactorySaleOrderID(data.factorySaleOrderID);
                }
                return parseFloat(totalSaleAmountValue);
            },
            getTotalDeliveryNoteAmountValue: function () {
                var totalDeliveryNoteAmountValue = parseFloat(0);
                for (var i = 0; i < $scope.data.length; i++) {
                    var data = $scope.data[i];
                    totalDeliveryNoteAmountValue += $scope.method.getDeliveryNoteAmountValueWithFactorySaleOrderID(data.factorySaleOrderID);
                }
                return parseFloat(totalDeliveryNoteAmountValue);
            },
            getDeliveryNote: function (factorySaleOrderID, productionItemID) {
                var item = [];
                var qnt = 0;
                for (var i = 0; i < $scope.deliveryNotes.length; i++) {
                    var sItem = $scope.deliveryNotes[i];
                    if (sItem.productionItemID === productionItemID && sItem.factorySaleOrderID === factorySaleOrderID) {
                        item.push(sItem);
                        qnt += sItem.qty;
                    }
                }
                for (var j = 0; j < $scope.datadetail.length; j++) {
                    var subItem = $scope.datadetail[j];
                    if (subItem.productionItemID === productionItemID && subItem.factorySaleOrderID === factorySaleOrderID) {
                        $scope.datadetail[j].totalQtyDeliveryNote = qnt;
                    }
                }
                return item;
            },

            getTotalItemDeliveryNote: function (quantity, totalQtyDeliveryNote, totalSaleAmount) {
                totalSaleAmount = Math.round(totalSaleAmount);
                if (quantity !== 0 && quantity !== null && totalSaleAmount !== null && totalQtyDeliveryNote !== null && totalQtyDeliveryNote !== 0) {
                    return totalSaleAmount / quantity * totalQtyDeliveryNote;
                }
                else {
                    return 0;
                }
            }
        };

        function init() {
            summarySalesRptService.serviceUrl = context.serviceUrl;
            summarySalesRptService.token = context.token;
            summarySalesRptService.supportServiceUrl = context.supportServiceUrl;
            summarySalesRptService.getInit(
                function (data) {
                    $scope.customer = data.data.supportCustomer;
                    $scope.deliveryNotes = data.data.deliveryNotes;
                    jQuery('#content').show();
                },
                function (error) {
                    $scope.seasons = null;
                }
            );
        };

        function generateExcel() {
            if (jQuery('#startDate').val() === null || jQuery('#startDate').val() === '') {
                jsHelper.showMessage('warning', 'Please input From Date.');
                return false;
            }

            if (jQuery('#endDate').val() === null || jQuery('#endDate').val() === '') {
                jsHelper.showMessage('warning', 'Please input To Date.');
                return false;
            }
            summarySalesRptService.generateExcel(
            {
                filters: {
                    factoryRawMaterialID: $scope.filter.factoryRawMaterialID,
                    salesOrderNo: $scope.filter.salesOrderNo,
                    startDate: $scope.filter.startDate,
                    endDate: $scope.filter.endDate,
                }
            },
            function (data) {
                window.location = context.backendReportUrl + data.data;
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            });
        };

        function search() {
            $scope.data = [];
            if (jQuery('#startDate').val() === null || jQuery('#startDate').val() === '') {
                jsHelper.showMessage('warning', 'Please input From Date.');
                return false;
            }

            if (jQuery('#endDate').val() === null || jQuery('#endDate').val() === '') {
                jsHelper.showMessage('warning', 'Please input To Date.');
                return false;
            }

            summarySalesRptService.getDataWithFilters(
            {
                filters: {
                    factoryRawMaterialID: $scope.filter.factoryRawMaterialID,
                    salesOrderNo: $scope.filter.salesOrderNo,
                    startDate: $scope.filter.startDate,
                    endDate: $scope.filter.endDate,
                }
            },
            function (data) {
                $scope.data = data.data.customerDatas;
                $scope.datadetail = data.data.productionItemDatas;
                $scope.$apply();
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            });
        };

        function changeSubSupplier(selectedItem) {
            if (selectedItem === null || selectedItem === '') {
                $scope.filter.factoryRawMaterialFullName = '';
                return;
            }

            for (var i = 0; i < $scope.subSuppliers.length; i++) {
                var eleItem = $scope.subSuppliers[i];

                if (eleItem.factoryRawMaterialID === selectedItem) {
                    $scope.filter.factoryRawMaterialFullName = eleItem.factoryRawMaterialOfficialNM;
                    return;
                }
            }
        };

        // default event
        $scope.event.init();
    }
})();