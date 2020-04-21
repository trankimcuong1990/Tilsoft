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
        invoiceNo: '',
        blNo: '',
        creditNoteNo: '',
        factoryID: null,
    };
    $scope.pageIndex = 1;
    $scope.totalPage = 0;

    $scope.data = null;
    $scope.factories = null;

    //
    // event
    //
    $scope.event = {
        reload: function () {
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = 1;
            jsonService.searchFilter.sortedBy = 'creditNoteNo';
            $scope.event.search();
        },
        search: function () {            
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    $scope.data = data.data.data;
                    $scope.factories = data.data.dataFactory;
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.$apply();

                    if (data.totalRows < jsonService.searchFilter.pageSize) {
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

        delete: function (id) {
            if (confirm('Are you sure you want to delete purchasing credit note ?')) {
                jsonService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].purchasingCreditNoteID == data.data) {
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

        getListFactory: function () {
            jsonService.getListFactory(
                function (data) {
                    $scope.factories = data.data;
                    $scope.$apply();

                    if (data.message.type == 2) {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    $scope.factories = null;
                    $scope.$apply();
                }
            );
        },


    }
    //$scope.event.getListFactory();
    $scope.event.reload();
}]);