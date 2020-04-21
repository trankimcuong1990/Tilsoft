//
// SUPPORT
//
jQuery('.select2-input').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

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
        FactoryID: '',
        FactoryName: '',
        SupplierID: '',
        LocationID: '',
        IsPotential: '',
        IsActive: 'true'
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
    $scope.factoryRawMaterialSupplierList = null;
    $scope.supplierList = null;
    $scope.locations = null;
    $scope.factories = null;

    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;
    $scope.rowIndex = 1;

    $scope.print_value = "1";

    //
    // event
    //
    $scope.event = {
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
            if ($scope.filters.FactoryID == null) {
                $scope.filters.FactoryID = '';
            }
            if ($scope.filters.FactoryRawMaterialID == null) {
                $scope.filters.FactoryRawMaterialID = '';
            }
            //
            // store filter in cookies
            //
            $cookieStore.put(context.cookieId, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            dataService.searchFilter.filters = $scope.filters;
            dataService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    debugger;
                    angular.forEach(data.data.data, function (item) {
                        if (item.factoryExpectedCapacitySearches !== undefined && item.factoryExpectedCapacitySearches !== null) {
                            if (item.factoryExpectedCapacitySearches.length >= 12) {
                                var tempIndex = item.factoryExpectedCapacitySearches.length - 1;
                                if (item.factoryExpectedCapacitySearches[tempIndex].month === 9) {
                                    var tempPos = item.factoryExpectedCapacitySearches[tempIndex];
                                    item.factoryExpectedCapacitySearches.splice(tempIndex, 1);
                                    item.factoryExpectedCapacitySearches.unshift(tempPos);
                                }
                            }
                        }
                    });

                    $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    jQuery('#content').show();

                    $("#FactoryID").select2();
                    $("#FactoryRawMaterialID").select2();
                    $("#LocationID").select2();
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
                FactoryID: '',
                FactoryName: '',
                SupplierID: '',
                LocationID: ''
            };
            $("#FactoryID").select2('data', null);
            $("#FactoryRawMaterialID").select2('data', null);
            $("#LocationID").select2('data', null);
            $scope.event.reload();
        },
        init: function () {
            dataService.getSearchFilter(
                function (data) {
                    $scope.factoryRawMaterialSupplierList = data.data.factoryRawMaterialSupplierList;
                    //$scope.supplierList = data.data.supplierList;
                    $scope.locations = data.data.locations;
                    $scope.factories = data.data.factories;
                    $scope.event.search();
                },
                function (error) {
                    $scope.factoryRawMaterialSupplierList = null;
                    $scope.locations = null;
                    $scope.factories = null;
                }
            );
        },
        exportExcel: function ($event, type) {
            $event.preventDefault();
            dataService.searchFilter.filters = $scope.filters;
            dataService.exportExcelFactory(
                type,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type !== 2) {
                        window.location = context.backendReportUrl + data.data;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        exportExcel_Capacity: function ($event) {
            $event.preventDefault();
            dataService.searchFilter.filters = $scope.filters;
            dataService.exportExcelFactory_Capacity(
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type !== 2) {
                        window.location = context.backendReportUrl + data.data;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        showPrintOption: function ($event) {
            $event.preventDefault();
            $('#frmPrintOption').modal('show');
        },
        printExcel: function ($event, type) {
            if (type === "1") {
                $scope.event.exportExcel($event, 1);
            }
            else if (type === "2") {
                $scope.event.exportExcel($event, 2);
            }
        }
    };

    //
    // method
    //
    $scope.method = {
        getTotalTurnover: function (data) {
            var result = 0;
            if ($scope.data != null) {
                angular.forEach(data, function (item) {
                    result += parseFloat(item.currentTurnover);
                });
            }
            return result;
        },
        getTotalCapacity: function (data) {
            var result = 0;
            if ($scope.data != null) {
                angular.forEach(data, function (item) {
                    result += parseFloat(item.currentTotalShipped40HC);
                }, null);
            }
            return result;
        },
        getPrevTotalTurnover: function (data) {
            var result = 0;
            if ($scope.data != null) {
                angular.forEach(data, function (item) {
                    result += parseFloat(item.prevTurnover);
                }, null);
            }
            return result;
        },
        getPrevTotalCapacity: function (data) {
            var result = 0;
            if ($scope.data != null) {
                angular.forEach(data, function (item) {
                    result += parseFloat(item.prevTotalShipped40HC);
                }, null);
            }
            return result;
        },
        getTurnoverPerCont: function (data) {
            var result = 0;
            if ($scope.data != null) {
                result = $scope.method.getTotalTurnover($scope.data) / $scope.method.getTotalCapacity($scope.data);
            }
            return result;
        },
        getPrevTurnoverPerCont: function (data) {
            var result = 0;
            if ($scope.data != null) {
                result = $scope.method.getPrevTotalTurnover($scope.data) / $scope.method.getPrevTotalCapacity($scope.data);
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