//
//APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.factories = null;
    $scope.frameMaterials = null;
    $scope.selectedFactory = null;

    $scope.newid = -1;

    //
    // event
    //

    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.factories = data.data.factories;
                    $scope.frameMaterials = data.data.frameMaterials;

                    console.log(data);
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
                    $scope.factories = null;
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
                            $scope.method.refresh(data.data.frameMaterialProfileID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        },
        delete: function ($event) {
            $event.preventDefault();
            if (confirm('Are you sure ?')) {
                jsonService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            window.location = context.backURL;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },
        //Add Factory
        addFactory: function () {
            if ($scope.selectedFactory != null) {
                var param = {
                    isExist: false,
                    factoryUD: ''
                };

                angular.forEach($scope.data.frameMaterialProfileFactories, function (value, key) {
                    if (value.factoryID == $scope.selectedFactory) {
                        param.isExist = true;
                    }
                }, param);

                if (!param.isExist) {
                    angular.forEach($scope.factories, function (value, key) {
                        if (value.factoryID == $scope.selectedFactory) {
                            param.factoryUD = value.factoryUD;
                        }
                    }, param);

                    $scope.data.frameMaterialProfileFactories.push({
                        frameMaterialProfileFactoryID: $scope.method.getNewID(),
                        factoryID: $scope.selectedFactory,
                        factoryUD: param.factoryUD
                    });

                    //console.log($scope.data.frameMaterialProfileFactories);
                }
            }
        },
        removeFactory: function (index) {
            $scope.data.frameMaterialProfileFactories.splice(index, 1);
        },

        //
        //Image
        //
        changeImage: function ($event) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.imageFile_DisplayUrl = img.fileURL;
                        scope.data.imageFile_NewFile = img.filename;
                        scope.data.imageFile_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeImage: function ($event) {
            $scope.data.imageFile_DisplayUrl = '';
            $scope.data.imageFile_NewFile = '';
            $scope.data.imageFile_HasChange = true;
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshURL + id;
        },
        getNewID: function () {
            $scope.newid--;
            return $scope.newid;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);