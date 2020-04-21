(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', showroomTransferController);
    showroomTransferController.$inject = ['$scope', 'dataService', '$timeout'];

    function showroomTransferController($scope, showroomTransferService, $timeout) {
        $scope.data = [];
        $scope.showroomTransferDetails = [];
        $scope.factoryWarehousePallets = [];
        $scope.factoryWarehouses = [];
        $scope.searchString = null;
        $scope.IDX = 0;
        $scope.getIDX = 0;

        // variable for total rows scan barcode.
        $scope.totalRowsScan = 0;

        $scope.method = {
            increaseIDX: function () {
                $scope.IDX = $scope.IDX - 1;
                return $scope.IDX;
            },

            existList: function (item) {
                for (var i = 0; i < $scope.data.showroomTransferDetails.length; i++) {
                    if ($scope.data.showroomTransferDetails[i].productionItemID === item.productionItemID) {
                        $scope.getIDX = i;
                        return true;
                    }
                }
                return false;
            },
        };

        $scope.event = {
            init: function init() {
                showroomTransferService.serviceUrl = context.serviceUrl;
                showroomTransferService.token = context.token;
                showroomTransferService.supportServiceUrl = context.supportServiceUrl;

                $scope.event.getInitData();
                jQuery('#content').show();
            },

            getInitData: function () {
                showroomTransferService.getInitData(
                    function (data) {
                        $scope.factoryWarehouses = data.data.factoryWarehouses;
                        $scope.factoryWarehousePallets = data.data.factoryWarehousePallets;

                        $scope.event.get();
                    },
                    function (error) {
                        //do nothing
                    },
                );
            },

            get: function () {
                showroomTransferService.load(
                    context.id,
                    null,
                    function (data) {
                        $scope.data = data.data.data;
                        $scope.showroomTransferDetails = data.data.data.showroomTransferDetails;

                        $timeout(function () { jQuery('#content').show(); }, 0);
                    },
                    function (error) {
                        //do nothing
                    });
            },

            remove: function (id) {
                showroomTransferService.delete(
                    id,
                    null,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.backUrl;
                        }
                    },
                    function (error) {
                        //do nothing
                    });
            },

            update: function ($event, data) {
                if ($scope.editForm.$valid) {
                    showroomTransferService.update(
                        context.id,
                        $scope.data,
                        function (data) {
                            jsHelper.processMessage(data.message);
                            window.location = context.refreshUrl + data.data.data.showroomTransferID;
                            $scope.data = data.data;
                        },
                        function (error) {
                            //do nothing
                        });
                } else {
                    jsHelper.showMessage('warning', context.msgValid);
                }
            },

            scan: function () {
                showroomTransferService.searchProductionItemByScanQRCode(
                    $scope.searchString,
                    function (data) {
                        if (data.data !== null) {
                            var item = data.data[0];
                            if (item !== undefined || item !== null) {
                                if ($scope.data.showroomTransferDetails === null) {
                                    $scope.data.showroomTransferDetails = [];
                                }

                                var showroomTransferDetail = {
                                    showroomTransferDetailID: $scope.method.increaseIDX(),
                                    productionItemID: item.id,
                                    productionItemUD: item.value,
                                    productionItemNM: item.label,
                                    productionItemTypeID: item.productionItemTypeID,
                                    productionItemTypeNM: item.productionItemTypeNM,
                                    description: null,
                                    fromFactoryWarehouseID: item.factoryWarehouseID,
                                    quantity: 1
                                };

                                // check $scope.searchString not read data many times.
                                if ($scope.searchString !== null) {
                                    if ($scope.data.showroomTransferDetails.length === 0) {
                                        $scope.data.showroomTransferDetails.push(showroomTransferDetail);

                                        // increase total rows when scan barcode.
                                        $scope.totalRowsScan = $scope.totalRowsScan + 1;
                                    } else {
                                        if ($scope.method.existList(showroomTransferDetail)) {
                                            $scope.data.showroomTransferDetails[$scope.getIDX].quantity = $scope.data.showroomTransferDetails[$scope.getIDX].quantity + showroomTransferDetail.quantity;
                                        } else {
                                            $scope.data.showroomTransferDetails.push(showroomTransferDetail);

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
            },

            removeItem: function ($event, item) {
                //$event.preventDefault();
                var confirmedMsg = 'Delete ' + item.productionItemUD + ' ?'
                if (confirm(confirmedMsg) == true) {
                    var index = $scope.data.showroomTransferDetails.indexOf(item);
                    $scope.data.showroomTransferDetails.splice(index, 1);
                    $scope.totalRowsScan = $scope.data.showroomTransferDetails.length;
                }

            },

            getTotalQuantity: function () {
                var sumQty = parseInt(0);
                angular.forEach($scope.data.showroomTransferDetails, function (item) {
                    if (item.quantity !== null && item.quantity !== '') {
                        sumQty = sumQty + parseInt(item.quantity);
                    }
                });
                return sumQty;
            },
        };

        $timeout(function initWithTimeout() {
            $scope.event.init();
        }, 0);
    };
})();