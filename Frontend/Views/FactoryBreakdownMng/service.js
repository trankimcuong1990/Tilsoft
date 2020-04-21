(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', factoryBreakdownMngService);
    factoryBreakdownMngService.$inject = ['$http', 'jsonService'];

    function factoryBreakdownMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'UpdatedDate';
        self.searchFilter.sortedDirection = 'DESC';

        self.getFilterData = function (iSuccessCallBack, iErrorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                dataType: 'json',
                url: context.serviceUrl + 'getfilterdata',
                beforeSend: function () {
                    jsHelper.loadingSwitch(true);
                },
                success: function (data) {
                    jsHelper.loadingSwitch(false);
                    iSuccessCallBack(data);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    jsHelper.loadingSwitch(false);
                    iErrorCallBack(xhr.responseText.exceptionMessage);
                }
            });

        };

        // reset
        self.resetData = function (id, data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                url: this.serviceUrl + 'reset?id=' + id,
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
        self.exportexcellistfactorybreakdown = function (successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: this.serviceUrl + 'exportexcellistfactorybreakdown',
                data: JSON.stringify(this.searchFilter),
                beforeSend: function () {
                    jsHelper.loadingSwitch(true);
                },
                success: function (data) {
                    jsHelper.loadingSwitch(false);
                    successCallBack(data);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    jsHelper.loadingSwitch(false);
                    errorCallBack(xhr.responseJSON.exceptionMessage);
                }
            });
        };
        // getData
        self.getData = function (id, sampleProductID, modelID, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                url: this.serviceUrl + 'getdata?id=' + id + '&sampleProductID=' + sampleProductID + '&modelID=' + modelID,
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

        //Get sample detail data
        self.getSampleDetailData = function (sampleProductID, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                url: context.serviceSampleUrl + 'get-detail-factory-breakdown?sampleProductID=' + sampleProductID,
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



        self.exportExcelDetail = function (id, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: this.serviceUrl + 'export-excel-detail?id=' + id,
                beforeSend: function () {
                    jsHelper.loadingSwitch(true);
                },
                success: function (data) {
                    jsHelper.loadingSwitch(false);
                    successCallBack(data);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    jsHelper.loadingSwitch(false);
                    errorCallBack(xhr.responseJSON.exceptionMessage);
                }
            });
        };

        // update import
        self.importUpdateData = function (data, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                contentType: "application/json",
                url: this.serviceUrl + 'updateImportData',
                data: JSON.stringify(data),
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