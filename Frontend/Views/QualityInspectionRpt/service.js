(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", qualityInspectionService);
    qualityInspectionService.$inject = ["$http", "jsonService"];

    function qualityInspectionService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "WorkCenterDate";
        self.searchFilter.sortedDirection = "ASC";

        self.getQualityInspectionType = function (iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);

            $http({
                method: "POST",
                url: self.serviceUrl + "getqualityinspectiontype",
                data: JSON.stringify(this.searchFilter),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage("warning", response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage("warning", response.data.message);
                iErrorCallback(response);
            });
        };
        self.updateQualityInspectionType = function (data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: "POST",
                url: this.serviceUrl + "updatequalityinspectiontype",
                data: JSON.stringify(data),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage("warning", response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage("warning", response.data.message);
                iErrorCallback(response);
            });
        };
        self.deleteQualityInspectionType = function (data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: "POST",
                url: this.serviceUrl + "deletequalityinspectiontype",
                data: JSON.stringify(data),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage("warning", response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage("warning", response.data.message);
                iErrorCallback(response);
            });
        };

        self.getQualityInspectionCorrectAction = function (iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);

            $http({
                method: "POST",
                url: self.serviceUrl + "getqualityinspectioncorrectaction",
                data: JSON.stringify(this.searchFilter),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage("warning", response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage("warning", response.data.message);
                iErrorCallback(response);
            });
        };
        self.updateQualityInspectionCorrectAction = function (data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: "POST",
                url: this.serviceUrl + "updatequalityinspectioncorrectaction",
                data: JSON.stringify(data),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage("warning", response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage("warning", response.data.message);
                iErrorCallback(response);
            });
        };
        self.deleteQualityInspectionCorrectAction = function (data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: "POST",
                url: this.serviceUrl + "deletequalityinspectioncorrectaction",
                data: JSON.stringify(data),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage("warning", response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage("warning", response.data.message);
                iErrorCallback(response);
            });
        };

        self.getQualityInspectionDefaultSetting = function (iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);

            $http({
                method: "POST",
                url: self.serviceUrl + "getqualityinspectiondefaultsetting",
                data: JSON.stringify(this.searchFilter),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage("warning", response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage("warning", response.data.message);
                iErrorCallback(response);
            });
        };
        self.updateQualityInspectionDefaultSetting = function (data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: "POST",
                url: this.serviceUrl + "updatequalityinspectiondefaultsetting",
                data: JSON.stringify(data),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage("warning", response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage("warning", response.data.message);
                iErrorCallback(response);
            });
        };
        self.deleteQualityInspectionDefaultSetting = function (data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: "POST",
                url: this.serviceUrl + "deletequalityinspectiondefaultsetting",
                data: JSON.stringify(data),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage("warning", response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage("warning", response.data.message);
                iErrorCallback(response);
            });
        };

        self.getQualityInspection = function (id, workOrderID, clientID, productionItemID, workCenterNM, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);

            $http({
                method: "POST",
                url: self.serviceUrl + "getqualityinspection?id=" + id + "&workOrderID=" + workOrderID + "&clientID=" + clientID + "&productionItemID=" + productionItemID + "&workCenterNM=" + workCenterNM,
                data: JSON.stringify(this.searchFilter),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage("warning", response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage("warning", response.data.message);
                iErrorCallback(response);
            });
        };
        self.updateQualityInspection = function (data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: "POST",
                url: this.serviceUrl + "updatequalityinspection",
                data: JSON.stringify(data),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage("warning", response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage("warning", response.data.message);
                iErrorCallback(response);
            });
        };
        self.deleteQualityInspection = function (id, workOrderID, clientID, productionItemID, workCenterNM, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);

            $http({
                method: "POST",
                url: self.serviceUrl + "deletequalityinspection?id=" + id + "&workOrderID=" + workOrderID + "&clientID=" + clientID + "&productionItemID=" + productionItemID + "&workCenterNM=" + workCenterNM,
                data: JSON.stringify(this.searchFilter),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage("warning", response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage("warning", response.data.message);
                iErrorCallback(response);
            });
        };
    }
})();