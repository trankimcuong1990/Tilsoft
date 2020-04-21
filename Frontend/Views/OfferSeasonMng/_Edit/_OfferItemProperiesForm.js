tilsoftApp.controller('_OfferItemProperiesForm', ['$scope', '$timeout', '$cookieStore', 'dataService', '$routeParams', function ($scope, $timeout, $cookieStore, dataService, $routeParams) {
    //
    //property
    //
    $scope.originRouter = $routeParams.originRouter;
    $scope.offerSeasonDetailID = $routeParams.offerSeasonDetailID;
    $scope.offerDetailItem = null;
    $scope.copyOfOfferDetailItem = null;
    $scope.isChangedProperties = false;

    //
    //init controller
    //

    $scope.initController = function () {
        //
        //tracking position
        //
        $scope.pagePosition.pageYOffset = window.pageYOffset;
        window.scrollTo(0, 0);

        //
        // init service
        //
        dataService.serviceUrl = context.serviceUrl;
        dataService.supportServiceUrl = context.supportServiceUrl;
        dataService.token = context.token;

        //
        //load data
        //
        if ($scope.data !== null) {
            //get item is editing
            $scope.offerDetailItem = $scope.data.offerSeasonDetailDTOs.filter(o => parseInt(o.offerSeasonDetailID) === parseInt($scope.offerSeasonDetailID))[0];
            //clone this OfferDetailItem so user can change and cancel change
            $scope.copyOfOfferDetailItem = angular.copy($scope.offerDetailItem);

            if ($scope.offerSeasonDetailID > 0) {
                //open view property form
                $scope.load();
            }
            else {
                //get default property and open winzard
                $scope.getOfferItemDefaultProperties(function () {
                    //redirect to winzard
                    $scope.changeProperty();
                });
            }
        }
        else {
            //auto back to edit-fob-product-form
            $('#goToFobProductForm').click();
        }
    };

    $scope.load = function () {
        //get properties
        var properties = {
            modelID: $scope.copyOfOfferDetailItem.modelID,
            materialID: $scope.copyOfOfferDetailItem.materialID,
            materialTypeID: $scope.copyOfOfferDetailItem.materialTypeID,
            materialColorID: $scope.copyOfOfferDetailItem.materialColorID,
            frameMaterialID: $scope.copyOfOfferDetailItem.frameMaterialID,
            frameMaterialColorID: $scope.copyOfOfferDetailItem.frameMaterialColorID,
            subMaterialID: $scope.copyOfOfferDetailItem.subMaterialID,
            subMaterialColorID: $scope.copyOfOfferDetailItem.subMaterialColorID,
            seatCushionID: $scope.copyOfOfferDetailItem.seatCushionID,
            backCushionID: $scope.copyOfOfferDetailItem.backCushionID,
            cushionColorID: $scope.copyOfOfferDetailItem.cushionColorID,
            fscTypeID: $scope.copyOfOfferDetailItem.fscTypeID,
            fscPercentID: $scope.copyOfOfferDetailItem.fscPercentID
        };
        dataService.getOfferItemProperties(
            properties,
            function (data) {
                jQuery('#content').show();
                var offerItem = $scope.copyOfOfferDetailItem;
                //assign value
                offerItem.materialText = data.data.materialText;
                offerItem.frameText = data.data.frameText;
                offerItem.subMaterialText = data.data.subMaterialText;
                offerItem.cushionText = data.data.cushionText;
                offerItem.fscText = data.data.fscText;
                offerItem.materialThumbnailLocation = data.data.materialThumbnailLocation;
                offerItem.frameMaterialThumbnailLocation = data.data.frameMaterialThumbnailLocation;
                offerItem.subMaterialColorThumbnailLocation = data.data.subMaterialColorThumbnailLocation;
                offerItem.cushionColorThumbnailLocation = data.data.cushionColorThumbnailLocation;
                offerItem.backCushionThumbnailLocation = data.data.backCushionThumbnailLocation;
                offerItem.seatCushionThumbnailLocation = data.data.seatCushionThumbnailLocation;
            },
            function (error) {
            }
        );
    };

    $scope.getOfferItemDefaultProperties = function (openWinzard) {
        dataService.getOfferItemDefaultProperties(
            $scope.copyOfOfferDetailItem.modelID,
            $scope.data.offerSeasonDTO.season,
            function (data) {
                var offerItem = $scope.copyOfOfferDetailItem;
               
                $scope.isChangedProperties = $scope.isChangedProperties || offerItem.modelID !== data.data.modelID;
                $scope.isChangedProperties = $scope.isChangedProperties || offerItem.materialID !== data.data.materialID;
                $scope.isChangedProperties = $scope.isChangedProperties || offerItem.materialTypeID !== data.data.materialTypeID;
                $scope.isChangedProperties = $scope.isChangedProperties || offerItem.materialColorID !== data.data.materialColorID;
                $scope.isChangedProperties = $scope.isChangedProperties || offerItem.frameMaterialID !== data.data.frameMaterialID;
                $scope.isChangedProperties = $scope.isChangedProperties || offerItem.frameMaterialColorID !== data.data.frameMaterialColorID;
                $scope.isChangedProperties = $scope.isChangedProperties || offerItem.subMaterialID !== data.data.subMaterialID;
                $scope.isChangedProperties = $scope.isChangedProperties || offerItem.subMaterialColorID !== data.data.subMaterialColorID;
                $scope.isChangedProperties = $scope.isChangedProperties || offerItem.seatCushionID !== data.data.seatCushionID;
                $scope.isChangedProperties = $scope.isChangedProperties || offerItem.backCushionID !== data.data.backCushionID;
                $scope.isChangedProperties = $scope.isChangedProperties || offerItem.cushionColorID !== data.data.cushionColorID;
                $scope.isChangedProperties = $scope.isChangedProperties || offerItem.fscTypeID !== data.data.fscTypeID;
                $scope.isChangedProperties = $scope.isChangedProperties || offerItem.fscPercentID !== data.data.fscPercentID;

                offerItem.modelID = data.data.modelID;
                offerItem.materialID = data.data.materialID;
                offerItem.materialTypeID = data.data.materialTypeID;
                offerItem.materialColorID = data.data.materialColorID;
                offerItem.frameMaterialID = data.data.frameMaterialID;
                offerItem.frameMaterialColorID = data.data.frameMaterialColorID;
                offerItem.subMaterialID = data.data.subMaterialID;
                offerItem.subMaterialColorID = data.data.subMaterialColorID;
                offerItem.seatCushionID = data.data.seatCushionID;
                offerItem.backCushionID = data.data.backCushionID;
                offerItem.cushionColorID = data.data.cushionColorID;
                offerItem.fscTypeID = data.data.fscTypeID;
                offerItem.fscPercentID = data.data.fscPercentID;

                offerItem.manufacturerCountryID = data.data.manufacturerCountryID;
                offerItem.packagingMethodID = data.data.packagingMethodID;
                offerItem.articleCode = data.data.articleCode;
                offerItem.description = data.data.description;
                offerItem.materialText = data.data.materialText;
                offerItem.frameText = data.data.frameText;
                offerItem.subMaterialText = data.data.subMaterialText;
                offerItem.cushionText = data.data.cushionText;
                offerItem.fscText = data.data.fscText;
                offerItem.materialThumbnailLocation = data.data.materialThumbnailLocation;
                offerItem.frameMaterialThumbnailLocation = data.data.frameMaterialThumbnailLocation;
                offerItem.subMaterialColorThumbnailLocation = data.data.subMaterialColorThumbnailLocation;
                offerItem.cushionColorThumbnailLocation = data.data.cushionColorThumbnailLocation;
                offerItem.backCushionThumbnailLocation = data.data.backCushionThumbnailLocation;
                offerItem.seatCushionThumbnailLocation = data.data.seatCushionThumbnailLocation;
                offerItem.productFileLocation = data.data.productFileLocation;
                offerItem.productThumbnailLocation = data.data.productThumbnailLocation;
               
                //open winzard
                if (openWinzard !== null) openWinzard();
            },
            function (error) {
            }
        );
    };

    $scope.changeProperty = function () {
        var productData = $scope.copyOfOfferDetailItem;
        //open winzard
        productWizard.open({
            articleCode: productData.articleCode,
            description: productData.description,
            modelID: productData.modelID,
            materialID: productData.materialID,
            materialTypeID: productData.materialTypeID,
            materialColorID: productData.materialColorID,
            frameMaterialID: productData.frameMaterialID,
            frameMaterialColorID: productData.frameMaterialColorID,
            subMaterialID: productData.subMaterialID,
            subMaterialColorID: productData.subMaterialColorID,
            backCushionID: productData.backCushionID,
            seatCushionID: productData.seatCushionID,
            cushionColorID: productData.cushionColorID,
            fSCTypeID: productData.fscTypeID,
            fSCPercentID: productData.fscPercentID,
            useFSCLabel: productData.useFSCLabel,
            manufacturerCountryID: productData.manufacturerCountryID,
            packagingMethodID: productData.packagingMethodID,
            materialText: '',
            frameText: '',
            subMaterialText: '',
            cushionText: '',
            fscText: '',
            packagingMethodText: '',
            cushionDescription: productData.cushionRemark,
            displayCushionDescription: true,
            displayPackagingMethod: true,
            modelImage: '',
            frameImage: '',
            materialImage: '',
            subMaterialImage: '',
            cushionImage: '',
            cushionColorImage: '',
            backCushionImage: '',
            seatCushionImage: '',

            //for pricing section
            showPricingOption: true,
            pricingOption: productData.offerSeasonDetailPriceOptionDTOs,
            season: $scope.data.offerSeasonDTO.season,
            currency: $scope.data.offerSeasonDTO.currency,
            discountPercent: productData.discountPercent,
            discountAmount: productData.discountAmount,
            salePriceCalculated: null,
            //for breakdown section
            clientID: $scope.data.offerSeasonDTO.clientID,
            clientSpecialPackagingMethodID: productData.clientSpecialPackagingMethodID,
            specialRequirement: productData.specialRequirement,
            offerLineID: null, //note this case

            onFinish: function (data) {
                //read return data
                $scope.finishedChangeProperties(data);                
            }
        });
    };

    $scope.finishedChangeProperties = function (data) {
        var offerItem = $scope.offerDetailItem;
        var isChanged = false;
        isChanged = isChanged || offerItem.modelID !== data.modelID;
        isChanged = isChanged || offerItem.materialID !== data.materialID;
        isChanged = isChanged || offerItem.materialTypeID !== data.materialTypeID;
        isChanged = isChanged || offerItem.materialColorID !== data.materialColorID;
        isChanged = isChanged || offerItem.frameMaterialID !== data.frameMaterialID;
        isChanged = isChanged || offerItem.frameMaterialColorID !== data.frameMaterialColorID;
        isChanged = isChanged || offerItem.subMaterialID !== data.subMaterialID;
        isChanged = isChanged || offerItem.subMaterialColorID !== data.subMaterialColorID;
        isChanged = isChanged || offerItem.seatCushionID !== data.seatCushionID;
        isChanged = isChanged || offerItem.backCushionID !== data.backCushionID;
        isChanged = isChanged || offerItem.cushionColorID !== data.cushionColorID;
        isChanged = isChanged || offerItem.fscTypeID !== data.fSCTypeID;
        isChanged = isChanged || offerItem.fscPercentID !== data.fSCPercentID;
        isChanged = isChanged || offerItem.packagingMethodID !== data.packagingMethodID;
        if (isChanged) {
            //keep old description to history
            var oldDescription = offerItem.description;

            //update main properties
            offerItem.articleCode = data.articleCode;
            offerItem.description = data.description;
            offerItem.modelID = data.modelID;
            offerItem.materialID = data.materialID;
            offerItem.materialTypeID = data.materialTypeID;
            offerItem.materialColorID = data.materialColorID;
            offerItem.frameMaterialID = data.frameMaterialID;
            offerItem.frameMaterialColorID = data.frameMaterialColorID;
            offerItem.subMaterialID = data.subMaterialID;
            offerItem.subMaterialColorID = data.subMaterialColorID;
            offerItem.backCushionID = data.backCushionID;
            offerItem.seatCushionID = data.seatCushionID;
            offerItem.cushionColorID = data.cushionColorID;
            offerItem.fscTypeID = data.fSCTypeID;
            offerItem.fscPercentID = data.fSCPercentID;
            offerItem.manufacturerCountryID = data.manufacturerCountryID;
            offerItem.cushionDescription = data.cushionDescription;
            offerItem.cushionRemark = data.cushionDescription;
            offerItem.packagingMethodID = data.packagingMethodID;
            offerItem.useFSCLabel = data.useFSCLabel;

            //text
            offerItem.materialText = data.materialText;
            offerItem.frameText = data.frameText;
            offerItem.subMaterialText = data.subMaterialText;
            offerItem.cushionText = data.cushionText;
            offerItem.fscText = data.fscText;
            offerItem.packagingMethodNM = data.packagingMethodText;

            //image
            offerItem.materialThumbnailLocation = data.materialImage;
            offerItem.frameMaterialThumbnailLocation = data.frameImage;
            offerItem.subMaterialColorThumbnailLocation = data.subMaterialImage;
            offerItem.backCushionThumbnailLocation = data.backCushionImage;
            offerItem.seatCushionThumbnailLocation = data.seatCushionImage;
            offerItem.cushionColorThumbnailLocation = data.cushionColorImage;
            offerItem.productFileLocation = $scope.copyOfOfferDetailItem.productFileLocation;
            offerItem.productThumbnailLocation = $scope.copyOfOfferDetailItem.productThumbnailLocation;

            //price option
            offerItem.offerLinePriceOptions = data.pricingOption;
            offerItem.discountPercent = data.discountPercent;
            offerItem.discountAmount = data.discountAmount;
            offerItem.salePriceCalculated = data.salePriceCalculated;

            // approval process
            offerItem.isApproved = false;
            offerItem.approvedBy = null;
            offerItem.approvedDate = null;
            offerItem.approverName = null;

            //breakdown
            offerItem.clientSpecialPackagingMethodID = data.clientSpecialPackagingMethodID;
            offerItem.specialRequirement = data.specialRequirement;

            //mark to finish edited
            offerItem.isEditing = true;            
            offerItem.salePriceConfig = data.salePriceCalculated;
            offerItem.purchasingPriceConfig = data.purchasingPriceCalculated;

            //reset item and history changes properties
            if (offerItem.offerSeasonDetailID > 0) {
                offerItem.isChangedProperties = true;
                if (!offerItem.isCorrectingItem) {
                    $scope.resetPlanningPurchasingPrice(offerItem);

                    //create remark history
                    offerItem.offerSeasonDetailRemarkDTOs.push({
                        offerSeasonDetailRemarkID: dataService.getIncrementId(),
                        remark: oldDescription
                    });
                }
                else {
                    offerItem.isChangedProperties = false;
                }
            }
            else {
                offerItem.salePrice = data.salePriceCalculated;
                offerItem.planingPurchasingPrice = data.purchasingPriceCalculated;
                offerItem.planingPurchasingPriceFactoryID = data.purchasingPriceFactoryID;
                offerItem.isPlaningPurchasingPriceSelected = true;
                //offerItem.planingPurchasingPriceSelectedBy = null;
                //offerItem.planingPurchasingPriceSelectedDate = null;
                //
                // themy: need to set factoryid here
                //
                offerItem.planingPurchasingPriceSourceID = 2;
                if (offerItem.salePrice && offerItem.planingPurchasingPrice) {
                    offerItem.itemStatus = 3;
                }
            }                                    
        }
        else if (offerItem.cushionRemark !== data.cushionDescription) {
            offerItem.description = data.description;
            offerItem.cushionRemark = data.cushionDescription;
            offerItem.isEditing = true;
        }
        
        //auto back to edit-fob-product-form
        $timeout(function () {
            $('#goToFobProductForm').click();
        }, 500);        
    };

    $scope.okGetDefaultProperties = function () {
        var offerDetailItem = $scope.offerDetailItem;
        var copyOfOfferDetailItem = $scope.copyOfOfferDetailItem;

        offerDetailItem.modelID = copyOfOfferDetailItem.modelID;
        offerDetailItem.materialID = copyOfOfferDetailItem.materialID;
        offerDetailItem.materialTypeID = copyOfOfferDetailItem.materialTypeID;
        offerDetailItem.materialColorID = copyOfOfferDetailItem.materialColorID;
        offerDetailItem.frameMaterialID = copyOfOfferDetailItem.frameMaterialID;
        offerDetailItem.frameMaterialColorID = copyOfOfferDetailItem.frameMaterialColorID;
        offerDetailItem.subMaterialID = copyOfOfferDetailItem.subMaterialID;
        offerDetailItem.subMaterialColorID = copyOfOfferDetailItem.subMaterialColorID;
        offerDetailItem.seatCushionID = copyOfOfferDetailItem.seatCushionID;
        offerDetailItem.backCushionID = copyOfOfferDetailItem.backCushionID;
        offerDetailItem.cushionColorID = copyOfOfferDetailItem.cushionColorID;
        offerDetailItem.fscTypeID = copyOfOfferDetailItem.fscTypeID;
        offerDetailItem.fscPercentID = copyOfOfferDetailItem.fscPercentID;

        offerDetailItem.manufacturerCountryID = copyOfOfferDetailItem.manufacturerCountryID;
        offerDetailItem.packagingMethodID = copyOfOfferDetailItem.packagingMethodID;
        offerDetailItem.articleCode = copyOfOfferDetailItem.articleCode;
        offerDetailItem.description = copyOfOfferDetailItem.description;
        offerDetailItem.materialText = copyOfOfferDetailItem.materialText;
        offerDetailItem.frameText = copyOfOfferDetailItem.frameText;
        offerDetailItem.subMaterialText = copyOfOfferDetailItem.subMaterialText;
        offerDetailItem.cushionText = copyOfOfferDetailItem.cushionText;
        offerDetailItem.fscText = copyOfOfferDetailItem.fscText;
        offerDetailItem.materialThumbnailLocation = copyOfOfferDetailItem.materialThumbnailLocation;
        offerDetailItem.frameMaterialThumbnailLocation = copyOfOfferDetailItem.frameMaterialThumbnailLocation;
        offerDetailItem.subMaterialColorThumbnailLocation = copyOfOfferDetailItem.subMaterialColorThumbnailLocation;
        offerDetailItem.cushionColorThumbnailLocation = copyOfOfferDetailItem.cushionColorThumbnailLocation;
        offerDetailItem.backCushionThumbnailLocation = copyOfOfferDetailItem.backCushionThumbnailLocation;
        offerDetailItem.seatCushionThumbnailLocation = copyOfOfferDetailItem.seatCushionThumbnailLocation;
        offerDetailItem.productFileLocation = copyOfOfferDetailItem.productFileLocation;
        offerDetailItem.productThumbnailLocation = copyOfOfferDetailItem.productThumbnailLocation;

        //mark row is editing
        offerDetailItem.isEditing = true;
    };

    $scope.resetPlanningPurchasingPrice = function (offerSeasonDetailItem) {        
        //reset planning price
        offerSeasonDetailItem.planingPurchasingPrice = null;
        offerSeasonDetailItem.planingPurchasingPriceSourceID = null;
        offerSeasonDetailItem.planingPurchasingPriceFactoryID = null;
        offerSeasonDetailItem.isPlaningPurchasingPriceSelected = null;
        offerSeasonDetailItem.planingPurchasingPriceFactoryUD = '';
        
        offerSeasonDetailItem.itemStatus = 2;
    };
    //
    //init
    //
    $scope.initController();
}]);
