function formatMaterial(data) {
    return $.map(data.data, function (item) {
        if (item !== null) {
            return {
                id: item.materialID,
                label: item.materialNM,
                description: item.materialUD
            };
        }
    });
};

jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.refresh();
    }
});

(function () {
    'use strict';
    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', purchasingPriceFactoryController);
    purchasingPriceFactoryController.$inject = ['$scope', '$timeout', '$cookieStore', 'dataService'];

    function purchasingPriceFactoryController($scope, $timeout, $cookieStore, purchasingPriceFactoryService) {
        $scope.filters = {
            validDate: null,
            supplierID: null,
            statusID: null,
            materialGroupID: null,
            //materialID: null
            materialNM: null
        };

        $scope.material = {
            api: {
                url: context.serviceUrl + 'getMaterial',
                token: context.token
            },
            data: {
                id: null,
                label: null,
                description: null,
            },
            param: null
        };

        $scope.materialGroups = [];
        $scope.suppliers = [];
        $scope.statuses = [
            { statusID: true, statusNM: 'APPROVED' },
            { statusID: false, statusNM: 'NOT APPROVED' }
        ];

        $scope.data = [];
        $scope.pageIndex = 1;
        $scope.totalPage = 0;
        $scope.totalRows = 0;

        $scope.event = {
            init: function () {
                purchasingPriceFactoryService.serviceUrl = context.serviceUrl;
                purchasingPriceFactoryService.token = context.token;
                var today = new Date();
                var dd = String(today.getDate()).padStart(2, '0');
                var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
                var yyyy = today.getFullYear();

                today = dd + '/' + mm + '/' + yyyy;
                
                $scope.filters.validDate = today;
                purchasingPriceFactoryService.init(
                    function (data) {
                        $scope.materialGroups = data.data.materialGroups;
                        $scope.suppliers = data.data.suppliers;

                        $timeout(function () {
                            $scope.event.search();
                        }, 0);
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.exceptionMessage);
                    });
            },
            getMaterialID: function () {
                if ($scope.material.data != null) {
                    $scope.filters.materialID = $scope.material.data.id;
                }
            },
            search: function () {
                purchasingPriceFactoryService.searchFilter.pageSize = context.pageSize;
                purchasingPriceFactoryService.searchFilter.filters = $scope.filters;

                purchasingPriceFactoryService.getPurchasingPrice(
                    function (data) {
                        if (data.message.type == 0) {
                            Array.prototype.push.apply($scope.data, data.data);

                            $scope.totalPage = Math.ceil(data.totalRows / purchasingPriceFactoryService.searchFilter.pageSize);
                            $scope.totalRows = data.totalRows;

                            jQuery("#content").show();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.exceptionMessage);
                    });
            },
            moveQuotation: function (purchaseQuotationID) {
                window.open(context.moveQuotation + purchaseQuotationID, '_blank')
            },
            loadNextPage: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;

                    purchasingPriceFactoryService.searchFilter.pageIndex = $scope.pageIndex;

                    $scope.event.search(true);
                }
            },
            sortData: function (sortedBy, sortedDirection) {
                $scope.data = [];

                purchasingPriceFactoryService.searchFilter.sortedDirection = sortedDirection;
                purchasingPriceFactoryService.searchFilter.pageIndex = $scope.pageIndex = 1;
                purchasingPriceFactoryService.searchFilter.sortedBy = sortedBy;

                $scope.event.search();
            },
            refresh: function () {
                $scope.data = [];
                $scope.pageIndex = 1;

                purchasingPriceFactoryService.searchFilter.pageIndex = $scope.pageIndex = 1;

                $scope.event.search();
            },
            exportExcel: function () {
                purchasingPriceFactoryService.export(
                    function (data) {
                        window.location = context.backendReportUrl + data.data;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.exceptionMessage);
                    });
            }
        };

        $timeout(function () {
            $scope.event.init();
        }, 0);
    };
})();