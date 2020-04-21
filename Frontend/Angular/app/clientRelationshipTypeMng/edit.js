(function () {
    "use strict";

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', ClientRelationshipTypeMngController);
    ClientRelationshipTypeMngController.$inject = ['$scope', 'dataService'];

    function ClientRelationshipTypeMngController($scope, clientRelationshipTypeMngService) {
        $scope.data = [];
        //$scope.workCenters = [];
        //$scope.employees = [];

        $scope.event =
            {
                activepage: activepage,
                updatedata: updatedata,
                deletedata: deletedata
            };

        $scope.event.activepage();

        function activepage() {
            createservices();

            $scope.data = {
                capacity: null,
                defaultCost: null,
                operatingTime: null
            };

            clientRelationshipTypeMngService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data; //console.log($scope.data);

                    jQuery("#content").show();
                },
                function (error) {
                    jsHelper.showMessage("warning", error);
                });
        };

        function updatedata($event) {
            $event.preventDefault();
            if ($scope.editForm.$valid) {
                clientRelationshipTypeMngService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.data.clientRelationshipTypeID;
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
            clientRelationshipTypeMngService.delete(
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
            clientRelationshipTypeMngService.serviceUrl = context.serviceUrl;
            clientRelationshipTypeMngService.token = context.token;
        };
    };
})();

//jQuery("#content").show();