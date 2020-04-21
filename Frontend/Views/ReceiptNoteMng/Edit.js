'use strict';

var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ngRoute', 'ngCookies']);

//Angular routing
tilsoftApp.config(function ($routeProvider) {
    $routeProvider.when('/',
        {
            templateUrl: '/ReceiptNoteMng/_General',
            controller: '_GeneralCtrl'
        });

    $routeProvider.when('/purchasing-invoice-item',
        {
            templateUrl: '/ReceiptNoteMng/_PurchasingInvoiceItem',
            controller: '_PurchasingInvoiceCtrl'
        });

    $routeProvider.when('/factory-sale-invoice-item',
        {
            templateUrl: '/ReceiptNoteMng/_FactorySaleInvoiceItem',
            controller: '_FactorySaleInvoiceCtrl'
        });
});


tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService, ) {

    /*
     * initialization Service
     */
    dataService.serviceUrl = context.serviceUrl;
    dataService.supportServiceUrl = context.supportServiceUrl;
    dataService.token = context.token;

    /*
     * property
     */
    $scope.getID = -1;
    $scope.data = null;
    $scope.supportData = null;
    $scope.status = [{ statusID: 1, statusNM: 'Open' }, { statusID: 2, statusNM: 'Confirm' }, { statusID: 3, statusNM: 'Cancel' }];

    /*
     *  Function
     */
    $scope.event = {

        load: function () {
            dataService.load(
                context.id,
                null,
                function (data) {                    
                    $scope.data = data.data.data;
                    $scope.supportData = data.data.supportData;

                    if ($scope.data.bankAcc == null && $scope.supportData.receiptNoteBankAccounts.length > 0) {
                        $scope.data.bankAcc = $scope.supportData.receiptNoteBankAccounts[0].bankAccount;
                    }
                   
                    //if (context.id === 0) {
                    //    dataService.getSupplier(                        
                    //        function (data) {                              
                    //            $scope.data.supplierID = data.supplierID;
                    //            $scope.data.supplierNM = data.supplierNM;
                    //            $scope.data.supplierUD = data.supplierUD;

                    //            $scope.$apply();
                    //        },
                    //        function (error) {

                    //        }
                    //    );
                    //}
                    
                    jQuery('#content').show();

                },
                function (error) {
                    $scope.data = null;
                }
            );
        },

        update: function () {
            if ($scope.editForm.$valid) {
                if ($scope.data.receiveTypeID != 2) {
                    $scope.data.bankAcc = null;
                };
                dataService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (context.id === 0) {
                            //refresh page
                            var id = data.data.receiptNoteID;
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
        },

        delete: function (id) {
            dataService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.indexUrl;
                    }
                },
                function (error) {

                }
            );
        },
        getPrintout: function () {
            dataService.getPrintout(
                context.id,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type !== 2) {
                        window.open(context.reportUrl + data.data, '');
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    };


    /*
     * Method
     */
    $scope.method = {
        getnewID: function () {
            $scope.getID--;
            return $scope.getID;
        }
    };

    /*
     * 
     */
    $scope.$on('dataService.pushDataInvoice', function (event, data) {
        angular.forEach(data, function (item) {
            var checkdata = $scope.data.receiptNoteInvoiceResults.filter(x => x.purchasingInvoiceID === item.purchasingInvoiceID);
            if (checkdata.length === 0) {
                $scope.data.receiptNoteInvoiceResults.push({
                    receiptNoteInvoiceID: $scope.method.getnewID(),
                    invoiceNo: item.invoiceNo,
                    invoiceDate: item.invoiceDate,
                    invoiceAmount: item.totalAmount,
                    totalReceipt: item.totalReceipt,
                    remain: item.remainQnt,
                    purchasingInvoiceID: item.purchasingInvoiceID
                });
            }
        });
    });

    $scope.$on('dataService.pushDataSaleInvoice', function (event, data) {
        angular.forEach(data, function (item) {
            var checkdata = $scope.data.receiptNoteSaleInvoiceResults.filter(x => x.factorySaleInvoiceID === item.factorySaleInvoiceID);
            if (checkdata.length === 0) {
                $scope.data.receiptNoteSaleInvoiceResults.push({
                    receiptNoteSaleInvoiceID: $scope.method.getnewID(),
                    invoiceNo: item.invoiceNo,
                    invoiceDate: item.invoiceDate,
                    saleInvoiceAmount: item.totalAmount,
                    totalReceipt: item.totalReceipt,
                    remain: item.remainQnt,
                    factorySaleInvoiceID: item.factorySaleInvoiceID
                });
            }
        });
    });

    /*
     *initialization controller
     */
    console.log('edit');
    $timeout(function () { $scope.event.load(); }, 0);

}]);
