(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', draftCommercialInvoiceMngController);
    draftCommercialInvoiceMngController.$inject = ['$scope', 'dataService', '$timeout'];

    function draftCommercialInvoiceMngController($scope, draftCommercialInvoiceMngService, $timeout) {
        // variable
        $scope.data = [];
        // event
        $scope.event = {
            get: get,
            viewDetailDescription: viewDetailDescription,
            getDraftInvoicePrintOut: getDraftInvoicePrintOut
        };

        function get() {
            draftCommercialInvoiceMngService.serviceUrl = context.serviceUrl;
            draftCommercialInvoiceMngService.token = context.token;
            draftCommercialInvoiceMngService.getOverview(
                context.id,
                function (data) {
                    $scope.data = data.data.dataOverView;
                    jQuery('#content').show();
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function viewDetailDescription(draftCommercialInvoiceDetailID) {
            $("#" + draftCommercialInvoiceDetailID).toggle();
            if ($("#icon-detail-description-" + draftCommercialInvoiceDetailID).hasClass('fa-plus-square-o')) {
                $("#icon-detail-description-" + draftCommercialInvoiceDetailID).removeClass('fa-plus-square-o');
                $("#icon-detail-description-" + draftCommercialInvoiceDetailID).addClass('fa-minus-square-o');
            }
            else if ($("#icon-detail-description-" + draftCommercialInvoiceDetailID).hasClass('fa-minus-square-o')) {
                $("#icon-detail-description-" + draftCommercialInvoiceDetailID).removeClass('fa-minus-square-o');
                $("#icon-detail-description-" + draftCommercialInvoiceDetailID).addClass('fa-plus-square-o');
            }
        };

        function getDraftInvoicePrintOut(id) {
            draftCommercialInvoiceMngService.getDraftCommercialPrintout(
                id,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type !== 2) {
                        window.open(context.backendReportUrl + data.data, '');
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        //
        // METHOD
        //
        $scope.method = {  
            calTotalAmount: function () {
                if ($scope.data == null) return;
                var total = 0;
                angular.forEach($scope.data.draftCommercialInvoiceDetails, function (item) {
                    if (item.saleOrderDetailID !== null) {
                        total = total + item.unitPrice * item.quantity;
                    }
                });
                return total;
            },

            calTotalQuantity: function () {
                if ($scope.data == null) return;
                var total = 0;
                angular.forEach($scope.data.draftCommercialInvoiceDetails, function (item) {
                    if (item.saleOrderDetailID !== null) {
                        total = total + item.quantity;
                    }
                });
                return total;
            },

            calTotalQuantitySparepart: function () {
                if ($scope.data == null) return;
                var total = 0;
                angular.forEach($scope.data.draftCommercialInvoiceDetails, function (item) {
                    if (item.saleOrderDetailSparepartID !== null) {
                        total = total + item.quantity;
                    }
                });
                return total;
            },

            calTotalAmountSparepart: function () {
                if ($scope.data == null) return;
                var total = 0;
                angular.forEach($scope.data.draftCommercialInvoiceDetails, function (item) {
                    if (item.saleOrderDetailSparepartID !== null) {
                        total = total + item.unitPrice * item.quantity;
                    }
                });
                return total;
            },

            netRecal: function () {
                var total = 0;
                angular.forEach($scope.data.draftCommercialInvoiceDetails, function (item) {
                    total = total + item.unitPrice * item.quantity;
                });
                $scope.data.netAmount = total;
                return total;
            },

            vatRecal: function () {
                $scope.data.vatAmount = $scope.data.netAmount * ($scope.data.vatRate == null || $scope.data.vatRate == 'undefined' ? 0 : $scope.data.vatRate) / 100;
                var amount = 0;
                amount = $scope.data.vatAmount;
                return amount;
            },

            totalRecal: function () {
                $scope.data.TotalAmount = $scope.data.netAmount + $scope.data.vatAmount;
                var amount = 0;
                amount = $scope.data.TotalAmount;
                return amount;
            }
        };
        // default event
        $scope.event.get();
    }
})();