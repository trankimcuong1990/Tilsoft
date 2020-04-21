angular.module('cpApp').service('dataService', ['$http', '$cookies', 'avsDataService', function ($http, $cookies, avsDataService) {
    angular.extend(this, avsDataService);
    this.serviceUrl = context.serviceUrl;
    this.token = $cookies.get(context.authStore);
}]);