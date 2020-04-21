//
// Support
//
jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
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
    // property
    //
    $scope.filters = {
        ProductionItemGroupUD: '',
        ProductionItemGroupNM: ''
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
            if ($scope.filters.ProductionItemGroupUD == null) {
                $scope.filters.ProductionItemGroupUD = '';
            }
            if ($scope.filters.ProductionItemGroupNM == null) {
                $scope.filters.ProductionItemGroupNM = '';
            }
      
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

                    console.log(data);

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
                ProductionItemGroupUD: '',
                ProductionItemGroupNM: ''
            };
            $scope.event.reload();
        },
        init: function () {
            dataService.getSearchFilter(
                function (data) {
                    $scope.event.search();
                },
                function (error) {
                    $scope.data = null;
                  
                }
            );
        }
    };

    //
    // method
    //
    $scope.method = {
        getTotal: function (data, val) {
            var result = 0;
            if (data != null) {
                for (i = 0; i < data.length; ++i) {
                    if (data[i][val] <= 0 || data[i][val] == null) {
                        data[i][val] = 0;
                    }
                    result += parseFloat(data[i][val]);
                }
                //angular.forEach(data, function (value, key) {
                //    console.log(value[]);
                //    if (value == val) {
                //        result += parseFloat(value);
                //    }
                //}, null);
            }
            return result;
        },
        getTurnoverPerCont: function (data) {
            var result = 0;
            if ($scope.data != null) {
                result = $scope.method.getTotalTurnover($scope.data) / $scope.method.getTotalCapacity($scope.data);
            }
            return result;
        }
    };

    //
    // method
    //


    //
    // init
    //
    $scope.event.init();
}]);