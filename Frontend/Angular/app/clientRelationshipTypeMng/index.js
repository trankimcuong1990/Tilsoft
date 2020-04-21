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

    angular.module("tilsoftApp", ["ngCookies", "avs-directives"]).controller("tilsoftController", ClientRelationshipTypeMngController);
    ClientRelationshipTypeMngController.$inject = ["$scope", "$cookieStore", "dataService"];

    function ClientRelationshipTypeMngController($scope, $cookieStore, clientRelationshipTypeMngService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            clientRelationshipTypeUD: "",
            clientRelationshipTypeNM: ""
        };

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    clientRelationshipTypeMngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.activepage(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                clientRelationshipTypeMngService.searchFilter.sortedDirection = sortedDirection;
                clientRelationshipTypeMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                clientRelationshipTypeMngService.searchFilter.sortedBy = sortedBy;
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
            updateorder: updateorder,
            deleterow: deleterow,
            changeorder: changeorder
        };

        $scope.event.activepage();

        function activepage(isLoadMore) {
            createservices();

            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            clientRelationshipTypeMngService.searchFilter.filters = $scope.filters;
            clientRelationshipTypeMngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    console.log($scope.data);
                    $scope.totalPage = Math.ceil(data.totalRows / clientRelationshipTypeMngService.searchFilter.pageSize);
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
            clientRelationshipTypeMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.activepage();
        };

        function deleterow(id, index) {
            clientRelationshipTypeMngService.delete(
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
            clientRelationshipTypeMngService.searchFilter.pageSize = context.pageSize;
            clientRelationshipTypeMngService.serviceUrl = context.serviceUrl;
            clientRelationshipTypeMngService.token = context.token;
        };

        function updateorder($event) {
            $event.preventDefault();

            clientRelationshipTypeMngService.updateorder(
                $scope.data,
                function (data) {
                    if (data.message.type === 0) {
                        window.location = context.refreshUrl;
                    }
                },
                function (error) {
                    jsHelper.showMessage("warning", error);
                });
        };

        function changeorder(item) {
            item.isChange = true;
        }
    };
})();