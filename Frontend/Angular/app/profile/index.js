//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngSanitize']).config([
    '$compileProvider',
    function ($compileProvider) {
        $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|skype):/);
    }
]);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', 'profileService', 'passwordService', function ($scope, dataService, profileService, passwordService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;
    $scope.locations = null;

    //
    // exposing service
    //
    $scope.dataService = dataService;

    $scope.profileService = profileService;
    $scope.profileService.parentScope = $scope;

    $scope.passwordService = passwordService;
    $scope.passwordService.parentScope = $scope;

    //
    // event
    //
    $scope.event = {
        init:function(){
            dataService.load(
                0,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    if ($scope.data.employeeNM) {
                        jQuery('#page-title').html('User Profile: ' + $scope.data.employeeNM);
                    }
                    $scope.locations = data.data.locations;
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function() {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.locations = null;
                }
            );
        },
        generateAPI: function () {
            var execute = 1;
            if ($scope.data && $scope.data.apiKey) {
                if (!confirm('If you get new key your old key will be invalid, are you sure you want to get the new api key ?')) {
                    execute = 0;
                }
            }

            if (execute) {
                dataService.getAPIKey(
                    function (data) {
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function(id){
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        }
    };

    //
    // trigger
    //
    $scope.$on('refresh', function (event, data) {
        $scope.method.refresh(context.id);
    })

    //
    // init
    //
    $scope.event.init();
}]);