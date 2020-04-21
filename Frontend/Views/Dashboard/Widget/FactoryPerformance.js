
angular.module('tilsoftApp').controller('factoryPerformanceController', ['$scope', '$rootScope', '$timeout', '$filter', '$interval', function ($scope, $rootScope, $timeout, $filter, $interval) {
    $scope.factoryPerformance = {};
    //
    // property
    //    
    $scope.factoryPerformance.data = [];

    //
    //capcity monitoring property
    //
    $scope.factoryPerformance.totalCapacityAndOrderByWeekDatas = [];
    $scope.factoryPerformance.capacityMonitoringHeaders = [];
    $scope.factoryPerformance.capacityMonitoringTotalRow = [];
    $scope.factoryPerformance.capacityMonitoringWeeklyTotalRow = [];
    $scope.factoryPerformance.capacityMonitoringHeaders_x2 = [];

    $scope.factoryPerformance.listHeaderCapacity = [];
    $scope.factoryPerformance.listDetail = [];

    $scope.factoryPerformance.shippedTotal = {
        factoryUD: 'TOTAL',
        totalShippedQnt40HCLastSeason: 0,
        totalShippedQnt40HC: 0,
        totalProducedQnt40HC: 0
    };
    $scope.factoryPerformance.selection = {
        season: jsHelper.getCurrentSeason(),
        factory: { factoryID: factoryPerformanceContext.factoryID, factoryUD: '' },
        factoryProduced: { factoryID: factoryPerformanceContext.factoryID, factoryUD: '' }
    };


    //
    // event
    //
    $scope.factoryPerformance.event = {
        init: function () {
            $timeout(function () {
                //init
                $scope.factoryPerformance.event.generateHtml();
            }, 0);
        },
        generateHtml: function () {
            $scope.factoryPerformance.shippedTotal = {
                factoryUD: 'TOTAL',
                totalShippedQnt40HCLastSeason: 0,
                totalShippedQnt40HC: 0,
                totalProducedQnt40HC: 0
            }
            jQuery('#highchartWeeklyShipped').hide();
            jQuery('#datatableWeeklyShipped').hide();
            jQuery('#highchartWeeklyProduced').hide();
            jQuery('#datatableWeeklyProduced').hide();
            jQuery('#highchartWeeklyShipped2nd').hide();
            jQuery('#highchartWeeklyShipped3rd').hide();

            //get data from server
            $scope.factoryPerformance.method.getHtmlReport(
                $scope.factoryPerformance.selection.season,
                factoryPerformanceContext.factoryID,
                function (data) {
                    $scope.factoryPerformance.totalCapacityAndOrderByWeekDatas = [];
                    $scope.factoryPerformance.capacityMonitoringHeaders = [];
                    $scope.factoryPerformance.capacityMonitoringHeaders_x2 = [];
                    $scope.factoryPerformance.data = data.data;
                    $scope.factoryPerformance.totalCapacityAndOrderByWeekDatas = data.data.totalCapacityAndOrderByWeekDatas;

                    //capacity section
                    $scope.factoryPerformance.capacityMonitoringHeaders = $scope.factoryPerformance.method.getHeader();

                    //console.log($scope.capacityMonitoringHeaders);

                    //angular.forEach($scope.capacityMonitoringHeaders, function (item) {
                    //    $scope.capacityMonitoringTotalRow.push({ qnt: 0, type: 1, column: '' });
                    //});
                    var totalCapacity = 0.00;
                    var totalOrder = 0.00;
                    var totalPreOrder = 0.00;
                    var totalPreProduced = 0.00;
                    var totalDelta = 0.00;
                    angular.forEach($scope.factoryPerformance.data.totalCapacityAndOrderDatas, function (item) {
                        totalCapacity += item.totalCapacity;
                        totalOrder += item.totalOrderQnt;
                        totalPreOrder += item.totalPreOrderQnt;
                        totalPreProduced += item.totalPreProducedQnt;
                        totalDelta += item.totalDeltaQnt;
                    });
                    $scope.factoryPerformance.capacityMonitoringTotalRow = {
                        totalCapacity: totalCapacity,
                        totalOrder: totalOrder,
                        totalPreOrder: totalPreOrder,
                        totalPreProduced: totalPreProduced,
                        totalDelta: totalDelta,
                        weeklyDetail: []
                    }

                    angular.forEach($scope.factoryPerformance.data.totalCapacityAndOrderDatas, function (fItem) {
                        var result = [];
                        angular.forEach($scope.factoryPerformance.capacityMonitoringHeaders, function (wItem) {
                            totalCapacity = 0.00;
                            totalOrder = 0.00;
                            totalPreOrder = 0.00;
                            totalPreProduced = 0.00;
                            totalDelta = 0.00;
                            angular.forEach($scope.factoryPerformance.totalCapacityAndOrderByWeekDatas, function (item) {
                                if (item.factoryID === fItem.factoryID && item.weekIndex === wItem.weekIndex) {
                                    result.push({ qnt: parseFloat(item.capacity), type: 1 });
                                    result.push({ qnt: parseFloat(item.orderQnt), type: 2 });
                                    result.push({ qnt: parseFloat(item.preOrderQnt), type: 3 });
                                    result.push({ qnt: parseFloat(item.preProducedQnt), type: 4 });
                                    result.push({ qnt: parseFloat(item.deltaQnt), type: 5 });
                                }

                                if (item.weekIndex === wItem.weekIndex) {
                                    totalCapacity += item.capacity;
                                    totalOrder += item.orderQnt;
                                    totalPreOrder += item.preOrderQnt;
                                    totalPreProduced += item.preProducedQnt;
                                    totalDelta += item.deltaQnt;
                                }
                            });
                        });
                        fItem.detail = result;
                    });



                    angular.forEach($scope.factoryPerformance.capacityMonitoringHeaders, function (wItem) {
                        totalCapacity = 0.00;
                        totalOrder = 0.00;
                        totalPreOrder = 0.00;
                        totalPreProduced = 0.00;
                        totalDelta = 0.00;
                        angular.forEach($scope.factoryPerformance.totalCapacityAndOrderByWeekDatas, function (item) {
                            if (item.weekIndex === wItem.weekIndex) {
                                totalCapacity += item.capacity;
                                totalOrder += item.orderQnt;
                                totalPreOrder += item.preOrderQnt;
                                totalPreProduced += item.preProducedQnt;
                                totalDelta += item.deltaQnt;
                            }
                        });
                        $scope.factoryPerformance.capacityMonitoringTotalRow.weeklyDetail.push({ qnt: totalCapacity, type: 1 });
                        $scope.factoryPerformance.capacityMonitoringTotalRow.weeklyDetail.push({ qnt: totalOrder, type: 2 });
                        $scope.factoryPerformance.capacityMonitoringTotalRow.weeklyDetail.push({ qnt: totalPreOrder, type: 3 });
                        $scope.factoryPerformance.capacityMonitoringTotalRow.weeklyDetail.push({ qnt: totalPreProduced, type: 4 });
                        $scope.factoryPerformance.capacityMonitoringTotalRow.weeklyDetail.push({ qnt: totalDelta, type: 5 });
                    });

                    console.log($scope.factoryPerformance.capacityMonitoringTotalRow);

                    //
                    // calculate total shipped
                    //
                    angular.forEach($scope.factoryPerformance.data.annualShippedData, function (item) {
                        $scope.factoryPerformance.shippedTotal.totalShippedQnt40HCLastSeason += item.totalShippedQnt40HCLastSeason;
                        $scope.factoryPerformance.shippedTotal.totalShippedQnt40HC += item.totalShippedQnt40HC;
                        $scope.factoryPerformance.shippedTotal.totalProducedQnt40HC += item.totalProducedQnt40HC;
                    });

                    //
                    // create the highcharts for total shipped
                    //
                    var seriData = [{ name: 'Shipped Last Season', data: [] }, { name: 'Shipped', data: [] }, { name: 'Actual Shipped', data: [], color: '#fffe11' }, { name: 'Produced', data: [] }];
                    var categoryData = [];
                    angular.forEach($scope.factoryPerformance.data.annualShippedData, function (item) {
                        var factoryUD = "";
                        if ($scope.factoryPerformance.method.getFactoryInfo(item.factoryID) !== null && $scope.factoryPerformance.method.getFactoryInfo(item.factoryID) !== undefined) {
                            factoryUD = $scope.factoryPerformance.method.getFactoryInfo(item.factoryID).factoryUD;
                        }
                        categoryData.push(factoryUD);
                        seriData[0].data.push(item.totalShippedQnt40HCLastSeason);
                        seriData[1].data.push(item.totalShippedQnt40HC);
                        seriData[2].data.push(item.totalShippCont);
                        seriData[3].data.push(item.totalProducedQnt40HC);
                    });
                    var chart = Highcharts.chart('highchartAnnualShipped', {
                        chart: {
                            type: 'column',
                            zoomType: 'x'
                        },
                        title: {
                            text: 'Total Shipped/Produced Report'
                        },
                        xAxis: {
                            type: 'category',
                            categories: categoryData
                        },
                        yAxis: {
                            title: {
                                text: ''
                            },
                            labels: {
                                format: '{value:,.2f}'
                            }
                        },
                        legend: {
                            layout: 'vertical',
                            align: 'right',
                            verticalAlign: 'middle',
                            floating: true
                        },
                        plotOptions: {
                            series: {
                                borderWidth: 0,
                                dataLabels: {
                                    enabled: true,
                                    format: '{point.y:,.2f}'
                                }
                            }
                        },
                        tooltip: {
                            headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:,.2f}</b>'
                        },
                        series: seriData
                    });
                    chart.redraw();

                    //
                    // create the highcharts for total produced
                    //
                    seriData = [{ name: 'Produced Last Season', data: [] }, { name: 'Produced Current Season', data: [] }];
                    categoryData = [];
                    angular.forEach($scope.factoryPerformance.data.factoryInfoData, function (item) {
                        categoryData.push(item.factoryUD);
                    });
                    angular.forEach($scope.factoryPerformance.data.annualProducedData, function (item) {
                        seriData[0].data.push(item.totalProducedQnt40HCLastSeason);
                        seriData[1].data.push(item.totalProducedQnt40HC);
                    });
                    var chartProduced = Highcharts.chart('highchartAnnualProduced', {
                        chart: {
                            type: 'column',
                            zoomType: 'x'
                        },
                        title: {
                            text: 'Total Produced Containers'
                        },
                        xAxis: {
                            type: 'category',
                            categories: categoryData
                        },
                        yAxis: {
                            title: {
                                text: ''
                            },
                            labels: {
                                format: '{value:,.2f}'
                            }
                        },
                        legend: {
                            layout: 'vertical',
                            align: 'right',
                            verticalAlign: 'middle',
                            floating: true
                        },
                        plotOptions: {
                            series: {
                                borderWidth: 0,
                                dataLabels: {
                                    enabled: true,
                                    format: '{point.y:,.2f}'
                                }
                            }
                        },
                        tooltip: {
                            headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:,.2f}</b>'
                        },
                        series: seriData
                    });
                    chartProduced.redraw();

                    $('#widget-factoryPerformance-loading').hide();
                    $('#widget-factoryPerformance-container').show();
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    console.log(error);
                    $scope.factoryPerformance.data = null;
                    $scope.$apply();
                }
            );
        },
        generateWeeklyShipped: function (id) {
            var isContinue = true;
            var firstWeek = 0;
            //
            // create the highcharts for weekly shipped - 1st
            //
            $scope.factoryPerformance.selection.factory = $scope.factoryPerformance.method.getFactoryInfo(id);
            jQuery('#highchartWeeklyShipped').show();
            jQuery('#datatableWeeklyShipped').show();
            jQuery('#highchartWeeklyShipped2nd').show();
            jQuery('#highchartWeeklyShipped3rd').show();
            var seriData = [{ name: 'Shipped Last Season', data: [] }, { name: 'Shipped', data: [] }, { name: 'Actual Shipped', data: [], color: '#fffe11' }, { name: 'Produced', data: [] }];
            var categoryData = [];
            isContinue = false;
            angular.forEach($scope.factoryPerformance.data.weekInfoData, function (item) {
                if (firstWeek == 0) {
                    firstWeek = item.weekIndex;
                }
                if (item.weekIndex >= firstWeek && item.weekIndex <= 54) {
                    isContinue = true;
                }
                else {
                    isContinue = false;
                }
                if (isContinue) {
                    categoryData.push('W-' + item.weekIndex);
                }
            });
            isContinue = false;
            angular.forEach($scope.factoryPerformance.data.weeklyShippedData, function (item) {
                if (item.factoryID == id) {
                    if (firstWeek == 0) {
                        firstWeek = item.weekIndex;
                    }
                    if (item.weekIndex >= firstWeek && item.weekIndex <= 54) {
                        isContinue = true;
                    }
                    else {
                        isContinue = false;
                    }
                    if (isContinue) {
                        seriData[0].data.push(item.totalShippedQnt40HCLastSeason);
                        seriData[1].data.push(item.totalShippedQnt40HC);
                        seriData[2].data.push(item.totalShippCont);
                        seriData[3].data.push(item.totalProducedQnt40HC);
                    }
                }
            });
            var chart = Highcharts.chart('highchartWeeklyShipped', {
                chart: {
                    type: 'column',
                    zoomType: 'x'
                },
                title: {
                    text: 'Weekly Shipped/Produced Report (1st chart)'
                },
                subtitle: {
                    text: $scope.factoryPerformance.method.getFactoryInfo(id).factoryUD
                },
                xAxis: {
                    type: 'category',
                    categories: categoryData
                },
                yAxis: {
                    title: {
                        text: ''
                    },
                    labels: {
                        format: '{value:,.2f}'
                    }
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    floating: true
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y:,.2f}'
                        }
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:,.2f}</b>'
                },
                series: seriData
            });
            chart.redraw();

            //
            // create the highcharts for weekly shipped - 2nd
            //
            seriData = [{ name: 'Shipped Last Season', data: [] }, { name: 'Shipped', data: [] }, { name: 'Actual Shipped', data: [], color: '#fffe11' },{ name: 'Produced', data: [] }];
            categoryData = [];
            isContinue = false;
            angular.forEach($scope.factoryPerformance.data.weekInfoData, function (item) {
                if (item.weekIndex >= 1 && item.weekIndex <= 18) {
                    isContinue = true;
                }
                else {
                    isContinue = false;
                }
                if (isContinue) {
                    categoryData.push('W-' + item.weekIndex);
                }
            });
            isContinue = false;
            angular.forEach($scope.factoryPerformance.data.weeklyShippedData, function (item) {
                if (item.factoryID == id) {
                    if (item.weekIndex >= 1 && item.weekIndex <= 18) {
                        isContinue = true;
                    }
                    else {
                        isContinue = false;
                    }
                    if (isContinue) {
                        seriData[0].data.push(item.totalShippedQnt40HCLastSeason);
                        seriData[1].data.push(item.totalShippedQnt40HC);
                        seriData[2].data.push(item.totalShippCont);
                        seriData[3].data.push(item.totalProducedQnt40HC);
                    }
                }
            });
            var chart2nd = Highcharts.chart('highchartWeeklyShipped2nd', {
                chart: {
                    type: 'column',
                    zoomType: 'x'
                },
                title: {
                    text: 'Weekly Shipped/Produced Report (2nd chart)'
                },
                subtitle: {
                    text: $scope.factoryPerformance.method.getFactoryInfo(id).factoryUD
                },
                xAxis: {
                    type: 'category',
                    categories: categoryData
                },
                yAxis: {
                    title: {
                        text: ''
                    },
                    labels: {
                        format: '{value:,.2f}'
                    }
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    floating: true
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y:,.2f}'
                        }
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:,.2f}</b>'
                },
                series: seriData
            });
            chart2nd.redraw();

            //
            // create the highcharts for weekly shipped - 3rd
            //
            seriData = [{ name: 'Shipped Last Season', data: [] }, { name: 'Shipped', data: [] }, { name: 'Actual Shipped', data: [], color: '#fffe11' }, { name: 'Produced', data: [] }];
            categoryData = [];
            isContinue = false;
            angular.forEach($scope.factoryPerformance.data.weekInfoData, function (item) {
                if (item.weekIndex >= 19 && item.weekIndex < firstWeek) {
                    isContinue = true;
                }
                else {
                    isContinue = false;
                }
                if (isContinue) {
                    categoryData.push('W-' + item.weekIndex);
                }
            });
            isContinue = false;
            angular.forEach($scope.factoryPerformance.data.weeklyShippedData, function (item) {
                if (item.factoryID == id) {
                    if (item.weekIndex >= 19 && item.weekIndex < firstWeek) {
                        isContinue = true;
                    }
                    else {
                        isContinue = false;
                    }
                    if (isContinue) {
                        seriData[0].data.push(item.totalShippedQnt40HCLastSeason);
                        seriData[1].data.push(item.totalShippedQnt40HC);
                        seriData[2].data.push(item.totalShippCont);
                        seriData[3].data.push(item.totalProducedQnt40HC);
                    }
                }
            });
            var chart3rd = Highcharts.chart('highchartWeeklyShipped3rd', {
                chart: {
                    type: 'column',
                    zoomType: 'x'
                },
                title: {
                    text: 'Weekly Shipped/Produced Report (3rd chart)'
                },
                subtitle: {
                    text: $scope.factoryPerformance.method.getFactoryInfo(id).factoryUD
                },
                xAxis: {
                    type: 'category',
                    categories: categoryData
                },
                yAxis: {
                    title: {
                        text: ''
                    },
                    labels: {
                        format: '{value:,.2f}'
                    }
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    floating: true
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y:,.2f}'
                        }
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:,.2f}</b>'
                },
                series: seriData
            });
            chart3rd.redraw();
        },
        generateWeeklyProduced: function (id) {
            //
            // create the highcharts for total shipped
            //
            $scope.factoryPerformance.selection.factoryProduced = $scope.factoryPerformance.method.getFactoryInfo(id);
            jQuery('#highchartWeeklyProduced').show();
            jQuery('#datatableWeeklyProduced').show();
            var seriData = [{ name: 'Last Season', data: [] }, { name: 'Current Season', data: [] }];
            var categoryData = [];
            angular.forEach($scope.factoryPerformance.data.weekInfoData, function (item) {
                categoryData.push('W-' + item.weekIndex);
            });
            angular.forEach($scope.factoryPerformance.data.weeklyProducedData, function (item) {
                if (item.factoryID == id) {
                    seriData[0].data.push(item.totalProducedQnt40HCLastSeason);
                    seriData[1].data.push(item.totalProducedQnt40HC);
                }
            });
            var chart = Highcharts.chart('highchartWeeklyProduced', {
                chart: {
                    type: 'column',
                    zoomType: 'x'
                },
                title: {
                    text: 'Weekly Produced Report'
                },
                xAxis: {
                    type: 'category',
                    categories: categoryData
                },
                yAxis: {
                    title: {
                        text: ''
                    },
                    labels: {
                        format: '{value:,.2f}'
                    }
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle',
                    floating: true
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y:,.2f}'
                        }
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:,.2f}</b>'
                },
                series: seriData
            });
            chart.redraw();
        },
    };

    //
    // method
    //
    $scope.factoryPerformance.method = {

        getHeader: function () {
            var result = [];
            var indexs = [];
            angular.forEach($scope.factoryPerformance.totalCapacityAndOrderByWeekDatas, function (item) {
                if (indexs.indexOf(item.weekIndex) < 0) {
                    indexs.push(item.weekIndex);
                    result.push({
                        weekIndex: item.weekIndex,
                        weekStart: item.weekStart,
                        weekEnd: item.weekEnd,
                    });
                }
            });
            return result;
        },

        getFactoryInfo: function (id) {
            if ($scope.factoryPerformance.data !== null) {
                var returnObj = null;
                angular.forEach($scope.factoryPerformance.data.factoryInfoData, function (item) {
                    if (item.factoryID == id) {
                        returnObj = item;
                    }
                });
                return returnObj;
            }
        },


        getCapacityInfo: function (factoryID, weekIndex) {
            if ($scope.factoryPerformance.totalCapacityAndOrderByWeekDatas === null) return {};
            var x = $scope.factoryPerformance.totalCapacityAndOrderByWeekDatas;
            var items = x.filter(o => o.factoryID === factoryID && o.weekIndex === weekIndex);
            if (items === null || items.length === 0) {
                return {};
            }
            return items[0];
        },

        getHtmlReport: function (season, factoryID, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + factoryPerformanceContext.token
                },
                type: "POST",
                dataType: 'json',
                url: factoryPerformanceContext.serviceUrl + 'gethtmlreport?season=' + season + '&factoryID=' + factoryID,
                beforeSend: function () {
                    //jsHelper.loadingSwitch(true);
                },
                success: function (data) {
                    //jsHelper.loadingSwitch(false);
                    successCallBack(data);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    //jsHelper.loadingSwitch(false);
                    errorCallBack(xhr.responseJSON.exceptionMessage);
                }
            });
        }
    };


    //auto load after 15'
    $interval(function () {
        $scope.factoryPerformance.event.init();
    }, 900000); //15' refesh auto

    $scope.factoryPerformance.event.init();


}]);