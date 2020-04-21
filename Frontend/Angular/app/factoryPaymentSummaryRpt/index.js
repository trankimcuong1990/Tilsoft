﻿//
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