(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', outSourcingCostTypeMngController);
    outSourcingCostTypeMngController.$inject = ['$scope', 'dataService', '$timeout'];

    function outSourcingCostTypeMngController($scope, outSourcingCostTypeMngService, $timeout) {
        // variable
        $scope.data = [];
        $scope.productionItemGroups = [];

        // event
        $scope.event = {
            get: get,
            update: update,
            deleted: deleted
        };

        function get() {
            outSourcingCostTypeMngService.serviceUrl = context.serviceUrl;
            outSourcingCostTypeMngService.token = context.token;
            outSourcingCostTypeMngService.load(
                context.id,
                null,
                function (data) {
                    $scope.productionItemGroups = data.data.productionItemGroups;
                    $scope.data = data.data.data;
                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function update() {
            if (jQuery('#outsourcingCostTypeNM').val() === null || jQuery('#outsourcingCostTypeNM').val() === '') {
                jsHelper.showMessage('warning', 'Please input to Cost Type Name.');
                return false;
            }

            if (jQuery('#outsourcingCostTypeNMEN').val() === null || jQuery('#outsourcingCostTypeNMEN').val() === '') {
                jsHelper.showMessage('warning', 'Please input to Cost Type Name EN.');
                return false;
            }

            if ($scope.data.productionItemGroupID === null || $scope.data.productionItemGroupID === '') {
                jsHelper.showMessage('warning', 'Please select to Material Group.');
                return false;
            }

            outSourcingCostTypeMngService.update(
                context.id,
                $scope.data,
                function (data) {
                    jsHelper.processMessage(data.message);

                    if (data.message.type === 0) {
                        window.location = context.refreshUrl + data.data.outsourcingCostTypeID;
                        $scope.data = data.data;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.message.message);
                });
        };

        function deleted(id) {
            outSourcingCostTypeMngService.delete(
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