//jQuery('.search-filter').keypress(
//    function (event) {
//        if (event.keyCode === 13) {
//            var scope = angular.element(jQuery('body')).scope();
//            scope.event.reloadpage();
//        }
//    }
//);

(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', deliveryNoteDefaultController);
    deliveryNoteDefaultController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function deliveryNoteDefaultController($scope, $cookieStore, deliveryNoteDefaultService) {
        $scope.data = [];
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            deliveryNoteUD: '',
            deliveryNoteDate: '',
            deliveryNoteTypeID:1
             //deliveryNoteTypeID: 1, // fix for each screen: 1: default, 2: repair, 4: destroy
        };
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;
        

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    deliveryNoteDefaultService.searchFilter.pageIndex = $scope.pageIndex;
                    //$scope.event.activepage(true);
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                deliveryNoteDefaultService.searchFilter.sortedDirection = sortedDirection;
                deliveryNoteDefaultService.searchFilter.pageIndex = $scope.pageIndex = 1;
                deliveryNoteDefaultService.searchFilter.sortedBy = sortedBy;
                //$scope.event.activepage();
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
            deliveryNoteDefaultService.searchFilter.pageSize = context.pageSize;
            deliveryNoteDefaultService.serviceUrl = context.serviceUrl;
            deliveryNoteDefaultService.token = context.token;

            $scope.event.search();
        };

        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            deliveryNoteDefaultService.searchFilter.filters = $scope.filters;
            deliveryNoteDefaultService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);

                    $scope.totalPage = Math.ceil(data.totalRows / deliveryNoteDefaultService.searchFilter.pageSize);
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

            deliveryNoteDefaultService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        };

        function clear() {
            $scope.filters = {
                deliveryNoteUD: '',
                deliveryNoteDate2: '',
                deliveryNoteTypeID: 1
            };
            $scope.event.refresh();

        }

        function remove(item) {
            deliveryNoteDefaultService.delete(
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