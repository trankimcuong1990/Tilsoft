﻿(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", dataService);
    dataService.$inject = ["$http", "jsonService"];

    function dataService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "DeliveryNoteUD";
        self.searchFilter.sortedDirection = "ASC";



        self.exportExcel = function (filters, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);
            $http({
                method: 'POST',
                url: this.serviceUrl + 'exportexcel',
                data: JSON.stringify(filters),
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
                jsHelper.showMessage('warning', response.data.message);
                iErrorCallback(response);
            });
        }

        self.getDeliveryNoteList = function (filters, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'getDeliveryNoteDetail',
                data: JSON.stringify(filters),
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
        }

        self.exportHTML = function (purchasingInvoiceIds, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: "POST",
                url: this.serviceUrl + 'exportexactonlinesoftware?purchasingInvoiceIds=' + purchasingInvoiceIds,
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
        }
    };
})();