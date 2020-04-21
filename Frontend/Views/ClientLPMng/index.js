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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', clientLPMngController);
    clientLPMngController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function clientLPMngController($scope, $cookieStore, clientLPMngService) {
        $scope.data = [];
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;
        $scope.lpManagingTeams = [];

        $scope.filters = {
            clientUD: '',
            isAutoUpdateSimilarLP: '',
            clientNM: '',
            lpManagingTeamID: ''
        };
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;


        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    clientLPMngService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                clientLPMngService.searchFilter.sortedDirection = sortedDirection;
                clientLPMngService.searchFilter.pageIndex = $scope.pageIndex = 1;
                clientLPMngService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };

        $scope.event = {
            init: init,
            search: search,
            refresh: refresh,
            clear: clear,
            remove: remove
        };

        function init() {
            clientLPMngService.searchFilter.pageSize = context.pageSize;
            clientLPMngService.serviceUrl = context.serviceUrl;
            clientLPMngService.token = context.token;
            clientLPMngService.getInit(
                function (data) {
                    $scope.lpManagingTeams = data.data.supportLPManagingDTOs;
                    $scope.event.search();
                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );          
        };

        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            clientLPMngService.searchFilter.filters = $scope.filters;
            clientLPMngService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / clientLPMngService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    //jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }

                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;

            clientLPMngService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        };

        function clear() {
            $scope.filters = {
                clientUD: '',
                isAutoUpdateSimilarLP: '',
                clientNM: '',
                lpManagingTeamID: ''
            };
            $scope.event.refresh();

        }

        function remove(item) {
            clientLPMngService.delete(
                item.clientLPID,
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
    };
})();
