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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', outSourcingCostTypeMngController);
    outSourcingCostTypeMngController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function outSourcingCostTypeMngController($scope, $cookieStore, outSourcingCostTypeMngService) {
        $scope.data = [];
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;
        $scope.productionItemGroups = [];

        $scope.filters = {
            outsourcingCostTypeUD: '',
            outsourcingCostTypeNM: '',
            outsourcingCostTypeNMEN: '',
            productionItemGroupID: ''
        };
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;


        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    outSourcingCostTypeMngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                outSourcingCostTypeMngService.searchFilter.sortedDirection = sortedDirection;
                outSourcingCostTypeMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                outSourcingCostTypeMngService.searchFilter.sortedBy = sortedBy;
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
            outSourcingCostTypeMngService.searchFilter.pageSize = context.pageSize;
            outSourcingCostTypeMngService.serviceUrl = context.serviceUrl;
            outSourcingCostTypeMngService.token = context.token;
            outSourcingCostTypeMngService.getInit(
                function (data) {
                    $scope.productionItemGroups = data.data.productionItemGroups;
                    $scope.event.search();
                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            outSourcingCostTypeMngService.searchFilter.filters = $scope.filters;
            outSourcingCostTypeMngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / outSourcingCostTypeMngService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    //jQuery('#content').show();

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

            outSourcingCostTypeMngService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        };

        function clear() {
            $scope.filters = {
                outsourcingCostTypeUD: '',
                outsourcingCostTypeNM: '',
                outsourcingCostTypeNMEN: '',
                productionItemGroupID: ''
            };
            $scope.event.refresh();

        }

        function remove(item) {
            outSourcingCostTypeMngService.delete(
                item.outsourcingCostTypeID,
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
