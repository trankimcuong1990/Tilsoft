(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', purchaseOrderTrackingController);
    purchaseOrderTrackingController.$inject = ['$scope', '$timeout', 'dataService'];

    function purchaseOrderTrackingController($scope, $timeout, purchaseOrderTrackingService) {
        $scope.filters = {
            fromDate: '',
            toDate: '',
            supplierID: '',
            status: ''
        };

        $scope.support = {
            supplier: []
        };

        $scope.data = {
            purchaseOrderTracking: [],
            weekInfo: []
        };

        $scope.event = {
            init: function () {
                purchaseOrderTrackingService.serviceUrl = context.serviceUrl;
                purchaseOrderTrackingService.token = context.token;

                purchaseOrderTrackingService.getInit(
                    function (data) {
                        $scope.support.supplier = data.data.supportSupplier;

                        jQuery('#content').show();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            },
            search: function () {
                purchaseOrderTrackingService.searchFilter.filters = $scope.filters;

                purchaseOrderTrackingService.search(
                    function (data) {
                        $scope.data.purchaseOrderTracking = data.data.purchaseOrderTracking;
                        $scope.data.weekInfo = data.data.purchaseOrderTrackingWeek;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            },
            movePurchasingOrder: function (purchaseOrderID) {
                window.open(context.editPurchaseOrderUrl + purchaseOrderID, '_blank');
            },
            generateExcel: function () {
                purchaseOrderTrackingService.searchFilter.filters = $scope.filters;
                purchaseOrderTrackingService.getExcelReport(
                    purchaseOrderTrackingService.searchFilter,
                    function (data) {
                        window.location = context.backendReportUrl + data.data;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        };

        $scope.method = {
            getTotalOrderWithItem: function (item) {
                var totalOrder = 0;

                if (item.purchaseOrderTrackingDetail.length > 0) {
                    for (var i = 0; i < item.purchaseOrderTrackingDetail.length; i++) {
                        if (item.purchaseOrderTrackingDetail[i].orderQnt !== null) {
                            totalOrder = totalOrder + item.purchaseOrderTrackingDetail[i].orderQnt;
                        }
                    }
                }

                return totalOrder;
            },
            getTotalReceiveWithItem: function (item) {
                var totalReceive = 0;

                if (item.purchaseOrderTrackingDetail.length > 0) {
                    for (var i = 0; i < item.purchaseOrderTrackingDetail.length; i++) {
                        if (item.purchaseOrderTrackingDetail[i].receiveQnt !== null) {
                            totalReceive = totalReceive + item.purchaseOrderTrackingDetail[i].receiveQnt;
                        }
                    }
                }

                return totalReceive;
            },
            getColorWithStatus: function (item) {
                if (item.purchaseOrderStatusID===1) {
                    //Open
                    return '#d3d3d3';
                }
                else if (item.purchaseOrderStatusID===2) {
                    //Confirmed
                    return '#bafbce';
                } else if (item.purchaseOrderStatusID===3) {
                    //Cancel
                    return '#bde550';
                }
                else if (item.purchaseOrderStatusID===4) {
                    //Finished
                    return '#fbd8d5';
                }
            },
            getTotalBalanceWithItem: function (item) {
                var totalBalance = 0;

                if (item.purchaseOrderTrackingDetail.length > 0) {
                    for (var i = 0; i < item.purchaseOrderTrackingDetail.length; i++) {
                        if (item.purchaseOrderTrackingDetail[i].balanceQnt !== null) {
                            totalBalance = totalBalance + item.purchaseOrderTrackingDetail[i].balanceQnt;
                        }
                    }
                }

                return totalBalance;
            },
            getTotalAmountWithItem: function (item) {
                var totalAmount = 0;

                if (item.purchaseOrderTrackingDetail.length > 0) {
                    for (var i = 0; i < item.purchaseOrderTrackingDetail.length; i++) {
                        if (item.purchaseOrderTrackingDetail[i].totalAmount !== null) {
                            totalAmount = totalAmount + item.purchaseOrderTrackingDetail[i].totalAmount;
                        }
                    }
                }

                return totalAmount;
            },

            getAllOrderWithItem: function (data) {
                var allOrder = 0;
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        allOrder += $scope.method.getTotalOrderWithItem(data[i]);
                    }
                }
                return allOrder;
            },
            getAllReceiveWithItem: function (data) {
                var allReceive = 0;
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        allReceive += $scope.method.getTotalReceiveWithItem(data[i]);
                    }
                }
                return allReceive;
            },
            getAllBalanceWithItem: function (data) {
                var allBalance = 0;
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        allBalance += $scope.method.getTotalBalanceWithItem(data[i]);
                    }
                }
                return allBalance;
            },
            getAllAmountWithItem: function (data) {
                var allAmount = 0;
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        allAmount += $scope.method.getTotalAmountWithItem(data[i]);
                    }
                }
                return allAmount;
            },

        };

        $timeout(function () {
            $scope.event.init();
        }, 0);
    };
})();