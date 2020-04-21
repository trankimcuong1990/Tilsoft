//
// APP START
//

var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'furnindo-directive', 'ui.select2', 'ng-currency']);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', '$timeout', function ($scope, dataService, $timeout) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;
    $scope.newID = -1;

    $scope.listProductionItemGroupNotification = [];
    $scope.listProductionItemGroupStockNotification = [];

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.listUser = data.data.listUser;
                    $scope.listProductionItemGroupNotification = data.data.data.listProductionItemGroupNotification;
                    $scope.listProductionItemGroupStockNotification = data.data.data.listProductionItemGroupStockNotification;
                    //console.log(data);

                    jQuery('#content').show();
                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                }
            );
        },
        update: function () {
            if ($scope.data.productionItemGroupUD.length !== 2) {
                jsHelper.showMessage('warning', 'Code must be in 2 characters format and unique!');
                return;
            }

            if ($scope.listProductionItemGroupNotification !== null) {
                $scope.data.listProductionItemGroupNotification = $scope.listProductionItemGroupNotification;
            }
            if ($scope.listProductionItemGroupStockNotification !== null) {
                $scope.data.listProductionItemGroupStockNotification = $scope.listProductionItemGroupStockNotification;
            }
            if ($scope.editForm.$valid) {
                dataService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            $scope.method.refresh(data.data.productionItemGroupID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.exceptionMessage);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        },
        // add item in list User to ProductItemGroup

        add: function (item) {
            if (!$scope.method.checkExistInList(item)) {
                $scope.listProductionItemGroupNotification.push(item);
            }


        },
        // remove item from list User of ProductitemGroup
        remove: function (item) {
            var index = $scope.listProductionItemGroupNotification.indexOf(item);
            $scope.listProductionItemGroupNotification.splice(index, 1);
        },

        //==========
        
        addStock: function (item) {
            if (!$scope.method.checkExistInStockList(item)) {
                $scope.listProductionItemGroupStockNotification.push(item);
            }


        },
        removeStock: function (item) {
            var index = $scope.listProductionItemGroupStockNotification.indexOf(item);
            $scope.listProductionItemGroupStockNotification.splice(index, 1);
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
        },
        checkExistInList: function (item) {
            for (var i = 0; i < $scope.listProductionItemGroupNotification.length; i++) {
                var ele = $scope.listProductionItemGroupNotification[i];
                if (ele.userID === item.userID) {
                    return true;
                }
            }

            return false;
        },
        checkExistInStockList: function (item) {
            for (var i = 0; i < $scope.listProductionItemGroupStockNotification.length; i++) {
                var ele = $scope.listProductionItemGroupStockNotification[i];
                if (ele.userID === item.userID) {
                    return true;
                }
            }

            return false;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);