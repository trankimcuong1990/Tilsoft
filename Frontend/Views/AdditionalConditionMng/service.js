(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", additionService);
    additionService.$inject = ["$http", "jsonService"];

    function additionService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "UpdateBy";
        self.searchFilter.sortedDirection = "ASC";
    };
})();