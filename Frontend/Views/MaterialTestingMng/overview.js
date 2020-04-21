

(function () {
    'use strict';

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', materialTestingController);
    materialTestingController.$inject = ['$scope', 'dataService'];

    function materialTestingController($scope, materialTestingService) {
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
            materialTestingService.serviceUrl = context.serviceUrl;
            materialTestingService.supportServiceUrl = context.supportServiceUrl;
            materialTestingService.token = context.token;

            $scope.event.get();
        };

        function get() {
            materialTestingService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.dataOverView;
                    $scope.materialTestStandards = data.data.supportMaterialTestStandards;
                    jQuery('#content').show();                   
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };
    

        $scope.event.init();
    };
})();