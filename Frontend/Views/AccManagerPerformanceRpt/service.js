(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', ['$http', 'jsonService', function ($http, jsonService) {
        angular.extend(this, jsonService);

        //
        // extend base function here
        //
        this.getInit = function (iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);

            $http({
                method: 'POST',
                url: this.serviceUrl + 'acc-manager-performance/getinitdata',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
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

        this.getAccPerformance = function (saleId, elemId, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitchExt(true, elemId);

            $http({
                method: 'POST',
                url: this.serviceUrl + 'acc-manager-performance/get-acc-performance?saleId=' + saleId,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitchExt(false, elemId);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage('warning', response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitchExt(false, elemId);

                jsHelper.showMessage('warning', response.data.message);
                iErrorCallback(response);
            });
        };

        this.getSalesByCountry = function (saleId, elemId, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitchExt(true, elemId);

            $http({
                method: 'POST',
                url: this.serviceUrl + 'misalebycountry/getreport?season=' + jsHelper.getCurrentSeason() + '&saleId=' + saleId,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitchExt(false, elemId);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage('warning', response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitchExt(false, elemId);

                jsHelper.showMessage('warning', response.data.message);
                iErrorCallback(response);
            });
        };

        this.getSalesConclusion = function (saleId, elemId, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitchExt(true, elemId);

            $http({
                method: 'POST',
                url: this.serviceUrl + 'misaleconclusion/getreport?season=' + jsHelper.getCurrentSeason() + '&saleId=' + saleId,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitchExt(false, elemId);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage('warning', response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitchExt(false, elemId);

                jsHelper.showMessage('warning', response.data.message);
                iErrorCallback(response);
            });
        };

        this.getSalesManagement = function (saleId, elemId, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitchExt(true, elemId);

            $http({
                method: 'POST',
                url: this.serviceUrl + 'misalemanagement/getreport?season=' + jsHelper.getCurrentSeason() + '&saleId=' + saleId,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitchExt(false, elemId);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage('warning', response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitchExt(false, elemId);

                jsHelper.showMessage('warning', response.data.message);
                iErrorCallback(response);
            });
        };

    }]);

})();