(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', preOrderPlanningMngController);
    preOrderPlanningMngController.$inject = ['$scope', '$timeout', 'dataService'];

    function preOrderPlanningMngController($scope, $timeout, preOrderPlanningMngService) {
        $scope.data = [];
        $scope.totalRows = 0;
        $scope.totalPage = 0;
        $scope.pageIndex = 1;

        $scope.factoryID = context.factoryID;

        $scope.supportSeason = [];
        $scope.supportFactory = [];

        $scope.filters = {
            season: jsHelper.getCurrentSeason(),
            factoryID: null,
        };

        $scope.gridHandler = {
            loadMore: function () {
            },
            sort: function (sortedBy, sortedDirection) {
            },
            isTriggered: false
        };

        $scope.event = {
            init: function () {
                preOrderPlanningMngService.serviceUrl = context.serviceUrl;
                preOrderPlanningMngService.token = context.token;
                preOrderPlanningMngService.searchFilter.pageSize = context.pageSize;

                preOrderPlanningMngService.getInit(
                    function (data) {
                        $scope.supportSeason = data.data.supportSeason;
                        $scope.supportFactory = data.data.supportFactory;
                        
                        jQuery('#content').show();

                        $timeout(function () {
                            if ($scope.factoryID !== undefined && $scope.factoryID !== null && $scope.factoryID !== '') {
                                $scope.filters.factoryID = $scope.factoryID;
                                $scope.event.search();
                            }
                        }, 50);
                    },
                    function (error) {
                    });
            },
            search: function (isLoadMore) {
                preOrderPlanningMngService.searchFilter.filters = $scope.filters;

                preOrderPlanningMngService.search(
                    function (data) {
                        $scope.data = data.data;
                        $scope.gridHandler.isTriggered = true;
                    },
                    function (error) {
                    });
            },
            getSubDelta: function (item) {
                var capacity = parseFloat(item.totalCapacityByWeekly);
                var officalOrder = parseFloat(0);
                var preOrder = parseFloat(0);
                var preProduction = parseFloat(0);

                // officalOrder(scale) > 0, officalOrder(scale), officalOrder
                if (item.officialOrderScaledQnt !== null && item.officialOrderScaledQnt !== '' && item.officialOrderScaledQnt > 0) {
                    officalOrder = parseFloat(item.officialOrderScaledQnt);
                } else {
                    if (item.totalOfficalOrder !== null && item.totalOfficalOrder !== '') {
                        officalOrder = parseFloat(item.totalOfficalOrder);
                    }
                }

                // preOrder(scale) > 0, preOrder(scale), preOrder
                if (item.preOrderScaledQnt !== null && item.preOrderScaledQnt !== '' && item.preOrderScaledQnt > 0) {
                    preOrder = parseFloat(item.preOrderScaledQnt);
                } else {
                    if (item.preOrderQnt !== null && item.preOrderQnt !== '') {
                        preOrder = parseFloat(item.preOrderQnt);
                    }
                }

                // preProduction(scale) > 0, preProduction(scale), preProduction
                if (item.preProducedScaledQnt !== null && item.preProducedScaledQnt !== '' && item.preProducedScaledQnt > 0) {
                    preProduction = parseFloat(item.preProducedScaledQnt);
                } else {
                    if (item.preProducedQnt !== null && item.preProducedQnt !== '') {
                        preProduction = parseFloat(item.preProducedQnt);
                    }
                }

                return capacity - (officalOrder + preOrder - preProduction);
            },
            update: function () {
                preOrderPlanningMngService.updateData(
                    1,
                    $scope.filters.season,
                    $scope.filters.factoryID,
                    $scope.data,
                    function (data) {
                        $scope.data = data.data;

                        $timeout(function () {
                            $scope.method.drawChart();
                        }, 0);
                    },
                    function (error) {
                    });
            },
            getSubTotalOrder: function (item) {
                var officalOrder = parseFloat(0);
                var preOrder = parseFloat(0);
                var preProduction = parseFloat(0);

                // officalOrder(scale) > 0, officalOrder(scale), officalOrder
                if (item.officialOrderScaledQnt !== null && item.officialOrderScaledQnt !== '' && item.officialOrderScaledQnt > 0) {
                    officalOrder = parseFloat(item.officialOrderScaledQnt);
                } else {
                    if (item.totalOfficalOrder !== null && item.totalOfficalOrder !== '') {
                        officalOrder = parseFloat(item.totalOfficalOrder);
                    }
                }

                // preOrder(scale) > 0, preOrder(scale), preOrder
                if (item.preOrderScaledQnt !== null && item.preOrderScaledQnt !== '' && item.preOrderScaledQnt > 0) {
                    preOrder = parseFloat(item.preOrderScaledQnt);
                } else {
                    if (item.preOrderQnt !== null && item.preOrderQnt !== '') {
                        preOrder = parseFloat(item.preOrderQnt);
                    }
                }

                // preProduction(scale) > 0, preProduction(scale), preProduction
                if (item.preProducedScaledQnt !== null && item.preProducedScaledQnt !== '' && item.preProducedScaledQnt > 0) {
                    preProduction = parseFloat(item.preProducedScaledQnt);
                } else {
                    if (item.preProducedQnt !== null && item.preProducedQnt !== '') {
                        preProduction = parseFloat(item.preProducedQnt);
                    }
                }

                return (officalOrder + preOrder - preProduction);
            },
        };

        $scope.method = {
            drawChart: function () {
                if ($scope.data !== null || $scope.data.length > 0) {
                    var categoryData = [];

                    var paramData = {
                        capacity: { name: 'Capacity', data: [] },
                        totalOrder: { name: 'Total Order', data: [] }
                    }

                    for (var i = 0; i < $scope.data.length; i++) {
                        var item = $scope.data[i];

                        var newItem = item.weekIndex.toString() + ' (' + item.displayWeekStart + ' - ' + item.displayWeekEnd + ')';

                        categoryData.push(newItem);
                        paramData.capacity.data.push(item.totalCapacityByWeekly);

                        var totalOrder = $scope.event.getSubTotalOrder(item)
                        paramData.totalOrder.data.push(totalOrder);
                    }

                    Highcharts.chart('chartPreOrderPlanning', {
                        title: { text: $scope.method.getFactoryName($scope.filters.factoryID) + ' CAPACITY' },
                        xAxis: {
                            categories: categoryData,
                            labels: {
                                rotation: -90
                            },
                        },
                        yAxis: {
                            min: 0,
                            title: { text: 'Pre-Order Planning' },
                            stackLabels: {
                                enabled: true,
                                style: {
                                    fontWeight: 'bold',
                                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                                },
                            },
                        },
                        legend: {
                            backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                            shadow: true
                        },
                        tooltip: {
                            headerFormat: 'Week <b>{point.x}</b><br/>',
                            pointFormat: '{series.name}: {point.y} '
                        },
                        series: [{
                            type: 'column',
                            color: '#0373fc',
                            name: 'Capacity',
                            data: paramData.capacity.data
                        }, {
                            type: 'column',
                            color: '#ff6a00',
                            name: 'Total Order',
                            data: paramData.totalOrder.data
                        }]
                    });
                }
            },
            getFactoryName: function (factoryID) {
                for (var i = 0; i < $scope.supportFactory.length; i++) {
                    var item = $scope.supportFactory[i];

                    if (item.factoryID.toString() === factoryID) {
                        return item.factoryUD;
                    }
                }

                return null;
            },
            getTotalCapacity: function () {
                var totalCapacity = 0;

                for (var i = 0; i < $scope.data.length; i++) {
                    var item = $scope.data[i];

                    if (item.totalCapacityByWeekly !== null) {
                        totalCapacity = totalCapacity + item.totalCapacityByWeekly;
                    }
                }

                return totalCapacity;
            },
            getTotalOrder: function () {
                var totalOrder = 0;

                for (var i = 0; i < $scope.data.length; i++) {
                    var item = $scope.data[i];
                    var qntOrder = $scope.event.getSubTotalOrder(item);

                    if (qntOrder !== null) {
                        totalOrder = totalOrder + qntOrder;
                    }
                }

                return totalOrder;
            },
            getTotalDelta: function () {
                var totalDelta = 0;

                for (var i = 0; i < $scope.data.length; i++) {
                    var item = $scope.data[i];
                    var qntDelta = $scope.event.getSubDelta(item);

                    if (qntDelta !== null) {
                        totalDelta = totalDelta + qntDelta;
                    }
                }

                return totalDelta;
            },
            getTotalOfficalOrder: function () {
                var totalOfficalOrder = 0;

                for (var i = 0; i < $scope.data.length; i++) {
                    var item = $scope.data[i];

                    if (item.totalOfficalOrder !== null) {
                        totalOfficalOrder = totalOfficalOrder + item.totalOfficalOrder;
                    }
                }

                return totalOfficalOrder;
            },
            getTotalOrderScale: function () {
                var totalOrderScale = 0;

                for (var i = 0; i < $scope.data.length; i++) {
                    var item = $scope.data[i];

                    if (item.officialOrderScaledQnt !== null && item.officialOrderScaledQnt !== '') {
                        totalOrderScale = totalOrderScale + item.officialOrderScaledQnt;
                    }
                }


                return totalOrderScale;
            },
            getTotalPreOrder: function () {
                var totalPreOrder = 0;

                for (var i = 0; i < $scope.data.length; i++) {
                    var item = $scope.data[i];

                    if (item.preOrderQnt !== null && item.preOrderQnt !== '') {
                        totalPreOrder = totalPreOrder + item.preOrderQnt;
                    }
                }

                return totalPreOrder;
            },
            getTotalPreOrderScale: function () {
                var totalPreOrderScale = 0;

                for (var i = 0; i < $scope.data.length; i++) {
                    var item = $scope.data[i];

                    if (item.preOrderScaledQnt !== null && item.preOrderScaledQnt !== '') {
                        totalPreOrderScale = totalPreOrderScale + item.preOrderScaledQnt;
                    }
                }

                return totalPreOrderScale;
            },
            getTotalPreProduction: function () {
                var totalPreProduction = 0;

                for (var i = 0; i < $scope.data.length; i++) {
                    var item = $scope.data[i];

                    if (item.preProducedQnt !== null && item.preProducedQnt !== '') {
                        totalPreProduction = totalPreProduction + item.preProducedQnt;
                    }
                }

                return totalPreProduction;
            },
            getTotalPreProductionScale: function () {
                var totalPreProductionScale = 0;

                for (var i = 0; i < $scope.data.length; i++) {
                    var item = $scope.data[i];

                    if (item.preProducedScaledQnt !== null && item.preProducedScaledQnt !== '') {
                        totalPreProductionScale = totalPreProductionScale + item.preProducedScaledQnt;
                    }
                }

                return totalPreProductionScale;
            }
        };

        $timeout(function () {
            $scope.event.init();
        }, 0);
    };
})();