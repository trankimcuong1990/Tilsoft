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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', productBreakDownController);
    productBreakDownController.$inject = ['$scope', '$timeout', '$cookieStore', 'dataService']

    function productBreakDownController($scope, $timeout, $cookieStore, productBreakDownService) {

        //Varible Zone
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        //Filters
        $scope.filters = {
            clientSearch: '',
            modelSearch: '',
            productSearch: '',
            typeSearch: '2'
        };

        //event
        $scope.event = {
            search: search,
            clear: clear,
            refresh: refresh,
            remove: remove,
            loadNextPage: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    productBreakDownService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sortData: function (sortedBy, sortedDirection) {
                $scope.data = [];
                productBreakDownService.searchFilter.sortedDirection = sortedDirection;
                productBreakDownService.searchFilter.pageIndex = $scope.pageIndex = 1;
                productBreakDownService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            }
        };

        //Load Data
        function search(isLoadMore) {
            productBreakDownService.searchFilter.pageSize = context.pageSize;
            productBreakDownService.serviceUrl = context.serviceUrl;
            productBreakDownService.token = context.token;

            $cookieStore.put(context.cookieID, $scope.filters);
            productBreakDownService.searchFilter.filters = $scope.filters;

            productBreakDownService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.mainData);

                    $scope.totalPage = Math.ceil(data.totalRows / productBreakDownService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    jQuery("#content").show();
                },
                function (error) {

                }
            );
        }

        //Event Clear
        function clear() {
            $scope.filters = {
                clientSearch: '',
                modelSearch: '',
                productSearch: '',
                typeSearch: '2'
            };

            $scope.event.refresh();
        }

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;
            productBreakDownService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.search();

        }

        function remove(item) {
            productBreakDownService.delete(
                item.productBreakDownID,
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
                    //not thing to do
                }
            );
        };
        //Event load
        $scope.event.search();

        //Creat Service
        function createservices() {
            productBreakDownService.searchFilter.pageSize = context.pageSize;
            productBreakDownService.searchFilter.serviceUrl = context.serviceUrl;
            productBreakDownService.token = context.token;
        }
    }
})();