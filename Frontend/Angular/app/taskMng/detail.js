//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;
    $scope.taskStatuses = null;
    $scope.taskStepComments = null;
    $scope.newid = 0;
    $scope.currentComment = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getDetail(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.taskStatuses = data.data.taskStatuses;
                    $scope.taskStepComments = data.data.taskStepComments;
                    $scope.currentComment = {
                        taskStepCommentID: $scope.method.getNewID(),
                        taskStepID: context.id,
                        comment: '',
                        taskStepCommentFiles: []
                    }

                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.taskStatuses = null;
                    $scope.taskStepComments = null;
                    $scope.$apply();
                }
            );
        },

        // status
        updateStatus: function () {
            jsonService.updateStepStatus(
                context.id,
                $scope.data.stepStatusID,
                function (data) {
                    jsHelper.processMessageEx(data.message);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        // comment
        updateComment: function () {
            console.log($scope.currentComment);
            jsonService.updateComment(
                $scope.currentComment.taskStepCommentID,
                $scope.currentComment,
                function (data) {
                    jsHelper.processMessageEx(data.message);
                    if (data.message.type == 0) {
                        $scope.method.refresh(context.id);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', context.error);
                }
            );
        },
        uploadFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.currentComment.taskStepCommentFiles.push({
                        taskStepCommentFileID: $scope.method.getNewID(),
                        fileUD: '',
                        fileLocation: fileUploader2.selectedFileUrl,
                        hasChanged: true,
                        newFile: fileUploader2.selectedFileName,
                        friendlyName: fileUploader2.selectedFriendlyName
                    });
                });
            };
            fileUploader2.open();
        },
        removeUploadFile: function (item) {
            $scope.currentComment.taskStepCommentFiles.splice($scope.currentComment.taskStepCommentFiles.indexOf(item), 1);
        },
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
        },
        canUpdate: function (id) {
            if ($scope.data) {
                index = jsHelper.getArrayIndex($scope.data.taskStepUsers, 'userID', id);
                if (index >= 0) {
                    if ($scope.data.taskStepUsers[index].taskUserRoleID == 1 || $scope.data.taskStepUsers[index].taskUserRoleID == 3) { // executor or admin
                        return true;
                    }
                }
            }
            
            return false;
        }
    };

    //
    // init
    //    
    $scope.event.init();
}]);