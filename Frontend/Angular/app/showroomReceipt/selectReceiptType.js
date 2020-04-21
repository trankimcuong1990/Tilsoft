var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    $scope.showroomReceiptTypes = null;
    $scope.selectedTypeID = 1;

    $scope.event = {
        load: function () {
            supportService.getShowroomReceiptType(
                function (data) {
                    $scope.showroomReceiptTypes = data.data;
                    $scope.$apply();
                    jQuery('#content').show();
                },
                function (error) {
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },

        onNextButtonClick: function () {
            switch ($scope.selectedTypeID) {
                case 1:
                    window.location = context.importEditUrl;
                    break;
                case 2:
                    window.location = context.exportEditUrl;
                    break;
                default:
                    alert('Please select receipt type to continue!');
                    break;
            }
        }
    }

    //load form
    $scope.event.load();
    
}]);