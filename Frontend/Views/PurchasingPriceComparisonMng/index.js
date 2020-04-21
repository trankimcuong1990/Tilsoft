(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', purchasingPriceComparisonMngController);
    purchasingPriceComparisonMngController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function purchasingPriceComparisonMngController($scope, $cookieStore, purchasingPriceComparisonMngService) {
        $scope.seasons = null;
        $scope.currentSeason = null;
        $scope.filter = {
            seasonText: ''
        };

        $scope.event = {
            init: function () {
                purchasingPriceComparisonMngService.searchFilter.pageSize = context.pageSize;
                purchasingPriceComparisonMngService.serviceUrl = context.serviceUrl;
                purchasingPriceComparisonMngService.token = context.token;

                //get season
                purchasingPriceComparisonMngService.getInit(
                    function (data) {
                        $scope.seasons = data.data.seasons;
                        $scope.currentSeason = data.data.currentSeason;
                        $scope.filter.seasonText = $scope.currentSeason;
                        jQuery('#content').show();
                    },
                    function (error) {
                        //$scope.seasons = null;
                        //$scope.$apply();
                    }
                );             
            },

            generateExcel: function () {
                purchasingPriceComparisonMngService.generateExcel(
                    $scope.filter.seasonText,
                    function (data) {
                        if (data.message.type == 2) {
                            jsHelper.processMessage(data.message);
                            return;
                        }
                        window.location = context.reportUrl + data.data;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },
        };

        $scope.method = {   
        };

        $scope.event.init();
    };
})();
