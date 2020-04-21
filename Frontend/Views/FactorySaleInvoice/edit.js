var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'furnindo-directive', 'ngRoute', 'ngCookies']);

tilsoftApp.config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/',
        {
            templateUrl: '/FactorySaleInvoice/_Genneral',
            controller: '_GeneralController'
        });
    $routeProvider.when('/general',
        {
            templateUrl: '/FactorySaleInvoice/_Genneral',
            controller: '_GeneralController'
        });
    $routeProvider.when('/factorysale-order',
        {
            templateUrl: '/FactorySaleInvoice/_SaleOrderView',
            controller: '_SaleOrderController'
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
        checkHasPO: false,
        seasons: []
    };

    $scope.supportData.spCurrency = [
        { sname: 'VND', name: 'VND' },
        { sname: 'USD', name: 'USD' }
    ];

    $scope.supportData.spcheckPercentVAT = [
        { id: 0, name: '0%' },
        { id: 5, name: '5%' },
        { id: 10, name: '10%' }
    ];

    //
    //function
    //
    $scope.load = function () {
        dataService.load(
            context.id,
            null,
            function (data) {
                console.log(data);
                $scope.data = data.data.data;
                $scope.supportData.purchaseInvoiceStatusDTOs = data.data.factorySaleInvoiceStatus;
                $scope.supportData.factoryRawMaterialDTOs = data.data.factoryRawMaterialDTOs;
                $scope.supportData.paymentTerms = data.data.paymentTermDTOs;
                $scope.supportData.seasons = data.data.seasons;
                if (context.id == 0) {
                    $scope.data.season = jsHelper.getCurrentSeason();
                }
            },
            function (error) {
                jsHelper.showMessage('warning', error);
                $scope.data = null;
            }
        );
    };

    $scope.update = function () {
        for (let i = 0; i < $scope.data.factorySaleInvoiceDetailDTOs.length; i++) {
            let item = $scope.data.factorySaleInvoiceDetailDTOs[i];
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
            jsHelper.showMessage('warning', 'Please select a Client.');
            return false;
        }

        if ($scope.data.supplierPaymentTerm === null || $scope.data.supplierPaymentTerm === '' || $scope.data.supplierPaymentTerm === undefined) {
            jsHelper.showMessage('warning', 'Please select a Payment Term.');
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
                        var id = data.data.factorySaleInvoiceID;
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
    //
    //init controller
    //
    console.log('edit');
    $scope.load();

}]);
