//
// SUPPORT
//
jQuery('#main-form').keypress(function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngSanitize', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', '$http', 'jsonService', function ($scope, $filter, $http, jsonService) {
    jsonService.serviceUrl = context.serviceUrl;
    jsonService.token = context.token;
    jsonService.printUrl = context.printUrl;

    // get model image
    jsonService.getModelImage = function (id, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'GET',
            url: context.supportServiceUrl + 'getmodelimage?id=' + id,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);
            iSuccessCallback(response.data);
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    // get product info
    jsonService.getProductInfo = function (code, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'GET',
            url: context.supportServiceUrl + 'getproductinfo?c=' + code,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);
            iSuccessCallback(response.data);
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    // technical drawing
    jsonService.approveTechnicalDrawing = function (id, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'approvetechnicaldrawing?id=' + id,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };
    jsonService.resetTechnicalDrawing = function (id, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'resettechnicaldrawing?id=' + id,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    // export to excel
    jsonService.getExcelPrint = function (iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.printUrl + 'getexcelprint?id=' + context.id,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);
            iSuccessCallback(response.data);
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    }

    jsonService.getModelList = function (param, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: context.serviceUrl + 'getModelList?param=' + param,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);
            iSuccessCallback(response.data);
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    }
    //
    // property
    //
    $scope.data = null;
    $scope.seasons = null;
    $scope.sampleRequestTypes = null;
    $scope.samplePurposes = null;
    $scope.sampleTransportTypes = null;
    $scope.users = null;
    $scope.subFactories = null;
    $scope.factories = null;
    $scope.developmentTypes = null;

    $scope.packagingMethods = null;
    $scope.sampleProductStatuses = null;
    $scope.sampleOrderStatuses = null;
    $scope.unitDTOs = null;
    $scope.sampleProductPackagingMaterialTypeDTOs = null;
    $scope.context = context;

    $scope.sampleProductPackagingMaterialTypeDTOs = null;
    $scope.unitDTOs = null;

    $scope.currentProduct = null;
    $scope.currentReferenceImage = null;
    $scope.currentMeetingMinute = null;
    $scope.currentRemark = null;
    $scope.currentMonitorType = null;
    $scope.currentMonitorFilter = '';
    $scope.currentSCM = null;
    $scope.listEurofar = null;
    $scope.listAVT = null;
    $scope.frmImportProductData = {
        articleCode: ''
    };
    $scope.sampleRemark = [];
    $scope.sampleProductMinute = [];

    //$scope.currentProgress = null;
    //$scope.currentRemark = null;
    //$scope.currentDrawing = null;
    //$scope.currentRef = null;
    //$scope.currentMinute = null

    $scope.selectedClient = {
        id: null,
        data: null,
    }

    $scope.otherModel = '';
    $scope.selectedModel = {
        id: null,
        data: null
    }

    $scope.otherMaterial = '';
    $scope.selectedMaterial = {
        id: null,
        data: null
    }

    $scope.otherMaterialType = '';
    $scope.selectedMaterialType = {
        id: null,
        data: null
    }

    $scope.otherMaterialColor = '';
    $scope.selectedMaterialColor = {
        id: null,
        data: null
    }

    $scope.otherSubMaterial = '';
    $scope.selectedSubMaterial = {
        id: null,
        data: null
    }

    $scope.otherSubMaterialColor = '';
    $scope.selectedSubMaterialColor = {
        id: null,
        data: null
    }

    $scope.otherFrameMaterial = '';
    $scope.selectedFrameMaterial = {
        id: null,
        data: null
    }

    $scope.otherFrameMaterialColor = '';
    $scope.selectedFrameMaterialColor = {
        id: null,
        data: null
    }

    $scope.otherBackCushion = '';
    $scope.selectedBackCushion = {
        id: null,
        data: null
    }

    $scope.otherSeatCushion = '';
    $scope.selectedSeatCushion = {
        id: null,
        data: null
    }

    $scope.otherCushionColor = '';
    $scope.selectedCushionColor = {
        id: null,
        data: null
    }

    $scope.newid = 0;
    $scope.jsonService = jsonService;

    $scope.modelList = [];
    $scope.sampleProductID = null;
    $scope.gridHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
        },
        isTriggered: false
    }

    $scope.spDevelopment = [
        { id: true, name: 'Yes' },
        { id: false, name: 'No' }
    ];

    //
    // event
    //
    $scope.event = {
        init: function () {

            jsonService.load(
                context.id,
                {},
                function (data) {
                    $scope.data = data.data.data;
                    $scope.seasons = data.data.seasons;
                    $scope.sampleRequestTypes = data.data.sampleRequestTypes;
                    $scope.samplePurposes = data.data.samplePurposes;
                    $scope.sampleTransportTypes = data.data.sampleTransportTypes;
                    $scope.sampleProductStatuses = data.data.sampleProductStatuses;
                    $scope.sampleOrderStatuses = data.data.sampleOrderStatuses;
                    $scope.unitDTOs = data.data.unitDTOs;
                    $scope.sampleProductPackagingMaterialTypeDTOs = data.data.sampleProductPackagingMaterialTypeDTOs;
                    $scope.users = [];
                    angular.forEach(data.data.users, function (value, key) {
                        $scope.users.push({
                            userId: value.userId,
                            fullName: value.fullName,
                            officeNM: value.officeNM,
                            isSelected: false,
                            isscm: value.isscm
                        });
                    }, null);

                    $scope.factories = data.data.factories;
                    $scope.packagingMethods = data.data.packagingMethods;
                    $scope.sampleProductPackagingMaterialTypeDTOs = data.data.sampleProductPackagingMaterialTypeDTOs;
                    $scope.unitDTOs = data.data.unitDTOs;
                    $scope.developmentTypes = data.data.developmentTypeDTOs;

                    $scope.subFactories = [];
                    angular.forEach(data.data.factories, function (value, key) {
                        $scope.subFactories.push({
                            factoryID: value.factoryID,
                            factoryUD: value.factoryUD,
                            isSelected: false
                        });
                    }, null);

                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                    debugger;
                    // go to page section
                    if (context.initParam) {
                        index = jsHelper.getArrayIndex($scope.data.sampleProducts, 'sampleProductID', context.initParam.pi);
                        if (index >= 0) {
                            switch (context.initParam.a) {

                                case 2:
                                    $scope.event.openTechnicalDrawing($scope.data.sampleProducts[index]);
                                    break;

                                case -1:
                                    $scope.event.editProduct($scope.data.sampleProducts[index]);
                                    break;

                            }
                        }
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.seasons = null;
                    $scope.sampleRequestTypes = null;
                    $scope.samplePurposes = null;
                    $scope.sampleTransportTypes = null;
                    $scope.sampleProductStatuses = null;
                    $scope.sampleOrderStatuses = null;
                    $scope.users = null;
                    $scope.factories = null;
                    $scope.unitDTOs = null;
                    $scope.sampleProductPackagingMaterialTypeDTOs = null;                    
                }
            );
        },

        print: function () {
            jsonService.getExcelPrint(
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        update: function () {
            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.sampleOrderID);
                        }
                    },
                    function (error) {
                        //jsHelper.showMessage('warning', error.data.message.message);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Validation failed, please check the input form for error!');
            }
        },

        delete: function ($event) {
            $event.preventDefault();
            jsonService.delete(
                context.id, null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.message.message);
                }
            );
        },

        setOrderStatus: function (id) {
            index = jsHelper.getArrayIndex($scope.sampleOrderStatuses, 'sampleOrderStatusID', id);
            if (index >= 0) {
                $scope.data.sampleOrderStatusID = id;
                $scope.data.sampleOrderStatusNM = $scope.sampleOrderStatuses[index].sampleOrderStatusNM;
            }
            else {
                $scope.data.sampleOrderStatusID = null;
                $scope.data.sampleOrderStatusNM = '';
            }
        },

        // product
        addProduct: function () {
            $scope.currentProduct = {
                sampleProductID: $scope.method.getNewID(),
                requestTypeID: null,
                etaDestination: '',
                articleDescription: '',
                overallDimL: '',
                overallDimW: '',
                overallDimH: '',
                modelID: null,
                frameMaterialID: null,
                frameMaterialColorID: null,
                materialID: null,
                materialTypeID: null,
                materialColorID: null,
                subMaterialID: null,
                subMaterialColorID: null,
                seatCushionID: null,
                seatCushionThickness: '',
                seatCushionSpecification: '',
                backCushionID: null,
                backCushionThickness: '',
                backCushionSpecification: '',
                cushionColorID: null,
                displayIndex: null,
                quantity: '',
                remark: '',
                isConfirmed: '',
                nlSuggestedFactoryID: null,
                nlSuggestedFactoryRemark: '',
                vnSuggestedFactoryID: null,
                vnSuggestedFactoryRemark: '',
                factoryDeadline: '',
                netWeight: '',
                grossWeight: '',
                loadAbility20DC: '',
                loadAbility40DC: '',
                loadAbility40HC: '',
                cartonBoxDimL: '',
                cartonBoxDimW: '',
                cartonBoxDimH: '',
                qntInBox: '',
                updatedDate: '',
                sampleProductStatusID: 1, // none
                statusUpdatedDate: '',
                materialDescription: '',
                materialTypeDescription: '',
                materialColorDescription: '',
                subMaterialDescription: '',
                subMaterialColorDescription: '',
                frameMaterialDescription: '',
                frameMaterialColorDescription: '',
                backCushionDescription: '',
                seatCushionDescription: '',
                cushionColorDescription: '',
                modelUD: '',
                modelNM: '',
                frameMaterialUD: '',
                frameMaterialNM: '',
                frameMaterialColorUD: '',
                frameMaterialColorNM: '',
                materialUD: '',
                materialNM: '',
                materialTypeUD: '',
                materialTypeNM: '',
                materialColorUD: '',
                materialColorNM: '',
                subMaterialUD: '',
                subMaterialNM: '',
                subMaterialColorUD: '',
                subMaterialColorNM: '',
                seatCushionUD: '',
                seatCushionNM: '',
                backCushionUD: '',
                backCushionNM: '',
                cushionColorUD: '',
                cushionColorNM: '',
                requestTypeNM: '',
                nlSuggestedFactoryUD: '',
                vnSuggestedFactoryUD: '',
                thumbnailLocation: '',
                fileLocation: '',
                updatorName: '',
                frameWeight: '',
                wickerWeight: '',
                remarkPaperWrap: '',
                packagingOption: '',
                isPaperWrap: false,
                isCartonBox: false,
                sampleProductStatusNM: 'CREATED',
                statusUpdatorName: '',
                sampleProductMinutes: [],
                sampleReferenceImages: [],
                sampleProgresses: [],
                sampleTechnicalDrawings: [],
                sampleRemarks: [],
                sampleProductSubFactories: [],
                samplePackagings: [],
                sampleProductDimensionDTOs: [],
                sampleProductPackagingMaterialDTOs: []

            };
            $scope.event.editProduct($scope.currentProduct);
        },
        editProduct: function (item) {
            $scope.currentProduct = JSON.parse(JSON.stringify(item));
            var isCheck = parseInt($scope.currentProduct.packagingOption);
            if (isCheck === 1) {
                $scope.currentProduct.isCartonBox = true;
            }
            if (isCheck === 2) {
                $scope.currentProduct.isPaperWrap = true;
            }

            // nếu chua có dữ liệu sampleProductPackagingMaterialDTOs của sameplePrduct 
            // thì add danh mục vào trước khi nhập liệu
            if ($scope.currentProduct.sampleProductPackagingMaterialDTOs.length == 0) {
                angular.forEach($scope.sampleProductPackagingMaterialTypeDTOs, function (value, key) {
                    $scope.currentProduct.sampleProductPackagingMaterialDTOs.push({
                        sampleProductPackagingMaterialID: $scope.method.getNewID(),
                        sampleProductPackagingMaterialTypeID: value.sampleproductPackagingMaterialTypeID,
                        sampleProductID: $scope.currentProduct.sampleProductID
                    });
                }, null);
            }

            jsHelper.showPopup('frmProduct', function () { });
            window.scrollTo(0, 0);
        },
        removeProduct: function (item) {
            if (confirm('Remove the current product?')) {
                $scope.data.sampleProducts.splice($scope.data.sampleProducts.indexOf(item), 1);
            }
        },
        setProductStatus: function (id, currentProduct) {
            index = jsHelper.getArrayIndex($scope.sampleProductStatuses, 'sampleProductStatusID', id);
            if (index >= 0) {
                $scope.currentProduct.sampleProductStatusID = id;
                $scope.currentProduct.sampleProductStatusNM = $scope.sampleProductStatuses[index].sampleProductStatusNM;
                $scope.currentProduct.statusUpdatedBy = context.userId;
                $scope.currentProduct.statusUpdatedDate = jsHelper.getCurrentDateTime();

                if (id == 10) { // sample rejected
                    alert('Please fill in the remark form to let others know why you reject the sample!');
                    $scope.event.addRemark();
                }

                if (id !== 9) { // completed
                    currentProduct.isEurofarSampleCollection = false;
                }
            }
            else {
                $scope.currentProduct.sampleProductStatusID = null;
                $scope.currentProduct.sampleProductStatusNM = '';
            }
        },

        // technical drawing
        openTechnicalDrawing: function (product) {
            $scope.currentProduct = product;
            jsHelper.showPopup('frmTechnicalDrawing', function () { });
            window.scrollTo(0, 0);
        },
        toggleTechnicalDrawingArchive: function (item) {
            jQuery('#archive-' + item.sampleTechnicalDrawingID).toggle();
        },
        approveTechnicalDrawing: function (item) {
            if (item.sampleTechnicalDrawingID > 0 && confirm('Approve the TD?')) {
                jsonService.approveTechnicalDrawing(
                    item.sampleTechnicalDrawingID,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            window.location = context.refreshUrl + context.id + '?param=pi:' + item.sampleProductID + ',a:2';
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        resetTechnicalDrawing: function (item) {
            if (item.sampleTechnicalDrawingID > 0 && confirm('Reset the TD to un-confirmed')) {
                jsonService.resetTechnicalDrawing(
                    item.sampleTechnicalDrawingID,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            window.location = context.refreshUrl + context.id + '?param=pi:' + item.sampleProductID + ',a:2';
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        closeTechnicalDrawing: function () {
            jsHelper.hidePopup('frmTechnicalDrawing', function () { });
            $scope.method.goToSection('product-' + $scope.currentProduct.sampleProductID);
        },

        // progress
        openProgress: function (product) {
            $scope.currentProduct = product;
            jsHelper.showPopup('frmProgress', function () { });
            window.scrollTo(0, 0);
        },
        closeProgress: function () {
            jsHelper.hidePopup('frmProgress', function () { });
            $scope.method.goToSection('product-' + $scope.currentProduct.sampleProductID);
        },

        // qa remark
        openQARemark: function (product) {
            $scope.currentProduct = product;
            jsHelper.showPopup('frmQARemark', function () { });
            window.scrollTo(0, 0);
        },
        closeQARemark: function () {
            jsHelper.hidePopup('frmQARemark', function () { });
            $scope.method.goToSection('product-' + $scope.currentProduct.sampleProductID);
        },

        // reference image
        addReferenceImage: function () {
            $scope.currentReferenceImage = {
                sampleReferenceImageID: $scope.method.getNewID(),
                fileUD: '',
                hasChanged: false,
                newFile: '',
                bringInBy: '',
                bringInDate: '',
                remark: '',
                isDefault: false,
                thumbnailLocation: '',
                fileLocation: ''
            };
            $scope.event.editReferenceImage($scope.currentReferenceImage);
        },
        editReferenceImage: function (item) {
            $scope.currentReferenceImage = JSON.parse(JSON.stringify(item));
            jQuery('#frmReferenceImage').modal('show');
        },
        updateReferenceImage: function () {
            if ($scope.currentProduct.sampleReferenceImages.length == 0) {
                $scope.currentReferenceImage.isDefault = true;
            }
            if ($scope.currentReferenceImage.isDefault) {
                angular.forEach($scope.currentProduct.sampleReferenceImages, function (value, key) {
                    value.isDefault = false;
                }, null);
                $scope.currentProduct.thumbnailLocation = $scope.currentReferenceImage.thumbnailLocation;
                $scope.currentProduct.fileLocation = $scope.currentReferenceImage.fileLocation;
            }

            index = jsHelper.getArrayIndex($scope.currentProduct.sampleReferenceImages, 'sampleReferenceImageID', $scope.currentReferenceImage.sampleReferenceImageID);
            if (index >= 0) {
                $scope.currentProduct.sampleReferenceImages[index] = JSON.parse(JSON.stringify($scope.currentReferenceImage));
            }
            else {
                $scope.currentProduct.sampleReferenceImages.push(JSON.parse(JSON.stringify($scope.currentReferenceImage)));
            }
            jQuery('#frmReferenceImage').modal('hide');
        },
        removeReferenceImage: function (item) {
            $scope.currentProduct.sampleReferenceImages.splice($scope.currentProduct.sampleReferenceImages.indexOf(item), 1);
            if ($scope.currentProduct.sampleReferenceImages.length == 0) {
                alert('asdasd');
                $scope.currentProduct.thumbnailLocation = $scope.currentProduct.modelThumbnailLocation;
                $scope.currentProduct.fileLocation = $scope.currentProduct.modelFileLocation;
            }
        },
        uploadReferenceImage: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.currentReferenceImage.fileLocation = fileUploader2.selectedFileUrl;
                    scope.currentReferenceImage.thumbnailLocation = fileUploader2.selectedFileUrl;
                    scope.currentReferenceImage.hasChanged = true;
                    scope.currentReferenceImage.newFile = fileUploader2.selectedFileName;
                });
            };
            fileUploader2.onhide = function () {
                jQuery('#frmReferenceImage').modal('show');
            };
            jQuery('#frmReferenceImage').modal('hide');
            fileUploader2.open();
        },

        // meeting minute
        addMeetingMinute: function () {
            $scope.currentMeetingMinute = {
                sampleProductMinuteID: $scope.method.getNewID(),
                meetingMinute: '',
                updatedDate: 'Just Now',
                updatorName: 'Yourself',
                sampleProductMinuteImages: []
            };
            $scope.event.editMeetingMinute($scope.currentMeetingMinute);
        },
        editMeetingMinute: function (item) {
            $scope.currentMeetingMinute = JSON.parse(JSON.stringify(item));
            jQuery('#frmMeetingMinute').modal('show');
        },
        updateMeetingMinute: function () {
            index = jsHelper.getArrayIndex($scope.currentProduct.sampleProductMinutes, 'sampleProductMinuteID', $scope.currentMeetingMinute.sampleProductMinuteID);
            if (index >= 0) {
                $scope.currentProduct.sampleProductMinutes[index] = JSON.parse(JSON.stringify($scope.currentMeetingMinute));
            }
            else {
                $scope.currentProduct.sampleProductMinutes.push(JSON.parse(JSON.stringify($scope.currentMeetingMinute)));
            }
            jQuery('#frmMeetingMinute').modal('hide');
        },
        removeMeetingMinute: function (item) {
            $scope.currentProduct.sampleProductMinutes.splice($scope.currentProduct.sampleProductMinutes.indexOf(item), 1);
        },
        addMeetingMinuteImage: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.currentMeetingMinute.sampleProductMinuteImages.push({
                        sampleProductMinuteImageID: $scope.method.getNewID(),
                        fileUD: '',
                        thumbnailLocation: '',
                        fileLocation: fileUploader2.selectedFileUrl,
                        hasChanged: true,
                        newFile: fileUploader2.selectedFileName,
                        friendlyName: fileUploader2.selectedFriendlyName
                    });
                });
            };
            fileUploader2.onhide = function () {
                jQuery('#frmMeetingMinute').modal('show');
            };
            jQuery('#frmMeetingMinute').modal('hide');
            fileUploader2.open();
        },
        removeMeetingMinuteImage: function (item) {
            $scope.currentMeetingMinute.sampleProductMinuteImages.splice($scope.currentMeetingMinute.sampleProductMinuteImages.indexOf(item), 1);
        },

        // remark
        addRemark: function () {
            $scope.currentRemark = {
                sampleRemarkID: $scope.method.getNewID(),
                remark: '',
                updatedDate: 'Just Now',
                updatorName: 'Yourself',
                sampleRemarkImages: []
            };
            $scope.event.editRemark($scope.currentRemark);
        },
        editRemark: function (item) {
            $scope.currentRemark = JSON.parse(JSON.stringify(item));
            jQuery('#frmRemark').modal('show');
        },
        updateRemark: function () {
            index = jsHelper.getArrayIndex($scope.currentProduct.sampleRemarks, 'sampleRemarkID', $scope.currentRemark.sampleRemarkID);
            if (index >= 0) {
                $scope.currentProduct.sampleRemarks[index] = JSON.parse(JSON.stringify($scope.currentRemark));
            }
            else {
                $scope.currentProduct.sampleRemarks.push(JSON.parse(JSON.stringify($scope.currentRemark)));
            }
            jQuery('#frmRemark').modal('hide');
        },
        removeRemark: function (item) {
            $scope.currentProduct.sampleRemarks.splice($scope.currentProduct.sampleRemarks.indexOf(item), 1);
        },
        addRemarkImage: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.currentRemark.sampleRemarkImages.push({
                        sampleRemarkImageID: $scope.method.getNewID(),
                        fileUD: '',
                        thumbnailLocation: '',
                        fileLocation: fileUploader2.selectedFileUrl,
                        hasChanged: true,
                        newFile: fileUploader2.selectedFileName,
                        friendlyName: fileUploader2.selectedFriendlyName
                    });
                });
            };
            fileUploader2.onhide = function () {
                jQuery('#frmRemark').modal('show');
            };
            jQuery('#frmRemark').modal('hide');
            fileUploader2.open();
        },
        removeRemarkImage: function (item) {
            $scope.currentRemark.sampleRemarkImages.splice($scope.currentRemark.sampleRemarkImages.indexOf(item), 1);
        },

        // technical drawing
        addTechnicalDrawing: function () {
            $scope.currentMeetingMinute = {
                sampleProductMinuteID: $scope.method.getNewID(),
                meetingMinute: '',
                updatedDate: 'Just Now',
                updatorName: 'Yourself',
                sampleProductMinuteImages: []
            };
            $scope.event.editMeetingMinute($scope.currentMeetingMinute);
        },
        editTechnicalDrawing: function (item) {
            $scope.currentMeetingMinute = JSON.parse(JSON.stringify(item));
            jQuery('#frmMeetingMinute').modal('show');
        },
        updateTechnicalDrawing: function () {
            index = jsHelper.getArrayIndex($scope.currentProduct.sampleProductMinutes, 'sampleProductMinuteID', $scope.currentMeetingMinute.sampleProductMinuteID);
            if (index >= 0) {
                $scope.currentProduct.sampleProductMinutes[index] = JSON.parse(JSON.stringify($scope.currentMeetingMinute));
            }
            else {
                $scope.currentProduct.sampleProductMinutes.push(JSON.parse(JSON.stringify($scope.currentMeetingMinute)));
            }
            jQuery('#frmMeetingMinute').modal('hide');
        },
        removeTechnicalDrawing: function (item) {
            $scope.currentProduct.sampleProductMinutes.splice($scope.currentProduct.sampleProductMinutes.indexOf(item), 1);
        },
        addTechnicalDrawingPreview: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.currentMeetingMinute.sampleProductMinuteImages.push({
                        sampleProductMinuteImageID: $scope.method.getNewID(),
                        fileUD: '',
                        thumbnailLocation: fileUploader2.selectedFileUrl,
                        fileLocation: fileUploader2.selectedFileUrl,
                        hasChanged: true,
                        newFile: fileUploader2.selectedFileName
                    });
                });
            };
            fileUploader2.onhide = function () {
                jQuery('#frmMeetingMinute').modal('show');
            };
            jQuery('#frmMeetingMinute').modal('hide');
            fileUploader2.open();
        },
        removeTechnicalDrawingPreview: function (item) {
            $scope.currentMeetingMinute.sampleProductMinuteImages.splice($scope.currentMeetingMinute.sampleProductMinuteImages.indexOf(item), 1);
        },
        addTechnicalDrawingSource: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.currentMeetingMinute.sampleProductMinuteImages.push({
                        sampleProductMinuteImageID: $scope.method.getNewID(),
                        fileUD: '',
                        thumbnailLocation: fileUploader2.selectedFileUrl,
                        fileLocation: fileUploader2.selectedFileUrl,
                        hasChanged: true,
                        newFile: fileUploader2.selectedFileName
                    });
                });
            };
            fileUploader2.onhide = function () {
                jQuery('#frmMeetingMinute').modal('show');
            };
            jQuery('#frmMeetingMinute').modal('hide');
            fileUploader2.open();
        },
        removeTechnicalDrawingSource: function (item) {
            $scope.currentMeetingMinute.sampleProductMinuteImages.splice($scope.currentMeetingMinute.sampleProductMinuteImages.indexOf(item), 1);
        },

        // monitor
        addMonitor: function (type) {
            $scope.listSCM = [];
            angular.forEach($scope.users, function (value, key) {
                value.isSelected = false;
            }, null);
            $scope.currentMonitorType = type;

            if (type === 1) {
                $scope.currentMonitorFilter = 'VN Team';
                $scope.listAVT = $scope.users.filter(x => x.officeNM !== 'Eurofar');
                $scope.listEurofar = null;
                $scope.listSCM = null;
            }

            if (type === 2) {
                $scope.currentMonitorFilter = 'Eurofar';
                $scope.listEurofar = $scope.users.filter(x => x.officeNM === 'Eurofar');
                $scope.listAVT = null;
                $scope.listSCM = null;
            }

            if (type === 3) {
                $scope.isscm = true;
                $scope.currentMonitorFilter = '';
                $scope.listSCM = $scope.users.filter(x => x.isscm === true);
                $scope.listAVT = null;
                $scope.listEurofar = null;
            }

            jQuery('#frmMonitor').modal('show');
        },
        updateMonitor: function () {
            angular.forEach($scope.users, function (value, key) {
                if (value.isSelected) {
                    $scope.data.sampleMonitors.push({
                        sampleMonitorID: $scope.method.getNewID(),
                        userID: value.userId,
                        sampleMonitorGroupID: $scope.currentMonitorType,
                        fullName: value.fullName,
                        officeNM: value.officeNM
                    });
                }
            }, null);

            jQuery('#frmMonitor').modal('hide');
        },
        removeMonitor: function (item) {
            $scope.data.sampleMonitors.splice($scope.data.sampleMonitors.indexOf(item), 1);
        },

        // form import product
        frmImportProduct_open: function () {
            $scope.frmImportProductData.articleCode = '';
            jQuery('#frmImportProduct').modal('show');
        },
        frmImportProduct_cmdImport_click: function () {
            // get product info
            jsonService.getProductInfo(
                $scope.frmImportProductData.articleCode,
                function (data) {
                    $scope.currentProduct.articleDescription = data.modelNM;
                    $scope.currentProduct.overallDimL = data.overallDimL;
                    $scope.currentProduct.overallDimW = data.overallDimW;
                    $scope.currentProduct.overallDimH = data.overallDimH;
                    $scope.currentProduct.modelID = data.modelID;
                    $scope.currentProduct.frameMaterialID = data.frameMaterialID;
                    $scope.currentProduct.frameMaterialColorID = data.frameMaterialColorID;
                    $scope.currentProduct.materialID = data.materialID;
                    $scope.currentProduct.materialTypeID = data.materialTypeID;
                    $scope.currentProduct.materialColorID = data.materialColorID;
                    $scope.currentProduct.subMaterialID = data.subMaterialID;
                    $scope.currentProduct.subMaterialColorID = data.subMaterialColorID;
                    $scope.currentProduct.seatCushionID = data.seatCushionID;
                    $scope.currentProduct.seatCushionSpecification = data.cushionRemark;
                    $scope.currentProduct.backCushionID = data.backCushionID;
                    $scope.currentProduct.cushionColorID = data.cushionColorID;
                    $scope.currentProduct.vnSuggestedFactoryID = data.factoryID;
                    $scope.currentProduct.netWeight = data.netWeight;
                    $scope.currentProduct.grossWeight = data.grossWeight;
                    $scope.currentProduct.loadAbility20DC = data.loadAbility20DC;
                    $scope.currentProduct.loadAbility40DC = data.loadAbility40DC;
                    $scope.currentProduct.loadAbility40HC = data.loadAbility40HC;
                    $scope.currentProduct.cartonBoxDimL = data.cartonBoxDimL;
                    $scope.currentProduct.cartonBoxDimW = data.cartonBoxDimW;
                    $scope.currentProduct.cartonBoxDimH = data.cartonBoxDimH;
                    $scope.currentProduct.qntInBox = data.qntInBox;
                    $scope.currentProduct.materialDescription = data.materialNM;
                    $scope.currentProduct.materialTypeDescription = data.materialTypeNM;
                    $scope.currentProduct.materialColorDescription = data.materialColorNM;
                    $scope.currentProduct.subMaterialDescription = data.subMaterialNM;
                    $scope.currentProduct.subMaterialColorDescription = data.subMaterialColorNM;
                    $scope.currentProduct.frameMaterialDescription = data.frameMaterialNM;
                    $scope.currentProduct.frameMaterialColorDescription = data.frameMaterialColorNM;
                    $scope.currentProduct.backCushionDescription = data.backCushionNM;
                    $scope.currentProduct.seatCushionDescription = data.seatCushionNM;
                    $scope.currentProduct.cushionColorDescription = data.cushionColorNM;
                    $scope.currentProduct.modelUD = data.modelUD;
                    $scope.currentProduct.modelNM = data.modelNM;
                    $scope.currentProduct.frameMaterialUD = data.frameMaterialUD;
                    $scope.currentProduct.frameMaterialColorUD = data.frameMaterialColorUD;
                    $scope.currentProduct.materialUD = data.materialUD;
                    $scope.currentProduct.materialTypeUD = data.materialTypeUD;
                    $scope.currentProduct.materialColorUD = data.materialColorUD;
                    $scope.currentProduct.subMaterialUD = data.subMaterialUD;
                    $scope.currentProduct.subMaterialColorUD = data.subMaterialColorUD;
                    $scope.currentProduct.seatCushionUD = data.seatCushionUD;
                    $scope.currentProduct.seatCushionNM = data.seatCushionNM;
                    $scope.currentProduct.backCushionUD = data.backCushionUD;
                    $scope.currentProduct.backCushionNM = data.backCushionNM;
                    $scope.currentProduct.cushionColorUD = data.cushionColorUD;
                    $scope.currentProduct.cushionColorNM = data.cushionColorNM;
                    $scope.currentProduct.vnSuggestedFactoryUD = data.factoryUD;
                    $scope.currentProduct.thumbnailLocation = data.thumbnailLocation;
                    $scope.currentProduct.fileLocation = data.fileLocation;
                    $scope.currentProduct.isDevelopment = data.isDevelopment;

                    jQuery('#frmImportProduct').modal('hide');
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        //
        // POPUP FORM EVENT
        //

        // client
        searchClient: function () {
            $scope.selectedClient.id = null;
            $scope.selectedClient.data = null;
            jQuery('#txtSearchClient').val('');
            jQuery('#frmSearchClient').modal('show');
        },
        clearClient: function () {
            $scope.data.clientID = null;
            $scope.data.clientUD = '';
        },

        // model
        searchModel: function () {
            $scope.selectedModel.id = null;
            $scope.selectedModel.data = null;
            jQuery('#txtSearchModel').val('');
            jQuery('#frmSearchModel').modal('show');
        },
        clearModel: function () {
            $scope.currentProduct.articleDescription = '';
            $scope.currentProduct.modelID = null;
            $scope.currentProduct.modelUD = '';
        },
        otherModel: function () {
            $scope.otherModel = '';
            jQuery('#frmOtherModel').modal('show');
        },

        // material
        searchMaterial: function () {
            $scope.selectedMaterial.id = null;
            $scope.selectedMaterial.data = null;
            jQuery('#txtSearchMaterial').val('');
            jQuery('#frmSearchMaterial').modal('show');
        },
        clearMaterial: function () {
            $scope.currentProduct.materialID = null;
            $scope.currentProduct.materialUD = '';
            $scope.currentProduct.materialDescription = '';
        },
        otherMaterial: function () {
            $scope.otherMaterial = '';
            jQuery('#frmOtherMaterial').modal('show');
        },
        selectOtherMaterial: function () {
            if (!$scope.otherMaterial) {
                alert('Please provide the name for the material!');
                return;
            }
            $scope.currentProduct.materialID = null;
            $scope.currentProduct.materialUD = '';
            $scope.currentProduct.materialDescription = $scope.otherMaterial;
            jQuery('#frmOtherMaterial').modal('hide');
        },

        // material type
        searchMaterialType: function () {
            $scope.selectedMaterialType.id = null;
            $scope.selectedMaterialType.data = null;
            jQuery('#txtSearchMaterialType').val('');
            jQuery('#frmSearchMaterialType').modal('show');
        },
        clearMaterialType: function () {
            $scope.currentProduct.materialTypeID = null;
            $scope.currentProduct.materialTypeUD = '';
            $scope.currentProduct.materialTypeDescription = '';
        },
        otherMaterialType: function () {
            $scope.otherMaterialType = '';
            jQuery('#frmOtherMaterialType').modal('show');
        },
        selectOtherMaterialType: function () {
            if (!$scope.otherMaterialType) {
                alert('Please provide the name for the material type!');
                return;
            }
            $scope.currentProduct.materialTypeID = null;
            $scope.currentProduct.materialTypeUD = '';
            $scope.currentProduct.materialTypeDescription = $scope.otherMaterialType;
            jQuery('#frmOtherMaterialType').modal('hide');
        },

        // material color
        searchMaterialColor: function () {
            $scope.selectedMaterialColor.id = null;
            $scope.selectedMaterialColor.data = null;
            jQuery('#txtSearchMaterialColor').val('');
            jQuery('#frmSearchMaterialColor').modal('show');
        },
        clearMaterialColor: function () {
            $scope.currentProduct.materialColorID = null;
            $scope.currentProduct.materialColorUD = '';
            $scope.currentProduct.materialColorDescription = '';
        },
        otherMaterialColor: function () {
            $scope.otherMaterialColor = '';
            jQuery('#frmOtherMaterialColor').modal('show');
        },
        selectOtherMaterialColor: function () {
            if (!$scope.otherMaterialColor) {
                alert('Please provide the name for the material color!');
                return;
            }
            $scope.currentProduct.materialColorID = null;
            $scope.currentProduct.materialColorUD = '';
            $scope.currentProduct.materialColorDescription = $scope.otherMaterialColor;
            jQuery('#frmOtherMaterialColor').modal('hide');
        },


        // material 2
        searchMaterial2: function () {
            $scope.selectedMaterial.id = null;
            $scope.selectedMaterial.data = null;
            jQuery('#txtSearchMaterial2').val('');
            jQuery('#frmSearchMaterial2').modal('show');
        },
        clearMaterial2: function () {
            $scope.currentProduct.material2ID = null;
            $scope.currentProduct.material2UD = '';
            $scope.currentProduct.material2Description = '';
        },
        otherMaterial2: function () {
            $scope.otherMaterial = '';
            jQuery('#frmOtherMaterial2').modal('show');
        },
        selectOtherMaterial2: function () {
            if (!$scope.otherMaterial) {
                alert('Please provide the name for the material 2!');
                return;
            }
            $scope.currentProduct.material2ID = null;
            $scope.currentProduct.material2UD = '';
            $scope.currentProduct.material2Description = $scope.otherMaterial;
            jQuery('#frmOtherMaterial2').modal('hide');
        },

        // material 2 type
        searchMaterial2Type: function () {
            $scope.selectedMaterialType.id = null;
            $scope.selectedMaterialType.data = null;
            jQuery('#txtSearchMaterial2Type').val('');
            jQuery('#frmSearchMaterial2Type').modal('show');
        },
        clearMaterial2Type: function () {
            $scope.currentProduct.material2TypeID = null;
            $scope.currentProduct.material2TypeUD = '';
            $scope.currentProduct.material2TypeDescription = '';
        },
        otherMaterial2Type: function () {
            $scope.otherMaterialType = '';
            jQuery('#frmOtherMaterial2Type').modal('show');
        },
        selectOtherMaterial2Type: function () {
            if (!$scope.otherMaterialType) {
                alert('Please provide the name for the material 2 type!');
                return;
            }
            $scope.currentProduct.material2TypeID = null;
            $scope.currentProduct.material2TypeUD = '';
            $scope.currentProduct.material2TypeDescription = $scope.otherMaterialType;
            jQuery('#frmOtherMaterial2Type').modal('hide');
        },

        // material 2 color
        searchMaterial2Color: function () {
            $scope.selectedMaterialColor.id = null;
            $scope.selectedMaterialColor.data = null;
            jQuery('#txtSearchMaterial2Color').val('');
            jQuery('#frmSearchMaterial2Color').modal('show');
        },
        clearMaterial2Color: function () {
            $scope.currentProduct.material2ColorID = null;
            $scope.currentProduct.material2ColorUD = '';
            $scope.currentProduct.material2ColorDescription = '';
        },
        otherMaterial2Color: function () {
            $scope.otherMaterialColor = '';
            jQuery('#frmOtherMaterial2Color').modal('show');
        },
        selectOtherMaterial2Color: function () {
            if (!$scope.otherMaterialColor) {
                alert('Please provide the name for the material 2 color!');
                return;
            }
            $scope.currentProduct.material2ColorID = null;
            $scope.currentProduct.material2ColorUD = '';
            $scope.currentProduct.material2ColorDescription = $scope.otherMaterialColor;
            jQuery('#frmOtherMaterial2Color').modal('hide');
        },


        // material 3
        searchMaterial3: function () {
            $scope.selectedMaterial.id = null;
            $scope.selectedMaterial.data = null;
            jQuery('#txtSearchMaterial3').val('');
            jQuery('#frmSearchMaterial3').modal('show');
        },
        clearMaterial3: function () {
            $scope.currentProduct.material3ID = null;
            $scope.currentProduct.material3UD = '';
            $scope.currentProduct.material3Description = '';
        },
        otherMaterial3: function () {
            $scope.otherMaterial = '';
            jQuery('#frmOtherMaterial3').modal('show');
        },
        selectOtherMaterial3: function () {
            if (!$scope.otherMaterial) {
                alert('Please provide the name for the material 3!');
                return;
            }
            $scope.currentProduct.material3ID = null;
            $scope.currentProduct.material3UD = '';
            $scope.currentProduct.material3Description = $scope.otherMaterial;
            jQuery('#frmOtherMaterial3').modal('hide');
        },

        // material 3 type
        searchMaterial3Type: function () {
            $scope.selectedMaterialType.id = null;
            $scope.selectedMaterialType.data = null;
            jQuery('#txtSearchMaterial3Type').val('');
            jQuery('#frmSearchMaterial3Type').modal('show');
        },
        clearMaterial3Type: function () {
            $scope.currentProduct.material3TypeID = null;
            $scope.currentProduct.material3TypeUD = '';
            $scope.currentProduct.material3TypeDescription = '';
        },
        otherMaterial3Type: function () {
            $scope.otherMaterialType = '';
            jQuery('#frmOtherMaterial3Type').modal('show');
        },
        selectOtherMaterial3Type: function () {
            if (!$scope.otherMaterialType) {
                alert('Please provide the name for the material 3 type!');
                return;
            }
            $scope.currentProduct.material3TypeID = null;
            $scope.currentProduct.material3TypeUD = '';
            $scope.currentProduct.material3TypeDescription = $scope.otherMaterialType;
            jQuery('#frmOtherMaterial3Type').modal('hide');
        },

        // material 3 color
        searchMaterial3Color: function () {
            $scope.selectedMaterialColor.id = null;
            $scope.selectedMaterialColor.data = null;
            jQuery('#txtSearchMaterial3Color').val('');
            jQuery('#frmSearchMaterial3Color').modal('show');
        },
        clearMaterial3Color: function () {
            $scope.currentProduct.material3ColorID = null;
            $scope.currentProduct.material3ColorUD = '';
            $scope.currentProduct.material3ColorDescription = '';
        },
        otherMaterial3Color: function () {
            $scope.otherMaterialColor = '';
            jQuery('#frmOtherMaterial3Color').modal('show');
        },
        selectOtherMaterial3Color: function () {
            if (!$scope.otherMaterialColor) {
                alert('Please provide the name for the material 3 color!');
                return;
            }
            $scope.currentProduct.material3ColorID = null;
            $scope.currentProduct.material3ColorUD = '';
            $scope.currentProduct.material3ColorDescription = $scope.otherMaterialColor;
            jQuery('#frmOtherMaterial3Color').modal('hide');
        },







        // sub material
        searchSubMaterial: function () {
            $scope.selectedSubMaterial.id = null;
            $scope.selectedSubMaterial.data = null;
            jQuery('#txtSearchSubMaterial').val('');
            jQuery('#frmSearchSubMaterial').modal('show');
        },
        clearSubMaterial: function () {
            $scope.currentProduct.subMaterialID = null;
            $scope.currentProduct.subMaterialUD = '';
            $scope.currentProduct.subMaterialDescription = '';
        },
        otherSubMaterial: function () {
            $scope.otherSubMaterial = '';
            jQuery('#frmOtherSubMaterial').modal('show');
        },
        selectOtherSubMaterial: function () {
            if (!$scope.otherSubMaterial) {
                alert('Please provide the name for the sub material!');
                return;
            }
            $scope.currentProduct.subMaterialID = null;
            $scope.currentProduct.subMaterialUD = '';
            $scope.currentProduct.subMaterialDescription = $scope.otherSubMaterial;
            jQuery('#frmOtherSubMaterial').modal('hide');
        },

        // sub material color
        searchSubMaterialColor: function () {
            $scope.selectedSubMaterialColor.id = null;
            $scope.selectedSubMaterialColor.data = null;
            jQuery('#txtSearchSubMaterialColor').val('');
            jQuery('#frmSearchSubMaterialColor').modal('show');
        },
        clearSubMaterialColor: function () {
            $scope.currentProduct.subMaterialColorID = null;
            $scope.currentProduct.subMaterialColorUD = '';
            $scope.currentProduct.subMaterialColorDescription = '';
        },
        otherSubMaterialColor: function () {
            $scope.otherSubMaterialColor = '';
            jQuery('#frmOtherSubMaterialColor').modal('show');
        },
        selectOtherSubMaterialColor: function () {
            if (!$scope.otherSubMaterialColor) {
                alert('Please provide the name for the sub material color!');
                return;
            }
            $scope.currentProduct.subMaterialColorID = null;
            $scope.currentProduct.subMaterialColorUD = '';
            $scope.currentProduct.subMaterialColorDescription = $scope.otherSubMaterialColor;
            jQuery('#frmOtherSubMaterialColor').modal('hide');
        },

        // frame material
        searchFrameMaterial: function () {
            $scope.selectedFrameMaterial.id = null;
            $scope.selectedFrameMaterial.data = null;
            jQuery('#txtSearchFrameMaterial').val('');
            jQuery('#frmSearchFrameMaterial').modal('show');
        },
        clearFrameMaterial: function () {
            $scope.currentProduct.frameMaterialID = null;
            $scope.currentProduct.frameMaterialUD = '';
            $scope.currentProduct.frameMaterialDescription = '';
        },
        otherFrameMaterial: function () {
            $scope.otherFrameMaterial = '';
            jQuery('#frmOtherFrameMaterial').modal('show');
        },
        selectOtherFrameMaterial: function () {
            if (!$scope.otherFrameMaterial) {
                alert('Please provide the name for the frame material!');
                return;
            }
            $scope.currentProduct.frameMaterialID = null;
            $scope.currentProduct.frameMaterialUD = '';
            $scope.currentProduct.frameMaterialDescription = $scope.otherFrameMaterial;
            jQuery('#frmOtherFrameMaterial').modal('hide');
        },

        // frame material color
        searchFrameMaterialColor: function () {
            $scope.selectedFrameMaterialColor.id = null;
            $scope.selectedFrameMaterialColor.data = null;
            jQuery('#txtSearchFrameMaterialColor').val('');
            jQuery('#frmSearchFrameMaterialColor').modal('show');
        },
        clearFrameMaterialColor: function () {
            $scope.currentProduct.frameMaterialColorID = null;
            $scope.currentProduct.frameMaterialColorUD = '';
            $scope.currentProduct.frameMaterialColorDescription = '';
        },
        otherFrameMaterialColor: function () {
            $scope.otherFrameMaterialColor = '';
            jQuery('#frmOtherFrameMaterialColor').modal('show');
        },
        selectOtherFrameMaterialColor: function () {
            if (!$scope.otherFrameMaterialColor) {
                alert('Please provide the name for the frame material color!');
                return;
            }
            $scope.currentProduct.frameMaterialColorID = null;
            $scope.currentProduct.frameMaterialColorUD = '';
            $scope.currentProduct.frameMaterialColorDescription = $scope.otherFrameMaterialColor;
            jQuery('#frmOtherFrameMaterialColor').modal('hide');
        },

        // back cushion
        searchBackCushion: function () {
            $scope.selectedBackCushion.id = null;
            $scope.selectedBackCushion.data = null;
            jQuery('#txtSearchBackCushion').val('');
            jQuery('#frmSearchBackCushion').modal('show');
        },
        clearBackCushion: function () {
            $scope.currentProduct.backCushionID = null;
            $scope.currentProduct.backCushionUD = '';
            $scope.currentProduct.backCushionDescription = '';
        },
        otherBackCushion: function () {
            $scope.otherBackCushion = '';
            jQuery('#frmOtherBackCushion').modal('show');
        },
        selectOtherBackCushion: function () {
            if (!$scope.otherBackCushion) {
                alert('Please provide the name for the frame material color!');
                return;
            }
            $scope.currentProduct.backCushionID = null;
            $scope.currentProduct.backCushionUD = '';
            $scope.currentProduct.backCushionDescription = $scope.otherBackCushion;
            jQuery('#frmOtherBackCushion').modal('hide');
        },

        // seat cushion
        searchSeatCushion: function () {
            $scope.selectedSeatCushion.id = null;
            $scope.selectedSeatCushion.data = null;
            jQuery('#txtSearchSeatCushion').val('');
            jQuery('#frmSearchSeatCushion').modal('show');
        },
        clearSeatCushion: function () {
            $scope.currentProduct.seatCushionID = null;
            $scope.currentProduct.seatCushionUD = '';
            $scope.currentProduct.seatCushionDescription = '';
        },
        otherSeatCushion: function () {
            $scope.otherSeatCushion = '';
            jQuery('#frmOtherSeatCushion').modal('show');
        },
        selectOtherSeatCushion: function () {
            if (!$scope.otherSeatCushion) {
                alert('Please provide the name for the frame material color!');
                return;
            }
            $scope.currentProduct.seatCushionID = null;
            $scope.currentProduct.seatCushionUD = '';
            $scope.currentProduct.seatCushionDescription = $scope.otherSeatCushion;
            jQuery('#frmOtherSeatCushion').modal('hide');
        },

        // cushion color
        searchCushionColor: function () {
            $scope.selectedCushionColor.id = null;
            $scope.selectedCushionColor.data = null;
            jQuery('#txtSearchCushionColor').val('');
            jQuery('#frmSearchCushionColor').modal('show');
        },
        clearCushionColor: function () {
            $scope.currentProduct.cushionColorID = null;
            $scope.currentProduct.cushionColorUD = '';
            $scope.currentProduct.cushionColorDescription = '';
        },
        otherCushionColor: function () {
            $scope.otherCushionColor = '';
            jQuery('#frmOtherCushionColor').modal('show');
        },
        selectOtherCushionColor: function () {
            if (!$scope.otherCushionColor) {
                alert('Please provide the name for the frame material color!');
                return;
            }
            $scope.currentProduct.cushionColorID = null;
            $scope.currentProduct.cushionColorUD = '';
            $scope.currentProduct.cushionColorDescription = $scope.otherCushionColor;
            jQuery('#frmOtherCushionColor').modal('hide');
        },

        openModelList: function (articleDescription, id) {
            jsonService.getModelList(
                articleDescription,
                function (data) {
                    $scope.sampleProductID = id;
                    $scope.modelList = data.data;

                    jQuery('#frmModelList').modal();
                },
                function (error) {
                });
        },

        checkPackaginOptionCartonBox: function () {

            if ($scope.currentProduct.isCartonBox === true) {
                $scope.currentProduct.isPaperWrap = false;
                $scope.currentProduct.packagingOption = "1";
            }

        },

        checkPackaginOptionPaperWrap: function () {
            if ($scope.currentProduct.isPaperWrap === true) {
                $scope.currentProduct.isCartonBox = false;
                $scope.currentProduct.packagingOption = "2";
            }
        },

        // Packaging
        editPackaging: function (item) {
            if (item.samplePackagings.length === 0) {
                if (item.cartonBoxDimH !== "" || item.cartonBoxDimL !== "" || item.cartonBoxDimW !== "") {
                    $scope.currentProduct.samplePackagings.push({
                        samplePackagingID: $scope.method.getNewID(),
                        cartonBoxDimH: item.cartonBoxDimH,
                        cartonBoxDimL: item.cartonBoxDimL,
                        cartonBoxDimW: item.cartonBoxDimW
                    });
                }

            }
            jQuery('#frmPackagingDetail').modal('show');
        },

        addNewPackaging: function () {
            $scope.currentProduct.samplePackagings.push({
                samplePackagingID: $scope.method.getNewID()
            });
        },

        removePackaging: function (item) {
            var index = $scope.currentProduct.samplePackagings.indexOf(item);
            $scope.currentProduct.samplePackagings.splice(index, 1);
        },
        
        okfrmPackaging: function () {
            jQuery('#frmPackagingDetail').modal('hide');
        },

        //SampleProductDimension
        removeSampleProductDimension: function ($event, id) {
            if ($event !== null) {
                $event.preventDefault();
            }
            var isExist = false;
            for (var j = 0; j < $scope.currentProduct.sampleProductDimensionDTOs.length; j++) {
                if ($scope.currentProduct.sampleProductDimensionDTOs[j].sampleProductDimensionID === id) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.currentProduct.sampleProductDimensionDTOs.splice(j, 1);
            }
        },
        addSampleProductDimension: function($event) {
            if ($event !== null) {
                $event.preventDefault();
            }
            $scope.currentProduct.sampleProductDimensionDTOs.push({
                sampleProductDimensionID: $scope.method.getNewID()
            });
        },

        //SampleProductCartonBox
        removeSampleProductCartonBox: function ($event, id) {
            if ($event !== null) {
                $event.preventDefault();
            }
            var isExist = false;
            for (var j = 0; j < $scope.currentProduct.sampleProductCartonBoxDTOs.length; j++) {
                if ($scope.currentProduct.sampleProductCartonBoxDTOs[j].sampleProductCartonBoxID === id) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.currentProduct.sampleProductCartonBoxDTOs.splice(j, 1);
            }
        },
        addSampleProductCartonBox: function ($event) {
            if ($event !== null) {
                $event.preventDefault();
            }
            $scope.currentProduct.sampleProductCartonBoxDTOs.push({
                sampleProductCartonBoxID: $scope.method.getNewID()
            });
        },

         //popup Sample Remark
        openSampleRemark: function (sampleRemarkID) {
            angular.forEach($scope.data.sampleProducts, function (item) {
                angular.forEach(item.sampleRemarks, function (sItem) {
                    if (sItem.sampleRemarkID === sampleRemarkID) {
                        $scope.sampleRemark = sItem;
                    }
                });
            });

            jQuery("#frmSampleRemark").modal();
        },

        //popup Sample Product Minute
        openSampleProductMinute: function (sampleProductMinuteID) {
            angular.forEach($scope.data.sampleProducts, function (item) {
                angular.forEach(item.sampleProductMinutes, function (sItem) {
                    if (sItem.sampleProductMinuteID === sampleProductMinuteID) {
                        $scope.sampleProductMinute = sItem;
                    }
                });
            });

            jQuery("#frmSampleProductMinute").modal();
        },

        changeEurofarSampleCollection: function (sampleProductDTO) {
            sampleProductDTO.isEurofarSampleCollection = true;
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
        getNewID: function () {
            $scope.newid--;
            return $scope.newid;
        },
        formatRemark: function (remark) {
            if (remark == null) {
                return '';
            }
            return remark.replace(/(?:\r\n|\r|\n)/g, '<br />');
        },
        formatMeetingMinute: function (remark) {
            if (remark == null) {
                return '';
            }
            return remark.replace(/(?:\r\n|\r|\n)/g, '<br />');
        },
        goToSection: function (id) {
            window.location.hash = id;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);

$(document).ready(function () {
    $('.gallery').featherlightGallery({
        gallery: {
            fadeIn: 300,
            fadeOut: 300,
            next: 'next »',
            previous: '« previous'
        },
        openSpeed: 300,
        closeSpeed: 300
    });
});