'use strict';
tilsoftApp.controller('_PurchaseOrderItemCtrl', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {

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
 
        $scope.eventPurchaseOrderItemCtrl.init();
    };

    /*
    * gridHandler
    */
    $scope.gridHandler = {
        loadMore: function () {
            if ($scope.pageIndex < $scope.totalPage) {
                $scope.pageIndex++;
                $scope.searchFilter.pageIndex = $scope.pageIndex;
                $scope.eventPurchaseOrderItemCtrl.getPurchaseOrder(true);
            }
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.dataPurchaseOrder = [];
            $scope.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            $scope.searchFilter.pageIndex = $scope.pageIndex;
            $scope.searchFilter.sortedBy = sortedBy;
            $scope.eventPurchaseOrderItemCtrl.getPurchaseOrder();
        },
        isTriggered: false
    };

    /*
     * property
     */
    $scope.searchFilter = {
        filters: {},
        sortedBy: 'PurchaseOrderUD',
        sortedDirection: 'DESC',
        pageSize: 25,
        pageIndex: 1
    };

    $scope.filters = {
        purchaseOrderStatusID: null,
        purchaseOrderUD: '',
        factoryRawMaterialID: context.factoryRawMaterialID,
        purchaseOrderDate: ''
    };
    $scope.dataPurchaseOrder = [];

    /*
     *  Function
     */
    $scope.eventPurchaseOrderItemCtrl = {

        init: function () {
            $scope.dataPurchaseOrder = [];
            $scope.pageIndex = 1;
            $scope.searchFilter.pageIndex = $scope.pageIndex;
            $scope.searchFilter.sortedBy = 'PurchaseOrderUD';
            $scope.eventPurchaseOrderItemCtrl.getPurchaseOrder();
        },

        getPurchaseOrder: function (isLoadMore) {
            $scope.searchFilter.filters = $scope.filters;
            dataService.getPurchaseOrder(
                $scope.searchFilter,
                function (data) {
                    Array.prototype.push.apply($scope.dataPurchaseOrder, data.data.data);
                    $scope.totalPage = Math.ceil(data.data.totalRows / $scope.searchFilter.pageSize);
                    $scope.totalRows = data.data.totalRows;
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
                purchaseOrderStatusID: null,
                purchaseOrderUD: '',
                factoryRawMaterialID: context.factoryRawMaterialID,
                purchaseOrderDate: ''
            };
            $scope.eventPurchaseOrderItemCtrl.init();
        },

        selectPurchaseOrderItem: function () {
            $scope.poItem = [];
            angular.forEach($scope.dataPurchaseOrder, function (item) {
                if (item.isSelected) {
                    $scope.poItem.push(item);
                }
            });
            var sendServiceData = angular.copy($scope.poItem);
            dataService.pushDataOrder(sendServiceData);
        }
    };

    /*
     *  Method
     */
    $scope.methodPurchaseOrderCtrl = {

    };

    /*
     * initialization
     */
    $scope.initController();
}]);