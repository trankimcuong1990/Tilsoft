
//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ngCookies', 'avs-directives', 'ui.select2']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', 'dataService', function ($scope, $cookieStore, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.supportServiceUrl = context.supportServiceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.filters = {
        clientUD: '',
        season: ''
    };
    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;
    $scope.dataContainer = null;

    //
    // get filter from cookies
    //
    var cookieValue = $cookieStore.get(context.cookieId);
    if (cookieValue) {
        $scope.filters = cookieValue;
    }

    $scope.data = [];
    //$scope.factories = null;

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
        init: function() {
            $scope.event.search();
            initJQueryExtensions();
        },
        exportExcel: function (successCallBack, errorCallBack) {
            dataService.exportExcelClientComplaint(
                        function (data) {
                            
                            if (data.message.type === 0) {
                                var result = data.data;
                                window.location = context.reportUrl + data.data;
                                $scope.$apply();
                            }
                        },
                        function (error) {
                            
                            jsHelper.showMessage('warning', error);
                        }
                    );
        },
        reload: function () {
            $scope.data = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.search();
        },
        search: function (isLoadMore) {
            $scope.filters.clientUD = $scope.filters.clientUD || '';
            $scope.filters.season = $scope.filters.season || '';
            
            //
            // store filter in cookies
            //
            $cookieStore.put(context.cookieId, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            $scope.gridHandler.isTriggered = false;
            dataService.searchFilter.filters = $scope.filters;
            dataService.search(
                function (data) {
                    $scope.data = data.data.data;

                    $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    $scope.seasons = data.data.seasons;
                    $scope.complaintTypes = data.data.complaintTypes;
                    $scope.complaintStatuses = data.data.complaintStatuses;
                    $scope.sales = data.data.sales;

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
                clientUD: '',
                season: ''
            };
            $scope.event.reload();
        } ,
        deleteItem: function (itemId, $event) {
            $event.preventDefault();

            dataService.delete(
                itemId,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        $scope.event.reload();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        getComplaintType: function (id) {
            var entry = _.find($scope.complaintTypes, { "entryValue": id });
            if (entry)
                return entry.entryText;
            else return "";
        },
        getComplaintStatus: function (id) {
            var entry = _.find($scope.complaintStatuses, { "entryValue": id });
            if (entry)
                return entry.entryText;
            else return "";
        }
    }

    //
    // method
    //

    function initJQueryExtensions() {
        jQuery(document).ready(function () {
            //
            // SUPPORT
            //
            jQuery('.search-filter').keypress(function (e) {
                if (e.keyCode === 13) {
                    var scope = angular.element(jQuery('body')).scope();
                    scope.event.reload();
                }
            });
        });

    }

    //
    // init
    //
    $scope.event.init();
}]);