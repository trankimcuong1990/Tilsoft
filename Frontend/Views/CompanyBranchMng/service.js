(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', companybranchMngService);
    companybranchMngService.$inject = ['$http', 'jsonService'];

    function companybranchMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'CompanyID';
        self.searchFilter.sortedDirection = 'ASC';
        self.getPurchaseOrderDetailByPurchaseRequestID = getPurchaseOrderDetailByPurchaseRequestID;

        function getPurchaseOrderDetailByPurchaseRequestID(purchaseRequestID, factoryRawMaterialID, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: self.serviceUrl + 'getPurchaseOrderDetailByPurchaseRequestID?purchaseRequestID=' + purchaseRequestID + '&factoryRawMaterialID=' + factoryRawMaterialID,
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