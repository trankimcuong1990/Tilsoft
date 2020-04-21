function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.value,
            description: item.label
        };
    });
}
(function () {
    'use strict';
    var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive', 'ngCookies']);

    tilsoftApp.controller('tilsoftController', revenueCostingRptController);
    revenueCostingRptController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function revenueCostingRptController($scope, $cookieStore, revenueCostingRptService) {
        // variable
        $scope.data = [];
        $scope.subSuppliers = [];
        $scope.subSupplierNM = '';
        $scope.filter = {
            factoryRawMaterialID: null,
            startDate: '',
            endDate: ''
        };

        // event
        $scope.event = {
            init: init,
            generateExcel: generateExcel
        };

        // method

        function init() {
            revenueCostingRptService.serviceUrl = context.serviceUrl;
            revenueCostingRptService.token = context.token;
            revenueCostingRptService.supportServiceUrl = context.supportServiceUrl;
            revenueCostingRptService.getInit(
                function (data) {
                    $scope.subSuppliers = data.data.supplierDTOs;
                    $scope.filter.factoryRawMaterialID = $scope.subSuppliers[0].supplierID;
                    $scope.subSupplierNM = $scope.subSuppliers[0].supplierNM;
                    jQuery('#content').show();
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.$apply();
                }
            );
        };

        function generateExcel() {
            if (jQuery('#startDate').val() === null || jQuery('#startDate').val() === '') {
                jsHelper.showMessage('warning', 'Please input From Date.');
                return false;
            }

            if (jQuery('#endDate').val() === null || jQuery('#endDate').val() === '') {
                jsHelper.showMessage('warning', 'Please input To Date.');
                return false;
            }
            debugger;
            revenueCostingRptService.getexcelreport(
                $scope.filter.factoryRawMaterialID,
                $scope.filter.startDate,
                $scope.filter.endDate,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        // default event
        $scope.event.init();
    }
})();