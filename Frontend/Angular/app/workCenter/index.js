jQuery('.search-filter').keypress(
    function (event) {
        if (event.keyCode === 13) {
            var scope = angular.element(jQuery("body")).scope();
            
            scope.event.refreshWorkCenter();
        }
    }
);

(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', workCenterController);
    workCenterController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function workCenterController($scope, $cookieStore, workCenterService) {
        $scope.data = [];
        $scope.detailData = [];

        $scope.employees = [];

        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            workCenterUD: '',
            workCenterNM: '',
            operatingTime: '',
            defaultCost: '',
            capacity: '',
            responsibleBy: '',
            defaultFactoryWarehouseID: null,
        };

        $scope.event = {
            initWorkCenter: initWorkCenter,
            loadWorkCenter: loadWorkCenter,
            refreshWorkCenter: refreshWorkCenter,
            clearFiltersWorkCenter: clearFiltersWorkCenter,
            updateWorkCenter: updateWorkCenter,
            deleteWorkCenter: deleteWorkCenter,            
        };

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;

                    workCenterService.searchFilter.pageIndex = $scope.pageIndex;

                    loadWorkCenter(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                workCenterService.searchFilter.sortedBy = sortedBy;
                workCenterService.searchFilter.sortedDirection = sortedDirection;
                workCenterService.searchFilter.pageIndex = $scope.pageIndex = 1;

                loadWorkCenter();
            },
            isTriggered: false
        };

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        var cookieValue = $cookieStore.get(context.cookieID);
        if (cookieValue) {
            $scope.filters = cookieValue;
        };

        // Init page Work Center
        function initWorkCenter() {
            workCenterService.searchFilter.pageSize = context.pageSize;
            workCenterService.serviceUrl = context.serviceUrl;
            workCenterService.token = context.token;

            workCenterService.getInit(
                function (data) {
                    $scope.employees = data.data.data;
                    loadWorkCenter();
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.message.message);
                    jQuery('#content').show();
                }
            );
        };

        // Load page Work Center
        function loadWorkCenter(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            workCenterService.searchFilter.filters = $scope.filters;
            workCenterService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.detailData = data.data.detailData;

                    $scope.totalPage = Math.ceil(data.totalRows / workCenterService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    $scope.factoryWarehouses = data.data.factoryWarehouses;
                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }

                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.message.message);

                    $scope.data = [];

                    jQuery('#content').show();
                }
            );
        };

        // Refresh page Work Center
        function refreshWorkCenter() {
            $scope.data = [];

            workCenterService.searchFilter.pageIndex = $scope.pageIndex = 1;

            loadWorkCenter();
        };

        // Clear filters page Work Center
        function clearFiltersWorkCenter() {
            $scope.filters = {
                workCenterUD: '',
                workCenterNM: '',
                operatingTime: '',
                defaultCost: '',
                capacity: '',
                responsibleBy: ''
            };

            $scope.data = [];

            workCenterService.searchFilter.pageIndex = $scope.pageIndex = 1;

            loadWorkCenter();
        };

        // Update a Work Center
        function updateWorkCenter(workCenterID) {
            window.location = context.urlNewWorkCenter + workCenterID;
        };

        // Delete a Work Center
        function deleteWorkCenter(workCenter) {
            workCenterService.delete(
                workCenter.workCenterID,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);

                    if (data.message.type === 0) {
                        var index = $scope.data.indexOf(workCenter);

                        $scope.data.splice(index, 1);
                        $scope.totalRows--;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error.data.message.message);
                }
            );
        };

        initWorkCenter();
    }
})();