//
// SUPPORT
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.filters = {
        season: context.season
    };
    $scope.seasons = null;
    //
    // event
    //
    $scope.event = {
        load: function () {
            supportService.getSeasons(
                function (data) {
                    $scope.seasons = data.data;
                    $scope.$apply();
                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        exportExcel: function ($event) {
            $event.preventDefault();
            jsonService.getReport($scope.filters,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type != 2) {
                        window.location = context.reportUrl + data.data + '.xlsm';
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
    }

    $scope.event.load();
}]);