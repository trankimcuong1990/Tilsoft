//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {

    $scope.invoiceTypes = null;
    $scope.dataContainer = {
        avfSupplierID: ''
    }
    $scope.event = {
        goNext: function ($event) {
            $event.preventDefault();

            if ($scope.dataContainer.avfSupplierID == '') {
                alert('Please select supplier before go to next step');
                return;
            }
            else {
                window.location = '@Url.Action("Init2", "AVFPurchasingInvoice")/' + $scope.dataContainer.avfSupplierID;
            }
        },
        goBack: function ($event) {
            $event.preventDefault();
            window.location = '@Url.Action("Index", "AVFPurchasingInvoice")';
        },
        init: function () {
            $scope.method.getInvoiceType();
        }
    };

    $scope.method = {
        
    };

    //quick seach client form
    var quickSearchAVFSupplierGrid = jQuery('#quickSearchAVFSupplierTable').scrollableTable(
        function (currentPage) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                scope.quickSearchAVFSupplier.filters.pageIndex = currentPage;
                scope.quickSearchAVFSupplier.method.searchSupplier();
            });
        },
        function (sortedBy, sortedDirection) {
            //do nothing
        }
    );
    searchSupplierTimer = null;
    $scope.quickSearchAVFSupplier = {
        data: null,
        filters: {
            filters: {
                searchQuery: ''
            },
            sortedBy: 'avfSupplierUD',
            sortedDirection: 'ASC',
            pageSize: 10,
            pageIndex: 1
        },
        totalPage: 0,

        method: {
            searchSupplier: function () {
                avfPurchasingInvoiceService.quickSearchAVFSupplier(
                    $scope.quickSearchAVFSupplier.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quickSearchAVFSupplier.data = data.data;
                            $scope.quickSearchAVFSupplier.totalPage = Math.ceil(data.totalRows / $scope.quickSearchAVFSupplier.filters.pageSize);
                            quickSearchAVFSupplierGrid.updateLayout();
                            $scope.$apply();
                            jQuery('#supplier-popup').show();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },

        event: {
            searchBoxKeyUp: function () {
                if (jQuery('#txtSupplier').val().length >= 3) {
                    clearTimeout(searchSupplierTimer);
                    searchSupplierTimer = setTimeout(
                        function () {
                            $scope.quickSearchAVFSupplier.filters.filters.searchQuery = jQuery('#txtSupplier').val();
                            $scope.quickSearchAVFSupplier.filters.pageIndex = 1;
                            $scope.quickSearchAVFSupplier.method.searchSupplier();
                        },
                        500);
                }
            },
            close: function ($event, clearSearchBox) {
                jQuery('#supplier-popup').hide();
                if (clearSearchBox) {
                    $scope.dataContainer.avfSupplierID = null;
                    $scope.dataContainer.avfSupplierUD = null;

                    $scope.dataContainer.avfSupplierNM = null;
                }
                $scope.quickSearchAVFSupplier.data = null;
                $event.preventDefault();
            },
            ok: function ($event, id) {
                $scope.dataContainer.avfSupplierID = id;
                //console.log($scope.dataContainer);
                jQuery.each($scope.quickSearchAVFSupplier.data, function () {
                    if (this.avfSupplierID == id) {
                        $scope.dataContainer.avfSupplierID = this.avfSupplierID;
                        $scope.dataContainer.avfSupplierUD = this.avfSupplierUD;
                        $scope.dataContainer.avfSupplierNM = this.avfSupplierNM;
                    }
                });
                $scope.quickSearchAVFSupplier.event.close($event, false);
            }
        }
    }


    $scope.event.init();
}]);