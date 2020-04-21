(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive']).controller('tilsoftController', masterExchangeRateMngController);
    masterExchangeRateMngController.$inject = ['$scope', 'dataService'];

    function masterExchangeRateMngController($scope, dataService) {
        $scope.data = [];
        $scope.newID = -1;
        $scope.currencyArr = [{ name: "USD", currency: "USD" }, { name: "EUR", currency: "EUR" }, { name: "CNY", currency: "CNY" }];
        $scope.event = {
            get: get,
            remove: remove,
            update: update
        };

        $scope.method = {
            refresh: refresh,
            getNewID: getNewID
        };


        //Function Start

        function get() {
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
            dataService.supportServiceUrl = context.supportServiceUrl;

            dataService.load(
                context.id,
               null,
                function (data) {
                    $scope.data = data.data.data;
                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        }

        function remove(id) {
            dataService.delete(
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
        }

        function update($event) {
            dataService.update(
                context.id,
                $scope.data,
                function (data) {
                    jsHelper.processMessage(data.message);
                    window.location = context.refreshUrl + data.data.data.masterExchangeRateID;
                    $scope.data = data.data;
                },
                function (error) {
                    //do not need do anything
                });
        };


        //Start Method
        function refresh() {
            window.location = context.refreshUrl + id;
        }

        function getNewID() {
            $scope.newID--;
            return $scope.newID;
        }

        $scope.event.get();

        function createservices() {
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
            dataService.supportServiceUrl = context.supportServiceUrl;

        };
    }
})();