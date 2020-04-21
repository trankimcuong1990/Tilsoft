(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', purchaseOrderService);
    purchaseOrderService.$inject = ['$http', 'jsonService'];

    function purchaseOrderService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'PurchaseOrderID';
        self.searchFilter.sortedDirection = 'DESC';
        self.getPurchaseOrderDetailByPurchaseRequestID = getPurchaseOrderDetailByPurchaseRequestID;
        self.excelPurchaseOrderReport = excelPurchaseOrderReport;
        self.getPurchaseQuotationDetail = getPurchaseQuotationDetail;
        self.getPurchaseOrderPrintout = getPurchaseOrderPrintout;
        self.getSupplierPaymentTerm = getSupplierPaymentTerm;
        self.cancel = cancel;
        self.finish = finish;
        self.revise = revise;

        function getPurchaseOrderDetailByPurchaseRequestID(purchaseRequestID, factoryRawMaterialID, purchaseOrderDate, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'getPurchaseOrderDetailByPurchaseRequestID?purchaseRequestID=' + purchaseRequestID + '&factoryRawMaterialID=' + factoryRawMaterialID + '&purchaseOrderDate=' + purchaseOrderDate,
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

        function excelPurchaseOrderReport(id, successCallBack, errorCallBack) {
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
                url: self.serviceUrl + 'getexcelreport?id=' + id,
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

        function getPurchaseQuotationDetail(factoryRawMaterialID, purchaseOrderDate, iSuccessCallback, iErrorCallback) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'get-purchase-quotation-detail?factoryRawMaterialID=' + factoryRawMaterialID + '&purchaseOrderDate=' + purchaseOrderDate,
                beforeSend: function () {
                    jsHelper.loadingSwitch(true);
                },
                success: function (data) {
                    jsHelper.loadingSwitch(false);
                    iSuccessCallback(data);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    jsHelper.loadingSwitch(false);
                    iErrorCallback(xhr.responseJSON.exceptionMessage);
                }
            });
        }

        function getPurchaseOrderPrintout(purchaseOrderID, successCallBack, errorCallBack) {
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
                url: self.serviceUrl + 'getPurchaseOrderPrintout?purchaseOrderID=' + purchaseOrderID,
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

        function getSupplierPaymentTerm(factoryRawMaterialID, iSuccessCallback, iErrorCallback) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                dataType: "json",
                url: self.serviceUrl + 'getSupplierPaymentTerm?factoryRawMaterialID=' + factoryRawMaterialID,
                beforeSend: function () {
                    jsHelper.loadingSwitch(true);
                },
                success: function (data) {
                    jsHelper.loadingSwitch(false);
                    iSuccessCallback(data);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    jsHelper.loadingSwitch(false);
                    iErrorCallback(xhr.responseJSON.exceptionMessage);
                }
            });
        }

        function cancel(id, data, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: "POST",
                dataType: "json",
                data: JSON.stringify(data),
                url: self.serviceUrl + 'cancel?id=' + id,
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

        function finish(id, data, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: "POST",
                dataType: "json",
                data: JSON.stringify(data),
                url: self.serviceUrl + 'finish?id=' + id,
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

        function revise(id, data, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: "POST",
                dataType: "json",
                data: JSON.stringify(data),
                url: self.serviceUrl + 'revise?id=' + id,
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