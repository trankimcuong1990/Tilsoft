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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', PODMngController);
    PODMngController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function PODMngController($scope, $cookieStore, PODMngService) {
        //
        // property
        //
        //$scope.data = null;
        $scope.filters = {
            PoDUD: '',
            PoDName: ''
        };
        $scope.data = [];
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;


        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    PODMngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                PODMngService.searchFilter.sortedDirection = sortedDirection;
                PODMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                PODMngService.searchFilter.sortedBy = sortedBy;
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

        //
        // event
        //
        function init() {
            PODMngService.searchFilter.pageSize = context.pageSize;
            PODMngService.serviceUrl = context.serviceUrl;
            PODMngService.token = context.token;
            $scope.event.search();
        };

        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            PODMngService.searchFilter.filters = $scope.filters;
            PODMngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / PODMngService.searchFilter.pageSize);
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

            PODMngService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        };

        function clear() {
            $scope.filters = {
                PoDUD: '',
                PoDName: ''
            };
            $scope.event.refresh();

        }

        function remove(item) {
            PODMngService.delete(
                item.podid,
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
        //
        // method
        //


        //
        // init
        //
        $scope.event.init();
    };
})();