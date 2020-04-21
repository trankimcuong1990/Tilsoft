jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives', 'furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', function ($scope, $cookieStore) {

    $scope.data = null;
    $scope.factoryOrders = [];

    $scope.filters = {
        workOrderUD:null,
        proformaInvoiceNo: null,
        clientUD:null,
        fromLDS: null,
        toLDS: null
    };
    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {},
        sort: function (sortedBy, sortedDirection) { },
        isTriggered: false
    };
    //
    // event
    //
    $scope.event = {
        reload: function () {
            //search data
            $scope.event.search();
        },
        search: function (isLoadMore) {
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    $scope.data = data.data;
                    console.log($scope.data.factoryOrderProductionStatuses);
                    $scope.factoryOrders = [];
                    //create factory order list
                    var indexs = [];
                    angular.forEach($scope.data.factoryOrderProductionStatuses, function (item) {
                        keyID = item.factoryOrderDetailID.toString();
                        if (indexs.indexOf(keyID) < 0) {
                            indexs.push(keyID);
                            var foItem = {
                                factoryOrderDetailID: item.factoryOrderDetailID,
                                proformaInvoiceNo: item.proformaInvoiceNo,
                                clientUD: item.clientUD,
                                lds: item.lds,
                                factoryUD: item.factoryUD,
                                articleCode: item.articleCode,
                                description: item.description,
                                orderQnt: item.orderQnt,

                                workOrders: [],
                            }
                            //creat factory order list
                            $scope.factoryOrders.push(foItem);
                            //create work order list
                            angular.forEach($scope.data.factoryOrderProductionStatuses, function (wItem) {
                                if (wItem.factoryOrderDetailID == foItem.factoryOrderDetailID) {
                                    foItem.workOrders.push(wItem);
                                }
                            });
                        }
                    });
                    console.log($scope.factoryOrders);
                    $scope.$apply();
                    jQuery('#content').show();
                },
                function (error) {
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },
    }

    $scope.method = {
    }
    $scope.event.reload();
}]);