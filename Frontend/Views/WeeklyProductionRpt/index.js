(function () {
    'use strict';

    //
    // SUPPORT
    //
    $('.search-filter').keypress(function (e) {
        if (e.keyCode == 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.reload();
        }
    });

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
    angular.module('tilsoftApp').controller('tilsoftController', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
        dataService.searchFilter.pageSize = context.pageSize;
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;

        //
        // grid handler
        //
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
                $scope.pageIndex = 1;
                dataService.searchFilter.pageIndex = scope.pageIndex;
                dataService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };

        //
        // property
        //
        $scope.data = [];
        $scope.seasons = [];
        $scope.filters = {
            season: '2018/2019',
            factoryID: 374043,
        };

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;
        $scope.pageIndex = 1;
        $scope.totalPage = 0;
        $scope.totalRows = 0;

        //
        // events
        //
        $scope.event = {
            init: function () {
                //dataService.getSearchFilter(
                //    function (data) {
                //        $scope.event.search();
                //    },
                //    function (error) {
                //        // reset data
                //    }
                //);
                $scope.event.reload();
            },
            reload: function () {
                $scope.data = [];
                $scope.pageIndex = 1;
                dataService.searchFilter.pageIndex = $scope.pageIndex;
                dataService.searchFilter.sortedBy = 'WeekStart';
                dataService.searchFilter.sortedDirection = 'ASC';
                dataService.searchFilter.pageSize = 55;
                $scope.event.search();
            },
            search: function (isLoadMore) {
                //
                // store filter in cookies
                //
                $cookieStore.put(context.cookieId, $scope.filters);
                $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

                $scope.gridHandler.isTriggered = false;
                dataService.searchFilter.filters = $scope.filters;
                dataService.search(
                    function (data) {
                        Array.prototype.push.apply($scope.data, data.data.data);
                        $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;
                        $scope.seasons = data.data.seasons;
                        jQuery('#content').show();

                        if (!isLoadMore) {
                            $scope.gridHandler.goTop();
                        }
                        $scope.gridHandler.isTriggered = true;
                    },
                    function (error) {
                        $scope.data = null;
                        $scope.pageIndex = 1;
                        $scope.totalPage = 0;
                        $scope.totalRows = 0;
                    }
                );
            },
            clearFilter: function () {
                $scope.filters = {
                    UserUD: '',
                    EmployeeNM: ''
                };
                $scope.event.reload();
            },
            delete: function (item) {
                dataService.delete(
                    item.id, // id of item
                    null,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.data.splice($scope.data.indexOf(item), 1);
                            $scope.totalRows--;
                        }
                    },
                    function (error) {
                    }
                );
            },
            print: function (item) {
                //do nothing
            }
        }

        //
        // method
        //
        $scope.method = {
            getTotalCapacity: function () {
                var total = parseFloat(0);
                angular.forEach($scope.data, function (item) {
                    total += (item.capacity == null ? parseFloat(0) : parseFloat(item.capacity));
                });
                return total;
            },

            getTotalPlan: function () {
                var total = parseFloat(0);
                angular.forEach($scope.data, function (item) {
                    total += (item.planCont == null ? parseFloat(0) : parseFloat(item.planCont));
                });
                return total;
            },

            getTotalReal: function () {
                var total = parseFloat(0);
                angular.forEach($scope.data, function (item) {
                    total += (item.realCont == null ? parseFloat(0) : parseFloat(item.realCont));
                });
                return total;
            },

            getTotalDelta: function () {
                var total = parseFloat(0);
                angular.forEach($scope.data, function (item) {
                    total += (item.deltaCont == null ? parseFloat(0) : parseFloat(item.deltaCont));
                });
                return total;
            },
        };


        //
        // angular init
        //
        $timeout(function () {
            $scope.event.init();
        }, 0);
    }]);

})();