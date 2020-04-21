tilsoftApp.controller('_PurchasingInvoiceCtrl', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {

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

        $scope.eventPurchasingInvoiceCtrl.init();
    };

    /*
    * gridHandler
    */
    $scope.gridHandler = {
        loadMore: function () {
            if ($scope.pageIndex < $scope.totalPage) {
                $scope.pageIndex++;
                $scope.searchFilter.pageIndex = $scope.pageIndex;
                $scope.eventPurchasingInvoiceCtrl.getPurchasingInvoice(true);
            }
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.dataPurchasingInvoice = [];
            $scope.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            $scope.searchFilter.pageIndex = $scope.pageIndex;
            $scope.searchFilter.sortedBy = sortedBy;
            $scope.eventPurchasingInvoiceCtrl.getPurchasingInvoice();
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
        //clientID: context.clientID
    };
    $scope.dataPurchasingInvoice = [];

    /*
     *  Function
     */
    $scope.eventPurchasingInvoiceCtrl = {

        init: function () {
            $scope.dataPurchasingInvoice = [];
            $scope.pageIndex = 1;
            $scope.searchFilter.pageIndex = $scope.pageIndex;
            $scope.searchFilter.sortedBy = 'invoiceNo';
            $scope.eventPurchasingInvoiceCtrl.getPurchasingInvoice();
        },

        getPurchasingInvoice: function (isLoadMore) {
            $scope.searchFilter.filters = $scope.filters;
            dataService.getPurchasingInvoice(
                $scope.searchFilter,
                function (data) {
                    Array.prototype.push.apply($scope.dataPurchasingInvoice, data.data.data);
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
                //clientID: context.clientID
            };
            $scope.eventPurchasingInvoiceCtrl.init();
        },

        selectPurchasingInvoiceItem: function () {
            $scope.receiptNoteInvoice = [];
            angular.forEach($scope.dataPurchasingInvoice, function (item) {
                if (item.isSelected) {
                    $scope.receiptNoteInvoice.push(item);
                }
            });
            var sendServiceData = angular.copy($scope.receiptNoteInvoice);
            dataService.pushDataInvoice(sendServiceData);
        }
    };

    /*
     *  Method
     */
    $scope.methodPurchasingInvoiceCtrl = {

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