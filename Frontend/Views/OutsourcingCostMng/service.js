(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", outsourcingCostMngService);
    outsourcingCostMngService.$inject = ["$http", "jsonService"];

    function outsourcingCostMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "OutsourcingCostNM";
        self.searchFilter.sortedDirection = "ASC";

    };
})();