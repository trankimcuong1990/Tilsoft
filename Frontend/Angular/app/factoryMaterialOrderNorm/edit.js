var quickSearchResultGridProduct = jQuery('#quickSearchResultGrid-product').scrollableTable2(
    'quickSearchProduct.filters.pageIndex',
    'quickSearchProduct.totalPage',
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quickSearchProduct.filters.pageIndex = currentPage;
            scope.quickSearchProduct.method.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quickSearchProduct.filters.sortedDirection = sortedDirection;
            scope.quickSearchProduct.filters.pageIndex = 1;
            scope.quickSearchProduct.filters.sortedBy = sortedBy;
            scope.quickSearchProduct.method.search();
        });
    }
);

var quickSearchResultGridMaterial = jQuery('#quickSearchResultGrid-material').scrollableTable2(
    'quickSearchMaterial.filters.pageIndex',
    'quickSearchMaterial.totalPage',
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quickSearchMaterial.filters.pageIndex = currentPage;
            scope.quickSearchMaterial.method.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quickSearchMaterial.filters.sortedDirection = sortedDirection;
            scope.quickSearchMaterial.filters.pageIndex = 1;
            scope.quickSearchMaterial.filters.sortedBy = sortedBy;
            scope.quickSearchMaterial.method.search();
        });
    }
);

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.units = null;
    $scope.seasons = null;
    $scope.factoryGoodsProcedures = null;
    $scope.newID = -1;

    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.getEditFormData(
                context.id,context.clientID, context.productID,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.units = data.data.units;
                    $scope.seasons = data.data.seasons;
                    $scope.factoryGoodsProcedures = data.data.factoryGoodsProcedures;
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },
        update: function ($event) {
            $event.preventDefault();

            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryMaterialOrderNormID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', context.errMsg);
            }
        },
        delete: function ($event) {
            $event.preventDefault();

            if (confirm('Are you sure you want to delete ?')) {
                jsonService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },

        removeMaterial: function ($event, index) {
            $event.preventDefault();
            $scope.data.factoryMaterialOrderNormDetails.splice(index, 1);
        }
    },

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            window.location = context.refreshUrl + id
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        }
    },


    //quick search product
    searchProductTimer = null;
    $scope.quickSearchProduct = {
        data: null,
        filters: {
            filters: {
                searchQuery: ''
            },
            sortedBy: 'Description',
            sortedDirection: 'ASC',
            pageSize: 20,
            pageIndex: 1
        },
        totalPage: 0,

        popupformID: 'popup-product',
        gridSearchResultID: 'quickSearchResultGrid-product',
        searchQueryBoxID: 'quickSearchBox-product',

        method: {
            search: function () {
                supportService.quickSearchProduct(
                    $scope.quickSearchProduct.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quickSearchProduct.data = data.data;
                            $scope.quickSearchProduct.totalPage = Math.ceil(data.totalRows / $scope.quickSearchProduct.filters.pageSize);
                            quickSearchResultGridProduct.updateLayout();
                            $scope.$apply();
                            jQuery('#' + $scope.quickSearchProduct.popupformID).show();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        event: {
            searchBoxKeyUp: function ($event) {
                if ($event.keyCode == 13) {
                    $scope.quickSearchProduct.event.searchClick();
                }
                else if (jQuery('#' + $scope.quickSearchProduct.searchQueryBoxID).val().length >= 3) {
                    clearTimeout(searchProductTimer);
                    searchProductTimer = setTimeout(
                        function () {
                            $scope.quickSearchProduct.event.searchClick();
                        },
                        500);
                }
            },

            searchClick: function () {
                $scope.quickSearchProduct.filters.filters.searchQuery = jQuery('#' + $scope.quickSearchProduct.searchQueryBoxID).val();
                $scope.quickSearchProduct.filters.pageIndex = 1;
                $scope.quickSearchProduct.method.search();
            },

            close: function ($event) {
                jQuery('#' + $scope.quickSearchProduct.popupformID).hide();
                jQuery('#' + $scope.quickSearchProduct.searchQueryBoxID).val('');
                $scope.quickSearchProduct.data = null;
                $event.preventDefault();
            },
            itemSelected: function ($event, id) {
                var selected_product = $scope.quickSearchProduct.data.filter(function (o) { return o.productID == id });
                $scope.data.productID = id;
                $scope.data.articleCode = selected_product[0].articleCode;
                $scope.data.description = selected_product[0].description;
                $scope.quickSearchProduct.event.close($event);
            }
        }
    }

    //quick search material
    searchMaterialTimer = null;
    $scope.quickSearchMaterial = {
        data: null,
        filters: {
            filters: {
                searchQuery: ''
            },
            sortedBy: 'FactoryMaterialNM',
            sortedDirection: 'ASC',
            pageSize: 20,
            pageIndex: 1
        },
        totalPage: 0,

        popupformID: 'popup-material',
        gridSearchResultID: 'quickSearchResultGrid-material',
        searchQueryBoxID: 'quickSearchBox-material',

        method: {
            search: function () {
                supportService.quickSearchFactoryMaterial(
                    $scope.quickSearchMaterial.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quickSearchMaterial.data = data.data.data;
                            var totalRows = data.data.totalRows;

                            $scope.quickSearchMaterial.totalPage = Math.ceil(totalRows / $scope.quickSearchMaterial.filters.pageSize);
                            quickSearchResultGridMaterial.updateLayout();
                            $scope.$apply();
                            jQuery('#' + $scope.quickSearchMaterial.popupformID).show();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        event: {
            searchBoxKeyUp: function ($event) {
                if ($event.keyCode == 13) {
                    $scope.quickSearchMaterial.event.searchClick();
                }
                else if (jQuery('#' + $scope.quickSearchMaterial.searchQueryBoxID).val().length >= 3) {
                    clearTimeout(searchMaterialTimer);
                    searchMaterialTimer = setTimeout(
                        function () {
                            $scope.quickSearchMaterial.event.searchClick();
                        },
                        500);
                }
            },

            searchClick: function () {
                $scope.quickSearchMaterial.filters.filters.searchQuery = jQuery('#' + $scope.quickSearchMaterial.searchQueryBoxID).val();
                $scope.quickSearchMaterial.filters.pageIndex = 1;
                $scope.quickSearchMaterial.method.search();
            },

            close: function ($event) {
                jQuery('#' + $scope.quickSearchMaterial.popupformID).hide();
                jQuery('#' + $scope.quickSearchMaterial.searchQueryBoxID).val('');
                $scope.quickSearchMaterial.data = null;
                $event.preventDefault();
            },
            itemSelected: function ($event) {
                $event.preventDefault();
                angular.forEach($scope.quickSearchMaterial.data, function (material_item) {
                    if (material_item.isSelected)
                    {
                        if ($scope.data.factoryMaterialOrderNormDetails.filter(function (o) { return o.factoryMaterialID == material_item.factoryMaterialID }).length == 0) // check item is existed in list
                        {
                            $scope.data.factoryMaterialOrderNormDetails.push({
                                factoryMaterialOrderNormDetailID: $scope.method.getNewID(),
                                factoryMaterialID: material_item.factoryMaterialID,
                                unitID: material_item.unitID,
                                factoryMaterialUD: material_item.factoryMaterialUD,
                                factoryMaterialNM: material_item.factoryMaterialNM,
                            });
                        }
                    }
                });
                $scope.quickSearchMaterial.event.close($event);
            }
        }
    }



    //
    // init
    //
    $scope.event.load();
}]);