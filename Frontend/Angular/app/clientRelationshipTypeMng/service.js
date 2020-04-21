(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", clientRelationshipTypeMngService);
    clientRelationshipTypeMngService.$inject = ["$http", "jsonService"];

    function clientRelationshipTypeMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "ClientRelationshipTypeNM";
        self.searchFilter.sortedDirection = "ASC";

        self.updateorder = function (data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                contentType: "application/json",
                url: self.serviceUrl + 'updateorder',
                data: JSON.stringify(data),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + self.token
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
                jsHelper.showMessage('warning', response.data.message);
                iErrorCallback(response);
            });
        };
    }
})();