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

    //angular.module('tilsoftApp', ['ngCookies', 'avs-directives', 'furnindo-directive']).controller('tilsoftController', serviceController);
    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', serviceController);
    serviceController.$inject = ['$scope', '$cookieStore', '$timeout', 'dataService'];

    function serviceController($scope, $cookieStore, $timeout, dataService) {
        //
        // grid handler
        //
        $scope.gridHandler = {
            loadMore: function () {
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                dataService.searchFilter.sortedDirection = sortedDirection;
                $scope.pageIndex = 1;
                dataService.searchFilter.pageIndex = scope.pageIndex;
                dataService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };
        $scope.nullHandler = {
            loadMore: function () {
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.$apply(function () {
                    $scope.orderByColumn = (sortedDirection === 'desc' ? '-' : '') + sortedBy;
                });
            },
            isTriggered: false
        };

        $scope.data = [];
        $scope.pageIndex = 1;
        $scope.totalPage = 0;
        $scope.totalRows = 0;

        $scope.insertData = [];

        $scope.support = {
            supportWorkCenterDTOs: [],
            supportWorkOrderStatusDTOs: []
        };

        $scope.filters = {
            workCenterID:''
        };

        //$scope.supportWorkCenterDTOs = {
        //    param: '',
        //    data: {
        //        id: null,
        //        label: '',
        //        description: ''
        //    },
        //    api: {
        //        url: context.serviceUrl + 'getWorkOrder',
        //        token: context.token
        //    }
        //};

        $scope.event = {
            init: function () {
                dataService.serviceUrl = context.serviceUrl;
                dataService.token = context.token;
                dataService.searchFilter.pageSize = context.pageSize;

                dataService.getInit(
                    function (data) {
                        $scope.support.supportWorkCenterDTOs = data.data.supportWorkCenterDTOs;
                        $scope.support.supportWorkOrderStatusDTOs = data.data.supportWorkOrderStatusDTOs;              

                        jQuery('#content').show();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            },
            search: function () {
                if ($scope.filters.workCenterID === '') {
                    jsHelper.showMessage('warning', 'Plz choose workcenter ');
                    return;
                }
                $scope.gridHandler.isTriggered = false;
                dataService.searchFilter.filters = $scope.filters;               
                dataService.search(
                    function (data) {
                        $scope.data = data.data.workOrderDTOs;
                        $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;
                        jQuery('#content').show();

                        //if (!isLoadMore) {
                        //    $scope.gridHandler.goTop();
                        //}
                        //$scope.gridHandler.isTriggered = true;

                        $timeout(function () {
                            $scope.$apply(function () {
                                $scope.orderByColumn = (dataService.searchFilter.sortedDirection === 'desc' ? '-' : '') + dataService.searchFilter.sortedBy;
                            });
                        }, 100);
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.data = null;
                        $scope.pageIndex = 1;
                        $scope.totalPage = 0;
                        $scope.totalRows = 0;
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
            }
        };       

        $timeout(function () {
            $scope.event.init();
        }, 0);
    }
})();