(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', storageCardRptService);
    storageCardRptService.$inject = ['$http', 'jsonService'];

    function storageCardRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.exportExcel = exportExcel;
        self.getInitFromInventoryOverview = getInitFromInventoryOverview;
        self.getDataWithFilters = getDataWithFilters;

        function exportExcel(productionItemID, factoryWarehouseID, startDate, endDate, successCallBack, errorCallBack) {
            jQuery.ajax({
               cache: false,
               headers: {
                   'Accept': 'application/json',
                   'Content-Type': 'application/json',
                   'Authorization': 'Bearer ' + this.token
               },
               type: 'POST',
               url: self.serviceUrl + 'getreportdata?productionItemID=' + productionItemID + '&factoryWarehouseID=' + factoryWarehouseID + '&startDate=' + startDate + '&endDate=' + endDate,
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

        function getInitFromInventoryOverview(productionItemID, factoryWarehouseID, startDate, endDate, branchID, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'getinitfrominventoryoverview?productionItemID=' + productionItemID + '&factoryWarehouseID=' + factoryWarehouseID + '&startDate=' + startDate + '&endDate=' + endDate + '&branchID=' + branchID,
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

        function getDataWithFilters(productionItemID, factoryWarehouseID, startDate, endDate, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'getdatawithfilters?productionItemID=' + productionItemID + '&factoryWarehouseID=' + factoryWarehouseID + '&startDate=' + startDate + '&endDate=' + endDate,
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