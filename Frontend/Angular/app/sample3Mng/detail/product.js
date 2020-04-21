angular.module('tilsoftApp').controller('productController', ['$scope', '$routeParams', 'dataService', function ($scope, $routeParams, dataService) {
    //
    // property
    //
    $scope.data = {};

    //
    // event
    //
    $scope.event = {
        init: function () {
            jQuery('#content').show();
            $('#ribbon .breadcrumb').html('').append('<li><a href="#/">Sample Order</a></li><li>Product</li>');
            $('#page-title').html('').append('<a href="#/">Sample Order</a> <span>> Product</span>');

            $scope.data = dataService.getLocalData(dataService.dataKey);
            console.log($scope.data);
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);