//
// SUpport
//
$("#userId").select2();
$("#userGroupId").select2();
$("#moduleId").select2();
$("#officeId").select2();

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ui.select2']);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.users = null;
    $scope.offices = null;
    $scope.userGroups = null;
    $scope.modules = null;

    $scope.selection = {
        userId: null,
        officeId: null,
        userGroupId: null,
        moduleId: null
    }

    $scope.data = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getInitData(
                function (data) {
                    $scope.users = data.data.users;
                    $scope.offices = data.data.offices;
                    $scope.userGroups = data.data.userGroups;
                    $scope.modules = data.data.modules;
                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    $scope.users = null;
                    $scope.offices = null;
                    $scope.userGroups = null;
                    $scope.modules = null;
                    $scope.$apply();
                }
            );
        },
        generateReport: function () {
            jsonService.getReport(
                $scope.selection.moduleId,
                $scope.selection.userId,
                $scope.selection.userGroupId,
                $scope.selection.officeId,
                function (data) {
                    $scope.data = data;
                    $scope.$apply();
                },
                function (error) {
                    $scope.data = null;
                    $scope.$apply();
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        getDetail: function (item) {
            jsonService.getReportDetail(
                item.moduleID,
                $scope.selection.userId,
                $scope.selection.userGroupId,
                $scope.selection.officeId,
                function (data) {
                    item.userPermissions = data;

                    console.log(item);

                    $scope.$apply();
                },
                function (error) {
                    item.userPermissions = null;
                    $scope.$apply();
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        clearFilter: function () {
            $scope.selection = {
                userId: null,
                officeId: null,
                userGroupId: null,
                moduleId: null
            }
            $("#userId").select2('data', null);
            $("#userGroupId").select2('data', null);
            $("#moduleId").select2('data', null);
            $("#officeId").select2('data', null);
            //$scope.event.reload();
        },
    }

    //
    // method
    //


    //
    // init
    //
    $scope.event.init();
}]);