//
// Support
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
//
// AP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.ledgerAccounts = [];
    $scope.filters = {
        AccountNo: '',
        VietnameseNM: '',
        EnglishNM: ''
    };
    $scope.pageIndex = 1;
    $scope.totalPage = 0;

    //
    // event
    //
    $scope.event = {
        reload: function () {
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.search();
        },
        search: function () {
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    $scope.data = data.data.data;
                    $scope.data.ledgerAccountDetailOverviews = data.data.data.ledgerAccountDetailOverviews;
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
                    $scope.$apply();
                }
            );
        },
        delete: function (id) {
            if (confirm('Are you sure ?')) {
                jsonService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].ledgerAccountID == data.data) {
                                    j = i;
                                    break;
                                }
                            }

                            if (j >= 0) {
                                $scope.data.splice(j, 1);
                            }

                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        closeEntry: function () {
            if (confirm('Account movement balance ?')) {
                jsonService.closeEntry(
                    $scope.data,
                    function (data) {
                        $scope.data = data.data.data;
                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        init: function () {
            $scope.event.search();
        },
        generateExcel: function () {
            jsonService.getExcelReport(
                function (data) {
                    window.location = context.backendReportUrl + data.data + '.xlsm';
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        getDetailOverview: function ($event, accountNo) {
            $event.preventDefault();
            if ($("#icon-view-pi-" + accountNo).hasClass('fa-plus-square-o'))
            {
                //load data
                var arr = $scope.data.filter(function (o) { return o.accountNo == accountNo });
                jsonService.getDetailOverview(accountNo,
                    function (data) {
                        arr[0].ledgerAccountDetailOverviews = data;
                        $scope.data.ledgerAccountDetailOverviews = data;
                        //console.log($scope.data);
                        $scope.$apply();

                        //show on view
                        $("#" + accountNo).toggle();
                        $("#icon-view-pi-" + accountNo).removeClass('fa-plus-square-o');
                        $("#icon-view-pi-" + accountNo).addClass('fa-minus-square-o');
                    },
                    function (error) {
                        arr[0].ledgerAccountDetailOverviews = null;
                        $scope.$apply();
                    }
                );
            }
            else if ($("#icon-view-pi-" + accountNo).hasClass('fa-minus-square-o'))
            {
                $("#" + accountNo).toggle();
                $("#icon-view-pi-" + accountNo).removeClass('fa-minus-square-o');
                $("#icon-view-pi-" + accountNo).addClass('fa-plus-square-o');
            }
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