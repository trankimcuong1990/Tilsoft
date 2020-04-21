$(document).keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element(jQuery('#partial-content')).scope();
        scope.localFilterItem();
        scope.$apply();
    }
});
tilsoftApp.controller('_EditFobSampleForm', ['$scope', '$timeout', '$cookieStore', 'dataService', '$routeParams', '$location', function ($scope, $timeout, $cookieStore, dataService, $routeParams, $location) {   
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

                //load image gallery
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

    $scope.productGrid = {
        loadNextPage: function () {
            //do nothing
        },
        sortData: function (sortedBy, sortedDirection) {
            //if (sortedDirection === 'asc') {
            //    $scope.sortHandler.orderByColumn = sortedBy;
            //}
            //else if (sortedDirection === 'desc') {
            //    $scope.sortHandler.orderByColumn = '-' + sortedBy;
            //}
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
        },

        getCurrentScrollTop: function (topValue, leftValue) {
            $scope.pagePosition.gridProductTop = topValue;
            $scope.pagePosition.gridProductLeft = leftValue;
        }
    };

    $scope.reCalculateValue = function (offerSeasonDetailItem) {
        //
        //this function only make for sort feature
        //
        offerSeasonDetailItem.orderQnt40HC = offerSeasonDetailItem.qnt40HC !== null && offerSeasonDetailItem.qnt40HC !== 0 ? offerSeasonDetailItem.quantity / offerSeasonDetailItem.qnt40HC : 0;
        //offerSeasonDetailItem.deltaPercent = $scope.deltaPercent(offerSeasonDetailItem);
        //offerSeasonDetailItem.deltaAmount = $scope.delta(offerSeasonDetailItem) * offerSeasonDetailItem.quantity;

        offerSeasonDetailItem.purchasingAmount = $scope.purchasingPriceInUSD(offerSeasonDetailItem) * offerSeasonDetailItem.quantity;
        offerSeasonDetailItem.saleAmountInUSD = $scope.salePriceInUSD(offerSeasonDetailItem) * offerSeasonDetailItem.quantity;
        offerSeasonDetailItem.saleAmount = offerSeasonDetailItem.salePrice * offerSeasonDetailItem.quantity;
        offerSeasonDetailItem.vatAmount = offerSeasonDetailItem.salePrice * offerSeasonDetailItem.quantity * $scope.data.offerSeasonDTO.vat / 100;
        offerSeasonDetailItem.saleAmountIncludeVAT = offerSeasonDetailItem.salePrice * offerSeasonDetailItem.quantity * (1 + $scope.data.offerSeasonDTO.vat / 100);

        //offerSeasonDetailItem.commissionInUSD = $scope.commissionInUSD(offerSeasonDetailItem) * offerSeasonDetailItem.quantity;
        //offerSeasonDetailItem.lcCostInUSD = $scope.lcCostInUSD(offerSeasonDetailItem) * offerSeasonDetailItem.quantity;
        //offerSeasonDetailItem.interestInUSD = $scope.interestInUSD(offerSeasonDetailItem) * offerSeasonDetailItem.quantity;
        //offerSeasonDetailItem.damagesCostInUSD = $scope.damagesCostInUSD(offerSeasonDetailItem) * offerSeasonDetailItem.quantity;
        //offerSeasonDetailItem.transportationInUSD = $scope.transportationInUSD(offerSeasonDetailItem) * offerSeasonDetailItem.quantity;
    };

    //
    //event
    //
    $scope.changeQnt = function (offerSeasonDetailItem) {
        $scope.reCalculateValue(offerSeasonDetailItem);
        offerSeasonDetailItem.isEditing = true;
    };
                  
    $scope.needMakeQuotationStatus = function (offerSeasonDetailItem) {        
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

    $scope.getItemStatusText = function (itemStatus) {
        var statuses = $scope.data.offerSeasonItemStatusDTOs.filter(o => o.itemStatus === itemStatus);
        if (statuses.length > 0) {
            return statuses[0].itemStatusText;
        }
        return '';
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

    $scope.selectFabricFactory = function (item) {
        item.isEditing = true;
    };

    $scope.reject = function (offerSeasonDetailItem) {
        offerSeasonDetailItem.itemStatus = 6;
        $scope.updateOfferSeasonDetail($scope.data.offerSeasonDTO.offerSeasonID, offerSeasonDetailItem.offerSeasonDetailID, offerSeasonDetailItem);
    };

    $scope.changeItem = function (offerSeasonDetailItem) {
        offerSeasonDetailItem.isEditing = true;
    };
    
    $scope.countItemNeedApprove = function () {
        if ($scope.data === null || $scope.data.offerSeasonDetailDTOs === null) return 0;
        var countItem = $scope.data.offerSeasonDetailDTOs.filter(o => o.itemStatus === 4).length;
        return countItem;
    };

    $scope.countItemAskForApproval = function () {
        if ($scope.data === null || $scope.data.offerSeasonDetailDTOs === null) return 0;
        var countItem = $scope.data.offerSeasonDetailDTOs.filter(o => o.offerSeasonDetailID > 0 && o.itemStatus !== 4 && o.itemStatus !== 5 && o.quantity && o.planingPurchasingPrice).length;
        return countItem;
    };

    $scope.askForApprovalItem = function (offerSeasonDetailItem) {        
        if (offerSeasonDetailItem.offerSeasonDetailID > 0 && offerSeasonDetailItem.itemStatus !== 4 && offerSeasonDetailItem.itemStatus !== 5 && offerSeasonDetailItem.quantity && offerSeasonDetailItem.planingPurchasingPrice) {
            offerSeasonDetailItem.itemStatus = 4;
            $scope.updateOfferSeasonDetail($scope.data.offerSeasonDTO.offerSeasonID, offerSeasonDetailItem.offerSeasonDetailID, offerSeasonDetailItem);
        }        
    };

    $scope.askForApprovalAllItem = function () {
        var askApprovalItems = $scope.data.offerSeasonDetailDTOs.filter(o => o.offerSeasonDetailID > 0 && o.itemStatus !== 4 && o.itemStatus !== 5 && o.quantity && o.planingPurchasingPrice);
        angular.forEach(askApprovalItems, function (item) {
            $scope.askForApprovalItem(item);
        });
    };

    $scope.unApproveItem = function (offerSeasonDetailItem) {
        //set offer detail item is to status NEED-APPROVE
        offerSeasonDetailItem.itemStatus = 4;
        offerSeasonDetailItem.markAsUnApproved = true;

        //approve item
        $scope.updateOfferSeasonDetail($scope.data.offerSeasonDTO.offerSeasonID, offerSeasonDetailItem.offerSeasonDetailID, offerSeasonDetailItem);
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

    $scope.showRemarkHistory = function (offerSeasonDetailItem) {
        $scope.remarks = offerSeasonDetailItem.offerSeasonDetailRemarkDTOs;
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

    $scope.getImageGalleryByItem = function (offerSeasonDetailID) {
        var noImage = 'http://media-tilsoft.com/file/no-image.jpg';
        if (!$scope.data.imageGalleryDTOs) return noImage;
        var x = $scope.data.imageGalleryDTOs.filter(o => o.offerSeasonDetailID === offerSeasonDetailID);
        if (x.length > 0 && x[0].imageGalleryFileLocation !== null) {
            return x[0].imageGalleryFileLocation;
        }
        return noImage;
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
    //Amount
    //
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

                case 'sampleAmount':
                    total = 0;
                    angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                        if (item.itemStatus === 5) {
                            total += parseFloat($scope.purchasingPriceInUSD(item) * (item.quantity === null ? 0 : item.quantity));   
                        }          
                    });
                    return total;

                case 'saleAmountInUSD':
                    total = 0;
                    angular.forEach($scope.offerSeasonDetailDTO_AfterFilter, function (item) {
                        total += parseFloat($scope.salePriceInUSD(item) * (item.quantity === null ? 0 : item.quantity));
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
