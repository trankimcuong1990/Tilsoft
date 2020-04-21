//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngSanitize']);
//create controller
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    $scope.data = null;
    $scope.remarkData = {
        sampleQARemarkID: dataService.getIncrementId(),
        sampleProductID: context.id,
        remark: '',
        updatedBy: context.userId,
        updatorName: 'Yourself',
        updatedDate: 'Just Now',
        thumbnailLocation: context.userPhoto,
        isEditing: false,
        remarkImageDTOs: []
    };
    $scope.newRemarkData = {
        sampleQARemarkID: dataService.getIncrementId(),
        sampleProductID: context.id,
        remark: '',
        updatedBy: context.userId,
        updatorName: 'Yourself',
        updatedDate: 'Just Now',
        thumbnailLocation: context.userPhoto,
        isEditing: false,
        remarkImageDTOs: []
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getQARemarkData(
                context.id,
                function (data) {
                    if (data.message.type === 0) {
                        $scope.data = data.data.data;
                        $('#content').show();
                    }
                },
                function (error) {
                    jsHelper.processMessageEx(error.data.message);
                    $scope.data = null;
                }
            );
        },
        add: function () {
            dataService.updateQARemarkData(
                    $scope.newRemarkData.sampleQARemarkID,
                    $scope.newRemarkData,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            //window.location = context.refreshURL;
                            $scope.data.remarkDTOs.push(data.data);

                            // reset remark Data
                            $scope.newRemarkData = {
                                sampleQARemarkID: dataService.getIncrementId(),
                                sampleProductID: context.id,
                                remark: '',
                                updatedBy: context.userId,
                                updatorName: 'Yourself',
                                updatedDate: 'Just Now',
                                thumbnailLocation: context.userPhoto,
                                isEditing: false,
                                remarkImageDTOs: []
                            };
                        }
                    },
                    function (error) {
                        jsHelper.processMessageEx(error.data.message);
                    }
                );
        },
        update: function (item) {
            dataService.updateQARemarkData(
                $scope.remarkData.sampleQARemarkID,
                $scope.remarkData,
                function (data) {
                    jsHelper.processMessageEx(data.message);
                    if (data.message.type == 0) {
                        //window.location = context.refreshURL;
                        $scope.data.remarkDTOs.splice($scope.data.remarkDTOs.indexOf(item), 1);
                        $scope.data.remarkDTOs.push(data.data);
                    }
                },
                function (error) {
                    jsHelper.processMessageEx(error.data.message);
                }
            );
        },
        delete: function (item) {
            if (confirm('Are you sure?')) {
                dataService.deleteQARemarkData(
                    item.sampleQARemarkID,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            //window.location = context.refreshURL;
                            $scope.data.remarkDTOs.splice($scope.data.remarkDTOs.indexOf(item), 1);
                        }
                    },
                    function (error) {
                        jsHelper.processMessageEx(error.data.message);
                    }
                );
            }
        },
        editRemark: function (item) {
            $scope.remarkData = angular.copy(item);
            item.isEditing = true;
        },
        cancelEdit: function (item) {
            item.isEditing = false;
        },
        addFileInNewRemark: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.newRemarkData.remarkImageDTOs.push({
                        sampleQARemarkImageID: dataService.getIncrementId(),
                        fileUD: '',
                        thumbnailLocation: '',
                        fileLocation: fileUploader2.selectedFileUrl,
                        hasChanged: true,
                        newFile: fileUploader2.selectedFileName,
                        friendlyName: fileUploader2.selectedFriendlyName
                    });
                });
            };
            fileUploader2.open();
        },
        removeFileInNewRemark: function (item) {
            $scope.newRemarkData.remarkImageDTOs.splice($scope.newRemarkData.remarkImageDTOs.indexOf(item), 1);
        },
        addFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.remarkData.remarkImageDTOs.push({
                        sampleQARemarkImageID: dataService.getIncrementId(),
                        fileUD: '',
                        thumbnailLocation: '',
                        fileLocation: fileUploader2.selectedFileUrl,
                        hasChanged: true,
                        newFile: fileUploader2.selectedFileName,
                        friendlyName: fileUploader2.selectedFriendlyName
                    });
                });
            };
            fileUploader2.open();
        },
        removeFile: function (item) {
            $scope.remarkData.remarkImageDTOs.splice($scope.remarkData.remarkImageDTOs.indexOf(item), 1);
        }
    }

    $scope.method = {
        formatRemark: function (remark) {
            if (remark == null) {
                return '';
            }
            return remark.replace(/(?:\r\n|\r|\n)/g, '<br />');
        },
        getUpdatedDateValue: function (item) {
            return jsHelper.stringToDate(item.updatedDate);
        },
        isTooLate: function (item) {
            if (jsHelper.getTotalDateDiff(jsHelper.stringToDate(item.updatedDate), new Date()) <= 1) {
                return false;
            }
            return true;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);