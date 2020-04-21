angular.module('tilsoftApp').service('dataService', ['$http', 'jsonService', function ($http, jsonService) {
    angular.extend(this, jsonService);

    this.searchFilter.sortedBy = 'Season';
    this.searchFilter.sortedDirection = 'DESC';
}]);