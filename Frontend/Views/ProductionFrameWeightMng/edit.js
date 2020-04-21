
(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'furnindo-directive']).controller('tilsoftController', productionFrameWeightMngController);
    productionFrameWeightMngController.$inject = ['$scope', 'dataService', '$timeout'];

    function productionFrameWeightMngController($scope, productionFrameWeightMngService, $timeout) {
        $scope.data = [];
        $scope.newID = -1;

        $scope.event = {
            get: get,
            remove: remove,
            update: update
        };

        $scope.method = {
            addListDetailHistory: function () {           
                var addItem = {
                    productionFrameWeightHistoryID: $scope.method.getNewID(),
                    productionFrameWeightID: $scope.data.productionFrameWeightID,
                    frameWeight: $scope.data.frameWeight
                };
                $scope.data.productionFrameWeightHistory.push(addItem);
            },
            getNewID: function() {
                $scope.newID--;
                return $scope.newID;
            }
        };

        function get() {
            
            productionFrameWeightMngService.serviceUrl = context.serviceUrl;
            productionFrameWeightMngService.token = context.token;
            //productionFrameWeightMngService.supportServiceUrl = context.supportServiceUrl;
            productionFrameWeightMngService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data;

                    jQuery('#content').show();
                },
                function (error) {
                });
        };

        function remove(id) {
            productionFrameWeightMngService.delete(
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

        function update() {
            if ($scope.editForm.$valid) {
                $scope.method.addListDetailHistory();
                productionFrameWeightMngService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.productionFrameWeightID;
                            $scope.data = data.data;
                        }
                       
                    },
                    function (error) {
                    });
            } else {
                jsHelper.showMessage('warning', context.msgValid);
            }
        };

        $timeout(function () {
            $scope.event.get();
        }, 0);
    };
})();