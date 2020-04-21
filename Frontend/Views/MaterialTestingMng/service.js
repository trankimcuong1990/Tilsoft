(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', materialTestingService);
    materialTestingService.$inject = ['$http', 'jsonService'];

    function materialTestingService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'MaterialTestReportUD';
        self.searchFilter.sortedDirection = 'ASC';        
    };
})();