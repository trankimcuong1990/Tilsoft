angular.module('tilsoftApp').controller('salesManagementController', ['$scope', '$rootScope', '$timeout', 'dataService', '$filter', function ($scope, $rootScope, $timeout, dataService, $filter) {
    //
    // property
    //
    $scope.salesManagement = {};

    //
    // listen to parent scope
    //
    $rootScope.$on('salesManagement', function () {
        if ($scope.data.selectedSaleID === -1) {
            $('#pnlSalesManagement').show();
        }
        else {
            $('#pnlSalesManagement').hide();
        }
    });

    //
    // method
    //
    $scope.salesManagement.method = {};    
}]);