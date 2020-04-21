$('.search-filter').keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element($('body')).scope();
        scope.event.research();
    }
});

(function () {
    'use strict';

    angular
        .module('tilsoftApp', ['ngCookies', 'avs-directives'])
        .controller('tilsoftController', cashBookReceiptController);
    cashBookReceiptController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function cashBookReceiptController($scope, $cookieStore, cashBookReceiptService) {
        $scope.data = [];
        $scope.pageIndex = 1;
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.currentRows = 0;

        $scope.supportType = [];
        $scope.supportSourceOfFlow = [];
        $scope.supportLocation = [];
        $scope.supportPostCost = [];
        $scope.supportCostItem = [];
        $scope.supportCostItemDetail = [];
        $scope.supportPaidBy = [];

        $scope.filters = {
            Code: '',
            FromDate: context.firstMonth,
            ToDate: context.lastMonth,
            Type: null,
            SourceOfFlow: null,
            Location: null,
            PaidBy: null,
            PostCost: null,
            CostItem: null,
            CostItemDetail: null
        };

        $scope.exports = {
            withMonth: '',
            withYear: ''
        };

        $scope.gridHandler = {
            loadMore: function loadMore() {
                if ($scope.pageIndex < $scope.totalPage) {
                    cashBookReceiptService.searchFilter.pageIndex = ++$scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function sort(orderBy, orderDirection) {
                $scope.data = [];
                cashBookReceiptService.searchFilter.sortedBy = orderBy;
                cashBookReceiptService.searchFilter.sortedDirection = orderDirection;
                cashBookReceiptService.searchFilter.pageIndex = $scope.pageIndex = 1;
                $scope.event.search();
            },
            isTriggered: false
        };

        var cookieValue = $cookieStore.get(context.cookieID);
        if (cookieValue) {
            $scope.filters = cookieValue;
        };

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        $scope.event = {
            init: init,
            search: search,
            research: research,
            clear: clear,
            remove: remove,
            exportFile: exportFile,
            open: open,
            close: close
        };

        function init() {
            cashBookReceiptService.token = context.token;
            cashBookReceiptService.serviceUrl = context.serviceUrl;
            cashBookReceiptService.searchFilter.pageSize = context.pageSize;
            cashBookReceiptService.getInit(
                function (data) {
                    $scope.supportType = data.data.cashBookTypes;
                    $scope.supportSourceOfFlow = data.data.cashBookSourceOfFlows;
                    $scope.supportLocation = data.data.cashBookLocations;
                    $scope.supportPostCost = data.data.cashBookPostCosts;
                    $scope.supportCostItem = data.data.cashBookCostItems;
                    $scope.supportCostItemDetail = data.data.cashBookCostItemDetails;
                    $scope.supportPaidBy = data.data.cashBookPaidBys; console.log($scope.supportPaidBy);
                    $scope.event.search();
                },
                function (error) {
                })
        };

        function convertStringtoDate(dateStr) {
            var dateString = dateStr;

            var dateParts = dateString.split("/");

            var dateObject = new Date(dateParts[2], dateParts[1] - 1, dateParts[0]);

            return dateObject;
        };

        function search(loadMore) {
            var fromDate = convertStringtoDate($scope.filters.FromDate);
            var toDate = convertStringtoDate($scope.filters.ToDate);
            
            if (toDate < fromDate) {
                jQuery('#content').show();
                jsHelper.showMessage('warning', 'From Date is lager than To Date!');
                return false;
            }

            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;
            cashBookReceiptService.searchFilter.filters = $scope.filters;
            cashBookReceiptService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data); console.log($scope.data);

                    $scope.totalPage = Math.ceil(data.totalRows / cashBookReceiptService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    var subNo = $scope.totalRows - $scope.currentRows;
                    if (subNo > cashBookReceiptService.searchFilter.pageSize) {
                        $scope.currentRows = parseInt($scope.currentRows) + parseInt(cashBookReceiptService.searchFilter.pageSize);
                    } else {
                        $scope.currentRows = parseInt($scope.currentRows) + parseInt(subNo);
                    }

                    jQuery('#content').show();

                    if (!loadMore) {
                        $scope.gridHandler.goTop();
                    }

                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                })
        };

        function research() {
            $scope.data = [];
            cashBookReceiptService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.search();
        };

        function clear() {
            $scope.filters = {
                Code: '',
                FromDate: context.firstMonth,
                ToDate: context.lastMonth,
                Type: null,
                SourceOfFlow: null,
                Location: null,
                PaidBy: null,
                PostCost: null,
                CostItem: null,
                CostItemDetail: null
            };
            $scope.event.research();
        };

        function remove(id, index) {
            if (id > 0) {
                cashBookReceiptService.delete(
                    id,
                    null,
                    function (data) {
                        $scope.data.splice(index, 1);

                        $scope.totalRows = $scope.totalRows - 1;
                        $scope.currentRows = $scope.currentRows - 1;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
        };

        function open() {
            $scope.exports = {
                withMonth: parseInt(context.month),
                withYear: parseInt(context.year)
            };

            $('#frmExportCashBook').modal();
        };

        function close() {
            $('#frmExportCashBook').modal('hide');
        };

        function exportFile() {
            cashBookReceiptService.exportCashBook(
                {
                    filters: {
                        Month: $scope.exports.withMonth,
                        Year: $scope.exports.withYear
                    }
                },
                function (data) {
                    debugger;
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        $scope.event.init();
    }
})();