(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', outSourcingCostMngController);
    outSourcingCostMngController.$inject = ['$scope', 'dataService', '$timeout'];

    function outSourcingCostMngController($scope, outsourcingCostMngService, $timeout) {
        // variable
        $scope.data = [];
        $scope.workCenters = [];

        // event
        $scope.event = {
            get: get,
            update: update,
            deleted: deleted
        };

        $scope.listAdditionalCost = [
            { isAdditionalCost: true, name: 'Yes' },
            { isAdditionalCost: false, name: 'No' }
        ];
        function get() {
            outsourcingCostMngService.serviceUrl = context.serviceUrl;
            outsourcingCostMngService.token = context.token;
            outsourcingCostMngService.load(
                context.id,
                null,
                function (data) {
                    $scope.workCenters = data.data.workCenters;
                    $scope.data = data.data.data;
                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function update() {
            if (jQuery('#outsourcingCostNM').val() === null || jQuery('#outsourcingCostNM').val() === '') {
                jsHelper.showMessage('warning', 'Please input to Cost Name.');
                return false;
            }

            if (jQuery('#outsourcingCostNMEN').val() === null || jQuery('#outsourcingCostNMEN').val() === '') {
                jsHelper.showMessage('warning', 'Please input to Cost Name EN.');
                return false;
            }

            if ($scope.data.workCenterID === null || $scope.data.workCenterID === '') {
                jsHelper.showMessage('warning', 'Please select to Material Group.');
                return false;
            }

            if ($scope.data.isAdditionalCost === null || $scope.data.isAdditionalCost === '') {
                jsHelper.showMessage('warning', 'Please select to Additional Cost.');
                return false;
            }

            outsourcingCostMngService.update(
                context.id,
                $scope.data,
                function (data) {
                    jsHelper.processMessage(data.message);

                    if (data.message.type === 0) {
                        window.location = context.refreshUrl + data.data.outsourcingCostID;
                        $scope.data = data.data;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.message.message);
                });
        };

        function deleted(id) {
            outsourcingCostMngService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {

                });
        };

        // default event
        $scope.event.get();
    }
})();