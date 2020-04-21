jQuery('.search-filter').keypress(
    function (event) {
        if (event.keyCode === 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.refresh();
        }
    }
);

(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', breakDownController);
    breakDownController.$inject = ['$scope', '$cookieStore', 'dataService'];

    function breakDownController($scope, $cookieStore, breakDownService) {
        $scope.data = [];
        $scope.season = [];
        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageIndex = 1;
        $scope.totalAVFConfirmedItem = null;
        $scope.totalAVTConfirmedItem = null;

        $scope.companyID = context.companyID;

        $scope.masterChange = null;
        //list 9 option
        $scope.listCaculateUSD = [];
        //list 1 option
        $scope.listCaculateMangementCost = [];
        $scope.calculationPriceMangementCost = [];

        $scope.filters = {
            breakDownUD: '',
            type: '',
            clientUD: '',
            description: '',
            season: null,
            modelStatusID: '',
            defaultFactory: '',
        };
        $scope.exchange = 1;

        $scope.listTypes = [
            { type: '', typeName: '' },
            { type: 1, typeName: 'Model' },
            { type: 2, typeName: 'Sample' }
        ];

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;


        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    breakDownService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];

                breakDownService.searchFilter.sortedDirection = sortedDirection;
                breakDownService.searchFilter.pageIndex = $scope.pageIndex = 1;
                breakDownService.searchFilter.sortedBy = sortedBy;                
                $scope.event.search();
            },
            isTriggered: false
        };

        $scope.event = {
            init: init,
            search: search,
            refresh: refresh,
            clear: clear,
            //remove: remove
            getFilterData: getFilterData,
        };
        $scope.method = {
            caculatePriceFullOptionVNDAVF: function (item) {
                var percent = 0;
                var totalAmountWithMangementCost = 0;
                for (var j = 0; j < $scope.listCaculateMangementCost.length; j++) {
                    var subItem = $scope.listCaculateMangementCost[j];
                    if (subItem.companyID === 1) //AVF
                    {
                        if (subItem.breakdownID === item.breakdownID) {
                            percent = percent + subItem.quantityPercent;
                        }
                    }                     
                }
                totalAmountWithMangementCost = (item.amountOptionTotalAVF * percent) / 100;
                item.masterChangeAVF += jsHelper.round(totalAmountWithMangementCost / $scope.masterChange, 2);
                if (item.amountOptionTotalAVF > 0)
                    return item.amountOptionTotalAVF + totalAmountWithMangementCost;
                else
                    return 0;
            },

            caculatePriceFullOptionVNDAVT: function (item) {
                var percent = 0;
                var totalAmountWithMangementCost = 0;
                for (var j = 0; j < $scope.listCaculateMangementCost.length; j++) {
                    var subItem = $scope.listCaculateMangementCost[j];
                    if (subItem.companyID === 3) //AVF
                    {
                        if (subItem.breakdownID === item.breakdownID) {
                            percent = percent + subItem.quantityPercent;
                        }
                    }
                }
                totalAmountWithMangementCost = (item.amountOptionTotalAVT * percent) / 100;
                item.masterChangeAVT += jsHelper.round(totalAmountWithMangementCost / $scope.masterChange,2);
                return item.amountOptionTotalAVT + totalAmountWithMangementCost;
            }
        };

        function getFilterData() {
            breakDownService.getFilterData(
                function (data) {
                    $scope.season = data.data.seasons;
                    $scope.modelStatuses = data.data.modelStatuses;
                    $scope.event.search();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function init() {
            breakDownService.searchFilter.pageSize = context.pageSize;
            breakDownService.serviceUrl = context.serviceUrl;
            breakDownService.token = context.token;
            $scope.event.getFilterData();


            //breakDownService.getCaculation(
            //    function (data) {
            //        $scope.listCaculateUSD = data.data.caculatePriceUSDs;
            //        $scope.calculationPriceMangementCost = data.data.calculationPriceMangementCosts;
            //        $scope.masterChange = data.data.exchange; 
            //        $scope.event.getFilterData();      
            //    },
            //    function (error) {
            //        jsHelper.showMessage('warning', error);
            //    }
            //);          
        };

        function search(isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            breakDownService.searchFilter.filters = $scope.filters;
            breakDownService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / breakDownService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    $scope.exchange = data.data.exchange;
                    $scope.totalAVFConfirmedItem = data.data.totalAVFConfirmedItem;
                    $scope.totalAVTConfirmedItem = data.data.totalAVTConfirmedItem;

                    //var calculationPriceMangementCosts = $scope.calculationPriceMangementCost;

                    ////filters listCaculateUSD input to listCaculateOf9option every breakdownID have only 10 option for a company
                    //var listCaculateOf9option = [];
                    //var breakdownid = null;
                    //var categoryid = null;
                    //var companyid = null;                   
                    //for (var c = 0; c < $scope.listCaculateUSD.length; c++) {
                    //    var list = $scope.listCaculateUSD[c];
                    //    if (c === 0) {
                    //        breakdownid = list.breakdownID;
                    //        categoryid = list.breakdownCategoryID;
                    //        companyid = list.companyID;
                    //        //if (list.amount !== null) {
                    //            listCaculateOf9option.push(list);
                    //        //}                          
                    //    }

                    //    if (list.breakdownID === breakdownid && list.breakdownCategoryID === categoryid && list.companyID === companyid) {
                    //        continue;
                    //    }
                    //    breakdownid = list.breakdownID;
                    //    categoryid = list.breakdownCategoryID;
                    //    companyid = list.companyID;
                    //    //if (list.amount !== null) {
                    //        listCaculateOf9option.push(list);
                    //    //}                       
                    //}

                    ////filters calculationPriceMangementCosts input to $scope.listCaculateMangementCost every breakdownID have only option 10 for a company
                    //$scope.listCaculateMangementCost = [];
                    //var breakdownidMangement = null;
                    //var categoryidMangement = null;
                    //var companyidMangement = null;
                    //for (var d = 0; d < calculationPriceMangementCosts.length; d++) {
                    //    var list1 = calculationPriceMangementCosts[d];
                    //    if (d === 0) {
                    //        breakdownidMangement = list1.breakdownID;
                    //        categoryidMangement = list1.breakdownCategoryID;
                    //        companyidMangement = list1.companyID;
                    //        if (list1.quantityPercent !== null) {
                    //            $scope.listCaculateMangementCost.push(list1);
                    //        }
                    //    }

                    //    if (list1.breakdownID === breakdownidMangement && list1.breakdownCategoryID === categoryidMangement && list1.companyID === companyidMangement) {
                    //        continue;
                    //    }
                    //    breakdownidMangement = list1.breakdownID;
                    //    categoryidMangement = list1.breakdownCategoryID;
                    //    companyidMangement = list1.companyID;
                    //    if (list1.quantityPercent !== null) {
                    //        $scope.listCaculateMangementCost.push(list1);
                    //    }
                    //}
                    //// Caculation
                    //for (var j = 0; j < listCaculateOf9option.length; j++) {
                    //    var itemList = listCaculateOf9option[j]; 
                    //    itemList.caculateUSD = jsHelper.round(itemList.amount / $scope.masterChange,2);                       
                    //}
                    //for (var i = 0; i < $scope.data.length; i++) {
                    //    var item = $scope.data[i];
                    //    var masterAVF = 0;
                    //    var masterAVT = 0;
                    //    var amountOption9OfAVF = 0;
                    //    var amountOption9OfAVT = 0;
                    //    var zzAVF = 0;
                    //    var zzAVT = 0;
                    //    var listCaculate = listCaculateOf9option.filter(function (o) { return o.breakdownID === item.breakdownID; });
                    //    var CountAVF = 0;
                    //    var CountAVT = 0;
                    //    for (var z = 0; z < listCaculate.length; z++) {
                    //        var itemListCaculate = listCaculate[z];
                    //        if (itemListCaculate.companyID === 1) //AVF
                    //        {
                    //            masterAVF += itemListCaculate.caculateUSD;
                    //            amountOption9OfAVF += itemListCaculate.amount;
                    //            CountAVF++;
                    //        }
                    //        if (itemListCaculate.companyID === 3) {//AVT
                    //            masterAVT += itemListCaculate.caculateUSD;
                    //            amountOption9OfAVT += itemListCaculate.amount;
                    //            CountAVT++;
                    //        }                                                                                
                    //    }
                    //    if (CountAVF < 10) {
                    //        item.masterChangeAVF = 0;
                    //    }
                    //    else {
                    //        item.masterChangeAVF = masterAVF;
                    //    }

                    //    if (CountAVT < 10) {
                    //        item.masterChangeAVT = 0;
                    //    }
                    //    else {
                    //        item.masterChangeAVT = masterAVT;
                    //    }
                    //    //Amount AVF
                    //    item.amountOptionTotalAVF = amountOption9OfAVF;                       
                    //    zzAVF = $scope.method.caculatePriceFullOptionVNDAVF(item);
                    //    item.amountOptionTotalAVF = zzAVF;
                    //    var seasonpercentAVF = item.amountOptionTotalAVF * (1 + item.seasonSpecPercent / 100);
                    //    item.amountOptionTotalAVF = seasonpercentAVF;
                    //    var seasonpercentUSDAVF = jsHelper.round(item.masterChangeAVF,2) * (1 + item.seasonSpecPercent / 100);
                    //    item.masterChangeAVF = seasonpercentUSDAVF;

                    //     //Amount AVT
                    //    item.amountOptionTotalAVT = amountOption9OfAVT;
                    //    zzAVT = $scope.method.caculatePriceFullOptionVNDAVT(item);
                    //    item.amountOptionTotalAVT = zzAVT;
                    //    var seasonpercentAVT = item.amountOptionTotalAVT * (1 + item.seasonSpecPercent / 100);
                    //    item.amountOptionTotalAVT = seasonpercentAVT;
                    //    var seasonpercentUSDAVT = jsHelper.round(item.masterChangeAVT,2) * (1 + item.seasonSpecPercent / 100);
                    //    item.masterChangeAVT = seasonpercentUSDAVT;

                    //    //Add item for Type
                    //    if (item.modelID !== null && item.modelID !== 0) {
                    //        item.typeName = 'Model';
                    //    }
                    //    else {
                    //        if (item.sampleProductID !== null && item.sampleProductID !== 0) {
                    //            item.typeName = 'Sample Product';
                    //        }                          
                    //    }
                    //}

                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }

                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;

            breakDownService.searchFilter.pageIndex = 1;

            $scope.event.search(false);
        };

        function clear() {
            $scope.filters = {
                breakDownUD: '',
                type: '',
                clientUD: '',
                description: ''
            };
            $scope.event.refresh();

        };

        //function remove(item) {
        //    breakDownService.delete(
        //        item.breakdownID,
        //        null,
        //        function (data) {
        //            jsHelper.processMessage(data.message);

        //            if (data.message.type === 0) {
        //                var index = $scope.data.indexOf(item);

        //                $scope.data.splice(index, 1);
        //                $scope.totalRows = $scope.totalRows - 1;
        //            }
        //        },
        //        function (error) {
        //            jsHelper.showMessage('warning', error);
        //        }
        //    );
        //}
        $scope.event.init();
    };
})();
