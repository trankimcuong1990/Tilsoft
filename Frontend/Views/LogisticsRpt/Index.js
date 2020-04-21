
// APP START 
// GO

var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', function ($scope, $timeout) {
   
    // property
    $scope.seasons = jsHelper.getSeasons();
    $scope.selection = {
        season: jsHelper.getCurrentSeason()
    };

    $scope.data = null;
    //
    // event
    //
    $scope.event = {
        generateExcel: function () {
            jsonService.getExcelReport(
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

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        }
    };


    //
    // init
    //
    $timeout(jQuery('#content').show());
}]);