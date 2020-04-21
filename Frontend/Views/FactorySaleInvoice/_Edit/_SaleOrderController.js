
tilsoftApp.controller('_SaleOrderController', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
    //
    //init controller
    //
    $scope.initController = function () {
        //
        // init service
        //
        dataService.serviceUrl = context.serviceUrl;
        dataService.supportServiceUrl = context.supportServiceUrl;
        dataService.token = context.token;

        //
        //        
        jQuery('#content').show();

        jQuery('.search-filter').keypress(function (e) {
            if (e.keyCode === 13) {
                $scope.event.reload();
            }
        });

        //
        //        
        $scope.event.init();
    };

    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
            if ($scope.pageIndex < $scope.totalPage) {
                $scope.pageIndex++;
                dataService.searchFilter.pageIndex = $scope.pageIndex;
                $scope.event.search(true);
            }
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.searchResult = [];
            dataService.searchFilter.sortedDirection = sortedDirection;
            dataService.searchFilter.pageIndex = $scope.pageIndex = 1;
            dataService.searchFilter.sortedBy = sortedBy;
            $scope.event.search();
        },
        isTriggered: false
    };

    //
    // property
    //
    $scope.searchResult = [];
    $scope.filters = {
        factorySaleOrderUD: '',
        factorySaleOrderDate: '',
        productionItemUD: '',
        productionItemNM: '',
        factoryRawMaterialID: $scope.data.factoryRawMaterialID
    };

    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;
    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;
    $scope.newID = -1;
    $scope.isAllSelected = false;


    //
    // events
    //
    $scope.event = {
        init: function () {
            //dataService.getSearchFilter(
            //    function (data) {
            //        $scope.event.search();
            //    },
            //    function (error) {
            //        // reset data
            //    }
            //);
            $scope.event.reload();
        },
        reload: function () {
            $scope.searchResult = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex;
            dataService.searchFilter.sortedBy = 'FactorySaleOrderUD';
            $scope.event.search();
        },
        search: function (isLoadMore) {
            //
            // store filter in cookies
            //
            $cookieStore.put(context.cookieId, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            $scope.gridHandler.isTriggered = false;
            dataService.searchFilter.filters = $scope.filters;
            dataService.getPurchaseOrderData(
                function (data) {
                    debugger;
                    var list = [];
                    if ($scope.data !== null) {
                        angular.forEach(data.data.data, function (item) {
                            var x = $scope.data.factorySaleInvoiceDetailDTOs.filter(o => o.factorySaleOrderDetailID === item.factorySaleOrderDetailID && o.productionItemID === item.productionItemID);
                            if (x.length === 0) {
                                list.push(item);
                            }
                        });
                        Array.prototype.push.apply($scope.searchResult, list);
                    }
                    else {
                        Array.prototype.push.apply($scope.searchResult, data.data.data);
                    }
                    $scope.totalPage = Math.ceil(data.data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.data.totalRows;
                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;


                },
                function (error) {
                    $scope.searchResult = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                }
            );
        },
        clearFilter: function () {
            $scope.filters = {
                purchaseOrderUD: '',
                purchaseOrderDate: '',
                productionItemUD: '',
                productionItemNM: '',
                factoryRawMaterialID: $scope.data.factoryRawMaterialID
            };
            $scope.event.reload();
        },
        ok: function () {
            for (let i = 0; i < $scope.searchResult.length; i++) {
                let item = $scope.searchResult[i];

                if (item.isSelected) {
                    var pItem = {
                        factorySaleInvoiceDetailID: $scope.event.getNewID(),
                        factorySaleOrderDetailID: item.factorySaleOrderDetailID,
                        factorySaleOrderID: item.factorySaleOrderID,
                        factorySaleOrderUD: item.factorySaleOrderUD,
                        fsoPrice: item.fsoPrice,
                        orderQnt: item.orderQnt,
                        productionItemID: item.productionItemID,
                        productionItemUD: item.productionItemUD,
                        productionItemNM: item.productionItemNM,
                        productionItemTypeID: item.productionItemTypeID,
                        unitNM: item.unitNM,
                        quantity: item.remainQnt,
                        unitPrice: item.fsoPrice
                        //poPrice: item.poPrice,
                        //totalReceivedQnt: item.totalReceivedQnt,
                        //productionItemTypeID: item.productionItemTypeID,
                        //receivingNoteID: item.receivingNoteID,
                        //receivingNoteDetailID: item.receivingNoteDetailID,
                        //type: item.type,
                        //poReceivedQnt: item.poReceivedQnt
                    };
                    //var listNote = {
                    //    purchaseOrderID: pItem.purchaseOrderID,
                    //    purchaseOrderDetailID: pItem.purchaseOrderDetailID,
                    //    productionItemID: pItem.productionItemID,
                    //    receivingNoteDetailID: pItem.receivingNoteDetailID,
                    //    type: pItem.type
                    //};
                    //$scope.data.listNoteStatuses.push(listNote);
                    if ($scope.data.factorySaleInvoiceDetailDTOs.filter(s => s.productionItemID === pItem.productionItemID && s.factorySaleOrderID === pItem.factorySaleOrderID).length > 0) {

                        angular.forEach($scope.data.factorySaleInvoiceDetailDTOs.filter(s => s.productionItemID === pItem.productionItemID), function (item) {
                            item.quantity += pItem.quantity;
                        });
                    }
                    else {
                        $scope.data.factorySaleInvoiceDetailDTOs.push(pItem);
                    }
                }
            }

            for (let j = 0; j < $scope.data.factorySaleInvoiceDetailDTOs.length; j++) {
                let sItem = $scope.data.factorySaleInvoiceDetailDTOs[j];
                if (sItem.factorySaleOrderID) {
                    $scope.supportData.checkHasPO = true;
                    break;
                }
            }
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },
        selectAll: function () {
            if ($scope.isAllSelected) {
                for (var i = 0; i < $scope.searchResult.length; i++) {
                    let item = $scope.searchResult[i];
                    item.isSelected = true;
                }
            }
            else {
                for (var j = 0; i < $scope.searchResult.length; i++) {
                    let item = $scope.searchResult[j];
                    item.isSelected = false;
                }
            }
        }
    };


    //
    //init
    //
    debugger;
    $scope.initController();
}]);
