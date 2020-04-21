//
// APP START
//

var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ui.select2', 'ng-currency']);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', '$timeout', function ($scope, dataService, $timeout) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    console.log(data);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                }
            );
        }
    };
}]);