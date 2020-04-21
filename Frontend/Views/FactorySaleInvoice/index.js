(function () {
    'use strict';

    //
    // SUPPORT
    //
    $('.search-filter').keypress(function (e) {
        if (e.keyCode === 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.reload();
        }
    });

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
    angular.module('tilsoftApp').controller('tilsoftController', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
        dataService.searchFilter.pageSize = context.pageSize;
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;


        $('input[name="datefilter"]').daterangepicker({
            autoUpdateInput: false,
            locale: {
                cancelLabel: 'Clear'
            }
        });

        $('input[name="datefilter"]').on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('DD/MM/YYYY') + ' - ' + picker.endDate.format('DD/MM/YYYY'));
            $scope.filters.FromDate = picker.startDate.format('DD/MM/YYYY');
            $scope.filters.ToDate = picker.endDate.format('DD/MM/YYYY');
        });

        $('input[name="datefilter"]').on('cancel.daterangepicker', function (ev, picker) {
            $(this).val('');
        });

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
            docCode: '',
            invoicedDate: '',
            factoryRawMaterialID: null,
            postingDate: '',
            invoiceStatusID: null,
            invoiceTypeID: null,
            season: jsHelper.getCurrentSeason(),
            FromDate: '',
            ToDate: ''
        };

        $scope.InvoiceTypes = [
            { value: 0, name: "Purchasing Invoice"},
            { value: 1, name: "Factory Sale Invoice" }
        ];

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;
        $scope.pageIndex = 1;
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.InvoiceTypeDTOs = [];
        $scope.Invoicestatusdtos = [];
        $scope.factoryrawmaterialdtos = [];

        //
        // events
        //
        $scope.event = {
            init: function () {
                $scope.event.reload();
            },
            reload: function () {
                $scope.data = [];
                $scope.pageIndex = 1;
                dataService.searchFilter.pageIndex = $scope.pageIndex;
                dataService.searchFilter.sortedBy = 'InvoiceDate';
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
                        //$scope.InvoiceTypeDTOs = data.data.purchaseInvoiceTypeDTOs;
                        $scope.InvoiceStatusDTOs = data.data.factorySaleInvoiceStatusDTOs;
                        $scope.factoryRawMaterialDTOs = data.data.factoryRawMaterialDTOs;
                        $scope.totalAmountDTO = data.data.totalAmountDTO;
                        $scope.seasons = data.data.seasons;
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
                    docCode: '',
                    invoicedDate: '',
                    factoryRawMaterialID: null,
                    postingDate: '',
                    factorySaleInvoiceStatusID: null,
                    invoiceTypeID: null,
                    season: null,
                    FromDate: '',
                    ToDate: ''
                };
                $('input[name="datefilter"]').val("");
                $scope.event.reload();
            },
            delete: function (item) {
                dataService.delete(
                    item.factorySaleInvoiceID, // id of item
                    null,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            $scope.data.splice($scope.data.indexOf(item), 1);
                            $scope.totalRows--;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },
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