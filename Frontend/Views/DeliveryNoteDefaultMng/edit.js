// formatData
// using for auto-complate support quicksearch.
function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.value,
            description: item.label
        }
    });
}

(function () {
    'use strict';

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', deliveryNoteDefaultController);
    deliveryNoteDefaultController.$inject = ['$scope', 'dataService'];

    function deliveryNoteDefaultController($scope, deliveryNoteDefaultService) {
        $scope.data = [];
        $scope.searchString = null;
        $scope.IDX = 0;
        $scope.getIDX = 0;
        $scope.supportFactoryWarehouses = [];
        $scope.factoryWarehousePallets = [];

        // variable for total rows scan barcode.
        $scope.totalRowsScan = 0;

        // variables for event quick-search with client.
        $scope.ngInitClient = null;
        $scope.ngItemClient = {
            id: null,
            label: null,
            description: null
        };
        $scope.ngRequestClient = {
            url: context.supportServiceUrl + 'getClient2',
            token: context.token,
        };
        $scope.ngSelectedClient = {
            id: null,
            label: null,
            description: null
        };

        // variables for event quick-search with factory.
        $scope.ngInitFactory = null;
        $scope.ngItemFactory = {
            id: null,
            label: null,
            description: null
        };
        $scope.ngRequestFactory = {
            url: context.supportServiceUrl + 'getFactory2',
            token: context.token,
        };
        $scope.ngSelectedFactory = {
            id: null,
            label: null,
            description: null
        };

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
            remove: remove,
            update: update,
            scan: scan,
            del: del,
            getTotalQuantity: getTotalQuantity
        };

        $scope.method = {
            increaseIDX: increaseIDX,
            existList: existList,
            getDefaultFactoryWarehouse: getDefaultFactoryWarehouse
        };

        function del(id) {
            deliveryNoteDefaultService.delete(
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

        function init() {
            deliveryNoteDefaultService.serviceUrl = context.serviceUrl;
            deliveryNoteDefaultService.supportServiceUrl = context.supportServiceUrl;
            deliveryNoteDefaultService.token = context.token;

            $scope.event.get();
        };

        function get() {
            deliveryNoteDefaultService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.supportFactoryWarehouses = data.data.factoryWarehouses;
                    $scope.factoryWarehousePallets = data.data.factoryWarehousePallets;

                    // set total rows scan barcode is delivery note detail.
                    $scope.totalRowsScan = $scope.data.deliveryNoteDetailDTOs.length;

                    // set ngInitClient.
                    if ($scope.data.clientUD !== null && $scope.data.clientUD !== '') {
                        $scope.ngInitClient = $scope.data.clientUD;
                    }

                    // set ngInitFactory.
                    if ($scope.data.factoryUD !== null && $scope.data.factoryUD !== '') {
                        $scope.ngInitFactory = $scope.data.factoryUD;
                    }

                    $scope.data.deliveryNoteTypeID = 1; // default

                    if ($scope.data.deliveryNoteDate === null || $scope.data.deliveryNoteDate === '') {
                        var today = new Date();
                        var dd = today.getDate();
                        var mm = today.getMonth() + 1;
                        if (dd < 10) {
                            dd = '0' + dd;
                        }
                        if (mm < 10) {
                            mm = '0' + mm;
                        } 
                        var yyyy = today.getFullYear();
                        $scope.data.deliveryNoteDate = dd + '/' + mm + '/' + yyyy;
                    }
                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function update() {
            if ($scope.editForm.$valid) {
                // set type = 1: default
                $scope.data.deliveryNoteTypeID = 1;
                // set clientID for data to insert.
                if ($scope.ngItemClient !== undefined && $scope.ngItemClient !== null) {
                    $scope.data.clientID = $scope.ngItemClient.id;
                } else {
                    $scope.data.clientID = null;
                }

                //add statusTypeID = 6, confirm from mr Hanh, status for delivery showroom AVT
                $scope.data.statusTypeID = 6;

                // set factoryID for data to insert.
                if ($scope.ngItemFactory !== undefined && $scope.ngItemFactory !== null) {
                    $scope.data.factoryID = $scope.ngItemFactory.id;
                } else {
                    $scope.data.factoryID = null;
                }

                deliveryNoteDefaultService.update(
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
                        jsHelper.showMessage('warning', error);
                    });
            } else {
                jsHelper.showMessage('warning', context.msgValid);
            }
        };

        function remove($event, item) {
            //$event.preventDefault();
            var confirmedMsg = 'Delete ' + item.productionItemUD + ' ?'
            if (confirm(confirmedMsg) == true) {
                var index = $scope.data.deliveryNoteDetailDTOs.indexOf(item);
                $scope.data.deliveryNoteDetailDTOs.splice(index, 1);
                $scope.totalRowsScan = $scope.data.deliveryNoteDetailDTOs.length;
            }

        };

        function scan() { 
            deliveryNoteDefaultService.searchProductionItemByScanQRCode(
                $scope.searchString,
                function (data) {
                    if (data.data !== null) {
                        var item = data.data[0];
                        console.log(item.id);
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
                                fromFactoryWarehouseID: item.factoryWarehouseID,
                                qty: 1
                            };
                            debugger;

                            // check $scope.searchString not read data many times.
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

        function getDefaultFactoryWarehouse() {
            var factoryWarehouseID = null;
            angular.forEach($scope.supportFactoryWarehouses, function (item, key) {
                if (item.isDefault) {
                    factoryWarehouseID = item.factoryWarehouseID;
                }
            });
            return factoryWarehouseID;
        };
        // Total for Quantity
        function getTotalQuantity() {
            var sumQty = parseInt(0);    
            angular.forEach($scope.data.deliveryNoteDetailDTOs, function (item) {
                if (item.qty !== null && item.qty !== '') {
                    sumQty = sumQty + parseInt(item.qty)
                }
            });
            return sumQty;
        }
        $scope.event.init();
    };
})();