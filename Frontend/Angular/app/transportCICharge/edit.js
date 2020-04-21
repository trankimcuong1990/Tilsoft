(function () {
    "use strict";

    angular
        .module("tilsoftApp", ["furnindo-directive", "avs-directives", "ui.select2", "ngCookies"])
        .directive('format', function ($filter) {
            return {
                require: '?ngModel',
                link: function (scope, elem, attrs, ctrl) {
                    if (!ctrl) {
                        return;
                    }

                    ctrl.$formatters.unshift(function () {
                        return $filter('number')(ctrl.$modelValue);
                    });

                    ctrl.$parsers.unshift(function (viewValue) {
                        var plainNumber = viewValue.replace(/[\,\.]/g, ''),
                            b = $filter('number')(plainNumber);

                        elem.val(b);

                        return plainNumber;
                    });
                }
            };
        })
        .controller("tilsoftController", EditTransportCostForwarderController);

    EditTransportCostForwarderController.$inject = ["$scope", "dataService"];

    // ReSharper disable once InconsistentNaming
    function EditTransportCostForwarderController($scope, dataService) {
        $scope.dataContainer = null;

        $scope.data = {};
        $scope.newID = -1;
        $scope.containerTypeNM = '';

        $scope.event = {
            activate: activate,
            update: update,
            deleteItem: deleteItem,
            addInvoiceDetail: addInvoiceDetail,
            removeInvoiceDetail: removeInvoiceDetail,
            getVATAmount: getVATAmount,
            getAmount: getAmount,
            getPricePerUnit: getPricePerUnit
        };
        $scope.method = {
            refresh: refresh,
            showContent: showContent,
            showChangedIcon: showChangedIcon,
            monitorDataChanged: monitorDataChanged,
            processMessage: processMessage,
            getNewId: getNewId,
            getBodyScope: getBodyScope
        };

        var isValidDataDetail = true;
        var keepForwarderNM = '';
        var keepBookingNM = '';

        $scope.searchClientTimer = null;

        var quickSearchClient = jQuery('#quickSearchClientTable').scrollableTable(
            function (currentPage) {
                var scope = $scope.method.getBodyScope();

                scope.$apply(function () {
                    scope.quickSearchClient.filters.pageIndex = currentPage;
                    scope.quickSearchClient.method.searchClient();
                });
            },
            function (sortedBy, sortedDirection) {
            }
        );

        $scope.quickSearchClient = {
            data: [],
            filters: {
                filters: {
                    searchQuery: ''
                },
                sortedBy: 'ClientNM',
                sortedDirection: 'ASC',
                pageSize: 10,
                pageIndex: 1
            },
            totalPage: 0,

            method: {
                searchClient: function () {
                    dataService.quickSearchClient(
                    $scope.quickSearchClient.filters,
                    function (data) {
                        if (data.message.type === 0) {
                            $scope.quickSearchClient.data = data.data; //console.log($scope.quickSearchClient.data)
                            $scope.quickSearchClient.totalPage = Math.ceil(data.totalRows / $scope.quickSearchClient.filters.pageSize);

                            quickSearchClient.updateLayout();

                            $scope.$apply();

                            jQuery('#clientPopup').show();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
                }
            },

            event: {
                searchBoxUp: function () {
                    if (jQuery('#inClientNM').val().length >= 3) {
                        clearTimeout($scope.searchClientTimer);
                        $scope.searchClientTimer = setTimeout(
                            function () {
                                $scope.quickSearchClient.filters.filters.searchQuery = jQuery('#inClientNM').val();
                                $scope.quickSearchClient.filters.pageIndex = 1;
                                $scope.quickSearchClient.method.searchClient();
                            }, 500);
                    }
                },
                close: function (clearSearchBox) {
                    jQuery('#clientPopup').hide();

                    if (clearSearchBox) {
                        $scope.editData.clientID = null;
                        $scope.editData.clientUD = null;
                        $scope.editData.clientNM = null;
                    }

                    $scope.quickSearchClient.data = [];
                },
                ok: function (id) {
                    $scope.editData.clientID = id;

                    jQuery.each($scope.quickSearchClient.data, function () {
                        if (this.clientID === id) {
                            $scope.editData.clientID = this.clientID;
                            $scope.editData.clientUD = this.clientUD;
                            $scope.editData.clientNM = this.clientNM;
                        }
                    });

                    $scope.quickSearchClient.event.close(false);
                }
            }
        }

        //#region quickSearchBooking
        var quickSearchBookingGrid = jQuery("#quickSearchBookingTable").scrollableTable(
            function (currentPage) {
                var scope = $scope.method.getBodyScope();
                scope.$apply(function () {
                    scope.quickSearchBooking.filters.pageIndex = currentPage;
                    scope.quickSearchBooking.method.searchBooking();
                });
            },
            function (sortedBy, sortedDirection) {
                //do nothing
            }
        );

        $scope.searchBookingTimer = null;
        $scope.quickSearchBooking = {
            data: null,
            filters: {
                filters: {
                    searchQuery: "",
                    clientID: ""
                },
                sortedBy: "BookingUD",
                sortedDirection: "ASC",
                pageSize: 10,
                pageIndex: 1
            },
            totalPage: 0,

            method: {
                searchBooking: function () {
                    dataService.quickSearchBooking(
                        $scope.quickSearchBooking.filters,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.quickSearchBooking.data = data.data.data;
                                $scope.quickSearchBooking.totalPage = Math.ceil(data.totalRows / $scope.quickSearchBooking.filters.pageSize);
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
                    if (jQuery("#inBookingUD").val().length >= 3) {
                        clearTimeout($scope.searchBookingTimer);
                        $scope.searchBookingTimer = setTimeout(
                            function () {
                                $scope.quickSearchBooking.filters.filters.searchQuery = jQuery("#inBookingUD").val();
                                $scope.quickSearchBooking.filters.filters.clientID = $scope.editData.clientID;
                                $scope.quickSearchBooking.filters.pageIndex = 1;
                                $scope.quickSearchBooking.method.searchBooking();
                            },
                            500);
                    }
                },
                close: function ($event, clearSearchBox) {
                    jQuery("#booking-popup").hide();
                    if (clearSearchBox) {
                        $scope.editData.bookingID = null;
                        $scope.editData.bookingUD = null;
                        $scope.editData.blNo = null;
                        $scope.editData.settingValue = null;
                        $scope.editData.etd = null;
                        $scope.editData.polid = null;
                    }
                    $scope.quickSearchBooking.data = null;
                    $event.preventDefault();
                },
                ok: function ($event, id) {

                    jQuery.each($scope.quickSearchBooking.data, function () {
                        if (this.bookingID === id) {
                            /*if (scope.editData.bookingID !== this.bookingID) {
                                scope.editData.transportCostForwarderItems = [];
                            }*/

                            $scope.editData.bookingID = this.bookingID;
                            $scope.editData.bookingUD = this.bookingUD;
                            $scope.editData.blNo = this.blNo;
                            $scope.editData.settingValue = this.settingValue;
                            $scope.editData.etd = this.etd;
                            $scope.editData.polid = this.polid;
                        }
                    });

                    $scope.editData.bookingID = id;
                    $scope.quickSearchBooking.event.close($event, false);
                    $scope.containers.method.getDropDownContainer();
                }
            }
        }
        //#endregion quickSearchBooking

        $scope.containerNrs = [];

        //#region get dropdown container
        $scope.containers = {
            data: [],
            filters: {
                filters: {
                    BLNo: ''
                }
            },
            method: {
                getDropDownContainer: function () {
                    $scope.containers.filters.filters.BLNo = $scope.editData.blNo;
                    dataService.getDropDownContainer(
                        $scope.containers.filters,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.containerNrs = $scope.containers.data = data.data;
                            }
                        },
                        function (error) {

                        });
                }
            }
        }
        //#endregion get dropdown container

        //#region pop-up quickSearchForwarder
        /*jQuery('#txtForwarder').on('blur', function () {
            //// reset forwarder
            //if (jQuery('#txtForwarder').val() === null || jQuery('#txtForwarder').val() === '') {
            //    $scope.editData.forwarderID = null;
            //    $scope.editData.forwarderNM = null;
            //}
            //// close pop-up when leave
            //if (jQuery('#forwarder-popup').css('display') !== 'none') {
            //    jQuery('#forwarder-popup').css('display', 'none');
            //}
        });
        //#endregion pop-up quickSearchForwarder

        //#region pop-up quickSearchBooking
        jQuery('#txtBooking').on('blur', function () {
            //if (jQuery('#txtBooking').val() === null || jQuery('#txtBooking').val() === '') {
            //    $scope.editData.bookingID = null;
            //    $scope.editData.bookingUD = null;
            //    $scope.editData.blNo = null;
            //    $scope.editData.settingValue = null;
            //}
            //if (jQuery('#booking-popup').css('display') !== 'none') {
            //    jQuery('#booking-popup').css('display', 'none');
            //}
        });*/
        //#endregion pop-up quickSearchBooking
        //debugger;
        activate();

        ////////////////                                           

        function activate() {
            //debugger;
            initDataService();

            dataService.load(
                context.id,
                function (data) {
                    $scope.editData = data.data.data;
                    $scope.paymentTerms = data.data.paymentTerms;
                    $scope.containerTypes = data.data.containerTypes;
                    $scope.chargeTypes = data.data.chargeTypes;
                    $scope.currencyTypes = data.data.currencyTypes;
                    $scope.containerNrs = data.data.containerNrs; console.log(data.data)

                    $scope.$apply();

                    $scope.method.showContent();
                    //$scope.method.monitorDataChanged();
                    //$scope.method.processMessage(data.message);
                },
                function (error) {
                    jsHelper.showMessage("warning", error);

                    $scope.editData = null;
                    $scope.chargeTypes = null;
                    $scope.currencyTypes = null;
                    $scope.$apply();
                }
            );


            /////
            function initDataService() {
                dataService.searchFilter.pageSize = context.pageSize;
                dataService.serviceUrl = context.serviceUrl;
                dataService.supportServiceUrl = context.supportServiceUrl;
                dataService.token = context.token;
            }
        }

        function update($event) {
            $event.preventDefault();
            if (isValidDataDetail) {
                if ($scope.editForm.$valid) {
                    dataService.update(
                        context.id,
                        $scope.editData,
                        function (data) {
                            isValidDataDetail = true;
                            jsHelper.processMessage(data.message);
                            if (data.message.type === 0) {
                                $scope.method.refresh(data.data.transportCIChargeID);
                                $scope.editData = data.data;
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
            }
        }

        function deleteItem() {
            if (confirm('Are you sure ?')) {
                forwarderInvoiceService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.backUrl;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        }

        function refresh(id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        }

        function showContent() {
            jQuery('#content').show();
        }

        function showChangedIcon() {
            jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
        }

        function monitorDataChanged() {
            $scope.$watch("editData", function () {
                $scope.method.showChangedIcon();
            });
        }

        function processMessage(messageObj) {
            if (typeof messageObj === "undefined" || messageObj === null) return;

            if (messageObj.type === 2) {
                jsHelper.processMessage(messageObj);
            }
        }

        function getNewId() {
            $scope.newID--;
            return $scope.newID;
        }

        function getBodyScope() {
            return angular.element(jQuery("body")).scope();
        }

        function addInvoiceDetail($event) {
            $event.preventDefault();

            if ($scope.editData.transportCIChargeDetail === null) {
                $scope.editData.transportCIChargeDetail = [];
            }
            // push new row
            $scope.editData.transportCIChargeDetail.push({
                transportCIChargeDetailID: $scope.method.getNewId(),
                pricePerUnit: 0,
                numberOfUnits: 0,
                amount: 0
            });
        }

        function removeInvoiceDetail($event, id) {
            $event.preventDefault();
            $scope.editData.transportCIChargeDetail.splice(id, 1);
        }

        function getVATAmount(vatRate, netAmount) {
            $scope.editData.vatAmount = (vatRate / 100) * netAmount;
            $scope.editData.totalAmount = (netAmount + $scope.editData.vatAmount);
        }

        function getAmount(item) {
            if (item.numberOfUnits === 0) {
                item.amount = item.pricePerUnit;
            } else {
                item.amount = item.pricePerUnit * item.numberOfUnits;
            }
        }

        function getPricePerUnit(item, etd, clientID, polID) {
            debugger;
            if (item.containerTypeID === null || item.containerTypeID === 0) {
                return false;
            }
            if (item.chargeTypeID === 1 || item.chargeTypeID === 2) {
                $scope.pricePerUnit.method.getPricePerUnit(item, etd, clientID, polID);
            }
        }

        $scope.pricePerUnit = {
            data: null,
            filters: {
                filters: {
                    typeCharge: '',
                    typeContainer: '',
                    etd: '',
                    clientID: '',
                    polID: ''
                }
            },
            method: {
                getPricePerUnit: function (item, etd, clientID, polID) {
                    $scope.pricePerUnit.filters.filters = {
                        typeCharge: item.chargeTypeID,
                        typeContainer: item.containerTypeID,
                        etd: etd,
                        clientID: clientID,
                        polID: polID
                    },
                    dataService.getPricePerUnit(
                        $scope.pricePerUnit.filters,
                        function (data) {
                            $scope.pricePerUnit.data = data.data;
                            console.log($scope.pricePerUnit.data)
                            if ($scope.pricePerUnit.data !== null) {
                                item.amount = item.pricePerUnit = $scope.pricePerUnit.data.pricePerUnit;
                            }

                            $scope.$apply();
                        },
                        function (error) {
                        });
                }
            }
        }

    }
})();