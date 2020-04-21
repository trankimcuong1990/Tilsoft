var tilsoftApp = angular.module("tilsoftApp", []);
tilsoftApp.controller("tilsoftController", ["$scope", "dataService", function ($scope, dataService) {
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;

        $scope.data = null;

        $scope.event = {
            init: function () {
                dataService.load(
                    context.id,
                    null,
                    function (data) {
                        $scope.data = data.data.data;
                        debugger;
                        jQuery("#content").show();

                        $scope.$watch("data", function () {
                            jQuery("#changeNotification").html('<i class="fa fa-save blink_me"></i>');
                        });
                    },
                    function (error) {
                        jsHelper.showMessage("warning", error);

                        $scope.data = null;
                    })
            },
            update: function ($event) {
                $event.preventDefault();              
                if ($scope.editForm.$valid) {                   
                    dataService.update(
                        context.id,
                        $scope.data,
                        function (data) {
                            jsHelper.processMessage(data.message);
                            if (data.message.type == 0)
                                debugger;
                            $scope.method.refesh(data.data.materialID);
                        },
                        function (error) {
                            jsHelper.showMessage("warning", error.data.exceptionMessage);
                        });
                } else {
                    jsHelper.showMessage("warning", "Invalid input data, please check");
                }
            },
            delete: function ($event) {
                $event.preventDefault();

                dataService.delete(
                    context.id,
                    null,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0)
                            window.location = context.backUrl;
                    },
                    function (error) {
                        jsHelper.showMessage("warning", error);
                    });
            }
        }

        $scope.method = {
            refesh: function (id) {
                jsHelper.loadingSwitch(true);
                window.location = context.refeshUrl + id;
            }
        };

        $scope.event.init();
    }]);