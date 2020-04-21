angular.module('cpApp').service('dataService', ['$http', '$cookies', 'avsDataService', function ($http, $cookies, avsDataService) {
    angular.extend(this, avsDataService);
    this.serviceUrl = context.serviceUrl;
    this.token = $cookies.get(context.authStore); // will be empty when user not logged in yet

    this.login = function (username, password, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'token',
            data: 'grant_type=password&username=' + username + '&password=' + encodeURI(password.replace(/\+/g, "[plus]")),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);
            iSuccessCallback(response.data);
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            iErrorCallback(response);
        });
    };

    this.getUserInfo = function (iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'api/account/get-user-info-light',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);
            iSuccessCallback(response.data);
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            iErrorCallback(response);
        });
    };
}]);