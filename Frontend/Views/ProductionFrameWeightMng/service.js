(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", productionFrameWeightMngService);
    productionFrameWeightMngService.$inject = ["$http", "jsonService"];

    function productionFrameWeightMngService($http, jsonService) {
      
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "ProductionFrameWeightID";
        self.searchFilter.sortedDirection = "DESC";
        self.generateExcel = generateExcel;

        function generateExcel(workOrderUD, clientUD, proformaInvoiceNo, productionItem, successCallBack, errorCallBack) {
            debugger;
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + context.token
                },
                type: "POST",
                dataType: 'json',
                url: context.serviceUrl + 'getexcelreport?workOrderUD=' + workOrderUD + '&clientUD=' + clientUD + '&proformaInvoiceNo=' + proformaInvoiceNo + '&productionItem=' + productionItem,
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