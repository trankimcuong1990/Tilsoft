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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', productBreakDownDefaultPALController);
    productBreakDownDefaultPALController.$inject = ['$scope', '$timeout', '$cookieStore', 'dataService'];

    function productBreakDownDefaultPALController($scope, $timeout, $cookieStore, productBreakDownDefaultPALService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            defaultNameSearch: '',
            defaultTypeSearch: '',
            defaultUnitSearch: '',
            getTypeSearch: '1'
        };

        $scope.event = {
            init: function () {
                productBreakDownDefaultPALService.serviceUrl = context.serviceUrl;
                productBreakDownDefaultPALService.token = context.token;
                productBreakDownDefaultPALService.searchFilter.pageSize = context.pageSize;

                $scope.event.search();
            },
            search: function () {
                productBreakDownDefaultPALService.searchFilter.filters = $scope.filters;

                productBreakDownDefaultPALService.search(
                    function (data) {
                        Array.prototype.push.apply($scope.data, data.data.productBreakDownDefaultCategoryPALSearchResult);

                        $scope.totalPage = Math.ceil(data.totalRows / productBreakDownDefaultPALService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;

                        jQuery('#content').show();
                    },
                    function (error) {
                    });
            },
            refresh: function () {
                $scope.data = [];
                $scope.pageIndex = 1;

                productBreakDownDefaultPALService.searchFilter.pageIndex = $scope.pageIndex;

                $scope.event.search();
            },
            clear: function () {
                $scope.filters = {
                    defaultNameSearch: '',
                    defaultTypeSearch: '',
                    defaultUnitSearch: '',
                    getTypeSearch: '1'
                };

                $scope.event.refresh();
            },
            delete: function (item) {
                productBreakDownDefaultPALService.delete(
                    item.productBreakDownDefaultCategoryID,
                    null,
                    function (data) {
                        if (data.message.type === 0) {
                            jsHelper.processMessage(data.message);

                            var index = $scope.data.indexOf(item);

                            $scope.data.splice(index, 1);
                            $scope.totalRows = $scope.totalRows - 1;
                        }
                    },
                    function (error) {
                    });
            },
            edit: function (id) {
                window.open(context.createUrl + id, '_blank');
            },
            view: function (id) {
                window.open(context.createUrl + id, '_blank');
            },
            create: function (id) {
                window.open(context.createUrl + id, '_blank');
            },
            loadNextPage: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    productBreakDownDefaultPALService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sortData: function (sortedBy, sortedDirection) {
                $scope.data = [];
                productBreakDownDefaultPALService.searchFilter.sortedDirection = sortedDirection;
                productBreakDownDefaultPALService.searchFilter.pageIndex = $scope.pageIndex = 1;
                productBreakDownDefaultPALService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            }
        };

        $timeout(function () {
            $scope.event.init();
        }, 0);
    }
})();