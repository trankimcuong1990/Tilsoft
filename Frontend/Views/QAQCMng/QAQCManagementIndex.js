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
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    /*
    * gridHandler
    */
    $scope.gridHandler = {
        loadMore: function () {
            if ($scope.pageIndex < $scope.totalPage) {
                $scope.pageIndex++;
                $scope.searchFilter.pageIndex = $scope.pageIndex;
                $scope.event.search(true);
            }
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.data = [];
            $scope.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            $scope.searchFilter.pageIndex = $scope.pageIndex;
            $scope.searchFilter.sortedBy = sortedBy;
            $scope.event.reload();
        },
        isTriggered: false
    };

    /*
     * property
     */
    $scope.searchFilter = {
        filters: {},
        sortedBy: 'CreatedDate',
        sortedDirection: 'DESC',
        pageSize: 25,
        pageIndex: 1
    };

    $scope.data = [];

    $scope.filters = {
        typeOfInspectionID: null,
        statusID: null,
        modelUD: '',
        modelNM: '',
        createdNM: '',
        createdDate: '',
        approvalNM: '',
        reportDate: ''

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
            $scope.searchFilter.pageIndex = $scope.pageIndex;
            $scope.searchFilter.sortedBy = 'CreatedDate';
            $scope.event.search();
        },
        search: function (isLoadMore) {
            //
            // store filter in cookies
            //
            //$cookieStore.put(context.cookieId, $scope.filters);
            //$scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            $scope.gridHandler.isTriggered = false;
            $scope.searchFilter.filters = $scope.filters;
            dataService.searchDataReport(
                $scope.searchFilter,
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.reportQAQCSearchDTOs);
                    $scope.totalPage = Math.ceil(data.data.totalRows / $scope.searchFilter.pageSize);
                    $scope.totalRows = data.data.totalRows;
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
                typeOfInspectionID: null,
                statusID: null,
                modelUD: '',
                modelNM: '',
                createdNM: '',
                createdDate: '',
                approvalNM: '',
                reportDate: ''
            };
            $scope.event.reload();
        },
        delete: function (item) {
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