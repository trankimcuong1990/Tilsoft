angular.module('tilsoftApp').controller('profileController', ['$scope', '$rootScope', '$timeout', 'dataService', function ($scope, $rootScope, $timeout, dataService) {
    $scope.profile = {}; 
    $rootScope.$on('profile', function () {
        $scope.profile.data = angular.copy($scope.data);
        if ($scope.profile.data) {
            delete $scope.profile.data.factoryAccesses;
            delete $scope.profile.data.permissions;
            delete $scope.profile.data.employeeFactories;
        }
        jsHelper.showPopup('frmProfile', function () { });
    });

    $scope.profile.event = {
        closeForm: function () {
            jsHelper.hidePopup('frmProfile', function () {
                jsHelper.goToSection('profile-section');
            });
        },
        update: function () {
            if ($scope.frmProfile.$valid) {
                $scope.profile.data.employeeFactories = [];
                angular.forEach($scope.selectedFactories, function (item) {
                    $scope.profile.data.employeeFactories.push({
                        factoryID: parseInt(item)
                    });
                });

                dataService.updateEmployee(
                    $scope.profile.data.employeeID,
                    $scope.profile.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            //$rootScope.$emit('refresh');
                            $scope.profile.event.closeForm();
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
        },
        uploadPhoto: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.profileService.data.thumbnailLocation = fileUploader2.selectedFileUrl;
                    scope.profileService.data.fileLocation = fileUploader2.selectedFileUrl;
                    scope.profileService.data.newFile = fileUploader2.selectedFileName;
                    scope.profileService.data.hasChanged = true;
                });
            };
            fileUploader2.open();
        },
        changeCVFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.profileService.data.cvFileName = fileUploader2.selectedFriendlyName;
                    scope.profileService.data.cvFileLocation = fileUploader2.selectedFileUrl;
                    scope.profileService.data.cvNewFile = fileUploader2.selectedFileName;
                    scope.profileService.data.cvHasChanged = true;
                });
            };
            fileUploader2.open();
        },
        removeCVFile: function () {
            scope.profileService.data.cvFileName = '';
            scope.profileService.data.cvFileLocation = '';
            scope.profileService.data.cvNewFile = '';
            scope.profileService.data.cvHasChanged = true;
        }
    };
}]);