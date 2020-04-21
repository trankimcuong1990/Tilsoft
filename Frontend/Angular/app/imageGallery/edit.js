//
// SUPPORT
//
jQuery('#main-form').keypress(function(e){
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ui.select2']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;
    $scope.materials = null;
    $scope.materialTypes = null;
    $scope.materialColors = null;
    $scope.backCushionOptions = null;
    $scope.seatCushionOption = null;
    $scope.cushionColors = null;
    $scope.seasonTypes = null;
    $scope.models = null;
    $scope.galleryItemTypes = null;
    $scope.clients = null;
    $scope.selectedClient = null;

    $scope.currentImage = null;

    $scope.newid = 0;

    $scope.versions = [];

    //
    // event
    //
    $scope.event = {
        init:function(){
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.materials = data.data.materials;
                    $scope.materialTypes = data.data.materialTypes;
                    $scope.materialColors = data.data.materialColors;
                    $scope.backCushionOptions = data.data.backCushionOptions;
                    $scope.seatCushionOption = data.data.seatCushionOption;
                    $scope.cushionColors = data.data.cushionColors;
                    $scope.seasonTypes = data.data.seasonTypes;
                    $scope.models = data.data.models;
                    $scope.galleryItemTypes = data.data.galleryItemTypes;
                    $scope.clients = data.data.clients;
                    $scope.method.renderImage();
                    $scope.$apply();

                    // init select 2 for model box
                    jQuery('#modelSelectBox').select2({
                        ajax: {
                            url: jsonService.supportServiceUrl + 'select2model',
                            dataType: 'json',
                            type: "GET",
                            quietMillis: 150,
                            data: function (params) {
                                return {
                                    query: params
                                };
                            },
                            results: function (data) {
                                return {
                                    results: jQuery.map(data, function (item) {
                                        return {
                                            text: item.modelNM,
                                            id: item.modelID
                                        }
                                    })
                                };
                            }
                        },
                        cache: true,
                        initSelection: function (item, callback) {
                            callback({ id: $scope.data.modelID, text: $scope.data.modelNM });
                        },
                        minimumInputLength: 3
                    });

                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function() {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.materials = null;
                    $scope.materialTypes = null;
                    $scope.materialColors = null;
                    $scope.backCushionOptions = null;
                    $scope.seatCushionOption = null;
                    $scope.cushionColors = null;
                    $scope.seasonTypes = null;
                    $scope.models = null;
                    $scope.galleryItemTypes = null;
                    $scope.clients = null;
                    $scope.$apply();
                }
            );
        },
        update: function($event){
            $event.preventDefault();

            if($scope.editForm.$valid)
            {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if(data.message.type == 0) {
                            $scope.method.refresh(data.data.imageGalleryID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else
            {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        },
        delete: function($event){
            $event.preventDefault();

            if (confirm('Are you sure ?')) {
                jsonService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if(data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },

        //
        // CLIENT
        //
        addClient: function () {
            if ($scope.selectedClient != null) {
                var param = {
                    isExist: false,
                    clientUD: ''
                };

                angular.forEach($scope.data.imageGalleryClients, function (value, key) {
                    if (value.clientID == $scope.selectedClient) {
                        param.isExist = true;
                    }
                }, param);

                if (!param.isExist) {
                    angular.forEach($scope.clients, function (value, key) {
                        if (value.clientID == $scope.selectedClient) {
                            param.clientUD = value.clientUD;
                        }
                    }, param);

                    $scope.data.imageGalleryClients.push({
                        imageGalleryClientID: $scope.method.getNewID(),
                        clientID: $scope.selectedClient,
                        clientUD: param.clientUD
                    });
                }
            }
        },
        removeClient: function (index) {
            $scope.data.imageGalleryClients.splice(index, 1);
        },

        //
        // IMAGE
        //
        addImage: function () {
            var newImage = {
                imageGalleryVersionID: $scope.method.getNewID(),
                hasChange: false,
                newFile: null,
                thumbnailLocation: null,
                fileLocation: null,
                version: null,
                isDefault: false
            };
            $scope.event.editImage(newImage);
        },
        editImage: function (item) {
            $scope.currentImage = JSON.parse(JSON.stringify(item));
            jQuery("#imageForm").modal();
        },
        updateImage: function () {
            if ($scope.currentImage.isDefault) {
                angular.forEach($scope.data.imageGalleryVersions, function (value, key) {
                    if (value.version == $scope.currentImage.version) {
                        value.isDefault = false;
                    }                    
                }, null);
            }

            var index = $scope.method.getImageIndex($scope.currentImage.imageGalleryVersionID)
            if (index >= 0) {
                $scope.data.imageGalleryVersions[index] = $scope.currentImage;
            }
            else {
                $scope.data.imageGalleryVersions.push($scope.currentImage);
            }
            jQuery("#imageForm").modal('hide');

            $scope.method.renderImage();
        },
        removeImage: function (item) {
            $scope.data.imageGalleryVersions.splice($scope.data.imageGalleryVersions.indexOf(item), 1);
        },
        changeImageFile: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        if (img.filename.substr(img.filename.length - 3, 3) == 'mp4') {
                            scope.currentImage.thumbnailLocation = context.movieImage;
                            scope.currentImage.fileLocation = context.moviePlayer + '?url=' + img.fileURL;
                        }
                        else {
                            scope.currentImage.thumbnailLocation = img.fileURL;
                            scope.currentImage.fileLocation = img.fileURL;
                        }
                        scope.currentImage.newFile = img.filename;
                        scope.currentImage.hasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function(id){
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl+id;
        },
        getNewID: function(){
            $scope.newid--;
            return $scope.newid;
        },
        getImageIndex: function (id) {
            var isExist = false;
            var index = 0;
            for (var index = 0; index < $scope.data.imageGalleryVersions.length; index++) {
                if ($scope.data.imageGalleryVersions[index].imageGalleryVersionID == id) {
                    isExist = true;
                    break;
                }
            }

            if (!isExist) {
                return -1;
            }
            else {
                return index;
            }
        },
        renderImage: function () {
            $scope.versions = [];
            angular.forEach($scope.data.imageGalleryVersions, function (value, key) {
                if ($scope.versions.indexOf(value.version) < 0) {
                    $scope.versions.push(value.version);
                }
            }, null);
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);