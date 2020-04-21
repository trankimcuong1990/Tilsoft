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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', qcReportMngController);
    qcReportMngController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function qcReportMngController($scope, $cookieStore, dataService) {
        // variable
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;
        $scope.factories = null;

        //Filters
        $scope.filters = {
            statusID: null,
            clientUD: '',
            factoryOrderUD: '',
            articleCode: '',
            description: '',
            factoryUD: '',
            statusNM:''

        };

        // grid handler
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
        };


        // event
        $scope.event = {
            search: search,
            refresh: refresh,
            clear: clear,
            remove: remove

        }
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        function search(isLoadMore) {
            dataService.searchFilter.pageSize = context.pageSize;
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;

            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.gridHandler.isTriggered = false;
            dataService.searchFilter.filters = $scope.filters;
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            dataService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.qaqcSearchResultDTOs);
                    $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    //$scope.factories = data.data.factories;

                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {

                }
            );

        };

        //Event Clear
        function clear() {
            $scope.filters = {
                statusID: null,
                clientUD: '',
                factoryOrderUD: '',
                articleCode: '',
                description: '',
                factoryUD: '',
                statusNM: ''
            };
            $scope.event.refresh();
        };

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.search();

        };

        function remove(item) {
            dataService.delete(
                item.qcReportID,
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

        // default event
        $scope.event.search();
    }
})();