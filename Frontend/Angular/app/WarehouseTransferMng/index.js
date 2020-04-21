jQuery('.search-filter').keypress(
    function (event) {
        if (event.keyCode === 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.refresh();
        }
    }
);

(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', warehouseTransferControllerController);
    warehouseTransferControllerController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function warehouseTransferControllerController($scope, $cookieStore, warehouseTransferService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            receiptNo: '',
            deliveryNoteUD: '',
            receivingNoteUD: '',
            receiptDate: '',
            fromFactoryWarehouseID:null,
            toFactoryWarehouseID: null
        };
        //$scope.defaultFilter = angular.copy($scope.filters);
        //$scope.isDefaultFilter = true;


        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    warehouseTransferService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(false);
                    //$scope.event.activepage(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                warehouseTransferService.searchFilter.sortedDirection = sortedDirection;
                warehouseTransferService.searchFilter.pageIndex = $scope.pageIndex = 1;
                warehouseTransferService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
                //$scope.event.activepage();
            },
            isTriggered: false
        };
       
        $scope.event = {
            init: init,
            search: search,
            refresh: refresh,
            clear: clear,
            remove: remove,
        };

        function init() {
            warehouseTransferService.searchFilter.pageSize = context.pageSize;
            warehouseTransferService.serviceUrl = context.serviceUrl;
            warehouseTransferService.token = context.token;

            $scope.event.search();
        }

        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            warehouseTransferService.searchFilter.filters = $scope.filters;
            warehouseTransferService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);

                    $scope.totalPage = Math.ceil(data.totalRows / warehouseTransferService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    $scope.factoryWarehouses = data.data.factoryWarehouses;

                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }

                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );                    
        }
        
        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1
            warehouseTransferService.searchFilter.pageIndex = $scope.pageIndex = 1;

            $scope.event.search(false);

        }

        function clear() {
            $scope.filters = {
                receiptNo: '',
                deliveryNoteUD: '',
                receivingNoteUD: '',
                receiptDate: '',
                fromFactoryWarehouseNM: null,
                toFactoryWarehouseNM: null
            };
            $scope.event.refresh();
        }

        function remove(item) {
            warehouseTransferService.delete(
                  item.warehouseTransferID,
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
                      jsHelper.showMessage('warning', error);
                  }
              );


        }

        $scope.event.init();              
    };
})();






