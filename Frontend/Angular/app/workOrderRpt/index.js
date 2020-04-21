
//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.filter('unique', function () {
    return function (arr, field) {
        return _.uniqBy(arr, function (a) {
            return a[field];
        });
    };
});
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;
    $scope.opSequenceDetails = null;
    $scope.productionTeams = null;
    $scope.factoryWarehouses = null;
    $scope.details = null;
    $scope.receipts = null;

    $scope.receiptOverviews = null;
    $scope.receiptOverviewDetails = null;

    //
    // event
    //
    $scope.event = {
        reload: function () {
            dataService.getReportData(
                context.id,
                function (data) {
                    if (data.message.type === 0) {
                        jQuery('#content').show();
                        
                        $scope.data = data.data.data;
                        $scope.opSequenceDetails = data.data.opSequenceDetails;
                        $scope.productionTeams = data.data.productionTeams;
                        $scope.factoryWarehouses = data.data.factoryWarehouses;
                        $scope.details = data.data.details;
                        $scope.receipts = data.data.receipts;

                        $scope.receiptOverviews = data.data.receiptOverviews;
                        $scope.receiptOverviewDetails = data.data.receiptOverviewDetails;
                    }
                },
                function (error) {
                    $scope.data = null;
                    $scope.opSequenceDetails = null;
                    $scope.productionTeams = null;
                    $scope.factoryWarehouses = null;
                    $scope.details = null;
                    $scope.receipts = null;

                    $scope.receiptOverviews = null;
                    $scope.receiptOverviewDetails = null;
                }
            );
        },
        init: function () {
            $scope.event.reload();
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);