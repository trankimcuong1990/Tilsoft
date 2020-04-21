(function () {
    'use strict';

    angular
        .module('tilsoftApp')
        .service('dataService', priceConfigurationService);

    priceConfigurationService.$inject = ['$http', 'jsonService'];

    function priceConfigurationService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;

        self.searchFilter.sortedBy = 'Season';
        self.searchFilter.sortedDirection = 'DESC';

        self.load = load;
        self.getAllSupportWithSeason = getAllSupportWithSeason;
        self.deleted = deleted;

        function load(id, season, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: "POST",
                url: self.serviceUrl + 'get?id=' + id +'&season=' + season,
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

        function getAllSupportWithSeason(filters, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: "POST",
                dataType: 'json',
                data: JSON.stringify(filters),
                url: self.serviceUrl + 'allsupportwithseason',
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

        function deleted(id, season, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: "POST",
                url: self.serviceUrl + 'delete?id=' + id + '&season=' + season,
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