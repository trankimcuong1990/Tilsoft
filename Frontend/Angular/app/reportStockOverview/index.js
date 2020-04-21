//
// SUPPORT
//
jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});


//var grdSearchResult = jQuery('#grdSearchResult').sortableTable(
//    function (sortedBy, sortedDirection) {

//        var scope = angular.element(jQuery('body')).scope();
//        datasource = scope.data;
//        if (sortedDirection == 'asc') {
//            datasource.sort(function (a, b) {
//                return a[sortedBy] > b[sortedBy] ? 1 : -1;
//            });
//        }
//        else if (sortedDirection == 'desc') {
//            datasource.sort(function (a, b) {
//                return a[sortedBy] < b[sortedBy] ? 1 : -1;
//            });
//        }
//        scope.$apply();
//    }
//);

var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.filters = {
        articleCode: '',
        description: '',
    };
    $scope.data = null;
    $scope.months = null;


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

        calTotalFTS_Onsea: function () {
            if ($scope.data == null) return;
            var total = parseInt('0');
            angular.forEach($scope.data, function (item) {
                total += (JSON.stringify(item.ftS_Onsea) == 'null' ? parseInt('0') : parseInt(item.ftS_Onsea));
            }, total);
            return total;
        },

        calTotalFTS_Onsea_TobeShipped: function () {
            if ($scope.data == null) return;
            var total = parseInt('0');
            angular.forEach($scope.data, function (item) {
                total += (JSON.stringify(item.ftS_Onsea_TobeShipped) == 'null' ? parseInt('0') : parseInt(item.ftS_Onsea_TobeShipped));
            }, total);
            return total;
        },

        //total cont

        calTotalOnseaIn40HC: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            var qnt40HC = parseFloat('0');
            angular.forEach($scope.data, function (item) {
                qnt40HC = JSON.stringify(item.qnt40HC) == 'null' ? parseFloat('0') : parseFloat(item.qnt40HC);
                if (qnt40HC != 0) total += (JSON.stringify(item.onSeaQnt) == 'null' ? parseFloat('0') : parseFloat(item.onSeaQnt) / qnt40HC);
            }, total);
            return total;
        },

        calTotalTobeShippedIn40HC: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            var qnt40HC = parseFloat('0');
            angular.forEach($scope.data, function (item) {
                qnt40HC = JSON.stringify(item.qnt40HC) == 'null' ? parseFloat('0') : parseFloat(item.qnt40HC);
                if (qnt40HC != 0) total += (JSON.stringify(item.tobeShippedQnt) == 'null' ? parseFloat('0') : parseFloat(item.tobeShippedQnt) / qnt40HC);
            }, total);
            return total;
        },

        calTotalReservationIn40HC: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            var qnt40HC = parseFloat('0');
            angular.forEach($scope.data, function (item) {
                qnt40HC = JSON.stringify(item.qnt40HC) == 'null' ? parseFloat('0') : parseFloat(item.qnt40HC);
                if (qnt40HC != 0) total += (JSON.stringify(item.reservationQnt) == 'null' ? parseFloat('0') : parseFloat(item.reservationQnt) / qnt40HC);
            }, total);
            return total;
        },

        calTotalFTSIn40HC: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            var qnt40HC = parseFloat('0');
            angular.forEach($scope.data, function (item) {
                qnt40HC = JSON.stringify(item.qnt40HC) == 'null' ? parseFloat('0') : parseFloat(item.qnt40HC);
                if (qnt40HC != 0) total += (JSON.stringify(item.ftsQnt) == 'null' ? parseFloat('0') : parseFloat(item.ftsQnt) / qnt40HC);
            }, total);
            return total;
        },

        calTotalFTS_OnseaIn40HC: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            var qnt40HC = parseFloat('0');
            angular.forEach($scope.data, function (item) {
                qnt40HC = JSON.stringify(item.qnt40HC) == 'null' ? parseFloat('0') : parseFloat(item.qnt40HC);
                if (qnt40HC != 0) total += (JSON.stringify(item.ftS_Onsea) == 'null' ? parseFloat('0') : parseFloat(item.ftS_Onsea) / qnt40HC);
            }, total);
            return total;
        },

        calTotalFTS_Onsea_TobeShippedIn40HC: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            var qnt40HC = parseFloat('0');
            angular.forEach($scope.data, function (item) {
                qnt40HC = JSON.stringify(item.qnt40HC) == 'null' ? parseFloat('0') : parseFloat(item.qnt40HC);
                if (qnt40HC != 0) total += (JSON.stringify(item.ftS_Onsea_TobeShipped) == 'null' ? parseFloat('0') : parseFloat(item.ftS_Onsea_TobeShipped) / qnt40HC);
            }, total);
            return total;
        },
        
    }


    //
    // event
    //
    $scope.event = {
        search: function () {
            jsonService.getStockOverview($scope.filters,
                function (data) {
                    $scope.data = data.data;
                    $scope.$apply();
                    jQuery('#content').show();                    
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.$apply();
                }
            );
        },
    }

    $scope.exportExcelForm = {
        
        months : null,
        filters :{
            month: 5,
            isIncludeImage: false,
            isIncludeDailyImageProduct: false,
        },

        event: {
            load: function ($event) {
                $event.preventDefault();

                //create month list
                if ($scope.exportExcelForm.months == null)
                {
                    $scope.exportExcelForm.months = [];
                    $scope.exportExcelForm.months.push({ id: 1, name: 'Jan' });
                    $scope.exportExcelForm.months.push({ id: 2, name: 'Feb' });
                    $scope.exportExcelForm.months.push({ id: 3, name: 'Mar' });
                    $scope.exportExcelForm.months.push({ id: 4, name: 'Apr' });
                    $scope.exportExcelForm.months.push({ id: 5, name: 'May' });
                    $scope.exportExcelForm.months.push({ id: 6, name: 'Jun' });
                    $scope.exportExcelForm.months.push({ id: 7, name: 'Jul' });
                    $scope.exportExcelForm.months.push({ id: 8, name: 'Aug' });
                    $scope.exportExcelForm.months.push({ id: 9, name: 'Sep' });
                    $scope.exportExcelForm.months.push({ id: 10, name: 'Oct' });
                    $scope.exportExcelForm.months.push({ id: 11, name: 'Nov' });
                    $scope.exportExcelForm.months.push({ id: 12, name: 'Dec' });
                }

                //show from export
                $('#frmExportExcel').modal('show');
            },
            
            cancel: function ($event) {
                $event.preventDefault();
                $('#frmExportExcel').modal('hide');
            },
            exportExcel: function ($event) {
                $event.preventDefault();

                jsonService.getReport ( $scope.exportExcelForm.filters,
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

    $scope.stockListDetailForm = {
        product: null,
        stockListDetailData: null,
        infoType: null,

        event: {
            load: function ($event, productRow, infoType) {
                $event.preventDefault();
                $scope.stockListDetailForm.infoType = infoType;
                $scope.stockListDetailForm.product = productRow;

                $scope.stockListDetailForm.method.getStockListDetail();
            },
        },

        method: {
            getStockListDetail: function () {
                //set json service connect to stock list
                jsonService.serviceUrl = context.stockListServiceUrl;
                //get data
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
                //back to json service current
                jsonService.serviceUrl = context.stockOverviewServiceUrl;
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
                    if (item.importTypeNM == 'RESERVATION') {
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

    $scope.event.search();
}]);