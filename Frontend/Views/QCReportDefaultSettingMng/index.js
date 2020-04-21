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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', qcReportDefaultSettingController);
    qcReportDefaultSettingController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function qcReportDefaultSettingController($scope, $cookieStore, qcReportDefaultSettingService) {
        // variable
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        //Filters
        $scope.filters = {                      
            QCReportSection: '',
            Description: ''
        };

        // grid handler
        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    qcReportDefaultSettingService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                qcReportDefaultSettingService.searchFilter.sortedDirection = sortedDirection;
                qcReportDefaultSettingService.searchFilter.pageIndex = $scope.pageIndex = 1;
                qcReportDefaultSettingService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };

        // cookie store

        // event
        $scope.event = {
            //init: init,
            search: search,
            clear: clear,
            refresh: refresh,
            remove: remove

        }
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        //function init() {
        //    jQuery('#content').show();
        //}

        function search(isLoadMore) {
            qcReportDefaultSettingService.searchFilter.pageSize = context.pageSize;
            qcReportDefaultSettingService.serviceUrl = context.serviceUrl;
            qcReportDefaultSettingService.token = context.token;

            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.gridHandler.isTriggered = false;
            qcReportDefaultSettingService.searchFilter.filters = $scope.filters;
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            qcReportDefaultSettingService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / qcReportDefaultSettingService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {

                }
                );
        };

        //Event Clear
        function clear() {
            $scope.filters = {
                QCReportSection: '',
                Description: ''
            };
            $scope.event.refresh();
        };

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;
            qcReportDefaultSettingService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.search();

        };

        function remove(item) {
            qcReportDefaultSettingService.delete(
                  item.qcReportDefaultSettingID,
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

        // default event
        $scope.event.search();
    }
})();