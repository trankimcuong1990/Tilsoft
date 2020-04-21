(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", eurofarSampleCollectionMngService);
    eurofarSampleCollectionMngService.$inject = ["$http", "jsonService"];

    function eurofarSampleCollectionMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "SampleProductID";
        self.searchFilter.sortedDirection = "DESC";
        self.exportExcel = exportExcel;
        function exportExcel(filters, successCallBack, errorCallBack) {
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
                url: self.serviceUrl + 'exportexcel',
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