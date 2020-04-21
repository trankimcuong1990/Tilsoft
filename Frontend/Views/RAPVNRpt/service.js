(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', rapVNRptService);
    rapVNRptService.$inject = ['$http', 'jsonService'];

    function rapVNRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'FactoryID';
        self.searchFilter.sortedDirection = 'ASC';
        self.getDataWithFilters = getDataWithFilters;
        self.generateExcel = generateExcel;
     
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
                url: context.serviceUrl + 'getdatawithfilters',
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

        function generateExcel(season, factoryID, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + context.token
                },
                type: "POST",
                dataType: 'json',
                url: context.serviceUrl + 'getexcelreport?season=' + season + '&factoryID=' + factoryID,
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