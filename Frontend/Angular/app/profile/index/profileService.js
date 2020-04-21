angular.module('tilsoftApp').service('profileService', ['$rootScope', 'dataService', function ($rootScope, dataService) {
    this.data = null;
    this.parentScope = null;    

    this.openForm = function () {
        this.data = angular.copy(this.parentScope.data);
        this.data.permissions = null;

        jsHelper.showPopup('frmProfile', function () { });
    };

    this.closeForm = function () {
        jsHelper.hidePopup('frmProfile', function () {
            jsHelper.goToSection('profile-section');
        });
    };

    this.update = function () {
        if (this.parentScope.frmProfile.$valid) {
            dataService.updateEmployee(
                this.data,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {
                        $rootScope.$broadcast('refresh');
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.message.message);
                }
            );
        }
        else {
            jsHelper.showMessage('warning', 'Invalid input data, please check');
        }
    };

    this.uploadPhoto = function () {
        fileUploader2.callback = function () {
            scope.$apply(function () {
                scope.profileService.data.thumbnailLocation = fileUploader2.selectedFileUrl;
                scope.profileService.data.fileLocation = fileUploader2.selectedFileUrl;
                scope.profileService.data.newFile = fileUploader2.selectedFileName;
                scope.profileService.data.hasChanged = true;
            });
        };
        fileUploader2.open();
    }

    //
    // CV file
    //
    this.changeCVFile = function () {
        fileUploader2.callback = function () {
            scope.$apply(function () {
                scope.profileService.data.cvFileName = fileUploader2.selectedFriendlyName;
                scope.profileService.data.cvFileLocation = fileUploader2.selectedFileUrl;
                scope.profileService.data.cvNewFile = fileUploader2.selectedFileName;
                scope.profileService.data.cvHasChanged = true;
            });
        };
        fileUploader2.open();
    };

    this.removeCVFile = function () {
        scope.profileService.data.cvFileName = '';
        scope.profileService.data.cvFileLocation = '';
        scope.profileService.data.cvNewFile = '';
        scope.profileService.data.cvHasChanged = true;
    };

    //
    //signature file
    //
    this.changeSignatureFile = function () {
        //fileUploader2.callback = function () {
        //    scope.$apply(function () {
        //        scope.profileService.data.signatureFileName = fileUploader2.selectedFriendlyName;
        //        scope.profileService.data.signatureFileLocation = fileUploader2.selectedFileUrl;
        //        scope.profileService.data.signatureNewFile = fileUploader2.selectedFileName;
        //        scope.profileService.data.signatureHasChanged = true;
        //    });
        //};
        //fileUploader2.open();

        masterUploader.multiSelect = false;
        masterUploader.imageOnly = true;
        masterUploader.callback = function () {
            scope.$apply(function () {
                var img = masterUploader.selectedFiles[0];
                scope.profileService.data.signatureFileName = img.friendlyName;
                scope.profileService.data.signatureFileLocation = img.fileURL;
                scope.profileService.data.signatureNewFile = img.filename;
                scope.profileService.data.signatureHasChanged = true;
            });
        };
        masterUploader.open();
    };

    this.removeSignatureFile = function () {
        scope.profileService.data.signatureFileName = '';
        scope.profileService.data.signatureFileLocation = '';
        scope.profileService.data.signatureNewFile = '';
        scope.profileService.data.signatureHasChanged = true;
    };

}]);