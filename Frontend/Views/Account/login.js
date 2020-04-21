//
// SUPPORT
//
jQuery('.login-input').keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.login();
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookies', '$timeout', 'dataService', function ($scope, $cookies, $timeout, dataService) {
    dataService.serviceUrl = context.serviceUrl;

    //
    // property
    //
    $scope.data = {
        username: '',
        password: '',
        error: ''
    };

    //
    // event
    //
    $scope.event = {
        login: function () {
            dataService.login(
                $scope.data.username,
                $scope.data.password,
                function (data) {
                    dataService.token = data.access_token;
                    dataService.getUserInfo(
                        function (data) { 
                            if (data.message.type === 0) {
                                $('#frmToPost_field1').val(JSON.stringify(data.data));
                                $('#frmToPost_field2').val(dataService.token);
                                $('#frmToPost').submit();
                            }
                        },
                        function (error) {
                            console.log(error);
                        }
                    );
                },
                function (error) {
                    if (error.data) {
                        if (error.data.error === 'IncorrectLogin') {
                            $scope.data.error = 'Invalid username or password!';
                        }
                        else if (error.data.error === 'AccountDisabled') {
                            $scope.data.error = 'Account has been disabled!';
                        }
                        else if (error.data.error === 'ChangePassword') {
                            window.location = context.changePassUrl + $scope.data.username;
                        }
                        else {
                            $scope.data.error = error.data.error;
                        }
                    }
                }
            );
        }
    };

    //
    //
    //
    $scope.method = {
    };
}]);