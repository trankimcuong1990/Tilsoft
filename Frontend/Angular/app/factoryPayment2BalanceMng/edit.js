//
// SUPPORT
//
//jQuery('#main-form').keypress(function (e) {
//    if (e.keyCode == 13) {
//        e.preventDefault();
//        return false;
//    }
//});

//
// APP START
//
jQuery('#grdDetail').scrollableTable2();

var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;
    $scope.details = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.details = data.data.details;
                    $scope.$apply();

                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.details = null;
                    $scope.$apply();
                }
            );
        },
        approve: function () {
            if (confirm('Carrying forward the balance to the next season ?')) {
                jsonService.approve(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryPayment2BalanceID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        reset: function () {

        }
    };

    //
    // method
    //
    $scope.method = {
        getTotalIncreasing: function () {
            var result = 0;
            if ($scope.details != null) {
                angular.forEach($scope.details, function (value, key) {
                    if (value.increasingMutation != null) {
                        result = result + parseFloat(value.increasingMutation);
                    }
                }, null);
            }

            return result;
        },
        getTotalDecreasing: function () {
            var result = 0;
            if ($scope.details != null) {
                angular.forEach($scope.details, function (value, key) {
                    if (value.decreasingMutation != null) {
                        result = result + parseFloat(value.decreasingMutation);
                    }
                }, null);
            }

            return result;
        },
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        }
    };

    //
    // init
    //    
    $scope.event.init();
}]);