
jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.getpurchaseinvoice();
    }
});
'use strict';

var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ngCookies']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore','$timeout', 'dataService', function ($scope, $cookieStore,$timeout, dataService, ) {

    /*
     * initialization Service
     */
    dataService.serviceUrl = context.serviceUrl;
    dataService.supportServiceUrl = context.supportServiceUrl;
    dataService.token = context.token;

    /*
     * property
     */
    $scope.data = null;

    $scope.keyRawMaterials = null;
    $scope.dueColors = null;
    $scope.supplierDTO = [];
    $scope.purchaseInvoiceDTO = [];
    $scope.supportData = {
        factoryRawMaterialOfficialNM: '',
        factoryRawMaterialUD: '',
        totalCount: 0,
        totalCountColor: 0
    };

    $scope.filters = {
        factoryRawMaterialID: null,
        startDate: '',
        endDate: '',
        keyRawMaterialID: null,
        dueColorID: null
    };

    $scope.filters2 = {
        purchaseInvoiceUD: '',
        invoiceNo: '',
        factoryRawMaterialID: null,
        dueday: null,
        keyRawMaterialID: null
    };

    var today = new Date();
    var day = 0, month = 0;
    if (today.getDate() < 10) {
         day = '0' + today.getDate();
    }
    else {
        day = today.getDate();
    }
    if ((today.getMonth() + 1) < 10) {
        month = '0' + (today.getMonth() + 1);
    }
    else {
        month = (today.getMonth() + 1);
    }
    var date = day + '/' + month +'/'+ today.getFullYear();

    $scope.filters.endDate = date;
    $scope.ctptra = null;
    $scope.isDefaultFilter = true;
    /*
     *  Function
     */
    function createservices() {
        dataService.searchFilter.pageSize = context.pageSize;
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;
    };
    $scope.event = {

        init: function () {
            dataService.getInit(
                function (data) {
                    $scope.keyRawMaterials = data.data.keyRawMaterials;
                    $scope.dueColors = data.data.dueColorDTOs;
                    $scope.supplierDTO = data.data.supplierDTO;
                    jQuery('#content').show();
                },
                function (error) {

                }
            );
        },
        process: function () {
            
            dataService.process(
                $scope.purchaseInvoiceDTO,
                function (data) {
                    
                    scope.event.getpurchaseinvoice();
                    jsHelper.processMessageEx(data.message);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });

        },
        selectInvoice: function (item) {
            if (item.isSelected) {
                item.planPayment = item.totalAmount - item.totalPayment;
            }
            else {
                item.planPayment = 0;
            }
        },
        calRemain: function (item) {
            return item.totalAmount - item.totalPayment;
        },
        calPlanRemain: function (item) {
            return $scope.event.calRemain(item) - item.planPayment;
        },
        selectAll: function () {
            
            if ($scope.isAllSelected) {
                for (var i = 0; i < $scope.purchaseInvoiceDTO.length; i++) {
                    let item = $scope.purchaseInvoiceDTO[i];
                    item.isSelected = true;
                }
            }
            else {
                for (var i = 0; i < $scope.purchaseInvoiceDTO.length; i++) {
                    let item = $scope.purchaseInvoiceDTO[i];
                    item.isSelected = false;
                }
            }
        },
        clear: function() {
            $scope.filters2 = {
                purchaseInvoiceUD: '',
                invoiceNo: '',
                factoryRawMaterialID: null,
                dueday: null,
                keyRawMaterialID: null,
            };
            $('#factoryRawMaterialID').val = "";
            $scope.event.getpurchaseinvoice();

        },
        
        getpurchaseinvoice: function () {
            $scope.purchaseInvoiceDTO = null;
            dataService.getpurchaseinvoice(
                $scope.filters2,
                function (data) {
                    $scope.purchaseInvoiceDTO = data.data;
                   // $scope.event.totalAmountPI();
                    jQuery('#content').show();
                },
                function (error) {
                    $scope.purchaseInvoiceDTO = null;
                }
            );
        },
    
        getTotalLiabilities: function () {
            if ($scope.filters.startDate === null || $scope.filters.startDate === '' || $scope.filters.startDate === undefined) {
                jsHelper.showMessage('warning', 'Please input Start Date.');
                return false;
            }
            if ($scope.filters.endDate === null || $scope.filters.endDate === '' || $scope.filters.endDate === undefined) {
                jsHelper.showMessage('warning', 'Please input End Date.');
                return false;
            }
            $scope.data = null;
            dataService.getTotalLiabilities(
                $scope.filters,
                function (data) {
                    $scope.data = data.data;
                    //jQuery('#content').show();

                    //
                    // create the highcharts for Invoice
                    //

                    var seriData = [{ name: 'Amount USD', data: [], type: 'column', colors: [] }];
                    //var seriData = [];
                    var categoryData = [];
                    angular.forEach($scope.data.listChartDetailDTOs, function (item) {
                        var colorCode = item.dueColorUD;
                        seriData[0].data.push(jsHelper.round(item.totalAmount, 2));
                        seriData[0].colors.push(colorCode);
                        categoryData.push(item.dueColorDate);
                    });
                    var chart2nd = Highcharts.chart('highchartabc', {
                        title: {
                            text: 'AP Aging Trend'
                        },
                        xAxis: {
                            categories: categoryData,
                            labels: {
                                rotation: -45,
                                enabled: true,
                                style: {
                                    fontWeight: 'bold',
                                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'black'
                                }
                            }
                        },
                        yAxis: {
                            min: 0,
                            title: {
                                text: 'USD'
                            },
                            stackLabels: {
                                enabled: true,
                                style: {
                                    fontWeight: 'bold',
                                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                                }
                            }
                        },
                        legend: {
                            backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                            shadow: true
                        },
                        plotOptions: {
                            column: {
                                stacking: 'normal'
                            },
                            series: {
                                colorByPoint: true
                            }
                        },
                        tooltip: {
                            headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                            pointFormat: '<span style="color:#fffe11">{point.name}</span>: <b>{point.y:,.2f}</b>'
                        },
                        series: seriData
                    });
                    chart2nd.redraw();
                },
                function (error) {
                    $scope.data = null;
                }
            );
        },
        openDetailOfLiabilities: function (item) {
            if (item.numberRowDetail > 0) {
                if (!item.openDetailOfLiabilities) {
                    item.openDetailOfLiabilities = true;
                } else {
                    item.openDetailOfLiabilities = false;
                }
            }
        },
    };


    /*
     * Method totalLiabilities
     */
    $scope.method = {
        //AutoComplete
        autocompleteSupplier: function () {
            $('#supplierAutoComplete').autocomplete({
                source: function (request, response) {
                    jQuery.ajax({
                        url: context.serviceUrl + 'query-supplier?param=' + request.term,
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json',
                            'Authorization': 'Bearer ' + context.token
                        },
                        dataType: "json",
                        type: "POST",
                        success: function (data) {
                            response($.map(data, function (value, key) {
                                return {
                                    id: value.factoryRawMaterialID,
                                    label: value.factoryRawMaterialOfficialNM,
                                    value: value.factoryRawMaterialOfficialNM,
                                    description: 'Code: ' + value.factoryRawMaterialUD,
                                    code: value.factoryRawMaterialUD
                                };
                            }));
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log(jqXHR);
                            console.log(textStatus);
                            console.log(errorThrown);
                        }
                    });
                },
                minLength: 3,
                select: function (event, ui) {
                    scope.$apply(function () {
                        $scope.filters.factoryRawMaterialID = ui.item.id;
                        $scope.supportData.factoryRawMaterialOfficialNM = ui.item.label;
                        $scope.supportData.factoryRawMaterialUD = ui.item.code;
                    });
                },
                change: function (event, ui) {
                    if (!ui.item) {
                        scope.$apply(function () {
                            $scope.filters.factoryRawMaterialID = null;
                            $scope.supportData.factoryRawMaterialOfficialNM = '';
                            $scope.supportData.factoryRawMaterialUD = '';
                        });
                        $('#supplierAutoComplete').val('');
                    }
                }
            })
                .data("ui-autocomplete")._renderItem = function (ul, item) {
                    return jQuery("<li>")
                        .data('item.autocomplete', item)
                        .append('<a href="javascript:void(0)"><strong style="text-decoration: underline; text-transform: uppercase;">' + item.label + '</strong><br>' + item.description + '</a>')
                        .appendTo(ul);
                };
        },
        sumAmountPaid: function (detailOfLiabilitiesDto) {
            var result = 0;
            if (detailOfLiabilitiesDto !== null) {
                for (var i = 0; i < detailOfLiabilitiesDto.length; i++) {
                    if (detailOfLiabilitiesDto[i].detailOfLiabilitiesByInvoiceNoDto !== null) {
                        for (var j = 0; j < detailOfLiabilitiesDto[i].detailOfLiabilitiesByInvoiceNoDto.length; j++) {
                            var item = detailOfLiabilitiesDto[i].detailOfLiabilitiesByInvoiceNoDto[j];
                            result += item.amountByCurrency;
                        }
                    }
                }
            }
            return result;
        },
        sumBeginningBalance: function (detailOfLiabilitiesDto, factoryRawMaterialID) {
            var result = 0;
            if (detailOfLiabilitiesDto !== null) {
                for (var i = 0; i < detailOfLiabilitiesDto.length; i++) {
                    var item = detailOfLiabilitiesDto[i];

                    if (factoryRawMaterialID === item.factoryRawMaterialID) {
                        result += item.beginningBalance;
                    }
                }
            }
            return result;
        },
        sumDeposit: function (detailOfLiabilitiesDto, factoryRawMaterialID) {
            var result = 0;
            if (detailOfLiabilitiesDto !== null) {
                for (var i = 0; i < detailOfLiabilitiesDto.length; i++) {
                    var item = detailOfLiabilitiesDto[i];

                    if (factoryRawMaterialID === item.factoryRawMaterialID) {
                        result += item.totalRealDeposit;
                    }
                }
            }
            return result;
        },
        sumPayment: function (detailOfLiabilitiesDto, factoryRawMaterialID) {
            var result = 0;
            if (detailOfLiabilitiesDto !== null) {
                for (var i = 0; i < detailOfLiabilitiesDto.length; i++) {
                    var item = detailOfLiabilitiesDto[i];

                    if (factoryRawMaterialID === item.factoryRawMaterialID) {
                        result += item.totalAmount;
                    }
                }
            }
            return result;
        },
        sumReceipt: function (detailOfLiabilitiesByInvoiceNoDto) {
            var result = 0;
            if (detailOfLiabilitiesByInvoiceNoDto !== null) {
                for (var i = 0; i < detailOfLiabilitiesByInvoiceNoDto.length; i++) {
                    var item = detailOfLiabilitiesByInvoiceNoDto[i];
                    result += item.amountByCurrency;
                }
            }
            return result;
        },
        sumBeginningBalanceDebt: function () {
            var result = 0;
            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.totalLiabilities.length; i++) {
                    var item = $scope.data.totalLiabilities[i];
                    result += item.beginningDebt;
                }
            }
            return result;
        },
        sumBeginningBalanceHave: function () {
            var result = 0;
            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.totalLiabilities.length; i++) {
                    var item = $scope.data.totalLiabilities[i];
                    result += item.beginningHas;
                }
            }
            return result;
        },
        sumtotalPurchaseInvoice: function () {
            var result = 0;
            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.totalLiabilities.length; i++) {
                    var item = $scope.data.totalLiabilities[i];
                    result += item.totalPurchaseInvoice;
                }
            }
            return result;
        },
        sumtotalPaymentVoucherInvoice: function () {
            var result = 0;
            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.totalLiabilities.length; i++) {
                    var item = $scope.data.totalLiabilities[i];
                    result += item.totalPaymentVoucherInvoice;
                }
            }
            return result;
        },
        sumEndingBalanceDebt: function () {
            var result = 0;
            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.totalLiabilities.length; i++) {
                    var item = $scope.data.totalLiabilities[i];
                    result += item.endingBalanceDebt;
                }
            }
            return result;
        },
        sumEndingBalanceHave: function () {
            var result = 0;
            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.totalLiabilities.length; i++) {
                    var item = $scope.data.totalLiabilities[i];
                    result += item.endingBalanceHas;
                }
            }
            return result;
        },
        sumEndingBalanceUSDDebt: function () {
            var result = 0;
            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.totalLiabilities.length; i++) {
                    var item = $scope.data.totalLiabilities[i];
                    result += item.endingBalanceUSDDebt;
                }
            }
            return result;
        },
        sumEndingBalanceUSDHave: function () {
            var result = 0;
            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.totalLiabilities.length; i++) {
                    var item = $scope.data.totalLiabilities[i];
                    result += item.endingBalanceUSDHas;
                }
            }
            return result;
        },
        sumInvoiceAmount: function (detailOfLiabilitiesDto) {
            var result = 0;
            if (detailOfLiabilitiesDto !== null) {
                for (var i = 0; i < detailOfLiabilitiesDto.length; i++) {
                    var item = detailOfLiabilitiesDto[i];
                    result += item.toTalInvoiceAmount;
                }
            }
            return result;
        },
        sumAPAmount: function () {
            var result = 0;
            $scope.supportData.totalCount = 0;
            if ($scope.data != null) {
                for (var i = 0; i < $scope.data.listColorCatagoryDTOs.length; i++) {
                    var item = $scope.data.listColorCatagoryDTOs[i];
                    result = result + item.totalAmount;
                    $scope.supportData.totalCount += item.totalCount;
                }
            }
            return result;
        },
        sumColorAmount: function () {
            var result = 0;
            $scope.supportData.totalCountColor = 0;
            if ($scope.data != null) {
                for (var i = 0; i < $scope.data.listChartDetailDTOs.length; i++) {
                    var item = $scope.data.listChartDetailDTOs[i];
                    result = result + item.totalAmount;
                    $scope.supportData.totalCountColor += item.totalCount;
                }
            }
            return result;
        },
        totalAmountPI: function () {
            var result = 0;
            if ($scope.purchaseInvoiceDTO != null) {
                for (var i = 0; i < $scope.purchaseInvoiceDTO.length; i++) {
                    var item = $scope.purchaseInvoiceDTO[i];
                    if (item.purchaseInvoiceStatusID == 2 || item.purchaseInvoiceStatusID == 3) {
                        result = result + item.totalAmount;
                    }
                }
                return result;
            }
        },
        totalPaymentPI: function () {
            var result = 0;
            if ($scope.purchaseInvoiceDTO != null) {
                for (var i = 0; i < $scope.purchaseInvoiceDTO.length; i++) {
                    var item = $scope.purchaseInvoiceDTO[i];
                    if (item.purchaseInvoiceStatusID == 2 || item.purchaseInvoiceStatusID == 3) {
                        result = result + item.totalPayment;
                    }
                }
                return result;
            }
        },
        totalRemainPI: function () {
            return $scope.method.totalAmountPI() - $scope.method.totalPaymentPI();
        }
    };

    /*
     *initialization controller
     */
    $timeout(function () { $scope.event.init(); }, 0);

}]);
