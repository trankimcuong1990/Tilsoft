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

//var grdStockRemain = jQuery('#grdStockRemain').scrollableTable(
//    function (currentPage) {
//        var scope = angular.element(jQuery('body')).scope();
//        scope.$apply(function () {
//            scope.quickSearchStockRemain.filters.pageIndex = currentPage;
//            scope.quickSearchStockRemain.method.search();
//        });
//    },
//    function (sortedBy, sortedDirection) {
//        var scope = angular.element(jQuery('body')).scope();
//        scope.$apply(function () {
//            scope.pageIndex = 1;
//            scope.quickSearchStockRemain.filters.pageIndex = 1;
//            scope.quickSearchStockRemain.filters.sortedBy = sortedBy;
//            scope.quickSearchStockRemain.filters.sortedDirection = sortedDirection;
//            scope.quickSearchStockRemain.method.search();
//        });
//    }
//);

//var grdReceiptDetail = jQuery('#grdReceiptDetail').scrollableTable(null,
//    function (sortedBy, sortedDirection) {
//        var scope = angular.element(jQuery('body')).scope();
//        datasource = scope.data.factoryMaterialReceiptDetails;
//        if (sortedDirection == 'asc') {
//            datasource.sort(function (a, b) {
//                return a[sortedBy] > b[sortedBy] ? 1 : -1;
//            });
//        }
//        else if (sortedDirection == 'desc') {
//            datasource.sort(function (a, b) {
//                return a[sortedBy] < b[sortedBy] ? 1 : -1;
//            });
//    }
//    scope.$apply();
//});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);
tilsoftApp.filter('sumFunction', function () {
    return function (data, key) {
        if (angular.isUndefined(data) || angular.isUndefined(key))
            return 0;
        var sum = 0;
        angular.forEach(data, function (v, k) {
            sum = sum + parseFloat(v[key] == null ? 0 : v[key]);
        });
        return sum;
    }
});
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.factories = null;
    $scope.newID = -1;
    $scope.receiptTypes = [{ receiptTypeID: 1, receiptTypeName: 'Import' }, { receiptTypeID: 2, receiptTypeName: 'Export' }];
    $scope.selectedReceiptType = 1;

    $scope.ui = {
        screenName: 'receipt',
        title : 'General'
    }

    $scope.gridFilter = {
        clientUD: '',
        proformaInvoiceNo: '',
        lds: '',
        articleCode: '',
        description: ''
    }

    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
            //do need do nothing
        },
        sort: function (sortedBy, sortedDirection) {
            datasource = $scope.data.factoryStockReceiptDetails;
            if (sortedDirection == 'asc') {
                datasource.sort(function (a, b) {
                    return a[sortedBy] > b[sortedBy] ? 1 : -1;
                });
            }
            else if (sortedDirection == 'desc') {
                datasource.sort(function (a, b) {
                    return a[sortedBy] < b[sortedBy] ? 1 : -1;
                });
            }
            $scope.$apply();
        },
        isTriggered: false
    };

    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.factories = data.data.factories;
                    $scope.$apply();
                    jQuery('#content').show();
                    //
                    if (context.id == 0)
                    {
                        jsHelper.showPopup('receiptTypeForm', function () {
                            $scope.data.factoryID = 374043; //374043 - PAL-36-VHM
                        });
                    }
                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                    //show mesage if loaded error
                    if (data.message.type == 2) {
                        jsHelper.processMessage(data.message);
                    }

                    //gridHandler
                    $scope.gridHandler.refresh();
                    $scope.gridHandler.goTop();
                    $scope.gridHandler.isTriggered = true;
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
                            $scope.method.refresh(data.data.factoryStockReceiptID);
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

        removeProduct: function ($event, factoryStockReceiptDetailID) {
            $event.preventDefault();
            var j = -1;
            for (var i = 0; i < $scope.data.factoryStockReceiptDetails.length; i++) {
                if ($scope.data.factoryStockReceiptDetails[i].factoryStockReceiptDetailID == factoryStockReceiptDetailID) {
                    j = i;
                    break;
                }
            }
            if (j >= 0) {
                $scope.data.factoryStockReceiptDetails.splice(j, 1);
            }
            $scope.$apply();
        },

        onNextButtonClick: function () {

            if ($scope.data.receiptTypeID === null) {
                bootbox.alert({ message: 'You have to select receipt type', size: 'small' });
                return;
            }

            if ($scope.data.factoryID == null)
            {
                bootbox.alert({ message: 'You have to select factory', size : 'small' });
                return;
            }
            jsHelper.hidePopup('receiptTypeForm', function () { });
        },

        changeProducedQnt: function (item) {
            item.notPackedQnt = item.producedQnt - item.totalPackedQnt;
            item.totalStockQnt = item.notPackedQnt + item.packedQnt;
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
        },
        getReceiptTypeName: function (id) {
            if ($scope.receiptTypes == null) return '';
            name = '';
            for (i = 0; i < $scope.receiptTypes.length; i++) // nen dung for thay vi angular.forEach doi voi truong hop tim gia tri (tim thay la lap tuc exit vong for, angular.forEach ko exit duoc)
            {
                item = $scope.receiptTypes[i];
                if (item.receiptTypeID == id) {
                    name = item.receiptTypeName;
                    break;
                }
            }
            return name;
        },
        getFactoryUD: function (id) {
            if ($scope.factories == null) return '';
            ud = '';
            for (i = 0; i < $scope.factories.length; i++) // nen dung for thay vi angular.forEach doi voi truong hop tim gia tri (tim thay la lap tuc exit vong for, angular.forEach ko exit duoc)
            {
                item = $scope.factories[i];
                if (item.factoryID == id) {
                    ud = item.factoryUD;
                    break;
                }
            }
            return ud;
        },

        getTotalStock: function (item) {
            return parseInt(item.notPackedQnt == null ? 0 : item.notPackedQnt) + parseInt(item.packedQnt == null ? 0 : item.packedQnt);
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
                            var totalRows = data.totalRows;

                            $scope.quickSearchProduct.totalPage = Math.ceil(totalRows / $scope.quickSearchProduct.filters.pageSize);
                            quickSearchResultGridMaterial.updateLayout();
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

            itemSelected: function ($event) {
                angular.forEach($scope.quickSearchProduct.data, function (product_item) {
                    if (product_item.isSelected)
                    {
                        $scope.data.factoryStockReceiptDetails.push({
                            factoryStockReceiptDetailID: $scope.method.getNewID(),
                            productID: product_item.productID,
                            articleCode: product_item.articleCode,
                            description: product_item.description,
                            totalStockQnt : product_item.totalStockQnt
                        });
                    }
                });
                $scope.quickSearchProduct.event.close($event);
            }
        }
    },


    //quick search stock remain
    searchStockRemainTimer = null;
    $scope.quickSearchStockRemain = {
        data: null,
        filters: {
            filters: {
                articleCode: '',
                description: '',
                factoryID: null,
            },
            sortedBy: 'ArticleCode',
            sortedDirection: 'ASC',
            pageSize: 20,
            pageIndex: 1
        },
        totalPage: 0,

        method: {
            search: function () {
                $scope.quickSearchStockRemain.filters.filters.factoryID = $scope.data.factoryID;
                jsonService.quickSearchStockRemain(
                    $scope.quickSearchStockRemain.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quickSearchStockRemain.data = data.data.data;

                            var totalRows = data.data.totalRows;

                            $scope.quickSearchStockRemain.totalPage = Math.ceil(totalRows / $scope.quickSearchStockRemain.filters.pageSize);
                            grdStockRemain.updateLayout();
                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        event: {
            openScreen: function ($event) {
                $event.preventDefault();
                $scope.ui.screenName = 'search-stock-remain';
                $scope.ui.title = 'Select product to export'
            },

            search: function ($event) {
                $event.preventDefault();
                $scope.quickSearchStockRemain.filters.pageIndex = 1;
                $scope.quickSearchStockRemain.method.search();
            },

            close: function ($event) {
                $event.preventDefault();
                $scope.ui.screenName = 'receipt';
                $scope.ui.title = 'General'
            },
            itemSelected: function ($event) {
                $event.preventDefault();

                angular.forEach($scope.quickSearchStockRemain.data, function (stock_remain_item) {
                    if (stock_remain_item.isSelected)
                    {
                        if ($scope.data.factoryStockReceiptDetails.filter(function (o) { return o.productID == stock_remain_item.productID }).length == 0)
                        {
                            $scope.data.factoryStockReceiptDetails.push({
                                factoryStockReceiptDetailID: $scope.method.getNewID(),
                                productID: stock_remain_item.productID,
                                totalStockQnt: stock_remain_item.totalStockQnt,
                                articleCode: stock_remain_item.articleCode,
                                description: stock_remain_item.description,
                            });
                        }
                    }
                });
                $scope.quickSearchStockRemain.event.close($event);
            }
        }
    }



    //
    // init
    //
    $scope.event.load();
}]);