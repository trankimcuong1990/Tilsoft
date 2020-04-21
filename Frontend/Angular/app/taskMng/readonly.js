var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getView(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        }
    };

    //
    // method
    //
    $scope.method = {
    };

    //
    // init
    //    
    $scope.event.init();
}]);