//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
//create controller
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', 'dataService', function ($scope, $cookieStore, dataService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    $scope.seasons = jsHelper.getSeasons();

    $scope.selection = {
        clientID: null,
        season: jsHelper.getCurrentSeason()
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
            $('#content').show();
        },
        goNext: function () {
            if ($scope.editForm.$valid) {
                window.location = context.nextURL + '0?c=' + $scope.selection.clientID + '&s=' + $scope.selection.season;
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);