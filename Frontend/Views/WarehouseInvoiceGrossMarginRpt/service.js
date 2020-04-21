(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', warehouseInvoiceGrossMarginRptService);
    warehouseInvoiceGrossMarginRptService.$inject = ['$http', 'jsonService'];

    function warehouseInvoiceGrossMarginRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'InvoiceNo';
        self.searchFilter.sortedDirection = 'ASC';

        self.exportExcel = function (season, code, dateFrom, dateTo, orderBy, orderDirection, iSuccessCallback, iErrorCallback) {
            jsHelper.loadingSwitch(true);

            $http({
                method: "POST",
                url: self.serviceUrl + "export?season=" + season + "&code=" + code + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&orderBy=" + orderBy + "&orderDirection=" + orderDirection,
                data: JSON.stringify(this.searchFilter),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json",
                    "Authorization": "Bearer " + this.token
                }
            }).then(function successCallback(response) {
                jsHelper.loadingSwitch(false);

                if (response.data.message.type === 0) {
                    iSuccessCallback(response.data);
                }
                else {
                    jsHelper.showMessage("warning", response.data.message.message);
                    console.log(response.data.message.message);
                    iErrorCallback(response);
                }
            }, function errorCallback(response) {
                jsHelper.loadingSwitch(false);
                jsHelper.showMessage("warning", response.data.message);
                iErrorCallback(response);
            });
        };
    };
})();