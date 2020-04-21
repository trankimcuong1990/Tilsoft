var tilsoftApp = angular.module('tilsoftApp', []);

tilsoftApp.filter('sumOfFloatValue', function () {
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

tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.seasons = null;
    $scope.selection = {
        season: '2019/2020'
    };
    $scope.data = null;
    $scope.currentDate = new Date();

    //filter
    $scope.sortType = 'totalConfirmedConvertToEUR'; // set the default sort type
    $scope.sortReverse = true;  // set the default sort order
    $scope.filters = {
        saleUD1: '',
        saleUD2: '',
        saleVNUD: '',
        agentUD3: '',
        haveBizRelationshipFrom: '',
        clientCountryNM: '',
        clientUD: '',
        clientNM: ''
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getInitData(
                function (data) {
                    $scope.seasons = data.data.seasons;
                    $scope.selection.season = jsHelper.getCurrentSeason();
                    $scope.$apply();
                    jQuery('#content').show();
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.$apply();
                }
            );
        },
        exportToExcel: function () {
            jsonService.getReport(
                $scope.selection.season,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        load: function () {
            var season = $scope.selection.season;
            jsonService.getReportHTML(season, null,
                function (data) {
                    $scope.data = data.data;
                    $scope.seasons = data.data.seasons;
                    $('#pnlHtmlReport').show();

                    //calculte ecommercial inovice convert to usd
                    angular.forEach($scope.data.clientInfos, function (item) {
                        item.totalNetAmountConvertToUSD = (item.totalNetAmountUSD === null ? 0 : item.totalNetAmountUSD) + (item.totalNetAmountEUR === null ? 0 : item.totalNetAmountEUR) * $scope.data.exchangeRateLastSeason;
                        item.deltaOfDeltaPercent = (item.osGrossMarginPercent === null ? 0 : item.osGrossMarginPercent) - (item.grossMarginPercent === null ? 0 : item.grossMarginPercent);
                    });

                    $scope.$apply();

                    //sroll 2 table at same time
                    $("div").on("scroll", function () {
                        $("div:not(this)").scrollLeft($(this).scrollLeft());
                    });

                    //auto full screen
                    if (!$scope.isLoaded) {
                        $scope.isLoaded = true;
                        $('#pnlDDCReport').find('.jarviswidget-fullscreen-btn').click();
                    }

                },
                function (error) {
                    $scope.seasons = null;

                }
            );
        },

        changSeason: function () {
            //$scope.event.load($scope.selection.season);
        }
    };

    //
    //method
    //
    $scope.method = {
        getTotalExpected: function () {
            var total = 0;
            if ($scope.data !== null && $scope.data.clientInfos !== null) {
                angular.forEach($scope.data.clientInfos, function (value, key) {
                    total += value.totalExpected;
                }, null);
            }
            return total;
        }
    };

    //
    // init
    //
    //$scope.event.load($scope.selection.season);
    $scope.event.init();
}]);