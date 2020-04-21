(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', warehouseOverviewRptService);
    warehouseOverviewRptService.$inject = ['$http', 'jsonService'];

    function warehouseOverviewRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.exportExcelWarehouseOverview = exportExcelWarehouseOverview;

        function exportExcelWarehouseOverview(startDate, endDate, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'getreportdata?startDate=' + startDate + '&endDate=' + endDate,
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