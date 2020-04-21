(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', purchasingPriceComparisonMngService);
    purchasingPriceComparisonMngService.$inject = ['$http', 'jsonService'];

    function purchasingPriceComparisonMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'SeaSon';
        self.searchFilter.sortedDirection = 'ASC';
        self.generateExcel = generateExcel;

        function generateExcel(season, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + context.token
                },
                type: "POST",
                dataType: 'json',
                url: context.serviceUrl + 'getreportdata?season=' + season,
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