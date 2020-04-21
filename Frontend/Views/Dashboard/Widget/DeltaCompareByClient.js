// object name  : deltaByClient
angular.module('tilsoftApp').filter('deltaByClientCompareCustomFilter', function () {
    return function (data, client, saleUD, isDeltaOfDetalAll) {
        if (client === '' && saleUD === '' && isDeltaOfDetalAll === false) return data;
        var result = [];
        angular.forEach(data, function (item) {
            var isValid = true;
            if (client !== '') {
                client = client.toUpperCase();
                isValid = isValid && (item.clientUD.toUpperCase().indexOf(client) >= 0 || item.clientNM.toUpperCase().indexOf(client) >= 0);
            }
            if (saleUD !== '') {
                saleUD = saleUD.toUpperCase();
                isValid = isValid && item.saleUD.toUpperCase().indexOf(saleUD) >= 0;
            }
            if (isDeltaOfDetalAll) {
                isValid = isValid && parseFloat(item.piConfirmedDelta5PercentLastYear) !== parseFloat(0) && parseFloat(item.piConfirmedDelta5PercentThisYear) !== parseFloat(0);
            }
            //result filter
            if (isValid) {
                result.push(item);
            }
        });

        //console.log(result);
        return result;
    };
});

angular.module('tilsoftApp').filter('sumFunc', function () {
    return function (data, key) {
        if (angular.isUndefined(data) || angular.isUndefined(key))
            return 0;
        var sum = parseFloat('0');
        angular.forEach(data, function (v, k) {
            sum = sum + parseFloat(v[key] == null ? 0 : v[key]);
        });
        return sum;
    };
});

