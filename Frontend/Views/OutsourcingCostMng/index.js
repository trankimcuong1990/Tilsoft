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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', OutsourceCostMngController);
    OutsourceCostMngController.$inject = ['$scope', '$cookieStore', 'dataService', '$timeout'];

    function OutsourceCostMngController($scope, $cookieStore, outsourceCostMngService, $timeout) {
        $scope.data = [];
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;
        $scope.workCenters = [];

        $scope.filters = {
            OutsourcingCostUD: '',
            OutsourcingCostNM: '',
            OutsourcingCostNMEN: '',
            WorkCenterID: '',
            IsAdditionalCost: ''
        };
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;


        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    outsourceCostMngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                outsourceCostMngService.searchFilter.sortedDirection = sortedDirection;
                outsourceCostMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                outsourceCostMngService.searchFilter.sortedBy = sortedBy;
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
            outsourceCostMngService.searchFilter.pageSize = context.pageSize;
            outsourceCostMngService.serviceUrl = context.serviceUrl;
            outsourceCostMngService.token = context.token;
            $scope.event.search();
        };

        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            outsourceCostMngService.searchFilter.filters = $scope.filters;
            outsourceCostMngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / outsourceCostMngService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    $timeout(function () {
                        $scope.workCenters = data.data.workCenters;
                    }, 0);
                   
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

            outsourceCostMngService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        };

        function clear() {
            $scope.filters = {
                OutsourcingCostUD: '',
                OutsourcingCostNM: '',
                OutsourcingCostNMEN: '',
                WorkCenterID: '',
                IsAdditionalCost: ''
            };
            $scope.event.refresh();

        }

        function remove(item) {
            outsourceCostMngService.delete(
                item.outsourcingCostID,
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
