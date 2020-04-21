//
// SUPPORT
//
jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
tilsoftApp.filter('sumFunction', function () {
    return function (data, key) {
        if (angular.isUndefined(data) || angular.isUndefined(key))
            return 0;
        var sum = 0;
        angular.forEach(data, function (v, k) {
            sum = sum + parseFloat(v[key] === null ? 0 : v[key]);
        });
        return sum;
    };
});
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', '$timeout', 'dataService', 'orderByFilter', '$filter', '$rootScope', function ($scope, $cookieStore, $timeout, dataService, orderByFilter, $filter, $rootScope) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.supportData = {
        sales: [],
        season: jsHelper.getCurrentSeason(),
        preSeason: jsHelper.getPrevSeason(jsHelper.getCurrentSeason())
    };
    $scope.data = {
        selectedSaleID: context.saleId
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
            $('.page-title').hide();
            dataService.getInit(
                function (data) { 
                    if (data.message.type === 0) {
                        $scope.supportData.sales = data.data.saleDTOs;
                        //$scope.supportData.sales.unshift({
                        //    userID: -1,
                        //    saleUD: '',
                        //    employeeNM: 'All Account Manager',
                        //    employeeFirstNM: ''
                        //});
                        $('#content').show();
                        $scope.event.onSelectSale();
                    }
                },
                function (err) {
                    $scope.supportData.sales = [];
                    jsHelper.showMessage('warning', error.message.message);
                    console.log(err);
                }
            );
        },
        onSelectSale: function () {
            $('.rptContent').show();
            $rootScope.$emit('performance');
            $rootScope.$emit('salesByCountry');
            $rootScope.$emit('salesConclusion');
            $rootScope.$emit('salesManagement');
            $rootScope.$emit('accSalesPerformanceController');
        }
    };

    //
    // method
    //
    $scope.method = {
    };

    //
    // init
    //
    $scope.event.init();
}]);