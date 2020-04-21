angular.module('cpApp').controller('cpController', ['$scope', '$cookies', '$timeout', 'dataService', function ($scope, $cookies, $timeout, dataService) {
    $scope.data = {
        username: '',
        password: '',
        errMsg: '',
        isSaveCookie: false, // persist cookies for 14 days
        isInvalid: false
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
                    if (data.access_token) {
                        // set expire date to 1 day from now
                        var expireDate = new Date();
                        expireDate.setDate(expireDate.getDate() + ($scope.data.isSaveCookie ? 14 : 1));
                        $cookies.put(context.authStore, data.access_token, { path: '/', expires: expireDate, samesite: 'lax' });
                        dataService.token = data.access_token;

                        // get user info
                        dataService.getUserInfo(
                            function (data) {
                                if (data.message.type === 0) {
                                    $cookies.put(context.userStore, JSON.stringify(data.data), { path: '/', expires: expireDate, samesite: 'lax' });
                                    window.location = context.returnUrl;
                                }
                                else {
                                    console.log(data.message);
                                }
                            },
                            function (error) {
                                console.log(error);
                            }
                        );
                    }
                    else {
                        $scope.data.isInvalid = true;
                        $timeout(function () {
                            $scope.data.isInvalid = false;
                        }, 3000);
                    }
                },
                function (error) {
                    if (error.data) {
                        if (error.data.error === 'IncorrectLogin') {
                            $scope.data.errMsg = 'Invalid username or password!';
                        }
                        else if (error.data.error === 'AccountDisabled') {
                            $scope.data.errMsg = 'Account has been disabled!';
                        }
                        else {
                            $scope.data.errMsg = error.data.error;
                        }
                    }
                }
            );
        }
    };

    //
    // methods
    //
    $scope.method = {};

    //
    // bootstrap the module
    //
    $timeout(function () {
        if ($cookies.get(context.authStore)) {
            window.location = context.returnUrl;
        }
    }, 0);
}]);