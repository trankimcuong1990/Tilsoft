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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', companyBranchMngController);
    companyBranchMngController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function companyBranchMngController($scope, $cookieStore, companyBranchMngService) {
        $scope.data = [];
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            Company: '',
            TaxCode: '',
            Address: '',
            Location: '',
            Phone: '',
            Fax: '',
            Email: '',
            Website: ''
        };
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;


        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    companyBranchMngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                companyBranchMngService.searchFilter.sortedDirection = sortedDirection;
                companyBranchMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                companyBranchMngService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };

        $scope.event = {
            init: init,
            search: search,
            refresh: refresh,
            clear: clear,
            remove: remove
        };


        function init() {
            companyBranchMngService.searchFilter.pageSize = context.pageSize;
            companyBranchMngService.serviceUrl = context.serviceUrl;
            companyBranchMngService.token = context.token;
            $scope.event.search();
        };

        function search(isLoadMore) {
            $scope.filters.UserID = context.userID;
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            companyBranchMngService.searchFilter.filters = $scope.filters;
            companyBranchMngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / companyBranchMngService.searchFilter.pageSize);
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
        };

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;

            companyBranchMngService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        };

        function clear() {
            $scope.filters = {
                Company: '',
                TaxCode: '',
                Address: '',
                Location: '',
                Phone: '',
                Fax: '',
                Email: '',
                Website: ''
            };
            $scope.event.refresh();

        };

        function remove(item) {
            companyBranchMngService.delete(
                item.companyID,
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
        };

        $scope.event.init();
    };
})();
