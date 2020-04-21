var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'furnindo-directive', 'ngRoute', 'ngCookies']);

tilsoftApp.config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/',
        {
            templateUrl: '/PurchaseInvoiceMng/_Genneral',
            controller: '_GeneralController'
        });
    $routeProvider.when('/general',
        {
            templateUrl: '/PurchaseInvoiceMng/_Genneral',
            controller: '_GeneralController'
        });
    $routeProvider.when('/purchase-order',
        {
            templateUrl: '/PurchaseInvoiceMng/_PurchaseOrderView',
            controller: '_PurchaseOrderController'
        });
});

tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
    //
    // init service
    //
    dataService.serviceUrl = context.serviceUrl;
    dataService.supportServiceUrl = context.supportServiceUrl;
    dataService.token = context.token;

    //
    //property
    //
    $scope.data = null;

    //
    //support data
    //

    $scope.supportData = {
        purchaseInvoiceTypeDTOs: [],
        purchaseInvoiceStatusDTOs: [],
        factoryRawMaterialDTOs: [],
        factoryWarehouseDTOs: [],
        spCurrency: [],
        spcheckPercentVAT: [],
        paymentTerms: [],
        checkHasPO: false
    };

    //
    //function
    //
    $scope.load = function () {
        var param = {
            typeID: 0
        };
        dataService.load(
            context.id,
            param,
            function (data) {
                console.log('load');
                $scope.data = data.data.data;
                $scope.supportData.purchaseInvoiceTypeDTOs = data.data.purchaseInvoiceTypeDTOs;
                $scope.supportData.purchaseInvoiceStatusDTOs = data.data.purchaseInvoiceStatusDTOs;
                $scope.supportData.factoryRawMaterialDTOs = data.data.factoryRawMaterialDTOs;
                $scope.supportData.factoryWarehouseDTOs = data.data.factoryWarehouseDTOs;
                $scope.supportData.paymentTerms = data.data.paymentTermDTOs;
            },
            function (error) {
                $scope.data = null;
            }
        );
    };

    $scope.update = function () {
        for (let i = 0; i < $scope.data.purchaseInvoiceDetailDTOs.length; i++) {
            let item = $scope.data.purchaseInvoiceDetailDTOs[i];
            if (item.productionItemID === null || item.productionItemID === '' || item.productionItemID === undefined) {
                jsHelper.showMessage('warning', 'Please input Items.');
                return false;
            }
            if ($scope.data.purchaseInvoiceTypeID === 2) {
                if (item.factoryWarehouseID === null || item.factoryWarehouseID === '' || item.factoryWarehouseID === undefined) {
                    jsHelper.showMessage('warning', 'Please input all Warehouse');
                    return false;
                }
            }
            if (item.unitPrice === null || item.unitPrice === '' || item.unitPrice === undefined) {
                jsHelper.showMessage('warning', 'Please input all Price.');
                return false;
            }

        }

        if ($scope.data.currency === null || $scope.data.currency === '' || $scope.data.currency === undefined) {
            jsHelper.showMessage('warning', 'Please input Currency.');
            return false;
        }

        if ($scope.data.factoryRawMaterialID === null || $scope.data.factoryRawMaterialID === '' || $scope.data.factoryRawMaterialID === undefined) {
            jsHelper.showMessage('warning', 'Please select a Supplier.');
            return false;
        }

        if ($scope.data.supplierPaymentTerm === null || $scope.data.supplierPaymentTerm === '' || $scope.data.supplierPaymentTerm === undefined) {
            jsHelper.showMessage('warning', 'Please select a Payment Term.');
            return false;
        }

        if ($scope.data.invoiceNo === null || $scope.data.invoiceNo === '' || $scope.data.invoiceNo === undefined) {
            jsHelper.showMessage('warning', 'Please input InvoiceNo.');
            return false;
        }

        if ($scope.editForm.$valid) {
            dataService.update(
                context.id,
                $scope.data,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (context.id === 0) {
                        //refresh page
                        var id = data.data.purchaseInvoiceID;
                        window.location = context.refreshUrl + id + '#/';
                    }
                    else {
                        //refresh data
                        $scope.data = data.data;
                    }
                },
                function (error) {
                    //do need do nothing
                }
            );
        }
        else {
            jsHelper.showMessage('warning', context.errMsg);
        }
    };

    $scope.del = function (id) {
        dataService.delete(
            id,
            null,
            function (data) {
                jsHelper.processMessage(data.message);
                if (data.message.type === 0) {
                    window.location = context.backUrl;
                }
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            });
    };

    $scope.approve = function () {
        if (confirm('Are you sure ?')) {
            dataService.approve(
                context.id,
                $scope.data,
                function (data) {
                    window.location = context.refreshUrl + data.data.purchaseInvoiceID;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        }
    };

    $scope.cancel = function () {
        if (confirm('Are you sure ?')) {
            dataService.cancel(
                $scope.data.purchaseInvoiceID,
                $scope.data.purchaseInvoiceStatusID,
                function (data) {
                    window.location = context.refreshUrl + data.data;
                },
                function (error) {
                    jsHelper.processMessage('warning', error);
                });
        }
    };
    //
    //init controller
    // 
    console.log('edit');
    $scope.load();

}]);
