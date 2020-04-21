(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", clientPermissionService);
    clientPermissionService.$inject = ["$http", "jsonService"];

    function clientPermissionService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        //self.searchFilter.sortedBy = "ProductionTeamUD";
        //self.searchFilter.sortedDirection = "ASC";
    };
})();