//
// SUPPORT
//
jQuery('.search-filter').keypress(function(e){
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.seasons = null;
    $scope.selection = {
        season: null
    };

    //
    // event
    //
    $scope.event = {
        init : function(){
            jsonService.getInitData(
                function (data) {
                    $scope.seasons = data.data.seasons;
                    $scope.selection.season = jsHelper.getCurrentSeason();
                    $scope.$apply();
                    

                    jQuery('#content').show();
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.$apply();
                }
            );
        },
        ok: function () {
            jsonService.getReport(
                $scope.selection.season,
                function (data) {
                    if (data.message.type == 0) {
                        window.location = context.backendReportUrl + data.data;
                    }
                    else {
                        jsHelper.showMessage('warning', error);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    }

    //
    // method
    //


    //
    // init
    //
    $scope.event.init();
}]);