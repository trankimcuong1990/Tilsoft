(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', warehouseOverviewRptController);
    warehouseOverviewRptController.$inject = ['$scope', 'dataService'];

    function warehouseOverviewRptController($scope, warehouseOverviewRptService) {
        $scope.filters = {
            startDate: null,
            endDate: null,
        };

        $scope.event = {
            initWarehouseOverviewRpt: initWarehouseOverviewRpt,
            exportWarehouseOverviewRpt: exportWarehouseOverviewRpt,
        };

        function initWarehouseOverviewRpt() {
            warehouseOverviewRptService.serviceUrl = context.serviceUrl;
            warehouseOverviewRptService.token = context.token;

            jQuery('#content').show();
        };

        function exportWarehouseOverviewRpt() {
            $scope.filters.startDate = jQuery('#startDate').val();
            $scope.filters.endDate = jQuery('#endDate').val();
            
            if ($scope.filters.startDate === null || $scope.filters.startDate === '') {
                jsHelper.showMessage('warning', context.errMsg);
                return false;
            }

            if ($scope.filters.endDate === null || $scope.filters.endDate === '') {
                jsHelper.showMessage('warning', context.errMsg);
                return false;
            }

            if ($scope.indexForm.$valid) {
                
                warehouseOverviewRptService.exportExcelWarehouseOverview(
                    $scope.filters.startDate,
                    $scope.filters.endDate,
                    function (data) {
                        window.location = context.backendReportUrl + data.data;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            } else {
                jsHelper.showMessage('warning', context.errMsg);
            }
        };

        $scope.event.initWarehouseOverviewRpt();
    };
})();