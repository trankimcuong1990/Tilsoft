(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', stockOverViewRptService);
    stockOverViewRptService.$inject = ['$http', 'jsonService'];

    function stockOverViewRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'ItemSource';
        self.searchFilter.sortedDirection = 'ASC';
        self.generateExcel = generateExcel;

        function generateExcel(successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + context.token
                },
                type: "POST",
                dataType: 'json',
                data: JSON.stringify(this.searchFilter),
                url: context.serviceUrl + 'getexcelreport',
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