angular.module('cpApp').controller('searchResult', ['$scope', '$rootScope', '$cookies', '$timeout', 'dataService', function ($scope, $rootScope, $cookies, $timeout, dataService) {
    $scope.gridHandler = angular.extend(this, dataService.gridHandler);
    $scope.gridHandler.search = function (pageIndex, sortedBy, sortedDirection) {
        dataService.searchFilter.filters = $scope.$parent.data.filters;
        dataService.searchFilter.pageIndex = pageIndex;

        if (sortedDirection) dataService.searchFilter.sortedDirection = sortedDirection;
        if (sortedBy) dataService.searchFilter.sortedBy = sortedBy;
        dataService.search(
            function (data) {
                if (data.message.type === 0) {
                    if (pageIndex === 1) {
                        $scope.$parent.data.data.loadingPlans = [];
                        $scope.$parent.data.data.orders = [];
                        $scope.gridHandler.saveState(data.totalRows, dataService.searchFilter.pageSize);
                    }
                    Array.prototype.push.apply($scope.$parent.data.data.loadingPlans, data.data.data);
                    Array.prototype.push.apply($scope.$parent.data.data.orders, data.data.orderInfoDTOs);
                }
                else {
                    console.log(data.message);
                    $scope.gridHandler.saveState(0, dataService.searchFilter.pageSize);
                }
            },
            function (error) {
                $scope.data = null;
                $scope.gridHandler.saveState(0, dataService.searchFilter.pageSize);
            }
        );
    };

    $scope.event = {
        goToFilter: function () {
            $rootScope.$emit('frmFilter');
        }
    };

    $rootScope.$on('frmResult', function (event, data) {
        jsHelper.showForm('frmResult');
    });
    $rootScope.$on('frmResult_reload', function (event, data) {
        jsHelper.showForm('frmResult');
        $scope.gridHandler.search(1, null, null);
    });

    $timeout(function () {
        $scope.gridHandler.search(1, null, null);
    }, 0);
}]);