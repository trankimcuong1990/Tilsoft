(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", frameMaterialMngService);
    frameMaterialMngService.$inject = ["$http", "jsonService"];

    function frameMaterialMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "FrameMaterialNM";
        self.searchFilter.sortedDirection = "ASC";

    }
})();