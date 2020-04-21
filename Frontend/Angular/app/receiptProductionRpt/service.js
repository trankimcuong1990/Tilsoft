(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', ReceiptProductionRptService);
    ReceiptProductionRptService.$inject = ['$http', 'jsonService'];

    function ReceiptProductionRptService($http, jsonService) {
        angular.extend(this, jsonService);

        var vm = this;
        vm.searchFilter.sortedBy = 'PrimaryID';
        vm.searchFilter.sortedDirection = 'ASC';
    };
})();
