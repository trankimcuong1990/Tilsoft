

var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.seasons = null;

    //
    // event
    //
    $scope.event = {
        init:function(){
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.seasons = data.data.seasons;
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function() {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.seasons = null;
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
                            $scope.method.refresh(data.data.cushionID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else
            {
                jsHelper.showMessage('warning', context.errMsg);
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
                            window.location = context.backURL;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },
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
        refresh: function(id){
            jsHelper.loadingSwitch(true);
            window.location = context.refreshURL + id;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);