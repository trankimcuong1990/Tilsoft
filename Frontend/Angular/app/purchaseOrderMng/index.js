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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', purchaseOrderController);
    purchaseOrderController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function purchaseOrderController($scope, $cookieStore, purchaseOrderService) {
        $scope.data = [];
        $scope.POStatus = [];
        $scope.seasons = [];
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            purchaseOrderUD: '',
            purchaseOrderDate: '',
            factoryRawMaterialShortNM: '',
            purchaseRequestUD: '',
            eTA: '',
            status: '',
            remark: '',
            purchaseOrderIDs: ''
        };
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;


        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    purchaseOrderService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                purchaseOrderService.searchFilter.sortedDirection = sortedDirection;
                purchaseOrderService.searchFilter.pageIndex = $scope.pageIndex = 1;
                purchaseOrderService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };

        $scope.event = {
            init: init,
            search: search,
            refresh: refresh,
            clear: clear,
            remove: remove,
        };

        function init() {
            purchaseOrderService.searchFilter.pageSize = context.pageSize;
            purchaseOrderService.serviceUrl = context.serviceUrl;
            purchaseOrderService.token = context.token;
            $scope.filters.purchaseOrderIDs = context.purchaseOrderIDs;
            $scope.event.search();
        }

        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            purchaseOrderService.searchFilter.filters = $scope.filters;
            purchaseOrderService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.POStatus = data.data.purchaseOrderStatus;
                    $scope.seasons = data.data.seasons;
                    $scope.totalPage = Math.ceil(data.totalRows / purchaseOrderService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }

                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;

            purchaseOrderService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        }

        function clear() {
            $scope.filters = {
                purchaseOrderUD: '',
                purchaseOrderDate: '',
                factoryRawMaterialShortNM: '',
                purchaseRequestUD: '',
                eTA: '',
                purchaseOrderIDs: ''
            };
            $scope.event.refresh();

        }

        function remove(item) {
            purchaseOrderService.delete(
                item.purchaseOrderID,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);

                    if (data.message.type === 0) {
                        var index = $scope.data.indexOf(item);

                        $scope.data.splice(index, 1);
                        $scope.totalRows = $scope.totalRows - 1;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
        $scope.method = {
            totalAmountPO: function () {
                debugger;
                var total = 0;
                for (var i = 0; i < $scope.data.length; i++) {
                    var item = $scope.data[i];
                    total = total + item.amount;
                }
                return total;
            }
        };
        $scope.event.init();
    };
})();
