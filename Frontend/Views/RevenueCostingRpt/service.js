(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', revenueCostingRptService);
    revenueCostingRptService.$inject = ['$http', 'jsonService'];

    function revenueCostingRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "ProductionItemUD";
        self.searchFilter.sortedDirection = "ASC";
        self.getexcelreport = getexcelreport;

        function getexcelreport(factoryRawMaterialID, startDate, endDate, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: "POST",
                dataType: 'json',
                url: self.serviceUrl + 'getexcelreport?factoryRawMaterialID=' + factoryRawMaterialID + '&startDate=' + startDate + '&endDate=' + endDate,
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