(function () {
    'use strict';

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', receivingNoteDefaultMngController);
    receivingNoteDefaultMngController.$inject = ['$scope', 'dataService'];

    function receivingNoteDefaultMngController($scope, receivingNoteService) {
        $scope.data = null; // [] dung cho index.
        $scope.searchString = null;
        $scope.IDX = 0;
        $scope.getIDX = 0;
        $scope.factoryWarehousePallets = [];

        // variable for total rows scan barcode.
        $scope.totalRowsScan = 0;

        $scope.event = {
            init: init,
            get: get,
            remove: remove,
            update: update,
            activepage: activepage,
            scan: scan,
            removeDetail: removeDetail,
            qrCode: qrCode,
            getTotalQuantity: getTotalQuantity
        };

        $scope.method = {
            increaseIDX: increaseIDX,
            existList: existList,
            getDefaultFactoryWarehouse: getDefaultFactoryWarehouse,
        };

        $scope.gridHandler = {
            loadMore: function () {
            },
            sort: function (sortedBy, sortedDirection) {
            },
            isTriggered: false
        };

        function init() {
            receivingNoteService.serviceUrl = context.serviceUrl;
            receivingNoteService.token = context.token;
            receivingNoteService.supportServiceUrl = context.supportServiceUrl;

            $scope.event.get();
        };

        function get() {
            receivingNoteService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.factoryWarehouses = data.data.factoryWarehouses;
                    $scope.factoryWarehousePallets = data.data.factoryWarehousePallets;

                    // set total rows scan barcode is delivery note detail.
                    $scope.totalRowsScan = $scope.data.receivingNoteDetailDTOs.length;

                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function removeDetail(item) {
            var confirmedMsg = 'Delete ' + item.productionItemUD + ' ?';
            
            if (confirm(confirmedMsg) === true) {
                var index = $scope.data.receivingNoteDetailDTOs.indexOf(item);
                $scope.data.receivingNoteDetailDTOs.splice(index, 1);
                $scope.totalRowsScan = $scope.data.receivingNoteDetailDTOs.length;
            } 
        };

        // Total Quantity
        function getTotalQuantity() {
            var sumQty = parseInt(0);
            angular.forEach($scope.data.receivingNoteDetailDTOs, function (item) {
                if (item.quantity !== null && item.quantity !== '') {
                    sumQty = sumQty + parseInt(item.quantity)
                }
            });
            return sumQty;
        }

        function remove(id) {
            receivingNoteService.delete(
                 id,
                 null,
                 function (data) {
                     jsHelper.processMessage(data.message);
                     if (data.message.type === 0) {
                         window.location = context.backUrl;
                     }
                 },
                 function (error) {
                     jsHelper.showMessage('warning', error);
                 });
        };

        function update($event, data) {
            if ($scope.editForm.$valid) {
                // set type = 1: default
                $scope.data.receivingNoteTypeID = 1;
                receivingNoteService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.receivingNoteID;
                            $scope.data = data.data;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            } else {
                jsHelper.showMessage('warning', context.msgValid);
            }
        };


        $scope.event.activepage();

        function activepage() {
            createservices();

            $scope.data = {

            };

            receivingNoteService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    //debugger;
                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function scan() {
            receivingNoteService.searchProductionItemByScanQRCode(
                $scope.searchString,
                function (data) {
                    if (data.data !== null) {

                        var item = data.data[0]; 

                        if (item !== undefined || item !== null) {
                            if ($scope.data.receivingNoteDetailDTOs === null) {
                                $scope.data.receivingNoteDetailDTOs = [];
                            }

                            var receivingNoteDetail = {
                                receivingNoteDetailID: $scope.method.increaseIDX(),
                                productionItemID: item.id,
                                productionItemUD: item.value,
                                productionItemNM: item.label,
                                productionItemTypeID: item.productionItemTypeID,
                                productionItemTypeNM: item.productionItemTypeNM,
                                description: null,
                                quantity: 1,
                                toFactoryWarehouseID: $scope.method.getDefaultFactoryWarehouse(),
                            }

                            if ($scope.searchString !== null) {
                                if ($scope.data.receivingNoteDetailDTOs.length === 0) {
                                    $scope.data.receivingNoteDetailDTOs.push(receivingNoteDetail);

                                    // increase total rows when scan barcode.
                                    $scope.totalRowsScan = $scope.totalRowsScan + 1;
                                } else {
                                    if ($scope.method.existList(receivingNoteDetail)) {
                                        $scope.data.receivingNoteDetailDTOs[$scope.getIDX].quantity = $scope.data.receivingNoteDetailDTOs[$scope.getIDX].quantity + receivingNoteDetail.quantity;
                                    } else {
                                        $scope.data.receivingNoteDetailDTOs.push(receivingNoteDetail);

                                        // increase total rows when scan barcode.
                                        $scope.totalRowsScan = $scope.totalRowsScan + 1;
                                    }
                                }
                            }

                            $scope.searchString = null;

                            $scope.event.getTotalQuantity();

                            $scope.$apply();
                        }
                    }
                },
                function (error) {
                });
        };

        // Loop to get information to export QR Code template.
        function qrCode() {
            var productionItemIDs = '', qntBarcodes = '';
            var i = 0; // Variable run when loop data.
            //debugger
            if ($scope.data.receivingNoteDetailDTOs.length > 0) {
                angular.forEach($scope.data.receivingNoteDetailDTOs, function (value, key) {
                    // Case list detail have more than data.
                    if (i > 0) {
                        productionItemIDs = productionItemIDs + ',';
                        qntBarcodes = qntBarcodes + ',';
                    }

                    productionItemIDs = productionItemIDs + value.productionItemID;
                    qntBarcodes = qntBarcodes + (value.quantity * ((value.qntBarCode === undefined || value.qntBarCode === null || value.qntBarCode === '') ? 1 : value.qntBarCode));

                    i = i + 1;
                });

                receivingNoteService.exportQRCode(
                        productionItemIDs,
                        qntBarcodes,
                        function (data) {
                            window.location = context.backendReportUrl + data.data;
                        },
                        function (error) {
                        });
            }
        };

        function increaseIDX() {
            $scope.IDX = $scope.IDX - 1;
            return $scope.IDX
        };

        function existList(item) {
            for (var i = 0; i < $scope.data.receivingNoteDetailDTOs.length; i++) {
                if ($scope.data.receivingNoteDetailDTOs[i].productionItemID === item.productionItemID) {
                    $scope.getIDX = i;
                    return true;
                }
            }

            return false;
        };

        function getDefaultFactoryWarehouse() {
            var factoryWarehouseID = null;
            angular.forEach($scope.factoryWarehouses, function (item) {
                if (item.isDefault) {
                    factoryWarehouseID = item.factoryWarehouseID;
                }
            });
            return factoryWarehouseID;
        }

        $scope.event.init();

        function createservices() {
            receivingNoteService.serviceUrl = context.serviceUrl;
            receivingNoteService.token = context.token;
        };
    };
})();