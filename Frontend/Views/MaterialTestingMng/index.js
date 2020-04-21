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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', materialTestingController);
    materialTestingController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function materialTestingController($scope, $cookieStore, materialTestingService) {
        $scope.data = [];
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageindex = 1;

        $scope.yesNoValues = null;

        $scope.filters = {
            materialTestReportUD: '',
            IsEnabled: '',
            materialOptionUD: '',
            materialOptionNM: '',
            clientUD: '',
            friendlyName: '',
            TestStandardNM: '',
            testDate: ''
        };
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;


        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    materialTestingService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                materialTestingService.searchFilter.sortedDirection = sortedDirection;
                materialTestingService.searchFilter.pageIndex = $scope.pageIndex = 1;
                materialTestingService.searchFilter.sortedBy = sortedBy;
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
            materialTestingService.searchFilter.pageSize = context.pageSize;
            materialTestingService.serviceUrl = context.serviceUrl;
            materialTestingService.token = context.token;
            $scope.event.search();
        };

        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            materialTestingService.searchFilter.filters = $scope.filters;
            materialTestingService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / materialTestingService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    $scope.yesNoValues = data.data.yesNoValues;

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

            materialTestingService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        };

        function clear() {
            $scope.filters = {
                materialTestReportUD: '',
                IsEnabled: '',
                materialOptionUD: '',
                materialOptionNM: '',
                clientUD: '',
                friendlyName: '',
                TestStandardNM: '',
                testDate: ''
            };
            $scope.event.refresh();

        }

        function remove(item) {
            materialTestingService.delete(
                item.materialTestReportID,
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
