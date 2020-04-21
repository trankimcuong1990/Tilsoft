
//
//APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['customFilters','furnindo-directive']);

tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    $scope.dataContainer = null;

    $scope.newID = -1;

    $scope.event = {
        init: function () {
            forwarderInvoiceService.load(
                context.id,
                function (data) {
                    $scope.dataContainer = data.data.data;
                    $scope.feeTypes = data.data.feeTypes;
                    //console.log(data);
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                    if (data.message.type == 2)
                    {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.datdataContainer = null;
                    $scope.feeTypes = null;
                    $scope.$apply();
                }
            );
        },
        update: function () {

            if ($scope.editForm.$valid) {
                forwarderInvoiceService.update(
                    context.id,
                    $scope.dataContainer,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.forwarderInvoiceID);
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
                forwarderInvoiceService.delete(
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
    };
    //
    // Custom function
    //
    $scope.addInvoiceDetail = function () {

        var detail = $scope.dataContainer.forwarderInvoiceDetails;
        max_rowIndex = 1;      
        $scope.dataContainer.forwarderInvoiceDetails.push({
            forwarderInvoiceDetailID: max_rowIndex * (-1),
            feeName: '',
            currency: '',
            amount: null,
        });
        setTimeout(function () { runAllForms() }, 200);
    }

    $scope.removeInvoiceDetail = function ($event, id) {
        $event.preventDefault();

        isExist = false;
        for (j = 0; j < $scope.dataContainer.forwarderInvoiceDetails.length; j++) {
            if ($scope.dataContainer.forwarderInvoiceDetails[j].forwarderInvoiceDetailID == id) {
                isExist = true;
                break;
            }
        }
        if (isExist) {
            $scope.dataContainer.forwarderInvoiceDetails.splice(j, 1);
        }
    }

    //quick seach client form
    var quickSearchBookingGrid = jQuery('#quickSearchBookingTable').scrollableTable(
        function (currentPage) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                scope.quicksearchBooking.filters.pageIndex = currentPage;
                scope.quicksearchBooking.method.searchBooking();
            });
        },
        function (sortedBy, sortedDirection) {
            //do nothing
        }
    );
    searchBookingTimer = null;
    $scope.quicksearchBooking = {
        data: null,
        filters: {
            filters: {
                searchQuery: ''
            },
            sortedBy: 'BLNo',
            sortedDirection: 'ASC',
            pageSize: 10,
            pageIndex: 1
        },
        totalPage: 0,

        method: {
            searchBooking: function () {
                forwarderInvoiceService.quicksearchBooking(
                    $scope.quicksearchBooking.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quicksearchBooking.data = data.data;
                            //console.log(data);
                            $scope.quicksearchBooking.totalPage = Math.ceil(data.totalRows / $scope.quicksearchBooking.filters.pageSize);
                            quickSearchBookingGrid.updateLayout();
                            $scope.$apply();
                            jQuery('#booking-popup').show();
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
                if (jQuery('#txtBooking').val().length >= 3) {
                    clearTimeout(searchBookingTimer);
                    searchBookingTimer = setTimeout(
                        function () {
                            $scope.quicksearchBooking.filters.filters.searchQuery = jQuery('#txtBooking').val();
                            $scope.quicksearchBooking.filters.pageIndex = 1;
                            $scope.quicksearchBooking.method.searchBooking();
                        },
                        500);
                }
            },
            close: function ($event, clearSearchBox) {
                jQuery('#booking-popup').hide();
                if (clearSearchBox) {
                    $scope.dataContainer.bookingID = null;
                    $scope.dataContainer.bookingUD = null;
                    $scope.dataContainer.blNo = null;

                    $scope.dataContainer.etd = null;
                    $scope.dataContainer.eta = null;

                    $scope.dataContainer.forwarderInfo = null;
                    $scope.dataContainer.poLname = null;
                    $scope.dataContainer.podName = null;
                }
                $scope.quicksearchBooking.data = null;
                $event.preventDefault();
            },
            ok: function ($event, id) {
                $scope.dataContainer.bookingID = id;
                //console.log($scope.dataContainer);
                jQuery.each($scope.quicksearchBooking.data, function () {
                    if (this.bookingID == id) {
                        $scope.dataContainer.bookingID = this.bookingID;
                        $scope.dataContainer.bookingUD = this.bookingUD;
                        $scope.dataContainer.blNo = this.blNo;

                        $scope.dataContainer.etd = this.etd;
                        $scope.dataContainer.eta = this.eta;

                        $scope.dataContainer.forwarderInfo = this.forwarderInfo;
                        $scope.dataContainer.poLname = this.poLname;
                        $scope.dataContainer.podName = this.podName;
                    }
                });
                $scope.quicksearchBooking.event.close($event, false);
            }
        }
    }
    //
    // init
    //
    $scope.event.init();
}]);