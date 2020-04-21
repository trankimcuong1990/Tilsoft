angular.module('tilsoftApp').service('dataService', ['$http', 'jsonService', function ($http, jsonService) {
    angular.extend(this, jsonService);
    this.searchFilter.sortedBy = 'CompanyID';
    this.searchFilter.sortedDirection = 'ASC';
}]);