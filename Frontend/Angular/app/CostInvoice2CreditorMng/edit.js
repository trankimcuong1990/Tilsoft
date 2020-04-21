(function () {
    "use strict";

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', CostInvoice2CreditorMngController);
    CostInvoice2CreditorMngController.$inject = ['$scope', 'dataService'];

    function CostInvoice2CreditorMngController($scope, costInvoice2CreditorMngService) {
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
           

            costInvoice2CreditorMngService.load(
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
                costInvoice2CreditorMngService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.data.costInvoice2CreditorID;
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
            costInvoice2CreditorMngService.delete(
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
            costInvoice2CreditorMngService.serviceUrl = context.serviceUrl;
            costInvoice2CreditorMngService.token = context.token;
        };
    };
})();

//jQuery("#content").show();