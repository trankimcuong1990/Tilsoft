jQuery('.search-filter').keypress(
    function (event) {
        if (event.keyCode === 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.reloadpage();
        }
    }
);

(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', deliveryNoteToBeDestroyedController);
    deliveryNoteToBeDestroyedController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function deliveryNoteToBeDestroyedController($scope, $cookieStore, deliveryNoteToBeDestroyedService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            deliveryNoteUD: '',
            deliveryNoteTypeID: 4, // fix for each screen: 1: default, 2: repair, 4: destroy
        };

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    deliveryNoteToBeDestroyedService.searchFilter.pageIndex = $scope.pageIndex;

                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                deliveryNoteToBeDestroyedService.searchFilter.sortedDirection = sortedDirection;
                deliveryNoteToBeDestroyedService.searchFilter.pageIndex = $scope.pageIndex = 1;
                deliveryNoteToBeDestroyedService.searchFilter.sortedBy = sortedBy;

                $scope.event.search(false);
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
            deliveryNoteToBeDestroyedService.searchFilter.pageSize = context.pageSize;
            deliveryNoteToBeDestroyedService.serviceUrl = context.serviceUrl;
            deliveryNoteToBeDestroyedService.token = context.token;

            $scope.event.search(false);
        };

        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);
            
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            deliveryNoteToBeDestroyedService.searchFilter.filters = $scope.filters;
            deliveryNoteToBeDestroyedService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);

                    $scope.totalPage = Math.ceil(data.totalRows / deliveryNoteToBeDestroyedService.searchFilter.pageSize);
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

            deliveryNoteToBeDestroyedService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        };

        function clear() {
            $scope.filters = {
                deliveryNoteUD: '',
            };

            $scope.event.refresh();
        };

        function remove(item) {
            deliveryNoteToBeDestroyedService.delete(
                item.deliveryNoteID,
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