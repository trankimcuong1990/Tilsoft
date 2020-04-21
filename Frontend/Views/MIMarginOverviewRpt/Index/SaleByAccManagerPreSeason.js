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
angular.module('tilsoftApp').filter('sumFunc2', function () {
    return function (data, field1, field2) {
        if (angular.isUndefined(data) || angular.isUndefined(field1) || angular.isUndefined(field2))
            return 0;
        var sum = parseFloat('0');
        angular.forEach(data, function (v, k) {
            sum = sum + parseFloat(v[field1] === null ? 0 : v[field1]) + parseFloat(v[field2] === null ? 0 : v[field2]);
        });
        return sum;
    };
});

angular.module('tilsoftApp').controller('SaleByAccManagerPreSeasonController', ['$scope', '$timeout', '$filter', function ($scope, $timeout, $filter) {
    //
    // property
    //
    $scope.ddcProformaBySales = [];
    $scope.param = saleByAccManagerPreSeasonContext.param;

    //
    // events
    //
    $scope.event = {
        init: function () {
            //get sale proforma for this season
            $scope.method.getSaleByAccManager(
                saleByAccManagerPreSeasonContext.param.currentSeason,
                function (data) {
                    if (data.message.type === 0) {
                        $scope.method.setPanelVisible('pnlSaleByAccManagerPreSeason');
                        $scope.ddcProformaBySales = data.data.ddcProformaBySales;

                        //set settingValue
                        //$scope.settingValue.exRate = data.data.exchangeRate;
                        //$scope.settingValue.usdContainerValue = data.data.usdContainerValue;
                        //$scope.settingValue.eurContainerValue = data.data.eurContainerValue;
                        $scope.$apply();

                        //
                        //chart
                        //
                        var param = {
                            expectedUSD: { name: 'Expected USD', data: [], stack: 'expected' },
                            expectedEUR: { name: 'Expected EUR', data: [], stack: 'expected' },
                            allUSD: { name: 'Proforma Invoice USD', data: [], stack: 'all' },
                            allEUR: { name: 'Proforma Invoice EUR', data: [], stack: 'all' },
                            confirmedUSD: { name: 'Proforma Invoice Confirmed USD', data: [], stack: 'confirmed' },
                            confirmedEUR: { name: 'Proforma Invoice Confirmed EUR', data: [], stack: 'confirmed' },
                            eurofarInvoiceUSD: { name: 'Commercial Invoice USD', data: [], stack: 'eurofarInvoiced' },
                            eurofarInvoiceEUR: { name: 'Commercial Invoice EUR', data: [], stack: 'eurofarInvoiced' },
                            charSeries: []
                        };
                        angular.forEach($scope.ddcProformaBySales, function (value, key) {
                            this.charSeries.push(value.saleUD);
                            this.expectedUSD.data.push(parseFloat(value.expectedUSD));
                            this.expectedEUR.data.push(parseFloat(value.expectedEUR));
                            this.allUSD.data.push(parseFloat(value.piusd));
                            this.allEUR.data.push(parseFloat(value.pieur));
                            this.confirmedUSD.data.push(parseFloat(value.piConfirmedUSD));
                            this.confirmedEUR.data.push(parseFloat(value.piConfirmedEUR));
                            this.eurofarInvoiceUSD.data.push(parseFloat(value.ciusd)); //will modify data later
                            this.eurofarInvoiceEUR.data.push(parseFloat(value.cieur)); //will modify data later

                        }, param);

                        console.log(param);

                        jQuery('#chartDDCSaleProformaPreSeason').highcharts({
                            chart: {
                                type: 'column'
                            },
                            //colors: ['#ff0000', '#0000ff'],
                            title: {
                                text: 'SALES BY ACC MANAGER ' + saleByAccManagerPreSeasonContext.currentSeason
                            },
                            xAxis: {
                                categories: param.charSeries
                            },
                            yAxis: {
                                allowDecimals: false,
                                min: 0,
                                title: {
                                    text: ''
                                },
                                labels: {
                                    formatter: function () {
                                        return $filter('currency')(this.value, '', 0);
                                    }
                                }
                            },

                            tooltip: {
                                formatter: function () {
                                    var tooltipContent = '<table border="1" border-collapse="0" style="border-color: #000;">';
                                    tooltipContent += '<tr><td colspan="2" style="padding: 1px; font-weight: bold;text-align: center;">' + this.x + '</td></tr>';

                                    tooltipContent += '<tr><td colspan="2" style="padding: 1px; text-align: center;font-weight: bold">Expected</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">Total</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[0].total, '', 0) + ' ( 100%)</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">EUR</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[0].y, '', 0) + ' (' + $filter('currency')(this.points[0].percentage, '', 0) + '%)</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">USD</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[1].y, '', 0) + ' (' + $filter('currency')(100 - this.points[0].percentage, '', 0) + '%)</td></tr>';

                                    tooltipContent += '<tr><td colspan="2" style="padding: 1px; text-align: center;font-weight: bold">Proforma Invoice</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">Total</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[2].total, '', 0) + ' ( 100%)</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">EUR</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[2].y, '', 0) + ' (' + $filter('currency')(this.points[2].percentage, '', 0) + '%)</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">USD</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[3].y, '', 0) + ' (' + $filter('currency')(100 - this.points[2].percentage, '', 0) + '%)</td></tr>';

                                    tooltipContent += '<tr><td colspan="2" style="padding: 1px; text-align: center;font-weight: bold">Proforma Invoice Confirmed</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">Total</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[4].total, '', 0) + ' ( 100%)</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">EUR</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[4].y, '', 0) + ' (' + $filter('currency')(this.points[4].percentage, '', 0) + '%)</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">USD</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[5].y, '', 0) + ' (' + $filter('currency')(100 - this.points[4].percentage, '', 0) + '%)</td></tr>';

                                    tooltipContent += '<tr><td colspan="2" style="padding: 1px; text-align: center;font-weight: bold">Commercial Invoice</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">Total</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[6].total, '', 0) + ' ( 100%)</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">EUR</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[6].y, '', 0) + ' (' + $filter('currency')(this.points[6].percentage, '', 0) + '%)</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">USD</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[7].y, '', 0) + ' (' + $filter('currency')(100 - this.points[6].percentage, '', 0) + '%)</td></tr>';

                                    tooltipContent += '</table>';

                                    return tooltipContent;
                                },
                                useHTML: true,
                                shared: true
                            },

                            plotOptions: {
                                column: {
                                    stacking: 'normal'
                                }
                            },
                            legend: {
                                enabled: true
                            },
                            series: [param.expectedEUR, param.expectedUSD, param.allEUR, param.allUSD, param.confirmedEUR, param.confirmedUSD, param.eurofarInvoiceEUR, param.eurofarInvoiceUSD]
                        });


                    }
                    else {
                        jsHelper.showMessage('warning', data.message.message);
                    }
                },
                function (error) {
                    $scope.saleByAccManager = [];
                }
            );
        }
    };


    $scope.method = {
        setPanelVisible: function (id) {
            $('#' + id).find('.rpt-content').show();
            $('#' + id).find('.rpt-loading').hide();
        },
        getSaleByAccManager: function (season, successCallBack, errorCallBack) {
            $.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + saleByAccManagerPreSeasonContext.token
                },
                type: "POST",
                dataType: 'json',
                url: saleByAccManagerPreSeasonContext.serviceUrl + 'get-sale-management-for-delta-overview?season=' + season,
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

    //
    // angular init
    //
    $timeout(function () {
        $scope.event.init();
    }, 0);
}]);