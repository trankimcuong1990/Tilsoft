var tilsoftApp = angular.module('tilsoftApp', []);

tilsoftApp.filter('sumFunc', function () {
    return function (data, key) {
        if (angular.isUndefined(data) || angular.isUndefined(key))
            return 0;
        var sum = parseFloat(0);
        angular.forEach(data, function (v, k) {
            sum = sum + parseFloat(v[key] == null ? 0 : v[key]);
        });
        return sum;
    }
});

tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    jQuery('#content').show();
}]);