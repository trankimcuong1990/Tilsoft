(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', rapVNRptController);
    rapVNRptController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function rapVNRptController($scope, $cookieStore, rapVNRptService) {
        $scope.dataOrder = [];
        $scope.dataLoaded = [];
        $scope.dataShipped = [];
        $scope.dataWorkOrder = [];
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;
        $scope.seasons = null;
        $scope.factories = null;

        //Total Order
        $scope.totalOrderQnt = 0;
        $scope.totalOrderInCont = 0;
        $scope.totalOrderShippedQnt = 0;
        $scope.totalOrderShippedQnt40HC = 0;
        $scope.totalOrderToBeShippedQnt = 0;
        $scope.totalOrderToBeShippedQnt40HC = 0;

        //Total Loaded
        $scope.totalLoadedQnt = 0;
        $scope.totalLoadedQnt40HC = 0;

        //Total Shipped
        $scope.shippedQnt = 0;
        $scope.shippedQnt40HC = 0;
        $scope.totalShippedLoadedQnt = 0;
        $scope.totalShippedLoadedQnt40HC = 0;

        $scope.loadTab = 0;

        $scope.filter = {
            seasonText: jsHelper.getCurrentSeason(),
            factoryID: null,
            proformaInvoiceNo: '',
            clientUD: '',
            articleCode: '',
            description: ''
        };


        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    rapVNRptService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.getOrder(true);
                    $scope.event.getLoaded(true);
                    $scope.event.getShipped(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.dataOrder = [];

                rapVNRptService.searchFilter.sortedDirection = sortedDirection;
                rapVNRptService.searchFilter.pageIndex = $scope.pageIndex = 1;
                rapVNRptService.searchFilter.sortedBy = sortedBy;
                $scope.event.getOrder();
                $scope.event.getLoaded();
                $scope.event.getShipped();
            },
            isTriggered: false
        };

        $scope.event = {
            init: function () {
                rapVNRptService.searchFilter.pageSize = context.pageSize;
                rapVNRptService.serviceUrl = context.serviceUrl;
                rapVNRptService.token = context.token;

                //get season
                rapVNRptService.getInit(
                    function (data) {
                        $scope.seasons = data.data.season;
                        $scope.factories = data.data.factory;
                        $scope.loadTab = 1;
                        //$scope.event.getOrder();
                        jQuery('#content').show();
                    },
                    function (error) {
                        //$scope.seasons = null;
                        //$scope.$apply();
                    }
                );
            },

            search: function () {
                switch ($scope.loadTab) {
                    case 1: //   
                        $scope.dataOrder = [];
                        $scope.dataWorkOrder = [];
                        $scope.event.getOrder();
                        break;
                    case 2: //
                        $scope.dataLoaded = [];
                        $scope.event.getLoaded();
                        break;
                    case 3:
                        $scope.dataShipped = [];
                        $scope.event.getShipped();
                        break;
                }
            },
            getOrder: function (isLoadMore) {
                $cookieStore.put(context.cookieID, $scope.filters);

                $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
                $scope.gridHandler.isTriggered = false;

                rapVNRptService.searchFilter.filters = $scope.filters;
                rapVNRptService.getDataWithFilters(
                    {
                        filters: {
                            pageSize: rapVNRptService.searchFilter.pageSize,
                            pageIndex: rapVNRptService.searchFilter.pageIndex,
                            seasonText: $scope.filter.seasonText,
                            factoryID: $scope.filter.factoryID,
                            loadTab: $scope.loadTab,
                            proformaInvoiceNo: $scope.filter.proformaInvoiceNo,
                            clientUD: $scope.filter.clientUD,
                            articleCode: $scope.filter.articleCode,
                            description: $scope.filter.description

                        }
                    },
                    function (data) {
                        Array.prototype.push.apply($scope.dataOrder, data.data.orderDatas);
                        $scope.totalPage = Math.ceil(data.totalRows / rapVNRptService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;
                        $scope.dataWorkOrder = data.data.workOrderDatas;

                        $scope.totalOrderQnt = data.data.totalOrderQnt;
                        $scope.totalOrderInCont = data.data.totalOrderInCont;
                        $scope.totalOrderShippedQnt = data.data.totalOrderShippedQnt;
                        $scope.totalOrderShippedQnt40HC = data.data.totalOrderShippedQnt40HC;
                        $scope.totalOrderToBeShippedQnt = data.data.totalOrderToBeShippedQnt;
                        $scope.totalOrderToBeShippedQnt40HC = data.data.totalOrderToBeShippedQnt40HC;

                        $scope.$apply();
                        if (!isLoadMore) {
                            $scope.gridHandler.goTop();
                        }

                        $scope.gridHandler.isTriggered = true;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },
            getLoaded: function (isLoadMore) {
                $cookieStore.put(context.cookieID, $scope.filters);

                $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
                $scope.gridHandler.isTriggered = false;

                rapVNRptService.searchFilter.filters = $scope.filters;
                rapVNRptService.getDataWithFilters(
                    {
                        filters: {
                            pageSize: rapVNRptService.searchFilter.pageSize,
                            pageIndex: rapVNRptService.searchFilter.pageIndex,
                            seasonText: $scope.filter.seasonText,
                            factoryID: $scope.filter.factoryID,
                            loadTab: $scope.loadTab,
                            proformaInvoiceNo: $scope.filter.proformaInvoiceNo,
                            clientUD: $scope.filter.clientUD,
                            articleCode: $scope.filter.articleCode,
                            description: $scope.filter.description
                        }
                    },
                    function (data) {
                        Array.prototype.push.apply($scope.dataLoaded, data.data.loadedDatas);
                        $scope.totalPage = Math.ceil(data.totalRows / rapVNRptService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;

                        $scope.totalLoadedQnt = data.data.totalLoadedQnt;
                        $scope.totalLoadedQnt40HC = data.data.totalLoadedQnt40HC;
                        $scope.$apply();
                        if (!isLoadMore) {
                            $scope.gridHandler.goTop();
                        }
                        $scope.gridHandler.isTriggered = true;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },
            getShipped: function (isLoadMore) {
                $cookieStore.put(context.cookieID, $scope.filters);

                $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
                $scope.gridHandler.isTriggered = false;

                rapVNRptService.searchFilter.filters = $scope.filters;
                rapVNRptService.getDataWithFilters(
                    {
                        filters: {
                            pageSize: rapVNRptService.searchFilter.pageSize,
                            pageIndex: rapVNRptService.searchFilter.pageIndex,
                            seasonText: $scope.filter.seasonText,
                            factoryID: $scope.filter.factoryID,
                            loadTab: $scope.loadTab,
                            proformaInvoiceNo: $scope.filter.proformaInvoiceNo,
                            clientUD: $scope.filter.clientUD,
                            articleCode: $scope.filter.articleCode,
                            description: $scope.filter.description
                        }
                    },
                    function (data) {
                        Array.prototype.push.apply($scope.dataShipped, data.data.shippedDatas);
                        $scope.totalPage = Math.ceil(data.totalRows / rapVNRptService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;

                        $scope.shippedQnt = data.data.shippedQnt;
                        $scope.shippedQnt40HC = data.data.shippedQnt40HC;
                        $scope.totalShippedLoadedQnt = data.data.totalShippedLoadedQnt;
                        $scope.totalShippedLoadedQnt40HC = data.data.totalShippedLoadedQnt40HC;

                        $scope.$apply();
                        if (!isLoadMore) {
                            $scope.gridHandler.goTop();
                        }
                        $scope.gridHandler.isTriggered = true;


                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },

            generateExcel: function () {
                rapVNRptService.generateExcel(
                    $scope.filter.seasonText,
                    $scope.filter.factoryID,
                    function (data) {
                        if (data.message.type == 2) {
                            jsHelper.processMessage(data.message);
                            return;
                        }
                        window.location = context.reportUrl + data.data;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },
        };

        $scope.method = {
            clickTab: function (tabSelected) {
                switch (tabSelected) {
                    case 1: //   
                        $scope.loadTab = 1;
                        break;
                    case 2: //

                        $scope.loadTab = 2;
                        break;
                    case 3:
                        $scope.loadTab = 3;
                        break;
                }
            },

            totalQuantity: function (item) {
                var totalQnt = 0;
                for (var i = 0; i < $scope.dataWorkOrder.length; i++) {
                    var subItem = $scope.dataWorkOrder[i];
                    if (subItem.proformaInvoiceNo === item.proformaInvoiceNo && subItem.articleCode === item.articleCode && subItem.clientUD === item.clientUD) {
                        totalQnt = totalQnt + subItem.totalQuantity;
                    }
                }
                item.productionQnt = totalQnt;
                item.remainQnt = item.orderQnt - item.productionQnt;
                return totalQnt;
            }
        };

        $scope.event.init();
    };
})();
