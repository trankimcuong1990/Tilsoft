var tilsoftApp = angular.module('tilsoftApp', []);

tilsoftApp.filter('sumFunc', function () {
    return function (data, key) {
        if (angular.isUndefined(data) || angular.isUndefined(key))
            return 0;
        var sum = parseFloat('0');
        angular.forEach(data, function (v, k) {
            sum = sum + parseFloat(v[key] == null ? 0 : v[key]);
        });
        return sum;
    }
});

tilsoftApp.filter('absFunc', function () {
    return function (input) {
        return Math.abs(input);
    };
});


tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;


    //sort able 
    $scope.sortType_Exp = 'totalEURAmount'; 
    $scope.sortReverse_Exp = true;

    $scope.sortType_CI = 'totalEURAmount'; 
    $scope.sortReverse_CI = true;

    $scope.sortType_CI_New = 'totalEURAmount';
    $scope.sortReverse_CI_New = true;

    $scope.sortType_CI_Losted = 'totalEURAmount';
    $scope.sortReverse_CI_Losted = true;

    $scope.sortType_PI_Losted = 'totalEURAmount';
    $scope.sortReverse_PI_Losted = true;

    $scope.sortType_PI_New = 'totalEURAmount';
    $scope.sortReverse_PI_New = true;

    $scope.filter = {
        clientNM: '',
        clientCountryUD: '',
        saleUD: ''
    };

    $scope.sortValue = {
        ExpectedByClient: '-totalEURAmount',
        PIByClient: '-totalEURAmount',
        PIConfirmedByClient: '-totalEURAmount',
    };
    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getReportData(
                context.season,
                function (data) {
                    $scope.data = data.data;
                    $scope.$apply();                    
                },
                function (error) {
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        }
    };

    //
    // method
    //
    $scope.method = {
        //sort function
        sortData: function (key, field) {
            var currentSetting = $scope.sortValue[key];
            if (currentSetting.replace('-', '') === field) {
                if (currentSetting.indexOf('-') < 0) {
                    $scope.sortValue[key] = '-' + field;
                }
                else {
                    $scope.sortValue[key] = field;
                }
            }
            else {
                $scope.sortValue[key] = field;
            }
        }
    };


    //
    // init
    //
    $scope.event.init();
    jQuery('#content').show();
}]);