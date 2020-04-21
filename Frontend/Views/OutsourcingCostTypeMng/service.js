(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', outSourcingCostTypeMngService);
    outSourcingCostTypeMngService.$inject = ['$http', 'jsonService'];

    function outSourcingCostTypeMngService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "OutsourcingCostTypeUD";
        self.searchFilter.sortedDirection = "ASC";
    }
})();