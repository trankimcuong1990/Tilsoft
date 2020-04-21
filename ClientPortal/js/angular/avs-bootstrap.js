angular.module('cpApp', ['ngCookies', 'ngSanitize', 'avs-directives']);

// header controller
angular.module('cpApp').controller('cplnController', ['$scope', '$cookies', '$timeout', function ($scope, $cookies, $timeout) {
    $scope.cplnData = {};

    //
    // init
    //
    $timeout(function () {
        if ($cookies.get(context.userStore)) {
            $scope.cplnData = JSON.parse($cookies.get(context.userStore));
            if ($scope.cplnData.personalPhoto_DisplayUrl) {
                $scope.cplnData.personalPhoto_DisplayUrl = $scope.cplnData.personalPhoto_DisplayUrl;
            }
        }
    }, 0);
}]);