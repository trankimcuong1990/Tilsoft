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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', POLMngController);
    POLMngController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function POLMngController($scope, $cookieStore, POLMngService) {
        //
        // property
        //
        //$scope.data = null;
        $scope.filters = {
            PoLUD: '',
            PoLname: ''
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
                    POLMngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                POLMngService.searchFilter.sortedDirection = sortedDirection;
                POLMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                POLMngService.searchFilter.sortedBy = sortedBy;
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
            POLMngService.searchFilter.pageSize = context.pageSize;
            POLMngService.serviceUrl = context.serviceUrl;
            POLMngService.token = context.token;
            $scope.event.search();
        };

        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            POLMngService.searchFilter.filters = $scope.filters;
            POLMngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / POLMngService.searchFilter.pageSize);
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

            POLMngService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        };

        function clear() {
            $scope.filters = {
                PoLUD: '',
                PoLname: ''
            };
            $scope.event.refresh();

        }

        function remove(item) {
            POLMngService.delete(
                item.polid,
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