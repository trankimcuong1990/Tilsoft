﻿jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});
//
// SUPPORT
//
jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});
var searchResultGrid = jQuery('#grdSearchResult').scrollableTable(
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.pageIndex = currentPage;
            jsonService.searchFilter.pageIndex = currentPage;
            scope.event.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = 1;
            jsonService.searchFilter.sortedBy = sortedBy;
            jsonService.searchFilter.sortedDirection = sortedDirection;
            scope.event.search();
        });
    }
);

var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.filters = {
        clientUD: '',
        articleCode: '',
        description: '',
    };
    $scope.pageIndex = 1;
    $scope.totalPage = 0;

    $scope.data = null;
    $scope.seasons = null;

    //
    // event
    //
    $scope.event = {
        reload: function () {
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = 1;
            jsonService.searchFilter.sortedBy = 'clientUD';
            jsonService.searchFilter.pageSize = 10;
            $scope.event.search();
        },
        search: function () {
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.getListClient(
                function (data) {
                    $scope.data = data.data.data;
                    $scope.seasons = data.data.seasons;

                    $scope.totalPage = Math.ceil(data.data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.$apply();

                    if (data.data.totalRows < jsonService.searchFilter.pageSize) {
                        jQuery('#grdSearchResult').find('ul').hide();
                    }
                    else {
                        jQuery('#grdSearchResult').find('ul').show();
                    }
                    jQuery('#content').show();

                    if (data.message.type == 2) {
                        jsHelper.processMessage(data.message);
                    }

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

        createOrderNorm: function (dtoOrder) {
            jsonService.createOrderNorm(dtoOrder,
                function (data) {
                    jsHelper.processMessage(data.message);
                },
                function (error) {
            });
        },
    }

    $scope.event.reload();
}]);