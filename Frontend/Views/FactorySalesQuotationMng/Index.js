﻿//
// Support
//
jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ngCookies', 'avs-directives', 'ui.select2']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', 'dataService', function ($scope, $cookieStore, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property filters
    //
    $scope.filters = {
        factorySaleQuotationUD: '',
        factoryRawMaterialOfficialNM: '',
        factorySaleQuotationStatusID: '',
        postingDate: '',
        validUntil: '',
        documentDate: ''

    };
    $scope.lstListRawMaterial = [];
    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;
    $scope.lstFactorySaleQuotation = [];
    $scope.lstStatus = [];
    //
    // get filter from cookies
    //
    var cookieValue = $cookieStore.get(context.cookieId);
    if (cookieValue) {
        $scope.filters = cookieValue;
    }


    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;

    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
            if ($scope.pageIndex < $scope.totalPage) {
                $scope.pageIndex++;
                dataService.searchFilter.pageIndex = $scope.pageIndex;
                $scope.event.search(true);
            }
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.lstFactorySaleQuotation = [];
            dataService.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = scope.pageIndex;
            dataService.searchFilter.sortedBy = sortedBy;
            $scope.event.search();
        },
        isTriggered: false
    };

    //
    // event reload or refesh
    //
    $scope.event = {
        reload: function () {
            $scope.lstFactorySaleQuotation = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.search();
        },

        search: function (isLoadMore) {

            $cookieStore.put(context.cookieId, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            $scope.gridHandler.isTriggered = false;
            dataService.searchFilter.filters = $scope.filters;
            dataService.search(
                function (data) {
                    Array.prototype.push.apply($scope.lstFactorySaleQuotation, data.data.lstFactorySaleQuotation);
                    $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    $scope.lstListRawMaterial = data.data.lstListRawMaterial;
                    //$scope.lstFactorySaleQuotation = data.data.lstFactorySaleQuotation;
                    $scope.lstStatus = data.data.lstStatus;
                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
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
                factorySaleQuotationUD: '',
                factoryRawMaterialOfficialNM: '',
                factorySaleQuotationStatusID: '',
                postingDate: '',
                validUntil: '',
                documentDate: ''
            };
            $scope.event.reload();
        },
        /// delete a factory Sale Quotation
        delete: function (item) {
            dataService.delete(
                item.factorySaleQuotationID,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);

                    if (data.message.type === 0) {
                        var index = $scope.lstFactorySaleQuotation.indexOf(item);
                        $scope.lstFactorySaleQuotation.splice(index, 1);
                        $scope.totalRows = $scope.totalRows - 1;
                    }
                },
                function (error) {                   
                    jsHelper.showMessage('warning', error.data.exceptionMessage);
                }
            );
        },


    };
    ////method
    $scope.method = {
        init: function () {
            $scope.event.clearFilter();
            $scope.event.search();
        },
    };


    // init

    $scope.method.init();
}]);