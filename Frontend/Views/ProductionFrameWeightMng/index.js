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
    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', productionFrameWeightMngController);
    productionFrameWeightMngController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function productionFrameWeightMngController($scope, $cookieStore, productionFrameWeightMngService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        //
        //filters with key word
        //
        $scope.filters = {
            ProductionItem: '',
            ClientUD: '',
            ProformaInvoiceNo: '',
            WorkOrderUD: ''
        };

        //GridHandler
        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    productionFrameWeightMngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                productionFrameWeightMngService.searchFilter.sortedDirection = sortedDirection;
                productionFrameWeightMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                productionFrameWeightMngService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };

        //
        //Event
        //
        $scope.event = {
            init: init,
            search: search,
            refresh: refresh,
            clear: clear,
            remove: remove,
            exportExcel: exportExcel
        };

        //
        //Init
        //
        function init() {
            productionFrameWeightMngService.searchFilter.pageSize = context.pageSize;
            productionFrameWeightMngService.serviceUrl = context.serviceUrl;
            productionFrameWeightMngService.token = context.token;

            $scope.event.search();
        };


        //
        //Load Data
        //
        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;
            productionFrameWeightMngService.searchFilter.filters = $scope.filters;
            productionFrameWeightMngService.search(

                function (data) {
                    Array.prototype.push.apply($scope.data, data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / productionFrameWeightMngService.searchFilter.pageSize);
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

        //
        //refresh page
        //
        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;
            productionFrameWeightMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.search();
        };

        //
        //clear Search
        //
        function clear() {
            $scope.filters = {
                ProductionItem: '',
                ClientUD: '',
                ProformaInvoiceNo: '',
                WorkOrderUD: ''
            };
            $scope.event.refresh();
        };

        //
        //Remove Function
        //
        function remove(item) {
            productionFrameWeightMngService.delete(
                item.productionFrameWeightID,
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
        //Export Excel
        //
        function exportExcel() {
            productionFrameWeightMngService.generateExcel(
                $scope.filters.WorkOrderUD,
                    $scope.filters.ClientUD,
                    $scope.filters.ProformaInvoiceNo,
                    $scope.filters.ProductionItem,
              
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                });
        };

        $scope.event.init();
    };
})();