(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', qcReportDefaultSettingService);
    qcReportDefaultSettingService.$inject = ['$http', 'jsonService'];

    function qcReportDefaultSettingService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;
        self.searchFilter.sortedBy = 'RowIndex';
        self.searchFilter.sortedDirection = 'ASC';
    }
})();