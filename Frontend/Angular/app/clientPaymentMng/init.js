//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.clients = null;
    $scope.methods = null;
    $scope.selection = {
        clientID: null,
        clientPaymentMethodID: false,
        currency: ''
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getInitData(
                function (data) {
                    $scope.clients = data.data.clients;
                    $scope.methods = data.data.clientPaymentMethods;
                    $scope.selection.clientID = null;
                    $scope.selection.clientPaymentMethodID = null;
                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.methods = null;
                    $scope.selection.clientID = null;
                    $scope.selection.clientPaymentMethodID = null;
                    $scope.$apply();
                }
            );
        },
        next: function () {
            if ($scope.initForm.$valid) {
                window.location = context.nextUrl + '?clientid=' + $scope.selection.clientID + '&m=' + $scope.selection.clientPaymentMethodID + '&c=' + $scope.selection.currency;
            }
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);