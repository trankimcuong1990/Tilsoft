var tilsoftApp = angular.module('tilsoftApp', []);
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
            jsonService.getReportData(
                context.season,
                function (data) {
                    $scope.data = data.data;

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
    // method
    //
    $scope.method = {
        formatCurrency: function (text) {
            return formatCurrency(parseFloat(text), 0);
        },
        formatContainerNumber: function (text) {
            return formatCurrency(parseFloat(text), 1);
        },
        formatNumber: function (text, decimal) {
            return formatCurrency(parseFloat(text), decimal);
        },

        //total cont Production
        calTotalConfirmedOrder: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            angular.forEach($scope.data.productions, function (item) {
                total += (JSON.stringify(item.totalOrder) == 'null' ? parseFloat('0') : parseFloat(item.totalOrder));
            }, total);
            return total;
        },


        calTotalShipped: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            angular.forEach($scope.data.productions, function (item) {
                total += (JSON.stringify(item.totalShipped) == 'null' ? parseFloat('0') : parseFloat(item.totalShipped));
            }, total);
            return total;
        },

        calTotalToBeShipped: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            angular.forEach($scope.data.productions, function (item) {
                total += (JSON.stringify(item.totalToBeShipped) == 'null' ? parseFloat('0') : parseFloat(item.totalToBeShipped));
            }, total);
            return total;
        },

        getExpectationNextSeason: function () {
            var objResult = null;
            angular.forEach($scope.data.ddcNextSeasons, function (item) {
                if (item.displayText.indexOf("Average") >= 0) {
                    objResult = item;             
                }
            });
            return objResult;
        }


    }


    //
    // init
    //
    $scope.event.init();
}]);