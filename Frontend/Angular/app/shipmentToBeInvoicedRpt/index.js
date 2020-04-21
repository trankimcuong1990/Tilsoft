(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', shipmentToBeInvoicedRptController);
    shipmentToBeInvoicedRptController.$inject = ['$scope', 'dataService'];

    function shipmentToBeInvoicedRptController($scope, shipmentToBeInvoicedRptService) {
        $scope.filters = {
            season: jsHelper.getCurrentSeason(),
        };

        $scope.listSeasons = null;

        $scope.event = {
            init: init,
            report: report,
        };

        function init() {
            shipmentToBeInvoicedRptService.serviceUrl = context.serviceUrl;
            shipmentToBeInvoicedRptService.token = context.token;

            shipmentToBeInvoicedRptService.getInit(
                function (data) {
                    $scope.listSeasons = data.data;

                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    jQuery('#content').show();
                });
        };

        function report() {
            if ($scope.filters.season === null || $scope.filters.season === '') {
                $scope.filters.season = jsHelper.getCurrentSeason();
            }

            shipmentToBeInvoicedRptService.exportExcel(
                $scope.filters.season,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        $scope.event.init();
    };
})();