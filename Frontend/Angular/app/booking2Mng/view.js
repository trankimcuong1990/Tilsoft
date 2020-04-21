jQuery('#grdLoadingPlan').scrollableTable2();

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ngSanitize']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                0,
                0,
                '',
                function (data) {
                    $scope.data = data.data.data;
                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },
        downloadFile: function () {
            if ($scope.data.fileLocation) {
                window.open($scope.data.fileLocation);
            }
        },
        print: function () {
            jsonService.print(
                context.id,
                function (data) {
                    window.location = context.backendReportUrl + data.data + '.xlsm';
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.editUrl + id;
        },
        formatRemark: function (remark) {
            if (remark == null) {
                return '';
            }
            return remark.replace(/(?:\r\n|\r|\n)/g, '<br />');
        },
    };

    //
    // init
    //    
    $scope.event.init();
}]);