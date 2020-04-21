(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', cashBookRptService);
    cashBookRptService.$inject = ['$http', 'jsonService'];

    function cashBookRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
     
        self.getDataWithFilters = getDataWithFilters;
        self.getDataWithFilterExcel = getDataWithFilterExcel;   

        function getDataWithFilterExcel(type, bankAccount, startDate, endDate, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'getdatawithfilterexcel?paymentType=' + type + '&bankAccount=' + bankAccount + '&startDate=' + startDate + '&endDate=' + endDate,
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

        function getDataWithFilters(type, bankAccount, startDate, endDate, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'getdatawithfilters?paymentType=' + type + '&bankAccount=' + bankAccount + '&startDate=' + startDate + '&endDate=' + endDate,
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
