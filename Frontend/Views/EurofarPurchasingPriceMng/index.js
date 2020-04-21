(function () {
    'use strict';

    //
    // SUPPORT
    //
    $('.search-filter').keypress(function (e) {
        if (e.keyCode === 13) {
            var scope = angular.element($('body')).scope();
            scope.controls.grdSearchResult.event.reload();
        }
    });

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
    angular.module('tilsoftApp').controller('tilsoftController', ['$scope', '$timeout', '$cookieStore', 'dataService', 'frmQuotationRequestJs', function ($scope, $timeout, $cookieStore, dataService, frmQuotationRequestJs) {
        dataService.searchFilter.pageSize = context.pageSize;
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;

        //
        // properties
        //
        $scope.frmQuotationRequestJs = frmQuotationRequestJs;
        $scope.frmQuotationRequestJs.token = context.token;
        $scope.frmQuotationRequestJs.serviceUrl = context.serviceQuotationRequestUrl;
        $scope.views = [
            { id: 'pnlCentralData', title: 'Central Database'},
            { id: 'pnlQuotationRequest', title: 'Quotation Request' }
        ];
        $scope.additional = null;
        $scope.factoryEmailAddresses = [];
        $scope.data = {
            quotationStatuses: null,
            orderTypes: null,
            eurofarSupportBreakdownCategorys: [],
            pricingTeamMemberInChargeDTOs: [],
            exchange: null,
            loadAbilitys: null,
            isPermissionSeeAllPurchasingData: null,
            seasons: jsHelper.getSeasons(),
            itemTypes: [
                { id: 1, name: 'F/O PRODUCT' },
                { id: 0, name: 'F/O SPAREPART' },
                { id: 6, name: 'F/O SAMPLE' },
                { id: 5, name: 'OFFER ITEM' },
                { id: 2, name: 'OFFER SAMPLE' },
                { id: 7, name: 'OFFER ITEM (Estimated)' },
                { id: 3, name: 'CATALOGUE ITEM' },
                { id: 4, name: 'REQUESTED ITEM' }
            ],
            businessDataStatuses: [
                { id: 1, name: 'NEGATIVE FINAL MARGIN' },
                { id: 2, name: 'MISSING PURCHASING PRICE & LOAD ABILITY' },
                { id: 3, name: 'MISSING PURCHASING PRICE' },
                { id: 4, name: 'MISSING LOAD ABILITY' }
            ],
            shippedStatuses: [
                { id: 0, name: 'NOT YET SHIPPED' },
                { id: 1, name: 'FULLY/PARTIALLY SHIPPED' },
            ],
            waitStatuses: [
                { id: 1, name: 'WAIT EUROFAR' },
                { id: 2, name: 'WAIT FACTORY' }
            ],
            isFirstLoadProcessed: false,
            isSelectAll: false,
            historyData: null,
            purchaseHistoryData: null,
            generalBreakDownData: null,
            palBreakDownData: null,
            status: {
                total: null,
                confirmed: null,
                pending: null,
                waitEurofar: null,
                waitFactory: null,
                estimated: null,

                totalContainer: null,
                totalConfirmedContainer: null,
                totalPendingContainer: null,
                totalContainerWaitForEurofar: null,
                totalContainerWaitForFactory: null,
                totalContainerEstimated: null,

                season: null,
                isLoaded: false,

                waitForFactoryConlusion: null,
                waitForFactoryConlusionSummary: null,
                waitForFactoryProductionConlusion: null,
                waitForFactoryProductionConlusionSummary: null
            },
            selectedRow: null
        };
        $scope.totalCostPriceUSD = {
            avfPriceUSD: parseFloat(0),
            avtPriceUSD: parseFloat(0),
            avfManagementFeeUSD: parseFloat(0),
            avtManagementFeeUSD: parseFloat(0)
        };

        //
        // events
        //
        $scope.event = {
            init: function () {
                dataService.getSearchFilter(
                    function (data) {
                        $scope.data.quotationStatuses = data.data.quotationStatusDTOs;
                        $scope.data.orderTypes = data.data.orderTypeDTOs;
                        $scope.data.eurofarSupportBreakdownCategorys = data.data.eurofarBreakdownCategoryDTOs;
                        $scope.data.pricingTeamMemberInChargeDTOs = data.data.pricingTeamMemberInChargeDTOs;
                        $scope.data.exchange = data.data.exchange;
                        $scope.data.isPermissionSeeAllPurchasingData = data.data.isPermissionSeeAllPurchasingData;
                        $scope.controls.grdSearchResult.event.reload();
                    },
                    function (error) {
                        // reset data
                    }
                );
            },
            updateTarget: function () {
                var dataToBeUpdated = [];
                angular.forEach($scope.controls.grdSearchResult.data, function (item) {
                    if (item.newTargetPrice) {
                        dataToBeUpdated.push(item);
                    }
                });

                dataService.updateTarget(
                    dataToBeUpdated,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            $scope.controls.grdSearchResult.event.reload();
                            ////
                            //// refresh data
                            ////
                            //angular.forEach(dataToBeUpdated, function (item) {
                            //    item.targetPrice = item.newTargetPrice;
                            //    if (parseFloat(item.newTargetPrice) === parseFloat(item.priceIncludeDiff)) {
                            //        item.statusID = 3; // auto set confirm status
                            //        item.quotationStatusNM = 'CONFIRMED';
                            //        item.quotationOfferDirectionID = null;
                            //    }
                            //    else {
                            //        item.quotationOfferDirectionID = 2;
                            //    }
                            //    item.newTargetPrice = null;
                            //    item.newTargetComment = null;

                            //    if ($scope.controls.grdSearchResult.filters.StatusID && $scope.controls.grdSearchResult.filters.StatusID !== item.statusID) {
                            //        $scope.controls.grdSearchResult.data.splice($scope.controls.grdSearchResult.data.indexOf(item), 1);
                            //    }

                            //    //
                            //    // refresh margin
                            //    //
                            //    item.marginBeforeCommissionUSD = item.purchasingPrice ? jsHelper.round((item.fobUnitPrice - item.purchasingPrice) * item.orderQnt, 2) : null;
                            //    item.marginPercentBeforeCommission = item.purchasingPrice ? jsHelper.round((item.fobUnitPrice - item.purchasingPrice) * 100 / item.purchasingPrice, 2) : null;
                            //    item.marginBeforeCommissionUSDByTarget = item.targetPrice ? jsHelper.round((item.fobUnitPrice - item.targetPrice) * item.orderQnt, 2) : null;
                            //    item.marginPercentBeforeCommissionByTarget = item.targetPrice ? jsHelper.round((item.fobUnitPrice - item.targetPrice) * 100 / item.targetPrice, 2) : null;
                            //    item.marginAfterCommissionUSD = item.purchasingPrice ? item.marginBeforeCommissionUSD - item.commissionAmountUSD : null;
                            //    item.marginPercentAfterCommission = item.purchasingPrice ? jsHelper.round(item.marginAfterCommissionUSD * 100 / (item.purchasingPrice * item.orderQnt), 2) : null;
                            //    item.marginAfterCommissionUSDByTarget = item.targetPrice ? item.marginBeforeCommissionUSDByTarget - item.commissionAmountUSD : null;
                            //    item.marginPercentAfterCommissionByTarget = item.targetPrice ? jsHelper.round(item.marginAfterCommissionUSDByTarget * 100 / (item.targetPrice * item.orderQnt), 2) : null;
                            //});

                            ////
                            //// refresh total row
                            ////
                            //$scope.controls.grdSearchResult.event.getTotal();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            },          
            confirmPrice: function () {
                if (!confirm('Confirm price for the selected item ?')) return;

                var dataToBeUpdated = [];
                angular.forEach($scope.controls.grdSearchResult.data, function (item) {
                    if (item.isSelected) {
                        dataToBeUpdated.push(item);
                    }
                });

                dataService.confirmPrice(
                    dataToBeUpdated,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            //
                            // refresh data
                            //
                            angular.forEach(dataToBeUpdated, function (item) {
                                if (item.priceIncludeDiff !== null) {
                                    item.targetPrice = item.priceIncludeDiff;
                                    item.statusID = 3; // auto set confirm status
                                    item.quotationStatusNM = 'CONFIRMED';
                                    item.newTargetPrice = null;
                                    item.newTargetComment = null;
                                    item.quotationOfferDirectionID = null;
                                }
                                if ($scope.controls.grdSearchResult.filters.StatusID && $scope.controls.grdSearchResult.filters.StatusID !== item.statusID) {
                                    $scope.controls.grdSearchResult.data.splice($scope.controls.grdSearchResult.data.indexOf(item), 1);
                                }
                            });
                            $scope.event.toggleSelectAll();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            },
            unConfirmPrice: function () {
                if (!confirm('Reset confirm status for the selected item ?')) return;

                var dataToBeUpdated = [];
                angular.forEach($scope.controls.grdSearchResult.data, function (item) {
                    if (item.isSelected) {
                        dataToBeUpdated.push(item);
                    }
                });

                dataService.unConfirmPrice(
                    dataToBeUpdated,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            //
                            // refresh data
                            //
                            angular.forEach(dataToBeUpdated, function (item) {
                                item.statusID = 1; // auto set pending status
                                item.quotationStatusNM = 'PENDING';
                                item.newTargetPrice = null;
                                item.newTargetComment = null;
                                item.quotationOfferDirectionID = 1;
                                if ($scope.controls.grdSearchResult.filters.StatusID && $scope.controls.grdSearchResult.filters.StatusID !== item.statusID) {
                                    $scope.controls.grdSearchResult.data.splice($scope.controls.grdSearchResult.data.indexOf(item), 1);
                                }
                            });
                            $scope.event.toggleSelectAll();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            },
            toggleSelectAll: function () {
                angular.forEach($scope.controls.grdSearchResult.data, function (item) {
                    if (item.unConfirmable && item.statusID !== 5) {
                        item.isSelected = $scope.data.isSelectAll;
                    }                    
                });
            },
            openHistory: function (item) {
                $scope.data.historyData = null;
                dataService.getHistory(
                    item.quotationDetailID,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            $scope.data.historyData = data.data;
                            $('#frmHistory').modal();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            },
            openPurchaseHistory: function (item) {
                $scope.data.purchaseHistoryData = null;
                dataService.getPurchaseHistory(
                    item.quotationDetailID,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            $scope.data.purchaseHistoryData = data.data;
                            $('#frmPurchaseHistory').modal();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            },
            openGeneralBreakDown: function (item) {
                $scope.data.generalBreakDownData = null;
                dataService.getGeneralBreakDown(
                    item.modelID,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            $scope.data.generalBreakDownData = data.data;
                            $('#frmGeneralBreakDown').modal();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            },
            openPALBreakDown: function (item) {
                $scope.data.palBreakDownData = null;
                dataService.getPALBreakDown(
                    item.modelID,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            $scope.data.palBreakDownData = data.data;
                            $('#frmPALBreakDown').modal();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            },
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
                if ($scope.controls.grdSearchResult.eurofarBreakdownOption !== null && $scope.controls.grdSearchResult.eurofarBreakdownOption !== undefined) {
                    breakdownCategoryOptions = $scope.controls.grdSearchResult.eurofarBreakdownOption.filter(o => o.breakdownCategoryID === breakdownCategoryID && o.modelID === dataSearch.modelID);
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
                            costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.data.exchange, 2);
                        }
                        //get AVT Price
                        companyID = 3;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.materialID === materialID && o.materialTypeID === materialTypeID && o.materialColorID === materialColorID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                            costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.data.exchange, 2);
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
                            costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.data.exchange, 2);
                        }
                        //get AVT price
                        companyID = 3;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.frameMaterialID === frameMaterialID && o.frameMaterialColorID === frameMaterialColorID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                            costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.data.exchange, 2);
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
                            costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.data.exchange, 2);
                        }
                        //get AVT price
                        companyID = 3;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.subMaterialID === subMaterialID && o.subMaterialColorID === subMaterialColorID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                            costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.data.exchange, 2);
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
                            costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.data.exchange, 2);
                        }
                        //get AVT price
                        companyID = 3;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.cushionColorID === cushionColorID && o.backCushionID === backCushionID && o.seatCushionID === seatCushionID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                            costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.data.exchange, 2);
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
                            costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.data.exchange, 2);
                        }
                        //get AVT price
                        companyID = 3;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.packagingMethodID === packagingMethodID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                            costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.data.exchange, 2);
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
                            costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.data.exchange, 2);
                        }
                        //get AVT price
                        companyID = 3;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.fscTypeID === fscTypeID && o.fscPercentID === fscPercentID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                            costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.data.exchange, 2);
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
                            costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.data.exchange, 2);
                        }
                        //get AVT price
                        companyID = 3;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID /*&& o.offerLineID === offerLineID*/);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                            costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.data.exchange, 2);
                        }
                        return costPrice;

                    case 6: case 7: case 10: //HARDWARE COST,OTHER MATERIAL COST,MANAGEMENT FEE
                        //no Option to Get
                        //get AVF price
                        companyID = 1;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                            costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.data.exchange, 2);
                        }
                        //get AVT price
                        companyID = 3;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                            costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.data.exchange, 2);
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
                if ($scope.controls !== undefined && $scope.controls !== null) {
                    var breakdownCategoryOptions = [];
                    if ($scope.controls.grdSearchResult.eurofarBreakdownManagementFees !== null && $scope.controls.grdSearchResult.eurofarBreakdownManagementFees !== undefined && $scope.controls.grdSearchResult.eurofarBreakdownManagementFees.length > 0) {
                        if ($scope.controls.grdSearchResult.eurofarBreakdownOption !== null && $scope.controls.grdSearchResult.eurofarBreakdownOption !== undefined) {
                            breakdownCategoryOptions = $scope.controls.grdSearchResult.eurofarBreakdownOption.filter(o => o.modelID === data.modelID);
                        }
                        if (breakdownCategoryOptions !== null && breakdownCategoryOptions !== undefined && breakdownCategoryOptions.length > 0) {
                            for (var i = 0; i < $scope.controls.grdSearchResult.eurofarBreakdownManagementFees.length; i++) {
                                var itemManagementFee = $scope.controls.grdSearchResult.eurofarBreakdownManagementFees[i];
                                for (var j = 0; j < breakdownCategoryOptions.length; j++) {
                                    var itembreakdownCategoryOption = breakdownCategoryOptions[j];
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
                }
                return managementFeePercent;
            },

            getseasonSpecPercent: function (data) {
                var seasonSpecPercent = 0;
                //var breakdownCategoryOptions = [];
                if ($scope.controls !== undefined && $scope.controls !== null) {
                    if ($scope.controls.grdSearchResult.eurofarSeasonSpecPercents !== null && $scope.controls.grdSearchResult.eurofarSeasonSpecPercents !== undefined && $scope.controls.grdSearchResult.eurofarSeasonSpecPercents.length > 0) {
                        if ($scope.controls.grdSearchResult.eurofarBreakdownOption !== null && $scope.controls.grdSearchResult.eurofarBreakdownOption !== undefined) {
                            var breakdownCategoryOptions = $scope.controls.grdSearchResult.eurofarBreakdownOption.filter(o => o.modelID === data.modelID);
                        }
                        if (breakdownCategoryOptions !== null && breakdownCategoryOptions !== undefined) {
                            for (var i = 0; i < $scope.controls.grdSearchResult.eurofarSeasonSpecPercents.length; i++) {
                                var itemseasonSpecPercent = $scope.controls.grdSearchResult.eurofarSeasonSpecPercents[i];
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
                if ($scope.controls !== undefined && $scope.controls !== null) {
                    if ($scope.controls.grdSearchResult.eurofarBreakdownOption !== null && $scope.controls.grdSearchResult.eurofarBreakdownOption !== undefined) {
                        var breakdownCategoryOptions = $scope.controls.grdSearchResult.eurofarBreakdownOption.filter(o => o.modelID === data.modelID && o.breakdownCategoryID === 5);
                    }
                    if (breakdownCategoryOptions !== null && breakdownCategoryOptions !== undefined) {
                        for (var i = 0; i < breakdownCategoryOptions.length; i++) {
                            var itemBreakdownCategoryOptions = breakdownCategoryOptions[i];
                            if (itemBreakdownCategoryOptions.modelID === data.modelID) {

                                //AVF
                                companyID = 1;
                                if (data.exportCost40HC > 0 && itemBreakdownCategoryOptions.loadAbilityAVF > 0 && itemBreakdownCategoryOptions.companyID === 1) {
                                    shippingFee.shippingFeeAVFVND = jsHelper.round(parseFloat(data.exportCost40HC) / parseFloat(itemBreakdownCategoryOptions.loadAbilityAVF), 2);
                                }
                                shippingFee.shippingFeeAVFUSD = jsHelper.round(parseFloat(shippingFee.shippingFeeAVFVND) / $scope.data.exchange, 2);

                                companyID = 3;
                                if (data.exportCost40HC > 0 && itemBreakdownCategoryOptions.loadAbilityAVT > 0 && itemBreakdownCategoryOptions.companyID === 3) {
                                    shippingFee.shippingFeeAVTVND = jsHelper.round(parseFloat(data.exportCost40HC) / parseFloat(itemBreakdownCategoryOptions.loadAbilityAVT), 2);
                                }
                                shippingFee.shippingFeeAVTUSD = jsHelper.round(parseFloat(shippingFee.shippingFeeAVTVND) / $scope.data.exchange, 2);
                            }
                        }
                    }
                }
                return shippingFee;
            },

            exportExcel: function () {
                dataService.exportExcel({
                    filters: {
                        Season: $scope.controls.grdSearchResult.filters.Season,
                        ClientNM: $scope.controls.grdSearchResult.filters.ClientNM,
                        Description: $scope.controls.grdSearchResult.filters.Description,
                        FactoryUD: $scope.controls.grdSearchResult.filters.FactoryUD,
                        ItemTypeID: $scope.controls.grdSearchResult.filters.ItemTypeID,
                        StatusID: $scope.controls.grdSearchResult.filters.StatusID,
                        QuotationOfferDirectionID: $scope.controls.grdSearchResult.filters.QuotationOfferDirectionID,
                        ProformaInvoiceNo: $scope.controls.grdSearchResult.filters.ProformaInvoiceNo,
                        OrderTypeID: $scope.controls.grdSearchResult.filters.OrderTypeID,
                        BusinessDataStatusID: $scope.controls.grdSearchResult.filters.BusinessDataStatusID,
                        ShippedStatus: $scope.controls.grdSearchResult.filters.ShippedStatus,
                        LDSFrom: $scope.controls.grdSearchResult.filters.LDSFrom,
                        LDSTo: $scope.controls.grdSearchResult.filters.LDSTo,
                        ModelStatusID: $scope.controls.grdSearchResult.filters.ModelStatusID,
                        CataloguePageNo: $scope.controls.grdSearchResult.filters.CataloguePageNo,
                        PricingTeamMemberID: $scope.controls.grdSearchResult.filters.PricingTeamMemberID
                    }
                },
                     function (data) {
                        window.location = context.backendReportUrl + data.data;
                    },
                    function (error) {
                    }
                );
            },
            getWaitForFactoryConclusion: function () {
                $('#pnlWaitForFactory').show();
            },
            getEmailAddresses: function (factoryID) {
                if ($scope.factoryEmailAddresses.filter(o => o.factoryID === factoryID).length > 0) {
                    return $scope.factoryEmailAddresses.filter(o => o.factoryID === factoryID)[0].emailAddress;
                }
                return null;
            },

            switchView: function (pnl) {
                angular.forEach($scope.views, function (item) {
                    if (item.id !== pnl) {
                        $('#' + item.id).hide();
                    }
                    else {
                        $('#' + item.id).show();
                        $('#sectionTitle').html(item.title);
                    }
                });

                switch (pnl) {
                    case 'pnlQuotationRequest':
                        $scope.frmQuotationRequestJs.searchFilter.filters.Season = $scope.controls.grdSearchResult.filters.Season;
                        $scope.frmQuotationRequestJs.event.init();
                        break;
                }
            },
            openAdditional: function (additional) {
                $timeout(function () {
                    $scope.additional = additional;
                }, 0);

                jQuery("#frmAdditionCondition").modal();
            },            
            seasonShortCut: function (season) {
                return season.substr(2, 3) + season.substr(7, 2);
            },
        };

        //
        //import from excel BOM file
        //
        $scope.importFromExcel = {

            importExcel: function () {
                var input = document.createElement("input");
                input.setAttribute("type", "file");
                input.setAttribute("accept", ".xlsx");
                input.addEventListener("change", function (e) {

                    //get file content
                    var file = e.target.files[0];
                    var f = e.target.files[0];
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        var data = e.target.result;

                        var result;
                        var workbook = XLSX.read(data, { type: 'binary' });

                        var sheet_name_list = workbook.SheetNames;
                        sheet_name_list.forEach(function (y) { /* iterate through sheets */
                            //Convert the cell value to Json
                            var roa = XLSX.utils.sheet_to_json(workbook.Sheets[y], { raw: true });
                            if (roa.length > 0) {
                                result = roa;
                            }
                        });
                        console.log(result);

                        //
                        //Import data
                        //
                        $scope.lstImport = [];

                        for (var i = 1; i < result.length; i++) {
                            var item = result[i];
                            console.log(item);
                            var addItem = {
                                quotationDetailID: item.QuotationDetailID,
                                newTargetComment: item.NewTargetComment,
                                targetPrice: item.TargetPrice,
                                season: item.Season
                            };

                            $scope.lstImport.push(addItem);
                        }

                        dataService.import(
                            $scope.lstImport,
                            function (data) {
                                jsHelper.processMessage(data.message);
                                if (data.message.type === 0) {
                                    $scope.event.reload();
                                }
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error.message.message);
                            }
                        );
                    };
                    reader.readAsArrayBuffer(f);

                }, false);
                input.click();
            },
        };

        //
        // method
        //
        $scope.method = {
            getPrevSeason: function () {
                if ($scope.controls.grdSearchResult.filters.Season)
                    return jsHelper.getPrevSeason($scope.controls.grdSearchResult.filters.Season, true);
                else
                    return null;
            },
            get2ndPrevSeason: function () {
                if ($scope.controls.grdSearchResult.filters.Season)
                    return jsHelper.getPrevSeason(jsHelper.getPrevSeason($scope.controls.grdSearchResult.filters.Season), true);
                else
                    return null;
            },
            get3rdPrevSeason: function () {
                if ($scope.controls.grdSearchResult.filters.Season)
                    return jsHelper.getPrevSeason(jsHelper.getPrevSeason(jsHelper.getPrevSeason($scope.controls.grdSearchResult.filters.Season)), true);
                else
                    return null;
            },
            getStatusNM: function (id) {
                var result = '';
                if ($scope.data.quotationStatuses) {
                    angular.forEach($scope.data.quotationStatuses, function (item) {
                        if (item.quotationStatusID === id) {
                            result = item.quotationStatusNM;
                        }
                    });
                }
                return result;
            },
            //getTotalCostPriceAVT: function (data) {
            //    var totalCostPrice = {
            //        avfPrice: parseFloat(0),
            //        avtPrice: parseFloat(0),
            //        avfManagementFee: parseFloat(0),
            //        avtManagementFee: parseFloat(0)
            //    };   
            //    $scope.totalCostPriceUSD = {
            //        avfPriceUSD: parseFloat(0),
            //        avtPriceUSD: parseFloat(0),
            //        avfManagementFeeUSD: parseFloat(0),
            //        avtManagementFeeUSD: parseFloat(0)
            //    };
            //    //get breakdown category
            //    var breakdownCategories = [];
            //    if ($scope.data.eurofarSupportBreakdownCategorys !== null && $scope.data.eurofarSupportBreakdownCategorys !== undefined) {
            //        breakdownCategories = angular.copy($scope.data.eurofarSupportBreakdownCategorys);
            //    }

            //    ////cal total price
            //    angular.forEach(breakdownCategories, function (item) {
            //        var costPrice = $scope.event.getCostPrice(item.breakdownCategoryID, data);
            //        //totalCostPrice.avfPrice += parseFloat(costPrice.avfPrice);
            //        //AVT
            //        var costUSD = 0;
            //        costUSD = jsHelper.round(costPrice.avtPrice / $scope.data.exchange, 2);
            //        $scope.totalCostPriceUSD.avtPriceUSD += costUSD; 
            //        totalCostPrice.avtPrice += parseFloat(costPrice.avtPrice);
            //        //AVF
            //        var costAVFUSD = 0;
            //        costAVFUSD = jsHelper.round(costPrice.avfPrice / $scope.data.exchange, 2);
            //        $scope.totalCostPriceUSD.avfPriceUSD += costAVFUSD;
            //        totalCostPrice.avfPrice += parseFloat(costPrice.avfPrice);
            //    });
            //    //management fee amount base on percent
            //    var managementFeePercent = $scope.event.getManagementFeePercent(data);
            //    //totalCostPrice.avtManagementFee = totalCostPrice.avtPrice * managementFeePercent / 100;
            //    //totalCostPrice.avtPrice = totalCostPrice.avtPrice + totalCostPrice.avtManagementFee;

            //    //total cost price after management fee           
            //    //AVT
            //    $scope.totalCostPriceUSD.avtPriceUSD = $scope.totalCostPriceUSD.avtPriceUSD * (1 + managementFeePercent.avtManagement / 100);
            //    totalCostPrice.avtPrice = totalCostPrice.avtPrice * (1 + managementFeePercent / 100);
            //    //AVF
            //    $scope.totalCostPriceUSD.avfPriceUSD = $scope.totalCostPriceUSD.avfPriceUSD * (1 + managementFeePercent.avfManagement / 100);
            //    totalCostPrice.avfPrice = totalCostPrice.avfPrice * (1 + managementFeePercent / 100);

            //    //total cost amount base on SeasonSpecPercent
            //    var seasonSpecPercent = $scope.event.getseasonSpecPercent(data);
            //    //totalCostPrice.avfPrice = totalCostPrice.avfPrice * (1 + seasonSpecPercent / 100);
            //    //AVT
            //    $scope.totalCostPriceUSD.avtPriceUSD = $scope.totalCostPriceUSD.avtPriceUSD * (1 + seasonSpecPercent / 100);
            //    totalCostPrice.avtPrice = totalCostPrice.avtPrice * (1 + seasonSpecPercent / 100);
            //    //AVF
            //    $scope.totalCostPriceUSD.avfPriceUSD = $scope.totalCostPriceUSD.avfPriceUSD * (1 + seasonSpecPercent / 100);
            //    totalCostPrice.avfPrice = totalCostPrice.avfPrice * (1 + seasonSpecPercent / 100);


            //    var loadAVT = 0;
            //    var loadAVF = 0;
            //    for (var i = 0; i < $scope.controls.grdSearchResult.data.length; i++) {
            //        var item = $scope.controls.grdSearchResult.data[i];
            //        if (item.modelID === data.modelID && item.deliveryTerm === 'FOB') {
            //            if (data.loadAbilityAVT !== 0 && data.loadAbilityAVT !== null && item.exportCost40HC !== null) {
            //                loadAVT = jsHelper.round(parseFloat(item.exportCost40HC) / parseFloat(data.loadAbilityAVT), 2);
            //            }
            //            if (data.loadAbilityAVF !== 0 && data.loadAbilityAVF !== null && item.exportCost40HC !== null) {
            //                loadAVF = jsHelper.round(parseFloat(item.exportCost40HC) / parseFloat(data.loadAbilityAVF), 2);
            //            }
            //        }
            //    }
            //    $scope.totalCostPriceUSD.avtPriceUSD += jsHelper.round(loadAVT / $scope.data.exchange, 2);

            //    //EXCHANGE RATE
            //    var vnD_USD_ExchangeRate = 2.3;
            //    //totalCostPrice.avtPrice = jsHelper.round(totalCostPrice.avtPrice / vnD_USD_ExchangeRate, 2);
            //    //return totalCostPrice.avtPrice + load;
            //    //AVF
            //    $scope.totalCostPriceUSD.avfPriceUSD += jsHelper.round(loadAVF / $scope.data.exchange, 2);

            //    return $scope.totalCostPriceUSD.avtPriceUSD;
            //},

            //getTotalCostPriceAVF: function (data) {
            //    var totalCostPrice = {
            //        avfPrice: parseFloat(0),
            //        avtPrice: parseFloat(0),
            //        avfManagementFee: parseFloat(0),
            //        avtManagementFee: parseFloat(0)
            //    };
            //    $scope.totalCostPriceUSD = {
            //        avfPriceUSD: parseFloat(0),
            //        avtPriceUSD: parseFloat(0),
            //        avfManagementFeeUSD: parseFloat(0),
            //        avtManagementFeeUSD: parseFloat(0)
            //    };
            //    //get breakdown category
            //    var breakdownCategories = [];
            //    if ($scope.data.avtSupportBreakdownCategorys !== null && $scope.data.avtSupportBreakdownCategorys !== undefined) {
            //        breakdownCategories = angular.copy($scope.data.avtSupportBreakdownCategorys);
            //    }
            //    ////cal total price
            //    angular.forEach(breakdownCategories, function (item) {
            //        var costPrice = $scope.event.getCostPrice(item.breakdownCategoryID, data);
            //        var costUSD = 0;
            //        costUSD = jsHelper.round(costPrice.avfPrice / $scope.data.exchange, 2);
            //        $scope.totalCostPriceUSD.avfPriceUSD += costUSD; 
            //        totalCostPrice.avfPrice += parseFloat(costPrice.avfPrice);
            //        //totalCostPrice.avtPrice += parseFloat(costPrice.avtPrice);
            //    });

            //    //////management fee amount base on percent
            //    var managementFeePercent = $scope.event.getManagementFeePercent(data);
            //    //totalCostPrice.avfManagementFee = totalCostPrice.avfPrice * managementFeePercent / 100;

            //    //total cost price after management fee
            //    //totalCostPrice.avtPrice = totalCostPrice.avtPrice * (1 + managementFeePercent / 100);
            //    $scope.totalCostPriceUSD.avfPriceUSD = $scope.totalCostPriceUSD.avfPriceUSD * (1 + managementFeePercent / 100);
            //    totalCostPrice.avfPrice = totalCostPrice.avfPrice * (1 + managementFeePercent / 100);
               
            //    //total cost amount base on SeasonSpecPercent
            //    var seasonSpecPercent = $scope.event.getseasonSpecPercent(data);
            //    $scope.totalCostPriceUSD.avfPriceUSD = $scope.totalCostPriceUSD.avfPriceUSD * (1 + seasonSpecPercent / 100);
            //    totalCostPrice.avfPrice = totalCostPrice.avfPrice * (1 + seasonSpecPercent / 100);
            //    ////totalCostPrice.avtPrice = totalCostPrice.avtPrice * (1 + seasonSpecPercent / 100);
            //    var load = 0;
            //    for (var i = 0; i < $scope.controls.grdSearchResult.data.length; i++) {
            //        var item = $scope.controls.grdSearchResult.data[i];
            //        if (item.modelID === data.modelID && item.deliveryTerm === 'FOB') {
            //            load = jsHelper.round(parseFloat(item.exportCost40HC) / parseFloat(data.loadAbility), 2);
            //        }
            //    }
            //    $scope.totalCostPriceUSD.avfPriceUSD += jsHelper.round(load / $scope.data.exchange, 2);


            //    //EXCHAGE RATE
            //    var vnD_USD_ExchangeRate = 2.3;
            //    //totalCostPrice.avfPrice = jsHelper.round(totalCostPrice.avfPrice / vnD_USD_ExchangeRate, 2);
            //    //totalCostPrice.avfManagementFee = jsHelper.round(totalCostPrice.avfManagementFee / scope.productWizard.vnD_USD_ExchangeRate, 2);
            //    //return totalCostPrice.avtPrice + load;
            //    return $scope.totalCostPriceUSD.avfPriceUSD;
            //},

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
                if ($scope.data.eurofarSupportBreakdownCategorys !== null && $scope.data.eurofarSupportBreakdownCategorys !== undefined) {
                    breakdownCategories = angular.copy($scope.data.eurofarSupportBreakdownCategorys);
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
                if ($scope.data.eurofarSupportBreakdownCategorys !== null && $scope.data.eurofarSupportBreakdownCategorys !== undefined) {
                    breakdownCategories = angular.copy($scope.data.eurofarSupportBreakdownCategorys);
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

                //Shipping Fee
                var shippingFee = $scope.event.getShippingFee(data);
                totalCostPrice.avfPriceUSD = totalCostPrice.avfPriceUSD + shippingFee.shippingFeeAVFUSD;

                return totalCostPrice.avfPriceUSD;
            },

            delta: function (factoryPrice, priceSeason) {
                if (factoryPrice !== null && factoryPrice !== '' && priceSeason !== null && priceSeason !== '') {
                    var price1 = jsHelper.round(factoryPrice, 2);
                    var price2 = jsHelper.round(priceSeason, 2);
                    if (price2 === 0) {
                        return 0;
                    }
                    else {
                        var totalAmount = ((price1 - price2) / price2) * 100;
                        return totalAmount;
                    }                 
                }
                else {
                    return 0;
                }
            }
        };

        //
        // controls
        //
        $scope.gridHandler = {
            loadMore: function () {
                $scope.controls.grdSearchResult.event.loadMore();
            },
            sort: function (sortedBy, sortedDirection) {
                console.log(sortedBy + ' - ' + sortedDirection);
                $scope.controls.grdSearchResult.event.sort(sortedBy, sortedDirection);
            },
            isTriggered: true
        };
        $scope.controls = {
            grdSearchResult : {
                data: [],
                totalRow: {
                    totalOrderQnt: 0,
                    totalOrderQnt40HC: 0.00,
                    saleAmountUSD: 0.00,
                    saleAmountEUR: 0.00,
                    saleAmountConvertedUSD: 0.00,
                    purchasingAmountUSD: 0.00,
                    transportationUSD: 0.00,
                    damageUSD: 0.00,
                    commissionUSD: 0.00,
                    bonusUSD: 0.00,
                    interestUSD: 0.00,
                    lcCostUSD: 0.00,
                    delta6Amount: 0.00,
                    isAdditionalDataLoaded: false
                },
                subTotalRow: {
                    totalOrderQnt: 0,
                    totalOrderQnt40HC: 0.00,
                    saleAmountUSD: 0.00,
                    saleAmountEUR: 0.00,
                    saleAmountConvertedUSD: 0.00,
                    purchasingAmountUSD: 0.00,
                    transportationUSD: 0.00,
                    damageUSD: 0.00,
                    commissionUSD: 0.00,
                    bonusUSD: 0.00,
                    interestUSD: 0.00,
                    lcCostUSD: 0.00,
                    delta6Amount: 0.00,
                    isAdditionalDataLoaded: false
                },
                eurofarBreakdownOption: [],
                eurofarSeasonSpecPercents: [],
                eurofarBreakdownManagementFees: [],
                filters: {
                    Season: context.autoSeason ? context.autoSeason : jsHelper.getCurrentSeason(),
                    ClientNM: '',
                    Description: context.autoArtCode ? context.autoArtCode : '',
                    FactoryUD: context.autoFactory ? context.autoFactory : '',
                    ItemTypeID: context.autoItemType ? context.autoItemType : null,
                    StatusID: null,
                    QuotationOfferDirectionID: null,
                    ProformaInvoiceNo: '',
                    OrderTypeID: '',
                    BusinessDataStatusID: null,
                    ShippedStatus: null,
                    LDSFrom: null,
                    LDSTo: null,
                    CataloguePageNo: null,
                    PricingTeamMemberID: null
                },
                status: {
                    isTriggered: false,
                    pageIndex: 1,
                    totalPage: 0,
                    totalRows: 0
                },
                event: {
                    loadMore: function () {
                        if ($scope.controls.grdSearchResult.status.pageIndex.isTriggered) return;
                        if ($scope.controls.grdSearchResult.status.pageIndex < $scope.controls.grdSearchResult.status.totalPage) {
                            $scope.controls.grdSearchResult.status.pageIndex++;
                            dataService.searchFilter.pageIndex = $scope.controls.grdSearchResult.status.pageIndex;
                            $scope.controls.grdSearchResult.status.isTriggered = true;
                            $scope.controls.grdSearchResult.event.getData();
                        }
                    },
                    sort: function (sortedBy, sortedDirection) {
                        $scope.controls.grdSearchResult.data = [];                        
                        $scope.controls.grdSearchResult.status.pageIndex = 1;
                        dataService.searchFilter.sortedDirection = sortedDirection;
                        dataService.searchFilter.pageIndex = $scope.controls.grdSearchResult.status.pageIndex;
                        dataService.searchFilter.sortedBy = sortedBy;
                        $scope.controls.grdSearchResult.event.getData();
                    },
                    reload: function () {
                        if ($scope.data.status.season !== $scope.controls.grdSearchResult.filters.Season) {
                            $scope.data.status.isLoaded = false;
                            if ($scope.controls.grdSearchResult.filters.ItemTypeID === 3) {
                                if (dataService.searchFilter.sortedBy !== 'CataloguePageNo') {
                                    dataService.searchFilter.sortedBy = 'CataloguePageNo';
                                    dataService.searchFilter.sortedDirection = 'ASC';
                                }
                            }
                        }
                        $scope.controls.grdSearchResult.totalRow.isAdditionalDataLoaded = false;
                        $scope.controls.grdSearchResult.subTotalRow.isAdditionalDataLoaded = false;

                        //console.log('get total');
                        //
                        // get sub total
                        //
                        dataService.getTotalData(
                            $scope.controls.grdSearchResult.filters,
                            function (data) {
                                $scope.controls.grdSearchResult.totalRow = data.data.totalRow;
                                $scope.controls.grdSearchResult.subTotalRow = data.data.subTotalRow;
                            },
                            function (error) {
                                console.log(error);
                            }
                        );

                        $scope.controls.grdSearchResult.data = [];
                        $scope.controls.grdSearchResult.status.pageIndex = 1;
                        $scope.controls.grdSearchResult.status.totalPage = 0;
                        $scope.controls.grdSearchResult.status.totalRows = 0;
                        dataService.searchFilter.pageIndex = $scope.controls.grdSearchResult.status.pageIndex;
                        $scope.controls.grdSearchResult.event.getData();
                    },
                    getData: function () {
                        dataService.searchFilter.filters = $scope.controls.grdSearchResult.filters;
                        if ($scope.controls.grdSearchResult.filters.ItemTypeID === 3) {
                            if (dataService.searchFilter.sortedBy !== 'CataloguePageNo') {
                                dataService.searchFilter.sortedBy = 'CataloguePageNo';
                                dataService.searchFilter.sortedDirection = 'ASC';
                            }
                        }
                        dataService.search(
                            function (data) {
                                if (data.message.type === 0) {
                                    Array.prototype.push.apply($scope.controls.grdSearchResult.data, data.data.data);
                                    $scope.controls.grdSearchResult.status.isTriggered = false;
                                    
                                    if (data.totalRows >= 0) {
                                        $scope.controls.grdSearchResult.status.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                                        $scope.controls.grdSearchResult.status.totalRows = data.totalRows;


                                        $scope.data.status.total = data.data.totalItem;
                                        $scope.data.status.confirmed = data.data.totalConfirmedItem;
                                        $scope.data.status.pending = data.data.totalPendingItem;
                                        $scope.data.status.waitEurofar = data.data.totalWaitForEurofar;
                                        $scope.data.status.waitFactory = data.data.totalWaitForFactory;
                                        $scope.data.status.estimated = data.data.totalEstimated;
                                        $scope.data.status.totalContainer = data.data.totalContainer;
                                        $scope.data.status.totalConfirmedContainer = data.data.totalConfirmedContainer;
                                        $scope.data.status.totalPendingContainer = data.data.totalPendingContainer;
                                        $scope.data.status.totalContainerWaitForEurofar = data.data.totalContainerWaitForEurofar;
                                        $scope.data.status.totalContainerWaitForFactory = data.data.totalContainerWaitForFactory;
                                        $scope.data.status.totalContainerEstimated = data.data.totalContainerEstimated;
                                        $scope.data.status.isLoaded = true;
                                        $scope.data.status.season = $scope.controls.grdSearchResult.filters.Season;

                                        // wait for factory offer conclusion
                                        $scope.data.status.waitForFactoryConlusion = data.data.waitForFactoryConclusionDTOs;
                                        $scope.data.status.waitForFactoryConlusionSummary = {
                                            totalPendingItem: 0,
                                            totalItem: 0,
                                            overDue1Day: 0,
                                            overDue2Day: 0,
                                            overDue3Day: 0,
                                            overDue4DayOrMore: 0
                                        };
                                        if (data.data.waitForFactoryConclusionDTOs && data.data.waitForFactoryConclusionDTOs.length > 0) {
                                            $scope.data.status.waitForFactoryConlusionSummary.totalPendingItem = data.data.waitForFactoryConclusionDTOs.reduce((output, item) => output + item.totalPendingItem, 0);
                                            $scope.data.status.waitForFactoryConlusionSummary.totalItem = data.data.waitForFactoryConclusionDTOs.reduce((output, item) => output + item.totalItem, 0);
                                            $scope.data.status.waitForFactoryConlusionSummary.overDue1Day = data.data.waitForFactoryConclusionDTOs.reduce((output, item) => output + item.overDue1Day, 0);
                                            $scope.data.status.waitForFactoryConlusionSummary.overDue2Day = data.data.waitForFactoryConclusionDTOs.reduce((output, item) => output + item.overDue2Day, 0);
                                            $scope.data.status.waitForFactoryConlusionSummary.overDue3Day = data.data.waitForFactoryConclusionDTOs.reduce((output, item) => output + item.overDue3Day, 0);
                                            $scope.data.status.waitForFactoryConlusionSummary.overDue4DayOrMore = data.data.waitForFactoryConclusionDTOs.reduce((output, item) => output + item.overDue4DayOrMore, 0);
                                        }                                        

                                        // wait for factory production conclusion
                                        $scope.data.status.waitForFactoryProductionConlusion = data.data.waitForFactoryProductionConclusionDTOs;
                                        $scope.data.status.waitForFactoryProductionConlusionSummary = {
                                            totalPendingItem: 0,
                                            overLDS: 0,
                                            lds: 0,
                                            oneToTwoDays: 0,
                                            threeToFourDays: 0,
                                            fiveToSixDays: 0,
                                            moreThanSixDays: 0
                                        };
                                        if (data.data.waitForFactoryProductionConclusionDTOs && data.data.waitForFactoryProductionConclusionDTOs.length > 0) {
                                            $scope.data.status.waitForFactoryProductionConlusionSummary.totalPendingItem = data.data.waitForFactoryProductionConclusionDTOs.reduce((output, item) => output + item.totalPendingItem, 0);
                                            $scope.data.status.waitForFactoryProductionConlusionSummary.overLDS = data.data.waitForFactoryProductionConclusionDTOs.reduce((output, item) => output + item.overLDS, 0);
                                            $scope.data.status.waitForFactoryProductionConlusionSummary.lds = data.data.waitForFactoryProductionConclusionDTOs.reduce((output, item) => output + item.lds, 0);
                                            $scope.data.status.waitForFactoryProductionConlusionSummary.oneToTwoDays = data.data.waitForFactoryProductionConclusionDTOs.reduce((output, item) => output + item.oneToTwoDays, 0);
                                            $scope.data.status.waitForFactoryProductionConlusionSummary.threeToFourDays = data.data.waitForFactoryProductionConclusionDTOs.reduce((output, item) => output + item.threeToFourDays, 0);
                                            $scope.data.status.waitForFactoryProductionConlusionSummary.fiveToSixDays = data.data.waitForFactoryProductionConclusionDTOs.reduce((output, item) => output + item.fiveToSixDays, 0);
                                            $scope.data.status.waitForFactoryProductionConlusionSummary.moreThanSixDays = data.data.waitForFactoryProductionConclusionDTOs.reduce((output, item) => output + item.moreThanSixDays, 0);
                                        }                                        

                                        $scope.factoryEmailAddresses = data.data.emailAddressReceivePriceRequestDTO;
                                    }                                    

                                    $scope.data.exchange = data.data.currentExchangeRate;

                                    //
                                    //get breakdown
                                    //
                                    //$scope.controls.grdSearchResult.eurofarBreakdownOption = data.data.eurofarBreakdownCategoryOptionDTOs;
                                    //$scope.controls.grdSearchResult.eurofarSeasonSpecPercents = data.data.eurofarSeasonSpecPercentDTOs;
                                    //$scope.controls.grdSearchResult.eurofarBreakdownManagementFees = data.data.eurofarBreakdownManagementFeeDTOs;
                                    //Array.prototype.push.apply($scope.controls.grdSearchResult.eurofarBreakdownOption, data.data.eurofarBreakdownCategoryOptionDTOs);
                                    //Array.prototype.push.apply($scope.controls.grdSearchResult.eurofarSeasonSpecPercents, data.data.eurofarSeasonSpecPercentDTOs);
                                    //Array.prototype.push.apply($scope.controls.grdSearchResult.eurofarBreakdownManagementFees, data.data.eurofarBreakdownManagementFeeDTOs);

                                    // show content and trigger full screen mode in the first load
                                    if (!$scope.data.isFirstLoadProcessed) {
                                        $scope.data.isFirstLoadProcessed = true;
                                        $('#content').show();
                                        $('#pnlSearchResult').find('.jarviswidget-fullscreen-btn').click();
                                    }

                                    if ($scope.controls.grdSearchResult.status.pageIndex === 1) {
                                        $('#pnlSearchResult').find('.tilsoft-table-wrapper').scrollTop(0);
                                    }

                                    // query additional data
                                    var ids = '';
                                    angular.forEach(data.data.data, function (item) {
                                        if (ids) {
                                            ids += ',' + item.quotationDetailID;
                                        }
                                        else {
                                            ids += item.quotationDetailID;
                                        }
                                    });
                                    if (ids) {
                                        dataService.getSearchResultAdditionalData(
                                            $scope.controls.grdSearchResult.filters.Season,
                                            ids,
                                            function (data) {
                                                angular.forEach(data.data, function (item) {
                                                    if ($scope.controls.grdSearchResult.data.filter(o => o.quotationDetailID === item.quotationDetailID).length > 0) {
                                                        var currentItem = $scope.controls.grdSearchResult.data.filter(o => o.quotationDetailID === item.quotationDetailID)[0];
                                                        currentItem.purchasingPrice3rdPreviousSeason = item.purchasingPrice3rdPreviousSeason;
                                                        currentItem.purchasingPrice2ndPreviousSeason = item.purchasingPrice2ndPreviousSeason;
                                                        //currentItem.purchasingPricePreviousSeason = item.purchasingPricePreviousSeason;
                                                        currentItem.factoryPrice1 = item.factoryPrice1;
                                                        currentItem.factoryPrice2 = item.factoryPrice2;
                                                        currentItem.factoryPrice3 = item.factoryPrice3;
                                                        currentItem.targetPrice1 = item.targetPrice1;
                                                        currentItem.targetPrice2 = item.targetPrice2;
                                                        currentItem.targetPrice3 = item.targetPrice3;
                                                        currentItem.soonestShippedDate = item.soonestShippedDate;
                                                        currentItem.avfBreakdownAmount = item.avfBreakdownAmount;
                                                        currentItem.avtBreakdownAmount = item.avtBreakdownAmount;
                                                        currentItem.isAdditionalDataLoaded = true;
                                                    }
                                                });
                                                $scope.controls.grdSearchResult.totalRow.isAdditionalDataLoaded = true;
                                                $scope.controls.grdSearchResult.subTotalRow.isAdditionalDataLoaded = true;
                                            },
                                            function (error) {
                                                console.log(error);
                                            }
                                        );
                                    }
                                }
                                else {
                                    console.log(data.message);
                                }
                            },
                            function (error) {
                                $scope.controls.grdSearchResult.status.isTriggered = false;
                            }
                        );
                    },
                    clearFilter: function () {
                        $scope.controls.grdSearchResult.filters = {
                            Season: jsHelper.getCurrentSeason(),
                            ClientNM: '',
                            Description: '',
                            FactoryUD: '',
                            ItemTypeID: 1,
                            StatusID: 1,
                            QuotationOfferDirectionID: 1,
                            ProformaInvoiceNo: '',
                            OrderTypeID: '',
                            BusinessDataStatusID: null,
                            ShippedStatus: null,
                            LDSFrom: null,
                            LDSTo: null,
                            CataloguePageNo: null,
                            PricingTeamMemberID: null
                        };
                        $scope.controls.grdSearchResult.event.reload();
                    },
                    getTotal: function () {
                        dataService.getTotalData(
                            $scope.controls.grdSearchResult.filters,
                            function (data) {
                                if (data.message.type === 0) {
                                    $scope.controls.grdSearchResult.totalRow = data.data.totalRow;
                                    $scope.controls.grdSearchResult.subTotalRow = data.data.subTotalRow;
                                }
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error.message.message);
                            }
                        );
                    }                                  
                },
                method: {
                    getRowBackgroundColor: function (item) {
                        // missing purchasing price & load ability
                        if (item.priceIncludeDiff === null && (!item.qnt40HC || item.qnt40HC === 0)) {
                            return '#ffffab';
                        }

                        // missing load ability
                        if (!item.qnt40HC || item.qnt40HC === 0) {
                            return '#bfe7ff';
                        }

                        // missing purchasing price
                        if (item.priceIncludeDiff === null) {
                            return '#cab1ff';
                        }

                        // margin after < 0
                        if (item.marginAfterUSD && parseFloat(item.marginAfterUSD) < 0) {
                            return '#ececec';
                        }
                        
                        return '#ffffff';
                    }
                }
            }
        };



        //
        // angular init
        //
        $timeout(function () {
            $scope.event.init();
        }, 0);
    }]);

})();