//
// SUPPORT
//
jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', 'dataService', '$timeout', function ($scope, $cookieStore, dataService, $timeout) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.filters = {
        Month: '',
        Year: ''
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
    $scope.MonthName = null;
    $scope.cashBookBalance = null;
    $scope.cashBookBalanceDetails = [];

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
            if ($scope.filters.Month == null) {
                $scope.filters.Month = '';
            }
            if ($scope.filters.Year == null) {
                $scope.filters.Year = '';
            }
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
        clearFilter: function () {
            $scope.filters = angular.copy($scope.defaultFilter);
            $scope.event.reload();
        },
        closeEntryBalance: function (Month, Year) {
            if (confirm('Are you sure close entry balance?')) {
                dataService.closeEntryBalance(
                    Month, Year,
                    function (data) {
                        if (data.message.message == null) {
                            jsHelper.processMessageEx(data.message);
                            $timeout(function () {
                                window.location = context.refreshUrl;
                            }, 500);
                        } else {
                            jsHelper.showMessage('warning', data.message.message);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.message.message);
                    }
                );
            }
        },
        getBalanceDetail: function (id) {
            dataService.getBalanceDetail(
                id,
                function (data) {
                    $scope.cashBookBalance = data.data.cashBookBalance;
                    $scope.cashBookBalanceDetails = data.data.cashBookBalanceDetails;
                    switch (data.data.cashBookBalance.balanceMonth) {
                        case 1:
                            $scope.MonthName = 'January';
                            break;
                        case 2:
                            $scope.MonthName = 'February';
                            break;
                        case 3:
                            $scope.MonthName = 'March';
                            break;
                        case 4:
                            $scope.MonthName = 'April';
                            break;
                        case 5:
                            $scope.MonthName = 'May';
                            break;
                        case 6:
                            $scope.MonthName = 'June';
                            break;
                        case 7:
                            $scope.MonthName = 'July';
                            break;
                        case 8:
                            $scope.MonthName = 'August';
                            break;
                        case 9:
                            $scope.MonthName = 'September';
                            break;
                        case 10:
                            $scope.MonthName = 'October';
                            break;
                        case 11:
                            $scope.MonthName = 'November';
                            break;
                        case 12:
                            $scope.MonthName = 'December';
                            break;
                        default:
                            $scope.MonthName = '';
                    }
                    jQuery('#balanceContainer').modal('show');
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    }

    //
    // method
    //
    $scope.method = {
        getNewID: function () {
            $scope.newid--;
            return $scope.newid;
        },
        refresh: function () {
            window.location = context.refreshUrl;
        }
    };

    //
    // init
    //
    $scope.event.search();
}]);