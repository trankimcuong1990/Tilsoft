angular.module('tilsoftApp').controller('salesConclusionController', ['$scope', '$rootScope', '$timeout', 'dataService', '$filter', function ($scope, $rootScope, $timeout, dataService, $filter) {
    //
    // property
    //
    $scope.salesConclusion = {};
    $scope.salesConclusion.data = {};
    $scope.salesConclusion.supportData = {
        expectedSaleByCountryGrowthIncreaseData: [],
        expectedSaleByCountryGrowthDecreaseData: [],
        totalGrowthIncrease: 0,
        totalGrowthDecrease: 0,
        expectedTopCountries: [],
        confirmedSaleByCountryGrowthIncreaseData: [],
        confirmedSaleByCountryGrowthDecreaseData: [],
        confirmedTotalGrowthIncrease: 0,
        confirmedTotalGrowthDecrease: 0,
        confirmedTopCountries: [],
        proformaInvoiceTopCountries: []
    };


    //
    // listen to parent scope
    //
    $rootScope.$on('salesConclusion', function () {
        $scope.salesConclusion.method.loadData();
    });

    //
    // method
    //
    $scope.salesConclusion.method = {
        loadData: function () {
            $scope.salesConclusion.data = {};
            dataService.getSalesConclusion(
                $scope.data.selectedSaleID,
                'pnlSalesConclusion',
                function (data) {
                    if (data.message.type === 0) {
                        $scope.salesConclusion.data = data.data;

                        $timeout(function () {
                            var total = 0;
                            var params = {
                                data: []
                            };
                            var clients = [];
                            var index = 1;
                            /*
                                proforma invoice by country compare with commecial invoice last season by country
                            */
                            // generate percent data - proforma by country
                            total = 0;
                            total = $scope.salesConclusion.method.getTotalProformaByCountry();
                            angular.forEach($scope.salesConclusion.data.proformaCountries, function (value, key) {
                                value.percent = jsHelper.round(value.totalProformaAmount * 100 / total, 2);

                                if (value.totalCommercialInvoiceAmountLastYear !== 0) {
                                    value.growth = (value.totalProformaAmount - value.totalCommercialInvoiceAmountLastYear) * 100 / value.totalCommercialInvoiceAmountLastYear;
                                }
                            }, null);

                            // generate charts proforma by country
                            params = {
                                data: []
                            };
                            angular.forEach($scope.salesConclusion.data.proformaCountries, function (value, key) {
                                this.data.push({ name: value.clientCountryNM, y: value.percent });
                            }, params);
                            $('#proformaInvoiceByCountry').highcharts({
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
                                    text: 'Proforma invoice by country season: ' + context.season
                                },
                                tooltip: {
                                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                                },
                                plotOptions: {
                                    pie: {
                                        size: '110%',
                                        allowPointSelect: true,
                                        cursor: 'pointer',
                                        dataLabels: {
                                            enabled: true,
                                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
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


                            /*
                                expected by country compare with commercial invoice last season
                            */

                            // generate percent data - expected country
                            total = 0;
                            total = $scope.salesConclusion.method.getESBCTotalAmount();
                            angular.forEach($scope.salesConclusion.data.expectedCountries, function (value, key) {
                                value.percent = jsHelper.round(value.totalExpectedAmount * 100 / total, 2);

                                if (value.totalCommercialInvoiceAmountLastYear !== 0) {
                                    value.growth = (value.totalExpectedAmount - value.totalCommercialInvoiceAmountLastYear) * 100 / value.totalCommercialInvoiceAmountLastYear;
                                }
                            }, null);


                            // generate charts expected by country
                            params = {
                                data: []
                            };
                            angular.forEach($scope.salesConclusion.data.expectedCountries, function (value, key) {
                                this.data.push({ name: value.clientCountryNM, y: value.percent });
                            }, params);
                            $('#expectedSaleByCountry').highcharts({
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
                                    text: 'Expected by country season: ' + context.season
                                },
                                tooltip: {
                                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                                },
                                plotOptions: {
                                    pie: {
                                        size: '110%',
                                        allowPointSelect: true,
                                        cursor: 'pointer',
                                        dataLabels: {
                                            enabled: true,
                                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                            style: {
                                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                                            }
                                        }
                                    }
                                },
                                series: [{
                                    name: 'Percent',
                                    colorByPoint: true,
                                    data: params.data,
                                }]
                            });


                            /*
                                proforma invoice confirmed by country compare with commecial invoice last season by country
                            */

                            // generate percent data - confirmed proforma by country
                            total = 0;
                            total = $scope.salesConclusion.method.getCPBCTotalAmount();
                            angular.forEach($scope.salesConclusion.data.confirmedProformaCountries, function (value, key) {
                                value.percent = jsHelper.round(value.totalConfirmedProformaAmount * 100 / total, 2);

                                if (value.totalConfirmedProformaAmountLastYear !== 0) {
                                    value.growth = (value.totalConfirmedProformaAmount - value.totalCommercialInvoiceAmountLastYear) * 100 / value.totalCommercialInvoiceAmountLastYear;
                                }
                            }, null);

                            // generate charts confirmed proforma by country
                            params = {
                                data: []
                            };
                            angular.forEach($scope.salesConclusion.data.confirmedProformaCountries, function (value, key) {
                                this.data.push({ name: value.clientCountryNM, y: value.percent });
                            }, params);
                            $('#confirmedProformaInvoiceByCountry').highcharts({
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
                                    text: 'Proforma invoice confirmed by country season: ' + context.season
                                },
                                tooltip: {
                                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                                },
                                plotOptions: {
                                    pie: {
                                        size: '110%',
                                        allowPointSelect: true,
                                        cursor: 'pointer',
                                        dataLabels: {
                                            enabled: true,
                                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
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


                            /*
                                commercial invoice by country of last season and last last season
                            */

                            // generate percent data - commercial invoice by country
                            total = 0;
                            total = $scope.salesConclusion.method.getCommercialInvoiceByCountryTotalAmount();
                            angular.forEach($scope.salesConclusion.data.commercialInvoiceCountries, function (value, key) {
                                value.percent = jsHelper.round(value.totalAmount * 100 / total, 2);

                                if (value.totalAmountLastYear !== 0) {
                                    value.growth = (value.totalAmount - value.totalAmountLastYear) * 100 / value.totalAmountLastYear;
                                }
                            }, null);

                            // generate charts commercial invoice by country
                            params = {
                                data: []
                            };
                            angular.forEach($scope.salesConclusion.data.commercialInvoiceCountries, function (value, key) {
                                this.data.push({ name: value.clientCountryNM, y: value.percent });
                            }, params);
                            $('#commercialInvoiceByCountry').highcharts({
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
                                    text: 'Commercial invoice by country season: ' + context.season
                                },
                                tooltip: {
                                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                                },
                                plotOptions: {
                                    pie: {
                                        size: '110%',
                                        allowPointSelect: true,
                                        cursor: 'pointer',
                                        dataLabels: {
                                            enabled: true,
                                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
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





                            /*
                            / EXPECTED
                            */

                            // growth expected
                            clients = [];
                            angular.forEach($scope.salesConclusion.data.expectedCountries, function (value, key) {
                                if (value.clientCountryID > -1) {
                                    if (value.totalExpectedAmount >= value.totalExpectedAmountLastYear) {
                                        $scope.salesConclusion.supportData.totalGrowthIncrease += value.totalExpectedAmount - value.totalExpectedAmountLastYear;
                                        clients = [];
                                        angular.forEach($scope.salesConclusion.data.clients, function (subV, subK) {
                                            if (subV.clientCountryID === value.clientCountryID) {
                                                clients.push({
                                                    clientUD: subV.clientUD,
                                                    clientNM: subV.clientNM
                                                });
                                            }
                                        }, null);
                                        $scope.salesConclusion.supportData.expectedSaleByCountryGrowthIncreaseData.push({
                                            countryNM: value.clientCountryNM,
                                            amount: value.totalExpectedAmount - value.totalExpectedAmountLastYear,
                                            percent: jsHelper.round((value.totalExpectedAmount - value.totalExpectedAmountLastYear) * 100 / value.totalExpectedAmountLastYear, 2),
                                            clients: clients
                                        });
                                    }
                                    else {
                                        $scope.salesConclusion.supportData.totalGrowthDecrease += value.totalExpectedAmount - value.totalExpectedAmountLastYear;
                                        clients = [];
                                        angular.forEach($scope.salesConclusion.data.clients, function (subV, subK) {
                                            if (subV.clientCountryID === value.clientCountryID) {
                                                clients.push({
                                                    clientUD: subV.clientUD,
                                                    clientNM: subV.clientNM
                                                });
                                            }
                                        }, null);
                                        $scope.salesConclusion.supportData.expectedSaleByCountryGrowthDecreaseData.push({
                                            countryNM: value.clientCountryNM,
                                            amount: value.totalExpectedAmount - value.totalExpectedAmountLastYear,
                                            percent: jsHelper.round((value.totalExpectedAmount - value.totalExpectedAmountLastYear) * 100 / value.totalExpectedAmountLastYear, 2),
                                            clients: clients
                                        });
                                    }
                                }
                            }, null);


                            // top 10 expected
                            index = 1;
                            angular.forEach($scope.salesConclusion.data.expectedCountries, function (value, key) {
                                if (index <= 5) {
                                    var total = 0;
                                    angular.forEach($scope.salesConclusion.data.expectedTopClients, function (subV, subK) {
                                        if (subV.clientCountryID === value.clientCountryID) {
                                            total += jsHelper.round(subV.totalAmount, 0);
                                        }
                                    }, null);

                                    $scope.salesConclusion.supportData.expectedTopCountries.push({ countryID: value.clientCountryID, countryNM: value.clientCountryNM, total: total, totalCountry: value.totalExpectedAmount });
                                }
                                index++;
                            }, null);


                            // client and sales
                            params = {
                                data: []
                            };
                            angular.forEach($scope.salesConclusion.data.rangeExpected, function (value, key) {
                                this.data.push({ name: $filter('currency')(value.totalClient, '', 0) + ' clients (' + $filter('currency')(value.totalAmount, '', 0) + ')', y: value.totalAmount });
                            }, params);
                            $('#expectedClientAndSale').highcharts({
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
                                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                                },
                                plotOptions: {
                                    pie: {
                                        allowPointSelect: true,
                                        cursor: 'pointer',
                                        dataLabels: {
                                            enabled: true,
                                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
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


                            // distribution of clients expected
                            params = {
                                data: []
                            };
                            angular.forEach($scope.salesConclusion.data.expected, function (value, key) {
                                this.data.push({ name: value.clientNM + ' (' + $filter('currency')(value.totalAmount, '', 0) + ')', y: value.totalAmount });
                            }, params);
                            $('#expectedDistribution').highcharts({
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
                                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                                },
                                plotOptions: {
                                    pie: {
                                        size: '110%',
                                        allowPointSelect: true,
                                        cursor: 'pointer',
                                        dataLabels: {
                                            enabled: true,
                                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
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




                            /*
                            / PROFORMA INVOICE CONFIRMED
                            */

                            // growth proforma invoice confirmed                        
                            angular.forEach($scope.salesConclusion.data.confirmedProformaCountries, function (value, key) {
                                if (value.clientCountryID > -1) {
                                    if (value.totalConfirmedProformaAmount >= value.totalConfirmedProformaAmountLastYear) {
                                        $scope.salesConclusion.supportData.confirmedTotalGrowthIncrease += value.totalConfirmedProformaAmount - value.totalConfirmedProformaAmountLastYear;
                                        clients = [];
                                        angular.forEach($scope.salesConclusion.data.clients, function (subV, subK) {
                                            if (subV.clientCountryID === value.clientCountryID) {
                                                clients.push({
                                                    clientUD: subV.clientUD,
                                                    clientNM: subV.clientNM
                                                });
                                            }
                                        }, null);
                                        $scope.salesConclusion.supportData.confirmedSaleByCountryGrowthIncreaseData.push({
                                            countryNM: value.clientCountryNM,
                                            amount: value.totalConfirmedProformaAmount - value.totalConfirmedProformaAmountLastYear,
                                            percent: jsHelper.round((value.totalConfirmedProformaAmount - value.totalConfirmedProformaAmountLastYear) * 100 / value.totalConfirmedProformaAmountLastYear, 2),
                                            clients: clients
                                        });
                                    }
                                    else {
                                        $scope.salesConclusion.supportData.confirmedTotalGrowthDecrease += value.totalConfirmedProformaAmount - value.totalConfirmedProformaAmountLastYear;
                                        clients = [];
                                        angular.forEach($scope.salesConclusion.data.clients, function (subV, subK) {
                                            if (subV.clientCountryID === value.clientCountryID) {
                                                clients.push({
                                                    clientUD: subV.clientUD,
                                                    clientNM: subV.clientNM
                                                });
                                            }
                                        }, null);
                                        $scope.salesConclusion.supportData.confirmedSaleByCountryGrowthDecreaseData.push({
                                            countryNM: value.clientCountryNM,
                                            amount: value.totalConfirmedProformaAmount - value.totalConfirmedProformaAmountLastYear,
                                            percent: jsHelper.round((value.totalConfirmedProformaAmount - value.totalConfirmedProformaAmountLastYear) * 100 / value.totalConfirmedProformaAmountLastYear, 2),
                                            clients: clients
                                        });
                                    }
                                }
                            }, null);


                            // top 10 proforma invoice confirmed
                            index = 1;
                            angular.forEach($scope.salesConclusion.data.confirmedProformaCountries, function (value, key) {
                                if (index <= 5) {
                                    var total = 0;
                                    angular.forEach($scope.salesConclusion.data.confirmedProformaTopClients, function (subV, subK) {
                                        if (subV.clientCountryID === value.clientCountryID) {
                                            total += jsHelper.round(subV.totalAmount, 0);
                                        }
                                    }, null);

                                    $scope.salesConclusion.supportData.confirmedTopCountries.push({ countryID: value.clientCountryID, countryNM: value.clientCountryNM, total: total, totalAmountOfCountry: value.totalConfirmedProformaAmount });
                                }
                                index++;
                            }, null);


                            // client & sale proforma invoice confirmed
                            params = {
                                data: []
                            };
                            angular.forEach($scope.salesConclusion.data.rangeConfirmedProformas, function (value, key) {
                                this.data.push({ name: $filter('currency')(value.totalClient, '', 0) + ' clients (' + $filter('currency')(value.totalAmount, '', 0) + ')', y: value.totalAmount });
                            }, params);
                            $('#confirmedClientAndSale').highcharts({
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
                                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                                },
                                plotOptions: {
                                    pie: {
                                        allowPointSelect: true,
                                        cursor: 'pointer',
                                        dataLabels: {
                                            enabled: true,
                                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
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


                            // distribution of clients proforma invoice confirmed
                            params = {
                                data: []
                            };
                            angular.forEach($scope.salesConclusion.data.confirmedProformas, function (value, key) {
                                this.data.push({ name: value.clientNM + ' (' + $filter('currency')(value.totalAmount, '', 0) + ')', y: value.totalAmount });
                            }, params);
                            $('#confirmedProformaDistribution').highcharts({
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
                                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                                },
                                plotOptions: {
                                    pie: {
                                        size: '110%',
                                        allowPointSelect: true,
                                        cursor: 'pointer',
                                        dataLabels: {
                                            enabled: true,
                                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
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





                            /*
                            /   PROFORMA INVOICE
                            */

                            // growth proforma invoice confirmed
                            // still missing

                            // top 10 proforma invoice
                            index = 1;
                            angular.forEach($scope.salesConclusion.data.proformaCountries, function (item) {
                                if (index <= 5) {
                                    var total = 0;
                                    angular.forEach($scope.salesConclusion.data.proformaInvoiceTopClients, function (sItem) {
                                        if (sItem.clientCountryID === item.clientCountryID) {
                                            total += jsHelper.round(sItem.totalAmount, 0);
                                        }
                                    });

                                    $scope.salesConclusion.supportData.proformaInvoiceTopCountries.push({ countryID: item.clientCountryID, countryNM: item.clientCountryNM, total: total, totalAmountOfCountry: item.totalProformaAmount });
                                }
                                index++;
                            });

                            // client & sale proforma invoice
                            params = {
                                data: []
                            };
                            angular.forEach($scope.salesConclusion.data.rangeProformaInvoices, function (value, key) {
                                this.data.push({ name: $filter('currency')(value.totalClient, '', 0) + ' clients (' + $filter('currency')(value.totalAmount, '', 0) + ')', y: value.totalAmount });
                            }, params);
                            $('#proformaInvoiceClientAndSale').highcharts({
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
                                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                                },
                                plotOptions: {
                                    pie: {
                                        allowPointSelect: true,
                                        cursor: 'pointer',
                                        dataLabels: {
                                            enabled: true,
                                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
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

                            // distribution of clients proforma invoice
                            params = {
                                data: []
                            };
                            angular.forEach($scope.salesConclusion.data.proformaInvoices, function (value, key) {
                                this.data.push({ name: value.clientNM + ' (' + $filter('currency')(value.totalAmount, '', 0) + ')', y: value.totalAmount });
                            }, params);
                            $('#proformaInvoiceDistribution').highcharts({
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
                                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                                },
                                plotOptions: {
                                    pie: {
                                        size: '110%',
                                        allowPointSelect: true,
                                        cursor: 'pointer',
                                        dataLabels: {
                                            enabled: true,
                                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
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
                        }, 100);
                    }
                },
                function (error) {
                    console.log(error);
                    $scope.salesConclusion.data = {};
                }
            );
        },



        /*
           proforma invoice confirmed by country compare with commecial invoice last season by country
       */

        getTotalProformaByCountry: function () {
            var total = 0;
            if ($scope.salesConclusion.data !== null && $scope.salesConclusion.data.proformaCountries !== null) {
                angular.forEach($scope.salesConclusion.data.proformaCountries, function (value, key) {
                    total += value.totalProformaAmount;
                }, null);
            }
            return total;
        },


        /*
            proforma invoice confirmed by country compare with commecial invoice last season by country
        */

        getCPBCTotalAmount: function () {
            var total = 0;
            if ($scope.salesConclusion.data !== null && $scope.salesConclusion.data.confirmedProformaCountries !== null) {
                angular.forEach($scope.salesConclusion.data.confirmedProformaCountries, function (value, key) {
                    total += value.totalConfirmedProformaAmount;
                }, null);
            }
            return total;
        },


        /*
            expected by country compare with commercial invoice last season
        */

        getESBCTotalAmount: function () {
            var total = 0;
            if ($scope.salesConclusion.data !== null && $scope.salesConclusion.data.expectedCountries !== null) {
                angular.forEach($scope.salesConclusion.data.expectedCountries, function (value, key) {
                    total += value.totalExpectedAmount;
                }, null);
            }
            return total;
        },

        /*
            commercial invoice by country of last season and last last season
        */

        //commercial invoice by country
        getCommercialInvoiceByCountryTotalAmount: function () {
            var total = 0;
            if ($scope.salesConclusion.data !== null && $scope.salesConclusion.data.commercialInvoiceCountries !== null) {
                angular.forEach($scope.salesConclusion.data.commercialInvoiceCountries, function (value, key) {
                    total += value.totalAmount;
                }, null);
            }
            return total;
        },


        /*
            TOP 10
        */

        //top 10 expected
        getESTCTotalPercent: function (country) {
            var total = 0;
            if ($scope.salesConclusion.data !== null && $scope.salesConclusion.data.expectedTopClients !== null) {
                angular.forEach($scope.salesConclusion.data.expectedTopClients, function (value, key) {
                    if (value.clientCountryID === country.countryID) {
                        total += jsHelper.round(value.totalAmount * 100 / $scope.salesConclusion.method.getESBCTotalAmount(), 0);
                    }
                }, null);
            }
            return total;
        },

        getESTCPercent: function (country, item) {
            return item.totalAmount * 100 / country.totalCountry;
        },


        /*
         *   CLIENT & SALES 
         */

        // client & sale expected
        getExpectedRangeTotalClient: function () {
            var total = 0;
            if ($scope.salesConclusion.data !== null && $scope.salesConclusion.data.rangeExpected !== null) {
                angular.forEach($scope.salesConclusion.data.rangeExpected, function (value, key) {
                    total += value.totalClient;
                }, null);
            }
            return total;
        },
        getExpectedRangeTotalAmount: function () {
            var total = 0;
            if ($scope.salesConclusion.data !== null && $scope.salesConclusion.data.rangeExpected !== null) {
                angular.forEach($scope.salesConclusion.data.rangeExpected, function (value, key) {
                    total += value.totalAmount;
                }, null);
            }
            return total;
        },
        getExpectedRangePercent: function (index, item) {
            if (index === $scope.salesConclusion.data.rangeExpected.length - 1) {
                var total = 0;
                if ($scope.salesConclusion.data !== null && $scope.salesConclusion.data.rangeExpected !== null) {
                    angular.forEach($scope.salesConclusion.data.rangeExpected, function (value, key) {
                        if (value !== item) {
                            total += jsHelper.round(value.totalAmount * 100 / $scope.salesConclusion.method.getExpectedRangeTotalAmount(), 1);
                        }
                    }, null);
                }
                return jsHelper.round(100 - total, 1);
            }
            else {
                return jsHelper.round(item.totalAmount * 100 / $scope.salesConclusion.method.getExpectedRangeTotalAmount(), 1);
            }
        },


        // client & sale proforma invoice
        getProformaInvoiceRangeTotalClient: function () {
            var total = 0;
            if ($scope.salesConclusion.data !== null && $scope.salesConclusion.data.rangeProformaInvoices !== null) {
                angular.forEach($scope.salesConclusion.data.rangeProformaInvoices, function (value, key) {
                    total += value.totalClient;
                }, null);
            }
            return total;
        },
        getProformaInvoiceRangeTotalAmount: function () {
            var total = 0;
            if ($scope.salesConclusion.data !== null && $scope.salesConclusion.data.rangeProformaInvoices !== null) {
                angular.forEach($scope.salesConclusion.data.rangeProformaInvoices, function (value, key) {
                    total += value.totalAmount;
                }, null);
            }
            return total;
        },
        getProformaInvoiceRangePercent: function (index, item) {
            if (index === $scope.salesConclusion.data.rangeProformaInvoices.length - 1) {
                var total = 0;
                if ($scope.salesConclusion.data !== null && $scope.salesConclusion.data.rangeProformaInvoices !== null) {
                    angular.forEach($scope.salesConclusion.data.rangeProformaInvoices, function (value, key) {
                        if (value !== item) {
                            total += jsHelper.round(value.totalAmount * 100 / $scope.salesConclusion.method.getProformaInvoiceRangeTotalAmount(), 1);
                        }
                    }, null);
                }
                return jsHelper.round(100 - total, 1);
            }
            else {
                return jsHelper.round(item.totalAmount * 100 / $scope.salesConclusion.method.getProformaInvoiceRangeTotalAmount(), 1);
            }
        },


        // client & sale confirmed
        getConfirmedRangeTotalClient: function () {
            var total = 0;
            if ($scope.salesConclusion.data !== null && $scope.salesConclusion.data.rangeConfirmedProformas !== null) {
                angular.forEach($scope.salesConclusion.data.rangeConfirmedProformas, function (value, key) {
                    total += value.totalClient;
                }, null);
            }
            return total;
        },
        getConfirmedRangeTotalAmount: function () {
            var total = 0;
            if ($scope.salesConclusion.data !== null && $scope.salesConclusion.data.rangeConfirmedProformas !== null) {
                angular.forEach($scope.salesConclusion.data.rangeConfirmedProformas, function (value, key) {
                    total += value.totalAmount;
                }, null);
            }
            return total;
        },
        getConfirmedRangePercent: function (index, item) {
            if (index === $scope.salesConclusion.data.rangeConfirmedProformas.length - 1) {
                var total = 0;
                if ($scope.salesConclusion.data !== null && $scope.salesConclusion.data.rangeConfirmedProformas !== null) {
                    angular.forEach($scope.salesConclusion.data.rangeConfirmedProformas, function (value, key) {
                        if (value !== item) {
                            total += jsHelper.round(value.totalAmount * 100 / $scope.salesConclusion.method.getConfirmedRangeTotalAmount(), 1);
                        }
                    }, null);
                }
                return jsHelper.round(100 - total, 1);
            }
            else {
                return jsHelper.round(item.totalAmount * 100 / $scope.salesConclusion.method.getConfirmedRangeTotalAmount(), 1);
            }
        }
    };    
}]);