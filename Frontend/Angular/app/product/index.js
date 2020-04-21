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

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.productTypes = null;
    $scope.seasons = null;
    $scope.confirmStatuses = null;
    $scope.filters = {
        ArticleCode: '',
        Description: '',
        Season: '',
        CataloguePageNo: '',
        ProductTypeID: '',
        IsConfirmed: '',
        EANCode: ''
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
            if ($scope.filters.ProductTypeID == null) {
                $scope.filters.ProductTypeID = '';
            }
            if ($scope.filters.IsConfirmed == null) {
                $scope.filters.IsConfirmed = '';
            }
            if ($scope.filters.Season == null) {
                $scope.filters.Season = '';
            }

            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    $scope.data = data.data.data; console.log($scope.data);
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.$apply();

                    if (data.totalRows < jsonService.searchFilter.pageSize) {
                        jQuery('#searchResult').find('ul').hide();
                    }
                    else {
                        jQuery('#searchResult').find('ul').show();
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
        delete: function () {
            if (confirm('Are you sure ?')) {
                jsonService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].productID == data.data) {
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
        init: function () {
            jsonService.getsearchfilter(
               function (data) {
                   $scope.seasons = data.data.seasons;
                   $scope.productTypes = data.data.productTypes;
                   $scope.confirmStatuses = data.data.confirmStatuses;
                   $scope.$apply();

                   $scope.event.search();
               },
               function (error) {
                   $scope.supportModel = null;
                   $scope.$apply();
               }
           );
        },

        printPS: function ($event, productID) {
            debugger;
            $event.preventDefault();           
            jsonService.printPS(productID,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type != 2) {
                        window.location = context.backendReportUrl + data.data;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );           
        }
    }

    //
    // method
    //


    //
    // INIT
    //
    $scope.event.init();
}]);