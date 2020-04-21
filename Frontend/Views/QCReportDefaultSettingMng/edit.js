(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive']).controller('tilsoftController', qcReportDefaultSettingController);
    qcReportDefaultSettingController.$inject = ['$scope', 'dataService'];

    function qcReportDefaultSettingController($scope, qcReportDefaultSettingService) {
        // variable
        $scope.data = [];

        // event
        $scope.event = {
            //init: init,
            get: get,
            updated: updated,
            deleted: deleted
        };

        //function init() {
            
        //    jQuery('#content').show();
        //};

        function get() {
            qcReportDefaultSettingService.serviceUrl = context.serviceUrl;
            qcReportDefaultSettingService.token = context.token;
            //qcReportDefaultSettingService.supportServiceUrl = context.supportServiceUrl;

            qcReportDefaultSettingService.load(
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

        function updated() {
            if ($scope.editForm.$valid) {
                qcReportDefaultSettingService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            debugger;
                            window.location = context.refreshUrl + data.data.qcReportDefaultSettingID;
                            $scope.data = data.data;    
                        }
                    },
                    function (error) {

                    });
            } else {
                jsHelper.showMessage('warning', context.msgValid);
            }
        };

        function deleted(id) {
            qcReportDefaultSettingService.delete(
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