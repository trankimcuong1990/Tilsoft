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
    
    $scope.devRequestTypes = null;
    $scope.devRequestProjects = null;
    $scope.devRequestPriorities = null;
    $scope.requesters = null;

    //
    // event
    //
    $scope.event = {
        init:function(){
            dataService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.data.requesterID = context.userId;
                    $scope.devRequestTypes = data.data.devRequestTypes;
                    $scope.devRequestProjects = data.data.devRequestProjects;
                    $scope.devRequestPriorities = data.data.devRequestPriorities;
                    $scope.requesters = data.data.requesters;
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function() {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.devRequestTypes = null;
                    $scope.devRequestProjects = null;
                    $scope.devRequestPriorities = null;
                    $scope.requesters = null;
                }
            );
        },
        update: function(){
            if($scope.editForm.$valid)
            {
                dataService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if(data.message.type == 0) {
                            $scope.method.refresh(data.data.devRequestID);
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

        addAttachment: function () {
            fileMultipleUploader.callback = function () {
                scope.$apply(function () {
                    angular.forEach(fileMultipleUploader.selectedFiles, function (value, key) {
                        $scope.data.devRequestFiles.push({
                            devRequestFileID: dataService.getIncrementId(),
                            fileUD: '',
                            fileLocation: value.fileURL,
                            hasChanged: true,
                            newFile: value.filename,
                            friendlyName: value.friendlyName
                        });
                    }, null);
                });
            };
            fileMultipleUploader.open();
        },
        removeAttachment: function (item) {
            $scope.data.devRequestFiles.splice($scope.data.devRequestFiles.indexOf(item), 1);
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function(id){
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl+id;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);