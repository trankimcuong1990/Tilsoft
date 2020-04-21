//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.seasons = null;
    $scope.selection = {
        season: null
    };

    $scope.data = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getInitData(
                function (data) {
                    $scope.seasons = data.data.seasons;
                    $scope.selection.season = jsHelper.getCurrentSeason();
                    $scope.$apply();


                    jQuery('#content').show();
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.$apply();
                }
            );
        },
        generateExcel: function () {
            jsonService.getExcelReport(
                $scope.selection.season,
                function (data) {
                    window.location = context.backendReportUrl + data.data + '.xlsm';
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        generateHtml: function () {
            jsonService.getHtmlReport(
                $scope.selection.season,
                function (data) {
                    $scope.data = data.data;
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
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