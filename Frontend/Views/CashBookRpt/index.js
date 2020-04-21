(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', cashBookRptController);
    cashBookRptController.$inject = ['$scope', '$timeout', 'dataService'];

    function cashBookRptController($scope, $timeout, cashBookRptService) {
        $scope.filters = {
            paymentType: null,
            startDate: '',
            endDate: '',
            bankAccount: null,
        };

        $scope.support = {
            paymentTypeList: [],
            bankAccountList: [],
        };

        $scope.data = null;

        $scope.method = {
            getBegin: getBegin,
            sumReceipt: sumReceipt,
            sumPayment: sumPayment,
            sumEnding: sumEnding
        };

        $scope.event = {
            init: init,
            search: search,
            report: report
        };

        function getBegin() {
            var sum = 0;

            if ($scope.data !== null) {
                sum = $scope.data.beginning;
            }

            return sum;
        };

        function sumReceipt() {
            var sum = 0;

            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.cashBookRpt_SearchResultDtos.length; i++) {
                    sum = parseFloat(sum) + parseFloat($scope.data.cashBookRpt_SearchResultDtos[i].totalReceipt);
                }
            }

            return sum;
        };

        function sumPayment() {
            var sum = 0;

            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.cashBookRpt_SearchResultDtos.length; i++) {
                    sum = parseFloat(sum) + parseFloat($scope.data.cashBookRpt_SearchResultDtos[i].totalPayment);
                }
            }

            return sum;
        };

        function sumEnding() {
            var tempSumBegin = getBegin();
            var tempSumReceipt = sumReceipt();
            var tempSumPayment = sumPayment();

            return tempSumBegin + tempSumReceipt - tempSumPayment;
        };

        function init() {
            cashBookRptService.serviceUrl = context.serviceUrl;
            cashBookRptService.token = context.token;

            cashBookRptService.getInit(
                function (data) {
                    $scope.support.paymentTypeList = data.data.cashBookRptPaymentTypeDtos;
                    $scope.support.bankAccountList = data.data.cashBookRptBankAccountDtos;

                    jQuery('#content').show();
                },
                function (error) {
                });
        };

        function search(paymentType, bankAccount, startDate, endDate) {
            startDate = jQuery('#startDate').val();
            endDate = jQuery('#endDate').val();

            if (startDate === null || startDate === '') {
                jsHelper.showMessage('warning', context.errorValid);
                return false;
            }

            if (endDate === null || endDate === '') {
                jsHelper.showMessage('warning', context.errorValid);
                return false;
            }

            if (paymentType === null) {
                jsHelper.showMessage('warning', 'Please select at least one value to search!');
                return false;
            }

            if (paymentType === 2 && bankAccount === null) {
                jsHelper.showMessage('warning', 'Please select at least one value to search!');
                return false;
            }

            cashBookRptService.getDataWithFilters(
                paymentType,
                bankAccount,
                startDate,
                endDate,
                function (data) {
                    if (data.message.type == 0) {
                        $scope.data = data.data;

                    } else {
                        jsHelper.showMessage('warning', data.message.message);
                    }

                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function report(paymentType, bankAccount, startDate, endDate) {
            startDate = jQuery('#startDate').val();
            endDate = jQuery('#endDate').val();

            if (startDate === null || startDate === '') {
                jsHelper.showMessage('warning', context.errorValid);
                return false;
            }

            if (endDate === null || endDate === '') {
                jsHelper.showMessage('warning', context.errorValid);
                return false;
            }

            if (paymentType === null) {
                jsHelper.showMessage('warning', 'Please select at least one value to search!');
                return false;
            }

            if (paymentType === 2 && bankAccount === null) {
                jsHelper.showMessage('warning', 'Please select at least one value to search!');
                return false;
            }

            cashBookRptService.getDataWithFilterExcel(
                paymentType,
                bankAccount,
                startDate,
                endDate,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        $timeout(function () {
            $scope.event.init();
        }, 0);
    };
})();