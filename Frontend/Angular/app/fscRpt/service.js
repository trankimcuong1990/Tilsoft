(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', fscRptService);
    fscRptService.$inject = ['$http', 'jsonService'];

    function fscRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;

        self.searchFilter.sortedBy = 'UpdatedDate';
        self.searchFilter.sortedDirection = 'DESC';

        self.exportFCSReport = exportFCSReport;

        function exportFCSReport(startDate, endDate, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'exportfscreport?startDate=' + startDate + '&endDate=' + endDate,
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
