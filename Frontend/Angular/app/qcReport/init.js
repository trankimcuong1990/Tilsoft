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
            qcReportService.searchFilter.pageIndex = scope.pageIndex;
            scope.event.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            jsonService.searchFilter.sortedDirection = sortedDirection;
            scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = scope.pageIndex;
            qcReportService.searchFilter.pageIndex = scope.pageIndex;
            jsonService.searchFilter.sortedBy = sortedBy;
            scope.event.search();
        });
    }
);

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = {
        FactoryOrderDetailID: 0,
        qcReportID: 0
    };
    $scope.filters = {
        ArticleCode: '',
        Season: '',
        ClientUD: '',
        FactoryID: '',
        ProformaInvoiceNo: '',
        PageIndex: 1
    };

    $scope.seasons = null;
    $scope.factories = null;

    //
    // event
    //
    $scope.event = {
        reload: function () {
            $scope.pageIndex = 1;
            qcReportService.searchFilter.pageIndex = 1;
            $scope.filters.PageIndex = 1;
            jQuery('.page-index').html('1');
            $scope.event.search();
        },
        search: function () {

            if ($scope.filters.FactoryID == null) {
                $scope.filters.FactoryID = '';
            }

            qcReportService.searchFilter.pageIndex = $scope.pageIndex;
            qcReportService.searchFilter.filters = $scope.filters;
            $scope.filters.PageIndex = $scope.pageIndex;

            qcReportService.search(
                function (data) {
                    $scope.data = data.data.data;
                    //console.log(qcReportService.searchFilter.pageSize);
                    $scope.totalPage = Math.ceil(data.data.totalRows / qcReportService.searchFilter.pageSize);
                    $scope.totalRows = data.data.totalRows;
                    $scope.$apply();

                    if (data.data.totalRows < qcReportService.searchFilter.pageSize) {
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
                    $scope.data.totalRows = 0;
                    $scope.$apply();
                }
            );
        },
        goNext: function (id) {
            window.location = context.nextURL + 'f=' + id;
        },
        init: function () {
            jsonService.getinitsearchfilter(
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
    }

    //
    // method
    //


    //
    // init
    //
    $scope.event.init();
}]);