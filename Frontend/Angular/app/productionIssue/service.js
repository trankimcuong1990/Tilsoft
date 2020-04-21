(function () {
    'use strict';

    angular
        .module('tilsoftApp')
        .service('dataService', productionIssueOverviewService);

    productionIssueOverviewService.$inject = ['$http', 'jsonService'];

    function productionIssueOverviewService($http, jsonService) {
        angular.extend(this, jsonService);

        var vm = this;
        vm.searchFilter.sortedBy = 'BOMID';
        vm.searchFilter.sortedDirection = 'ASC';
        vm.getHistoryDeliveryNote = getHistoryDeliveryNote;

        function getHistoryDeliveryNote(workOrderID, workCenterID, productionItemID, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + this.token
                },
                type: 'POST',
                url: vm.serviceUrl + 'getHistoryDeliveryNote?workOrderID=' + workOrderID + '&workCenterID=' + workCenterID + '&productionItemID=' + productionItemID,
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