(function () {
    'use strict';
    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', opSequenceController);
    opSequenceController.$inject = ['$scope', 'dataService'];

    function opSequenceController($scope, opSequenceService) {
        $scope.data = [];
        $scope.workCenters = [];
        $scope.detailID = -1;

        $scope.event = {
            loaded: loaded,
            added: added,
            removed: removed,
            updated: updated,
            deleted: deleted
        };

        $scope.event.loaded();

        function loaded() {
            inited();
            opSequenceService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.workCenters = data.data.workCenters;
                    jQuery('#content').show();
                },
                function (error) {
                    //do not need do anything
                }
            );
        };

        function added($event) {
            $event.preventDefault();

            if ($scope.data.opSequenceDetails === null) {
                $scope.data.opSequenceDetails = [];
            }

            $scope.data.opSequenceDetails.push({
                opSequenceDetailID: $scope.detailID--,
                opSequenceDetailNM: '',
                sequenceIndexNumber: 0
            });
        };

        function removed($event, index) {
            $event.preventDefault();

            $scope.data.opSequenceDetails.splice(index, 1);
        };

        function updated($event) {
            $event.preventDefault();
            if ($scope.editForm.$valid) {
                opSequenceService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        window.location = context.refreshUrl + data.data.opSequenceID;
                        $scope.data = data.data;
                    },
                    function (error) {
                        //do not need do anything
                    });
            } else {
                jsHelper.showMessage('warning', 'Input data is valid.');
            }
        };

        function deleted(id) {
            opSequenceService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    window.location = context.backUrl;
                },
                function (error) {
                    //do not need do anything
                }
            );
        };

        function inited() {
            opSequenceService.serviceUrl = context.serviceUrl;
            opSequenceService.token = context.token;
        };
    };
})();