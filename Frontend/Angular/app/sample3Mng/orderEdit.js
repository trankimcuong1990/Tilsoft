//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
//create controller
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', 'dataService', function ($scope, $cookieStore, dataService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    $scope.data = null;

    $scope.samplePurposes = null;
    $scope.sampleTransportTypes = null;
    $scope.sampleRequestTypes = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getOrderData(
                context.id,
                context.clientId,
                context.season,
                function (data) {
                    if (data.message.type === 0) {
                        $scope.data = data.data.data;
                        $scope.samplePurposes = data.data.supportList.samplePurposes;
                        $scope.sampleTransportTypes = data.data.supportList.sampleTransportTypes;
                        $scope.sampleRequestTypes = data.data.supportList.sampleRequestTypes;

                        $('#content').show();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.samplePurposes = null;
                    $scope.sampleTransportTypes = null;
                    $scope.sampleRequestTypes = null;
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                dataService.updateOrderData(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            window.location = context.refreshURL + data.data.sampleOrderID;
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
        addNLMonitor: function () {
            var selectedItem = $('#nlMonitorSelect2').select2('data');
            if (selectedItem) {
                var isExist = false;
                angular.forEach($scope.data.sampleMonitorDTOs, function (item) {
                    if (item.userID == selectedItem.data.userId) {
                        isExist = true;
                    }
                });
                if (!isExist) {
                    $scope.data.sampleMonitorDTOs.push({
                        sampleMonitorID: dataService.getIncrementId(),
                        userID: selectedItem.data.userId,
                        sampleMonitorGroupID: 2,
                        fullName: selectedItem.data.employeeNM,
                        internalCompanyNM: selectedItem.data.internalCompanyNM
                    });
                }
                $('#nlMonitorSelect2').select2('data', null);
            }
        },
        addVNMonitor: function () {
            var selectedItem = $('#vnMonitorSelect2').select2('data');
            if (selectedItem) {
                var isExist = false;
                angular.forEach($scope.data.sampleMonitorDTOs, function (item) {
                    if (item.userID == selectedItem.data.userId) {
                        isExist = true;
                    }
                });
                if (!isExist) {
                    $scope.data.sampleMonitorDTOs.push({
                        sampleMonitorID: dataService.getIncrementId(),
                        userID: selectedItem.data.userId,
                        sampleMonitorGroupID: 1,
                        fullName: selectedItem.data.employeeNM,
                        internalCompanyNM: selectedItem.data.internalCompanyNM
                    });
                }
                $('#vnMonitorSelect2').select2('data', null);
            }
        },
        removeMonitorUser: function (item) {
            $scope.data.sampleMonitorDTOs.splice($scope.data.sampleMonitorDTOs.indexOf(item), 1);
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);