(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', factorySaleQuotationService);
    factorySaleQuotationService.$inject = ['$http', 'jsonService'];

    function factorySaleQuotationService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'DocumentDate';
        self.searchFilter.sortedDirection = 'DESC';
        self.cancel = cancel;
        function cancel(id, data, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: "POST",
                dataType: "json",
                data: JSON.stringify(data),
                url: self.serviceUrl + 'cancel?id=' + id,
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