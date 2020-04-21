(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', systemSetting2Service);
    systemSetting2Service.$inject = ['$http', 'jsonService'];

    function systemSetting2Service($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;

        self.searchFilter.sortedBy = 'SystemSettingID';
        self.searchFilter.sortedDirection = 'ASC';
        self.coppyitemfromseasion = coppyitemfromseasion;
    };
    function coppyitemfromseasion(season, successCallBack, errorCallBack) {
        jQuery.ajax({
            cache: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            },
            type: "POST",
            url: this.serviceUrl + 'coppyseason?season=' + season,
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
})();
