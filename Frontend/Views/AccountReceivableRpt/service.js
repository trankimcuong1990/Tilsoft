angular.module('tilsoftApp').service('dataService', ['$http', 'jsonService', '$rootScope', function ($http, jsonService, $rootScope) {
    angular.extend(this, jsonService);


    this.getTotalLiabilities = function (filters, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);

        $http({
            method: 'POST',           
            url: context.serviceUrl + 'getliabilities',
            data: JSON.stringify(filters),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + context.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type === 0) {
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };
}]);