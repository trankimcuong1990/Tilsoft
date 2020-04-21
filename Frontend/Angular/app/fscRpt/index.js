(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', fscRptController);
    fscRptController.$inject = ['$scope', 'dataService'];

    function fscRptController($scope, fscRptService) {
        $scope.seasons = null;

        // filter.
        $scope.filters = {
            season: null,
            startDate: null,
            endDate: null,
        };

        // event.
        $scope.event = {
            init: init,
            exportFSC: exportFSC,
        };

        // method.
        $scope.method = {
            getFirstDate: getFirstDate,
            getLastDate: getLastDate,
        };

        // define init.
        function init() {
            // set info service.
            fscRptService.serviceUrl = context.serviceUrl;
            fscRptService.token = context.token;

            fscRptService.getInit(
                function (data) {
                    // set data season.
                    $scope.filters.startDate = $scope.method.getFirstDate();
                    $scope.filters.endDate = $scope.method.getLastDate();

                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    jQuery('#content').show();
                });
        };

        // define export.
        function exportFSC() {
            if (jQuery('#startDate').val() === null || jQuery('#startDate').val() === '') {
                jsHelper.showMessage('warning', 'Please input Start Date.');
                return false;
            }

            if (jQuery('#endDate').val() === null || jQuery('#endDate').val() === '') {
                jsHelper.showMessage('warning', 'Please input End Date.');
                return false;
            }

            $scope.filters.startDate = jQuery('#startDate').val();
            $scope.filters.endDate = jQuery('#endDate').val();

            fscRptService.exportFCSReport(
                $scope.filters.startDate,
                $scope.filters.endDate,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        // define get first date.
        function getFirstDate() {
            var date = new Date();
            return moment(new Date(date.getFullYear(), date.getMonth(), 1)).format('DD/MM/YYYY');
        };

        // define get last date.
        function getLastDate() {
            var date = new Date();
            return moment(new Date(date.getFullYear(), date.getMonth() + 1, 0)).format('DD/MM/YYYY');
        };

        // default.
        $scope.event.init();
    };
})();
