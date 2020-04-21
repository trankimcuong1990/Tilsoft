var tilsoftApp = angular.module('tilsoftApp', []);

tilsoftApp.filter('sumFunction', function () {
    return function (data, key) {
        if (angular.isUndefined(data) || angular.isUndefined(key))
            return 0;
        var sum = 0;
        angular.forEach(data, function (v, k) {
            sum = sum + parseFloat(v[key] == null ? 0 : v[key]);
        });
        return sum;
    }
});


tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;
    $scope.sales = [];
    $scope.confirmedSales = [];

    //sort able expected
    $scope.sortType_Expected = 'totalEURAmount'; // set the default sort type
    $scope.sortReverse_Expected = true;  // set the default sort order

    $scope.sortType_ProformaInvoice = 'totalEURAmount'; // set the default sort type
    $scope.sortReverse_ProformaInvoice = true;  // set the default sort order

    $scope.sortType_CommercialInvoice = 'totalEURAmount_ThisYear'; // set the default sort type
    $scope.sortReverse_CommercialInvoice = true;  // set the default sort order

    $scope.sortValue = {
        SaleByCountry: '-totalEURAmount_LastYear'
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getReportData(
                context.season,
                function (data) {
                    $scope.data = data.data;
                    debugger
                    //percent expectation
                    var totalExpectation = 0;
                    angular.forEach($scope.data.expectedSummaries, function (value, key) {
                        totalExpectation += value.totalEURAmount;
                    }, null);

                    angular.forEach($scope.data.expectedSummaries, function (value, key) {
                        value.percent = jsHelper.round(value.totalEURAmount * 100 / totalExpectation, 2);
                    }, null);

                    //percent proforma invoice
                    var totalProformaInvoice = 0;
                    angular.forEach($scope.data.summaries, function (value, key) {
                        totalProformaInvoice += value.totalEURAmount;
                    }, null);

                    angular.forEach($scope.data.summaries, function (value, key) {
                        value.percent = jsHelper.round(value.totalEURAmount * 100 / totalProformaInvoice, 2);
                    }, null);

                    //percent of usd and eur compare with total (sales expectation)
                    angular.forEach($scope.data.expectedSummaries, function (value, key) {
                        value.usdPercent = value.usdAmount / value.totalEURAmount * 100;
                        value.eurPercent = 100 - value.usdPercent;
                    }, null);

                    //percent of usd and eur compare with total (sales proforma invoice)
                    angular.forEach($scope.data.summaries, function (value, key) {
                        value.usdPercent = value.usdAmount / value.totalEURAmount * 100;
                        value.eurPercent = 100 - value.usdPercent;
                    }, null);

                    //percent of usd and eur compare with total (commercial invoice)
                    angular.forEach($scope.data.commercialInvoiceSummaries, function (value, key) {
                        value.usdPercent = value.usdAmount / value.totalEURAmount * 100;
                        value.eurPercent = 100 - value.usdPercent;
                    }, null);

                    //percent commerciall invoice
                    var totalCommercialInvoiceThisYear = 0;
                    var totalCommercialInvoiceLastYear = 0;
                    angular.forEach($scope.data.commercialInvoiceSummaries, function (value, key) {
                        totalCommercialInvoiceThisYear += value.totalEURAmount_ThisYear;
                        totalCommercialInvoiceLastYear += value.totalEURAmount_LastYear;
                    }, null);

                    angular.forEach($scope.data.commercialInvoiceSummaries, function (value, key) {
                        value.percent_ThisYear = jsHelper.round(value.totalEURAmount_ThisYear * 100 / totalCommercialInvoiceThisYear, 2);
                        value.percent_LastYear = jsHelper.round(value.totalEURAmount_LastYear * 100 / totalCommercialInvoiceLastYear, 2);
                    }, null);

                    angular.forEach($scope.data.commercialInvoiceSummaries, function (value, key) {
                        //value.totalEURMargin_LastYear = value.totalEURAmount_LastYear - value.totalCostEURAmount_LastYear;
                        value.percentEURMargin_LastYear = value.totalMarginAmountEUR_LastYear * 100 / value.totalEURAmount_LastYear;
                    }, null);

                    /*
                    var confirmedTotal = 0;
                    angular.forEach($scope.data.confirmedSummaries, function (value, key) {
                        confirmedTotal += value.totalEURAmount;
                    }, null);

                    angular.forEach($scope.data.confirmedSummaries, function (value, key) {
                        value.percent = jsHelper.round(value.totalEURAmount * 100 / confirmedTotal, 2);
                    }, null);

                    //detail
                    angular.forEach($scope.data.details, function (value, key) {
                        value.percent = jsHelper.round(value.totalEURAmount * 100 / totalProformaInvoice, 2);
                    }, null);

                    angular.forEach($scope.data.confirmedDetails, function (value, key) {
                        value.percent = jsHelper.round(value.totalEURAmount * 100 / confirmedTotal, 2);
                    }, null);

                    // get sale list
                    angular.forEach($scope.data.details, function (value, key) {
                        if ($scope.sales.indexOf(value.saleUD) < 0) {
                            $scope.sales.push(value.saleUD);
                        }
                    }, null);
                    angular.forEach($scope.data.confirmedDetails, function (value, key) {
                        if ($scope.confirmedSales.indexOf(value.saleUD) < 0) {
                            $scope.confirmedSales.push(value.saleUD);
                        }
                    }, null);

                    */

                    $scope.$apply();
                    jQuery('#content').show();
                },
                function (error) {
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        }
    }

    //
    // method
    //
    $scope.method = {
        // summary sale
        getSummarySaleTotalContainer: function (listData) {
            var total = 0;
            if (listData != null) {
                angular.forEach(listData, function (value, key) {
                    total += value.totalCont;
                }, null);
            }
            return total;
        },
        getSummarySaleTotalAmount: function (listData) {
            var total = 0;
            if (listData != null) {
                angular.forEach(listData, function (value, key) {
                    total += value.totalEURAmount;
                }, null);
            }
            return total;
        },
        getSummarySaleTotalUSDAmount: function (listData) {
            var total = 0;
            if (listData != null) {
                angular.forEach(listData, function (value, key) {
                    total += value.usdAmount;
                }, null);
            }
            return total;
        },
        getSummarySaleTotalEurAmount: function (listData) {
            var total = 0;
            if (listData != null) {
                angular.forEach(listData, function (value, key) {
                    total += value.eurAmount;
                }, null);
            }
            return total;
        },

        getSummarySaleTotalClient: function (listData) {
            var total = 0;
            if (listData != null) {
                angular.forEach(listData, function (value, key) {
                    total += value.totalClient;
                }, null);
            }
            return total;
        },

        /*

        // detail
        getDetailSaleTotalContainer: function (saleUD) {
            var total = 0;
            if ($scope.data != null && $scope.data.details != null) {
                angular.forEach($scope.data.details, function (value, key) {
                    if (value.saleUD == saleUD) {
                        total += value.totalCont;
                    }                    
                }, null);
            }
            return total;
        },
        getDetailSaleTotalAmount: function (saleUD) {
            var total = 0;
            if ($scope.data != null && $scope.data.details != null) {
                angular.forEach($scope.data.details, function (value, key) {
                    if (value.saleUD == saleUD) {
                        total += value.totalEURAmount;
                    }
                }, null);
            }
            return total;
        },
        getDetailSaleTotalEurAmount: function (saleUD) {
            var total = 0;
            if ($scope.data != null && $scope.data.details != null) {
                angular.forEach($scope.data.details, function (value, key) {
                    if (value.saleUD == saleUD) {
                        total += value.eurAmount;
                    }
                }, null);
            }
            return total;
        },
        getDetailSaleTotalUSDAmount: function (saleUD) {
            var total = 0;
            if ($scope.data != null && $scope.data.details != null) {
                angular.forEach($scope.data.details, function (value, key) {
                    if (value.saleUD == saleUD) {
                        total += value.usdAmount;
                    }
                }, null);
            }
            return total;
        },
        getDetailSaleTotalClient: function (saleUD) {
            var total = 0;
            if ($scope.data != null && $scope.data.details != null) {
                angular.forEach($scope.data.details, function (value, key) {
                    if (value.saleUD == saleUD) {
                        total += value.totalClient;
                    }
                }, null);
            }
            return total;
        },
        getDetailSaleTotalPercent: function (saleUD) {
            var total = 0;
            if ($scope.data != null && $scope.data.details != null) {
                angular.forEach($scope.data.details, function (value, key) {
                    if (value.saleUD == saleUD) {
                        total += value.percent;
                    }
                }, null);
            }
            return total;
        },

        // confirmed detail
        getConfirmedDetailSaleTotalContainer: function (saleUD) {
            var total = 0;
            if ($scope.data != null && $scope.data.confirmedDetails != null) {
                angular.forEach($scope.data.confirmedDetails, function (value, key) {
                    if (value.saleUD == saleUD) {
                        total += value.totalCont;
                    }
                }, null);
            }
            return total;
        },
        getConfirmedDetailSaleTotalAmount: function (saleUD) {
            var total = 0;
            if ($scope.data != null && $scope.data.confirmedDetails != null) {
                angular.forEach($scope.data.confirmedDetails, function (value, key) {
                    if (value.saleUD == saleUD) {
                        total += value.totalEURAmount;
                    }
                }, null);
            }
            return total;
        },
        getConfirmedDetailSaleTotalEurAmount: function (saleUD) {
            var total = 0;
            if ($scope.data != null && $scope.data.confirmedDetails != null) {
                angular.forEach($scope.data.confirmedDetails, function (value, key) {
                    if (value.saleUD == saleUD) {
                        total += value.eurAmount;
                    }
                }, null);
            }
            return total;
        },
        getConfirmedDetailSaleTotalUSDAmount: function (saleUD) {
            var total = 0;
            if ($scope.data != null && $scope.data.confirmedDetails != null) {
                angular.forEach($scope.data.confirmedDetails, function (value, key) {
                    if (value.saleUD == saleUD) {
                        total += value.usdAmount;
                    }
                }, null);
            }
            return total;
        },
        getConfirmedDetailSaleTotalClient: function (saleUD) {
            var total = 0;
            if ($scope.data != null && $scope.data.confirmedDetails != null) {
                angular.forEach($scope.data.confirmedDetails, function (value, key) {
                    if (value.saleUD == saleUD) {
                        total += value.totalClient;
                    }
                }, null);
            }
            return total;
        },
        getConfirmedDetailSaleTotalPercent: function (saleUD) {
            var total = 0;
            if ($scope.data != null && $scope.data.confirmedDetails != null) {
                angular.forEach($scope.data.confirmedDetails, function (value, key) {
                    if (value.saleUD == saleUD) {
                        total += value.percent;
                    }
                }, null);
            }
            return total;
        }

        //end detail

        */
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
    // init
    //
    $scope.event.init();
}]);