//(function () {
//    'use strict';

//    angular.module('tilsoftApp').controller('MISaleByClientController', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
//        dataService.serviceUrl = context.serviceUrl;
//        dataService.token = context.token;

//        //
//        //property
//        //
//        $scope.ExpectedByClient = [];
//        $scope.PIByClient = [];
//        $scope.PIConfirmedByClient = [];
//        $scope.CIByClient = [];
//        $scope.NewClientCurrentSeason = [];
//        $scope.LostClientCurrentSeason = [];
//        $scope.NewClientPreSeason = [];
//        $scope.LostClientPreSeason = [];
//        $scope.NewClientOnProformaInvoice = [];
//        $scope.LostClientOnProformaInvoice = [];

//        //
//        // events
//        //
//        $scope.event = {
//            init: function () {                
//                dataService.getSaleByClient(
//                    context.season,
//                    function (data) {
//                        if (data.message.type == 0) {
//                            //mark as loaded data for mi sale by client
//                            $scope.isLoadedMiSaleByClient = true;

//                            // ExpectedByClient
//                            $scope.method.setPanelVisible('pnlExpectedByClient');
//                            $scope.ExpectedByClient = data.data.expectations;
//                            angular.forEach($scope.ExpectedByClient, function (item) {
//                                if (item.ciAmountInEURLastYear !== 0) {
//                                    item.deltaPercentWithLastYear = (item.expectedAmountInEURThisYear - item.ciAmountInEURLastYear) * 100 / Math.abs(item.ciAmountInEURLastYear);
//                                }
//                            });

//                            // PIByClient
//                            $scope.method.setPanelVisible('pnlPIByClient');
//                            $scope.PIByClient = data.data.proformaInvoices;
//                            angular.forEach($scope.PIByClient, function (item) {
//                                //this year base on PI
//                                if (item.ciAmountInEURLastYear !== 0) {
//                                    item.deltaPercentWithLastYear = (item.piAmountInEURThisYear - item.ciAmountInEURLastYear) * 100 / Math.abs(item.ciAmountInEURLastYear);
//                                }
//                            });

//                            // PIConfirmedByClient
//                            $scope.method.setPanelVisible('pnlPIConfirmedByClient');
//                            $scope.PIConfirmedByClient = data.data.proformaInvoiceConfirmeds;
//                            angular.forEach($scope.PIConfirmedByClient, function (item) {
//                                if (item.ciAmountInEURLastYear !== 0) {
//                                    item.deltaPercentWithLastYear = (item.piConfirmedAmountInEURThisYear - item.ciAmountInEURLastYear) * 100 / Math.abs(item.ciAmountInEURLastYear);
//                                }
//                            });

//                            // CIByClient
//                            $scope.method.setPanelVisible('pnlCIByClient');
//                            $scope.CIByClient = data.data.eurofarCommercialInvoices;
                            
//                            // NewClientSeason base on proforma invoice
//                            $scope.method.setPanelVisible('pnlNewClientOnProformaInvoice');
//                            $scope.NewClientOnProformaInvoice = data.data.proformaInvoiceOfNewClients;

//                            // LostClientSeason base on proforma invoice
//                            $scope.method.setPanelVisible('pnlLostClientOnProformaInvoice');
//                            $scope.LostClientOnProformaInvoice = data.data.proformaInvoiceOfLostedClients;

//                            // NewClientPreSeason base on commercial invoice
//                            $scope.method.setPanelVisible('pnlNewClientPreSeason');
//                            $scope.NewClientPreSeason = data.data.eurofarCommercialInvoiceOfNewClientLastSeason;

//                            // LostClientPreSeason base on commercial invoice
//                            $scope.method.setPanelVisible('pnlLostClientPreSeason');
//                            $scope.LostClientPreSeason = data.data.eurofarCommercialInvoiceOfLostedClientLastSeason;
//                        }
//                        else {
//                            jsHelper.showMessage('warning', data.message.message);
//                            console.log(data.message.message);
//                        }
//                    },
//                    function (error) {
//                        $scope.ExpectedByClient = null;
//                        $scope.PIByClient = null;
//                        $scope.PIConfirmedByClient = null;
//                        $scope.CIByClient = null;
//                        $scope.NewClientCurrentSeason = null;
//                        $scope.LostClientCurrentSeason = null;
//                        $scope.NewClientPreSeason = null;
//                        $scope.LostClientPreSeason = null;
//                    }
//                );
//            }
//        };

//        //
//        // angular init
//        //
//        $timeout(function () {            
//            $scope.event.init();         
//        }, 0);
//    }]);

//})();