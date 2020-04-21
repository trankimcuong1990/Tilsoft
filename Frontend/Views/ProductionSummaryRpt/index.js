jQuery('.search-filter').keypress(
    function (event) {
        if (event.keyCode === 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.search();
        }
    }
);
(function () {
    'use strict';
    var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ngCookies']);
    tilsoftApp.controller('tilsoftController', productionSummaryRptController);
    productionSummaryRptController.$inject = ['$scope', '$timeout', '$cookieStore', 'dataService'];

    function productionSummaryRptController($scope, $timeout, $cookieStore, productionSummaryRptService) {
        // variable
        $scope.data = [];
        $scope.productionSummaries = [];
        $scope.productionSummaryDetails = [];
        $scope.seasons = [];
        $scope.factories = [];
        $scope.workCenters = [];
        $scope.detailRow = [];
        $scope.totalOrderQnt = 0;
        $scope.totalProQnt = 0;
        $scope.filter = {
            factoryID: '',
            season: '',
            clientUD: '',
            pi: ''
        };

        $scope.gridHandler = {
            loadMore: function () {     
                //$scope.event.search(true);
            },
            sort: function (sortedBy, sortedDirection) {
                //$scope.event.search();
            },
            isTriggered: false
        };

        $scope.totalProduct = {
            frameImport: 0,
            frameFinish: 0,
            frameExport: 0,
            frameRemain: 0,

            paintedImport: 0,
            paintedFinish: 0,
            paintedExport: 0,
            paintedRemain: 0,

            weavingImport: 0,
            weavingFinish: 0,
            weavingExport: 0,
            weavingRemain: 0,

            packingImport: 0,
            packingFinish: 0,
            packingExport: 0,
            packingRemain: 0
        };

        // event
        $scope.event = {
            init: init,
            search: search,
            totalOrder40HC: function () {
                var totalOrder40HC = 0;

                angular.forEach($scope.productionSummaries, function (item) {
                    totalOrder40HC = totalOrder40HC + item.order40HC;
                });

                return totalOrder40HC;
            },
            totalPro40HC: function () {
                var totalPro40HC = 0;

                angular.forEach($scope.productionSummaries, function (item) {
                    totalPro40HC = totalPro40HC + item.pro40HC;
                });

                return totalPro40HC;
            },
            totalImport40HC: function (workCenterID) {
                var import40HC = 0;

                angular.forEach($scope.productionSummaryDetails, function (item) {
                    if (item.workCenterID == workCenterID) {
                        import40HC = import40HC + item.totalImport40HC;
                    }
                });

                return import40HC;
            },
            totalFinish40HC: function (workCenterID) {
                var finish40HC = 0;

                angular.forEach($scope.productionSummaryDetails, function (item) {
                    if (item.workCenterID == workCenterID) {
                        finish40HC = finish40HC + item.totalFinish40HC;
                    }
                });

                return finish40HC;
            },
            totalExport40HC: function (workCenterID) {
                var export40HC = 0;

                angular.forEach($scope.productionSummaryDetails, function (item) {
                    if (item.workCenterID == workCenterID) {
                        export40HC = export40HC + item.totalExport40HC;
                    }
                });

                return export40HC;
            },
            totalRemain40HC: function (workCenterID) {
                var remain40HC = 0;

                angular.forEach($scope.productionSummaryDetails, function (item) {
                    if (item.workCenterID == workCenterID) {
                        remain40HC = remain40HC + item.totalRemain40HC;
                    }
                });

                return remain40HC;
            },
        };

        var a = 0;

        // method
        $scope.method = {
            getDetailRow: function (item) {
                var result = [];
                for (var i = 0; i < 4; i++) {
                    result.push({
                        colName: 'import',
                        workCenterID: item[i].workCenterID
                    });
                    result.push({
                        colName: 'finish',
                        workCenterID: item[i].workCenterID
                    });
                    result.push({
                        colName: 'export',
                        workCenterID: item[i].workCenterID
                    });
                    result.push({
                        colName: 'remain',
                        workCenterID: item[i].workCenterID
                    });
                };
                return result;
            },

            getImport: function (productID, workCenterID) {
                var result = parseFloat(0);
                for (var i = 0; i < $scope.productionSummaryDetails.length; i++) {
                    var rItem = $scope.productionSummaryDetails[i];
                    if (rItem.productID === productID && rItem.workCenterID === workCenterID) {
                        result = rItem.totalImportQnt;
                    }
                }
                if (result === 0 || result === "") {
                    result = "";
                }
                return result;
            },

            getFinish: function (productID, workCenterID) {
                var result = parseFloat(0);
                for (var i = 0; i < $scope.productionSummaryDetails.length; i++) {
                    var rItem = $scope.productionSummaryDetails[i];
                    if (rItem.productID === productID && rItem.workCenterID === workCenterID) {
                        result = rItem.totalFinishQnt;
                    }
                }
                if (result === 0 || result === "") {
                    result = "";
                }
                return result;
            },

            getRemain: function (productID, workCenterID) {
                var result = parseFloat(0);
                for (var i = 0; i < $scope.productionSummaryDetails.length; i++) {
                    var rItem = $scope.productionSummaryDetails[i];
                    if (rItem.productID === productID && rItem.workCenterID === workCenterID) {
                        result = rItem.totalRemainQnt;
                    }
                }
                if (result === 0 || result === "") {
                    result = "";
                }
                return result;
            },

            getExport: function (productID, workCenterID) {
                var result = parseFloat(0);
                for (var i = 0; i < $scope.productionSummaryDetails.length; i++) {
                    var rItem = $scope.productionSummaryDetails[i];
                    if (rItem.productID === productID && rItem.workCenterID === workCenterID) {
                        result = rItem.totalExportQnt;
                    }
                }
                if (result === 0 || result === "") {
                    result = "";
                }
                return result;
            },

            calTotalImport: function () {
                for (var i = 0; i < $scope.productionSummaryDetails.length; i++) {
                    var rItem = $scope.productionSummaryDetails[i];
                    if (rItem.workCenterID === 7) {
                        $scope.totalProduct.frameImport += rItem.totalImportQnt;
                    }
                    if (rItem.workCenterID === 8) {
                        $scope.totalProduct.paintedImport += rItem.totalImportQnt;
                    }
                    if (rItem.workCenterID === 9) {
                        $scope.totalProduct.weavingImport += rItem.totalImportQnt;
                    }
                    if (rItem.workCenterID === 11) {
                        $scope.totalProduct.packingImport += rItem.totalImportQnt;
                    }
                }
            },
            calTotalFinish: function () {
                for (var i = 0; i < $scope.productionSummaryDetails.length; i++) {
                    var rItem = $scope.productionSummaryDetails[i];
                    if (rItem.workCenterID === 7) {
                        $scope.totalProduct.frameFinish += rItem.totalFinishQnt;
                    }
                    if (rItem.workCenterID === 8) {
                        $scope.totalProduct.paintedFinish += rItem.totalFinishQnt;
                    }
                    if (rItem.workCenterID === 9) {
                        $scope.totalProduct.weavingFinish += rItem.totalFinishQnt;
                    }
                    if (rItem.workCenterID === 11) {
                        $scope.totalProduct.packingFinish += rItem.totalFinishQnt;
                    }
                }
            },
            calTotalExport: function () {
                for (var i = 0; i < $scope.productionSummaryDetails.length; i++) {
                    var rItem = $scope.productionSummaryDetails[i];
                    if (rItem.workCenterID === 7) {
                        $scope.totalProduct.frameExport += rItem.totalExportQnt;
                    }
                    if (rItem.workCenterID === 8) {
                        $scope.totalProduct.paintedExport += rItem.totalExportQnt;
                    }
                    if (rItem.workCenterID === 9) {
                        $scope.totalProduct.weavingExport += rItem.totalExportQnt;
                    }
                    if (rItem.workCenterID === 11) {
                        $scope.totalProduct.packingExport += rItem.totalExportQnt;
                    }
                }
            },
            calTotalRemain: function () {
                for (var i = 0; i < $scope.productionSummaryDetails.length; i++) {
                    var rItem = $scope.productionSummaryDetails[i];
                    if (rItem.workCenterID === 7) {
                        $scope.totalProduct.frameRemain += rItem.totalRemainQnt;
                    }
                    if (rItem.workCenterID === 8) {
                        $scope.totalProduct.paintedRemain += rItem.totalRemainQnt;
                    }
                    if (rItem.workCenterID === 9) {
                        $scope.totalProduct.weavingRemain += rItem.totalRemainQnt;
                    }
                    if (rItem.workCenterID === 11) {
                        $scope.totalProduct.packingRemain += rItem.totalRemainQnt;
                    }
                }
            },

            calOrderQnt: function () {
                for (var i = 0; i < $scope.productionSummaries.length; i++) {
                    var rItem = $scope.productionSummaries[i];
                    if (rItem.orderQnt !== "") {
                        $scope.totalOrderQnt += rItem.orderQnt;
                    }
                }
            },

            calProQnt: function () {
                for (var i = 0; i < $scope.productionSummaries.length; i++) {
                    var rItem = $scope.productionSummaries[i];
                    if (rItem.workOrderQnt !== "") {
                        $scope.totalProQnt += rItem.workOrderQnt;
                    }
                }
            },
            getProductionSummaries: function () {
                var resultPre = [];
                var resultWorkOrder = [];
                var result = [];
                for (var i = 0; i < $scope.productionSummaries.length; i++) {
                    var item = $scope.productionSummaries[i];
                    if (item.productID === null) {
                        resultPre.push(item);
                    }
                    else {
                        resultWorkOrder.push(item);
                    }
                }
                for (var j = 0; j < resultPre.length; j++) {
                    var sItem = resultPre[j];
                    result.push(sItem);
                    for (var x = 0; x < resultWorkOrder.length; x++) {
                        var subItem = resultWorkOrder[x];
                        if (sItem.workOrderID === subItem.preOrderWorkOrderID) {
                            result.push(subItem);
                        }
                    }  
                }
                for (var z = 0; z < resultWorkOrder.length; z++) {
                    var items = resultWorkOrder[z];
                    if (items.preOrderWorkOrderID === null) {
                        result.push(items);
                    } 
                }
                return result;
            }
        };

        function init() {
            productionSummaryRptService.serviceUrl = context.serviceUrl;
            productionSummaryRptService.token = context.token;
            productionSummaryRptService.supportServiceUrl = context.supportServiceUrl;
            productionSummaryRptService.getInit(
                function (data) {
                    $scope.seasons = data.data.seasons;
                    $scope.factories = data.data.factories;
                    $scope.filter.season = jsHelper.getCurrentSeason();
                    jQuery('#content').show();
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.factories = null;
                }
            );
        };

        function search(isLoadMore) {
            productionSummaryRptService.getDataWithFilters(
                {
                    filters: {
                        FactoryID: $scope.filter.factoryID,
                        Season: $scope.filter.season,
                        ClientUD: $scope.filter.clientUD,
                        ProformaInvoice: $scope.filter.pi,
                    }
                },
                function (data) {
                    $scope.detailRow = [];
                    $scope.totalOrderQnt = 0;
                    $scope.totalProQnt = 0;
                    $scope.totalProduct = {
                        frameImport: 0,
                        frameFinish: 0,
                        frameExport: 0,
                        frameRemain: 0,

                        paintedImport: 0,
                        paintedFinish: 0,
                        paintedExport: 0,
                        paintedRemain: 0,

                        weavingImport: 0,
                        weavingFinish: 0,
                        weavingExport: 0,
                        weavingRemain: 0,

                        packingImport: 0,
                        packingFinish: 0,
                        packingExport: 0,
                        packingRemain: 0
                    };
                    $scope.data = data.data;
                    $scope.productionSummaries = data.data.productionSummaries;
                    $scope.productionSummaryDetails = data.data.productionSummaryDetails;
                    $scope.workCenters = data.data.workCenters;
                    $scope.detailRow = $scope.method.getDetailRow($scope.workCenters);
                    $scope.method.calTotalImport();
                    $scope.method.calTotalFinish();
                    $scope.method.calTotalExport();
                    $scope.method.calTotalRemain();
                    $scope.method.calOrderQnt();
                    $scope.method.calProQnt();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }

                    $scope.gridHandler.isTriggered = true;
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        // default event
        $scope.event.init();
    }
})();