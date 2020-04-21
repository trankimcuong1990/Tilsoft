//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.seasons = null;
    $scope.selection = {
        from: null,
        to: null
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
                    //$scope.selection.season = jsHelper.getCurrentSeason();
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
            if (!$scope.selection.from) {
                alert('Please select date!');
                return false;
            }
            if (!$scope.selection.to) {
                alert('Please select date!');
                return false;
            }
            jsonService.getExcelReport(
                $scope.selection.from,
                $scope.selection.to,
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


    //
    // init
    //
    $scope.event.init();
}]);