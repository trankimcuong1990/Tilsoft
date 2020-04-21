jQuery('.search-filter').keypress(
    function (event) {
        if (event.keyCode === 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.reloaded();
        }
    }
);

(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', deliveryNoteRepairAssemplyController);
    deliveryNoteRepairAssemplyController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function deliveryNoteRepairAssemplyController($scope, $cookieStore, deliveryNoteRepairAssemplyService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            deliveryNoteTypeID: 2, // fix for each screen: 1: default, 2: repair, 4: destroy
            deliveryNoteTypeUD: ''
        };

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    deliveryNoteRepairAssemplyService.searchFilter.pageIndex = $scope.pageIndex;

                    $scope.event.activepage(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                deliveryNoteRepairAssemplyService.searchFilter.sortedDirection = sortedDirection;
                deliveryNoteRepairAssemplyService.searchFilter.pageIndex = $scope.pageIndex = 1;
                deliveryNoteRepairAssemplyService.searchFilter.sortedBy = sortedBy;

                $scope.event.activepage();
            },
            isTriggered: false
        };

        $scope.event = {
            init: init,
            search: search,
            refresh: refresh,
            reloaded: reloaded,
            clear: clear,
            remove: remove,
            deleterow: deleterow
        };

        function init() {
            deliveryNoteRepairAssemplyService.searchFilter.pageSize = context.pageSize;
            deliveryNoteRepairAssemplyService.serviceUrl = context.serviceUrl;
            deliveryNoteRepairAssemplyService.token = context.token;

            $scope.event.search(false);
        };

        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            deliveryNoteRepairAssemplyService.searchFilter.filters = $scope.filters;
            deliveryNoteRepairAssemplyService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);

                    $scope.totalPage = Math.ceil(data.totalRows / deliveryNoteRepairAssemplyService.searchFilter.pageSize);
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

            deliveryNoteRepairAssemplyService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        };

        function reloaded() {
            //debugger
            $scope.data = [];
            deliveryNoteRepairAssemplyService.searchFilter.pageIndex = $scope.pageIndex = 1;

            $scope.event.search();
        };

        function deleterow(id, index) {
            deliveryNoteRepairAssemplyService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        if (index >= 0) {
                            $scope.data.splice(index, 1);
                            $scope.totalRows--;
                        }
                    }
                },
                function (error) {
                    jsHelper.showMessage("warning", error);
                });
        };

        function clear() {

        };

        function remove() {

        };

        $scope.event.init();
    };
})();
