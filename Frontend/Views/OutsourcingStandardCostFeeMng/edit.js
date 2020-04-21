

var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap']);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
    //
    // init service
    //
    dataService.serviceUrl = context.serviceUrl;
    dataService.supportServiceUrl = context.supportServiceUrl;
    dataService.token = context.token;

    jQuery('#content').show();
    //
    //property
    //
    $scope.data = null;

    //
    //support data
    //

    $scope.supportData = {

    };

    //
    //function
    //
    $scope.load = function () {
        var param = {};
        dataService.load(
            context.id,
            param,
            function (data) {
                $scope.data = data.data;

            },
            function (error) {
                $scope.data = null;
            }
        );
    };

    $scope.update = function (item) {
        if ($scope.editForm.$valid) {
            if (item.validFrom === null || item.validFrom === "") { jsHelper.showMessage('warning', 'Please input valid from'); return; }
            dataService.update(
                0,
                item,
                function (data) {
                    jsHelper.processMessage(data.message);
                    var id = data.data.modelID;
                    window.location = context.refreshUrl + id;
                },
                function (error) {
                    //do need do nothing
                }
            );
        }
        else {
            jsHelper.showMessage('warning', context.errMsg);
        }
    };

    $scope.confirm = function (item) {
        if ($scope.editForm.$valid) {
            dataService.approve(
                0,
                item,
                function (data) {
                    jsHelper.processMessage(data.message);
                    var id = data.data.modelID;
                    window.location = context.refreshUrl + id;
                },
                function (error) {
                    //do need do nothing
                }
            );
        } else {
            jsHelper.showMessage('warning', context.errMsg);
        }
    };

    $scope.reset = function (item) {
        if ($scope.editForm.$valid) {
            dataService.resetForModel(
                item.outsourcingStandardCostFeeID,
                function (data) {
                    jsHelper.processMessage(data.message);
                    var id = item.modelID;
                    window.location = context.refreshUrl + id;
                },
                function (error) {
                    //do need do nothing
                }
            );
        } else {
            jsHelper.showMessage('warning', context.errMsg);
        }
    };


    //
    //init controller
    // 
    $scope.load();

}]);