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

    angular.module("tilsoftApp", ["ngCookies", "avs-directives"]).controller("tilsoftController", additionalConditionController);
    additionalConditionController.$inject = ["$scope", "$cookieStore", "dataService"];

    function additionalConditionController($scope, $cookieStore, additionService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            AdditionalConditionNM: ""
            
        };

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    additionService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.activepage(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                additionService.searchFilter.sortedDirection = sortedDirection;
                additionService.searchFilter.pageIndex = $scope.pageIndex = 1;
                additionService.searchFilter.sortedBy = sortedBy;
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
            clearFilters: clearFilters,
            getInit: getInit
        };

        $scope.event.activepage();
        $scope.event.getInit();

        function activepage(isLoadMore) {
            createservices();
            debugger;
            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            additionService.searchFilter.filters = $scope.filters;
            additionService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.additionalConditionSearch);
                    console.log($scope.data);
                    debugger;
                    $scope.totalPage = Math.ceil(data.totalRows / additionService.searchFilter.pageSize);
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
            additionService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.activepage();
        };

        function deleterow(id, index) {
            additionService.delete(
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
                AdditionalConditionNM: ''
            };
            reloadpage();
        };

        function createservices() {
            additionService.searchFilter.pageSize = context.pageSize;
            additionService.serviceUrl = context.serviceUrl;
            additionService.token = context.token;
        };
        function getInit() {
            createservices();
            additionService.getInit(
                function (data) {

                    $scope.additionalConditionTypeDTO = data.data.additionalConditionTypeDTO;

                    jQuery("#content").show();

                },
                function (error) {
                    //alert("444")
                    //jsHelper.showMessage("warning", error);
                    $scope.additionalConditionTypeDTO = [];
                });


        };
    };
})();