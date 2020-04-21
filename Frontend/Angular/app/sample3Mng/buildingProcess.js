//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngSanitize']);
//create controller
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    $scope.data = null;
    $scope.progressData = {
        sampleProgressID: dataService.getIncrementId(),
        sampleProductID: context.id,
        remark: '',
        updatedBy: context.userId, 
        updatorName: 'Yourself',
        updatedDate: 'Just Now',
        thumbnailLocation: context.userPhoto,
        isEditing: false,
        progressImageDTOs: []
    };
    $scope.newProgressData = {
        sampleProgressID: dataService.getIncrementId(),
        sampleProductID: context.id,
        remark: '',
        updatedBy: context.userId,
        updatorName: 'Yourself',
        updatedDate: 'Just Now',
        thumbnailLocation: context.userPhoto,
        isEditing: false,
        progressImageDTOs: []
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getBuildingProcessData(
                context.id,
                function (data) {
                    if (data.message.type === 0) {
                        $scope.data = data.data.data;
                        $('#content').show();

                        //
                        // add image to cache area - help loading images faster
                        //
                        $('#pnlCacheImg').append('<img src="' + $scope.data.fileLocation + '" />');
                        angular.forEach($scope.data.progressDTOs, function (item) {
                            angular.forEach(item.progressImageDTOs, function (image) {
                                $('#pnlCacheImg').append('<img src="' + image.fileLocation + '" />');
                            });
                        });
                    }
                },
                function (error) {
                    jsHelper.processMessageEx(error.data.message);
                    $scope.data = null;
                }
            );
        },
        add: function () {
            dataService.updateBuildingProcessData(
                    $scope.newProgressData.sampleProgressID,
                    $scope.newProgressData,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            //window.location = context.refreshURL;
                            $scope.data.progressDTOs.push(data.data);

                            // reset remark Data
                            $scope.newProgressData = {
                                sampleProgressID: dataService.getIncrementId(),
                                sampleProductID: context.id,
                                remark: '',
                                updatedBy: context.userId,
                                updatorName: 'Yourself',
                                updatedDate: 'Just Now',
                                thumbnailLocation: context.userPhoto,
                                isEditing: false,
                                progressImageDTOs: []
                            };
                        }
                    },
                    function (error) {
                        jsHelper.processMessageEx(error.data.message);
                    }
                );
        },
        update: function (item) {
            dataService.updateBuildingProcessData(
                $scope.progressData.sampleProgressID,
                $scope.progressData,
                function (data) {
                    jsHelper.processMessageEx(data.message);
                    if (data.message.type == 0) {
                        //window.location = context.refreshURL;
                        $scope.data.progressDTOs.splice($scope.data.progressDTOs.indexOf(item), 1);
                        $scope.data.progressDTOs.push(data.data);
                    }
                },
                function (error) {
                    jsHelper.processMessageEx(error.data.message);
                }
            );
        },
        delete: function (item) {
            if (confirm('Are you sure?')) {
                dataService.deleteBuildingProcessData(
                    item.sampleProgressID,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            //window.location = context.refreshURL;
                            $scope.data.progressDTOs.splice($scope.data.progressDTOs.indexOf(item), 1);
                        }
                    },
                    function (error) {
                        jsHelper.processMessageEx(error.data.message);
                    }
                );
            }
        },
        editProgress: function (item) {
            $scope.progressData = angular.copy(item);
            item.isEditing = true;
        },
        cancelEdit: function (item) {
            item.isEditing = false;
        },
        addFileInNewProgress: function () {
            imageMultipleUploader.callback = function () {
                scope.$apply(function () {
                    angular.forEach(imageMultipleUploader.selectedFiles, function (value, key) {
                        scope.newProgressData.progressImageDTOs.push({
                            sampleProgressImageID: dataService.getIncrementId(),
                            fileUD: '',
                            thumbnailLocation: value.fileURL,
                            fileLocation: value.fileURL,
                            hasChanged: true,
                            newFile: value.filename
                        });
                    }, null);
                });
            };
            imageMultipleUploader.open();
        },
        removeFileInNewProgress: function (item) {
            $scope.newProgressData.progressImageDTOs.splice($scope.newProgressData.progressImageDTOs.indexOf(item), 1);
        },
        addFile: function () {
            imageMultipleUploader.callback = function () {
                scope.$apply(function () {
                    angular.forEach(imageMultipleUploader.selectedFiles, function (value, key) {
                        scope.progressData.progressImageDTOs.push({
                            sampleProgressImageID: dataService.getIncrementId(),
                            fileUD: '',
                            thumbnailLocation: value.fileURL,
                            fileLocation: value.fileURL,
                            hasChanged: true,
                            newFile: value.filename
                        });
                    }, null);
                });
            };
            imageMultipleUploader.open();
        },
        removeFile: function (item) {
            $scope.progressData.progressImageDTOs.splice($scope.progressData.progressImageDTOs.indexOf(item), 1);
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