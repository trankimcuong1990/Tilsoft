(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", productionTeamService);
    productionTeamService.$inject = ["$http", "jsonService"];

    function productionTeamService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "ProductionTeamUD";
        self.searchFilter.sortedDirection = "ASC";
    };
})();