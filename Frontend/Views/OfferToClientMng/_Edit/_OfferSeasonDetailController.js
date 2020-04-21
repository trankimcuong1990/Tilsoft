tilsoftApp.controller('_OfferSeasonDetailController', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {

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

        //jQuery('#content').show();

        $scope.event.init();
    };

    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
            debugger;
            if ($scope.pageIndex < $scope.totalPage) {
                $scope.pageIndex++;
                dataService.searchFilter.pageIndex = $scope.pageIndex;
                $scope.event.search(true);
            }
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.searchResult = [];
            dataService.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = scope.pageIndex;
            dataService.searchFilter.sortedBy = sortedBy;
            $scope.event.search();
        },
        isTriggered: false,
    };  

    //
    // property
    //
    $scope.searchResult = [];
    $scope.filters = {
        articleCode: '',
        description: '',
        clientID: context.clientID,
        season: context.season,
        currency: context.currency,
        offerLineType: 0
    };

    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;
    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;
    $scope.newID = -1;
    $scope.isAllSelected = false;


    //
    // events
    //
    $scope.event = {
        init: function () {
            //dataService.getSearchFilter(
            //    function (data) {
            //        $scope.event.search();
            //    },
            //    function (error) {
            //        // reset data
            //    }
            //);
            $scope.event.search();
        },
        reload: function () {
            $scope.searchResult = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex;
            dataService.searchFilter.sortedBy = 'ArticleCode';

            $scope.event.search();
        },
        search: function (isLoadMore) {
            //
            // store filter in cookies
            //
            $cookieStore.put(context.cookieId, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
          
            $scope.gridHandler.isTriggered = false;
            //lấy offerLineType
            var href = window.location.href;
            var type = href[href.length - 1];
            var offerLineType = parseInt(type);
            $scope.filters.offerLineType = offerLineType;           

            dataService.searchFilter.filters = $scope.filters;
            dataService.quicksearchOfferLine(
                function (data) {                   
                    if (offerLineType === 1) { //sparepart
                        data.data.data = data.data.data.filter(function (item) {
                            return $scope.offerDataContainer.offerDTO.offerLineSparepartDTOs.filter(s => s.offerSeasonDetailID === item.offerSeasonDetailID).length === 0;
                        })
                    } else if (offerLineType === 0) { //product 
                        data.data.data = data.data.data.filter(function (item) {
                            return $scope.offerDataContainer.offerDTO.offerLineDTOs.filter(s => s.offerSeasonDetailID === item.offerSeasonDetailID).length === 0;
                        })
                    } else ( //sample
                        data.data.data = data.data.data.filter(function (item) {
                            return $scope.offerDataContainer.offerDTO.offerLineSampleProductDTOs.filter(s => s.offerSeasonDetailID === item.offerSeasonDetailID).length === 0;
                        })
                    );
                    
                    Array.prototype.push.apply($scope.searchResult, data.data.data);
                  
                    $scope.totalPage = Math.ceil(data.data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.data.totalRows;
                    jQuery('#content').show();
                    //debugger;
                    //if (!isLoadMore) {
                    //    $scope.gridHandler.goTop();
                    //}
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    $scope.searchResult = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                }
            );
        },
        clearFilter: function () {
            $scope.filters = {
                articleCode: '',
                description: '',
                clientID: context.clientID,
                season: context.season,
                currency: context.currency,
                offerLineType: 0
            };
            $scope.event.reload();
        },
        ok: function () {
            for (let i = 0; i < $scope.searchResult.length; i++) {
                let item = $scope.searchResult[i];
                if (item.isSelected) {

                    if (item.offerLineType === 0) {
                        var pItem = {
                            offerLineID: $scope.event.getNewID(),
                            articleCode: item.articleCode,
                            description: item.description,
                            remark: '',
                            unitPrice: item.salePrice,
                            quantity: item.quantity,
                            offerSeasonDetailQuantity: item.quantity, 
                            qnt40HC: item.qnt40HC,
                            offerSeasonDetailID: item.offerSeasonDetailID,
                            productFileLocation: item.productFileLocation,
                            productThumbnailLocation: item.productThumbnailLocation,
                            offerSeasonTypeNM: item.offerSeasonTypeNM,
                            planingPurchasingPrice: item.planingPurchasingPrice,
                            packagingMethodText: item.packagingMethodText,
                            planingPurchasingPriceSourceID: item.planingPurchasingPriceSourceID,
                            rowIndex: item.rowIndex,
                            commissionPercent: item.commissionPercent,
                            offerSeasonDetailCommissionPercent: item.commissionPercent                           
                        };                      

                        if ($scope.offerDataContainer.offerDTO.offerLineDTOs.filter(s => s.offerSeasonDetailID == pItem.offerSeasonDetailID).length === 0) {
                            $scope.offerDataContainer.offerDTO.offerLineDTOs.push(pItem);
                        }
                        else {
                            angular.forEach($scope.offerDataContainer.offerDTO.offerLineDTOs.filter(s => s.offerSeasonDetailID == pItem.offerSeasonDetailID), function (item) {

                                item.articleCode = pItem.articleCode;
                                item.description = pItem.description;
                                item.remark = '';
                                item.unitPrice = pItem.unitPrice;
                                item.quantity = pItem.quantity;
                                item.offerSeasonDetailQuantity = pItem.offerSeasonDetailQuantity;
                                item.qnt40HC = pItem.qnt40HC;
                                item.productFileLocation = pItem.productFileLocation;
                                item.productThumbnailLocation = pItem.productThumbnailLocation;
                                item.offerSeasonTypeNM = pItem.offerSeasonTypeNM;
                                item.planingPurchasingPrice = pItem.planingPurchasingPrice;
                                item.packagingMethodText = pItem.packagingMethodText;
                                item.planingPurchasingPriceSourceID = pItem.planingPurchasingPriceSourceID;
                                item.rowIndex = pItem.rowIndex;
                                item.commissionPercent = pItem.commissionPercent;
                                item.offerSeasonDetailCommissionPercent = pItem.offerSeasonDetailCommissionPercent;                               
                            });
                        }
                    }
                    else if (item.offerLineType === 1) {
                        var pItem = {
                            offerLineSparePartID: $scope.event.getNewID(),
                            articleCode: item.articleCode,
                            description: item.description,
                            remark: '',
                            unitPrice: item.salePrice,
                            quantity: item.quantity,                      
                            partID: item.partID,
                            offerSeasonDetailID: item.offerSeasonDetailID,                          
                            offerSeasonTypeNM: item.offerSeasonTypeNM,
                            rowIndex: item.rowIndex,
                            commissionPercent: item.commissionPercent,
                            offerSeasonDetailCommissionPercent: item.commissionPercent 
                        };

                        if ($scope.offerDataContainer.offerDTO.offerLineSparepartDTOs.filter(s => s.offerSeasonDetailID == pItem.offerSeasonDetailID).length === 0) {
                            $scope.offerDataContainer.offerDTO.offerLineSparepartDTOs.push(pItem);
                        }
                        else {
                            angular.forEach($scope.offerDataContainer.offerDTO.offerLineSparepartDTOs.filter(s => s.offerSeasonDetailID == pItem.offerSeasonDetailID), function (item) {

                                item.articleCode = pItem.articleCode;
                                item.description = pItem.description;
                                item.remark = '';
                                item.unitPrice = pItem.unitPrice;
                                item.quantity = pItem.quantity;
                                item.partID = pItem.partID;
                                item.offerSeasonTypeNM = pItem.offerSeasonTypeNM;
                                item.rowIndex = pItem.rowIndex;
                                item.commissionPercent = pItem.commissionPercent;
                                item.offerSeasonDetailCommissionPercent = pItem.offerSeasonDetailCommissionPercent;
                                
                            });

                        }
                    }
                    else  {// (item.offerLineType === 2)
                        var pItem = {
                            offerLineSampleProductID: $scope.event.getNewID(),
                            articleCode: item.articleCode,
                            description: item.description,                        
                            salePrice: item.salePrice,
                            quantity: item.quantity, 
                            offerSeasonDetailQuantity: item.quantity, 
                            qnt40HC: item.qnt40HC,
                            offerSeasonDetailID: item.offerSeasonDetailID,
                            thumbnailLocation: item.productThumbnailLocation,
                            fileLocation: item.productFileLocation,
                            offerSeasonTypeNM: item.offerSeasonTypeNM,
                            rowIndex: item.rowIndex,
                            planingPurchasingPrice: item.planingPurchasingPrice,
                            packagingMethodText: item.packagingMethodText,
                            planingPurchasingPriceSourceID: item.planingPurchasingPriceSourceID
                        };

                        if ($scope.offerDataContainer.offerDTO.offerLineSampleProductDTOs.filter(s => s.offerSeasonDetailID == pItem.offerSeasonDetailID).length === 0) {
                            $scope.offerDataContainer.offerDTO.offerLineSampleProductDTOs.push(pItem);
                        }
                        else {
                            angular.forEach($scope.offerDataContainer.offerDTO.offerLineSampleProductDTOs.filter(s => s.offerSeasonDetailID == pItem.offerSeasonDetailID), function (item) {
                                item.articleCode = pItem.articleCode;
                                item.description = pItem.description;
                                item.salePrice = pItem.salePrice;
                                item.quantity = pItem.quantity;  
                                item.offerSeasonDetailQuantity = pItem.offerSeasonDetailQuantity;
                                item.qnt40H = pItem.qnt40HC;
                                item.offerSeasonDetailID= pItem.offerSeasonDetailID;
                                item.thumbnailLocation = pItem.thumbnailLocation;
                                item.fileLocation = pItem.fileLocation;
                                item.offerSeasonTypeNM= pItem.offerSeasonTypeNM;
                                item.rowIndex = pItem.rowIndex;      
                                item.planingPurchasingPrice = pItem.planingPurchasingPrice;   
                                item.packagingMethodText = pItem.packagingMethodText;   
                                item.planingPurchasingPriceSourceID = pItem.planingPurchasingPriceSourceID;
                            });

                        }
                    }

                    $scope.offerDataContainer.offerDTO.saleID = $scope.searchResult[0].saleID;
                }
            }
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },
        selectAll: function () {
            if ($scope.isAllSelected) {
                for (var i = 0; i < $scope.searchResult.length; i++) {
                    let item = $scope.searchResult[i];
                    item.isSelected = true;
                }
            }
        }
    };


    //
    //init
    //
    $timeout(function () {
        $scope.initController();
    }, 0);

}]);
