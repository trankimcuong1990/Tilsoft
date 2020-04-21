angular.module('tilsoftApp').service('dataService', ['$http', 'jsonService', function ($http, jsonService) {
    angular.extend(this, jsonService);
    this.searchFilter.sortedBy = 'MaterialNM';
    this.searchFilter.sortedDirection = 'ASC';
}]);