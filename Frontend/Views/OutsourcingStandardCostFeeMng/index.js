(function () {
    'use strict';
    //
    // SUPPORT
    //
    $('.search-filter').keypress(function (e) {
        if (e.keyCode === 13) {
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
        $scope.dataModel = [];
        $scope.support = {
            outSourcingCostSPs: [],
            outSourcingCostTypeSPs: []
        };
        $scope.filters = {
            modelUD: ''
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
                //jQuery('#content').show();
                $scope.event.reload();
            },
            reload: function () {
                $scope.dataModel = [];
                $scope.pageIndex = 1;
                dataService.searchFilter.pageIndex = $scope.pageIndex;
                dataService.searchFilter.sortedBy = 'ModelUD';
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
                        Array.prototype.push.apply($scope.dataModel, data.data.outsourcingModels);
                        $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;
                        jQuery('#content').show();

                        $scope.support.outSourcingCostSPs = data.data.support.outSourcingCostSPs;
                        $scope.support.outSourcingCostTypeSPs = data.data.support.outSourcingCostTypeSPs;
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
                    modelUD: ''
                };
                $scope.event.reload();
            },

            getDetailModelItem: function (item) {
                if (item.outsourcingModelPieces.length > 0)
                {
                    angular.forEach(item.outsourcingModelPieces, function (item) {
                        item.isShow = !item.isShow;
                    });
                }
                else
                {
                    dataService.getDetailModel(
                        item.modelID,

                        function (data) {
                            item.outsourcingModelPieces = data.data;
                        },
                        function (error) {

                        }
                    );
                }
                
            },

            updateDataFromMaterial: function () {
                dataService.insertData(
                    function (data) {
                        jsHelper.processMessage(data.message);
                        $scope.event.init();
                    },
                    function (error) {
                        //do need do nothing
                    }
                );
            }

        };
        //
        // method
        //
        $scope.method = {
        };
        //
        // angular init
        //
        $timeout(function () {
            $scope.event.init();
        }, 0);
    }]);

})();