//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ui.select2']);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;
    $scope.seasons = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.seasons = data.data.seasons;

                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    //jsHelper.showMessage('warning', error);
                    alert(error);
                    console.log(error);
                    $scope.data = null;
                    $scope.seasons = null;
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                dataService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.catalogFileID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        },
        delete: function () {
            if (context.id > 0) {
                dataService.delete(
                    context.id,
                    null,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        },

        changeFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.friendlyName = fileUploader2.selectedFriendlyName;
                    $scope.data.fileLocation = fileUploader2.selectedFileUrl;
                    $scope.data.newFile = fileUploader2.selectedFileName;
                    $scope.data.hasChanged = true;
                });
            };
            fileUploader2.open();
        },
        removeFile: function () {
            $scope.data.friendlyName = '';
            $scope.data.fileLocation = '';
            $scope.data.newFile = '';
            $scope.data.hasChanged = true;
        },

        plChangeFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.plFriendlyName = fileUploader2.selectedFriendlyName;
                    $scope.data.plFileLocation = fileUploader2.selectedFileUrl;
                    $scope.data.plNewFile = fileUploader2.selectedFileName;
                    $scope.data.plHasChanged = true;
                });
            };
            fileUploader2.open();
        },
        plRemoveFile: function () {
            $scope.data.plFriendlyName = '';
            $scope.data.plFileLocation = '';
            $scope.data.plNewFile = '';
            $scope.data.plHasChanged = true;
        }
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