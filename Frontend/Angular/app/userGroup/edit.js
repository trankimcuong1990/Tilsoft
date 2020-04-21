var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.canCreateSelectAll = false;
    $scope.canReadSelectAll = false;
    $scope.canUpdateSelectAll = false;
    $scope.canDeleteSelectAll = false;
    $scope.canPrintSelectAll = false;
    $scope.canApproveSelectAll = false;
    $scope.canResetSelectAll = false;

    $scope.gridHandler = {
        loadMore: function () {},
        sort: function (sortedBy, sortedDirection) {},
        isTriggered: false
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;

                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },
        update: function ($event) {
            $event.preventDefault();

            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.userGroupID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', context.errorMsg);
            }
        },
        delete: function ($event) {
            $event.preventDefault();

            if (confirm('Are you sure ?')) {
                jsonService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },
        canCreateSelectAll: function () {
            angular.forEach($scope.data.permissions, function (value, key) {
                value.canCreate = $scope.canCreateSelectAll;
            }, null);
        },
        canReadSelectAll: function () {
            angular.forEach($scope.data.permissions, function (value, key) {
                value.canRead = $scope.canReadSelectAll;
            }, null);
        },
        canUpdateSelectAll: function () {
            angular.forEach($scope.data.permissions, function (value, key) {
                value.canUpdate = $scope.canUpdateSelectAll;
            }, null);
        },
        canDeleteSelectAll: function () {
            angular.forEach($scope.data.permissions, function (value, key) {
                value.canDelete = $scope.canDeleteSelectAll;
            }, null);
        },
        canPrintSelectAll: function () {
            angular.forEach($scope.data.permissions, function (value, key) {
                value.canPrint = $scope.canPrintSelectAll;
            }, null);
        },
        canApproveSelectAll: function () {
            angular.forEach($scope.data.permissions, function (value, key) {
                value.canApprove = $scope.canApproveSelectAll;
            }, null);
        },
        canResetSelectAll: function () {
            angular.forEach($scope.data.permissions, function (value, key) {
                value.canReset = $scope.canResetSelectAll;
            }, null);
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);