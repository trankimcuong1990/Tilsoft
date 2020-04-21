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
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;
    $scope.currentItem = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
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
                    $scope.$apply();
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.quotationID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', context.error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', errMsg);
            }
        },
        history: function (item) {
            $scope.currentItem = item;

            console.log($scope.currentItem);

            jQuery("#historyForm").modal();
        }
    };

    //
    // method
    //
    $scope.method = {
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