angular.module('tilsoftApp').controller('deltaByClientController', ['$scope', '$rootScope', '$timeout', '$filter', '$interval', function ($scope, $rootScope, $timeout, $filter, $interval) {
    $scope.deltaByClient = {};

    //
    //filter property
    //
    $scope.deltaByClient.deltaByClientCompareFilter = {
        client: '',
        saleUD: '',
        isDeltaOfDetalAll: false
    };

    $scope.deltaByClient.compareDeltaHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.$apply(function () {
                $scope.deltaByClient.compareDeltaHandler.orderByColumn = (sortedDirection === 'desc' ? '-' : '') + sortedBy;
            });
        },
        isTriggered: false,
        orderByColumn: ''
    };

    //
    //property
    //
    $scope.deltaByClient.detalByClientCompare = [];
    $scope.deltaByClient.totalPIConfirmedAmountLastYear = 0.00;
    $scope.deltaByClient.totalOfferAmountThisYear = 0.00;
    $scope.deltaByClient.totalPIAmountThisYear = 0.00;
    $scope.deltaByClient.totalPIConfirmedAmountThisYear = 0.00;

    $scope.deltaByClient.event = {
        init: function () {
            // load data
            $scope.deltaByClient.method.loadData(
                contextDeltaByClientCompare.currentSeason,
                function (data) { 
                    $scope.deltaByClient.detalByClientCompare = data.data;

                    //calculate delta by Sale
                    var sales = [];
                    var deltaBySale = [];
                    var delta7BySale = [];
                    for (var i = 0; i < $scope.deltaByClient.detalByClientCompare.length; i++) {
                        var item = $scope.deltaByClient.detalByClientCompare[i];
                        if (sales.indexOf(item.saleUD) < 0) {
                            sales.push(item.saleUD);
                            var deltaData = $scope.deltaByClient.detalByClientCompare.filter(o => o.saleUD === item.saleUD);

                            var piConfirmedPurchasingAmountLastYear = jsHelper.sum(deltaData, ['piConfirmedPurchasingAmountLastYear']);
                            var piConfirmedSaleAmountLastYear = jsHelper.sum(deltaData, ['piConfirmedSaleAmountLastYear']);
                            var piConfirmedDelta5AmountLastYear = jsHelper.sum(deltaData, ['piConfirmedDelta5AmountLastYear']);
                            var piConfirmedDelta7AmountLastYear = jsHelper.sum(deltaData, ['piConfirmedDelta7AmountLastYear']);

                            var offerPurchasingAmountThisYear = jsHelper.sum(deltaData, ['offerPurchasingAmountThisYear']);
                            var offerSaleAmountThisYear = jsHelper.sum(deltaData, ['offerSaleAmountThisYear']);
                            var offerDelta5AmountThisYear = jsHelper.sum(deltaData, ['offerDelta5AmountThisYear']);
                            var offerDelta7AmountThisYear = jsHelper.sum(deltaData, ['offerDelta7AmountThisYear']);

                            var piPurchasingAmountThisYear = jsHelper.sum(deltaData, ['piPurchasingAmountThisYear']);
                            var piSaleAmountThisYear = jsHelper.sum(deltaData, ['piSaleAmountThisYear']);
                            var piDelta5AmountThisYear = jsHelper.sum(deltaData, ['piDelta5AmountThisYear']);
                            var piDelta7AmountThisYear = jsHelper.sum(deltaData, ['piDelta7AmountThisYear']);

                            var piConfirmedPurchasingAmountThisYear = jsHelper.sum(deltaData, ['piConfirmedPurchasingAmountThisYear']);
                            var piConfirmedSaleAmountThisYear = jsHelper.sum(deltaData, ['piConfirmedSaleAmountThisYear']);
                            var piConfirmedDelta5AmountThisYear = jsHelper.sum(deltaData, ['piConfirmedDelta5AmountThisYear']);
                            var piConfirmedDelta7AmountThisYear = jsHelper.sum(deltaData, ['piConfirmedDelta7AmountThisYear']);

                            //delta5
                            var deltaItem = {
                                name: 'DELTA6',
                                saleUD: item.saleUD,

                                piConfirmedDelta5PercentLastYear: (piConfirmedPurchasingAmountLastYear === 0 ? 0 : piConfirmedDelta5AmountLastYear / piConfirmedPurchasingAmountLastYear * 100),
                                piConfirmedSaleAmountLastYear: piConfirmedSaleAmountLastYear,
                                piConfirmedDelta5AmountLastYear: piConfirmedDelta5AmountLastYear,
                                piConfirmedContLastYear: jsHelper.sum(deltaData, ['piConfirmedContLastYear']),

                                offerDelta5PercentThisYear: (offerPurchasingAmountThisYear === 0 ? 0 : offerDelta5AmountThisYear / offerPurchasingAmountThisYear * 100),
                                offerSaleAmountThisYear: offerSaleAmountThisYear,
                                offerDelta5AmountThisYear: offerDelta5AmountThisYear,
                                offerContThisYear: jsHelper.sum(deltaData, ['offerContThisYear']),

                                piDelta5PercentThisYear: (piPurchasingAmountThisYear === 0 ? 0 : piDelta5AmountThisYear / piPurchasingAmountThisYear * 100),
                                piSaleAmountThisYear: piSaleAmountThisYear,
                                piDelta5AmountThisYear: piDelta5AmountThisYear,
                                piContThisYear: jsHelper.sum(deltaData, ['piContThisYear']),

                                piConfirmedDelta5PercentThisYear: (piConfirmedPurchasingAmountThisYear === 0 ? 0 : piConfirmedDelta5AmountThisYear / piConfirmedPurchasingAmountThisYear * 100),
                                piConfirmedSaleAmountThisYear: piConfirmedSaleAmountThisYear,
                                piConfirmedDelta5AmountThisYear: piConfirmedDelta5AmountThisYear,
                                piConfirmedContThisYear: jsHelper.sum(deltaData, ['piConfirmedContThisYear']),

                                deltaOfDelta: null,
                                deltaOfDelta2: null,
                                deltaOfDelta3: null,
                                deltaOfDelta_ExistingClient: null
                            };
                            if (item.saleUD === 'RV') {
                                console.log(deltaItem);
                            }

                            deltaItem.deltaOfDelta = deltaItem.offerDelta5PercentThisYear - deltaItem.piConfirmedDelta5PercentLastYear;
                            deltaItem.deltaOfDelta2 = deltaItem.piDelta5PercentThisYear - deltaItem.piConfirmedDelta5PercentLastYear;
                            deltaItem.deltaOfDelta3 = deltaItem.piConfirmedDelta5PercentThisYear - deltaItem.piConfirmedDelta5PercentLastYear;
                            deltaItem.deltaOfDelta_ExistingClient = $scope.deltaByClient.deltaOfDelta5_PIConfirmed_Existing_Client(item.saleUD);
                            deltaBySale.push(deltaItem);

                            //delta7
                            var delta7Item = {
                                name: 'DELTA9',
                                saleUD: item.saleUD,

                                piConfirmedDelta7PercentLastYear: (piConfirmedPurchasingAmountLastYear === 0 ? 0 : piConfirmedDelta7AmountLastYear / piConfirmedPurchasingAmountLastYear * 100),
                                piConfirmedSaleAmountLastYear: piConfirmedSaleAmountLastYear,
                                piConfirmedDelta7AmountLastYear: piConfirmedDelta7AmountLastYear,
                                piConfirmedContLastYear: jsHelper.sum(deltaData, ['piConfirmedContLastYear']),

                                offerDelta7PercentThisYear: (offerPurchasingAmountThisYear === 0 ? 0 : offerDelta7AmountThisYear / offerPurchasingAmountThisYear * 100),
                                offerSaleAmountThisYear: offerSaleAmountThisYear,
                                offerDelta7AmountThisYear: offerDelta7AmountThisYear,
                                offerContThisYear: jsHelper.sum(deltaData, ['offerContThisYear']),

                                piDelta7PercentThisYear: (piPurchasingAmountThisYear === 0 ? 0 : piDelta7AmountThisYear / piPurchasingAmountThisYear * 100),
                                piSaleAmountThisYear: piSaleAmountThisYear,
                                piDelta7AmountThisYear: piDelta7AmountThisYear,
                                piContThisYear: jsHelper.sum(deltaData, ['piContThisYear']),

                                piConfirmedDelta7PercentThisYear: (piConfirmedPurchasingAmountThisYear === 0 ? 0 : piConfirmedDelta7AmountThisYear / piConfirmedPurchasingAmountThisYear * 100),
                                piConfirmedSaleAmountThisYear: piConfirmedSaleAmountThisYear,
                                piConfirmedDelta7AmountThisYear: piConfirmedDelta7AmountThisYear,
                                piConfirmedContThisYear: jsHelper.sum(deltaData, ['piConfirmedContThisYear']),

                                deltaOfDelta: null,
                                deltaOfDelta2: null,
                                deltaOfDelta3: null,
                                deltaOfDelta_ExistingClient: null
                            };

                            delta7Item.deltaOfDelta = delta7Item.offerDelta7PercentThisYear - delta7Item.piConfirmedDelta7PercentLastYear;
                            delta7Item.deltaOfDelta2 = delta7Item.piDelta7PercentThisYear - delta7Item.piConfirmedDelta7PercentLastYear;
                            delta7Item.deltaOfDelta3 = delta7Item.piConfirmedDelta7PercentThisYear - delta7Item.piConfirmedDelta7PercentLastYear;
                            delta7Item.deltaOfDelta_ExistingClient = $scope.deltaByClient.deltaOfDelta7_PIConfirmed_Existing_Client(item.saleUD);
                            delta7BySale.push(delta7Item);
                        }
                    }

                    //delta5 by sale
                    $scope.deltaByClient.deltaBySale = deltaBySale;

                    //delta7 by sale
                    $scope.deltaByClient.delta7BySale = delta7BySale;

                    $scope.$apply();

                    $('#widget-deltaByClient-loading').hide();
                    $('#widget-deltaByClient-container').show();
                },
                function (error) {
                    console.log(error);
                    $scope.deltaByClient.deltaComparison = null;
                    $scope.$apply();
                }
            );
        }
    };
    $scope.deltaByClient.method = {
        loadData: function (season, successCallBack, errorCallBack) {
            $.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + context.token
                },
                type: "POST",
                dataType: 'json',
                url: context.serviceUrl + 'get-delta-by-client-compare?season=' + season,
                beforeSend: function () {
                    //jsHelper.loadingSwitch(true);
                },
                success: function (data) {
                    //jsHelper.loadingSwitch(false);
                    successCallBack(data);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    //jsHelper.loadingSwitch(false);
                    errorCallBack(xhr.responseJSON.exceptionMessage);
                }
            });
        }
    };

    //
    //delta 5
    //

    $scope.deltaByClient.totalPiConfirmedDelta5PercentLastYear = function () {
        if (!$scope.deltaByClient.detalByClientCompare_AfterFilter) return 0;

        var delta = 0;
        var purchase = 0;
        angular.forEach($scope.deltaByClient.detalByClientCompare_AfterFilter.filter(o => o.piConfirmedPurchasingAmountLastYear > 0), function (item) {
            delta += parseFloat(item.piConfirmedDelta5AmountLastYear);
            purchase += parseFloat(item.piConfirmedPurchasingAmountLastYear);
        });
        if (purchase !== 0) {
            return delta * 100 / purchase;
        }
        else {
            return 0;
        }
    };

    $scope.deltaByClient.totalOfferDelta5PercentThisYear = function () {
        if (!$scope.deltaByClient.detalByClientCompare_AfterFilter) return 0;

        var delta = 0;
        var purchase = 0;
        angular.forEach($scope.deltaByClient.detalByClientCompare_AfterFilter.filter(o => o.offerPurchasingAmountThisYear > 0), function (item) {
            delta += parseFloat(item.offerDelta5AmountThisYear);
            purchase += parseFloat(item.offerPurchasingAmountThisYear);
        });
        if (purchase !== 0) {
            return delta * 100 / purchase;
        }
        else {
            return 0;
        }
    };

    $scope.deltaByClient.totalPiDelta5PercentThisYear = function () {
        if (!$scope.deltaByClient.detalByClientCompare_AfterFilter) return 0;

        var delta = 0;
        var purchase = 0;
        angular.forEach($scope.deltaByClient.detalByClientCompare_AfterFilter.filter(o => o.piPurchasingAmountThisYear > 0), function (item) {
            delta += parseFloat(item.piDelta5AmountThisYear);
            purchase += parseFloat(item.piPurchasingAmountThisYear);
        });
        if (purchase !== 0) {
            return delta * 100 / purchase;
        }
        else {
            return 0;
        }
    };

    $scope.deltaByClient.totalPiConfirmedDelta5PercentThisYear = function () {
        if (!$scope.deltaByClient.detalByClientCompare_AfterFilter) return 0;

        var delta = 0;
        var purchase = 0;
        angular.forEach($scope.deltaByClient.detalByClientCompare_AfterFilter.filter(o => o.piConfirmedPurchasingAmountThisYear > 0), function (item) {
            delta += parseFloat(item.piConfirmedDelta5AmountThisYear);
            purchase += parseFloat(item.piConfirmedPurchasingAmountThisYear);
        });
        if (purchase !== 0) {
            return delta * 100 / purchase;
        }
        else {
            return 0;
        }
    };

    $scope.deltaByClient.deltaOfDelta5_PIConfirmed_Existing_Client = function (saleUD) {
        if (!$scope.deltaByClient.detalByClientCompare_AfterFilter) return 0;

        var deltaThisYear = 0;
        var purchaseThisYear = 0;
        var deltaLastYear = 0;
        var purchaseLastYear = 0;

        var dataExistingClient = [];
        if (saleUD !== null) {
            dataExistingClient = $scope.deltaByClient.detalByClientCompare.filter(o => o.saleUD === saleUD && parseFloat(o.piConfirmedDelta5PercentLastYear) !== parseFloat(0) && parseFloat(o.piConfirmedDelta5PercentThisYear) !== parseFloat(0));
        }
        else {
            dataExistingClient = $scope.deltaByClient.detalByClientCompare_AfterFilter.filter(o => parseFloat(o.piConfirmedDelta5PercentLastYear) !== parseFloat(0) && parseFloat(o.piConfirmedDelta5PercentThisYear) !== parseFloat(0));
        }

        angular.forEach(dataExistingClient, function (item) {
            deltaThisYear += parseFloat(item.piConfirmedDelta5AmountThisYear);
            purchaseThisYear += parseFloat(item.piConfirmedPurchasingAmountThisYear);

            deltaLastYear += parseFloat(item.piConfirmedDelta5AmountLastYear);
            purchaseLastYear += parseFloat(item.piConfirmedPurchasingAmountLastYear);
        });

        var deltaPercentThisYear = 0;
        var deltaPercentLastYear = 0;
        if (purchaseThisYear !== 0) {
            deltaPercentThisYear = deltaThisYear * 100 / purchaseThisYear;
        }
        if (purchaseLastYear !== 0) {
            deltaPercentLastYear = deltaLastYear * 100 / purchaseLastYear;
        }
        return deltaPercentThisYear - deltaPercentLastYear;
    };

    //
    //delta 7
    //

    $scope.deltaByClient.totalPiConfirmedDelta7PercentLastYear = function () {
        if (!$scope.deltaByClient.detalByClientCompare_AfterFilter) return 0;

        var delta = 0;
        var purchase = 0;
        angular.forEach($scope.deltaByClient.detalByClientCompare_AfterFilter.filter(o => o.piConfirmedPurchasingAmountLastYear > 0), function (item) {
            delta += parseFloat(item.piConfirmedDelta7AmountLastYear);
            purchase += parseFloat(item.piConfirmedPurchasingAmountLastYear);
        });
        if (purchase !== 0) {
            return delta * 100 / purchase;
        }
        else {
            return 0;
        }
    };

    $scope.deltaByClient.totalOfferDelta7PercentThisYear = function () {
        if (!$scope.deltaByClient.detalByClientCompare_AfterFilter) return 0;

        var delta = 0;
        var purchase = 0;
        angular.forEach($scope.deltaByClient.detalByClientCompare_AfterFilter.filter(o => o.offerPurchasingAmountThisYear > 0), function (item) {
            delta += parseFloat(item.offerDelta7AmountThisYear);
            purchase += parseFloat(item.offerPurchasingAmountThisYear);
        });
        if (purchase !== 0) {
            return delta * 100 / purchase;
        }
        else {
            return 0;
        }
    };

    $scope.deltaByClient.totalPiDelta7PercentThisYear = function () {
        if (!$scope.deltaByClient.detalByClientCompare_AfterFilter) return 0;

        var delta = 0;
        var purchase = 0;
        angular.forEach($scope.deltaByClient.detalByClientCompare_AfterFilter.filter(o => o.piPurchasingAmountThisYear > 0), function (item) {
            delta += parseFloat(item.piDelta7AmountThisYear);
            purchase += parseFloat(item.piPurchasingAmountThisYear);
        });
        if (purchase !== 0) {
            return delta * 100 / purchase;
        }
        else {
            return 0;
        }
    };

    $scope.deltaByClient.totalPiConfirmedDelta7PercentThisYear = function () {
        if (!$scope.deltaByClient.detalByClientCompare_AfterFilter) return 0;

        var delta = 0;
        var purchase = 0;
        angular.forEach($scope.deltaByClient.detalByClientCompare_AfterFilter.filter(o => o.piConfirmedPurchasingAmountThisYear > 0), function (item) {
            delta += parseFloat(item.piConfirmedDelta7AmountThisYear);
            purchase += parseFloat(item.piConfirmedPurchasingAmountThisYear);
        });
        if (purchase !== 0) {
            return delta * 100 / purchase;
        }
        else {
            return 0;
        }
    };

    $scope.deltaByClient.deltaOfDelta7_PIConfirmed_Existing_Client = function (saleUD) {
        if (!$scope.deltaByClient.detalByClientCompare_AfterFilter) return 0;

        var deltaThisYear = 0;
        var purchaseThisYear = 0;
        var deltaLastYear = 0;
        var purchaseLastYear = 0;

        var dataExistingClient = [];
        if (saleUD !== null) {
            dataExistingClient = $scope.deltaByClient.detalByClientCompare.filter(o => o.saleUD === saleUD && o.piConfirmedPurchasingAmountThisYear !== 0 && o.piConfirmedDelta7AmountThisYear !== 0 && o.piConfirmedPurchasingAmountLastYear !== 0 && o.piConfirmedDelta7AmountLastYear !== 0);
        }
        else {
            dataExistingClient = $scope.deltaByClient.detalByClientCompare_AfterFilter.filter(o => o.piConfirmedPurchasingAmountThisYear !== 0 && o.piConfirmedDelta7AmountThisYear !== 0 && o.piConfirmedPurchasingAmountLastYear !== 0 && o.piConfirmedDelta7AmountLastYear !== 0);
        }

        angular.forEach(dataExistingClient, function (item) {
            deltaThisYear += parseFloat(item.piConfirmedDelta7AmountThisYear);
            purchaseThisYear += parseFloat(item.piConfirmedPurchasingAmountThisYear);

            deltaLastYear += parseFloat(item.piConfirmedDelta7AmountLastYear);
            purchaseLastYear += parseFloat(item.piConfirmedPurchasingAmountLastYear);
        });

        var deltaPercentThisYear = 0;
        var deltaPercentLastYear = 0;
        if (purchaseThisYear !== 0) {
            deltaPercentThisYear = deltaThisYear * 100 / purchaseThisYear;
        }
        if (purchaseLastYear !== 0) {
            deltaPercentLastYear = deltaLastYear * 100 / purchaseLastYear;
        }
        return deltaPercentThisYear - deltaPercentLastYear;
    };

    //
    //get total function
    //
    $scope.deltaByClient.totalPIConfirmedAmountLastYear = function () {
        if (!$scope.deltaByClient.detalByClientCompare_AfterFilter) return 0;
        var total = 0;
        total = jsHelper.sum($scope.deltaByClient.detalByClientCompare_AfterFilter, ['piConfirmedSaleAmountLastYear']);
        return total;
    };

    $scope.deltaByClient.totalOfferAmountThisYear = function () {
        if (!$scope.deltaByClient.detalByClientCompare_AfterFilter) return 0;
        var total = 0;
        total = jsHelper.sum($scope.deltaByClient.detalByClientCompare_AfterFilter, ['offerSaleAmountThisYear']);
        return total;
    };

    $scope.deltaByClient.totalPIAmountThisYear = function () {
        if (!$scope.deltaByClient.detalByClientCompare_AfterFilter) return 0;
        var total = 0;
        total = jsHelper.sum($scope.deltaByClient.detalByClientCompare_AfterFilter, ['piSaleAmountThisYear']);
        return total;
    };

    $scope.deltaByClient.totalPIConfirmedAmountThisYear = function () {
        if (!$scope.deltaByClient.detalByClientCompare_AfterFilter) return 0;
        var total = 0;
        total = jsHelper.sum($scope.deltaByClient.detalByClientCompare_AfterFilter, ['piConfirmedSaleAmountThisYear']);
        return total;
    };

    $scope.deltaByClient.event.init();
}]);