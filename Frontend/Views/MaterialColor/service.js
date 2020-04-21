(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", materialColorMngService);
    materialColorMngService.$inject = ["$http", "jsonService"];

    function materialColorMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "MaterialColorNM";
        self.searchFilter.sortedDirection = "ASC";

    };
})();