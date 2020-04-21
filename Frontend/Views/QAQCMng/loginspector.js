jQuery('#content').show();

'use strict';
var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap']);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {

    //
    // init service
    //
    dataService.serviceUrl = context.serviceUrl;
    dataService.supportServiceUrl = context.supportServiceUrl;
    dataService.token = context.token;

    //
    //property
    //
    $scope.data = null;

    //
    //function
    //
    $scope.event = {

        load: function () {
            dataService.getLoginspector(
                context.id,
                function (data) {
                    $scope.data = data.data;
                    jQuery('#content').show();
                },
                function (error) {
                    //Noting to do
                }
            );
        }

    };

    //
    //init controller
    // 
    $scope.event.load();

}]);