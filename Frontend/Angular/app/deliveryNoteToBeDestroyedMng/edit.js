(function () {
    'use strict';

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', deliveryNoteToBeDestroyedController);
    deliveryNoteToBeDestroyedController.$inject = ['$scope', 'dataService'];

    function deliveryNoteToBeDestroyedController($scope, deliveryNoteToBeDestroyedService) {
        $scope.data = [];
        $scope.searchString = null;
        $scope.IDX = 0;
        $scope.getIDX = 0;

        // variable for total rows scan barcode.
        $scope.totalRowsScan = 0;

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
            add: add,
            scan: scan,
            removeDetail: removeDetail,
            getTotalQuantity: getTotalQuantity
        };

        $scope.method = {
            increaseIDX: increaseIDX,
            existList: existList,
        };

        function init() {
            deliveryNoteToBeDestroyedService.serviceUrl = context.serviceUrl;
            deliveryNoteToBeDestroyedService.supportServiceUrl = context.supportServiceUrl;
            deliveryNoteToBeDestroyedService.token = context.token;

            $scope.event.get();
        };

        function getTotalQuantity() {
            var sumQty = parseInt(0);
            angular.forEach($scope.data.deliveryNoteDetailDTOs, function (item) {
                if (item.qty !== null & item.qty !== '') {
                    sumQty = sumQty + parseInt(item.qty)
                }
            });
            return sumQty;
        }

        function get() {
            deliveryNoteToBeDestroyedService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.data.deliveryNoteTypeID = 4; // type: tobe destroy

                    // set total rows scan barcode is delivery note detail.
                    $scope.totalRowsScan = $scope.data.deliveryNoteDetailDTOs.length;

                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function remove() {
            deliveryNoteToBeDestroyedService.delete(
                context.id,
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

        function update() {
            if ($scope.editForm.$valid) {
                // set type = 4: tobe destroyed
                $scope.data.deliveryNoteTypeID = 4;

                deliveryNoteToBeDestroyedService.update(
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

        function add() {
        };

        function scan() {
            deliveryNoteToBeDestroyedService.searchProductionItemByScanQRCode(
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
                                qty: 1
                            }

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
                    jsHelper.showMessage('warning', error);
                });
        };

        function removeDetail(item) {
            var confirmedMsg = 'Delete ' + item.productionItemUD + ' ?';
            if (confirm(confirmedMsg) == true) {
                var index = $scope.data.deliveryNoteDetailDTOs.indexOf(item);
                $scope.data.deliveryNoteDetailDTOs.splice(index, 1);
                $scope.totalRowsScan = $scope.data.deliveryNoteDetailDTOs.length;
            }
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

        $scope.event.init();
    };
})();