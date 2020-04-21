//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    //$scope.factories = null;
   
    $scope.seasons = null;
    $scope.clients = null;
    $scope.data = {
        season: null,
        clientID: null
    };

    //
    // event
    //
    $scope.event = {
        init: function () {           
            dataService.getInit(
                function (data) {               
                    $scope.seasons = data.data.seasons;                  
                    $scope.clients = data.data.clients;
              
                    jQuery('#content').show();
                },
                function (error) {                    
                    $scope.clients = null;
                    $scope.seasons = null;                  
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        goNext: function () {
            if (!$scope.data.clientID || $scope.data.clientID == '' || typeof $scope.data.clientID == 'undefined') {
                alert('Client is invalid !');
                return;
            }
            if (!$scope.data.season || $scope.data.season == '' || typeof $scope.data.season == 'undefined') {
                alert('Season is invalid !');
                return;
            }           

            var queryString = '';
            queryString += 'clientID=' + $scope.data.clientID;
            queryString += '&season=' + $scope.data.season;

            window.location = context.nextURL + queryString;
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