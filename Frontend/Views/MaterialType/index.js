jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', function ($scope, $cookieStore) {
    //
    // property
    //
    $scope.data = [];
    $scope.filters = {
        MaterialTypeUD: '',
        MaterialTypeNM: ''
    };

    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;

    var cookieValue = $cookieStore.get(jsonService.cookiesId);
    if (cookieValue)
        $scope.filters = cookieValue;

    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;

    $scope.gridHandler = {
        loadMore: function () {
            if ($scope.pageIndex < $scope.totalPage) {
                $scope.pageIndex++;
                jsonService.searchFilter.pageIndex = $scope.pageIndex;
                $scope.event.search(true);
            }
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.data = [];
            jsonService.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = scope.pageIndex;
            jsonService.searchFilter.sortedBy = sortedBy;
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
            jsonService.searchFilter.pageIndex = 1;
            $scope.event.search();
        },
        search: function (isLoadMore) {
            $cookieStore.put(jsonService.cookiesId, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            $scope.gridHandler.isTriggered = false;

            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data); //$scope.data = data.data.data;
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    $scope.$apply();

                    /*if(data.totalRows < jsonService.searchFilter.pageSize) {
                        jQuery('#searchResult').find('ul').hide();
                    }
                    else {
                        jQuery('#searchResult').find('ul').show();
                    }*/

                    jQuery('#content').show();

                    if (!isLoadMore) { //searchResultGrid.updateLayout();
                        $scope.gridHandler.goTop();
                    }

                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                    $scope.$apply();
                }
            );
        },
        delete: function (id) {
            if (confirm('Are you sure ?')) {
                jsonService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].materialTypeID == data.data) {
                                    j = i;
                                    break;
                                }
                            }

                            if (j >= 0) {
                                $scope.data.splice(j, 1);
                                $scope.totalRows--;
                            }

                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        init: function () {
            $scope.event.search();
        },
        clearFilter: function () {
            $scope.filters = {
                MaterialTypeUD: '',
                MaterialTypeNM: ''
            };
            $scope.event.reload();
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