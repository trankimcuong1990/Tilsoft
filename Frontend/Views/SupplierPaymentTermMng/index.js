(function () {
    'use strict';
    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', supplierPaymentTermMngController);
    supplierPaymentTermMngController.$inject = ['$scope', '$cookieStore', 'dataService']

    function supplierPaymentTermMngController($scope, $cookieStore, supplierPaymentTermMngService) {

        //Varible Zone
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        //GridHandler
        $scope.gridHandler = {
            loadMore: function () {
                //if ($scope.pageIndex < $scope.totalPage) {
                //    $scope.pageIndex++;
                //    supplierPaymentTermMngService.searchFilter.pageIndex = $scope.pageIndex;
                //    $scope.event.search(true);
                //}
            },
            sort: function (sortedBy, sortedDirection) {
                //$scope.data = [];
                //supplierPaymentTermMngService.searchFilter.sortedDirection = sortedDirection;
                //supplierPaymentTermMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                //supplierPaymentTermMngService.searchFilter.sortedBy = sortedBy;
                //$scope.event.search();
            },
            isTriggered: false
        };
        //event
        $scope.event = {
            search: search,
            //clear: clear,
            //refresh: refresh,
            remove: remove
        };

        //Load Data
        function search(isLoadMore) {
            supplierPaymentTermMngService.searchFilter.pageSize = context.pageSize;
            supplierPaymentTermMngService.serviceUrl = context.serviceUrl;
            supplierPaymentTermMngService.token = context.token;

            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.gridHandler.isTriggered = false;
            //supplierPaymentTermMngService.searchFilter.filters = $scope.filters;

            supplierPaymentTermMngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    //$scope.totalPage = Math.ceil(data.totalRows / supplierPaymentTermMngService.searchFilter.pageSize);
                    //$scope.totalRows = data.totalRows;
                    $scope.data = data.data.data;
                    jQuery("#content").show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {

                }
            );
        }

        function remove(item) {
            supplierPaymentTermMngService.delete(
                item.supplierPaymentTermID,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);

                    if (data.message.type === 0) {
                        var index = $scope.data.indexOf(item);

                        $scope.data.splice(index, 1);
                        $scope.totalRows = $scope.totalRows - 1;
                    }
                },
                function (error) {
                    //not thing to do
                }
            );
        };
        //Event load
        $scope.event.search();

    }

})();