(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", draftCommercialInvoiceMngService);
    draftCommercialInvoiceMngService.$inject = ["$http", "jsonService"];

    function draftCommercialInvoiceMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "InvoiceNo";
        self.searchFilter.sortedDirection = "DESC";
        self.getSaleOrder = getSaleOrder;
        self.getOverview = getOverview;   
        self.getDraftCommercialPrintout = getDraftCommercialPrintout;
        function getSaleOrder(clientID, checkSelect, season, proformaInvoiceNo, articleCode, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'getsaleorder?clientID=' + clientID + '&checkSelect=' + checkSelect + '&season=' + season + '&proformaInvoiceNo=' + proformaInvoiceNo + '&articleCode=' + articleCode,
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
        function getOverview(id, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'getoverview?id=' + id,
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
        function getDraftCommercialPrintout(id, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: "POST",
                dataType: "json",
                //data: JSON.stringify(ids),
                url: self.serviceUrl + 'getDraftInvoicePrintoutData?id=' + id,
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