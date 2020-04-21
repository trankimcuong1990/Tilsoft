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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', qcReportMngController);
    qcReportMngController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function qcReportMngController($scope, $cookieStore, qcReportMngService) {
        // variable
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;
        $scope.factories = null;

        //Filters
        $scope.filters = {
            QCReportUD: '',
            FactoryID: null,
            ClientUD: '',
            ArticleCode: '',
            ProformaInvoiceNo: ''

        };

        // grid handler
        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    qcReportMngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                qcReportMngService.searchFilter.sortedDirection = sortedDirection;
                qcReportMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                qcReportMngService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };


        // event
        $scope.event = {
            search: search,
            refresh: refresh,
            clear: clear,
            remove: remove,

        }
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        function search(isLoadMore) {  
            qcReportMngService.searchFilter.pageSize = context.pageSize;
            qcReportMngService.serviceUrl = context.serviceUrl;
            qcReportMngService.token = context.token;

            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.gridHandler.isTriggered = false;
            qcReportMngService.searchFilter.filters = $scope.filters;
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            qcReportMngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / qcReportMngService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    $scope.factories = data.data.factories;

                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {

                }
            );
                  
        };

        //Event Clear
        function clear() {
            $scope.filters = {
                QCReportUD: '',
                FactoryID: null,
                ClientUD: '',
                ArticleCode: '',
                ProformaInvoiceNo: ''
            };
            $scope.event.refresh();
        };

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;
            qcReportMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.search();

        };

        function remove(item) {
            qcReportMngService.delete(
                item.qcReportID,
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
                    //not thing to do
                }
            );
        };

        // default event
        $scope.event.search();
    }
})();