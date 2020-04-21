tilsoftApp.controller('_SearchModelForm', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
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
    };

    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
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
        isTriggered: false
    };

    //
    // property
    //
    $scope.searchResult = [];


    //search query
    $scope.searchQuery = '';

    //
    //pager
    //    
    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;
    $scope.isShowSearchModel = false;

    //filter of pager
    $scope.searchFilter = {
        filters: {},
        sortedBy: 'ModelStatusID',
        sortedDirection: 'ASC',
        pageSize: 20,
        pageIndex: 1
    };

    
    //
    //id of lasted items that user push to dto detail when user select many model
    //we use this field to auto open screen edit item (product/sparepart) and direct to winzard
    $scope.offerSeasonDetailID = -1;

    //
    // events
    //
    $scope.event = {
        autoSearch: function ($event) {
            var keyCode = $event.keyCode;
            if (keyCode === 13) {
                if ($scope.searchQuery.length === 26) {
                    $scope.event.searchProduct();
                }
                else {
                    $scope.event.reload();
                }
            }
        },

        reload: function () {
            $scope.searchResult = [];
            $scope.pageIndex = 1;
            $scope.searchFilter.pageIndex = $scope.pageIndex;
            $scope.searchFilter.sortedBy = 'ModelStatusID';
            $scope.searchFilter.sortedDirection = 'ASC';
            $scope.event.search();
        },

        search: function (isLoadMore) {
            //
            // store filter in cookies
            //
            $cookieStore.put(context.cookieId, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            //get filter
            $scope.searchFilter.filters.modelUD = $scope.searchQuery;
            $scope.searchFilter.filters.modelNM = $scope.searchQuery;
            $scope.searchFilter.filters.rangeName = $scope.searchQuery;
            $scope.searchFilter.filters.collection = $scope.searchQuery;

            //search data
            dataService.searchModel($scope.searchFilter,
                function (data) {
                    Array.prototype.push.apply($scope.searchResult, data.data.data);
                    $scope.totalPage = Math.ceil(data.data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.data.totalRows;
                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;

                    //show form search result
                    $scope.isShowSearchModel = true;
                },
                function (error) {
                    $scope.searchResult = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                }
            );
        },

        searchProduct: function () {
            $scope.searchFilter.pageIndex = 1;
            $scope.searchFilter.sortedBy = 'ArticleCode';
            $scope.searchFilter.sortedDirection = 'ASC';
            $scope.searchFilter.filters.articleCode = $scope.searchQuery;
            //search product
            dataService.searchProduct2($scope.searchFilter,
                function (data) {
                    //show form search result
                    $scope.isShowSearchModel = false;
                    var products = data.data.data;
                    if (products.length > 0) {
                        var item = products[0];

                        //check this articleCode is exist in offer
                        var x = $scope.data.offerSeasonDetailDTOs.filter(o => o.articleCode === item.articleCode);
                        if (x.length > 0) {
                            //bootbox.alert("Product " + item.articleCode + "has already exist in this offer !!!");
                            return;
                        }

                        //create table pricing option
                        var pricingOption = [];
                        angular.forEach($scope.data.productElementDTOs, function (item) {
                            pricingOption.push({
                                offerSeasonDetailPriceOption: -1,
                                productElementID: item.productElementID,
                                productElementNM: item.productElementNM
                            });
                        });

                        //add product to dto detail
                        $scope.data.offerSeasonDetailDTOs.push({
                            offerSeasonDetailID: dataService.getIncrementId(),
                            itemStatus: 1, //PENDING                            
                            isEditing: true,
                            isClientSelected: true,                            
                            markAsClientSelected: true,
                            isNeedFactoryQuotation: true,
                            markAsNeedFactoryQuotation: true,
                            planingPurchasingPriceRemark: null,

                            modelID: item.modelID,
                            modelUD: item.modelUD,
                            modelNM: item.modelNM,
                            productFileLocation: item.fileLocation,
                            productThumbnailLocation: item.thumbnailLocation,
                            manufacturerCountryID: item.manufacturerCountryID,
                            packagingMethodID: item.packagingMethodID,
                            offerSeasonDetailPriceOptionDTOs: pricingOption,
                            cushionRemark: '',
                            commissionPercent: $scope.data.offerSeasonDTO.defaultCommissionPercent,
                            offerSeasonDetailRemarkDTOs:[],

                            articleCode: item.articleCode,
                            description: item.description,
                            materialID: item.materialID,
                            materialTypeID: item.materialTypeID,
                            materialColorID: item.materialColorID,
                            frameMaterialID: item.frameMaterialID,
                            frameMaterialColorID: item.frameMaterialColorID,
                            subMaterialID: item.subMaterialID,
                            subMaterialColorID: item.subMaterialColorID,
                            backCushionID: item.backCushionID,
                            seatCushionID: item.seatCushionID,
                            cushionColorID: item.cushionColorID,
                            fscTypeID: item.fscTypeID,
                            fscPercentID: item.fscPercentID,

                            //salePriceCalculated: $scope.offerDataContainer.offerData.currency === 'USD' ? item.indicatedSellPriceUSD : item.indicatedSellPriceEUR,
                            //quantity: 1,
                            //qnt40HC: item.qnt40HC,

                            //offerItemTypeID: 1,
                            //offerItemTypeNM: 'FOB ITEM',
                            //isApproved: false,
                            //approvedDate: null,
                            //approverName: null
                        });

                        //show message
                        bootbox.alert("Product " + item.articleCode + " has been added success !!!");
                    }
                },
                function (error) {                    
                }
            );
        },

        cancel: function () {
            $scope.isShowSearchModel = false;
        },
       
        selectedModelForFobProductForm: function (modelItem) {
            modelItem.isSelected = true;
            $scope.event.okFobProduct();
        },

        selectedModelForFobSparepartForm: function (modelItem) {
            modelItem.isSelected = true;
            $scope.event.okFobSparepart();
        },
        
        okFobProduct: function () {
            var selectedItems = $scope.searchResult.filter(o => o.isSelected === true);
            //assign product properties
            angular.forEach(selectedItems, function (item) {
                //generate id and pink this value to auto go to edit sparepart property screen
                $scope.offerSeasonDetailID = dataService.getIncrementId();

                //create table pricing option
                var pricingOption = [];
                angular.forEach($scope.data.productElementDTOs, function (item) {
                    pricingOption.push({
                        offerSeasonDetailPriceOption: -1,                        
                        productElementID: item.productElementID,
                        productElementNM: item.productElementNM
                    });
                });

                //push to dto detail
                $scope.data.offerSeasonDetailDTOs.push({
                    offerSeasonDetailID: $scope.offerSeasonDetailID,
                    itemStatus: 1, //PENDING                    
                    isClientSelected: true,
                    markAsClientSelected: true,
                    isNeedFactoryQuotation: true,
                    markAsNeedFactoryQuotation: true,
                    quantity: null,
                    salePrice: null,
                    planingPurchasingPriceRemark: null,

                    modelID: item.modelID,
                    modelUD: item.modelUD,
                    modelNM: item.modelNM,
                    articleCode: item.modelUD + '********************',
                    description: item.modelNM,
                    productFileLocation: item.fileLocation,
                    productThumbnailLocation: item.thumbnailLocation,
                    manufacturerCountryID: item.manufacturerCountryID,
                    packagingMethodID: item.packagingMethodID,
                    offerSeasonDetailPriceOptionDTOs: pricingOption,
                    cushionRemark: '',
                    commissionPercent: $scope.data.offerSeasonDTO.defaultCommissionPercent,
                    offerSeasonDetailRemarkDTOs: [],

                    //default option
                    //articleCode: item.articleCode,
                    //description: item.description,
                    //materialID: item.materialID,
                    //materialTypeID: item.materialTypeID,
                    //materialColorID: item.materialColorID,
                    //frameMaterialID: item.frameMaterialID,
                    //frameMaterialColorID: item.frameMaterialColorID,
                    //subMaterialID: item.subMaterialID,
                    //subMaterialColorID: item.subMaterialColorID,
                    //backCushionID: item.backCushionID,
                    //seatCushionID: item.seatCushionID,
                    //cushionColorID: item.cushionColorID,
                    //fscTypeID: item.fscTypeID,
                    //fscPercentID: item.fscPercentID,
                    //salePriceCalculated: $scope.offerDataContainer.offerData.currency === 'USD' ? item.indicatedSellPriceUSD : item.indicatedSellPriceEUR,
                    //quantity: 1,
                    //qnt40HC: item.qnt40HC,

                    //offerItemTypeID: 1,
                    //offerItemTypeNM: 'FOB ITEM',
                    //isApproved: false,
                    //approvedDate: null,
                    //approverName: null
                });
            });
            
            //close form search model
            $scope.isShowSearchModel = false;
        },

        okFobSparepart: function () {
            var selectedItems = $scope.searchResult.filter(o => o.isSelected === true);
            //assign sparepart property
            angular.forEach(selectedItems, function (item) {
                //generate id and pink this value to auto go to edit sparepart property screen
                $scope.offerSeasonDetailID = dataService.getIncrementId();

                //push to dto detail
                $scope.data.offerSeasonDetailDTOs.push({
                    offerSeasonDetailID: $scope.offerSeasonDetailID,
                    modelID: item.modelID,
                    modelUD: item.modelUD,
                    modelNM: item.modelNM,
                    articleCode: item.modelUD + '-S-**',
                    description: "SPAREPART / " + this.modelNM,
                    quantity: 1,
                    isEditing: true,
                    isClientSelected: true,
                });
            });

            //close form search model
            $scope.isShowSearchModel = false;
        }        
    };

    //
    //init
    //
    $scope.initController();
}]);
