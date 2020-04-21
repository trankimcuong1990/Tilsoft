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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', annualLeaveController);
    annualLeaveController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function annualLeaveController($scope, $cookieStore, annualLeaveService) {
        $scope.data = [];     

        $scope.employees = [];
        $scope.companies = [];
        $scope.status = [];

        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            employeeNM: null,
            companyNM: null,
            fromDate: null,
            toDate: null,   
            days: null,   
            statusID: null,
            statusUpdatedBy: null,
            HasLeft:'false'
        };

        $scope.event = {
            initWorkCenter: initWorkCenter,
            loadWorkCenter: loadWorkCenter,
            refreshWorkCenter: refreshWorkCenter,
            clearFiltersWorkCenter: clearFiltersWorkCenter,
            updateWorkCenter: updateWorkCenter,
            deleteWorkCenter: deleteWorkCenter          
        };

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;

                    annualLeaveService.searchFilter.pageIndex = $scope.pageIndex;

                    loadWorkCenter(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {              
                $scope.data = []; 
                annualLeaveService.searchFilter.sortedBy = sortedBy;
                annualLeaveService.searchFilter.sortedDirection = sortedDirection;
                annualLeaveService.searchFilter.pageIndex = $scope.pageIndex = 1;
                $scope.event.loadWorkCenter();
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
            annualLeaveService.searchFilter.pageSize = context.pageSize;
            annualLeaveService.serviceUrl = context.serviceUrl;
            annualLeaveService.token = context.token;
       
            annualLeaveService.getInit(
                function (data) {
                    $scope.employees = data.data.employeeDTOs;
                    $scope.status = data.data.annualLeaveStatusDTOs;
                    $scope.companies = data.data.companyDTOs;
                    
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

            annualLeaveService.searchFilter.filters = $scope.filters;
            annualLeaveService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                 
                    $scope.totalPage = Math.ceil(data.totalRows / annualLeaveService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    //$scope.factoryWarehouses = data.data.factoryWarehouses;
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

            annualLeaveService.searchFilter.pageIndex = $scope.pageIndex = 1;

            loadWorkCenter();
        };

        // Clear filters page Work Center
        function clearFiltersWorkCenter() {
            $scope.filters = {
                employeeNM: null,
                companyNM: null,
                fromDate: null,
                toDate: null,
                days: null,
                statusID: null,
                statusUpdatedBy: null,
                HasLeft:'false'
            };
            
            $scope.data = [];

            annualLeaveService.searchFilter.pageIndex = $scope.pageIndex = 1;

            loadWorkCenter();
        };

        // Update a Work Center
        function updateWorkCenter(workCenterID) {
            window.location = context.urlNewWorkCenter + workCenterID;
        };

        // Delete a Work Center
        function deleteWorkCenter(workCenter) {
            annualLeaveService.delete(
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