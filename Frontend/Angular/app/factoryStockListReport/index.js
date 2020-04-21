jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives', 'furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', function ($scope, $cookieStore) {

    $scope.data = null;
    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;

    $scope.filters = {
        factoryUD: '',
        clientUD: '',
        proformaInvoiceNo: '',
        lds: '',
        articleCode: '',
        description: '',
        factoryID: null,
        isIncludeImage: true,
    };
    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;

    $scope.factories = [];
    //total value
    $scope.totalStockQnt = 0;
    $scope.totalStockQntIn40HC = 0;
    $scope.totalProducedQnt = 0;
    $scope.totalNotPackedQnt = 0;
    $scope.totalPackedQnt = 0;

    //sub total value
    $scope.subTotalStockQnt = 0;
    $scope.subTotalStockQntIn40HC = 0;
    $scope.subTotalProducedQnt = 0;
    $scope.subTotalNotPackedQnt = 0;
    $scope.subTotalPackedQnt = 0;
    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
            if ($scope.pageIndex < $scope.totalPage) {
                $scope.pageIndex++;
                jsonService.searchFilter.pageIndex = $scope.pageIndex;
                $scope.event.search(true);
            }
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.data = [];
            jsonService.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = scope.pageIndex;
            jsonService.searchFilter.sortedBy = sortedBy;
            $scope.event.search(true);
        },
        isTriggered: false
    };

    //
    // event
    //
    $scope.event = {
        reload: function () {
            //reset main data
            $scope.data = [];

            //assign pager for search result
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = 1;
            jsonService.searchFilter.sortedBy = 'TotalStockQntIn40HC';
            jsonService.searchFilter.sortedDirection = 'DESC';

            //search data
            $scope.event.search();
        },
        search: function (isLoadMore) {
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.factories = data.data.factories;

                    //total value
                    $scope.totalStockQnt = data.data.totalStockQnt;
                    $scope.totalStockQntIn40HC = data.data.totalStockQntIn40HC;
                    $scope.totalProducedQnt = data.data.totalProducedQnt;
                    $scope.totalNotPackedQnt = data.data.totalNotPackedQnt;
                    $scope.totalPackedQnt = data.data.totalPackedQnt;

                    //sub total value
                    $scope.subTotalStockQnt = data.data.subTotalStockQnt;
                    $scope.subTotalStockQntIn40HC = data.data.subTotalStockQntIn40HC;
                    $scope.subTotalProducedQnt = data.data.subTotalProducedQnt;
                    $scope.subTotalNotPackedQnt = data.data.subTotalNotPackedQnt;
                    $scope.subTotalPackedQnt = data.data.subTotalPackedQnt;

                    //get total row
                    $scope.totalRows = data.totalRows;
                    //cal total page
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.$apply();
                    jQuery('#content').show();
                    $("#factoryID").select2();

                    //gridHandler
                    $scope.gridHandler.refresh();
                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.$apply();
                }
            );
        },
        clearFilter: function () {
            $scope.filters = {
                factoryUD: '',
                clientUD: '',
                proformaInvoiceNo: '',
                lds: '',
                articleCode: '',
                description: '',
            };
            $scope.event.reload();
        },

        exportExcel: function ($event) {
            $event.preventDefault();
            $scope.filters.factoryIDs = [];
            jsonService.getFactoryStockListReport($scope.filters,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type != 2) {
                        window.location = context.reportUrl + data.data + '.xlsm';
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        
    }

    $scope.event.reload();
}]);