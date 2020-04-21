var productWizard = {
    data: {
        articleCode: '',
        description: '',
        modelID: null,
        materialID: null,
        materialTypeID: null,
        materialColorID: null,
        frameMaterialID: null,
        frameMaterialColorID: null,
        subMaterialID: null,
        subMaterialColorID: null,
        cushionID: null,
        cushionColorID: null,
        backCushionID: null,
        seatCushionID: null,
        fSCTypeID: null,
        fSCPercentID: null,
        useFSCLabel: false,
        manufacturerCountryID: null,
        packagingMethodID: null,
        materialText: '',
        frameText: '',
        subMaterialText: '',
        cushionText: '',
        packagingMethodText: '',
        cushionDescription: '',
        fscText: '',
        displayCushionDescription: false,
        displayPackagingMethod: false,
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
        pricingOption: [],
        season: '',
        currency: '',
        discountPercent: null,
        discountAmount: null,
        salePriceCalculated: null,
        purchasingPriceCalculated: null,
        purchasingPriceFactoryID: null,

        //for breakdown section
        clientID: null,
        clientSpecialPackagingMethodID: null,
        specialRequirement: '',
        offerLineID: null,

        onFinish: null
    },
    open: function (initProduct) {
        scope.productWizard.data = initProduct;
        scope.productWizard.startListener = false;
        jsHelper.showPopup(
            'productWizard',
            function () {
                // get all the list
                jQuery.ajax({
                    cache: false,
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + productWizardContext.token
                    },
                    type: "POST",
                    dataType: 'json',
                    url: productWizardContext.supportServiceUrl + 'getproductwizarddata?modelID=' + scope.productWizard.data.modelID + '&clientID=' + scope.productWizard.data.clientID + '&season=' + scope.productWizard.data.season,
                    beforeSend: function () {
                        jsHelper.loadingSwitch(true);
                    },
                    success: function (data) {
                        jsHelper.loadingSwitch(false);

                        // to do
                        scope.$apply(function () {
                            scope.productWizard.modelUD = data.data.modelUD;
                            scope.productWizard.modelNM = data.data.modelNM;
                            scope.productWizard.manufacturerCountryUD = data.data.manufacturerCountryUD;
                            scope.productWizard.manufacturerCountryID = data.data.manufacturerCountryID;
                            scope.productWizard.ImageFile_DisplayUrl = data.data.ImageFile_DisplayUrl;
                            scope.productWizard.ImageFile_FullSizeUrl = data.data.ImageFile_FullSizeUrl;
                            scope.productWizard.hasCushion = data.data.hasCushion;
                            scope.productWizard.hasFrame = data.data.hasFrameMaterial;
                            scope.productWizard.hasSubMaterial = data.data.hasSubMaterial;
                            scope.productWizard.materialOptions = data.data.materialOptions;
                            scope.productWizard.materialTypeOptions = data.data.materialTypeOptions;
                            scope.productWizard.materialColorOptions = data.data.materialColorOptions;
                            scope.productWizard.frameMaterialOptions = data.data.frameMaterialOptions;
                            scope.productWizard.frameMaterialColorOptions = data.data.frameMaterialColorOptions;
                            scope.productWizard.subMaterialOptions = data.data.subMaterialOptions;
                            scope.productWizard.subMaterialColorOptions = data.data.subMaterialColorOptions;
                            scope.productWizard.cushionOptions = data.data.cushionOptions;
                            scope.productWizard.cushionTypeOptions = data.data.cushionTypeOptions;
                            scope.productWizard.cushionColorOptions = data.data.cushionColorOptions;
                            scope.productWizard.modelImage = data.data.imageFile_DisplayUrl;
                            scope.productWizard.packagingMethodOptions = data.data.packagingMethods;
                            scope.productWizard.seatCushionOptions = data.data.seatCushionOptions;
                            scope.productWizard.backCushionOptions = data.data.backCushionOptions;
                            scope.productWizard.fscTypes = data.data.fscTypes;
                            scope.productWizard.fscPercents = data.data.fscPercents;

                            //
                            //price option
                            //
                            scope.productWizard.modelPriceConfigurationDetails = data.data.modelPriceConfigurationDetails;
                            scope.productWizard.modelFixPriceConfigurations = data.data.modelFixPriceConfigurations;

                            //
                            //breakdown cost price
                            //
                            scope.productWizard.breakdownCategories = data.data.breakdownCategories;                            
                            scope.productWizard.breakdownCategoryOptions = data.data.breakdownCategoryOptions;
                            scope.productWizard.breakdownManagementFees = data.data.breakdownManagementFees;
                            scope.productWizard.vnD_USD_ExchangeRate = data.data.vnD_USD_ExchangeRate;
                            scope.productWizard.seasonSpecPercent = data.data.seasonSpecPercent;
                            scope.productWizard.clientSpecialPackagingMethods = data.data.clientSpecialPackagingMethods;

                            //
                            //purchasing price config
                            //
                            scope.productWizard.configuredPurchasingPriceModelConfirmedFixedPrices = data.data.configuredPurchasingPriceModelConfirmedFixedPrices;
                            scope.productWizard.configuredPurchasingPriceModelConfirmedPriceConfigurations = data.data.configuredPurchasingPriceModelConfirmedPriceConfigurations;

                            // 
                            //init state
                            //
                            param = scope.productWizard.data;
                            angular.forEach(scope.productWizard.materialColorOptions, function (value, key) {
                                if (value.materialID === this.materialID && value.materialTypeID === this.materialTypeID && value.materialColorID === this.materialColorID) {
                                    scope.productWizard.selectedMaterialID = value.materialID;
                                    scope.productWizard.selectedMaterialTypeID = value.materialTypeID;
                                    scope.productWizard.selectedMaterialOptionID = value.materialOptionID;
                                    scope.productWizard.materialStandard = value.isStandard;
                                    scope.productWizard.materialText = value.materialColorNM;
                                    scope.productWizard.materialImage = value.imageFile_DisplayUrl;

                                    value.isSelected = true;
                                }
                                else {
                                    value.isSelected = false;
                                }
                            }, param);

                            if (scope.productWizard.hasFrame) {
                                angular.forEach(scope.productWizard.frameMaterialColorOptions, function (value, key) {
                                    if (value.frameMaterialID === this.frameMaterialID && value.frameMaterialColorID === this.frameMaterialColorID && value.materialOptionID === scope.productWizard.selectedMaterialOptionID) {
                                        scope.productWizard.selectedFrameMaterialID = value.frameMaterialID;
                                        scope.productWizard.frameStandard = value.isStandard;
                                        scope.productWizard.frameMaterialText = value.frameMaterialColorNM;
                                        scope.productWizard.frameImage = value.imageFile_DisplayUrl;

                                        value.isSelected = true;
                                    }
                                    else {
                                        value.isSelected = false;
                                    }
                                }, param);
                            }
                            else {
                                scope.productWizard.frameMaterialColorOptions[0].isSelected = true;
                                scope.productWizard.frameMaterialText = scope.productWizard.frameMaterialColorOptions[0].frameMaterialColorNM;
                            }
                            if (scope.productWizard.hasSubMaterial) {

                                angular.forEach(scope.productWizard.subMaterialColorOptions, function (value, key) {
                                    if (value.subMaterialID === this.subMaterialID && value.subMaterialColorID === this.subMaterialColorID) {
                                        scope.productWizard.selectedSubMaterialID = value.subMaterialID;
                                        scope.productWizard.subMaterialStandard = value.isStandard;
                                        scope.productWizard.subMaterialText = value.subMaterialColorNM;
                                        scope.productWizard.subMaterialImage = value.imageFile_DisplayUrl;

                                        value.isSelected = true;
                                    }
                                    else {
                                        value.isSelected = false;
                                    }
                                }, param);
                            }
                            else {
                                scope.productWizard.subMaterialColorOptions[0].isSelected = true;
                                scope.productWizard.subMaterialText = scope.productWizard.subMaterialColorOptions[0].subMaterialColorNM;
                            }

                            if (scope.productWizard.hasCushion) {
                                angular.forEach(scope.productWizard.cushionOptions, function (value, key) {
                                    if (value.cushionID === this.cushionID) {
                                        scope.productWizard.cushionStandard = value.isStandard;
                                        scope.productWizard.cushionText = value.cushionNM;
                                        scope.productWizard.cushionImage = value.imageFile_DisplayUrl;

                                        value.isSelected = true;
                                    }
                                    else {
                                        value.isSelected = false;
                                    }
                                }, param);

                                //cushion color
                                angular.forEach(scope.productWizard.cushionColorOptions, function (value, key) {
                                    if (value.cushionColorID === this.cushionColorID) {
                                        scope.productWizard.selectedCushionTypeID = value.cushionTypeID;
                                        scope.productWizard.cushionColorStandard = value.isStandard;
                                        scope.productWizard.cushionColorText = value.cushionColorNM;
                                        scope.productWizard.cushionColorImage = value.imageFile_DisplayUrl;

                                        value.isSelected = true;
                                    }
                                    else {
                                        value.isSelected = false;
                                    }
                                }, param);

                                // back cushion
                                angular.forEach(scope.productWizard.backCushionOptions, function (value, key) {
                                    if (value.backCushionID === this.backCushionID) {
                                        scope.productWizard.backCushionText = value.backCushionNM;
                                        scope.productWizard.backCushionImage = value.imageFile_DisplayUrl;

                                        value.isSelected = true;
                                    }
                                    else {
                                        value.isSelected = false;
                                    }
                                }, param);

                                // seat cushion
                                angular.forEach(scope.productWizard.seatCushionOptions, function (value, key) {
                                    if (value.seatCushionID === this.seatCushionID) {
                                        scope.productWizard.seatCushionText = value.seatCushionNM;
                                        scope.productWizard.seatCushionImage = value.imageFile_DisplayUrl;

                                        value.isSelected = true;
                                    }
                                    else {
                                        value.isSelected = false;
                                    }
                                }, param);
                            }
                            else {
                                //scope.productWizard.cushionOptions[0].isSelected = true;
                                scope.productWizard.cushionColorOptions[0].isSelected = true;
                                scope.productWizard.cushionText = scope.productWizard.cushionOptions[0].cushionNM;
                                scope.productWizard.cushionColorText = scope.productWizard.cushionColorOptions[0].cushionColorNM;
                                scope.productWizard.backCushionOptions[0].isSelected = true;
                                scope.productWizard.seatCushionOptions[0].isSelected = true;
                                scope.productWizard.backCushionText = scope.productWizard.backCushionOptions[0].backCushionNM;
                                scope.productWizard.seatCushionText = scope.productWizard.seatCushionOptions[0].seatCushionNM;
                            }

                            angular.forEach(scope.productWizard.packagingMethodOptions, function (value, key) {
                                if (value.packagingMethodID === this.packagingMethodID) {
                                    value.isSelected = true;                                    
                                    scope.productWizard.packagingMethodText = value.packagingMethodNM;
                                }
                                else {
                                    value.isSelected = false;
                                }
                            }, param);

                            // fsc type
                            if (scope.productWizard.data.fSCTypeID === null) {
                                scope.productWizard.data.fSCTypeID = 1; // hardcode for NONE fsc type case
                            }
                            if (scope.productWizard.data.fSCPercentID === null) {
                                scope.productWizard.data.fSCPercentID = 1; // hardcode for NONE fsc percent case
                            }
                            if (scope.productWizard.data.fSCTypeID > 1 || scope.productWizard.data.fSCPercentID > 1) {
                                scope.productWizard.fscVisible = true;
                            }
                            else {
                                scope.productWizard.fscVisible = false;
                            }
                            if (scope.productWizard.data.fSCTypeID === 4 || scope.productWizard.data.fSCPercentID === 6) {
                                scope.productWizard.fscPercentVisible = true;
                            }
                            else {
                                scope.productWizard.fscPercentVisible = false;
                            }
                            scope.productWizard.generateFSCText();
                            scope.productWizard.fscFilter.fscTypeID = scope.productWizard.data.fSCTypeID;

                            // use fsc label
                            if (scope.productWizard.data.useFSCLabel && scope.productWizard.data.useFSCLabel === '' && typeof scope.productWizard.data.useFSCLabel === 'undefined') {
                                scope.productWizard.data.useFSCLabel = false;
                            }

                            //init pricing option
                            angular.forEach(productWizard.data.pricingOption, function (item) {
                                scope.productWizard.assignPriceOptionValue(item.productElementID);
                            });                            
                        });
                        scope.productWizard.startListener = true;
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        jsHelper.loadingSwitch(false);
                        jsHelper.showMessage('warning', xhr.responseJSON.exceptionMessage);
                    }
                });
            }
        );
    },
    close: function (dialogResult) {
        if (dialogResult) {
            result = {
                materialValid: false,
                frameValid: false,
                subMaterialValid: false,
                cushionColorValid: false,
                backCushionValid: false,
                seatCushionValid: false,
                fscValid: false,
                packagingMethodValid: false
            };
            scope.productWizard.data.articleCode = scope.productWizard.modelUD;

            // set default for disabled attribute
            if (!scope.productWizard.hasFrame) {
                scope.productWizard.frameMaterialOptions[0].isSelected = true;
                scope.productWizard.frameMaterialColorOptions[0].isSelected = true;
            }
            if (!scope.productWizard.hasCushion) {
                scope.productWizard.cushionColorOptions[0].isSelected = true;
                //scope.productWizard.cushionOptions[0].isSelected = true;
                scope.productWizard.backCushionOptions[0].isSelected = true;
                scope.productWizard.seatCushionOptions[0].isSelected = true;
            }
            if (!scope.productWizard.hasSubMaterial) {
                scope.productWizard.subMaterialColorOptions[0].isSelected = true;
                scope.productWizard.subMaterialOptions[0].isSelected = true;
            }
            angular.forEach(scope.productWizard.frameMaterialColorOptions, function (value, key) {
                if (value.isSelected) {
                    scope.productWizard.data.frameMaterialID = value.frameMaterialID;
                    scope.productWizard.data.frameMaterialColorID = value.frameMaterialColorID;
                    result.frameValid = true;

                    scope.productWizard.data.articleCode += value.frameMaterialUD + value.frameMaterialColorUD;
                }
            }, null);
            angular.forEach(scope.productWizard.materialColorOptions, function (value, key) {
                if (value.isSelected) {
                    scope.productWizard.data.materialID = value.materialID;
                    scope.productWizard.data.materialTypeID = value.materialTypeID;
                    scope.productWizard.data.materialColorID = value.materialColorID;
                    result.materialValid = true;

                    scope.productWizard.data.articleCode += value.materialUD + value.materialTypeUD + value.materialColorUD;
                }
            }, null);
            angular.forEach(scope.productWizard.subMaterialColorOptions, function (value, key) {
                if (value.isSelected) {
                    scope.productWizard.data.subMaterialID = value.subMaterialID;
                    scope.productWizard.data.subMaterialColorID = value.subMaterialColorID;
                    result.subMaterialValid = true;

                    scope.productWizard.data.articleCode += value.subMaterialUD + value.subMaterialColorUD;
                }
            }, null);
            angular.forEach(scope.productWizard.seatCushionOptions, function (value, key) {
                if (value.isSelected) {
                    scope.productWizard.data.seatCushionID = value.seatCushionID;
                    result.seatCushionValid = true;

                    scope.productWizard.data.articleCode += value.seatCushionUD;
                }
            }, null);
            angular.forEach(scope.productWizard.backCushionOptions, function (value, key) {
                if (value.isSelected) {
                    scope.productWizard.data.backCushionID = value.backCushionID;
                    result.backCushionValid = true;

                    scope.productWizard.data.articleCode += value.backCushionUD;
                }
            }, null);
            angular.forEach(scope.productWizard.cushionColorOptions, function (value, key) {
                if (value.isSelected) {
                    scope.productWizard.data.cushionColorID = value.cushionColorID;
                    result.cushionColorValid = true;

                    scope.productWizard.data.articleCode += value.cushionColorUD;
                }
            }, null);
            angular.forEach(scope.productWizard.packagingMethodOptions, function (value, key) {
                if (value.isSelected) {
                    scope.productWizard.data.packagingMethodID = value.packagingMethodID;
                    result.packagingMethodValid = true;
                }
            }, null);

            scope.productWizard.data.articleCode += scope.productWizard.manufacturerCountryUD;
            scope.productWizard.data.manufacturerCountryID = scope.productWizard.manufacturerCountryID;
            scope.productWizard.data.materialText = scope.productWizard.materialText;
            scope.productWizard.data.frameText = scope.productWizard.frameMaterialText;
            scope.productWizard.data.subMaterialText = scope.productWizard.subMaterialText;
            scope.productWizard.data.cushionText = scope.productWizard.seatCushionText + ' (SEAT CUSHION) + ' + scope.productWizard.backCushionText + ' (BACK CUSHION)';
            scope.productWizard.data.packagingMethodText = scope.productWizard.packagingMethodText;

            scope.productWizard.data.fscText = '';
            angular.forEach(scope.productWizard.fscTypes, function (value, key) {
                if (value.fscTypeID === this.fSCTypeID) {
                    if (value.fscTypeNM !== 'NONE') {
                        scope.productWizard.data.fscText += value.fscTypeNM;
                    }
                    scope.productWizard.data.articleCode += value.fscTypeUD;
                }
            }, scope.productWizard.data);
            angular.forEach(scope.productWizard.fscPercents, function (value, key) {
                if (value.fscPercentID === this.fSCPercentID) {
                    if (scope.productWizard.data.fscText !== 'NONE' && value.fscPercentNM !== 'NONE') {
                        scope.productWizard.data.fscText = scope.productWizard.data.fscText.replace('XX%', value.fscPercentNM);
                    }
                    scope.productWizard.data.articleCode += value.fscPercentUD;
                }
            }, scope.productWizard.data);

            scope.productWizard.data.frameImage = scope.productWizard.frameImage;
            scope.productWizard.data.materialImage = scope.productWizard.materialImage;
            scope.productWizard.data.subMaterialImage = scope.productWizard.subMaterialImage;
            scope.productWizard.data.cushionImage = scope.productWizard.cushionImage;
            scope.productWizard.data.cushionColorImage = scope.productWizard.cushionColorImage;
            scope.productWizard.data.backCushionImage = scope.productWizard.backCushionImage;
            scope.productWizard.data.seatCushionImage = scope.productWizard.seatCushionImage;

            if (scope.productWizard.fscVisible === false) {
                scope.productWizard.data.fSCTypeID = 1; // hardcode for NONE fsc type
                scope.productWizard.data.fSCPercentID = 1; // hardcode for NONE fsc percent
            }

            scope.productWizard.data.description = scope.productWizard.generateDescription();
            if (!result.materialValid || !result.frameValid || !result.subMaterialValid || !result.backCushionValid || !result.seatCushionValid || !result.cushionColorValid || !result.packagingMethodValid) {
                alert('Please select all attribute');
                return;
            }

            //pricing option output
            angular.forEach(productWizard.data.pricingOption, function (item) {
                item.modelPriceConfigurationDetailID = productWizard.getModelPriceConfigurationDetailID(item.productElementID);
            });
            scope.productWizard.data.salePriceCalculated = productWizard.getSalePrices();
            scope.productWizard.data.purchasingPriceCalculated = productWizard.getTotalPurchasingConfigPrice();
            scope.productWizard.data.purchasingPriceFactoryID = productWizard.getPurchasingFactory();
        }

        jsHelper.hidePopup(
            'productWizard',
            function () {
                if (dialogResult) {
                    if (scope.productWizard.data.onFinish !== null) {
                        scope.productWizard.data.onFinish(scope.productWizard.data);
                    }
                }

                // reset
                scope.productWizard.data = {
                    articleCode: '',
                    description: '',
                    modelID: null,
                    materialID: null,
                    materialTypeID: null,
                    materialColorID: null,
                    frameMaterialID: null,
                    frameMaterialColorID: null,
                    subMaterialID: null,
                    subMaterialColorID: null,
                    cushionID: null,
                    cushionColorID: null,
                    backCushionID: null,
                    seatCushionID: null,
                    fSCTypeID: null,
                    fSCPercentID: null,
                    useFSCLabel: false,
                    manufacturerCountryID: null,
                    packagingMethodID: null,
                    materialText: '',
                    frameText: '',
                    subMaterialText: '',
                    cushionText: '',
                    packagingMethodText: '',
                    cushionDescription: '',
                    fscText: '',
                    displayCushionDescription: false,
                    displayPackagingMethod: false,
                    modelImage: '',
                    frameImage: '',
                    materialImage: '',
                    subMaterialImage: '',
                    cushionImage: '',
                    cushionColorImage: '',
                    backCushionImage: '',
                    seatCushionImage: '',
                    onFinish: null
                };
                scope.productWizard.modelUD = '';
                scope.productWizard.modelNM = '';
                scope.productWizard.manufacturerCountryUD = '';
                scope.productWizard.ImageFile_DisplayUrl = '';
                scope.productWizard.ImageFile_FullSizeUrl = '';
                scope.productWizard.hasCushion = true;
                scope.productWizard.hasFrame = true;
                scope.productWizard.hasSubMaterial = true;
                scope.productWizard.materialOptions = null;
                scope.productWizard.materialTypeOptions = null;
                scope.productWizard.materialColorOptions = null;
                scope.productWizard.frameMaterialOptions = null;
                scope.productWizard.frameMaterialColorOptions = null;
                scope.productWizard.subMaterialOptions = null;
                scope.productWizard.subMaterialColorOptions = null;
                scope.productWizard.cushionOptions = null;
                scope.productWizard.cushionTypeOptions = null;
                scope.productWizard.cushionColorOptions = null;
                scope.productWizard.seatCushionOptions = null;
                scope.productWizard.backCushionOptions = null;
                scope.productWizard.materialText = '';
                scope.productWizard.frameMaterialText = '';
                scope.productWizard.subMaterialText = '';
                scope.productWizard.cushionText = '';
                scope.productWizard.cushionColorText = '';
                scope.productWizard.packagingMethodText = '';
                scope.productWizard.selectedMaterialID = -1;
                scope.productWizard.selectedMaterialTypeID = -1;
                scope.productWizard.materialStandard = true;
                scope.productWizard.selectedMaterialOptionID = -1;
                scope.productWizard.selectedFrameMaterialID = -1;
                scope.productWizard.frameStandard = true;
                scope.productWizard.selectedSubMaterialID = -1;
                scope.productWizard.subMaterialStandard = true;
                scope.productWizard.selectedCushionTypeID = -1;
                scope.productWizard.cushionColorStandard = true;
                scope.productWizard.cushionStandard = true;
                scope.productWizard.modelImage = '';
                scope.productWizard.frameImage = '';
                scope.productWizard.materialImage = '';
                scope.productWizard.subMaterialImage = '';
                scope.productWizard.cushionImage = '';
                scope.productWizard.cushionColorImage = '';

                fscTypes = null;
                fscPercents = null;
                startListener = false;
            }
        );

        jsHelper.refreshAvsScroll();
    },
    //
    //classic property
    //
    modelUD: '',
    modelNM: '',
    manufacturerCountryUD: '',
    modelImage: '',
    frameImage: '',
    materialImage: '',
    subMaterialImage: '',
    cushionImage: '',
    cushionColorImage: '',
    backCushionImage: '',
    seatCushionImage: '',
    hasCushion: true,
    hasFrame: true,
    hasSubMaterial: true,
    materialOptions: null,
    materialTypeOptions: null,
    materialColorOptions: null,
    frameMaterialOptions: null,
    frameMaterialColorOptions: null,
    subMaterialOptions: null,
    subMaterialColorOptions: null,
    cushionOptions: null,
    seatCushionOptions: null,
    backCushionOptions: null,
    cushionTypeOptions: null,
    cushionColorOptions: null,
    packagingMethodOptions: null,
    fscTypes: null,
    fscPercents: null,
    startListener: false,
    //pricing option
    modelPriceConfigurationDetails: [],
    modelFixPriceConfigurations: [],
    //purchasing price configuration
    configuredPurchasingPriceModelConfirmedFixedPrices: [],
    configuredPurchasingPriceModelConfirmedPriceConfigurations: [],

    //
    //selected text property
    //
    materialText: '',
    frameMaterialText: '',
    subMaterialText: '',
    cushionText: '',
    cushionColorText: '',
    backCushionText: '',
    seatCushionText: '',
    packagingMethodText: '',
    fscText: '',

    //
    //event
    //
    fscVisible: false,
    fscPercentVisible: false,
    fscLabelVisible: true,
    fscFilter: { fscTypeID: null },
    onFSCChange: function () {
        if (scope.productWizard.startListener) {
            scope.productWizard.fscPercentVisible = false;
            scope.productWizard.data.fSCTypeID = 1;
            scope.productWizard.data.fSCPercentID = 1;
            scope.productWizard.fscFilter.fscTypeID = 1;
            scope.productWizard.data.useFSCLabel = false;

            // set default if enable
            if (scope.productWizard.fscVisible) {
                scope.productWizard.fscPercentVisible = false;
                scope.productWizard.data.fSCTypeID = 2;
                scope.productWizard.data.fSCPercentID = 9;
                scope.productWizard.fscFilter.fscTypeID = 2;
                scope.productWizard.data.useFSCLabel = false;
            }
        }
        scope.productWizard.generateFSCText();
        //assign pricing for product element
        var producElementID = 1 //FSC
        scope.productWizard.assignPriceOptionValue(producElementID);
    },
    onFSCTypeChange: function () {
        if (scope.productWizard.startListener) {
            if (scope.productWizard.data.fSCTypeID === 4 || scope.productWizard.data.fSCTypeID === 6) {
                scope.productWizard.fscPercentVisible = true;
            }
            else {
                scope.productWizard.fscPercentVisible = false;
            }

            scope.productWizard.fscFilter.fscTypeID = scope.productWizard.data.fSCTypeID;
            scope.productWizard.data.fSCPercentID = null;
            angular.forEach(scope.productWizard.fscPercents, function (value, key) {
                if (value.fscTypeID === this.fSCTypeID) {
                    scope.productWizard.data.fSCPercentID = value.fscPercentID;
                }
            }, scope.productWizard.data);

            scope.productWizard.generateFSCText();
            //assign pricing for product element
            var producElementID = 1 //FSC
            scope.productWizard.assignPriceOptionValue(producElementID);

        }
    },
    onFSCPercentChange: function () {
        if (scope.productWizard.startListener) {
            scope.productWizard.generateFSCText();
        }
    },
    generateFSCText: function () {
        scope.productWizard.data.fscText = '';
        angular.forEach(scope.productWizard.fscTypes, function (value, key) {
            if (value.fscTypeID === this.fSCTypeID) {
                scope.productWizard.data.fscText += value.fscTypeNM;
            }
        }, scope.productWizard.data);
        if (scope.productWizard.data.fscText !== 'NONE') {
            angular.forEach(scope.productWizard.fscPercents, function (value, key) {
                if (value.fscPercentID === this.fSCPercentID) {
                    if (value.fscPercentNM !== 'NONE') {
                        scope.productWizard.data.fscText = scope.productWizard.data.fscText.replace('XX%', value.fscPercentNM);
                    }
                }
            }, scope.productWizard.data);
        }
    },

    selectedMaterialID: -1,
    selectedMaterialTypeID: -1,
    materialStandard: true,
    materialSelect: function () {
        scope.productWizard.selectedMaterialTypeID = -1;
        scope.productWizard.materialStandardSelect();
    },
    materialTypeSelect: function () {
        scope.productWizard.materialText = '';
        scope.productWizard.frameMaterialText = '';
        scope.productWizard.frameImage = '';
        scope.productWizard.materialImage = '';
        scope.productWizard.selectedMaterialOptionID = -1;
        angular.forEach(scope.productWizard.materialColorOptions, function (value, key) {
            value.isSelected = false;
        }, null);
        angular.forEach(scope.productWizard.frameMaterialColorOptions, function (value, key) {
            value.isSelected = false;
        }, null);
    },
    materialColorSelect: function (materialOptionID) {
        scope.productWizard.selectedMaterialOptionID = materialOptionID;
        var params = {
            selectedID: materialOptionID
        };
        angular.forEach(scope.productWizard.materialColorOptions, function (value, key) {
            if (value.materialOptionID === this.selectedID) {
                value.isSelected = true;
                scope.productWizard.materialText = value.materialColorNM;
                scope.productWizard.materialImage = value.imageFile_DisplayUrl;
            }
            else {
                value.isSelected = false;
            }
        }, params);
        //assign pricing for product element
        var producElementID = 2; //Material Color
        scope.productWizard.assignPriceOptionValue(producElementID);
    },
    materialStandardSelect: function () {
        scope.productWizard.materialText = '';
        scope.productWizard.frameMaterialText = '';
        scope.productWizard.frameImage = '';
        scope.productWizard.materialImage = '';
        scope.productWizard.selectedMaterialOptionID = -1;
        scope.productWizard.selectedMaterialTypeID = -1;
        angular.forEach(scope.productWizard.materialColorOptions, function (value, key) {
            value.isSelected = false;
        }, null);
        angular.forEach(scope.productWizard.frameMaterialColorOptions, function (value, key) {
            value.isSelected = false;
        }, null);
    },

    selectedMaterialOptionID: -1,
    selectedFrameMaterialID: -1,
    frameStandard: true,
    frameMaterialSelect: function () {
        scope.productWizard.frameMaterialText = '';
        scope.productWizard.frameImage = '';
        angular.forEach(scope.productWizard.frameMaterialColorOptions, function (value, key) {
            value.isSelected = false;
        }, null);
        //assign pricing for product element
        var producElementID = 3 //Frame Material
        scope.productWizard.assignPriceOptionValue(producElementID);
    },
    frameMaterialColorSelect: function (frameMaterialOptionID) {
        var params = {
            selectedID: frameMaterialOptionID
        };
        angular.forEach(scope.productWizard.frameMaterialColorOptions, function (value, key) {
            if (value.frameMaterialOptionID === this.selectedID && value.materialOptionID === scope.productWizard.selectedMaterialOptionID) {
                value.isSelected = true;
                scope.productWizard.frameMaterialText = value.frameMaterialColorNM;
                scope.productWizard.frameImage = value.imageFile_DisplayUrl;
            }
            else {
                value.isSelected = false;
            }
        }, params);
    },
    frameMaterialStandardSelect: function () {
        scope.productWizard.frameMaterialText = '';
        scope.productWizard.frameImage = '';
        scope.productWizard.selectedFrameMaterialID = -1;
        angular.forEach(scope.productWizard.frameMaterialColorOptions, function (value, key) {
            value.isSelected = false;
        }, null);
    },

    selectedSubMaterialID: -1,
    subMaterialStandard: true,
    subMaterialSelect: function () {
        scope.productWizard.subMaterialText = '';
        scope.productWizard.subMaterialImage = '';
        angular.forEach(scope.productWizard.subMaterialColorOptions, function (value, key) {
            value.isSelected = false;
        }, null);
    },
    subMaterialColorSelect: function (subMaterialOptionID) {
        var params = {
            selectedID: subMaterialOptionID
        };
        angular.forEach(scope.productWizard.subMaterialColorOptions, function (value, key) {
            if (value.subMaterialOptionID === this.selectedID) {
                value.isSelected = true;
                scope.productWizard.subMaterialText = value.subMaterialColorNM;
                scope.productWizard.subMaterialImage = value.imageFile_DisplayUrl;
            }
            else {
                value.isSelected = false;
            }
        }, params);
    },
    subMaterialStandardSelect: function () {
        scope.productWizard.subMaterialText = '';
        scope.productWizard.subMaterialImage = '';
        scope.productWizard.selectedSubMaterialID = -1;
        angular.forEach(scope.productWizard.subMaterialColorOptions, function (value, key) {
            value.isSelected = false;
        }, null);
    },

    selectedCushionTypeID: -1,
    cushionColorStandard: true,
    cushionTypeSelect: function () {
        scope.productWizard.cushionColorText = '';
        scope.productWizard.cushionColorImage = '';
        angular.forEach(scope.productWizard.cushionColorOptions, function (value, key) {
            value.isSelected = false;
        }, null);
    },
    cushionColorSelect: function (cushionColorID) {
        var params = {
            selectedID: cushionColorID
        };
        angular.forEach(scope.productWizard.cushionColorOptions, function (value, key) {
            if (value.cushionColorID === this.selectedID) {
                value.isSelected = true;
                scope.productWizard.cushionColorText = value.cushionColorNM;
                scope.productWizard.cushionColorImage = value.imageFile_DisplayUrl;
            }
            else {
                value.isSelected = false;
            }
        }, params);

        //assign pricing for product element
        var producElementID = 4 //Cushion Color
        scope.productWizard.assignPriceOptionValue(producElementID);
    },
    cushionColorStandardSelect: function () {
        scope.productWizard.cushionColorText = '';
        scope.productWizard.cushionColorImage = '';
        scope.productWizard.selectedCushionTypeID = -1;
        angular.forEach(scope.productWizard.cushionColorOptions, function (value, key) {
            value.isSelected = false;
        }, null);

        //assign pricing for product element
        var producElementID = 4 //Cushion Color
        scope.productWizard.assignPriceOptionValue(producElementID);
    },

    cushionStandard: true,
    cushionSelect: function (cushionID) {
        var params = {
            selectedID: cushionID
        };
        angular.forEach(scope.productWizard.cushionOptions, function (value, key) {
            if (value.cushionID === this.selectedID) {
                value.isSelected = true;
                scope.productWizard.cushionText = value.cushionNM;
                scope.productWizard.cushionImage = value.imageFile_DisplayUrl;
            }
            else {
                value.isSelected = false;
            }
        }, params);
    },
    cushionStandardSelect: function () {
        scope.productWizard.cushionText = '';
        scope.productWizard.cushionImage = '';
        angular.forEach(scope.productWizard.cushionOptions, function (value, key) {
            value.isSelected = false;
        }, null);
    },

    seatCushionSelect: function (seatCushionID) {
        var params = {
            selectedID: seatCushionID
        };
        angular.forEach(scope.productWizard.seatCushionOptions, function (value, key) {
            if (value.seatCushionID === this.selectedID) {
                value.isSelected = true;
                scope.productWizard.seatCushionText = value.seatCushionNM;
                scope.productWizard.seatCushionImage = value.imageFile_DisplayUrl;
            }
            else {
                value.isSelected = false;
            }
        }, params);
    },
    backCushionSelect: function (backCushionID) {
        var params = {
            selectedID: backCushionID
        };
        angular.forEach(scope.productWizard.backCushionOptions, function (value, key) {
            if (value.backCushionID === this.selectedID) {
                value.isSelected = true;
                scope.productWizard.backCushionText = value.backCushionNM;
                scope.productWizard.backCushionImage = value.imageFile_DisplayUrl;
            }
            else {
                value.isSelected = false;
            }
        }, params);
    },

    //selectedPackagingMethodID: -1,
    selectedPackagingMethodID: 17,
    packagingMethodSelect: function (id) {
        param = id;
        scope.productWizard.packagingMethodText = '';
        angular.forEach(scope.productWizard.packagingMethodOptions, function (value, key) {
            if (value.packagingMethodID === param) {
                value.isSelected = true;
                scope.productWizard.packagingMethodText = value.packagingMethodNM;
                scope.selectedPackagingMethodID = value.packagingMethodID;
            }
            else {
                value.isSelected = false;
                //selectedPackagingMethodID = -1;
                selectedPackagingMethodID = 17; // TBA with code 0
            }
        }, param);

        //assign pricing for product element
        var producElementID = 5; //Packaging Method
        scope.productWizard.assignPriceOptionValue(producElementID);       
    },
    generateDescription: function () {
        var displayDescription = '';
        displayDescription = scope.productWizard.modelNM;
        if (scope.productWizard.frameMaterialText !== 'NONE NONE' && scope.productWizard.frameMaterialText !== '') {
            displayDescription += ' / ' + scope.productWizard.frameMaterialText;
        }
        displayDescription += ' / ' + scope.productWizard.materialText;
        if (scope.productWizard.subMaterialText !== 'NONE NONE' && scope.productWizard.subMaterialText !== '') {
            displayDescription += ' / ' + scope.productWizard.subMaterialText;
        }
        if (scope.productWizard.data.fscText !== 'NONE' && scope.productWizard.data.fscText !== '') {
            displayDescription += ' / ' + scope.productWizard.data.fscText;
        }
        if (scope.productWizard.seatCushionText !== 'NONE' && scope.productWizard.seatCushionText !== '') {
            displayDescription += ' / SEAT CUSHION: ' + scope.productWizard.seatCushionText;
            if (scope.productWizard.backCushionText !== 'NONE' && scope.productWizard.backCushionText !== '') {
                displayDescription += ' + BACK CUSHION: ' + scope.productWizard.backCushionText;
            }
        }
        else {
            if (scope.productWizard.backCushionText !== 'NONE' && scope.productWizard.backCushionText !== '') {
                displayDescription += ' / BACK CUSHION: ' + scope.productWizard.backCushionText;
            }
        }
        if (scope.productWizard.cushionColorText !== 'NONE' && scope.productWizard.cushionColorText !== '') {
            displayDescription += ' ' + scope.productWizard.cushionColorText;
        }
        if (scope.productWizard.data.displayCushionDescription && scope.productWizard.data.cushionDescription && scope.productWizard.data.cushionDescription !== '' && typeof scope.productWizard.data.cushionDescription !== 'undefined') {
            displayDescription += ' (' + scope.productWizard.data.cushionDescription + ')';
        }
        return displayDescription;
    },

    //
    //sale price config feature
    //
    getOptionID: function (productElement) {
        var optionID = -999;
        if (productElement === 1) {
            optionID = parseInt(scope.productWizard.data.fSCTypeID);
        }
        else if (productElement === 2) {
            angular.forEach(scope.productWizard.materialColorOptions, function (item) {
                if (item.isSelected) {
                    optionID = parseInt(item.materialColorID);
                }
            });
        }
        else if (productElement === 3) {
            optionID = parseInt(scope.productWizard.selectedFrameMaterialID);
        }
        else if (productElement === 4) {
            angular.forEach(scope.productWizard.cushionColorOptions, function (item) {
                if (item.isSelected) {
                    optionID = parseInt(item.cushionColorID);
                }
            });
        }
        else if (productElement === 5) {
            angular.forEach(scope.productWizard.packagingMethodOptions, function (item) {
                if (item.isSelected) {
                    optionID = parseInt(item.packagingMethodID);
                }
            });
        }
        return optionID;
    },

    getOptionNM: function (productElement) {
        var displayDescription = 'NONE';
        if (productElement === 1) {
            displayDescription = scope.productWizard.data.fscText;
        }
        else if (productElement === 2) {
            angular.forEach(scope.productWizard.materialColorOptions, function (item) {
                if (item.isSelected) {
                    displayDescription = item.materialColorNM_Origin;
                }
            });
        }
        else if (productElement === 3) {
            var frameMaterialID = scope.productWizard.selectedFrameMaterialID;
            if (frameMaterialID !== -1) {
                angular.forEach(scope.productWizard.frameMaterialOptions, function (item) {
                    if (item.frameMaterialID === frameMaterialID) {
                        displayDescription = item.frameMaterialNM;
                    }
                });
            }
        }
        else if (productElement === 4) {
            angular.forEach(scope.productWizard.cushionColorOptions, function (item) {
                if (item.isSelected) {
                    displayDescription = item.cushionColorNM;
                }
            });
        }
        else if (productElement === 5) {
            angular.forEach(scope.productWizard.packagingMethodOptions, function (item) {
                if (item.isSelected) {
                    displayDescription = item.packagingMethodNM;
                }
            });
        }
        return displayDescription;
    },

    getModelPriceConfigurationDetailID: function (productElementID) {
        var season = scope.productWizard.data.season;
        var optionID = scope.productWizard.getOptionID(productElementID);
        var result = null;
        angular.forEach(scope.productWizard.modelPriceConfigurationDetails, function (item) {
            if (item.productElementID === productElementID && item.optionID === optionID && item.season === season) {
                result = item.modelPriceConfigurationDetailID;
            }
        });
        return result;
    },

    getPercentValue: function (productElementID) {
        var season = scope.productWizard.data.season;
        var optionID = scope.productWizard.getOptionID(productElementID);
        var result = null;
        angular.forEach(scope.productWizard.modelPriceConfigurationDetails, function (item) {
            if (item.productElementID === productElementID && item.optionID === optionID && item.season === season) {
                result = item.percentValue;
            }
        });
        return result;
    },

    getUSDAmount: function (productElementID) {
        var season = scope.productWizard.data.season;
        var optionID = scope.productWizard.getOptionID(productElementID);
        var result = null;
        angular.forEach(scope.productWizard.modelPriceConfigurationDetails, function (item) {
            if (item.productElementID === productElementID && item.optionID === optionID && item.season === season) {
                result = item.usdAmount;
            }
        });
        return result;
    },

    getEURAmount: function (productElementID) {
        var season = scope.productWizard.data.season;
        var optionID = scope.productWizard.getOptionID(productElementID);
        var result = null;
        angular.forEach(scope.productWizard.modelPriceConfigurationDetails, function (item) {
            if (item.productElementID === productElementID && item.optionID === optionID && item.season === season) {
                result = item.eurAmount;
            }
        });
        return result;
    },

    getStandardPrice: function () {
        var season = scope.productWizard.data.season;
        var currency = scope.productWizard.data.currency;
        var materialTypeID = scope.productWizard.selectedMaterialTypeID; //materialTypeID==-1 if user not selected yet        
        var result = null;

        //return materialTypeID;
        //get fix price
        var usdAmount = null;
        var eurAmount = null;
        var usdAmountFallback = null;
        var eurAmountFallback = null;
        
        angular.forEach(scope.productWizard.modelFixPriceConfigurations, function (item) {
            if (item.materialTypeID === materialTypeID && item.season === season) {
                usdAmount = item.usdAmount;
                eurAmount = item.eurAmount;
            }
        });
        angular.forEach(scope.productWizard.modelFixPriceConfigurations, function (item) {
            if (item.materialTypeID === -1 && item.season === season) {
                usdAmountFallback = item.usdAmount;
                eurAmountFallback = item.eurAmount;
            }
        });
        if (currency === 'USD') {
            result = usdAmount;
            if (result === null) {
                result = usdAmountFallback === null ? 0 : usdAmountFallback;
            }            
        }
        else if (currency === 'EUR') {
            result = eurAmount;
            if (result === null) {
                result = eurAmountFallback === null ? 0 : eurAmountFallback;
            }  
        }
        return result;
    },

    getIncreasePriceByItem: function (item) {
        var price = parseFloat(0);
        var standardPrice = parseFloat(productWizard.getStandardPrice());
        if (item.increasePercent !== null && item.increasePercent !== '') {
            price = standardPrice * parseFloat(item.increasePercent) / 100;
        }
        if (item.increaseAmount !== null) {
            price = price + parseFloat(item.increaseAmount);
        }
        return price;
    },

    getPriceIncludeSpecification: function () {
        var price = parseFloat(0);
        var standardPrice = parseFloat(productWizard.getStandardPrice());
        angular.forEach(productWizard.data.pricingOption, function (item) {
            price += parseFloat(productWizard.getIncreasePriceByItem(item));
        });
        return price + standardPrice;
    },

    getDiscountAmount: function () {
        var price = parseFloat(0);
        var standardPrice = parseFloat(productWizard.getStandardPrice());
        var discountPercent = parseFloat(productWizard.data.discountPercent === null || productWizard.data.discountPercent === '' || productWizard.data.discountPercent === undefined ? 0 : productWizard.data.discountPercent);
        price = standardPrice * discountPercent / 100;
        var discountAmount = parseFloat(productWizard.data.discountAmount === null || productWizard.data.discountAmount === '' || productWizard.data.discountAmount === undefined ? 0 : productWizard.data.discountAmount);
        price = price + discountAmount;
        return price;             
    },

    getSalePrices: function () {
        var price = parseFloat(0);
        var priceIncludeSpecification = parseFloat(productWizard.getPriceIncludeSpecification());
        var discount = parseFloat(productWizard.getDiscountAmount());
        price = priceIncludeSpecification - discount;
        return price;
    },

    getStandardDescription: function () {
        var modelNM = productWizard.modelNM;
        var materialID = productWizard.selectedMaterialID;
        var materialTypeID = productWizard.selectedMaterialTypeID;
        var materialNM = '';
        var materialTypeNM = '';
        angular.forEach(productWizard.materialOptions, function (item) {
            if (item.materialID === materialID) {
                materialNM = item.materialNM;
            }
        });
        angular.forEach(productWizard.materialTypeOptions, function (item) {
            if (item.materialTypeID === materialTypeID) {
                materialTypeNM = item.materialTypeNM;
            }
        });
        return modelNM + ' / ' + materialNM + ' ' + materialTypeNM;
    },

    assignPriceOptionValue: function (productElementID) {
        let productElementItems = productWizard.data.pricingOption.filter(function (o) { return o.productElementID === productElementID });
        if (productElementItems.length > 0) {
            var productElementItem = productElementItems[0];
            productElementItem.increasePercent = productWizard.getPercentValue(productElementID);

            if (productWizard.data.currency === 'USD') {
                productElementItem.increaseAmount = productWizard.getUSDAmount(productElementID);
            }
            else if (productWizard.data.currency === 'EUR') {
                productElementItem.increaseAmount = productWizard.getEURAmount(productElementID);
            }
        }        
    },

    //
    //breakdown
    //
    getBreakdownCategoryByProductElement: function (productElementID) {
        switch (productElementID) {
            case 1:
                return 8;
            case 2:
                return 1;
            case 3:
                return 2;
            case 4:
                return 4;
            case 5:
                return 5;
            default:
                return 0;
        }
    },

    getBreakdownCategoryOptionText: function (breakdownCategoryID) {
        switch (breakdownCategoryID) {
            case 1:
                return scope.productWizard.materialText;
            case 2:
                return scope.productWizard.frameMaterialText;
            case 3:
                return scope.productWizard.subMaterialText;
            case 4:
                return scope.productWizard.cushionColorText + '(BACK CUSHION: ' + scope.productWizard.backCushionText + ' + SEAT CUSHION: ' + scope.productWizard.seatCushionText + ')';
            case 5:
                return scope.productWizard.packagingMethodText;
            case 8:
                return scope.productWizard.data.fscText;
            default:
                return '';
        }
    },

    getCostPrice: function (breakdownCategoryID) {
        //properyty
        var costPrice = {
            avfPrice: 0,
            avtPrice: 0
        };
        var selectedBreakdownOption = [];
        var breakdownCategoryOptions = [];
        var companyID = 0;
        //get breakdown option
        if (scope.productWizard.breakdownCategoryOptions !== null && scope.productWizard.breakdownCategoryOptions !== undefined) {            
            breakdownCategoryOptions = scope.productWizard.breakdownCategoryOptions.filter(o => o.breakdownCategoryID === breakdownCategoryID);
        }
        //get cost
        switch (breakdownCategoryID) {
            case 1: //MATERIAL COST
                if (scope.productWizard.materialColorOptions !== null) {
                    var materialOptions = scope.productWizard.materialColorOptions.filter(o => o.isSelected);
                    if (materialOptions.length > 0) {
                        //get option
                        materialID = materialOptions[0].materialID;
                        materialTypeID = materialOptions[0].materialTypeID;
                        materialColorID = materialOptions[0].materialColorID;
                        //get AVF Price
                        companyID = 1;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.materialID === materialID && o.materialTypeID === materialTypeID && o.materialColorID === materialColorID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                        }
                        //get AVT Price
                        companyID = 3;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.materialID === materialID && o.materialTypeID === materialTypeID && o.materialColorID === materialColorID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                        }
                    }
                }
                break;
            case 2: //FRAME COST
                if (scope.productWizard.frameMaterialColorOptions !== null) {
                    var frameOptions = scope.productWizard.frameMaterialColorOptions.filter(o => o.isSelected);
                    if (frameOptions.length > 0) {
                        //get option
                        frameMaterialID = frameOptions[0].frameMaterialID;
                        frameMaterialColorID = frameOptions[0].frameMaterialColorID;
                        //get AVF price
                        companyID = 1;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.frameMaterialID === frameMaterialID && o.frameMaterialColorID === frameMaterialColorID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                        }
                        //get AVT price
                        companyID = 3;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.frameMaterialID === frameMaterialID && o.frameMaterialColorID === frameMaterialColorID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                        }
                    }
                }                
                break;
            case 3: //SUB MATERIAL COST
                if (scope.productWizard.subMaterialColorOptions !== null) {
                    var subMaterialOptions = scope.productWizard.subMaterialColorOptions.filter(o => o.isSelected);
                    if (subMaterialOptions.length > 0) {
                        //get option
                        subMaterialID = subMaterialOptions[0].subMaterialID;
                        subMaterialColorID = subMaterialOptions[0].subMaterialColorID;
                        //get AVF price
                        companyID = 1;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.subMaterialID === subMaterialID && o.subMaterialColorID === subMaterialColorID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                        }
                        //get AVT price
                        companyID = 3;
                        selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.subMaterialID === subMaterialID && o.subMaterialColorID === subMaterialColorID);
                        if (selectedBreakdownOption.length > 0) {
                            costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                        }
                    }
                }
                break;
            case 4: //CUSHION COST
                //get option
                cushionColorID = 0;
                backCushionID = 0;
                seatCushionID = 0;
                if (scope.productWizard.cushionColorOptions !== null) {
                    var cushionColorOptions = scope.productWizard.cushionColorOptions.filter(o => o.isSelected);
                    if (cushionColorOptions.length > 0) {
                        cushionColorID = cushionColorOptions[0].cushionColorID;
                    }
                }
                if (scope.productWizard.backCushionOptions !== null) {
                    var backCushionOptions = scope.productWizard.backCushionOptions.filter(o => o.isSelected);
                    if (backCushionOptions.length > 0) {
                        backCushionID = backCushionOptions[0].backCushionID;
                    }
                }
                if (scope.productWizard.seatCushionOptions !== null) {
                    var seatCushionOptions = scope.productWizard.seatCushionOptions.filter(o => o.isSelected);
                    if (seatCushionOptions.length > 0) {
                        seatCushionID = seatCushionOptions[0].seatCushionID;
                    }
                }
                //get AVF price
                companyID = 1;
                selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.cushionColorID === cushionColorID && o.backCushionID === backCushionID && o.seatCushionID === seatCushionID);
                if (selectedBreakdownOption.length > 0) {
                    costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                }
                //get AVT price
                companyID = 3;
                selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.cushionColorID === cushionColorID && o.backCushionID === backCushionID && o.seatCushionID === seatCushionID);
                if (selectedBreakdownOption.length > 0) {
                    costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                }
                break;
            case 5: //PACKING COST
                if (scope.productWizard.packagingMethodOptions !== null) {
                    var packagingMethodOptions = scope.productWizard.packagingMethodOptions.filter(o => o.isSelected);
                    if (packagingMethodOptions.length > 0) {
                        //get option
                        var packagingMethodID = packagingMethodOptions[0].packagingMethodID;
                        if (packagingMethodID === 11) { //Packaging Request
                            var clientSpecialPackagingMethodID = scope.productWizard.data.clientSpecialPackagingMethodID;
                            //get AVF price
                            companyID = 1;
                            selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.packagingMethodID === packagingMethodID && o.clientSpecialPackagingMethodID === clientSpecialPackagingMethodID);
                            if (selectedBreakdownOption.length > 0) {
                                costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                            }
                            //get AVT price
                            companyID = 3;
                            selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.packagingMethodID === packagingMethodID && o.clientSpecialPackagingMethodID === clientSpecialPackagingMethodID);
                            if (selectedBreakdownOption.length > 0) {
                                costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                            }
                        }
                        else {
                            //get AVF price
                            companyID = 1;
                            selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.packagingMethodID === packagingMethodID);
                            if (selectedBreakdownOption.length > 0) {
                                costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                            }
                            //get AVT price
                            companyID = 3;
                            selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.packagingMethodID === packagingMethodID);
                            if (selectedBreakdownOption.length > 0) {
                                costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                            }
                        }
                    }
                }                
                break;
            case 8: //FSC COST
                fscTypeID = scope.productWizard.data.fSCTypeID;
                //get AVF price
                companyID = 1;
                selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.fscTypeID === fscTypeID);
                if (selectedBreakdownOption.length > 0) {
                    costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                }
                //get AVT price
                companyID = 3;
                selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.fscTypeID === fscTypeID);
                if (selectedBreakdownOption.length > 0) {
                    costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                }
                break;

            case 9: //SPECIAL REQUIREMENT COST                
                var offerLineID = scope.productWizard.data.offerLineID;
                //get AVF price
                companyID = 1;
                selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.offerLineID === offerLineID);
                if (selectedBreakdownOption.length > 0) {
                    costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                }
                //get AVT price
                companyID = 3;
                selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.offerLineID === offerLineID);
                if (selectedBreakdownOption.length > 0) {
                    costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                }
                break;

            case 6: case 7: case 10: //HARDWARE COST,OTHER MATERIAL COST,MANAGEMENT FEE
                //get AVF price
                companyID = 1;
                selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID);
                if (selectedBreakdownOption.length > 0) {
                    costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                }
                //get AVT price
                companyID = 3;
                selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID);
                if (selectedBreakdownOption.length > 0) {
                    costPrice.avtPrice = selectedBreakdownOption[0].totalPrice;
                }
                break;

            default:
                break;
        }
        //cost amount base on SeasonSpecPercent
        var seasonSpecPercent = scope.productWizard.seasonSpecPercent;
        costPrice.avfPrice = costPrice.avfPrice * (1 + seasonSpecPercent / 100);
        costPrice.avtPrice = costPrice.avtPrice * (1 + seasonSpecPercent / 100);   
        return costPrice;
    },

    getCostPriceUSD: function (breakdownCategoryID) {
        var price = scope.productWizard.getCostPrice(breakdownCategoryID);
        price.avfPrice = jsHelper.round(price.avfPrice / scope.productWizard.vnD_USD_ExchangeRate, 2);
        price.avtPrice = jsHelper.round(price.avtPrice / scope.productWizard.vnD_USD_ExchangeRate, 2);
        return price;
    },

    getManagementFeePercent: function (companyId) {
        var managementFeePercent = 0;
        if (scope.productWizard.breakdownManagementFees !== null && scope.productWizard.breakdownManagementFees !== undefined && scope.productWizard.breakdownManagementFees.length > 0) {
            if (scope.productWizard.breakdownManagementFees.filter(o=>o.companyID === companyId).length > 0) {
                managementFeePercent = scope.productWizard.breakdownManagementFees.filter(o=>o.companyID === companyId)[0].quantityPercent;
            }            
        }        
        return managementFeePercent;
    },

    getTotalCostPrice: function () {
        var totalCostPrice = {
            avfPrice: parseFloat(0),
            avtPrice: parseFloat(0),
            avfManagementFee: parseFloat(0),
            avtManagementFee: parseFloat(0)
        };
        //get breakdown category
        var breakdownCategories = [];
        if (scope.productWizard.breakdownCategories !== null && scope.productWizard.breakdownCategories !== undefined) {
            breakdownCategories = angular.copy(scope.productWizard.breakdownCategories);
            angular.forEach(productWizard.data.pricingOption, function (item) {
                breakdownCategories.push({
                    breakdownCategoryID: scope.productWizard.getBreakdownCategoryByProductElement(item.productElementID)
                });
            });
        }        
        //cal total price
        angular.forEach(breakdownCategories, function (item) {
            var costPrice = scope.productWizard.getCostPrice(item.breakdownCategoryID);
            totalCostPrice.avfPrice += parseFloat(costPrice.avfPrice);
            totalCostPrice.avtPrice += parseFloat(costPrice.avtPrice);
        });
        //management fee amount base on percent
        var managementFeePercentAVF = scope.productWizard.getManagementFeePercent(1);
        var managementFeePercentAVT = scope.productWizard.getManagementFeePercent(3);
        totalCostPrice.avfManagementFee = totalCostPrice.avfPrice * managementFeePercentAVF / 100;
        totalCostPrice.avtManagementFee = totalCostPrice.avtPrice * managementFeePercentAVT / 100;

        //total cost price after management fee
        totalCostPrice.avfPrice = totalCostPrice.avfPrice * (1 + managementFeePercentAVF / 100);
        totalCostPrice.avtPrice = totalCostPrice.avtPrice * (1 + managementFeePercentAVT / 100);

        //total cost amount base on SeasonSpecPercent
        //var seasonSpecPercent = scope.productWizard.seasonSpecPercent;
        //totalCostPrice.avfPrice = totalCostPrice.avfPrice * (1 + seasonSpecPercent / 100);
        //totalCostPrice.avtPrice = totalCostPrice.avtPrice * (1 + seasonSpecPercent / 100);   

        return totalCostPrice;
    },

    getTotalCostPriceUSD: function () {
        var totalCostPrice = scope.productWizard.getTotalCostPrice();
        totalCostPrice.avfPrice = jsHelper.round(totalCostPrice.avfPrice / scope.productWizard.vnD_USD_ExchangeRate, 2);
        totalCostPrice.avtPrice = jsHelper.round(totalCostPrice.avtPrice / scope.productWizard.vnD_USD_ExchangeRate, 2);
        totalCostPrice.avfManagementFee = jsHelper.round(totalCostPrice.avfManagementFee / scope.productWizard.vnD_USD_ExchangeRate, 2);
        totalCostPrice.avtManagementFee = jsHelper.round(totalCostPrice.avtManagementFee / scope.productWizard.vnD_USD_ExchangeRate, 2);
        return totalCostPrice;
    },


    //
    //purchasing price config
    //
    getPurchasingFactory: function () {
        var materialTypeID = scope.productWizard.selectedMaterialTypeID; //materialTypeID==-1 if user not selected yet        
        //get factory
        var factoryID = null;

        var fixPrice = scope.productWizard.configuredPurchasingPriceModelConfirmedFixedPrices.filter(o => o.materialTypeID === materialTypeID || o.materialTypeID === -1);
        if (fixPrice.length > 0) {
            factoryID = fixPrice[0].factoryID;
        }
        return factoryID;
    },

    getPurchasingFixPrice: function () {
        var materialTypeID = scope.productWizard.selectedMaterialTypeID; //materialTypeID==-1 if user not selected yet        
        //get fix price
        var purchasFixPrice = null;

        var fixPrice = scope.productWizard.configuredPurchasingPriceModelConfirmedFixedPrices.filter(o => o.materialTypeID === materialTypeID);
        if (fixPrice.length > 0) {
            purchasFixPrice = fixPrice[0].usdAmount;
        }
        if (purchasFixPrice === null) {
            var fallBackPrice = scope.productWizard.configuredPurchasingPriceModelConfirmedFixedPrices.filter(o => o.materialTypeID === -1);
            if (fallBackPrice.length > 0) {
                purchasFixPrice = fallBackPrice[0].usdAmount;
            }
        }
        var result = purchasFixPrice === null ? 0 : purchasFixPrice;     
        return result;
    },

    getPurchasingConfigPrice: function (productElementID) {
        var result = 0;
        var optionID = scope.productWizard.getOptionID(productElementID);
        var fixPrice = scope.productWizard.getPurchasingFixPrice();
        var configPrice = scope.productWizard.configuredPurchasingPriceModelConfirmedPriceConfigurations.filter(o => o.productElementID === productElementID && o.optionID === optionID);
        if (configPrice.length > 0) {
            var percentValue = parseFloat(configPrice[0].percentValue === null ? 0 : configPrice[0].percentValue);
            var usdAmount = parseFloat(configPrice[0].usdAmount === null ? 0 : configPrice[0].usdAmount);
            result = percentValue * fixPrice / 100 + usdAmount;
        }        
        return result;
    },

    getTotalPurchasingConfigPrice: function () {
        var result = 0;
        var fixPrice = scope.productWizard.getPurchasingFixPrice();
        angular.forEach(scope.productWizard.data.pricingOption, function (item) {
            result += scope.productWizard.getPurchasingConfigPrice(item.productElementID);
        });
        result += fixPrice;
        return result;
    }
};

if (window.addEventListener) // W3C standard
{
    window.addEventListener(
        'load',
        function () {            
            scope.$apply(function () {
                scope.productWizard = productWizard;
            });
        },
        false
    );
}
else if (window.attachEvent) // Microsoft
{
    window.attachEvent(
        'onload',
        function () {
            scope.$apply(function () {
                scope.productWizard = productWizard;
            });
        }
    );
}