//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.suppliers = null;
    $scope.seasons = null;
    $scope.selection = {
        supplierId: null,
        season: null
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getInitData(
                function (data) {
                    $scope.suppliers = data.data.suppliers;
                    $scope.seasons = data.data.seasons;
                    $scope.selection.clientID = null;
                    $scope.selection.season = null;
                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    $scope.suppliers = null;
                    $scope.seasons = null;
                    $scope.selection.clientID = null;
                    $scope.selection.season = null;
                    $scope.$apply();
                }
            );
        },
        generateExcel: function () {
            if ($scope.reportForm.$valid) {
                jsonService.getExcelReport(
                    $scope.selection.supplierId,
                    $scope.selection.season,
                    function (data) {
                        window.location = context.backendReportUrl + data.data;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        }
    }

    //
    // method
    //


    //
    // init
    //
    $scope.event.init();
}]);