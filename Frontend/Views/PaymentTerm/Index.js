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


var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {

    jsonService.searchFilter.pageSize = context.pageSize;
    jsonService.serviceUrl = context.serviceUrl;
    jsonService.token = context.token;

    jsonService.searchFilter.pageSize = context.pageSize;
    jsonService.serviceUrl = context.serviceUrl;
    jsonService.token = context.token;
    //
    // property
    //
    $scope.data = null;
    $scope.paymentTypes = null;
    $scope.yesNoValues = null;
    $scope.paymentMethods = null;
    $scope.filters = {
        PaymentTermNM: '',
        IsActive: '',
        PaymentMethod: '',
        PaymentTypeID: ''
    };
    $scope.pageIndex = 1;
    $scope.totalPage = 0;

    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
           
        },
        sort: function (sortedBy, sortedDirection) {
           
        },
        isTriggered: false
    };

    //
    // event
    //
    $scope.event = {
        clearFilter: function () {
            $scope.filters = {
                PaymentTermNM: '',
                IsActive: '',
                PaymentMethod: '',
                PaymentTypeID: ''
            };
            $scope.event.search();
        },
        reload: function () {
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.search();
        },
        search: function () {
            if ($scope.filters.PaymentTypeID == null) {
                $scope.filters.PaymentTypeID = '';
            }
            if ($scope.filters.IsActive == null) {
                $scope.filters.IsActive = '';
            }
            if ($scope.filters.PaymentMethod == null) {
                $scope.filters.PaymentMethod = '';
            }
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    $scope.data = data.data.data;
                    //console.log(data);
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
        delete: function (id) {
            if (confirm('Are you sure ?')) {
                jsonService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].paymentTermID == data.data) {
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
                    $scope.paymentMethods = data.data.paymentMethods;
                    $scope.paymentTypes = data.data.paymentTypes;
                    $scope.yesNoValues = data.data.yesNoValues;
                    $scope.$apply();

                    $scope.event.search();
                },
                function (error) {
                    $scope.paymentMethods = null;
                    $scope.paymentTypes = null;
                    $scope.yesNoValues = null;
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