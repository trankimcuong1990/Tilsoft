//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.factoryMaterialGroups = null;
    $scope.factoryMaterialTypes = null;
    $scope.factoryMaterialColors = null;
    $scope.units = null;
    $scope.newID = -1;

    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.factoryMaterialGroups = data.data.factoryMaterialGroups;
                    $scope.factoryMaterialTypes = data.data.factoryMaterialTypes;
                    $scope.factoryMaterialColors = data.data.factoryMaterialColors;
                    $scope.units = data.data.units;
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },
        update: function ($event) {
            $event.preventDefault();

            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryMaterialID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', context.errMsg);
            }
        },
        delete: function ($event) {
            $event.preventDefault();

            if (confirm('Are you sure you want to delete ?')) {
                jsonService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },

        addFactoryMaterialImage: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        $scope.data.factoryMaterialImages.push({
                            factoryMaterialImageID: $scope.method.getNewID(),
                            imageFile_HasChange: true,
                            imageFile_NewFile: img.filename,
                            thumbnailLocation: img.fileURL,
                            fileLocation: img.fileURL
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },

        removeFactoryMaterialImage: function ($index) {
            $scope.data.factoryMaterialImages.splice($index, 1);
        },

        // image event bottom rith
        changeImage: function ($event) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.showroomItemThumbnailImage = img.fileURL;
                        scope.data.imageFile_NewFile = img.filename;
                        scope.data.imageFile_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeImage: function ($event) {
            $scope.data.showroomItemThumbnailImage = '';
            $scope.data.imageFile_NewFile = '';
            $scope.data.imageFile_HasChange = true;
        },
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            window.location = context.refreshUrl + id  
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        }
    };

    //
    // init
    //
    $scope.event.load();
}]);