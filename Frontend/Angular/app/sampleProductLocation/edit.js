(function () {
    'use strict';

    angular
        .module('tilsoftApp', ['furnindo-directive', 'avs-directives'])
        .controller('tilsoftController', sampleProductLocationController);
    sampleProductLocationController.$inject = ['$scope', 'dataService'];

    function sampleProductLocationController($scope, sampleProductLocationService) {
        // declare.
        $scope.data = null;
        $scope.loadAgainData = null;
        $scope.sampleProductLocations = null;
        $scope.destroyed = false;

        // support.
        $scope.otherLocations = null;
        $scope.factoryLocations = null;

        var remark = '';
        var locationDate = '';
        var sampleProductLocationID = -1;

        // event.
        $scope.event = {
            initPage: initPage,
            updateData: updateData,
            changeOtherLocation: changeOtherLocation,
            changeFactoryLocation: changeFactoryLocation
        }

        function initPage() {
            $scope.method.initAPIs();

            sampleProductLocationService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = [];
                    $scope.loadAgainData = [];
                    $scope.otherLocations = [];
                    $scope.sampleProductLocations = [];
                    $scope.factoryLocations = [];
                    console.log(data.data);
                    $scope.data = data.data.editData;
                    $scope.loadAgainData = data.data.editData;
                    $scope.sampleProductLocations = data.data.sampleProductLocationDatas;
                    $scope.otherLocations = data.data.supportSampleOtherLocations;
                    $scope.factoryLocations = data.data.supportFactoryLocations;

                    remark = $scope.loadAgainData.remark;
                    locationDate = $scope.loadAgainData.locationDate;
                    sampleProductLocationID = $scope.loadAgainData.sampleProductLocationID;

                    if ($scope.data.otherLocationID === 4) {
                        $scope.destroyed = true;
                    }

                    $('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        }

        function updateData($event) {
            $event.preventDefault();

            if ($scope.data.sampleProductUD === null) {
                $scope.data.sampleProductUD = '';
            }

            if ($scope.data.factoryLocationID === null && $scope.data.otherLocationID === null) {
                jsHelper.showMessage('warning', 'Please select Factory Location or Other Location.');

                return false;
            }

            if ($scope.editForm.$valid) {
                sampleProductLocationService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.editData.sampleProductID;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            } else {
                jsHelper.showMessage('warning', context.msgValid);
            }
        }

        function changeOtherLocation() {
            if ($scope.sampleProductLocations.length === 0) {
                return false;
            }

            if ($scope.sampleProductLocations.length > 0) {
                if ($scope.sampleProductLocations[0].otherLocationID !== $scope.data.otherLocationID) {
                    $scope.data.remark = null;
                    $scope.data.locationDate = '';
                    $scope.data.sampleProductLocationID = -1;
                } else {
                    $scope.data.remark = remark;
                    $scope.data.locationDate = locationDate;
                    $scope.data.sampleProductLocationID = sampleProductLocationID;
                }
            }
        }

        function changeFactoryLocation() {
            if ($scope.sampleProductLocations.length === 0) {
                return false;
            }

            if ($scope.sampleProductLocations.length > 0) {
                if ($scope.sampleProductLocations[0].factoryLocationID !== $scope.data.factoryLocationID) {
                    $scope.data.remark = null;
                    $scope.data.locationDate = '';
                    $scope.data.sampleProductLocationID = -1;
                } else {
                    $scope.data.remark = remark;
                    $scope.data.locationDate = locationDate;
                    $scope.data.sampleProductLocationID = sampleProductLocationID;
                }
            }
        }

        // method.
        $scope.method = {
            initAPIs: initAPIs
        }

        function initAPIs() {
            sampleProductLocationService.serviceUrl = context.serviceUrl;
            sampleProductLocationService.token = context.token;
        }

        // default.
        $scope.event.initPage();
    };
})();