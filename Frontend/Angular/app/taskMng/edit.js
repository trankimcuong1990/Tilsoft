//
// SUPPORT
//
//jQuery('#main-form').keypress(function (e) {
//    if (e.keyCode == 13) {
//        e.preventDefault();
//        return false;
//    }
//});

//
// APP START
//
jQuery('#grdProduct').scrollableTable2();
jQuery('#grdSparepart').scrollableTable2();

var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;
    $scope.taskStatuses = null;
    $scope.users = null;
    $scope.taskRoles = null;
    $scope.newid = 0;

    $scope.currentTaskUser = null;
    $scope.currentTaskFile = null;
    $scope.currentTaskStep = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.taskStatuses = data.data.taskStatuses;
                    $scope.users = data.data.users;
                    $scope.taskRoles = data.data.taskRoles;
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
                    $scope.taskStatuses = null;
                    $scope.users = null;
                    $scope.taskRoles = null;
                    $scope.$apply();
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.taskID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', context.error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', errMsg);
            }
        },
        //delete: function () {
        //    if (confirm('Are you sure ?')) {
        //        jsonService.delete(context.id,
        //            function (data) {
        //                jsHelper.processMessage(data.message);
        //                window.location = context.backUrl;
        //            },
        //            function (error) {
        //                jsHelper.showMessage('warning', error);
        //            }
        //        );
        //    }
        //},

        // file
        addFile: function () {
            // set current file
            $scope.currentTaskFile = {
                taskFileID: $scope.method.getNewID(),
                fileUD: '',
                description: '',
                fileLocation: '',
                friendlyName: '',
                hasChanged: false,
                newFile: ''
            };
            $scope.event.editFile($scope.currentTaskFile);
        },
        editFile: function (item) {
            $scope.currentTaskFile = JSON.parse(JSON.stringify(item));
            jQuery("#frmFile").modal();
        },
        updateFile: function () {
            index = jsHelper.getArrayIndex($scope.data.taskFiles, 'taskFileID', $scope.currentTaskFile.taskFileID);
            if (index >= 0) {
                $scope.data.taskFiles[index] = JSON.parse(JSON.stringify($scope.currentTaskFile));
            }
            else {
                $scope.data.taskFiles.push(JSON.parse(JSON.stringify($scope.currentTaskFile)));
            }

            $('#frmFile').modal('hide');
        },
        removeFile: function (item) {
            $scope.data.taskFiles.splice($scope.data.taskFiles.indexOf(item), 1);
        },
        uploadFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.currentTaskFile.fileLocation = fileUploader2.selectedFileUrl;
                    scope.currentTaskFile.hasChanged = true;
                    scope.currentTaskFile.newFile = fileUploader2.selectedFileName;
                    scope.currentTaskFile.friendlyName = fileUploader2.selectedFriendlyName;
                });
            };
            fileUploader2.open();
        },
        removeUploadFile: function () {
            $scope.currentTaskFile.fileLocation = '';
            $scope.currentTaskFile.hasChanged = true;
            $scope.currentTaskFile.newFile = '';
            $scope.currentTaskFile.friendlyName = '';
        },

        // step
        addStep: function () {
            // set current step
            $scope.currentTaskStep = {
                taskStepID: $scope.method.getNewID(),
                stepIndex: '',
                description: '',
                stepStatusID: null,
                taskStatusNM: '',
                taskStepUsers: []
            };
            $scope.event.editStep($scope.currentTaskStep);
        },
        editStep: function (item) {
            $scope.currentTaskStep = JSON.parse(JSON.stringify(item));
            jQuery("#frmStep").modal();
        },
        updateStep: function () {
            var tmpIndex = -1;
            if ($scope.currentTaskStep.taskStepID) {
                // get status name
                tmpIndex = jsHelper.getArrayIndex($scope.taskStatuses, 'taskStatusID', $scope.currentTaskStep.stepStatusID);
                if (tmpIndex >= 0) {
                    $scope.currentTaskStep.taskStatusNM = $scope.taskStatuses[tmpIndex].taskStatusNM;
                }
                else {
                    $scope.currentTaskStep.taskStatusNM = '';
                }
            }

            index = jsHelper.getArrayIndex($scope.data.taskSteps, 'taskStepID', $scope.currentTaskStep.taskStepID);
            if (index >= 0) {
                $scope.data.taskSteps[index] = JSON.parse(JSON.stringify($scope.currentTaskStep));
            }
            else {
                $scope.data.taskSteps.push(JSON.parse(JSON.stringify($scope.currentTaskStep)));
            }

            $('#frmStep').modal('hide');
        },
        removeStep: function (item) {
            $scope.data.taskSteps.splice($scope.data.taskFiles.indexOf(item), 1);
        },
        addStepUser: function (user) {
            index = jsHelper.getArrayIndex($scope.currentTaskStep.taskStepUsers, 'taskUserID', user.taskUserID);
            if (index < 0) {
                $scope.currentTaskStep.taskStepUsers.push({
                    taskStepUserID: $scope.method.getNewID(),
                    taskUserID: user.taskUserID,
                    fullName: user.fullName,
                    officeNM: user.officeNM,
                    taskRoleNM: user.taskRoleNM
                });
            }
        },
        removeStepUser: function (item) {
            $scope.currentTaskStep.taskStepUsers.splice($scope.currentTaskStep.taskStepUsers.indexOf(item), 1);
        },

        // users
        addUser: function (parent) {
            // set current progress
            $scope.currentTaskUser = {
                taskStepUserID: $scope.method.getNewID(),
                userID: null,
                taskUserRoleID: null,
                description: '',
                fullName: '',
                officeNM: '',
                taskRoleNM: ''
            };
            $scope.event.editUser($scope.currentTaskUser, parent);
        },
        editUser: function (item, parent) {
            $scope.currentTaskStep = parent;
            $scope.currentTaskUser = JSON.parse(JSON.stringify(item));
            jQuery("#frmUser").modal();
        },
        updateUser: function () {
            var tmpIndex = -1;
            if ($scope.currentTaskUser.userID) {
                // get user display info
                tmpIndex = jsHelper.getArrayIndex($scope.users, 'userId', $scope.currentTaskUser.userID);
                if (tmpIndex >= 0) {
                    $scope.currentTaskUser.fullName = $scope.users[tmpIndex].fullName;
                    $scope.currentTaskUser.officeNM = $scope.users[tmpIndex].officeNM;
                }
                else {
                    $scope.currentTaskUser.fullName = '';
                    $scope.currentTaskUser.officeNM = '';
                }

                // get role display info
                tmpIndex = jsHelper.getArrayIndex($scope.taskRoles, 'taskRoleID', $scope.currentTaskUser.taskUserRoleID);
                if (tmpIndex >= 0) {
                    $scope.currentTaskUser.taskRoleNM = $scope.taskRoles[tmpIndex].taskRoleNM;
                }
                else {
                    $scope.currentTaskUser.taskRoleNM = '';
                }
            }

            index = jsHelper.getArrayIndex($scope.currentTaskStep.taskStepUsers, 'taskStepUserID', $scope.currentTaskUser.taskStepUserID);
            if (index >= 0) {
                $scope.currentTaskStep.taskStepUsers[index] = JSON.parse(JSON.stringify($scope.currentTaskUser));
            }
            else {
                $scope.currentTaskStep.taskStepUsers.push(JSON.parse(JSON.stringify($scope.currentTaskUser)));
            }

            $('#frmUser').modal('hide');
        },
        removeUser: function (item, parent) {
            parent.taskStepUsers.splice(parent.taskStepUsers.indexOf(item), 1);
        }
    };

    //
    // method
    //
    $scope.method = {
        getNewID: function(){
            $scope.newid--;
            return $scope.newid;
        },
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