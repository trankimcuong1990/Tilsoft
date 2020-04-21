//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.newID = -1;

    $scope.topleft_imagedata = null;
    $scope.topright_imagedata = null;
    $scope.bottomleft_imagedata = null;
    $scope.bottomright_imagedata = null;
    $scope.map_imagedata = null;

    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;

                    $scope.topleft_imagedata = $scope.data.showroomAreaImages.filter(function (o) { return o.areaImageUD == 'TL' })[0];
                    $scope.topright_imagedata = $scope.data.showroomAreaImages.filter(function (o) { return o.areaImageUD == 'TR' })[0];
                    $scope.bottomleft_imagedata = $scope.data.showroomAreaImages.filter(function (o) { return o.areaImageUD == 'BL' })[0];
                    $scope.bottomright_imagedata = $scope.data.showroomAreaImages.filter(function (o) { return o.areaImageUD == 'BR' })[0];
                    $scope.map_imagedata = $scope.data.showroomAreaImages.filter(function (o) { return o.areaImageUD == 'MA' })[0];

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
                    $scope.users = null;
                    $scope.showrooms = null;;
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
                            $scope.method.refresh(data.data.showroomAreaID);
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

        //image event top left
        changeTopLeftImage: function ($event) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.topleft_imagedata.showroomAreaThumbnailImage = img.fileURL;
                        scope.topleft_imagedata.imageFile_NewFile = img.filename;
                        scope.topleft_imagedata.imageFile_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeTopLeftImage: function ($event) {
            $scope.topleft_imagedata.showroomAreaThumbnailImage = '';
            $scope.topleft_imagedata.imageFile_NewFile = '';
            $scope.topleft_imagedata.imageFile_HasChange = true;
        },

        // image event top right
        changeTopRightImage: function ($event) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.topright_imagedata.showroomAreaThumbnailImage = img.fileURL;
                        scope.topright_imagedata.imageFile_NewFile = img.filename;
                        scope.topright_imagedata.imageFile_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeTopRightImage: function ($event) {
            $scope.topright_imagedata.showroomAreaThumbnailImage = '';
            $scope.topright_imagedata.imageFile_NewFile = '';
            $scope.topright_imagedata.imageFile_HasChange = true;
        },

        // image event bottom left
        changeBottomLeftImage: function ($event) {
            $event.preventDefault();

            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.bottomleft_imagedata.showroomAreaThumbnailImage = img.fileURL;
                        scope.bottomleft_imagedata.imageFile_NewFile = img.filename;
                        scope.bottomleft_imagedata.imageFile_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeBottomLeftImage: function ($event) {
            $scope.bottomleft_imagedata.showroomAreaThumbnailImage = '';
            $scope.bottomleft_imagedata.imageFile_NewFile = '';
            $scope.bottomleft_imagedata.imageFile_HasChange = true;
        },

        // image event bottom right
        changeBottomRightImage: function ($event) {
            $event.preventDefault();

            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.bottomright_imagedata.showroomAreaThumbnailImage = img.fileURL;
                        scope.bottomright_imagedata.imageFile_NewFile = img.filename;
                        scope.bottomright_imagedata.imageFile_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeBottomRightImage: function ($event) {
            $scope.bottomright_imagedata.showroomAreaThumbnailImage = '';
            $scope.bottomright_imagedata.imageFile_NewFile = '';
            $scope.bottomright_imagedata.imageFile_HasChange = true;
        },

        // image event map
        changeMapImage: function ($event) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.map_imagedata.showroomAreaThumbnailImage = img.fileURL;
                        scope.map_imagedata.imageFile_NewFile = img.filename;
                        scope.map_imagedata.imageFile_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeMapImage: function ($event) {
            $scope.map_imagedata.showroomAreaThumbnailImage = '';
            $scope.map_imagedata.imageFile_NewFile = '';
            $scope.map_imagedata.imageFile_HasChange = true;
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