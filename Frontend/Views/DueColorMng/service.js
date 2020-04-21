(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", dueColorMngService);
    dueColorMngService.$inject = ["$http", "jsonService"];

    function dueColorMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "DueColorNM";
        self.searchFilter.sortedDirection = "ASC";

    };
})();