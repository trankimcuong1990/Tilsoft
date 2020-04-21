//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ngSanitize']);
//create controller
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    $scope.data = null;

    $scope.currentReferenceImage = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getProductInfoData(
                context.id,
                function (data) {
                    if (data.message.type === 0) {
                        $timeout(function () {
                            $scope.data = data.data.data;
                            $('#content').show();

                            //
                            // add image to cache area - help loading images faster
                            //
                            $('#pnlCacheImg').append('<img src="' + $scope.data.finishedImageFileLocation + '" />');
                            angular.forEach($scope.data.referenceImageDTOs, function (item) {
                                $('#pnlCacheImg').append('<img src="' + item.fileLocation + '" />');
                            });
                        }, 10);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                dataService.updateProductInfoData(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            window.location = context.refreshURL;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Validation failed, please check the input form for error!');
            }
        },

        //
        // reference image
        //
        referenceImage_add: function () {
            $scope.currentReferenceImage = {
                sampleReferenceImageID: dataService.getIncrementId(),
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
            $scope.event.referenceImage_edit($scope.currentReferenceImage);
        },
        referenceImage_edit: function (item) {
            $scope.currentReferenceImage = angular.copy(item);
            jsHelper.showPopup('frmReferenceImage', function () { });
            window.scrollTo(0, 0);
        },
        referenceImage_edit_ok: function () {
            if ($scope.data.referenceImageDTOs.length == 0) {
                $scope.currentReferenceImage.isDefault = true;
            }
            if ($scope.currentReferenceImage.isDefault) {
                angular.forEach($scope.data.referenceImageDTOs, function (value, key) {
                    value.isDefault = false;
                });
            }

            index = jsHelper.getArrayIndex($scope.data.referenceImageDTOs, 'sampleReferenceImageID', $scope.currentReferenceImage.sampleReferenceImageID);
            if (index >= 0) {
                $scope.data.referenceImageDTOs[index] = angular.copy($scope.currentReferenceImage);
            }
            else {
                $scope.data.referenceImageDTOs.push(angular.copy($scope.currentReferenceImage));
            }
            jsHelper.hidePopup('frmReferenceImage', function () { });
        },
        referenceImage_changeImage: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.currentReferenceImage.hasChanged = true;
                    scope.currentReferenceImage.newFile = fileUploader2.selectedFileName;
                    scope.currentReferenceImage.fileLocation = fileUploader2.selectedFileUrl;
                    scope.currentReferenceImage.thumbnailLocation = fileUploader2.selectedFileUrl;
                });
            };
            fileUploader2.open();
        },
        referenceImage_edit_cancel: function () {
            jsHelper.hidePopup('frmReferenceImage', function () { });
        },
        referenceImage_markDefaul: function (item) {
            angular.forEach($scope.data.referenceImageDTOs, function (image) {
                image.isDefault = false;
            });
            item.isDefault = true;
        },
        referenceImage_delete: function (item) {
            item.isDefault = false;
            if (item.isDefault) {
                var isDefaultExist = false;
                angular.forEach($scope.data.referenceImageDTOs, function (image) {
                    if (image.sampleReferenceImageID !== item.sampleReferenceImageID && !isDefaultExist) {
                        image.isDefault = true;
                    }
                });
            }
            $scope.data.referenceImageDTOs.splice($scope.data.referenceImageDTOs.indexOf(item), 1);
        }
    };

    $scope.method = {
        formatRemark: function (remark) {
            if (remark == null) {
                return '';
            }
            return remark.replace(/(?:\r\n|\r|\n)/g, '<br />');
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);