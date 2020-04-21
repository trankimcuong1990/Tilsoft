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
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', function ($scope, $cookieStore) {
    $scope.filters = {
        ModelUD: '',
        ModelNM: '',
        UpdatorName: '',
        Season: '',
        IsStandard: '',
        ProductTypeID: '',
        ProductGroupID: '',
        HasCushion: '',
        HasFrameMaterial: '',
        HasSubMaterial: '',
        ProductCategoryID: '',
        FactoryUD: '',
        IsExcludedInNotification: '',
        CatalogPage: '',
        IsArchived: 'false',
    };
    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;

    var cookieValue = $cookieStore.get(context.cookieID);
    if (cookieValue) {
        $scope.filters = cookieValue;
    }

    $scope.data = [];

    // season for print.
    $scope.printSeason = jsHelper.getCurrentSeason();

    $scope.productTypes = null;
    $scope.productGroups = null;
    $scope.seasons = null;
    $scope.manufacturerCountries = null;
    $scope.modelStatuses = null;
    $scope.standardValues = null;
    $scope.productCategories = null;

    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;

    $scope.event = {
        search: function (isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    //debugger;
                    Array.prototype.push.apply($scope.data, data.data.data);

                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    $scope.$apply();
                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }

                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    $scope.data = null;
                    //debugger;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                });
        },
        restore: function (id) {
            jsonService.restore(id,
                function (data) {
                    jsHelper.processMessage(data.message);

                    if (data.message.type === 0) {
                        var j = -1;
                        for (var i = 0; i < $scope.data.length; i++) {
                            if ($scope.data[i].modelID === data.data) {
                                j = i;
                                break;
                            }
                        }

                        if (j >= 0) {
                            $scope.data.splice(j, 1);
                        }

                        $scope.$apply();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        reload: function () {
            $scope.pageIndex = 1;
            $scope.data = [];

            jsonService.searchFilter.pageIndex = $scope.pageIndex;

            $scope.event.search();
        },
        delete: function (id) {
            if (confirm('Put item to archive ?')) {
                jsonService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].modelID === data.data) {
                                    j = i;
                                    break;
                                }
                            }

                            if (j >= 0) {
                                $scope.data.splice(j, 1);
                            }

                            if ($scope.totalRows > 0) {
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
            $scope.method.init();

            jsonService.getsearchfilter(
                function (data) {
                    $scope.productTypes = data.data.productTypes;
                    $scope.productGroups = data.data.productGroups;
                    $scope.seasons = data.data.seasons;
                    $scope.standardValues = data.data.standardValues;
                    $scope.manufacturerCountries = data.data.manufacturerCountries;
                    $scope.modelStatuses = data.data.modelStatuses;
                    $scope.productCategories = data.data.productCategories;
                    $scope.yesNoValues = data.data.yesNoValues;

                    $scope.event.search();
                },
                function (error) {
                    $scope.productTypes = null;
                    $scope.productGroups = null;
                    $scope.seasons = null;
                    $scope.standardValues = null;
                    $scope.manufacturerCountries = null;
                    $scope.modelStatuses = null;
                    $scope.productCategories = null;
                    $scope.yesNoValues = null;

                    $scope.$apply();
                }
            );
        },
        clearFilter: function () {
            $scope.filters = {
                ModelUD: '',
                ModelNM: '',
                UpdatorName: '',
                Season: '',
                IsStandard: '',
                ProductTypeID: '',
                ProductGroupID: '',
                HasCushion: '',
                HasFrameMaterial: '',
                HasSubMaterial: '',
                ProductCategoryID: '',
                FactoryUD: '',
                IsExcludedInNotification: '',
                CatalogPage: '',
                IsArchived: 'false'
            };

            $scope.event.reload();
        },
        //===================================================================================================================//

        //open: function () {
        //    jQuery('#frmPrintOption').modal();

        //},

        //close: function () {
        //    jQuery('#frmPrintOption').modal('hide');
        //},

        //print: function () {
        //    debugger;
        //    jsonService.exportModel(
        //        $scope.printSeason,
        //        function (data) {
        //            window.location = context.backendReportUrl + data.data;
        //        },
        //        function (error) {
        //        });

        //    //});
        //} 

        exportExcel: function ($event) {
            $event.preventDefault();

            jsonService.exportExcelModel({
                filters: {
                        UserID: $scope.filters.UserID,
                        ModelUD: $scope.filters.ModelUD,
                        ModelNM: $scope.filters.ModelNM,
                        UpdatorName: $scope.filters.UpdatorName,
                        IsStandard: $scope.filters.IsStandard,
                        Season: $scope.filters.Season,
                        ProductTypeID: $scope.filters.ProductTypeID,
                        ProductGroupID: $scope.filters.ProductGroupID,
                        HasCushion: $scope.filters.HasCushion,
                        HasFrameMaterial: $scope.filters.HasFrameMaterial,
                        HasSubMaterial: $scope.filters.HasSubMaterial,
                        ManufacturerCountryID: $scope.filters.ManufacturerCountryID,
                        ModelStatusID: $scope.filters.ModelStatusID,
                        ProductCategoryID: $scope.filters.ProductCategoryID,
                        IsExcludedInNotification: $scope.filters.IsExcludedInNotification,
                        IsArchived:$scope.filters.IsArchived,
                        CatalogPage: $scope.filters.CatalogPage
                    }
                },
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                });
        },
        //====================================================================================================================//



    };

    $scope.method = {
        init: function () {
            jsonService.token = context.token;
            jsonService.searchFilter.pageSize = context.pageSize;
            jsonService.serviceUrl = context.urlService;
        }
    };

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
            $scope.pageIndex = 1;

            jsonService.searchFilter.sortedDirection = sortedDirection;
            jsonService.searchFilter.pageIndex = scope.pageIndex;
            jsonService.searchFilter.sortedBy = sortedBy;

            $scope.event.search();
        },
        isTriggered: false
    };

    $scope.event.init();
}]);


//===================================================================================================================//
//(function () {
//    'use strict';

//    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', modelController);
//    modelController.$inject = ['$scope', '$cookieStore', 'dataService'];

//    function modelController($scope, $cookieStore, modelService) {
//        $scope.data = [];

// season for print.
//scope.printSeason = jsHelper.getCurrentSeason();

//function open() {
//    jQuery('#frmPrintOption').modal();

//        }

//        function close() {
//            jQuery('#frmPrintOption').modal('hide');
//        }

//        function print() {
//            //if (jQuery('#season').val() === null || jQuery('#season').val() === '') {
//            //    jsHelper.showMessage('warning', 'Please input Start Date.');
//            //    return false;
//            //}

//            //$scope.filters.season = jQuery('#season').val();
//            debugger
//            costInvoice2Service.exportCostInvoice2(
//                $scope.printSeason,
//                function (data) {
//                    debugger
//                    window.location = context.serviceReportUrl + data.data;
//                },
//                function (error) {
//                    jsHelper.showMessage('warning', error);
//                });
//        }
//        //===========================================================================================================================================//

//    };
//    })();