angular
.module('tilsoftApp', [])
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
.controller('tilsoftController', ['$scope', 'dataService', function($scope, dataService) {
    
    dataService.serviceUrl = context.serviceUrl;
    dataService.supportServiceUrl = context.supportServiceUrl;
    dataService.token = context.token;

    //#region

    $scope.data = [];
    $scope.currency = [];
    $scope.charge = [];
    $scope.pol = [];
    $scope.pod = [];
    $scope.containertype = [];

    $scope.newDetailID = -1;

    //#endregion

    //#region
    $scope.event = {
        init: function () {
            dataService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data; console.log($scope.data)
                    $scope.currency = data.data.supportCurrency;
                    $scope.charge = data.data.supportCharge;
                    $scope.pol = data.data.supportPOL;
                    $scope.pod = data.data.supportPOD;
                    $scope.containerType = data.data.supportContainerType;
                    
                    jQuery('#content').show();
                },
                function (error) {
                });
        },
        addDetails: function () {
            if ($scope.data.priceListClientChargeDetail === null) {
                $scope.data.priceListClientChargeDetail = [];
            }

            $scope.data.priceListClientChargeDetail.push({
                priceListClientChargeDetailID: --$scope.newDetailID,
                polid: null,
                podid: null,
                containerTypeID: null,
                pricePerUnit: null
            });
        },
        deleteDetails: function (id) {
            $scope.data.priceListClientChargeDetail.splice(id, 1);
        },
        update: function () {
            if ($scope.editForm.$valid) {
                dataService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            $scope.event.refresh(data.data.data.priceListClientChargeID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }/* else {
            } */
        },
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        }
    };
    //#endregion

    //#region quick search client
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
                        $scope.quickSearchClient.data = data.data; console.log($scope.quickSearchClient.data)
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
                if (jQuery('#clientNM').val().length >= 3) {
                    clearTimeout($scope.searchClientTimer);
                    $scope.searchClientTimer = setTimeout(
                    function () {
                        $scope.quickSearchClient.filters.filters.searchQuery = jQuery('#clientNM').val();
                        $scope.quickSearchClient.filters.pageIndex = 1;
                        $scope.quickSearchClient.method.searchClient();
                    }, 500);
                }
            },
            close: function (clearSearchBox) {
                jQuery('#clientPopup').hide();

                if (clearSearchBox) {
                    $scope.data.clientID = null;
                    $scope.data.clientUD = null;
                    $scope.data.clientNM = null;
                }

                $scope.quickSearchClient.data = [];
            },
            ok: function (id) {
                $scope.data.clientID = id;

                jQuery.each($scope.quickSearchClient.data, function () {
                    if (this.clientID === id) {
                        $scope.data.clientID = this.clientID;
                        $scope.data.clientUD = this.clientUD;
                        $scope.data.clientNM = this.clientNM;
                    }
                });

                $scope.quickSearchClient.event.close(false);
            }
        }
    }
    //#endregion

    $scope.event.init();
    
}]);