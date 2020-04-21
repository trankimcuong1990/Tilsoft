angular.module('tilsoftApp').service('dataService', ['$http', 'jsonService', '$rootScope', function ($http, jsonService, $rootScope) {
    angular.extend(this, jsonService);


    this.searchDataReport = function (searchFilter, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);

        $http({
            method: 'POST',
            //contentType: "application/json",
            url: context.serviceUrl + 'search-data-report',
            data: JSON.stringify(searchFilter),
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

    this.getDataReport = function (reportQAQCID, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);

        $http({
            method: 'POST',
            //contentType: "application/json",
            url: context.serviceUrl + 'get-data-report?reportQAQCID=' + reportQAQCID,
            //data: JSON.stringify(searchFilter),
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

    this.setStatusReport = function (reportQAQCID, statusID, comment, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);

        $http({
            method: 'POST',
            //contentType: "application/json",
            url: context.serviceUrl + 'set-status-report?reportQAQCID=' + reportQAQCID + '&statusID=' + statusID + '&comment=' + comment,
            //data: JSON.stringify(searchFilter),
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

    this.getLoginspector = function (qaqcid, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);

        $http({
            method: 'POST',
            //contentType: "application/json",
            url: context.serviceUrl + 'get-loginspector?qaqcid=' + qaqcid,
            //data: JSON.stringify(searchFilter),
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