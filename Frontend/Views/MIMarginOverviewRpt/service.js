(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', ['$http', 'jsonService', function ($http, jsonService) {
        angular.extend(this, jsonService);

        // SALE MANAGEMENT
        this.getSaleByAccManager = function (season, iSuccessCallback, iErrorCallback) {
            $http({
                method: 'POST',
                url: this.serviceUrl + 'misalemanagement/get-sale-management-for-delta-overview?season=' + season,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                }
            }).then(function successCallback(response) {
                if (response.data.message.type == 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage('warning', response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.showMessage('warning', response.data.message);
                iErrorCallback(response);
            });
        };

        // SALE BY ITEM
        this.getInvoiceByItem = function (season, iSuccessCallback, iErrorCallback) {
            $http({
                method: 'POST',
                url: this.serviceUrl + 'misalebyitem/getreportdata-for-delta-overview?season=' + season,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                }
            }).then(function successCallback(response) {
                if (response.data.message.type == 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage('warning', response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.showMessage('warning', response.data.message);
                iErrorCallback(response);
            });
        };

        // SALE BY COUNTRY
        this.getSaleByCountry = function (season, iSuccessCallback, iErrorCallback) {
            $http({
                method: 'POST',
                url: this.serviceUrl + 'misalebycountry/getreport-mimargin?season=' + season,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                }
            }).then(function successCallback(response) {
                if (response.data.message.type == 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage('warning', response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.showMessage('warning', response.data.message);
                iErrorCallback(response);
            });
        };

        // SALE BY CLIENT
        this.getSaleByClient = function (season, iSuccessCallback, iErrorCallback) {
            $http({
                method: 'POST',
                url: this.serviceUrl + 'misalebyclient/getreport?season=' + season,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                }
            }).then(function successCallback(response) {
                if (response.data.message.type == 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage('warning', response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.showMessage('warning', response.data.message);
                iErrorCallback(response);
            });
        };
    }]);
})();