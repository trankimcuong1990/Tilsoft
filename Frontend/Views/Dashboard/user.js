//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$rootScope', '$timeout', function ($scope, $rootScope, $timeout) {
    $scope.gridHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
        },
        isTriggered: true,
    };
    $scope.event = {
        init: function () {
            // to do
            jQuery('#content').show();
        }
    };

    $scope.event.init();
}]);