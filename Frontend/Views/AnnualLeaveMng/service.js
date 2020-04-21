(function () {
    'use strict';

    angular.module('tilsoftApp').service('dataService', annualLeaveService);
    annualLeaveService.$inject = ['$http', 'jsonService'];

    function annualLeaveService($http, jsonService) {
        angular.extend(this, jsonService);

        var self = this;

        self.searchFilter.sortedBy = 'FromDate';
        self.searchFilter.sortedDirection = 'Desc';

        //var searchFilter = {
        //    filters: {
        //        employeeID: null,
        //        companyID: null,
        //        fromDate: null,
        //        toDate: null,
        //        days: null,
        //        statusID: null,
        //        statusUpdatedBy: null,
        //    },
        //    sortedBy: 'FromDate',
        //    sortedDirection: 'DESC',
        //    pageSize: 20,
        //    pageIndex: 1
        //};
    };
})();