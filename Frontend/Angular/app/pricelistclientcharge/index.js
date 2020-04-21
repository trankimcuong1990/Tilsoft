angular
.module('tilsoftApp', ['ngCookies', 'avs-directives'])
.controller('tilsoftController', ['$scope', '$cookieStore', 'dataService', function($scope, $cookieStore, dataService){

    // #region variable
    $scope.data = [];
    $scope.filters = {

    };

    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 1;

    $scope.isDefaultFilter = true;
    // #endregion variable

    // #region proccess default filter
    $scope.defaultFilter = angular.copy($scope.filters);
    // #endregion proccess default filter

    // #region update value dataService
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;
    // #endregion update value dataService

    // #region define and proccess gridHandler
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
            $scope.pageIndex = 1;

            dataService.searchFilter.sortedDirection = sortedDirection;
            dataService.searchFilter.sortedBy = sortedBy;
            dataService.searchFilter.pageIndex = $scope.pageIndex;

            $scope.event.search();
        },
        isTriggered: false
    };
    // #endregion define and proccess gridHandler

    $scope.event = {
        reload: function () {
                $scope.data = [];
                $scope.pageIndex = 1;

                dataService.searchFilter.pageIndex = $scope.pageIndex;

                $scope.event.search();
            },
            search: function (isLoadMore) {
                $cookieStore.put(context.cookieId, $scope.filters); // keep filter in cookies browser
                $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter); // check search condition or not condition

                $scope.gridHandler.isTriggered = false;

                dataService.searchFilter.filters = $scope.filters;

                // proccess call api search data
                dataService.search(
                    function (data) {

                        Array.prototype.push.apply($scope.data, data.data.data);
                        console.log($scope.data)
                        $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;

                        jQuery('#content').show();

                        if (!isLoadMore) {
                            $scope.gridHandler.goTop();
                        }

                        $scope.gridHandler.isTriggered = true;
                    },
                    function (error) {
                    });
            },
            init: function () {
                $scope.event.search();
            },
            clearfilter: function () {
                $scope.filters = {

                };

                $scope.event.reload();
            }
    };

    $scope.event.init();

}]);