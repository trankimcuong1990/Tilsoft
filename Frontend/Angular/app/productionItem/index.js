﻿//
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
        ProductionItemUD: '',
        ProductionItemNM: '',
        ProductionItemVNNM: '',
        ProductionItemGroupID: '',
        ProductionItemMaterialTypeID: '',
        ProductionItemSpecID: ''
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
    $scope.factoryWarehouseSearchResults = [];
    $scope.factoryWarehouses = null;
    $scope.productionItemMaterialTypes = null;
    $scope.productionItemGroups = null;
    $scope.productionItemSpecs = null;

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
            if ($scope.filters.ProductionItemUD == null) {
                $scope.filters.ProductionItemUD = '';
            }
            if ($scope.filters.ProductionItemNM == null) {
                $scope.filters.ProductionItemNM = '';
            }
            if ($scope.filters.ProductionItemVNNM == null) {
                $scope.filters.ProductionItemVNNM = '';
            }
            if ($scope.filters.ProductionItemGroupID == null) {
                $scope.filters.ProductionItemGroupID = '';
            }
            if ($scope.filters.ProductionItemMaterialTypeID == null) {
                $scope.filters.ProductionItemMaterialTypeID = '';
            }
            
            if ($scope.filters.DefaultWarehouseID == null) {
                $scope.filters.DefaultWarehouseID = '';
            }
            if ($scope.filters.Status == null) {
                $scope.filters.Status = '';
            }
            if ($scope.filters.ProductionItemTypeID == null) {
                $scope.filters.ProductionItemTypeID = '';
            }
            if ($scope.filters.isAVTGroup == null) {
                $scope.filters.isAVTGroup = 'any';
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
                    $scope.factoryWarehouseSearchResults = data.data.factoryWarehouseSearchResults;

                    $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    jQuery('#content').show();

                    $("#DefaultWarehouseID").select2();

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
                ProductionItemUD: '',
                ProductionItemNM: '',
                ProductionItemVNNM: '',
                ProductionItemGroupID: '',
                ProductionItemMaterialTypeID: '',
                DefaultWarehouseID: '',
                Status: '',
                ProductionItemTypeID: '',
                isAVTGroup: 'any'
            };
            $("#DefaultWarehouseID").select2('data', null);
            $scope.event.reload();
        },
        init: function () {
            dataService.getSearchFilter(
                function (data) {
                    $scope.factoryWarehouses = data.data.factoryWarehouses;
                    $scope.productionItemMaterialTypes = data.data.productionItemMaterialTypes;
                    $scope.productionItemGroups = data.data.productionItemGroups;
                    $scope.productionItemSpecs = data.data.productionItemSpecs;
                    $scope.productionItemTypes = data.data.productionItemTypeSupports;
                    
                    $scope.event.search();
                },
                function (error) {
                    $scope.factoryWarehouses = null;
                    $scope.productionItemMaterialTypes = null;
                    $scope.productionItemGroups = null;
                    $scope.productionItemSpecs = null;
                    $scope.productionItemTypes = null;
                }
            );
        },
        exportExcel: function () {
            if ($scope.filters.ProductionItemUD === null) {
                $scope.filters.ProductionItemUD = '';
            }
            if ($scope.filters.ProductionItemNM === null) {
                $scope.filters.ProductionItemNM = '';
            }
            if ($scope.filters.ProductionItemVNNM === null) {
                $scope.filters.ProductionItemVNNM = '';
            }
            if ($scope.filters.ProductionItemGroupID === null) {
                $scope.filters.ProductionItemGroupID = '';
            }          
            if ($scope.filters.DefaultWarehouseID === null) {
                $scope.filters.DefaultWarehouseID = '';
            }
            if ($scope.filters.ProductionItemTypeID === null) {
                $scope.filters.ProductionItemTypeID = '';
            }
            if ($scope.filters.isAVTGroup === null) {
                $scope.filters.isAVTGroup = 'any';
            }
            dataService.exportexcellistproductionitem(
                function (data) {
                    if (data.message.type === 0) {
                        window.location = context.backendReportUrl + data.data;
                    }
                    else {
                        jsHelper.processMessageEx(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
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