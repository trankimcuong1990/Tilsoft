(function () {
    "use strict";

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', complianceController);
    complianceController.$inject = ['$scope', 'dataService'];

    function complianceController($scope, complianceService) {
        $scope.data = [];


        //for combo box
        $scope.complianceType = [
            { id: 1, name: '01' },
            { id: 2, name: '02' }
        ];

        //end 

        $scope.event =
            {
                activepage: activepage,
                updatedata: updatedata,
                deletedata: deletedata,
                // getInit: getInit
            };
        //$scope.event.getInit();
        $scope.event.activepage();


        //function getInit() {
        //    createservices();
        //    unitService.getInit(
        //        function (data) {

        //            //$scope.checkListGroups = data.data.checkListGroups;
        //            //$scope.typeOfInspectionDTO = data.data.typeOfInspectionDTO;
        //            jQuery("#content").show();

        //        },
        //        function (error) {
        //            //alert("444")
        //            //jsHelper.showMessage("warning", error);
        //            $scope.checkListGroups = [];
        //            $scope.typeOfInspectionDTO = [];
        //        });
        //};

        function activepage() {
            createservices();

            complianceService.load(
                context.id,
                null,
                function (data) {

                    $scope.data = data.data.complianceCertificateTypeDTO;
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
                complianceService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.complianceCertificateTypeDTO.complianceCertificateTypeID;
                            $scope.data = data.data;
                        }
                    },
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (context.id === 0) {
                            //refresh page
                            var id = data.data.complianceCertificateTypeID;
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
            complianceService.delete(
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
            complianceService.serviceUrl = context.serviceUrl;
            complianceService.token = context.token;
        };
    };
})();
