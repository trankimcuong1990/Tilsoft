angular.module('cpApp').controller('searchFilter', ['$scope', '$rootScope', '$cookies', '$timeout', 'dataService', function ($scope, $rootScope, $cookies, $timeout, dataService) {
    $scope.data = angular.copy($scope.$parent.data.filters);

    $scope.event = {
        goToResult: function () {
            $rootScope.$emit('frmResult');
        },
        applyFilter: function () {
            $scope.$parent.data.filters = angular.copy($scope.data);
            $rootScope.$emit('frmResult_reload');
        }
    };

    $rootScope.$on('frmFilter', function (event, data) {
        jsHelper.showForm('frmFilter');
    });
}]);