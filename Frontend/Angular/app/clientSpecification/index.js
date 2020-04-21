jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.refresh();
    }
});

(function () {
    'use strict';
    
    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', clientSpecificationController);
    clientSpecificationController.$inject = ['$scope', '$cookieStore', 'dataService', '$window'];

    function clientSpecificationController($scope, $cookieStore, clientSpecificationService, $window) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.currentRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            clientSpecificationCode: '',
            clientUD: '',
            proformaInvoice: '',
            itemDesc: ''
        };

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        var cookieValue = $cookieStore.get(context.cookieID);
        if (cookieValue) {
            $scope.filters = cookieValue;
        };

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;

                    clientSpecificationService.searchFilter.pageIndex = $scope.pageIndex;

                    load(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                clientSpecificationService.searchFilter.sortedDirection = sortedDirection;
                clientSpecificationService.searchFilter.pageIndex = $scope.pageIndex = 1;
                clientSpecificationService.searchFilter.sortedBy = sortedBy;

                load();
            },
            isTriggered: false
        };

        $scope.event = {
            init: init,
            load: load,
            refresh: refresh,
            clear: clear,
            edit: edit,
            del: del,
        };

        init();

        function init() {
            clientSpecificationService.searchFilter.pageSize = context.pageSize;
            clientSpecificationService.serviceUrl = context.serviceUrl;
            clientSpecificationService.token = context.token;

            load();
        };

        function load(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            clientSpecificationService.searchFilter.filters = $scope.filters;

            clientSpecificationService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.resultData); //console.log($scope.data);

                    $scope.totalPage = Math.ceil(data.totalRows / clientSpecificationService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    var subNo = $scope.totalRows - $scope.currentRows;
                    if (subNo > clientSpecificationService.searchFilter.pageSize) {
                        $scope.currentRows = parseInt($scope.currentRows) + parseInt(clientSpecificationService.searchFilter.pageSize);
                    } else {
                        $scope.currentRows = parseInt($scope.currentRows) + parseInt(subNo);
                    }

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
        };

        function refresh() {
            $scope.data = [];
            $scope.currentRows = 0;

            clientSpecificationService.searchFilter.pageIndex = $scope.pageIndex = 1;

            load();
        };

        function clear() {
            $scope.filters = {
                clientUD: '',
                itemDesc: ''
            };

            refresh();
        };

        function edit(id, code) {
            $window.open(context.editUrl + id + '?code=' + code, '_blank');
        };

        function del(id) {

        };
    };
})();