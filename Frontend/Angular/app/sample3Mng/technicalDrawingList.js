//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
//create controller
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', 'dataService', function ($scope, $cookieStore, dataService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    $scope.data = null;
    $scope.samplePurposes = null;
    $scope.sampleTransportTypes = null;
    $scope.sampleRequestTypes = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getOrderOverviewData(
                context.id,
                function (data) {
                    if (data.message.type === 0) {
                        $scope.data = data.data;

                        $('#content').show();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                }
            );
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);