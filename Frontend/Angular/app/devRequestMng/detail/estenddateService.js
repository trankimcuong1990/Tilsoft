angular.module('tilsoftApp').service('estenddateService', ['$rootScope', 'dataService', function ($rootScope, dataService) {
    this.data = null;

    this.openForm = function () {
        this.data = {
            estEndDate: null,
            comment: ''
        }

        jQuery('#frmEstEndDate').modal('show');
    };

    this.update = function () {
        if (this.data.estEndDate !== null) {
            dataService.estenddate(
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