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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', draftCommercialInvoiceMngController);
    draftCommercialInvoiceMngController.$inject = ['$scope', '$cookieStore', 'dataService', '$timeout'];

    function draftCommercialInvoiceMngController($scope, $cookieStore, draftCommercialInvoiceMngService, $timeout) {
        $scope.data = [];
        $scope.seasons = [];
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;
        $scope.workCenters = [];

        $scope.filters = {
            invoiceNo: '',
            clientUD: '',
            clientNM: '',
            clientInvoiceNo: '',
            season: ''
        };
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;


        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    draftCommercialInvoiceMngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                draftCommercialInvoiceMngService.searchFilter.sortedDirection = sortedDirection;
                draftCommercialInvoiceMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                draftCommercialInvoiceMngService.searchFilter.sortedBy = sortedBy;
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
            draftCommercialInvoiceMngService.searchFilter.pageSize = context.pageSize;
            draftCommercialInvoiceMngService.serviceUrl = context.serviceUrl;
            draftCommercialInvoiceMngService.token = context.token;
            $scope.event.search();
        };

        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            draftCommercialInvoiceMngService.searchFilter.filters = $scope.filters;
            draftCommercialInvoiceMngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / draftCommercialInvoiceMngService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    $scope.seasons = data.data.seasons;

                    //$timeout(function () {
                    //    $scope.workCenters = data.data.workCenters;
                    //}, 0);

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

            draftCommercialInvoiceMngService.searchFilter.pageIndex = 1;

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
            draftCommercialInvoiceMngService.delete(
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
