(function () {
    'use strict';

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', deliveryNoteRepairAssemplyController);
    deliveryNoteRepairAssemplyController.$inject = ['$scope', 'dataService'];

    function deliveryNoteRepairAssemplyController($scope, deliveryNoteRepairAssemplyService) {
        $scope.data = [];
        $scope.supportFactoryWarehouses = [];

        // variable for total rows scan barcode.
        $scope.totalRowsScan = 0;

        $scope.searchString = null;
        $scope.IDX = 0;
        $scope.getIDX = 0;

        $scope.gridHandler = {
            loadMore: function () {
            },
            sort: function (sortedBy, sortedDirection) {
            },
            isTriggered: false
        };

        $scope.event = {
            init: init,
            get: get,
            removed: removed,
            update: update,
            scan: scan,
            deletedata: deletedata,
            getTotalQuantity: getTotalQuantity
        };

        $scope.method = {
            increaseIDX: increaseIDX,
            existList: existList,
            getDefaultFactoryWarehouse: getDefaultFactoryWarehouse, // get default factory warehouse.
        };

        function init() {
            deliveryNoteRepairAssemplyService.serviceUrl = context.serviceUrl;
            deliveryNoteRepairAssemplyService.supportServiceUrl = context.supportServiceUrl;
            deliveryNoteRepairAssemplyService.token = context.token;

            $scope.event.get();
        };

        function get() {
            deliveryNoteRepairAssemplyService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.supportFactoryWarehouses = data.data.factoryWarehouses;

                    // set total rows scan barcode is delivery note detail.
                    $scope.totalRowsScan = $scope.data.deliveryNoteDetailDTOs.length;

                    $scope.data.deliveryNoteTypeID = 2; // type: repair assemply

                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function removed($event, item) {
            //$event.preventDefault();
            var confirmedMsg = 'Delete ' + item.productionItemUD + ' ?';
            if (confirm(confirmedMsg) == true) {
                var index = $scope.data.deliveryNoteDetailDTOs.indexOf(item);
                $scope.data.deliveryNoteDetailDTOs.splice(index, 1);
                $scope.totalRowsScan = $scope.data.deliveryNoteDetailDTOs.length;
            }
            
        };

        function getTotalQuantity() {
            var sumQty = parseInt(0);
            angular.forEach($scope.data.deliveryNoteDetailDTOs, function (item) {
                if (item.qty !== null && item.qty !== '') {
                    sumQty = sumQty + parseInt(item.qty)
                }
            });
            return sumQty;
        }

        function update() {
            if ($scope.editForm.$valid) {
                // set type = 2: repair assemply
                $scope.data.deliveryNoteTypeID = 2;

                deliveryNoteRepairAssemplyService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.deliveryNoteID;
                            $scope.data = data.data;
                        }
                    },
                    function (error) {
                        // debugger
                        jsHelper.showMessage('warning', error);
                    });
            } else {
                jsHelper.showMessage('warning', context.msgValid);
            }
        };

        function deletedata(id) {
            // debugger
            deliveryNoteRepairAssemplyService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    jsHelper.showMessage("warning", error);
                });
        };

        function scan() {
            deliveryNoteRepairAssemplyService.searchProductionItemByScanQRCode(
                $scope.searchString,
                function (data) {
                    // debugger
                    if (data.data !== null) {
                        var item = data.data[0];

                        if (item !== undefined || item !== null) {
                            if ($scope.data.deliveryNoteDetailDTOs === null) {
                                $scope.data.deliveryNoteDetailDTOs = [];
                            }

                            var deliveryNoteDetail = {
                                deliveryNoteDetailID: $scope.method.increaseIDX(),
                                productionItemID: item.id,
                                productionItemUD: item.value,
                                productionItemNM: item.label,
                                productionItemTypeID: item.productionItemTypeID,
                                productionItemTypeNM: item.productionItemTypeNM,
                                description: null,
                                fromFactoryWarehouseID: $scope.method.getDefaultFactoryWarehouse(), // set default factory warehouse.
                                qty: 1
                            }

                            // check searchString is not null.
                            if ($scope.searchString !== null) {
                                if ($scope.data.deliveryNoteDetailDTOs.length === 0) {
                                    $scope.data.deliveryNoteDetailDTOs.push(deliveryNoteDetail);

                                    // increase total rows when scan barcode.
                                    $scope.totalRowsScan = $scope.totalRowsScan + 1;
                                } else {
                                    if ($scope.method.existList(deliveryNoteDetail)) {
                                        $scope.data.deliveryNoteDetailDTOs[$scope.getIDX].qty = $scope.data.deliveryNoteDetailDTOs[$scope.getIDX].qty + deliveryNoteDetail.qty;
                                    } else {
                                        $scope.data.deliveryNoteDetailDTOs.push(deliveryNoteDetail);

                                        // increase total rows when scan barcode.
                                        $scope.totalRowsScan = $scope.totalRowsScan + 1;
                                    }
                                }
                            }

                            $scope.searchString = null;
                            $scope.$apply();
                        }
                    }
                },
                function (error) {
                });
        };

        function increaseIDX() {
            $scope.IDX = $scope.IDX - 1;
            return $scope.IDX
        };

        function existList(item) {
            for (var i = 0; i < $scope.data.deliveryNoteDetailDTOs.length; i++) {
                if ($scope.data.deliveryNoteDetailDTOs[i].productionItemID === item.productionItemID) {
                    $scope.getIDX = i;
                    return true;
                }
            }
            return false;
        };

        // get factory warehouse isDefault=true.
        function getDefaultFactoryWarehouse() {
            var factoryWarehouseID = null;
            angular.forEach($scope.supportFactoryWarehouses, function (item, key) {
                if (item.isDefault) {
                    factoryWarehouseID = item.factoryWarehouseID;
                }
            });
            return factoryWarehouseID;
        };

        $scope.event.init();
    };
})();
