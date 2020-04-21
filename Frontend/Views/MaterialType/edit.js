var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(context.id,
                function (data) {
                    $scope.data = data.data.data;
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
                            $scope.method.refresh(data.data.materialTypeID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', context.errorMsg);
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
        // file functions
        //
        changeFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.hangTagFileFriendlyName = fileUploader2.selectedFriendlyName;
                    $scope.data.hangTagFileUrl = fileUploader2.selectedFileUrl;
                    $scope.data.newHangTagFile = fileUploader2.selectedFileName;
                    $scope.data.hangTagFileHasChange = true;
                });
            };
            fileUploader2.open();
        },
        removeFile: function () {
            $scope.data.hangTagFileFriendlyName = '';
            $scope.data.hangTagFileUrl = '';
            $scope.data.newHangTagFile = '';
            $scope.data.hangTagFileHasChange = true;
        },
        downloadFile: function () {
            if ($scope.data.hangTagFileUrl) {
                window.open($scope.data.hangTagFileUrl);
            }
        },
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);