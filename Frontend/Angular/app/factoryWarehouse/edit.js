//
// APP START
//

var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'furnindo-directive', 'ui.select2', 'ng-currency']);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;
    $scope.newID = -1;
    $scope.stockOverviews = null;
    $scope.stockOverviewDetails = null;
    $scope.palletOverviews = null;
    $scope.branches = null;
    $scope.capacityOverviews = [];
    $scope.palletOverviews = [];

    $scope.setColor = function (item) {
        if (item.totalBalance === 0) {
            return { background: "green", color: "white" };
        }
        else if ((item.capacity - item.totalBalance) === 0) {
            return { background: "orange", color: "white" };
        }
        else if ((item.capacity - item.totalBalance) < 0) {
            return { background: "red", color: "white" };
        }
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.load(
                context.id,
                context.branchID,
                function (data) {
                    $scope.data = data.data.data;
                    //$scope.stockOverviews = data.data.stockOverviews;
                    //$scope.stockOverviewDetails = data.data.stockOverviewDetails;
                    $scope.capacityOverviews = data.data.capacityOverviews;
                    $scope.palletOverviews = data.data.palletOverviews;
                    $scope.branches = data.data.branches;
                    $scope.supportEmployee = data.data.employees;
                    jQuery('#content').show();

                    //console.log(data);

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });


                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.companies = null;
                    $scope.stockOverviews = null;
                    $scope.stockOverviewDetails = null;
                    $scope.palletOverviews = null;
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                dataService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryWarehouseID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        },

        addDetail: function ($event) {
            $event.preventDefault();
            $scope.data.factoryWarehousePallets.push({
                factoryWarehousePalletID: $scope.method.getNewID(),
            });
        },
        removeDetail: function ($event, index) {
            $event.preventDefault();
            $scope.data.factoryWarehousePallets.splice(index, 1);
        },
        showCapacity: function () {
            dataService.getCapacityData(
                context.id,
                function (data) {
                    $scope.capacityOverviews = data.data.capacityOverviews;
                    scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        showCapacityDetail: function () {
            dataService.getCapacityDetailData(
                context.id,
                function (data) {
                    console.log(data);
                    $scope.palletOverviews = data.data.palletOverviews;
                    scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);