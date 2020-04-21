(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', summaryOutWardRptService);
    summaryOutWardRptService.$inject = ['$http', 'jsonService'];

    function summaryOutWardRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "WorkOrderUD";
        self.searchFilter.sortedDirection = "ASC";
        self.getDataWithFilters = getDataWithFilters;
        self.generateExcel = generateExcel;

        function getDataWithFilters(month, year, workOrderStatusNM, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'getdatawithfilters?month=' + month + '&year=' + year + '&workOrderStatusNM=' + workOrderStatusNM,
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

        function generateExcel(month, year, workOrderStatusNM, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: "POST",
                dataType: 'json',
                url: self.serviceUrl + 'getexcelreport?month=' + month + '&year=' + year + '&workOrderStatusNM=' + workOrderStatusNM,
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