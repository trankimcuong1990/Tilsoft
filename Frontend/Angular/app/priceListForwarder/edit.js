angular
    .module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies'])
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
    .controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {
        // #region initialize data service
        dataService.serviceUrl = context.serviceUrl;
        dataService.supportServiceUrl = context.supportServiceUrl;
        dataService.token = context.token;
        // #endregion

        // #region variable and constant
        $scope.data = [];
        $scope.typeOfCosts = [];
        $scope.typeOfCurrencys = [];
        $scope.poLs = [];
        $scope.poDs = [];
        $scope.typeOfContainers = [];
        $scope.newId = -1;
        // #endregion

        // #region event
        $scope.event = {
            init: function () {
                dataService.load(
                    context.id,
                    null,
                    function (data) {
                        $scope.data = data.data.data; //console.log($scope.data)
                        $scope.typeOfCosts = data.data.typeCosts;
                        $scope.typeOfCurrencys = data.data.typeCurrencys;
                        $scope.poLs = data.data.poLs;
                        $scope.poDs = data.data.poDs;
                        $scope.typeOfContainers = data.data.typeContainers;

                        jQuery('#content').show();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.data = null;
                    });
            },
            addDetail: function ($event) {
                $event.preventDefault();

                if ($scope.data.priceListForwarderDetails === null) {
                    $scope.data.priceListForwarderDetails = [];
                }

                $scope.data.priceListForwarderDetails.push({
                    priceListForwarderDetailID: $scope.newId--,
                    pricePerUnit: 0
                });
            },
            deleteDetail: function ($event, position) {
                $event.preventDefault();
                $scope.data.priceListForwarderDetails.splice(position, 1);
            },
            update: function ($event) {
                $event.preventDefault();
                if ($scope.editForm.$valid) {
                    dataService.update(
                        context.id,
                        $scope.data,
                        function (data) {
                            jsHelper.processMessage(data.message);
                            if (data.message.type === 0) {
                                window.location = context.refreshUrl + data.data.data.priceListForwarderID;
                            }
                        },
                        function (error) {
                            jsHelper.showMessage("warning", error.data.message.detailMessage[1]);
                        });
                } else {
                    jsHelper.showMessage("warning", context.errMsg);
                }
            },
            changeCost: function ($event, typeCostID) {
                // check type of cost is null or 0
                if (typeCostID === undefined || typeCostID === null || typeCostID === 0) {
                    $scope.data.typeOfCurrencyID = 0;
                } else {
                    if (typeCostID === 12) {
                        $scope.data.typeOfCurrencyID = 2;
                    } else {
                        $scope.data.typeOfCurrencyID = 1;
                    }
                }

            }
        }
        // #endregion

        // #region check validation data input
        $scope.method = {
            isValid: function (form) {

            }
        };
        // #endregion 

        // #region quick search forwarder
        var quickSearchForwarderPopup = jQuery('#quickSearchForwarderTable').scrollableTable(
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
            data: [],
            filters: {
                filters: {
                    searchQuery: ''
                },
                sortedBy: 'ForwarderNM',
                sortedDirection: 'ASC',
                pageSize: 10,
                pageIndex: 1
            },
            totalPage: 0,
            // method
            method: {
                searchForwarder: function () {
                    dataService.quickSearchForwarder(
                        $scope.quickSearchForwarder.filters,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.quickSearchForwarder.data = data.data;
                                $scope.quickSearchForwarder.totalPage = Math.ceil(data.totalRows / $scope.quickSearchForwarder.filters.pageSize);

                                quickSearchForwarderPopup.updateLayout();

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
            // event
            event: {
                searchBoxKeyUp: function () {
                    if (jQuery('#in-forwarder').val().length >= 3) {
                        clearTimeout($scope.searchForwarderTimer);
                        $scope.searchBookingTimer = setTimeout(
                            function () {
                                $scope.quickSearchForwarder.filters.filters.searchQuery = jQuery('#in-forwarder').val();
                                $scope.quickSearchForwarder.filters.pageIndex = 1;
                                $scope.quickSearchForwarder.method.searchForwarder();
                            },
                            500);
                    }
                },
                close: function ($event, clearSearchBox) {
                    jQuery('#forwarder-popup').hide();

                    if (clearSearchBox) {
                        $scope.data.forwarderID = null;
                        $scope.data.forwarderNM = null;
                    }

                    $scope.quickSearchForwarder.data = null;

                    $event.preventDefault();
                },
                ok: function ($event, id) {
                    $scope.data.forwarderID = id;

                    jQuery.each($scope.quickSearchForwarder.data, function () {
                        if (this.forwarderID === id) {
                            $scope.data.forwarderID = this.forwarderID;
                            $scope.data.forwarderNM = this.forwarderNM;
                        }
                    });

                    $scope.quickSearchForwarder.event.close($event, false);
                }
            }
        };
        // #endregion quick search forwarder

        // #region default initialize
        $scope.event.init();
        // #endregion
    }]);