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
    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', developmentItemsRptController);
    developmentItemsRptController.$inject = ['$scope', '$cookieStore', 'dataService']

    function developmentItemsRptController($scope, $cookieStore, dataService) {

        //Varible Zone
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        //varible Filters
        $scope.filters = {
            season: '',
            clientUD: '',
            vnSuggestedFactoryUD: '',
            articleDescription: '',
            materialDescription: '',
            materialTypeDescription: '',
            materialColorDescription: '',
            material2Description: '',
            material2TypeDescription: '',
            material2ColorDescription: '',
            material3Description: '',
            material3TypeDescription: '',
            seatCushionDescription: '',
            backCushionDescription: '',
            cushionColorDescription: '',
            seatCushionSpecification: '',
            backCushionSpecification: '',
            sampleProductStatusID: null
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
            refresh: refresh
        };
        //init
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
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.seasons = data.data.seasons;
                    $scope.sampleProductStatuses = data.data.sampleProductStatuses;
                    $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    jQuery("#content").show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error)
                }
                );
        }

        //Event Clear
        function clear() {
            $scope.filters = {
                season: '',
                clientUD: '',
                vnSuggestedFactoryUD: '',
                articleDescription: '',
                materialDescription: '',
                materialTypeDescription: '',
                materialColorDescription: '',
                material2Description: '',
                material2TypeDescription: '',
                material2ColorDescription: '',
                material3Description: '',
                material3TypeDescription: '',
                seatCushionDescription: '',
                backCushionDescription: '',
                cushionColorDescription: '',
                seatCushionSpecification: '',
                backCushionSpecification: '',
                sampleProductStatusID: null
            };
            $scope.event.refresh();
        }

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.search();

        }
        //Event init
        $scope.event.init();

        //Creat Service
        function createservices() {
            dataService.searchFilter.pageSize = context.pageSize;
            dataService.searchFilter.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
        }

    }

})();