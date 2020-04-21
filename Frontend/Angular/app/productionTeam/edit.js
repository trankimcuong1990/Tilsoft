(function () {
    "use strict";

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', productionTeamController);
    productionTeamController.$inject = ['$scope', 'dataService'];

    function productionTeamController($scope, productionTeamService) {
        $scope.data = [];
        $scope.workCenters = [];
        $scope.employees = [];

        $scope.event =
        {
            activepage: activepage,
            updatedata: updatedata,
            deletedata: deletedata,
            checkIsOutsource: checkIsOutsource
        };

        $scope.event.activepage();

        function activepage() {
            createservices();

            $scope.data = {
                capacity: null,
                defaultCost: null,
                operatingTime: null
            };

            productionTeamService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data; //console.log($scope.data);
                    $scope.workCenters = data.data.workCenters;
                    $scope.employees = data.data.employees;
                    $scope.subSuppliers = data.data.factoryRawMaterialDatas;
                    
                    jQuery("#content").show();
                },
                function (error) {
                    jsHelper.showMessage("warning", error);
                });
        };

        function updatedata($event) {
            
            $event.preventDefault();
            if ($scope.editForm.$valid) {
                productionTeamService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.data.productionTeamID;
                            $scope.data = data.data;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage("warning", error);
                    });
            } else {
                jsHelper.showMessage("warning", context.msgValid);
            }
        };

        function deletedata(id) {
            productionTeamService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    jsHelper.showMessage("warning", error);
                });
        };

        function createservices() {
            productionTeamService.serviceUrl = context.serviceUrl;
            productionTeamService.token = context.token;
        };

        function checkIsOutsource() {
            if (!$scope.data.isOutsource) {
                $scope.data.subSupplier = null;
            }
        }
    };
})();