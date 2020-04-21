//
// APP START
//
var clientApp = angular.module('tilsoftApp', ['avs-directives']);
clientApp.filter('listGeneralFilter', function () {
    return function (data, additionalConditionNM) {
        debugger;
        var result = [];
        angular.forEach(data, function (item) {
            if (item.additionalConditionNM === additionalConditionNM && item.additionalConditionTypeID === 1) {
                result.push(item);
            }
        });
        return result;
    };
});
clientApp.filter('listPackagingFilter', function () {
    return function (data, additionalConditionNM) {
        var result = [];
        debugger;
        angular.forEach(data, function (item) {
            if (item.additionalConditionNM === additionalConditionNM && item.additionalConditionTypeID === 3) {
                result.push(item);
            }
        });
        return result;
    };
});
clientApp.filter('listTestingFilter', function () {
    return function (data, additionalConditionNM) {
        var result = [];
        debugger;
        angular.forEach(data, function (item) {
            if (item.additionalConditionNM === additionalConditionNM && item.additionalConditionTypeID === 2) {
                result.push(item);
            }
        });
        return result;
    };
});
clientApp.controller('tilsoftController', ['$scope', '$filter', '$timeout', function ($scope, $filter, $timeout) {
    clientService.serviceUrl = context.serviceUrl;
    clientService.baseUrl = context.baseUrl;
    clientService.token = context.token;
    clientService.viewUrl = context.viewUrl;

    $scope.data = null;
    $scope.offers = null;

    $scope.SelectedPriceID = null;
    $scope.plcs = null;
    $scope.plcSaleOrders = null;
    $scope.plcLoadingPLans = null;
    $scope.commercialInvoices = null;
    $scope.sampleOrders = null;
    $scope.clientComplaints = null;
    $scope.saleOrders = null;
    $scope.loadingPlanDTOs = null;
    $scope.chartCommercialInvoices = [];
    $scope.piSearchResult = [];
    $scope.clientBreakdownOptions = [];
    $scope.clientSeasonSpecPercents = [];
    $scope.clientBreakdownManagementFees = [];
    $scope.sdata = [];
    $scope.subTotalRow = [];
    $scope.totalRow = [];
    $scope.breakdownCategorys = [];
    $scope.estimatedAdditionalCost = [];
    $scope.clientAdditionalConditionDTO = [];
    $scope.clientAdditionalConditionDTOShow = [];

    $scope.clientComplaintStatuses = [];
    $scope.clientComplaintTypes = [];
    $scope.clientAdditionalConditionOverviewDTO = [];
    $scope.percentMarginSortClick = -1;

    $scope.filterContractData = function (item) {
        return (item.isSigned === true && item.contractTypeID === 1);
    };

    $scope.support = {
        seasons: jsHelper.getSeasons(),
        itemTypes: [
            { id: 1, name: 'PRODUCT' },
            { id: 2, name: 'SPAREPART' }
        ],
        waitStatuses: [
            { id: 1, name: 'WAIT FOR EUROFAR' },
            { id: 2, name: 'WAIT FOR FACTORY' }
        ],
    };

    $scope.delta = {
        season: jsHelper.getCurrentSeason(),
        exRate: 0,
        eurofarPriceOverviewDTOs: []
    };

    $scope.deltaSummary = {
        totalOrderQnt: 0,
        totalOrderQnt40HC: 0.00,
        totalLoadedQnt: 0,
        totalLoadedQnt40HC: 0.00,
        totalShippedQnt: 0,
        totalShippedQnt40HC: 0.00,
        totalToBeShippedQnt: 0,
        totalToBeShippedQnt40HC: 0.00,
        totalPurchasingAmountUSD: 0.00,
        totalSaleAmountUSD: 0.00,
        totalSaleAmountEUR: 0.00,
        totalSaleAmountConvertToEUR: 0.00,
        totalTransportAmountEUR: 0.00,
        totalSaleAmountUSDForMargin: 0.00,
        totalGrossMarginAmountBeforeComm: 0.00,
        totalGrossMarginPercentBeforeComm: 0.00,
        totalCommissionAmount: 0.00,
        totalTransportAmount: 0.00,
        totalGrossMarginAmountAfterComm: 0.00,
        totalGrossMarginPercentAfterComm: 0.00
    };

    $scope.delta2 = {
        season: jsHelper.getCurrentSeason(),
        exRate: 0,
        eurofarPriceOverviewDTOs: []
    };

    $scope.delta2Summary = {
        totalOrderQnt: 0,
        totalOrderQnt40HC: 0.00,
        totalLoadedQnt: 0,
        totalLoadedQnt40HC: 0.00,
        totalShippedQnt: 0,
        totalShippedQnt40HC: 0.00,
        totalToBeShippedQnt: 0,
        totalToBeShippedQnt40HC: 0.00,
        totalPurchasingAmountUSD: 0.00,
        totalSaleAmountUSD: 0.00,
        totalSaleAmountEUR: 0.00,
        totalSaleAmountConvertToEUR: 0.00,
        totalTransportAmountEUR: 0.00,
        totalSaleAmountUSDForMargin: 0.00,
        totalGrossMarginAmountBeforeComm: 0.00,
        totalGrossMarginPercentBeforeComm: 0.00,
        totalCommissionAmount: 0.00,
        totalTransportAmount: 0.00,
        totalGrossMarginAmountAfterComm: 0.00,
        totalGrossMarginPercentAfterComm: 0.00
    };

    $scope.delta3 = {
        season: jsHelper.getCurrentSeason(),
        exRate: 0,
        eurofarPriceOverviewDTOs: []
    };

    $scope.offersmargin = {
        season: jsHelper.getCurrentSeason(),
        exRate: 0,
        offerDatas : []
    };
    if (context.autoSeason) {
        $scope.offersmargin.season = context.autoSeason;
    }


    $scope.offersmargin.offerMarginDTOs = [];

    $scope.seasons = jsHelper.getSeasons();

    // overview - cis
    $scope.shippingPerformanceData = null;

    $scope.itemData = null;
    $scope.modelData = {
        season: null,
        modelDTOs: [],
        exRate: null,
    };
    $scope.itemDataSummary = {
        totalOrderQnt: 0,
        totalOrderQnt40HC: 0.00,
        totalShippedQnt: 0,
        totalShippedQnt40HC: 0.00,
        totalToBeShippedQnt: 0,
        totalToBeShippedQnt40HC: 0.00,
        totalAmountUSD: 0.00,
        totalAmountEUR: 0.00,
        totalAmountConvertToEUR: 0.00
    };

    $scope.priceComparisonData = null;

    $scope.filters = {
        season: jsHelper.getCurrentSeason(),
        Season: jsHelper.getCurrentSeason(),
        ClientID: context.id,
        Description: '',
        FactoryUD: '',
        ItemTypeID: 1,
        StatusID: 1,
        QuotationOfferDirectionID: 1,
        ProformaInvoiceNo: '',
        OrderTypeID: ''
    };

    $scope.gridHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
        },
        isTriggered: false
    };

    $scope.GICItem = {
        //clientContactHistoryID: null,
        communicationContent: null,
        lastCommunicationContent: null
    };

    //$scope.editGIC = function (item) {
    //    $scope.GICItem = {
    //        clientContactHistoryID: item.clientContactHistoryID,
    //        communicationContent: item.communicationContent
    //    };
    //};

    $scope.checkEditDate = function (item) {
        var today = new Date;
        var day = ("0" + today.getDate()).slice(-2);
        var month = ("0" + (today.getMonth() + 1)).slice(-2);
        var year = today.getFullYear();
        var todayFormated = day + '/' + month + '/' + year;
        if (todayFormated === item.contactDate) {
            return true;
        }
        else {
            return false;
        }
    };

    $scope.loadingState = {
        cis: false,
        offer: false,
        pi: false,
        ci: false,
        loadingplan: false,
        plc: false,
        sample: false,
        complain: false,
        delta: false,
        delta2: false,
        delta3: false,
        purchasingoverview: false,
        estimatedAdditionalCost: false,
        clientAdditionalConditionDTO: false,
        offermargin: false
    };

    //Offer overview
    $scope.calTotalQuantity = function (OfferMargin) {
        if (OfferMargin === null) return;
        var total = parseInt('0');
        angular.forEach(OfferMargin, function (item) {
            if(item.status === 1)
                total += (JSON.stringify(item.totalQnt) === 'null' ? parseInt('0') : parseInt(item.totalQnt));
        });
        return total;
    };

    $scope.calTotalQuantity40HC = function (OfferMargin) {
        if (OfferMargin === null) return;
        var total = parseInt('0');
        angular.forEach(OfferMargin, function (item) {
            if(item.status === 1)
                total += (JSON.stringify(item.totalCont) === 'null' ? parseInt('0') : parseFloat(item.totalCont));
        });
        return total;
    };

    $scope.calTotalOfferAmount = function (OfferMargin) {
        if (OfferMargin === null) return;
        var total = 0.0;
        angular.forEach(OfferMargin, function (item) {
            if (item.status === 1)
                total = total + item.totalAmount;            
        }); 
        return total;
    };

    $scope.calTotalFactoryAmount = function (OfferMargin) {
        if (OfferMargin === null) return;
        var total = 0.0;
        angular.forEach(OfferMargin, function (item) {
            if (item.status === 1)
                total = total + item.totalFactoryAmount;
        });
        return total;
    };

    $scope.calTotalPercentMargin = function (OfferMargin) {
        if (OfferMargin === null) return;
        var percent = 0.0;
        var totalAmount = 0.0;
        var totalFactoryAmount = 0.0;
        angular.forEach(OfferMargin, function (item) {
            if (item.totalAmount !== null && item.totalAmount > 0 && item.status === 1) {
                totalAmount = totalAmount + item.totalAmount ;
            }
        });
        angular.forEach(OfferMargin, function (item) {
            if (item.totalFactoryAmount !== null && item.totalFactoryAmount > 0 && item.status === 1) {
                totalFactoryAmount = totalFactoryAmount + item.totalFactoryAmount;
            }
        });

        var transport = $scope.calTransportationAmountUSD(OfferMargin);
        var lc = $scope.calLcCostAmountUSD(OfferMargin);
        var inter = $scope.calInterestAmountUSD(OfferMargin);
        var comm = $scope.calCommissionAmountUSD(OfferMargin);
        var dam = $scope.calDamageAmountUSD(OfferMargin);


        percent = ((totalAmount - totalFactoryAmount - transport - lc - inter - comm - dam) * 100) / totalFactoryAmount;
        return percent;
    };

    $scope.calTotalOfferItem = function (id) {
        if ($scope.offersmargin.offerMarginDTOs === null) return;
        var total = 0;
        angular.forEach($scope.offersmargin.offerMarginDTOs, function (item) {
            if (item.offerID === id) {
                total = total + item.totalItem;
            }
        });
        return total;
    };

    $scope.calTotalMargin = function (OfferMargin) {
        if (OfferMargin === null) return;
        var total = 0.0;
        angular.forEach(OfferMargin, function (item) {
            if (item.status === 1)
                total = total + item.totalMargin;
        });
        return total;
    };

    $scope.calTransportationAmountUSD = function (OfferMargin) {
        if (OfferMargin === null) return;
        var total = 0.0;
        angular.forEach(OfferMargin, function (item) {
            if (item.status === 1)
                total = total + item.transportationAmountUSD;
        });
        return total;
    };

    $scope.calLcCostAmountUSD = function (OfferMargin) {
        if (OfferMargin === null) return;
        var total = 0.0;
        angular.forEach(OfferMargin, function (item) {
            if (item.status === 1)
                total = total + item.lcCostAmountUSD;
        });
        return total;
    };

    $scope.calInterestAmountUSD = function (OfferMargin) {
        if (OfferMargin === null) return;
        var total = 0.0;
        angular.forEach(OfferMargin, function (item) {
            if (item.status === 1)
                total = total + item.interestAmountUSD;
        });
        return total;
    };

    $scope.calCommissionAmountUSD = function (OfferMargin) {
        if (OfferMargin === null) return;
        var total = 0.0;
        angular.forEach(OfferMargin, function (item) {
            if (item.status === 1)
                total = total + item.commissionAmountUSD;
        });
        return total;
    };

    $scope.calDamageAmountUSD = function (OfferMargin) {
        if (OfferMargin === null) return;
        var total = 0.0;
        angular.forEach(OfferMargin, function (item) {
            if (item.status === 1)
                total = total + item.damageAmountUSD;
        });
        return total;
    };

    $scope.searchOfferDetail = function ($event, item) {
        $event.preventDefault();
        if ($("#icon-view-detail-" + item.offerID + item.status).hasClass('fa-plus-square-o')) {
            //var arr_offer = $scope.offerMarginDetail.filter(function (o) { return o.offerID == item.offerID });
            if (item.isloaded === false) {
                clientService.getOfferMarginDetail(
                    item.offerID,
                    $scope.offersmargin.season,
                    item.status,
                    $scope.data.clientID,
                    function (data) {
                        item.offerMarginDetail = data.data;
                        angular.forEach(item.offerMarginDetail, function (item) {
                            //item.offerAmount = item.offerAmount - item.transportationAmountUSD - item.lcCostAmountUSD - item.interestAmountUSD - item.commissionAmountUSD - item.damageAmountUSD;
                            item.totalMargin = item.totalMargin - item.transportationAmountUSD - item.lcCostAmountUSD - item.interestAmountUSD - item.commissionAmountUSD - item.damageAmountUSD;
                            item.singleMargin = item.singleMargin - (item.transportationAmountUSD / item.quantity) - (item.lcCostAmountUSD / item.quantity) - (item.interestAmountUSD / item.quantity) - (item.commissionAmountUSD / item.quantity) - (item.damageAmountUSD / item.quantity);
                            if (item.factoryAmount != null && item.factoryAmount != '' && item.factoryAmount > 0) {
                                item.percentMargin = (item.totalMargin / item.factoryAmount) * 100;
                                item.percentSingleMargin = (item.singleMargin / (item.factoryAmount / item.quantity)) * 100;
                            }
                            else {
                                item.percentMargin = null;
                                item.percentSingleMargin = null;
                            }
                        });
                        $scope.$apply();
                        $("." + item.offerID + item.status).toggle();
                        $("#icon-view-detail-" + item.offerID + item.status).removeClass('fa-plus-square-o');
                        $("#icon-view-detail-" + item.offerID + item.status).addClass('fa-minus-square-o');
                    },
                    function (error) {
                        arr_offer[0].offerMarginDetail = null;
                        $scope.$apply();
                    }
                );
                item.isloaded = true;
            }
            else {
                $("." + item.offerID + item.status).toggle();
                $("#icon-view-detail-" + item.offerID + item.status).removeClass('fa-plus-square-o');
                $("#icon-view-detail-" + item.offerID + item.status).addClass('fa-minus-square-o');
            }
        }
        else {
            $("." + item.offerID + item.status).toggle();
            $("#icon-view-detail-" + item.offerID + item.status).removeClass('fa-minus-square-o');
            $("#icon-view-detail-" + item.offerID + item.status).addClass('fa-plus-square-o');
        }
    };

    // event.
    $scope.event = {
        load: load,
        skypeChat: skypeChat,
        openCall: openCall,
        getAgenda: getAgenda,
        getLinkedData: getLinkedData,

        saveGIC: saveGIC,

        getCISShippingPerformance: function () {
            clientService.getCISShippingPerformance(
                context.id,
                $scope.shippingPerformanceData.season,
                function (data) {
                    $scope.shippingPerformanceData = data.data;
                    $scope.$apply();

                    $scope.method.drawShippingPerformanceChart();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.$apply();
                }
            );
        },

        getCISItem: function () {
            clientService.getCISItem(
                context.id,
                $scope.itemData.season,
                function (data) {
                    $scope.itemData = data.data;
                    $scope.itemDataSummary = {
                        totalOrderQnt: 0,
                        totalOrderQnt40HC: 0.00,
                        totalShippedQnt: 0,
                        totalShippedQnt40HC: 0.00,
                        totalToBeShippedQnt: 0,
                        totalToBeShippedQnt40HC: 0.00,
                        totalAmountUSD: 0.00,
                        totalAmountEUR: 0.00,
                        totalAmountConvertToEUR: 0.00
                    };
                    angular.forEach($scope.itemData.itemDTOs, function (item) {
                        if (item.unitPriceUSD || item.unitPriceEUR) {
                            $scope.itemDataSummary.totalOrderQnt += item.totalOrderQnt;
                            $scope.itemDataSummary.totalOrderQnt40HC += item.totalOrderQnt40HC;
                            $scope.itemDataSummary.totalShippedQnt += item.totalShippedQnt;
                            $scope.itemDataSummary.totalShippedQnt40HC += item.totalShippedQnt40HC;
                            $scope.itemDataSummary.totalToBeShippedQnt += item.totalToBeShippedQnt;
                            $scope.itemDataSummary.totalToBeShippedQnt40HC += item.totalToBeShippedQnt40HC;
                            $scope.itemDataSummary.totalAmountUSD += item.totalAmountUSD;
                            $scope.itemDataSummary.totalAmountEUR += item.totalAmountEUR;
                            $scope.itemDataSummary.totalAmountConvertToEUR += item.totalAmountConvertToEUR;
                        }
                    });

                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.$apply();
                }
            );
        },

        getCISModel: function () {
            clientService.getCISModel(
                context.id,
                $scope.modelData.season,
                function (data) {
                    $scope.modelData = data.data;
                    $scope.modelDataSummary = {
                        totalOrdered: 0,
                        totalOrderQnt40HC: 0.00,
                        totalShipped: 0,
                        totalShippedQnt40HC: 0.00,
                        totalToBeShippedQnt: 0,
                        totalToBeShippedQnt40HC: 0.00,
                        totalAmountUSD: 0.00,
                        totalAmountEUR: 0.00,
                        totalConvertToEUR: 0.00
                    };
                    angular.forEach($scope.modelData.modelDTOs, function (item) {
                        if (item.totalUnitPriceUSD || item.totalUnitPriceEUR) {
                            $scope.modelDataSummary.totalOrdered += item.totalOrdered;
                            $scope.modelDataSummary.totalOrderQnt40HC += item.totalOrderQnt40HC;
                            $scope.modelDataSummary.totalShipped += item.totalShipped;
                            $scope.modelDataSummary.totalShippedQnt40HC += item.totalShippedQnt40HC;
                            $scope.modelDataSummary.totalToBeShippedQnt += item.totalToBeShippedQnt;
                            $scope.modelDataSummary.totalToBeShippedQnt40HC += item.totalToBeShippedQnt40HC;
                            $scope.modelDataSummary.totalAmountUSD += item.totalAmountUSD;
                            $scope.modelDataSummary.totalAmountEUR += item.totalAmountEUR;
                            $scope.modelDataSummary.totalConvertToEUR += item.totalConvertToEUR;
                        }
                    });
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.$apply();
                }
            );
        },

        getCISPriceComparison: function () {
            clientService.getCISPriceComparison(
                context.id,
                $scope.priceComparisonData.season,
                $scope.priceComparisonData.seasonToCompare,
                function (data) {
                    $scope.priceComparisonData = data.data;
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.$apply();
                }
            );
        },

        getPIByArticleCode: function (item) {
            clientService.getPIByArticleCode(
                context.id,
                $scope.itemData.season,
                item.articleCode,
                function (data) {
                    $scope.piSearchResult = data.data;
                    $scope.$apply();

                    $timeout(function () {
                        $('#frmPIResult').modal('show');
                    }, 0);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.piSearchResult = [];
                    $scope.$apply();
                }
            );
        },

        getDeltaData: function () {
            clientService.getDeltaData(
                context.id,
                $scope.delta.season,
                function (data) {
                    $scope.delta = data.data;

                    $scope.deltaSummary = {
                        totalOrderQnt: 0,
                        totalOrderQnt40HC: 0.00,
                        totalLoadedQnt: 0,
                        totalLoadedQnt40HC: 0.00,
                        totalShippedQnt: 0,
                        totalShippedQnt40HC: 0.00,
                        totalToBeShippedQnt: 0,
                        totalToBeShippedQnt40HC: 0.00,
                        totalPurchasingAmountUSD: 0.00,
                        totalSaleAmountUSD: 0.00,
                        totalSaleAmountEUR: 0.00,
                        totalSaleAmountConvertToEUR: 0.00,
                        totalTransportAmountEUR: 0.00,
                        totalSaleAmountUSDForMargin: 0.00,
                        totalGrossMarginAmountBeforeComm: 0.00,
                        totalGrossMarginPercentBeforeComm: 0.00,
                        totalCommissionAmount: 0.00,
                        totalGrossMarginAmountAfterComm: 0.00,
                        totalGrossMarginPercentAfterComm: 0.00,

                        totalCommissionAmountUSD: 0.00,
                        totalDamagesCostAmount: 0.00,
                        totalLCCostAmount: 0.00,
                        totalInterestAmount: 0.00,
                        totalTransportAmount: 0.00
                    };
                    angular.forEach($scope.delta.eurofarPriceOverviewDTOs, function (item) {
                        $scope.deltaSummary.totalOrderQnt += item.orderQnt;
                        $scope.deltaSummary.totalOrderQnt40HC += jsHelper.round(item.qnt40HC ? item.orderQnt / item.qnt40HC : 0, 2);
                        $scope.deltaSummary.totalLoadedQnt += item.loadedQnt;
                        $scope.deltaSummary.totalLoadedQnt40HC += jsHelper.round(item.qnt40HC ? item.loadedQnt / item.qnt40HC : 0, 2);
                        $scope.deltaSummary.totalShippedQnt += item.shippedQnt;
                        $scope.deltaSummary.totalShippedQnt40HC += jsHelper.round(item.qnt40HC ? item.shippedQnt / item.qnt40HC : 0, 2);
                        $scope.deltaSummary.totalToBeShippedQnt += item.orderQnt - item.shippedQnt;
                        $scope.deltaSummary.totalToBeShippedQnt40HC += jsHelper.round(item.qnt40HC ? (item.orderQnt - item.shippedQnt) / item.qnt40HC : 0, 2);
                        $scope.deltaSummary.totalPurchasingAmountUSD += jsHelper.round(item.salePrice * item.orderQnt, 2);
                        $scope.deltaSummary.totalSaleAmountUSD += jsHelper.round(item.currency == 'USD' ? item.unitPrice * item.orderQnt : 0, 2);
                        $scope.deltaSummary.totalSaleAmountEUR += jsHelper.round(item.currency == 'EUR' ? item.unitPrice * item.orderQnt : 0, 2);
                        $scope.deltaSummary.totalSaleAmountConvertToEUR += jsHelper.round(item.currency == 'USD' ? item.unitPrice * item.orderQnt / $scope.delta.exRate : item.unitPrice * item.orderQnt, 2);
                        $scope.deltaSummary.totalTransportAmountEUR += jsHelper.round((item.qnt40HC && item.currency == 'EUR') ? item.transportation * item.orderQnt / item.qnt40HC : 0, 2);
                        $scope.deltaSummary.totalSaleAmountUSDForMargin += jsHelper.round(item.currency == 'USD' ? item.unitPrice * item.orderQnt : (item.unitPrice - (item.qnt40HC ? item.transportation * item.orderQnt / item.qnt40HC : 0)) * $parent.delta.exRate * item.orderQnt, 2);
                        $scope.deltaSummary.totalGrossMarginAmountBeforeComm += jsHelper.round(((item.currency == 'USD' ? item.unitPrice : (item.unitPrice - (item.qnt40HC ? item.transportation * item.orderQnt / item.qnt40HC : 0)) * $parent.delta.exRate) - item.salePrice) * item.orderQnt, 2);
                        //$scope.deltaSummary.totalGrossMarginPercentBeforeComm = 0.00;
                        $scope.deltaSummary.totalCommissionAmount += jsHelper.round((item.currency == 'USD' ? item.unitPrice * item.commission : item.unitPrice * item.commission * $parent.delta.exRate) * item.orderQnt, 2);
                        //$scope.deltaSummary.totalGrossMarginAmountAfterComm += jsHelper.round(item.salePrice ? ((item.currency == 'USD' ? item.unitPrice : (item.unitPrice - (item.qnt40HC ? item.transportation * item.orderQnt / item.qnt40HC : 0)) * $parent.delta.exRate) - item.salePrice - (item.currency == 'USD' ? item.unitPrice * item.commission : item.unitPrice * item.commission * $parent.delta.exRate)) * item.orderQnt - item.lcCostAmount - item.interestAmount : 0, 2);
                        $scope.deltaSummary.totalGrossMarginAmountAfterComm += item.grossMarginAfterInUSD;
                        //$scope.deltaSummary.totalGrossMarginPercentAfterComm = 0.00;
                        $scope.deltaSummary.totalCommissionAmountUSD += item.commissionAmount;
                        $scope.deltaSummary.totalDamagesCostAmount += item.damagesCost;
                        $scope.deltaSummary.totalLCCostAmount += item.lcCostAmount;
                        $scope.deltaSummary.totalInterestAmount += item.interestAmount;
                        $scope.deltaSummary.totalTransportAmount += item.transportCost;
                    });
                    $scope.deltaSummary.totalGrossMarginPercentBeforeComm = jsHelper.round($scope.deltaSummary.totalGrossMarginAmountBeforeComm * 100 / $scope.deltaSummary.totalPurchasingAmountUSD, 2);
                    $scope.deltaSummary.totalGrossMarginPercentAfterComm = jsHelper.round($scope.deltaSummary.totalGrossMarginAmountAfterComm * 100 / $scope.deltaSummary.totalPurchasingAmountUSD, 2);

                    $scope.$apply();

                    $timeout(function () {
                        $('#deltaTable').css('margin-top', '105px');
                    }, 0);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.$apply();
                }
            );
        },

        deltaExportExcel: function () {
            clientService.deltaExportExcel(
                context.id,
                $scope.delta.season,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type != 2) {
                        window.open(context.reportUrl + data.data);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        getDelta2Data: function () {
            clientService.getDelta2Data(
                context.id,
                $scope.delta2.season,
                function (data) {
                    $scope.delta2 = data.data;

                    $scope.delta2Summary = {
                        totalOrderQnt: 0,
                        totalOrderQnt40HC: 0,
                        totalPurchasingAmountUSD: 0.00,
                        totalSaleAmountUSD: 0.00,
                        totalGrossMarginAmountAfterComm: 0.00,
                        totalGrossMarginPercentAfterComm: 0.00,
                        totalLCCostAmount: 0.00,
                        totalInterestAmount: 0.00,
                        totalCommissionAmount: 0.00,
                        totalDamagesCostAmount: 0.00,
                        totalTransportAmount: 0.00
                    };
                    angular.forEach($scope.delta2.eurofarPriceOverviewDTOs, function (item) {
                        $scope.delta2Summary.totalOrderQnt += item.totalOrderQnt;
                        $scope.delta2Summary.totalOrderQnt40HC += item.totalOrderedQnt40HC;
                        $scope.delta2Summary.totalPurchasingAmountUSD += item.totalPurchasingAmount;
                        $scope.delta2Summary.totalSaleAmountUSD += item.totalSaleAmount;
                        $scope.delta2Summary.totalGrossMarginAmountAfterComm += item.grossMargin;
                        $scope.delta2Summary.totalLCCostAmount += item.lcCostAmount;
                        $scope.delta2Summary.totalInterestAmount += item.interestAmount;
                        $scope.delta2Summary.totalCommissionAmount += item.commissionAmount;
                        $scope.delta2Summary.totalDamagesCostAmount += item.damagesCost;
                        $scope.delta2Summary.totalTransportAmount += item.transportCost;
                    });
                    $scope.delta2Summary.totalGrossMarginPercentAfterComm = jsHelper.round($scope.delta2Summary.totalGrossMarginAmountAfterComm * 100 / $scope.delta2Summary.totalPurchasingAmountUSD, 2);

                    $scope.$apply();

                    $timeout(function () {
                        $('#delta2Table').css('margin-top', '105px');
                    }, 0);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.$apply();
                }
            );
        },

        delta2ExportExcel: function () {
            clientService.delta2ExportExcel(
                context.id,
                $scope.delta2.season,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type != 2) {
                        window.open(context.reportUrl + data.data);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        getDelta3Data: function () {
            clientService.getDelta3Data(
                context.id,
                $scope.delta3.season,
                function (data) {
                    $scope.delta3 = data.data;

                    $scope.delta3Summary = {
                        totalOrderQnt: 0,
                        totalOrderQnt40HC: 0,
                        totalPurchasingAmountUSD: 0.00,
                        totalSaleAmountUSD: 0.00,
                        totalGrossMarginAmountAfterComm: 0.00,
                        totalGrossMarginPercentAfterComm: 0.00,
                        totalLCCostAmount: 0.00,
                        totalInterestAmount: 0.00,
                        totalCommissionAmount: 0.00,
                        totalDamagesCostAmount: 0.00,
                        totalTransportAmount: 0.00
                    };
                    angular.forEach($scope.delta3.eurofarPriceOverviewDTOs, function (item) {
                        $scope.delta3Summary.totalOrderQnt += item.totalOrderQnt;
                    });

                    angular.forEach($scope.delta3.eurofarPriceOverviewDTOs, function (item) {
                        $scope.delta3Summary.totalOrderQnt40HC += item.totalOrderedQnt40HC;
                        $scope.delta3Summary.totalPurchasingAmountUSD += item.totalPurchasingAmount;
                        $scope.delta3Summary.totalSaleAmountUSD += item.totalSaleAmount;
                        $scope.delta3Summary.totalGrossMarginAmountAfterComm += item.grossMargin;
                        $scope.delta3Summary.totalLCCostAmount += item.lcCostAmount;
                        $scope.delta3Summary.totalInterestAmount += item.interestAmount;
                        $scope.delta3Summary.totalCommissionAmount += item.commissionAmount;
                        $scope.delta3Summary.totalDamagesCostAmount += item.damagesCost;
                        $scope.delta3Summary.totalTransportAmount += item.transportCost;
                    });
                    $scope.delta3Summary.totalGrossMarginPercentAfterComm = jsHelper.round($scope.delta3Summary.totalGrossMarginAmountAfterComm * 100 / $scope.delta3Summary.totalPurchasingAmountUSD, 3);

                    $scope.$apply();

                    $timeout(function () {
                        //$('#delta3Table').css('margin-top', '105px');
                    }, 0);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.$apply();
                }
            );
        },

        delta3ExportExcel: function () {
            clientService.delta3ExportExcel(
                context.id,
                $scope.delta3.season,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type != 2) {
                        window.open(context.reportUrl + data.data);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        getOfferMarginData: function () {
            $('#offermargintab .tilsoft-table-wrapper').hide();
            clientService.getOfferOverviewData(
                context.id,
                $scope.offersmargin.season,
                function (data) {
                    $scope.offersmargin = data.data;
                    $scope.offersmargin.offerMarginDTOs = data.data.offerMarginDTOs;
                    angular.forEach($scope.offersmargin.offerMarginDTOs, function (item) {
                        item.offerUD = item.offerUD.substring(0, 9);
                        //item.totalAmount = item.totalAmount - item.transportationAmountUSD - item.lcCostAmountUSD - item.interestAmountUSD - item.commissionAmountUSD - item.damageAmountUSD;
                        item.totalMargin = item.totalMargin - item.transportationAmountUSD - item.lcCostAmountUSD - item.interestAmountUSD - item.commissionAmountUSD - item.damageAmountUSD;
                        item.singleMargin = item.singleMargin / item.totalItem - (item.transportationAmountUSD + item.lcCostAmountUSD + item.interestAmountUSD + item.commissionAmountUSD + item.damageAmountUSD) / item.totalQnt;
                       
                        if (item.totalFactoryAmount !== null && item.totalFactoryAmount !== "" && item.totalFactoryAmount > 0) {
                            item.percentMargin = (item.totalMargin / item.totalFactoryAmount) * 100;
                            item.percentSingleMargin = (item.singleMargin / (item.totalFactoryAmount / item.totalQnt)) * 100;
                        }
                        else {
                            item.percentMargin = null;
                            item.percentSingleMargin = null;
                        }
                    });

                    //for (var i = 0; i < $scope.offersmargin.length; i++) {
                    //    var offerLineCount = $scope.offersmargin[i].offerLines.length;
                    //    for (var j = 0; j < offerLineCount; j++) {
                    //        if ($scope.offersmargin[i].offerLines[j].avtPurchasingPrice == 0 || $scope.offersmargin[i].offerLines[j].avtPurchasingPrice == null) {
                    //            $scope.offersmargin[i].offerLines.splice(j, 1);
                    //            j--;
                    //            offerLineCount--;
                    //        }
                    //    }
                    //}

                    $scope.$apply();

                    // refresh avs scroll
                    $('#offermargintab .tilsoft-table-wrapper').show();
                    $timeout(function () {
                        jsHelper.refreshAvsScroll();
                    }, 100);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.$apply();
                }
            );
        },

        clearFilter: function () {
            $scope.filters = {
                Season: jsHelper.getCurrentSeason(),
                ClientID: context.id,
                Description: '',
                FactoryUD: '',
                ItemTypeID: '',
                StatusID: '',
                QuotationOfferDirectionID: '',
                ProformaInvoiceNo: '',
                OrderTypeID: ''
            };
        },

        getBreakdownData: function () {
            clientService.searchFilter.filters = $scope.filters;
            clientService.getBreakdownWithFilter(
                function (data) {
                    //
                    // add custom field before merge to onpage data
                    //                                
                    angular.forEach(data.data.data, function (item) {
                        item.saleUnitPriceEUR = jsHelper.round(parseFloat(item.currency === 'EUR' ? item.salePrice : null), 2);
                        item.saleAmountEUR = jsHelper.round(parseFloat(item.currency === 'EUR' ? item.salePrice * item.orderQnt : null), 2);
                        item.saleUnitPriceUSD = jsHelper.round(parseFloat(item.currency === 'USD' ? item.salePrice : null), 2);
                        item.saleAmountUSD = jsHelper.round(parseFloat(item.currency === 'USD' ? item.salePrice * item.orderQnt : null), 2);
                        item.saleAmountConvertedToEUR = jsHelper.round(item.currency === 'EUR' ? item.saleAmountEUR : item.saleUnitPriceUSD / item.exRate, 2);
                        item.transportation = item.currency === 'EUR' ? 2500 : null;
                        item.transportPerItemEUR = item.currency === 'EUR' ? jsHelper.round(item.qnt40HC ? item.transportation / item.qnt40HC : 0, 2) : null;
                        item.transportAmountEUR = item.currency === 'EUR' ? jsHelper.round(item.transportPerItemEUR * item.orderQnt, 2) : null;
                        item.saleUnitPriceTransportExcludedConvertedToEUR = item.currency === 'EUR' ? jsHelper.round((item.currency === 'EUR' ? item.salePrice - item.transportPerItemEUR : 0) * item.exRate, 2) : null;
                        item.fobUnitPrice = parseFloat(item.currency === 'USD' ? item.salePrice : item.saleUnitPriceTransportExcludedConvertedToEUR);
                        item.fobAmountUSD = jsHelper.round(item.fobUnitPrice * item.orderQnt, 2);
                        item.marginBeforeCommissionUSD = item.priceIncludeDiff ? jsHelper.round((item.fobUnitPrice - item.priceIncludeDiff) * item.orderQnt, 2) : null;
                        item.marginPercentBeforeCommission = item.priceIncludeDiff ? jsHelper.round((item.fobUnitPrice - item.priceIncludeDiff) * 100 / item.priceIncludeDiff, 2) : null;
                        item.marginBeforeCommissionUSDByTarget = item.targetPrice ? jsHelper.round((item.fobUnitPrice - item.targetPrice) * item.orderQnt, 2) : null;
                        item.marginPercentBeforeCommissionByTarget = item.targetPrice ? jsHelper.round((item.fobUnitPrice - item.targetPrice) * 100 / item.targetPrice, 2) : null;
                        item.commissionPerItemUSD = jsHelper.round(parseFloat(item.saleUnitPriceUSD * item.commissionPercent), 2);
                        item.commissionPerItemEUR = jsHelper.round(parseFloat(item.saleUnitPriceEUR * item.commissionPercent), 2);
                        item.commissionPerItemConvertedToUSD = jsHelper.round(item.currency === 'USD' ? item.commissionPerItemUSD : item.commissionPerItemEUR * item.exRate, 2);
                        item.commissionAmountUSD = jsHelper.round(item.commissionPerItemConvertedToUSD * item.orderQnt, 2);
                        item.marginAfterCommissionUSD = item.priceIncludeDiff ? item.marginBeforeCommissionUSD - item.commissionAmountUSD : null;
                        item.marginPercentAfterCommission = item.priceIncludeDiff ? jsHelper.round(item.marginAfterCommissionUSD * 100 / (item.priceIncludeDiff * item.orderQnt), 2) : null;
                        item.marginAfterCommissionUSDByTarget = item.targetPrice ? item.marginBeforeCommissionUSDByTarget - item.commissionAmountUSD : null;
                        item.marginPercentAfterCommissionByTarget = item.targetPrice ? jsHelper.round(item.marginAfterCommissionUSDByTarget * 100 / (item.targetPrice * item.orderQnt), 2) : null;
                        item.fobPurchasingUSD = item.priceIncludeDiff ? jsHelper.round(item.priceIncludeDiff * item.orderQnt, 2) : 0;
                    });

                    Array.prototype.push.apply($scope.sdata, data.data.data);
                    $scope.isTriggered = false;

                    $scope.totalPage = Math.ceil(data.totalRows / clientService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    //$scope.total = data.data.totalItem;
                    //$scope.confirmed = data.data.totalConfirmedItem;
                    //$scope.pending = data.data.totalPendingItem;
                    //$scope.waitEurofar = data.data.totalWaitForEurofar;
                    //$scope.waitFactory = data.data.totalWaitForFactory;

                    //
                    // get sub total
                    //
                    $scope.totalRow = data.data.totalRow;
                    $scope.subTotalRow = data.data.subTotalRow;

                    //
                    //get breakdown
                    //
                    Array.prototype.push.apply($scope.clientBreakdownOptions, data.data.breakdownCategoryOptionDTOs);
                    Array.prototype.push.apply($scope.clientSeasonSpecPercents, data.data.breakdownSeasonSpecPercentDTOs);
                    Array.prototype.push.apply($scope.clientBreakdownManagementFees, data.data.breakdownManagementFeeDTOs);

                    $scope.$apply();

                    $('#content').show();
                },
                function (error) {
                    $scope.isTriggered = false;
                }
            );
        },

        reload: function () {
            $scope.data = [];
            $scope.pageIndex = 1;
            $scope.totalPage = 0;
            $scope.totalRows = 0;
            clientService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.getBreakdownData();
        },

        getEstimatedAdditionalCost: function () {
            debugger
            clientService.getEstimatedAdditionalCost(
                context.id,
                function (data) {
                    $scope.estimatedAdditionalCost = data.data;
                    
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.$apply();
                }
            );
        },
        
        /* For Breakdown Zone --------Start-------*/
        getCostPrice: function (breakdownCategoryID, dataSearch) {
            //properyty
            var costPrice = {
                avfPrice: 0,
                avtPrice: 0,
                avfPriceUSD: parseFloat(0),
                avtPriceUSD: parseFloat(0)
            };
            var selectedBreakdownOption = [];
            var breakdownCategoryOptions = [];
            var companyID = 0;
            //get breakdown option
            if ($scope.clientBreakdownOptions !== null && $scope.clientBreakdownOptions !== undefined) {
                breakdownCategoryOptions = $scope.clientBreakdownOptions.filter(o => o.breakdownCategoryID === breakdownCategoryID && o.modelID === dataSearch.modelID);
            }
            //get cost
            switch (breakdownCategoryID) {
                case 1: //MATERIAL COST
                    //get option
                    var materialID = dataSearch.materialID;
                    var materialTypeID = dataSearch.materialTypeID;
                    var materialColorID = dataSearch.materialColorID;
                    //get AVF Price
                    companyID = 1;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.materialID === materialID && o.materialTypeID === materialTypeID && o.materialColorID === materialColorID);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                        costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.exchange, 2);
                    }
                    //get AVT Price
                    companyID = 3;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.materialID === materialID && o.materialTypeID === materialTypeID && o.materialColorID === materialColorID);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                        costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.exchange, 2);
                    }
                    return costPrice;
                case 2: //FRAME COST
                    //get option
                    var frameMaterialID = dataSearch.frameMaterialID;
                    var frameMaterialColorID = dataSearch.frameMaterialColorID;
                    //get AVF price
                    companyID = 1;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.frameMaterialID === frameMaterialID && o.frameMaterialColorID === frameMaterialColorID);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                        costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.exchange, 2);
                    }
                    //get AVT price
                    companyID = 3;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.frameMaterialID === frameMaterialID && o.frameMaterialColorID === frameMaterialColorID);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                        costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.exchange, 2);
                    }
                    return costPrice;
                case 3: //SUB MATERIAL COST
                    //get option
                    var subMaterialID = dataSearch.subMaterialID;
                    var subMaterialColorID = dataSearch.subMaterialColorID;
                    //get AVF price
                    companyID = 1;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.subMaterialID === subMaterialID && o.subMaterialColorID === subMaterialColorID);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                        costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.exchange, 2);
                    }
                    //get AVT price
                    companyID = 3;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.subMaterialID === subMaterialID && o.subMaterialColorID === subMaterialColorID);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                        costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.exchange, 2);
                    }
                    return costPrice;
                case 4: //CUSHION COST
                    //get option
                    var cushionColorID = dataSearch.cushionColorID;
                    var backCushionID = dataSearch.backCushionID;
                    var seatCushionID = dataSearch.seatCushionID;
                    //get AVF price
                    companyID = 1;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.cushionColorID === cushionColorID && o.backCushionID === backCushionID && o.seatCushionID === seatCushionID);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                        costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.exchange, 2);
                    }
                    //get AVT price
                    companyID = 3;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.cushionColorID === cushionColorID && o.backCushionID === backCushionID && o.seatCushionID === seatCushionID);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                        costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.exchange, 2);
                    }
                    return costPrice;

                case 5: //PACKING COST
                    //get option
                    var packagingMethodID = dataSearch.packagingMethodID;
                    //get AVF price
                    companyID = 1;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.packagingMethodID === packagingMethodID);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                        costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.exchange, 2);
                    }
                    //get AVT price
                    companyID = 3;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.packagingMethodID === packagingMethodID);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                        costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.exchange, 2);
                    }
                    return costPrice;

                case 8: //FSC COST
                    var fscTypeID = dataSearch.fSCTypeID;
                    var fscPercentID = dataSearch.fSCPercentID;

                    //get AVF price
                    companyID = 1;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.fscTypeID === fscTypeID && o.fscPercentID === fscPercentID);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                        costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.exchange, 2);
                    }
                    //get AVT price
                    companyID = 3;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.fscTypeID === fscTypeID && o.fscPercentID === fscPercentID);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                        costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.exchange, 2);
                    }
                    return costPrice;
                case 9: //SPECIAL REQUIREMENT COST    
                    //get Option
                    // var offerLineID = dataSearch.offerLineID;
                    //get AVF price
                    companyID = 1;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID /*&& o.offerLineID === offerLineID*/);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                        costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.exchange, 2);
                    }
                    //get AVT price
                    companyID = 3;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID /*&& o.offerLineID === offerLineID*/);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                        costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.exchange, 2);
                    }
                    return costPrice;

                case 6: case 7: case 10: //HARDWARE COST,OTHER MATERIAL COST,MANAGEMENT FEE
                    //no Option to Get
                    //get AVF price
                    companyID = 1;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                        costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.exchange, 2);
                    }
                    //get AVT price
                    companyID = 3;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                        costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.exchange, 2);
                    }
                    return costPrice;

                default:
                    return costPrice;
            }
        },

        getManagementFeePercent: function (data) {
            var managementFeePercent = {
                avtManagement: 0,
                avfManagement: 0
            };
            var breakdownCategoryOptions = [];
            if ($scope.clientBreakdownManagementFees !== null && $scope.clientBreakdownManagementFees !== undefined && $scope.clientBreakdownManagementFees.length > 0) {

                if ($scope.clientBreakdownOptions !== null && $scope.clientBreakdownOptions !== undefined) {
                    breakdownCategoryOptions = $scope.clientBreakdownOptions.filter(o => o.modelID === data.modelID);
                }
                if (breakdownCategoryOptions !== null && breakdownCategoryOptions !== undefined && breakdownCategoryOptions.length > 0) {
                    for (var i = 0; i < $scope.clientBreakdownManagementFees.length; i++) {
                        var itemManagementFee = $scope.clientBreakdownManagementFees[i];
                        for (var j = 0; j < breakdownCategoryOptions.length; j++) {
                            var itembreakdownCategoryOption = breakdownCategoryOptions[i];
                            if (itemManagementFee.breakdownID === itembreakdownCategoryOption.breakdownID && itemManagementFee.companyID === 1) {
                                managementFeePercent.avfManagement = itemManagementFee.quantityPercent;
                                break;
                            }
                            if (itemManagementFee.breakdownID === itembreakdownCategoryOption.breakdownID && itemManagementFee.companyID === 3) {
                                managementFeePercent.avtManagement = itemManagementFee.quantityPercent;
                                break;
                            }
                        }
                    }
                }
            }
            return managementFeePercent;
        },

        getseasonSpecPercent: function (data) {
            var seasonSpecPercent = 0;
            //var breakdownCategoryOptions = [];

            if ($scope.clientSeasonSpecPercents !== null && $scope.clientSeasonSpecPercents !== undefined && $scope.clientSeasonSpecPercents.length > 0) {
                if ($scope.clientBreakdownOptions !== null && $scope.clientBreakdownOptions !== undefined) {
                    var breakdownCategoryOptions = $scope.clientBreakdownOptions.filter(o => o.modelID === data.modelID);
                }
                if (breakdownCategoryOptions !== null && breakdownCategoryOptions !== undefined) {
                    for (var i = 0; i < $scope.clientSeasonSpecPercents.length; i++) {
                        var itemseasonSpecPercent = $scope.clientSeasonSpecPercents[i];
                        for (var j = 0; j < breakdownCategoryOptions.length; j++) {
                            var itembreakdownCategoryOption = breakdownCategoryOptions[j];
                            if (itemseasonSpecPercent.breakdownID === itembreakdownCategoryOption.breakdownID) {
                                seasonSpecPercent = itemseasonSpecPercent.seasonSpecPercent;
                                break;
                            }
                        }
                    }
                }
            }
            return seasonSpecPercent;
        },

        getShippingFee: function (data) {
            var shippingFee = {
                shippingFeeAVFVND: parseFloat(0),
                shippingFeeAVFUSD: parseFloat(0),
                shippingFeeAVTVND: parseFloat(0),
                shippingFeeAVTUSD: parseFloat(0)
            };
            var companyID = 0;
            if ($scope.clientBreakdownOptions !== null && $scope.clientBreakdownOptions !== undefined) {
                var breakdownCategoryOptions = $scope.clientBreakdownOptions.filter(o => o.modelID === data.modelID && o.breakdownCategoryID === 5);
            }
            if (breakdownCategoryOptions !== null && breakdownCategoryOptions !== undefined) {
                for (var i = 0; i < breakdownCategoryOptions.length; i++) {
                    var itemBreakdownCategoryOptions = breakdownCategoryOptions[i];
                    if (itemBreakdownCategoryOptions.modelID === data.modelID && data.deliveryTerm === "FOB") {

                        //AVF
                        companyID = 1;
                        if (data.exportCost40HC > 0 && itemBreakdownCategoryOptions.loadAbilityAVF > 0 && itemBreakdownCategoryOptions.companyID === 1) {
                            shippingFee.shippingFeeAVFVND = jsHelper.round(parseFloat(data.exportCost40HC) / parseFloat(itemBreakdownCategoryOptions.loadAbilityAVF), 2);
                        }
                        shippingFee.shippingFeeAVFUSD = jsHelper.round(parseFloat(shippingFee.shippingFeeAVFVND) / $scope.exchange, 2);

                        companyID = 3;
                        if (data.exportCost40HC > 0 && itemBreakdownCategoryOptions.loadAbilityAVT > 0 && itemBreakdownCategoryOptions.companyID === 3) {
                            shippingFee.shippingFeeAVTVND = jsHelper.round(parseFloat(data.exportCost40HC) / parseFloat(itemBreakdownCategoryOptions.loadAbilityAVT), 2);
                        }
                        shippingFee.shippingFeeAVTUSD = jsHelper.round(parseFloat(shippingFee.shippingFeeAVTVND) / $scope.exchange, 2);
                    }
                }
            }
            return shippingFee;
        },

        /* For Breakdown Zone -----------End Zone------------*/

        getComplaintStatus: function (clientComplaintStatusID) {
            var complaintStatusNM = '';
            angular.forEach($scope.clientComplaintStatuses, function (value) {
                if (value.complaintStatusID === clientComplaintStatusID) {
                    complaintStatusNM = value.complaintStatusNM;
                }
            });
            return complaintStatusNM;
        },
        getComplaintType: function (clientComplaintTypeID) {
            var complaintTypeNM = '';
            angular.forEach($scope.clientComplaintTypes, function (value) {
                if (value.complaintTypeID === clientComplaintTypeID) {
                    complaintTypeNM = value.complaintTypeNM;
                }
            });
            return complaintTypeNM;
        },
        
        getPlaningPurchasingPrice: function (item) {
            $scope.currentOfferLineItem = null;
            //if (!item.isPlaningPurchasingPriceLoaded) {
                clientService.getPlaningPurchasingPrice(
                    item.offerLineID,
                    function (data) {
                        if (data.message.type === 0) {
                            item.isPlaningPurchasingPriceLoaded = true;
                            item.planingPurchasingPriceDTOs = data.data;
                            $scope.currentOfferLineItem = angular.copy(item);
                            $scope.SelectedPriceID = $scope.currentOfferLineItem.avtPurchasingPriceStatusID;
                            $scope.$apply();
                            $('#frmAvailablePurchasingPrice').modal('show');
                        }
                    },
                    function (error) {
                        console.log(error);
                    }
                );
            //}
            //else {
            //    $scope.currentOfferLineItem = angular.copy(item);
            //    $('#frmAvailablePurchasingPrice').modal('show');
            //}
        },

        isSelected: function (id) {
            if (id === $scope.SelectedPriceID) {
                return true;
            }
            else {
                return false;
            }
        },

        orderPercentMargin: function () {
            if ($scope.percentMarginSortClick === -1) {
                $scope.percentMarginSortClick = 0;
            }
            else if ($scope.percentMarginSortClick === 0) {
                $scope.percentMarginSortClick = 1;
            }
            else if ($scope.percentMarginSortClick === 1) {
                $scope.percentMarginSortClick = 0;
            }
            angular.forEach($scope.offersmargin.offerMarginDTOs, function (item) {
                if (item.offerMarginDetail !== null) {
                    if ($scope.percentMarginSortClick === 0) {
                        item.offerMarginDetail = $filter('orderBy')(item.offerMarginDetail, '+percentMargin');
                    }
                    else if ($scope.percentMarginSortClick === 1) {
                        item.offerMarginDetail = $filter('orderBy')(item.offerMarginDetail, '-percentMargin');
                    }
                }
            });
        }
    };

    $scope.newID = -1;
    $scope.method = {
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },
        getPercentValue: function (season) {
            var result = 0;
            angular.forEach($scope.chartCommercialInvoices, function (item) {
                if (item.clientNM === 'Delta6 Percent' && item.season === season) {
                    result = parseFloat(item.totalEURAmount);
                }
            });
            return result;
        },
        drawShippingPerformanceChart: function () {
         
            //
            // chartShippingPerformance
            //
            var overDueTypeID = [];
            var seriData = [];
            var categories = [
                  'Ontime'
                , 'Less than 0-1 week'
                , 'Less than 1-2 week'
                , 'Less than 2-3 week'
                , 'Less than 3-4 week'
                , 'Less than 4-5 week'
                , 'Less than 5-6 week'
                , 'Less than 6 - 7 week'
                , 'More than 7 week'
            ];

            var totalContOfSeason = parseFloat(0);
            angular.forEach(categories, function (cItem) {
                var index = categories.indexOf(cItem);
                var contItem = $scope.shippingPerformanceData.shippingPerformanceChart2DTOs.filter(o => o.overDueTypeID === index);
                var totalContainer = parseFloat(contItem.length > 0 ? contItem[0].totalContainer : 0);
                totalContOfSeason += totalContainer;
                seriData.push(totalContainer);
            });

            //add cont for all season to series
            categories.push('Shipped all season');
            seriData.push(totalContOfSeason);
           
            //add tobe shipped to series
            categories.push('Tobe shipped (based on load ability 40HC)');
            seriData.push($scope.shippingPerformanceData.totalToBeShipped);

            jQuery('#chartShippingPerformance').highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'SHIPPING PERFORMANCE'
                },
                xAxis: {
                    categories: categories
                },
                yAxis: {
                    title: {
                        text: 'Number of container(s)'
                    },
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y} container</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                series: [{
                    name: 'Container',
                    data: seriData
                }]
            });
        },
        getRatingClass: function (rating) {
            switch (rating) {
                case 'GOOD':
                    return 'bg-color-green';
                    break;

                case 'OK':
                    return 'bg-color-purple';
                    break;

                case 'NOT OK':
                    return 'bg-color-red';
                    break;
            }
        },
        getPrevSeasonOfferMargin: function (season) {
            if (season) {
                return jsHelper.getPrevSeason(season, false);
            }
            else
                return null;
        },

        getPrevSeason: function () {
            if ($scope.filters.Season)
                return jsHelper.getPrevSeason($scope.filters.Season, true);
            else
                return null;
        },
        get2ndPrevSeason: function () {
            if ($scope.filters.Season)
                return jsHelper.getPrevSeason(jsHelper.getPrevSeason($scope.filters.Season), true);
            else
                return null;
        },
        get3rdPrevSeason: function () {
            if ($scope.filters.Season)
                return jsHelper.getPrevSeason(jsHelper.getPrevSeason(jsHelper.getPrevSeason($scope.filters.Season)), true);
            else
                return null;
        },

        /* Breakdown Zone */
        getTotalCostPriceAVT_USD: function (data) {
            var totalCostPrice = {
                avfPrice: parseFloat(0),
                avtPrice: parseFloat(0),
                avfManagementFee: parseFloat(0),
                avtManagementFee: parseFloat(0),
                avtPriceUSD: parseFloat(0),
                avfPriceUSD: parseFloat(0)
            };
            //get breakdown category
            var breakdownCategories = [];
            if ($scope.breakdownCategorys !== null && $scope.breakdownCategorys !== undefined) {
                breakdownCategories = angular.copy($scope.breakdownCategorys);
            }
            ////cal total price
            angular.forEach(breakdownCategories, function (item) {
                var costPrice = $scope.event.getCostPrice(item.breakdownCategoryID, data);
                totalCostPrice.avtPriceUSD += jsHelper.round(parseFloat(costPrice.avtPriceUSD), 2);
            });
            //management fee amount base on percent
            var managementFeePercent = $scope.event.getManagementFeePercent(data);
            //totalCostPrice.avtManagementFee = totalCostPrice.avtPrice * managementFeePercent / 100;

            //total cost price after management fee
            totalCostPrice.avtPriceUSD = jsHelper.round(totalCostPrice.avtPriceUSD * (1 + managementFeePercent.avtManagement / 100), 2);

            //total cost amount base on SeasonSpecPercent
            var seasonSpecPercent = $scope.event.getseasonSpecPercent(data);
            totalCostPrice.avtPriceUSD = jsHelper.round(totalCostPrice.avtPriceUSD * (1 + seasonSpecPercent / 100), 2);

            var shippingFee = $scope.event.getShippingFee(data);
            totalCostPrice.avtPriceUSD = totalCostPrice.avtPriceUSD + shippingFee.shippingFeeAVTUSD;

            return totalCostPrice.avtPriceUSD;
        },
        getTotalCostPriceAVF_USD: function (data) {
            var totalCostPrice = {
                avfPrice: parseFloat(0),
                avtPrice: parseFloat(0),
                avfManagementFee: parseFloat(0),
                avtManagementFee: parseFloat(0),
                avtPriceUSD: parseFloat(0),
                avfPriceUSD: parseFloat(0)
            };
            //get breakdown category
            var breakdownCategories = [];
            if ($scope.breakdownCategorys !== null && $scope.breakdownCategorys !== undefined) {
                breakdownCategories = angular.copy($scope.breakdownCategorys);
            }
            ////cal total price
            angular.forEach(breakdownCategories, function (item) {
                var costPrice = $scope.event.getCostPrice(item.breakdownCategoryID, data);
                totalCostPrice.avfPriceUSD += jsHelper.round(parseFloat(costPrice.avfPriceUSD), 2);
            });

            //////management fee amount base on percent
            var managementFeePercent = $scope.event.getManagementFeePercent(data);
            //totalCostPrice.avfManagementFee = totalCostPrice.avfPrice * managementFeePercent / 100;

            //total cost price after management fee
            totalCostPrice.avfPriceUSD = jsHelper.round(totalCostPrice.avfPriceUSD * (1 + managementFeePercent.avfManagement / 100), 2);

            //total cost amount base on SeasonSpecPercent
            var seasonSpecPercent = $scope.event.getseasonSpecPercent(data);
            totalCostPrice.avfPriceUSD = jsHelper.round(totalCostPrice.avfPriceUSD * (1 + seasonSpecPercent / 100), 2);

            var shippingFee = $scope.event.getShippingFee(data);
            totalCostPrice.avfPriceUSD = totalCostPrice.avfPriceUSD + shippingFee.shippingFeeAVFUSD;

            return totalCostPrice.avfPriceUSD;
        },

        getShippedPrice_EUR: function (totalShippedUSD, exchangeRate) {
            if (totalShippedUSD != null) {
                return (parseFloat(totalShippedUSD) / parseFloat(exchangeRate)).toFixed(2);
            }
            return parseFloat(0).toFixed(2);
        },
        getTotalShippedPrice_USD: function () {
            var totalPrice = 0;

            if ($scope.itemData != null) {
                angular.forEach($scope.itemData.itemDTOs, function (item) {
                    if (item != null && item.totalShippedPrice != null) {
                        totalPrice = parseFloat(totalPrice) + parseFloat(item.totalShippedPrice);
                    }
                });
            }

            return parseFloat(totalPrice).toFixed(2);
        },
        getTotalShippedPrice_EUR: function (exchangeRate) {
            var totalPrice = 0;

            if ($scope.itemData != null) {
                angular.forEach($scope.itemData.itemDTOs, function (item) {
                    if (item != null && item.totalShippedPrice != null) {
                        var shippedPrice_EUR = $scope.method.getShippedPrice_EUR(item.totalShippedPrice, exchangeRate);
                        totalPrice = parseFloat(totalPrice) + parseFloat(shippedPrice_EUR);
                    }
                });
            }

            return parseFloat(totalPrice).toFixed(2);
        },

        getShippedPrice_EUR_Model: function (totalShippedUSD, exchangeRate) {
            if (totalShippedUSD != null) {
                return (parseFloat(totalShippedUSD) / parseFloat(exchangeRate)).toFixed(2);
            }
            return parseFloat(0).toFixed(2);
        },
        getTotalShippedPrice_USD_Model: function () {
            var totalPrice = 0;

            if ($scope.modelData != null) {
                angular.forEach($scope.modelData.modelDTOs, function (item) {
                    if (item != null && item.totalShippedPrice != null) {
                        totalPrice = parseFloat(totalPrice) + parseFloat(item.totalShippedPrice);
                    }
                });
            }

            return parseFloat(totalPrice).toFixed(2);
        },
        getTotalShippedPrice_EUR_Model: function (exchangeRate) {
            var totalPrice = 0;

            if ($scope.modelData != null) {
                angular.forEach($scope.modelData.modelDTOs, function (item) {
                    if (item != null && item.totalShippedPrice != null) {
                        var shippedPrice_EUR = $scope.method.getShippedPrice_EUR(item.totalShippedPrice, exchangeRate);
                        totalPrice = parseFloat(totalPrice) + parseFloat(shippedPrice_EUR);
                    }
                });
            }

            return parseFloat(totalPrice).toFixed(2);
        },

        getDefaultEmail: function (clientContacts) {
            var defaultEmail = "";
            angular.forEach(clientContacts, function (item) {
                if (item.isDefault) {
                    defaultEmail = item.email;
                }
            });
            return defaultEmail;
        },
    };

    function saveGIC() {        
        //if ($scope.GICItem.clientContactHistoryID === null || $scope.GICItem.clientContactHistoryID === "") {
        //    var today = new Date;
        //    var minutes = today.getMinutes();
        //    var hours = today.getHours();
        //    var day = today.getDate();
        //    var month = today.getMonth() + 1;
        //    var year = today.getFullYear();
        //    var temp = 0;
        //    var todayFormated = day + '/' + month + '/' + year + '  ' + hours + ' : ' + minutes;
        //    if ($scope.GICItem.communicationContent !== null && $scope.GICItem.communicationContent !== "") {
        //        var tempItem = {
        //            clientContactHistoryID: $scope.method.getNewID(),
        //            contactDate: todayFormated,
        //            communicationContent: $scope.GICItem.communicationContent,
        //            clientID: $scope.data.clientID
        //        };
        //        for (var i = 0; i < $scope.data.clientContactHistories.length; i++) {
        //            var item = $scope.data.clientContactHistories[i];
        //            if (item.communicationContent === $scope.GICItem.communicationContent) {
        //                temp = 1;
        //            }

        //        }
        //        if (temp === 0) {
        //            $scope.data.clientContactHistories.push(tempItem);
        //        }
        //    }
        //}
        //else {
        //    if ($scope.data.clientContactHistories.length > 0) {
        //        angular.forEach($scope.data.clientContactHistories, function (tempItem) {
        //            if (tempItem.clientContactHistoryID === $scope.GICItem.clientContactHistoryID && tempItem.communicationContent === $scope.GICItem.communicationContent) {
        //                //tempItem.communicationContent = $scope.GICItem.communicationContent;
        //            }
        //            else if (tempItem.clientContactHistoryID === $scope.GICItem.clientContactHistoryID && tempItem.communicationContent !== $scope.GICItem.communicationContent) {
        //                // do notthing
        //                tempItem.communicationContent = $scope.GICItem.communicationContent;
        //            }
        //        });
        //    }
        //}
        if ($scope.GICItem.communicationContent !== $scope.GICItem.lastCommunicationContent) {
            clientService.saveGIC(
                context.id,
                [{
                    clientContactHistoryID: -1,
                    hasChanged: true,
                    communicationContent: $scope.GICItem.communicationContent
                }],
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = clientService.viewUrl;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    };

    // define function load.
    function load() {
        clientService.getClientOverview(
            context.id,
            function (data) {
                $scope.data = data.data;
                //$scope.offers = data.data.clientOffers;
                $scope.chartCommercialInvoices = data.data.chartCommercialInvoices;
                $scope.clientBusinessCardDTO = data.data.clientBusinessCardDTO;
                $scope.clientComplaintStatuses = data.data.clientComplaintStatuses;
                $scope.clientComplaintTypes = data.data.clientComplaintTypes;
                $scope.clientAdditionalConditionOverviewDTO = data.data.clientAdditionalConditionOverviewDTO;
                for (var i = 0; i < $scope.data.clientAdditionalConditionDTO.length; i++) {
                    var item = $scope.data.clientAdditionalConditionDTO[i];
                    if (item.isSelected === true) {
                        $scope.clientAdditionalConditionDTOShow.push(item);
                    }
                }
                var newdate = null;
                $scope.newComment = {
                    clientContactHistoryID: null,
                    communicationContent: null
                };               
                if ($scope.data.clientContactHistories && $scope.data.clientContactHistories.length > 0) {
                    $scope.GICItem.communicationContent = $scope.data.clientContactHistories[0].communicationContent;
                    $scope.GICItem.lastCommunicationContent = $scope.data.clientContactHistories[0].communicationContent;
                }
               
                //$scope.shippingPerformanceData = data.data.shippingPerformanceData;
                //$scope.itemData = data.data.itemData;
                //$scope.priceComparisonData = data.data.priceComparisonData;
                $scope.$apply();
                $('.page-title i').hide();
                $('#page-title').html('<strong>' + $scope.data.clientUD + '</strong> ' + $scope.data.clientNM + ' | Acc: ' + $scope.data.accountManagerName + ' | ' + ($scope.data.website ? '<span><i class="fa fa-globe"></i> <a href="http://' + $scope.data.website + '" target="_blank">' + $scope.data.website + '</a></span> ' : '') + ($scope.data.website2 ? '<span><i class="fa fa-globe"></i> <a href="http://' + $scope.data.website2 + '" target="_blank">' + $scope.data.website2 + '</a></span> ' : ''));


                jQuery('#content').show();

                // Call to get under with season
                //$scope.event.getClientOverview();
                //$scope.method.drawShippingPerformanceChart();

                /*
                * chartCommercialInvoice
                */
                var currentSeason = jsHelper.getCurrentSeason();
                //create data

                var chartDataSorted = $scope.chartCommercialInvoices.sort(function (a, b) { return a.season > b.season ? 1 : -1; });

                //get distinct season
                var seasons = [];
                angular.forEach(chartDataSorted, function (item) {
                    if (seasons.indexOf(item.season) < 0) {
                        seasons.push(item.season);
                    }
                });

                //get distinct client
                var clients = [];
                //push Expected first
                angular.forEach(chartDataSorted, function (item) {
                    if (clients.indexOf(item.clientNM) < 0 && item.clientNM === 'Expected') {
                        clients.push(item.clientNM);
                    }
                });
                //push CI second
                angular.forEach(chartDataSorted, function (item) {
                    if (clients.indexOf(item.clientNM) < 0 && item.clientNM === 'CI of previous season or PI Confirmed of current season') {
                        clients.push(item.clientNM);
                    }
                });
                //push other remain
                angular.forEach(chartDataSorted, function (item) {
                    if (clients.indexOf(item.clientNM) < 0 && item.clientNM !== 'Delta6 Percent' && item.clientNM !== 'AVG Delta6 Percent') {
                        clients.push(item.clientNM);
                    }
                });

                //buil series data for column chart
                var series = [];
                angular.forEach(clients, function (cItem) {
                    var serie = {
                        name: cItem,
                        data: [],
                        color: ''
                    };
                    angular.forEach(seasons, function (sItem) {
                        var x = chartDataSorted.filter(function (o) { return o.season === sItem && o.clientNM === cItem;});
                        var amount = 0;
                        if (x.length > 0) {
                            amount = x[0].totalEURAmount;
                        }
                        else {
                            amount = 0;
                        }
                        serie.data.push(amount);

                        //assign color
                        switch (cItem) {
                            case 'CI of previous season or PI Confirmed of current season':
                                serie.color = jsHelper.getDefaultColor('ci');
                                break;

                            case 'Expected':
                                serie.color = jsHelper.getDefaultColor('expectation');
                                break;

                            case 'Delta6 Amount':
                                serie.color = jsHelper.getDefaultColor('delta');
                                break;                            
                        }
                    });
                    series.push(serie);
                });

                //bind to chart line delta5 percent for this client 
                var percentMarginData = {
                    name: 'Delta6 Percent',
                    data: [],
                    color: jsHelper.getDefaultColor('%delta'),
                    type: 'spline',
                    yAxis: 1,
                    tooltip: {
                        formatter: function () {
                            return '<b>' + this.series.name + '(' + this.x + ')' + '</b>:'
                                + $filter('number')(this.y, 2) + '%';
                        },
                        useHTML: true
                    }
                };
                angular.forEach(seasons, function (sItem) {
                    var x = chartDataSorted.filter(function (o) { return o.season === sItem && o.clientNM === 'Delta6 Percent';});
                    var amount = 0;
                    if (x.length > 0) {
                        amount = x[0].totalEURAmount;
                        //if (x.season === '2014/2015') {
                        //    amount = null;
                        //}
                    }
                    else {
                        amount = null;
                    }                    
                    percentMarginData.data.push(amount);
                });
                series.push(percentMarginData);

                //bind to chart line s for every season
                var delta5PercentBySeason = {
                    name: 'AVG Delta6 Percent',
                    data: [],
                    color: jsHelper.getDefaultColor('%avgdelta'),
                    type: 'spline',
                    yAxis: 1,
                    tooltip: {
                        formatter: function () {
                            return '<b>' + this.series.name + '(' + this.x + ')' + '</b>:'
                                + $filter('number')(this.y, 2) + '%';
                        },
                        useHTML: true
                    }
                };
                angular.forEach(seasons, function (sItem) {
                    var x = chartDataSorted.filter(function (o) { return o.season === sItem && o.clientNM === 'AVG Delta6 Percent'; });
                    var amount = 0;
                    if (x.length > 0) {
                        amount = x[0].totalEURAmount;
                    }
                    else {
                        amount = 0;
                    }
                    delta5PercentBySeason.data.push(amount);
                });
                series.push(delta5PercentBySeason);

                jQuery('#chartCommercialInvoice').highcharts({
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: 'COMMERCIAL INVOICE & DELTA6'
                    },
                    xAxis: {
                        categories: seasons
                    },
                    yAxis: [
                        {
                            title: {
                                text: 'Amount In EUR'
                            }
                        },
                        {
                            labels: {
                                format: '{value}%'
                            },
                            title: {
                                text: '% Delta 6'
                            },
                            opposite: true
                        }
                    ],
                    tooltip: {
                        formatter: function () {
                            if (this.series.name === 'Delta6 Percent' || this.series.name === 'AVG Delta6 Percent') {
                                return '<b>' + this.series.name + ' (' + this.x + ')</b>: ' + $filter('number')(this.y, 2) + '%';
                            }
                            else {
                                return '<b>' + this.x + ' - '
                                    + (this.series.name !== 'CI of previous season or PI Confirmed of current season' ? this.series.name : (this.x === currentSeason ? 'Proforma Invoice Confirmed' : 'Commercial Invoice'))
                                    + '</b>:'
                                    + ' &euro;'
                                    + $filter('currency')(this.y, '', 2)
                                    + ' ' + ((this.series.name === 'Delta6 Percent' && scope.method.getPercentValue(this.x)) ? '(' + scope.method.getPercentValue(this.x) + '%)' : '');
                            }
                        },
                        useHTML: true
                    },
                    series: series
                });

                // open tab base on param autoTab
                if (context.autoTab) {
                    $('a[href="#' + context.autoTab + '"]').tab('show');
                }
            },
            function (error) {
                jsHelper.showMessage('warning', error);

                $scope.data = null;
                $scope.$apply();
            }
        );
    };

    // define function skypeChat.
    function skypeChat(value) {
        window.location = 'skype:' + value + '?chat';
    };

    // define function openCall.
    function openCall(value) {
        window.location = 'tel:' + value;
    };

    function getAgenda(id) {
        window.location = clientService.baseUrl + 'event/view/' + id;
    };

    // define function getClientOverview.
    function getClientOverview() {
        clientService.getClientOverview2(
            context.id,
            $scope.filters.season,
            function (data) {
                $scope.offers = data.data.offers;
                //$scope.plcs = data.data.plCs;
                $scope.commercialInvoices = data.data.commercialInvoices;
                $scope.sampleOrders = data.data.sampleOrders;
                $scope.clientComplaints = data.data.clientComplaints;
                $scope.saleOrders = data.data.saleOrders;

                $scope.$apply();
            },
            function (error) {
                jsHelper.showMessage('warning', error);
                $scope.$apply();
            });
    };

    function getLinkedData(param) {
        switch (param) {
            case '#cistab':
                if ($scope.loadingState.cis) return;
                debugger;
                clientService.getCISData(
                    context.id,
                    function (data) {
                        $scope.shippingPerformanceData = data.data.shippingPerformanceData;
                        $scope.itemData = data.data.itemData;
                        // Set value for model data in Product Summary
                        if ($scope.itemData.season === null || $scope.itemData.season === '') {
                            $scope.modelData.season = jsHelper.getCurrentSeason();
                        } else {
                            $scope.modelData.season = $scope.itemData.season;
                        }
                        $scope.modelData.modelDTOs = $scope.itemData.modelDTOs;
                        $scope.modelData.exRate = $scope.itemData.exRate;

                        $scope.priceComparisonData = data.data.priceComparisonData;
                        $scope.itemDataSummary = {
                            totalOrderQnt: 0,
                            totalOrderQnt40HC: 0.00,
                            totalShippedQnt: 0,
                            totalShippedQnt40HC: 0.00,
                            totalToBeShippedQnt: 0,
                            totalToBeShippedQnt40HC: 0.00,
                            totalAmountUSD: 0.00,
                            totalAmountEUR: 0.00,
                            totalAmountConvertToEUR: 0.00
                        };
                        angular.forEach($scope.itemData.itemDTOs, function (item) {
                            if (item.unitPriceUSD || item.unitPriceEUR) {
                                $scope.itemDataSummary.totalOrderQnt += item.totalOrderQnt;
                                $scope.itemDataSummary.totalOrderQnt40HC += item.totalOrderQnt40HC;
                                $scope.itemDataSummary.totalShippedQnt += item.totalShippedQnt;
                                $scope.itemDataSummary.totalShippedQnt40HC += item.totalShippedQnt40HC;
                                $scope.itemDataSummary.totalToBeShippedQnt += item.totalToBeShippedQnt;
                                $scope.itemDataSummary.totalToBeShippedQnt40HC += item.totalToBeShippedQnt40HC;
                                $scope.itemDataSummary.totalAmountUSD += item.totalAmountUSD;
                                $scope.itemDataSummary.totalAmountEUR += item.totalAmountEUR;
                                $scope.itemDataSummary.totalAmountConvertToEUR += item.totalAmountConvertToEUR;
                            }
                        });

                        $scope.modelDataSummary = {
                            totalOrdered: 0,
                            totalOrderQnt40HC: 0.00,
                            totalShipped: 0,
                            totalShippedQnt40HC: 0.00,
                            totalToBeShippedQnt: 0,
                            totalToBeShippedQnt40HC: 0.00,
                            totalAmountUSD: 0.00,
                            totalAmountEUR: 0.00,
                            totalConvertToEUR: 0.00
                        };
                        angular.forEach($scope.modelData.modelDTOs, function (item) {
                            if (item.totalUnitPriceUSD || item.totalUnitPriceEUR) {
                                $scope.modelDataSummary.totalOrdered += item.totalOrdered;
                                $scope.modelDataSummary.totalOrderQnt40HC += item.totalOrderQnt40HC;
                                $scope.modelDataSummary.totalShipped += item.totalShipped;
                                $scope.modelDataSummary.totalShippedQnt40HC += item.totalShippedQnt40HC;
                                $scope.modelDataSummary.totalToBeShippedQnt += item.totalOrdered - item.totalShipped;
                                $scope.modelDataSummary.totalToBeShippedQnt40HC += item.totalToBeShippedQnt40HC;
                                $scope.modelDataSummary.totalAmountUSD += item.totalAmountUSD;
                                $scope.modelDataSummary.totalAmountEUR += item.totalAmountEUR;
                                $scope.modelDataSummary.totalConvertToEUR += item.totalConvertToEUR;
                            }
                        });
                        $scope.$apply();

                        $scope.method.drawShippingPerformanceChart();
                        $scope.loadingState.cis = true;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.$apply();
                    }
                );

                break;

            case '#offertab':
                if ($scope.loadingState.offer) return;

                clientService.getOfferData(
                    context.id,
                    function (data) {
                        $scope.offers = data.data.offers;
                        $scope.loadingState.offer = true;

                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.$apply();
                    }
                );

                break;

            case '#offermargintab':
                if ($scope.loadingState.offermargin) return;
                $scope.event.getOfferMarginData();                
                $scope.loadingState.offermargin = true;

                break;

            case '#pitab':
                if ($scope.loadingState.pi) return;

                clientService.getPIData(
                    context.id,
                    function (data) {
                        $scope.saleOrders = data.data.saleOrders;
                        $scope.loadingState.pi = true;

                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.$apply();
                    }
                );
                break;

            case '#plctab':
                if ($scope.loadingState.plc) return;

                clientService.getPLCData(
                    context.id,
                    function (data) {
                        $scope.plcs = data.data.plCs;
                        $scope.plcSaleOrders = data.data.plcSaleOrders;
                        $scope.plcLoadingPLans = data.data.plcLoadingPLans;
                        $scope.loadingState.plc = true;

                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.$apply();
                    }
                );
                break;

            case '#loadingplantab':
                if ($scope.loadingState.loadingplan) return;

                clientService.getLoadingPlan(
                    context.id,
                    function (data) {
                        $scope.loadingPlanDTOs = data.data;
                        $scope.loadingState.loadingplan = true;

                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.$apply();
                    }
                );
                break;

            case '#citab':
                if ($scope.loadingState.ci) return;

                clientService.getCIData(
                  context.id,
                  function (data) {
                      $scope.commercialInvoices = data.data.commercialInvoices;
                      $scope.loadingState.ci = true;

                      $scope.$apply();
                  },
                  function (error) {
                      jsHelper.showMessage('warning', error);
                      $scope.$apply();
                  }
              );
                break;

            case '#sampletab':
                if ($scope.loadingState.sample) return;

                clientService.getSampleOrderData(
                  context.id,
                  function (data) {
                      $scope.sampleOrders = data.data.sampleOrders;
                      $scope.loadingState.sample = true;

                      $scope.$apply();
                  },
                  function (error) {
                      jsHelper.showMessage('warning', error);
                      $scope.$apply();
                  }
              );
                break;

            case '#complaintab':
                if ($scope.loadingState.complain) return;

                clientService.getClientComplainData(
                    context.id,
                    function (data) {
                        $scope.clientComplaints = data.data.clientComplaints;
                        $scope.loadingState.complain = true;

                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.$apply();
                    }
                );
                break;

            case '#deltatab':
                if ($scope.loadingState.delta) return;

                $scope.event.getDeltaData();
                $scope.loadingState.delta = true;

                break;

            case '#deltatab2':
                if ($scope.loadingState.delta2) return;

                $scope.event.getDelta2Data();
                $scope.loadingState.delta2 = true;

                break;

            case '#deltatab3':
                if ($scope.loadingState.delta3) return;

                $scope.event.getDelta3Data();
                $scope.loadingState.delta3 = true;

                break;


            case '#purchasingoverviewtab':
                if ($scope.loadingState.purchasingoverview) return;

                clientService.getBreakdownFilter(
                    function (data) {
                        $scope.orderTypes = data.data.orderTypeDTOs;
                        $scope.quotationStatus = data.data.quotationStatusDTOs;
                        $scope.breakdownCategorys = data.data.breakdownCategoryDTOs;
                        $scope.exchange = data.data.exchange;

                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage("warning", error);
                    });
                $scope.event.getBreakdownData();
                $scope.loadingState.purchasingoverview = true;

                break;

            case '#additionalCostTab':
                if ($scope.loadingState.estimatedAdditionalCost) return;

                $scope.event.getEstimatedAdditionalCost();
                $scope.loadingState.estimatedAdditionalCost = true;

                break;
        }
    };

    $scope.viewDetailForm = {
        data: null,
        moduleName: 'OFFER',
        ciType: 1, //FOB

        load: function (moduleName, item) {
            //test
            var chartData = [];
            chartData.push({
                clientNM: 'PIER 1 IMPORTS (U.S.) INC.',
                season: '2015/2016',
                totalEURAmount: 940010.55
            });
            chartData.push({
                clientNM: 'PIER 1 IMPORTS (U.S.) INC.',
                season: '2016/2017',
                totalEURAmount: 720237.35
            });
            chartData.push({
                clientNM: 'PIER 1 IMPORTS (U.S.) INC.',
                season: '2014/2015',
                totalEURAmount: 897841
            });

            chartData.push({
                clientNM: 'Average of all clients',
                season: '2014/2015',
                totalEURAmount: 111
            });
            chartData.push({
                clientNM: 'Average of all clients',
                season: '2015/2016',
                totalEURAmount: 222
            });
            chartData.push({
                clientNM: 'Average of all clients',
                season: '2016/2017',
                totalEURAmount: 333
            });
            chartData.push({
                clientNM: 'Average of all clients',
                season: '2017/2018',
                totalEURAmount: 444
            });

            //mixed data
            var chartDataSorted = chartData.sort(function (a, b) { return a.season > b.season ? 1 : -1; });

            //get distinct season
            var seasons = [];
            angular.forEach(chartDataSorted, function (item) {
                if (seasons.indexOf(item.season) < 0) {
                    seasons.push(item.season)
                }
            });

            //get distinct client
            var clients = [];
            angular.forEach(chartDataSorted, function (item) {
                if (clients.indexOf(item.clientNM) < 0) {
                    clients.push(item.clientNM);
                }
            });

            //buil data for chart
            var series = [];
            angular.forEach(clients, function (cItem) {
                var serie = {
                    name: cItem,
                    data: []
                };
                angular.forEach(seasons, function (sItem) {
                    var x = chartDataSorted.filter(function (o) { return o.season === sItem && o.clientNM === cItem });
                    var amount = 0
                    if (x.length > 0) {
                        amount = x[0].totalEURAmount;
                    }
                    else {
                        amount = 0;
                    }
                    serie.data.push(amount);
                });
                series.push(serie);
            });

            //main
            $scope.viewDetailForm.moduleName = moduleName;
            switch (moduleName) {
                case 'OFFER':
                    clientService.getOfferLine(item.offerID
                        , function (data) {
                            $scope.viewDetailForm.data = data.data;
                            $('#frmDetail').modal('show');
                            $scope.$apply();
                        }
                        , function (error) {
                            jsHelper.showMessage('warning', error);
                            $scope.viewDetailForm.data = null;
                        });
                    break;
                case 'PI':
                    clientService.getSaleOrderDetail(item.offerID
                        , function (data) {
                            $scope.viewDetailForm.data = data.data;
                            $('#frmDetail').modal('show');
                            $scope.$apply();
                        }
                        , function (error) {
                            jsHelper.showMessage('warning', error);
                            $scope.viewDetailForm.data = null;
                        });
                    break;
                case 'CI':
                    $scope.viewDetailForm.ciType = item.typeOfInvoice;
                    if (item.typeOfInvoice === 1) { //FOB CI
                        clientService.getECommercialInvoiceDetail(item.eCommercialInvoiceID
                        , function (data) {
                            $scope.viewDetailForm.data = data.data;
                            $('#frmDetail').modal('show');
                            $scope.$apply();
                        }
                        , function (error) {
                            jsHelper.showMessage('warning', error);
                            $scope.viewDetailForm.data = null;
                        });
                    }
                    else if (item.typeOfInvoice === 2) { //WAREHOUSE INVOICE CI
                        clientService.getWarehouseInvoiceProductDetail(item.eCommercialInvoiceID
                        , function (data) {
                            $scope.viewDetailForm.data = data.data;
                            $('#frmDetail').modal('show');
                            $scope.$apply();
                        }
                        , function (error) {
                            jsHelper.showMessage('warning', error);
                            $scope.viewDetailForm.data = null;
                        });
                    }
                    else if (item.typeOfInvoice === 3) { //OTHER CI
                        clientService.getECommercialInvoiceExtDetail(item.eCommercialInvoiceID
                        , function (data) {
                            $scope.viewDetailForm.data = data.data;
                            $('#frmDetail').modal('show');
                            $scope.$apply();
                        }
                        , function (error) {
                            jsHelper.showMessage('warning', error);
                            $scope.viewDetailForm.data = null;
                        });
                    }
                    break;
            }
        }
    }

    // default event.
    $scope.event.load();


    $scope.nullHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
        },
        isTriggered: false
    };

    $scope.updateOfferPotentialStatus = function (isPotential) {
        var data = $scope.offersmargin.offerMarginDTOs.filter(o=>o.isSelected);
        angular.forEach(data, function (item) {
            item.isPotential = isPotential;
            item.isSelected = false;
        });
        clientService.updateOfferPotentialStatus(
            data,
            function (data) {

            },
            function (error) {
            }
        );
    }
}]);