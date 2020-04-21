(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive']).controller('tilsoftController', supplierPaymentTermMngController);
    supplierPaymentTermMngController.$inject = ['$scope', 'dataService'];

    function supplierPaymentTermMngController($scope, supplierPaymentTermMngService) {
        $scope.data = [];
        $scope.newID = -1;

        $scope.event = {
            get: get,
            remove: remove,
            update: update
        };

        function get() {
            supplierPaymentTermMngService.serviceUrl = context.serviceUrl;
            supplierPaymentTermMngService.token = context.token;

            supplierPaymentTermMngService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    jQuery('#content').show();

                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function remove(id) {
            supplierPaymentTermMngService.delete(
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
                supplierPaymentTermMngService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.supplierPaymentTermID;
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