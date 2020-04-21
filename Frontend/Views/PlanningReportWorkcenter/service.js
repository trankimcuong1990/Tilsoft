(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', ['$http', 'jsonService', function ($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;

        self.getWorkcenterStatus = function (workOrderID, workCenterID, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                url: self.serviceUrl + 'getworkcenterstatus?workOrderID=' + workOrderID + '&workCenterID=' + workCenterID,
                //data: JSON.stringify(data),
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
        //this.getWorkcenterStatus = function (workOrderID, workCenterID, iSuccessCallback, iErrorCallback) {
        //    jsHelper.loadingSwitch(true);
        //    $http({
        //        method: 'POST',
        //        url: this.serviceUrl + 'getworkcenterstatus?workOrderID=' + workOrderID + '&workCenterID=' + workCenterID,
        //        headers: {
        //            'Accept': 'application/json',
        //            'Content-Type': 'application/json',
        //            'Authorization': 'Bearer ' + this.token
        //        }
        //    }).then(function successCallback(response) {
        //        jsHelper.loadingSwitch(false);
        //        if (response.data.message.type === 0) {
        //            iSuccessCallback(response.data);
        //        }
        //        else {
        //            jsHelper.showMessage('warning', response.data.message.message);
        //            iErrorCallback(response);
        //        }
        //    }, function errorCallback(response) {
        //        jsHelper.loadingSwitch(false);
        //        jsHelper.showMessage('warning', response.data.message);
        //        iErrorCallback(response);
        //    });
        //};
    }]);

})();