(function () {
    'use strict';
    angular.module('tilsoftApp').service('dataService', opSequenceService);
    opSequenceService.$inject = ['$http', 'jsonService'];

    function opSequenceService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;

        self.searchFilter.sortedBy = 'UpdatedDate';
        self.searchFilter.sortedDirection = 'DESC';
    };
})();