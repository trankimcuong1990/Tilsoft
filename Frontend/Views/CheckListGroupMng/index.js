jQuery(".search-filter").keypress(
    function (event) {
        if (event.keyCode === 13) {
            var scope = angular.element(jQuery("body")).scope();
            scope.event.reloadpage();
        }
    }
);

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

(function () {
    "use strict";

    angular.module("tilsoftApp", ["ngCookies", "avs-directives"]).controller("tilsoftController", UnitController);
    UnitController.$inject = ["$scope", "$cookieStore", "dataService"];

    function UnitController($scope, $cookieStore, unitService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            checkListGroupNM: "",
            checkListGroupUD: ""
        };

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    unitService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.activepage(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                unitService.searchFilter.sortedDirection = sortedDirection;
                unitService.searchFilter.pageIndex = $scope.pageIndex = 1;
                unitService.searchFilter.sortedBy = sortedBy;
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
            deleterow: deleterow,
            clearFilters: clearFilters
        };

        $scope.event.activepage();

        function activepage(isLoadMore) {
            createservices();

            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            unitService.searchFilter.filters = $scope.filters;
            unitService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    console.log($scope.data);
                    $scope.totalPage = Math.ceil(data.totalRows / unitService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

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
            unitService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.activepage();
        };

        function deleterow(id, index) {
            unitService.delete(
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

        function clearFilters() {
            $scope.filters = {
                checkListGroupNM: '',
                checkListGroupUD: ''
            };
            reloadpage();
        };

        function createservices() {
            unitService.searchFilter.pageSize = context.pageSize;
            unitService.serviceUrl = context.serviceUrl;
            unitService.token = context.token;
        };
    };
})();