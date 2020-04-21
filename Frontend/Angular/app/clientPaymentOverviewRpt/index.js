//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.clients = null;
    $scope.selection = {
        clientID: null
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getInitData(
                function (data) {
                    $scope.clients = data.data.clients;
                    $scope.selection.clientID = null;
                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    $scope.clients = null;
                    $scope.selection.clientID = null;
                    $scope.$apply();
                }
            );
        },
        generateExcel: function () {
            if ($scope.reportForm.$valid) {
                jsonService.getExcelReport(
                    $scope.selection.clientID,
                    function (data) {
                        window.location = context.backendReportUrl + data.data + ".xlsm";
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
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