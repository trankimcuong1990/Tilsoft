jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives', 'furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', function ($scope, $cookieStore) {

    $scope.data = null;
    $scope.workOrders = [];

    $scope.filters = {
        workOrderUD: null,
        workCenterID: null,
        productionTeamID: null,
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
                    $scope.workOrders = [];
                    //create workorder list
                    var indexs = [];
                    angular.forEach($scope.data.masterProductionSchedules, function (item) {
                        keyID = item.workOrderID.toString();
                        if (indexs.indexOf(keyID) < 0) {
                            indexs.push(keyID);
                            var woItem = {
                                workOrderID: item.workOrderID,
                                workOrderUD: item.workOrderUD,
                                proformaInvoiceNo: item.proformaInvoiceNo,
                                clientUD: item.clientUD,
                                startDate: item.startDate,
                                finishDate: item.finishDate,
                                articleCode: item.articleCode,
                                description: item.description,
                                workOrderQnt: item.workOrderQnt,
                                workCenters: [],
                            }
                            $scope.workOrders.push(woItem);

                            //get distinct work center by work order
                            var wcIndexs = [];
                            angular.forEach($scope.data.masterProductionSchedules, function (xItem) {
                                if (xItem.workOrderID == woItem.workOrderID) {                                    
                                    if (wcIndexs.indexOf(xItem.workCenterID) < 0) {
                                        wcIndexs.push(xItem.workCenterID);
                                        var wcItem = {
                                            workCenterID: xItem.workCenterID,
                                            workCenterNM: xItem.workCenterNM,
                                            productionItems: [],
                                        }
                                        woItem.workCenters.push(wcItem);

                                        //get distinct production item by work order & work center
                                        var piIndexs = [];
                                        angular.forEach($scope.data.masterProductionSchedules, function (yItem) {
                                            if (yItem.workOrderID == woItem.workOrderID && yItem.workCenterID==wcItem.workCenterID) {
                                                if (piIndexs.indexOf(yItem.productionItemID) < 0) {
                                                    piIndexs.push(yItem.productionItemID);
                                                    var piItem = {
                                                        productionItemID: yItem.productionItemID,
                                                        productionItemUD: yItem.productionItemUD,
                                                        productionItemNM: yItem.productionItemNM,
                                                        normQnt: yItem.normQnt,
                                                        productionTeams: [],
                                                    }
                                                    wcItem.productionItems.push(piItem);
                                                    
                                                    //get production team by work order & work center & production item
                                                    angular.forEach($scope.data.masterProductionSchedules, function (zItem) {
                                                        if (zItem.workOrderID == woItem.workOrderID && zItem.workCenterID == wcItem.workCenterID && zItem.productionItemID == piItem.productionItemID) {
                                                            piItem.productionTeams.push(zItem);
                                                        }
                                                    });
                                                }
                                            }
                                        });

                                    }
                                }
                            });                                                      
                        }
                    });
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