//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {

    //get service info
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getReportData(
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type != 2) {
                        window.open(context.backendReportUrl + data.data);
                    }
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