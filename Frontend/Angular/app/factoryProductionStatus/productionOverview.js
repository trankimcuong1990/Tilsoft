var grdOverview = jQuery('#grdOverview').scrollableTable(null, function (sortedBy, sortedDirection) {
    var scope = angular.element(jQuery('body')).scope();
    datasource = scope.data.orders;

    if (sortedDirection == 'asc') {
        datasource.sort(function (a, b) {
            return a[sortedBy] > b[sortedBy] ? 1 : -1;
        });
    }
    else if (sortedDirection == 'desc') {
        datasource.sort(function (a, b) {
            return a[sortedBy] < b[sortedBy] ? 1 : -1;
        });
    }
    scope.$apply();
});

var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives', 'furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', function ($scope, $cookieStore) {

    $scope.data = {
        factoryID : null,
        season: context.currentSeason,

        currentWeek: -1,
        weekSeasons:[],
        orders: [],
        productionByWeeks:[]
    }
    $scope.filters = {
    };
    $scope.factories = [];
    $scope.seasons = [];
    //
    // event
    //
    $scope.event = {
        search: function () {
            jsonService.getProductionOverview($scope.data.factoryID, $scope.data.season, false,
                 function (data) {
                     $scope.data.currentWeek = data.data.currentWeek;
                     $scope.data.weekSeasons = data.data.weekSeasons;
                     $scope.data.orders = data.data.orders;
                     $scope.data.productionByWeeks = data.data.productionByWeeks;
                     $scope.$apply();
                     if (data.message.type == 2) {
                         jsHelper.processMessage(data.message);
                     }
                 },
                 function (error) {
                     $scope.data = null;
                     $scope.pageIndex = 1;
                     $scope.totalPage = 0;
                     $scope.$apply();
                 }
             );
        },
        reload: function () {
            jsonService.getProductionOverview($scope.data.factoryID,$scope.data.season,true,
                function (data) {
                    $scope.factories = data.data.factories;
                    $scope.seasons = data.data.seasons;
                    $scope.data.currentWeek = data.data.currentWeek;
                    $scope.data.weekSeasons = data.data.weekSeasons;
                    $scope.data.orders = data.data.orders;
                    $scope.data.productionByWeeks = data.data.productionByWeeks;
                    $scope.$apply();
                    $("#factoryID").select2();
                    $("#season").select2();
                    jQuery('#content').show();
                    if (data.message.type == 2) {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.$apply();
                }
            );
        },
    }

    $scope.method = {
        getProducedCont : function(factoryOrderID,weekNo){
            for (var i = 0; i < $scope.data.productionByWeeks.length; i++) {
                if ($scope.data.productionByWeeks[i].factoryOrderID == factoryOrderID && $scope.data.productionByWeeks[i].weekNo == weekNo) {
                    if ($scope.data.productionByWeeks[i].totalProducedContainerQnt != 0) {
                        return $scope.data.productionByWeeks[i].totalProducedContainerQnt;
                    }
                    else {
                        return '';
                    }
                }
            }
        },

        getLDSClass: function (factoryOrderID, weekNo) {
            for (var i = 0; i < $scope.data.orders.length; i++) {
                if ($scope.data.orders[i].factoryOrderID == factoryOrderID && $scope.data.orders[i].ldS_Week == weekNo) {
                    return 'lds';
                }
            }
        },

        getCurrentWeekClass: function (weekNo) {
            if ($scope.data.currentWeek == weekNo)
            {
                return 'currentWeek';
            }
        },

        getTotalOrderCont: function () {
            var total = 0;
            if ($scope.data!=null && $scope.data.orders != null) {
                angular.forEach($scope.data.orders, function (value, key) {
                    total += value.totalContOrder;
                }, null);
            }
            return total;
        },

        getTotalProducedCont: function () {
            var total = 0;
            if ($scope.data != null && $scope.data.orders != null) {
                angular.forEach($scope.data.orders, function (value, key) {
                    total += value.totalProducedCont;
                }, null);
            }
            return total;
        },

        getTotalProducedContByWeek: function (weekNo) {
            var total = 0;
            if ($scope.data != null && $scope.data.productionByWeeks != null) {
                angular.forEach($scope.data.productionByWeeks, function (value, key) {
                    if (value.weekNo == weekNo)
                    total += value.totalProducedContainerQnt;
                }, null);
            }
            return total;
        },

    }

    $scope.event.reload();
}]);