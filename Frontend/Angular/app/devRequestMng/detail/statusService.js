angular.module('tilsoftApp').service('statusService', ['$rootScope', 'dataService', function ($rootScope, dataService) {
    this.data = null;

    this.openForm = function () {
        this.data = {
            devRequestStatusID: null,
            comment: ''
        }

        jQuery('#frmStatus').modal('show');
    };

    this.update = function () {
        if (this.data.devRequestStatusID !== null) {
            dataService.status(
                context.id,
                this.data,
                function (data) {
                    if (data.message.type == 0) {
                        $rootScope.$broadcast('refresh');
                    }
                },
                function (error) {
                    alert(error.data.message.message);
                    jsHelper.showMessage('warning', error);
                }
            );
        }
        else {
            alert('Invalid input data, please check');
        }
    };
}]);