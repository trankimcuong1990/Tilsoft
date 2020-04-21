angular.module('tilsoftApp').service('commentService', ['$rootScope', 'dataService', function ($rootScope, dataService) {
    this.data = null;

    this.openComment = function () {
        this.data = {
            devRequestHistoryID: dataService.getIncrementId(),
            devRequestHistoryActionID: null,
            actionDescription: '',
            comment: '',
            updatedBy: null,
            updatedDate: null,
            actionUpdatorName: '',
            devRequestHistoryActionNM: '',
            devRequestCommentAttachedFiles: []
        }

        jQuery('#frmComment').modal('show');
    };

    this.addCommentFile = function () {
        fileMultipleUploader.callback = function () {
            scope.$apply(function () {
                angular.forEach(fileMultipleUploader.selectedFiles, function (value, key) {
                    scope.commentService.data.devRequestCommentAttachedFiles.push({
                        devRequestCommentAttachedFileID: dataService.getIncrementId(),
                        fileUD: '',
                        fileLocation: value.fileURL,
                        hasChanged: true,
                        newFile: value.filename,
                        friendlyName: value.friendlyName
                    });
                }, null);
            });
        };
        fileMultipleUploader.onhide = function () {
            jQuery('#frmComment').modal('show');
        };
        jQuery('#frmComment').modal('hide');
        fileMultipleUploader.open();
    }

    this.removeCommentFile = function (item) {
        this.data.devRequestCommentAttachedFiles.splice(this.data.devRequestCommentAttachedFiles.indexOf(item), 1);
    };

    this.updateComment = function () {
        dataService.addComment(
            context.id,
            this.data,
            function (data) {
                jsHelper.processMessage(data.message);
                if (data.message.type == 0) {
                    $rootScope.$broadcast('refresh');
                }
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            }
        );
    };
}]);