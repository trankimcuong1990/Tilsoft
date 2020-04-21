(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive']).controller('tilsoftController', productBreakDownDefaultCategoryMngController);
    productBreakDownDefaultCategoryMngController.$inject = ['$scope', 'dataService'];

    function productBreakDownDefaultCategoryMngController($scope, dataService) {
        $scope.data = [];
        $scope.newID = -1;

        $scope.event = {
            getviewdata: getviewdata
        };

        function getviewdata() {debugger
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;

            dataService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.defaultData;

                    jQuery('#content').show();

                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
            //dataService.serviceUrl = context.serviceUrl;
            //dataService.token = context.token;

            //dataService.load(
            //    context.id,
            //    null,
            //    function (data) {
            //        $scope.data = data.data.data;

            //        $scope.$apply();

            //        jQuery('#content').show();

            //    },
            //    function (error) {
            //        jsHelper.showMessage('warning', error);
            //    });
        };

        $scope.event.getviewdata();

        function createservices() {
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
            dataService.supportServiceUrl = context.supportServiceUrl;

        };
    };
})();