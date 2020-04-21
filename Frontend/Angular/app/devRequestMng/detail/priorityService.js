angular.module('tilsoftApp').service('priorityService', ['$rootScope', 'dataService', function ($rootScope, dataService) {
    this.data = null;

    this.openForm = function () {
        this.data = {
            priority: null,
            comment: ''
        }

        jQuery('#frmPriority').modal('show');
    };

    this.update = function () {
        if (this.data.priority !== null) {
            dataService.priority(
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