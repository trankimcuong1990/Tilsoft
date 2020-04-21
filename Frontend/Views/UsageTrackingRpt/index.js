//
// SUPPORT
//
jQuery('.search-filter').keypress(function(e){
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ui.select2']);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.filters = {
        fromDate: '',
        toDate: '',
        employeeID: [],
        companyID: [],
        moduleID: [],
        locationID: []
    };
    $scope.data = null;
    $scope.userDetail = {
        userId: null,
        fullName: null,
        overviewData: null,
        weeklyData: {
            overview: null,
            drilldown: null
        }
    };
    $scope.companyDetail = {
        companyId: null,
        companyNm: null,
        overviewData: null,
        weeklyData: {
            overview: null,
            drilldown: null
        }
    };
    $scope.moduleDetail = {
        moduleNM: null,
        details: null
    }
    $scope.employees = null;
    $scope.internalCompanies = null;
    $scope.modules = null;
    $scope.locations = null;

    if (employeeIDtemp != null && employeeIDtemp != -1) {
        $scope.filters.employeeID.push(employeeIDtemp.toString());
    }
    //
    // event
    //
    $scope.event = {
        reload: function () {
            dataService.getConclusion(
                $scope.filters.fromDate,
                $scope.filters.toDate,
                $scope.filters.employeeID,                
                $scope.filters.companyID,
                $scope.filters.moduleID,
                $scope.filters.locationID,
                function (data) {
                    $scope.data = data.data;
                    jQuery('#content').show();

                    // spark line
                    $timeout(function(){
                        angular.forEach($scope.data.activeCompanies, function (item) {
                            var sparkData = [];
                            angular.forEach($scope.data.activeCompanyWeeklyHits, function (sItem) {
                                if(sItem.companyID == item.companyID){
                                    sparkData.push(sItem.totalHit);
                                }
                            });
                            jQuery('#sparkChart-' + item.companyID).sparkline(sparkData, {type: 'line'});
                        });                        
                    }, 0);                    
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        loadUserChart: function () {
            jQuery('#frmUserChart').modal('show');

            // prepare high chart - user
            var userChart = Highcharts.chart('highchartWeeklyUserCount', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Weekly user count report'
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Total hit'
                    },
                    labels: {
                        format: '{value:,.0f}'
                    }
                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y:,.0f}'
                        }
                    }
                },
                tooltip: {
                    headerFormat: '',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:,.0f}</b>'
                },
                series: [{
                    name: 'Week',
                    data: []
                }]
            });

            var userChartSeriData = [];
            angular.forEach($scope.data.userMutations, function (item) {
                // processing seri data
                userChartSeriData.push({ name: 'Week ' + item.weekIndex, y: item.totalUser});
            });
            userChart.series[0].setData(userChartSeriData, true);
            userChart.redraw();
        },
        loadAllCompany: function () {
            dataService.getCompany(
                $scope.filters.fromDate,
                $scope.filters.toDate,
                $scope.filters.employeeID,
                $scope.filters.companyID,
                $scope.filters.moduleID,
                function (data) {
                    $scope.data.activeCompanies = data.data;
                },
                function (error) {
                }
            );
        },
        loadAllModule: function () {
            dataService.getModule(
                $scope.filters.fromDate,
                $scope.filters.toDate,
                $scope.filters.employeeID,
                $scope.filters.companyID,
                $scope.filters.moduleID,
                function (data) {
                    $scope.data.activeModules = data.data;
                },
                function (error) {
                }
            );
        },
        loadAllIP: function () {
            dataService.getIP(
                $scope.filters.fromDate,
                $scope.filters.toDate,
                $scope.filters.employeeID,
                $scope.filters.companyID,
                $scope.filters.moduleID,
                function (data) {
                    $scope.data.activeIPs = data.data;
                },
                function (error) {
                }
            );
        },
        loadAllBrowser: function () {
            dataService.getBrowser(
                $scope.filters.fromDate,
                $scope.filters.toDate,
                $scope.filters.employeeID,
                $scope.filters.companyID,
                $scope.filters.moduleID,
                function (data) {
                    $scope.data.activeBrowsers = data.data;
                },
                function (error) {
                }
            );
        },
        loadUserDetail: function (item) {
            $scope.userDetail.userId = item.userID;
            $scope.userDetail.fullName = item.employeeNM + (item.internalCompanyNM?' - ' + item.internalCompanyNM:'');
            dataService.getUserDetail(
                item.userID,
                $scope.filters.fromDate,
                $scope.filters.toDate,
                $scope.filters.moduleID,
                function (data) {
                    $scope.userDetail.overviewData = data.data.userDetails;
                    $scope.userDetail.weeklyData.overview = data.data.userDetailWeeklyOverviews;
                    $scope.userDetail.weeklyData.drilldown = data.data.userDetailWeeklyDetails;
                    jQuery('#frmUserDetail').modal('show');

                    //
                    // create the highcharts
                    // description: chart variable is declared in html page
                    //
                    // Create the user chart
                    var chart = Highcharts.chart('highchartWeeklyView', {
                        chart: {
                            type: 'column'
                        },
                        title: {
                            text: 'Weekly report'
                        },
                        subtitle: {
                            text: 'Click the columns to view detail'
                        },
                        xAxis: {
                            type: 'category'
                        },
                        yAxis: {
                            title: {
                                text: 'Total hit'
                            },
                            labels: {
                                format: '{value:,.0f}'
                            }
                        },
                        legend: {
                            enabled: false
                        },
                        plotOptions: {
                            series: {
                                borderWidth: 0,
                                dataLabels: {
                                    enabled: true,
                                    format: '{point.y:,.0f}'
                                }
                            }
                        },
                        tooltip: {
                            headerFormat: '',
                            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:,.0f}</b>'
                        },
                        series: [{
                            name: 'Week',
                            colorByPoint: true,
                            data: []
                        }],
                        drilldown: {
                            series: []
                        }
                    });

                    var seriData = [];
                    var drilldownData = [];
                    angular.forEach($scope.userDetail.weeklyData.overview, function (item) {
                        // processing seri data
                        seriData.push({ name: 'Week ' + item.weekIndex + '/' + item.yearIndex, y: item.totalHit, drilldown: 'week_' + item.weekIndex + '_' + item.yearIndex });

                        // processing drilldown data
                        var drilldownItemData = { name: 'Week ' + item.weekIndex + '/' + item.yearIndex, id: 'week_' + item.weekIndex + '_' + item.yearIndex, data: [] };
                        angular.forEach($scope.userDetail.weeklyData.drilldown, function (subItem) {
                            if (item.weekIndex === subItem.weekIndex && item.yearIndex === subItem.yearIndex) {
                                drilldownItemData.data.push([subItem.moduleNM, subItem.totalHit]);
                            }
                        }, null);
                        drilldownData.push(angular.copy(drilldownItemData));
                    }, null);
                    chart.options.drilldown.series = drilldownData;
                    chart.series[0].setData(seriData, true);

                    chart.redraw();
                },
                function (error) {
                    $scope.userDetail.details = null;
                }
            );
        },
        loadCompanyDetail: function (item) {
            $scope.companyDetail.companyId = item.companyID;
            $scope.companyDetail.companyNm = item.internalCompanyNM;
            dataService.getCompanyDetail(
                item.companyID,
                $scope.filters.fromDate,
                $scope.filters.toDate,
                $scope.filters.moduleID,
                function (data) {
                    $scope.companyDetail.overviewData = data.data.companyDetails;
                    $scope.companyDetail.weeklyData.overview = data.data.companyDetailWeeklyOverviews;
                    $scope.companyDetail.weeklyData.drilldown = data.data.companyDetailWeeklyDetails;
                    jQuery('#frmCompanyDetail').modal('show');

                    //
                    // Create the company highcharts
                    // description: chart variable is declared in html page
                    //
                    var companyChart = Highcharts.chart('highchartCompanyWeeklyView', {
                        chart: {
                            type: 'column'
                        },
                        title: {
                            text: 'Weekly report'
                        },
                        subtitle: {
                            text: 'Click the columns to view detail'
                        },
                        xAxis: {
                            type: 'category'
                        },
                        yAxis: {
                            title: {
                                text: 'Total hit'
                            },
                            labels: {
                                format: '{value:,.0f}'
                            }
                        },
                        legend: {
                            enabled: false
                        },
                        plotOptions: {
                            series: {
                                borderWidth: 0,
                                dataLabels: {
                                    enabled: true,
                                    format: '{point.y:,.0f}'
                                }
                            }
                        },
                        tooltip: {
                            headerFormat: '',
                            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:,.0f}</b>'
                        },
                        series: [{
                            name: 'Week',
                            colorByPoint: true,
                            data: []
                        }],
                        drilldown: {
                            series: []
                        }
                    });

                    var seriData = [];
                    var drilldownData = [];
                    angular.forEach($scope.companyDetail.weeklyData.overview, function (item) {
                        // processing seri data
                        seriData.push({ name: 'Week ' + item.weekIndex + '/' + item.yearIndex, y: item.totalHit, drilldown: 'week_' + item.weekIndex + '_' + item.yearIndex });

                        // processing drilldown data
                        var drilldownItemData = { name: 'Week ' + item.weekIndex + '/' + item.yearIndex, id: 'week_' + item.weekIndex + '_' + item.yearIndex, data: [] };
                        angular.forEach($scope.companyDetail.weeklyData.drilldown, function (subItem) {
                            if (item.weekIndex == subItem.weekIndex && item.yearIndex == subItem.yearIndex) {
                                drilldownItemData.data.push([subItem.moduleNM, subItem.totalHit]);
                            }
                        }, null);
                        drilldownData.push(angular.copy(drilldownItemData));
                    }, null);
                    companyChart.options.drilldown.series = drilldownData;
                    companyChart.series[0].setData(seriData, true);

                    companyChart.redraw();
                },
                function (error) {
                    $scope.companyDetail.overviewData = null;
                    $scope.companyDetail.weeklyData.overview = null;
                    $scope.companyDetail.weeklyData.drilldown = null;
                }
            );
        },
        loadModuleDetail: function (item) {
            $scope.moduleDetail.moduleNM = item.displayText;
            dataService.getModuleDetail(
                item.controllerName,
                $scope.filters.fromDate,
                $scope.filters.toDate,
                $scope.filters.employeeID,
                $scope.filters.companyID,
                function (data) {
                    $scope.moduleDetail.details = data.data;
                    jQuery('#frmModuleDetail').modal('show');
                },
                function (error) {
                    $scope.moduleDetail.details = null;
                }
            );
        },
        init: function () {
            //$scope.filters.fromDate = dataService.dateAdd(new Date(), -7);
            //$scope.filters.toDate = dataService.today();

            dataService.getSearchFilter(
                function (data) {
                    debugger;
                    $scope.employees = data.data.employees;
                    $scope.internalCompanies = data.data.internalCompanies;
                    $scope.modules = data.data.modules;
                    $scope.locations = data.data.locations;

                    $scope.event.reload();
                    //jQuery('#content').show();
                },
                function (error) {
                    $scope.employees = null;
                    $scope.internalCompanies = null;
                    $scope.modules = null;
                    $scope.locations = null;
                }
            );
        },
        prepareCacheData: function () {
            dataService.prepareCacheData(
                function (data) {
                    if (data.message.type === 0) {
                        if (data) {
                            $scope.event.reload();
                        }
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    console.log(error);
                }
            );
        }
    }

    //
    // method
    //
    $scope.method = {
        totalUserHit: function () {
            var result = 0;
            if ($scope.data && $scope.data.activeUsers) {
                angular.forEach($scope.data.activeUsers, function (item) {
                    result += parseInt(item.totalHit);
                }, null);
            }
            return result;
        },
        totalCompanyHit: function () {
            var result = 0;
            if ($scope.data && $scope.data.activeCompanies) {
                angular.forEach($scope.data.activeCompanies, function (item) {
                    result += parseInt(item.totalHit);
                }, null);
            }
            return result;
        },
        totalModuleHit: function () {
            var result = 0;
            if ($scope.data && $scope.data.activeModules) {
                angular.forEach($scope.data.activeModules, function (item) {
                    result += parseInt(item.totalHit);
                }, null);
            }            
            return result;
        },
        totalIPHit: function () {
            var result = 0;
            if ($scope.data && $scope.data.activeIPs) {
                angular.forEach($scope.data.activeIPs, function (item) {
                    result += parseInt(item.totalHit);
                }, null);
            }            
            return result;
        },
        totalBrowserHit: function () {
            var result = 0;
            if ($scope.data && $scope.data.activeBrowsers) {
                angular.forEach($scope.data.activeBrowsers, function (item) {
                    result += parseInt(item.totalHit);
                }, null);
            }            
            return result;
        }
    }


    //
    // init
    //
    $scope.event.init();
}]);