(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', shipmentToBeInvoicedRptService);
    shipmentToBeInvoicedRptService.$inject = ['$http', 'jsonService'];

    function shipmentToBeInvoicedRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.exportExcel = exportExcel;

        function exportExcel(season, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'getreportdata?season=' + season,
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