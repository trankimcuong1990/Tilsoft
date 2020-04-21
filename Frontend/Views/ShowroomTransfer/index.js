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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', showroomTransferController);
    showroomTransferController.$inject = ['$scope', '$cookieStore', 'dataService', '$timeout'];

    function showroomTransferController($scope, $cookieStore, showroomTransferService, $timeout) {
        $scope.data = [];
        $scope.factoryWarehouses = [];

        $scope.filters = {
            showroomTransferUD: '',
            showroomTransferDate: '',
            fromWarehouseID: '',
            toWarehouseID: ''
        };

        //$scope.gridHandler = {
        //    loadMore: function () {
        //        if ($scope.pageIndex < $scope.totalPage) {
        //            $scope.pageIndex++;
        //            showroomTransferService.searchFilter.pageIndex = $scope.pageIndex;
        //            $scope.event.activepage(true);
        //        }
        //    },
        //    sort: function (sortedBy, sortedDirection) {
        //        $scope.data = [];
        //        showroomTransferService.searchFilter.sortedDirection = sortedDirection;
        //        showroomTransferService.searchFilter.pageIndex = $scope.pageIndex = 1;
        //        showroomTransferService.searchFilter.sortedBy = sortedBy;
        //        $scope.event.activepage();
        //    },
        //    isTriggered: false
        //};

        $scope.event = {
            init: function init() {
                showroomTransferService.searchFilter.pageSize = context.pageSize;
                showroomTransferService.serviceUrl = context.serviceUrl;
                showroomTransferService.token = context.token;
                $scope.event.getInitData();
                //jQuery('#content').show();
            },
            edit: function edit(id, $event) {
                $event.preventDefault();

                if (id !== null) {
                    window.open(context.editUrl + id, '_blank');
                }
            },

            getInitData: function () {
                showroomTransferService.getInitData(
                    function (data) {
                        $scope.factoryWarehouses = data.data.factoryWarehouses;

                        $scope.event.search();
                    },
                    function (error) {
                        //do nothing
                    },
                );
            },

            search: function () {
                $cookieStore.put(context.cookieID, $scope.filters);
                $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

                showroomTransferService.searchFilter.filters = $scope.filters;
                showroomTransferService.search(
                    function (data) {
                        $scope.data = data.data.data;
                        $scope.totalPage = Math.ceil(data.totalRows / showroomTransferService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;

                        jQuery('#content').show();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },

            refresh: function () {
                $scope.data = [];
                $scope.pageIndex = 1;
                showroomTransferService.searchFilter.pageIndex = $scope.pageIndex = 1;

                $scope.event.search(false);
            },

            clear: function () {
                $scope.filters = {
                    showroomTransferUD: '',
                    showroomTransferDate: '',
                    fromWarehouseID: '',
                    toWarehouseID: ''
                };
                $scope.event.refresh();
            },

            remove: function (item) {
                showroomTransferService.delete(
                    item.showroomTransferID,
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


            },

            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    showroomTransferService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                showroomTransferService.searchFilter.sortedDirection = sortedDirection;
                $scope.pageIndex = 1;
                showroomTransferService.searchFilter.pageIndex = scope.pageIndex;
                showroomTransferService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
        };



        $timeout(function initWithTimeout() {
            $scope.event.init();
        }, 0);
    }
})();