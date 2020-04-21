angular.module('tilsoftApp').service('editService', ['$rootScope', 'dataService', function ($rootScope, dataService) {
    this.data = null;
    this.parentScope = null;

    this.openForm = function (item) {
        this.data = angular.copy(item);
        this.parentScope.selectedUserId = null;
        jsHelper.showPopup('frmEdit', function () { });
    };

    this.closeForm = function(){
        jsHelper.hidePopup('frmEdit', function () { });
    }

    this.addPermission = function () {
        debugger;
        var item = parseInt(this.parentScope.selectedUserId);
        if (!item) return; // dont proceed if select id invalid
        if (jsHelper.getArrayIndex(this.data.onlineFilePermissions, 'userID', item) >= 0) return; // dont proceed if userid exists
        var userIndex = jsHelper.getArrayIndex(this.parentScope.users, 'userID', item);   // dont proceed if selected id dont have detail info      
        if(userIndex < 0) return;
        
        this.data.onlineFilePermissions.push({
            onlineFilePermissionID: dataService.getIncrementId(),
            userID: this.parentScope.selectedUserId,
            employeeNM: this.parentScope.users[userIndex].employeeNM,
            internalCompanyNM: this.parentScope.users[userIndex].internalCompanyNM
        });
        this.parentScope.selectedUserId = null;
    };

    this.removePermission = function(permission){
        this.data.onlineFilePermissions.splice(this.data.onlineFilePermissions.indexOf(permission), 1);
    };

    this.update = function () {
        dataService.update(
            this.data.onlineFileID,
            this.data,
            function (data) {
                jsHelper.processMessage(data.message);
                if (data.message.type === 0) {
                    $rootScope.$broadcast('refresh');
                }
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            }
        );
    };
}]);