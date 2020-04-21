var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;
    $scope.categories = [];
    $scope.countriesByItem = [];
    $scope.saleByItem = [];
    $scope.TotalAmountOfCommercialInvoice = null;
    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getReportData(
                context.season,
                function (data) {
                    $scope.data = data.data;

                    // get category
                    var indexs = [];
                    angular.forEach($scope.data.top20ByCategories, function (value, key) {
                        if (indexs.indexOf(value.productCategoryID) < 0) {
                            indexs.push(value.productCategoryID);
                            $scope.categories.push({ id: value.productCategoryID, name: value.productCategoryNM, displayOrder: value.productCategoryDisplayOrder });
                        }
                    }, null);

                    //countries by item
                    indexs = [];
                    angular.forEach($scope.data.itemByClients, function (value, key) {
                        keyID = value.modelID.toString() + '_' + value.clientCountryID.toString();
                        if (indexs.indexOf(keyID) < 0) {
                            indexs.push(keyID);
                            $scope.countriesByItem.push({ modelID: value.modelID, clientCountryUD: value.clientCountryUD, clientCountryNM: value.clientCountryNM });
                        }
                    }, null);

                    //sale by item
                    indexs = [];
                    angular.forEach($scope.data.itemByClients, function (value, key) {
                        keyID = value.modelID.toString() + '_' + value.saleID.toString();
                        if (indexs.indexOf(keyID) < 0) {
                            indexs.push(keyID);
                            $scope.saleByItem.push({ modelID: value.modelID, saleUD: value.saleUD, saleNM: value.saleNM });
                        }
                    }, null);


                    //calculate pecent
                    var total = $scope.method.calTotalAmountOfCommercialInvoice();
                    angular.forEach($scope.data.commercialInvoiceByCategories, function (value, key) {
                        value.percent = jsHelper.round(value.totalCommercialInvoiceAmount * 100 / total, 2);
                    });

                    //chart of commercial invoice by category
                    var params = {
                        data: []
                    }
                    angular.forEach($scope.data.commercialInvoiceByCategories, function (value, key) {
                        this.data.push({ name: value.productCategoryNM, y: value.percent });
                    }, params);
                    jQuery('#commercialInvoiceByCategory').highcharts({
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
                            text: 'Commercial invoice by item category ' + context.season
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

                    //call Margin
                    angular.forEach($scope.data.commercialInvoiceByCategories, function (item) {
                        item.marginTotal = item.totalCommercialInvoiceAmount - item.totalCostAmountEUR;
                    });
                    //call MarginPercent
                    angular.forEach($scope.data.commercialInvoiceByCategories, function (item) {
                        item.marginPercent = item.totalMarginAmountEUR * 100 / item.totalCommercialInvoiceAmount;
                    });

                    // call total margin
                    //$scope.method.getTotalMargin();

                    //apply data
                    $scope.$apply();
                    jQuery('#content').show();
                },
                function (error) {
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        }
    };

    $scope.method = {

        //total row top 30
        calTotalOrderQnt: function () {
            if ($scope.data == null) return;
            var total = parseInt('0');
            angular.forEach($scope.data.top30s, function (item) {
                total += (JSON.stringify(item.totalOrderQnt) == 'null' ? parseInt('0') : parseInt(item.totalOrderQnt));
            }, total);
            return total;
        },

        calTotalOrderQntIn40HC: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            angular.forEach($scope.data.top30s, function (item) {
                total += (JSON.stringify(item.totalOrderQntIn40HC) == 'null' ? parseFloat('0') : parseFloat(item.totalOrderQntIn40HC));
            }, total);
            return total;
        },

        calTotalEURAmount: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            angular.forEach($scope.data.top30s, function (item) {
                total += (JSON.stringify(item.eurAmount) == 'null' ? parseFloat('0') : parseFloat(item.eurAmount));
            }, total);
            return total;
        },

        //total row top 20 by category
        calTotalOrderQnt_Top20ByCategory: function (productCategoryID) {
            if ($scope.data == null) return;
            var total = parseInt('0');
            angular.forEach($scope.data.top20ByCategories, function (item) {
                if (item.productCategoryID == productCategoryID) {
                    total += (JSON.stringify(item.totalOrderQnt) == 'null' ? parseInt('0') : parseInt(item.totalOrderQnt));
                }
            }, total);
            return total;
        },

        calTotalOrderQntIn40HC_Top20ByCategory: function (productCategoryID) {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            angular.forEach($scope.data.top20ByCategories, function (item) {
                if (item.productCategoryID == productCategoryID) {
                    total += (JSON.stringify(item.totalOrderQntIn40HC) == 'null' ? parseFloat('0') : parseFloat(item.totalOrderQntIn40HC));
                }
            }, total);
            return total;
        },

        calTotalEURAmount_Top20ByCategory: function (productCategoryID) {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            angular.forEach($scope.data.top20ByCategories, function (item) {
                if (item.productCategoryID == productCategoryID) {
                    total += (JSON.stringify(item.eurAmount) == 'null' ? parseFloat('0') : parseFloat(item.eurAmount));
                }
            }, total);
            return total;
        },

        //total row top 20 all
        calTotalOrderQnt_Top20ByCategory_All: function () {
            if ($scope.data == null) return;
            var total = parseInt('0');
            angular.forEach($scope.data.top20ByCategories, function (item) {
                total += (JSON.stringify(item.totalOrderQnt) == 'null' ? parseInt('0') : parseInt(item.totalOrderQnt));
            }, total);
            return total;
        },

        calTotalOrderQntIn40HC_Top20ByCategory_All: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            angular.forEach($scope.data.top20ByCategories, function (item) {
                total += (JSON.stringify(item.totalOrderQntIn40HC) == 'null' ? parseFloat('0') : parseFloat(item.totalOrderQntIn40HC));
            }, total);
            return total;
        },

        calTotalEURAmount_Top20ByCategory_All: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            angular.forEach($scope.data.top20ByCategories, function (item) {
                total += (JSON.stringify(item.eurAmount) == 'null' ? parseFloat('0') : parseFloat(item.eurAmount));
            }, total);
            return total;
        },

        //commercial invoice
        calTotalContOfCommercialInvoice: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            angular.forEach($scope.data.commercialInvoiceByCategories, function (item) {
                total += (JSON.stringify(item.totalCont) == 'null' ? parseFloat('0') : parseFloat(item.totalCont));
            }, total);
            return total;
        },

        calTotalItemOfCommercialInvoice: function () {
            if ($scope.data == null) return;
            var total = parseInt('0');
            angular.forEach($scope.data.commercialInvoiceByCategories, function (item) {
                total += (JSON.stringify(item.totalItem) == 'null' ? parseInt('0') : parseInt(item.totalItem));
            }, total);
            return total;
        },

        calTotalAmountOfCommercialInvoice: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            angular.forEach($scope.data.commercialInvoiceByCategories, function (item) {
                total += (JSON.stringify(item.totalCommercialInvoiceAmount) == 'null' ? parseFloat('0') : parseFloat(item.totalCommercialInvoiceAmount));
            }, total);
            return total;
        },

        getTotalMargin: function () {
            if ($scope.data == null) return;
            var totalMargin = parseFloat('0');
            angular.forEach($scope.data.commercialInvoiceByCategories, function (item) {
                totalMargin += item.totalMarginAmountEUR;
            })
            return totalMargin;
        }
      
    };


    //
    // init
    //
    $scope.event.init();
    //jQuery('#content').show();
}]);