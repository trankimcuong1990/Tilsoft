//
//APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngSanitize']).config([
    '$compileProvider',
    function ($compileProvider) {
        $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|skype|chrome-extension):/);
    }
]);


tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', function ($scope, $timeout) {
    $scope.data = null;
    $scope.userDataRpt = null;
    $scope.hitCountDataRpt = null;
    $scope.sData = null;
    $scope.verifyInfo = {
        username: '',
        password: ''
    }

    $scope.event = {
        init: function () {
            employeeMngService.getOverviewData(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.userDataRpt = data.data.userDataRpt;
                    $scope.hitCountDataRpt = data.data.hitCountDataRpt;
                    $scope.$apply();
                    jQuery('#content').show();

                    $('#page-title').html(context.pageTitle + ': ' + $scope.data.employeeNM);

                    // CHART USER INCREASEMENT
                    var param = {
                        high: { name: 'High', data: [] },
                        intense: { name: 'Intense', data: [] },
                        aboveAverage: { name: 'Above Average', data: [] },
                        average: { name: 'Average', data: [] },
                        low: { name: 'Low', data: [] },
                        veryLow: { name: 'Very Low', data: [] },
                        none: { name: 'None', data: [] },
                        target: { name: 'Target', data: [] },
                        weekIndex: []
                    }
                    var target = 0;
                    var keepGoing = true;
                    var i = 0;
                    debugger;
                    angular.forEach($scope.userDataRpt, function (value, key) {
                        for (var j = 0; j < $scope.hitCountDataRpt.length; j++) {
                            var item2 = $scope.hitCountDataRpt[j];
                            var total = parseInt(value.cHigh) + parseInt(value.cIntense) + parseInt(value.cAboveAverage) + parseInt(value.cAverage) + parseInt(value.cLow) + parseInt(value.cVeryLow) + parseInt(value.cNone);
                            if (value.weekIndex == item2.weekIndex && item2.totalHit > 0) {
                                target = (item2.totalHit / total);

                            }
                            else if (value.weekIndex == item2.weekIndex && item2.totalHit == 0) {
                                target = 0;

                            }

                        }
                        this.high.data.push(parseInt(value.cHigh));
                        this.intense.data.push(parseInt(value.cIntense));
                        this.aboveAverage.data.push(parseInt(value.cAboveAverage));
                        this.average.data.push(parseInt(value.cAverage));
                        this.low.data.push(parseInt(value.cLow));
                        this.veryLow.data.push(parseInt(value.cVeryLow));
                        this.none.data.push(parseInt(value.cNone));
                        this.target.data.push(
                            target
                        );
                        this.weekIndex.push("Week " + value.weekIndex + "<br/>From " + value.weekStart + "<br/>To " + value.weekEnd);
                    }, param);

                    //for (var i = 0; i < $scope.data.userDataRpt.length; i++) {
                    //    var item = $scope.data.userDataRpt[i];
                    //    for (var j =0 ; j < $scope.data.hitCountDataRpt.length; j++) {
                    //        var item2 = $scope.data.hitCountDataRpt[j];
                    //        if (item.weekIndex == item2.weekIndex && item2.totalHit > 0) {
                    //            param.target.data = item2.totalHit / param.target.data;
                    //        }


                    //    }
                    //}

                    // render high chart
                    var chart = Highcharts.chart('weeklyChart', {
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
                        },
                        {
                            type: 'spline',
                            color: '#FF0000 ',
                            name: 'Target',
                            data: param.target.data,
                            marker: {
                                lineWidth: 2,
                                lineColor: Highcharts.getOptions().colors[3],
                                fillColor: 'white'
                            }
                        }],
                        drilldown: {
                            series: []
                        }
                    });

                    var seriData = [];
                    var drilldownData = [];
                    angular.forEach($scope.data.userDetailWeeklyOverviewDTOs, function (item) {
                        // processing seri data
                        seriData.push({ name: 'Week ' + item.weekIndex + '/' + item.yearIndex, y: item.totalHit, drilldown: 'week_' + item.weekIndex + '_' + item.yearIndex });

                        // processing drilldown data
                        var drilldownItemData = { name: 'Week ' + item.weekIndex + '/' + item.yearIndex, id: 'week_' + item.weekIndex + '_' + item.yearIndex, data: [] };
                        angular.forEach($scope.data.userDetailWeeklyDetailDTOs, function (subItem) {
                            if (item.weekIndex == subItem.weekIndex && item.yearIndex == subItem.yearIndex) {
                                drilldownItemData.data.push([subItem.moduleNM, subItem.totalHit]);
                            }
                        }, null);
                        drilldownData.push(angular.copy(drilldownItemData));
                    }, null);
                    chart.options.drilldown.series = drilldownData;
                    chart.series[0].setData(seriData, true);

                    chart.redraw();

                    if (data.message.type == 2) {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.datdata = null;
                    $scope.$apply();
                }
            );
        },

        verifyAccount: function () {
            $('#frmVerifyAccount').modal('show');
        },
        verifyAccountPost: function () {
            if ($scope.verifyInfo.username && $scope.verifyInfo.password) {
                $('#frmVerifyAccount').modal('hide');
                employeeMngService.getOverviewSensitiveData(
                    context.id,
                    $scope.verifyInfo.username,
                    $scope.verifyInfo.password,
                    function (data) {
                        if (data.message.type == 0) {
                            // success
                            $scope.sData = data.data.sensitiveData;
                            console.log($scope.sData);
                        }
                        else {
                            // error
                            jsHelper.processMessageEx(data.message);
                            $scope.sData = null;
                        }
                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.$apply();
                    }
                );
                $scope.verifyInfo.username = '';
                $scope.verifyInfo.password = '';
            }
        },
        printNote: function () {
            if ($scope.sData && $scope.sData.managerNote) {
                var printWindow = window.open();
                printWindow.document.open('text/plain')
                printWindow.document.write($scope.sData.managerNote);
                printWindow.document.close();
                printWindow.focus();
                printWindow.print();
                printWindow.close();
            }
        }
    };

    //
    // method
    //
    $scope.method = {
        formatRemark: function (remark) {
            if (remark == null) {
                return '';
            }
            return remark.replace(/(?:\r\n|\r|\n)/g, '<br />');
        },
        getUsageClass: function (usage) {
            switch (usage) {
                case 'NO ACCOUNT':
                    return 0;

                case 'NONE':
                    return 1;

                case 'VERY LOW':
                    return 2;

                case 'LOW':
                    return 3;

                case 'AVERAGE':
                    return 4;

                case 'ABOVE AVERAGE':
                    return 5;

                case 'INTENSE':
                    return 6;

                case 'HIGH':
                    return 7;
            }
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);