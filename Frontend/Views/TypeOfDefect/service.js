﻿(function () {
    "use strict";

    angular.module("tilsoftApp").service("dataService", unitService);
    unitService.$inject = ["$http", "jsonService"];

    function unitService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "TypeOfDefectNM";
        self.searchFilter.sortedDirection = "ASC";
    };
})();