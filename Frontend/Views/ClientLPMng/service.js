(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', clientLPMngService);
    clientLPMngService.$inject = ['$http', 'jsonService'];

    function clientLPMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "ClientUD";
        self.searchFilter.sortedDirection = "ASC";
    }
})();