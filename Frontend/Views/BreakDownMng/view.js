(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', breakDownController);
    breakDownController.$inject = ['$scope', '$timeout', 'dataService'];

    function breakDownController($scope, $timeout, breakDownService) {
        breakDownService.serviceUrl = context.serviceUrl;
        breakDownService.token = context.token;

        //
        // scope data
        //
        $scope.data = null;
        $scope.companyID = context.companyID;
        $scope.supportFactory = [];
        $scope.factoryID = null;

        $scope.categories = null;
        $scope.exRate = 1;

        $scope.hasOptionDetail = false;
        $scope.hasColspanDetail = false;

        $scope.selectedOptions = {
            materialCostOptionID: null,
            frameCostOptionID: null,
            subMaterialCostOptionID: null,
            cushionCostOptionID: null,
            packingCostOptionID: null,
            hardwareCostOptionID: null,
            otherMaterialCostOptionID: null,
            fscCostOptionID: null,
            specialRequirementCostOptionID: null,
            managementCostOptionID: null
        };

        $scope.detailOptions = {
            isMaterial: false,
            materialOptionDetail: [],

            isFrameMaterial: false,
            frameOptionDetail: [],

            isSubMaterial: false,
            subMaterialOptionDetail: [],

            isCushion: false,
            cushionOptionDetail: [],

            isPacking: false,
            packingOptionDetail: [],

            isHardware: false,
            hardwareOptionDetail: [],

            isOtherMaterial: false,
            otherMaterialOptionDetail: [],

            isFSC: false,
            fscOptionDetail: [],

            isSpecialRequirement: false,
            specialRequirementOptionDetail: [],

            isManagement: false,
            managementOptionDetail: []
        };

        $scope.groupOptions = {
            isMaterial: false,
            materialOptionGroup: [],

            isFrameMaterial: false,
            frameOptionGroup: [],

            isSubMaterial: false,
            subMaterialOptionGroup: [],

            isCushion: false,
            cushionOptionGroup: [],

            isPacking: false,
            packingOptionGroup: [],

            isHardware: false,
            hardwareOptionGroup: [],

            isOtherMaterial: false,
            otherMaterialOptionGroup: [],

            isFSC: false,
            fscOptionGroup: [],

            isSpecialRequirement: false,
            specialRequirementOptionGroup: [],

            isManagement: false,
            managementOptionGroup: []
        };

        $scope.event = {
            init: function () {
                breakDownService.load(
                    context.id,
                    context.modelID,
                    context.sampleProductID,
                    context.offerSeasonDetailID,
                    context.factoryID,
                    function (data) {
                        if (data.message.type === 0) {
                            if (context.id === 0) {
                                window.location = context.redirectUrl + '/' + data.data.data.breakdownID + '?modelId=' + context.modelID + '&sampleProductId=' + context.sampleProductID + '&offerSeasonDetailId=' + context.offerSeasonDetailID + '&factoryId=' + context.factoryID;
                            }
                            $scope.data = data.data.data;
                            $scope.categories = data.data.breakdownCategoryDTOs;
                            $scope.exRate = data.data.exchangeRate;
                            $scope.supportFactory = data.data.factoryDTOs;

                            $scope.method.getOptionDefault($scope.companyID);

                            for (var i = 0; i < $scope.data.breakdownCategoryOptionDTOs.length; i++) {
                                var item = $scope.data.breakdownCategoryOptionDTOs[i];
                                if (item != null) {
                                    switch (item.breakdownCategoryID) {
                                        case 1:
                                            $scope.method.getOptionDetail(item, $scope.selectedOptions.materialCostOptionID);
                                            $scope.method.getOptionGroup(item, $scope.selectedOptions.materialCostOptionID);
                                            break;
                                        case 2:
                                            $scope.method.getOptionDetail(item, $scope.selectedOptions.frameCostOptionID);
                                            $scope.method.getOptionGroup(item, $scope.selectedOptions.frameCostOptionID);
                                            break;
                                        case 3:
                                            $scope.method.getOptionDetail(item, $scope.selectedOptions.subMaterialCostOptionID);
                                            $scope.method.getOptionGroup(item, $scope.selectedOptions.subMaterialCostOptionID);
                                            break;
                                        case 4:
                                            $scope.method.getOptionDetail(item, $scope.selectedOptions.cushionCostOptionID);
                                            $scope.method.getOptionGroup(item, $scope.selectedOptions.cushionCostOptionID);
                                            break;
                                        case 5:
                                            $scope.method.getOptionDetail(item, $scope.selectedOptions.packingCostOptionID);
                                            $scope.method.getOptionGroup(item, $scope.selectedOptions.packingCostOptionID);
                                            break;
                                        case 6:
                                            $scope.method.getOptionDetail(item, $scope.selectedOptions.hardwareCostOptionID);
                                            $scope.method.getOptionGroup(item, $scope.selectedOptions.hardwareCostOptionID);
                                            break;
                                        case 7:
                                            $scope.method.getOptionDetail(item, $scope.selectedOptions.otherMaterialCostOptionID);
                                            $scope.method.getOptionGroup(item, $scope.selectedOptions.otherMaterialCostOptionID);
                                            break;
                                        case 8:
                                            $scope.method.getOptionDetail(item, $scope.selectedOptions.fscCostOptionID);
                                            $scope.method.getOptionGroup(item, $scope.selectedOptions.fscCostOptionID);
                                            break;
                                        case 9:
                                            $scope.method.getOptionDetail(item, $scope.selectedOptions.specialRequirementCostOptionID);
                                            $scope.method.getOptionGroup(item, $scope.selectedOptions.specialRequirementCostOptionID);
                                            break;
                                        case 10:
                                            $scope.method.getOptionDetail(item, $scope.selectedOptions.managementCostOptionID);
                                            $scope.method.getOptionGroup(item, $scope.selectedOptions.managementCostOptionID);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                            $scope.hasOptionDetail = $scope.method.hasOptionDetail();

                            $timeout(function () {
                                $scope.factoryID = data.data.factoryID;
                            }, 0);

                            jQuery('#content').show();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            },
            change: function (getOptionID, categoryID) {
                $scope.method.resetOptionDetail(categoryID);
                $scope.method.resetOptionGroup(categoryID);

                for (var i = 0; i < $scope.data.breakdownCategoryOptionDTOs.length; i++) {
                    var item = $scope.data.breakdownCategoryOptionDTOs[i];
                    if (item != null) {
                        switch (item.breakdownCategoryID) {
                            case 1:
                                $scope.method.getOptionDetail(item, $scope.selectedOptions.materialCostOptionID);
                                $scope.method.getOptionGroup(item, $scope.selectedOptions.materialCostOptionID);
                                break;
                            case 2:
                                $scope.method.getOptionDetail(item, $scope.selectedOptions.frameCostOptionID);
                                $scope.method.getOptionGroup(item, $scope.selectedOptions.frameCostOptionID);
                                break;
                            case 3:
                                $scope.method.getOptionDetail(item, $scope.selectedOptions.subMaterialCostOptionID);
                                $scope.method.getOptionGroup(item, $scope.selectedOptions.subMaterialCostOptionID);
                                break;
                            case 4:
                                $scope.method.getOptionDetail(item, $scope.selectedOptions.cushionCostOptionID);
                                $scope.method.getOptionGroup(item, $scope.selectedOptions.cushionCostOptionID);
                                break;
                            case 5:
                                $scope.method.getOptionDetail(item, $scope.selectedOptions.packingCostOptionID);
                                $scope.method.getOptionGroup(item, $scope.selectedOptions.packingCostOptionID);
                                break;
                            case 6:
                                $scope.method.getOptionDetail(item, $scope.selectedOptions.hardwareCostOptionID);
                                $scope.method.getOptionGroup(item, $scope.selectedOptions.hardwareCostOptionID);
                                break;
                            case 7:
                                $scope.method.getOptionDetail(item, $scope.selectedOptions.otherMaterialCostOptionID);
                                $scope.method.getOptionGroup(item, $scope.selectedOptions.otherMaterialCostOptionID);
                                break;
                            case 8:
                                $scope.method.getOptionDetail(item, $scope.selectedOptions.fscCostOptionID);
                                $scope.method.getOptionGroup(item, $scope.selectedOptions.fscCostOptionID);
                                break;
                            case 9:
                                $scope.method.getOptionDetail(item, $scope.selectedOptions.specialRequirementCostOptionID);
                                $scope.method.getOptionGroup(item, $scope.selectedOptions.specialRequirementCostOptionID);
                                break;
                            case 10:
                                $scope.method.getOptionDetail(item, $scope.selectedOptions.managementCostOptionID);
                                $scope.method.getOptionGroup(item, $scope.selectedOptions.managementCostOptionID);
                                break;
                            default:
                                break;
                        }
                    }
                }

                $scope.hasOptionDetail = $scope.method.hasOptionDetail();
            },
            back: function () {
                window.location = context.backUrl;
            },
            openBreakdownDetail: function (categoryID) {
                switch (categoryID) {
                    case 1:
                        $scope.detailOptions.isMaterial = ($scope.detailOptions.materialOptionDetail.length > 0 && !$scope.detailOptions.isMaterial) ? true : false;
                        $scope.groupOptions.isMaterial = ($scope.groupOptions.materialOptionGroup.length > 0 && !$scope.groupOptions.isMaterial) ? true : false;
                        break;

                    case 2:
                        $scope.detailOptions.isFrameMaterial = ($scope.detailOptions.frameOptionDetail.length > 0 && !$scope.detailOptions.isFrameMaterial) ? true : false;
                        $scope.groupOptions.isFrameMaterial = ($scope.groupOptions.frameOptionGroup.length > 0 && !$scope.groupOptions.isFrameMaterial) ? true : false;
                        break;

                    case 3:
                        $scope.detailOptions.isSubMaterial = ($scope.detailOptions.subMaterialOptionDetail.length > 0 && !$scope.detailOptions.isSubMaterial) ? true : false;
                        $scope.groupOptions.isSubMaterial = ($scope.groupOptions.subMaterialOptionGroup.length > 0 && !$scope.groupOptions.isSubMaterial) ? true : false;
                        break;

                    case 4:
                        $scope.detailOptions.isCushion = ($scope.detailOptions.cushionOptionDetail.length > 0 && !$scope.detailOptions.isCushion) ? true : false;
                        $scope.groupOptions.isCushion = ($scope.groupOptions.cushionOptionGroup.length > 0 && !$scope.groupOptions.isCushion) ? true : false;
                        break;

                    case 5:
                        $scope.detailOptions.isPacking = ($scope.detailOptions.packingOptionDetail.length > 0 && !$scope.detailOptions.isPacking) ? true : false;
                        $scope.groupOptions.isPacking = ($scope.groupOptions.packingOptionGroup.length > 0 && !$scope.groupOptions.isPacking) ? true : false;
                        break;

                    case 6:
                        $scope.detailOptions.isHardware = ($scope.detailOptions.hardwareOptionDetail.length > 0 && !$scope.detailOptions.isHardware) ? true : false;
                        $scope.groupOptions.isHardware = ($scope.groupOptions.hardwareOptionGroup.length > 0 && !$scope.groupOptions.isHardware) ? true : false;
                        break;

                    case 7:
                        $scope.detailOptions.isOtherMaterial = ($scope.detailOptions.otherMaterialOptionDetail.length > 0 && !$scope.detailOptions.isOtherMaterial) ? true : false;
                        $scope.groupOptions.isOtherMaterial = ($scope.groupOptions.otherMaterialOptionGroup.length > 0 && !$scope.groupOptions.isOtherMaterial) ? true : false;
                        break;

                    case 8:
                        $scope.detailOptions.isFSC = ($scope.detailOptions.fscOptionDetail.length > 0 && !$scope.detailOptions.isFSC) ? true : false;
                        $scope.groupOptions.isFSC = ($scope.groupOptions.fscOptionGroup.length > 0 && !$scope.groupOptions.isFSC) ? true : false;
                        break;

                    case 9:
                        $scope.detailOptions.isSpecialRequirement = ($scope.detailOptions.specialRequirementOptionDetail.length > 0 && !$scope.detailOptions.isSpecialRequirement) ? true : false;
                        $scope.groupOptions.isSpecialRequirement = ($scope.groupOptions.specialRequirementOptionGroup.length > 0 && !$scope.groupOptions.isSpecialRequirement) ? true : false;
                        break;

                    case 10:
                        $scope.detailOptions.isManagement = ($scope.detailOptions.managementOptionDetail.length > 0 && !$scope.detailOptions.isManagement) ? true : false;
                        $scope.groupOptions.isManagement = ($scope.groupOptions.managementOptionGroup.length > 0 && !$scope.groupOptions.isManagement) ? true : false;
                        break;

                    default:
                        break;
                }
            },
            exportExcel: function () {
                breakDownService.exportExcel(
                    {
                        filters: {
                            materialCostOptionID: $scope.selectedOptions.materialCostOptionID,
                            frameCostOptionID: $scope.selectedOptions.frameCostOptionID,
                            subMaterialCostOptionID: $scope.selectedOptions.subMaterialCostOptionID,
                            cushionCostOptionID: $scope.selectedOptions.cushionCostOptionID,
                            packingCostOptionID: $scope.selectedOptions.packingCostOptionID,
                            hardwareCostOptionID: $scope.selectedOptions.hardwareCostOptionID,
                            otherMaterialCostOptionID: $scope.selectedOptions.otherMaterialCostOptionID,
                            fscCostOptionID: $scope.selectedOptions.fscCostOptionID,
                            specialRequirementCostOptionID: $scope.selectedOptions.specialRequirementCostOptionID,
                            managementCostOptionID: $scope.selectedOptions.managementCostOptionID,
                            shippedFromFactoryID: $scope.factoryID,
                            breakdownID: $scope.data.breakdownID,
                            companyID: context.companyID
                        }
                    },
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type != 2) {
                            window.location = context.backendReportUrl + data.data;
                        }
                    },
                    function (erorr) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        };

        $scope.method = {
            getOptionDefault: function (companyID) {
                for (var i = 0; i < $scope.data.breakdownCategoryOptionDTOs.length; i++) {
                    var item = $scope.data.breakdownCategoryOptionDTOs[i];
                    if (item != null) {
                        switch (item.breakdownCategoryID) {
                            case 1:
                                if (item.companyID == companyID) {
                                    if (item.isSelected == true) {
                                        $scope.selectedOptions.materialCostOptionID = item.breakdownCategoryOptionID;
                                    }
                                }
                                break;
                            case 2:
                                if (item.companyID == companyID) {
                                    if (item.isSelected == true) {
                                        $scope.selectedOptions.frameCostOptionID = item.breakdownCategoryOptionID;
                                    }
                                }
                                break;
                            case 3:
                                if (item.companyID == companyID) {
                                    if (item.isSelected == true) {
                                        $scope.selectedOptions.subMaterialCostOptionID = item.breakdownCategoryOptionID;
                                    }
                                }
                                break;
                            case 4:
                                if (item.companyID == companyID) {
                                    if (item.isSelected == true) {
                                        $scope.selectedOptions.cushionCostOptionID = item.breakdownCategoryOptionID;
                                    }
                                }
                                break;
                            case 5:
                                if (item.companyID == companyID) {
                                    if (item.isSelected == true) {
                                        $scope.selectedOptions.packingCostOptionID = item.breakdownCategoryOptionID;
                                    }
                                }
                                break;
                            case 6:
                                if (item.companyID == companyID) {
                                    if (item.isSelected == true) {
                                        $scope.selectedOptions.hardwareCostOptionID = item.breakdownCategoryOptionID;
                                    }
                                }
                                break;
                            case 7:
                                if (item.companyID == companyID) {
                                    if (item.isSelected == true) {
                                        $scope.selectedOptions.otherMaterialCostOptionID = item.breakdownCategoryOptionID;
                                    }
                                }
                                break;
                            case 8:
                                if (item.companyID == companyID) {
                                    if (item.isSelected == true) {
                                        $scope.selectedOptions.fscCostOptionID = item.breakdownCategoryOptionID;
                                    }
                                }
                                break;
                            case 9:
                                if (item.companyID == companyID) {
                                    if (item.isSelected == true) {
                                        $scope.selectedOptions.specialRequirementCostOptionID = item.breakdownCategoryOptionID;
                                    }
                                }
                                break;
                            case 10:
                                if (item.companyID == companyID) {
                                    if (item.isSelected == true) {
                                        $scope.selectedOptions.managementCostOptionID = item.breakdownCategoryOptionID;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            },
            getOptionName: function (option) {
                var result = null;

                if (option) {
                    switch (option.breakdownCategoryID) {
                        case 1:
                            return result = option.materialNM + ' ' + option.materialTypeNM + ' ' + option.materialColorNM;
                        case 2:
                            return result = option.frameMaterialNM + ' ' + option.frameMaterialColorNM;
                        case 3:
                            return result = option.subMaterialNM + ' ' + option.subMaterialColorNM;
                        case 4:
                            return option.cushionTypeNM + ' ' + option.cushionColorNM + ' (BACK CUSHION: ' + option.backCushionNM + ' + SEAT CUSHION: ' + option.seatCushionNM + ')';
                        case 5:
                            return result = option.packagingMethodNM;
                        case 8:
                            return result = option.fscTypeNM;
                        case 9:
                            return result = option.description;
                        default:
                            return null;
                    }
                }

                return result;
            },
            getOptionInfo: function (defaultOptionID) {
                for (var i = 0; i < $scope.data.breakdownCategoryOptionDTOs.length; i++) {
                    var item = $scope.data.breakdownCategoryOptionDTOs[i];
                    if (item != null && item.breakdownCategoryOptionID == defaultOptionID) {
                        return item;
                    }
                }
                return null;
            },
            getOptionDetail: function (item, defaultOptionID) {
                var defaultOption = $scope.method.getOptionInfo(defaultOptionID);
                if (defaultOption != null) {
                    switch (item.breakdownCategoryID) {
                        case 1:
                            if (defaultOption.materialID == item.materialID && defaultOption.materialTypeID == item.materialTypeID && defaultOption.materialColorID == item.materialColorID) {
                                for (var i = 0; i < item.breakdownCategoryOptionDetailDTOs.length; i++) {
                                    var detailItem = item.breakdownCategoryOptionDetailDTOs[i];
                                    if (detailItem != null) {
                                        var newDetailItem = {
                                            breakdownCategoryOptionGroupID: detailItem.breakdownCategoryOptionGroupID,
                                            breakdownCategoryOptionID: defaultOptionID,
                                            productionItemID: detailItem.productionItemID,
                                            productionItemUD: detailItem.productionItemUD,
                                            productionItemNM: detailItem.productionItemNM,
                                            unitID: detailItem.unitID,
                                            unitNM: detailItem.unitNM,
                                            fileLocation: detailItem.fileLocation,
                                            thumbnailLocation: detailItem.thumbnailLocation,

                                            avfWastedRatePercent: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                            avfQuantity: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                            avfPrice: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.avfPrice : null,
                                            avfAmount: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                            avfPercent: (item.companyID == 1 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,

                                            avtWastedRatePercent: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                            avtQuantity: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                            avtPrice: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.avtPrice : null,
                                            avtAmount: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                            avtPercent: (item.companyID == 3 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,
                                        };

                                        var index = $scope.method.getIndexDetail(newDetailItem, $scope.detailOptions.materialOptionDetail);
                                        if (index == -1) {
                                            $scope.detailOptions.materialOptionDetail.push(newDetailItem);
                                        } else {
                                            if (item.companyID == 1) {
                                                $scope.detailOptions.materialOptionDetail[index].avfWastedRatePercent = detailItem.wastedRatePercent;
                                                $scope.detailOptions.materialOptionDetail[index].avfQuantity = detailItem.quantity;
                                                $scope.detailOptions.materialOptionDetail[index].avfPrice = detailItem.avfPrice;
                                                $scope.detailOptions.materialOptionDetail[index].avfAmount = detailItem.totalAmount;
                                                $scope.detailOptions.materialOptionDetail[index].avfPercent = detailItem.quantityPercent;
                                            } else {
                                                $scope.detailOptions.materialOptionDetail[index].avtWastedRatePercent = detailItem.wastedRatePercent;
                                                $scope.detailOptions.materialOptionDetail[index].avtQuantity = detailItem.quantity;
                                                $scope.detailOptions.materialOptionDetail[index].avtPrice = detailItem.avtPrice;
                                                $scope.detailOptions.materialOptionDetail[index].avtAmount = detailItem.totalAmount;
                                                $scope.detailOptions.materialOptionDetail[index].avtPercent = detailItem.quantityPercent;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case 2:
                            if (defaultOption.frameMaterialID == item.frameMaterialID && defaultOption.frameMaterialColorID == item.frameMaterialColorID) {
                                for (var i = 0; i < item.breakdownCategoryOptionDetailDTOs.length; i++) {
                                    var detailItem = item.breakdownCategoryOptionDetailDTOs[i];
                                    if (detailItem != null) {
                                        var newDetailItem = {
                                            breakdownCategoryOptionGroupID: detailItem.breakdownCategoryOptionGroupID,
                                            breakdownCategoryOptionID: defaultOptionID,
                                            productionItemID: detailItem.productionItemID,
                                            productionItemUD: detailItem.productionItemUD,
                                            productionItemNM: detailItem.productionItemNM,
                                            unitID: detailItem.unitID,
                                            unitNM: detailItem.unitNM,
                                            fileLocation: detailItem.fileLocation,
                                            thumbnailLocation: detailItem.thumbnailLocation,

                                            avfWastedRatePercent: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                            avfQuantity: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                            avfPrice: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.avfPrice : null,
                                            avfAmount: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                            avfPercent: (item.companyID == 1 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,

                                            avtWastedRatePercent: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                            avtQuantity: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                            avtPrice: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.avtPrice : null,
                                            avtAmount: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                            avtPercent: (item.companyID == 3 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,
                                        };

                                        var index = $scope.method.getIndexDetail(newDetailItem, $scope.detailOptions.frameOptionDetail);
                                        if (index == -1) {
                                            $scope.detailOptions.frameOptionDetail.push(newDetailItem);
                                        } else {
                                            if (item.companyID == 1) {
                                                $scope.detailOptions.frameOptionDetail[index].avfWastedRatePercent = detailItem.wastedRatePercent;
                                                $scope.detailOptions.frameOptionDetail[index].avfQuantity = detailItem.quantity;
                                                $scope.detailOptions.frameOptionDetail[index].avfPrice = detailItem.avfPrice;
                                                $scope.detailOptions.frameOptionDetail[index].avfAmount = detailItem.totalAmount;
                                                $scope.detailOptions.frameOptionDetail[index].avfPercent = detailItem.quantityPercent;
                                            } else {
                                                $scope.detailOptions.frameOptionDetail[index].avtWastedRatePercent = detailItem.wastedRatePercent;
                                                $scope.detailOptions.frameOptionDetail[index].avtQuantity = detailItem.quantity;
                                                $scope.detailOptions.frameOptionDetail[index].avtPrice = detailItem.avtPrice;
                                                $scope.detailOptions.frameOptionDetail[index].avtAmount = detailItem.totalAmount;
                                                $scope.detailOptions.frameOptionDetail[index].avtPercent = detailItem.quantityPercent;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case 3:
                            if (defaultOption.subMaterialID == item.subMaterialID && defaultOption.subMaterialID == item.subMaterialID) {
                                for (var i = 0; i < item.breakdownCategoryOptionDetailDTOs.length; i++) {
                                    var detailItem = item.breakdownCategoryOptionDetailDTOs[i];
                                    if (detailItem != null) {
                                        var newDetailItem = {
                                            breakdownCategoryOptionGroupID: detailItem.breakdownCategoryOptionGroupID,
                                            breakdownCategoryOptionID: defaultOptionID,
                                            productionItemID: detailItem.productionItemID,
                                            productionItemUD: detailItem.productionItemUD,
                                            productionItemNM: detailItem.productionItemNM,
                                            unitID: detailItem.unitID,
                                            unitNM: detailItem.unitNM,
                                            fileLocation: detailItem.fileLocation,
                                            thumbnailLocation: detailItem.thumbnailLocation,

                                            avfWastedRatePercent: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                            avfQuantity: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                            avfPrice: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.avfPrice : null,
                                            avfAmount: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                            avfPercent: (item.companyID == 1 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,

                                            avtWastedRatePercent: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                            avtQuantity: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                            avtPrice: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.avtPrice : null,
                                            avtAmount: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                            avtPercent: (item.companyID == 3 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,
                                        };

                                        var index = $scope.method.getIndexDetail(newDetailItem, $scope.detailOptions.subMaterialOptionDetail);
                                        if (index == -1) {
                                            $scope.detailOptions.subMaterialOptionDetail.push(newDetailItem);
                                        } else {
                                            if (item.companyID == 1) {
                                                $scope.detailOptions.subMaterialOptionDetail[index].avfWastedRatePercent = detailItem.wastedRatePercent;
                                                $scope.detailOptions.subMaterialOptionDetail[index].avfQuantity = detailItem.quantity;
                                                $scope.detailOptions.subMaterialOptionDetail[index].avfPrice = detailItem.avfPrice;
                                                $scope.detailOptions.subMaterialOptionDetail[index].avfAmount = detailItem.totalAmount;
                                                $scope.detailOptions.subMaterialOptionDetail[index].avfPercent = detailItem.quantityPercent;
                                            } else {
                                                $scope.detailOptions.subMaterialOptionDetail[index].avtWastedRatePercent = detailItem.wastedRatePercent;
                                                $scope.detailOptions.subMaterialOptionDetail[index].avtQuantity = detailItem.quantity;
                                                $scope.detailOptions.subMaterialOptionDetail[index].avtPrice = detailItem.avtPrice;
                                                $scope.detailOptions.subMaterialOptionDetail[index].avtAmount = detailItem.totalAmount;
                                                $scope.detailOptions.subMaterialOptionDetail[index].avtPercent = detailItem.quantityPercent;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case 4:
                            if (defaultOption.cushionTypeID == item.cushionTypeID && defaultOption.cushionColorID == item.cushionColorID && defaultOption.backCushionID == item.backCushionID && defaultOption.seatCushionID == item.seatCushionID) {
                                for (var i = 0; i < item.breakdownCategoryOptionDetailDTOs.length; i++) {
                                    var detailItem = item.breakdownCategoryOptionDetailDTOs[i];
                                    if (detailItem != null) {
                                        var newDetailItem = {
                                            breakdownCategoryOptionGroupID: detailItem.breakdownCategoryOptionGroupID,
                                            breakdownCategoryOptionID: defaultOptionID,
                                            productionItemID: detailItem.productionItemID,
                                            productionItemUD: detailItem.productionItemUD,
                                            productionItemNM: detailItem.productionItemNM,
                                            unitID: detailItem.unitID,
                                            unitNM: detailItem.unitNM,
                                            fileLocation: detailItem.fileLocation,
                                            thumbnailLocation: detailItem.thumbnailLocation,

                                            avfWastedRatePercent: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                            avfQuantity: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                            avfPrice: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.avfPrice : null,
                                            avfAmount: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                            avfPercent: (item.companyID == 1 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,

                                            avtWastedRatePercent: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                            avtQuantity: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                            avtPrice: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.avtPrice : null,
                                            avtAmount: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                            avtPercent: (item.companyID == 3 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,
                                        };

                                        var index = $scope.method.getIndexDetail(newDetailItem, $scope.detailOptions.cushionOptionDetail);
                                        if (index == -1) {
                                            $scope.detailOptions.cushionOptionDetail.push(newDetailItem);
                                        } else {
                                            if (item.companyID == 1) {
                                                $scope.detailOptions.cushionOptionDetail[index].avfWastedRatePercent = detailItem.wastedRatePercent;
                                                $scope.detailOptions.cushionOptionDetail[index].avfQuantity = detailItem.quantity;
                                                $scope.detailOptions.cushionOptionDetail[index].avfPrice = detailItem.avfPrice;
                                                $scope.detailOptions.cushionOptionDetail[index].avfAmount = detailItem.totalAmount;
                                                $scope.detailOptions.cushionOptionDetail[index].avfPercent = detailItem.quantityPercent;
                                            } else {
                                                $scope.detailOptions.cushionOptionDetail[index].avtWastedRatePercent = detailItem.wastedRatePercent;
                                                $scope.detailOptions.cushionOptionDetail[index].avtQuantity = detailItem.quantity;
                                                $scope.detailOptions.cushionOptionDetail[index].avtPrice = detailItem.avtPrice;
                                                $scope.detailOptions.cushionOptionDetail[index].avtAmount = detailItem.totalAmount;
                                                $scope.detailOptions.cushionOptionDetail[index].avtPercent = detailItem.quantityPercent;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case 5:
                            if (defaultOption.packagingMethodID == item.packagingMethodID) {
                                for (var i = 0; i < item.breakdownCategoryOptionDetailDTOs.length; i++) {
                                    var detailItem = item.breakdownCategoryOptionDetailDTOs[i];
                                    if (detailItem != null) {
                                        var newDetailItem = {
                                            breakdownCategoryOptionGroupID: detailItem.breakdownCategoryOptionGroupID,
                                            breakdownCategoryOptionID: defaultOptionID,
                                            productionItemID: detailItem.productionItemID,
                                            productionItemUD: detailItem.productionItemUD,
                                            productionItemNM: detailItem.productionItemNM,
                                            unitID: detailItem.unitID,
                                            unitNM: detailItem.unitNM,
                                            fileLocation: detailItem.fileLocation,
                                            thumbnailLocation: detailItem.thumbnailLocation,

                                            avfWastedRatePercent: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                            avfQuantity: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                            avfPrice: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.avfPrice : null,
                                            avfAmount: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                            avfPercent: (item.companyID == 1 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,

                                            avtWastedRatePercent: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                            avtQuantity: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                            avtPrice: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.avtPrice : null,
                                            avtAmount: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                            avtPercent: (item.companyID == 3 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,
                                        };

                                        var index = $scope.method.getIndexDetail(newDetailItem, $scope.detailOptions.packingOptionDetail);
                                        if (index == -1) {
                                            $scope.detailOptions.packingOptionDetail.push(newDetailItem);
                                        } else {
                                            if (item.companyID == 1) {
                                                $scope.detailOptions.packingOptionDetail[index].avfWastedRatePercent = detailItem.wastedRatePercent;
                                                $scope.detailOptions.packingOptionDetail[index].avfQuantity = detailItem.quantity;
                                                $scope.detailOptions.packingOptionDetail[index].avfPrice = detailItem.avfPrice;
                                                $scope.detailOptions.packingOptionDetail[index].avfAmount = detailItem.totalAmount;
                                                $scope.detailOptions.packingOptionDetail[index].avfPercent = detailItem.quantityPercent;
                                            } else {
                                                $scope.detailOptions.packingOptionDetail[index].avtWastedRatePercent = detailItem.wastedRatePercent;
                                                $scope.detailOptions.packingOptionDetail[index].avtQuantity = detailItem.quantity;
                                                $scope.detailOptions.packingOptionDetail[index].avtPrice = detailItem.avtPrice;
                                                $scope.detailOptions.packingOptionDetail[index].avtAmount = detailItem.totalAmount;
                                                $scope.detailOptions.packingOptionDetail[index].avtPercent = detailItem.quantityPercent;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case 6:
                            for (var i = 0; i < item.breakdownCategoryOptionDetailDTOs.length; i++) {
                                var detailItem = item.breakdownCategoryOptionDetailDTOs[i];
                                if (detailItem != null) {
                                    var newDetailItem = {
                                        breakdownCategoryOptionGroupID: detailItem.breakdownCategoryOptionGroupID,
                                        breakdownCategoryOptionID: defaultOptionID,
                                        productionItemID: detailItem.productionItemID,
                                        productionItemUD: detailItem.productionItemUD,
                                        productionItemNM: detailItem.productionItemNM,
                                        unitID: detailItem.unitID,
                                        unitNM: detailItem.unitNM,
                                        fileLocation: detailItem.fileLocation,
                                        thumbnailLocation: detailItem.thumbnailLocation,

                                        avfWastedRatePercent: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                        avfQuantity: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                        avfPrice: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.avfPrice : null,
                                        avfAmount: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                        avfPercent: (item.companyID == 1 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,

                                        avtWastedRatePercent: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                        avtQuantity: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                        avtPrice: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.avtPrice : null,
                                        avtAmount: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                        avtPercent: (item.companyID == 3 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,
                                    };

                                    var index = $scope.method.getIndexDetail(newDetailItem, $scope.detailOptions.hardwareOptionDetail);
                                    if (index == -1) {
                                        $scope.detailOptions.hardwareOptionDetail.push(newDetailItem);
                                    } else {
                                        if (item.companyID == 1) {
                                            $scope.detailOptions.hardwareOptionDetail[index].avfWastedRatePercent = detailItem.wastedRatePercent;
                                            $scope.detailOptions.hardwareOptionDetail[index].avfQuantity = detailItem.quantity;
                                            $scope.detailOptions.hardwareOptionDetail[index].avfPrice = detailItem.avfPrice;
                                            $scope.detailOptions.hardwareOptionDetail[index].avfAmount = detailItem.totalAmount;
                                            $scope.detailOptions.hardwareOptionDetail[index].avfPercent = detailItem.quantityPercent;
                                        } else {
                                            $scope.detailOptions.hardwareOptionDetail[index].avtWastedRatePercent = detailItem.wastedRatePercent;
                                            $scope.detailOptions.hardwareOptionDetail[index].avtQuantity = detailItem.quantity;
                                            $scope.detailOptions.hardwareOptionDetail[index].avtPrice = detailItem.avtPrice;
                                            $scope.detailOptions.hardwareOptionDetail[index].avtAmount = detailItem.totalAmount;
                                            $scope.detailOptions.hardwareOptionDetail[index].avtPercent = detailItem.quantityPercent;
                                        }
                                    }
                                }
                            }
                            break;
                        case 7:
                            for (var i = 0; i < item.breakdownCategoryOptionDetailDTOs.length; i++) {
                                var detailItem = item.breakdownCategoryOptionDetailDTOs[i];
                                if (detailItem != null) {
                                    var newDetailItem = {
                                        breakdownCategoryOptionGroupID: detailItem.breakdownCategoryOptionGroupID,
                                        breakdownCategoryOptionID: defaultOptionID,
                                        productionItemID: detailItem.productionItemID,
                                        productionItemUD: detailItem.productionItemUD,
                                        productionItemNM: detailItem.productionItemNM,
                                        unitID: detailItem.unitID,
                                        unitNM: detailItem.unitNM,
                                        fileLocation: detailItem.fileLocation,
                                        thumbnailLocation: detailItem.thumbnailLocation,

                                        avfWastedRatePercent: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                        avfQuantity: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                        avfPrice: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.avfPrice : null,
                                        avfAmount: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                        avfPercent: (item.companyID == 1 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,

                                        avtWastedRatePercent: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                        avtQuantity: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                        avtPrice: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.avtPrice : null,
                                        avtAmount: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                        avtPercent: (item.companyID == 3 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,
                                    };

                                    var index = $scope.method.getIndexDetail(newDetailItem, $scope.detailOptions.otherMaterialOptionDetail);
                                    if (index == -1) {
                                        $scope.detailOptions.otherMaterialOptionDetail.push(newDetailItem);
                                    } else {
                                        if (item.companyID == 1) {
                                            $scope.detailOptions.otherMaterialOptionDetail[index].avfWastedRatePercent = detailItem.wastedRatePercent;
                                            $scope.detailOptions.otherMaterialOptionDetail[index].avfQuantity = detailItem.quantity;
                                            $scope.detailOptions.otherMaterialOptionDetail[index].avfPrice = detailItem.avfPrice;
                                            $scope.detailOptions.otherMaterialOptionDetail[index].avfAmount = detailItem.totalAmount;
                                            $scope.detailOptions.otherMaterialOptionDetail[index].avfPercent = detailItem.quantityPercent;
                                        } else {
                                            $scope.detailOptions.otherMaterialOptionDetail[index].avtWastedRatePercent = detailItem.wastedRatePercent;
                                            $scope.detailOptions.otherMaterialOptionDetail[index].avtQuantity = detailItem.quantity;
                                            $scope.detailOptions.otherMaterialOptionDetail[index].avtPrice = detailItem.avtPrice;
                                            $scope.detailOptions.otherMaterialOptionDetail[index].avtAmount = detailItem.totalAmount;
                                            $scope.detailOptions.otherMaterialOptionDetail[index].avtPercent = detailItem.quantityPercent;
                                        }
                                    }
                                }
                            }
                            break;
                        case 8:
                            if (defaultOption.fscTypeID == item.fscTypeID) {
                                for (var i = 0; i < item.breakdownCategoryOptionDetailDTOs.length; i++) {
                                    var detailItem = item.breakdownCategoryOptionDetailDTOs[i];
                                    if (detailItem != null) {
                                        var newDetailItem = {
                                            breakdownCategoryOptionGroupID: detailItem.breakdownCategoryOptionGroupID,
                                            breakdownCategoryOptionID: defaultOptionID,
                                            productionItemID: detailItem.productionItemID,
                                            productionItemUD: detailItem.productionItemUD,
                                            productionItemNM: detailItem.productionItemNM,
                                            unitID: detailItem.unitID,
                                            unitNM: detailItem.unitNM,
                                            fileLocation: detailItem.fileLocation,
                                            thumbnailLocation: detailItem.thumbnailLocation,

                                            avfWastedRatePercent: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                            avfQuantity: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                            avfPrice: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.avfPrice : null,
                                            avfAmount: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                            avfPercent: (item.companyID == 1 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,

                                            avtWastedRatePercent: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                            avtQuantity: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                            avtPrice: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.avtPrice : null,
                                            avtAmount: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                            avtPercent: (item.companyID == 3 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,
                                        };

                                        var index = $scope.method.getIndexDetail(newDetailItem, $scope.detailOptions.fscOptionDetail);
                                        if (index == -1) {
                                            $scope.detailOptions.fscOptionDetail.push(newDetailItem);
                                        } else {
                                            if (item.companyID == 1) {
                                                $scope.detailOptions.fscOptionDetail[index].avfWastedRatePercent = detailItem.wastedRatePercent;
                                                $scope.detailOptions.fscOptionDetail[index].avfQuantity = detailItem.quantity;
                                                $scope.detailOptions.fscOptionDetail[index].avfPrice = detailItem.avfPrice;
                                                $scope.detailOptions.fscOptionDetail[index].avfAmount = detailItem.totalAmount;
                                                $scope.detailOptions.fscOptionDetail[index].avfPercent = detailItem.quantityPercent;
                                            } else {
                                                $scope.detailOptions.fscOptionDetail[index].avtWastedRatePercent = detailItem.wastedRatePercent;
                                                $scope.detailOptions.fscOptionDetail[index].avtQuantity = detailItem.quantity;
                                                $scope.detailOptions.fscOptionDetail[index].avtPrice = detailItem.avtPrice;
                                                $scope.detailOptions.fscOptionDetail[index].avtAmount = detailItem.totalAmount;
                                                $scope.detailOptions.fscOptionDetail[index].avtPercent = detailItem.quantityPercent;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case 9:
                            if (defaultOption.description == item.description) {
                                for (var i = 0; i < item.breakdownCategoryOptionDetailDTOs.length; i++) {
                                    var detailItem = item.breakdownCategoryOptionDetailDTOs[i];
                                    if (detailItem != null) {
                                        var newDetailItem = {
                                            breakdownCategoryOptionGroupID: detailItem.breakdownCategoryOptionGroupID,
                                            breakdownCategoryOptionID: defaultOptionID,
                                            productionItemID: detailItem.productionItemID,
                                            productionItemUD: detailItem.productionItemUD,
                                            productionItemNM: detailItem.productionItemNM,
                                            unitID: detailItem.unitID,
                                            unitNM: detailItem.unitNM,
                                            fileLocation: detailItem.fileLocation,
                                            thumbnailLocation: detailItem.thumbnailLocation,

                                            avfWastedRatePercent: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                            avfQuantity: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                            avfPrice: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.avfPrice : null,
                                            avfAmount: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                            avfPercent: (item.companyID == 1 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,

                                            avtWastedRatePercent: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                            avtQuantity: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                            avtPrice: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.avtPrice : null,
                                            avtAmount: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                            avtPercent: (item.companyID == 3 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,
                                        };

                                        var index = $scope.method.getIndexDetail(newDetailItem, $scope.detailOptions.specialRequirementOptionDetail);
                                        if (index == -1) {
                                            $scope.detailOptions.specialRequirementOptionDetail.push(newDetailItem);
                                        } else {
                                            if (item.companyID == 1) {
                                                $scope.detailOptions.specialRequirementOptionDetail[index].avfWastedRatePercent = detailItem.wastedRatePercent;
                                                $scope.detailOptions.specialRequirementOptionDetail[index].avfQuantity = detailItem.quantity;
                                                $scope.detailOptions.specialRequirementOptionDetail[index].avfPrice = detailItem.avfPrice;
                                                $scope.detailOptions.specialRequirementOptionDetail[index].avfAmount = detailItem.totalAmount;
                                                $scope.detailOptions.specialRequirementOptionDetail[index].avfPercent = detailItem.quantityPercent;
                                            } else {
                                                $scope.detailOptions.specialRequirementOptionDetail[index].avtWastedRatePercent = detailItem.wastedRatePercent;
                                                $scope.detailOptions.specialRequirementOptionDetail[index].avtQuantity = detailItem.quantity;
                                                $scope.detailOptions.specialRequirementOptionDetail[index].avtPrice = detailItem.avtPrice;
                                                $scope.detailOptions.specialRequirementOptionDetail[index].avtAmount = detailItem.totalAmount;
                                                $scope.detailOptions.specialRequirementOptionDetail[index].avtPercent = detailItem.quantityPercent;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case 10:
                            for (var i = 0; i < item.breakdownCategoryOptionDetailDTOs.length; i++) {
                                var detailItem = item.breakdownCategoryOptionDetailDTOs[i];
                                if (detailItem != null) {
                                    var newDetailItem = {
                                        breakdownCategoryOptionGroupID: detailItem.breakdownCategoryOptionGroupID,
                                        breakdownCategoryOptionID: defaultOptionID,
                                        productionItemID: detailItem.productionItemID,
                                        productionItemUD: detailItem.productionItemUD,
                                        productionItemNM: detailItem.productionItemNM,
                                        unitID: detailItem.unitID,
                                        unitNM: detailItem.unitNM,
                                        fileLocation: detailItem.fileLocation,
                                        thumbnailLocation: detailItem.thumbnailLocation,

                                        avfWastedRatePercent: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                        avfQuantity: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                        avfPrice: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.avfPrice : null,
                                        avfAmount: (item.companyID == 1 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                        avfPercent: (item.companyID == 1 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,

                                        avtWastedRatePercent: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.wastedRatePercent : null,
                                        avtQuantity: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.quantity : null,
                                        avtPrice: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.avtPrice : null,
                                        avtAmount: (item.companyID == 3 && item.breakdownCategoryID != 10) ? detailItem.totalAmount : null,
                                        avtPercent: (item.companyID == 3 && item.breakdownCategoryID == 10) ? detailItem.quantityPercent : null,
                                    };

                                    var index = $scope.method.getIndexDetail(newDetailItem, $scope.detailOptions.managementOptionDetail);
                                    if (index == -1) {
                                        $scope.detailOptions.managementOptionDetail.push(newDetailItem);
                                    } else {
                                        if (item.companyID == 1) {
                                            $scope.detailOptions.managementOptionDetail[index].avfWastedRatePercent = detailItem.wastedRatePercent;
                                            $scope.detailOptions.managementOptionDetail[index].avfQuantity = detailItem.quantity;
                                            $scope.detailOptions.managementOptionDetail[index].avfPrice = detailItem.avfPrice;
                                            $scope.detailOptions.managementOptionDetail[index].avfAmount = detailItem.totalAmount;
                                            $scope.detailOptions.managementOptionDetail[index].avfPercent = detailItem.quantityPercent;
                                        } else {
                                            $scope.detailOptions.managementOptionDetail[index].avtWastedRatePercent = detailItem.wastedRatePercent;
                                            $scope.detailOptions.managementOptionDetail[index].avtQuantity = detailItem.quantity;
                                            $scope.detailOptions.managementOptionDetail[index].avtPrice = detailItem.avtPrice;
                                            $scope.detailOptions.managementOptionDetail[index].avtAmount = detailItem.totalAmount;
                                            $scope.detailOptions.managementOptionDetail[index].avtPercent = detailItem.quantityPercent;
                                        }
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            },
            getOptionGroup: function (item, defaultOptionID) {
                var defaultOption = $scope.method.getOptionInfo(defaultOptionID);
                if (defaultOption != null) {
                    switch (item.breakdownCategoryID) {
                        case 1:
                            if (defaultOption.materialID == item.materialID && defaultOption.materialTypeID == item.materialTypeID && defaultOption.materialColorID == item.materialColorID) {
                                for (var i = 0; i < item.breakdownCategoryOptionGroupDTOs.length; i++) {
                                    var groupItem = item.breakdownCategoryOptionGroupDTOs[i];
                                    var newGroupItem = {
                                        breakdownCategoryOptionGroupID: groupItem.breakdownCategoryOptionGroupID,
                                        breakdownCategoryOptionGroupNM: groupItem.breakdownCategoryOptionGroupNM,
                                        breakdownCategoryOptionGroupNMEN: groupItem.breakdownCategoryOptionGroupNMEN,
                                        breakdownCategoryOptionID: groupItem.breakdownCategoryOptionID,
                                        avfGroupQuantity: (item.companyID == 1) ? groupItem.quantity : null,
                                        avtGroupQuantity: (item.companyID == 3) ? groupItem.quantity : null,
                                    };

                                    var index = $scope.method.getIndexGroup(newGroupItem, $scope.groupOptions.materialOptionGroup);
                                    if (index == -1) {
                                        $scope.groupOptions.materialOptionGroup.push(newGroupItem);
                                    } else {
                                        if (item.companyID == 1) {
                                            $scope.groupOptions.materialOptionGroup[index].avfGroupQuantity = groupItem.quantity;
                                        } else {
                                            $scope.groupOptions.materialOptionGroup[index].avtGroupQuantity = groupItem.quantity;
                                        }
                                    }
                                }
                            }
                            break;
                        case 2:
                            if (defaultOption.frameMaterialID == item.frameMaterialID && defaultOption.frameMaterialColorID == item.frameMaterialColorID) {
                                for (var i = 0; i < item.breakdownCategoryOptionGroupDTOs.length; i++) {
                                    var groupItem = item.breakdownCategoryOptionGroupDTOs[i];
                                    var newGroupItem = {
                                        breakdownCategoryOptionGroupID: groupItem.breakdownCategoryOptionGroupID,
                                        breakdownCategoryOptionGroupNM: groupItem.breakdownCategoryOptionGroupNM,
                                        breakdownCategoryOptionGroupNMEN: groupItem.breakdownCategoryOptionGroupNMEN,
                                        breakdownCategoryOptionID: groupItem.breakdownCategoryOptionID,
                                        avfGroupQuantity: (item.companyID == 1) ? groupItem.quantity : null,
                                        avtGroupQuantity: (item.companyID == 3) ? groupItem.quantity : null,
                                    };

                                    var index = $scope.method.getIndexGroup(newGroupItem, $scope.groupOptions.frameOptionGroup);
                                    if (index == -1) {
                                        $scope.groupOptions.frameOptionGroup.push(newGroupItem);
                                    } else {
                                        if (item.companyID == 1) {
                                            $scope.groupOptions.frameOptionGroup[index].avfGroupQuantity = groupItem.quantity;
                                        } else {
                                            $scope.groupOptions.frameOptionGroup[index].avtGroupQuantity = groupItem.quantity;
                                        }
                                    }
                                }
                            }
                            break;
                        case 3:
                            if (defaultOption.subMaterialID == item.subMaterialID && defaultOption.subMaterialID == item.subMaterialID) {
                                for (var i = 0; i < item.breakdownCategoryOptionGroupDTOs.length; i++) {
                                    var groupItem = item.breakdownCategoryOptionGroupDTOs[i];
                                    var newGroupItem = {
                                        breakdownCategoryOptionGroupID: groupItem.breakdownCategoryOptionGroupID,
                                        breakdownCategoryOptionGroupNM: groupItem.breakdownCategoryOptionGroupNM,
                                        breakdownCategoryOptionGroupNMEN: groupItem.breakdownCategoryOptionGroupNMEN,
                                        breakdownCategoryOptionID: groupItem.breakdownCategoryOptionID,
                                        avfGroupQuantity: (item.companyID == 1) ? groupItem.quantity : null,
                                        avtGroupQuantity: (item.companyID == 3) ? groupItem.quantity : null,
                                    };

                                    var index = $scope.method.getIndexGroup(newGroupItem, $scope.groupOptions.subMaterialOptionGroup);
                                    if (index == -1) {
                                        $scope.groupOptions.subMaterialOptionGroup.push(newGroupItem);
                                    } else {
                                        if (item.companyID == 1) {
                                            $scope.groupOptions.subMaterialOptionGroup[index].avfGroupQuantity = groupItem.quantity;
                                        } else {
                                            $scope.groupOptions.subMaterialOptionGroup[index].avtGroupQuantity = groupItem.quantity;
                                        }
                                    }
                                }
                            }
                            break;
                        case 4:
                            if (defaultOption.cushionTypeID == item.cushionTypeID && defaultOption.cushionColorID == item.cushionColorID && defaultOption.backCushionID == item.backCushionID && defaultOption.seatCushionID == item.seatCushionID) {
                                for (var i = 0; i < item.breakdownCategoryOptionGroupDTOs.length; i++) {
                                    var groupItem = item.breakdownCategoryOptionGroupDTOs[i];
                                    var newGroupItem = {
                                        breakdownCategoryOptionGroupID: groupItem.breakdownCategoryOptionGroupID,
                                        breakdownCategoryOptionGroupNM: groupItem.breakdownCategoryOptionGroupNM,
                                        breakdownCategoryOptionGroupNMEN: groupItem.breakdownCategoryOptionGroupNMEN,
                                        breakdownCategoryOptionID: groupItem.breakdownCategoryOptionID,
                                        avfGroupQuantity: (item.companyID == 1) ? groupItem.quantity : null,
                                        avtGroupQuantity: (item.companyID == 3) ? groupItem.quantity : null,
                                    };

                                    var index = $scope.method.getIndexGroup(newGroupItem, $scope.groupOptions.cushionOptionGroup);
                                    if (index == -1) {
                                        $scope.groupOptions.cushionOptionGroup.push(newGroupItem);
                                    } else {
                                        if (item.companyID == 1) {
                                            $scope.groupOptions.cushionOptionGroup[index].avfGroupQuantity = groupItem.quantity;
                                        } else {
                                            $scope.groupOptions.cushionOptionGroup[index].avtGroupQuantity = groupItem.quantity;
                                        }
                                    }
                                }
                            }
                            break;
                        case 5:
                            if (defaultOption.packagingMethodID == item.packagingMethodID) {
                                for (var i = 0; i < item.breakdownCategoryOptionGroupDTOs.length; i++) {
                                    var groupItem = item.breakdownCategoryOptionGroupDTOs[i];
                                    var newGroupItem = {
                                        breakdownCategoryOptionGroupID: groupItem.breakdownCategoryOptionGroupID,
                                        breakdownCategoryOptionGroupNM: groupItem.breakdownCategoryOptionGroupNM,
                                        breakdownCategoryOptionGroupNMEN: groupItem.breakdownCategoryOptionGroupNMEN,
                                        breakdownCategoryOptionID: groupItem.breakdownCategoryOptionID,
                                        avfGroupQuantity: (item.companyID == 1) ? groupItem.quantity : null,
                                        avtGroupQuantity: (item.companyID == 3) ? groupItem.quantity : null,
                                    };

                                    var index = $scope.method.getIndexGroup(newGroupItem, $scope.groupOptions.packingOptionGroup);
                                    if (index == -1) {
                                        $scope.groupOptions.packingOptionGroup.push(newGroupItem);
                                    } else {
                                        if (item.companyID == 1) {
                                            $scope.groupOptions.packingOptionGroup[index].avfGroupQuantity = groupItem.quantity;
                                        } else {
                                            $scope.groupOptions.packingOptionGroup[index].avtGroupQuantity = groupItem.quantity;
                                        }
                                    }
                                }
                            }
                            break;
                        case 6:
                            for (var i = 0; i < item.breakdownCategoryOptionGroupDTOs.length; i++) {
                                var groupItem = item.breakdownCategoryOptionGroupDTOs[i];
                                var newGroupItem = {
                                    breakdownCategoryOptionGroupID: groupItem.breakdownCategoryOptionGroupID,
                                    breakdownCategoryOptionGroupNM: groupItem.breakdownCategoryOptionGroupNM,
                                    breakdownCategoryOptionGroupNMEN: groupItem.breakdownCategoryOptionGroupNMEN,
                                    breakdownCategoryOptionID: groupItem.breakdownCategoryOptionID,
                                    avfGroupQuantity: (item.companyID == 1) ? groupItem.quantity : null,
                                    avtGroupQuantity: (item.companyID == 3) ? groupItem.quantity : null,
                                };

                                var index = $scope.method.getIndexGroup(newGroupItem, $scope.groupOptions.hardwareOptionGroup);
                                if (index == -1) {
                                    $scope.groupOptions.hardwareOptionGroup.push(newGroupItem);
                                } else {
                                    if (item.companyID == 1) {
                                        $scope.groupOptions.hardwareOptionGroup[index].avfGroupQuantity = groupItem.quantity;
                                    } else {
                                        $scope.groupOptions.hardwareOptionGroup[index].avtGroupQuantity = groupItem.quantity;
                                    }
                                }
                            }
                            break;
                        case 7:
                            for (var i = 0; i < item.breakdownCategoryOptionGroupDTOs.length; i++) {
                                var groupItem = item.breakdownCategoryOptionGroupDTOs[i];
                                var newGroupItem = {
                                    breakdownCategoryOptionGroupID: groupItem.breakdownCategoryOptionGroupID,
                                    breakdownCategoryOptionGroupNM: groupItem.breakdownCategoryOptionGroupNM,
                                    breakdownCategoryOptionGroupNMEN: groupItem.breakdownCategoryOptionGroupNMEN,
                                    breakdownCategoryOptionID: groupItem.breakdownCategoryOptionID,
                                    avfGroupQuantity: (item.companyID == 1) ? groupItem.quantity : null,
                                    avtGroupQuantity: (item.companyID == 3) ? groupItem.quantity : null,
                                };

                                var index = $scope.method.getIndexGroup(newGroupItem, $scope.groupOptions.otherMaterialOptionGroup);
                                if (index == -1) {
                                    $scope.groupOptions.otherMaterialOptionGroup.push(newGroupItem);
                                } else {
                                    if (item.companyID == 1) {
                                        $scope.groupOptions.otherMaterialOptionGroup[index].avfGroupQuantity = groupItem.quantity;
                                    } else {
                                        $scope.groupOptions.otherMaterialOptionGroup[index].avtGroupQuantity = groupItem.quantity;
                                    }
                                }
                            }
                            break;
                        case 8:
                            if (defaultOption.fscTypeID == item.fscTypeID) {
                                for (var i = 0; i < item.breakdownCategoryOptionGroupDTOs.length; i++) {
                                    var groupItem = item.breakdownCategoryOptionGroupDTOs[i];
                                    var newGroupItem = {
                                        breakdownCategoryOptionGroupID: groupItem.breakdownCategoryOptionGroupID,
                                        breakdownCategoryOptionGroupNM: groupItem.breakdownCategoryOptionGroupNM,
                                        breakdownCategoryOptionGroupNMEN: groupItem.breakdownCategoryOptionGroupNMEN,
                                        breakdownCategoryOptionID: groupItem.breakdownCategoryOptionID,
                                        avfGroupQuantity: (item.companyID == 1) ? groupItem.quantity : null,
                                        avtGroupQuantity: (item.companyID == 3) ? groupItem.quantity : null,
                                    };

                                    var index = $scope.method.getIndexGroup(newGroupItem, $scope.groupOptions.fscOptionGroup);
                                    if (index == -1) {
                                        $scope.groupOptions.fscOptionGroup.push(newGroupItem);
                                    } else {
                                        if (item.companyID == 1) {
                                            $scope.groupOptions.fscOptionGroup[index].avfGroupQuantity = groupItem.quantity;
                                        } else {
                                            $scope.groupOptions.fscOptionGroup[index].avtGroupQuantity = groupItem.quantity;
                                        }
                                    }
                                }
                            }
                            break;
                        case 9:
                            if (defaultOption.description == item.description) {
                                for (var i = 0; i < item.breakdownCategoryOptionGroupDTOs.length; i++) {
                                    var groupItem = item.breakdownCategoryOptionGroupDTOs[i];
                                    var newGroupItem = {
                                        breakdownCategoryOptionGroupID: groupItem.breakdownCategoryOptionGroupID,
                                        breakdownCategoryOptionGroupNM: groupItem.breakdownCategoryOptionGroupNM,
                                        breakdownCategoryOptionGroupNMEN: groupItem.breakdownCategoryOptionGroupNMEN,
                                        breakdownCategoryOptionID: groupItem.breakdownCategoryOptionID,
                                        avfGroupQuantity: (item.companyID == 1) ? groupItem.quantity : null,
                                        avtGroupQuantity: (item.companyID == 3) ? groupItem.quantity : null,
                                    };

                                    var index = $scope.method.getIndexGroup(newGroupItem, $scope.groupOptions.specialRequirementOptionGroup);
                                    if (index == -1) {
                                        $scope.groupOptions.specialRequirementOptionGroup.push(newGroupItem);
                                    } else {
                                        if (item.companyID == 1) {
                                            $scope.groupOptions.specialRequirementOptionGroup[index].avfGroupQuantity = groupItem.quantity;
                                        } else {
                                            $scope.groupOptions.specialRequirementOptionGroup[index].avtGroupQuantity = groupItem.quantity;
                                        }
                                    }
                                }
                            }
                            break;
                        case 10:
                            for (var i = 0; i < item.breakdownCategoryOptionGroupDTOs.length; i++) {
                                var groupItem = item.breakdownCategoryOptionGroupDTOs[i];
                                var newGroupItem = {
                                    breakdownCategoryOptionGroupID: groupItem.breakdownCategoryOptionGroupID,
                                    breakdownCategoryOptionGroupNM: groupItem.breakdownCategoryOptionGroupNM,
                                    breakdownCategoryOptionGroupNMEN: groupItem.breakdownCategoryOptionGroupNMEN,
                                    breakdownCategoryOptionID: groupItem.breakdownCategoryOptionID,
                                    avfGroupQuantity: (item.companyID == 1) ? groupItem.quantity : null,
                                    avtGroupQuantity: (item.companyID == 3) ? groupItem.quantity : null,
                                };

                                var index = $scope.method.getIndexGroup(newGroupItem, $scope.groupOptions.managementOptionGroup);
                                if (index == -1) {
                                    $scope.groupOptions.managementOptionGroup.push(newGroupItem);
                                } else {
                                    if (item.companyID == 1) {
                                        $scope.groupOptions.managementOptionGroup[index].avfGroupQuantity = groupItem.quantity;
                                    } else {
                                        $scope.groupOptions.managementOptionGroup[index].avtGroupQuantity = groupItem.quantity;
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            },
            getIndexDetail: function (newDetailItem, listDetailItem) {
                for (var i = 0; i < listDetailItem.length; i++) {
                    var eleDetailItem = listDetailItem[i];
                    if (newDetailItem.productionItemID == eleDetailItem.productionItemID && newDetailItem.unitID == eleDetailItem.unitID && newDetailItem.breakdownCategoryOptionGroupID == eleDetailItem.breakdownCategoryOptionGroupID) {
                        return i;
                    }
                }
                return -1;
            },
            getIndexGroup: function (newGroupItem, listGroupItem) {
                for (var i = 0; i < listGroupItem.length; i++) {
                    var eleGroupItem = listGroupItem[i];
                    if (newGroupItem.breakdownCategoryOptionGroupID == eleGroupItem.breakdownCategoryOptionGroupID) {
                        return i;
                    }
                }
                return -1;
            },

            hasOptionDetail: function () {
                return ($scope.detailOptions.materialOptionDetail.length > 0 || $scope.detailOptions.frameOptionDetail.length > 0 || $scope.detailOptions.subMaterialOptionDetail.length > 0
                    || $scope.detailOptions.cushionOptionDetail.length > 0 || $scope.detailOptions.packingOptionDetail.length > 0 || $scope.detailOptions.hardwareOptionDetail.length > 0
                    || $scope.detailOptions.otherMaterialOptionDetail.length > 0 || $scope.detailOptions.fscOptionDetail.length > 0 || $scope.detailOptions.specialRequirementOptionDetail.length > 0
                    || $scope.detailOptions.managementOptionDetail.length > 0);
            },
            hasColspanDetail: function () {
                return (($scope.detailOptions.isMaterial || $scope.groupOptions.isMaterial) || ($scope.detailOptions.isFrameMaterial || $scope.groupOptions.isFrameMaterial) || ($scope.detailOptions.isSubMaterial || $scope.groupOptions.isSubMaterial)
                    || ($scope.detailOptions.isCushion || $scope.groupOptions.isCushion) || ($scope.detailOptions.isPacking || $scope.groupOptions.isPacking) || ($scope.detailOptions.isHardware || $scope.groupOptions.isHardware)
                    || ($scope.detailOptions.isOtherMaterial || $scope.groupOptions.isOtherMaterial) || ($scope.detailOptions.isFSC || $scope.groupOptions.isFSC) || ($scope.detailOptions.isSpecialRequirement || $scope.groupOptions.isSpecialRequirement)
                    || ($scope.detailOptions.isManagement || $scope.groupOptions.isManagement));
            },

            resetOptionDetail: function (categoryID) {
                switch (categoryID) {
                    case 1:
                        $scope.detailOptions.materialOptionDetail = [];
                        break;
                    case 2:
                        $scope.detailOptions.frameOptionDetail = [];
                        break;
                    case 3:
                        $scope.detailOptions.subMaterialOptionDetail = [];
                        break;
                    case 4:
                        $scope.detailOptions.cushionOptionDetail = [];
                        break;
                    case 5:
                        $scope.detailOptions.packingOptionDetail = [];
                        break;
                    case 8:
                        $scope.detailOptions.fscOptionDetail = [];
                        break;
                    case 9:
                        $scope.detailOptions.specialRequirementOptionDetail = [];
                        break;
                    default:
                        break;
                }
            },
            resetOptionGroup: function (categoryID) {
                switch (categoryID) {
                    case 1:
                        $scope.groupOptions.materialOptionGroup = [];
                        break;
                    case 2:
                        $scope.groupOptions.frameOptionGroup = [];
                        break;
                    case 3:
                        $scope.groupOptions.subMaterialOptionGroup = [];
                        break;
                    case 4:
                        $scope.groupOptions.cushionOptionGroup = [];
                        break;
                    case 5:
                        $scope.groupOptions.packingOptionGroup = [];
                        break;
                    case 8:
                        $scope.groupOptions.fscOptionGroup = [];
                        break;
                    case 9:
                        $scope.groupOptions.specialRequirementOptionGroup = [];
                        break;
                    default:
                        break;
                }
            },

            getUnitPriceUSD: function (price, exRate) {
                if (price == null) {
                    return null;
                }
                return (parseFloat(price) / parseFloat(exRate)).toFixed(2);
            },
            getSubTotalAmount: function (wastedPercent, quantity, price, totalAmount) {
                if (totalAmount != null && totalAmount != 0) {
                    return parseFloat(totalAmount);
                }

                if (price == null) {
                    return null;
                }

                if (wastedPercent != null) {
                    return Math.round(parseFloat(price) * parseFloat(quantity) * (1 + parseFloat(wastedPercent) / 100));
                }

                return parseFloat(quantity) * parseFloat(quantity);
            },
            getSubTotalAmountUSD: function (wastedPercent, quantity, price, totalAmount, exRate) {
                var result = $scope.method.getSubTotalAmount(wastedPercent, quantity, price, totalAmount);
                return (parseFloat(result) / parseFloat(exRate)).toFixed(2);
            },

            getQuantityInGroup: function (optionGroupID, companyID, optionGroups) {
                for (var i = 0; i < optionGroups.length; i++) {
                    var group = optionGroups[i];
                    if (group.breakdownCategoryOptionGroupID == optionGroupID) {
                        if (companyID == 1) {
                            return (group.avfGroupQuantity == null) ? 1 : group.avfGroupQuantity;
                        } else {
                            return (group.avtGroupQuantity == null) ? 1 : group.avtGroupQuantity;
                        }
                    }
                }
                return 1;
            },
            getSubTotalAmountGroup: function (categoryID, optionID, optionGroupID, groupQuantity, companyID) {
                switch (categoryID) {
                    case 1:
                        return $scope.method.getChildSubTotalAmountGroup(optionID, optionGroupID, groupQuantity, companyID, $scope.detailOptions.materialOptionDetail);
                    case 2:
                        return $scope.method.getChildSubTotalAmountGroup(optionID, optionGroupID, groupQuantity, companyID, $scope.detailOptions.frameOptionDetail);
                    case 3:
                        return $scope.method.getChildSubTotalAmountGroup(optionID, optionGroupID, groupQuantity, companyID, $scope.detailOptions.subMaterialOptionDetail);
                    case 4:
                        return $scope.method.getChildSubTotalAmountGroup(optionID, optionGroupID, groupQuantity, companyID, $scope.detailOptions.cushionOptionDetail);
                    case 5:
                        return $scope.method.getChildSubTotalAmountGroup(optionID, optionGroupID, groupQuantity, companyID, $scope.detailOptions.packingOptionDetail);
                    case 6:
                        return $scope.method.getChildSubTotalAmountGroup(optionID, optionGroupID, groupQuantity, companyID, $scope.detailOptions.hardwareOptionDetail);
                    case 7:
                        return $scope.method.getChildSubTotalAmountGroup(optionID, optionGroupID, groupQuantity, companyID, $scope.detailOptions.otherMaterialOptionDetail);
                    case 8:
                        return $scope.method.getChildSubTotalAmountGroup(optionID, optionGroupID, groupQuantity, companyID, $scope.detailOptions.fscOptionDetail);
                    case 9:
                        return $scope.method.getChildSubTotalAmountGroup(optionID, optionGroupID, groupQuantity, companyID, $scope.detailOptions.specialRequirementOptionDetail);
                    case 10:
                        return $scope.method.getChildSubTotalAmountGroup(optionID, optionGroupID, groupQuantity, companyID, $scope.detailOptions.managementOptionDetail);
                    default:
                        return null;
                }
            },
            getChildSubTotalAmountGroup: function (optionID, optionGroupID, groupQuantity, companyID, breakdownCategoryOptionDetails) {
                var totalAmount = parseFloat(0);

                for (var i = 0; i < breakdownCategoryOptionDetails.length; i++) {
                    var breakdownCategoryOptionDetail = breakdownCategoryOptionDetails[i];
                    if (breakdownCategoryOptionDetail.breakdownCategoryOptionID == optionID) {
                        if (breakdownCategoryOptionDetail.breakdownCategoryOptionGroupID != null && breakdownCategoryOptionDetail.breakdownCategoryOptionGroupID == optionGroupID) {
                            if (companyID == 1) {
                                totalAmount += $scope.method.getSubTotalAmount(breakdownCategoryOptionDetail.avfWastedRatePercent, breakdownCategoryOptionDetail.avfQuantity, breakdownCategoryOptionDetail.avfPrice, breakdownCategoryOptionDetail.avfAmount);
                            } else {
                                totalAmount += $scope.method.getSubTotalAmount(breakdownCategoryOptionDetail.avtWastedRatePercent, breakdownCategoryOptionDetail.avtQuantity, breakdownCategoryOptionDetail.avtPrice, breakdownCategoryOptionDetail.avtAmount);
                            }
                        }
                    }
                }
                if (totalAmount == null || totalAmount == 0) {
                    return null;
                }

                return parseFloat(Math.round(totalAmount)) * parseFloat(groupQuantity);
            },
            getSubTotalAmountGroupUSD: function (breakdownCategoryID, breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, exRate, companyID) {
                switch (breakdownCategoryID) {
                    case 1:
                        return $scope.method.getChildSubTotalAmountGroupUSD(breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, exRate, companyID, $scope.detailOptions.materialOptionDetail);

                    case 2:
                        return $scope.method.getChildSubTotalAmountGroupUSD(breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, exRate, companyID, $scope.detailOptions.frameOptionDetail);

                    case 3:
                        return $scope.method.getChildSubTotalAmountGroupUSD(breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, exRate, companyID, $scope.detailOptions.subMaterialOptionDetail);

                    case 4:
                        return $scope.method.getChildSubTotalAmountGroupUSD(breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, exRate, companyID, $scope.detailOptions.cushionOptionDetail);

                    case 5:
                        return $scope.method.getChildSubTotalAmountGroupUSD(breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, exRate, companyID, $scope.detailOptions.packingOptionDetail);

                    case 6:
                        return $scope.method.getChildSubTotalAmountGroupUSD(breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, exRate, companyID, $scope.detailOptions.hardwareOptionDetail);

                    case 7:
                        return $scope.method.getChildSubTotalAmountGroupUSD(breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, exRate, companyID, $scope.detailOptions.otherMaterialOptionDetail);

                    case 8:
                        return $scope.method.getChildSubTotalAmountGroupUSD(breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, exRate, companyID, $scope.detailOptions.fscOptionDetail);

                    case 9:
                        return $scope.method.getChildSubTotalAmountGroupUSD(breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, exRate, companyID, $scope.detailOptions.specialRequirementOptionDetail);

                    case 10:
                        return $scope.method.getChildSubTotalAmountGroupUSD(breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, exRate, companyID, $scope.detailOptions.managementOptionDetail);

                    default:
                        return null;
                }
            },
            getChildSubTotalAmountGroupUSD: function (breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, exRate, companyID, breakdownCategoryOptionDetails) {
                var totalAmountVND = $scope.method.getChildSubTotalAmountGroup(breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, companyID, breakdownCategoryOptionDetails);

                if (totalAmountVND == 0) {
                    return null;
                }

                return (parseFloat(totalAmountVND) / parseFloat(exRate)).toFixed(2);
            },
            getTotalAmountCategory: function (breakdownCategoryID, breakdownCategoryOptionID, companyID) {
                switch (breakdownCategoryID) {
                    case 1:
                        return $scope.method.getChildTotalAmountCatgory(breakdownCategoryID, breakdownCategoryOptionID, companyID, $scope.detailOptions.materialOptionDetail, $scope.groupOptions.materialOptionGroup);

                    case 2:
                        return $scope.method.getChildTotalAmountCatgory(breakdownCategoryID, breakdownCategoryOptionID, companyID, $scope.detailOptions.frameOptionDetail, $scope.groupOptions.frameOptionGroup);

                    case 3:
                        return $scope.method.getChildTotalAmountCatgory(breakdownCategoryID, breakdownCategoryOptionID, companyID, $scope.detailOptions.subMaterialOptionDetail, $scope.groupOptions.subMaterialOptionGroup);

                    case 4:
                        return $scope.method.getChildTotalAmountCatgory(breakdownCategoryID, breakdownCategoryOptionID, companyID, $scope.detailOptions.cushionOptionDetail, $scope.groupOptions.cushionOptionGroup);

                    case 5:
                        return $scope.method.getChildTotalAmountCatgory(breakdownCategoryID, breakdownCategoryOptionID, companyID, $scope.detailOptions.packingOptionDetail, $scope.groupOptions.packingOptionGroup);

                    case 6:
                        return $scope.method.getChildTotalAmountCatgory(breakdownCategoryID, breakdownCategoryOptionID, companyID, $scope.detailOptions.hardwareOptionDetail, $scope.groupOptions.hardwareOptionGroup);

                    case 7:
                        return $scope.method.getChildTotalAmountCatgory(breakdownCategoryID, breakdownCategoryOptionID, companyID, $scope.detailOptions.otherMaterialOptionDetail, $scope.groupOptions.otherMaterialOptionGroup);

                    case 8:
                        return $scope.method.getChildTotalAmountCatgory(breakdownCategoryID, breakdownCategoryOptionID, companyID, $scope.detailOptions.fscOptionDetail, $scope.groupOptions.fscOptionGroup);

                    case 9:
                        return $scope.method.getChildTotalAmountCatgory(breakdownCategoryID, breakdownCategoryOptionID, companyID, $scope.detailOptions.specialRequirementOptionDetail, $scope.groupOptions.specialRequirementOptionGroup);

                    case 10:
                        return $scope.method.getChildTotalAmountCatgory(breakdownCategoryID, breakdownCategoryOptionID, companyID, $scope.detailOptions.managementOptionDetail, $scope.groupOptions.managementOptionGroup);

                    default:
                        return parseFloat(0);
                }
            },
            getChildTotalAmountCatgory: function (categoryID, optionCategoryID, companyID, optionDetails, optionGroups) {
                var totalAmountNullGroup = parseFloat(0);
                var totalAmountGroup = parseFloat(0);
                var onlyCalculate = 1; // Only calculate number in group once.

                for (var i = 0; i < optionDetails.length; i++) {
                    var optionDetail = optionDetails[i];

                    if (optionDetail.breakdownCategoryOptionGroupID == null) {
                        if (companyID == 1) {
                            var totalAmountTemp = $scope.method.getSubTotalAmount(optionDetail.avfWastedRatePercent, optionDetail.avfQuantity, optionDetail.avfPrice, optionDetail.avfAmount);
                            totalAmountNullGroup += totalAmountTemp;
                        } else {
                            var totalAmountTemp = $scope.method.getSubTotalAmount(optionDetail.avtWastedRatePercent, optionDetail.avtQuantity, optionDetail.avtPrice, optionDetail.avtAmount);
                            totalAmountNullGroup += totalAmountTemp;
                        }
                    } else {
                        if (onlyCalculate == 1) {
                            if (companyID == 1) {
                                var groupQuantity = $scope.method.getQuantityInGroup(optionDetail.breakdownCategoryOptionGroupID, 1, optionGroups);
                                var totalAmountTemp = $scope.method.getSubTotalAmountGroup(categoryID, optionCategoryID, optionDetail.breakdownCategoryOptionGroupID, groupQuantity, 1);
                                totalAmountGroup += totalAmountTemp;
                            } else {
                                var groupQuantity = $scope.method.getQuantityInGroup(optionDetail.breakdownCategoryOptionGroupID, 3, optionGroups);
                                var totalAmountTemp = $scope.method.getSubTotalAmountGroup(categoryID, optionCategoryID, optionDetail.breakdownCategoryOptionGroupID, groupQuantity, 3);
                                totalAmountGroup += totalAmountTemp;
                            }

                            onlyCalculate += 1;
                        }
                    }
                }

                return parseFloat(totalAmountNullGroup) + parseFloat(totalAmountGroup);
            },
            getTotalAmountCategoryUSD: function (breakdownCategoryID, breakdownCategoryOptionID, exRate, companyID) {
                switch (breakdownCategoryID) {
                    case 1:
                        return $scope.method.getChildTotalAmountCatgoryUSD(breakdownCategoryID, breakdownCategoryOptionID, exRate, companyID, $scope.detailOptions.materialOptionDetail, $scope.groupOptions.materialOptionGroup);

                    case 2:
                        return $scope.method.getChildTotalAmountCatgoryUSD(breakdownCategoryID, breakdownCategoryOptionID, exRate, companyID, $scope.detailOptions.frameOptionDetail, $scope.groupOptions.frameOptionGroup);

                    case 3:
                        return $scope.method.getChildTotalAmountCatgoryUSD(breakdownCategoryID, breakdownCategoryOptionID, exRate, companyID, $scope.detailOptions.subMaterialOptionDetail, $scope.groupOptions.subMaterialOptionGroup);

                    case 4:
                        return $scope.method.getChildTotalAmountCatgoryUSD(breakdownCategoryID, breakdownCategoryOptionID, exRate, companyID, $scope.detailOptions.cushionOptionDetail, $scope.groupOptions.cushionOptionGroup);

                    case 5:
                        return $scope.method.getChildTotalAmountCatgoryUSD(breakdownCategoryID, breakdownCategoryOptionID, exRate, companyID, $scope.detailOptions.packingOptionDetail, $scope.groupOptions.packingOptionGroup);

                    case 6:
                        return $scope.method.getChildTotalAmountCatgoryUSD(breakdownCategoryID, breakdownCategoryOptionID, exRate, companyID, $scope.detailOptions.hardwareOptionDetail, $scope.groupOptions.hardwareOptionGroup);

                    case 7:
                        return $scope.method.getChildTotalAmountCatgoryUSD(breakdownCategoryID, breakdownCategoryOptionID, exRate, companyID, $scope.detailOptions.otherMaterialOptionDetail, $scope.groupOptions.otherMaterialOptionGroup);

                    case 8:
                        return $scope.method.getChildTotalAmountCatgoryUSD(breakdownCategoryID, breakdownCategoryOptionID, exRate, companyID, $scope.detailOptions.fscOptionDetail, $scope.groupOptions.fscOptionGroup);

                    case 9:
                        return $scope.method.getChildTotalAmountCatgoryUSD(breakdownCategoryID, breakdownCategoryOptionID, exRate, companyID, $scope.detailOptions.specialRequirementOptionDetail, $scope.groupOptions.specialRequirementOptionGroup);

                    case 10:
                        return $scope.method.getChildTotalAmountCatgoryUSD(breakdownCategoryID, breakdownCategoryOptionID, exRate, companyID, $scope.detailOptions.managementOptionDetail, $scope.groupOptions.managementOptionGroup);

                    default:
                        return parseFloat(0);
                }
            },
            getChildTotalAmountCatgoryUSD: function (categoryID, optionCategoryID, exRate, companyID, optionDetails, optionGroups) {
                var totalAmountUSD = $scope.method.getChildTotalAmountCatgory(categoryID, optionCategoryID, companyID, optionDetails, optionGroups);
                return (parseFloat(totalAmountUSD) / parseFloat(exRate)).toFixed(2);
            },
            getSubTotalAmountManagement: function (percent, companyID) {
                var totalMaterialCost = $scope.method.getTotalAmountCategory(1, $scope.selectedOptions.materialCostOptionID, companyID);
                var totalFrameCost = $scope.method.getTotalAmountCategory(2, $scope.selectedOptions.frameCostOptionID, companyID);
                var totalSubMaterialCost = $scope.method.getTotalAmountCategory(3, $scope.selectedOptions.subMaterialCostOptionID, companyID);
                var totalCushionCost = $scope.method.getTotalAmountCategory(4, $scope.selectedOptions.cushionCostOptionID, companyID);
                var totalPackingCost = $scope.method.getTotalAmountCategory(5, $scope.selectedOptions.packingCostOptionID, companyID);
                var totalHardwareCost = $scope.method.getTotalAmountCategory(6, $scope.selectedOptions.hardwareCostOptionID, companyID);
                var totalOtherMaterialCost = $scope.method.getTotalAmountCategory(7, $scope.selectedOptions.otherMaterialCostOptionID, companyID);
                var totalFscCost = $scope.method.getTotalAmountCategory(8, $scope.selectedOptions.fscCostOptionID, companyID);
                var totalSpecialRequirmentCost = $scope.method.getTotalAmountCategory(9, $scope.selectedOptions.specialRequirementCostOptionID, companyID);

                if (percent == null || percent == 0) {
                    return null;
                } else {
                    if (percent == 0) {
                        return (parseFloat(totalMaterialCost) + parseFloat(totalFrameCost) + parseFloat(totalSubMaterialCost) + parseFloat(totalCushionCost) + parseFloat(totalPackingCost) + parseFloat(totalHardwareCost) + parseFloat(totalOtherMaterialCost) + parseFloat(totalFscCost) + parseFloat(totalSpecialRequirmentCost));
                    }
                    return (parseFloat(totalMaterialCost) + parseFloat(totalFrameCost) + parseFloat(totalSubMaterialCost) + parseFloat(totalCushionCost) + parseFloat(totalPackingCost) + parseFloat(totalHardwareCost) + parseFloat(totalOtherMaterialCost) + parseFloat(totalFscCost) + parseFloat(totalSpecialRequirmentCost)) * parseFloat(percent) / parseFloat(100);
                }
            },
            getSubTotalAmountManagementUSD: function (percent, exRate, companyID) {
                var totalMaterialCost = $scope.method.getTotalAmountCategory(1, $scope.selectedOptions.materialCostOptionID, companyID);
                var totalFrameCost = $scope.method.getTotalAmountCategory(2, $scope.selectedOptions.frameCostOptionID, companyID);
                var totalSubMaterialCost = $scope.method.getTotalAmountCategory(3, $scope.selectedOptions.subMaterialCostOptionID, companyID);
                var totalCushionCost = $scope.method.getTotalAmountCategory(4, $scope.selectedOptions.cushionCostOptionID, companyID);
                var totalPackingCost = $scope.method.getTotalAmountCategory(5, $scope.selectedOptions.packingCostOptionID, companyID);
                var totalHardwareCost = $scope.method.getTotalAmountCategory(6, $scope.selectedOptions.hardwareCostOptionID, companyID);
                var totalOtherMaterialCost = $scope.method.getTotalAmountCategory(7, $scope.selectedOptions.otherMaterialCostOptionID, companyID);
                var totalFscCost = $scope.method.getTotalAmountCategory(8, $scope.selectedOptions.fscCostOptionID, companyID);
                var totalSpecialRequirmentCost = $scope.method.getTotalAmountCategory(9, $scope.selectedOptions.specialRequirementCostOptionID, companyID);

                if (percent == null || percent == 0) {
                    return null;
                } else {
                    if (percent == 0) {
                        return (parseFloat(totalMaterialCost) + parseFloat(totalFrameCost) + parseFloat(totalSubMaterialCost) + parseFloat(totalCushionCost) + parseFloat(totalPackingCost) + parseFloat(totalHardwareCost) + parseFloat(totalOtherMaterialCost) + parseFloat(totalFscCost) + parseFloat(totalSpecialRequirmentCost)) / parseFloat(exRate);
                    }
                    return (((parseFloat(totalMaterialCost) + parseFloat(totalFrameCost) + parseFloat(totalSubMaterialCost) + parseFloat(totalCushionCost) + parseFloat(totalPackingCost) + parseFloat(totalHardwareCost) + parseFloat(totalOtherMaterialCost) + parseFloat(totalFscCost) + parseFloat(totalSpecialRequirmentCost)) * parseFloat(percent) / parseFloat(100)) / parseFloat(exRate)).toFixed(2);
                }
            },
            getBasedOnAmountManagement: function (percent, companyID) {
                var totalMaterialCost = $scope.method.getTotalAmountCategory(1, $scope.selectedOptions.materialCostOptionID, companyID);
                var totalFrameCost = $scope.method.getTotalAmountCategory(2, $scope.selectedOptions.frameCostOptionID, companyID);
                var totalSubMaterialCost = $scope.method.getTotalAmountCategory(3, $scope.selectedOptions.subMaterialCostOptionID, companyID);
                var totalCushionCost = $scope.method.getTotalAmountCategory(4, $scope.selectedOptions.cushionCostOptionID, companyID);
                var totalPackingCost = $scope.method.getTotalAmountCategory(5, $scope.selectedOptions.packingCostOptionID, companyID);
                var totalHardwareCost = $scope.method.getTotalAmountCategory(6, $scope.selectedOptions.hardwareCostOptionID, companyID);
                var totalOtherMaterialCost = $scope.method.getTotalAmountCategory(7, $scope.selectedOptions.otherMaterialCostOptionID, companyID);
                var totalFscCost = $scope.method.getTotalAmountCategory(8, $scope.selectedOptions.fscCostOptionID, companyID);
                var totalSpecialRequirmentCost = $scope.method.getTotalAmountCategory(9, $scope.selectedOptions.specialRequirementCostOptionID, companyID);

                if (percent == null || percent == 0) {
                    return null;
                } else {
                    return (parseFloat(totalMaterialCost) + parseFloat(totalFrameCost) + parseFloat(totalSubMaterialCost) + parseFloat(totalCushionCost) + parseFloat(totalPackingCost) + parseFloat(totalHardwareCost) + parseFloat(totalOtherMaterialCost) + parseFloat(totalFscCost) + parseFloat(totalSpecialRequirmentCost));
                }
            },
            getBasedOnAmountManagementUSD: function (percent, exRate, companyID) {
                var totalMaterialCost = $scope.method.getTotalAmountCategory(1, $scope.selectedOptions.materialCostOptionID, companyID);
                var totalFrameCost = $scope.method.getTotalAmountCategory(2, $scope.selectedOptions.frameCostOptionID, companyID);
                var totalSubMaterialCost = $scope.method.getTotalAmountCategory(3, $scope.selectedOptions.subMaterialCostOptionID, companyID);
                var totalCushionCost = $scope.method.getTotalAmountCategory(4, $scope.selectedOptions.cushionCostOptionID, companyID);
                var totalPackingCost = $scope.method.getTotalAmountCategory(5, $scope.selectedOptions.packingCostOptionID, companyID);
                var totalHardwareCost = $scope.method.getTotalAmountCategory(6, $scope.selectedOptions.hardwareCostOptionID, companyID);
                var totalOtherMaterialCost = $scope.method.getTotalAmountCategory(7, $scope.selectedOptions.otherMaterialCostOptionID, companyID);
                var totalFscCost = $scope.method.getTotalAmountCategory(8, $scope.selectedOptions.fscCostOptionID, companyID);
                var totalSpecialRequirmentCost = $scope.method.getTotalAmountCategory(9, $scope.selectedOptions.specialRequirementCostOptionID, companyID);

                if (percent == null || percent == 0) {
                    return null;
                } else {
                    return ((parseFloat(totalMaterialCost) + parseFloat(totalFrameCost) + parseFloat(totalSubMaterialCost) + parseFloat(totalCushionCost) + parseFloat(totalPackingCost) + parseFloat(totalHardwareCost) + parseFloat(totalOtherMaterialCost) + parseFloat(totalFscCost) + parseFloat(totalSpecialRequirmentCost)) / parseFloat(exRate)).toFixed(2);
                }
            },
            getSubTotalAmountGroupManagement: function (breakdownCategoryID, breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, companyID) {
                return $scope.method.getChildSubTotalAmountGroupManagement(breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, companyID, $scope.detailOptions.managementOptionDetail);
            },
            getSubTotalAmountGroupUSDManagement: function (breakdownCategoryID, breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, exRate, companyID) {
                return $scope.method.getChildSubTotalAmountGroupUSDManagement(breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, exRate, companyID, $scope.detailOptions.managementOptionDetail);
            },
            getChildSubTotalAmountGroupManagement: function (breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, companyID, breakdownCategoryOptionDetails) {
                var totalAmount = parseFloat(0);

                for (var i = 0; i < breakdownCategoryOptionDetails.length; i++) {
                    var breakdownCategoryOptionDetail = breakdownCategoryOptionDetails[i];
                    //if (breakdownCategoryOptionDetail.breakdownCategoryOptionID == breakdownCategoryOptionID) {
                    if (breakdownCategoryOptionDetail.breakdownCategoryOptionGroupID != null && breakdownCategoryOptionDetail.breakdownCategoryOptionGroupID == breakdownCategoryOptionGroupID) {
                        if (companyID == 1) {
                            totalAmount += Math.round($scope.method.getSubTotalAmountManagement(breakdownCategoryOptionDetail.avfPercent, companyID));
                        } else {
                            totalAmount += Math.round($scope.method.getSubTotalAmountManagement(breakdownCategoryOptionDetail.avtPercent, companyID));
                        }
                    }
                    //}
                }

                if (totalAmount == null || totalAmount == 0) {
                    return null;
                }

                return parseFloat(totalAmount) * parseFloat(groupQuantity);
            },
            getChildSubTotalAmountGroupUSDManagement: function (breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, exRate, companyID, breakdownCategoryOptionDetails) {
                var totalAmountUSD = $scope.method.getChildSubTotalAmountGroupManagement(breakdownCategoryOptionID, breakdownCategoryOptionGroupID, groupQuantity, companyID, breakdownCategoryOptionDetails);

                if (totalAmountUSD == 0) {
                    return null;
                }

                return (parseFloat(totalAmountUSD) / parseFloat(exRate)).toFixed(2);
            },
            getTotalAmountCategoryManagement: function (breakdownCategoryID, breakdownCategoryOptionID, companyID) {
                return $scope.method.getChildTotalAmountCatgoryManagement(breakdownCategoryID, breakdownCategoryOptionID, companyID, $scope.detailOptions.managementOptionDetail, $scope.groupOptions.managementOptionGroup);
            },
            getTotalAmountCategoryUSDManagement: function (breakdownCategoryID, breakdownCategoryOptionID, exRate, companyID) {
                return $scope.method.getChildTotalAmountCatgoryUSDManagement(breakdownCategoryID, breakdownCategoryOptionID, exRate, companyID, $scope.detailOptions.managementOptionDetail, $scope.groupOptions.managementOptionGroup);
            },
            getChildTotalAmountCatgoryManagement: function (categoryID, optionCategoryID, companyID, optionDetails, optionGroups) {
                var totalAmountNullGroup = parseFloat(0);
                var totalAmountGroup = parseFloat(0);
                var onlyCalculate = 1; // Only calculate number in group once.

                for (var i = 0; i < optionDetails.length; i++) {
                    var optionDetail = optionDetails[i];

                    //if (optionDetail.breakdownCategoryOptionID == optionCategoryID) {
                    if (optionDetail.breakdownCategoryOptionGroupID == null) {
                        if (companyID == 1) {
                            var totalAmountTemp = Math.round($scope.method.getSubTotalAmountManagement(optionDetail.avfPercent, companyID));
                            totalAmountNullGroup += totalAmountTemp;
                        } else {
                            var totalAmountTemp = Math.round($scope.method.getSubTotalAmountManagement(optionDetail.avtPercent, companyID));
                            totalAmountNullGroup += totalAmountTemp;
                        }
                    } else {
                        if (onlyCalculate == 1) {
                            if (companyID == 1) {
                                var groupQuantity = $scope.method.getQuantityInGroup(optionDetail.breakdownCategoryOptionGroupID, 1, optionGroups);
                                var totalAmountTemp = Math.round($scope.method.getSubTotalAmountGroupManagement(categoryID, optionCategoryID, optionDetail.breakdownCategoryOptionGroupID, groupQuantity, 1));
                                totalAmountGroup += totalAmountTemp;
                            } else {
                                var groupQuantity = $scope.method.getQuantityInGroup(optionDetail.breakdownCategoryOptionGroupID, 3, optionGroups);
                                var totalAmountTemp = Math.round($scope.method.getSubTotalAmountGroupManagement(categoryID, optionCategoryID, optionDetail.breakdownCategoryOptionGroupID, groupQuantity, 3));
                                totalAmountGroup += totalAmountTemp;
                            }

                            onlyCalculate += 1;
                        }
                    }
                    //}
                }

                return parseFloat(totalAmountNullGroup) + parseFloat(totalAmountGroup);
            },
            getChildTotalAmountCatgoryUSDManagement: function (categoryID, optionCategoryID, exRate, companyID, optionDetails, optionGroups) {
                var totalAmountUSD = $scope.method.getChildTotalAmountCatgoryManagement(categoryID, optionCategoryID, companyID, optionDetails, optionGroups);
                return (parseFloat(totalAmountUSD) / parseFloat(exRate)).toFixed(2);
            },

            getTotalAmount: function (companyID) {
                var totalMaterialCost = $scope.method.getTotalAmountCategory(1, $scope.selectedOptions.materialCostOptionID, companyID);
                var totalFrameCost = $scope.method.getTotalAmountCategory(2, $scope.selectedOptions.frameCostOptionID, companyID);
                var totalSubMaterialCost = $scope.method.getTotalAmountCategory(3, $scope.selectedOptions.subMaterialCostOptionID, companyID);
                var totalCushionCost = $scope.method.getTotalAmountCategory(4, $scope.selectedOptions.cushionCostOptionID, companyID);
                var totalPackingCost = $scope.method.getTotalAmountCategory(5, $scope.selectedOptions.packingCostOptionID, companyID);
                var totalHardwareCost = $scope.method.getTotalAmountCategory(6, $scope.selectedOptions.hardwareCostOptionID, companyID);
                var totalOtherMaterialCost = $scope.method.getTotalAmountCategory(7, $scope.selectedOptions.otherMaterialCostOptionID, companyID);
                var totalFscCost = $scope.method.getTotalAmountCategory(8, $scope.selectedOptions.fscCostOptionID, companyID);
                var totalSpecialRequirmentCost = $scope.method.getTotalAmountCategory(9, $scope.selectedOptions.specialRequirementCostOptionID, companyID);
                var totalManagementCost = $scope.method.getTotalAmountCategoryManagement(10, $scope.selectedOptions.managementCostOptionID, companyID);

                return totalMaterialCost + totalFrameCost + totalSubMaterialCost + totalCushionCost + totalPackingCost + totalHardwareCost + totalOtherMaterialCost + totalFscCost + totalSpecialRequirmentCost + totalManagementCost;
            },
            getTotalAmountUSD: function (exRate, companyID) {
                var totalAmountVND = $scope.method.getTotalAmount(companyID);
                return (parseFloat(totalAmountVND) / parseFloat(exRate)).toFixed(2);
            },

            getSpecAmount: function (specSeason, companyID) {
                var totalAmountVND = $scope.method.getTotalAmount(companyID);
                if (specSeason == null || specSeason == 0) {
                    return totalAmountVND;
                }
                return Math.round(parseFloat(totalAmountVND) * parseFloat(specSeason) / 100);
            },
            getSpecAmountUSD: function (specSeason, exRate, companyID) {
                var totalAmountUSD = $scope.method.getTotalAmountUSD(exRate, companyID);
                if (specSeason == null || specSeason == 0) {
                    return totalAmountUSD;
                }
                return (parseFloat(totalAmountUSD) * parseFloat(specSeason) / 100).toFixed(2);
            },

            getFromShippingFactory: function (factoryID) {
                if (factoryID == null) {
                    return null;
                }
                for (var i = 0; i < $scope.supportFactory.length; i++) {
                    var factory = $scope.supportFactory[i];
                    if (factory.factoryID == factoryID) {
                        return factory;
                    }
                }
                return null;
            },
            getShippingFee: function (factoryID, categoryID, optionCategoryID, companyID) {
                var result = parseFloat(0);
                var fromShippingFactory = $scope.method.getFromShippingFactory(factoryID);
                var loadAbility = $scope.method.getLoadAbility(categoryID, optionCategoryID, companyID);
                if (fromShippingFactory != null && loadAbility != null && fromShippingFactory.exportCost40HC != null) {
                    result = Math.round(parseFloat(fromShippingFactory.exportCost40HC) / parseFloat(loadAbility));
                }
                return result;
            },
            getShippingFeeUSD: function (factoryID, categoryID, optionCategoryID, exRate, companyID) {
                var result = $scope.method.getShippingFee(factoryID, categoryID, optionCategoryID, companyID);
                return (parseFloat(result) / parseFloat(exRate)).toFixed(2);
            },

            getTotalCostPrice: function (specSeason, factoryID, categoryID, optionCategoryID, companyID) {
                var totalAmount = $scope.method.getTotalAmount(companyID);
                var shippingFee = $scope.method.getShippingFee(factoryID, categoryID, optionCategoryID, companyID);

                if (specSeason != null && specSeason != 0) {
                    return Math.round(parseFloat(totalAmount) * (1 + parseFloat(specSeason) / 100) + parseFloat(shippingFee));
                } else {
                    return Math.round(parseFloat(totalAmount) * (1 + parseFloat(0) / 100) + parseFloat(shippingFee));
                }
            },
            getTotalCostPriceUSD: function (specSeason, factoryID, categoryID, optionCategoryID, exRate, companyID) {
                var totalCostPrice = $scope.method.getTotalCostPrice(specSeason, factoryID, categoryID, optionCategoryID, companyID);
                if (exRate == null || exRate == 0) {
                    return parseFloat(totalCostPrice);
                } else {
                    return (parseFloat(totalCostPrice) / parseFloat(exRate)).toFixed(2);
                }
            },
            getLoadAbility: function (categoryID, optionCategoryID, companyID) {
                if ($scope.data != null) {
                    for (var i = 0; i < $scope.data.breakdownCategoryOptionDTOs.length; i++) {
                        var optionCategory = $scope.data.breakdownCategoryOptionDTOs[i];
                        if (optionCategory.breakdownCategoryID == categoryID && /*(optionCategory.breakdownCategoryOptionID == optionCategoryID || optionCategory.breakdownCategoryOptionID == (optionCategoryID - 1)) &&*/ optionCategory.companyID == companyID) {
                            if (optionCategory.loadAbility == null) {
                                return null;
                            } else {
                                return parseFloat(optionCategory.loadAbility);
                            }
                        }
                    }
                }
                return null;
            },
        };

        $timeout(function () {
            $scope.event.init();
        }, 0);
    };
})();