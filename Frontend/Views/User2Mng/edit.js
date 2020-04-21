//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ui.select2', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = {
        username: '',
        email: '',
        userGroupID: null,
        password: '',
        confirmation: ''
    };

    $scope.employees = null;
    $scope.userGroups = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getInit(
                function (data) {
                    $scope.employees = data.data.employees;
                    $scope.userGroups = data.data.userGroups;

                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.employees = null;
                    $scope.userGroups = null;
                }
            );
        },
        create: function () {
            if ($scope.data.password !== $scope.data.confirmation) {
                alert('Password and confirmation do not match!');
                return;
            }

            if($scope.editForm.$valid)
            {
                dataService.initAccount(
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if(data.message.type === 0) {
                            $scope.method.refresh(data.data);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            }
            else
            {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function(id){
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl+id;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);