//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.selection = {
        season: null
    };
    $scope.seasons = null;

    //
    // event
    //
    $scope.event = {
        init : function(){
            jsonService.getInitData(
                function (data) {
                    $scope.seasons = data.data.seasons;
                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.$apply();
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        goNext: function () {
            if ($scope.selection.season == null || typeof $scope.selection.season == 'undefined' || $scope.selection.season == '') {
                alert('Season is invalid !');
                return;
            }            

            window.location = context.nextURL + 's=' + $scope.selection.season;
        },
        goBack: function ($event) {
            $event.preventDefault();

            window.location = context.backURL;
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);