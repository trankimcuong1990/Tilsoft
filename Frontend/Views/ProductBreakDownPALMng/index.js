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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', productBreakDownPALController);
    productBreakDownPALController.$inject = ['$scope', '$timeout', '$cookieStore', 'dataService'];

    function productBreakDownPALController($scope, $timeout, $cookieStore, productBreakDownPALService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;

        $scope.filters = {
            clientSearch: '',
            modelSearch: '',
            productSearch: '',
            sampleSearch: '',
            currencySearch: '',
            getTypeSearch: '2'
        };

        $scope.support = {
            currency: [{ currencyID: 1, currencyNM: 'EUR' }, { currencyID: 2, currencyNM: 'USD' }]
        };

        $scope.event = {
            init: function () {
                productBreakDownPALService.searchFilter.pageSize = context.pageSize;
                productBreakDownPALService.serviceUrl = context.serviceUrl;
                productBreakDownPALService.token = context.token;

                $scope.event.search();
            },
            search: function () {
                productBreakDownPALService.searchFilter.filters = $scope.filters;

                productBreakDownPALService.search(
                    function (data) {
                        Array.prototype.push.apply($scope.data, data.data.productBreakDownPALSearchResult);

                        $scope.totalPage = Math.ceil(data.totalRows / productBreakDownPALService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;

                        jQuery("#content").show();
                    },
                    function (error) {

                    }
                );
            },
            clear: function () {
                $scope.filters = {
                    clientSearch: '',
                    modelSearch: '',
                    productSearch: '',
                    currencySearch: '',
                    getTypeSearch: '2'
                };

                $scope.event.refresh();
            },
            refresh: function () {
                $scope.data = [];
                $scope.pageIndex = 1;
                productBreakDownPALService.searchFilter.pageIndex = $scope.pageIndex;

                $scope.event.search();
            },
            edit: function (id) {
                window.open(context.editUrl + id, '_blank')
            },
            create: function (id) {
                window.open(context.createUrl + id, '_blank');
            },
            delete: function (item) {
                productBreakDownPALService.delete(
                    item.productBreakDownID,
                    null,
                    function (data) {
                        if (data.message.type === 0) {
                            jsHelper.processMessage(data.message);
                            $scope.totalRows = $scope.totalRows - 1;

                            var index = $scope.data.indexOf(item);
                            $scope.data.splice(index, 1);

                            window.location = context.indexUrl;
                        }
                    },
                    function (error) {
                    });
            },
            loadNextPage: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    productBreakDownPALService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sortData: function (sortedBy, sortedDirection) {
                $scope.data = [];
                productBreakDownPALService.searchFilter.sortedDirection = sortedDirection;
                productBreakDownPALService.searchFilter.pageIndex = $scope.pageIndex = 1;
                productBreakDownPALService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            view: function (id) {
                window.open(context.viewUrl + id, '_blank')
            },
            export: function () {
                productBreakDownPALService.export(
                    function (data) {
                        window.location = context.reportUrl + data.data;
                    },
                    function (error) {
                    })
            }
        };

        $timeout(function () {
            $scope.event.init();
        }, 0);
    };
})();