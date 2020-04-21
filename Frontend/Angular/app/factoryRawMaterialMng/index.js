//
// Support
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
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ngCookies', 'avs-directives', 'ui.select2']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', 'dataService', function ($scope, $cookieStore, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.filters = {
        FactoryRawMaterialUD: '',
        FactoryRawMaterialOfficialNM: '',
        FactoryRawMaterialShortNM: '',
        LocationID: null,
        KeyRawMaterialID: null,
        IsPotential: null,
        UpdatedBy: ''
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
    $scope.locations = null;

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

    $scope.exportExcel = function () {
        dataService.exportExcel(         
            function (data) {
                window.location = context.serviceReport + data.data;
            },
            function (error) {
            });
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
            if ($scope.filters.LocationID == null) {
                $scope.filters.LocationID = '';
            }
            if ($scope.filters.KeyRawMaterialID == null) {
                $scope.filters.KeyRawMaterialID = '';
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
                    console.log($scope.data);
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
                FactoryRawMaterialUD: '',
                FactoryRawMaterialOfficialNM: '',
                FactoryRawMaterialShortNM: '',
                LocationID: '',
                IsPotential: null,
                UpdatedBy: ''
            };

            $scope.event.reload();
        },
        init: function () {
            dataService.getSearchFilter(
                function (data) {
                    $scope.locations = data.data.locations;
                    $scope.keyRawMaterials = data.data.keyRawMaterials;
                    $scope.event.search();
                },
                function (error) {
                    $scope.locations = null;
                    $scope.keyRawMaterials = null;
                }
            );
        },
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
        }
    };

    //
    // method
    //
    $scope.method = {
        getTotalCurrentTurnover: function (data) {
            var result = 0;
            if (data != null) {
                angular.forEach(data, function (item) {
                    result += parseFloat(item.currentTurnover);
                }, null);
            }
            return result;
        },
        getTotalPreviousTurnover: function (data) {
            var result = 0;
            if (data != null) {
                angular.forEach(data, function (item) {
                    result += parseFloat(item.previousTurnover);
                }, null);
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