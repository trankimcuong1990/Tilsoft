﻿angular.module('tilsoftApp').service('dataService', ['$http', 'jsonService', function ($http, jsonService) {
    angular.extend(this, jsonService);

    this.createPlanningProductionFromBOM = function (bomID, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'create-planning-production-from-bom?bomID=' + bomID,
            //data: JSON.stringify(param),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            console.log(response);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    this.createPlanningProductionFromBOM = function (bomID) {
        return $http({
            method: 'POST',
            url: this.serviceUrl + 'create-planning-production-from-bom?bomID=' + bomID,
            //data: JSON.stringify(param),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        });
    };

    //this.getData = function (id) {
    //    return $http({
    //        method: 'POST',
    //        url: this.serviceUrl + 'get?id=' + id,
    //        //data: JSON.stringify(param),
    //        headers: {
    //            'Accept': 'application/json',
    //            'Content-Type': 'application/json',
    //            'Authorization': 'Bearer ' + this.token
    //        }
    //    });
    //};

    this.getGrantChart = function (planningProductionID, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'get-grant-chart?planningProductionID=' + planningProductionID,
            //data: JSON.stringify(param),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            console.log(response);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    





}]);