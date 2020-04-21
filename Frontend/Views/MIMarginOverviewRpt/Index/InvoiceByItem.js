(function () {
    'use strict';

    angular.module('tilsoftApp').controller('InvoiceByItemController', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;
        //
        // property
        //
        $scope.commercialInvoiceByCategories = [];
        //
        // events
        //
        $scope.event = {
            init: function () {
                //COMMERCIAL INVOICE BY ITEM CATEGORY
                dataService.getInvoiceByItem(
                    context.preSeason,
                    function (data) {
                        if (data.message.type === 0) {
                            $scope.method.setPanelVisible('pnlInvoiceByItem');
                            $scope.commercialInvoiceByCategories = data.data.commercialInvoiceByCategories;

                            //chart of commercial invoice by category
                            var params = {
                                data: []
                            };
                            angular.forEach($scope.commercialInvoiceByCategories, function (item) {
                                this.data.push({ name: item.productCategoryNM, y: item.ciAmountInEUR });
                            }, params);
                            $('#chartInvoiceByItem').highcharts({
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
                                    text: 'Commercial invoice by item category ' + context.preSeason
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
                                            format: '<b>{point.name}</b>: {point.percentage:.0f} %',
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
                        }
                        else {
                            jsHelper.showMessage('warning', data.message.message);
                            console.log(data.message.message);
                        }
                    },
                    function (error) {
                        $scope.commercialInvoiceByCategories = null;
                    }
                );
            }
        };

        //
        // angular init
        //
        $timeout(function () {
            $scope.event.init();
        }, 0);
    }]);

})();