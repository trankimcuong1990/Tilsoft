// object name  : miSaleConclusionRpt
angular.module('tilsoftApp').filter('sumFunc', function () {
    return function (data, field) {
        if (angular.isUndefined(data) || angular.isUndefined(field))
            return 0;
        var sum = parseFloat('0');
        angular.forEach(data, function (v, k) {
            sum = sum + parseFloat(v[field] === null ? 0 : v[field]);
        });
        return sum;
    };
});
angular.module('tilsoftApp').controller('MiSaleConclusionRpt_Top20ClientByDeltaController', ['$scope', '$rootScope', '$timeout', '$filter', function ($scope, $rootScope, $timeout, $filter) {
    $scope.miSaleConclusionRpt = {};
    //
    // property
    //
    $scope.miSaleConclusionRpt.data = null;
    $scope.miSaleConclusionRpt.param = miSaleConclusionRpt_Top20ClientByDelta_Context.param;

    $scope.miSaleConclusionRpt.filter = {
        clientCountryUD: '',
        clientCountryNM: ''
    };

    $scope.miSaleConclusionRpt.sortValue = {
        colName: '-Delta5Amount'
    };

    //
    // events
    //
    $scope.miSaleConclusionRpt.event = {
        init: function () {
            $scope.miSaleConclusionRpt.method.getData(
                $scope.miSaleConclusionRpt.param.season,
                function (data) {
                    $scope.miSaleConclusionRpt.data = data.data;
                    $scope.$apply();

                    //pie  chart
                    var params = {
                        data: []
                    };
                    angular.forEach($scope.miSaleConclusionRpt.data, function (value, key) {
                        this.data.push({ name: value.clientNM + ' (' + $filter('currency')(value.delta5Amount, '$', 0) + ' - ' + jsHelper.round(value.delta5Percent, 1) + '%)' + ' : ' + value.delta5PercentOf100 + '%', y: value.delta5Amount });
                    }, params);
                    jQuery('#miSaleConclusionRpt_DeltaDistribution_PieChart').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'pie'
                        },
                        credits: {
                            enabled: false
                        },
                        title: {
                            text: ''
                        },
                        tooltip: {
                            pointFormat: '{series.name}'
                        },
                        plotOptions: {
                            pie: {
                                size: '110%',
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: true,
                                    format: '<b>{point.name}</b>',
                                    style: {
                                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                                    }
                                }
                            }
                        },
                        series: [{
                            name: 'Percent',
                            colorByPoint: true,
                            data: params.data
                        }]
                    });

                    //
                    //stack bar chart for delta, commercial invoice, proforma invoice confirmed
                    //
                    var deltaCategory = 'DELTA 6 (PI CONFIRMED  - ' + $scope.shortSeason + ')';
                    var ciCategory = 'C/I AMOUNT - ' + $scope.shortPrevSeason;
                    var piCategory = 'P/I CONFIRMED AMOUNT - ' + $scope.shortSeason;
                    var stackBarChartSaleConclusionData = {
                        categories: [
                            deltaCategory,
                            ciCategory,
                            piCategory
                        ],
                        series: []
                    };
                    var miConclusionDataDeltasTop10 = [];
                    miConclusionDataDeltasTop10 = $scope.miSaleConclusionRpt.data.slice(0, 10);
                    miConclusionDataDeltasTop10 = miConclusionDataDeltasTop10.sort(function (a, b) { return a.delta5Amount - b.delta5Amount; }); //sort on delta percent
                    angular.forEach(miConclusionDataDeltasTop10, function (item) {
                        stackBarChartSaleConclusionData.series.push({
                            name: item.clientNM,
                            data: [jsHelper.round(item.delta5Amount, 1), jsHelper.round(item.ciAmount, 0), jsHelper.round(item.piConfirmedAmount, 0)],
                            deltaLabel: ' (' + $filter('currency')(item.delta5Amount, '$', 0) + ' - ' + jsHelper.round(item.delta5Percent, 1) + '%)' + ' : ' + item.delta5PercentOf100 + '%',
                            ciLablel: ' (' + $filter('currency')(item.ciAmount, '$', 0) + ')' + ' : ' + item.ciPercentOf100 + '%',
                            piLablel: ' (' + $filter('currency')(item.piConfirmedAmount, '$', 0) + ')' + ' : ' + item.piPercentOf100 + '%'
                        });
                    });
                    Highcharts.chart('miSaleConclusionRpt_DeltaDistribution_StackBarChart', {
                        chart: {
                            type: 'column'
                        },
                        title: {
                            text: ''
                        },
                        xAxis: {
                            categories: stackBarChartSaleConclusionData.categories
                        },
                        yAxis: {
                            min: 0,
                            title: {
                                text: 'Total Percent'
                            }
                        },
                        tooltip: {
                            formatter: function () {
                                if (this.x === deltaCategory) {
                                    return '<span>' + this.series.name + this.series.userOptions.deltaLabel + '</span>';
                                }
                                else if (this.x === ciCategory) {
                                    return '<span>' + this.series.name + this.series.userOptions.ciLablel + '</span>';
                                }
                                else if (this.x === piCategory) {
                                    return '<span>' + this.series.name + this.series.userOptions.piLablel + '</span>';
                                }
                            }
                        },
                        plotOptions: {
                            column: {
                                stacking: 'percent',
                                dataLabels: {
                                    enabled: true,
                                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'black',
                                    formatter: function () {
                                        if (this.x === deltaCategory) {
                                            return '<span>' + this.series.name + this.series.userOptions.deltaLabel + '</span>';
                                        }
                                        else if (this.x === ciCategory) {
                                            return '<span>' + this.series.name + this.series.userOptions.ciLablel + '</span>';
                                        }
                                        else if (this.x === piCategory) {
                                            return '<span>' + this.series.name + this.series.userOptions.piLablel + '</span>';
                                        }
                                    }
                                }
                            }
                        },
                        series: stackBarChartSaleConclusionData.series
                    });
                },
                function (error) {

                });
        }
    };


    $scope.miSaleConclusionRpt.method = {
        getData: function (season, successCallBack, errorCallBack) {
            $.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + miSaleConclusionRpt_Top20ClientByDelta_Context.token
                },
                type: "POST",
                dataType: 'json',
                url: miSaleConclusionRpt_Top20ClientByDelta_Context.serviceUrl + 'get-delta-by-country?season=' + season + '&saleID=-1',
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
        },

        sortData: function (key, field) {
            var currentSetting = $scope.miSaleConclusionRpt.sortValue[key];
            if (currentSetting.replace('-', '') === field) {
                if (currentSetting.indexOf('-') < 0) {
                    $scope.miSaleConclusionRpt.sortValue[key] = '-' + field;
                }
                else {
                    $scope.miSaleConclusionRpt.sortValue[key] = field;
                }
            }
            else {
                $scope.miSaleConclusionRpt.sortValue[key] = field;
            }
        }
    };

    //
    // angular init
    //
    $timeout(function () {
        $scope.miSaleConclusionRpt.event.init();
    }, 0);

}]);