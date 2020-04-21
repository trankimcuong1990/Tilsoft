(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", POLMngService);
    POLMngService.$inject = ["$http", "jsonService"];

    function POLMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "PoLname";
        self.searchFilter.sortedDirection = "ASC";

    };
})();
