//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {

    //get service info
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //property
    $scope.filter = {
        //season: context.currentSeason,
        season: jsHelper.getCurrentSeason()
    }

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getInit(
                function (data) {
                    $scope.seasons = data.data;
                    jQuery('#content').show();
                },
                function (error) {
                    //do nothing
                }
            );
        },     
        getReportData: function (season) {
            dataService.getReportData(season,
                function (data) {
                    jsHelper.processMessage(data.message);
                    window.open(context.backendReportUrl + data.data);
                    jQuery('#content').show();
                },
                function (error) {
                    //do nothing
                }
            );
        },        
    }

    //
    // init
    //
    $scope.event.init();
}]);