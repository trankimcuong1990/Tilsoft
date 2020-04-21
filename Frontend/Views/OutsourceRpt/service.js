(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', outsourceRptService);
    outsourceRptService.$inject = ['$http', 'jsonService'];

    function outsourceRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'WorkOrderUD';
        self.searchFilter.sortedDirection = 'ASC';

        self.getProductionItem = getProductionItem;
        self.getDocumentNote = getDocumentNote;
        self.exportOutsourceReport = exportOutsourceReport;

        function getProductionItem(filters, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                data: JSON.stringify(filters),
                url: self.serviceUrl + 'getProductionItem',
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

        function getDocumentNote(filters, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                data: JSON.stringify(filters),
                url: self.serviceUrl + 'getDocumentNote',
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

        function exportOutsourceReport(filters, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                data: JSON.stringify(filters),
                url: self.serviceUrl + 'exportOutsourceReport',
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