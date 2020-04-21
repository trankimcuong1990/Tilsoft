(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', workCenterService);
    workCenterService.$inject = ['$http', 'jsonService'];

    function workCenterService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;

        self.searchFilter.sortedBy = 'WorkCenterID';
        self.searchFilter.sortedDirection = 'ASC';
    };
})();