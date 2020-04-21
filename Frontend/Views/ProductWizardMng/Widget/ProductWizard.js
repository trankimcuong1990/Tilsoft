tilsoftApp.controller('ProductWizardController', ['$scope', '$timeout', '$cookieStore', 'dataService', '$rootScope', function ($scope, $timeout, $cookieStore, dataService, $rootScope) {
    $scope.productWizard = {};
    //
    //init controller
    //
    $scope.initController = function () {
    };

    //
    // property
    //
    $scope.productWizard.isWinzardShowed = false;
    $scope.productWizard.data = null;
    $scope.productWizard.optionsData = null;
    
    $scope.productWizard.isMaterialStandard = null;
    $scope.productWizard.isFrameStandard = null;
    $scope.productWizard.isSubMaterialStandard = null;
    $scope.productWizard.isCushionStandard = null;

    $scope.productWizard.selectedMaterialOptionID = null;
    $scope.productWizard.selectedCushionTypeID = null;
    $scope.productWizard.useFSC = false;

    //
    // events
    //
    $scope.productWizard.event = {

        //init value
        init: function () {
            var data = $scope.productWizard.data;
            var materialColorOptions = $scope.productWizard.optionsData.materialColorOptions;
            var frameMaterialColorOptions = $scope.productWizard.optionsData.frameMaterialColorOptions;
            var subMaterialColorOptions = $scope.productWizard.optionsData.subMaterialColorOptions;
            var cushionColorOptions = $scope.productWizard.optionsData.cushionColorOptions;
            var seatCushionOptions = $scope.productWizard.optionsData.seatCushionOptions;
            var backCushionOptions = $scope.productWizard.optionsData.backCushionOptions;
            var packagingMethods = $scope.productWizard.optionsData.packagingMethods;

            //material
            angular.forEach(materialColorOptions, function (item) {
                if (item.materialID === data.materialID && item.materialTypeID === data.materialTypeID && item.materialColorID === data.materialColorID) {
                    $scope.productWizard.selectedMaterialOptionID = item.materialOptionID;
                    $scope.productWizard.isMaterialStandard = item.isStandard;
                    item.isSelected = true;
                }
            });

            //frame material
            angular.forEach(frameMaterialColorOptions, function (item) {
                if (item.frameMaterialID === data.frameMaterialID && item.frameMaterialColorID === data.frameMaterialColorID && item.materialOptionID === $scope.productWizard.selectedMaterialOptionID) {
                    $scope.productWizard.isFrameStandard = item.isStandard;
                    item.isSelected = true;                    
                }
            });

            //sub material
            angular.forEach(subMaterialColorOptions, function (item) {
                if (item.subMaterialID === data.subMaterialID && item.subMaterialColorID === data.subMaterialColorID) {
                    $scope.productWizard.isSubMaterialStandard = item.isStandard;
                    item.isSelected = true;
                }
            });

            //cushion color
            angular.forEach(cushionColorOptions, function (item) {
                if (item.cushionColorID === data.cushionColorID) {
                    $scope.productWizard.selectedCushionTypeID = item.cushionTypeID;
                    $scope.productWizard.isCushionStandard = item.isStandard;
                    item.isSelected = true;                                
                }
            });

            //seat cushion
            angular.forEach(seatCushionOptions, function (item) {
                if (item.seatCushionID === data.seatCushionID) {
                    item.isSelected = true;                    
                }
            });

            //back cushion
            angular.forEach(backCushionOptions, function (item) {
                if (item.backCushionID === data.backCushionID) {
                    item.isSelected = true;                    
                }
            });

            //packaging method
            angular.forEach(packagingMethods, function (item) {
                if (item.packagingMethodID === data.packagingMethodID) {
                    item.isSelected = true;
                }
            });
        },

        finished: function () {            
            //set default value for disabled attribute
            $scope.productWizard.method.setDefaultDisabledAttribute();

            //validate that user select all attribute
            if (!$scope.productWizard.method.validateOption()) {
                alert('Please select all attribute for product');
                return;
            }

            //set output data
            $scope.productWizard.method.setOutputData();

            //get output data
            var outputData = {
                data: $scope.productWizard.data,
                optionsData: $scope.productWizard.optionsData
            };
            $rootScope.$broadcast('finishedWinzard', outputData);
        },

        cancel: function () {
            $scope.productWizard.method.hideWinzard();
        },

        //
        //event on options selected
        //
        useFscOnChange: function () {
            if ($scope.productWizard.useFSC) {
                $scope.productWizard.data.fscTypeID = 2;
            }
            else {
                $scope.productWizard.data.fscTypeID = 1;
            }
        },

        materialColorSelect: function (materialColorOptionItem) {
            $scope.productWizard.selectedMaterialOptionID = materialColorOptionItem.materialOptionID;
            $scope.productWizard.optionsData.materialColorOptions.forEach(o => o.isSelected = false);
            materialColorOptionItem.isSelected = true;
            $scope.productWizard.method.resetSelectedAttribute('frame-material-color');            
        },

        frameMaterialColorSelect: function (frameMaterialColorOptionItem) {
            $scope.productWizard.optionsData.frameMaterialColorOptions.forEach(o => o.isSelected = false);
            frameMaterialColorOptionItem.isSelected = true;            
        },

        subMaterialColorSelect: function (subMaterialColorOptionItem) {
            $scope.productWizard.optionsData.subMaterialColorOptions.forEach(o => o.isSelected = false);
            subMaterialColorOptionItem.isSelected = true;
        },

        cushionColorSelect: function (cushionColorOptionItem) {
            $scope.productWizard.optionsData.cushionColorOptions.forEach(o => o.isSelected = false);
            cushionColorOptionItem.isSelected = true;
        },

        seatCushionSelect: function (seatCushionOptionItem) {
            $scope.productWizard.optionsData.seatCushionOptions.forEach(o => o.isSelected = false);
            seatCushionOptionItem.isSelected = true;            
        },

        backCushionSelect: function (backCushionOption) {
            $scope.productWizard.optionsData.backCushionOptions.forEach(o => o.isSelected = false);
            backCushionOption.isSelected = true;            
        },

        packagingMethodSelect: function (packagingMethod) {
            $scope.productWizard.optionsData.packagingMethods.forEach(o => o.isSelected = false);
            packagingMethod.isSelected = true;            
        },

        materialSelect: function () {
            $scope.productWizard.method.resetSelectedAttribute('material-color');
            $scope.productWizard.method.resetSelectedAttribute('frame-material-color');            
        },

        materialTypeSelect: function () {
            $scope.productWizard.method.resetSelectedAttribute('material-color');
            $scope.productWizard.method.resetSelectedAttribute('frame-material-color');  
        },
    };

    $scope.productWizard.method = {
        showWinzard: function () {
            $scope.productWizard.isWinzardShowed = true;
            jQuery('article').hide();
            jQuery('#articleWinzard').show();        
        },

        hideWinzard: function () {
            $scope.productWizard.isWinzardShowed = false;
            jQuery('article').show();
            jQuery('#articleWinzard').hide();
        },

        getOptionsData: function (successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + productWizardContext2.token
                },
                type: "POST",
                dataType: 'json',
                url: productWizardContext2.serviceUrl + 'get-product-option?modelID=' + $scope.productWizard.data.modelID + '&clientID=' + $scope.productWizard.data.clientID + '&season=' + $scope.productWizard.data.season,
                beforeSend: function () {
                    jsHelper.loadingSwitch(true);
                },
                success: function (data) {
                    jsHelper.loadingSwitch(false);
                    successCallBack(data);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    jsHelper.loadingSwitch(false);
                    jsHelper.showMessage('warning', xhr.responseJSON.exceptionMessage);
                    errorCallBack(xhr);
                }
            });
        },

        getMaterialText: function () {
            if (!$scope.productWizard.optionsData) return '';
            var materialColorOptions = $scope.productWizard.optionsData.materialColorOptions.filter(o => o.isSelected);
            if (materialColorOptions.length > 0) {
                return materialColorOptions[0].materialOptionNM === 'NONE NONE NONE' ? '' : materialColorOptions[0].materialOptionNM;
            }
            return '';
        },

        getFrameMaterialText: function () {
            if (!$scope.productWizard.optionsData) return '';
            var frameMaterialColorOptions = $scope.productWizard.optionsData.frameMaterialColorOptions.filter(o => o.isSelected);
            if (frameMaterialColorOptions.length > 0) {
                return frameMaterialColorOptions[0].frameMaterialOptionNM === 'NONE NONE' ? '' : frameMaterialColorOptions[0].frameMaterialOptionNM;
            }
            return '';
        },

        getSubMaterialText: function () {
            if (!$scope.productWizard.optionsData) return '';
            var subMaterialColorOptions = $scope.productWizard.optionsData.subMaterialColorOptions.filter(o => o.isSelected);
            if (subMaterialColorOptions.length > 0) {
                return subMaterialColorOptions[0].subMaterialOptionNM === 'NONE NONE' ? '' : subMaterialColorOptions[0].subMaterialOptionNM;
            }
            return '';
        },

        getCushionColorText: function () {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return '';
            var cushionColorOptions = $scope.productWizard.optionsData.cushionColorOptions.filter(o => o.isSelected);
            if (cushionColorOptions.length > 0) {
                var cushionFR = $scope.productWizard.data.useCushionFR === true ? '-FR' : '';
                return cushionColorOptions[0].cushionColorNM === 'NONE' ? '' : cushionColorOptions[0].cushionColorNM + cushionFR;
            }
            return '';
        },

        getSeatCushionText: function () {
            if (!$scope.productWizard.optionsData) return '';
            var seatCushionOptions = $scope.productWizard.optionsData.seatCushionOptions.filter(o => o.isSelected);
            if (seatCushionOptions.length > 0) {
                return seatCushionOptions[0].seatCushionNM === 'NONE' ? '' : seatCushionOptions[0].seatCushionNM;
            }
            return '';
        },

        getBackCushionText: function () {
            if (!$scope.productWizard.optionsData) return '';
            var backCushionOptions = $scope.productWizard.optionsData.backCushionOptions.filter(o => o.isSelected);
            if (backCushionOptions.length > 0) {
                return backCushionOptions[0].backCushionNM === 'NONE' ? '' : backCushionOptions[0].backCushionNM;
            }
            return '';
        },

        getFSCInfo: function () {
            if (!$scope.productWizard.useFSC) {
                return {
                    useFSCLabel: false,
                    fscTypeID: 1, //NONE
                    fscPercentID: 1, //NONE
                    fscTypeUD: '0',
                    fscTypeNM: 'NONE',
                    fscPercentUD: '0',
                    fscPercentNM: 'NONE'
                };
            }
            else {
                return {
                    useFSCLabel: $scope.productWizard.data.useFSCLabel,
                    fscTypeID: $scope.productWizard.data.fscTypeID,
                    fscPercentID: $scope.productWizard.data.fscPercentID,
                    fscTypeUD: !$scope.productWizard.optionsData ? '' : $scope.productWizard.optionsData.fscTypes.filter(o => o.fscTypeID === $scope.productWizard.data.fscTypeID)[0].fscTypeUD,
                    fscTypeNM: !$scope.productWizard.optionsData ? '' : $scope.productWizard.optionsData.fscTypes.filter(o => o.fscTypeID === $scope.productWizard.data.fscTypeID)[0].fscTypeNM,
                    fscPercentUD: '0',
                    fscPercentNM: 'NONE'
                };
            }
        },

        getPackagingMethodText: function () {
            if (!$scope.productWizard.optionsData) return '';
            var packagingMethods = $scope.productWizard.optionsData.packagingMethods.filter(o => o.isSelected);
            if (packagingMethods.length > 0) {
                return packagingMethods[0].packagingMethodNM === 'NONE' ? '' : packagingMethods[0].packagingMethodNM;
            }
            return '';
        },

        getDescription: function () {
            if (!$scope.productWizard.optionsData) return '';
            var materialText = $scope.productWizard.method.getMaterialText();
            var frameText = $scope.productWizard.method.getFrameMaterialText();
            var subText = $scope.productWizard.method.getSubMaterialText();
            var cushionText = $scope.productWizard.method.getCushionColorText();
            var seatText = $scope.productWizard.method.getSeatCushionText();
            var backText = $scope.productWizard.method.getBackCushionText();
            var fscText = $scope.productWizard.method.getFSCInfo().fscTypeNM;

            var displayDescription = '';
            displayDescription = $scope.productWizard.optionsData.modelNM;
            displayDescription += frameText === '' ? '' : ' / ' + frameText;
            displayDescription += ' / ' + materialText;
            displayDescription += subText === '' ? '' : ' / ' + subText;
            displayDescription += fscText === '' ? '' : ' / ' + fscText;
            if (seatText !== '') {
                displayDescription += ' / SEAT CUSHION: ' + seatText;
                if (backText !== '') {
                    displayDescription += ' + BACK CUSHION: ' + backText;
                }
            }
            else {
                if (backText !== '') {
                    displayDescription += ' / BACK CUSHION: ' + backText;
                }
            }
            displayDescription += cushionText === '' ? '' : ' ' + cushionText;
            //if (scope.productWizard.data.displayCushionDescription && scope.productWizard.data.cushionDescription && scope.productWizard.data.cushionDescription !== '' && typeof scope.productWizard.data.cushionDescription !== 'undefined') {
            //    displayDescription += ' (' + scope.productWizard.data.cushionDescription + ')';
            //}
            return displayDescription;
        },

        setDefaultDisabledAttribute: function () {
            if (!$scope.productWizard.optionsData.hasFrameMaterial) {
                $scope.productWizard.optionsData.frameMaterialColorOptions[0].isSelected = true;
            }
            if (!$scope.productWizard.optionsData.hasSubMaterial) {
                $scope.productWizard.optionsData.subMaterialColorOptions[0].isSelected = true;
            }
            if (!$scope.productWizard.optionsData.hasCushion) {
                $scope.productWizard.optionsData.cushionColorOptions[0].isSelected = true;
                $scope.productWizard.optionsData.seatCushionOptions[0].isSelected = true;
                $scope.productWizard.optionsData.backCushionOptions[0].isSelected = true;
            }
        },

        validateOption: function () {            
            var isValid = true;
            isValid = isValid && $scope.productWizard.optionsData.materialColorOptions.filter(o => o.isSelected).length > 0;
            isValid = isValid && $scope.productWizard.optionsData.frameMaterialColorOptions.filter(o => o.isSelected).length > 0;
            isValid = isValid && $scope.productWizard.optionsData.subMaterialColorOptions.filter(o => o.isSelected).length > 0;
            isValid = isValid && $scope.productWizard.optionsData.cushionColorOptions.filter(o => o.isSelected).length > 0;
            isValid = isValid && $scope.productWizard.optionsData.seatCushionOptions.filter(o => o.isSelected).length > 0;
            isValid = isValid && $scope.productWizard.optionsData.backCushionOptions.filter(o => o.isSelected).length > 0;
            isValid = isValid && $scope.productWizard.optionsData.packagingMethods.filter(o => o.isSelected).length > 0;
            return isValid;
        },

        getArticleCode: function () {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return '';
            var articleCode = '';
            articleCode = $scope.productWizard.optionsData.modelUD;
            articleCode += $scope.productWizard.optionsData.frameMaterialColorOptions.filter(o => o.isSelected)[0].frameMaterialOptionUD;
            articleCode += $scope.productWizard.optionsData.materialColorOptions.filter(o => o.isSelected)[0].materialOptionUD;
            articleCode += $scope.productWizard.optionsData.subMaterialColorOptions.filter(o => o.isSelected)[0].subMaterialOptionUD;
            articleCode += $scope.productWizard.optionsData.seatCushionOptions.filter(o => o.isSelected)[0].seatCushionUD;
            articleCode += $scope.productWizard.optionsData.backCushionOptions.filter(o => o.isSelected)[0].backCushionUD;
            articleCode += $scope.productWizard.optionsData.cushionColorOptions.filter(o => o.isSelected)[0].cushionColorUD;
            articleCode += $scope.productWizard.optionsData.manufacturerCountryUD;
            articleCode += $scope.productWizard.method.getFSCInfo().fscTypeUD;
            articleCode += $scope.productWizard.method.getFSCInfo().fscPercentUD;
            articleCode += $scope.productWizard.data.useCushionFR === true ? '1' : '0';
            return articleCode;
        },

        setOutputData: function () {                        
            var materialColorOptions = $scope.productWizard.optionsData.materialColorOptions.filter(o => o.isSelected);
            var frameMaterialColorOptions = $scope.productWizard.optionsData.frameMaterialColorOptions.filter(o => o.isSelected);
            var subMaterialColorOptions = $scope.productWizard.optionsData.subMaterialColorOptions.filter(o => o.isSelected);
            var cushionColorOptions = $scope.productWizard.optionsData.cushionColorOptions.filter(o => o.isSelected);
            var seatCushionOptions = $scope.productWizard.optionsData.seatCushionOptions.filter(o => o.isSelected);
            var backCushionOptions = $scope.productWizard.optionsData.backCushionOptions.filter(o => o.isSelected);
            var packagingMethods = $scope.productWizard.optionsData.packagingMethods.filter(o => o.isSelected);

            $scope.productWizard.data.articleCode = $scope.productWizard.method.getArticleCode();
            $scope.productWizard.data.description = $scope.productWizard.method.getDescription();
            $scope.productWizard.data.materialID = materialColorOptions[0].materialID;
            $scope.productWizard.data.materialTypeID = materialColorOptions[0].materialTypeID;
            $scope.productWizard.data.materialColorID = materialColorOptions[0].materialColorID;
            $scope.productWizard.data.frameMaterialID = frameMaterialColorOptions[0].frameMaterialID;
            $scope.productWizard.data.frameMaterialColorID = frameMaterialColorOptions[0].frameMaterialColorID;
            $scope.productWizard.data.subMaterialID = subMaterialColorOptions[0].subMaterialID;
            $scope.productWizard.data.subMaterialColorID = subMaterialColorOptions[0].subMaterialColorID;
            $scope.productWizard.data.backCushionID = backCushionOptions[0].backCushionID;
            $scope.productWizard.data.seatCushionID = seatCushionOptions[0].seatCushionID;
            $scope.productWizard.data.cushionColorID = cushionColorOptions[0].cushionColorID;
            $scope.productWizard.data.fscTypeID = $scope.productWizard.method.getFSCInfo().fscTypeID;
            $scope.productWizard.data.fscPercentID = $scope.productWizard.method.getFSCInfo().fscPercentID;
            //$scope.productWizard.data.useCushionFR = ... //this field get direct from binding
            $scope.productWizard.data.packagingMethodID = packagingMethods[0].packagingMethodID;

            $scope.productWizard.data.materialText = $scope.productWizard.method.getMaterialText();
            $scope.productWizard.data.frameMaterialText = $scope.productWizard.method.getFrameMaterialText();
            $scope.productWizard.data.subMaterialText = $scope.productWizard.method.getSubMaterialText();
            $scope.productWizard.data.cushionColorText = $scope.productWizard.method.getCushionColorText();
            $scope.productWizard.data.backCushionText = $scope.productWizard.method.getBackCushionText();
            $scope.productWizard.data.seatCushionText = $scope.productWizard.method.getSeatCushionText();
            $scope.productWizard.data.fscText = $scope.productWizard.method.getFSCInfo().fscTypeNM;
            $scope.productWizard.data.packagingMethodText = $scope.productWizard.method.getPackagingMethodText();
            
            $scope.productWizard.data.materialImage = materialColorOptions[0].imageFile_DisplayUrl;
            $scope.productWizard.data.frameImage = frameMaterialColorOptions[0].imageFile_DisplayUrl;
            $scope.productWizard.data.subMaterialImage = subMaterialColorOptions[0].imageFile_DisplayUrl;
            $scope.productWizard.data.cushionColorImage = cushionColorOptions[0].imageFile_DisplayUrl;
            $scope.productWizard.data.backCushionImage = backCushionOptions[0].imageFile_DisplayUrl;
            $scope.productWizard.data.seatCushionImage = seatCushionOptions[0].imageFile_DisplayUrl;            
        },  

        resetSelectedAttribute: function (typeOfAttribute) {
            if (!$scope.productWizard.optionsData) return;
            switch (typeOfAttribute) {
                case 'material-color':
                    $scope.productWizard.optionsData.materialColorOptions.forEach(o => o.isSelected = false);
                    break;
                case 'frame-material-color':
                    $scope.productWizard.optionsData.frameMaterialColorOptions.forEach(o => o.isSelected = false);               
                    break;
                case 'sub-material-color':
                    $scope.productWizard.optionsData.subMaterialColorOptions.forEach(o => o.isSelected = false);
                    break;
                case 'cushion-color':
                    $scope.productWizard.optionsData.cushionColorOptions.forEach(o => o.isSelected = false);
                    break;
            }
        },

        //
        //sale price config feature
        //
        getOptionID: function (productElement) {
            var optionID = -999;
            if (!$scope.productWizard.optionsData) return optionID;
            if (productElement === 1) {
                optionID = parseInt($scope.productWizard.method.getFSCInfo().fscTypeID);
            }
            else if (productElement === 2) {
                angular.forEach($scope.productWizard.optionsData.materialColorOptions, function (item) {
                    if (item.isSelected) {
                        optionID = parseInt(item.materialColorID);
                    }
                });
            }
            else if (productElement === 3) {
                angular.forEach($scope.productWizard.optionsData.frameMaterialColorOptions, function (item) {
                    if (item.isSelected) {
                        optionID = parseInt(item.frameMaterialID);
                    }
                });
            }
            else if (productElement === 4) {
                angular.forEach($scope.productWizard.optionsData.cushionColorOptions, function (item) {
                    if (item.isSelected) {
                        optionID = parseInt(item.cushionColorID);
                    }
                });
            }
            else if (productElement === 5) {
                angular.forEach($scope.productWizard.optionsData.packagingMethods, function (item) {
                    if (item.isSelected) {
                        optionID = parseInt(item.packagingMethodID);
                    }
                });
            }
            return optionID;
        },

        getOptionNM: function (productElement) {
            if (!$scope.productWizard.optionsData) return '';
            var displayDescription = 'NONE';            
            if (productElement === 1) {
                displayDescription = $scope.productWizard.method.getFSCInfo().fscTypeNM;
            }
            else if (productElement === 2) {
                angular.forEach($scope.productWizard.optionsData.materialColorOptions, function (item) {
                    if (item.isSelected) {
                        displayDescription = item.materialColorNM;
                    }
                });
            }
            else if (productElement === 3) {
                angular.forEach($scope.productWizard.optionsData.frameMaterialOptions, function (item) {
                    if (item.isSelected) {
                        displayDescription = item.frameMaterialNM;
                    }
                });
            }
            else if (productElement === 4) {
                angular.forEach($scope.productWizard.optionsData.cushionColorOptions, function (item) {
                    if (item.isSelected) {
                        displayDescription = item.cushionColorNM;
                    }
                });
            }
            else if (productElement === 5) {
                angular.forEach($scope.productWizard.optionsData.packagingMethodOptions, function (item) {
                    if (item.isSelected) {
                        displayDescription = item.packagingMethodNM;
                    }
                });
            }
            return displayDescription;
        },

        getModelPriceConfigurationDetailID: function (productElementID) {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return null;
            var season = $scope.productWizard.data.season;
            var optionID = $scope.productWizard.method.getOptionID(productElementID);
            var result = null;
            angular.forEach($scope.productWizard.optionsData.modelPriceConfigurationDetails, function (item) {
                if (item.productElementID === productElementID && item.optionID === optionID && item.season === season) {
                    result = item.modelPriceConfigurationDetailID;
                }
            });
            return result;
        },

        getPercentValue: function (productElementID) {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return null;
            var season = $scope.productWizard.data.season;
            var optionID = $scope.productWizard.method.getOptionID(productElementID);
            var result = null;
            angular.forEach($scope.productWizard.optionsData.modelPriceConfigurationDetails, function (item) {
                if (item.productElementID === productElementID && item.optionID === optionID && item.season === season) {
                    result = item.percentValue;
                }
            });
            return result;
        },

        getUSDAmount: function (productElementID) {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return null;
            var season = $scope.productWizard.data.season;
            var optionID = $scope.productWizard.method.getOptionID(productElementID);
            var result = null;
            angular.forEach($scope.productWizard.optionsData.modelPriceConfigurationDetails, function (item) {
                if (item.productElementID === productElementID && item.optionID === optionID && item.season === season) {
                    result = item.usdAmount;
                }
            });
            return result;
        },

        getEURAmount: function (productElementID) {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return null;
            var season = $scope.productWizard.data.season;
            var optionID = $scope.productWizard.method.getOptionID(productElementID);
            var result = null;
            angular.forEach($scope.productWizard.optionsData.modelPriceConfigurationDetails, function (item) {
                if (item.productElementID === productElementID && item.optionID === optionID && item.season === season) {
                    result = item.eurAmount;
                }
            });
            return result;
        },

        getStandardPrice: function () {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return null;
            var season = $scope.productWizard.data.season;
            var currency = $scope.productWizard.data.currency;
            var materialTypeID = -1; //materialTypeID==-1 if user not selected yet 
            angular.forEach($scope.productWizard.optionsData.materialColorOptions, function (item) {
                if (item.isSelected) {
                    materialTypeID = parseInt(item.materialTypeID);
                }
            });
            var result = null;

            //return materialTypeID;
            //get fix price
            var usdAmount = null;
            var eurAmount = null;
            var usdAmountFallback = null;
            var eurAmountFallback = null;

            angular.forEach($scope.productWizard.optionsData.modelFixPriceConfigurations, function (item) {
                if (item.materialTypeID === materialTypeID && item.season === season) {
                    usdAmount = item.usdAmount;
                    eurAmount = item.eurAmount;
                }
            });
            angular.forEach($scope.productWizard.optionsData.modelFixPriceConfigurations, function (item) {
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
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return 0;
            var price = parseFloat(0);            
            var standardPrice = parseFloat($scope.productWizard.method.getStandardPrice());
            //increase percent
            var increasePercent = $scope.productWizard.method.getPercentValue(item.productElementID);
            if (increasePercent !== null && increasePercent !== '') {
                price = standardPrice * parseFloat(increasePercent) / 100;
            }

            //increase amount
            var increaseAmount = null;
            if ($scope.productWizard.data.currency === 'USD') {
                increaseAmount = $scope.productWizard.method.getUSDAmount(item.productElementID);
            }
            else if ($scope.productWizard.data.currency === 'EUR') {
                increaseAmount = $scope.productWizard.method.getEURAmount(item.productElementID);
            }
            if (item.increaseAmount !== null) {
                price = price + parseFloat(increaseAmount);
            }
            return price;            
        },

        getPriceIncludeSpecification: function () {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return 0;
            var price = parseFloat(0);
            var standardPrice = parseFloat($scope.productWizard.method.getStandardPrice());
            angular.forEach($scope.productWizard.data.pricingOption, function (item) {
                price += parseFloat($scope.productWizard.method.getIncreasePriceByItem(item));
            });
            return price + standardPrice;
        },

        getDiscountAmount: function () {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return 0;
            var price = parseFloat(0);
            var standardPrice = parseFloat($scope.productWizard.method.getStandardPrice());
            var discountPercent = parseFloat($scope.productWizard.data.discountPercent === null || $scope.productWizard.data.discountPercent === '' || $scope.productWizard.data.discountPercent === undefined ? 0 : $scope.productWizard.data.discountPercent);
            price = standardPrice * discountPercent / 100;
            var discountAmount = parseFloat($scope.productWizard.data.discountAmount === null || $scope.productWizard.data.discountAmount === '' || $scope.productWizard.data.discountAmount === undefined ? 0 : $scope.productWizard.data.discountAmount);
            price = price + discountAmount;
            return price;
        },

        getSalePrices: function () {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return 0;
            var price = parseFloat(0);
            var priceIncludeSpecification = parseFloat($scope.productWizard.method.getPriceIncludeSpecification());
            var discount = parseFloat($scope.productWizard.method.getDiscountAmount());
            price = priceIncludeSpecification - discount;
            return price;
        },

        getStandardDescription: function () {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return '';
            var modelNM = $scope.productWizard.optionsData.modelNM;
            var materialNM = '';
            var materialTypeNM = '';
            angular.forEach($scope.productWizard.optionsData.materialColorOptions, function (item) {
                if (item.isSelected) {
                    materialNM = item.materialNM;
                    materialTypeNM = item.materialTypeNM;
                }
            });
            return modelNM + ' / ' + materialNM + ' ' + materialTypeNM;
        },

        assignPriceOptionValue: function (productElementID) {
            let productElementItems = $scope.productWizard.data.pricingOption.filter(function (o) { return o.productElementID === productElementID;});
            if (productElementItems.length > 0) {
                var productElementItem = productElementItems[0];
                productElementItem.increasePercent = $scope.productWizard.method.getPercentValue(productElementID);

                if (productWizard.data.currency === 'USD') {
                    productElementItem.increaseAmount = $scope.productWizard.method.getUSDAmount(productElementID);
                }
                else if ($scope.productWizard.data.currency === 'EUR') {
                    productElementItem.increaseAmount = $scope.productWizard.method.getEURAmount(productElementID);
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
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return '';
            switch (breakdownCategoryID) {
                case 1:
                    return $scope.productWizard.method.getMaterialText();
                case 2:
                    return $scope.productWizard.method.getFrameMaterialText();
                case 3:
                    return $scope.productWizard.method.getSubMaterialText();
                case 4:
                    //return $scope.productWizard.method.getCushionColorText() + '(BACK CUSHION: ' + $scope.productWizard.method.getBackCushionText() + ' + SEAT CUSHION: ' + $scope.productWizard.method.getSeatCushionText + ')';
                    return $scope.productWizard.method.getCushionColorText();
                case 5:
                    return $scope.productWizard.method.getPackagingMethodText();
                case 8:
                    return $scope.productWizard.method.getFSCInfo().fscTypeNM;
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
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return costPrice;
            var selectedBreakdownOption = [];
            var breakdownCategoryOptions = [];
            var companyID = 0;
            //get breakdown option
            if ($scope.productWizard.optionsData.breakdownCategoryOptions !== null && $scope.productWizard.optionsData.breakdownCategoryOptions !== undefined) {
                breakdownCategoryOptions = $scope.productWizard.optionsData.breakdownCategoryOptions.filter(o => o.breakdownCategoryID === breakdownCategoryID);
            }
            //get cost
            switch (breakdownCategoryID) {
                case 1: //MATERIAL COST                    
                    if ($scope.productWizard.optionsData) {
                        var materialOptions = $scope.productWizard.optionsData.materialColorOptions.filter(o => o.isSelected);
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
                    if ($scope.productWizard.optionsData) {
                        var frameOptions = $scope.productWizard.optionsData.frameMaterialColorOptions.filter(o => o.isSelected);
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
                    if ($scope.productWizard.optionsData) {
                        var subMaterialOptions = $scope.productWizard.optionsData.subMaterialColorOptions.filter(o => o.isSelected);
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
                    if ($scope.productWizard.optionsData) {
                        var cushionColorOptions = $scope.productWizard.optionsData.cushionColorOptions.filter(o => o.isSelected);
                        if (cushionColorOptions.length > 0) {
                            cushionColorID = cushionColorOptions[0].cushionColorID;
                        }

                        var backCushionOptions = $scope.productWizard.optionsData.backCushionOptions.filter(o => o.isSelected);
                        if (backCushionOptions.length > 0) {
                            backCushionID = backCushionOptions[0].backCushionID;
                        }

                        var seatCushionOptions = $scope.productWizard.optionsData.seatCushionOptions.filter(o => o.isSelected);
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
                    if ($scope.productWizard.optionsData) {
                        var packagingMethods = $scope.productWizard.optionsData.packagingMethods.filter(o => o.isSelected);
                        if (packagingMethods.length > 0) {
                            //get option
                            var packagingMethodID = packagingMethods[0].packagingMethodID;
                            if (packagingMethodID === 11) { //Packaging Request
                                var clientSpecialPackagingMethodID = $scope.productWizard.data.clientSpecialPackagingMethodID;
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
                    fscTypeID = $scope.productWizard.method.getFSCInfo().fscTypeID;
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
                    var offerSeasonDetailID = $scope.productWizard.data.offerSeasonDetailID;
                    //get AVF price
                    companyID = 1;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.offerSeasonDetailID === offerSeasonDetailID);
                    if (selectedBreakdownOption.length > 0) {
                        costPrice.avfPrice = selectedBreakdownOption[0].totalPrice;
                    }
                    //get AVT price
                    companyID = 3;
                    selectedBreakdownOption = breakdownCategoryOptions.filter(o => o.companyID === companyID && o.offerSeasonDetailID === offerSeasonDetailID);
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
            var seasonSpecPercent = $scope.productWizard.optionsData.seasonSpecPercent;
            costPrice.avfPrice = costPrice.avfPrice * (1 + seasonSpecPercent / 100);
            costPrice.avtPrice = costPrice.avtPrice * (1 + seasonSpecPercent / 100);
            return costPrice;
        },

        getCostPriceUSD: function (breakdownCategoryID) {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return 0;
            var price = $scope.productWizard.method.getCostPrice(breakdownCategoryID);
            price.avfPrice = jsHelper.round(price.avfPrice / $scope.productWizard.optionsData.vnD_USD_ExchangeRate, 2);
            price.avtPrice = jsHelper.round(price.avtPrice / $scope.productWizard.optionsData.vnD_USD_ExchangeRate, 2);
            return price;
        },

        getManagementFeePercent: function (companyId) {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return 0;
            var managementFeePercent = 0;
            var breakdownManagementFees = $scope.productWizard.optionsData.breakdownManagementFees;
            if (breakdownManagementFees !== null && breakdownManagementFees !== undefined && breakdownManagementFees.length > 0) {
                if (breakdownManagementFees.filter(o => o.companyID === companyId).length > 0) {
                    managementFeePercent = breakdownManagementFees.filter(o => o.companyID === companyId)[0].quantityPercent;
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
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return totalCostPrice;
            //get breakdown category
            var breakdownCategories = [];
            if ($scope.productWizard.optionsData.breakdownCategories !== null && $scope.productWizard.optionsData.breakdownCategories !== undefined) {
                breakdownCategories = angular.copy($scope.productWizard.optionsData.breakdownCategories);
                angular.forEach($scope.productWizard.data.pricingOption, function (item) {
                    breakdownCategories.push({
                        breakdownCategoryID: $scope.productWizard.method.getBreakdownCategoryByProductElement(item.productElementID)
                    });
                });
            }
            //cal total price
            angular.forEach(breakdownCategories, function (item) {
                var costPrice = $scope.productWizard.method.getCostPrice(item.breakdownCategoryID);
                totalCostPrice.avfPrice += parseFloat(costPrice.avfPrice);
                totalCostPrice.avtPrice += parseFloat(costPrice.avtPrice);
            });
            //management fee amount base on percent
            var managementFeePercentAVF = $scope.productWizard.method.getManagementFeePercent(1);
            var managementFeePercentAVT = $scope.productWizard.method.getManagementFeePercent(3);
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
            if (!$scope.productWizard.optionsData) return 0;
            var vnD_USD_ExchangeRate = $scope.productWizard.optionsData.vnD_USD_ExchangeRate;
            var totalCostPrice = $scope.productWizard.method.getTotalCostPrice();
            totalCostPrice.avfPrice = jsHelper.round(totalCostPrice.avfPrice / vnD_USD_ExchangeRate, 2);
            totalCostPrice.avtPrice = jsHelper.round(totalCostPrice.avtPrice / vnD_USD_ExchangeRate, 2);
            totalCostPrice.avfManagementFee = jsHelper.round(totalCostPrice.avfManagementFee / vnD_USD_ExchangeRate, 2);
            totalCostPrice.avtManagementFee = jsHelper.round(totalCostPrice.avtManagementFee / vnD_USD_ExchangeRate, 2);
            return totalCostPrice;
        },

        //
        //purchasing price config
        //
        getPurchasingFactory: function () {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return null;
            var materialTypeID = -1; //materialTypeID==-1 if user not selected yet      
            angular.forEach($scope.productWizard.optionsData.materialColorOptions, function (item) {
                if (item.isSelected) {
                    materialTypeID = parseInt(item.materialTypeID);
                }
            });
            //get factory
            var factoryID = null;

            var fixPrice = $scope.productWizard.optionsData.configuredPurchasingPriceModelConfirmedFixedPrices.filter(o => o.materialTypeID === materialTypeID || o.materialTypeID === -1);
            if (fixPrice.length > 0) {
                factoryID = fixPrice[0].factoryID;
            }
            return factoryID;
        },

        getPurchasingFixPrice: function () {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return null;
            var materialTypeID = -1; //materialTypeID==-1 if user not selected yet   
            angular.forEach($scope.productWizard.optionsData.materialColorOptions, function (item) {
                if (item.isSelected) {
                    materialTypeID = parseInt(item.materialTypeID);
                }
            });
            //get fix price
            var purchasFixPrice = null;

            var fixPrice = $scope.productWizard.optionsData.configuredPurchasingPriceModelConfirmedFixedPrices.filter(o => o.materialTypeID === materialTypeID);
            if (fixPrice.length > 0) {
                purchasFixPrice = fixPrice[0].usdAmount;
            }
            if (purchasFixPrice === null) {
                var fallBackPrice = $scope.productWizard.optionsData.configuredPurchasingPriceModelConfirmedFixedPrices.filter(o => o.materialTypeID === -1);
                if (fallBackPrice.length > 0) {
                    purchasFixPrice = fallBackPrice[0].usdAmount;
                }
            }
            var result = purchasFixPrice === null ? 0 : purchasFixPrice;
            return result;
        },

        getPurchasingConfigPrice: function (productElementID) {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return 0;
            var result = 0;
            var optionID = $scope.productWizard.method.getOptionID(productElementID);
            var fixPrice = $scope.productWizard.method.getPurchasingFixPrice();
            var configPrice = $scope.productWizard.optionsData.configuredPurchasingPriceModelConfirmedPriceConfigurations.filter(o => o.productElementID === productElementID && o.optionID === optionID);
            if (configPrice.length > 0) {
                var percentValue = parseFloat(configPrice[0].percentValue === null ? 0 : configPrice[0].percentValue);
                var usdAmount = parseFloat(configPrice[0].usdAmount === null ? 0 : configPrice[0].usdAmount);
                result = percentValue * fixPrice / 100 + usdAmount;
            }
            return result;
        },

        getTotalPurchasingConfigPrice: function () {
            if (!$scope.productWizard.optionsData || !$scope.productWizard.data) return 0;
            var result = 0;
            var fixPrice = $scope.productWizard.method.getPurchasingFixPrice();
            angular.forEach($scope.productWizard.data.pricingOption, function (item) {
                result += $scope.productWizard.method.getPurchasingConfigPrice(item.productElementID);
            });
            result += fixPrice;
            return result;
        }

    };

    //
    //lisener
    //
    $rootScope.$on('openWinzard', function (event, args) {
        //get data from offer
        $scope.productWizard.data = args.data;
        $scope.productWizard.optionsData = args.optionsData;
        if ($scope.productWizard.optionsData === null) {
            $scope.productWizard.method.getOptionsData(
                function (data) {
                    $scope.productWizard.optionsData = data.data;

                    //init data                    
                    $scope.productWizard.event.init();
                    $scope.productWizard.method.showWinzard();

                    //apply
                    $scope.$apply();
                },
                function () {
                });
        }
        else {
            $scope.productWizard.event.init();
            $scope.productWizard.method.showWinzard();
        }         
    });

    //$rootScope.$on('openWinzard', function (event, args) {
    //    //get data from offer
    //    $scope.productWizard.data = args.data;
    //    $scope.productWizard.optionsData = args.optionsData;
    //    alert(12345);
    //});

    $rootScope.$on('closeWinzard', function () {
        $scope.productWizard.method.hideWinzard();     
    });    
}]);
