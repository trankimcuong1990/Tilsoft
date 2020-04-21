(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', inventoryCostRptService);
    inventoryCostRptService.$inject = ['$http', 'jsonService'];

    function inventoryCostRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;

        self.exportInventoryCostReport = exportInventoryCostReport;
        self.getDataWithFilters = getDataWithFilters;

        function exportInventoryCostReport(factoryWarehouseID, productionItemTypeID, startDate, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'getexcelreport?factoryWarehouseID=' + factoryWarehouseID + '&productionItemTypeID=' + productionItemTypeID + '&startDate=' + startDate,
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

        function getDataWithFilters(factoryWarehouseID, productionItemTypeID, startDate, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'getdatawithfilters?factoryWarehouseID=' + factoryWarehouseID + '&productionItemTypeID=' + productionItemTypeID + '&startDate=' + startDate,
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
