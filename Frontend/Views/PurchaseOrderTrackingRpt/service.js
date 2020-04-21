(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', purchaseOrderTrackingRptService);
    purchaseOrderTrackingRptService.$inject = ['$http', 'jsonService'];

    function purchaseOrderTrackingRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'PurchaseOrderID';
        self.searchFilter.sortedDirection = 'ASC';

        self.getExcelReport = function (data, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: "POST",
                dataType: 'json',
                data: JSON.stringify(data),
                url: this.serviceUrl + 'getexcelreport',
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