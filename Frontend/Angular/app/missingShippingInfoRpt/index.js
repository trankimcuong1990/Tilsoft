var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getReportData(
                function (data) {
                    $scope.data = data.data;
                    $scope.$apply();
                    jQuery('#content').show();
                },
                function (error) {
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },
        generateExcel: function () {
            jsonService.getExcelReport(
                    function (data) {
                        window.location = context.backendReportUrl + data.data + '.xlsm';
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
        }
    }

    //
    // method
    //
    $scope.method = {
    }


    //
    // init
    //
    $scope.event.init();
}]);