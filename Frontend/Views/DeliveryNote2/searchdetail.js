'use strict';

angular.module('tilsoftApp').service('searchDetailDeliveryNote', ['$http', '$rootScope', '$timeout', function ($http, $rootScope, $timeout) {
    var self = this;
    self.data = null;
    self.supportData = null;
    self.productionTeams = null;
    self.workCenters = null;

    console.log(this);
    self.load = function (iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: context.searchDetailUrl + 'getSearchDetail',
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

    self.event = {
        load: function () {
            self.load(
                function (data) {
                    self.productionTeams = data.data.productionTeams;
                    self.workCenters = data.data.workCenters;

                    $rootScope.$broadcast('searchDetailDeliveryNote.productionTeam', self.productionTeams);
                    $rootScope.$broadcast('searchDetailDeliveryNote.workCenter', self.workCenters);
                    $rootScope.$broadcast('searchDetailDeliveryNote.checkDetail', true);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }

    };

    self.method = {

    };
}]);