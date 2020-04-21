(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', cushionTestingService);
    cushionTestingService.$inject = ['$http', 'jsonService'];

    function cushionTestingService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'CushionTestReportUD';
        self.searchFilter.sortedDirection = 'ASC';
    };
})();