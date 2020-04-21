(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives','ui.bootstrap']);
    angular.module('tilsoftApp').controller('tilsoftController', ['$scope', '$timeout', 'dataService', 'factoryBreakdown', function ($scope, $timeout, dataService, factoryBreakdown) {
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;
        dataService.supportServiceUrl = context.factoryBreakdownUrl;

        //
        // scope data
        //
        $scope.data = null;
        $scope.availableOptionByArticleCodeDTOs = null;
        $scope.companyId = context.companyId;

        $scope.categories = null;
        $scope.factories = null;
        $scope.exRate = 1;
        $scope.seasonSpecObj = {
            rate: null
        };

        $scope.currentArticleCode = null;

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
            managementCostOptionID: null,
            shippedFromFactoryID: null
        };

        $scope.selectedOptionsTemp = {
            materialCostOptionID: 0,
            frameCostOptionID: 0,
            subMaterialCostOptionID: 0,
            cushionCostOptionID: 0,
            packingCostOptionID: 0,
            hardwareCostOptionID: 0,
            otherMaterialCostOptionID: 0,
            fscCostOptionID: 0,
            specialRequirementCostOptionID: 0,
            managementCostOptionID: 0
        };

        $scope.countConfirmed = 0;
        $scope.countConfirmedOtherCompany = 0;

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    breakDownService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                breakDownService.searchFilter.sortedDirection = sortedDirection;
                breakDownService.searchFilter.pageIndex = $scope.pageIndex = 1;
                breakDownService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };

        //
        // events
        //
        $scope.event = {
            init: function () {
                dataService.load(
                    context.id,
                    context.modelId,
                    context.sampleProductId,
                    context.offerSeasonDetailId,
                    context.factoryId,
                    function (data) {
                        if (data.message.type === 0) {
                            if (context.id === 0) {
                                // redirect to existing id - created automatically from the server
                                window.location = context.redirectUrl + '/' + data.data.data.breakdownID + '?modelId=' + context.modelId + '&sampleProductId=' + context.sampleProductId + '&offerSeasonDetailId=' + context.offerSeasonDetailId + '&factoryId=' + context.factoryId;
                            }

                            // map data when load success
                            $scope.data = data.data.data;
                            $scope.availableOptionByArticleCodeDTOs = data.data.availableOptionByArticleCodeDTOs;
                            $scope.categories = data.data.breakdownCategoryDTOs;
                            $scope.exRate = data.data.exchangeRate;
                            $scope.factories = data.data.factoryDTOs;
                            $scope.selectedOptions.shippedFromFactoryID = data.data.factoryID;

                            // pre-select the default option
                            angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                                if (option.isSelected) {
                                    switch (option.breakdownCategoryID) {
                                        case 1:
                                            $scope.selectedOptions.materialCostOptionID = option.breakdownCategoryOptionID;
                                            break;

                                        case 2:
                                            $scope.selectedOptions.frameCostOptionID = option.breakdownCategoryOptionID;
                                            break;

                                        case 3:
                                            $scope.selectedOptions.subMaterialCostOptionID = option.breakdownCategoryOptionID;
                                            break;

                                        case 4:
                                            $scope.selectedOptions.cushionCostOptionID = option.breakdownCategoryOptionID;
                                            break;

                                        case 5:
                                            $scope.selectedOptions.packingCostOptionID = option.breakdownCategoryOptionID;
                                            break;

                                        case 6:
                                            $scope.selectedOptions.hardwareCostOptionID = option.breakdownCategoryOptionID;
                                            break;

                                        case 7:
                                            $scope.selectedOptions.otherMaterialCostOptionID = option.breakdownCategoryOptionID;
                                            break;

                                        case 8:
                                            $scope.selectedOptions.fscCostOptionID = option.breakdownCategoryOptionID;
                                            break;

                                        case 9:
                                            $scope.selectedOptions.specialRequirementCostOptionID = option.breakdownCategoryOptionID;
                                            break;

                                        case 10:
                                            $scope.selectedOptions.managementCostOptionID = option.breakdownCategoryOptionID;
                                            break;
                                    }
                                }
                            });

                            $scope.method.checkConfirmAll();
                            $scope.method.checkConfirmAllOtherCompany();                            

                            $('#content').show();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                        $scope.data = null;
                        $scope.categories = null;
                        $scope.factories = null;
                    }
                );
            },
            addOption: function (categoryId) {
                switch (categoryId) {
                    case 1:
                        $scope.frmNewMaterialObj.event.open();
                        break;

                    case 2:
                        $scope.frmNewFrameObj.event.open();
                        break;

                    case 3:
                        var subMaterialID = null;
                        var subMaterialColorID = null;

                        for (var i = 0; i < $scope.data.breakdownCategoryOptionDTOs.length; i++) {
                            var item = $scope.data.breakdownCategoryOptionDTOs[i];
                            if (item.breakdownCategoryID == 3 && item.breakdownCategoryOptionID == $scope.selectedOptions.subMaterialCostOptionID) {
                                subMaterialID = item.subMaterialID;
                                subMaterialColorID = item.subMaterialColorID;
                            }
                        }

                        $scope.frmNewSubMaterialObj.event.open(subMaterialID, subMaterialColorID);
                        break;

                    case 4:
                        $scope.frmNewCushionObj.event.open();
                        break;

                    case 5:
                        $scope.frmNewPackingObj.event.open();
                        break;

                    case 8:
                        $scope.frmNewFSCObj.event.open();
                        break;

                    case 9:
                        $scope.frmNewSpecialRequestObj.event.open();
                        break;

                }
            },
            editOption: function (id) {
                var selectedCategory;
                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                    if (item.breakdownCategoryOptionID === id) {
                        selectedCategory = item;
                    }
                });
                $scope.frmDetailObj.data = JSON.parse(JSON.stringify(selectedCategory));
                $scope.frmDetailObj.event.open();
            },
            approveOption: function (id) {
                if (confirm('Approve and lock the selected option for editing?')) {
                    dataService.approveOption(
                        id,
                        function (data) {
                            if (data.message.type === 0) {
                                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                                    if (item.breakdownCategoryOptionID === id) {
                                        item.confirmerName = 'yourself';
                                        item.confirmedDate = 'Just now';
                                        item.isConfirmed = true;
                                    }
                                });
                                $scope.method.checkConfirmAll();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                            $scope.frmNewMaterialObj.data = null;
                            $scope.frmNewMaterialObj.supportData = null;
                        }
                    );
                }
            },
            unApproveOption: function (id) {
                if (confirm('Reset confirm status and unlock the selected option for editing?')) {
                    dataService.unApproveOption(
                        id,
                        function (data) {
                            if (data.message.type === 0) {
                                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                                    if (item.breakdownCategoryOptionID === id) {
                                        item.confirmerName = null;
                                        item.confirmedDate = null;
                                        item.isConfirmed = null;
                                    }
                                });
                                $scope.method.checkConfirmAll();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                            $scope.frmNewMaterialObj.data = null;
                            $scope.frmNewMaterialObj.supportData = null;
                        }
                    );
                }
            },
            approveAllOption: function (data) {
                if (confirm('Approve and lock all option for editing?')) {
                    if ($scope.method.checkIsConfirm(data.materialCostOptionID)) {
                        $scope.selectedOptionsTemp.materialCostOptionID = $scope.selectedOptions.materialCostOptionID;
                    }
                    if ($scope.method.checkIsConfirm(data.frameCostOptionID)) {
                        $scope.selectedOptionsTemp.frameCostOptionID = $scope.selectedOptions.frameCostOptionID;
                    }
                    if ($scope.method.checkIsConfirm(data.subMaterialCostOptionID)) {
                        $scope.selectedOptionsTemp.subMaterialCostOptionID = $scope.selectedOptions.subMaterialCostOptionID;
                    }
                    if ($scope.method.checkIsConfirm(data.cushionCostOptionID)) {
                        $scope.selectedOptionsTemp.cushionCostOptionID = $scope.selectedOptions.cushionCostOptionID;
                    }
                    if ($scope.method.checkIsConfirm(data.packingCostOptionID)) {
                        $scope.selectedOptionsTemp.packingCostOptionID = $scope.selectedOptions.packingCostOptionID;
                    }
                    if ($scope.method.checkIsConfirm(data.hardwareCostOptionID)) {
                        $scope.selectedOptionsTemp.hardwareCostOptionID = $scope.selectedOptions.hardwareCostOptionID;
                    }
                    if ($scope.method.checkIsConfirm(data.otherMaterialCostOptionID)) {
                        $scope.selectedOptionsTemp.otherMaterialCostOptionID = $scope.selectedOptions.otherMaterialCostOptionID;
                    }
                    if ($scope.method.checkIsConfirm(data.fscCostOptionID)) {
                        $scope.selectedOptionsTemp.fscCostOptionID = $scope.selectedOptions.fscCostOptionID;
                    }
                    if ($scope.method.checkIsConfirm(data.specialRequirementCostOptionID)) {
                        $scope.selectedOptionsTemp.specialRequirementCostOptionID = $scope.selectedOptions.specialRequirementCostOptionID;
                    }
                    if ($scope.method.checkIsConfirm(data.managementCostOptionID)) {
                        $scope.selectedOptionsTemp.managementCostOptionID = $scope.selectedOptions.managementCostOptionID;
                    }
                    dataService.approveAllOption(
                        $scope.selectedOptionsTemp,
                        function (data) {
                            $scope.event.init();
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                            $scope.frmNewMaterialObj.data = null;
                            $scope.frmNewMaterialObj.supportData = null;
                        }
                    );
                }
            },
            unApproveAllOption: function (data) {
                if (confirm('Reset confirm status and unlock all option for editing?')) {
                    if (!$scope.method.checkIsConfirm(data.materialCostOptionID)) {
                        $scope.selectedOptionsTemp.materialCostOptionID = $scope.selectedOptions.materialCostOptionID;
                    }
                    if (!$scope.method.checkIsConfirm(data.frameCostOptionID)) {
                        $scope.selectedOptionsTemp.frameCostOptionID = $scope.selectedOptions.frameCostOptionID;
                    }
                    if (!$scope.method.checkIsConfirm(data.subMaterialCostOptionID)) {
                        $scope.selectedOptionsTemp.subMaterialCostOptionID = $scope.selectedOptions.subMaterialCostOptionID;
                    }
                    if (!$scope.method.checkIsConfirm(data.cushionCostOptionID)) {
                        $scope.selectedOptionsTemp.cushionCostOptionID = $scope.selectedOptions.cushionCostOptionID;
                    }
                    if (!$scope.method.checkIsConfirm(data.packingCostOptionID)) {
                        $scope.selectedOptionsTemp.packingCostOptionID = $scope.selectedOptions.packingCostOptionID;
                    }
                    if (!$scope.method.checkIsConfirm(data.hardwareCostOptionID)) {
                        $scope.selectedOptionsTemp.hardwareCostOptionID = $scope.selectedOptions.hardwareCostOptionID;
                    }
                    if (!$scope.method.checkIsConfirm(data.otherMaterialCostOptionID)) {
                        $scope.selectedOptionsTemp.otherMaterialCostOptionID = $scope.selectedOptions.otherMaterialCostOptionID;
                    }
                    if (!$scope.method.checkIsConfirm(data.fscCostOptionID)) {
                        $scope.selectedOptionsTemp.fscCostOptionID = $scope.selectedOptions.fscCostOptionID;
                    }
                    if (!$scope.method.checkIsConfirm(data.specialRequirementCostOptionID)) {
                        $scope.selectedOptionsTemp.specialRequirementCostOptionID = $scope.selectedOptions.specialRequirementCostOptionID;
                    }
                    if (!$scope.method.checkIsConfirm(data.managementCostOptionID)) {
                        $scope.selectedOptionsTemp.managementCostOptionID = $scope.selectedOptions.managementCostOptionID;
                    }
                    dataService.unApproveAllOption(
                         $scope.selectedOptionsTemp,
                        function (data) {
                            $scope.event.init();
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                            $scope.frmNewMaterialObj.data = null;
                            $scope.frmNewMaterialObj.supportData = null;
                        }
                    );
                }
            },
            viewOption: function (id) {
                var selectedCategory;
                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                    if (item.breakdownCategoryOptionID === id) {
                        selectedCategory = item;
                    }
                });

                $scope.frmDetailViewObj.data = JSON.parse(JSON.stringify(selectedCategory));
                $scope.frmDetailViewObj.event.open();
            },
            viewOtherCompanyOption: function (id) {
                var otherCompanyOptionId;
                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                    if (item.breakdownCategoryOptionID === id) {
                        otherCompanyOptionId = $scope.method.getOtherCompanyOption(item);
                    }
                });
                $scope.event.viewOption(otherCompanyOptionId);
            },
            openPopupSeasonSpec: function () {
                $scope.seasonSpecObj.rate = $scope.data.seasonSpecPercent;
                $('#popupSeasonSpec').modal();
            },
            openPopupArticleCode: function () {

                $('#popupArticleCode').modal();
            },
            saveSeasonSpec: function () {
                dataService.updateSeasonSpecPercent(
                    $scope.data.breakdownID,
                    $scope.seasonSpecObj.rate,
                    function (data) {
                        if (data.message.type === 0) {
                            $scope.data.seasonSpecPercent = $scope.seasonSpecObj.rate;
                            $scope.data.updatorName = 'yourself';
                            $scope.data.updatedBy = context.userId;
                            $scope.data.updatedDate = 'Just now';
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                        $scope.frmNewMaterialObj.data = null;
                        $scope.frmNewMaterialObj.supportData = null;
                    }
                );
                $('#popupSeasonSpec').modal('hide');
            },
            exportExcel: function () {
                dataService.exportExcel(
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
                            shippedFromFactoryID: $scope.selectedOptions.shippedFromFactoryID,
                            breakdownID: $scope.data.breakdownID,
                            companyID: context.companyId
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
            },
            selectArticleCode: function (item) {
                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                    if (option.companyID == context.companyId) {
                        if (item.materialID == option.materialID && item.materialTypeID == option.materialTypeID && item.materialColorID == option.materialColorID && option.breakdownCategoryID == 1) {
                            $scope.selectedOptions.materialCostOptionID = option.breakdownCategoryOptionID;
                        }
                        if (item.frameMaterialID == option.frameMaterialID && item.frameMaterialColorID == option.frameMaterialColorID && option.breakdownCategoryID == 2) {
                            $scope.selectedOptions.frameCostOptionID = option.breakdownCategoryOptionID;
                        }
                        if (item.subMaterialID == option.subMaterialID && item.subMaterialColorID == option.subMaterialColorID && option.breakdownCategoryID == 3) {
                            $scope.selectedOptions.subMaterialCostOptionID = option.breakdownCategoryOptionID;
                        }
                        if (item.cushionTypeID == option.cushionTypeID && item.backCushionID == option.backCushionID && item.seatCushionID == option.seatCushionID && item.cushionColorID == option.cushionColorID && option.breakdownCategoryID == 4) {
                            $scope.selectedOptions.cushionCostOptionID = option.breakdownCategoryOptionID;
                        }
                        if (item.fscTypeID == option.fscTypeID && item.fscPercentID == option.fscPercentID && option.breakdownCategoryID == 8) {
                            $scope.selectedOptions.fscCostOptionID = option.breakdownCategoryOptionID;
                        }
                        if (item.packagingMethodID == $scope.data.packagingMethodID && item.clientSpecialPackagingMethodID == $scope.data.clientSpecialPackagingMethodID && option.breakdownCategoryID == 5) {
                            $scope.selectedOptions.packingCostOptionID = option.breakdownCategoryOptionID;
                        }
                    }
                });
                $scope.currentArticleCode = item.articleCode;
                $('#popupArticleCode').modal('hide');
            }
        };

        //
        // method
        //
        $scope.method = {
            refresh: function (id) {
                jsHelper.loadingSwitch(true);
                window.location = context.refreshUrl + id;
            },
            getTotalAmountSumPriceAVF: function () {

                var result = parseFloat(0);
                if ($scope.frmEditPriceObj.data !== null) {
                    angular.forEach($scope.frmEditPriceObj.data.breakDownBreakdownCategoryDTOs, function (itemBreakDownBreakdownCategoryDTO) {
                        angular.forEach(itemBreakDownBreakdownCategoryDTO.breakDownPriceDefaultDTOs, function (itemBreakDownPriceDefaultDTO) {
                            result += (itemBreakDownPriceDefaultDTO.quantityAVF * itemBreakDownPriceDefaultDTO.unitPriceAVF)
                        })
                    });
                }
                return result;
            },
            getTotalAmountSumPrice: function () {
                
                var result = parseFloat(0);
                if ($scope.frmEditPriceObj.data !== null) {
                    angular.forEach($scope.frmEditPriceObj.data.breakDownBreakdownCategoryDTOs, function (itemBreakDownBreakdownCategoryDTO) {
                        angular.forEach(itemBreakDownBreakdownCategoryDTO.breakDownPriceDefaultDTOs, function (itemBreakDownPriceDefaultDTO) {
                            result += (itemBreakDownPriceDefaultDTO.quantity * itemBreakDownPriceDefaultDTO.unitPrice)
                        })
                    });
                }               
                return result;
            },
            getTotalAmountSumPrice1: function () {

                var result = parseFloat(0);
                if ($scope.frmEditPriceObj.data !== null) {
                    angular.forEach($scope.frmEditPriceObj.data.breakDownBreakdownCategoryDTOs, function (itemBreakDownBreakdownCategoryDTO) {
                        angular.forEach(itemBreakDownBreakdownCategoryDTO.breakDownPriceDefaultDTOs, function (itemBreakDownPriceDefaultDTO) {
                            result += (itemBreakDownPriceDefaultDTO.quantity1 * itemBreakDownPriceDefaultDTO.unitPrice1)
                        })
                    });
                }
                return result;
            },
            getTotalAmountSumPrice2: function () {

                var result = parseFloat(0);
                if ($scope.frmEditPriceObj.data !== null) {
                    angular.forEach($scope.frmEditPriceObj.data.breakDownBreakdownCategoryDTOs, function (itemBreakDownBreakdownCategoryDTO) {
                        angular.forEach(itemBreakDownBreakdownCategoryDTO.breakDownPriceDefaultDTOs, function (itemBreakDownPriceDefaultDTO) {
                            result += (itemBreakDownPriceDefaultDTO.quantity2 * itemBreakDownPriceDefaultDTO.unitPrice2)
                        })
                    });
                }
                return result;
            },
            getTotalAmountSumPrice3: function () {

                var result = parseFloat(0);
                if ($scope.frmEditPriceObj.data !== null) {
                    angular.forEach($scope.frmEditPriceObj.data.breakDownBreakdownCategoryDTOs, function (itemBreakDownBreakdownCategoryDTO) {
                        angular.forEach(itemBreakDownBreakdownCategoryDTO.breakDownPriceDefaultDTOs, function (itemBreakDownPriceDefaultDTO) {
                            result += (itemBreakDownPriceDefaultDTO.quantity3 * itemBreakDownPriceDefaultDTO.unitPrice3)
                        })
                    });
                }
                return result;
            },
            getTotalAmountPrice: function (item) {
                var result = parseFloat(0);
                angular.forEach(item.breakDownPriceDefaultDTOs, function (itembreakDownPriceDefaultDTOs) {
                    result += (itembreakDownPriceDefaultDTOs.quantity * itembreakDownPriceDefaultDTOs.unitPrice)
                });               
                return result;
            },
            getTotalAmountPrice1: function (item) {
                var result = parseFloat(0);
                angular.forEach(item.breakDownPriceDefaultDTOs, function (itembreakDownPriceDefaultDTOs) {
                    result += (itembreakDownPriceDefaultDTOs.quantity1 * itembreakDownPriceDefaultDTOs.unitPrice1)
                });
                return result;
            },
            getTotalAmountPrice2: function (item) {
                var result = parseFloat(0);
                angular.forEach(item.breakDownPriceDefaultDTOs, function (itembreakDownPriceDefaultDTOs) {
                    result += (itembreakDownPriceDefaultDTOs.quantity2 * itembreakDownPriceDefaultDTOs.unitPrice2)
                });
                return result;
            },
            getTotalAmountPrice3: function (item) {
                var result = parseFloat(0);
                angular.forEach(item.breakDownPriceDefaultDTOs, function (itembreakDownPriceDefaultDTOs) {
                    result += (itembreakDownPriceDefaultDTOs.quantity3 * itembreakDownPriceDefaultDTOs.unitPrice3)
                });
                return result;
            },
            getOptionName: function (item) {
                var result = '';
                if (!$scope.data || !item) return '';
                if ($scope.data.sampleProductID) return '';

                switch (item.breakdownCategoryID) {
                    case 1:
                        result = item.materialNM + ' ' + item.materialTypeNM + ' ' + item.materialColorNM + (item.isConfirmed ? ' (APPROVED)' : '');
                        break;

                    case 2:
                        result = item.frameMaterialNM + ' ' + item.frameMaterialColorNM + (item.isConfirmed ? ' (APPROVED)' : '');
                        break;

                    case 3:
                        result = item.subMaterialNM + ' ' + item.subMaterialColorNM + (item.isConfirmed ? ' (APPROVED)' : '');
                        break;

                    case 4:
                        result = item.cushionTypeNM + ' ' + item.cushionColorNM + ' (BACK CUSHION: ' + item.backCushionNM + ' + SEAT CUSHION: ' + item.seatCushionNM + ')' + (item.isConfirmed ? ' (APPROVED)' : '');
                        break;

                    case 5:
                        if (item.packagingMethodID !== 11) {
                            if (item.packagingMethodID === -1) {
                                result = 'TBA';
                            }
                            else {
                                result = item.packagingMethodNM + (item.isConfirmed ? ' (APPROVED)' : '');
                            }
                        }
                        else {
                            result = item.packagingMethodNM + ': ' + item.clientSpecialPackagingMethodNM + (item.isConfirmed ? ' (APPROVED)' : '');
                        }
                        break;

                    case 8:
                        result = item.fscTypeNM + (item.isConfirmed ? ' (APPROVED)' : '');
                        break;

                    case 9:
                        result = item.description + (item.isConfirmed ? ' (APPROVED)' : '');
                        break;
                }

                return result;
            },
            getGroupObj: function (id) {
                var result = null;
                if ($scope.data) {
                    angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                        angular.forEach(option.breakdownCategoryOptionGroupDTOs, function (item) {
                            if (item.breakdownCategoryOptionGroupID === id) {
                                result = item;
                            }
                        });
                    });
                }
                return result;
            },

            getSubTotalAmount: function (optionId) {
                var result = parseFloat(0);
                if ($scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    var categoryId = 0;
                    var newOptionId = optionId;
                    var groupQnt = 1;
                    var groupObj = null;
                    angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                        if (option.breakdownCategoryOptionID === optionId) {
                            categoryId = option.breakdownCategoryID;
                            if (option.companyID !== 1) {
                                newOptionId = $scope.method.getOtherCompanyOption(option);
                            }
                        }
                    });
                    optionId = newOptionId;

                    if (categoryId !== 10) {
                        angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                            if (option.breakdownCategoryOptionID === optionId && option.companyID === 1) {
                                angular.forEach(option.breakdownCategoryOptionDetailDTOs, function (item) {
                                    groupObj = null;
                                    groupQnt = 1;
                                    if (item.breakdownCategoryOptionGroupID !== null) {
                                        groupObj = $scope.method.getGroupObj(item.breakdownCategoryOptionGroupID);
                                        if (groupObj) {
                                            groupQnt = parseInt(groupObj.quantity);
                                        }
                                    }
                                    if (!groupQnt) {
                                        groupQnt = 1;
                                    }

                                    if (item.totalAmount) {
                                        result += groupQnt * parseFloat(item.totalAmount);
                                    }
                                    else {
                                        if (item.avfPrice && item.quantity) {
                                            if (item.wastedRatePercent) {
                                                result += groupQnt * parseFloat(item.avfPrice) * parseFloat(item.quantity) * (1 + jsHelper.round(parseFloat(item.wastedRatePercent) / 100, 2));
                                            }
                                            else {
                                                result += groupQnt * parseFloat(item.avfPrice) * parseFloat(item.quantity);
                                            }
                                        }
                                    }
                                });
                            }
                        });
                    }
                    else {
                        var materialCost = $scope.method.getSubTotalAmount($scope.selectedOptions.materialCostOptionID);
                        var frameCost = $scope.method.getSubTotalAmount($scope.selectedOptions.frameCostOptionID);
                        var subMaterialCost = $scope.method.getSubTotalAmount($scope.selectedOptions.subMaterialCostOptionID);
                        var cushionCost = $scope.method.getSubTotalAmount($scope.selectedOptions.cushionCostOptionID);
                        var packingCost = $scope.method.getSubTotalAmount($scope.selectedOptions.packingCostOptionID);
                        var hardwareCost = $scope.method.getSubTotalAmount($scope.selectedOptions.hardwareCostOptionID);
                        var otherMaterialCost = $scope.method.getSubTotalAmount($scope.selectedOptions.otherMaterialCostOptionID);
                        var fscCost = $scope.method.getSubTotalAmount($scope.selectedOptions.fscCostOptionID);
                        var specialRequirementCost = $scope.method.getSubTotalAmount($scope.selectedOptions.specialRequirementCostOptionID);
                        var total = materialCost + frameCost + subMaterialCost + cushionCost + packingCost + hardwareCost + otherMaterialCost + fscCost + specialRequirementCost;
                        var totalPercent = parseFloat(0);
                        angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                            if (option.breakdownCategoryOptionID === optionId && option.companyID === 1) {
                                angular.forEach(option.breakdownCategoryOptionDetailDTOs, function (item) {
                                    if (item.quantityPercent) {
                                        totalPercent += parseFloat(item.quantityPercent);
                                    }
                                });
                            }
                        });
                        result = total * totalPercent / 100;
                    }
                }
                return result;
            },
            getSubTotalAmountUSD: function (optionId) {
                var result = parseFloat(0);
                if ($scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    var categoryId = 0;
                    var newOptionId = optionId;
                    var groupQnt = 1;
                    var groupObj = null;
                    angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                        if (option.breakdownCategoryOptionID === optionId) {
                            categoryId = option.breakdownCategoryID;
                            if (option.companyID !== 1) {
                                newOptionId = $scope.method.getOtherCompanyOption(option);
                            }
                        }
                    });
                    optionId = newOptionId;

                    if (categoryId !== 10) {
                        angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                            if (option.breakdownCategoryOptionID === optionId && option.companyID === 1) {
                                angular.forEach(option.breakdownCategoryOptionDetailDTOs, function (item) {
                                    groupObj = null;
                                    groupQnt = 1;
                                    if (item.breakdownCategoryOptionGroupID !== null) {
                                        groupObj = $scope.method.getGroupObj(item.breakdownCategoryOptionGroupID);
                                        if (groupObj) {
                                            groupQnt = parseInt(groupObj.quantity);
                                        }
                                    }
                                    if (!groupQnt) {
                                        groupQnt = 1;
                                    }

                                    if (item.totalAmount) {
                                        result += groupQnt * jsHelper.round(parseFloat(item.totalAmount) / $scope.exRate, 2);
                                    }
                                    else {
                                        if (item.avfPrice && item.quantity) {
                                            if (item.wastedRatePercent) {
                                                result += groupQnt * jsHelper.round(parseFloat(item.avfPrice) * parseFloat(item.quantity) * (1 + jsHelper.round(parseFloat(item.wastedRatePercent) / 100, 2)) / $scope.exRate, 2);
                                            }
                                            else {
                                                result += groupQnt * jsHelper.round(parseFloat(item.avfPrice) * parseFloat(item.quantity) / $scope.exRate, 2);
                                            }
                                        }
                                    }
                                });
                            }
                        });
                    }
                    else {
                        var materialCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.materialCostOptionID);
                        var frameCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.frameCostOptionID);
                        var subMaterialCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.subMaterialCostOptionID);
                        var cushionCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.cushionCostOptionID);
                        var packingCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.packingCostOptionID);
                        var hardwareCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.hardwareCostOptionID);
                        var otherMaterialCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.otherMaterialCostOptionID);
                        var fscCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.fscCostOptionID);
                        var specialRequirementCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.specialRequirementCostOptionID);
                        var total = materialCost + frameCost + subMaterialCost + cushionCost + packingCost + hardwareCost + otherMaterialCost + fscCost + specialRequirementCost;
                        var totalPercent = parseFloat(0);
                        angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                            if (option.breakdownCategoryOptionID === optionId && option.companyID === 1) {
                                angular.forEach(option.breakdownCategoryOptionDetailDTOs, function (item) {
                                    totalPercent += parseFloat(item.quantityPercent);
                                });
                            }
                        });
                        result = total * totalPercent / 100;
                    }
                }
                return result;
            },
            getTotalAmount: function () {
                var result = parseFloat(0);
                if ($scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    var materialCost = $scope.method.getSubTotalAmount($scope.selectedOptions.materialCostOptionID);
                    var frameCost = $scope.method.getSubTotalAmount($scope.selectedOptions.frameCostOptionID);
                    var subMaterialCost = $scope.method.getSubTotalAmount($scope.selectedOptions.subMaterialCostOptionID);
                    var cushionCost = $scope.method.getSubTotalAmount($scope.selectedOptions.cushionCostOptionID);
                    var packingCost = $scope.method.getSubTotalAmount($scope.selectedOptions.packingCostOptionID);
                    var hardwareCost = $scope.method.getSubTotalAmount($scope.selectedOptions.hardwareCostOptionID);
                    var otherMaterialCost = $scope.method.getSubTotalAmount($scope.selectedOptions.otherMaterialCostOptionID);
                    var fscCost = $scope.method.getSubTotalAmount($scope.selectedOptions.fscCostOptionID);
                    var specialRequirementCost = $scope.method.getSubTotalAmount($scope.selectedOptions.specialRequirementCostOptionID);
                    var managementCost = $scope.method.getSubTotalAmount($scope.selectedOptions.managementCostOptionID);

                    return materialCost + frameCost + subMaterialCost + cushionCost + packingCost + hardwareCost + otherMaterialCost + fscCost + specialRequirementCost + managementCost;
                }
                return result;
            },
            getTotalAmountUSD: function () {
                var result = parseFloat(0);
                if ($scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    var materialCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.materialCostOptionID);
                    var frameCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.frameCostOptionID);
                    var subMaterialCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.subMaterialCostOptionID);
                    var cushionCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.cushionCostOptionID);
                    var packingCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.packingCostOptionID);
                    var hardwareCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.hardwareCostOptionID);
                    var otherMaterialCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.otherMaterialCostOptionID);
                    var fscCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.fscCostOptionID);
                    var specialRequirementCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.specialRequirementCostOptionID);
                    var managementCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.managementCostOptionID);

                    return materialCost + frameCost + subMaterialCost + cushionCost + packingCost + hardwareCost + otherMaterialCost + fscCost + specialRequirementCost + managementCost;
                }
                return result;
            },

            getOtherCompanyOption: function (option) {
                var result = null;
                var otherCompanyID = option.companyID === 1 ? 3 : 1;
                if ($scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    if (option.companyID !== otherCompanyID) {
                        angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (otherOption) {
                            if (otherOption.companyID === otherCompanyID
                                && otherOption.breakdownCategoryID === option.breakdownCategoryID
                                && otherOption.materialID === option.materialID
                                && otherOption.materialTypeID === option.materialTypeID
                                && otherOption.materialColorID === option.materialColorID
                                && otherOption.frameMaterialTypeID === option.frameMaterialTypeID
                                && otherOption.frameMaterialColorID === option.frameMaterialColorID
                                && otherOption.subMaterialID === option.subMaterialID
                                && otherOption.subMaterialColorID === option.subMaterialColorID
                                && otherOption.cushionTypeID === option.cushionTypeID
                                && otherOption.backCushionID === option.backCushionID
                                && otherOption.seatCushionID === option.seatCushionID
                                && otherOption.cushionColorID === option.cushionColorID
                                && otherOption.packagingMethodID === option.packagingMethodID
                                && otherOption.fscTypeID === option.fscTypeID
                                && otherOption.fscPercentID === option.fscPercentID
                                && otherOption.description === option.description
                                && otherOption.clientSpecialPackagingMethodID === option.clientSpecialPackagingMethodID) {
                                result = otherOption.breakdownCategoryOptionID;
                            }
                        });
                    }
                }
                return result;
            },

            getSubTotalAmountAVT: function (optionId) {
                var result = parseFloat(0);
                if ($scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    var categoryId = 0;
                    var newOptionId = optionId;
                    var groupQnt = 1;
                    var groupObj = null;
                    angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                        if (option.breakdownCategoryOptionID === optionId) {
                            categoryId = option.breakdownCategoryID;
                            if (option.companyID !== 3) {
                                newOptionId = $scope.method.getOtherCompanyOption(option);
                            }
                        }
                    });
                    optionId = newOptionId;

                    if (categoryId !== 10) {
                        angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                            if (option.breakdownCategoryOptionID === optionId && option.companyID === 3) {
                                angular.forEach(option.breakdownCategoryOptionDetailDTOs, function (item) {
                                    groupObj = null;
                                    groupQnt = 1;
                                    if (item.breakdownCategoryOptionGroupID !== null) {
                                        groupObj = $scope.method.getGroupObj(item.breakdownCategoryOptionGroupID);
                                        if (groupObj) {
                                            groupQnt = parseInt(groupObj.quantity);
                                        }
                                    }
                                    if (!groupQnt) {
                                        groupQnt = 1;
                                    }

                                    if (item.totalAmount) {
                                        result += groupQnt * parseFloat(item.totalAmount);
                                    }
                                    else {
                                        if (item.avtPrice && item.quantity) {
                                            if (item.wastedRatePercent) {
                                                result += groupQnt * parseFloat(item.avtPrice) * parseFloat(item.quantity) * (1 + jsHelper.round(parseFloat(item.wastedRatePercent) / 100, 2));
                                            }
                                            else {
                                                result += groupQnt * parseFloat(item.avtPrice) * parseFloat(item.quantity);
                                            }
                                        }
                                    }
                                });
                            }
                        });
                    }
                    else {
                        var materialCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.materialCostOptionID);
                        var frameCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.frameCostOptionID);
                        var subMaterialCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.subMaterialCostOptionID);
                        var cushionCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.cushionCostOptionID);
                        var packingCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.packingCostOptionID);
                        var hardwareCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.hardwareCostOptionID);
                        var otherMaterialCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.otherMaterialCostOptionID);
                        var fscCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.fscCostOptionID);
                        var specialRequirementCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.specialRequirementCostOptionID);
                        var total = materialCost + frameCost + subMaterialCost + cushionCost + packingCost + hardwareCost + otherMaterialCost + fscCost + specialRequirementCost;
                        var totalPercent = parseFloat(0);
                        angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                            if (option.breakdownCategoryOptionID === optionId && option.companyID === 3) {
                                angular.forEach(option.breakdownCategoryOptionDetailDTOs, function (item) {
                                    totalPercent += parseFloat(item.quantityPercent);
                                });
                            }
                        });
                        result = total * totalPercent / 100;
                    }
                }
                return result;
            },
            getSubTotalAmountUSDAVT: function (optionId) {
                var result = parseFloat(0);
                if ($scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    var categoryId = 0;
                    var newOptionId = optionId;
                    var groupQnt = 1;
                    var groupObj = null;
                    angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                        if (option.breakdownCategoryOptionID === optionId) {
                            categoryId = option.breakdownCategoryID;
                            if (option.companyID !== 3) {
                                newOptionId = $scope.method.getOtherCompanyOption(option);
                            }
                        }
                    });
                    optionId = newOptionId;

                    if (categoryId !== 10) {
                        angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                            if (option.breakdownCategoryOptionID === optionId && option.companyID === 3) {
                                angular.forEach(option.breakdownCategoryOptionDetailDTOs, function (item) {
                                    groupObj = null;
                                    groupQnt = 1;
                                    if (item.breakdownCategoryOptionGroupID !== null) {
                                        groupObj = $scope.method.getGroupObj(item.breakdownCategoryOptionGroupID);
                                        if (groupObj) {
                                            groupQnt = parseInt(groupObj.quantity);
                                        }
                                    }
                                    if (!groupQnt) {
                                        groupQnt = 1;
                                    }

                                    if (item.totalAmount) {
                                        result += groupQnt * jsHelper.round(parseFloat(item.totalAmount) / $scope.exRate, 2);
                                    }
                                    else {
                                        if (item.avtPrice && item.quantity) {
                                            if (item.wastedRatePercent) {
                                                result += groupQnt * jsHelper.round(parseFloat(item.avtPrice) * parseFloat(item.quantity) * (1 + jsHelper.round(parseFloat(item.wastedRatePercent) / 100, 2)) / $scope.exRate, 2);
                                            }
                                            else {
                                                result += groupQnt * jsHelper.round(parseFloat(item.avtPrice) * parseFloat(item.quantity) / $scope.exRate, 2);
                                            }
                                        }
                                    }
                                });
                            }
                        });
                    }
                    else {
                        var materialCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.materialCostOptionID);
                        var frameCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.frameCostOptionID);
                        var subMaterialCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.subMaterialCostOptionID);
                        var cushionCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.cushionCostOptionID);
                        var packingCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.packingCostOptionID);
                        var hardwareCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.hardwareCostOptionID);
                        var otherMaterialCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.otherMaterialCostOptionID);
                        var fscCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.fscCostOptionID);
                        var specialRequirementCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.specialRequirementCostOptionID);
                        var total = materialCost + frameCost + subMaterialCost + cushionCost + packingCost + hardwareCost + otherMaterialCost + fscCost + specialRequirementCost;
                        var totalPercent = parseFloat(0);
                        angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                            if (option.breakdownCategoryOptionID === optionId && option.companyID === 3) {
                                angular.forEach(option.breakdownCategoryOptionDetailDTOs, function (item) {
                                    totalPercent += parseFloat(item.quantityPercent);
                                });
                            }
                        });
                        result = total * totalPercent / 100;
                    }
                }
                return result;
            },
            getTotalAmountAVT: function () {
                var result = parseFloat(0);
                if ($scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    var materialCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.materialCostOptionID);
                    var frameCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.frameCostOptionID);
                    var subMaterialCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.subMaterialCostOptionID);
                    var cushionCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.cushionCostOptionID);
                    var packingCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.packingCostOptionID);
                    var hardwareCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.hardwareCostOptionID);
                    var otherMaterialCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.otherMaterialCostOptionID);
                    var fscCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.fscCostOptionID);
                    var specialRequirementCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.specialRequirementCostOptionID);
                    var managementCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.managementCostOptionID);

                    return materialCost + frameCost + subMaterialCost + cushionCost + packingCost + hardwareCost + otherMaterialCost + fscCost + specialRequirementCost + managementCost;
                }
                return result;
            },
            getTotalAmountUSDAVT: function () {
                var result = parseFloat(0);
                if ($scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    var materialCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.materialCostOptionID);
                    var frameCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.frameCostOptionID);
                    var subMaterialCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.subMaterialCostOptionID);
                    var cushionCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.cushionCostOptionID);
                    var packingCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.packingCostOptionID);
                    var hardwareCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.hardwareCostOptionID);
                    var otherMaterialCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.otherMaterialCostOptionID);
                    var fscCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.fscCostOptionID);
                    var specialRequirementCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.specialRequirementCostOptionID);
                    var managementCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.managementCostOptionID);

                    return materialCost + frameCost + subMaterialCost + cushionCost + packingCost + hardwareCost + otherMaterialCost + fscCost + specialRequirementCost + managementCost;
                }
                return result;
            },

            getTotalExcludeManagementFee: function () {
                var result = parseFloat(0);
                if ($scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    var materialCost = $scope.method.getSubTotalAmount($scope.selectedOptions.materialCostOptionID);
                    var frameCost = $scope.method.getSubTotalAmount($scope.selectedOptions.frameCostOptionID);
                    var subMaterialCost = $scope.method.getSubTotalAmount($scope.selectedOptions.subMaterialCostOptionID);
                    var cushionCost = $scope.method.getSubTotalAmount($scope.selectedOptions.cushionCostOptionID);
                    var packingCost = $scope.method.getSubTotalAmount($scope.selectedOptions.packingCostOptionID);
                    var hardwareCost = $scope.method.getSubTotalAmount($scope.selectedOptions.hardwareCostOptionID);
                    var otherMaterialCost = $scope.method.getSubTotalAmount($scope.selectedOptions.otherMaterialCostOptionID);
                    var fscCost = $scope.method.getSubTotalAmount($scope.selectedOptions.fscCostOptionID);
                    var specialRequirementCost = $scope.method.getSubTotalAmount($scope.selectedOptions.specialRequirementCostOptionID);
                    return materialCost + frameCost + subMaterialCost + cushionCost + packingCost + hardwareCost + otherMaterialCost + fscCost + specialRequirementCost;
                }
                return result;
            },
            getTotalExcludeManagementFeeUSD: function () {
                var result = parseFloat(0);
                if ($scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    var materialCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.materialCostOptionID);
                    var frameCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.frameCostOptionID);
                    var subMaterialCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.subMaterialCostOptionID);
                    var cushionCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.cushionCostOptionID);
                    var packingCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.packingCostOptionID);
                    var hardwareCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.hardwareCostOptionID);
                    var otherMaterialCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.otherMaterialCostOptionID);
                    var fscCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.fscCostOptionID);
                    var specialRequirementCost = $scope.method.getSubTotalAmountUSD($scope.selectedOptions.specialRequirementCostOptionID);
                    return materialCost + frameCost + subMaterialCost + cushionCost + packingCost + hardwareCost + otherMaterialCost + fscCost + specialRequirementCost;
                }
                return result;
            },
            getTotalExcludeManagementFeeAVT: function () {
                var result = parseFloat(0);
                if ($scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    var materialCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.materialCostOptionID);
                    var frameCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.frameCostOptionID);
                    var subMaterialCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.subMaterialCostOptionID);
                    var cushionCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.cushionCostOptionID);
                    var packingCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.packingCostOptionID);
                    var hardwareCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.hardwareCostOptionID);
                    var otherMaterialCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.otherMaterialCostOptionID);
                    var fscCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.fscCostOptionID);
                    var specialRequirementCost = $scope.method.getSubTotalAmountAVT($scope.selectedOptions.specialRequirementCostOptionID);
                    return materialCost + frameCost + subMaterialCost + cushionCost + packingCost + hardwareCost + otherMaterialCost + fscCost + specialRequirementCost;
                }
                return result;
            },
            getTotalExcludeManagementFeeUSDAVT: function () {
                var result = parseFloat(0);
                if ($scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    var materialCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.materialCostOptionID);
                    var frameCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.frameCostOptionID);
                    var subMaterialCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.subMaterialCostOptionID);
                    var cushionCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.cushionCostOptionID);
                    var packingCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.packingCostOptionID);
                    var hardwareCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.hardwareCostOptionID);
                    var otherMaterialCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.otherMaterialCostOptionID);
                    var fscCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.fscCostOptionID);
                    var specialRequirementCost = $scope.method.getSubTotalAmountUSDAVT($scope.selectedOptions.specialRequirementCostOptionID);
                    return materialCost + frameCost + subMaterialCost + cushionCost + packingCost + hardwareCost + otherMaterialCost + fscCost + specialRequirementCost
                }
                return result;
            },

            getShippingFee: function () {
                var result = parseFloat(0);
                if ($scope.factories && $scope.selectedOptions.packingCostOptionID && $scope.selectedOptions.shippedFromFactoryID && $scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    var selectedFactory = null;
                    var loadAbility = $scope.method.getLoadAbility();
                    angular.forEach($scope.factories, function (item) {
                        if (item.factoryID === $scope.selectedOptions.shippedFromFactoryID) {
                            selectedFactory = item;
                        }
                    });
                    if (selectedFactory && selectedFactory.exportCost40HC && loadAbility) {
                        result = jsHelper.round(parseFloat(selectedFactory.exportCost40HC) / parseFloat(loadAbility), 2);
                    }
                }
                return result;
            },
            getShippingFeeUSD: function () {
                if ($scope.data) {
                    return jsHelper.round($scope.method.getShippingFee() / $scope.exRate, 2);
                }
                return 0;
            },
            getShippingFeeAVT: function () {
                var result = parseFloat(0);
                if ($scope.factories && $scope.selectedOptions.packingCostOptionID && $scope.selectedOptions.shippedFromFactoryID && $scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    var selectedFactory = null;
                    var loadAbility = $scope.method.getLoadAbilityAVT();
                    angular.forEach($scope.factories, function (item) {
                        if (item.factoryID === $scope.selectedOptions.shippedFromFactoryID) {
                            selectedFactory = item;
                        }
                    });
                    if (selectedFactory && selectedFactory.exportCost40HC && loadAbility) {
                        result = jsHelper.round(parseFloat(selectedFactory.exportCost40HC) / parseFloat(loadAbility), 2);
                    }
                }
                return result;
            },
            getShippingFeeUSDAVT: function () {
                if ($scope.data) {
                    return jsHelper.round($scope.method.getShippingFeeAVT() / $scope.exRate, 2);
                }
                return 0;
            },

            getLoadAbility: function () {
                var result = parseFloat(0);
                if ($scope.selectedOptions.packingCostOptionID && $scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    var optionId = $scope.selectedOptions.packingCostOptionID;
                    angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                        if (option.breakdownCategoryOptionID === $scope.selectedOptions.packingCostOptionID) {
                            if (option.companyID !== 1) {
                                optionId = $scope.method.getOtherCompanyOption(option);
                            }
                        }
                    });

                    var selectedPackingOption = null;
                    angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                        if (item.breakdownCategoryOptionID === optionId) {
                            selectedPackingOption = item;
                        }
                    });

                    if (selectedPackingOption && selectedPackingOption.loadAbility) {
                        result = parseFloat(selectedPackingOption.loadAbility);
                    }
                }
                return result;
            },
            getLoadAbilityAVT: function () {
                var result = parseFloat(0);
                if ($scope.selectedOptions.packingCostOptionID && $scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    var optionId = $scope.selectedOptions.packingCostOptionID;
                    angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                        if (option.breakdownCategoryOptionID === $scope.selectedOptions.packingCostOptionID) {
                            if (option.companyID !== 3) {
                                optionId = $scope.method.getOtherCompanyOption(option);
                            }
                        }
                    });

                    var selectedPackingOption = null;
                    angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                        if (item.breakdownCategoryOptionID === optionId) {
                            selectedPackingOption = item;
                        }
                    });

                    if (selectedPackingOption && selectedPackingOption.loadAbility) {
                        result = parseFloat(selectedPackingOption.loadAbility);
                    }
                }
                return result;
            },

            getBlankOptionData: function () {
                return {
                    breakdownCategoryOptionID: -1,
                    breakdownID: $scope.data.breakdownID,
                    breakdownCategoryID: null,
                    isDefaultOption: false,
                    materialID: null,
                    materialTypeID: null,
                    materialColorID: null,
                    frameMaterialID: null,
                    frameMaterialColorID: null,
                    subMaterialID: null,
                    subMaterialColorID: null,
                    cushionTypeID: null,
                    backCushionID: null,
                    seatCushionID: null,
                    cushionColorID: null,
                    fSCTypeID: null,
                    fSCPercentID: null,
                    packagingMethodID: null,
                    description: null,
                    updatedBy: null,
                    updatedDate: null,
                    isConfirmed: null,
                    confirmedBy: null,
                    confirmedDate: null,
                    materialNM: null,
                    materialTypeNM: null,
                    materialColorNM: null,
                    frameMaterialNM: null,
                    frameMaterialColorNM: null,
                    subMaterialNM: null,
                    subMaterialColorNM: null,
                    cushionTypeNM: null,
                    backCushionNM: null,
                    seatCushionNM: null,
                    cushionColorNM: null,
                    fSCTypeNM: null,
                    fSCPercentNM: null,
                    packagingMethodNM: null,
                    updatorName: null,
                    confirmerName: null
                };
            },
            getCategoryName: function (id) {
                var result = '';
                angular.forEach($scope.categories, function (item) {
                    if (item.breakdownCategoryID === id) {
                        result = item;
                    }
                });
                return result.breakdownCategoryNM;
            },
            getApproveStatus: function (id) {
                var result = false;
                if ($scope.data && $scope.data.breakdownCategoryOptionDTOs) {
                    angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                        if (item.breakdownCategoryOptionID === id) {
                            result = item.isConfirmed;
                        }
                    });
                }
                return result;
            },

            checkIsConfirm: function (id) {
                var isConfirmed = false;
                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                    if (item.breakdownCategoryOptionID === id && !item.isConfirmed) {
                        isConfirmed = true;
                    }
                });
                return isConfirmed;
            },

            checkConfirmAll: function () {
                //bind articleCode
                $scope.method.getArticleCode();               

                $scope.countConfirmed = 0;
                if (!$scope.method.checkIsConfirm($scope.selectedOptions.materialCostOptionID)) {
                    $scope.countConfirmed++;
                }
                if (!$scope.method.checkIsConfirm($scope.selectedOptions.frameCostOptionID)) {
                    $scope.countConfirmed++;
                }
                if (!$scope.method.checkIsConfirm($scope.selectedOptions.subMaterialCostOptionID)) {
                    $scope.countConfirmed++;
                }
                if (!$scope.method.checkIsConfirm($scope.selectedOptions.cushionCostOptionID)) {
                    $scope.countConfirmed++;
                }
                if (!$scope.method.checkIsConfirm($scope.selectedOptions.packingCostOptionID)) {
                    $scope.countConfirmed++;
                }
                if (!$scope.method.checkIsConfirm($scope.selectedOptions.hardwareCostOptionID)) {
                    $scope.countConfirmed++;
                }
                if (!$scope.method.checkIsConfirm($scope.selectedOptions.otherMaterialCostOptionID)) {
                    $scope.countConfirmed++;
                }
                if (!$scope.method.checkIsConfirm($scope.selectedOptions.fscCostOptionID)) {
                    $scope.countConfirmed++;
                }
                if (!$scope.method.checkIsConfirm($scope.selectedOptions.specialRequirementCostOptionID)) {
                    $scope.countConfirmed++;
                }
                if (!$scope.method.checkIsConfirm($scope.selectedOptions.managementCostOptionID)) {
                    $scope.countConfirmed++;
                }
            },

            checkConfirmAllOtherCompany: function () {
                $scope.countConfirmedOtherCompany = 0;
                var otherCaterialCostOptionID;
                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                    if (item.breakdownCategoryOptionID === $scope.selectedOptions.materialCostOptionID) {
                        otherCaterialCostOptionID = $scope.method.getOtherCompanyOption(item);
                    }
                });
                if (!$scope.method.checkIsConfirm(otherCaterialCostOptionID)) {
                    $scope.countConfirmedOtherCompany++;
                }
                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                    if (item.breakdownCategoryOptionID === $scope.selectedOptions.frameCostOptionID) {
                        otherCaterialCostOptionID = $scope.method.getOtherCompanyOption(item);
                    }
                });
                if (!$scope.method.checkIsConfirm(otherCaterialCostOptionID)) {
                    $scope.countConfirmedOtherCompany++;
                }
                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                    if (item.breakdownCategoryOptionID === $scope.selectedOptions.subMaterialCostOptionID) {
                        otherCaterialCostOptionID = $scope.method.getOtherCompanyOption(item);
                    }
                });
                if (!$scope.method.checkIsConfirm(otherCaterialCostOptionID)) {
                    $scope.countConfirmedOtherCompany++;
                }
                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                    if (item.breakdownCategoryOptionID === $scope.selectedOptions.cushionCostOptionID) {
                        otherCaterialCostOptionID = $scope.method.getOtherCompanyOption(item);
                    }
                });
                if (!$scope.method.checkIsConfirm(otherCaterialCostOptionID)) {
                    $scope.countConfirmedOtherCompany++;
                }
                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                    if (item.breakdownCategoryOptionID === $scope.selectedOptions.packingCostOptionID) {
                        otherCaterialCostOptionID = $scope.method.getOtherCompanyOption(item);
                    }
                });
                if (!$scope.method.checkIsConfirm(otherCaterialCostOptionID)) {
                    $scope.countConfirmedOtherCompany++;
                }
                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                    if (item.breakdownCategoryOptionID === $scope.selectedOptions.hardwareCostOptionID) {
                        otherCaterialCostOptionID = $scope.method.getOtherCompanyOption(item);
                    }
                });
                if (!$scope.method.checkIsConfirm(otherCaterialCostOptionID)) {
                    $scope.countConfirmedOtherCompany++;
                }
                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                    if (item.breakdownCategoryOptionID === $scope.selectedOptions.otherMaterialCostOptionID) {
                        otherCaterialCostOptionID = $scope.method.getOtherCompanyOption(item);
                    }
                });
                if (!$scope.method.checkIsConfirm(otherCaterialCostOptionID)) {
                    $scope.countConfirmedOtherCompany++;
                }
                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                    if (item.breakdownCategoryOptionID === $scope.selectedOptions.fscCostOptionID) {
                        otherCaterialCostOptionID = $scope.method.getOtherCompanyOption(item);
                    }
                });
                if (!$scope.method.checkIsConfirm(otherCaterialCostOptionID)) {
                    $scope.countConfirmedOtherCompany++;
                }
                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                    if (item.breakdownCategoryOptionID === $scope.selectedOptions.specialRequirementCostOptionID) {
                        otherCaterialCostOptionID = $scope.method.getOtherCompanyOption(item);
                    }
                });
                if (!$scope.method.checkIsConfirm(otherCaterialCostOptionID)) {
                    $scope.countConfirmedOtherCompany++;
                }
                angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                    if (item.breakdownCategoryOptionID === $scope.selectedOptions.managementCostOptionID) {
                        otherCaterialCostOptionID = $scope.method.getOtherCompanyOption(item);
                    }
                });
                if (!$scope.method.checkIsConfirm(otherCaterialCostOptionID)) {
                    $scope.countConfirmedOtherCompany++;
                }
            },
            getArticleCode: function () {
                dataService.getArticleCode(
                    $scope.selectedOptions,
                    context.modelId,
                    context.offerSeasonDetailId,
                    function (data) {
                        if (data.message.type === 0) {

                            $scope.currentArticleCode = data.data
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                        $scope.frmEditPriceObj.data = null;
                    }
                );
            }
        };

        //
        // new material option form
        //
        $scope.frmNewMaterialObj = {
            data: null,
            supportData: null,
            supportMaterialOption: [],
            supportMaterialColorOption: [],
            supportMaterialTypeOption: [],
            materialWizard: {
                materialText: '',
                materialImage: '',
                selectedMaterialID: -1,
                selectedMaterialTypeID: -1,
                materialStandard: true,
                materialSelect: function () {
                    $scope.frmNewMaterialObj.data.materialID = parseInt($scope.frmNewMaterialObj.data.materialID);
                    $scope.frmNewMaterialObj.data.materialTypeID = -1;
                    $scope.frmNewMaterialObj.materialWizard.materialStandardSelect();
                    $scope.frmNewMaterialObj.event.materialTypeOptions($scope.frmNewMaterialObj.data.materialColorID, $scope.frmNewMaterialObj.materialWizard.materialStandard);
                },
                materialStandardSelect: function () {                    
                    //$scope.frmNewMaterialObj.data.materialTypeID = -1;  
                    //$scope.frmNewMaterialObj.data.materialColor = -1;  
                    angular.forEach($scope.frmNewMaterialObj.supportMaterialColorOption, function (value, key) {
                        value.isSelected = false;
                    }, null);
                },
                materialTypeSelect: function () {
                    $scope.frmNewMaterialObj.data.materialTypeID = parseInt($scope.frmNewMaterialObj.data.materialTypeID);
                    angular.forEach($scope.frmNewMaterialObj.supportMaterialColorOption, function (value, key) {
                        value.isSelected = false;
                    }, null);
                },
                materialColorSelect: function (materialColorOption, materialColorID) {
                    $scope.frmNewMaterialObj.data.materialColorID = parseInt(materialColorID);
                    for (var i = 0; i < $scope.frmNewMaterialObj.supportMaterialColorOption.length; i++) {
                        var value = $scope.frmNewMaterialObj.supportMaterialColorOption[i];
                        if (value.materialColorID === materialColorID) {
                            value.isSelected = true;
                            $scope.frmNewMaterialObj.materialWizard.materialText = value.materialColorNM;
                            $scope.frmNewMaterialObj.materialWizard.materialImage = value.thumbnailLocation;
                        }
                        else {
                            value.isSelected = false;
                        }
                    }
                }
            },
            event: {
                open: function () {
                    $scope.frmNewMaterialObj.data = null;
                    $scope.frmNewMaterialObj.supportData = null;

                    $scope.frmNewMaterialObj.supportMaterialOption = [];
                    $scope.frmNewMaterialObj.supportMaterialColorOption = [];
                    $scope.frmNewMaterialObj.supportMaterialTypeOption = [];

                    dataService.getProductOption(
                        1,
                        $scope.data.modelID,
                        $scope.data.productGroupID,
                        function (data) {
                            if (data.message.type === 0) {
                                jsHelper.showPopup('frmNewMaterial', function () { });
                                window.scrollTo(0, 0);
                                $scope.frmNewMaterialObj.data = $scope.method.getBlankOptionData();
                                $scope.frmNewMaterialObj.data.breakdownCategoryID = 1;
                                $scope.frmNewMaterialObj.supportData = data.data;
                                $scope.frmNewMaterialObj.supportMaterialOption = data.data.materialOptionDTOs;
                                $scope.frmNewMaterialObj.supportMaterialColorOption = data.data.materialColorOptionDTOs;
                                $scope.frmNewMaterialObj.supportMaterialTypeOption = data.data.materialTypeOptionDTOs;
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                            $scope.frmNewMaterialObj.data = null;
                            $scope.frmNewMaterialObj.supportData = null;
                            $scope.frmNewMaterialObj.supportMaterialOption = [];
                            $scope.frmNewMaterialObj.supportMaterialColorOption = [];
                            $scope.frmNewMaterialObj.supportMaterialTypeOption = [];
                        }
                    );
                },
                close: function () {
                    $scope.frmNewMaterialObj.data = null;
                    $scope.frmNewMaterialObj.supportData = null;
                    $scope.frmNewMaterialObj.supportMaterialOption = [];
                    $scope.frmNewMaterialObj.supportMaterialColorOption = [];
                    $scope.frmNewMaterialObj.supportMaterialTypeOption = [];
                    jsHelper.hidePopup('frmNewMaterial', function () { });
                },
                save: function () {
                    dataService.updateCategoryOption(
                        -1,
                        $scope.frmNewMaterialObj.data,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.data.breakdownCategoryOptionDTOs.push(data.data.data);
                                $scope.availableOptionByArticleCodeDTOs = data.data.availableOptionByArticleCodeDTOs;
                                $scope.event.editOption(data.data.breakdownCategoryOptionID);
                                $scope.method.getArticleCode();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                        }
                    );
                    $scope.frmNewMaterialObj.event.close();
                },
                materialTypeOptions: function (selectedMaterialID, materialStandard) {
                    var materialType = [];
                    var id = 0;
                    var ud = '';
                    var name = '';
                    var season = jsHelper.getCurrentSeason();
                    for (var i = 0; i < $scope.frmNewMaterialObj.supportMaterialTypeOption.length; i++) {
                        var item = $scope.frmNewMaterialObj.supportMaterialTypeOption[i];
                        if (item.materialID === selectedMaterialID) {
                            if (materialStandard === true && item.season === season) {
                                if (id !== item.materialTypeID && ud !== item.materialTypeUD && name !== item.materialTypeNM) {
                                    id = item.materialTypeID;
                                    ud = item.materialTypeUD;
                                    name = item.materialTypeNM;
                                    materialType.push(item);
                                }
                            }
                            if (materialStandard === false && item.season !== season) {
                                if (id !== item.materialTypeID && ud !== item.materialTypeUD && name !== item.materialTypeNM) {
                                    id = item.materialTypeID;
                                    ud = item.materialTypeUD;
                                    name = item.materialTypeNM;
                                    materialType.push(item);
                                }
                            }
                        }

                    }
                    return materialType;
                },
                materialColorOptions: function (selectedMaterialID, selectMaterialTypeID, materialStandard) {
                    var materialColor = [];
                    var season = jsHelper.getCurrentSeason();
                    for (var i = 0; i < $scope.frmNewMaterialObj.supportMaterialColorOption.length; i++) {
                        var item = $scope.frmNewMaterialObj.supportMaterialColorOption[i];
                        if (item.materialID === selectedMaterialID && item.materialTypeID === selectMaterialTypeID) {
                            if (item.season === season && materialStandard === true) {
                                materialColor.push(item);
                            }
                            if (item.season !== season && materialStandard === false) {
                                materialColor.push(item);
                            }
                        }

                    }
                    return materialColor;
                }
            }
        };

        //
        // new frame option form
        //
        $scope.frmNewFrameObj = {
            data: null,
            supportData: null,
            supportFrameMaterialOption: [],
            supportFrameMaterialColorOption: [],
            frameMaterialWizard: {
                frameMaterialText: '',
                frameMaterialImage: '',
                selectedFrameMaterialID: -1,
                frameMaterialStandard: true,
                frameMaterialSelect: function () {
                    $scope.frmNewFrameObj.data.frameMaterialID = parseInt($scope.frmNewFrameObj.data.frameMaterialID);
                    $scope.frmNewFrameObj.frameMaterialWizard.frameMaterialStandardSelect();
                },
                frameMaterialStandardSelect: function () {
                    //$scope.frmNewFrameObj.data.frameMaterialID = -1;
                    //$scope.frmNewFrameObj.data.frameMaterialColorID = -1;
                    angular.forEach($scope.frmNewFrameObj.supportFrameMaterialColorOption, function (value, key) {
                        value.isSelected = false;
                    }, null);
                },
               
                frameMaterialColorSelect: function (frameMaterialColorOption, frameMaterialColorID) {
                    $scope.frmNewFrameObj.data.frameMaterialColorID = parseInt(frameMaterialColorID);
                    for (var i = 0; i < $scope.frmNewFrameObj.supportFrameMaterialColorOption.length; i++) {
                        var value = $scope.frmNewFrameObj.supportFrameMaterialColorOption[i];
                        if (value.frameMaterialColorID === frameMaterialColorID) {
                            value.isSelected = true;
                            $scope.frmNewFrameObj.frameMaterialWizard.frameMaterialText = value.frameMaterialColorNM;
                            $scope.frmNewFrameObj.frameMaterialWizard.frameMaterialImage = value.thumbnailLocation;
                        }
                        else {
                            value.isSelected = false;
                        }
                    }
                }
            },
            event: {
                open: function () {
                    $scope.frmNewFrameObj.data = null;
                    $scope.frmNewFrameObj.supportData = null;

                    $scope.frmNewMaterialObj.supportFrameMaterialOption = [];
                    $scope.frmNewMaterialObj.supportFrameMaterialColorOption = [];

                    dataService.getProductOption(
                        2,
                        $scope.data.modelID,
                        $scope.data.productGroupID,
                        function (data) {
                            if (data.message.type === 0) {
                                jsHelper.showPopup('frmNewFrame', function () { });
                                window.scrollTo(0, 0);

                                $scope.frmNewFrameObj.data = $scope.method.getBlankOptionData();
                                $scope.frmNewFrameObj.data.breakdownCategoryID = 2;
                                $scope.frmNewFrameObj.supportData = data.data;
                                $scope.frmNewFrameObj.supportFrameMaterialOption = data.data.frameMaterialOptionDTOs;
                                $scope.frmNewFrameObj.supportFrameMaterialColorOption = data.data.frameMaterialColorOptionDTOs;
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                            $scope.frmNewFrameObj.data = null;
                            $scope.frmNewFrameObj.supportData = null;
                            $scope.frmNewFrameObj.supportFrameMaterialOption = [];
                            $scope.frmNewFrameObj.supportFrameMaterialColorOption = [];
                        }
                    );
                },
                close: function () {
                    $scope.frmNewFrameObj.data = null;
                    $scope.frmNewFrameObj.supportData = null;
                    $scope.frmNewFrameObj.supportFrameMaterialOption = [];
                    $scope.frmNewFrameObj.supportFrameMaterialColorOption = [];

                    jsHelper.hidePopup('frmNewFrame', function () { });
                },
                save: function () {
                    dataService.updateCategoryOption(
                        -1,
                        $scope.frmNewFrameObj.data,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.data.breakdownCategoryOptionDTOs.push(data.data.data);
                                $scope.availableOptionByArticleCodeDTOs = data.data.availableOptionByArticleCodeDTOs;
                                $scope.event.editOption(data.data.breakdownCategoryOptionID);
                                $scope.method.getArticleCode();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                        }
                    );
                    $scope.frmNewFrameObj.event.close();
                },
                frameMaterialOptions: function () {
                    var frameMaterial = [];
                    var id = 0;
                    var name = '';
                    for (var i = 0; i < $scope.frmNewFrameObj.supportFrameMaterialOption.length; i++) {
                        var item = $scope.frmNewFrameObj.supportFrameMaterialOption[i];
                        if (id !== item.frameMaterialID && name !== item.frameMaterialNM) {
                            id = item.frameMaterialID;
                            name = item.frameMaterialNM;
                            frameMaterial.push(item);
                        }    

                    }
                    return frameMaterial;
                },
                frameMaterialColorOptions: function (selectedFrameMaterialID, frameMaterialStandard) {
                    var frameMaterialColor = [];
                    var season = jsHelper.getCurrentSeason();
                    var id = 0;
                    var name = '';
                    for (var i = 0; i < $scope.frmNewFrameObj.supportFrameMaterialColorOption.length; i++) {
                        var item = $scope.frmNewFrameObj.supportFrameMaterialColorOption[i];
                        if (item.frameMaterialID === selectedFrameMaterialID) {
                            if (item.season === season && frameMaterialStandard === true) {
                                if (id !== item.frameMaterialColorID && name !== item.frameMaterialColorNM) {
                                    id = item.frameMaterialColorID;
                                    name = item.frameMaterialColorNM;
                                    frameMaterialColor.push(item);
                                } 
                            }
                            if (item.season !== season && frameMaterialStandard === false) {
                                if (id !== item.frameMaterialColorID && name !== item.frameMaterialColorNM) {
                                    id = item.frameMaterialColorID;
                                    name = item.frameMaterialColorNM;
                                    frameMaterialColor.push(item);
                                } 
                            }
                        }

                    }
                    return frameMaterialColor;
                }  
            }
        }

        //
        // new sub material option form
        //
      $scope.frmNewSubMaterialObj = {
          data: null,
          supportData: null,
          supportSubMaterialOption: [],
          supportSubMaterialColorOption: [],
          subMaterialWizard: {
              subMaterialText: '',
              subMaterialImage: '',
              selectedSubMaterialID: -1,
              subMaterialStandard: true,
              subMaterialSelect: function () {
                  $scope.frmNewSubMaterialObj.data.subMaterialID = parseInt($scope.frmNewSubMaterialObj.data.subMaterialID);
                  $scope.frmNewSubMaterialObj.subMaterialWizard.subMaterialStandardSelect();
              },
              subMaterialStandardSelect: function () {
                  //$scope.frmNewFrameObj.data.frameMaterialID = -1;
                  //$scope.frmNewFrameObj.data.frameMaterialColorID = -1;
                  angular.forEach($scope.frmNewSubMaterialObj.supportSubMaterialColorOption, function (value, key) {
                      value.isSelected = false;
                  }, null);
              },

              subMaterialColorSelect: function (subMaterialColorOption, subMaterialColorID) {
                  $scope.frmNewSubMaterialObj.data.subMaterialColorID = parseInt(subMaterialColorID);
                  for (var i = 0; i < $scope.frmNewSubMaterialObj.supportSubMaterialColorOption.length; i++) {
                      var value = $scope.frmNewSubMaterialObj.supportSubMaterialColorOption[i];
                      if (value.subMaterialColorID === subMaterialColorID) {
                          value.isSelected = true;
                          $scope.frmNewSubMaterialObj.subMaterialWizard.subMaterialText = value.subMaterialColorNM;
                          $scope.frmNewSubMaterialObj.subMaterialWizard.subMaterialImage = value.thumbnailLocation;
                      }
                      else {
                          value.isSelected = false;
                      }
                  }
              }
          },
            event: {
                open: function (subMaterialID, subMaterialColorID) {
                    $scope.frmNewSubMaterialObj.data = null;
                    $scope.frmNewSubMaterialObj.supportData = null;
                    $scope.frmNewMaterialObj.supportSubMaterialOption = [];
                    $scope.frmNewMaterialObj.supportSubMaterialColorOption = [];

                    dataService.getProductOption(
                        3,
                        $scope.data.modelID,
                        $scope.data.productGroupID,
                        function (data) {
                            if (data.message.type === 0) {
                                jsHelper.showPopup('frmNewSubMaterial', function () { });
                                window.scrollTo(0, 0);

                                $scope.frmNewSubMaterialObj.data = $scope.method.getBlankOptionData();
                                $scope.frmNewSubMaterialObj.data.breakdownCategoryID = 3;
                                $scope.frmNewSubMaterialObj.supportData = data.data;
                                $scope.frmNewSubMaterialObj.supportSubMaterialOption = data.data.subMaterialOptionDTOs;
                                $scope.frmNewSubMaterialObj.supportSubMaterialColorOption = data.data.subMaterialColorOptionDTOs;
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                            $scope.frmNewSubMaterialObj.data = null;
                            $scope.frmNewSubMaterialObj.supportData = null;
                            $scope.frmNewSubMaterialObj.supportSubMaterialOption = [];
                            $scope.frmNewSubMaterialObj.supportSubMaterialColorOption = [];
                        }
                    );
                },
                close: function () {
                    $scope.frmNewSubMaterialObj.data = null;
                    $scope.frmNewSubMaterialObj.supportData = null;
                    $scope.frmNewSubMaterialObj.supportSubMaterialOption = [];
                    $scope.frmNewSubMaterialObj.supportSubMaterialColorOption = [];
                    jsHelper.hidePopup('frmNewSubMaterial', function () { });
                },
                save: function () {
                    dataService.updateCategoryOption(
                        -1,
                        $scope.frmNewSubMaterialObj.data,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.data.breakdownCategoryOptionDTOs.push(data.data.data);
                                $scope.availableOptionByArticleCodeDTOs = data.data.availableOptionByArticleCodeDTOs;
                                $scope.event.editOption(data.data.breakdownCategoryOptionID);
                                $scope.method.getArticleCode();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                        }
                    );
                    $scope.frmNewSubMaterialObj.event.close();
                },
                subMaterialOptions: function () {
                    var subMaterial = [];
                    var id = 0;
                    var name = '';
                    for (var i = 0; i < $scope.frmNewSubMaterialObj.supportSubMaterialOption.length; i++) {
                        var item = $scope.frmNewSubMaterialObj.supportSubMaterialOption[i];
                        if (id !== item.subMaterialID && name !== item.subMaterialNM) {
                            id = item.subMaterialID;
                            name = item.subMaterialNM;
                            subMaterial.push(item);
                        }

                    }
                    return subMaterial;
                },
                subMaterialColorOptions: function (selectedSubMaterialID, subMaterialStandard) {
                    var subMaterialColor = [];
                    var season = jsHelper.getCurrentSeason();
                    var id = 0;
                    var name = '';
                    for (var i = 0; i < $scope.frmNewSubMaterialObj.supportSubMaterialColorOption.length; i++) {
                        var item = $scope.frmNewSubMaterialObj.supportSubMaterialColorOption[i];
                        if (item.subMaterialID === selectedSubMaterialID) {
                            if (item.season === season && subMaterialStandard === true) {
                                if (id !== item.subMaterialColorID && name !== item.subMaterialColorNM) {
                                    id = item.subMaterialColorID;
                                    name = item.subMaterialColorNM;
                                    subMaterialColor.push(item);
                                }
                            }
                            if (item.season !== season && subMaterialStandard === false) {
                                if (id !== item.subMaterialColorID && name !== item.subMaterialColorNM) {
                                    id = item.subMaterialColorID;
                                    name = item.subMaterialColorNM;
                                    subMaterialColor.push(item);
                                }
                            }
                        }

                    }
                    return subMaterialColor;
                }  
            },   
        }

        //
        // new cushion option form
        //
        $scope.frmNewCushionObj = {
            data: null,
            supportData: null,
            supportCushionTypeOption: [],
            supportCushionColorOption: [],
            supportSeatCushionOption: [],
            supportBackCushionOption: [],
            cushionTypeWizard: {
                cushionColorText: '',
                cushionColorImage: '',
                selectedCushionTypeID: -1,
                cushionTypeStandard: true,
                cushionTypeSelect: function () {
                    $scope.frmNewCushionObj.data.cushionTypeID = parseInt($scope.frmNewCushionObj.data.cushionTypeID);
                    $scope.frmNewCushionObj.cushionTypeWizard.cushionTypeStandardSelect();
                },
                cushionTypeStandardSelect: function () {
                    //$scope.frmNewCushionObj.data.cushionTypeID = -1;
                    //$scope.frmNewCushionObj.data.cushionColorID = -1;
                    angular.forEach($scope.frmNewCushionObj.supportCushionColorOption, function (value, key) {
                        value.isSelected = false;
                    }, null);
                },

                cushionColorSelect: function (cushionColorOption, cushionColorID) {
                    $scope.frmNewCushionObj.data.cushionColorID = parseInt(cushionColorID);
                    for (var i = 0; i < $scope.frmNewCushionObj.supportCushionColorOption.length; i++) {
                        var value = $scope.frmNewCushionObj.supportCushionColorOption[i];
                        if (value.cushionColorID === cushionColorID) {
                            value.isSelected = true;
                            $scope.frmNewCushionObj.cushionTypeWizard.cushionColorText = value.cushionColorNM;
                            $scope.frmNewCushionObj.cushionTypeWizard.cushionColorImage = value.thumbnailLocation;
                        }
                        else {
                            value.isSelected = false;
                        }
                    }
                },
                seatCushionSelect: function (seatCushionID) {
                    $scope.frmNewCushionObj.data.seatCushionID = parseInt(seatCushionID);
                    for (var i = 0; i < $scope.frmNewCushionObj.supportSeatCushionOption.length; i++) {
                        var value = $scope.frmNewCushionObj.supportSeatCushionOption[i];
                        if (value.seatCushionID === seatCushionID) {
                            value.isSelected = true;
                            $scope.frmNewCushionObj.cushionTypeWizard.seatCushionText = value.seatCushionNM;
                        }
                        else {
                            value.isSelected = false;
                        }
                    }
                },
                backCushionSelect: function (backCushionID) {
                    $scope.frmNewCushionObj.data.backCushionID = parseInt(backCushionID);
                    for (var i = 0; i < $scope.frmNewCushionObj.supportBackCushionOption.length; i++) {
                        var value = $scope.frmNewCushionObj.supportBackCushionOption[i];
                        if (value.backCushionID === backCushionID) {
                            value.isSelected = true;
                            $scope.frmNewCushionObj.cushionTypeWizard.backCushionText = value.backCushionNM;
                        }
                        else {
                            value.isSelected = false;
                        }
                    }
                }
            },
            event: {
                open: function () {
                    $scope.frmNewCushionObj.data = null;
                    $scope.frmNewCushionObj.supportData = null;
                    $scope.frmNewCushionObj.supportCushionTypeOption = [];
                    $scope.frmNewCushionObj.supportCushionColorOption = [];
                    $scope.frmNewCushionObj.supportSeatCushionOption = [];
                    $scope.frmNewCushionObj.supportBackCushionOption = [];

                    dataService.getProductOption(
                        4,
                        $scope.data.modelID,
                        $scope.data.productGroupID,
                        function (data) {
                            if (data.message.type === 0) {
                                jsHelper.showPopup('frmNewCushion', function () { });
                                window.scrollTo(0, 0);

                                $scope.frmNewCushionObj.data = $scope.method.getBlankOptionData();
                                $scope.frmNewCushionObj.data.breakdownCategoryID = 4;
                                $scope.frmNewCushionObj.supportData = data.data;
                                $scope.frmNewCushionObj.supportCushionTypeOption = data.data.cushionTypeOptionDTOs;
                                $scope.frmNewCushionObj.supportCushionColorOption = data.data.cushionColorOptionDTOs;
                                $scope.frmNewCushionObj.supportSeatCushionOption = data.data.seatCushionOptionDTOs;
                                $scope.frmNewCushionObj.supportBackCushionOption = data.data.backCushionOptionDTOs;
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                            $scope.frmNewCushionObj.data = null;
                            $scope.frmNewCushionObj.supportData = null;
                            $scope.frmNewCushionObj.supportCushionTypeOption = [];
                            $scope.frmNewCushionObj.supportCushionColorOption = [];
                            $scope.frmNewCushionObj.supportSeatCushionOption = [];
                            $scope.frmNewCushionObj.supportBackCushionOption = [];
                        }
                    );
                },
                close: function () {
                    $scope.frmNewCushionObj.data = null;
                    $scope.frmNewCushionObj.supportData = null;
                    $scope.frmNewCushionObj.supportCushionTypeOption = [];
                    $scope.frmNewCushionObj.supportCushionColorOption = [];
                    $scope.frmNewCushionObj.supportSeatCushionOption = [];
                    $scope.frmNewCushionObj.supportBackCushionOption = [];

                    jsHelper.hidePopup('frmNewCushion', function () { });
                },
                save: function () {
                    dataService.updateCategoryOption(
                        -1,
                        $scope.frmNewCushionObj.data,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.data.breakdownCategoryOptionDTOs.push(data.data.data);
                                $scope.availableOptionByArticleCodeDTOs = data.data.availableOptionByArticleCodeDTOs;
                                $scope.event.editOption(data.data.breakdownCategoryOptionID);
                                $scope.method.getArticleCode();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                        }
                    );
                    $scope.frmNewCushionObj.event.close();
                },
                cushionTypeOptions: function () {
                    var cushionType = [];
                    var id = 0;
                    var name = '';
                    for (var i = 0; i < $scope.frmNewCushionObj.supportCushionTypeOption.length; i++) {
                        var item = $scope.frmNewCushionObj.supportCushionTypeOption[i];
                        if (id !== item.cushionTypeID && name !== item.cushionTypeNM) {
                            id = item.cushionTypeID;
                            name = item.cushionTypeNM;
                            cushionType.push(item);
                        }

                    }
                    return cushionType;
                },
                cushionColorOptions: function (selectedCushionTypeID, cushionTypeStandard) {
                    var cushionColor = [];
                    var season = jsHelper.getCurrentSeason();
                    var id = 0;
                    var name = '';
                    for (var i = 0; i < $scope.frmNewCushionObj.supportCushionColorOption.length; i++) {
                        var item = $scope.frmNewCushionObj.supportCushionColorOption[i];
                        if (item.cushionTypeID === selectedCushionTypeID) {
                            if (item.season === season && cushionTypeStandard === true) {
                                if (id !== item.cushionColorID && name !== item.cushionColorNM) {
                                    id = item.cushionColorID;
                                    name = item.cushionColorNM;
                                    cushionColor.push(item);
                                }
                            }
                            if (item.season !== season && cushionTypeStandard === false) {
                                if (id !== item.cushionColorID && name !== item.cushionColorNM) {
                                    id = item.cushionColorID;
                                    name = item.cushionColorNM;
                                    cushionColor.push(item);
                                }
                            }
                        }

                    }
                    return cushionColor;
                },
                seatCushionOptions: function () {
                    var seatCushion = [];
                    var id = 0;
                    var name = '';
                    for (var i = 0; i < $scope.frmNewCushionObj.supportSeatCushionOption.length; i++) {
                        var item = $scope.frmNewCushionObj.supportData.seatCushionOptionDTOs[i];
                        if (id !== item.seatCushionID && name !== item.seatCushionNM) {
                            id = item.seatCushionID;
                            name = item.seatCushionNM;
                            seatCushion.push(item);
                        }

                    }
                    return seatCushion;
                },
                backCushionOptions: function () {
                    var backcushion = [];
                    var id = 0;
                    var name = '';
                    for (var i = 0; i < $scope.frmNewCushionObj.supportBackCushionOption.length; i++) {
                        var item = $scope.frmNewCushionObj.supportData.backCushionOptionDTOs[i];
                        if (id !== item.backCushionID && name !== item.backCushionNM) {
                            id = item.backCushionID;
                            name = item.backCushionNM;
                            backcushion.push(item);
                        }

                    }
                    return backcushion;
                }
            }
        };

        //
        // new cushion option form
        //
        $scope.frmNewPackingObj = {
            data: null,
            supportData: null,
            event: {
                open: function () {
                    $scope.frmNewPackingObj.data = null;
                    $scope.frmNewPackingObj.supportData = null;

                    dataService.getProductOption(
                        5,
                        $scope.data.modelID,
                        $scope.data.productGroupID,
                        function (data) {
                            if (data.message.type === 0) {
                                jsHelper.showPopup('frmNewPacking', function () { });
                                window.scrollTo(0, 0);

                                $scope.frmNewPackingObj.data = $scope.method.getBlankOptionData();
                                $scope.frmNewPackingObj.data.breakdownCategoryID = 5;
                                $scope.frmNewPackingObj.supportData = data.data;
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                            $scope.frmNewPackingObj.data = null;
                            $scope.frmNewPackingObj.supportData = null;
                        }
                    );
                },
                close: function () {
                    $scope.frmNewPackingObj.data = null;
                    $scope.frmNewPackingObj.supportData = null;

                    jsHelper.hidePopup('frmNewPacking', function () { });
                },
                save: function () {
                    if ($scope.frmNewPackingObj.data.packagingMethodID !== 11) {
                        $scope.frmNewPackingObj.data.clientSpecialPackagingMethodID = null;
                    }

                    dataService.updateCategoryOption(
                        -1,
                        $scope.frmNewPackingObj.data,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.data.breakdownCategoryOptionDTOs.push(data.data.data);
                                $scope.availableOptionByArticleCodeDTOs = data.data.availableOptionByArticleCodeDTOs;
                                $scope.event.editOption(data.data.breakdownCategoryOptionID);
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                        }
                    );
                    $scope.frmNewPackingObj.event.close();
                }
            }
        };

        //
        // new fsc option form
        //
        $scope.frmNewFSCObj = {
            data: null,
            supportData: null,
            event: {
                open: function () {
                    $scope.frmNewFSCObj.data = null;
                    $scope.frmNewFSCObj.supportData = null;

                    dataService.getProductOption(
                        8,
                        $scope.data.modelID,
                        $scope.data.productGroupID,
                        function (data) {
                            if (data.message.type === 0) {
                                jsHelper.showPopup('frmNewFSC', function () { });
                                window.scrollTo(0, 0);

                                $scope.frmNewFSCObj.data = $scope.method.getBlankOptionData();
                                $scope.frmNewFSCObj.data.breakdownCategoryID = 8;
                                $scope.frmNewFSCObj.supportData = data.data;
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                            $scope.frmNewFSCObj.data = null;
                            $scope.frmNewFSCObj.supportData = null;
                        }
                    );
                },
                close: function () {
                    $scope.frmNewFSCObj.data = null;
                    $scope.frmNewFSCObj.supportData = null;

                    jsHelper.hidePopup('frmNewFSC', function () { });
                },
                save: function () {
                    if ($scope.frmNewFSCObj.data.fscTypeID === 1) {
                        $scope.frmNewFSCObj.data.fscPercentID = 1;
                    }
                    if ($scope.frmNewFSCObj.data.fscTypeID === 2) {
                        $scope.frmNewFSCObj.data.fscPercentID = 9;
                    }

                    dataService.updateCategoryOption(
                        -1,
                        $scope.frmNewFSCObj.data,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.data.breakdownCategoryOptionDTOs.push(data.data.data);
                                $scope.availableOptionByArticleCodeDTOs = data.data.availableOptionByArticleCodeDTOs;
                                $scope.event.editOption(data.data.breakdownCategoryOptionID);
                                $scope.method.getArticleCode();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                        }
                    );
                    $scope.frmNewFSCObj.event.close();
                }
            }
        };

        //
        // new special request form
        //
        $scope.frmNewSpecialRequestObj = {
            data: null,
            supportData: null,
            event: {
                open: function () {
                    $scope.frmNewSpecialRequestObj.data = $scope.method.getBlankOptionData();
                    $scope.frmNewSpecialRequestObj.data.breakdownCategoryID = 9;
                    $scope.frmNewSpecialRequestObj.supportData = null;

                    jsHelper.showPopup('frmNewSpecialRequest', function () { });
                    window.scrollTo(0, 0);
                },
                close: function () {
                    $scope.frmNewSpecialRequestObj.data = null;
                    $scope.frmNewSpecialRequestObj.supportData = null;

                    jsHelper.hidePopup('frmNewSpecialRequest', function () { });
                },
                save: function () {
                    dataService.updateCategoryOption(
                        -1,
                        $scope.frmNewSpecialRequestObj.data,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.data.breakdownCategoryOptionDTOs.push(data.data.data);
                                $scope.availableOptionByArticleCodeDTOs = data.data.availableOptionByArticleCodeDTOs;
                                $scope.event.editOption(data.data.breakdownCategoryOptionID);
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                        }
                    );
                    $scope.frmNewSpecialRequestObj.event.close();
                }
            }
        };

        //
        // detail form
        //
        $scope.frmDetailObj = {
            data: null,
            selectedGroupData: null,
            selectedItemData: null,
            selectedCompanyId: context.companyId,
            selectedBreakdownCategoryId: null,
            totalExcludeManagementFeeData: {
                total: 0,
                totalUSD: 0,
                totalAVT: 0,
                totalUSDAVT: 0
            },
            method: {
                getSubTotalAmount: function () {
                    var result = parseFloat(0);
                    var groupQnt = 1;
                    var groupObj = null;
                    if ($scope.frmDetailObj.data) {
                        angular.forEach($scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs, function (item) {
                            groupObj = null;
                            groupQnt = 1;
                            if (item.breakdownCategoryOptionGroupID !== null) {
                                groupObj = $scope.frmDetailObj.method.getGroupObj(item.breakdownCategoryOptionGroupID);
                                if (groupObj) {
                                    groupQnt = parseInt(groupObj.quantity);
                                }
                            }
                            if (!groupQnt) {
                                groupQnt = 1;
                            }

                            if ($scope.frmDetailObj.data.breakdownCategoryID !== 10) {
                                if (item.totalAmount) {
                                    result += groupQnt * parseFloat(item.totalAmount);
                                }
                                else {
                                    if (item.quantity) {
                                        if (item.wastedRatePercent) {
                                            result += groupQnt * parseFloat(($scope.frmDetailObj.data.companyID === 1) ? item.avfPrice : item.avtPrice) * parseFloat(item.quantity) * (1 + jsHelper.round(parseFloat(item.wastedRatePercent) / 100, 2));
                                        }
                                        else {
                                            result += groupQnt * parseFloat(($scope.frmDetailObj.data.companyID === 1) ? item.avfPrice : item.avtPrice) * parseFloat(item.quantity);
                                        }
                                    }
                                }
                            }
                            else {
                                if (item.quantityPercent) {
                                    result += groupQnt * (($scope.frmDetailObj.data.companyID === 1) ? $scope.frmDetailObj.totalExcludeManagementFeeData.total : $scope.frmDetailObj.totalExcludeManagementFeeData.totalAVT) * item.quantityPercent / 100;
                                }
                            }
                        });
                    }
                    return result;
                },
                getSubTotalAmountUSD: function () {
                    var result = parseFloat(0);
                    var groupQnt = 1;
                    var groupObj = null;
                    if ($scope.frmDetailObj.data) {
                        angular.forEach($scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs, function (item) {
                            groupObj = null;
                            groupQnt = 1;
                            if (item.breakdownCategoryOptionGroupID !== null) {
                                groupObj = $scope.frmDetailObj.method.getGroupObj(item.breakdownCategoryOptionGroupID);
                                if (groupObj) {
                                    groupQnt = parseInt(groupObj.quantity);
                                }
                            }
                            if (!groupQnt) {
                                groupQnt = 1;
                            }

                            if ($scope.frmDetailObj.data.breakdownCategoryID !== 10) {
                                if (item.totalAmount) {
                                    result += groupQnt * jsHelper.round(parseFloat(item.totalAmount) / $scope.exRate, 2);
                                }
                                else {
                                    if (item.quantity) {
                                        if (item.wastedRatePercent) {
                                            result += groupQnt * jsHelper.round(parseFloat(($scope.frmDetailObj.data.companyID === 1) ? item.avfPrice : item.avtPrice) * parseFloat(item.quantity) * (1 + jsHelper.round(parseFloat(item.wastedRatePercent) / 100, 2)) / $scope.exRate, 2);
                                        }
                                        else {
                                            result += groupQnt * jsHelper.round(parseFloat(($scope.frmDetailObj.data.companyID === 1) ? item.avfPrice : item.avtPrice) * parseFloat(item.quantity) / $scope.exRate, 2);
                                        }
                                    }
                                }
                            }
                            else {
                                if (item.quantityPercent) {
                                    result += groupQnt * jsHelper.round((($scope.frmDetailObj.data.companyID === 1) ? $scope.frmDetailObj.totalExcludeManagementFeeData.totalUSD : $scope.frmDetailObj.totalExcludeManagementFeeData.totalUSDAVT) * item.quantityPercent / 100, 2);
                                }
                            }
                        });
                    }
                    return result;
                },
                getTotalPercentManagement: function () {
                    var result = parseFloat(0);
                    if ($scope.frmDetailObj.data && $scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs && $scope.frmDetailObj.data.breakdownCategoryID === 10) {
                        angular.forEach($scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs, function (item) {
                            if (item.quantityPercent) {
                                result += parseFloat(item.quantityPercent);
                            }
                        });
                    }
                    return result;
                },

                getGroupObj: function (id) {
                    var result = null;
                    if ($scope.frmDetailObj.data) {
                        angular.forEach($scope.frmDetailObj.data.breakdownCategoryOptionGroupDTOs, function (item) {
                            if (item.breakdownCategoryOptionGroupID === id) {
                                result = item;
                            }
                        });
                    }
                    return result;
                },

                getSubTotalAmountByGroup: function (id) {
                    var result = parseFloat(0);
                    var groupQnt = 1;
                    var groupObj = $scope.frmDetailObj.method.getGroupObj(id);
                    if (!groupObj) return 0;

                    if ($scope.frmDetailObj.data) {
                        groupQnt = groupObj.quantity;
                        if (!groupQnt) {
                            groupQnt = 1;
                        }

                        angular.forEach($scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs, function (item) {
                            if (item.breakdownCategoryOptionGroupID === id) {
                                if ($scope.frmDetailObj.data.breakdownCategoryID !== 10) {
                                    if (item.totalAmount) {
                                        result += groupQnt * parseFloat(item.totalAmount);
                                    }
                                    else {
                                        if (item.quantity) {
                                            if (item.wastedRatePercent) {
                                                result += groupQnt * parseFloat(($scope.frmDetailObj.data.companyID === 1) ? item.avfPrice : item.avtPrice) * parseFloat(item.quantity) * (1 + jsHelper.round(parseFloat(item.wastedRatePercent) / 100, 2));
                                            }
                                            else {
                                                result += groupQnt * parseFloat(($scope.frmDetailObj.data.companyID === 1) ? item.avfPrice : item.avtPrice) * parseFloat(item.quantity);
                                            }
                                        }
                                    }
                                }
                                else {
                                    if (item.quantityPercent) {
                                        result += groupQnt * (($scope.frmDetailObj.data.companyID === 1) ? $scope.frmDetailObj.totalExcludeManagementFeeData.total : $scope.frmDetailObj.totalExcludeManagementFeeData.totalAVT) * item.quantityPercent / 100;
                                    }
                                }
                            }
                        });
                    }
                    return result;
                },
                getSubTotalAmountUSDByGroup: function (id) {
                    var result = parseFloat(0);
                    var groupQnt = 1;
                    var groupObj = $scope.frmDetailObj.method.getGroupObj(id);
                    if (!groupObj) return 0;

                    if ($scope.frmDetailObj.data) {
                        groupQnt = groupObj.quantity;
                        if (!groupQnt) {
                            groupQnt = 1;
                        }

                        angular.forEach($scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs, function (item) {
                            if (item.breakdownCategoryOptionGroupID === id) {
                                if ($scope.frmDetailObj.data.breakdownCategoryID !== 10) {
                                    if (item.totalAmount) {
                                        result += groupQnt * jsHelper.round(parseFloat(item.totalAmount) / $scope.exRate, 2);
                                    }
                                    else {
                                        if (item.quantity) {
                                            if (item.wastedRatePercent) {
                                                result += groupQnt * jsHelper.round(parseFloat(($scope.frmDetailObj.data.companyID === 1) ? item.avfPrice : item.avtPrice) * parseFloat(item.quantity) * (1 + jsHelper.round(parseFloat(item.wastedRatePercent) / 100, 2)) / $scope.exRate, 2);
                                            }
                                            else {
                                                result += groupQnt * jsHelper.round(parseFloat(($scope.frmDetailObj.data.companyID === 1) ? item.avfPrice : item.avtPrice) * parseFloat(item.quantity) / $scope.exRate, 2);
                                            }
                                        }
                                    }
                                }
                                else {
                                    if (item.quantityPercent) {
                                        result += groupQnt * jsHelper.round(((($scope.frmDetailObj.data.companyID === 1) ? $scope.frmDetailObj.totalExcludeManagementFeeData.total : $scope.frmDetailObj.totalExcludeManagementFeeData.totalAVT) * item.quantityPercent / 100) / $scope.exRate, 2);
                                    }
                                }
                            }
                        });
                    }
                    return result;
                },
            },
            event: {
                open: function () {
                    if ($scope.frmDetailObj.data) {
                        $('#txtSearchProductionItem').val('');
                        if ($scope.frmDetailObj.data.breakdownCategoryID !== 10) {
                            $('#txtParam').val('m');
                            $scope.frmDetailObj.totalExcludeManagementFeeData.total = 0;
                            $scope.frmDetailObj.totalExcludeManagementFeeData.totalUSD = 0;
                            $scope.frmDetailObj.totalExcludeManagementFeeData.totalAVT = 0;
                            $scope.frmDetailObj.totalExcludeManagementFeeData.totalUSDAVT = 0;
                        }
                        else {
                            $('#txtParam').val('c');
                            // get total amount exclude management fee if needed
                            $scope.frmDetailObj.totalExcludeManagementFeeData.total = $scope.method.getTotalExcludeManagementFee();
                            $scope.frmDetailObj.totalExcludeManagementFeeData.totalUSD = $scope.method.getTotalExcludeManagementFeeUSD();
                            $scope.frmDetailObj.totalExcludeManagementFeeData.totalAVT = $scope.method.getTotalExcludeManagementFeeAVT();
                            $scope.frmDetailObj.totalExcludeManagementFeeData.totalUSDAVT = $scope.method.getTotalExcludeManagementFeeUSDAVT();
                        }

                        jsHelper.showPopup('frmDetail', function () { });
                        window.scrollTo(0, 0);

                        //
                        // quick search
                        //
                        $('#txtSearchProductionItem').autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    url: context.serviceUrl + 'quick-search-production-item',
                                    headers: {
                                        'Accept': 'application/json',
                                        'Content-Type': 'application/json',
                                        'Authorization': 'Bearer ' + context.token
                                    },
                                    type: 'GET',
                                    dataType: "json",
                                    data: {
                                        query: request.term,
                                        type: $('#txtParam').val()
                                    },
                                    success: function (data) {
                                        response($.map(data, function (item) {
                                            return {
                                                productionItemID: item.productionItemID,
                                                productionItemUD: item.productionItemUD,
                                                productionItemNM: item.productionItemNM,
                                                unitID: item.unitID,
                                                unitNM: item.unitNM,
                                                thumbnailLocation: item.thumbnailLocation,
                                                fileLocation: item.fileLocation,
                                                productionItemTypeID: item.productionItemTypeID
                                            }
                                        }));
                                    },
                                    error: function (jqXHR, textStatus, errorThrown) {
                                        console.log(jqXHR);
                                        console.log(textStatus);
                                        console.log(errorThrown);
                                    }
                                });
                            },
                            minLength: 3,
                            select: function (event, ui) {
                                // add to detail
                                scope.frmDetailObj.event.addDetail(ui.item);
                                scope.$apply();
                            },
                            change: function (event, ui) {
                            },
                        })
                            .data("ui-autocomplete")._renderItem = function (ul, item) {
                                return $("<li>")
                                    .data('item.autocomplete', item)
                                    .append('<a href="javascript:void(0)"><strong style="text-decoration: underline; text-transform: uppercase;">' + item.productionItemUD + '</strong><br>' + item.productionItemNM + '</a>')
                                    .appendTo(ul);
                            };
                    }
                    else {
                        alert('Data has not been set!');
                    }
                },
                close: function () {
                    $scope.frmDetailObj.data = null;
                    jsHelper.hidePopup('frmDetail', function () { });
                },
                save: function () {
                    dataService.updateCategoryOption(
                        $scope.frmDetailObj.data.breakdownCategoryOptionID,
                        $scope.frmDetailObj.data,
                        function (data) {
                            if (data.message.type === 0) {
                                for (var i = 0; i <= $scope.data.breakdownCategoryOptionDTOs.length - 1; i++) {
                                    if ($scope.data.breakdownCategoryOptionDTOs[i].breakdownCategoryOptionID === data.data.data.breakdownCategoryOptionID) {
                                        $scope.data.breakdownCategoryOptionDTOs[i] = data.data.data;
                                    }
                                }
                                //angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (item) {
                                //    if (item.breakdownCategoryOptionID === data.data.breakdownCategoryOptionID) {
                                //        console.log('set');
                                //        item = data.data;
                                //    }
                                //});
                                $scope.frmDetail.data = null;
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                        }
                    );
                    $scope.frmDetailObj.event.close();
                },
                addDetail: function (item) {
                    //var isExist = false;
                    //angular.forEach($scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs, function (detail) {
                    //    if (detail.productionItemID === item.productionItemID) {
                    //        isExist = true;
                    //    }
                    //});
                    //if (isExist) {
                    //    return;
                    //}

                    $scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs.push({
                        breakdownCategoryOptionDetailID: dataService.getIncrementId(),
                        breakdownCategoryOptionID: $scope.frmDetailObj.data.breakdownCategoryOptionID,
                        description: null,
                        descriptionEN: null,
                        productionItemID: item.productionItemID,
                        unitID: item.unitID,
                        quantity: 0,
                        quantityPercent: null,
                        wastedRatePercent: 0,
                        totalAmount: 0,
                        productionItemUD: item.productionItemUD,
                        productionItemNM: item.productionItemNM,
                        unitNM: item.unitNM,
                        avfPrice: 0,
                        avtPrice: 0,
                        thumbnailLocation: item.thumbnailLocation,
                        fileLocation: item.fileLocation,
                        breakdownCategoryOptionGroupID: null,
                        productionItemTypeID: item.productionItemTypeID
                    });
                },
                deleteDetail: function (item) {
                    $scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs.splice($scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs.indexOf(item), 1);
                },
                addSpecialRequirement: function () {
                    $scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs.push({
                        breakdownCategoryOptionDetailID: dataService.getIncrementId(),
                        breakdownCategoryOptionID: $scope.frmDetailObj.data.breakdownCategoryOptionID,
                        description: $('#txtSpecialRequirementItem').val(),
                        descriptionEN: $('#txtSpecialRequirementItemEN').val(),
                        productionItemID: null,
                        unitID: null,
                        quantity: 0,
                        quantityPercent: null,
                        wastedRatePercent: 0,
                        totalAmount: 0,
                        productionItemUD: null,
                        productionItemNM: null,
                        unitNM: null,
                        avfPrice: 0,
                        avtPrice: 0,
                        thumbnailLocation: null,
                        fileLocation: null,
                        breakdownCategoryOptionGroupID: null
                    });
                    $('#txtSpecialRequirementItem').val('');
                    $('#txtSpecialRequirementItemEN').val('');
                },
                addWoodItem: function () {
                    angular.forEach($scope.data.breakdownCategoryOptionDTOs, function (option) {
                        angular.forEach(option.breakdownCategoryOptionDetailDTOs, function (item) {
                            if (item.productionItemGroupID === 15) { // wooden item
                                $scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs.push({
                                    breakdownCategoryOptionDetailID: dataService.getIncrementId(),
                                    breakdownCategoryOptionID: $scope.frmDetailObj.data.breakdownCategoryOptionID,
                                    description: null,
                                    descriptionEN: null,
                                    productionItemID: item.productionItemID,
                                    unitID: item.unitID,
                                    quantity: 0,
                                    quantityPercent: null,
                                    wastedRatePercent: 0,
                                    totalAmount: 0,
                                    productionItemUD: item.productionItemUD,
                                    productionItemNM: item.productionItemNM,
                                    unitNM: item.unitNM,
                                    avfPrice: 0,
                                    avtPrice: 0,
                                    thumbnailLocation: item.thumbnailLocation,
                                    fileLocation: item.fileLocation,
                                    breakdownCategoryOptionGroupID: null
                                });
                            }
                        });
                    });
                },
                removeFromGroup: function (item) {
                    item.breakdownCategoryGroupID = null;
                },
                moveToGroupOpen: function (item) {
                    $scope.frmDetailObj.selectedItemData = angular.copy(item);
                    $('#popupSelectGroup').modal('show');
                },
                moveToGroupSave: function () {
                    angular.forEach($scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs, function (item) {
                        if (item.breakdownCategoryOptionDetailID === $scope.frmDetailObj.selectedItemData.breakdownCategoryOptionDetailID) {
                            item.breakdownCategoryOptionGroupID = $scope.frmDetailObj.selectedItemData.breakdownCategoryOptionGroupID;
                        }
                    });

                    $('#popupSelectGroup').modal('hide');
                },
                openEditGroup: function (item) {
                    if (item) {
                        $scope.frmDetailObj.selectedGroupData = angular.copy(item);
                    }
                    else {
                        $scope.frmDetailObj.selectedGroupData = {
                            breakdownCategoryOptionGroupID: dataService.getIncrementId(),
                            breakdownCategoryOptionID: $scope.frmDetailObj.data.breakdownCategoryOptionID,
                            breakdownCategoryOptionGroupNM: '',
                            breakdownCategoryOptionGroupNMEN: ''
                        };
                    }

                    $('#popupEditGroup').modal('show');
                },
                updateGroup: function () {
                    var isExist = false;
                    for (var i = 0; i < $scope.frmDetailObj.data.breakdownCategoryOptionGroupDTOs.length; i++) {
                        if ($scope.frmDetailObj.data.breakdownCategoryOptionGroupDTOs[i].breakdownCategoryOptionGroupID === $scope.frmDetailObj.selectedGroupData.breakdownCategoryOptionGroupID) {
                            $scope.frmDetailObj.data.breakdownCategoryOptionGroupDTOs[i] = angular.copy($scope.frmDetailObj.selectedGroupData);
                            isExist = true;
                        }
                    }
                    if (!isExist) {
                        $scope.frmDetailObj.data.breakdownCategoryOptionGroupDTOs.push(angular.copy($scope.frmDetailObj.selectedGroupData));
                    }

                    $('#popupEditGroup').modal('hide');
                },
                removeGroup: function (group) {
                    angular.forEach($scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs, function (item) {
                        if (item.breakdownCategoryOptionGroupID === group.breakdownCategoryOptionGroupID) {
                            item.breakdownCategoryOptionGroupID = null;
                        }
                    });
                    $scope.frmDetailObj.data.breakdownCategoryOptionGroupDTOs.splice($scope.frmDetailObj.data.breakdownCategoryOptionGroupDTOs.indexOf(group), 1);
                },
                addDefault: function () {
                    dataService.getDefaultProductionItem(
                        $scope.frmDetailObj.data.breakdownCategoryID,
                        function (data) {
                            if (data.message.type === 0) {
                                var defaultValue = null;
                                if ($scope.frmDetailObj.data.breakdownCategoryOptionGroupDTOs.length > 0) {
                                    angular.forEach($scope.frmDetailObj.data.breakdownCategoryOptionGroupDTOs, function (group) {
                                        angular.forEach(data.data, function (item) {
                                            if ($scope.frmDetailObj.data.breakdownCategoryID === 10) {
                                                if (item.itemCost) {
                                                    defaultValue = item.itemCost;
                                                }
                                                else {
                                                    defaultValue = null;
                                                }
                                            }
                                            else {
                                                defaultValue = null;
                                            }

                                            $scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs.push({
                                                breakdownCategoryOptionDetailID: dataService.getIncrementId(),
                                                breakdownCategoryOptionID: $scope.frmDetailObj.data.breakdownCategoryOptionID,
                                                description: null,
                                                descriptionEN: null,
                                                productionItemID: item.productionItemID,
                                                unitID: item.unitID,
                                                quantity: 0,
                                                quantityPercent: defaultValue ? defaultValue : null,
                                                wastedRatePercent: 0,
                                                totalAmount: 0,
                                                productionItemUD: item.productionItemUD,
                                                productionItemNM: item.productionItemNM,
                                                unitNM: item.unitNM,
                                                avfPrice: 0,
                                                avtPrice: 0,
                                                thumbnailLocation: item.thumbnailLocation,
                                                fileLocation: item.fileLocation,
                                                breakdownCategoryOptionGroupID: group.breakdownCategoryOptionGroupID,
                                                productionItemTypeID: item.productionItemTypeID
                                            });
                                        });
                                    });
                                }
                                else {
                                    angular.forEach(data.data, function (item) {
                                        if ($scope.frmDetailObj.data.breakdownCategoryID === 10) {
                                            if (item.itemCost) {
                                                defaultValue = item.itemCost;
                                            }
                                            else {
                                                defaultValue = null;
                                            }
                                        }
                                        else {
                                            defaultValue = null;
                                        }

                                        $scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs.push({
                                            breakdownCategoryOptionDetailID: dataService.getIncrementId(),
                                            breakdownCategoryOptionID: $scope.frmDetailObj.data.breakdownCategoryOptionID,
                                            description: null,
                                            descriptionEN: null,
                                            productionItemID: item.productionItemID,
                                            unitID: item.unitID,
                                            quantity: 0,
                                            quantityPercent: defaultValue ? defaultValue : null,
                                            wastedRatePercent: 0,
                                            totalAmount: 0,
                                            productionItemUD: item.productionItemUD,
                                            productionItemNM: item.productionItemNM,
                                            unitNM: item.unitNM,
                                            avfPrice: 0,
                                            avtPrice: 0,
                                            thumbnailLocation: item.thumbnailLocation,
                                            fileLocation: item.fileLocation,
                                            breakdownCategoryOptionGroupID: null,
                                            productionItemTypeID: item.productionItemTypeID
                                        });
                                    });
                                }
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                        }
                    );
                },
                selectCategoryForAddingDefaultItem: function () {
                    $scope.frmDetailObj.selectedBreakdownCategoryId = $scope.frmDetailObj.data.breakdownCategoryID;
                    $('#popupBreakdownCategory').modal();
                },
                addDefaultByOptionCategory: function () {
                    $('#popupBreakdownCategory').modal('hide');
                    dataService.getDefaultProductionItem(
                        $scope.frmDetailObj.selectedBreakdownCategoryId,
                        function (data) {
                            if (data.message.type === 0) {
                                var defaultValue = null;
                                if ($scope.frmDetailObj.data.breakdownCategoryOptionGroupDTOs.length > 0) {
                                    angular.forEach($scope.frmDetailObj.data.breakdownCategoryOptionGroupDTOs, function (group) {
                                        angular.forEach(data.data, function (item) {
                                            if ($scope.frmDetailObj.data.breakdownCategoryID === 10) {
                                                if (item.itemCost) {
                                                    defaultValue = item.itemCost;
                                                }
                                                else {
                                                    defaultValue = null;
                                                }
                                            }
                                            else {
                                                defaultValue = null;
                                            }

                                            $scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs.push({
                                                breakdownCategoryOptionDetailID: dataService.getIncrementId(),
                                                breakdownCategoryOptionID: $scope.frmDetailObj.data.breakdownCategoryOptionID,
                                                description: null,
                                                descriptionEN: null,
                                                productionItemID: item.productionItemID,
                                                unitID: item.unitID,
                                                quantity: 0,
                                                quantityPercent: defaultValue ? defaultValue : null,
                                                wastedRatePercent: 0,
                                                totalAmount: 0,
                                                productionItemUD: item.productionItemUD,
                                                productionItemNM: item.productionItemNM,
                                                unitNM: item.unitNM,
                                                avfPrice: 0,
                                                avtPrice: 0,
                                                thumbnailLocation: item.thumbnailLocation,
                                                fileLocation: item.fileLocation,
                                                breakdownCategoryOptionGroupID: group.breakdownCategoryOptionGroupID,
                                                productionItemTypeID: item.productionItemTypeID
                                            });
                                        });
                                    });
                                }
                                else {
                                    angular.forEach(data.data, function (item) {
                                        if ($scope.frmDetailObj.data.breakdownCategoryID === 10) {
                                            if (item.itemCost) {
                                                defaultValue = item.itemCost;
                                            }
                                            else {
                                                defaultValue = null;
                                            }
                                        }
                                        else {
                                            defaultValue = null;
                                        }

                                        $scope.frmDetailObj.data.breakdownCategoryOptionDetailDTOs.push({
                                            breakdownCategoryOptionDetailID: dataService.getIncrementId(),
                                            breakdownCategoryOptionID: $scope.frmDetailObj.data.breakdownCategoryOptionID,
                                            description: null,
                                            descriptionEN: null,
                                            productionItemID: item.productionItemID,
                                            unitID: item.unitID,
                                            quantity: 0,
                                            quantityPercent: defaultValue ? defaultValue : null,
                                            wastedRatePercent: 0,
                                            totalAmount: 0,
                                            productionItemUD: item.productionItemUD,
                                            productionItemNM: item.productionItemNM,
                                            unitNM: item.unitNM,
                                            avfPrice: 0,
                                            avtPrice: 0,
                                            thumbnailLocation: item.thumbnailLocation,
                                            fileLocation: item.fileLocation,
                                            breakdownCategoryOptionGroupID: null,
                                            productionItemTypeID: item.productionItemTypeID
                                        });
                                    });
                                }
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                        }
                    );
                }
            }
        };

        //
        // detail form
        //
        $scope.frmDetailViewObj = {
            data: null,
            totalExcludeManagementFeeData: {
                total: 0,
                totalUSD: 0,
                totalAVT: 0,
                totalUSDAVT: 0
            },
            method: {
                getSubTotalAmount: function () {
                    var result = parseFloat(0);
                    var groupQnt = 1;
                    var groupObj = null;
                    if ($scope.frmDetailViewObj.data) {
                        angular.forEach($scope.frmDetailViewObj.data.breakdownCategoryOptionDetailDTOs, function (item) {
                            groupObj = null;
                            groupQnt = 1;
                            if (item.breakdownCategoryOptionGroupID !== null) {
                                groupObj = $scope.frmDetailViewObj.method.getGroupObj(item.breakdownCategoryOptionGroupID);
                                if (groupObj) {
                                    groupQnt = parseInt(groupObj.quantity);
                                }
                            }
                            if (!groupQnt) {
                                groupQnt = 1;
                            }

                            if ($scope.frmDetailViewObj.data.breakdownCategoryID !== 10) {
                                if (item.totalAmount) {
                                    result += groupQnt * parseFloat(item.totalAmount);
                                }
                                else {
                                    if (item.quantity) {
                                        if (item.wastedRatePercent) {
                                            result += groupQnt * parseFloat(($scope.frmDetailViewObj.data.companyID === 1) ? item.avfPrice : item.avtPrice) * parseFloat(item.quantity) * (1 + jsHelper.round(parseFloat(item.wastedRatePercent) / 100, 2));
                                        }
                                        else {
                                            result += groupQnt * parseFloat(($scope.frmDetailViewObj.data.companyID === 1) ? item.avfPrice : item.avtPrice) * parseFloat(item.quantity);
                                        }
                                    }
                                }
                            }
                            else {
                                if (item.quantityPercent) {
                                    result += groupQnt * (($scope.frmDetailViewObj.data.companyID === 1) ? $scope.frmDetailViewObj.totalExcludeManagementFeeData.total : $scope.frmDetailViewObj.totalExcludeManagementFeeData.totalAVT) * item.quantityPercent / 100;
                                }
                            }
                        });
                    }
                    return result;
                },
                getSubTotalAmountUSD: function () {
                    var result = parseFloat(0);
                    var groupQnt = 1;
                    var groupObj = null;
                    if ($scope.frmDetailViewObj.data) {
                        angular.forEach($scope.frmDetailViewObj.data.breakdownCategoryOptionDetailDTOs, function (item) {
                            groupObj = null;
                            groupQnt = 1;
                            if (item.breakdownCategoryOptionGroupID !== null) {
                                groupObj = $scope.frmDetailViewObj.method.getGroupObj(item.breakdownCategoryOptionGroupID);
                                if (groupObj) {
                                    groupQnt = parseInt(groupObj.quantity);
                                }
                            }
                            if (!groupQnt) {
                                groupQnt = 1;
                            }

                            if ($scope.frmDetailViewObj.data.breakdownCategoryID !== 10) {
                                if (item.totalAmount) {
                                    result += groupQnt * jsHelper.round(parseFloat(item.totalAmount) / $scope.exRate, 2);
                                }
                                else {
                                    if (item.quantity) {
                                        if (item.wastedRatePercent) {
                                            result += groupQnt * jsHelper.round(parseFloat(($scope.frmDetailViewObj.data.companyID === 1) ? item.avfPrice : item.avtPrice) * parseFloat(item.quantity) * (1 + jsHelper.round(parseFloat(item.wastedRatePercent) / 100, 2)) / $scope.exRate, 2);
                                        }
                                        else {
                                            result += groupQnt * jsHelper.round(parseFloat(($scope.frmDetailViewObj.data.companyID === 1) ? item.avfPrice : item.avtPrice) * parseFloat(item.quantity) / $scope.exRate, 2);
                                        }
                                    }
                                }
                            }
                            else {
                                if (item.quantityPercent) {
                                    result += groupQnt * jsHelper.round((($scope.frmDetailViewObj.data.companyID === 1) ? $scope.frmDetailViewObj.totalExcludeManagementFeeData.totalUSD : $scope.frmDetailViewObj.totalExcludeManagementFeeData.totalUSDAVT) * item.quantityPercent / 100, 2);
                                }
                            }
                        });
                    }
                    return result;
                },
                getTotalPercentManagement: function () {
                    var result = parseFloat(0);
                    if ($scope.frmDetailViewObj.data && $scope.frmDetailViewObj.data.breakdownCategoryOptionDetailDTOs && $scope.frmDetailViewObj.data.breakdownCategoryID === 10) {
                        angular.forEach($scope.frmDetailViewObj.data.breakdownCategoryOptionDetailDTOs, function (item) {
                            if (item.quantityPercent) {
                                result += parseFloat(item.quantityPercent);
                            }
                        });
                    }
                    return result;
                },

                getGroupObj: function (id) {
                    var result = null;
                    if ($scope.frmDetailViewObj.data) {
                        angular.forEach($scope.frmDetailViewObj.data.breakdownCategoryOptionGroupDTOs, function (item) {
                            if (item.breakdownCategoryOptionGroupID === id) {
                                result = item;
                            }
                        });
                    }
                    return result;
                },

                getSubTotalAmountByGroup: function (id) {
                    var result = parseFloat(0);
                    var groupQnt = 1;
                    var groupObj = $scope.frmDetailViewObj.method.getGroupObj(id);
                    if (!groupObj) return 0;

                    if ($scope.frmDetailViewObj.data) {
                        groupQnt = groupObj.quantity;
                        if (!groupQnt) {
                            groupQnt = 1;
                        }

                        angular.forEach($scope.frmDetailViewObj.data.breakdownCategoryOptionDetailDTOs, function (item) {
                            if (item.breakdownCategoryOptionGroupID === id) {
                                if ($scope.frmDetailViewObj.data.breakdownCategoryID !== 10) {
                                    if (item.totalAmount) {
                                        result += groupQnt * parseFloat(item.totalAmount);
                                    }
                                    else {
                                        if (item.quantity) {
                                            if (item.wastedRatePercent) {
                                                result += groupQnt * parseFloat(($scope.frmDetailViewObj.data.companyID === 1) ? item.avfPrice : item.avtPrice) * parseFloat(item.quantity) * (1 + jsHelper.round(parseFloat(item.wastedRatePercent) / 100, 2));
                                            }
                                            else {
                                                result += groupQnt * parseFloat(($scope.frmDetailViewObj.data.companyID === 1) ? item.avfPrice : item.avtPrice) * parseFloat(item.quantity);
                                            }
                                        }
                                    }
                                }
                                else {
                                    if (item.quantityPercent) {
                                        result += groupQnt * (($scope.frmDetailViewObj.data.companyID === 1) ? $scope.frmDetailViewObj.totalExcludeManagementFeeData.total : $scope.frmDetailViewObj.totalExcludeManagementFeeData.totalAVT) * item.quantityPercent / 100;
                                    }
                                }
                            }
                        });
                    }
                    return result;
                },
                getSubTotalAmountUSDByGroup: function (id) {
                    var result = parseFloat(0);
                    var groupQnt = 1;
                    var groupObj = $scope.frmDetailViewObj.method.getGroupObj(id);
                    if (!groupObj) return 0;

                    if ($scope.frmDetailViewObj.data) {
                        groupQnt = groupObj.quantity;
                        if (!groupQnt) {
                            groupQnt = 1;
                        }

                        angular.forEach($scope.frmDetailViewObj.data.breakdownCategoryOptionDetailDTOs, function (item) {
                            if (item.breakdownCategoryOptionGroupID === id) {
                                if ($scope.frmDetailViewObj.data.breakdownCategoryID !== 10) {
                                    if (item.totalAmount) {
                                        result += groupQnt * jsHelper.round(parseFloat(item.totalAmount) / $scope.exRate, 2);
                                    }
                                    else {
                                        if (item.quantity) {
                                            if (item.wastedRatePercent) {
                                                result += groupQnt * jsHelper.round(parseFloat(($scope.frmDetailViewObj.data.companyID === 1) ? item.avfPrice : item.avtPrice) * parseFloat(item.quantity) * (1 + jsHelper.round(parseFloat(item.wastedRatePercent) / 100, 2)) / $scope.exRate, 2);
                                            }
                                            else {
                                                result += groupQnt * jsHelper.round(parseFloat(($scope.frmDetailViewObj.data.companyID === 1) ? item.avfPrice : item.avtPrice) * parseFloat(item.quantity) / $scope.exRate, 2);
                                            }
                                        }
                                    }
                                }
                                else {
                                    if (item.quantityPercent) {
                                        result += groupQnt * jsHelper.round(((($scope.frmDetailViewObj.data.companyID === 1) ? $scope.frmDetailViewObj.totalExcludeManagementFeeData.total : $scope.frmDetailViewObj.totalExcludeManagementFeeData.totalAVT) * item.quantityPercent / 100) / $scope.exRate, 2);
                                    }
                                }
                            }
                        });
                    }
                    return result;
                },
            },
            event: {
                open: function () {
                    if ($scope.frmDetailViewObj.data) {
                        $scope.frmDetailViewObj.data.isConfirmed = true;
                        jsHelper.showPopup('frmDetailView', function () { });
                        window.scrollTo(0, 0);
                    }
                    else {
                        alert('Data has not been set!');
                    }
                },
                close: function () {
                    $scope.frmDetailViewObj.data = null;
                    jsHelper.hidePopup('frmDetailView', function () { });
                }
            }
        };


        //
        // edit price form
        //
        $scope.frmEditPriceObj = {
            data: null,
            purchasingPriceList: null,
            companyId: context.companyId,
            pricePurchasing: null,
            event: {
                open: function () {                   
                    $scope.frmEditPriceObj.data = null;
                    dataService.getPrice(
                        $scope.selectedOptions,
                        context.id,
                        $scope.currentArticleCode,
                        function (data) {
                            if (data.message.type === 0) {
                                jsHelper.showPopup('frmEditPrice', function () { });
                                window.scrollTo(0, 0);

                                $scope.frmEditPriceObj.data = data.data;

                                //BIND LẠI VALUE CỦA MANAGEMENT COST 
                                var sumExcludeManagementCode = 0;
                                angular.forEach($scope.frmEditPriceObj.data.breakDownBreakdownCategoryDTOs.filter(s => s.breakdownCategoryID !== 10), function (breakDownBreakdownCategoryDTO) {
                                    angular.forEach(breakDownBreakdownCategoryDTO.breakDownPriceDefaultDTOs, function (item) {
                                        sumExcludeManagementCode = sumExcludeManagementCode + (item.quantity * item.unitPrice);
                                    }); 
                                });

                                angular.forEach($scope.frmEditPriceObj.data.breakDownBreakdownCategoryDTOs.filter(s => s.breakdownCategoryID === 10), function (breakDownBreakdownCategoryDTO) {
                                    angular.forEach(breakDownBreakdownCategoryDTO.breakDownPriceDefaultDTOs, function (item) {
                                        item.unitPrice = sumExcludeManagementCode;
                                    });
                                });                   

                                $timeout(function () {
                                    $scope.frmEditPriceObj.event.getPurchasingPrice();
                                }, 0); 
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                            $scope.frmEditPriceObj.data = null;
                        }
                    );
                },
                close: function () {
                    $scope.frmEditPriceObj.data = null;
                    jsHelper.hidePopup('frmEditPrice', function () { });
                },
                getPurchasingPrice: function () {
                    dataService.getPurchasingPrice(
                        context.id,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.frmEditPriceObj.purchasingPriceList = data.data;
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                            $scope.frmEditPriceObj.data = null;
                        }
                    );
                },
                save: function () {
                    dataService.updatePrice(
                        $scope.frmEditPriceObj.data,
                        context.id,
                        $scope.currentArticleCode,
                        function (data) {
                            if (data.message.type === 0) {
                                location.reload();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                        }
                    );
                    $scope.frmEditPriceObj.event.close();
                },
                getPriceQuotation: function () {
                    dataService.getPriceQuotation(
                        $scope.frmEditPriceObj.data,                        
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.frmEditPriceObj.data = data.data;
                                $scope.$apply();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                        }
                    );
                   // $scope.frmEditPriceObj.event.close();
                },
                updatePrice: function () {
                    for (var i = 0; i < $scope.frmEditPriceObj.data.length; i++) {
                        if ($scope.frmEditPriceObj.data[i].avfPrice === 0) {
                            if ($scope.frmEditPriceObj.method.checkProductionItemYesOrNo($scope.frmEditPriceObj.data[i].productionItemID)) {
                                $scope.frmEditPriceObj.data[i].avfPrice = $scope.frmEditPriceObj.pricePurchasing;
                            }
                        }
                    }
                }
            },
            method: {
                checkProductionItemYesOrNo: function (productionItemID) {
                    for (var i = 0; i < $scope.frmEditPriceObj.purchasingPriceList.length; i++) {
                        if ($scope.frmEditPriceObj.purchasingPriceList[i].productionItemID === productionItemID) {
                            $scope.frmEditPriceObj.pricePurchasing = $scope.frmEditPriceObj.purchasingPriceList[i].avfPrice;
                            return true;
                        }
                    }
                }
            }
        }

        //
        // factory breakdown
        //      
        $scope.factoryBreakdown = {
            factoryBreakdownData: null
            //getFactoryBreakdown: function (sampleProductID) {

            //}
        }
        $scope.popupFactoryBreakdown = factoryBreakdown;
        $scope.$on('factoryBreakdown.getFactoryBreakdown_Close', function (event, args) {
            $scope.factoryBreakdown.factoryBreakdownData = args;
        });


        //
        // angular init
        //
        $timeout(function () {
            $scope.event.init();
        }, 0);
    }]);

})();