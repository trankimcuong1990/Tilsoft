(function () {
    'use strict';
    //
    // SUPPORT
    //
    $('.search-filter').keypress(function (e) {
        if (e.keyCode === 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.reload();
        }
    });

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
    angular.module('tilsoftApp').controller('tilsoftController', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
        dataService.searchFilter.pageSize = context.pageSize;
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;
        dataService.supportServiceUrl = context.supportServiceUrl;

        //
        //properties
        //
        $scope.seasons = [];
        $scope.filters = {
            season: jsHelper.getCurrentSeason()
        };

        //
        // events
        //
        $scope.event = {
            init: function () {
                jQuery('#content').show();
                dataService.getSeason(
                    function (data) {
                        $scope.seasons = data.data;
                        console.log($scope.seasons);
                    }
                    , function () {
                    });                                    
            },            
            getWarehouseSoldItem: function (season) {
                dataService.getWarehouseSoldItem(season
                    , function (data) {
                        console.log(data.data);
                        window.location = context.backendReportUrl + data.data;
                    }
                    , function () {

                    });
            }   
        };

        //
        // method
        //
        $scope.method = {            
        };


        //
        // angular init
        //
        $timeout(function () {
            $scope.event.init();
        }, 0);
    }]);

})();