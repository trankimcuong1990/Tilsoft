//
// SUPPORT
//
jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});
var searchResultGrid = jQuery('#grdSearchResult').scrollableTable(
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.pageIndex = currentPage;
            jsonService.searchFilter.pageIndex = currentPage;
            scope.searchPurchasingInvoice();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = 1;
            jsonService.searchFilter.sortedBy = sortedBy;
            jsonService.searchFilter.sortedDirection = sortedDirection;
            scope.searchPurchasingInvoice();
        });
    }
);

var tilsoftApp = angular.module('tilsoftApp', []);

tilsoftApp.filter('supplier_Filter', function () {
    return function (suppliers, supplierValue) {
        var items = {
            supplierUD: supplierValue,
            supplierNM: supplierValue,
            out: []
        }
        if (supplierValue == '') {
            return suppliers;
        }
        angular.forEach(suppliers, function (value, key) {
            pushed = false;
            if (this.supplierUD != '') {
                pushed = pushed || (value.supplierUD.toUpperCase().indexOf(this.supplierUD.toUpperCase()) >= 0);
            }
            if (this.supplierNM != '') {
                pushed = pushed || (value.supplierNM.toUpperCase().indexOf(this.supplierNM.toUpperCase()) >= 0);
            }
            if (pushed) {
                this.out.push(value);
            }
        }, items);
        return items.out;
    };
});



tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.filters = {
        invoiceQuerySearch : ''
    };
    $scope.pageIndex = 1;
    $scope.totalPage = 0;

    $scope.purchasingInvoices = null;
    $scope.creditNoteTypes = null;
    $scope.suppliers = null;

    $scope.creditNoteTypeSelectedValue = 1;
    $scope.supplierFilterValue = '';

    //
    // event
    //
    $scope.event = {
        init: function () {
            $scope.getInitData();
        },

        reload: function () {
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = 1;
            jsonService.searchFilter.sortedBy = 'invoiceNo';
            $scope.searchPurchasingInvoice();
        },

        selectedCreditNoteType : function(x){
            if (x == 1)
            {
                $scope.event.reload(); //load purchasing invoice
            }
            else if (x == 2)
            {
                if ($scope.suppliers != null) return;
                $scope.getSuppliers(); // load supplier
            }
        }
    },

    $scope.searchPurchasingInvoice = function () {
        jsonService.searchFilter.filters = $scope.filters;
        jsonService.getListPurchasingInvoice(
            jsonService.searchFilter,
            function (data) {
                $scope.purchasingInvoices = data.data.data;
                var totalRows = data.data.totalRows;
                $scope.totalPage = Math.ceil(totalRows / jsonService.searchFilter.pageSize);
                $scope.$apply();

                if (totalRows < jsonService.searchFilter.pageSize) {
                    jQuery('#grdSearchResult').find('ul').hide();
                }
                else {
                    jQuery('#grdSearchResult').find('ul').show();
                }
                jQuery('#content').show();

                if (data.message.type == 2)
                {
                    jsHelper.processMessage(data.message);
                }

                searchResultGrid.updateLayout();
            },
            function (error) {
                $scope.data = null;
                $scope.pageIndex = 1;
                $scope.totalPage = 0;
                $scope.$apply();
            }
        );
    },

    $scope.getInitData = function () {
        jsonService.getInitData(
        function (data) {
            $scope.creditNoteTypes = data.data;
            $scope.$apply();
        },
        function (error) {
            jsHelper.showMessage('warning', error);
            $scope.creditNoteTypes = null;
            $scope.$apply();
        }
    );
    },

    $scope.getSuppliers = function () {
        jsonService.getSupplier(
            function (data) {
                $scope.suppliers = data.data;
                $scope.$apply();
                jQuery('#content').show();

                if (data.message.type == 2) {
                    jsHelper.processMessage(data.message);
                }
            },
            function (error) {
                $scope.suppliers = null;
                $scope.$apply();
            }
        );
    },
    $scope.event.init();
    $scope.event.reload();
}]);