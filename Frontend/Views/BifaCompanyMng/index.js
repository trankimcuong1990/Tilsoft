jQuery('.search-filter').keypress(function (event) {
        if (event.keyCode === 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.refresh();
        }
    });

(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', bifaCompanyController);
    bifaCompanyController.$inject = ['$scope', '$timeout', '$cookieStore', 'dataService'];

    function bifaCompanyController($scope, $timeout, $cookieStore, bifaCompanyService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            bifaCompany: '',
            taxCode: '',
            bifaIndustry: '',
            bifaCity: '',
            isActive: null,
        };

        $scope.supportActive = [
            { activeID: true, activeNM: 'Actived' },
            { activeID: false, activeNM: 'Not Actived' }
        ];

        var cookieValue = $cookieStore.get(context.cookieID);
        if (cookieValue) {
            $scope.filters = cookieValue;
        };

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        $scope.event = {
            init: init,
            edit: edit,
            search: search,
            clear: clear,
            refresh: refresh,
            loadNextPage: loadNextPage,
            sortData: sortData,
            remove: remove,
        };

        $timeout(function () {
            $scope.event.init();
        }, 0);

        function init() {
            bifaCompanyService.serviceUrl = context.serviceUrl;
            bifaCompanyService.token = context.token;

            $scope.event.search();
        };

        function edit(id) {
            window.open(context.createOrEditUrl + id, '_blank');
        };

        function search(isLoadMore) {
            bifaCompanyService.searchFilter.pageSize = context.pageSize;

            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            bifaCompanyService.searchFilter.filters = $scope.filters;

            bifaCompanyService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);

                    $scope.totalPage = Math.ceil(data.totalRows / bifaCompanyService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    jQuery("#content").show();
                },
                function (error) {

                }
            );
        };

        function clear() {
            $scope.filters = {
                bifaCompany: '',
                taxCode: '',
                bifaIndustry: '',
                bifaCity: '',
                isActive: null,
            };

            $scope.event.refresh();
        };

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;
            bifaCompanyService.searchFilter.pageIndex = $scope.pageIndex = 1;

            $scope.event.search();
        };

        function loadNextPage() {
            if ($scope.pageIndex < $scope.totalPage) {
                $scope.pageIndex++;
                bifaCompanyService.searchFilter.pageIndex = $scope.pageIndex;

                $scope.event.search(true);
            }
        };

        function sortData(sortedBy, sortedDirection) {
            $scope.data = [];
            bifaCompanyService.searchFilter.sortedDirection = sortedDirection;
            bifaCompanyService.searchFilter.pageIndex = $scope.pageIndex = 1;
            bifaCompanyService.searchFilter.sortedBy = sortedBy;

            $scope.event.search();
        };

        function remove(id) {
        };
    };
})();