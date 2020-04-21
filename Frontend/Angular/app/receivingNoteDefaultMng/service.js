﻿(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", receivingNoteService);
    receivingNoteService.$inject = ["$http", "jsonService"];

    function receivingNoteService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "UpdatedDate";
        self.searchFilter.sortedDirection = "DESC";
        self.searchProductionItemByScanQRCode = searchProductionItemByScanQRCode;
        function searchProductionItemByScanQRCode(searchString, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.supportServiceUrl + 'quickSearchProductionItem?param=' + searchString,
                //data: JSON.stringify(filters),
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

        self.exportQRCode = exportQRCode;
        function exportQRCode(sampleProductIDs, qntBarcodes, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'exportQRCode?id=' + sampleProductIDs + '&qnt=' + qntBarcodes,
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


