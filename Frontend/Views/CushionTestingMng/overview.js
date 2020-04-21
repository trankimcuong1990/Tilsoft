

(function () {
    'use strict';

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', cushionTestingController);
    cushionTestingController.$inject = ['$scope', 'dataService'];

    function cushionTestingController($scope, cushionTestingService) {
        $scope.data = [];
        $scope.searchString = null;
        $scope.IDX = 0;
        $scope.getIDX = 0;
        $scope.newid = 0;

        $scope.materialTestStandards = [];


        $scope.event = {
            init: init,
            get: get
        };


        function init() {
            cushionTestingService.serviceUrl = context.serviceUrl;
            cushionTestingService.supportServiceUrl = context.supportServiceUrl;
            cushionTestingService.token = context.token;

            $scope.event.get();
        };

        function get() {
            cushionTestingService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.dataOverView;
                    $scope.cushionTestStandards = data.data.supportCushionTestStandards;
                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };     

        $scope.event.init();
    };
})();