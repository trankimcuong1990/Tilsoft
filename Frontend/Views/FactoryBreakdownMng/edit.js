(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', factoryBreakdownMngController);
    factoryBreakdownMngController.$inject = ['$scope', 'dataService', '$timeout'];

    function factoryBreakdownMngController($scope, factoryBreakdownMngService, $timeout) {
        // variable
        $scope.data = [];
        $scope.sampleData = [];
        $scope.productionItemGroups = [];

        $scope.newDetailID = 0;

        // event
        $scope.event = {
            get: get,
            update: update,
            approve: approve,
            reset: reset,
            exportExcelDetail: exportExcelDetail,
            openSampleDetail: openSampleDetail,
            getSampleDetailData: getSampleDetailData,
            addModel: addModel,
            removeModel: removeModel,
        };

        function openSampleDetail() {
            factoryBreakdownMngService.getSampleDetailData(
                context.sampleProductID,
                function (data) {
                    $scope.sampleData = data.data;
                    jQuery('#frmSampleDetail').modal();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function getSampleDetailData() {
            factoryBreakdownMngService.getSampleDetailData(
            context.sampleProductID,
            function (data) {
                //if (context.id === '0') {
                //    window.location = context.refreshUrl + data.data.data.factoryBreakdownID + '?sampleProductID=' + data.data.data.sampleProductID;
                //}
                $scope.sampleData = data.data.data;
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            });
        };

        function get() {
            factoryBreakdownMngService.serviceUrl = context.serviceUrl;
            factoryBreakdownMngService.token = context.token;
            factoryBreakdownMngService.getData(
                context.id,
                context.sampleProductID,
                context.modelID,
                function (data) {
                    if (context.id === '0') {
                        window.location = context.refreshUrl + data.data.data.factoryBreakdownID + '?sampleProductID=' + data.data.data.sampleProductID;
                    }
                    $scope.data = data.data.data;

                    // Auto generate code Model
                    angular.forEach($scope.data.factoryBreakdownDetailDTOs, function (detailItem) {
                        detailItem.modelDetailUD = $scope.data.modelUD + '_' + ($scope.data.factoryBreakdownDetailDTOs.indexOf(detailItem) + 1).toString();
                    });
                    
                    if ($scope.data.sampleProductID !== null && $scope.data.isConfirmed) {
                        for (var i = $scope.data.factoryBreakdownDetailDTOs.length - 1; i >= 0; i--){
                            
                            if ($scope.data.factoryBreakdownDetailDTOs[i].quantity === 0) {
                                $scope.data.factoryBreakdownDetailDTOs.splice(i, 1);
                            }
                        }
                    }

                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
            
        };

        function update() {
            factoryBreakdownMngService.update(
                context.id,
                $scope.data,
                function (data) {
                    jsHelper.processMessage(data.message);

                    if (data.message.type === 0) {
                        window.location = context.refreshUrl + data.data.factoryBreakdownID + '?sampleProductID=' + data.data.sampleProductID + '&modelID=' + data.data.modelID;
                        $scope.data = data.data;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function approve() {
            if (confirm('Are you sure ?')) {
                factoryBreakdownMngService.approve(
                    context.id,
                    $scope.data,
                    function (data) {
                        window.location = context.refreshUrl + data.data.factoryBreakdownID + '?sampleProductID=' + data.data.sampleProductID;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
        };

        function reset() {
            if (confirm('Are you sure ?')) {
                factoryBreakdownMngService.resetData(
                    context.id,
                    $scope.data,
                    function (data) {
                        window.location = context.refreshUrl + data.data.factoryBreakdownID + '?sampleProductID=' + data.data.sampleProductID;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
        };

        function exportExcelDetail() {
            factoryBreakdownMngService.exportExcelDetail(
                context.id,
                function (data) {
                    if (data.message.type === 0) {
                        window.location = context.backendReportUrl + data.data;
                    }
                    else {
                        jsHelper.processMessageEx(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function addModel(modelUD) {
            var length = $scope.data.factoryBreakdownModels.length;
            $scope.data.factoryBreakdownModels.push({
                factoryBreakdownModelID: $scope.method.newFactoryBreakdownModelID(),
                modelUD: modelUD + "_" + (length + 1).toString(),
                modelNM: null,
                quantity: null,
                remark: null,
                });
        };

        function removeModel(model) {
            var index = $scope.data.factoryBreakdownModels.indexOf(model);
            $scope.data.factoryBreakdownModels.splice(index, 1);
        };

        $scope.method = {
            newFactoryBreakdownModelID: newFactoryBreakdownModelID,
        };

        function newFactoryBreakdownModelID() {
            return $scope.newDetailID--;
        };

        // default event
        $scope.event.get();
    }
})();