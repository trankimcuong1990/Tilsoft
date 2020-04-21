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

    angular.module("tilsoftApp", ["ngCookies", "avs-directives"]).controller("tilsoftController", CostInvoice2TypeMngController);
    CostInvoice2TypeMngController.$inject = ["$scope", "$cookieStore", "dataService"];

    function CostInvoice2TypeMngController($scope, $cookieStore, costInvoice2TypeMngService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {          
            costInvoice2TypeNM: ""
        };

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    costInvoice2TypeMngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.activepage(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                costInvoice2TypeMngService.searchFilter.sortedDirection = sortedDirection;
                costInvoice2TypeMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                costInvoice2TypeMngService.searchFilter.sortedBy = sortedBy;
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

            costInvoice2TypeMngService.searchFilter.filters = $scope.filters;
            costInvoice2TypeMngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    console.log($scope.data);
                    $scope.totalPage = Math.ceil(data.totalRows / costInvoice2TypeMngService.searchFilter.pageSize);
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
            costInvoice2TypeMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.activepage();
        };

        function deleterow(id, index) {
            costInvoice2TypeMngService.delete(
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
            costInvoice2TypeMngService.searchFilter.pageSize = context.pageSize;
            costInvoice2TypeMngService.serviceUrl = context.serviceUrl;
            costInvoice2TypeMngService.token = context.token;
        };
    };
})();
//jQuery("#content").show();