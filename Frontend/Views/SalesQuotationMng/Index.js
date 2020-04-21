//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', '$filter', 'dataService', function ($scope, $cookieStore, $filter, dataService) {

    //get service info
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    //filter property
    //
    $scope.filters = {
        SalesQuotationID: null,
        month: null,
        year: null,
        isLocked: null,
    };
    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;

    //
    //pager property
    //
    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;

    //
    // form property
    //
    $scope.data = [];   
    

    //
    // get filter from cookies
    //
    //var cookieValue = $cookieStore.get(context.cookieId);
    //if (cookieValue) {
    //    $scope.filters = cookieValue;
    //}

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
        reload: function () {
            $scope.data = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex;
            dataService.searchFilter.sortedBy = 'UpdatedDate';
            $scope.event.search();
        },

        search: function (isLoadMore) {
            //
            // store filter in cookies
            //
            $cookieStore.put(context.cookieId, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            $scope.gridHandler.isTriggered = false;
            dataService.searchFilter.filters = $scope.filters;
            dataService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
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

        delete: function (item) {
            dataService.delete(
                item.salesQuotationID,
                'price table',
                function (data) {
                    if (data.message.type === 0) {
                        $scope.data.splice($scope.data.indexOf(item), 1);
                        $scope.totalRows--;
                    }
                },
                function (error) {
                    //do not need do anything
                }
            );
        },

        init: function () {
            dataService.getSearchFilter(
                function (data) {                   
                    $scope.event.reload();
                },
                function (error) {
                    
                }
            );
        },      

        clearFilter: function () {
            $scope.filters = {                
                month: null,
                year: null,
                isLocked: null,
            };
            $scope.event.reload();
        },
    }
    //
    // angular init
    //
    $timeout(function () {
        $scope.event.init();
    }, 0);  
}]);