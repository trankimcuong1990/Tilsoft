﻿(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', preOrderPlanningMngService);
    preOrderPlanningMngService.$inject = ['$http', 'jsonService'];

    function preOrderPlanningMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'PrimaryID';
        self.searchFilter.sortedDirection = 'ASC';

        self.updateData = function (id, season, factoryID, data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                url: self.serviceUrl + 'update?id=' + id + '&season=' + season + '&factoryID=' + factoryID,
                data: JSON.stringify(data),
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
    };
})();