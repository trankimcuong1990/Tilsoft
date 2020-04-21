angular.module('tilsoftApp').controller('requirementController', ['$scope', 'dataService', function ($scope, dataService) {
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
            $('#ribbon .breadcrumb').html('').append('<li><a href="#/">Sample Order</a></li><li><a href="#/product">Product</a></li><li>Client Requirement</li>');
            $('#page-title').html('').append('<a href="#/">Sample Order</a> <span>> <a href="#/product">Product</a></span> <span>> Client Requirement</span>');
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);