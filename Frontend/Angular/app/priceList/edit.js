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

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.newID = -1;

    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data;
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
                            $scope.method.refresh(data.data.priceListID);
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
                searchQuery: '',
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
            itemSelected: function ($event, item) {
                $scope.data.productID = item.productID;
                $scope.data.articleCode = item.articleCode;
                $scope.data.description = item.description;
                $scope.quickSearchProduct.event.close($event);
            }
        }
    }
    //
    // init
    //
    $scope.event.load();
}]);