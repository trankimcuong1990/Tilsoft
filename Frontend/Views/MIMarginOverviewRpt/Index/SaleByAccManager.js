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

angular.module('tilsoftApp').controller('SaleByAccManagerController', ['$scope', '$timeout', '$filter', function ($scope, $timeout, $filter) {
        //
        // property
        //
        $scope.ddcProformaBySales = [];
        $scope.param = saleByAccManagerContext.param;

        //
        // events
        //
        $scope.event = {
            init: function () {
                //get sale proforma for this season
                $scope.method.getSaleByAccManager(
                    saleByAccManagerContext.param.currentSeason,
                    function (data) {
                        if (data.message.type === 0) {
                            $scope.method.setPanelVisible('pnlSaleByAccManager');
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

                            jQuery('#chartDDCSaleProforma').highcharts({
                                chart: {
                                    type: 'column'
                                },
                                //colors: ['#ff0000', '#0000ff'],
                                title: {
                                    text: 'SALES BY ACC MANAGER ' + saleByAccManagerContext.currentSeason
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

                //
                $scope.event.loadSaleProformaByMonth();
                
            },

            loadSaleProformaByMonth: function () {
                //get sale proforma by month
                $scope.method.getSaleProformaByMonth(
                    saleByAccManagerContext.param.currentSeason,
                    function (data) {
                        $scope.saleProformaByMonth = data.data;
                        console.log($scope.saleProformaByMonth);

                        var param = {
                            ProformaInvoice_RV: { name: 'RV - Proforma Invoice', data: [], stack: 'RV' },
                            ProformaInvoiceConfirmed_RV: { name: 'RV - Proforma Invoice Confirmed', data: [], stack: 'RV' },
                            ProformaInvoice_GM: { name: 'GM - Proforma Invoice', data: [], stack: 'GM' },
                            ProformaInvoiceConfirmed_GM: { name: 'GM - Proforma Invoice Confirmed', data: [], stack: 'GM' },
                            ProformaInvoice_RL: { name: 'RL - Proforma Invoice', data: [], stack: 'RL' },
                            ProformaInvoiceConfirmed_RL: { name: 'RL - Proforma Invoice Confirmed', data: [], stack: 'RL' },
                            ProformaInvoice_LW: { name: 'LW - Proforma Invoice', data: [], stack: 'LW' },
                            ProformaInvoiceConfirmed_LW: { name: 'LW - Proforma Invoice Confirmed', data: [], stack: 'LW' },
                            ProformaInvoice_RJ: { name: 'RJ - Proforma Invoice', data: [], stack: 'RJ' },
                            ProformaInvoiceConfirmed_RJ: { name: 'RJ - Proforma Invoice Confirmed', data: [], stack: 'RJ' },
                            charSeries: []
                        };

                        param.charSeries.push('October');
                        param.charSeries.push('November');
                        param.charSeries.push('December');
                        param.charSeries.push('January');
                        param.charSeries.push('Febuary');
                        param.charSeries.push('March');
                        param.charSeries.push('April');
                        param.charSeries.push('May');
                        param.charSeries.push('June');
                        param.charSeries.push('July');
                        param.charSeries.push('August');
                        param.charSeries.push('September');

                        var proformaInvoices = [];
                        angular.forEach(param.charSeries, function (item) {
                            proformaInvoices.push({
                                Month: item,
                                ProformaInvoice_RV: 0,
                                ProformaInvoiceConfirmed_RV: 0,
                                ProformaInvoice_GM: 0,
                                ProformaInvoiceConfirmed_GM: 0,
                                ProformaInvoice_RL: 0,
                                ProformaInvoiceConfirmed_RL: 0,
                                ProformaInvoice_LW: 0,
                                ProformaInvoiceConfirmed_LW: 0,
                                ProformaInvoice_RJ: 0,
                                ProformaInvoiceConfirmed_RJ: 0,
                            });
                        })

                        //get data from db to bind on chard
                        for (i = 0; i < $scope.saleProformaByMonth.allSaleProformaByMonths.length; i++) {
                            var dbData = $scope.saleProformaByMonth.allSaleProformaByMonths[i];
                            var saleUD = dbData.saleUD;
                            for (j = 0; j < param.charSeries.length; j++) {
                                var month = param.charSeries[j];
                                var x = proformaInvoices.filter(function (o) { return o.Month === param.charSeries[j] })[0];
                                x["ProformaInvoice_" + saleUD] = dbData[month.toLowerCase()];
                            }
                        }

                        for (i = 0; i < $scope.saleProformaByMonth.confirmedSaleProformaByMonths.length; i++) {
                            var dbData = $scope.saleProformaByMonth.confirmedSaleProformaByMonths[i];
                            var saleUD = dbData.saleUD;
                            for (j = 0; j < param.charSeries.length; j++) {
                                var month = param.charSeries[j];
                                var x = proformaInvoices.filter(function (o) { return o.Month === param.charSeries[j] })[0];
                                x["ProformaInvoiceConfirmed_" + saleUD] = dbData[month.toLowerCase()];
                            }
                        }

                        angular.forEach(proformaInvoices, function (value, key) {
                            this.ProformaInvoice_RV.data.push(parseFloat(value.ProformaInvoice_RV));
                            this.ProformaInvoiceConfirmed_RV.data.push(parseFloat(value.ProformaInvoiceConfirmed_RV));
                            this.ProformaInvoice_GM.data.push(parseFloat(value.ProformaInvoice_GM));
                            this.ProformaInvoiceConfirmed_GM.data.push(parseFloat(value.ProformaInvoiceConfirmed_GM));
                            this.ProformaInvoice_RL.data.push(parseFloat(value.ProformaInvoice_RL));
                            this.ProformaInvoiceConfirmed_RL.data.push(parseFloat(value.ProformaInvoiceConfirmed_RL));
                            this.ProformaInvoice_LW.data.push(parseFloat(value.ProformaInvoice_LW));
                            this.ProformaInvoiceConfirmed_LW.data.push(parseFloat(value.ProformaInvoiceConfirmed_LW));
                            this.ProformaInvoice_RJ.data.push(parseFloat(value.ProformaInvoice_RJ));
                            this.ProformaInvoiceConfirmed_RJ.data.push(parseFloat(value.ProformaInvoiceConfirmed_RJ));
                        }, param);

                        jQuery('#chartSaleProforma').highcharts({
                            chart: {
                                type: 'column'
                            },
                            //colors: ['#ff0000', '#0000ff'],
                            title: {
                                text: 'PROFORMA INVOICE AND PROFORMA INVOICE CONFIRMED ' + saleByAccManagerContext.currentSeason + ' BY MONTH'
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

                                    tooltipContent += '<tr><td colspan="2" style="padding: 1px; text-align: center;font-weight: bold">Proforma Invoice</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">RV</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[0].y, '€ ', 0) + '</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">GM</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[2].y, '€ ', 0) + '</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">RL</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[4].y, '€ ', 0) + '</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">LW</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[6].y, '€ ', 0) + '</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">RJ</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[8].y, '€ ', 0) + '</td></tr>';

                                    tooltipContent += '<tr><td colspan="2" style="padding: 1px; text-align: center;font-weight: bold">Proforma Invoice Confirmed</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">RV</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[1].y, '€ ', 0) + '</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">GM</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[3].y, '€ ', 0) + '</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">RL</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[5].y, '€ ', 0) + '</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">LW</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[7].y, '€ ', 0) + '</td></tr>';
                                    tooltipContent += '<tr><td style="padding: 2px;">RJ</td><td style="padding: 2px; text-align: right;">' + $filter('currency')(this.points[9].y, '€ ', 0) + '</td></tr>';

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
                            series: [param.ProformaInvoice_RV, param.ProformaInvoiceConfirmed_RV, param.ProformaInvoice_GM, param.ProformaInvoiceConfirmed_GM, param.ProformaInvoice_RL, param.ProformaInvoiceConfirmed_RL, param.ProformaInvoice_LW, param.ProformaInvoiceConfirmed_LW, param.ProformaInvoice_RJ, param.ProformaInvoiceConfirmed_RJ]

                        });
                    },
                    function (error) {
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
                        'Authorization': 'Bearer ' + saleByAccManagerContext.token
                    },
                    type: "POST",
                    dataType: 'json',
                    url: saleByAccManagerContext.serviceUrl + 'get-sale-management-for-delta-overview?season=' + season,
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

            getSaleProformaByMonth: function (season, successCallBack, errorCallBack) {
                $.ajax({
                    cache: false,
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + saleByAccManagerContext.token
                    },
                    type: "POST",
                    dataType: 'json',
                    url: saleByAccManagerContext.serviceUrl + 'getreport?season=' + season + '&saleId=',
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