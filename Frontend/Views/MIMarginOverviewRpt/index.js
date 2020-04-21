(function () {
    'use strict';
    
    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);

    angular.module('tilsoftApp').filter('sumFunc', function () {
        return function (data, field) {
            if (angular.isUndefined(data) || angular.isUndefined(field))
                return 0;
            var sum = parseFloat('0');
            angular.forEach(data, function (v, k) {
                sum = sum + parseFloat(v[field] == null ? 0 : v[field]);
            });
            return sum;
        };
    });
    angular.module('tilsoftApp').filter('sumFunc2', function () {
        return function (data, field1, field2) {
            if (angular.isUndefined(data) || angular.isUndefined(field1) || angular.isUndefined(field2))
                return 0;
            var sum = parseFloat('0');
            angular.forEach(data, function (v, k) {
                sum = sum + parseFloat(v[field1] == null ? 0 : v[field1]) + parseFloat(v[field2] == null ? 0 : v[field2]);
            });
            return sum;
        };
    });

    //CUSTUM FILTER
    angular.module('tilsoftApp').filter('sumFuncX', function () {
        return function (data, key) {
            if (angular.isUndefined(data) || angular.isUndefined(key)) return 0;
            var sum = parseFloat(0);
            angular.forEach(data, function (dvalue, dkey) {
                sum = sum + parseFloat(dvalue[key] === null ? 0 : dvalue[key]);
            });
            return sum;
        };
    });

    angular.module('tilsoftApp').filter('abs', function () {
        return function (input) {
            return Math.abs(input);
        };
    });
    angular.module('tilsoftApp').controller('tilsoftController', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;
        $('#content').show();

        //
        // property
        //
        $scope.settingValue = {
            exRate: 0.00,
            usdContainerValue: 0.00,
            eurContainerValue: 0.00
        };
        $scope.sortValue = {
            SaleByCountry: '-totalEURAmount_LastYear',
            ExpectedByClient: '-totalEURAmount',
            PIByClient: '-totalEURAmount',
            PIConfirmedByClient: '-totalEURAmount',
            CIByClient: '-totalEURAmount',
            NewClientCurrentSeason: '-totalEURAmount',
            LostClientCurrentSeason: '-totalEURAmount',
            NewClientPreSeason: '-totalEURAmount',
            LostClientPreSeason: '-ciAmountInEUR',

            NewClientOnProformaInvoice: '-totalEURAmount',
            LostClientOnProformaInvoice: '-totalEURAmount'
        };
        $scope.filter = {
            ExpectedByClient: {},
            PIByClient: {},
            PIConfirmedByClient: {},
            CIByClient: {},
            NewClientCurrentSeason: {},
            LostClientCurrentSeason: {},
            NewClientPreSeason: {},
            LostClientPreSeason: {},
            NewClientOnProformaInvoice: {},
            LostClientOnProformaInvoice: {}
        };

        $scope.ExpectedByClient = [];
        $scope.PIByClient = [];
        $scope.PIConfirmedByClient = [];
        $scope.CIByClient = [];
        $scope.NewClientCurrentSeason = [];
        $scope.LostClientCurrentSeason = [];
        $scope.NewClientPreSeason = [];
        $scope.LostClientPreSeason = [];
        $scope.NewClientOnProformaInvoice = [];
        $scope.LostClientOnProformaInvoice = [];

        //
        // events
        //
        $scope.event = {
            toggPanel: function ($event, panelID) {
                $("#" + panelID).toggle();
                var element = $($event.target);
                if (element.hasClass('fa-minus-square-o')){
                    element.removeClass('fa-minus-square-o');
                    element.addClass('fa-plus-square-o');
                }
                else if (element.hasClass('fa-plus-square-o')) {
                    element.removeClass('fa-plus-square-o');
                    element.addClass('fa-minus-square-o');
                }
            },

            init: function () {
                $('#content').show(); 

                dataService.getSaleByClient(
                    context.season,
                    function (data) {
                        if (data.message.type == 0) {
                            //mark as loaded data for mi sale by client
                            $scope.isLoadedMiSaleByClient = true;

                            // ExpectedByClient
                            $scope.method.setPanelVisible('pnlExpectedByClient');
                            $scope.ExpectedByClient = data.data.expectations;
                            angular.forEach($scope.ExpectedByClient, function (item) {
                                if (item.ciAmountInEURLastYear !== 0) {
                                    item.deltaPercentWithLastYear = (item.expectedAmountInEURThisYear - item.ciAmountInEURLastYear) * 100 / Math.abs(item.ciAmountInEURLastYear);
                                }
                            });

                            // PIByClient
                            $scope.method.setPanelVisible('pnlPIByClient');
                            $scope.PIByClient = data.data.proformaInvoices;
                            angular.forEach($scope.PIByClient, function (item) {
                                //this year base on PI
                                if (item.ciAmountInEURLastYear !== 0) {
                                    item.deltaPercentWithLastYear = (item.piAmountInEURThisYear - item.ciAmountInEURLastYear) * 100 / Math.abs(item.ciAmountInEURLastYear);
                                }
                            });

                            // PIConfirmedByClient
                            $scope.method.setPanelVisible('pnlPIConfirmedByClient');
                            $scope.PIConfirmedByClient = data.data.proformaInvoiceConfirmeds;
                            angular.forEach($scope.PIConfirmedByClient, function (item) {
                                if (item.ciAmountInEURLastYear !== 0) {
                                    item.deltaPercentWithLastYear = (item.piConfirmedAmountInEURThisYear - item.ciAmountInEURLastYear) * 100 / Math.abs(item.ciAmountInEURLastYear);
                                }
                            });

                            // CIByClient
                            $scope.method.setPanelVisible('pnlCIByClient');
                            $scope.CIByClient = data.data.eurofarCommercialInvoices;

                            // NewClientSeason base on proforma invoice
                            $scope.method.setPanelVisible('pnlNewClientOnProformaInvoice');
                            $scope.NewClientOnProformaInvoice = data.data.proformaInvoiceOfNewClients;

                            // LostClientSeason base on proforma invoice
                            $scope.method.setPanelVisible('pnlLostClientOnProformaInvoice');
                            $scope.LostClientOnProformaInvoice = data.data.proformaInvoiceOfLostedClients;

                            // NewClientPreSeason base on commercial invoice
                            $scope.method.setPanelVisible('pnlNewClientPreSeason');
                            $scope.NewClientPreSeason = data.data.eurofarCommercialInvoiceOfNewClientLastSeason;

                            // LostClientPreSeason base on commercial invoice
                            $scope.method.setPanelVisible('pnlLostClientPreSeason');
                            $scope.LostClientPreSeason = data.data.eurofarCommercialInvoiceOfLostedClientLastSeason;
                        }
                        else {
                            jsHelper.showMessage('warning', data.message.message);
                            console.log(data.message.message);
                        }
                    },
                    function (error) {
                        $scope.ExpectedByClient = null;
                        $scope.PIByClient = null;
                        $scope.PIConfirmedByClient = null;
                        $scope.CIByClient = null;
                        $scope.NewClientCurrentSeason = null;
                        $scope.LostClientCurrentSeason = null;
                        $scope.NewClientPreSeason = null;
                        $scope.LostClientPreSeason = null;
                    }
                );
            }
        };

        //
        // method
        //
        $scope.method = {
            setPanelVisible: function (id) {
                $('#' + id).find('.rpt-content').show();
                $('#' + id).find('.rpt-loading').hide();
            },

            //sort function
            sortData: function (key, field) {
                var currentSetting = $scope.sortValue[key];
                if (currentSetting.replace('-', '') === field) {
                    if (currentSetting.indexOf('-') < 0) {
                        $scope.sortValue[key] = '-' + field;
                    }
                    else {
                        $scope.sortValue[key] = field;
                    }
                }
                else {
                    $scope.sortValue[key] = field;
                }
            }
        };
        //
        // angular init
        //
        //$timeout(function () {
        //    $scope.event.init();
        //}, 0);

    }]);

})();