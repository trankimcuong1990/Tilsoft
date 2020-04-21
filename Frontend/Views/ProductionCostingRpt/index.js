function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.value,
            description: item.label
        }
    });
}
(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive']).controller('tilsoftController', productionCostingRptController);
    productionCostingRptController.$inject = ['$scope', '$timeout', 'dataService'];

    function productionCostingRptController($scope, $timeout, productionCostingRptService) {
        // variable
        $scope.dataWorkOrder = [];
        $scope.workOrders = [];
        $scope.selection = {
            workOrderID: null
        };
        $scope.piece = null;
        $scope.totalAmountVarianceQnt = 0;
        $scope.totalPlanCosting = 0;
        $scope.totalUsingCosting = 0;
        $scope.totalVarianceValueCosting = 0;
        $scope.totalVarianceCosting = 0;
        $scope.amountPlanCosting = 0;
        $scope.amountUsingCosting = 0;
        $scope.amountVarianceValueCosting = 0;
        $scope.amountVarianceCosting = 0;
        $scope.totalReceivingNotes = [];

        // variables for event quick-search with WorkOrder.
        $scope.workOrder = {
            id: null,
            label: null,
            description: null
        };
        $scope.ngRequestWorkOrder = {
            url: context.supportServiceUrl + 'getWorkOrder',
            token: context.token,
        };

        // event
        $scope.event = {
            init: init,
            selectWorkOrder: selectWorkOrder,
            generateExcel: generateExcel,
            search: search,
            loadWorkOrder: loadWorkOrder
        };
        $scope.method = {
            parseDataToList: parseDataToList,
            totalCostEveryWorkCenter: totalCostEveryWorkCenter
        };

        function init() {
            productionCostingRptService.serviceUrl = context.serviceUrl;
            productionCostingRptService.token = context.token;
            productionCostingRptService.supportServiceUrl = context.supportServiceUrl;
            jQuery('#content').show();
        };

        function selectWorkOrder(workOrder) {
            $scope.selection.workOrderID = workOrder.id;
        };

        function generateExcel() {
            productionCostingRptService.generateExcel(
                $scope.selection.workOrderID,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function search(workOrderID) {
            productionCostingRptService.getDataWithFilters(
                $scope.selection.workOrderID,
                function (data) {
                    //parse bom to bom list format
                    $scope.data = [];
                    $scope.totalAmountVarianceQnt = 0;
                    $scope.totalPlanCosting = 0;
                    $scope.totalUsingCosting = 0;
                    $scope.totalVarianceValueCosting = 0;
                    $scope.totalVarianceCosting = 0;
                    $scope.amountPlanCosting = 0;
                    $scope.amountUsingCosting = 0;
                    $scope.amountVarianceValueCosting = 0;
                    $scope.amountVarianceCosting = 0;
                    $scope.totalReceivingNotes = data.data.totalReceivingNotes;
                    $scope.method.parseDataToList(data.data.productionCostingData, $scope.data);
                    $scope.method.totalCostEveryWorkCenter($scope.data);
                    $timeout(function () { $scope.event.loadWorkOrder(); }, 10);
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function parseDataToList(productionCostingData, result) {
            if (productionCostingData !== null && productionCostingData.productionCostingDTOs !== null) {
                if (productionCostingData.productionItemTypeID === 2 && (productionCostingData.planQnt === null || productionCostingData.planQnt === 0) && (productionCostingData.usingQnt === null || productionCostingData.usingQnt === 0)) {
                    console.log("Missing value");
                }
                else {
                    //push to list result
                    result.push(productionCostingData);

                    //it just only assign piece index for child of root node
                    if (productionCostingData.parentBOMID === null) {
                        for (var i = 0; i < productionCostingData.productionCostingDTOs.length; i++) {
                            productionCostingData.productionCostingDTOs[i].pieceIndex = i + 1;
                        }
                    }

                    //assign number if child bom item for every child bom so we can sort to make sure code read from materil to component (material don't have chidl bom anymore and component can have child bom)
                    angular.forEach(productionCostingData.productionCostingDTOs, function (item) {
                        item.countChildBOM = item.productionCostingDTOs.length;
                        if (item.pieceIndex === undefined || item.pieceIndex === null) {
                            item.pieceIndex = productionCostingData.pieceIndex;
                        }
                    });
                    if (productionCostingData.productionItemTypeID === 1/* || productionCostingData.productionItemTypeID === 3*/) {
                        productionCostingData.usingQnt = 0;
                        productionCostingData.usingQnt = productionCostingData.totalReceivedQnt;
                    }

                    //sort base on number item of child bom
                    var bomSorted = productionCostingData.productionCostingDTOs.sort(function (a, b) { return a.countChildBOM - b.countChildBOM });
                    angular.forEach(bomSorted, function (item) {
                        $scope.method.parseDataToList(item, result);
                    });
                }
            }
        };

        function totalCostEveryWorkCenter(data) {
            //planCosting
            var planCostingPiece = 0;
            var planCostingComponent = 0;

            //usingCosting
            var usingCostingPiece = 0;
            var usingCostingComponent = 0;

            //variance value
            var varianceValuePiece = 0;
            var varianceValueComponent = 0;

            for (var i = data.length - 1; i > 0; i--) {
                var item = data[i];
                if (item.productionItemTypeID === 3) {//Piece
                    item.varianceQnt = null;
                    item.planCosting = planCostingPiece;
                    item.usingCosting = usingCostingPiece;
                    item.varianceValue = varianceValuePiece;
                    item.varianceCosting = item.varianceValue / item.planCosting * 100;
                    if (item.workCenterID === null) {
                        //Total Amount
                        $scope.totalPlanCosting += item.planCosting;
                        $scope.totalUsingCosting += item.usingCosting;
                        $scope.totalVarianceValueCosting += item.varianceValue;

                        planCostingPiece = 0;
                        planCostingComponent = 0;
                        usingCostingPiece = 0;
                        usingCostingComponent = 0;
                        varianceValuePiece = 0;
                        varianceValueComponent = 0;
                    }
                }
                if (item.productionItemTypeID === 2) { //Material
                    planCostingComponent += item.planCosting;
                    planCostingPiece += item.planCosting;
                    usingCostingComponent += item.usingCosting;
                    usingCostingPiece += item.usingCosting;
                    varianceValueComponent += item.varianceValue;
                    varianceValuePiece += item.varianceValue;
                }
                if (item.productionItemTypeID === 1) { //Component
                    item.varianceQnt = null;
                    item.planCosting = planCostingComponent;
                    item.usingCosting = usingCostingComponent;
                    item.varianceValue = varianceValueComponent;
                    item.varianceCosting = (item.varianceValue / item.planCosting) * 100;
                }
            }
            $scope.totalVarianceCosting += ($scope.totalVarianceValueCosting / $scope.totalPlanCosting) * 100;

            //Amount per product
            var amountPlanCost = 0;
            var amountUsingCost = 0;
            var amountVarianceValueCost = 0;
            if ($scope.data.length !== 0) {
                if ($scope.data[1].workOrderQnt === undefined || $scope.data[1].workOrderQnt === 0 || $scope.data[1].workOrderQnt === null) {
                    amountPlanCost = 0;
                    amountUsingCost = 0;
                    amountVarianceValueCost = 0;
                }
                else {
                    amountPlanCost = parseFloat($scope.totalPlanCosting / $scope.data[1].workOrderQnt);
                    amountUsingCost = parseFloat($scope.totalUsingCosting / $scope.data[1].workOrderQnt);
                    amountVarianceValueCost = parseFloat($scope.totalVarianceValueCosting / $scope.data[1].workOrderQnt);
                }
                $scope.amountPlanCosting = parseInt(amountPlanCost);
                $scope.amountUsingCosting = parseInt(amountUsingCost);
                $scope.amountVarianceValueCosting = parseInt(amountVarianceValueCost);
                if (amountPlanCost === 0) {
                    $scope.amountVarianceCosting = 0;
                }
                else {
                    $scope.amountVarianceCosting = parseFloat((amountVarianceValueCost / amountPlanCost) * 100);
                }
            }  
        };
        function loadWorkOrder() {
            productionCostingRptService.load(
                $scope.selection.workOrderID,
                null,
                function (data) {
                    $scope.dataWorkOrder = [];
                    $scope.dataWorkOrder = data.data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    //$scope.dataWorkOrder = null;
                    //$scope.$apply();
                }
            );
        };

        // default event
        $scope.event.init();
    }
})();