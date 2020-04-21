angular.module('tilsoftApp').controller('salesByCountryController', ['$scope', '$rootScope', '$timeout', 'dataService', '$filter', function ($scope, $rootScope, $timeout, dataService, $filter) {
    //
    // property
    //
    $scope.salesByCountry = {};
    $scope.salesByCountry.data = {};
    $scope.salesByCountry.supportData = {
        sortType_Expected: 'totalEURAmount',
        sortReverse_Expected: true,
        sortType_ProformaInvoice: 'totalEURAmount',
        sortReverse_ProformaInvoice: true,
        sortType_CommercialInvoice: 'totalEURAmount_ThisYear',
        sortReverse_CommercialInvoice: true,
        sortValue: {
            SaleByCountry: '-totalEURAmount_LastYear'
        }
    };

    //
    // listen to parent scope
    //
    $rootScope.$on('salesByCountry', function () {
        $scope.salesByCountry.method.loadData();
    });

    //
    // method
    //
    $scope.salesByCountry.method = {
        loadData: function () {
            $scope.salesByCountry.data = {};
            dataService.getSalesByCountry(
                $scope.data.selectedSaleID,
                'pnlSalesByCountry',
                function (data) {
                    if (data.message.type === 0) {
                        $scope.salesByCountry.data = data.data;

                        //percent expectation
                        var totalExpectation = 0;
                        angular.forEach($scope.salesByCountry.data.expectedSummaries, function (value, key) {
                            totalExpectation += value.totalEURAmount;
                        }, null);

                        angular.forEach($scope.salesByCountry.data.expectedSummaries, function (value, key) {
                            value.percent = jsHelper.round(value.totalEURAmount * 100 / totalExpectation, 2);
                        }, null);

                        //percent proforma invoice
                        var totalProformaInvoice = 0;
                        angular.forEach($scope.salesByCountry.data.summaries, function (value, key) {
                            totalProformaInvoice += value.totalEURAmount;
                        }, null);

                        angular.forEach($scope.salesByCountry.data.summaries, function (value, key) {
                            value.percent = jsHelper.round(value.totalEURAmount * 100 / totalProformaInvoice, 2);
                        }, null);

                        //percent of usd and eur compare with total (sales expectation)
                        angular.forEach($scope.salesByCountry.data.expectedSummaries, function (value, key) {
                            value.usdPercent = value.usdAmount / value.totalEURAmount * 100;
                            value.eurPercent = 100 - value.usdPercent;
                        }, null);

                        //percent of usd and eur compare with total (sales proforma invoice)
                        angular.forEach($scope.salesByCountry.data.summaries, function (value, key) {
                            value.usdPercent = value.usdAmount / value.totalEURAmount * 100;
                            value.eurPercent = 100 - value.usdPercent;
                        }, null);

                        //percent of usd and eur compare with total (commercial invoice)
                        angular.forEach($scope.salesByCountry.data.commercialInvoiceSummaries, function (value, key) {
                            value.usdPercent = value.usdAmount / value.totalEURAmount * 100;
                            value.eurPercent = 100 - value.usdPercent;
                        }, null);

                        //percent commerciall invoice
                        var totalCommercialInvoiceThisYear = 0;
                        var totalCommercialInvoiceLastYear = 0;
                        angular.forEach($scope.salesByCountry.data.commercialInvoiceSummaries, function (value, key) {
                            totalCommercialInvoiceThisYear += value.totalEURAmount_ThisYear;
                            totalCommercialInvoiceLastYear += value.totalEURAmount_LastYear;
                        }, null);

                        angular.forEach($scope.salesByCountry.data.commercialInvoiceSummaries, function (value, key) {
                            value.percent_ThisYear = jsHelper.round(value.totalEURAmount_ThisYear * 100 / totalCommercialInvoiceThisYear, 2);
                            value.percent_LastYear = jsHelper.round(value.totalEURAmount_LastYear * 100 / totalCommercialInvoiceLastYear, 2);
                        }, null);

                        angular.forEach($scope.salesByCountry.data.commercialInvoiceSummaries, function (value, key) {
                            //value.totalEURMargin_LastYear = value.totalEURAmount_LastYear - value.totalCostEURAmount_LastYear;
                            value.percentEURMargin_LastYear = value.totalMarginAmountEUR_LastYear * 100 / value.totalEURAmount_LastYear;
                        }, null);


                        var seriData = [];
                        var uniqueCountry = [];
                        angular.forEach($scope.salesByCountry.data.summaries, function (item) {
                            if (uniqueCountry.filter(o => o.clientCodeForLog === item.clientCodeForLog.toLowerCase()).length === 0) {
                                uniqueCountry.push(item.clientCodeForLog.toLowerCase());
                                seriData.push([item.clientCodeForLog.toLowerCase(), 1]);
                            }
                        });
                        angular.forEach($scope.salesByCountry.data.expectedSummaries, function (item) {
                            if (uniqueCountry.filter(o => o.clientCodeForLog === item.clientCodeForLog.toLowerCase()).length === 0) {
                                uniqueCountry.push(item.clientCodeForLog.toLowerCase());
                                seriData.push([item.clientCodeForLog.toLowerCase(), 1]);
                            }
                        });

                        $timeout(function () {
                            $('#map-salesByCountry').highcharts('Map', {
                                chart: {
                                    map: 'custom/world-highres3',
                                    borderWidth: 1
                                },

                                title: {
                                    text: null
                                },

                                mapNavigation: {
                                    enabled: true,
                                    buttonOptions: {
                                        verticalAlign: 'bottom'
                                    }
                                },

                                legend: {
                                    enabled: false
                                },

                                tooltip: {
                                    enabled: false
                                    //formatter: function () {
                                    //    return this.point.name + ': ' + $filter('currency')(this.point.value, '', 1) + ' container(s)';
                                    //},
                                    //useHTML: true
                                },

                                series: [{
                                    name: 'Country',
                                    data: seriData,
                                    dataLabels: {
                                        enabled: true,
                                        color: '#FFFFFF',
                                        formatter: function () {
                                            if (this.point.value) {
                                                return this.point.name;
                                            }
                                        }
                                    }
                                }]
                            });
                        }, 100);
                    }
                },
                function (error) {
                    console.log(error);
                    $scope.salesByCountry.data = [];
                }
            );
        },

        // summary sale
        getSummarySaleTotalContainer: function (listData) {
            var total = 0;
            if (listData !== null) {
                angular.forEach(listData, function (value, key) {
                    total += value.totalCont;
                }, null);
            }
            return total;
        },
        getSummarySaleTotalAmount: function (listData) {
            var total = 0;
            if (listData !== null) {
                angular.forEach(listData, function (value, key) {
                    total += value.totalEURAmount;
                }, null);
            }
            return total;
        },
        getSummarySaleTotalUSDAmount: function (listData) {
            var total = 0;
            if (listData !== null) {
                angular.forEach(listData, function (value, key) {
                    total += value.usdAmount;
                }, null);
            }
            return total;
        },
        getSummarySaleTotalEurAmount: function (listData) {
            var total = 0;
            if (listData !== null) {
                angular.forEach(listData, function (value, key) {
                    total += value.eurAmount;
                }, null);
            }
            return total;
        },

        getSummarySaleTotalClient: function (listData) {
            var total = 0;
            if (listData !== null) {
                angular.forEach(listData, function (value, key) {
                    total += value.totalClient;
                }, null);
            }
            return total;
        },

        sortData: function (key, field) {
            var currentSetting = $scope.salesByCountry.supportData.sortValue[key];
            if (currentSetting.replace('-', '') === field) {
                if (currentSetting.indexOf('-') < 0) {
                    $scope.salesByCountry.supportData.sortValue[key] = '-' + field;
                }
                else {
                    $scope.salesByCountry.supportData.sortValue[key] = field;
                }
            }
            else {
                $scope.salesByCountry.supportData.sortValue[key] = field;
            }
        }
    };
}]);