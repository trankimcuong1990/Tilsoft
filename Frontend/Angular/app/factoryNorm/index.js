jQuery('.search-filter').keypress(function (e) {
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
        modelUD: '',
        modelNM: ''
    };
    $scope.pageIndex = 1;
    $scope.totalPage = 0;

    $scope.data = null;

    //
    // event
    //
    $scope.event = {
        reload: function () {
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = 1;
            jsonService.searchFilter.sortedBy = 'ModelNM';
            $scope.event.search();
        },
        search: function () {
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    $scope.data = data.data.data;
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.$apply();

                    if (data.totalRows < jsonService.searchFilter.pageSize) {
                        jQuery('#grdSearchResult').find('ul').hide();
                    }
                    else {
                        jQuery('#grdSearchResult').find('ul').show();
                    }
                    jQuery('#content').show();

                    if (data.message.type == 2)
                    {
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
        delete: function (id, index, $event) {
            $event.preventDefault();
            bootbox.confirm({
                message: 'Are you sure you want to delete ?',
                size : 'small',
                buttons: {
                    confirm: {
                        label: 'Yes',
                        className : 'btn-primary'
                    },
                    cancel: {
                        label: 'No',
                        className : 'btn-danger'
                    }
                },
                callback: function (result) {
                    if (result) {
                        jsonService.delete(id,
                            function (data) {
                                jsHelper.processMessage(data.message);
                                if (data.message.type == 0) {
                                    $scope.data.splice(index, 1);
                                    $scope.$apply();
                                }
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error);
                            }
                        );
                    }
                }
            });
        },


    }

    $scope.event.reload();
}]);