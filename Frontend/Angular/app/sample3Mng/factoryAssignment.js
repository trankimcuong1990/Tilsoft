//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);
//create controller
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    $scope.data = null;
    $scope.factories = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getFactoryAssignmentData(
                context.id,
                function (data) {
                    if (data.message.type === 0) {
                        $scope.factories = data.data.supportList.factories;
                        $timeout(function () {
                            $scope.data = data.data.data;
                            $('#content').show();
                        }, 0);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.factories = null;
                    $scope.data = null;
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                dataService.updateFactoryAssignmentData(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            window.location = context.refreshURL;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Validation failed, please check the input form for error!');
            }
        },
        addSubFactory: function () {
            var selectedItem = $('#subFactorySelect2').select2('data');
            if (selectedItem) {
                var isExist = false;
                angular.forEach($scope.data.subFactoryDTOs, function (item) {
                    if (item.factoryID == selectedItem.id) {
                        isExist = true;
                    }
                });
                if (!isExist) {
                    $scope.data.subFactoryDTOs.push({
                        sampleProductSubFactoryID: dataService.getIncrementId(),
                        factoryID: selectedItem.id,
                        factoryUD: selectedItem.text
                    });
                }
                $('#subFactorySelect2').select2('data', null);
            }
        },
        removeSubFactory: function (item) {
            $scope.data.subFactoryDTOs.splice($scope.data.subFactoryDTOs.indexOf(item), 1);
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);