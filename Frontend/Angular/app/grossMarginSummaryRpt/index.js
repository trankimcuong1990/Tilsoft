//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.selection = {
        fromDate: null,
        toDate: null,
        season: jsHelper.getCurrentSeason()
    };

    $scope.data = null;
    $scope.seasons = jsHelper.getSeasons();
    $scope.totalAmount = {
        totalNet: 0,
        totalCost: 0,
        totalCom: 0,
        totalCreditNote: 0,
        totalGrossMargin: 0,
        totalGrossMaroginCom: 0,
        totalGrossMarginComCredit: 0,

        percentGrossMargin: 0,
        percentGrossMaroginCom: 0,
        percentGrossMarginComCredit: 0
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
            jQuery('#content').show();
        },
        generateExcel: function () {
            jsonService.getExcelReport(
                $scope.selection.fromDate,
                $scope.selection.toDate,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        generateExcelForecast: function () {
            jsonService.getExcelForecastReport(
                $scope.selection.season,
                function (data) {
                    if (data.message.type === 0) {
                        window.location = context.backendReportUrl + data.data;
                    }
                    else {
                        jsHelper.showMessage('warning', data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        generateHtml: function () {
            jsonService.getHtmlReport(
                $scope.selection.fromDate,
                $scope.selection.toDate,
                function (data) {
                    $scope.data = data.data;
                    $scope.event.calTotalAmount();
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },

        calTotalAmount: function () {
            angular.forEach($scope.data.htmlGrossMarginSummaries, function (item) {
                $scope.totalAmount.totalNet += item.netAmountEUR;
                $scope.totalAmount.totalCost += item.costAmountEUR;
                $scope.totalAmount.totalCom += item.commAmountEUR;
                $scope.totalAmount.totalCreditNote += item.creditNoteAmountEUR;
                $scope.totalAmount.totalGrossMargin += item.grossMarginBeforeEUR;
                $scope.totalAmount.totalGrossMaroginCom += item.grossMarginAfterCommEUR;
                $scope.totalAmount.totalGrossMarginComCredit += item.grossMarginAfterCommAndCreditNoteEUR;
            });
            $scope.totalAmount.percentGrossMargin = ($scope.totalAmount.totalGrossMargin / $scope.totalAmount.totalNet) * 100;
            $scope.totalAmount.percentGrossMaroginCom = ($scope.totalAmount.totalGrossMaroginCom / $scope.totalAmount.totalNet) * 100;
            $scope.totalAmount.percentGrossMarginComCredit = ($scope.totalAmount.totalGrossMarginComCredit / $scope.totalAmount.totalNet) * 100;
        },
    }

    //
    // method
    //

    //
    // init
    //
    $scope.event.init();
}]);