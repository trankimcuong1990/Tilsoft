﻿angular
.module('tilsoftApp')
.service('dataService', ['$http', 'jsonService', function($http, jsonService) {
    angular.extend(this, jsonService);

    this.searchFilter.sortedBy = 'UpdatedDate';
    this.searchFilter.sortedDirection = 'DESC';

    //#region
    this.quickSearchClient = function (filters, successCallBack, errorCallBack) {
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
            url: this.supportServiceUrl + 'quicksearchclient',
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
    //#endregion
}]);