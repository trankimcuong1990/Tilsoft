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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', costInvoice2Controller);
    costInvoice2Controller.$inject = ['$scope', '$cookieStore', 'dataService'];

    function costInvoice2Controller($scope, $cookieStore, costInvoice2Service) {
        $scope.data = [];

        // season for print.
        $scope.printSeason = jsHelper.getCurrentSeason();
        
        // support search data.
        $scope.seasons = [];
        $scope.typeOfPayments = [];
        $scope.paymentStatus = [
            { id: 0, name: 'PENDING' },
            { id: 1, name: 'PAID' }
        ];
        //total value
        $scope.totalAmountValue = 0;
        //sub total value
        $scope.subTotalAmountValue = 0;

        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;
        $scope.currentRows = 0;

        $scope.filters = {
            season: jsHelper.getCurrentSeason(),
            creditorCode: '',
            costInvoice2TypeID: null,
            relatedDocumentNo: '',
            invoiceNo: '',
            invoiceDate: '',
            dueDate: '',
            paidDate: '',
            isPaid: null,
        };

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    costInvoice2Service.searchFilter.pageIndex = $scope.pageIndex;

                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                costInvoice2Service.searchFilter.sortedDirection = sortedDirection;
                costInvoice2Service.searchFilter.pageIndex = $scope.pageIndex = 1;
                costInvoice2Service.searchFilter.sortedBy = sortedBy;

                $scope.event.search(false);
            },
            isTriggered: false
        };

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        var cookieValue = $cookieStore.get(context.cookieID);
        if (cookieValue) {
            $scope.filters = cookieValue;
        }

        $scope.event = {
            init: init,
            clear: clear,
            search: search,
            refresh: refresh,
            print: print,
            remove: remove,
            close: close,
            open: open
        };



        function init() {
            costInvoice2Service.searchFilter.pageSize = context.pageSize;
            costInvoice2Service.serviceUrl = context.serviceUrl;
            costInvoice2Service.token = context.token;

            costInvoice2Service.getInit(
                function (data) {
                    $scope.seasons = data.data.seasons;
                    $scope.typeOfPayments = data.data.costInvoice2Types;

                    $scope.event.search(false);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        }

        function clear() {
            $scope.filters = {
                season: jsHelper.getCurrentSeason(),
                creditorCode: '',
                costInvoice2TypeID: null,
                relatedDocumentNo: '',
                invoiceNo: '',
                invoiceDate: '',
                dueDate: '',
                paidDate: '',
                isPaid: null,
            };

            $scope.event.refresh();
        }

       

        function search(isLoadMore) {

            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            costInvoice2Service.searchFilter.filters = $scope.filters;
            costInvoice2Service.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.costInvoice2SearchResults);

                    $scope.totalPage = Math.ceil(data.totalRows / costInvoice2Service.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    //total value
                    $scope.totalAmountValue = data.data.totalAmountValue;
                    //sub total value
                    $scope.subTotalAmountValue = data.data.subTotalAmountValue;

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
        }

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1
            costInvoice2Service.searchFilter.pageIndex = $scope.pageIndex;

            $scope.event.search(false);
        }
  //===========================================================================================================================//
        function open() {
            jQuery('#frmPrintOption').modal();
           
        }

        function close() {
            jQuery('#frmPrintOption').modal('hide');
        }

        function print() {
            //if (jQuery('#season').val() === null || jQuery('#season').val() === '') {
            //    jsHelper.showMessage('warning', 'Please input Start Date.');
            //    return false;
            //}

            //$scope.filters.season = jQuery('#season').val();
                        debugger
            costInvoice2Service.exportCostInvoice2(
                $scope.printSeason,
                function (data) {
                    debugger
                    window.location = context.serviceReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        }
 //===========================================================================================================================================//
        function remove(item) {
            costInvoice2Service.delete(
                item.costInvoice2ID,
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
        }

        $scope.event.init();
    };
})();