jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.refresh();
    }
});

(function () {
    'use strict';
    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', factoryLocationController);
    factoryLocationController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function factoryLocationController($scope, $cookieStore, factoryLocationService) {
        // support data.
        $scope.support = {
            countries: null,
        };

        // filter data.
        $scope.filters = {
            locationUD: '',
            locationNM: '',
            manufacturerCountryID: null,
        };

        // display data.
        $scope.data = {
            totalRows: 0,
            totalPage: 0,
            pageIndex: 1,
            factoryLocations: [],
            beforeLocations: [],
        };

        // condition cookie filters.
        $scope.conditions = {
            defaultFilter: angular.copy($scope.filters),
            isDefaultFilter: true,
        };

        // cookie browser.
        $scope.cookie = {
            value: $cookieStore.get(context.cookieID),
        };

        if ($scope.cookie.value) {
            $scope.filters = $scope.cookie.value;
        };

        // event.
        $scope.event = {
            init: init,
            search: search,
            refresh: refresh,
            clear: clear,
            addItem: addItem,
            updateItem: updateItem,
            deleteItem: deleteItem,
            cancelItem: cancelItem,
            saveItem: saveItem,
            saveItems: saveItems,
        };

        // gridHandler.
        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.data.pageIndex < $scope.data.totalPage) {
                    factoryLocationService.searchFilter.pageIndex = $scope.data.pageIndex + 1;

                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data.factoryLocations = [];

                factoryLocationService.searchFilter.sortedBy = orderBy;
                factoryLocationService.searchFilter.sortedDirection = orderDirection;
                factoryLocationService.searchFilter.pageIndex = $scope.data.pageIndex = 1;

                $scope.event.search();
            },
            isTriggered: false
        };

        // define init.
        function init() {
            factoryLocationService.searchFilter.pageSize = context.pageSize;
            factoryLocationService.serviceUrl = context.serviceUrl;
            factoryLocationService.token = context.token;

            factoryLocationService.getInit(
                function (success) {
                    $scope.support.countries = success.data.countries;

                    jQuery('#content').show();

                    $scope.event.search();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    jQuery('#content').show();
                });
        };

        // define search.
        function search(loadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.conditions.isDefaultFilter = angular.equals($scope.filters, $scope.conditions.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            factoryLocationService.searchFilter.filters = $scope.filters;
            factoryLocationService.search(
                function (success) {
                    Array.prototype.push.apply($scope.data.factoryLocations, success.data.factoryLocations);

                    $scope.data.totalPage = Math.ceil(success.totalRows / factoryLocationService.searchFilter.pageSize);
                    $scope.data.totalRows = success.totalRows;

                    jQuery('#content').show();

                    if (!loadMore) {
                        $scope.gridHandler.goTop();
                    }

                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    jQuery('#content').show();
                });
        };

        // define add item.
        function addItem() {
            var item = {
                locationID: -1,
                locationUD: null,
                locationNM: null,
                manufacturerCountryID: null,
                isEditItem: true,
            };

            $scope.data.factoryLocations.splice(0, 0, item);
        };

        // define update item.
        function updateItem(item) {
            item.isEditItem = true;
        };

        // define delete item.
        function deleteItem(item) {
            factoryLocationService.delete(
                item.locationID,
                null,
                function (success) {
                    jsHelper.processMessage(success.message);

                    var index = $scope.data.factoryLocations.indexOf(item);
                    $scope.data.factoryLocations.splice(index, 1);
                    
                    $scope.data.totalRows = $scope.data.totalRows - 1;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        // define cancel item.
        function cancelItem(item) {
            if (item.locationID > 0) {
                item.isEditItem = false;
            } else {
                var index = $scope.data.factoryLocations.indexOf(item);
                $scope.data.factoryLocations.splice(index, 1);
            }
        };

        // define save item.
        function saveItem(item) {
            if (item.locationUD === null || item.locationUD === '') {
                jsHelper.showMessage('warning', 'Please input Location Code!');
                return false;
            }

            factoryLocationService.update(
                item.locationID,
                item,
                function (success) {
                    jsHelper.processMessage(success.message);

                    if (success.message.type === 0) {
                        var index = $scope.data.factoryLocations.indexOf(item);

                        if (item.locationID < 0) {
                            $scope.data.totalRows = $scope.data.totalRows + 1;
                        }

                        $scope.data.factoryLocations[index] = success.data;
                        $scope.data.factoryLocations[index].isEditItem = false;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        // define save items.
        function saveItems() {
        };

        // define refresh.
        function refresh() {
            $scope.data.factoryLocations = [];

            factoryLocationService.searchFilter.pageIndex = 1;

            $scope.event.search();
        };

        // define clear.
        function clear() {
            $scope.filters = {
                locationUD: '',
                locationNM: '',
                manufacturerCountryID: null,
            };

            $scope.event.refresh();
        };

        // default.
        $scope.event.init();
    };
})();