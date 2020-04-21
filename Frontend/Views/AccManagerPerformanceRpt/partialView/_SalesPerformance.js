angular.module('tilsoftApp').controller('accSalesPerformanceController', ['$scope', '$rootScope', '$timeout', 'dataService', '$filter', function ($scope, $rootScope, $timeout, dataService, $filter) {
    //
    // property
    //
    $scope.salesManagement = {};

    //
    // listen to parent scope
    //
    $rootScope.$on('accSalesPerformanceController', function () {
        if ($scope.data.selectedSaleID === -1) {
            $('#pnlSalesPerformance').show();
        }
        else {
            $('#pnlSalesPerformance').hide();
        }
    });

    //
    // method
    //
    $scope.salesManagement.method = {};    
}]);