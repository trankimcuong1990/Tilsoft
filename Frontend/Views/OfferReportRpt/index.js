var tilsoftApp = angular.module('tilsoftApp', []);

tilsoftApp.controller('tilsoftController', ['$scope', '$filter', '$interval', function ($scope, $filter, $interval) {
    //
    // property
    //
    $scope.fobItemOnlys = null;
    $scope.seasonOffers = null;
    $scope.selectedSeasonOffer = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            
            $scope.selectedSeasonOffer = jsHelper.getNextSeason();
            jsonService.getUserFOBItemOnly(
                $scope.selectedSeasonOffer,
                function (data) {
                    //$scope.fobItemOnlys = data.data.fOBItemOnlyDTOs;
                    $scope.seasonOffers = data.data.seasons;
                    $scope.$apply();
                },
                function (error) {
                    //$scope.fobItemOnlys = null;
                }
            );
            
        },
        changeSeasonOffer: function (selectedSeasonOffer) {
            jsonService.getUserFOBItemOnly(
                $scope.selectedSeasonOffer,
                function (data) {
                    $scope.fobItemOnlys = data.data.fOBItemOnlyDTOs;
                    //$scope.seasonOffers = data.data.seasons;
                    $scope.$apply();
                },
                function (error) {
                    $scope.fobItemOnlys = null;
                }
            );
        },
        printExcelFOBItemOnly: function (selectedSeasonOffer) {
            jsonService.excelPOBItemOnly(
                selectedSeasonOffer,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type !== 2) {
                        window.location = context.backendReportUrl + data.data;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    };

    //
    // method
    //
    $scope.method = {
        
    };


    //
    // init
    //
    $scope.event.init();
}]);