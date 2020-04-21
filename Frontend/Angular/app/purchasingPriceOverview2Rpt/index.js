//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.filters = {
        FactoryID: '',
        ClientUD: '',
        Season: ''
    };
    $scope.localFilters = {
        proformaInvoiceNo: undefined,
        invoiceDate: undefined,
        clientUD: undefined,
        clientNM: undefined,
        vnMonitorName: undefined,
        nlMonitorName: undefined,
        agentNM: undefined,
        saleNM: undefined,
        articleCode: undefined,
        description: undefined,
        qnt40HC: undefined,
        orderQnt: undefined,
        orderQnt40HC: undefined,
        loadedQnt: undefined,
        loadedQnt40HC: undefined,
        shippedQnt: undefined,
        shippedQnt40HC: undefined,
        toBeShippedQnt: undefined,
        toBeShippedQnt40HC: undefined,
        materialNM: undefined,
        productionStatus: undefined,
        factoryUD: undefined,
        lds: undefined,
        sc: undefined,
        quotationUD: undefined,
        manufacturerCountryNM: undefined,
        city: undefined,
        priceDifferenceCode: undefined,
        salePriceInThePast2Season: undefined,
        salePriceInTheLastSeason: undefined,
        purchasingInvoicePrice: undefined,
        salePrice: undefined,
        targetPrice: undefined,
        breakDownPrice: undefined,
        quotationStatusNM: undefined,
        statusUpdatedDate: undefined,
        factoryProformaInvoiceNo: undefined,
        remark: undefined,
        season: undefined,
        factoryID: undefined,
        clientID: undefined,
        itemType: undefined
    };
    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;

    $scope.data = [];
    $scope.seasons = null;
    $scope.factories = null;

    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;

    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {},
        sort: function (sortedBy, sortedDirection) {
            $scope.data = [];
            dataService.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = scope.pageIndex;
            dataService.searchFilter.sortedBy = sortedBy;
            $scope.event.search();
        },
        isTriggered: false
    };

    //
    // event
    //
    $scope.event = {
        htmlReport: function () {
            $scope.data = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.search();
        },
        excelReport: function () {
            if (!$scope.filters.Season) {
                alert('Season is invalid!');
                return;
            }

            if (!$scope.filters.FactoryID) {
                $scope.filters.FactoryID = '';
            }
            if (!$scope.filters.ClientUD) {
                $scope.filters.ClientUD = '';
            }

            dataService.getExcelReport(
                $scope.filters.FactoryID,
                $scope.filters.ClientUD,
                $scope.filters.Season,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        search: function () {
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            $scope.gridHandler.isTriggered = false;
            dataService.searchFilter.filters = $scope.filters;

            if (!$scope.filters.Season) {
                alert('Season is invalid!');
                return;
            }

            dataService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    $scope.gridHandler.goTop();
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                }
            );
        },
        clearFilter: function () {
            $scope.filters = {
                FactoryID: '',
                ClientUD: '',
                Season: ''
            };
            $scope.event.reload();
        },
        init: function () {
            dataService.getSearchFilter(
                function (data) {
                    $scope.seasons = data.data.seasons;
                    $scope.factories = data.data.factories;

                    jQuery('#content').show();
                    $scope.filters.Season = dataService.currentSeason();
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.factories = null;
                }
            );
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);