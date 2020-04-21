jQuery('.search-filter').keypress(
    function (event) {
        if (event.keyCode === 13) {
            var scope = angular.element(jQuery('body')).scope();

            scope.event.refresh();
        }
    }
);

(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', warehouseInvoiceGrossMarginRptController);
    warehouseInvoiceGrossMarginRptController.$inject = ['$scope', '$cookieStore', '$timeout', 'dataService'];

    function warehouseInvoiceGrossMarginRptController($scope, $cookieStore, $timeout, warehouseInvoiceGrossMarginRptService) {
        $scope.data = [];
        $scope.totalRows = 0;
        $scope.totalPage = 0;
        $scope.pageIndex = 1;

        $scope.supportSeason = [];

        $scope.filters = {
            season: jsHelper.getCurrentSeason(),
            commercialSaleInvoiceNr: '',
            commercialSaleInvoiceDate: '',
            commercialSaleInvoiceDateTo: ''
        };

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;

                    warehouseInvoiceGrossMarginRptService.searchFilter.pageIndex = $scope.pageIndex;

                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                warehouseInvoiceGrossMarginRptService.searchFilter.sortedDirection = sortedDirection;
                warehouseInvoiceGrossMarginRptService.searchFilter.sortedBy = sortedBy;

                warehouseInvoiceGrossMarginRptService.searchFilter.pageIndex = $scope.pageIndex = 1;

                $scope.event.search();
            },
            isTriggered: false
        };

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        var cookieValue = $cookieStore.get(context.cookieID);
        if (cookieValue) {
            $scope.filters = cookieValue;
        }

        $scope.event = {
            init: function () {
                warehouseInvoiceGrossMarginRptService.serviceUrl = context.serviceUrl;
                warehouseInvoiceGrossMarginRptService.token = context.token;

                warehouseInvoiceGrossMarginRptService.getInit(
                    function (data) {
                        $scope.supportSeason = data.data.supportSeason;

                        $scope.event.search();
                    },
                    function (error) {
                    });
            },
            search: function (isLoadMore) {
                $cookieStore.put(context.cookieID, $scope.filters);

                $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

                $scope.gridHandler.isTriggered = false;

                //$scope.filters.commercialSaleInvoiceDate = jQuery('#invoiceDate').val();

                warehouseInvoiceGrossMarginRptService.searchFilter.filters = $scope.filters;

                warehouseInvoiceGrossMarginRptService.search(
                    function (data) {
                        Array.prototype.push.apply($scope.data, data.data);

                        $scope.totalPage = Math.ceil(data.totalRows / warehouseInvoiceGrossMarginRptService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;

                        jQuery("#content").show();

                        if (!isLoadMore) {
                            $scope.gridHandler.goTop();
                        }

                        $scope.gridHandler.isTriggered = true;
                    },
                    function (error) {
                    });
            },
            refresh: function () {
                $scope.data = [];
                $scope.pageIndex = 1;

                warehouseInvoiceGrossMarginRptService.searchFilter.pageIndex = $scope.pageIndex = 1;

                $scope.event.search();
            },
            clear: function () {
                $scope.filters = {
                    season: jsHelper.getCurrentSeason(),
                    commercialSaleInvoiceNr: '',
                    commercialSaleInvoiceDate: '',
                    commercialSaleInvoiceDateTo: ''
                };

                jQuery('#invoiceDate').val(null);

                $scope.event.refresh();
            },
            getGrossMarginPercent: function (item) {
                return parseFloat(item.grossMargin) / parseFloat(item.saleAmount);
            },
            exportExcel: function () {
                warehouseInvoiceGrossMarginRptService.exportExcel(
                    $scope.filters.season,
                    $scope.filters.commercialSaleInvoiceNr,
                    $scope.filters.commercialSaleInvoiceDate,
                    $scope.filters.commercialSaleInvoiceDateTo,
                    warehouseInvoiceGrossMarginRptService.searchFilter.sortedBy,
                    warehouseInvoiceGrossMarginRptService.searchFilter.sortedDirection,
                    function (data) {
                        window.location = context.backendReportUrl + data.data;
                    },
                    function (error) {
                    });
            },
            getTotalQnt: function () {
                var totalQnt = 0;
                for (var i = 0; i < $scope.data.length; i++) {
                    var item = $scope.data[i];
                    if (item.quantity !== undefined && item.quantity !== null) {
                        totalQnt = parseFloat(totalQnt) + parseFloat(item.quantity);
                    }
                }
                return totalQnt;
            },
            getTotalStandardCostPricePerUnit: function () {
                var totalStandardCostPricePerUnit = 0;
                for (var i = 0; i < $scope.data.length; i++) {
                    var item = $scope.data[i];
                    if (item.purchasingPrice !== undefined && item.purchasingPrice !== null) {
                        totalStandardCostPricePerUnit = parseFloat(totalStandardCostPricePerUnit) + parseFloat(item.purchasingPrice);
                    }
                }
                return totalStandardCostPricePerUnit;
            },
            getTotalStandardCostPriceAmount: function () {
                var totalStandardCostPriceAmount = 0;
                for (var i = 0; i < $scope.data.length; i++) {
                    var item = $scope.data[i];
                    if (item.purchasingAmount !== undefined && item.purchasingAmount !== null) {
                        totalStandardCostPriceAmount = parseFloat(totalStandardCostPriceAmount) + parseFloat(item.purchasingAmount);
                    }
                }
                return totalStandardCostPriceAmount;
            },
            getTotalSalePricePerUnit: function () {
                var totalSalePricePerUnit = 0;
                for (var i = 0; i < $scope.data.length; i++) {
                    var item = $scope.data[i];
                    if (item.unitPrice !== undefined && item.unitPrice !== null) {
                        totalSalePricePerUnit = parseFloat(totalSalePricePerUnit) + parseFloat(item.unitPrice);
                    }
                }
                return totalSalePricePerUnit;
            },
            getTotalSalePriceAmount: function () {
                var totalSalePriceAmount = 0;
                for (var i = 0; i < $scope.data.length; i++) {
                    var item = $scope.data[i];
                    if (item.saleAmount !== undefined && item.saleAmount !== null) {
                        totalSalePriceAmount = parseFloat(totalSalePriceAmount) + parseFloat(item.saleAmount);
                    }
                }
                return totalSalePriceAmount;
            },
            getTotalGrossMarginAmount: function () {
                var totalGrossMarginAmount = 0;
                for (var i = 0; i < $scope.data.length; i++) {
                    var item = $scope.data[i];
                    if (item.grossMargin !== undefined && item.grossMargin !== null) {
                        totalGrossMarginAmount = parseFloat(totalGrossMarginAmount) + parseFloat(item.grossMargin);
                    }
                }
                return totalGrossMarginAmount;
            }
        };

        $timeout(function () {
            $scope.event.init();
        }, 10);
    };
})();