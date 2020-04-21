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
            addTransportCostItem: addTransportCostItem,
            removeTransportCostItem: removeTransportCostItem,
            addInvoiceDetail: addInvoiceDetail,
            removeInvoiceDetail: removeInvoiceDetail,
            calculateAmount: calculateAmount,
            checkValidWithBLNo: checkValidWithBLNo,
            checkForContainerNo: checkForContainerNo,
            loadAllContainersWithBooking: loadAllContainersWithBooking
        };
        $scope.method = {
            refresh: refresh,
            showContent: showContent,
            showChangedIcon: showChangedIcon,
            monitorDataChanged: monitorDataChanged,
            processMessage: processMessage,
            getNewId: getNewId,
            getBodyScope: getBodyScope,
            getContainerTypeNM: getContainerTypeNM//,
            //getPricePerUnit: getPricePerUnit
        };

        var isValidDataDetail = true;
        var keepForwarderNM = '';
        var keepBookingNM = '';

        //#region quickSearchForwarder
        var quickSearchForwarderGrid = jQuery("#quickSearchForwarderTable").scrollableTable(
            function (currentPage) {
                var scope = $scope.method.getBodyScope();
                scope.$apply(function () {
                    scope.quickSearchForwarder.filters.pageIndex = currentPage;
                    scope.quickSearchForwarder.method.searchForwarder();
                });
            },
            function (sortedBy, sortedDirection) {
                //do nothing
            }
        );

        $scope.searchForwarderTimer = null;
        $scope.quickSearchForwarder = {
            data: null,
            filters: {
                filters: {
                    searchQuery: ""
                },
                sortedBy: "ForwarderNM",
                sortedDirection: "ASC",
                pageSize: 10,
                pageIndex: 1
            },
            totalPage: 0,

            method: {
                searchForwarder: function () {
                    dataService.quickSearchForwarder(
                        $scope.quickSearchForwarder.filters,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.quickSearchForwarder.data = data.data;
                                $scope.quickSearchForwarder.totalPage = Math.ceil(data.totalRows / $scope.quickSearchForwarder.filters.pageSize);

                                quickSearchForwarderGrid.updateLayout();
                                $scope.$apply();
                                jQuery('#forwarder-popup').show();
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
                    if (jQuery("#txtForwarder").val().length >= 3) {
                        clearTimeout($scope.searchForwarderTimer);
                        $scope.searchBookingTimer = setTimeout(
                            function () {
                                $scope.quickSearchForwarder.filters.filters.searchQuery = jQuery("#txtForwarder").val();
                                $scope.quickSearchForwarder.filters.pageIndex = 1;
                                $scope.quickSearchForwarder.method.searchForwarder();
                            },
                            500);
                    }
                },
                close: function ($event, clearSearchBox) {
                    jQuery("#forwarder-popup").hide();
                    if (clearSearchBox) {
                        $scope.editData.forwarderID = null;
                        $scope.editData.forwarderNM = null;
                    }
                    $scope.quickSearchForwarder.data = null;
                    $event.preventDefault();
                },
                ok: function ($event, id) {
                    $scope.editData.forwarderID = id;
                    jQuery.each($scope.quickSearchForwarder.data, function () {
                        if (this.forwarderID === id) {
                            $scope.editData.forwarderID = this.forwarderID;
                            $scope.editData.forwarderNM = this.forwarderNM;
                        }
                    });
                    $scope.quickSearchForwarder.event.close($event, false);
                }
            }
        }
        //#endregion quickSearchForwarder

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
                    searchQuery: ""
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
                                $scope.quickSearchBooking.data = data.data.data; //console.log(data.data.data)
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
                    if (jQuery("#txtBooking").val().length >= 3) {
                        clearTimeout($scope.searchBookingTimer);
                        $scope.searchBookingTimer = setTimeout(
                            function () {
                                $scope.quickSearchBooking.filters.filters.searchQuery = jQuery("#txtBooking").val();
                                $scope.quickSearchBooking.filters.filters.forwarderID = $scope.editData.forwarderID;
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
                            if (scope.editData.bookingID !== this.bookingID) {
                                scope.editData.transportCostForwarderItems = [];
                                //scope.containerTypeNM = '';
                            }

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
                                $scope.containers.data = data.data;
                                //console.log($scope.containers.data)
                            }
                        },
                        function (error) {

                        });
                }
            }
        }
        //#endregion get dropdown container

        //#region pop-up quickSearchForwarder
        jQuery('#txtForwarder').on('blur', function () {
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
        });
        //#endregion pop-up quickSearchBooking

        activate();

        ////////////////                                           

        function activate() {

            initDataService();

            dataService.load(
                context.id,
                function (data) {

                    $scope.editData = data.data.data; console.log(data.data)
                    //$scope.editData.currency = "USD";
                    $scope.typeOfCosts = data.data.typeOfCosts;
                    $scope.containerTypes = data.data.containerTypes;
                    $scope.containers.data = data.data.containers;
                    $scope.listCurrency = data.data.dropDownCurrency; //console.log($scope.listCurrency)
                    $scope.$apply();

                    $scope.method.showContent();
                    $scope.method.monitorDataChanged();
                    $scope.method.processMessage(data.message);
                },
                function (error) {
                    jsHelper.showMessage("warning", error);

                    $scope.editData = null;
                    $scope.typeOfCosts = null;
                    $scope.listCurrency = null;
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
                                $scope.method.refresh(data.data.transportCostForwarderID);
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

        function addTransportCostItem() {
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

        function removeTransportCostItem($event, id) {
            $event.preventDefault();

            isExist = false;
            for (j = 0; j < $scope.dataContainer.forwarderInvoiceDetails.length; j++) {
                if ($scope.dataContainer.forwarderInvoiceDetails[j].forwarderInvoiceDetailID === id) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.dataContainer.forwarderInvoiceDetails.splice(j, 1);
            }
        }

        //#region add, remove invoice detail
        function addInvoiceDetail($event) {
            $event.preventDefault();

            jQuery('#notificationMessage article').html('');
            jQuery('#notificationMessage').hide();

            if ($scope.editData.forwarderID === null) {
                //jsHelper.showMessage('warning', 'Please input value of Forwarder');
                return false;
            }

            if ($scope.editData.bookingID === null) {
                //jsHelper.showMessage('warning', 'Please input value of Booking');
                return false;
            }

            // init forwarder item detail
            if ($scope.editData.transportCostForwarderItems === null) {
                $scope.editData.transportCostForwarderItems = [];
            }
            // push new row
            $scope.editData.transportCostForwarderItems.push({
                transportCostForwarderItemID: $scope.method.getNewId(),
                typeOfCost: 0,
                containerNumber: 0,
                containerType: 0,
                currencyValue: 0,
                pricePerUnit: 0,
                numberOfUnits: 0,
                amount: 0
            });
            //console.log($scope.editData.transportCostForwarderItems)
        }

        function removeInvoiceDetail($event, id) {
            $event.preventDefault();
            $scope.editData.transportCostForwarderItems.splice(id, 1);
        }

        function loadAllContainersWithBooking($event) {
            var isLoadAgain = true;

            if (scope.editData.transportCostForwarderItems !== null && scope.editData.transportCostForwarderItems.length > 0) {
                if (!confirm('Do you want delete data on screen to load new data?')) {
                    isLoadAgain = false;
                }
            }

            if (isLoadAgain) {
                scope.editData.transportCostForwarderItems = [];
                console.log(scope.containers.data)
                for (var i = 0; i < scope.containers.data.length; i++) {
                    scope.editData.transportCostForwarderItems.push({
                        transportCostForwarderItemID: scope.method.getNewId(),
                        containerNumber: scope.containers.data[i].containerNo,
                        containerType: scope.containers.data[i].containerTypeID,
                        containerNm: getContainerTypeNM(scope.containers.data[i].containerTypeID),
                        pricePerUnit: 0,
                        numberOfUnits: 0,
                        amount: 0
                    });
                }
            }
        }
        //#endregion

        //#region calculate amount
        function calculateAmount(item, ratePerSeason) {
            if (item.pricePerUnit === null || item.pricePerUnit === '') {
                item.pricePerUnit = 0;
            }
            if (item.numberOfUnits === null || item.numberOfUnits === '') {
                item.numberOfUnits = 0;
            }
            if (item.pricePerUnit !== 0 && item.numberOfUnits === 0) {
                item.amount = item.pricePerUnit;
            } else {
                item.amount = item.pricePerUnit * item.numberOfUnits;
            }

            /*if (item.currencyValue === 1) {

            }*/
        };
        //#endregion calculate amount

        //#region check valid data detail with BLNo.
        function checkValidWithBLNo(item, index) {
            isValidDataDetail = true;

            for (var i = 0; i < $scope.editData.transportCostForwarderItems.length; i++) {
                if (index === i) {
                    continue;
                }

                var row = $scope.editData.transportCostForwarderItems[i];

                if ((item.typeOfCost === row.typeOfCost) && (item.containerNumber === row.containerNumber) && (item.containerType === row.containerType)) {
                    isValidDataDetail = false;
                    break;
                }
            }

            if (isValidDataDetail) {
                jQuery('#notificationMessage article').html('');
                jQuery('#notificationMessage').hide();

                /**
                 * Clear Container No and Container Type
                 */
                if (item.typeOfCost === 5 || item.typeOfCost === 6) {
                    item.containerNumber = 0;
                    item.containerType = 0;
                    item.containerNm = '';
                }

                /**
                 * Get Price Per Unit with "Sea freight"
                 */
                if (item.typeOfCost === 1) {
                    // Check Container No
                    if (item.containerNumber === null || item.containerNumber === 0) {
                        //jQuery('#notificationMessage').show();
                        //jsHelper.showMessage('warning', 'Please selected value Container No');
                        return false;
                    }

                    // Check Container Type
                    if (item.containerType === null || item.containerType === 0) {
                        //jQuery('#notificationMessage').show();
                        //jsHelper.showMessage('warning', 'Please selected value Container Type');
                        return false;
                    }

                    // Call api get "Price Per Unit"
                    $scope.priceUnit.method.getPricePerUnit(item, scope.editData.etd, scope.editData.forwarderID, scope.editData.polid);
                } else {
                    item.amount = item.pricePerUnit = 0;
                }
            } else {
                jsHelper.showMessage('warning', 'Data detail is exist ');
            }
        }
        //#endregion check valid data detail with BLNo.

        function checkForContainerNo(item, index) {
            for (var i = 0; i < $scope.containers.data.length; i++) {
                if ($scope.containers.data[i].containerNo === item.containerNumber) {
                    item.containerType = $scope.containers.data[i].containerTypeID;
                    break;
                }
            }
            if (item.containerNumber !== null) {
                for (var j = 0; j < $scope.containerTypes.length; j++) {
                    if ($scope.containerTypes[j].containerTypeID === item.containerType) {
                        item.containerNm = $scope.containerTypes[j].containerTypeNM;
                        break;
                    }
                }
            } else {
                item.containerNm = '';
            }

            checkValidWithBLNo(item, index);
        }

        function getContainerTypeNM(typeId) {
            for (var j = 0; j < scope.containerTypes.length; j++) {
                if (scope.containerTypes[j].containerTypeID === typeId) {
                    return scope.containerTypes[j].containerTypeNM;
                }
            }
        }

        //#region Get price per unit with "Type Of Cost", "Type Of Container", "ETD", "Forwarder", "Port of Departure"
        $scope.priceUnit = {
            data: null,
            filters: {
                filters: {
                    TypeCost: 0,
                    TypeContainer: 0,
                    ETD: '',
                    Forwarder: 0,
                    POL: 0
                }
            },
            method: {
                getPricePerUnit: function (item, etd, forwarderId, polid) {
                    // Get filter
                    $scope.priceUnit.filters.filters = {
                        TypeCost: item.typeOfCost,
                        TypeContainer: item.containerType,
                        ETD: etd,
                        Forwarder: forwarderId,
                        POL: polid
                    },
                    // Call api
                    dataService.getPricePerUnit(
                        $scope.priceUnit.filters,
                        function (data) { // Success
                            $scope.priceUnit.data = data.data;
                            item.amount = item.pricePerUnit = $scope.priceUnit.data;
                            $scope.$apply();
                        },
                        function (error) { // Fail
                            // do nothing
                        });
                }
            }
        }
        //#endregion Get price per unit with "Type Of Cost", "Type Of Container", "ETD", "Forwarder", "Port of Departure"
    }
})();