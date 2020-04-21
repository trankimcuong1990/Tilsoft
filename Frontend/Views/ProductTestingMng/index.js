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
    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', productTestingMngController);
    productTestingMngController.$inject = ['$scope', '$cookieStore', 'dataService']

    function productTestingMngController($scope, $cookieStore, dataService) {

        //Varible Zone
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        //Filters
        $scope.filters = {
            productTestUD: '',
            modelUD: '',
            modelNM: '',
            clientUD: '',
            friendlyName: '',
            TestStandardNM: '',
            testDate: ''
        };
        //GridHandler
        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    dataService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                dataService.searchFilter.sortedDirection = sortedDirection;
                dataService.searchFilter.pageIndex = $scope.pageIndex = 1;
                dataService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };
        //event
        $scope.event = {
            search: search,
            clear: clear,
            refresh: refresh,
            remove: remove
        };

        //Load Data
        function search(isLoadMore) {
            dataService.searchFilter.pageSize = context.pageSize;
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;

            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.gridHandler.isTriggered = false;
            dataService.searchFilter.filters = $scope.filters;

            dataService.search(
                function (data) {                   
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

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

        //Event Clear
        function clear() {
            $scope.filters = {
                productTestUD: '',
                modelUD: '',
                modelNM: '',
                clientUD: '',
                friendlyName: '',
                TestStandardNM: '',
                testDate: ''
            };
            $scope.event.refresh();
        }

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.search();

        }

        function remove(item) {
            dataService.delete(
                  item.productTestID,
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

        //Creat Service
        function createservices() {
            dataService.searchFilter.pageSize = context.pageSize;
            dataService.searchFilter.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
        }

    }

})();