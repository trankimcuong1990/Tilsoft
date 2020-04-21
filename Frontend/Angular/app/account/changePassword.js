//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []).config([
    '$compileProvider',
    function ($compileProvider) {
        $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|skype):/);
    }
]);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = {
        userName: context.userName,
        oldPassword: '',
        newPassword: '',
        confirmation: ''
    };

    //
    // event
    //
    $scope.event = {
        changePassword: function () {
            // validation
            if ($scope.data.newPassword !== $scope.data.confirmation) {
                alert('New password and confirmation do not match!');
                return;
            }

            if ($scope.data.oldPassword == $scope.data.newPassword) {
                alert('New password can not be the same with old password!');
                return;
            }

            if ($scope.editForm.$valid) {
                dataService.changePassword(
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            window.location = context.loginUrl + '?a=' + data.data.access_token + '&e=' + data.data.expires_in;
                        }
                    },
                    function (error) {
                        alert(error.data.message.message);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function(id){
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        }
    };
}]);