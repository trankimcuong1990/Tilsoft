(function () {
    'use strict';

    //
    // APP START
    //
    angular
        .module('tilsoftApp', ['widgets'])
        .controller('tilsoftController', ['$scope', PurchasingPriceOverviewRptController]);


    function PurchasingPriceOverviewRptController($scope) {
        //
        // property
        //
        $scope.data = null;
        $scope.filters = {
            FactoryID: '',
            ModelUD: '',
            ModelNM: '',
            ClientUD: '',
            Season: ''
        };
        var searchFilters = {
            filters: {},
            sortedBy: 'StatusUpdatedDate',
            sortedDirection: 'DESC',
            pageSize: '',
            pageIndex: ''
        };
        $scope.seasons = null;
        $scope.factories = null;

        //
        // event
        //
        $scope.event = {
            reload: reload,
            search: search,
            init: init,
            generateExcel: generateExcel
        };

        //
        // init
        //
        $scope.event.init();

        //
        // method
        //
        function reload() {
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.search();
        }

        function search() {
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    $scope.data = data.data.data;
                    injectShowLinks($scope.data);
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    $scope.$apply();

                    if (data.totalRows < jsonService.searchFilter.pageSize) {
                        jQuery('#searchResult').find('ul').hide();
                        jQuery('#searchResult').find('span.more-info').hide();
                    }
                    else {
                        jQuery('#searchResult').find('ul').show();
                        jQuery('#searchResult').find('span.more-info').css('display', 'inline-block');
                    }

                    jQuery('#content').show();
                    searchResultGrid.updateLayout();
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                    $scope.$apply();
                }
            );

            function injectShowLinks(data) {
                if (data === null || typeof (data) === "undefined") return;

                data.forEach(function (item) {
                    item.showUpdatorLink = item.updatorID !== null && item.updatorName !== null;
                });
            }
        }

        function init() {
            jsonService.getsearchfilter(
                function (data) {
                    $scope.seasons = data.data.seasons;
                    $scope.factories = data.data.factories;
                    $scope.$apply();

                    $scope.event.search();
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.factories = null;
                    $scope.$apply();
                }
            );
        }

        function generateExcel() {
            searchFilters.filters = $scope.filters;
            jsonService.getExcelReport(
                searchFilters,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    }

    //
    // SUPPORT
    //
    jQuery('.search-filter').keypress(function (e) {
        if (e.keyCode == 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.reload();
        }
    });

    var searchResultGrid = jQuery('#searchResult').scrollableTable(
        function (currentPage) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                scope.pageIndex = currentPage;
                jsonService.searchFilter.pageIndex = scope.pageIndex;
                scope.event.search();
            });
        },
        function (sortedBy, sortedDirection) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                jsonService.searchFilter.sortedDirection = sortedDirection;
                scope.pageIndex = 1;
                jsonService.searchFilter.pageIndex = scope.pageIndex;
                jsonService.searchFilter.sortedBy = sortedBy;
                scope.event.search();
            });
        }
    );
})();


