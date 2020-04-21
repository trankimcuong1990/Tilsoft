//
// SUPPORT
//
jQuery('.search-filter').keypress(function(e){
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

var searchResultGrid = jQuery('#searchResult').scrollableTable(
    function(currentPage){
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function(){
            scope.pageIndex = currentPage;
            jsonService.searchFilter.pageIndex = scope.pageIndex;
            scope.event.search();
        });
    },
    function(sortedBy, sortedDirection){
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function(){
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
    $scope.filters = {
        FactoryUD: '',
        ProformaInvoiceNo: '',
        Season: '',
    };
    $scope.seasons = null;
    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;

    //
    // event
    //
    $scope.event = {
        reload: function(){
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.search();
        },
        search: function(){
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    $scope.data = data.data.data;
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    $scope.$apply();

                    if(data.totalRows < jsonService.searchFilter.pageSize) {
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
        },
        delete: function (id){
            if (confirm('Are you sure ?')) {
                jsonService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if(data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].factoryProformaInvoiceID == data.data) {
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
        download: function (url) {
            window.open(url);
        },
        init : function(){
            jsonService.getsearchfilter(
                function (data) {
                    $scope.seasons = data.data.seasons;
                    $scope.$apply();

                    $scope.event.search();
                },
                function (error) {
                    $scope.seasons = null;
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