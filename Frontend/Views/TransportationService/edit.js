(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive']).controller('tilsoftController', transportationServiceMngController);
    transportationServiceMngController.$inject = ['$scope', 'dataService'];

    function transportationServiceMngController($scope, transportationServiceMngService) {
        $scope.data = [];
        $scope.newID = -1;

        $scope.event = {
            get: get,
            remove: remove,
            update: update
        };

        function get() {
            transportationServiceMngService.serviceUrl = context.serviceUrl;
            transportationServiceMngService.token = context.token;

            transportationServiceMngService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
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
            transportationServiceMngService.delete(
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
                transportationServiceMngService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.transportationServiceID;
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