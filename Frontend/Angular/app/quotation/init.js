//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.factories = null;
    $scope.orders = null;
    $scope.seasons = null;
    $scope.filters = {
        factoryID: null,
        season: null
    };

    //
    // event
    //
    $scope.event = {
        init : function(){
            jsonService.getInitData(
                function (data) {
                    $scope.factories = data.data.factories;
                    $scope.orders = data.data.orders;
                    $scope.seasons = data.data.seasons;
                    $scope.$apply();

                    jQuery('#content').show();
                },
                function (error) {
                    $scope.factories = null;
                    $scope.orders = null;
                    $scope.seasons = null;
                    $scope.$apply();
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        goNext: function () {
            if (!$scope.filters.factoryID || $scope.filters.factoryID == '' || typeof $scope.filters.factoryID == 'undefined') {
                alert('Factory is invalid !');
                return;
            }
            if (!$scope.filters.season || $scope.filters.season == '' || typeof $scope.filters.season == 'undefined') {
                alert('Season is invalid !');
                return;
            }
            var orderList = [];
            angular.forEach($scope.orders, function (value, key) {
                if (value.isSelected) {
                    orderList.push(value.factoryOrderID);
                }
            }, null);
            if (orderList.length == 0) {
                alert('Please select order to create quotation !');
                return;
            }

            var queryString = '';
            queryString += 'f=' + $scope.filters.factoryID;
            queryString += '&s=' + $scope.filters.season;

            window.location = context.nextURL + queryString + '&o=' + JSON.stringify(orderList);
        },
        goBack: function ($event) {
            $event.preventDefault();

            window.location = context.backURL;
        },
        clearSelection: function () {
            angular.forEach($scope.orders, function (value, key) {
                value.isSelected = false;
            }, null);
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);