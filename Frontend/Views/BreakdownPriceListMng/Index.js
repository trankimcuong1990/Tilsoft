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
        $scope.filters = {
            productionItemUD: '',
            productionItemNM: '',
            productionItemNMEN:''
        };

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;
        $scope.pageIndex = 1;
        $scope.totalPage = 0;
        $scope.totalRows = 0;

        //
        //extend property
        //
        $scope.companyID = null;
        $scope.exchangeRate = null;
        $scope.selectedItem = [];
        $scope.screen = {
            _MAIN: "main form",
            _SELECTED_ITEM: "selected item to update price"
        };
        $scope.currentScreen = $scope.screen._MAIN;

        //
        // events
        //
        $scope.event = {
            init: function () {
                dataService.addProductionItemPrice(
                    function (data) {
                        $scope.event.reload();
                    },
                    function (error) {
                        // reset data
                    }
                );                
            },
            reload: function () {
                $scope.data = [];
                $scope.pageIndex = 1;
                dataService.searchFilter.pageIndex = $scope.pageIndex;
                dataService.searchFilter.sortedBy = 'ProductionItemUD';
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
                        $scope.companyID = data.data.companyID;
                        $scope.exchangeRate = data.data.exchangeRate;
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
                    productionItemUD: '',
                    productionItemNM: '',
                    productionItemNMEN: ''
                };
                $scope.event.reload();
            },

            changePrice: function (item) {
                var x = $scope.selectedItem.filter(o => o.productionItemID === item.productionItemID);
                if (x.length === 0) {
                    $scope.selectedItem.push(angular.copy(item));
                }
                else {
                    x[0].avfPrice = item.avfPrice;
                    x[0].avtPrice = item.avtPrice;
                }
            },

            showSelectedItem: function () {
                $scope.currentScreen = $scope.screen._SELECTED_ITEM;
            },
            backToSearchForm: function () {
                $scope.currentScreen = $scope.screen._MAIN;
            },
            updatePrice: function () {
                if ($scope.selectedItem.length === 0) {
                    bootbox.alert("There are not item was updated price");
                    return;
                }
                dataService.updatePrice(
                    $scope.selectedItem,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        //refresh new price
                        angular.forEach($scope.data, function (item) {
                            var newPrice = $scope.selectedItem.filter(o => o.productionItemID === item.productionItemID);
                            if (newPrice.length > 0) {
                                item.avfPrice = newPrice[0].avfPrice;
                                item.avtPrice = newPrice[0].avtPrice;
                            }
                        });

                        //reset list selected
                        $scope.selectedItem = [];

                        //back to main form
                        $scope.currentScreen = $scope.screen._MAIN;
                    },
                    function (error) {                        
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