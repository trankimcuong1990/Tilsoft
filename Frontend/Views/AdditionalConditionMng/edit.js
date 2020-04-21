(function () {
    "use strict";

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', additionalConditionController);
    additionalConditionController.$inject = ['$scope', 'dataService'];

    function additionalConditionController($scope, additionService) {
        $scope.data = [];
        $scope.additionalConditionTypeDTO = [];

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
                deletedata: deletedata,
                getInit: getInit
            };
        $scope.event.getInit();
        $scope.event.activepage();


        function getInit() {
            createservices();
            debugger;
            additionService.getInit(
                function (data) {
                    $scope.additionalConditionTypeDTO = data.data.additionalConditionTypeDTO;
                    jQuery("#content").show();
                },
                function (error) {
                    //alert("444")
                    //jsHelper.showMessage("warning", error);                    
                    $scope.additionalConditionTypeDTO = [];
                });
        };
        function activepage() {
            createservices();

            additionService.load(
                context.id,
                null,
                function (data) {

                    $scope.data = data.data.additionalConditionDTO;
                    jQuery("#content").show();

                },
                function (error) {
                    //alert("444")
                    //jsHelper.showMessage("warning", error);
                    $scope.data = [];
                });


        };

        function updatedata($event) {
            $event.preventDefault();
            if ($scope.editForm.$valid) {
                additionService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.additionalConditionDTO.additionalConditionID;
                            $scope.data = data.data;
                        }
                    },
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (context.id === 0) {
                            //refresh page
                            var id = data.data.additionalConditionID;
                            window.location = context.refreshUrl + id + '#/';
                        }
                        else {
                            //refresh data
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
            additionService.delete(
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
            additionService.serviceUrl = context.serviceUrl;
            additionService.token = context.token;
        };
    };
})();
