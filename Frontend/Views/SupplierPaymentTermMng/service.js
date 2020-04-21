(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", supplierPaymentTermMngService);
    supplierPaymentTermMngService.$inject = ["$http", "jsonService"];

    function supplierPaymentTermMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "SupplierPaymentTermNM";
        self.searchFilter.sortedDirection = "ASC";

    };
})();