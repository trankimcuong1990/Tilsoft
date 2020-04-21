(function () {
    "use strict";

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', UnitController);
    UnitController.$inject = ['$scope', 'dataService'];

    function UnitController($scope, unitService) {
        $scope.data = [];

        //for combo box
        $scope.supportUnitType = [
        { id: 1, name: '01' },
        { id: 2, name: '02' }
        ];

        //end 

        $scope.event =
            {
                activepage: activepage,
                updatedata: updatedata,
                deletedata: deletedata
            };

        $scope.event.activepage();

        function activepage() {
            createservices();

            unitService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;

                    jQuery("#content").show();
                },
                function (error) {
                    jsHelper.showMessage("warning", error);
                });
        };

        function updatedata($event) {
            $event.preventDefault();
            if ($scope.editForm.$valid) {
                unitService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.data.unitID;
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
            unitService.delete(
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
            unitService.serviceUrl = context.serviceUrl;
            unitService.token = context.token;
        };
    };
})();
