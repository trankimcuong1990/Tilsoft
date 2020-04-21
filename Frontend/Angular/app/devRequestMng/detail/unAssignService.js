angular.module('tilsoftApp').service('unAssignService', ['$rootScope', 'dataService', function ($rootScope, dataService) {
    this.data = null;

    this.openForm = function (item) {
        this.data = {
            assignedTo: item.assignedTo,
            assignedPersonName: item.assignedPersonName,
            comment: ''
        }

        jQuery('#frmUnAssign').modal('show');
    };

    this.update = function () {
        dataService.unAssign(
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