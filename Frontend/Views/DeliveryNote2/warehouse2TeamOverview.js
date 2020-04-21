var tilsoftApp = angular.module('tilsoftApp', ['ui.bootstrap', 'furnindo-directive', 'avs-directives']);

tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', function ($scope, $timeout) {
    //
    // property
    //
    $scope.data = [];
    $scope.newID = -1;

    //using to parse to view
    $scope.deliveryNoteProducts = [];

    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.getDataWarehouse2TeamOverView(
                context.id,
                function (data) {
                    $scope.data = data.data.data;

                    //incase add new: initialize  param
                    if (context.id === 0) {
                        $scope.data.viewName = context.viewName;
                    }

                    //show data on view
                    $scope.method.parseDataToView();

                    //$scope.$apply();

                    $scope.$apply();
                    jQuery('#content').show();

                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },

    };
    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            window.location = context.refreshUrl + id;
        },

        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },


        parseDataToView: function () {
            $scope.deliveryNoteProducts = [];
            var indexs_editing = [];
            var indexs_addnew = [];

            //create group item is editing
            angular.forEach($scope.data.deliveryNoteDetailFormViewDTOs, function (item) {
                if (item.productionItemID !== null) {
                    if (item.deliveryNoteDetailID > 0) {
                        //create detail list
                       var keyID = item.productionItemID.toString();
                        if (indexs_editing.indexOf(keyID) < 0) {
                            indexs_editing.push(keyID);
                            var pItem = {
                                isAddNew: false,
                                rowID: $scope.method.getNewID(),
                                productionItemID: item.productionItemID,
                                productionItemUD: item.productionItemUD,
                                productionItemNM: item.productionItemNM,
                                stockQnt: item.stockQnt,
                                unitNM: item.unitNM,
                                factoryWarehousePalletNM: item.factoryWarehousePalletNM,
                                factoryWarehouseNM: item.factoryWarehouseNM,
                                pieces: []
                            };
                            $scope.deliveryNoteProducts.push(pItem);
                            angular.forEach($scope.data.deliveryNoteDetailFormViewDTOs, function (sItem) {
                                if (pItem.productionItemID === sItem.productionItemID) {
                                    pItem.pieces.push(sItem);
                                }
                            });
                        }
                    }
                }
            });

            //create group item is addnew
            angular.forEach($scope.data.deliveryNoteDetailFormViewDTOs, function (item) {
                if (item.productionItemID !== null) {
                    if (item.deliveryNoteDetailID < 0) {
                        // create detail list
                        keyID = item.productionItemID.toString();
                        if (indexs_editing.indexOf(keyID) < 0) {
                            indexs_editing.push(keyID);
                            var pItem = {
                                isAddNew: true,
                                rowID: $scope.method.getNewID(),
                                productionItemID: item.productionItemID,
                                productionItemUD: item.productionItemUD,
                                productionItemNM: item.productionItemNM,
                                stockQnt: item.stockQnt,
                                unitNM: item.unitNM,
                                factoryWarehousePalletNM: item.factoryWarehousePalletNM,
                                factoryWarehouseNM: item.factoryWarehouseNM,
                                pieces: []
                            };
                            $scope.deliveryNoteProducts.push(pItem);
                            angular.forEach($scope.data.deliveryNoteDetailFormViewDTOs, function (sItem) {
                                if (pItem.productionItemID === sItem.productionItemID) {
                                    pItem.pieces.push(sItem);
                                }
                            });
                        }
                    }
                }
            });
        }
    };

    //
    // init
    //
    $scope.event.load();
}]);