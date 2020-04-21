tilsoftApp.controller('_FactorySaleInvoiceCtrl', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {

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
        $scope.eventFactorySaleInvoiceCtrl.init();
    };

    /*
    * gridHandler
    */
    $scope.gridHandler = {
        loadMore: function () {
            if ($scope.pageIndex < $scope.totalPage) {
                $scope.pageIndex++;
                $scope.searchFilter.pageIndex = $scope.pageIndex;
                $scope.eventFactorySaleInvoiceCtrl.getPurchasingInvoice(true);
            }
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.dataFactorySaleInvoice = [];
            $scope.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            $scope.searchFilter.pageIndex = $scope.pageIndex;
            $scope.searchFilter.sortedBy = sortedBy;
            $scope.eventFactorySaleInvoiceCtrl.getFactorySaleInvoice();
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
        invoiceNo: '',
        invoiceDate: '',
        factoryRawMaterialID: context.factoryRawMaterialID,
        docNo: ''
    };
    $scope.dataFactorySaleInvoice = [];

    /*
     *  Function
     */
    $scope.eventFactorySaleInvoiceCtrl = {

        init: function () {
            $scope.dataFactorySaleInvoice = [];
            $scope.pageIndex = 1;
            $scope.searchFilter.pageIndex = $scope.pageIndex;
            $scope.searchFilter.sortedBy = 'invoiceNo';
            $scope.eventFactorySaleInvoiceCtrl.getFactorySaleInvoice();
        },

        getFactorySaleInvoice: function (isLoadMore) {
            $scope.searchFilter.filters = $scope.filters;
            dataService.getFactorySaleInvoice(
                $scope.searchFilter,
                function (data) {
                    Array.prototype.push.apply($scope.dataFactorySaleInvoice, data.data.data);
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
                invoiceNo: '',
                invoiceDate: '',
                factoryRawMaterialID: context.factoryRawMaterialID,
                docNo: ''
            };
            $scope.eventFactorySaleInvoiceCtrl.init();
        },

        selectFactorySaleInvoiceItem: function () {
            $scope.receiptNoteSaleInvoice = [];
            angular.forEach($scope.dataFactorySaleInvoice, function (item) {
                if (item.isSelected) {
                    $scope.receiptNoteSaleInvoice.push(item);
                }
            });
            var sendServiceData = angular.copy($scope.receiptNoteSaleInvoice);
            dataService.pushDataSaleInvoice(sendServiceData);
        }
    };

    /*
     *  Method
     */
    $scope.methodFactorySaleInvoiceCtrl = {

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