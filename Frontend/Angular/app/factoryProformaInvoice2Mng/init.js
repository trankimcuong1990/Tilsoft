function formatClient(data) {
    return $.map(data, function (item) {
        return {
            description: '',
            id: item.id,
            label: item.label,
            value: item.value
        };
    });
}
//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = {
        season: jsHelper.getCurrentSeason(),
        factoryID: null,
        clientID: null,

        client: null
    };
    $scope.factories = null;
    $scope.seasons = null;
    $scope.autocompleteClient = {
        client: {
            id: null,
            label: null,
            description: null
        },
        api: {
            url: jsonService.serviceUrl + 'quicksearchclient',
            token: jsonService.token
        },
        param: null,
    };
    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getInitData(
                function (data) {
                    $scope.factories = data.data.factories;
                    $scope.seasons = data.data.seasons;
                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    $scope.factories = null;
                    $scope.seasons = null;
                    $scope.$apply();
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        goNext: function () {
            if (!$scope.data.clientID || $scope.data.clientID == '' || typeof $scope.data.clientID == 'undefined') {
                alert('Client is invalid !');
                return;
            }
            if (!$scope.data.factoryID || $scope.data.factoryID == '' || typeof $scope.data.factoryID == 'undefined') {
                alert('Factory is invalid !');
                return;
            }
            if (!$scope.data.season || $scope.data.season == '' || typeof $scope.data.season == 'undefined') {
                alert('Season is invalid !');
                return;
            }

            window.location = context.nextURL + 'c=' + $scope.data.clientID + '&f=' + $scope.data.factoryID + '&s=' + $scope.data.season;
        },
        goBack: function () {
            window.location = context.backURL;
        },
        selectClient: function (client) {
            if (client !== null) {
                $scope.data.clientID = client.id;
                jQuery('#supportClientID').blur();
                $scope.$apply();
            }
        },
        changeClient: function () {
            if ($scope.data.clientID !== null) {
                $scope.data.clientID = null;
            }

            if ($scope.data.clientUD === '') {
                $scope.data.clientUD = null;
            }
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);