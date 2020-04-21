var tilsoftApp = angular.module('tilsoftApp', []);

tilsoftApp.filter('sumOfFloatValue', function () {
    return function (data, key) {
        if (angular.isUndefined(data) || angular.isUndefined(key))
            return 0;
        var sum = 0;
        angular.forEach(data, function (v, k) {
            sum = sum + parseFloat(v[key]);
        });
        return sum;
    }
});

tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;
    $scope.categories = [];
    $scope.countriesByItem = [];
    $scope.saleByItem = [];

    //table field
    $scope.sortType = 'numContOrder'; // set the default sort type
    $scope.sortReverse = true;  // set the default sort order
    $scope.searchFish = '';     // set the default search/filter term

    $scope.filters = {
        factoryUD: '',
        locationNM : '',
        manufacturerCountryNM: '',
        factoryName: '',
        numContOrder: '',
        numContShipped: '',
        numContTobeShipped : '',
        factoryProformaInvoiceTotalAmount: '',
        factoryProformaInvoiceTotalCont: '',
        factoryInvoiceTotalAmount:''
    }

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getReportData(
                context.season,
                function (data) {
                    $scope.data = data.data;

                    //apply data
                    $scope.$apply();
                    jQuery('#content').show();
                },
                function (error) {
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);