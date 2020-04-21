//
// SUPPORT
//
jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', '$timeout', 'dataService', function ($scope, $cookieStore, $timeout, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.filters = {
        Season: jsHelper.getCurrentSeason(),
        SaleID: null,
        ClientID: null
    };
    $scope.saleFilter = [];
    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;
    $scope.canShowGeneral = false;
    $scope.canShowIndividual = false;
    $scope.canShowNoData = false;

    //
    // get filter from cookies
    //
    var cookieValue = $cookieStore.get(context.cookieId);
    if (cookieValue) {
        $scope.filters = cookieValue;
    }

    $scope.data = [];
    $scope.sales = [];
    $scope.seasons = jsHelper.getSeasons();

    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;
    $scope.summary = {
        orderQnt: 0,
        orderQnt40HC: 0.00,
        delta40HC: 0.00,
        purchasingAmount: 0.00,
        saleAmount: 0.00,
        inspectionCostUSD: 0.00,
        sampleCostUSD: 0.00,
        sparepartCostUSD: 0.00,
        transportCost: 0.00,
        lcCost: 0.00,
        interest: 0.00,
        commission: 0.00,
        bonus: 0.00,
        damage: 0.00,
        grossMargin: 0.00
    }
    $scope.summaryBySales = [];

    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
            //if ($scope.pageIndex < $scope.totalPage) {
            //    $scope.pageIndex++;
            //    dataService.searchFilter.pageIndex = $scope.pageIndex;
            //    $scope.event.search(true);
            //}
        },
        sort: function (sortedBy, sortedDirection) {


            $scope.data = [];
            dataService.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = scope.pageIndex;
            dataService.searchFilter.sortedBy = sortedBy;
            $scope.event.search();
        },
        isTriggered: false
    };
    $scope.nullHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
        },
        isTriggered: false
    };

    //
    // event
    //
    $scope.event = {
        reload: function () {
            $scope.data = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.search();
        },
        search: function (isLoadMore) {
            //
            // store filter in cookies
            //
            $cookieStore.put(context.cookieId, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            $scope.gridHandler.isTriggered = false;
            dataService.searchFilter.filters = $scope.filters;
            dataService.search(
                function (data) {
                    //Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.sales = [];
                    $scope.summaryBySales = [];
                    $scope.data = data.data.data;
                    $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;

                    // calculate sum amount
                    $scope.summary = {
                        orderQnt: 0,
                        orderQnt40HC: 0.00,
                        delta40HC: 0.00,
                        purchasingAmount: 0.00,
                        saleAmount: 0.00,
                        inspectionCostUSD: 0.00,
                        sampleCostUSD: 0.00,
                        sparepartCostUSD: 0.00,
                        transportCost: 0.00,
                        lcCost: 0.00,
                        interest: 0.00,
                        commission: 0.00,
                        bonus: 0.00,
                        damage: 0.00,
                        grossMargin: 0.00
                    };
                    angular.forEach($scope.data, function (item) {
                        if (!$scope.summaryBySales[item.saleUD]) {
                            $scope.summaryBySales[item.saleUD] = {
                                orderQnt: 0,
                                orderQnt40HC: 0.00,
                                delta40HC: 0.00,
                                purchasingAmount: 0.00,
                                saleAmount: 0.00,
                                inspectionCostUSD: 0.00,
                                sampleCostUSD: 0.00,
                                sparepartCostUSD: 0.00,
                                transportCost: 0.00,
                                lcCost: 0.00,
                                interest: 0.00,
                                commission: 0.00,
                                bonus: 0.00,
                                damage: 0.00,
                                grossMargin: 0.00,
                                count: 0
                            };
                        }

                        $scope.summaryBySales[item.saleUD].count++;
                        $scope.summary.orderQnt += item.totalOrderQnt;
                        $scope.summaryBySales[item.saleUD].orderQnt += item.totalOrderQnt;
                        $scope.summary.orderQnt40HC += item.totalOrderedQnt40HC;
                        $scope.summaryBySales[item.saleUD].orderQnt40HC += item.totalOrderedQnt40HC;
                        $scope.summary.delta40HC += item.delta40HC;
                        $scope.summaryBySales[item.saleUD].delta40HC += item.delta40HC;
                        $scope.summary.purchasingAmount += item.purchasingAmountUSD;
                        $scope.summaryBySales[item.saleUD].purchasingAmount += item.purchasingAmountUSD;
                        $scope.summary.saleAmount += item.saleAmountInUSD;
                        $scope.summaryBySales[item.saleUD].saleAmount += item.saleAmountInUSD;

                        $scope.summary.inspectionCostUSD += item.inspectionCostUSD;
                        $scope.summaryBySales[item.saleUD].inspectionCostUSD += item.inspectionCostUSD;
                        $scope.summary.sampleCostUSD += item.sampleCostUSD;
                        $scope.summaryBySales[item.saleUD].sampleCostUSD += item.sampleCostUSD;
                        $scope.summary.sparepartCostUSD += item.sparepartCostUSD;
                        $scope.summaryBySales[item.saleUD].sparepartCostUSD += item.sparepartCostUSD;

                        $scope.summary.transportCost += item.totalTransportUSD;
                        $scope.summaryBySales[item.saleUD].transportCost += item.totalTransportUSD;
                        $scope.summary.lcCost += item.lcCostAmount;
                        $scope.summaryBySales[item.saleUD].lcCost += item.lcCostAmount;
                        $scope.summary.interest += item.interestAmount;
                        $scope.summaryBySales[item.saleUD].interest += item.interestAmount;
                        $scope.summary.commission += item.commissionAmount;
                        $scope.summaryBySales[item.saleUD].commission += item.commissionAmount;
                        $scope.summary.bonus += item.bonusAmount;
                        $scope.summaryBySales[item.saleUD].bonus += item.bonusAmount;
                        $scope.summary.damage += item.damagesCost;
                        $scope.summaryBySales[item.saleUD].damage += item.damagesCost;
                        $scope.summary.grossMargin += item.grossMarginAmount;
                        $scope.summaryBySales[item.saleUD].grossMargin += item.grossMarginAmount;

                        // get sales if needed
                        if ($scope.sales.length === 0 || $scope.sales.indexOf(item.saleUD) < 0) {
                            $scope.sales.push(item.saleUD);
                        }
                    });

                    // calculate avg for delta40HC
                    $scope.summary.delta40HC = $scope.summary.delta40HC / $scope.data.length;
                    angular.forEach($scope.sales, function (item) {
                        $scope.summaryBySales[item].delta40HC = $scope.summaryBySales[item].delta40HC / $scope.summaryBySales[item].count;
                    });

                    // ui config hide/show
                    $scope.canShowGeneral = false;
                    $scope.canShowIndividual = false;
                    $scope.canShowNoData = false;
                    if (context.isAdmin && ($scope.filters.SaleID === null || $scope.filters.SaleID === -1)) {
                        $scope.canShowGeneral = true;
                    }
                    if ($scope.filters.SaleID !== -1) {
                        $scope.canShowIndividual = true;
                    }
                    if ($scope.sales.length === 0) {
                        $scope.canShowNoData = true;
                    }

                    $timeout(function () {
                        jsHelper.refreshAvsScroll();
                    }, 100);
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                }
            );
        },
        init: function () {
            // load list sale account mng
            //SupportMng_AccountManager_View
            dataService.getSearchFilter(
                function (data) {
                    $scope.saleFilter = data.data.saleDTOs;
                    jQuery('#content').show();
                    $scope.event.reload();
                },
                function (error) {
                    $scope.saleFilter = [];
                }
            );            
        },
        exportExcel: function () {
            dataService.exportExcel(
                {
                    filters: $scope.filters
                    //sortedBy: 'StockQnt',
                    //sortedDirection: 'DESC',
                    //pageSize: 20,
                    //pageIndex: 1
                },
                function (data) {
                    //jsHelper.processMessage(data.message);
                    if (data.message.type != 2) {
                        window.open(context.backendReportUrl + data.data);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    },
    //
    // init
    //
    $scope.event.init();
}]);