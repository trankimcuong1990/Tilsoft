function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.label,
            description: item.value,
            defaultFactoryWarehouse: item.factoryWarehouseID
        };
    });
}

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
    var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);

    tilsoftApp.filter('projectOnHandFilter', function () {
        return function (productionItemData, productItemDate, filterBySatus) {
            var result = [];

            angular.forEach(productionItemData, function (pItem) {
                if (filterBySatus === 1) {
                    result.push(pItem);
                }
                else if (filterBySatus === 2) {
                    var isValid = false;
                    for (var i = 0; i < productItemDate.length; i++) {
                        var x = productItemDate[i];
                        if (x.productionItemID === pItem.productionItemID) {
                            if (x.projectOnHandItem < 0) {
                                isValid = true;
                                break;
                            }
                        }
                    }
                    if (isValid) {
                        result.push(pItem);
                    }
                }
                else if (filterBySatus === 3) {
                    var isValid = true;
                    for (var i = 0; i < productItemDate.length; i++) {
                        var x = productItemDate[i];
                        if (x.productionItemID === pItem.productionItemID) {
                            isValid = isValid && (x.projectOnHandItem >= 0);
                            if (!isValid) break;
                        }
                    }
                    if (isValid) {
                        result.push(pItem);
                    }
                }
            });


            //for (var i = 0; i < productionItemData.length; i++) {
            //    var pItem = productionItemData[i];
            //    if (filterBySatus === 2) {
            //        for (var j = 0; j < productItemDate.length; j++) {
            //            var xx = productItemDate[j];
            //            if (parseFloat(xx.projectOnHandItem) < parseFloat(0) && pItem.productionItemID === xx.productionItemID) {
            //                result.push(pItem);
            //                //var isValid = true;
            //                break;
            //            }
            //        }
            //    }
            //    if (filterBySatus === 1) {
            //        result.push(pItem);
            //    }

            //    if (filterBySatus === 3) {

            //        for (var j = 0; j < productItemDate.length; j++) {
            //            var xx = productItemDate[j];
            //            if (parseFloat(xx.projectOnHandItem) > parseFloat(0) && pItem.productionItemID === xx.productionItemID) {
            //                //result.push(pItem);
            //                 isValid = false;
            //                break;
            //            }
            //        }
            //    }
            //}

            //for (var i = 0; i < productionItemData.length; i++) {
            //    var pItem = productionItemData[i];
            //    if (filterBySatus === 2) {
            //        for (var j = 0; j < productItemDate.length; j++) {
            //            var xx = productItemDate[j];
            //            if (xx.projectOnHandItem < 0) {
            //                if (result.length !== 0) {
            //                    var isPush = false;
            //                    for (var x = 0; x < result.length; x++) {
            //                        var xResut = result[x];
            //                        if (pItem.productionItemID !== xResut.productionItemID) {
            //                            isPush = true;
            //                            break;
            //                        }
            //                    }
            //                    if (isPush === true) {
            //                        result.push(pItem);
            //                        break;
            //                    }
            //                } else {
            //                    result.push(pItem);
            //                }
            //            }
            //        }
            //    }
            //}

            //var isValid = false;
            //angular.forEach(productItemDateByItem, function (item) {
            //    //console.log(filterBySatus);
            //    if (filterBySatus == 2) {
            //        console.log('ok');
            //        if (item.projectOnHandItem < 0) isValid = true;
            //    }
            //});
            //if (isValid) {
            //    result.push(productionItemDataByItem);
            //}
            //console.log(result);

            return result;
        };
    });


    tilsoftApp.controller('tilsoftController', mrpRptController);
    mrpRptController.$inject = ['$scope', '$timeout', '$cookieStore', 'dataService']

    function mrpRptController($scope, $timeout, $cookieStore, dataService) {

        //Varible Zone
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;
        $scope.supportItemGroup = [];
        $scope.dateRanges = [];
        $scope.fromDate = '';
        $scope.toDate = '';
        $scope.dateRanges = [];
        $scope.productItemDate = [];
        $scope.purchaseOrderArr = [];
        $scope.productionAffterFilters = [];
        $scope.tempArr = [];
        //$scope.data2 = [];

        $scope.filtersStatus = [{ name: 'Show all', statusID: 1 }, { name: 'show only items which are lack of quantity', statusID: 2 }, { name: 'show only items which are enough for production', statusID: 3 }];

        //Quick Search ProductionItem
        $scope.ngInitProductionItem = null;

        //Quick Search ProductionItem
        $scope.ngItemProductionItem = {
            id: null,
            label: null,
            description: null
        };

        //Quick Search ProductionItem
        $scope.ngRequestProductionItem = {
            url: context.supportServiceUrl + 'getProductionItemWithFilters',
            token: context.token
        };

        //Quick Search ProductionItem
        $scope.ngSelectedProductionItem = {
            id: null,
            label: null,
            description: null
        };

        //Filters
        $scope.filters = {
            ProductionItemID: null,
            ProductionItemGroupID: null,
            statusID: null,
            fromDate: null,
            toDate: null
        };

        //GridHandler
        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    dataService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                dataService.searchFilter.sortedDirection = sortedDirection;
                $scope.pageIndex = 1;
                dataService.searchFilter.pageIndex = scope.pageIndex;
                dataService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };

        //event
        $scope.event = {
            search: search,
            clear: clear,
            refresh: refresh,
            init: init,
            getDates: getDates,
            filterData: filterData,
            getProductionItemList: getProductionItemList,
            getPurchaseList: getPurchaseList,
            getProjectOnHand: getProjectOnHand,
            getfiltersList: getfiltersList,
            //checkStatus: checkStatus
        };

        function init() {
            //dataService.getSearchFilter(
            //    function (data) {
            //        $scope.event.search();
            //    },
            //    function (error) {
            //        // reset data
            //    }
            //);
            //$scope.event.search();
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
            dataService.supportServiceUrl = context.supportServiceUrl;

            dataService.getInit(
                function (data) {
                    $scope.supportItemGroup = data.data.supportProductionItemGroupSearchViews;
                    jQuery('#content').show();
                },
                function (error) {

                    //$scope.$apply();
                    jsHelper.showMessage('warning', error);
                }
            );
        }


        //Load Data
        function search(isLoadMore) {
            dataService.searchFilter.pageSize = context.pageSize;
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;

            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.gridHandler.isTriggered = false;
            dataService.searchFilter.filters = $scope.filters;

            dataService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    //$scope.supportItemGroup = data.data.supportProductionItemGroupSearchViews;
                    $scope.purchaseOrderMRP = data.data.purchaseOrderDetailMRPs;
                    $scope.purchaseRequestMRP = data.data.purchaseRequestDetailMRPs;

                    jQuery("#content").show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;

                    $scope.event.getProductionItemList();
                    $scope.event.getPurchaseList();
                    $scope.event.getProjectOnHand();
                    
                    //$scope.event.getfiltersList();
                },
                function (error) {
                    //not thing to do
                }
            );
        }

        //Event Clear
        function clear() {
            $scope.filters = {
                ProductionItemID: '',
                ProductionItemGroupID: null
            };
            $scope.event.refresh();
        }

        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex = 1;

            $timeout(function () {
                $scope.event.search();
            }, 0);
            //debugger
            //$scope.event.getProjectOnHand();
        }

        //Gate Range Time
        function getDates(startDate, endDate) {
            var dateRanges = [];
            var currentDate = startDate;

            function addDays(days) {
                var date = new Date(this.valueOf());
                date.setDate(date.getDate() + days);
                return date;
            }

            while (currentDate <= endDate) {
                dateRanges.push(currentDate);
                currentDate = addDays.call(currentDate, 1);
            }
            return dateRanges;
        }

        //Get Range Time and List 
        function filterData(fromDate, toDate) {
            debugger
            $scope.dateRanges = [];
            $scope.productItemDate = [];
            $scope.purchaseOrderArr = [];
            //$scope.data2 = [];
            $scope.productionAffterFilters = [];

            $scope.filters.ProductionItemID = null;

            if (fromDate === null || toDate === null) {
                bootbox.alert('Plase Input From Date and To Date');
                return;
            }



            if ($scope.ngItemProductionItem !== null) {
                $scope.filters.ProductionItemID = $scope.ngItemProductionItem.id;
            }

            if ($scope.filters.ProductionItemID === null && $scope.filters.ProductionItemGroupID === null) {
                bootbox.alert('Plase Input Production Item or Production Group');
                return;
            }

            //if ($scope.filters.statusID === null) {
            //    bootbox.alert('Plase Input Condition')
            //    return;
            //}

            $scope.dateRanges = [];
            //get day && month && year		
            var frday = parseInt(fromDate.substr(0, 2));
            var frmm = parseInt(fromDate.substr(3, 2));
            var fryyyy = parseInt(fromDate.substr(6, 4));

            var frmmNew = (frmm - 1);

            var tday = parseInt(toDate.substr(0, 2));
            var tmm = parseInt(toDate.substr(3, 2));
            var tyyyy = parseInt(toDate.substr(6, 4));

            var tmmNew = (tmm - 1);

            var dateRanges = getDates(new Date(fryyyy, frmmNew, frday), new Date(tyyyy, tmmNew, tday));

            dateRanges.forEach(function (date) {
                var dd = date.getDate();
                var mm = date.getMonth() + 1;
                var yy = date.getFullYear();
                if (dd < 10) { dd = '0' + dd }
                if (mm < 10) { mm = '0' + mm }

                var currenDate = dd + '/' + mm + '/' + yy;

                var dateList = {
                    date: currenDate
                }
                $scope.dateRanges.push(dateList);
            });
            $scope.filters.statusID = 1;
            $scope.event.refresh();
            //$scope.data2 = $scope.data;
        }

        //Gate Production Item List
        function getProductionItemList() {
            $scope.productItemDate = [];
            for (var i = 0; i < $scope.data.length; i++) {
                var productItem = $scope.data[i];
                for (var j = 0; j < $scope.dateRanges.length; j++) {
                    var dateItem = $scope.dateRanges[j];

                    var productDateItem = {
                        productionItemID: productItem.productionItemID,
                        projectOnHandItem: null,
                        date: dateItem.date,
                        orderQnt: 0,
                        requestQnt: 0
                    }
                    $scope.productItemDate.push(productDateItem);
                }
            }
        }

        //Get Purchase List 
        function getPurchaseList() {

            for (var e = 0; e < $scope.productItemDate.length; e++) {
                var productionItem = $scope.productItemDate[e];

                for (var r = 0; r < $scope.purchaseOrderMRP.length; r++) {
                    var purchaseOrderItem = $scope.purchaseOrderMRP[r];
                    if (purchaseOrderItem.productionItemID === productionItem.productionItemID && purchaseOrderItem.plannedETA === productionItem.date) {
                        productionItem.orderQnt = purchaseOrderItem.plannedReceivingQnt;
                    }
                }

                for (var t = 0; t < $scope.purchaseRequestMRP.length; t++) {
                    var purchaseRequestItem = $scope.purchaseRequestMRP[t];

                    if (purchaseRequestItem.productionItemID === productionItem.productionItemID && purchaseRequestItem.eta === productionItem.date) {
                        productionItem.requestQnt = purchaseRequestItem.requestQnt;
                    }
                }
            }
        }

        //Get ProjectOnHand List
        function getProjectOnHand() {
            for (var a = 0; a < $scope.data.length; a++) {
                var product = $scope.data[a];
                var stockQnt = product.projectOnHand;
                var projectOnHandItemTemp = null;
                var index = 0;

                for (var s = 0; s < $scope.productItemDate.length; s++) {
                    var newProductionItem = $scope.productItemDate[s];
                    if (product.productionItemID === newProductionItem.productionItemID) {

                        if (index === 0) {
                            projectOnHandItemTemp = parseFloat(stockQnt) + (newProductionItem.orderQnt === null ? 0 : parseFloat(newProductionItem.orderQnt)) - (newProductionItem.requestQnt === null ? 0 : parseFloat(newProductionItem.requestQnt));
                            newProductionItem.projectOnHandItem = projectOnHandItemTemp;
                            index++;
                        } else {
                            projectOnHandItemTemp = parseFloat(projectOnHandItemTemp) + (newProductionItem.orderQnt === null ? 0 : parseFloat(newProductionItem.orderQnt)) - (newProductionItem.requestQnt === null ? 0 : parseFloat(newProductionItem.requestQnt));
                            newProductionItem.projectOnHandItem = projectOnHandItemTemp;

                        }
                    }
                }
            }
        }

        //Filter Data
        function getfiltersList() {

            // filters for show only items which are lack of quantity
            if ($scope.statusID === 2) {

                $scope.productionAffterFilters = [];

                for (var i = 0; i < $scope.productItemDate.length; i++) {

                    var productionItem = $scope.productItemDate[i];

                    if (productionItem.projectOnHandItem < 0) {

                        var lackOfQuantity = {
                            productionItemID: productionItem.productionItemID
                        };

                        if ($scope.productionAffterFilters.length !== 0) {

                            var isPush = false;
                            for (var q = 0; q < $scope.productionAffterFilters.length; q++) {

                                var kjectItem = $scope.productionAffterFilters[q];

                                if (productionItem.productionItemID !== kjectItem.productionItemID) {
                                    isPush = true;
                                    break;
                                }
                            }

                            if (isPush === true) {
                                $scope.productionAffterFilters.push(lackOfQuantity);
                                break;
                            }
                        }
                        else {
                            $scope.productionAffterFilters.push(lackOfQuantity);
                        }
                    }
                }
                //angular.forEach($scope.productItemDate, function (item) {

                //    if (item.projectOnHandItem < 0) {

                //        var lackOfQuantity = {
                //            productionItemID: item.productionItemID
                //        }

                //        if ($scope.productionAffterFilters.length !== 0) {
                //            for (var i = 0; i < $scope.productionAffterFilters.length; i++) {
                //                var itemFilter = $scope.productionAffterFilters[i];
                //                if (item.productionItemID !== itemFilter.productionItemID) {
                //                    $scope.productionAffterFilters.push(lackOfQuantity);
                //                    break;
                //                }
                //            }
                //        } else {
                //            $scope.productionAffterFilters.push(lackOfQuantity);
                //        }
                //    }

                //})
                console.log($scope.productionAffterFilters);
            }

            //Filters show only items which are enough for production
            if ($scope.statusID === 3) {

                $scope.productionAffterFilters = [];

                for (var ii = 0; iii < $scope.productItemDate.length; ii++) {

                    var productionItem3 = $scope.productItemDate[ii];

                    if (productionItem3.projectOnHandItem < 0) {

                        var lackOfQuantity3 = {
                            productionItemID: productionItem3.productionItemID
                        };

                        if ($scope.tempArr.length !== 0) {

                            var isPush3 = false;
                            for (var t = 0; t < $scope.tempArr.length; t++) {

                                var kjectItem3 = $scope.tempArr[t];

                                if (productionItem3.productionItemID !== kjectItem3.productionItemID) {
                                    isPush3 = true;
                                    break;
                                }
                            }

                            if (isPush3 === true) {
                                $scope.tempArr.push(lackOfQuantity3);
                                break;
                            }
                        }
                        else {
                            $scope.tempArr.push(lackOfQuantity3);
                        }
                    }
                }

                //for (var e = 0; e < $scope.productItemDate.length; e++) {

                //    var productionItemStatus3 = $scope.productItemDate[e];

                //    for (var z = 0; z < $scope.tempArr.length; z++) {
                //        var tempItem = $scope.tempArr[z];

                //        if (productionItemStatus3.productionItemID !== tempItem.productionItemID) {

                //        }
                //    }


                //}
                //angular.forEach($scope.productItemDate, function (item) {

                //    if (item.projectOnHandItem > 0) {

                //        var enoughForProduct = {
                //            productionItemID: item.productionItemID
                //        };

                //        if ($scope.productionAffterFilters.length !== 0) {

                //            for (var a = 0; a < $scope.productionAffterFilters.length; a++) {
                //                var enoughItem = $scope.productionAffterFilters[a];

                //                if (item.productionItemID !== enoughItem.productionItemID) {
                //                    $scope.productionAffterFilters.push(enoughForProduct);
                //                }
                //            }
                //        } else {
                //            $scope.productionAffterFilters.push(enoughForProduct);
                //        }
                //    }

                //})

                console.log($scope.productionAffterFilters);
            }

            //Filters Show all
            if ($scope.statusID === 1) {
                //To do Some thing
            }

        }

        //checkStatus function() {
        //    if ($scope.fromDate === null && $scope.toDate === null) {
        //        $scope.filter.statusID = null;
        //        bootbox.alert('');
        //    }
        //}

        //
        // angular init
        //
        $timeout(function () {
            $scope.event.init();
        }, 0);

        //Creat Service
        function createservices() {
            dataService.searchFilter.pageSize = context.pageSize;
            dataService.searchFilter.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
        }

    }

})();