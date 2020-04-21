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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', receivingNoteDefaultController);
    receivingNoteDefaultController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function receivingNoteDefaultController($scope, $cookieStore, receivingNoteDefaultService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            receivingNoteUD: '',
            receivingNoteDate: '',
            workOrderUD: '',
            saleOrderNo: '',
            receivingNoteTypeID:1
        };

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    receivingNoteDefaultService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.activepage(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                receivingNoteDefaultService.searchFilter.sortedDirection = sortedDirection;
                receivingNoteDefaultService.searchFilter.pageIndex = $scope.pageIndex = 1;
                receivingNoteDefaultService.searchFilter.sortedBy = sortedBy;
                $scope.event.activepage();
            },
            isTriggered: false
        };
        //===For event ====>
        $scope.event = {
            init: init,
            search: search,
            refresh: refresh,
            clear: clear,
            remove: remove,
        };

        //===End Event===>

        function init() {
            receivingNoteDefaultService.searchFilter.pageSize = context.pageSize;
            receivingNoteDefaultService.serviceUrl = context.serviceUrl;
            receivingNoteDefaultService.token = context.token;

            $scope.event.search();
        }

         //Load Data====>
        function search(isLoadMore) {
            //jQuery('#content').show();
            //============================================================//

            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            receivingNoteDefaultService.searchFilter.filters = $scope.filters;
            receivingNoteDefaultService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);

                    $scope.totalPage = Math.ceil(data.totalRows / receivingNoteDefaultService.searchFilter.pageSize);
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
            
        //===============================================================//
        }

        //===================End Load Data================================================================<

        //refresh page========>
        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1
            receivingNoteDefaultService.searchFilter.pageIndex = $scope.pageIndex = 1;

            $scope.event.search(false);

        }
        //===============================End Refresh Page=================================================<



        //clear Search=========>
        function clear() {
            $scope.filters = {
                receivingNoteUD: '',
                receivingNoteDate: '',
                workOrderUD: '',
                saleOrderNo: '',
                receivingNoteTypeID: 1
            };
            $scope.event.refresh();

        }
        //====================================End Clear Search===============================================<


         //remove data==========>
        function remove(item) {
            receivingNoteDefaultService.delete(
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


        }

        $scope.event.init();


           

        function createservices() {
            receivingNoteDefaultService.searchFilter.pageSize = context.pageSize;
            receivingNoteDefaultService.serviceUrl = context.serviceUrl;
            receivingNoteDefaultService.token = context.token;
        };
    };
})();






