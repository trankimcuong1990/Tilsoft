(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", testStandardMngService);
    testStandardMngService.$inject = ["$http", "jsonService"];

    function testStandardMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "TestStandardNM";
        self.searchFilter.sortedDirection = "ASC";               

    };
})();