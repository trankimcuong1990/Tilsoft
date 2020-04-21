(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", costInvoice2TypeMngService);
    costInvoice2TypeMngService.$inject = ["$http", "jsonService"];

    function costInvoice2TypeMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "CostInvoice2TypeNM";
        self.searchFilter.sortedDirection = "ASC";
    };
})();