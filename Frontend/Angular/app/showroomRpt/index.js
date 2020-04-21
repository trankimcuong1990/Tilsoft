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
    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', showroomRptController);
    showroomRptController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function showroomRptController($scope, $cookieStore, showroomService) {
        $scope.data = [];
        $scope.descriptions = [];
        $scope.totalQuantity = 0;
        $scope.seasons = [];
        $scope.totalRows = 0;
        $scope.totalPage = 0;
        $scope.pageIndex = 1;

        $scope.factoryWarehouses = [];
        $scope.receivingNotes = [];
        $scope.factoryWarehousePallets = [];

        $scope.filters = {
            clientUD: '',
            productionItemUD: '',
            productionItemNM: '',
            factoryUD: '',
            listFactoryWarehouse: '',
            factoryWarehousePalletUD: '',
            season: ''
        };
        $scope.filter = {
            filters: {
                clientUD: '',
                productionItemUD: '',
                productionItemNM: '',
                factoryUD: '',
                listFactoryWarehouse: '',
                factoryWarehousePalletUD: '',
                season: ''
            }
        }

        var cookieValue = $cookieStore.get(context.cookieID);
        if (cookieValue) {
            $scope.filters = cookieValue;
        }

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        $scope.event = {
            init: init,
            search: search,
            refresh: refresh,
            clear: clear,
            filterShowroom: filterShowroom,
            exportExcel: exportExcel,
            loadNextPage: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    showroomService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sortData: function (sortedBy, sortedDirection) {
                $scope.data = [];
                showroomService.searchFilter.sortedDirection = sortedDirection;
                showroomService.searchFilter.pageIndex = $scope.pageIndex = 1;
                showroomService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            exportExcelWithoutImage: function () {
                $scope.filters.listFactoryWarehouse = $scope.method.getFactoryWarehouseFilters();
                showroomService.exportExcelWithoutImage(
                    $scope.filters.listFactoryWarehouse,
                    $scope.filter,
                    function (data) {
                        window.location = context.backendReportUrl + data.data;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
        };

        $scope.method = {
            getFactoryWarehouseFilters: getFactoryWarehouseFilters
        };

        function init() {
            showroomService.serviceUrl = context.serviceUrl;
            showroomService.token = context.token;

            showroomService.searchFilter.pageSize = context.pageSize;

            showroomService.getInit(
                function (data) {
                    $scope.factoryWarehouses = data.data.data;
                    $scope.receivingNotes = data.data.receivingNotes;
                    $scope.factoryWarehousePallets = data.data.factoryWarehousePallets;
                    $scope.seasons = data.data.seasons;
                    $scope.filters.listFactoryWarehouse = '';

                    for (var i = 0; i < $scope.factoryWarehouses.length; i++) {
                        var item = $scope.factoryWarehouses[i];
                        if (item.isChecked === undefined || item.isChecked) {
                            item.isChecked = false;

                            if (i > 0) {
                                $scope.filter.filters.listFactoryWarehouse = $scope.filter.filters.listFactoryWarehouse + ', ';
                            }

                            $scope.filter.filters.listFactoryWarehouse = $scope.filter.filters.listFactoryWarehouse + item.factoryWarehouseID.toString();
                        }
                    }
                    $('#content').show();
                },
                function (error) {
                })
        }

        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            showroomService.searchFilter.filters = $scope.filter.filters;
            showroomService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    Array.prototype.push.apply($scope.descriptions, data.data.productionItemWithDescriptions);
                    $scope.totalQuantity = data.data.totalQuantity;
                    $scope.totalPage = Math.ceil(data.totalRows / showroomService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                },
                function (error) {
                })
        }

        function refresh() {
            $scope.data = [];
            $scope.descriptions = [];
            $scope.pageIndex = 1;

            showroomService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.search(false);
        }

        function clear() {
            $scope.filter = {
                filters: {
                    clientUD: '',
                    productionItemUD: '',
                    productionItemNM: '',
                    factoryUD: '',
                    listFactoryWarehouse: '',
                    factoryWarehousePalletUD: '',
                    season: ''
                }
            };
            for (var i = 0; i < $scope.factoryWarehouses.length; i++) {
                $scope.factoryWarehouses[i].isChecked = false;
            }
            
            $scope.event.refresh();
        }

        function filterShowroom() {
            $scope.filter.filters.listFactoryWarehouse = '';

            for (var i = 0; i < $scope.factoryWarehouses.length; i++) {
                var item = $scope.factoryWarehouses[i];

                if (item.isChecked) {
                    if (i > 0 && $scope.filter.filters.listFactoryWarehouse !== '') {
                        $scope.filter.filters.listFactoryWarehouse = $scope.filter.filters.listFactoryWarehouse + ', ';
                    }

                    $scope.filter.filters.listFactoryWarehouse = $scope.filter.filters.listFactoryWarehouse + item.factoryWarehouseID.toString();
                }
            }

            //if ($scope.filters.listFactoryWarehouse === '') {
            //    $scope.data = [];
            //} else {
            //    $scope.event.refresh();
            //}
        }

        function exportExcel() {
            $scope.filter.filters.listFactoryWarehouse = $scope.method.getFactoryWarehouseFilters();

            showroomService.exportExcel(
                $scope.filters.listFactoryWarehouse,
                $scope.filter,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        }

        function getFactoryWarehouseFilters() {
            var result = ''

            for (var i = 0; i < $scope.factoryWarehouses.length; i++) {
                var item = $scope.factoryWarehouses[i];

                if (item.isChecked) {
                    if (i > 0 && result !== '') {
                        result += ', ';
                    }

                    result += item.factoryWarehouseID.toString();
                }
            }

            return result;
        };

        $scope.event.init();
    };
})();