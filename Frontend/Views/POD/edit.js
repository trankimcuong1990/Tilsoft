(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive']).controller('tilsoftController', PODMngController);
    PODMngController.$inject = ['$scope', 'dataService'];

    function PODMngController($scope, PODMngService) {
        $scope.data = [];
        $scope.clientCountries = [];
        $scope.newID = -1;

        $scope.event = {
            get: get,
            remove: remove,
            update: update
        };

        function get() {
            PODMngService.serviceUrl = context.serviceUrl;
            PODMngService.token = context.token;

            PODMngService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.clientCountries = data.data.clientCountries;
                    jQuery('#content').show();
                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function remove(id) {
            PODMngService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    //not thing to do
                });
        };

        function update() {
            if ($scope.editForm.$valid) {
                PODMngService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.podid;
                            $scope.data = data.data.data;
                        }
                    },
                    function (error) {
                        //not thing to do
                    });
            } else {
                jsHelper.showMessage('warning', context.msgValid);
            }
        };

        $scope.event.get();

    };
})();