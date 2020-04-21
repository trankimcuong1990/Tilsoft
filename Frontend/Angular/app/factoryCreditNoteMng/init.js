//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = {
        supplierID: null,
        season: null
    };
    $scope.suppliers = null;
    $scope.seasons = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getInitData(
                function (data) {
                    $scope.suppliers = data.data.suppliers;
                    $scope.seasons = data.data.seasons;
                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    $scope.suppliers = null;
                    $scope.seasons = null;
                    $scope.$apply();
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        goNext: function () {
            if (!$scope.data.supplierID || $scope.data.supplierID == '' || typeof $scope.data.supplierID == 'undefined') {
                alert('Supplier is invalid !');
                return;
            }

            if (!$scope.data.season || $scope.data.season == '' || typeof $scope.data.season == 'undefined') {
                alert('Season is invalid !');
                return;
            }

            window.location = context.nextURL + 's=' + $scope.data.supplierID + '&se=' + $scope.data.season;
        },
        goBack: function () {
            window.location = context.backURL;
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);