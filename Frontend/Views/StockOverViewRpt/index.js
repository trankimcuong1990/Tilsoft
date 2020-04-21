(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', stockOverViewRptController);
    stockOverViewRptController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function stockOverViewRptController($scope, $cookieStore, stockOverViewRptService) {
        $scope.data = [];
        $scope.productTypes = [];
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            itemSource: '',
            productTypeID: null,
            articleCode: '',
            subEANCode: '',
            description: ''
        };


        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    stockOverViewRptService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.dataOrder = [];

                stockOverViewRptService.searchFilter.sortedDirection = sortedDirection;
                stockOverViewRptService.searchFilter.pageIndex = $scope.pageIndex = 1;
                stockOverViewRptService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };

        $scope.event = {         
            init: function () {
                stockOverViewRptService.searchFilter.pageSize = context.pageSize;
                stockOverViewRptService.serviceUrl = context.serviceUrl;
                stockOverViewRptService.token = context.token;
                stockOverViewRptService.getInit(
                    function (data) {
                        $scope.productTypes = data.data.productTypes;
                        $scope.event.search();
                        jQuery('#content').show();
                    },
                    function (error) {
                    }
                );
            },

            search: function (isLoadMore) {
                
                $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

                stockOverViewRptService.searchFilter.filters = $scope.filters;
                stockOverViewRptService.search(
                    function (data) {
                        Array.prototype.push.apply($scope.data, data.data.stockDTOs);
                        //get total row
                        $scope.totalRows = data.totalRows;
                        //cal total page
                        $scope.totalPage = Math.ceil(data.totalRows / stockOverViewRptService.searchFilter.pageSize);
                        jQuery('#content').show();

                        //gridHandler
                        $scope.gridHandler.refresh();
                        if (!isLoadMore) {
                            $scope.gridHandler.goTop();
                        }
                        $scope.gridHandler.isTriggered = true;
                    },
                    function (error) {
                        $scope.data = null;
                        $scope.pageIndex = 1;
                        $scope.totalPage = 0;
                        $scope.$apply();
                    }
                );
            },

            generateExcel: function () {
                stockOverViewRptService.generateExcel(
                    function (data) {
                        if (data.message.type === 2) {
                            jsHelper.processMessage(data.message);
                            return;
                        }
                        window.location = context.reportUrl + data.data;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },
            gethtml: function () {
                $scope.data = [];
                $scope.event.search();
            }
        };

        $scope.method = {
           
        };

        $scope.event.init();
    };
})();
