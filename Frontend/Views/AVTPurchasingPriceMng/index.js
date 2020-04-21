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
    angular.module('tilsoftApp').controller('tilsoftController', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
        dataService.searchFilter.pageSize = context.pageSize;
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;

        //
        // properties
        //
        $scope.data = {
            exchange: 1,
            quotationStatuses: null,
            seasons: jsHelper.getSeasons(),
            itemTypes: [
                { id: 1, name: 'PRODUCT' },
                { id: 0, name: 'SPAREPART' },
                { id: 2, name: 'SAMPLE' },
                { id: 3, name: 'MODEL' }
            ],
            businessDataStatuses: [
                { id: 2, name: 'MISSING PURCHASING PRICE & LOAD ABILITY' },
                { id: 3, name: 'MISSING PURCHASING PRICE' },
                { id: 4, name: 'MISSING LOAD ABILITY' }
            ],
            shippedStatuses: [
                { id: 0, name: 'NOT YET SHIPPED' },
                { id: 1, name: 'FULLY/PARTIALLY SHIPPED' },
            ],
            waitStatuses: [
                { id: 1, name: 'WAIT FOR EUROFAR' },
                { id: 2, name: 'WAIT FOR FACTORY' }
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

                totalContainer: null,
                totalConfirmedContainer: null,
                totalPendingContainer: null,
                totalContainerWaitForEurofar: null,
                totalContainerWaitForFactory: null,

                season: null,
                isLoaded: false
            }
        };

        //
        // events
        //
        $scope.event = {
            init: function () {
                dataService.getSearchFilter(
                    function (data) {
                        $scope.data.quotationStatuses = data.data.quotationStatusDTOs;
                        $scope.data.avtSupportBreakdownCategorys = data.data.avtSupportBreakdownCategoryDTOs;
                        $scope.vnD_To_USD_ExchangeRate = data.data.exchange;
                        $scope.controls.grdSearchResult.event.getData();
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
                            //});
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
                    item.isSelected = $scope.data.isSelectAll;
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
                if ($scope.controls.grdSearchResult.avtBreakdownOption !== null && $scope.controls.grdSearchResult.avtBreakdownOption !== undefined) {
                    breakdownCategoryOptions = $scope.controls.grdSearchResult.avtBreakdownOption.filter(o => o.breakdownCategoryID === breakdownCategoryID && o.modelID === dataSearch.modelID);
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
                                    costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.vnD_To_USD_ExchangeRate, 2);
                                }
                                //get AVT Price
                                companyID = 3;
                                selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.materialID === materialID && o.materialTypeID === materialTypeID && o.materialColorID === materialColorID);
                                if (selectedBreakdownOption.length > 0) {
                                    costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                                    costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.vnD_To_USD_ExchangeRate, 2);
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
                                    costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.vnD_To_USD_ExchangeRate, 2);
                                }
                                //get AVT price
                                companyID = 3;
                                selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.frameMaterialID === frameMaterialID && o.frameMaterialColorID === frameMaterialColorID);
                                if (selectedBreakdownOption.length > 0) {
                                    costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                                    costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.vnD_To_USD_ExchangeRate, 2);
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
                                    costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.vnD_To_USD_ExchangeRate, 2);
                                }
                                //get AVT price
                                companyID = 3;
                                selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.subMaterialID === subMaterialID && o.subMaterialColorID === subMaterialColorID);
                                if (selectedBreakdownOption.length > 0) {
                                    costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                                    costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.vnD_To_USD_ExchangeRate, 2);
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
                            costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.vnD_To_USD_ExchangeRate, 2);
                        }
                        //get AVT price
                        companyID = 3;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.cushionColorID === cushionColorID && o.backCushionID === backCushionID && o.seatCushionID === seatCushionID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                            costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.vnD_To_USD_ExchangeRate, 2);
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
                            costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.vnD_To_USD_ExchangeRate, 2);
                        }
                        //get AVT price
                        companyID = 3;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.packagingMethodID === packagingMethodID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                            costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.vnD_To_USD_ExchangeRate, 2);
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
                            costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.vnD_To_USD_ExchangeRate, 2);
                        }
                        //get AVT price
                        companyID = 3;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.fscTypeID === fscTypeID && o.fscPercentID === fscPercentID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                            costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.vnD_To_USD_ExchangeRate, 2);
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
                            costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.vnD_To_USD_ExchangeRate, 2);
                        }
                        //get AVT price
                        companyID = 3;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID /*&& o.offerLineID === offerLineID*/);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                            costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.vnD_To_USD_ExchangeRate, 2);
                        }
                        return costPrice;

                    case 6: case 7: case 10: //HARDWARE COST,OTHER MATERIAL COST,MANAGEMENT FEE
                        //no Option to Get
                        //get AVF price
                        companyID = 1;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                            costPrice.avfPriceUSD = jsHelper.round(costPrice.avfPrice / $scope.vnD_To_USD_ExchangeRate, 2);
                        }
                        //get AVT price
                        companyID = 3;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                            costPrice.avtPriceUSD = jsHelper.round(costPrice.avtPrice / $scope.vnD_To_USD_ExchangeRate, 2);
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
                if ($scope.controls.grdSearchResult.avtBreakdownManagementFees !== null && $scope.controls.grdSearchResult.avtBreakdownManagementFees !== undefined && $scope.controls.grdSearchResult.avtBreakdownManagementFees.length > 0) {

                    if ($scope.controls.grdSearchResult.avtBreakdownOption !== null && $scope.controls.grdSearchResult.avtBreakdownOption !== undefined) {             
                        breakdownCategoryOptions = $scope.controls.grdSearchResult.avtBreakdownOption.filter(o => o.modelID === data.modelID);
                    }
                    if (breakdownCategoryOptions !== null && breakdownCategoryOptions !== undefined && breakdownCategoryOptions.length > 0) {
                        for (var i = 0; i < $scope.controls.grdSearchResult.avtBreakdownManagementFees.length; i++) {
                            var itemManagementFee = $scope.controls.grdSearchResult.avtBreakdownManagementFees[i];
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
                return managementFeePercent;
            },

            getseasonSpecPercent: function (data) {
                var seasonSpecPercent = 0;
                //var breakdownCategoryOptions = [];

                if ($scope.controls.grdSearchResult.avtSeasonSpecPercents !== null && $scope.controls.grdSearchResult.avtSeasonSpecPercents !== undefined && $scope.controls.grdSearchResult.avtSeasonSpecPercents.length > 0) {
                    if ($scope.controls.grdSearchResult.avtBreakdownOption !== null && $scope.controls.grdSearchResult.avtBreakdownOption !== undefined) {
                        var breakdownCategoryOptions = $scope.controls.grdSearchResult.avtBreakdownOption.filter(o => o.modelID === data.modelID);
                    }
                    if (breakdownCategoryOptions !== null && breakdownCategoryOptions !== undefined ) {
                        for (var i = 0; i < $scope.controls.grdSearchResult.avtSeasonSpecPercents.length; i++) {
                            var itemseasonSpecPercent = $scope.controls.grdSearchResult.avtSeasonSpecPercents[i];
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
                if ($scope.controls.grdSearchResult.avtBreakdownOption !== null && $scope.controls.grdSearchResult.avtBreakdownOption !== undefined) {
                    var breakdownCategoryOptions = $scope.controls.grdSearchResult.avtBreakdownOption.filter(o => o.modelID === data.modelID && o.breakdownCategoryID === 5);
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
                            shippingFee.shippingFeeAVFUSD = jsHelper.round(parseFloat(shippingFee.shippingFeeAVFVND) / $scope.vnD_To_USD_ExchangeRate, 2);

                            companyID = 3;
                            if (data.exportCost40HC > 0 && itemBreakdownCategoryOptions.loadAbilityAVT > 0 && itemBreakdownCategoryOptions.companyID === 3) {
                                shippingFee.shippingFeeAVTVND = jsHelper.round(parseFloat(data.exportCost40HC) / parseFloat(itemBreakdownCategoryOptions.loadAbilityAVT), 2);
                            }
                            shippingFee.shippingFeeAVTUSD = jsHelper.round(parseFloat(shippingFee.shippingFeeAVTVND) / $scope.vnD_To_USD_ExchangeRate, 2);
                        }
                    }
                }
                return shippingFee;
            }
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
       
            //ForBreakdown
            getTotalCostPriceAVT: function (data) {
                var totalCostPrice = {
                    avfPrice: parseFloat(0),
                    avtPrice: parseFloat(0),
                    avfManagementFee: parseFloat(0),
                    avtManagementFee: parseFloat(0)
                };
                //get breakdown category
                var breakdownCategories = [];
                if ($scope.data.avtSupportBreakdownCategorys !== null && $scope.data.avtSupportBreakdownCategorys !== undefined) {
                    breakdownCategories = angular.copy($scope.data.avtSupportBreakdownCategorys);
                }
                //debugger;
                ////cal total price
                angular.forEach(breakdownCategories, function (item) {
                    var costPrice = $scope.event.getCostPrice(item.breakdownCategoryID, data);
                    //totalCostPrice.avfPrice += parseFloat(costPrice.avfPrice);
                    totalCostPrice.avtPrice += parseFloat(costPrice.avtPrice);
                });
                //management fee amount base on percent
                var managementFeePercent = $scope.event.getManagementFeePercent(data);
                //totalCostPrice.avtManagementFee = totalCostPrice.avtPrice * managementFeePercent / 100;
                //totalCostPrice.avtPrice = totalCostPrice.avtPrice + totalCostPrice.avtManagementFee;

                //total cost price after management fee
                totalCostPrice.avtPrice = totalCostPrice.avtPrice * (1 + managementFeePercent.avtManagement / 100);

                //total cost amount base on SeasonSpecPercent
                var seasonSpecPercent = $scope.event.getseasonSpecPercent(data);
                //totalCostPrice.avfPrice = totalCostPrice.avfPrice * (1 + seasonSpecPercent / 100);
                totalCostPrice.avtPrice = totalCostPrice.avtPrice * (1 + seasonSpecPercent / 100);

                var shippingFee = $scope.event.getShippingFee(data);
                totalCostPrice.avtPrice = totalCostPrice.avtPrice + shippingFee.shippingFeeVND;

                return totalCostPrice.avtPrice;
            },

            getTotalCostPriceAVF: function (data) {
                var totalCostPrice = {
                    avfPrice: parseFloat(0),
                    avtPrice: parseFloat(0),
                    avfManagementFee: parseFloat(0),
                    avtManagementFee: parseFloat(0)
                };
                //get breakdown category
                var breakdownCategories = [];
                if ($scope.data.avtSupportBreakdownCategorys !== null && $scope.data.avtSupportBreakdownCategorys !== undefined) {
                    breakdownCategories = angular.copy($scope.data.avtSupportBreakdownCategorys);
                }
                //debugger;
                ////cal total price
                angular.forEach(breakdownCategories, function (item) {
                    var costPrice = $scope.event.getCostPrice(item.breakdownCategoryID, data);
                    totalCostPrice.avfPrice += parseFloat(costPrice.avfPrice);
                    //totalCostPrice.avtPrice += parseFloat(costPrice.avtPrice);
                });
                //////management fee amount base on percent
                var managementFeePercent = $scope.event.getManagementFeePercent(data);
                //totalCostPrice.avfManagementFee = totalCostPrice.avfPrice * managementFeePercent / 100;

                //total cost price after management fee
                //totalCostPrice.avtPrice = totalCostPrice.avtPrice * (1 + managementFeePercent / 100);
                totalCostPrice.avfPrice = totalCostPrice.avfPrice * (1 + managementFeePercent.avfManagement / 100);

                //total cost amount base on SeasonSpecPercent
                var seasonSpecPercent = $scope.event.getseasonSpecPercent(data);
                totalCostPrice.avfPrice = totalCostPrice.avfPrice * (1 + seasonSpecPercent / 100);
                ////totalCostPrice.avtPrice = totalCostPrice.avtPrice * (1 + seasonSpecPercent / 100);

                var shippingFee = $scope.event.getShippingFee(data);
                totalCostPrice.avfPrice = totalCostPrice.avfPrice + shippingFee.shippingFeeVND;

                return totalCostPrice.avfPrice;
            },

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
                if ($scope.data.avtSupportBreakdownCategorys !== null && $scope.data.avtSupportBreakdownCategorys !== undefined) {
                    breakdownCategories = angular.copy($scope.data.avtSupportBreakdownCategorys);
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
                if ($scope.data.avtSupportBreakdownCategorys !== null && $scope.data.avtSupportBreakdownCategorys !== undefined) {
                    breakdownCategories = angular.copy($scope.data.avtSupportBreakdownCategorys);
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
            }
        };

        //
        // controls
        //
        $scope.controls = {
            grdSearchResult : {
                data: [],
                filters: {
                    Season: jsHelper.getCurrentSeason(),
                    ClientNM: '',
                    Description: '',
                    FactoryUD: '',
                    ItemTypeID: 1,
                    StatusID: 1,
                    QuotationOfferDirectionID: 1,
                    ProformaInvoiceNo: '',
                    BusinessDataStatusID: null,
                    ShippedStatus: null,
                    LDSFrom: null,
                    LDSTo: null
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
                        $scope.controls.grdSearchResult.data = [];
                        $scope.controls.grdSearchResult.status.pageIndex = 1;
                        $scope.controls.grdSearchResult.status.totalPage = 0;
                        $scope.controls.grdSearchResult.status.totalRows = 0;
                        dataService.searchFilter.pageIndex = $scope.controls.grdSearchResult.status.pageIndex;
                        $scope.controls.grdSearchResult.event.getData();
                    },
                    getData: function () {
                        dataService.searchFilter.filters = $scope.controls.grdSearchResult.filters;
                        dataService.search(
                            function (data) {
                                Array.prototype.push.apply($scope.controls.grdSearchResult.data, data.data.data);
                                $scope.controls.grdSearchResult.status.isTriggered = false;

                                if (data.totalRows > 0) {
                                    $scope.controls.grdSearchResult.status.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                                    $scope.controls.grdSearchResult.status.totalRows = data.totalRows;

                                    $scope.data.status.total = data.data.totalItem;
                                    $scope.data.status.confirmed = data.data.totalConfirmedItem;
                                    $scope.data.status.pending = data.data.totalPendingItem;
                                    $scope.data.status.waitEurofar = data.data.totalWaitForEurofar;
                                    $scope.data.status.waitFactory = data.data.totalWaitForFactory;

                                    $scope.data.status.totalContainer = data.data.totalContainer;
                                    $scope.data.status.totalConfirmedContainer = data.data.totalConfirmedContainer;
                                    $scope.data.status.totalPendingContainer = data.data.totalPendingContainer;
                                    $scope.data.status.totalContainerWaitForEurofar = data.data.totalContainerWaitForEurofar;
                                    $scope.data.status.totalContainerWaitForFactory = data.data.totalContainerWaitForFactory;
                                }

                                $scope.data.exchange = data.data.exchange;

                                //$scope.controls.grdSearchResult.avtBreakdownOption = [];
                                //$scope.controls.grdSearchResult.avtBreakdownOption = data.data.avtBreakdownCategoryOptionDTOs;
                                //$scope.controls.grdSearchResult.avtSeasonSpecPercents = [];
                                //$scope.controls.grdSearchResult.avtSeasonSpecPercents = data.data.avtSeasonSpecPercentDTOs;
                                //$scope.controls.grdSearchResult.avtBreakdownManagementFees = [];
                                //$scope.controls.grdSearchResult.avtBreakdownManagementFees = data.data.avtBreakdownManagementFeeDTOs;

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
                                                   
                                                    currentItem.avfBreakdownAmount = item.avfBreakdownAmount;
                                                    currentItem.avtBreakdownAmount = item.avtBreakdownAmount;
                                                    currentItem.avfBreakdownLoadability = item.avfBreakdownLoadability;
                                                    currentItem.avtBreakdownLoadability = item.avtBreakdownLoadability;
                                                    currentItem.isAdditionalDataLoaded = true;
                                                }
                                            });
                                        },
                                        function (error) {
                                            console.log(error);
                                        }
                                    );
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
                            BusinessDataStatusID: null,
                            ShippedStatus: null,
                            LDSFrom: null,
                            LDSTo: null
                        };
                        $scope.controls.grdSearchResult.event.reload();
                    },
                    exportExcel: function () {
                        dataService.exportExcel(
                            {
                                filters: {
                                    Season: scope.controls.grdSearchResult.filters.Season,
                                    ClientNM: scope.controls.grdSearchResult.filters.ClientNM,
                                    Description: scope.controls.grdSearchResult.filters.Description,
                                    FactoryUD: scope.controls.grdSearchResult.filters.FactoryUD,
                                    ItemTypeID: scope.controls.grdSearchResult.filters.ItemTypeID,
                                    StatusID: scope.controls.grdSearchResult.filters.StatusID,
                                    QuotationOfferDirectionID: scope.controls.grdSearchResult.filters.QuotationOfferDirectionID,
                                    ProformaInvoiceNo: scope.controls.grdSearchResult.filters.ProformaInvoiceNo,
                                    BusinessDataStatusID: scope.controls.grdSearchResult.filters.BusinessDataStatusID,
                                    ShippedStatus: scope.controls.grdSearchResult.filters.ShippedStatus,
                                    LDSFrom: scope.controls.grdSearchResult.filters.LDSFrom,
                                    LDSTo: scope.controls.grdSearchResult.filters.LDSTo
                                }
                            },
                            function (data) {
                                jsHelper.processMessage(data.message);
                                if (data.message.type !== 2) {
                                    window.location = context.backendReportUrl + data.data;
                                }
                            },
                            function (erorr) {
                                jsHelper.showMessage('warning', error);
                            }
                        );
                    },
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