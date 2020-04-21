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

    angular.module("tilsoftApp", ["ngCookies", "avs-directives"]).controller("tilsoftController", complianceCertificateTypeController);
    complianceCertificateTypeController.$inject = ["$scope", "$cookieStore", "dataService"];

    function complianceCertificateTypeController($scope, $cookieStore, complianceService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            complianceCertificateTypeNM: "",
            complianceCertificateTypeUD: ""
        };

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    complianceService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.activepage(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                complianceService.searchFilter.sortedDirection = sortedDirection;
                complianceService.searchFilter.pageIndex = $scope.pageIndex = 1;
                complianceService.searchFilter.sortedBy = sortedBy;
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
            // getInit: getInit
        };

        $scope.event.activepage();
        //$scope.event.getInit();

        function activepage(isLoadMore) {
            createservices();

            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            complianceService.searchFilter.filters = $scope.filters;
            complianceService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    console.log($scope.data);
                    $scope.totalPage = Math.ceil(data.totalRows / complianceService.searchFilter.pageSize);
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
            complianceService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.activepage();
        };

        function deleterow(id, index) {
            complianceService.delete(
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
                complianceCertificateTypeNM: '',
                complianceCertificateTypeUD: ''
            };
            reloadpage();
        };

        function createservices() {
            complianceService.searchFilter.pageSize = context.pageSize;
            complianceService.serviceUrl = context.serviceUrl;
            complianceService.token = context.token;
        };
        //function getInit() {
        //    createservices();

        //    complianceService.getInit(
        //        function (data) {

        //            $scope.checkListGroups = data.data.checkListGroups;

        //            jQuery("#content").show();

        //        },
        //        function (error) {
        //            //alert("444")
        //            //jsHelper.showMessage("warning", error);
        //            $scope.checkListGroups = [];
        //        });


        //};
    };
})();