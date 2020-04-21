
(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive']).controller('tilsoftController', summaryOutWardRptController);
    summaryOutWardRptController.$inject = ['$scope', 'dataService'];

    function summaryOutWardRptController($scope, summaryOutWardRptService) {
        // variable
        $scope.data = [];

        $scope.workOrder = [];
        $scope.workOrderDetail = [];

        $scope.selection = {
            monthValue: null,
            yearValue: null,
            workOrderStatusNM: null,
        };

        $scope.listStatus = [
            { workOrderStatusID: 'Finish', workOrderStatusNM: 'Finish' },
            { workOrderStatusID: 'Not Finish', workOrderStatusNM: 'Not Finish' }
        ];

        $scope.supportYear = [];
        $scope.supportMonth = [];

        $scope.totalBOM = 0;
        $scope.totalBOMActual = 0;
        $scope.totalDiffirence = 0;
        // event
        $scope.event = {
            init: init,
            generateExcel: generateExcel,
            search: search
        };
        $scope.method = {
            getYear: getYear,
            getMonth: getMonth,

            getTotalAmountBOM: function (workOrder) {
                var totalAmount = 0;

                for (var i = 0; i < $scope.workOrderDetail.length; i++) {
                    var workOrderDetail = $scope.workOrderDetail[i];

                    if (workOrderDetail.workOrderID === workOrder.workOrderID) {
                        if (workOrderDetail.totalAmountBOM !== null) {
                            totalAmount = parseFloat(totalAmount) + parseFloat(workOrderDetail.totalAmountBOM);
                        }
                    }
                }

                return totalAmount;
            },

            getTotalAmountActual: function (workOrder) {
                var totalAmount = 0;

                for (var i = 0; i < $scope.workOrderDetail.length; i++) {
                    var workOrderDetail = $scope.workOrderDetail[i];

                    if (workOrderDetail.workOrderID === workOrder.workOrderID) {
                        if (workOrderDetail.totalValue !== null) {
                            totalAmount = parseFloat(totalAmount) + parseFloat(workOrderDetail.totalValue);
                        }
                        if (workOrderDetail.totalAmountActual !== null){
                            totalAmount = parseFloat(totalAmount) + parseFloat(workOrderDetail.totalAmountActual);
                        }
                    }
                }

                return totalAmount;
            },

            getTotalAmountDiffirence: function (workOrder) {
                var totalAmount = 0;

                for (var i = 0; i < $scope.workOrderDetail.length; i++) {
                    var workOrderDetail = $scope.workOrderDetail[i];

                    if (workOrderDetail.workOrderID === workOrder.workOrderID) {
                        if (workOrderDetail.totalAmountDiffirence !== null) {
                            totalAmount = parseFloat(totalAmount) + parseFloat(workOrderDetail.totalAmountDiffirence);
                        }
                        if (workOrderDetail.totalDiffirenceOfComponent !== null) {
                            totalAmount = parseFloat(totalAmount) + parseFloat(workOrderDetail.totalDiffirenceOfComponent);
                        }
                    }
                }

                return totalAmount;
            }
        };

        function init() {       
            summaryOutWardRptService.serviceUrl = context.serviceUrl;
            summaryOutWardRptService.token = context.token;
            summaryOutWardRptService.supportServiceUrl = context.supportServiceUrl;
            jQuery('#content').show();
            $scope.method.getYear();
            $scope.method.getMonth();
        };


        function generateExcel() {
            summaryOutWardRptService.generateExcel(
                scope.selection.monthValue,
                scope.selection.yearValue,
                scope.selection.workOrderStatusNM,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function search() {
            $scope.workOrder = [];
            $scope.workOrderDetail = [];
            summaryOutWardRptService.getDataWithFilters(
                scope.selection.monthValue,
                scope.selection.yearValue,
                scope.selection.workOrderStatusNM,
                function (data) {
                    $scope.data = data.data.summaryOutWardReportDatas;

                    // get parent bomid is null.
                    for (var i = 0; i < $scope.data.length; i++) {
                        var itemParentBomIsNull = $scope.data[i];

                        if (itemParentBomIsNull.parentBOMID === null) {
                            $scope.workOrder.push(itemParentBomIsNull);
                        }
                    }

                    // get parent bomid isnot null.
                    for (var i = 0; i < $scope.data.length; i++) {
                        var itemParentBomIsNotNull = $scope.data[i];

                        if (itemParentBomIsNotNull.parentBOMID !== null) {
                            $scope.workOrderDetail.push(itemParentBomIsNotNull);
                        }
                    }
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });          
        };     

        function getYear() {
            var startYear = '1990'
            var currentYear = (new Date()).getFullYear();
            for (var i = parseInt(startYear); i <= parseInt(currentYear); i++) {
                var currentItem = { 'yearID': i, 'yearValue': i };
                $scope.supportYear.push(currentItem);
            }
        };

        function getMonth() {
            var startMonth = '1'
            for (var i = parseInt(startMonth); i <= 12; i++) {
                var currentItem = { 'monthID': i, 'monthValue': i };
                $scope.supportMonth.push(currentItem);
            }
        };

        // default event
        $scope.event.init();
    }
})();