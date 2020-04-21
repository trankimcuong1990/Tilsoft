(function () {
    'use strict';
    angular.module('tilsoftApp').service('dataService', factoryLocationService);
    factoryLocationService.$inject = ['$http', 'jsonService'];

    function factoryLocationService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'ManufacturerCountryID';
        self.searchFilter.sortedDirection = 'ASC';
    };
})();