function formatWorkOrder(data) {
    return $.map(data.data, function (item) {
        if (item !== null) {
            return {
                id: item.id,
                label: item.value,
                description: ''
            }
        }
    });
};

(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', productionIssueOverviewController);
    productionIssueOverviewController.$inject = ['$scope', '$cookieStore', '$timeout', 'dataService'];

    function productionIssueOverviewController($scope, $cookieStore, $timeout, productionIssueOverviewService) {
        $scope.data = {
            data: [],
            history: [],
            totalRows: 0,
            totalPage: 0,
            pageIndex: 1
        };

        $scope.insertData = [];

        $scope.support = {
            workCenter: [],
            productionTeam: [],
            fromFactoryWarehouse: []
        };

        $scope.filters = {
            workOrderID: '',
            workCenterID: '',
            productionTeamID: ''
        };

        $scope.workOrder = {
            param: '',
            data: {
                id: null,
                label: '',
                description: ''
            },
            api: {
                url: context.serviceUrl + 'getWorkOrder',
                token: context.token
            }
        };

        $scope.event = {
            init: function () {
                productionIssueOverviewService.serviceUrl = context.serviceUrl;
                productionIssueOverviewService.token = context.token;
                productionIssueOverviewService.searchFilter.pageSize = context.pageSize;

                productionIssueOverviewService.getInit(
                    function (data) {
                        $scope.support.workCenter = data.data.supportWorkCenter;
                        $scope.support.productionTeam = data.data.supportProductionTeam;
                        $scope.support.fromFactoryWarehouse = data.data.supportFactoryWarehouse;

                        jQuery('#content').show();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            },
            search: function () {
                productionIssueOverviewService.searchFilter.filters = $scope.filters;

                productionIssueOverviewService.search(
                    function (data) {
                        if (data.message.type === 0) {
                            $scope.data.data = data.data.productionIssue;
                            $scope.data.totalRows = data.totalRows;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            },
            update: function () {
                $scope.insertData = [];

                if ($scope.filters.productionTeamID != null && $scope.filters.productionTeamID != '') {
                    angular.forEach($scope.data.data, function (item) {
                        angular.forEach(item.productionIssueDetail, function (itemDetail) {
                            if (itemDetail.issueQuantity != null && item.issueQuantity != '') {
                                item.toProductionTeamID = $scope.filters.productionTeamID;
                                $scope.insertData.push(item);
                            }
                        });
                    });
                }

                productionIssueOverviewService.update(
                    0,
                    $scope.insertData,
                    function (data) {
                        $timeout(function () {
                            $scope.event.search()
                        }, 0);
                    },
                    function (error) {
                        var errorMsg = error.data.message;
                        if (error.data.message.message != null && error.data.message.message != '') {
                            errorMsg = error.data.message.message;
                        }
                        jsHelper.showMessage('warning', errorMsg);
                    });
            },
            selectWorkOrder: function (item) {
                if (item !== null) {
                    $scope.filters.workOrderID = item.id;
                    jQuery('#workOrder').blur();
                }
            },
            changeWorkOrder: function () {
                if ($scope.filters.workOrderID !== null || $scope.filters.workOrderID !== '') {
                    $scope.filters.workOrderID = '';
                }
            },
            setProductionTeam: function (productionTeamID) {
                angular.forEach($scope.data.data, function (item) {
                    angular.forEach(item.productionIssueDetail, function (detailItem) {
                        if (detailItem.issueQuantity != null && detailItem.issueQuantity != '') {
                            detailItem.toProductionTeamID = $scope.filters.productionTeamID;
                        }
                    })
                })
            }
        };

        $scope.method = {
            getTotalHistoryQnt: function (productionIssueDetailHistory) {
                var totalQuantity = 0;
                angular.forEach(productionIssueDetailHistory, function (historyDetail) {
                    totalQuantity += historyDetail.historyQnt;
                });
                return totalQuantity;
            },
            getRemainQnt: function (normQnt, productionIssueDetailHistory) {
                return parseFloat(normQnt) - $scope.method.getTotalHistoryQnt(productionIssueDetailHistory);
            },
            checkValidQuantity: function (detailItem) {
                if (detailItem.issueQuantity != null && detailItem.issueQuantity != '') {
                    var remainQuantity = $scope.method.getRemainQnt(detailItem.normQuantity, detailItem.productionIssueDetailHistory);
                    if (remainQuantity - parseFloat(detailItem.issueQuantity) < 0) {
                        jsHelper.showMessage('warning', '<b>Issue Quantity</b> is larger than <b>Remain Quantity</b>');
                        return false;
                    }
                }
            }
        };

        $timeout(function () {
            $scope.event.init();
        }, 0);
    }
})();