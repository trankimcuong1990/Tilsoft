//
// SUPPORT
//
jQuery('#main-form').keypress(function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
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
            jsonService.gets(
                function (data) {
                    $scope.data = data.data;
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
        getTotal: function (order) {
            var result = 0;
            angular.forEach(order.factoryOrderDetails, function (value, key) {
                result += parseInt(value.orderQnt);
            }, null);
            return result;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);