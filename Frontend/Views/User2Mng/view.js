//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngSanitize', 'avs-directives', 'furnindo-directive', 'ui.select2']).config([
    '$compileProvider',
    function ($compileProvider) {
        $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|skype):/);
    }
]);
tilsoftApp.controller('tilsoftController', ['$scope', '$rootScope', 'dataService', 'accountService', 'permissionService', 'passwordService', 'factoryAccessService', function ($scope, $rootScope, dataService, accountService, permissionService, passwordService, factoryAccessService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;
    
    $scope.gridHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
        },
        isTriggered: false
    };

    //
    // property
    //
    $scope.data = null;
    $scope.internalCompanies = null;
    $scope.internalDepartments = null;
    $scope.locations = null;
    $scope.userGroups = null;
    $scope.managers = null;
    $scope.jobLevels = null;
    $scope.factories = null;

    //
    // exposing service
    //
    $scope.dataService = dataService;

    //$scope.profileService = profileService;
    //$scope.profileService.parentScope = $scope;
    $scope.selectedFactories = [];

    $scope.accountService = accountService;
    $scope.accountService.parentScope = $scope;

    $scope.passwordService = passwordService;
    $scope.passwordService.parentScope = $scope;

    $scope.permissionService = permissionService;
    $scope.permissionService.parentScope = $scope;

    $scope.factoryAccessService = factoryAccessService;
    $scope.factoryAccessService.parentScope = $scope;

    //
    // event
    //
    $scope.event = {
        init:function(){
            dataService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    if ($scope.data.employeeNM) {
                        jQuery('#page-title').html('User Profile: ' + $scope.data.employeeNM);
                    }
                    $scope.internalCompanies = data.data.internalCompanies;
                    $scope.internalDepartments = data.data.internalDepartments;
                    $scope.locations = data.data.locations;
                    $scope.userGroups = data.data.userGroups;
                    $scope.managers = data.data.managers;
                    $scope.jobLevels = data.data.jobLevels;
                    $scope.factories = data.data.factories;

                    angular.forEach($scope.data.employeeFactories, function (item) {
                        $scope.selectedFactories.push(item.factoryID + '');
                    }, null);

                    $('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.internalCompanies = null;
                    $scope.internalDepartments = null;
                    $scope.locations = null;
                    $scope.userGroups = null;
                    $scope.managers = null;
                    $scope.jobLevels = null;
                    $scope.factories = null;
                }
            );
        },
        delete: function () {
            dataService.delete(
                $scope.data.userId,
                $scope.data.employeeNM,
                function (data) {
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
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
                    context.id,
                    function (data) {
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + context.id;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },

        // profile
        profile_openForm: function () {
            $rootScope.$emit('profile');
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
    });

    //
    // init
    //
    $scope.event.init();
}]);