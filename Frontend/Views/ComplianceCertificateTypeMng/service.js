(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", complianceService);
    complianceService.$inject = ["$http", "jsonService"];

    function complianceService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "ComplianceCertificateTypeNM";
        self.searchFilter.sortedDirection = "ASC";
    };
})();


