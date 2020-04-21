jQuery('#main-form').keypress(function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});
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

var quickSearchResultGridSample = jQuery('#quickSearchResultGrid-sample').scrollableTable2(
    'quickSearchSample.filters.pageIndex',
    'quickSearchSample.totalPage',
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quickSearchSample.filters.pageIndex = currentPage;
            scope.quickSearchSample.method.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quickSearchSample.filters.sortedDirection = sortedDirection;
            scope.quickSearchSample.filters.pageIndex = 1;
            scope.quickSearchSample.filters.sortedBy = sortedBy;
            scope.quickSearchSample.method.search();
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
    var searchProductTimer = null;

    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
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
                            $scope.method.refresh(data.data.showroomItemID);
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

        // image event bottom rith
        changeImage: function ($event) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.showroomItemThumbnailImage = img.fileURL;
                        scope.data.imageFile_NewFile = img.filename;
                        scope.data.imageFile_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeImage: function ($event) {
            $scope.data.showroomItemThumbnailImage = '';
            $scope.data.imageFile_NewFile = '';
            $scope.data.imageFile_HasChange = true;
        },
    };

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
    };

    //
    //support product
    //
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
                $scope.data.productArticleCode = selected_product[0].articleCode;
                $scope.data.productDescription = selected_product[0].description;
                $scope.quickSearchProduct.event.close($event);
            }
        }
    }

    //
    //support sample
    //
    $scope.quickSearchSample = {
        data: null,
        filters: {
            filters: {
                searchQuery: ''
            },
            sortedBy: 'ArticleDescription',
            sortedDirection: 'ASC',
            pageSize: 20,
            pageIndex: 1
        },
        totalPage: 0,

        popupformID: 'popup-sample',
        gridSearchResultID: 'quickSearchResultGrid-sample',
        searchQueryBoxID: 'quickSearchBox-sample',

        method: {
            search: function () {
                jsonService.quickSearchSampleProduct(
                    $scope.quickSearchSample.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quickSearchSample.data = data.data;
                            $scope.quickSearchSample.totalPage = Math.ceil(data.totalRows / $scope.quickSearchSample.filters.pageSize);
                            quickSearchResultGridSample.updateLayout();
                            $scope.$apply();
                            jQuery('#' + $scope.quickSearchSample.popupformID).show();
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
                    $scope.quickSearchSample.event.searchClick();
                }
                else if (jQuery('#' + $scope.quickSearchSample.searchQueryBoxID).val().length >= 3) {
                    clearTimeout(searchProductTimer);
                    searchProductTimer = setTimeout(
                        function () {
                            $scope.quickSearchSample.event.searchClick();
                        },
                        500);
                }
            },

            searchClick: function () {
                $scope.quickSearchSample.filters.filters.searchQuery = jQuery('#' + $scope.quickSearchSample.searchQueryBoxID).val();
                $scope.quickSearchSample.filters.pageIndex = 1;
                $scope.quickSearchSample.method.search();
            },

            close: function ($event) {
                jQuery('#' + $scope.quickSearchSample.popupformID).hide();
                jQuery('#' + $scope.quickSearchSample.searchQueryBoxID).val('');
                $scope.quickSearchSample.data = null;
                $event.preventDefault();
            },
            itemSelected: function ($event, id) {
                var selected_product = $scope.quickSearchSample.data.filter(function (o) { return o.sampleProductID == id });
                $scope.data.sampleProductID = id;
                $scope.data.sampleArticleDescription = selected_product[0].articleDescription;
                $scope.quickSearchSample.event.close($event);
            }
        }
    }

    //
    // init
    //
    $scope.event.load();
}]);