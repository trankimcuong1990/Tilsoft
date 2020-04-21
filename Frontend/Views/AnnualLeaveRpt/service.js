(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', annualLeaveService);
    annualLeaveService.$inject = ['$http', 'jsonService'];

    function annualLeaveService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;

        self.searchFilter.sortedBy = 'RequestedTimeOff';
        self.searchFilter.sortedDirection = 'Desc';

        this.getDetail = function (employeeID, affectedYear, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                url: this.serviceUrl + 'getdetail?employeeID=' + employeeID + '&affectedYear=' + affectedYear,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type == 0) {
                    //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
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

        this.getDetailTotal = function (employeeID, affectedYear, type, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                url: this.serviceUrl + 'getdetailtotal?employeeID=' + employeeID + '&affectedYear=' + affectedYear + '&type=' + type,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type == 0) {
                    //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
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
    };
})();