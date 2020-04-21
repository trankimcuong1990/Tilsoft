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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', moduleStatusMngController);
    moduleStatusMngController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function moduleStatusMngController($scope, $cookieStore, moduleStatusMngService) {
        $scope.data = [];
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;
        $scope.modules = [];

        $scope.filters = {
            statusUD: '',
            statusNM: '',
            moduleID: '',
            isActived: ''
        };
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;


        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    moduleStatusMngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                moduleStatusMngService.searchFilter.sortedDirection = sortedDirection;
                moduleStatusMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                moduleStatusMngService.searchFilter.sortedBy = sortedBy;
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
            moduleStatusMngService.searchFilter.pageSize = context.pageSize;
            moduleStatusMngService.serviceUrl = context.serviceUrl;
            moduleStatusMngService.token = context.token;
            moduleStatusMngService.getInit(
                function (data) {
                    $scope.modules = data.data.modules;
                    $scope.event.search();
                },
                function (error) {
                    // do nothing
                });
        }

        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            moduleStatusMngService.searchFilter.filters = $scope.filters;
            moduleStatusMngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);

                    $scope.totalPage = Math.ceil(data.totalRows / moduleStatusMngService.searchFilter.pageSize);
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

            moduleStatusMngService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        }

        function clear() {
            $scope.filters = {
                statusUD: '',
                statusNM: '',
                moduleID: '',
                isActived: ''
            };
            $scope.event.refresh();

        }

        function remove(item) {
            moduleStatusMngService.delete(
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

        $scope.event.init();
    };
})();
