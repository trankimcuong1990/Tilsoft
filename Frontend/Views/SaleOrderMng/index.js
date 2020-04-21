jQuery('.search-filter').keypress(
    function (event) {
        if (event.keyCode === 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.refresh();
        }
    }
);

var saleOrderDetailGrid = jQuery('#saleOrderDetail').scrollableTable(null, function (sortedBy, sortedDirection) {
    var scope = angular.element(jQuery('body')).scope();
    datasource = scope.saleorder.saleOrderData.saleOrderDetails;

    if (sortedDirection == 'asc') {
        datasource.sort(function (a, b) {
            return a[sortedBy] > b[sortedBy] ? 1 : -1;
        });
    }
    else if (sortedDirection == 'desc') {
        datasource.sort(function (a, b) {
            return a[sortedBy] < b[sortedBy] ? 1 : -1;
        });
    }
    scope.$apply();
});

var saleOrderDetailSparepartGrid = jQuery('#saleOrderDetailSparepart').scrollableTable(null, function (sortedBy, sortedDirection) {
    var scope = angular.element(jQuery('body')).scope();
    datasource = scope.saleorder.saleOrderData.saleOrderDetailSpareparts;
    if (sortedDirection == 'asc') {
        datasource.sort(function (a, b) {
            return a[sortedBy] > b[sortedBy] ? 1 : -1;
        });
    }
    else if (sortedDirection == 'desc') {
        datasource.sort(function (a, b) {
            return a[sortedBy] < b[sortedBy] ? 1 : -1;
        });
    }
    scope.$apply();
});

(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', saleOrderMngController);

    saleOrderMngController.$inject = ['$scope', '$cookieStore', 'dataService'];
    function saleOrderMngController($scope, $cookieStore, saleOrderMngService, saleOrderDetail_Filter) {
        $scope.data = [];
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;

        $scope.seasons = jsHelper.getSeasons();

        $scope.filters = {
            Season: '',
            ClientUD: '',
            ClientNM: '',
            PINo: ''

        };
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;


        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    saleOrderMngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                saleOrderMngService.searchFilter.sortedDirection = sortedDirection;
                saleOrderMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                saleOrderMngService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };

        $scope.event = {
            init: init,
            search: search,
            refresh: refresh,
            clear: clear,
            //remove: remove
        };

        function init() {
            saleOrderMngService.searchFilter.pageSize = context.pageSize;
            saleOrderMngService.serviceUrl = context.serviceUrl;
            saleOrderMngService.token = context.token;
            $scope.event.search();
        };

        function search(isLoadMore) {
            jQuery('#content').show();
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            saleOrderMngService.searchFilter.filters = $scope.filters;
            saleOrderMngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / saleOrderMngService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }

                    $scope.gridHandler.isTriggered = true;
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;

            saleOrderMngService.searchFilter.pageIndex = 1;

            $scope.event.search(true);
        };

        function clear() {
            $scope.filters = {
                Season: '',
                ClientUD: '',
                ClientNM: '',
                PINo: ''
            };
            $scope.event.refresh();

        }

        //function remove(item) {
        //    saleOrderService.delete(
        //        item.transportationServiceID,
        //        null,
        //        function (data) {
        //            jsHelper.processMessage(data.message);

        //            if (data.message.type === 0) {
        //                var index = $scope.data.indexOf(item);

        //                $scope.data.splice(index, 1);
        //                $scope.totalRows = $scope.totalRows - 1;
        //            }
        //        },
        //        function (error) {
        //            jsHelper.showMessage('warning', error);
        //        }
        //    );
        //};

        $scope.event.init();
    };
})();
