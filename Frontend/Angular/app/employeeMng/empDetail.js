//
//APP START
//
$("#factoryID").select2({
    closeOnSelect: true,
});
var tilsoftApp = angular.module('tilsoftApp', ['customFilters', 'furnindo-directive']);

tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    $scope.dataContainer = null;
    $scope.newID = -1;

    $scope.event = {
        init: function () {
            employeeMngService.loadEmpDetail(
                context.id,
                function (data) {
                    $scope.dataContainer = data.data.data;

                    //console.log(data);
                    //apply data
                    $scope.$apply();

                    jQuery('#content').show();
                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                    if (data.message.type == 2) {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.datdataContainer = null;
                    $scope.$apply();
                }
            );
        },
        skypeChat: function (value) {
            window.location = 'skype:' + value + '?chat';
        },
        openCall: function (value) {
            window.location = 'tel:' + value;
        },
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        },
    };
    //
    // init
    //
    $scope.event.init();
}]);
