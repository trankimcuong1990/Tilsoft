(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", defectService);
    defectService.$inject = ["$http", "jsonService"];

    function defectService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "DefectUD";
        self.searchFilter.sortedDirection = "ASC";
    };
})();
