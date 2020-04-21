(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive']).controller('tilsoftController', dueColorMngController);
    dueColorMngController.$inject = ['$scope', 'dataService'];

    function dueColorMngController($scope, dueColorMngService) {
        $scope.data = [];
        $scope.newID = -1;

        $scope.event = {
            get: get,
            remove: remove,
            update: update
        };

        function get() {
            dueColorMngService.serviceUrl = context.serviceUrl;
            dueColorMngService.token = context.token;

            dueColorMngService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    if (context.id > 0) {
                        document.getElementById('foo').value = data.data.data.dueColorUD;
                        document.getElementById('foo').style.backgroundColor = '#' + data.data.data.dueColorUD;
                    }
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
            dueColorMngService.delete(
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
                dueColorMngService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.dueColorID;
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