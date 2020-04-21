angular.module('tilsoftApp').service('esthourspendService', ['$rootScope', 'dataService', function ($rootScope, dataService) {
    this.data = null;
    this.parentScope = null;

    this.openForm = function () {
        this.data = angular.copy(this.parentScope.data.devRequestAssignments);

        jQuery('#frmEstHourSpend').modal('show');
    };

    this.update = function () {
        dataService.esthourspend(
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