angular.module("tilsoftApp", [])
    .controller("tilsoftController", ["$scope", "dataService", function ($scope, dataService) {

        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;

        $scope.data = null;

        $scope.newId = -1;

        $scope.event = {
            init: function () {
                dataService.load(
                    context.id,
                    null,
                    function (data) {
                        $scope.data = data.data.data;
                        $scope.factorySteps = data.data.factorySteps;
                        jQuery("#content").show();
                        jQuery('#componentNM').focus();
                        $scope.$watch("data", function () {
                            jQuery("#changeNotification").html('<i class="fa fa-save blink_me"></i>');
                        });
                    },
                    function (error) {
                        jsHelper.showMessage("warning", error);
                        $scope.data = null;
                    });
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
                                $scope.method.refesh(data.data.factoryGoodsProcedureID);
                        },
                        function (error) {
                            jsHelper.showMessage("warning", error.data.message.detailMessage[1]);
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
                        jsHelper.showMessage('warning', error);
                    });
            },
            addStep: function ($event) {
                $event.preventDefault();
                if ($scope.data.factoryGoodsProcedureDetailDTOs == null)
                    $scope.data.factoryGoodsProcedureDetailDTOs = [];
                $scope.data.factoryGoodsProcedureDetailDTOs.push({
                    factoryGoodsProcedureDetailID: $scope.method.getNewId(),
                });
            },
            deleteStep: function ($event, detailStepId) {
                $event.preventDefault();
                if (detailStepId > 0) {
                    dataService.deleteDetail(
                        detailStepId,
                        function (data) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.factoryGoodsProcedureDetailDTOs.length; i++) {
                                if ($scope.data.factoryGoodsProcedureDetailDTOs[i].factoryGoodsProcedureDetailID == detailStepId) {
                                    j = i;
                                    break;
                                }
                            }
                            if (j >= 0)
                                $scope.data.factoryGoodsProcedureDetailDTOs.splice(j, 1);
                        },
                        function (error) {
                        });
                } else {
                    var j = detailStepId;
                    $scope.data.factoryGoodsProcedureDetailDTOs.splice(j, 1);
                }
            }
        }

        $scope.method = {
            getNewId: function () {
                $scope.newId--;
                return $scope.newId;
            },
            refesh: function (id) {
                jsHelper.loadingSwitch(true);
                window.location = context.refeshUrl + id;
            }
        };

        $scope.event.init();

    }]);