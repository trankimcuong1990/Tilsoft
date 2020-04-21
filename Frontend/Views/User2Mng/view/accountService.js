angular.module('tilsoftApp').service('accountService', ['$rootScope', 'dataService', function ($rootScope, dataService) {
    this.data = null;
    this.parentScope = null;

    this.openForm = function () {
        this.data = {
            userName: this.parentScope.data.userName,
            userGroupID: this.parentScope.data.userGroupID,
            isActivated: this.parentScope.data.isActivated,
            isSuperUser: this.parentScope.data.isSuperUser,
            lastLoginDate: this.parentScope.data.lastLoginDate,
        };
        jsHelper.showPopup('frmAccount', function () { });
    };

    this.closeForm = function () {
        jsHelper.hidePopup('frmAccount', function () {
            jsHelper.goToSection('account-section');
        });
    };

    this.update = function () {
        if (this.parentScope.frmAccount.$valid) {
            dataService.updateAccount(
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
        }
        else {
            jsHelper.showMessage('warning', 'Invalid input data, please check');
        }
    };
}]);