(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', deliveryNoteRepairAssemplyService);
    deliveryNoteRepairAssemplyService.$inject = ['$http', 'jsonService'];

    function deliveryNoteRepairAssemplyService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'DeliveryNoteUD';
        self.searchFilter.sortedDirection = 'ASC';

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
    };
})();