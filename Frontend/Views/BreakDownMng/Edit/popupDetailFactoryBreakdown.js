'use strict';

angular.module('tilsoftApp').service('factoryBreakdown', ['$http', '$rootScope', '$timeout', function ($http, $rootScope, $timeout) {
    var self = this;
    self.data = null;
    self.supportData = null;
    console.log(this);
    self.load = function (sampleProductID, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: context.factoryBreakdownUrl + 'get-data-breakdown?sampleProductID=' + sampleProductID,
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
        getFactoryBreakdown: function (sampleProductID) {
            self.load(
                sampleProductID,
                function (data) {
                    self.data = data.data;
                    $rootScope.$broadcast('factoryBreakdown.getFactoryBreakdown_Close', self.data);
                    self.data = null;
                    jQuery("#getFactoryBreakdown").modal();
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