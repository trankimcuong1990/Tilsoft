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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', transportationServiceMngController);
    transportationServiceMngController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function transportationServiceMngController($scope, $cookieStore, transportationServiceMngService) {
        $scope.data = [];
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            TransportationServiceNM: '',
            PlateNumber: '',
            DriverName: '',
            MobileNumber: ''

        };
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;


        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    transportationServiceMngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                transportationServiceMngService.searchFilter.sortedDirection = sortedDirection;
                transportationServiceMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                transportationServiceMngService.searchFilter.sortedBy = sortedBy;
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
            transportationServiceMngService.searchFilter.pageSize = context.pageSize;
            transportationServiceMngService.serviceUrl = context.serviceUrl;
            transportationServiceMngService.token = context.token;
            $scope.event.search();
        };

        function search(isLoadMore) {
            jQuery('#content').show();
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            transportationServiceMngService.searchFilter.filters = $scope.filters;
            transportationServiceMngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / transportationServiceMngService.searchFilter.pageSize);
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

            transportationServiceMngService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        };

        function clear() {
            $scope.filters = {
                TransportationServiceNM: '',
                PlateNumber: '',
                DriverName: '',
                MobileNumber: ''
            };
            $scope.event.refresh();

        }

        function remove(item) {
            transportationServiceMngService.delete(
                item.transportationServiceID,
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
