(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", PODMngService);
    PODMngService.$inject = ["$http", "jsonService"];

    function PODMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "PoDname";
        self.searchFilter.sortedDirection = "ASC";

    };
})();
