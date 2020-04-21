(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', workCenterController);
    workCenterController.$inject = ['$scope', 'dataService'];

    function workCenterController($scope, workCenterService) {
        $scope.data = [];
        $scope.supportEmployee = [];
        $scope.workCenterDetails = [];
        $scope.factoryWarehouses = [];
        $scope.branches = [];
        $scope.newID = -1;

        $scope.event = {
            initWorkCenter: initWorkCenter,
            saveWorkCenter: saveWorkCenter,
            deleteWorkCenter: deleteWorkCenter,
            removeWarehouse: removeWarehouse,
            addWarehouse: addWarehouse,
        };

        // Init page Work Center
        function initWorkCenter() {
            workCenterService.serviceUrl = context.serviceUrl;
            workCenterService.token = context.token;

            $scope.data = {
                operatingTime: null,
                defaultCost: null,
                capacity: null
            };

            workCenterService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.supportEmployee = data.data.employees;
                    $scope.factoryWarehouses = data.data.factoryWarehouses;
                    $scope.workCenterDetails = data.data.data.workCenterDetails;
                    $scope.factoryWarehouses = data.data.factoryWarehouses;
                    $scope.branches = data.data.branches;
                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };
            
        // Save a Work Center
        function saveWorkCenter() {
            if ($scope.editForm.$valid) {
                var count = 0;

                for (var i = 0; i < $scope.branches.length; i++) {
                    var branchDefault = $scope.branches[i];
                    var countDefault = 0;
                    var countAdded = 0;
                    for (var j = 0; j < $scope.workCenterDetails.length; j++) {
                        var WarehouseDefault = $scope.workCenterDetails[j];
                        if (branchDefault.branchID === WarehouseDefault.branchID && WarehouseDefault.isDefault) {
                            countDefault++;
                        }
                        else {
                            if (branchDefault.branchID === WarehouseDefault.branchID && !WarehouseDefault.isDefault) {
                                countAdded++;
                            }
                        }
                    }
                    if (countDefault === 0 && countAdded > 0) {
                        jsHelper.showMessage('warning', 'Branch ' + branchDefault.branchUD +' must have default warehouse');
                        break;
                    }
                    if (countDefault > 1) {
                        jsHelper.showMessage('warning', 'Branch ' + branchDefault.branchUD + ' must have only one default warehouse');
                        break;
                    }
                    count++;
                }

                if (count === $scope.branches.length) {
                    workCenterService.update(
                        context.id,
                        $scope.data,
                        function (data) {
                            jsHelper.processMessage(data.message);

                            if (data.message.type === 0) {
                                window.location = context.refreshUrl + data.data.data.workCenterID;
                                $scope.data = data.data.data;
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.data.message.message);
                        }
                    );
                }
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        };

        // Delete a Work Center
        function deleteWorkCenter() {
            workCenterService.delete(
                context.id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);

                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    var contentMessage = '';
                    jsHelper.showMessage('warning', contentMessage);
                }
            );
        };

        //Warehouse
        function removeWarehouse($event, id) {
            if ($event !== null) {
                $event.preventDefault();
            }
            var isExist = false;
            for (var j = 0; j < $scope.workCenterDetails.length; j++) {
                if ($scope.workCenterDetails[j].workCenterDetailID === id) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.workCenterDetails.splice(j, 1);
            }
        };

        function addWarehouse($event) {
            if ($event !== null) {
                $event.preventDefault();
            }
            $scope.workCenterDetails.push({
                workCenterDetailID: $scope.method.getNewID(),
                isDefault: 0
            });
        };

        $scope.method = {
            getNewID: function () {
                $scope.newID--;
                return $scope.newID;
            },
            checkDefault: function ($event, data) {
                for (var j = 0; j < $scope.workCenterDetails.length; j++) {
                    if ($scope.workCenterDetails[j].workCenterDetailID !== data.workCenterDetailID
                        && $scope.workCenterDetails[j].isDefault
                        && $scope.workCenterDetails[j].branchID === data.branchID) {
                        data.isDefault = 0;
                    }
                }
            }
        };

        initWorkCenter();
    };
})();