angular.module('tilsoftApp').service('passwordService', ['$rootScope', 'dataService', function ($rootScope, dataService) {
    this.data = null;
    this.parentScope = null;

    this.openForm = function () {
        this.data = {
            userName: this.parentScope.data.userName,
            newPassword: '',
            confirmation: ''
        };
        jsHelper.showPopup('frmPassword', function () { });
    };

    this.closeForm = function () {
        jsHelper.hidePopup('frmPassword', function () {
            jsHelper.goToSection('account-section');
        });
    };

    this.update = function () {
        if (this.parentScope.frmPassword.$valid) {
            if (this.data.newPassword != this.data.confirmation) {
                jsHelper.showMessage('warning', 'Password and confirmation do not match!');
                return;
            }

            dataService.changePassword(
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

    this.forceChangePassword = function () {
        if (confirm('Force user to change the password in the next time he/she login ?')) {
            dataService.forceChangePassword(
                this.parentScope.data.userId,
                function (data) {
                    jsHelper.processMessage(data.message);
                },
                function (error) {
                    jsHelper.showMessage('warning', error.message.message);
                }
            );
        }
    };
}]);