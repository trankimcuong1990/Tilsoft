(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', productBreakDownDefaultPALService);
    productBreakDownDefaultPALService.$inject = ['$http', 'jsonService'];

    function productBreakDownDefaultPALService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = "ProductBreakDownDefaultCategoryID";
        self.searchFilter.sortedDirection = "ASC";
    };
})();