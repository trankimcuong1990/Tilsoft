(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", dataService);
    dataService.$inject = ["$http", "jsonService"];

    function dataService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "ProjectOnHand";
        self.searchFilter.sortedDirection = "DESC";
    }


})();