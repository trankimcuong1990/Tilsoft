$(document).keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element(jQuery('#partial-content')).scope();
        scope.localFilterItem();
        scope.$apply();
    }
});
tilsoftApp.controller('_EditFobProductForm', ['$scope', '$timeout', '$cookieStore', 'dataService', '$routeParams', '$location', function ($scope, $timeout, $cookieStore, dataService, $routeParams, $location) {   
    //
    //property
    //
    $scope.baseRouter = $location.path().split('/')[1];

    //
    //init controller
    //
    $scope.initController = function () {
        //
        // init service
        //
        dataService.serviceUrl = context.serviceUrl;
        dataService.supportServiceUrl = context.supportServiceUrl;
        dataService.token = context.token;

        //
        //load data
        //
        if ($scope.data === null) {
            $scope.load(function () {
                //calculate value that we calculate realtime, this make us can sort for these column
                angular.forEach($scope.data.offerSeasonDetailDTOs, function (item) {
                    $scope.reCalculateValue(item);
                });

                //handle sort feature
                //$scope.sortHandler.orderByColumn = 'deltaPercent';
                $scope.data.offerSeasonDetailDTOs.sort(function (a, b) {
                    return a["deltaPercent"] > b["deltaPercent"] ? 1 : -1;
                });

                //
                //lazy load sale price last season
                //
                $scope.getSalePriceTableLastSeason();

                //
                //load purchasing price last year (current supplier)
                //
                $scope.getPurchasingPriceLastYear();
               
                //
                //load %delta from module MIDeltaByClientRpt
                //
                $scope.getDeltaByClient();

                //
                //load data to check offer item is in factory order
                //
                $scope.getRelatedFactoryOrderDetail();

                //
                //load image gallery
                //
                $scope.getImageGallery();
            });            
        }
        else {
            //re-calculate value if user change purchasing price
            //this make us can sort for these column
            angular.forEach($scope.data.offerSeasonDetailDTOs, function (item) {
                $scope.reCalculateValue(item);
            });
        }

        //
        //get param router and init filter
        //
        if (context.id > 0) {
            var itemStatus = $routeParams.itemStatus;
            var description = $routeParams.description;
            var hasQuantity = $routeParams.hasQuantity;
            var isClientSelected = $routeParams.isClientSelected;

            $scope.localFilter.itemStatusSearchQuery = !itemStatus || itemStatus === "-1" ? null : parseInt(itemStatus);
            $scope.localFilter.productSearchQuery = !description || description === 'allofferitem' ? '' : description;
            $scope.localFilter.hasQuantity = !hasQuantity || hasQuantity === "-1" ? null : JSON.parse(hasQuantity);
            if (!isClientSelected) {
                $scope.localFilter.isClientSelected = true;
            }
            else if (isClientSelected === "-1") {
                $scope.localFilter.isClientSelected = null;
            }
            else {
                $scope.localFilter.isClientSelected = JSON.parse(isClientSelected);
            }
            $scope.localFilterItem();
        }

        //
        //go to position before
        //
        $scope.goToCurrentScrollPosition();        
    };

    $scope.productGridHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
            if (sortedDirection === 'asc') {
                $scope.data.offerSeasonDetailDTOs.sort(function (a, b) {
                    return a[sortedBy] > b[sortedBy] ? 1 : -1;
                });
            }
            else if (sortedDirection === 'desc') {
                $scope.data.offerSeasonDetailDTOs.sort(function (a, b) {
                    return a[sortedBy] < b[sortedBy] ? 1 : -1;
                });
            }
            $scope.$apply();
        },
        isTriggered: false, 
        getScrollPosistion: function (topValue, leftValue) {
            $scope.pagePosition.gridProductTop = topValue;
            $scope.pagePosition.gridProductLeft = leftValue;
        }
    };

    //
    //selected tab
    //
    $scope.selectedTab = function (tabName) {        
        //remove all active class of tab
        $("#tab-content-detail > div").removeClass("active");
        //add active class to selected tab
        $("#" + tabName).addClass("active");   
        $(".tab-content-header > li").removeClass("active");        
    };

    $scope.reCalculateValue = function (offerSeasonDetailItem) {
        //
        //this function only make for sort feature
        //
        offerSeasonDetailItem.orderQnt40HC = offerSeasonDetailItem.qnt40HC !== null && offerSeasonDetailItem.qnt40HC !== 0 ? offerSeasonDetailItem.quantity / offerSeasonDetailItem.qnt40HC : 0;
        offerSeasonDetailItem.deltaPercent = $scope.deltaPercent(offerSeasonDetailItem);
        offerSeasonDetailItem.deltaAmount = $scope.delta(offerSeasonDetailItem) * offerSeasonDetailItem.quantity;

        offerSeasonDetailItem.purchasingAmount = $scope.purchasingPriceInUSD(offerSeasonDetailItem) * offerSeasonDetailItem.quantity;
        offerSeasonDetailItem.saleAmountInUSD = $scope.salePriceInUSD(offerSeasonDetailItem) * offerSeasonDetailItem.quantity;
        offerSeasonDetailItem.saleAmount = offerSeasonDetailItem.salePrice * offerSeasonDetailItem.quantity;
        offerSeasonDetailItem.vatAmount = offerSeasonDetailItem.salePrice * offerSeasonDetailItem.quantity * $scope.data.offerSeasonDTO.vat / 100;
        offerSeasonDetailItem.saleAmountIncludeVAT = offerSeasonDetailItem.salePrice * offerSeasonDetailItem.quantity * (1 + $scope.data.offerSeasonDTO.vat / 100);

        offerSeasonDetailItem.commissionInUSD = $scope.commissionInUSD(offerSeasonDetailItem) * offerSeasonDetailItem.quantity;
        offerSeasonDetailItem.lcCostInUSD = $scope.lcCostInUSD(offerSeasonDetailItem) * offerSeasonDetailItem.quantity;
        offerSeasonDetailItem.interestInUSD = $scope.interestInUSD(offerSeasonDetailItem) * offerSeasonDetailItem.quantity;
        offerSeasonDetailItem.damagesCostInUSD = $scope.damagesCostInUSD(offerSeasonDetailItem) * offerSeasonDetailItem.quantity;
        offerSeasonDetailItem.transportationInUSD = $scope.transportationInUSD(offerSeasonDetailItem) * offerSeasonDetailItem.quantity;
    };

    $scope.getItemStatusText = function (itemStatus) {
        var statuses = $scope.data.offerSeasonItemStatusDTOs.filter(o => o.itemStatus === itemStatus);
        if (statuses.length > 0) {
            return statuses[0].itemStatusText;
        }
        return '';
    };

    $scope.getLastSeason = function () {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null) return '';
        return jsHelper.getPrevSeason($scope.data.offerSeasonDTO.season);
    };
        
    //
    //filter
    //
    $scope.localFilterItem = function () {
        $scope.filterItem();
        $scope.editUrl();
    };

    $scope.localClearFilter = function () {
        $scope.clearFilter();
        $scope.editUrl();
    };

    $scope.editUrl = function () {
        //edit url so sale can send this url to manager to approve price        
        window.history.pushState({}, null, "/OfferSeasonMng/Edit/" + context.id + $scope.getRouterWithParam($scope.baseRouter));
    };

    //
    //event on ui
    //
    $scope.changeItem = function (offerSeasonDetailItem) {
        offerSeasonDetailItem.isEditing = true;
    };

    $scope.changeQnt = function (offerSeasonDetailItem) {
        $scope.reCalculateValue(offerSeasonDetailItem);
        offerSeasonDetailItem.isEditing = true;
    };
                  
    $scope.changeSalePrice = function (offerSeasonDetailItem) {        
        var salePrice = parseFloat(offerSeasonDetailItem.salePrice === null || offerSeasonDetailItem.salePrice === "" ? 0 : offerSeasonDetailItem.salePrice);
        var planingPurchasingPrice = parseFloat(offerSeasonDetailItem.planingPurchasingPrice === null || offerSeasonDetailItem.planingPurchasingPrice === "" ? 0 : offerSeasonDetailItem.planingPurchasingPrice);
        
        if (offerSeasonDetailItem.itemStatus === 1 || offerSeasonDetailItem.itemStatus === 2) {//1: PENDING, 2:NEED MAKE QUOTATION  
            if (offerSeasonDetailItem.offerSeasonDetailID < 0) {
                if (salePrice !== 0) {
                    offerSeasonDetailItem.itemStatus = 2; //NEED MAKE QUOTATION  
                }
                else {
                    offerSeasonDetailItem.itemStatus = 1; //PENDING
                }                
            }
            offerSeasonDetailItem.isEditing = true;
        }
        
        if (offerSeasonDetailItem.itemStatus === 3) {
            //if (salePrice !== 0 && planingPurchasingPrice !== 0) {
            //    offerSeasonDetailItem.itemStatus = 4; //NEED APPROVAL
            //    offerSeasonDetailItem.isEditing = true;
            //}
            //else {
            //    offerSeasonDetailItem.itemStatus = 3;
            //    offerSeasonDetailItem.isEditing = false;
            //}
            if (salePrice) {
                offerSeasonDetailItem.isEditing = true;
            }
        }

        if (offerSeasonDetailItem.itemStatus === 4) {            
            if (salePrice === 0) {
                offerSeasonDetailItem.itemStatus = 3;
            }
            offerSeasonDetailItem.isEditing = true;
        }

        if (offerSeasonDetailItem.itemStatus === 6) {
            //if (salePrice !== 0 && planingPurchasingPrice !== 0) {
            //    offerSeasonDetailItem.itemStatus = 4; //NEED APPROVAL
            //    offerSeasonDetailItem.isEditing = true;
            //}
            //else {
            //    offerSeasonDetailItem.itemStatus = 6;
            //    offerSeasonDetailItem.isEditing = false;
            //}
            if (salePrice) {
                offerSeasonDetailItem.isEditing = true;
            }
        }

        //refresh delta
        $scope.reCalculateValue(offerSeasonDetailItem);
    };

    $scope.exportDetail = function () {
        if (context.id) {
            dataService.exportDetail(
                context.id,
                function (data) {
                    if (data.message.type === 0) {
                        window.location = context.reportUrl + data.data;
                    }
                    else {
                        jsHelper.processMessageEx(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    };

    $scope.needFactoryQuotationChanged = function (offerSeasonDetailItem) {
        offerSeasonDetailItem.isEditing = true;
        offerSeasonDetailItem.markAsNeedFactoryQuotation = true;

        offerSeasonDetailItem.needFactoryQuotationName = 'you';
        offerSeasonDetailItem.needFactoryQuotationDate = 'just now';
    };

    $scope.showRemarkHistory = function (offerSeasonDetailItem) {
        $scope.remarks = offerSeasonDetailItem.offerSeasonDetailRemarkDTOs;
    };

    $scope.showPriceHistory = function (offerSeasonDetailItem) {
        $scope.priceHistories = offerSeasonDetailItem.offerSeasonDetailPriceHistoryDTOs;
    };

    $scope.clientSelectedChanged = function (offerSeasonDetailItem) {
        offerSeasonDetailItem.isEditing = true;
        offerSeasonDetailItem.markAsClientSelected = true;

        offerSeasonDetailItem.clientSelectorName = 'you';
        offerSeasonDetailItem.clientSelectedDate = 'just now';
    };

    //
    //approve item
    //
    $scope.countItemNeedApprove = function () {
        if ($scope.data === null || $scope.data.offerSeasonDetailDTOs === null) return 0;
        var countItem = $scope.data.offerSeasonDetailDTOs.filter(o => o.itemStatus === 4).length;
        return countItem;
    };

    $scope.approveItem = function (offerSeasonDetailItem) {
        //set offer detail item is approved
        offerSeasonDetailItem.itemStatus = 5;
        offerSeasonDetailItem.markAsApproved = true;

        //approve item
        $scope.updateOfferSeasonDetail($scope.data.offerSeasonDTO.offerSeasonID, offerSeasonDetailItem.offerSeasonDetailID, offerSeasonDetailItem);        
    };

    $scope.approveAllItem = function () {
        var offerSeasonID = $scope.data.offerSeasonDTO.offerSeasonID;
        var dtoOfferSeasonDetails = [];
        if (!offerSeasonID > 0) {
            bootbox.alert("Please save general info of offer before save items !!!");
            return;
        }
        //get edit item to update
        var offerSeasonDetailDTOs = $scope.data.offerSeasonDetailDTOs.filter(o => o.itemStatus === 4);
        angular.forEach(offerSeasonDetailDTOs, function (item) {
            //set status in approved status
            item.itemStatus = 5;
            item.markAsApproved = true;

            //tracking row to return after update
            item.rowID = item.offerSeasonDetailID;

            //get rows to update
            dtoOfferSeasonDetails.push(item);
        });

        //update offer detail
        dataService.updateOfferSeasonDetail2(
            offerSeasonID,
            dtoOfferSeasonDetails,
            function (data) {
                if (data.message.type !== 0) {
                    //show error
                    jsHelper.processMessage(data.message);
                }
                //update back to frontend
                angular.forEach(offerSeasonDetailDTOs, function (item) {
                    var returnItem = data.data.filter(o => o.rowID === item.rowID);
                    if (returnItem.length > 0) {
                        //refresh dto detail
                        jsHelper.autoMapper(returnItem[0], item);
                    }
                });
            },
            function (error) {
                //do need do nothing
            }
        );
    };

    //
    //un-approve item
    //
    $scope.unApproveItem = function (offerSeasonDetailItem) {
        //set offer detail item is to status NEED-APPROVE
        offerSeasonDetailItem.itemStatus = 4;
        offerSeasonDetailItem.markAsUnApproved = true;

        //approve item
        $scope.updateOfferSeasonDetail($scope.data.offerSeasonDTO.offerSeasonID, offerSeasonDetailItem.offerSeasonDetailID, offerSeasonDetailItem);
    };

    //
    //reject item
    //
    $scope.reject = function (offerSeasonDetailItem) {
        offerSeasonDetailItem.itemStatus = 6;
        $scope.updateOfferSeasonDetail($scope.data.offerSeasonDTO.offerSeasonID, offerSeasonDetailItem.offerSeasonDetailID, offerSeasonDetailItem);
    };

    //
    //ask for approval
    //
    $scope.countItemAskForApproval = function () {
        if ($scope.data === null || $scope.data.offerSeasonDetailDTOs === null) return 0;
        var countItem = $scope.data.offerSeasonDetailDTOs.filter(o => $scope.data.offerSeasonDTO.isClientEstimatedAdditionalCostConfirmed === true && o.offerSeasonDetailID > 0 && o.itemStatus !== 4 && o.itemStatus !== 5 && o.quantity && o.salePrice && o.planingPurchasingPrice).length;
        return countItem;
    };

    $scope.askForApprovalItem = function (offerSeasonDetailItem) {
        if ($scope.data.offerSeasonDTO.isClientEstimatedAdditionalCostConfirmed === true && offerSeasonDetailItem.offerSeasonDetailID > 0 && offerSeasonDetailItem.itemStatus !== 4 && offerSeasonDetailItem.itemStatus !== 5 && offerSeasonDetailItem.quantity && offerSeasonDetailItem.salePrice && offerSeasonDetailItem.planingPurchasingPrice) {
            offerSeasonDetailItem.itemStatus = 4;
            $scope.updateOfferSeasonDetail($scope.data.offerSeasonDTO.offerSeasonID, offerSeasonDetailItem.offerSeasonDetailID, offerSeasonDetailItem);
        }        
    };

    $scope.askForApprovalAllItem = function () {
        var askApprovalItems = $scope.data.offerSeasonDetailDTOs.filter(o => $scope.data.offerSeasonDTO.isClientEstimatedAdditionalCostConfirmed === true && o.offerSeasonDetailID > 0 && o.itemStatus !== 4 && o.itemStatus !== 5 && o.quantity && o.salePrice && o.planingPurchasingPrice);
        angular.forEach(askApprovalItems, function (item) {
            $scope.askForApprovalItem(item);
        });
    };
       
    //
    //sale price last season
    //
    $scope.getSalePriceTable = function (offerDetailItem) {
        //get properties
        var properties = {
            clientID: $scope.data.offerSeasonDTO.clientID,
            modelID: offerDetailItem.modelID,
            materialID: offerDetailItem.materialID,
            materialTypeID: offerDetailItem.materialTypeID,
            materialColorID: offerDetailItem.materialColorID,
            frameMaterialID: offerDetailItem.frameMaterialID,
            frameMaterialColorID: offerDetailItem.frameMaterialColorID,
            subMaterialID: offerDetailItem.subMaterialID,
            subMaterialColorID: offerDetailItem.subMaterialColorID,
            seatCushionID: offerDetailItem.seatCushionID,
            backCushionID: offerDetailItem.backCushionID,
            cushionColorID: offerDetailItem.cushionColorID,
            fscTypeID: offerDetailItem.fscTypeID,
            fscPercentID: offerDetailItem.fscPercentID,

            packagingMethodID: offerDetailItem.packagingMethodID,
            clientSpecialPackagingMethodID: offerDetailItem.clientSpecialPackagingMethodID,
            season: $scope.data.offerSeasonDTO.season,
            currency: $scope.data.offerSeasonDTO.currency
        };
        dataService.getSalePriceTable(
            properties,
            function (data) {                
                $scope.salePriceTable = data.data;                
            },
            function (error) {
            }
        ); 
    };

    $scope.getSalePriceTableLastSeason = function () {
        dataService.getSalePriceTableLastSeason(
            context.id,
            function (data) {
                $scope.data.salePriceTableLastSeasons = data.data;
            },
            function (error) {
            }
        );
    };

    $scope.salePriceLastSeasonByItem = function (offerItem) {
        if (!$scope.data.salePriceTableLastSeasons) return 0;
        var x = $scope.data.salePriceTableLastSeasons.filter(o => o.offerSeasonDetailID === offerItem.offerSeasonDetailID);
        if (x.length > 0) {
            var result = x[0].salePrice;
            return result === null ? 0 : result;
        }
        return 0;
    };

    $scope.getOfferLineByOfferSeasonDetail = function (item) {
        $scope.adminItem = item;
        dataService.getOfferLineByOfferSeasonDetail(
            item.offerSeasonDetailID,
            function (data) {
                $scope.offerLineByItems = data.data;
            },
            function (error) {
            }
        );
    };

    $scope.adminUpdateSalePrice = function (item) {
        bootbox.confirm("Are you sure you want to update all price. All price in PI and Offer will be updated !!!", function (result) {
            if (result) {
                dataService.adminUpdateSalePrice(
                    item.offerSeasonDetailID,
                    $scope.adminSalePrice,
                    function (data) {
                        item.salePrice = $scope.adminSalePrice;
                        $scope.adminSalePrice = null;

                        item.statustorName = 'yourself';
                        item.setItemStatusDate = 'just now';
                    },
                    function (error) {
                    }
                );
            }
        });              
    };

    //
    //purchasing price last year (current supplier)
    //
    $scope.getPurchasingPriceLastYear = function () {
        dataService.getPurchasingPriceLastYear(
            context.id,
            function (data) {
                $scope.data.purchasingPriceLastYearDTOs = data.data;
            },
            function (error) {
            }
        );
    };

    $scope.getPurchasingPriceLastSeasonByItem = function (offerItem) {
        if (!$scope.data.purchasingPriceLastYearDTOs) return 0;
        var x = $scope.data.purchasingPriceLastYearDTOs.filter(o => o.offerSeasonDetailID === offerItem.offerSeasonDetailID);
        if (x.length > 0) {
            var price = x[0].purchasingPriceIncludeDiff === null ? 0 : x[0].purchasingPriceIncludeDiff;
            return price;
        }
        return 0;
    };

    $scope.getCurrentSupplierLastSeasonByItem = function (offerItem) {
        if (!$scope.data.purchasingPriceLastYearDTOs) return null;
        var x = $scope.data.purchasingPriceLastYearDTOs.filter(o => o.offerSeasonDetailID === offerItem.offerSeasonDetailID);
        if (x.length > 0) {
            return x[0].factoryUD;
        }
        return null;
    };

    //
    //get data to check offer in put in factory order
    //
    $scope.getRelatedFactoryOrderDetail = function () {
        dataService.getRelatedFactoryOrderDetail(
            context.id,
            function (data) {
                $scope.data.relatedFactoryOrderDetailDTOs = data.data;
            },
            function (error) {
            }
        );
    };

    $scope.isPutInFactoryOrder = function (offerItem) {
        if (!$scope.data.relatedFactoryOrderDetailDTOs) return null;
        var x = $scope.data.relatedFactoryOrderDetailDTOs.filter(o => o.offerSeasonDetailID === offerItem.offerSeasonDetailID);
        if (x.length > 0) {
            return true;
        }
        return null;
    };

    $scope.getTotalFactoryOrderQntByItem = function (offerItem) {
        if ($scope.data === null || !$scope.data.relatedFactoryOrderDetailDTOs) return 0;
        var x = $scope.data.relatedFactoryOrderDetailDTOs.filter(o => o.offerSeasonDetailID === offerItem.offerSeasonDetailID);
        if (x.length > 0) {
            return parseFloat(x[0].totalFactoryOrderQnt === null ? 0 : x[0].totalFactoryOrderQnt);
        }
        return 0;
    };

    //
    //image gallery
    //
    $scope.getImageGallery = function () {
        dataService.getImageGallery(
            context.id,
            function (data) {
                $scope.data.imageGalleryDTOs = data.data;                
            },
            function (error) {
            }
        );
    };

    $scope.getImageGalleryByItem = function (offerItem) {
        var noImage = 'http://media-tilsoft.com/file/no-image.jpg';
        if (!$scope.data.imageGalleryDTOs) return noImage;
        var x = $scope.data.imageGalleryDTOs.filter(o => o.offerSeasonDetailID === offerItem.offerSeasonDetailID);
        if (x.length > 0 && x[0].imageGalleryFileLocation !== null) {
            return x[0].imageGalleryFileLocation;
        }
        return noImage;
    };

    //
    //create offer season sample
    //
    $scope.createOfferSeasonSample = function (item) {
        dataService.createOfferSeasonSample(
            item.offerSeasonDetailID,
            $scope.data.offerSeasonDTO.clientID,
            $scope.data.offerSeasonDTO.season,
            function (data) {
                var sampleOfferSeasonID = data.data.sampleOfferSeasonID;
                window.open(context.refreshUrl + sampleOfferSeasonID + '#/edit-fob-sample-form', '');
            },
            function (error) {
            }
        );
    };

    //
    //get delta by client (module MIDeltaByClientRpt)
    //
    $scope.getDeltaByClient = function () {
        if (!context.canReadDeltaByClient) return;
        dataService.getDeltaByClient(
            context.miDeltaByClientUrl,
            context.token,            
            jsHelper.getPrevSeason($scope.data.offerSeasonDTO.season),
            $scope.data.offerSeasonDTO.clientID,
            function (data) {
                var deltaData = data.data.data;
                if (deltaData.length > 0) {
                    $scope.data.offerSeasonDTO.piConfirmedDeltaLastYear = deltaData[0].grossMarginPercent;                    
                }                
            },
            function (error) {
            }
        );
    };

    //
    //ClientEstimatedAdditionalCost info, get costs percent
    //
    $scope.getLCCostPercent = function () {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return null;
        var season = $scope.data.offerSeasonDTO.season;
        var lcCostPercent = null;
        var estimatedAdditionalCost = $scope.data.clientEstimatedAdditionalCostDTOs.filter(o => o.season === season);
        if (estimatedAdditionalCost.length > 0) lcCostPercent = estimatedAdditionalCost[0].lcCostPercent;
        if (!lcCostPercent) {
            var lcPercent = !$scope.data.masterSettingDTO.lcPercent ? 0 : parseFloat($scope.data.masterSettingDTO.lcPercent);
            lcCostPercent = lcPercent;
        }
        return lcCostPercent;
    };

    $scope.getInterestCostPercent = function () {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return null;
        var season = $scope.data.offerSeasonDTO.season;
        var interestCostPercent = null;
        var estimatedAdditionalCost = $scope.data.clientEstimatedAdditionalCostDTOs.filter(o => o.season === season);
        if (estimatedAdditionalCost.length > 0) interestCostPercent = estimatedAdditionalCost[0].interestCostPercent;
        if (!interestCostPercent) {
            var interestPercent = !$scope.data.masterSettingDTO.interestPercent ? 0 : parseFloat($scope.data.masterSettingDTO.interestPercent);
            interestCostPercent = interestPercent;
        }
        return interestCostPercent;
    };

    $scope.getBonusPercent = function () {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return null;
        var season = $scope.data.offerSeasonDTO.season;
        var bonusPercent = null;
        var estimatedAdditionalCost = $scope.data.clientEstimatedAdditionalCostDTOs.filter(o => o.season === season);
        if (estimatedAdditionalCost.length > 0) bonusPercent = estimatedAdditionalCost[0].bonusPercent;
        return bonusPercent;
    };

    //
    //item correction
    //
    $scope.itemCorrection = function (offerItem) {
        offerItem.isCorrectingItem = true;
    };

    //
    //delta of item
    //
    $scope.testDelta = function (offerItem) {
        //debugger;
        var delta = $scope.salePriceInUSD(offerItem);
        var aa = $scope.purchasingPriceInUSD(offerItem);
        var bb = $scope.commissionInUSD(offerItem);
        var cc = $scope.lcCostInUSD(offerItem);
        var dd = $scope.interestInUSD(offerItem);
        var eea = $scope.damagesCostInUSD(offerItem);
        var ffa = $scope.transportationInUSD(offerItem);
        return delta;
    };

    $scope.delta = function (offerItem) {
        if ($scope.purchasingPriceInUSD(offerItem) !== 0) {
            var delta = $scope.salePriceInUSD(offerItem)
                - $scope.purchasingPriceInUSD(offerItem)
                - $scope.commissionInUSD(offerItem)
                - $scope.lcCostInUSD(offerItem)
                - $scope.interestInUSD(offerItem)
                - $scope.damagesCostInUSD(offerItem)
                - $scope.transportationInUSD(offerItem)
                - $scope.bonusCostInUSD(offerItem);
            return delta;
        }
        else {
            return 0;
        }
    };

    $scope.deltaPercent = function (offerItem) {
        if ($scope.purchasingPriceInUSD(offerItem) !== 0) {
            var deltaPercent = $scope.delta(offerItem) / $scope.purchasingPriceInUSD(offerItem) * 100;
            return deltaPercent;
        }
        else {
            return 0;
        }
    };

    $scope.salePriceInUSD = function (offerItem) {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return 0;

        var currency = $scope.data.offerSeasonDTO.currency;
        var exRate = $scope.getExRate();
        var salePrice = parseFloat(offerItem.salePrice === null || offerItem.salePrice === '' ? 0 : offerItem.salePrice);

        //get price in USD
        var salePriceInUSD = salePrice * (currency === 'EUR' ? exRate : 1);
        return salePriceInUSD;
    };

    $scope.purchasingPriceInUSD = function (offerItem) {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return 0;
        var purchasingPriceInUSD = parseFloat(offerItem.planingPurchasingPrice === null || offerItem.planingPurchasingPrice === '' ? 0 : offerItem.planingPurchasingPrice);

        //get purchasing price
        return purchasingPriceInUSD;
    };

    $scope.commissionInUSD = function (offerItem) {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return 0;

        var currency = $scope.data.offerSeasonDTO.currency;
        var exRate = $scope.getExRate();
        var salePrice = parseFloat(offerItem.salePrice === null || offerItem.salePrice === '' ? 0 : offerItem.salePrice);
        var commissionPercent = parseFloat(offerItem.commissionPercent === null || offerItem.commissionPercent === '' ? 0 : offerItem.commissionPercent);

        //get commission
        var commissionInUSD = 0;
        commissionInUSD = salePrice * (currency === 'EUR' ? exRate : 1) * commissionPercent / 100;
        return commissionInUSD;
    };

    $scope.lcCostInUSD = function (offerItem) {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return 0;

        var currency = $scope.data.offerSeasonDTO.currency;
        var exRate = $scope.getExRate();
        var season = $scope.data.offerSeasonDTO.season;
        var salePrice = parseFloat(offerItem.salePrice === null || offerItem.salePrice === '' ? 0 : offerItem.salePrice);

        //get lc cost percent
        var lcCostPercent = null;
        var estimatedAdditionalCost = $scope.data.clientEstimatedAdditionalCostDTOs.filter(o => o.season === season);
        if (estimatedAdditionalCost.length > 0) lcCostPercent = estimatedAdditionalCost[0].lcCostPercent;

        //get payment type
        var paymentTypeID = $scope.data.offerSeasonDTO.paymentTypeID;

        //get lc cost
        var lcCostInUSD = 0;
        if (lcCostPercent !== null) {
            lcCostInUSD = salePrice * lcCostPercent / 100 * (currency === 'EUR' ? exRate : 1);
        }
        else if (paymentTypeID === 4) {
            var lcPercent = !$scope.data.masterSettingDTO.lcPercent ? 0 : parseFloat($scope.data.masterSettingDTO.lcPercent);
            lcCostInUSD = salePrice * lcPercent / 100 * (currency === 'EUR' ? exRate : 1);
        }
        return lcCostInUSD;
    };

    $scope.interestInUSD = function (offerItem) {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return 0;

        var currency = $scope.data.offerSeasonDTO.currency;
        var exRate = $scope.getExRate();
        var season = $scope.data.offerSeasonDTO.season;
        var salePrice = parseFloat(offerItem.salePrice === null || offerItem.salePrice === '' ? 0 : offerItem.salePrice);

        //get interest cost percent
        var interestCostPercent = null;
        var estimatedAdditionalCost = $scope.data.clientEstimatedAdditionalCostDTOs.filter(o => o.season === season);
        if (estimatedAdditionalCost.length > 0) interestCostPercent = estimatedAdditionalCost[0].interestCostPercent;

        //get payment day && payment method && down value
        var inDays = $scope.data.offerSeasonDTO.inDays;
        var paymentMethod = $scope.data.offerSeasonDTO.paymentMethod;
        var downValue = $scope.data.offerSeasonDTO.downValue;

        //get interest cost
        var interestInUSD = 0;
        if (interestCostPercent !== null) {
            interestInUSD = salePrice * interestCostPercent / 100 * (currency === 'EUR' ? exRate : 1);
        }
        else if (inDays !== null) {
            var interestPercent = !$scope.data.masterSettingDTO.interestPercent ? 0 : parseFloat($scope.data.masterSettingDTO.interestPercent);
            interestInUSD = salePrice * inDays * (currency === 'EUR' ? exRate : 1) * (paymentMethod === 'DP-PERCENT' ? (1 - downValue / 100) : 1) * interestPercent / 100 / 360;
        }
        return interestInUSD;
    };

    $scope.damagesCostInUSD = function (offerItem) {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return 0;
        var purchasingPriceInUSD = parseFloat(offerItem.planingPurchasingPrice === null || offerItem.planingPurchasingPrice === '' ? 0.001 : offerItem.planingPurchasingPrice);

        //get damage cost
        var damagePercent = !$scope.data.masterSettingDTO.damagePercent ? 0 : parseFloat($scope.data.masterSettingDTO.damagePercent);
        var damagesCostInUSD = purchasingPriceInUSD * damagePercent / 100;
        return damagesCostInUSD;
    };

    $scope.transportationInUSD = function (offerItem) {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return 0;

        var currency = $scope.data.offerSeasonDTO.currency;
        var exRate = $scope.getExRate();
        var qnt40HC = offerItem.qnt40HC === null || offerItem.qnt40HC === '' ? 0 : offerItem.qnt40HC;
        var transportationFee = $scope.data.offerSeasonDTO.estTransportationPerContainerEUR;
        transportationFee = parseFloat(transportationFee === null || transportationFee === '' ? 0 : transportationFee);

        //get transportation
        var transportationInUSD = 0;
        if (currency === 'EUR' && qnt40HC !== 0) {
            transportationInUSD = transportationFee / qnt40HC * exRate;
        }
        return transportationInUSD;
    };

    $scope.bonusCostInUSD = function (offerItem) {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return 0;

        var currency = $scope.data.offerSeasonDTO.currency;
        var exRate = $scope.getExRate();
        var season = $scope.data.offerSeasonDTO.season;
        var salePrice = parseFloat(offerItem.salePrice === null || offerItem.salePrice === '' ? 0 : offerItem.salePrice);

        //get bonus cost percent
        var bonusPercent = 0;
        var estimatedAdditionalCost = $scope.data.clientEstimatedAdditionalCostDTOs.filter(o => o.season === season);
        if (estimatedAdditionalCost.length > 0 && estimatedAdditionalCost[0].bonusPercent) bonusPercent = parseFloat(estimatedAdditionalCost[0].bonusPercent);

        //get bonus cost
        var bonusCostInUSD = 0;
        bonusCostInUSD = salePrice * bonusPercent / 100 * (currency === 'EUR' ? exRate : 1);
        return bonusCostInUSD;
    };

    $scope.inspectionCostUSD = function () {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return 0;

        var season = $scope.data.offerSeasonDTO.season;
        //get inspection cost
        var inspectionCostUSD = 0;
        var estimatedAdditionalCost = $scope.data.clientEstimatedAdditionalCostDTOs.filter(o => o.season === season);
        if (estimatedAdditionalCost.length > 0) {
            inspectionCostUSD = estimatedAdditionalCost[0].inspectionCostUSD;
        }

        //get interest cost
        inspectionCostUSD = parseFloat(inspectionCostUSD === null ? 0 : inspectionCostUSD);
        return inspectionCostUSD;
    };

    $scope.sampleCostUSD = function () {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return 0;

        var season = $scope.data.offerSeasonDTO.season;
        //get inspection cost
        var sampleCostUSD = 0;
        var estimatedAdditionalCost = $scope.data.clientEstimatedAdditionalCostDTOs.filter(o => o.season === season);
        if (estimatedAdditionalCost.length > 0) {
            sampleCostUSD = estimatedAdditionalCost[0].sampleCostUSD;
        }
        //get interest cost
        sampleCostUSD = parseFloat(sampleCostUSD === null ? 0 : sampleCostUSD);
        return sampleCostUSD;
    };

    $scope.sparepartCostUSD = function () {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return 0;

        var season = $scope.data.offerSeasonDTO.season;
        //get sparepart cost
        var sparepartCostUSD = 0;
        var estimatedAdditionalCost = $scope.data.clientEstimatedAdditionalCostDTOs.filter(o => o.season === season);
        if (estimatedAdditionalCost.length > 0) {
            sparepartCostUSD = estimatedAdditionalCost[0].sparepartCostUSD;
        }
        //get interest cost
        sparepartCostUSD = parseFloat(sparepartCostUSD === null ? 0 : sparepartCostUSD);
        return sparepartCostUSD;
    };

    //
    //delta last year
    //
    $scope.salePriceLastYearInUSD = function (offerItem) {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return 0;

        var currency = $scope.data.offerSeasonDTO.currency;
        var exRate = $scope.getExRate();
        var salePrice = $scope.salePriceLastSeasonByItem(offerItem);

        //get price in USD
        var salePriceInUSD = salePrice * (currency === 'EUR' ? exRate : 1);
        return salePriceInUSD;
    };

    $scope.purchasingPriceLastYearInUSD = function (offerItem) {        
        var purchasingPriceInUSD = $scope.getPurchasingPriceLastSeasonByItem(offerItem);        
        return purchasingPriceInUSD;
    };

    $scope.deltaLastYear = function (offerItem) {
        if ($scope.purchasingPriceLastYearInUSD(offerItem) !== 0 && $scope.salePriceLastYearInUSD(offerItem) !== 0) {
            var delta = $scope.salePriceLastYearInUSD(offerItem)
                - $scope.purchasingPriceLastYearInUSD(offerItem)
                - $scope.commissionInUSD(offerItem)
                - $scope.lcCostInUSD(offerItem)
                - $scope.interestInUSD(offerItem)
                - $scope.damagesCostInUSD(offerItem)
                - $scope.transportationInUSD(offerItem)
                - $scope.bonusCostInUSD(offerItem);
            return delta;
        }
        else {
            return 0;
        }
    };

    $scope.deltaPercentLastYear = function (offerItem) {
        if ($scope.purchasingPriceLastYearInUSD(offerItem) !== 0 && $scope.salePriceLastYearInUSD(offerItem) !== 0) {
            var deltaPercent = $scope.deltaLastYear(offerItem) / $scope.purchasingPriceLastYearInUSD(offerItem) * 100;
            return deltaPercent;
        }
        else {
            return 0;
        }
    };

    $scope.getTotalItemHasDeltaSingleLastYear = function () {
        var totalItem = 0;
        angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
            if ($scope.deltaPercentLastYear(item) !== 0) {
                totalItem += 1;
            }
        });
        return totalItem;
    };

    //
    //delta config (spoga)
    //
    $scope.salePriceConfigInUSD = function (offerItem) {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return 0;

        var currency = $scope.data.offerSeasonDTO.currency;
        var exRate = $scope.getExRate();
        var salePriceConfig = parseFloat(offerItem.salePriceConfig === null ? 0 : offerItem.salePriceConfig);

        //get price in USD
        var salePriceConfigInUSD = salePriceConfig * (currency === 'EUR' ? exRate : 1);
        return salePriceConfigInUSD;
    };

    $scope.purchasingPriceConfigInUSD = function (offerItem) {
        if ($scope.data === null || $scope.data.offerSeasonDTO === null || $scope.data.offerSeasonDetailDTOs === null) return 0;
        var purchasingPriceConfigInUSD = parseFloat(offerItem.purchasingPriceConfig === null ? 0 : offerItem.purchasingPriceConfig);
        return purchasingPriceConfigInUSD;
    };

    $scope.deltaConfig = function (offerItem) {
        if ($scope.purchasingPriceConfigInUSD(offerItem) !== 0 && $scope.salePriceConfigInUSD(offerItem)) {
            var delta = $scope.salePriceConfigInUSD(offerItem)
                - $scope.purchasingPriceConfigInUSD(offerItem)
                - $scope.commissionInUSD(offerItem)
                - $scope.lcCostInUSD(offerItem)
                - $scope.interestInUSD(offerItem)
                - $scope.damagesCostInUSD(offerItem)
                - $scope.transportationInUSD(offerItem)
                - $scope.bonusCostInUSD(offerItem);
            return delta;
        }
        else {
            return 0;
        }
    };

    $scope.deltaPercentConfig = function (offerItem) {
        if ($scope.purchasingPriceConfigInUSD(offerItem) !== 0 && $scope.salePriceConfigInUSD(offerItem) !== 0) {
            var deltaPercent = $scope.deltaConfig(offerItem) / $scope.purchasingPriceConfigInUSD(offerItem) * 100;
            return deltaPercent;
        }
        else {
            return 0;
        }
    };

    //
    //get total row
    //
    $scope.getTotalRow = function (category) {
        var total = 0;
        var totalDeltaAmount = 0;
        var totalPurchasingAmount = 0;
        if ($scope.data && $scope.data.offerSeasonDTO && $scope.offerSeasonDetailDTO_AfterFilter) {
            switch (category) {
                case 'quantity':
                    return $scope.offerSeasonDetailDTO_AfterFilter.reduce((output, item) => output + (item.quantity ? parseInt(item.quantity) : 0), 0);

                case 'quantity40HC':
                    return $scope.offerSeasonDetailDTO_AfterFilter.reduce((output, item) => output + jsHelper.round(item.quantity / item.qnt40HC, 2), 0.00);

                case 'deltaTotalPercentSingleLastYear':
                    //var deltaPercentSingleLastYear = 0;
                    //var totalPurchasingAmountLastYear = 0;
                    //var totalDeltaAmountLastYear = 0;
                    //angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                    //    var quantity = parseFloat(item.quantity === null || item.quantity === '' ? 0 : 1);
                    //    totalPurchasingAmountLastYear += parseFloat($scope.purchasingPriceLastYearInUSD(item)) * quantity;
                    //    totalDeltaAmountLastYear += parseFloat($scope.deltaLastYear(item)) * quantity;
                    //});
                    //if (totalPurchasingAmountLastYear !== 0) {
                    //    deltaPercentSingleLastYear = totalDeltaAmountLastYear * 100 / totalPurchasingAmountLastYear;
                    //    return deltaPercentSingleLastYear;
                    //}
                    //else {
                    //    return 0;
                    //}
                    var deltaPercentSingleLastYear = parseFloat(0);
                    var totalSingleDeltaLastYear = parseFloat(0);
                    var totalItemLastYear = parseInt(0);
                    angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                        var deltaPercentPerItem = jsHelper.round($scope.deltaPercentLastYear(item), 1);
                        if (deltaPercentPerItem !== 0) {
                            totalSingleDeltaLastYear += deltaPercentPerItem;
                            totalItemLastYear += 1;
                        }                                                
                    });
                    if (totalItemLastYear !== 0) {
                        deltaPercentSingleLastYear = jsHelper.round(totalSingleDeltaLastYear / totalItemLastYear, 1);
                    }
                    return deltaPercentSingleLastYear;

                case 'deltaTotalPercentSingle':
                    //var deltaPercentSingle = 0;
                    //totalPurchasingAmount = 0;
                    //totalDeltaAmount = 0;
                    //angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                    //    var quantity = parseFloat(item.quantity === null || item.quantity === '' ? 0 : 1);
                    //    totalPurchasingAmount += parseFloat($scope.purchasingPriceInUSD(item)) * quantity;
                    //    totalDeltaAmount += parseFloat($scope.delta(item)) * quantity;
                    //});
                    //if (totalPurchasingAmount !== 0) {
                    //    deltaPercentSingle = totalDeltaAmount * 100 / totalPurchasingAmount;
                    //    return deltaPercentSingle;
                    //}
                    //else {
                    //    return 0;
                    //}
                    var deltaPercentSingle = parseFloat(0);
                    var totalSingleDelta = parseFloat(0);
                    var totalItem = parseInt(0);
                    angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                        var deltaPercentPerItem = jsHelper.round($scope.deltaPercent(item), 1);
                        if (deltaPercentPerItem !== 0) {
                            totalSingleDelta += deltaPercentPerItem;
                            totalItem += 1;
                        }                        
                    });
                    if (totalItem !== 0) {
                        deltaPercentSingle = jsHelper.round(totalSingleDelta / totalItem, 1);
                    }                    
                    return deltaPercentSingle;
                    
                case 'deltaTotalPercent':
                    var deltaPercent = 0;
                    totalPurchasingAmount = 0;
                    totalDeltaAmount = 0;
                    angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                        var quantity = parseFloat(item.quantity === null || item.quantity === '' ? 0 : item.quantity);
                        totalPurchasingAmount += parseFloat($scope.purchasingPriceInUSD(item) * quantity);
                        totalDeltaAmount += parseFloat($scope.delta(item) * quantity);
                    });
                    if (totalPurchasingAmount !== 0) {
                        deltaPercent = totalDeltaAmount * 100 / totalPurchasingAmount;
                        return deltaPercent;
                    }
                    else {
                        return 0;
                    }

                case 'deltaTotal':
                    totalDeltaAmount = 0;
                    angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                        var quantity = parseFloat(item.quantity === null || item.quantity === '' ? 0 : item.quantity);
                        totalDeltaAmount += parseFloat($scope.delta(item) * quantity);
                    });
                    return totalDeltaAmount;

                case 'commission':
                    total = 0;
                    angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                        var quantity = parseFloat(item.quantity === null || item.quantity === '' ? 0 : item.quantity);
                        total += parseFloat($scope.commissionInUSD(item) * quantity);
                    });
                    return total;

                case 'lccost':
                    total = 0;
                    angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                        var quantity = parseFloat(item.quantity === null || item.quantity === '' ? 0 : item.quantity);
                        total += parseFloat($scope.lcCostInUSD(item) * quantity);
                    });
                    return total;

                case 'interest':
                    total = 0;
                    angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                        var quantity = parseFloat(item.quantity === null || item.quantity === '' ? 0 : item.quantity);
                        total += parseFloat($scope.interestInUSD(item) * quantity);
                    });
                    return total;

                case 'damages':
                    total = 0;
                    angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                        var quantity = parseFloat(item.quantity === null || item.quantity === '' ? 0 : item.quantity);
                        total += parseFloat($scope.damagesCostInUSD(item) * quantity);
                    });
                    return total;

                case 'transportation':
                    total = 0;
                    angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                        var quantity = parseFloat(item.quantity === null || item.quantity === '' ? 0 : item.quantity);
                        total += parseFloat($scope.transportationInUSD(item) * quantity);
                    });
                    return total;

                case 'bonus':
                    total = 0;
                    angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                        var quantity = parseFloat(item.quantity === null || item.quantity === '' ? 0 : item.quantity);
                        total += parseFloat($scope.bonusCostInUSD(item) * quantity);
                    });
                    return total;

                case 'saleAmount':
                    return $scope.offerSeasonDetailDTO_AfterFilter.reduce((output, item) => output + item.salePrice * item.quantity, 0.00);

                case 'vat':
                    return $scope.offerSeasonDetailDTO_AfterFilter.reduce((output, item) => output + item.salePrice * item.quantity, 0.00) * ($scope.data.offerSeasonDTO.vat ? $scope.data.offerSeasonDTO.vat / 100 : 0);

                case 'total':
                    return $scope.offerSeasonDetailDTO_AfterFilter.reduce((output, item) => output + item.salePrice * item.quantity, 0.00) * (1 + ($scope.data.offerSeasonDTO.vat ? $scope.data.offerSeasonDTO.vat / 100 : 0));

                case 'purchaseAmount':
                    total = 0;
                    angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                        total += parseFloat($scope.purchasingPriceInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                    });
                    return total;

                case 'saleAmountInUSD':
                    total = 0;
                    angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                        total += parseFloat($scope.salePriceInUSD(item) * (item.quantity === null ? 0 : item.quantity));
                    });
                    return total;

                case 'commissionPercent':
                    var totalCommission = parseFloat(0);
                    var totalSale = parseFloat(0);
                    var commissionPercent = 0;
                    angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                        var quantity = parseFloat(item.quantity === null || item.quantity === '' ? 0 : item.quantity);
                        totalCommission += parseFloat($scope.commissionInUSD(item) * quantity);
                        totalSale += parseFloat($scope.salePriceInUSD(item) * quantity);
                    });
                    if (totalCommission !== 0) {
                        commissionPercent = totalCommission * 100 / totalSale;
                    }
                    return commissionPercent;

                case 'factoryOrderQnt':
                    total = 0;
                    angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                        total += parseFloat($scope.getTotalFactoryOrderQntByItem(item));
                    });
                    return total;
            }
        }

        return null;
    };

    //
    //init
    //    
    $timeout(function () {
        $scope.initController();        
    });
}]);
