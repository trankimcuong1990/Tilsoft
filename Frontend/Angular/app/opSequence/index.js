jQuery('.search-filter').keypress(
    function (event) {
        if (event.keyCode === 13) {
            var scope = angular.element(jQuery("body")).scope();
            scope.event.reloaded();
        }
    }
);

(function () {
    'use strict';
    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', opSequenceController);
    opSequenceController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function opSequenceController($scope, $cookieStore, opSequenceService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            OPSequenceNM: ''
        };

        $scope.event = {
            loaded: loaded,
            reloaded: reloaded,
            deleted: deleted
        };

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    opSequenceService.searchFilter.pageIndex = $scope.pageIndex;

                    $scope.event.loaded(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                opSequenceService.searchFilter.sortedDirection = sortedDirection;
                opSequenceService.searchFilter.pageIndex = $scope.pageIndex = 1;
                opSequenceService.searchFilter.sortedBy = sortedBy;

                $scope.event.loaded();
            },
            isTriggered: false
        };

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        var cookieValue = $cookieStore.get(context.cookieID);
        if (cookieValue) {
            $scope.filters = cookieValue;
        }

        $scope.event.loaded();

        function loaded(isLoadMore) {
            inited();
            
            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            opSequenceService.searchFilter.filters = $scope.filters;
            opSequenceService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    
                    $scope.totalPage = Math.ceil(data.totalRows / opSequenceService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    jQuery('#content').show();

                    if (!isLoadMore){
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                }
            );
        };

        function reloaded() {
            $scope.data = [];
            opSequenceService.searchFilter.pageIndex = $scope.pageIndex = 1;

            $scope.event.loaded();
        };

        function deleted(id, index) {
            opSequenceService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    
                    if (data.message.type === 0) {
                        if (index >= 0) {
                            $scope.data.splice(index, 1);
                            $scope.totalRows--;
                        }
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function inited() {
            opSequenceService.searchFilter.pageSize = context.pageSize;
            opSequenceService.serviceUrl = context.serviceUrl;
            opSequenceService.token = context.token;
        };
    };
})();