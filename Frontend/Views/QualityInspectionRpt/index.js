function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.value,
            description: item.label
        }
    });
};

(function () {
    "use strict";

    angular.module("tilsoftApp", ["avs-directives"]).controller("tilsoftController", qualityInspectionController);
    qualityInspectionController.$inject = ["$scope", "$timeout", "dataService"];

    function qualityInspectionController($scope, $timeout, qualityInspectionService) {
        $scope.editFormData = null;
        $scope.searchFormData = [];
        $scope.supportQIType = [];
        $scope.supportQICorrectAction = [];

        $scope.editQITypeData = null;
        $scope.searchQITypeData = [];

        $scope.editQICorrectActionData = null;
        $scope.searchQICorrectActionData = [];

        $scope.editQIDefaultSettingData = null;
        $scope.searchQIDefaultSettingData = [];

        $scope.supportWorkCenter = [];
        $scope.supportSupplier = [];
        $scope.supportWickerColor = [];
        $scope.supportResult = [
            { 'qualityInspectionResultID': 1, 'qualityInspectionResultNM': 'Passed' },
            { 'qualityInspectionResultID': 2, 'qualityInspectionResultNM': 'Rejected' }
        ];

        $scope.initEmployee = null;
        $scope.itemEmployee = {
            id: null,
            label: null,
            description: null,
        };
        $scope.apiEmployee = {
            url: context.supportServiceUrl + 'getuserprofiles',
            token: context.token,
        };

        $scope.gridHandler = {
            loadMore: function () {
            },
            sort: function (sortedBy, sortedDirection) {
            },
            isTriggered: false
        };

        $scope.event = {
            init: function () {
                qualityInspectionService.serviceUrl = context.serviceUrl;
                qualityInspectionService.token = context.token;

                qualityInspectionService.getInit(
                    function (data) {
                        $scope.supportSupplier = data.data.supportSupplier;
                        $scope.supportQIType = data.data.supportQualityInspectionType;
                        $scope.supportQICorrectAction = data.data.supportQualityInspectionCorrectAction;

                        $scope.event.load();
                    },
                    function (error) {
                    });
            },
            load: function () {
                qualityInspectionService.getQualityInspection(
                    context.id,
                    context.workOrderID,
                    context.clientID,
                    context.productionItemID,
                    context.workCenterNM,
                    function (data) {
                        $scope.editFormData = data.data.editData;
                        $scope.searchFormData = data.data.searchData;
                        $scope.supportWickerColor = data.data.supportWickerColor;

                        var index = -1;
                        for (var i = 0; i < $scope.editFormData.qualityInspectionCategories.length; i++) {
                            var eItem = $scope.editFormData.qualityInspectionCategories[i];
                            eItem.qualityInspectionCategoryID = index;

                            index--;
                        }

                        jQuery("#content").show();
                    },
                    function (error) {
                    });
            },
            search: function () {

            },
            update: function () {
                qualityInspectionService.updateQualityInspection(
                    $scope.editFormData,
                    function (data) {
                        $scope.editFormData = data.data.editData;
                        $scope.searchFormData = data.data.searchData;

                        jQuery("#workCenterQuantity").val(null);
                    },
                    function (error) {
                    });
            },
            edit: function (id) {
                qualityInspectionService.getQualityInspection(
                    id,
                    $scope.editFormData.workOrderID,
                    $scope.editFormData.clientID,
                    $scope.editFormData.productionItemID,
                    context.workCenterNM,
                    function (data) {
                        $scope.editFormData = data.data.editData;
                        $scope.searchFormData = data.data.searchData;

                        jQuery("#content").show();
                    },
                    function (error) {
                    });
            },
            clear: function () {
                qualityInspectionService.getQualityInspection(
                    context.id,
                    context.workOrderID,
                    context.clientID,
                    context.productionItemID,
                    context.workCenterNM,
                    function (data) {
                        $scope.editFormData = data.data.editData;
                        $scope.searchFormData = data.data.searchData;
                        $scope.supportWickerColor = data.data.supportWickerColor;

                        var index = -1;
                        for (var i = 0; i < $scope.editFormData.qualityInspectionCategories.length; i++) {
                            var eItem = $scope.editFormData.qualityInspectionCategories[i];
                            eItem.qualityInspectionCategoryID = index;

                            index--;
                        }

                        jQuery("#workCenterQuantity").val(null);
                        jQuery("#content").show();
                    },
                    function (error) {
                    });
            },
            delete: function (id, workOrderID, clientID, productionItemID, workCenterNM) {
                qualityInspectionService.deleteQualityInspection(
                    id,
                    workOrderID,
                    clientID,
                    productionItemID,
                    workCenterNM,
                    function (data) {
                        $scope.editFormData = data.data.editData;
                        $scope.searchFormData = data.data.searchData;

                        jQuery("#workCenterQuantity").val(null);
                        jQuery("#content").show();
                    },
                    function (error) {
                    });
            },

            openConfigure: function () {
                qualityInspectionService.getQualityInspectionDefaultSetting(
                    function (data) {
                        $scope.editQIDefaultSettingData = data.data.defaultSettingData;
                        $scope.searchQIDefaultSettingData = data.data.defaultSettingList;
                        $scope.supportWorkCenter = data.data.supportWorkCenter;

                        jQuery("#frmDefaultSetting").modal();
                    },
                    function (error) {
                    });
            },
            updateConfigure: function () {
                if ($scope.editQIDefaultSettingData.qualityInspectionDefaultSection !== null && $scope.editQIDefaultSettingData.qualityInspectionDefaultSection !== "") {
                    qualityInspectionService.updateQualityInspectionDefaultSetting(
                        $scope.editQIDefaultSettingData,
                        function (data) {
                            $scope.editQIDefaultSettingData = data.data.defaultSettingData;
                            $scope.searchQIDefaultSettingData = data.data.defaultSettingList;
                        },
                        function (error) {
                        });
                }
            },
            editConfigure: function (item) {
                $scope.editQIDefaultSettingData = {
                    qualityInspectionDefaultSettingID: item.qualityInspectionDefaultSettingID,
                    qualityInspectionDefaultSection: item.qualityInspectionDefaultSection,
                    description: item.description
                };
            },
            clearConfigure: function () {
                $scope.editQIDefaultSettingData = {
                    qualityInspectionDefaultSettingID: 0,
                    qualityInspectionDefaultSection: null,
                    description: null
                };
            },
            deleteConfigure: function (item) {
                qualityInspectionService.deleteQualityInspectionDefaultSetting(
                    item,
                    function (data) {
                        $scope.editQIDefaultSettingData = data.data.defaultSettingData;
                        $scope.searchQIDefaultSettingData = data.data.defaultSettingList;
                    },
                    function (error) {
                    });
            },
            closeConfigure: function () {
                if ($scope.editFormData.qualityInspectionCategories.length > 0) {
                    $scope.editFormData.qualityInspectionCategories = [];
                }

                for (var i = 0; i < $scope.searchQIDefaultSettingData.length; i++) {
                    var eItem = $scope.searchQIDefaultSettingData[i];
                    var nItem = {
                        qualityInspectionCategoryID: -1,
                        qualityInspectionID: $scope.editFormData.qualityInspectionID,
                        qualityInpsectionResultID: null,
                        description: eItem.description,
                        qualityInpsectionCategoryImages: [],
                    }

                    $scope.editFormData.qualityInspectionCategories.push(nItem);
                }

                jQuery("#frmDefaultSetting").modal("hide");
            },

            openType: function () {
                qualityInspectionService.getQualityInspectionType(
                    function (data) {
                        $scope.editQITypeData = data.data.typeData;
                        $scope.searchQITypeData = data.data.typeList;

                        jQuery("#frmTypeSetting").modal();
                    },
                    function (error) {
                    });
            },
            updateType: function () {
                if ($scope.editQITypeData.qualityInspectionTypeUD !== null && $scope.editQITypeData.qualityInspectionTypeUD !== "") {
                    qualityInspectionService.updateQualityInspectionType(
                        $scope.editQITypeData,
                        function (data) {
                            $scope.editQITypeData = data.data.typeData;
                            $scope.searchQITypeData = data.data.typeList;
                        },
                        function (error) {
                        });
                }
            },
            editType: function (item) {
                $scope.editQITypeData = {
                    qualityInspectionTypeID: item.qualityInspectionTypeID,
                    qualityInspectionTypeUD: item.qualityInspectionTypeUD,
                    qualityInspectionTypeNM: item.qualityInspectionTypeNM
                };
            },
            clearType: function () {
                $scope.editQITypeData = {
                    qualityInspectionTypeID: 0,
                    qualityInspectionTypeUD: null,
                    qualityInspectionTypeNM: null
                };
            },
            deleteType: function (item) {
                qualityInspectionService.deleteQualityInspectionType(
                    item,
                    function (data) {
                        $scope.editQITypeData = data.data.typeData;
                        $scope.searchQITypeData = data.data.typeList;
                    },
                    function (error) {
                    });
            },
            closeType: function () {
                jQuery("#frmTypeSetting").modal("hide");
                $scope.supportQIType = $scope.searchQITypeData;
            },

            openCorrectAction: function () {
                qualityInspectionService.getQualityInspectionCorrectAction(
                    function (data) {
                        $scope.editQICorrectActionData = data.data.correctActionData;
                        $scope.searchQICorrectActionData = data.data.correctActionList;

                        jQuery("#frmCorrectActionSetting").modal();
                    },
                    function (error) {
                    });
            },
            updateCorrectAction: function () {
                if ($scope.editQICorrectActionData.qualityInspectionCorrectActionUD !== null && $scope.editQICorrectActionData.qualityInspectionCorrectActionUD !== "") {
                    qualityInspectionService.updateQualityInspectionCorrectAction(
                        $scope.editQICorrectActionData,
                        function (data) {
                            $scope.editQICorrectActionData = data.data.correctActionData;
                            $scope.searchQICorrectActionData = data.data.correctActionList;
                        },
                        function (error) {
                        });
                }
            },
            editCorrectAction: function (item) {
                $scope.editQICorrectActionData = {
                    qualityInspectionCorrectActionID: item.qualityInspectionCorrectActionID,
                    qualityInspectionCorrectActionUD: item.qualityInspectionCorrectActionUD,
                    qualityInspectionCorrectActionNM: item.qualityInspectionCorrectActionNM
                };
            },
            clearCorrectAction: function () {
                $scope.editQICorrectActionData = {
                    qualityInspectionCorrectActionID: 0,
                    qualityInspectionCorrectActionUD: null,
                    qualityInspectionCorrectActionNM: null
                };
            },
            deleteCorrectAction: function (item) {
                qualityInspectionService.deleteQualityInspectionCorrectAction(
                    item,
                    function (data) {
                        $scope.editQICorrectActionData = data.data.correctActionData;
                        $scope.searchQICorrectActionData = data.data.correctActionList;
                    },
                    function (error) {
                    });
            },
            closeCorrectAction: function () {
                jQuery("#frmCorrectActionSetting").modal("hide");
                $scope.supportQICorrectAction = $scope.searchQICorrectActionData;
            },

            selectQCName: function (itemEmployee) {
                if (itemEmployee !== undefined && itemEmployee !== null) {
                    $scope.editFormData.qcid = itemEmployee.id;
                }
            },
            selectTeamLeaderName: function (itemEmployee) {
                if (itemEmployee !== undefined && itemEmployee !== null) {
                    $scope.editFormData.teamLeaderID = itemEmployee.id;
                }
            },
            selectProductionSupervisorName: function (itemEmployee) {
                if (itemEmployee !== undefined && itemEmployee !== null) {
                    $scope.editFormData.productionSupervisorID = itemEmployee.id;
                }
            },

            addImage: function (subItem) {
                var newId = -1;
                subItem.qualityInpsectionCategoryImages.push({
                    qualityInspectionCategoryImageID: newId,
                    qualityInspectionCategoryID: subItem.qualityInspectionCategoryID,
                    fileUD: null,
                    hasChange: false,
                    newFile: '',
                    fileLocation: '',
                    thumbnailLocation: '',
                    friendlyName: ''
                });
                $scope.event.changeImage(newId, subItem);
            },
            changeImage: function (id, subItem) {
                var imageObject = null;
                jQuery.each(subItem.qualityInpsectionCategoryImages, function () {
                    if (this.qualityInspectionCategoryImageID == id) {
                        imageObject = this;
                    }
                });

                if (imageObject && imageObject != '' && typeof imageObject != undefined) {
                    masterUploader.multiSelect = true;
                    masterUploader.imageOnly = true;
                    masterUploader.callback = function () {
                        var scope = angular.element(jQuery('body')).scope();
                        scope.$apply(function () {
                            angular.forEach(masterUploader.selectedFiles, function (img) {
                                imageObject.fileLocation = img.fileURL;
                                imageObject.newFile = img.filename;
                                imageObject.hasChange = true;
                                imageObject.friendlyName = img.filename;
                            }, null);
                        });
                    };
                    masterUploader.open();
                }
            },
            removeImage: function (id, subItem) {
                var isExist = false;
                for (var j = 0; j < subItem.qualityInpsectionCategoryImages.length; j++) {
                    if (subItem.qualityInpsectionCategoryImages[j].qualityInspectionCategoryImageID == id) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    subItem.qualityInpsectionCategoryImages.splice(j, 1);
                }
            },
        };

        $scope.method = {

        };

        $timeout(function () {
            $scope.event.init();
        }, 10);
    }
})();