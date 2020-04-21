//jQuery('.search-filter').keypress(
//    function (event) {
//        if (event.keyCode === 13) {
//            var scope = angular.element(jQuery('body')).scope();
//            scope.event.reloadpage();
//        }
//    }
//);

(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', receivingNoteRepairAssemblyController);
    receivingNoteRepairAssemblyController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function receivingNoteRepairAssemblyController($scope, $cookieStore, receivingNoteRepairAssemblyService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            receivingNoteUD: '',
            receivingNoteDate: '',
            workOrderUD: '',
            saleOrderNo: '',
            receivingNoteTypeID: 2
        };

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    receivingNoteRepairAssemblyService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.activepage(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                receivingNoteRepairAssemblyService.searchFilter.sortedDirection = sortedDirection;
                receivingNoteRepairAssemblyService.searchFilter.pageIndex = $scope.pageIndex = 1;
                receivingNoteRepairAssemblyService.searchFilter.sortedBy = sortedBy;
                $scope.event.activepage();
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
            receivingNoteRepairAssemblyService.searchFilter.pageSize = context.pageSize;
            receivingNoteRepairAssemblyService.serviceUrl = context.serviceUrl;
            receivingNoteRepairAssemblyService.token = context.token;

            $scope.event.search(false);
        };

        function search(isLoadMore) {
            jQuery('#content').show();

            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            receivingNoteRepairAssemblyService.searchFilter.filters = $scope.filters;
            receivingNoteRepairAssemblyService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);

                    $scope.totalPage = Math.ceil(data.totalRows / receivingNoteRepairAssemblyService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows; debugger;

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
            receivingNoteRepairAssemblyService.searchFilter.pageIndex = $scope.pageIndex = 1;

            $scope.event.search(false);

        }

        function clear() {

            $scope.filters = {
                receivingNoteUD: '',
                receivingNoteDate: '',
                workOrderUD: '',
                saleOrderNo: ''

            };
            $scope.event.refresh();

        }

        function remove(item) {
            receivingNoteRepairAssemblyService.delete(
                  item.receivingNoteID,
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

        };

        $scope.event.init();


        function createservices() {
            receivingNoteRepairAssemblyService.searchFilter.pageSize = context.pageSize;
            receivingNoteRepairAssemblyService.serviceUrl = context.serviceUrl;
            receivingNoteRepairAssemblyService.token = context.token;
        };
    };
})();






