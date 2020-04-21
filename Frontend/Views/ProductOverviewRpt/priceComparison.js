//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;

    $scope.seasons = [];
    $scope.seasons.push({ seasonValue: '', seasonText: '' });
    Array.prototype.push.apply($scope.seasons, jsHelper.getSeasons());

    $scope.factories = null;
    $scope.quotationStatuses = null;

    $scope.comparableItems = null;
    $scope.bestComparableItems = null;

    $scope.filters = {
        Season: '',
        FactoryID: '',
        QuotationStatusID: '',
        ModelID: '',
        FactoryOrderDetailID: '',
        MaterialID: '',
        MaterialTypeID: '',
        MaterialColorID: '',
        FrameMaterialID: '',
        FrameMaterialColorID: '',
        SubMaterialID: '',
        SubMaterialColorID: '',
        CushionID: '',
        BackCushionID: '',
        SeatCushionID: '',
        CushionColorID: '',
        FSCTypeID: '',
        FSCPercentID: '',
        UseFSCLabel: '',
        PackagingMethodID: '',
        ClientSpecialPackagingMethodID: ''
    };
    $scope.checkboxValues = {
        MaterialID: true,
        MaterialTypeID: true,
        MaterialColorID: true,
        FrameMaterialID: true,
        FrameMaterialColorID: true,
        SubMaterialID: true,
        SubMaterialColorID: true,
        CushionID: true,
        BackCushionID: true,
        SeatCushionID: true,
        CushionColorID: true,
        FSCTypeID: true,
        FSCPercentID: true,
        UseFSCLabel: true,
        PackagingMethodID: true
    };

    $scope.priceHistoryUI = {
        orderBy: '',
        filters: {
            season: '',
            factoryUD: '',
            clientUD: '',
            proformaInvoiceNo: '',
            description: ''
        }
    };
    $scope.selectedProduct = null;
    $scope.priceHistoryData = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getItemToCompareData(
                context.id, context.offerSeasonDetailID,
                function (data) {
                    if (data.message.type === 0) {
                        $scope.data = data.data.data;
                        $scope.factories = data.data.factories;
                        $scope.quotationStatuses = data.data.quotationStatuses;
                        $scope.filters.ModelID = data.data.data.modelID;
                        $scope.filters.FactoryOrderDetailID = data.data.data.factoryOrderDetailID;

                        $('#content').show();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.factories = null;
                    $scope.quotationStatuses = null;
                    $scope.filters.ModelID = null;
                    $scope.filters.FactoryOrderDetailID = null;
                }
            );
        },
        compare: function () {
            $scope.filters.MaterialID = '';
            $scope.filters.MaterialTypeID = '';
            $scope.filters.MaterialColorID = '';
            $scope.filters.FrameMaterialID = '';
            $scope.filters.FrameMaterialColorID = '';
            $scope.filters.SubMaterialID = '';
            $scope.filters.SubMaterialColorID = '';
            $scope.filters.CushionID = '';
            $scope.filters.BackCushionID = '';
            $scope.filters.SeatCushionID = '';
            $scope.filters.CushionColorID = '';
            $scope.filters.FSCTypeID = '';
            $scope.filters.FSCPercentID = '';
            $scope.filters.UseFSCLabel = '';
            $scope.filters.PackagingMethodID = '';
            $scope.filters.ClientSpecialPackagingMethodID = '';

            if ($scope.checkboxValues.MaterialID) {
                $scope.filters.MaterialID = $scope.data.materialID;
            }
            if ($scope.checkboxValues.MaterialTypeID) {
                $scope.filters.MaterialTypeID = $scope.data.materialTypeID;
            }
            if ($scope.checkboxValues.MaterialColorID) {
                $scope.filters.MaterialColorID = $scope.data.materialColorID;
            }
            if ($scope.checkboxValues.FrameMaterialID) {
                $scope.filters.FrameMaterialID = $scope.data.frameMaterialID;
            }
            if ($scope.checkboxValues.FrameMaterialColorID) {
                $scope.filters.FrameMaterialColorID = $scope.data.frameMaterialColorID;
            }
            if ($scope.checkboxValues.SubMaterialID) {
                $scope.filters.SubMaterialID = $scope.data.subMaterialID;
            }
            if ($scope.checkboxValues.SubMaterialColorID) {
                $scope.filters.SubMaterialColorID = $scope.data.subMaterialColorID;
            }
            if ($scope.checkboxValues.CushionID) {
                $scope.filters.CushionID = $scope.data.cushionID;
            }
            if ($scope.checkboxValues.BackCushionID) {
                $scope.filters.BackCushionID = $scope.data.backCushionID;
            }
            if ($scope.checkboxValues.SeatCushionID) {
                $scope.filters.SeatCushionID = $scope.data.seatCushionID;
            }
            if ($scope.checkboxValues.CushionColorID) {
                $scope.filters.CushionColorID = $scope.data.cushionColorID;
            }
            if ($scope.checkboxValues.FSCTypeID) {
                $scope.filters.FSCTypeID = $scope.data.fscTypeID;
            }
            if ($scope.checkboxValues.FSCPercentID) {
                $scope.filters.FSCPercentID = $scope.data.fscPercentID;
            }
            if ($scope.checkboxValues.UseFSCLabel) {
                $scope.filters.UseFSCLabel = $scope.data.useFSCLabel;
            }
            if ($scope.checkboxValues.PackagingMethodID) {
                $scope.filters.PackagingMethodID = $scope.data.packagingMethodID;
                $scope.filters.ClientSpecialPackagingMethodID = $scope.data.clientSpecialPackagingMethodID;
            }

            // get best comparable items
            $('#chartArea').html(''); // clear high chart
            dataService.getBestComparableItemData(
                $scope.filters,
                function (data) {
                    if (data.message.type === 0) {
                        $scope.bestComparableItems = data.data;

                        //
                        // create color table
                        //
                        var colorTable = [];
                        angular.forEach($scope.bestComparableItems, function (item) {
                            if (colorTable.length === 0 || colorTable.filter(o=>o.factoryUD === item.factoryUD).length === 0) {
                                colorTable.push({ factoryUD: item.factoryUD, color: jsHelper.getRandomColor() });
                            }
                        });

                        // get all comparable items
                        dataService.getComparableItemData(
                            $scope.filters,
                            function (data) {
                                if (data.message.type === 0) {
                                    $scope.comparableItems = data.data;
                                }
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error);
                                $scope.comparableItems = null;
                            }
                        );

                        // add high chart
                        var seasonData = [];
                        var chartIndex = 1;
                        var seasonIndex = -1;
                        angular.forEach($scope.bestComparableItems, function (item) {
                            seasonIndex = -1;
                            for (i = 0; i < seasonData.length; i++) {
                                if (seasonData[i].season == item.season) {
                                    seasonIndex = i;
                                }
                            }
                            if (seasonIndex < 0) {
                                seasonData.push({
                                    id: chartIndex,
                                    season: item.season,
                                    xData: [item.factoryUD],
                                    yData: [{ y: item.purchasingPrice, color: colorTable.filter(o=>o.factoryUD === item.factoryUD)[0].color }]
                                });
                                chartIndex++;
                            }
                            else {                                
                                seasonData[seasonIndex].xData.push(item.factoryUD);
                                seasonData[seasonIndex].yData.push({ y: item.purchasingPrice, color: colorTable.filter(o=>o.factoryUD === item.factoryUD)[0].color });
                            }
                        });
                        console.log(seasonData);

                        angular.forEach(seasonData, function (item) {
                            $('#chartArea').append('<section class="col col-6"><div id="chart_season_' + item.id + '"></div></section>');
                            Highcharts.chart('chart_season_' + item.id, {
                                chart: {
                                    type: 'column'
                                },
                                title: {
                                    text: 'Season: ' + item.season
                                },
                                xAxis: {
                                    categories: item.xData,
                                    crosshair: true
                                },
                                yAxis: {
                                    min: 0,
                                    title: {
                                        text: ''
                                    }
                                },
                                tooltip: {
                                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                                        '<td style="padding:0"><b>$ {point.y:.2f}</b></td></tr>',
                                    footerFormat: '</table>',
                                    shared: true,
                                    useHTML: true
                                },
                                plotOptions: {
                                    column: {
                                        pointPadding: 0.2,
                                        borderWidth: 0
                                    }
                                },
                                series: [{
                                    name: 'Purchasing Price',
                                    data: item.yData
                                }]
                            });
                        });
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.bestComparableItems = null;
                }
            );
        },
        priceHistory_sort: function (e) {
            $this = $(e.currentTarget);
            var newClass = '';
            if ($this.hasClass('sorting') || $this.hasClass('sorting_desc')) {
                newClass = 'sorting_asc';
            }
            else {
                newClass = 'sorting_desc';
            }
            angular.forEach($this.parent().find('th'), function (item) {
                if ($(item).hasClass('sorting_desc') || $(item).hasClass('sorting_asc')) {
                    $(item).removeClass('sorting_desc').removeClass('sorting_asc').addClass('sorting');
                }
            });
            $this.removeClass('sorting_desc').removeClass('sorting_asc').removeClass('sorting').addClass(newClass);
            if (newClass == 'sorting_desc') {
                $scope.priceHistoryUI.orderBy = '-' + $(e.currentTarget).data('column');
            }
            else {
                $scope.priceHistoryUI.orderBy = $(e.currentTarget).data('column');
            }
        },
        priceHistory_detail: function (item) {
            $scope.selectedProduct = item;
            dataService.getPriceOfferHistory(
                item.factoryOrderDetailID,
                function (data) {
                    if (data.message.type === 0) {
                        $scope.priceHistoryData = data.data;
                        $('#frmPriceHistoryDetail').modal('show');
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.priceHistoryData = null;
                }
            );
        },
    }

    //
    // method
    //
    $scope.method = {
    }


    //
    // init
    //
    $scope.event.init();
}]);