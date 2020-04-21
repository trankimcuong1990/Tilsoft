(function () {
    'use strict';

    var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);
    tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
        dataService.searchFilter.pageSize = context.pageSize;
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;
        //
        // grid handler
        //
        $scope.gridHandler = {

        };
        //
        // property
        //

        //
        // events
        //
        $scope.event = {
            init: function () {
                jQuery('#content').show();
                //$scope.event.reload();
            }

        };
        //
        // method
        //
        $scope.method = {

        };
        //
        // angular init
        //
        $timeout(function () {
            $scope.event.init();
        }, 0);
    }]);

})();