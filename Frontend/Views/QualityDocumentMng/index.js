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
    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', qualityDocumentMngController);
    qualityDocumentMngController.$inject = ['$scope', '$cookieStore', 'dataService']

    function qualityDocumentMngController($scope, $cookieStore, dataService) {

        //Varible Zone
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        //Filters
        $scope.filters = {
            qualityDocumentUD: '',
            description: '',
            issuedDate: '',
            friendlyName: ''
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
            init: init,
            clear: clear,
            refresh: refresh,
            remove: remove
        };
        ////init
        function init() {
            dataService.searchFilter.pageSize = context.pageSize;
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;

            $scope.event.search();
        }
        //Load Data
        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.gridHandler.isTriggered = false;
            dataService.searchFilter.filters = $scope.filters;

            dataService.search(
                //jQuery("#content").show(),
                function (data) {
                    debugger;
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
                qualityDocumentUD: '',
                description: '',
                issuedDate: '',
                friendlyName: ''
            };
            $scope.event.refresh();
        }

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.search();

        }

        //Event Remove
        function remove(item) {
            dataService.delete(
                  item.qualityDocumentID,
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
                     
                  }
              );
        };
        //Event load
        $scope.event.init();

        //Creat Service
        function createservices() {
            dataService.searchFilter.pageSize = context.pageSize;
            dataService.searchFilter.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
        }

    }

})();