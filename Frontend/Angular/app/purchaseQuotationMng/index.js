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
    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', purchaseQuotationMngController);
    purchaseQuotationMngController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function purchaseQuotationMngController($scope, $cookieStore, purchaseQuotationService) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;
        $scope.supportDeliveryTerms = [];
        $scope.supportPaymentTerms = [];
        $scope.supportFactoryRawMaterials = [];

        //Currency List
        $scope.supportCurrency = [
            { sname: 'VND', name: 'VND' },
            { sname: 'USD', name: 'USD' }
        ];

        //
        //filters with key word
        //
        $scope.filters = {
            purchaseQuotationUD: '',
            factoryRawMaterialID: '',
            validFrom: '',
            validTo: '',
            purchaseDeliveryID: '',
            purchasePaymentTermID: '',
            currency: ''
        };

        //GridHandler
        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    purchaseQuotationService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                purchaseQuotationService.searchFilter.sortedDirection = sortedDirection;
                purchaseQuotationService.searchFilter.pageIndex = $scope.pageIndex = 1;
                purchaseQuotationService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };

        //
        //Event
        //
        $scope.event = {
            init: init,
            search: search,
            refresh: refresh,
            clear: clear,
            remove: remove,
            download: download
        };

        //
        //Init
        //
        function init() {
            purchaseQuotationService.searchFilter.pageSize = context.pageSize;
            purchaseQuotationService.serviceUrl = context.serviceUrl;
            purchaseQuotationService.token = context.token;

            $scope.event.search();
        };


        //
        //Load Data
        //
        function search(isLoadMore) {
            //jQuery('#content').show();
            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;
            purchaseQuotationService.searchFilter.filters = $scope.filters;
            purchaseQuotationService.search(

                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / purchaseQuotationService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    $scope.supportDeliveryTerms = data.data.supportDeliveryTermDTOs;
                    $scope.supportPaymentTerms = data.data.supportPaymentTermDTOs;
                    $scope.supportFactoryRawMaterials = data.data.supportFactoryRawMaterialDTOs;

                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        //
        //refresh page
        //
        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1
            purchaseQuotationService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.search();
        };

        //
        //clear Search
        //
        function clear() {
            $scope.filters = {
                purchaseQuotationUD: '',
                factoryRawMaterialID: '',
                validFrom: '',
                validTo: '',
                purchaseDeliveryID: '',
                purchasePaymentTermID: '',
                currency: ''
            };
            $scope.event.refresh();
        };

        //
        //Remove Function
        //
        function remove(item) {
            purchaseQuotationService.delete(
                  item.purchaseQuotationID,
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
                      jsHelper.showMessage('warning', error);
                  }
              );
        };

        function download(url) {
            window.open(url);
        };

        $scope.event.init();

        function createservices() {
            purchaseQuotationService.searchFilter.pageSize = context.pageSize;
            purchaseQuotationService.serviceUrl = context.serviceUrl;
            purchaseQuotationService.token = context.token;
        };
    };
})();