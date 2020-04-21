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
        workOrderID: null,
        workCenterID: null,
        productionTeamID: null,
    };

    $scope.currentForm = 'workOrder';
    $scope.selectedWorkOrder = null;
    $scope.receiptDept = [{ id: 1, name: 'Delivery to Team' }, { id: 2, name: 'Delivery to warehouse' }];
    $scope.receiptDeptID = 1;

    $scope.warehouseSelectedID = null;
    $scope.productionTeamSelectedID = null;
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
                    angular.forEach($scope.data.productionSchedules, function (item) {
                        if (item.workCenterID != null) {
                            keyID = item.workOrderID.toString() + '_' + item.workCenterID.toString() + '_' + item.productionTeamID.toString();
                            if (indexs.indexOf(keyID) < 0) {
                                indexs.push(keyID);
                                var wItem = {
                                    workOrderID: item.workOrderID,
                                    workOrderUD: item.workOrderUD,
                                    articleCode: item.articleCode,
                                    workOrderQnt: item.workOrderQnt,
                                    startDate: item.startDate,
                                    finishDate: item.finishDate,
                                    workCenterID: item.workCenterID,
                                    workCenterNM: item.workCenterNM,
                                    sequenceIndexNumber: item.sequenceIndexNumber,
                                    productionTeamID: item.productionTeamID,
                                    productionTeamNM: item.productionTeamNM,
                                    nextWorkCenterID : item.nextWorkCenterID,
                                    components:[]
                                }
                                //create component list
                                angular.forEach($scope.data.productionSchedules, function (cItem) {
                                    if (cItem.workOrderID == wItem.workOrderID && cItem.workCenterID == wItem.workCenterID && cItem.productionTeamID == wItem.productionTeamID) {
                                        wItem.components.push(cItem);
                                    }
                                });
                                //creat workOrder list
                                $scope.workOrders.push(wItem);
                            }
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

        finished: function (item) {
            $scope.currentForm = 'component';
            $scope.selectedWorkOrder = item;
            console.log(item);
            //get default production team of next workcenter
            var nextProductionTeamID = $scope.method.getTeamOfNextWorkCenter($scope.selectedWorkOrder.workOrderID, $scope.selectedWorkOrder.workCenterID);
            $scope.productionTeamSelectedID = nextProductionTeamID;            
        },

        back: function () {
            $scope.currentForm = 'workOrder';
            $scope.selectedWorkOrder = null;
        },

        finishComponent: function () {
            //assign team & factory warehouse
            angular.forEach($scope.selectedWorkOrder.components, function (item) {
                item.toProductionTeamID = $scope.productionTeamSelectedID;
                item.toFactoryWarehouseID = $scope.warehouseSelectedID;
            });
            //save data
            jsonService.finishComponent($scope.receiptDeptID, $scope.selectedWorkOrder.components,
                function (data) {
                    if (data.message.type == 0) {
                        if ($scope.receiptDeptID == 1) { // delivery from team to team
                            window.open(context.deliveryNoteUrl + data.data.toString(), '');
                        }
                        else if ($scope.receiptDeptID == 2) { //receiving note from team to warehouse
                            window.open(context.receivingNoteUrl + data.data.toString(), '');
                        }
                    }
                    else {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    $scope.data = null;
                    $scope.$apply();
                }
            );

        },

        changeDept: function (dept) {
            if (dept == 1) { // delivery to team
                var nextProductionTeamID = $scope.method.getTeamOfNextWorkCenter($scope.selectedWorkOrder.workOrderID, $scope.selectedWorkOrder.workCenterID);
                $scope.productionTeamSelectedID = nextProductionTeamID
            }
        }

    }

    $scope.method = {
        getTeamOfNextWorkCenter: function (workOrderID, workCenterID) {
            var result = null;
            for (var i = 0; i < $scope.data.productionSchedules.length; i++) {
                var cItem = $scope.data.productionSchedules[i];
                if (cItem.workOrderID == workOrderID && cItem.workCenterID == workCenterID) {
                    result = cItem.nextProductionTeamID;
                    if (result != null) return result;
                }
            }
        }
    }

    $scope.event.reload();
}]);