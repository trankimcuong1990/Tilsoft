'use strict';

/*
 * SUPPORT
 */
$('.search-filter').keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

//Angular
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {

    /*
     * initialization Service
     */
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    /*
     * gridHandler
     */
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

    /*
     *Property 
     */
    $scope.data = [];
    $scope.supportdata = {
        status: null,
        receiptNoteSupportTypes: null
    };

    $scope.filters = {
    };

    //Fiters
    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;
    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;


    /*
     * Event
     */

    $scope.event = {
        init: function () {
            //dataService.getSearchFilter(
            //    function (data) {
            //        $scope.event.search();
            //    },
            //    function (error) {
            //        // reset data
            //    }
            //);
            $scope.event.reload();
        },
        reload: function () {
            $scope.data = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex;
            dataService.searchFilter.sortedBy = 'receiptNoteNo';
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
                    $scope.supportdata.status = data.data.receiptSupportStatuses;
                    $scope.supportdata.receiptNoteSupportTypes = data.data.receiptNoteSupportTypes;
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
                //Input Here
            };
            $scope.event.reload();
        },
        delete: function (item) {
            debugger;
            dataService.delete(
                item.id, // id of item
                null,
                function (data) {
                    if (data.message.type === 0) {
                        $scope.data.splice($scope.data.indexOf(item), 1);
                        $scope.totalRows--;
                    }
                },
                function (error) {
                }
            );
        },
        print: function (item) {
            //do nothing
        }
    };

    /*
     * Method
     */
    $scope.method = {
        //Method here
    };

    /*
     * Run event
     */
    $timeout(function () {
        $scope.event.init();
    }, 0);

}]);