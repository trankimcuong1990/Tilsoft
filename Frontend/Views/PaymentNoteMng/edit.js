'use strict';

var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ngRoute', 'ngCookies']);

//Angular routing
tilsoftApp.config(function ($routeProvider) {
    $routeProvider.when('/',
        {
            templateUrl: '/PaymentNoteMng/_General',
            controller: '_GeneralCtrl'
        });

    $routeProvider.when('/purchase-invoice-item',
        {
            templateUrl: '/PaymentNoteMng/_PurchasingInvoiceItem',
            controller: '_PurchasingInvoiceItemCtrl'
        });

    $routeProvider.when('/purchase-order-item',
        {
            templateUrl: '/PaymentNoteMng/_PurchaseOrderItem',
            controller: '_PurchaseOrderItemCtrl'
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
    
    $scope.DepositAfterPayment = 0;
    $scope.TotalDepositThisNote = 0;
    $scope.PaymentByDeposit = 0;
    $scope.TotalDeposit = 0;
    $scope.DepositRemain = 0;
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
                    console.log(data);
                    $scope.data = data.data.data;
                    $scope.supportData = data.data.supportData;

                    if ($scope.data.currency !== null && $scope.data.currency !== undefined && $scope.data.factoryRawMaterialID !== null && $scope.data.factoryRawMaterialID !== undefined) {
                        //$scope.event.getDepositRemain($scope.data);
                        //$scope.event.getDepositThisNote();
                        //$scope.event.getDepositAfterPayment();
                    }

                    if ($scope.data.bankAcc === null && $scope.supportData.paymentNoteBankAccounts.length > 0) {
                        $scope.data.bankAcc = $scope.supportData.paymentNoteBankAccounts[0].bankAccount;
                    }

                    //F**king Function
                    //if ($scope.data.paymentNoteSupplierResults.length > 0) {
                    //    angular.forEach($scope.data.paymentNoteSupplierResults, function (item) {
                    //        $scope.event.getPaymentByDeposit($scope.data, item);
                    //        $scope.event.getTotalDeposit($scope.data, item);
                    //    });
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
                if ($scope.data.paymentTypeID !== 2) {
                    $scope.data.bankAcc = null;
                }

                if ($scope.data.currency !== null && $scope.data.currency !== undefined && $scope.data.factoryRawMaterialID !== null && $scope.data.factoryRawMaterialID !== undefined) {
                    //$scope.event.getDepositRemain($scope.data);
                    //$scope.event.getDepositThisNote();
                    //$scope.event.getDepositAfterPayment();
                }
                //if ($scope.data.paymentNoteSupplierResults.length > 0) {
                //    angular.forEach($scope.data.paymentNoteSupplierResults, function (item) {
                //        $scope.event.getPaymentByDeposit($scope.data, item);
                //        $scope.event.getTotalDeposit($scope.data, item);
                //    });
                //}

                if ($scope.DepositAfterPayment >= 0) {
                    if ($scope.data.statusID === 1) {
                        $scope.data.depositRemain = $scope.DepositRemain;
                    }
                    dataService.update(
                        context.id,
                        $scope.data,
                        function (data) {
                            jsHelper.processMessage(data.message);
                            if (context.id === 0) {
                                //refresh page
                                var id = data.data.paymentNoteID;
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
                    jsHelper.showMessage('warning', 'Deposit after payment can not lower than 0, please check Deposit');
                }
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

        //getPaymentByDeposit: function (data, item) {
        //    var year = data.postingDate.substring(6);
        //    dataService.getPaymentByDeposit(
        //        item.factoryRawMaterialID,
        //        year,
        //        data.currency,
        //        function (data) {
        //            item.paymentByDeposit = data.data;
        //        },
        //        function (error) {
        //            jsHelper.showMessage('warning', error);
        //        }
        //    );
        //},

        //getTotalDeposit: function (data, item) {
        //    var year = data.postingDate.substring(6);
        //    dataService.getTotalDeposit(
        //        item.factoryRawMaterialID,
        //        year,
        //        data.currency,
        //        function (data) {
        //            item.totalDeposit = data.data;
        //        },
        //        function (error) {
        //            jsHelper.showMessage('warning', error);
        //        }
        //    );
        //},

        //getDepositThisNote: function () {
        //    var total = 0;
        //    if ($scope.data.paymentNoteInvoiceResults !== null && $scope.data.paymentNoteInvoiceResults.length > 0) {
        //        angular.forEach($scope.data.paymentNoteInvoiceResults, function (item) {
        //            if (item.deposit === null) {
        //                item.deposit = 0;
        //            }
        //            if (item.deposit !== undefined && item.deposit !== '') {
        //                if ($scope.data.currency === 'VND')
        //                    total = total + parseFloat(item.deposit);
        //                else {
        //                    if ($scope.data.currency === 'USD') {
        //                        total = total + parseFloat(item.deposit) * scope.data.exchangeRate;
        //                    }
        //                }
        //            }
        //        });
        //        $scope.TotalDepositThisNote = total;
        //        $scope.event.getDepositAfterPayment();
        //    }
        //},

        //getDepositAfterPayment: function () {
        //    var total = 0;
        //    if (!isNaN($scope.DepositRemain) && !isNaN($scope.TotalDepositThisNote)) {
        //        if ($scope.data.statusID === 1)
        //            total = $scope.DepositRemain - $scope.TotalDepositThisNote;
        //        else
        //            total = $scope.data.depositRemain - $scope.TotalDepositThisNote;
        //    }
        //    $scope.DepositAfterPayment = total;
        //},

        //getDepositRemain: function (item) {
        //    var year = item.postingDate.substring(6);
        //    dataService.getPaymentByDeposit(
        //        item.factoryRawMaterialID,
        //        year,
        //        item.currency,
        //        function (data) {
        //            $scope.PaymentByDeposit = data.data;
        //            dataService.getTotalDeposit(
        //                item.factoryRawMaterialID,
        //                year,
        //                item.currency,
        //                function (data) {
        //                    $scope.TotalDeposit = data.data;
        //                    $scope.DepositRemain = $scope.TotalDeposit - $scope.PaymentByDeposit;
        //                    $scope.event.getDepositThisNote();
        //                },
        //                function (error) {
        //                    jsHelper.showMessage('warning', error);
        //                }
        //            );
        //        },
        //        function (error) {
        //            jsHelper.showMessage('warning', error);
        //        }
        //    );
        //}
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
            var checkdata = $scope.data.paymentNoteInvoiceResults.filter(x => x.purchaseInvoiceID === item.purchaseInvoiceID);
            if (checkdata.length === 0) {
                $scope.data.paymentNoteInvoiceResults.push({
                    paymentNoteInvoiceID: $scope.method.getnewID(),
                    purchaseInvoiceUD: item.purchaseInvoiceUD,
                    invoiceNo: item.invoiceNo,
                    purchaseInvoiceDate: item.purchaseInvoiceDate,
                    totalPurchaseInvoice: item.totalPurchaseInvoice,
                    totalPayment: item.totalPayment,
                    remain: item.remain,
                    purchaseInvoiceID: item.purchaseInvoiceID,
                    currency: item.currency,
                    paymentNotePODepositResults: [],
                    totalRealDeposit: item.totalRealDeposit,
                    isShow: false
                });
            }
        });
    });

    $scope.$on('dataService.pushDataOrder', function (event, data) {
        angular.forEach(data, function (item) {
            var checkdata = $scope.data.paymentNoteSupplierResults.filter(x => x.purchaseOrderID === item.purchaseOrderID);
            if (checkdata.length === 0) {
                $scope.data.paymentNoteSupplierResults.push({
                    paymentNoteSupplierID: $scope.method.getnewID(),
                    purchaseOrderID: item.purchaseOrderID,
                    purchaseOrderUD: item.purchaseOrderUD,
                    purchaseOrderDate: item.purchaseOrderDate,
                    depositAmount: null,
                    totalDepositAmount: item.totalDepositAmount,
                    totalPaymentDepositAmount: item.totalPayDepositAmount,
                    remainDepositAmount: item.remainDepositAmount,
                    totalPurchaseOrderAmount: item.totalPurchaseOrderAmount,
                    supplierPaymentTermNM: item.supplierPaymentTermNM
                });
            }
        });
    });

    /*
     *initialization controller
     */
    $timeout(function () { $scope.event.load(); }, 0);

}]);
