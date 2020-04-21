(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', moduleStatusMngService);
    moduleStatusMngService.$inject = ['$http', 'jsonService'];

    function moduleStatusMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'ModuleID';
        self.searchFilter.sortedDirection = 'ASC';
    };
})();