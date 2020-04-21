//
// APP START
//

var searchResultGrid = jQuery('#searchResult').scrollableTable(
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.pageIndex = currentPage;
            dataService.searchFilter.pageIndex = scope.pageIndex;
            scope.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            dataService.searchFilter.sortedDirection = sortedDirection;
            scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = scope.pageIndex;
            dataService.searchFilter.sortedBy = sortedBy;
            scope.search();
        });
    }
);

jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.search();
    }
});


var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // data
    //
    $scope.models = null;
    $scope.supportModel = null;
    $scope.saler = [];
    $scope.yesNoValues = [
        { value: true, label: 'YES' },
        { value: false, label: 'NO' }
    ];
    $scope.filters = {
        OfferUD: '',
        Season: '',
        ClientUD: '',
        ClientNM: '',
        PaymentTermNM: '',
        DeliveryTermNM: '',
        Currency: '',
        ForwarderNM: '',
        PODNM: '',
        ArticleCode: '',
        Description: '',
        V4ID: '',
        OfferStatus: '',
        IsPotential: '',
        SaleID: ''
    };
    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;

    //
    // event defined
    //
    $scope.event = {
        search: function () {
            dataService.searchFilter.filters = $scope.filters;
            $scope.search();
        },
        exportExcelOffer: function ($event) {
            $event.preventDefault();

            dataService.exportExcelOffer({
                filters: {
                    OfferUD: $scope.filters.OfferUD,
                    Season: $scope.filters.Season,
                    ClientUD: $scope.filters.ClientUD,
                    ClientNM: $scope.filters.ClientNM,
                    PaymentTermNM: $scope.filters.PaymentTermNM,
                    DeliveryTermNM: $scope.filters.DeliveryTermNM,
                    Currency: $scope.filters.Currency,
                    ForwarderNM: $scope.filters.ForwarderNM,
                    PODNM: $scope.filters.PODNM,
                    ArticleCode: $scope.filters.ArticleCode,
                    Description: $scope.filters.Description,
                    V4ID: $scope.filters.V4ID,
                    OfferStatus: $scope.filters.OfferStatus,
                    IsPotential: $scope.filters.IsPotential,
                    SaleID: $scope.filters.SaleID,
                }
            },
                function (data) {
                    window.location = context.serviceReport + data.data;
                },
                function (error) {
                });
        },
    }

    //
    // private function defined
    //
    $scope.prepareForm = function () {
        dataService.getSearchSupport(
            function (data) {            
                $scope.supportModel = data.data;
                $scope.saler = data.data.sales;
                //$scope.$apply();

                $scope.search();
            },
            function (error) {
                $scope.supportModel = null;
               // $scope.$apply();
            }
        );
    }

    $scope.search = function () {
        dataService.search(
            function (data) {
                $scope.models = data.data.offerSearchResultDTOs;
                $scope.totalRows = data.totalRows;
                $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                //$scope.$apply();

                if (data.totalRows < dataService.searchFilter.pageSize) {
                    jQuery('#searchResult').find('ul').hide();
                }
                else {
                    jQuery('#searchResult').find('ul').show();
                }

                jQuery('#content').show();
                searchResultGrid.updateLayout();
            },
            function (error) {
                $scope.models = null;
                $scope.pageIndex = 1;
                $scope.totalPage = 0;
                //$scope.$apply();
            }
        );
    }

    $scope.delete = function (id, $event) {
        $event.preventDefault();
        if (confirm('Are you sure ?')) {
            dataService.delete(id,
                function (data) {
                    jsHelper.processMessage(data.message);

                    if (data.message.type == 0) {
                        var j = -1;
                        for (var i = 0; i < $scope.models.length; i++) {
                            if ($scope.models[i].offerID == data.data.offerSearchResultDTOs) {
                                j = i;
                                break;
                            }
                        }

                        if (j >= 0) {
                            $scope.models.splice(j, 1);
                        }

                        //$scope.$apply();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    }

    //
    // INIT
    //
    $scope.prepareForm();
}]);