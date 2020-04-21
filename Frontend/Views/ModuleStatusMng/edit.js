(function () {
    'use strict';

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', moduleStatusMngController);
    moduleStatusMngController.$inject = ['$scope', 'dataService', '$timeout'];

    function moduleStatusMngController($scope, moduleStatusMngService, $timeout) {
        $scope.data = null;
        $scope.modules = [];
        $scope.newID = -1;

        //
        //implement function for event
        //
        $scope.event = {
            init: function () {
                moduleStatusMngService.serviceUrl = context.serviceUrl;
                moduleStatusMngService.supportServiceUrl = context.supportServiceUrl;
                moduleStatusMngService.token = context.token;
                moduleStatusMngService.getInit(
                    function (data) {
                        $scope.modules = data.data.modules;
                        $scope.event.load();
                    },
                    function (error) {
                        // do nothing
                    });

            },
            load: function () {
                moduleStatusMngService.load(
                    context.id,
                    null,
                    function (data) {
                        $scope.data = data.data.data;
                        //Select 2 Zone
                        jQuery('#moduleID').select2();
                        jQuery('#content').show();
                        $timeout(function () { jQuery('#moduleID').trigger('change'); });
                    },
                    function (error) {
                        // do nothing
                    });
            },
            update: function () {
                moduleStatusMngService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.moduleStatusID;
                            $scope.data = data.data;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', data.message);
                    });
            },
            del: function (id) {
                moduleStatusMngService.delete(
                    id,
                    null,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.backUrl;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            },
        };

        //
        //implement function for method
        //
        $scope.method = {
            getNewID: function () {
                $scope.newID--;
                return $scope.newID;
            },

        };
        //
        //init form
        //
        $scope.event.init();
    };
})();