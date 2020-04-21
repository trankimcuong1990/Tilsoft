(function () {
    'use strict';
    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', masterExchangeRateMngController);
    masterExchangeRateMngController.$inject = ['$scope', '$cookieStore', 'dataService', '$timeout'];

    function masterExchangeRateMngController($scope, $cookieStore, dataService, $timeout) {

        //Varible Zone
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;


        $scope.currencyArr = [{ name: "USD", currency: "USD" }, { name: "EUR", currency: "EUR" }, { name: "CNY", currency: "CNY" }];
        
        //$scope.currencyArr = [{ name: 'USD', currency: USD }, { name: 'EUR', currency: EUR }, { name: 'CNY', currency: CNY }];
        //Filters
        $scope.filters = {
            currency: '',
            validDate:''
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
            clear: clear,
            refresh: refresh,
            remove: remove,
            cancelEditValue: cancelEditValue,
            editValue: editValue,
            updateValue: updateValue,
            onUnitValue: onUnitValue
        };

        //Load Data
        function search(isLoadMore) {
            dataService.searchFilter.pageSize = context.pageSize;
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;

            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.gridHandler.isTriggered = false;
            dataService.searchFilter.filters = $scope.filters;

            dataService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    jQuery("#content").show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {

                }
            );
        }

        //Event Clear
        function clear() {
            $scope.filters = {
                currency: '',
                validDate: ''
            };
            $scope.event.refresh();
        }

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.search();

        }

        function remove(item) {
            dataService.delete(
                item.productSpecificationID,
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

        function editValue(item) {
            item.isEditValue = true;
        }

        function cancelEditValue(item) {
            item.isEditValue = false;
            //$scope.event.refresh();
        }

        function updateValue(item) {
            var postedData = [];
            if (item.isChanged !== undefined && item.isChanged === true) {
                postedData.push({
                    masterExchangeRateID: item.masterExchangeRateID,
                    currency: item.currency,
                    exchangeRate: item.exchangeRate,
                    validDate: item.validDate
                });
            } else {
                bootbox.alert("Pls Input data !");
                return false;
            }

            dataService.updateIndexData(
                postedData,
                function (data) {
                    if (data.data) {
                        angular.forEach($scope.data, function (item) {
                            item.isChanged = false;
                        });
                    }
                },
                function (error) {

                }
            );
            item.isEditValue = false;
            $scope.apply();
        }

        function onUnitValue(item) {
            item.isChanged = true;
        }

        $timeout(function () {
            $scope.event.search();
        }, 0);

        //Creat Service
        function createservices() {
            dataService.searchFilter.pageSize = context.pageSize;
            dataService.searchFilter.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
        }

    }

})();