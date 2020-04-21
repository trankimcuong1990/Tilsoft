//
// SUPPORT
//
jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

//var searchResultGrid = jQuery('#grdSearchResult').scrollableTable(
//    function (currentPage) {
//        var scope = angular.element(jQuery('body')).scope();
//        scope.$apply(function () {
//            scope.pageIndex = currentPage;
//            jsonService.searchFilter.pageIndex = currentPage;
//            scope.event.search();
//        });
//    },
//    function (sortedBy, sortedDirection) {
//        var scope = angular.element(jQuery('body')).scope();
//        scope.$apply(function () {
//            scope.pageIndex = 1;
//            jsonService.searchFilter.pageIndex = 1;
//            jsonService.searchFilter.sortedBy = sortedBy;
//            jsonService.searchFilter.sortedDirection = sortedDirection;
//            scope.event.search();
//        });
//    }
//);

var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives','furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', function ($scope, $cookieStore) {
    //
    // property
    //
    //$scope.filters = {
    //    articleCode: '',
    //    description: '',
    //    productStatusIDs: '',
    //    wareHouseAreaIDs: '',
    //    isIncludeImage: false,

    //    selectedProductStatuses: [],
    //    selectedWarehouseAreas : []
    //};

    $scope.filters = {
        productStatusIDs: '',
        isMatchedImage: 'any',
        isHaveImage: 'any',
        articleCode: '',
        description: '',
        qnt40HC: '',
        physicalQnt: '',
        physicalQntIn40HC: '',
        wareHouseAreaIDs: '',
        qntPerWarehouseArea: '',
        onSeaQnt: '',
        tobeShippedQnt: '',
        reservationQnt: '',
        ftsQnt: '',
        isActiveFreeToSale: 'any',
        eanCode: '',
        freeToSaleCategoryIDs: '',

        //temp filter field
        productStatusID: null,
        wareHouseAreaID: null,
        freeToSaleCategoryID : null,
        isIncludeImage: false,
    };

    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;

    $scope.data = null;
    $scope.freeToSaleCategories = null;
    $scope.productStatuses = null;
    $scope.warehouseAreas = null;
    $scope.productSetEANCodes = null;

    //total fields
    $scope.totalPhysicalQnt = 0;
    $scope.totalPhysicalQntIn40HC = 0;
    $scope.totalOnSeaQnt = 0;
    $scope.totalTobeShippedQnt = 0;
    $scope.totalReservationQnt = 0;
    $scope.totalFTSQnt = 0;

    //sub total fields
    $scope.subTotalPhysicalQnt = 0;
    $scope.subTotalPhysicalQntIn40HC = 0;
    $scope.subTotalOnSeaQnt = 0;
    $scope.subTotalTobeShippedQnt = 0;
    $scope.subTotalReservationQnt = 0;
    $scope.subTotalFTSQnt = 0;

    //
    // grid handler
    //
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
            $scope.event.search(true);
        },
        isTriggered: false
    };

    //
    // event
    //
    $scope.event = {
        reload: function () {
            $scope.data = [];
            $scope.productSetEANCodes = [];
            //assign pager for search result
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = 1;
            jsonService.searchFilter.sortedBy = 'DisplayOrder';
            jsonService.searchFilter.sortedDirection = 'DESC';

            /*
                //assign area filter for search result
                //selectedAreas = $("#locationSelect2").val();
                selectedAreas = $scope.filters.selectedWarehouseAreas
                $scope.filters.wareHouseAreaIDs = "";
                for (i = 0; i < selectedAreas.length; i++) {
                    $scope.filters.wareHouseAreaIDs += selectedAreas[i] + ',';
                }
            
                //assign product status filter for search result
                //selectedProStatuses = $("#productStatusSelect2").val();
                selectedProStatuses = $scope.filters.selectedProductStatuses;
                $scope.filters.productStatusIDs = "";
                for (i = 0; i < selectedProStatuses.length; i++) {
                    $scope.filters.productStatusIDs += selectedProStatuses[i] + ',';
                }
            */
            $scope.filters.productStatusIDs = '';
            if ($scope.filters.productStatusID != null) {
                $scope.filters.productStatusIDs = $scope.filters.productStatusID + ',';
            }
            $scope.filters.wareHouseAreaIDs = '';
            if ($scope.filters.wareHouseAreaID != null) {
                $scope.filters.wareHouseAreaIDs = $scope.filters.wareHouseAreaID + ',';
            }
            $scope.filters.freeToSaleCategoryIDs = '';
            if ($scope.filters.freeToSaleCategoryID != null) {
                $scope.filters.freeToSaleCategoryIDs = $scope.filters.freeToSaleCategoryID + ',';
            }

            //search data
            $scope.event.search();            
        },
        search: function (isLoadMore) {
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    //$scope.data = data.data.data;
                    Array.prototype.push.apply($scope.data, data.data.data);

                    //total
                    $scope.totalPhysicalQnt = data.data.totalPhysicalQnt;
                    $scope.totalPhysicalQntIn40HC = data.data.totalPhysicalQntIn40HC;
                    $scope.totalOnSeaQnt = data.data.totalOnSeaQnt;
                    $scope.totalTobeShippedQnt = data.data.totalTobeShippedQnt;
                    $scope.totalReservationQnt = data.data.totalReservationQnt;
                    $scope.totalFTSQnt = data.data.totalFTSQnt;

                    //sub total
                    $scope.subTotalPhysicalQnt = data.data.subTotalPhysicalQnt;
                    $scope.subTotalPhysicalQntIn40HC = data.data.subTotalPhysicalQntIn40HC;
                    $scope.subTotalOnSeaQnt = data.data.subTotalOnSeaQnt;
                    $scope.subTotalTobeShippedQnt = data.data.subTotalTobeShippedQnt;
                    $scope.subTotalReservationQnt = data.data.subTotalReservationQnt;
                    $scope.subTotalFTSQnt = data.data.subTotalFTSQnt;

                    //$scope.freeToSaleCategories = data.data.freeToSaleCategories;
                    if ($scope.freeToSaleCategories == null) $scope.freeToSaleCategories = data.data.freeToSaleCategories;
                    if ($scope.productStatuses==null) $scope.productStatuses = data.data.productStatuses;
                    if ($scope.warehouseAreas == null) $scope.warehouseAreas = data.data.warehouseAreas;

                    //get eac code list
                    Array.prototype.push.apply($scope.productSetEANCodes, data.data.productSetEANCodes);

                    //get total row
                    $scope.totalRows = data.totalRows;

                    //cal total page
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.$apply();
                    jQuery('#content').show();

                    //gridHandler
                    $scope.gridHandler.refresh();
                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;                    
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.$apply();
                }
            );
        },

        real_ref_Selected: function (item) {
            isMatchedImage = (item.isMatchedImage == 1 ? true : false);
            jsonService.matchedImageProduct(item.goodsID, isMatchedImage,
                    function (data) {
                        if (data.message.type != 2) {
                            if (isMatchedImage) {
                                document.getElementById(item.keyID).src = '';
                                item.selectedThumbnailImage = item.productThumbnailImage;
                                item.selectedFullImage = item.productFullImage;
                            }
                            else {
                                item.selectedThumbnailImage = item.modelThumbnailImage;
                                item.selectedFullImage = item.modelFullImage;
                            }
                            $scope.$apply();
                        }
                        else {
                            jsHelper.processMessage(data.message);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
        },

        clearFilter: function () {
            $scope.filters = {
                productStatusIDs: '',
                isMatchedImage: 'any',
                isHaveImage: 'any',
                articleCode: '',
                description: '',
                qnt40HC: '',
                physicalQnt: '',
                physicalQntIn40HC: '',
                wareHouseAreaIDs: '',
                qntPerWarehouseArea: '',
                onSeaQnt: '',
                tobeShippedQnt: '',
                reservationQnt: '',
                ftsQnt: '',
                isActiveFreeToSale: 'any',
                eanCode : '',
                freeToSaleCategoryIDs: '',

                //temp filter field
                productStatusID: null,
                wareHouseAreaID: null,
                freeToSaleCategoryID: null,
                isIncludeImage: false,
            };
            $scope.event.reload();
        },

    }

    $scope.infoDetailForm = {
        product: null,
        stockReservationData : null,
        event: {
            load: function ($event,productRow) {
                $event.preventDefault();
                $scope.infoDetailForm.product = productRow;
                $('#frmInfoDetail').modal('show');
                $scope.infoDetailForm.method.getStockReservation();
            },
            
            cancel: function ($event) {
                $event.preventDefault();
                $('#frmInfoDetail').modal('hide');
            },
            ok: function ($event, index) {
                $event.preventDefault();
                $('#frmInfoDetail').modal('hide');
            },

            activeFTS: function(){
                if ($scope.infoDetailForm.product.productType == 'PRODUCT') {
                    $scope.infoDetailForm.method.activeFreeToSaleProduct();
                }
                else if ($scope.infoDetailForm.product.productType == 'SPAREPART') {
                    alert('This is sparepart. You can can not enable free to sale');
                }
            },

            selectedCategory: function () {
                if ($scope.infoDetailForm.product.productType == 'PRODUCT')
                {
                    $scope.infoDetailForm.method.setFreeToSaleCategory();
                }
                else if ($scope.infoDetailForm.product.productType == 'SPAREPART') {
                    alert('This is sparepart. You can can not set categories');
                }
            }
        },

        method: {
            activeFreeToSaleProduct: function () {
                jsonService.activeFreeToSaleProduct($scope.infoDetailForm.product.goodsID, $scope.infoDetailForm.product.isActiveFreeToSale,
                    function (data) {
                        jsHelper.processMessage(data.message);
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },

            setFreeToSaleCategory: function () {
                jsonService.setFreeToSaleCategory($scope.infoDetailForm.product.goodsID, $scope.infoDetailForm.product.freeToSaleCategoryID,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type != 2) {
                            if ($scope.infoDetailForm.product.freeToSaleCategoryID != null) {
                                var category = $scope.freeToSaleCategories.filter(function (o) { return o.freeToSaleCategoryID == $scope.infoDetailForm.product.freeToSaleCategoryID })[0];
                                $scope.infoDetailForm.product.freeToSaleCategoryNM = category.freeToSaleCategoryNM;
                            }
                            else {
                                $scope.infoDetailForm.product.freeToSaleCategoryNM = '';
                            }
                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },

            getStockReservation: function () {
                jsonService.getStockReservation($scope.infoDetailForm.product.goodsID, $scope.infoDetailForm.product.productType, $scope.infoDetailForm.product.productStatusID,
                    function (data) {
                        $scope.infoDetailForm.stockReservationData = data.data;
                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },            
        }
    }


    $scope.stockListDetailForm = {
        product: null,
        stockListDetailData: null,
        infoType : null,

        event: {
            load: function ($event, productRow,infoType) {
                $event.preventDefault();
                $scope.stockListDetailForm.infoType = infoType;
                $scope.stockListDetailForm.product = productRow;

                $scope.stockListDetailForm.method.getStockListDetail();
            },
        },

        method: {
            getStockListDetail: function () {
                jsonService.getStockListDetail($scope.stockListDetailForm.product.keyID,
                    function (data) {
                        $scope.stockListDetailForm.stockListDetailData = data.data;
                        $scope.$apply();

                        $('#frmStockListDetail').modal('show');
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },

            calTotalImport: function () {
                total = 0;
                angular.forEach($scope.stockListDetailForm.stockListDetailData.warehouseImportDetails, function (item) {
                    total += item.importQnt;
                });
                return total;
            },

            calTotalPicked: function () {
                total = 0;
                angular.forEach($scope.stockListDetailForm.stockListDetailData.warehousePickingListDetails, function (item) {
                    total += item.pickedQnt;
                });
                return total;
            },

            calTotal_FobWarehouse_Shipped: function () {
                total = 0;
                angular.forEach($scope.stockListDetailForm.stockListDetailData.loadingPlanDetails, function (item) {
                    total += item.fobWarehouse_ShippedQnt;
                });
                return total;
            },

            calTotal_FobWarehouse_Imported: function () {
                total = 0;
                angular.forEach($scope.stockListDetailForm.stockListDetailData.warehouseImportDetails, function (item) {
                    if (item.importTypeNM == 'RESERVATION')
                    {
                        total += item.fobWarehouse_ImportedQnt;
                    }
                });
                return total;
            },

            calTotal_FobWarehouse_Reservation: function () {
                total = 0;
                angular.forEach($scope.stockListDetailForm.stockListDetailData.saleOrderDetails, function (item) {
                    if (item.orderType == 'FOB_WAREHOUSE') {
                        total += item.fobWarehouse_ReservationQnt;
                    }
                });
                return total;
            },

            calTotal_Reservation: function () {
                total = 0;
                angular.forEach($scope.stockListDetailForm.stockListDetailData.saleOrderDetails, function (item) {
                    total += item.reservationQnt;
                });
                return total;
            },

            calTotal_ReservationRemain: function () {
                total = 0;
                angular.forEach($scope.stockListDetailForm.stockListDetailData.saleOrderDetails, function (item) {
                    total += item.remainReservationQnt;
                });
                return total;
            },
        }
    }

    $scope.exportExcelForm = {

        months: null,
        filters: {
            month: 5,
            isIncludeImage: false,
            isIncludeDailyImageProduct: false,
        },

        event: {
            load: function ($event) {
                $event.preventDefault();
                /*select2 info :how to get and set value
                console.log($('#locationSelect2').val()); // get array of string with only id
                console.log($('#locationSelect2').select2('data')); //get array of object include id and text
                $('#locationSelect2').select2('data', [{ "id": "2", "text": "abc" }]); //set value for select2
                */

                //get area filter in html search form to fill-in export excel filter form
                //$('#locationSelect2_').select2('data', $('#locationSelect2').select2('data'));

                //get productstatus filter in html search form to fill-in export excel filter form
                //$('#productStatusSelect2_').select2('data', $('#productStatusSelect2').select2('data'));

                //show from export
                $('#frmExportExcel').modal('show');
            },

            cancel: function ($event) {
                $event.preventDefault();
                $('#frmExportExcel').modal('hide');
            },
            exportExcel: function ($event) {
                $event.preventDefault();

                /*
                //assign area filter for export excel function
                //selectedAreas = $("#locationSelect2_").val();
                selectedAreas = $scope.filters.selectedWarehouseAreas;
                $scope.filters.wareHouseAreaIDs = "";
                for (i = 0; i < selectedAreas.length; i++) {
                    $scope.filters.wareHouseAreaIDs += selectedAreas[i] + ',';
                }
                //assign product status filter for export excel function
                //selectedProStatuses = $("#productStatusSelect2_").val();
                selectedProStatuses = $scope.filters.selectedProductStatuses;
                $scope.filters.productStatusIDs = "";
                for (i = 0; i < selectedProStatuses.length; i++) {
                    $scope.filters.productStatusIDs += selectedProStatuses[i] + ',';
                }
                */

                //export excel
                jsonService.getReportStockList($scope.filters,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type != 2) {
                            window.location = context.reportUrl + data.data + '.xlsm';
                            $('#frmExportExcel').modal('hide');
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );

            },
        },
    }

    //
    //total function
    //
    $scope.method = {

        calTotalPhysical: function () {
            if ($scope.data == null) return;
            var total = parseInt('0');
            angular.forEach($scope.data, function (item) {
                total += (JSON.stringify(item.physicalQnt) == 'null' ? parseInt('0') : parseInt(item.physicalQnt));
            }, total);
            return total;
        },

        calTotalPhysicalIn40HC: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            angular.forEach($scope.data, function (item) {
                total += (JSON.stringify(item.physicalQntIn40HC) == 'null' ? parseFloat('0') : parseFloat(item.physicalQntIn40HC));
            }, total);
            return total;
        },

        calTotalOnsea: function () {
            if ($scope.data == null) return;
            var total = parseInt('0');
            angular.forEach($scope.data, function (item) {
                total += (JSON.stringify(item.onSeaQnt) == 'null' ? parseInt('0') : parseInt(item.onSeaQnt));
            }, total);
            return total;
        },

        calTotalTobeShipped: function () {
            if ($scope.data == null) return;
            var total = parseInt('0');
            angular.forEach($scope.data, function (item) {
                total += (JSON.stringify(item.tobeShippedQnt) == 'null' ? parseInt('0') : parseInt(item.tobeShippedQnt));
            }, total);
            return total;
        },

        calTotalReservation: function () {
            if ($scope.data == null) return;
            var total = parseInt('0');
            angular.forEach($scope.data, function (item) {
                total += (JSON.stringify(item.reservationQnt) == 'null' ? parseInt('0') : parseInt(item.reservationQnt));
            }, total);
            return total;
        },

        calTotalFTS: function () {
            if ($scope.data == null) return;
            var total = parseInt('0');
            angular.forEach($scope.data, function (item) {
                total += (JSON.stringify(item.ftsQnt) == 'null' ? parseInt('0') : parseInt(item.ftsQnt));
            }, total);
            return total;
        },


        //total cont
    }



    $scope.event.reload();
}]);