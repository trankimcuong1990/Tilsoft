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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', productBreakDownDefaultCategoryController);
    productBreakDownDefaultCategoryController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function productBreakDownDefaultCategoryController($scope, $cookieStore, dataService) {
        /// zone variable
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        /// zone filters
        $scope.filters = {
            name: '',
            typeName: '',
            unit: '',
            typeSearch: '1'
        }

        /// zone grid-handler
        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    dataService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                dataService.searchFilter.sortedDirection = sortedDirection;
                dataService.searchFilter.pageIndex = $scope.pageIndex = 1;
                dataService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        }

        /// zone event
        $scope.event = {
            search: function (isLoadMore) {
                // define service and token
                dataService.serviceUrl = context.serviceUrl;
                dataService.token = context.token;

                dataService.searchFilter.filters = $scope.filters;

                // call api
                dataService.search(
                    function (data) {
                        Array.prototype.push.apply($scope.data, data.data.defaultData);

                        $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;

                        $('#content').show();

                        if (!isLoadMore) {
                            $scope.gridHandler.goTop();
                        }

                        $scope.gridHandler.isTriggered = true;
                    },
                    function (error) {
                    })
            },
            clear: function () {
                $scope.filters = {
                    name: '',
                    typeName: '',
                    unit: '',
                    typeSearch: '1'
                }

                $scope.event.refresh();
            },
            refresh: function () {
                $scope.data = [];
                $scope.pageIndex = 1;
                dataService.searchFilter.pageIndex = $scope.pageIndex = 1;

                $scope.event.search();
            },
            remove: function (item) {
                dataService.delete(
                    item.productBreakDownDefaultCategoryID,
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
                )
            }
        }

        /// zone default
        $scope.event.search(false);
    }
})();