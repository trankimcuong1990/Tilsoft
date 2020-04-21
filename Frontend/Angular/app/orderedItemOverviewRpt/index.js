//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.totalPage = 0;
    $scope.pageIndex = 1;
    $scope.selection = {
        season: null
    };

    $scope.event = {
        init: function () {
            jsonService.getInitData(
                function (data) {
                    $scope.seasons = data.data.seasons;
                    $scope.selection.season = null;
                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.selection.season = null;
                    $scope.$apply();
                }
            );
        },
        generateExcel: function () {
            if ($scope.reportForm.$valid) {
                jsonService.getExcelReport(
                    $scope.selection.season,
                    function (data) {
                        window.location = context.backendReportUrl + data.data;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        search: function search () {
            $scope.data = [];
            jsonService.getDataWithFilters(
                {
                    filters: {
                        season: $scope.selection.season,
                        pageSize: 20,
                        pageIndex: 1
                    }
                },
                function (data) {
                    $scope.data = data.data;
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });         
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);