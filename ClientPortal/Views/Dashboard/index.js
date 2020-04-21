angular.module('cpApp').controller('cpController', ['$scope', '$cookies', '$timeout', 'dataService', function ($scope, $cookies, $timeout, dataService) {
    //
    // init
    //
    $timeout(function () {
        if ($cookies.get(context.userStore)) {
            var user = JSON.parse($cookies.get(context.userStore));
            $('#content .page-title').html('Welcome back ' + user.fullName + '!');
        }        
    }, 0);
}]);