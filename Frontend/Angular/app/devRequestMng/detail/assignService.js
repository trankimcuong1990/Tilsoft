angular.module('tilsoftApp').service('assignService', ['$rootScope', 'dataService', function ($rootScope, dataService) {
    this.data = null;

    this.openForm = function () {
        this.data = {
            assignedTo: null,
            isPersonInCharge: false,
            comment: ''
        }

        jQuery('#frmAssign').modal('show');
    };

    this.update = function () {
        if (this.data.assignedTo !== null) {
            dataService.assign(
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
            alert('Invalid input data, please check');
        }
    };
}]);