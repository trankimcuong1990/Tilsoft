(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', inventoryRptService);
    inventoryRptService.$inject = ['$http', 'jsonService'];

    function inventoryRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;

        self.exportInventoryReport = exportInventoryReport;
        self.getDataWithFilters = getDataWithFilters;
        self.exportInventoryReportDetail = exportInventoryReportDetail;
        self.getInitCustom = getInitCustom;
        self.getDataFilterCustom = getDataFilterCustom;
        self.exportDataFilterCustom = exportDataFilterCustom;

        function exportInventoryReport(factoryWarehouseID, productionTeamID, startDate, endDate, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'exportinventoryreport?factoryWarehouseID=' + factoryWarehouseID + '&productionTeamID=' + productionTeamID + '&startDate=' + startDate + '&endDate=' + endDate,
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

        function getDataWithFilters(factoryWarehouseID, productionTeamID, startDate, endDate, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'getdatawithfilters?factoryWarehouseID=' + factoryWarehouseID + '&productionTeamID=' + productionTeamID + '&startDate=' + startDate + '&endDate=' + endDate,
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

        function exportInventoryReportDetail(factoryWarehouseID, productionTeamID, startDate, endDate, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'exportinventoryreportdetail?factoryWarehouseID=' + factoryWarehouseID + '&productionTeamID=' + productionTeamID + '&startDate=' + startDate + '&endDate=' + endDate,
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

        /// custom function
        /// get init with branchID
        function getInitCustom(branchID, successCallBack, errorCallBack) {
            jsHelper.loadingSwitch(true);

            $http({
                method: 'POST',
                url: this.serviceUrl + 'get-init-custom?branchID=' + branchID,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type == 0) {
                    successCallBack(response.data);
                }
                else {
                    jsHelper.showMessage('warning', response.data.message.message);
                    errorCallBack(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage('warning', response.data.message);
                errorCallBack(response);
            });
        };
        /// get data filter
        function getDataFilterCustom(factoryWarehouses, productionTeamID, startDate, endDate, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'get-data-filter-custom?factoryWarehouses=' + factoryWarehouses + '&productionTeamID=' + productionTeamID + '&startDate=' + startDate + '&endDate=' + endDate,
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
        };


        function exportDataFilterCustom(factoryWarehouses, productionTeamID, startDate, endDate, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'export-data-filter-custom?factoryWarehouses=' + factoryWarehouses + '&productionTeamID=' + productionTeamID + '&startDate=' + startDate + '&endDate=' + endDate,
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
        };
    };
})();
