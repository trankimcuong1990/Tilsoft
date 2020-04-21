(function () {
    "use strict";

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', CostInvoice2TypeMngController);
    CostInvoice2TypeMngController.$inject = ['$scope', 'dataService'];

    function CostInvoice2TypeMngController($scope, costInvoice2TypeMngService) {
        $scope.data = [];

        $scope.event =
            {
                activepage: activepage,
                updatedata: updatedata,
                deletedata: deletedata
            };

        $scope.event.activepage();

        function activepage() {
            createservices();

            //$scope.data = {
            //    capacity: null,
            //    defaultCost: null,
            //    operatingTime: null
            //};

            costInvoice2TypeMngService.load(
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
                costInvoice2TypeMngService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.data.costInvoice2TypeID;
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
            costInvoice2TypeMngService.delete(
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
            costInvoice2TypeMngService.serviceUrl = context.serviceUrl;
            costInvoice2TypeMngService.token = context.token;
        };
    };
})();

//jQuery("#content").show();