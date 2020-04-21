//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    $scope.selection = {
        season: ''
    };
    $scope.seasons = jsHelper.getSeasons();

    //
    // event
    //
    $scope.event = {
        init: function () {
            $('#content').show();
        },
        generateExcel: function () {
            if ($scope.selection.season) {
                dataService.getExcelReportData(
                    $scope.selection.season,
                    function (data) {
                        if (data.message.type == 0) {
                            window.location = context.backendReportUrl + data.data;
                        }
                        else {
                            jsHelper.processMessageEx(data.message);
                        }
                    },
                    function (error) {
                        jsHelper.processMessageEx('warning', error.message.message);
                    }
                );
            }
            else {
                jsHelper.processMessageEx({ type: 1, message: 'Please select season to generate report!' });
            }            
        },
    };

    //
    // init
    //
    $scope.event.init();
}]);


