//
// SUPPORT
//
jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', '$timeout', 'dataService', function ($scope, $cookieStore, $timeout, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.filters = {
        Season: jsHelper.getCurrentSeason()
    };
    
    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;

    //
    // get filter from cookies
    //
    var cookieValue = $cookieStore.get(context.cookieId);
    if (cookieValue) {
        $scope.filters = cookieValue;
    }

    $scope.data = [];
    $scope.seasons = jsHelper.getSeasons();

    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
            //if ($scope.pageIndex < $scope.totalPage) {
            //    $scope.pageIndex++;
            //    dataService.searchFilter.pageIndex = $scope.pageIndex;
            //    $scope.event.search(true);
            //}
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
            $scope.event.search();
        },

        generateHtml: function () {
            dataService.searchFilter.filters = $scope.filters;
            dataService.search(
                function (data) { 
                    $scope.data = data.data.data;
                },
                function (error) {
                    $scope.data = null;
                }
            );
        },

        init: function () {
            jQuery('#content').show();
        },

        generateExcel: function () {
            dataService.searchFilter.filters = $scope.filters;
            dataService.exportExcel(
                {
                    Filters: $scope.filters
                    //sortedBy: '',
                    //sortedDirection: 'DESC'
                },
                function (data) {
                    //jsHelper.processMessage(data.message);
                    if (data.message.type !== 2) {
                        window.open(context.backendReportUrl + data.data);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    };

    //
    //
    //
    $scope.method = {
        
    };

    //
    // init
    //
    $scope.event.init();
}]);