//
// SUPPORT
//
$('.search-filter').keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.refresh();
    }
});
(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', DraftPackingListController);
    DraftPackingListController.$inject = ['$scope', '$cookieStore', 'dataService', '$timeout'];

    function DraftPackingListController($scope, $cookieStore, dataService, $timeout) {
        $scope.data = [];
        $scope.pageIndex = 1;
        $scope.totalPage = 0;
        $scope.filters = {
            DraftPackingListUD: '',
            InvoiceNo: '',
            ClientUD: '',
            ClientNM: ''
        };

        $scope.event = {
            init: init,
            search: search,
            remove: remove,
            refresh: refresh,
            clear: clear
        };

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

        function init() {
            dataService.searchFilter.pageSize = context.pageSize;
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
            $scope.event.search();
            jQuery('#content').show();
        };

        function search(isLoadMore) {
            dataService.searchFilter.filters = $scope.filters;
            dataService.search(
                function (data) {
                    debugger;
                    $scope.data = data.data.data;
                    $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.packingList = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.$apply();
                }
            );
        }

        function remove(item) {
            dataService.delete(
                item.draftPackingListID,
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

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;

            dataService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        };

        function clear() {
            $scope.filters = {
                DraftPackingListUD: '',
                InvoiceNo: '',
                ClientUD: '',
                ClientNM: ''
            };
            $scope.event.refresh();

        }

        $scope.event.init();
    };
    var searchGrid = jQuery('#searchTableContent').scrollableTable(
        function (currentPage) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                scope.pageIndex = currentPage;
                dataService.searchFilter.pageIndex = scope.pageIndex;
                scope.event.search();
            });
        },
        function (sortedBy, sortedDirection) {
            debugger;
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                dataService.searchFilter.sortedDirection = sortedDirection;
                scope.pageIndex = 1;
                dataService.searchFilter.pageIndex = scope.pageIndex;
                dataService.searchFilter.sortedBy = sortedBy;
                scope.event.search();
            });
        }
    );
})();