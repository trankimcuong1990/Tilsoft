
var tilsoftApp = angular.module('tilsoftApp', ['ui.bootstrap', 'furnindo-directive']);


tilsoftApp.controller('tilsoftController', ['$scope', '$timeout',  function ($scope, $timeout) {
    //
    // property
    //
    $scope.data = null;
    $scope.newID = -1;

    $scope.deliveryNoteProducts = [];
    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.getDataWarehouse2TeamOverView(
                context.id,
                function (data) {
                    $scope.data = data.data.data;

                    //incase add new: initialize  param
                    if (context.id === 0) {
                        $scope.data.viewName = context.viewName;
                    }

                    //get support list
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
            refresh: function (id) {
                window.location = context.refreshUrl + id;
            },

            getNewID: function () {
                $scope.newID--;
                return $scope.newID;
            }
        
        };
    //
    //
    // init
    //
    $scope.event.load();
}]);