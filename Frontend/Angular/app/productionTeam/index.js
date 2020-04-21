jQuery(".search-filter").keypress(
    function (event) {
        if (event.keyCode === 13) {
            var scope = angular.element(jQuery("body")).scope();
            scope.event.reloadpage();
        }
    }
);

(function () {
    "use strict";

    angular.module("tilsoftApp", ["ngCookies", "avs-directives"]).controller("tilsoftController", productionTeamController);
    productionTeamController.$inject = ["$scope", "$cookieStore", "dataService"];

    function productionTeamController($scope, $cookieStore, productionTeamService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            ProductionTeamUD: "",
            ProductionTeamNM: "",
            WorkCenterID: null,
        };

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    productionTeamService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.activepage(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                productionTeamService.searchFilter.sortedDirection = sortedDirection;
                productionTeamService.searchFilter.pageIndex = $scope.pageIndex = 1;
                productionTeamService.searchFilter.sortedBy = sortedBy;
                $scope.event.activepage();
            },
            isTriggered: false
        };

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        var cookieValue = $cookieStore.get(context.cookieID);
        if (cookieValue) {
            $scope.filters = cookieValue;
        }

        $scope.event = {
            activepage: activepage,
            reloadpage: reloadpage,
            deleterow: deleterow
        };

        $scope.event.activepage();

        function activepage(isLoadMore) {
            createservices();

            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            productionTeamService.searchFilter.filters = $scope.filters;
            productionTeamService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / productionTeamService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    $scope.workCenters = data.data.workCenters;
                    jQuery("#content").show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    jsHelper.showMessage("warning", error);
                }
            );
        };

        function reloadpage() {
            $scope.data = [];
            productionTeamService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.activepage();
        };

        function deleterow(id, index) {
            productionTeamService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        if (index >= 0) {
                            $scope.data.splice(index, 1);
                            $scope.totalRows--;
                        }
                    }
                },
                function (error) {
                    jsHelper.showMessage("warning", error);
                });
        };

        function createservices() {
            productionTeamService.searchFilter.pageSize = context.pageSize;
            productionTeamService.serviceUrl = context.serviceUrl;
            productionTeamService.token = context.token;
        };
    };
})();