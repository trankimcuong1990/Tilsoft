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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', eurofarSampleCollectionMngController);
    eurofarSampleCollectionMngController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function eurofarSampleCollectionMngController($scope, $cookieStore, eurofarSampleCollectionMngService) {
        $scope.data = [];
        $scope.seasons = jsHelper.getSeasons();
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            client: '',
            description: '',
            sampleOrderUD: '',
            season:''
        };
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;


        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    eurofarSampleCollectionMngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                eurofarSampleCollectionMngService.searchFilter.sortedDirection = sortedDirection;
                eurofarSampleCollectionMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                eurofarSampleCollectionMngService.searchFilter.sortedBy = sortedBy;
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
            exportExcel: function ($event) {
                $event.preventDefault();

                eurofarSampleCollectionMngService.exportExcel({
                    filters: {
                        client: $scope.filters.client,
                        description: $scope.filters.description,
                        sampleOrderUD: $scope.filters.sampleOrderUD,
                        season: $scope.filters.season
                    }
                },
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                });
            },
        };

        function init() {
            eurofarSampleCollectionMngService.searchFilter.pageSize = context.pageSize;
            eurofarSampleCollectionMngService.serviceUrl = context.serviceUrl;
            eurofarSampleCollectionMngService.token = context.token;
            $scope.event.search();
        };

        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            eurofarSampleCollectionMngService.searchFilter.filters = $scope.filters;
            eurofarSampleCollectionMngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / eurofarSampleCollectionMngService.searchFilter.pageSize);
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

            eurofarSampleCollectionMngService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        };

        function clear() {
            $scope.filters = {
                client: '',
                description: '',
                sampleOrderUD: '',
                season: ''
            };
            $scope.event.refresh();

        }

        function remove(item) {
            eurofarSampleCollectionMngService.delete(
                item.sampleProductID,
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
