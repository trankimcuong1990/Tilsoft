(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', stockStatusQntRptService);
    stockStatusQntRptService.$inject = ['$http', 'jsonService'];

    function stockStatusQntRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "PrimaryID";
        self.searchFilter.sortedDirection = "ASC";
        self.getDataWithFilters = getDataWithFilters;
        self.getInit = getInit;

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
                url: context.serviceUrl + 'gets',
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

        function getInit(branchID, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + context.token
                },
                type: 'POST',
                url: context.serviceUrl + 'getinitdata?branchID=' + branchID,
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