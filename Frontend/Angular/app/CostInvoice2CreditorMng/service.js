(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", costInvoice2CreditorMngService);
    costInvoice2CreditorMngService.$inject = ["$http", "jsonService"];

    function costInvoice2CreditorMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "CostInvoice2CreditorNM";
        self.searchFilter.sortedDirection = "ASC";
    };
})();