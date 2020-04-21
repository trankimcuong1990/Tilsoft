//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);
//create controller
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    $scope.data = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getItemData(
                context.id,
                function (data) {
                    if (data.message.type === 0) {
                        $scope.data = data.data.data;
                        $('#content').show();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                dataService.updateItemData(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            window.location = context.refreshURL;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Validation failed, please check the input form for error!');
            }
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);