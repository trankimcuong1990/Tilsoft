function formatModel(data) {
    return $.map(data.data, function (item) {
        if (item !== null) {
            return {
                id: item.modelID,
                label: item.modelUD,
                description: item.modelNM
            };
        }
    });
};

function formatSampleProduct(data) {
    return $.map(data.data, function (item) {
        if (item !== null) {
            return {
                id: item.sampleProductID,
                label: item.sampleProductUD,
                description: item.articleDescription
            };
        }
    });
};

(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', breakDownController);
    breakDownController.$inject = ['$scope', '$timeout', 'dataService'];

    function breakDownController($scope, $timeout, breakDownService) {
        $scope.selectedRadio = null;

        $scope.initParam = {
            breakdownID: context.id,
            modelID: 0,
            sampleProductID: 0,
            freeItemDescription: null,
            offerLineID: 0
        };

        $scope.autoModel = {
            api: {
                url: context.serviceUrl + 'getModel',
                token: context.token
            },
            selectModel: {
                id: null,
                label: null,
                description: null,
            },
            initModel: null
        };

        $scope.autoSampleProduct = {
            api: {
                url: context.serviceUrl + 'getSampleProduct',
                token: context.token
            },
            selectSampleProduct: {
                id: null,
                label: null,
                description: null,
            },
            initSampleProduct: null
        };

        $scope.event = {
            init: function () {
                breakDownService.serviceUrl = context.serviceUrl;
                breakDownService.token = context.token;

                $scope.selectedRadio = "model";

                jQuery('#content').show();
            },
            changeInitOption: function (value) {
                if (value == "model") {
                    // Clear value SampleProduct
                    $scope.initParam.sampleProductID = 0;
                    $scope.autoSampleProduct.selectSampleProduct.id = null;
                    $scope.autoSampleProduct.selectSampleProduct.label = null;
                    $scope.autoSampleProduct.selectSampleProduct.description = null;
                    $scope.autoSampleProduct.initSampleProduct = null;

                    // Clear value FreeItem
                    $scope.initParam.freeItemDescription = null;
                } else if (value == "sampleProduct") {
                    // Clear value Model
                    $scope.initParam.modelID = 0;
                    $scope.autoModel.selectModel.id = null;
                    $scope.autoModel.selectModel.label = null;
                    $scope.autoModel.selectModel.description = null;
                    $scope.autoModel.initModel = null;

                    // Clear value FreeItem
                    $scope.initParam.freeItemDescription = null;
                } else {
                    // Clear value Model
                    $scope.initParam.modelID = 0;
                    $scope.autoModel.selectModel.id = null;
                    $scope.autoModel.selectModel.label = null;
                    $scope.autoModel.selectModel.description = null;
                    $scope.autoModel.initModel = null;

                    // Clear value SampleProduct
                    $scope.initParam.sampleProductID = 0;
                    $scope.autoSampleProduct.selectSampleProduct.id = null;
                    $scope.autoSampleProduct.selectSampleProduct.label = null;
                    $scope.autoSampleProduct.selectSampleProduct.description = null;
                    $scope.autoSampleProduct.initSampleProduct = null;
                }
            },
            getModelID: function (selectModel) {
                if (selectModel != null) {
                    $scope.initParam.modelID = selectModel.id;
                }
            },
            getSampleProductID: function (selectSampleProduct) {
                if (selectSampleProduct != null) {
                    $scope.initParam.sampleProductID = selectSampleProduct.id;
                }
            },
            changeModelUD: function (initModel) {
                if ($scope.autoModel.selectModel != null) {
                    if (initModel != $scope.autoModel.selectModel.label) {
                        $scope.initParam.modelID = 0;
                        $scope.autoModel.selectModel.id = null;
                        $scope.autoModel.selectModel.label = '';
                        $scope.autoModel.selectModel.description = '';
                    }
                }
            },
            changeSampleProductUD: function (initSampleProduct) {
                if ($scope.autoSampleProduct.selectSampleProduct != null) {
                    if (initSampleProduct != $scope.autoSampleProduct.selectSampleProduct.label) {
                        $scope.initParam.sampleProductID = 0;
                        $scope.autoSampleProduct.selectSampleProduct.id = null;
                        $scope.autoSampleProduct.selectSampleProduct.label = '';
                        $scope.autoSampleProduct.selectSampleProduct.description = '';
                    }
                }
            },
            backUrl: function () {
                window.location = context.backUrl;
            },
            nextEdit: function () {
                if ($scope.initParam.modelID == null && $scope.selectedRadio == 'model') {
                    jsHelper.showMessage('warning', 'Please select Model to create Breakdown');
                } else if ($scope.initParam.sampleProductID == null && $scope.selectedRadio == 'sampleProduct') {
                    jsHelper.showMessage('warning', 'Please select SampleProduct to create Breakdown');
                } else if (($scope.initParam.freeItemDescription == null || $scope.initParam.freeItemDescription == '') && $scope.selectedRadio == 'freeItem') {
                    jsHelper.showMessage('warning', 'Please input Description for Free Item to create Breakdown');
                } else {
                    window.open(context.nextUrl + $scope.initParam.breakdownID + '?modelID=' + $scope.initParam.modelID + '&sampleProductID=' + $scope.initParam.sampleProductID + '&offerLineID=' + $scope.initParam.offerLineID, '_blank');
                }
            }
        };

        $scope.method = {
        };

        $timeout(function () {
            $scope.event.init();
        }, 0);
    };
})();