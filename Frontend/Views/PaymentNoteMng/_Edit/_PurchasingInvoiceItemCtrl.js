'use strict';
tilsoftApp.controller('_PurchasingInvoiceItemCtrl', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {

    /*
     * initialization Controller
     */
    $scope.initController = function () {
        /*
         * initialization Service
         */
        dataService.serviceUrl = context.serviceUrl;
        dataService.supportServiceUrl = context.supportServiceUrl;
        dataService.token = context.token;

        $scope.eventPurchaseInvoiceCtrl.init();
    };

    /*
    * gridHandler
    */
    $scope.gridHandler = {
        loadMore: function () {
            if ($scope.pageIndex < $scope.totalPage) {
                $scope.pageIndex++;
                $scope.searchFilter.pageIndex = $scope.pageIndex;
                $scope.eventPurchaseInvoiceCtrl.getPurchaseInvoice(true);
            }
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.dataPurchaseInvoice = [];
            $scope.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            $scope.searchFilter.pageIndex = $scope.pageIndex;
            $scope.searchFilter.sortedBy = sortedBy;
            $scope.eventPurchaseInvoiceCtrl.getPurchaseInvoice();
        },
        isTriggered: false
    };

    /*
     * property
     */
    $scope.searchFilter = {
        filters: {},
        sortedBy: 'UpdatedDate',
        sortedDirection: 'DESC',
        pageSize: 25,
        pageIndex: 1
    };

    $scope.filters = {
        purchaseInvoiceUD: '',
        purchaseInvoiceDate: '',
        invoiceNo: '',
        factoryRawMaterialID: context.factoryRawMaterialID
    };
    $scope.dataPurchaseInvoice = [];

    /*
     *  Function
     */
    $scope.eventPurchaseInvoiceCtrl = {

        init: function () {
            $scope.dataPurchaseInvoice = [];
            $scope.pageIndex = 1;
            $scope.searchFilter.pageIndex = $scope.pageIndex;
            $scope.searchFilter.sortedBy = 'purchaseInvoiceUD';
            $scope.eventPurchaseInvoiceCtrl.getPurchaseInvoice();
        },

        getPurchaseInvoice: function (isLoadMore) {
            $scope.searchFilter.filters = $scope.filters;
            dataService.getPurchaseInvoice(
                $scope.searchFilter,
                function (data) {
                    console.log(data);
                    Array.prototype.push.apply($scope.dataPurchaseInvoice, data.data.data);
                    $scope.totalPage = Math.ceil(data.data.totalRows / $scope.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    jQuery('#content').show();


                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {

                }
            );
        },

        clearFilter: function () {
            $scope.filters = {
                purchaseInvoiceUD: '',
                invoiceDate: '',
                factoryRawMaterialID: context.factoryRawMaterialID
            };
            $scope.eventPurchaseInvoiceCtrl.init();
        },

        selectPurchaseInvoiceItem: function () {
            $scope.paymentVoucherInvoice = [];
            angular.forEach($scope.dataPurchaseInvoice, function (item) {
                if (item.isSelected) {
                    $scope.paymentVoucherInvoice.push(item);
                }
            });
            var sendServiceData = angular.copy($scope.paymentVoucherInvoice);
            dataService.pushDataInvoice(sendServiceData);
        }
    };

    /*
     *  Method
     */
    $scope.methodPurchaseInvoiceCtrl = {

    };

    /*
     * 
     */


    console.log('load in');

    /*
     * initialization
     */
    $scope.initController();
}]);