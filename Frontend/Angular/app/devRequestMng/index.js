//
// SUPPORT
//
jQuery('.search-filter').keypress(function(e){
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives', 'ui.select2']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', 'dataService', function ($scope, $cookieStore, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.filters = {
        DevRequestID: '',
        DevRequestStatusID: '',
        DevRequestProjectID: '',
        DevRequestTypeID: '',
        RequesterID: '',
        Title: '',
        AssignedPersonID: '',
        Priority: '',
        SpecialCriteria: '1'
    };

    //
    // get filter from cookies
    //
    var cookieValue = $cookieStore.get(context.cookieId);
    if (cookieValue) {
        $scope.filters = cookieValue;
    }

    $scope.data = [];
    $scope.devRequestStatuses = null;
    $scope.devRequestProjects = null;
    $scope.devRequestTypes = null;
    $scope.devRequestPriorities = null;
    $scope.devRequestPersons = null;
    $scope.requesters = null;

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
        search: function (isLoadMore) {
            //
            // store filter in cookies
            //
            $cookieStore.put(context.cookieId, $scope.filters);

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
        init : function(){
            dataService.getSearchFilter(
                function (data) {
                    $scope.devRequestStatuses = data.data.devRequestStatuses;
                    $scope.devRequestProjects = data.data.devRequestProjects;
                    $scope.devRequestTypes = data.data.devRequestTypes;
                    $scope.requesters = data.data.requesters;
                    $scope.devRequestPriorities = data.data.devRequestPriorities;
                    $scope.devRequestPersons = data.data.devRequestPersons;
                    $scope.event.search();
                },
                function (error) {
                    $scope.devRequestStatuses = null;
                    $scope.devRequestProjects = null;
                    $scope.devRequestTypes = null;
                    $scope.requesters = null;
                    $scope.devRequestPriorities = null;
                    $scope.devRequestPersons = null;
                }
            );
        }
    }

    //
    // method
    //


    //
    // init
    //
    $scope.event.init();
}]);