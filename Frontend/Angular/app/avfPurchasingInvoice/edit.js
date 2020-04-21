//
//APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['customFilters', 'furnindo-directive']);

tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    $scope.dataContainer = null;
    $scope.ledgerAccounts = null;
    $scope.seasons = null;
    $scope.newID = -1;

    $scope.event = {
        init: function () {
            avfPurchasingInvoiceService.load(
                context.id,
                function (data) {
                    $scope.dataContainer = data.data.data;
                    $scope.ledgerAccounts = data.data.ledgerAccounts;
                    $scope.seasons = data.data.seasons;
                    //console.log(data);
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                    if (data.message.type == 2) {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.datdataContainer = null;
                    $scope.ledgerAccounts = null;
                    $scope.$apply();
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                avfPurchasingInvoiceService.update(
                    context.id,
                    $scope.dataContainer,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.avfPurchasingInvoiceID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', context.errMsg);
            }
        },
        delete: function () {
            if (confirm('Are you sure ?')) {
                avfPurchasingInvoiceService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },

        changeFile: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.dataContainer.fileLocation = img.fileURL;
                        scope.dataContainer.friendlyName = img.filename;
                        scope.dataContainer.pdfFileScan_NewFile = img.filename;
                        scope.dataContainer.pdfFileScan_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeFile: function () {
            $scope.dataContainer.friendlyName = '';
            $scope.dataContainer.fileLocation = '';
            $scope.dataContainer.pdfFileScan_NewFile = '';
            $scope.dataContainer.pdfFileScan_HasChange = true;
        },
        downloadFile: function () {
            if ($scope.dataContainer.fileLocation) {
                window.open($scope.dataContainer.fileLocation);
            }
        },

        addDetail: function ($event) {
            $event.preventDefault();
            $scope.dataContainer.avfPurchasingInvoiceDetails.push({
                avfPurchasingInvoiceDetailID: $scope.method.getNewID(),
            });
        },
        removeDetail: function ($event, index) {
            $event.preventDefault();
            $scope.dataContainer.avfPurchasingInvoiceDetails.splice(index, 1);
        }

    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },
        getTotal: function () {
            var result = 0;
            if ($scope.dataContainer != null) {
                angular.forEach($scope.dataContainer.avfPurchasingInvoiceDetails, function (value, key) {
                    result += parseFloat(value.amount);
                }, null);
            }
            return result;
        }
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
    //
    // init
    //
    $scope.event.init();
}]);