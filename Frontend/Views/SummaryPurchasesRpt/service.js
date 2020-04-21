(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', summaryPurchasesRptService);
    summaryPurchasesRptService.$inject = ['$http', 'jsonService'];

    function summaryPurchasesRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "ProductionItemUD";
        self.searchFilter.sortedDirection = "ASC";
        self.getDataWithFilters = getDataWithFilters;
        self.generateExcel = generateExcel;

        function getDataWithFilters(filters, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + context.token
                },
                data: JSON.stringify(filters),
                type: 'POST',
                url: context.serviceUrl + 'getdatawithfilters',
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

        function generateExcel(factoryRawMaterialID, productionItemUD, startDate, endDate, factoryRawMaterialFullName, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: "POST",
                dataType: 'json',
                url: self.serviceUrl + 'getexcelreport?factoryRawMaterialID=' + factoryRawMaterialID + '&productionItemUD=' + productionItemUD + '&startDate=' + startDate + '&endDate=' + endDate + '&factoryRawMaterialFullName=' + factoryRawMaterialFullName,
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
    }
})();