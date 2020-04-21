(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', costInvoice2Service);
    costInvoice2Service.$inject = ['$http', 'jsonService'];

    function costInvoice2Service($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'Season';
        self.searchFilter.sortedDirection = 'DESC';

        ///===================================================================///
        self.exportCostInvoice2 = exportCostInvoice2;

        function exportCostInvoice2(season, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'exportcostinvoice2?season=' + season,
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

        //==============================================================================//
    };
})();