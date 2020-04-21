(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", transportationServiceMngService);
    transportationServiceMngService.$inject = ["$http", "jsonService"];

    function transportationServiceMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "TransportationServiceNM";
        self.searchFilter.sortedDirection = "ASC";

    };
})();