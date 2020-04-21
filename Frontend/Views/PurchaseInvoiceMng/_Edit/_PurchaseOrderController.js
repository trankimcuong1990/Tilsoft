

tilsoftApp.controller('_PurchaseOrderController', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
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
            if (e.keyCode == 13) {              
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
        purchaseOrderUD: '',
        purchaseOrderDate: '',
        productionItemUD: '',
        productionItemNM: '',
        factoryRawMaterialID: $scope.data.factoryRawMaterialID,
        FromDate: '',
        ToDate: ''
    };

    $('input[name="datefilter"]').daterangepicker({
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Clear'
        }
    });

    $('input[name="datefilter"]').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format('DD/MM/YYYY') + ' - ' + picker.endDate.format('DD/MM/YYYY'));
        $scope.filters.FromDate = picker.startDate.format('DD/MM/YYYY');
        $scope.filters.ToDate = picker.endDate.format('DD/MM/YYYY');
    });

    $('input[name="datefilter"]').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });

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
            dataService.searchFilter.sortedBy = 'PurchaseOrderUD';
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
                    var list = [];
                    if ($scope.data !== null) {
                        angular.forEach(data.data.data, function (item) {
                            var x = $scope.data.purchaseInvoiceDetailDTOs.filter(o => o.purchaseOrderDetailID === item.purchaseOrderDetailID && o.productionItemID === item.productionItemID && o.receivingNoteID === item.receivingNoteID);
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
                factoryRawMaterialID: $scope.data.factoryRawMaterialID,
                FromDate: '',
                ToDate: ''
            };
            $scope.event.reload();
        },
        ok: function () {
            for (let i = 0; i < $scope.searchResult.length; i++) {
                let item = $scope.searchResult[i];
                if (item.type === 1) {
                    item.receivedQNT = 0 - item.receivedQNT;
                }

                if (item.isSelected) {
                    var remarkItem = null;
                    remarkItem = item.receivingNoteUD;
                    var pItem = {
                        purchaseInvoiceDetailID: $scope.event.getNewID(),
                        productionItemID: item.productionItemID,
                        productionItemUD: item.productionItemUD,
                        productionItemNM: item.productionItemNM,
                        purchaseOrderDetailID: item.purchaseOrderDetailID,
                        purchaseOrderID: item.purchaseOrderID,
                        purchaseOrderUD: item.purchaseOrderUD,
                        unitNM: item.unitNM,
                        orderQnt: item.orderQnt,
                        poPrice: item.poPrice,
                        unitPrice: item.poPrice,
                        quantity: item.receivedQNT,
                        totalReceivedQnt: item.totalReceivedQnt,
                        productionItemTypeID: item.productionItemTypeID,
                        receivingNoteID: item.receivingNoteID,
                        type: item.type,
                        poReceivedQnt: item.poReceivedQnt,
                        remark: remarkItem
                    };
                    debugger;
                    if ($scope.data.purchaseInvoiceDetailDTOs.filter(s => s.productionItemID === pItem.productionItemID && s.type === pItem.type && s.purchaseOrderID === pItem.purchaseOrderID).length > 0) {

                        angular.forEach($scope.data.purchaseInvoiceDetailDTOs.filter(s => s.productionItemID === pItem.productionItemID && s.type === item.type && s.purchaseOrderID === pItem.purchaseOrderID), function (item) {
                            item.quantity += pItem.quantity;
                            item.remark += ', ' + remarkItem;
                        });
                    }
                    else {
                        $scope.data.purchaseInvoiceDetailDTOs.push(pItem);
                    }                   
                }
            }

            for (let j = 0; j < $scope.data.purchaseInvoiceDetailDTOs.length; j++) {
                let sItem = $scope.data.purchaseInvoiceDetailDTOs[j];
                if (sItem.purchaseOrderID) {
                    $scope.supportData.checkHasPO = true;
                    break;
                }
            }

            let itemAmount = 0;
            let costAmount = 0;
            if ($scope.data.purchaseInvoiceDetailDTOs.length > 0) {
                for (let i = 0; i < $scope.data.purchaseInvoiceDetailDTOs.length; i++) {
                    let item = $scope.data.purchaseInvoiceDetailDTOs[i];
                    if (item.productionItemTypeID === 7) {
                        if (item.unitPrice !== null && item.quantity !== null) {
                            costAmount += (item.unitPrice * item.quantity);
                        } else {
                            costAmount += 0;
                        }
                    }

                    if (item.productionItemTypeID !== 7) {
                        if (item.unitPrice !== null && item.quantity !== null) {
                            itemAmount += (item.unitPrice * item.quantity);
                        } else {
                            itemAmount += 0;
                        }
                    }
                }
            }
            else {
                costAmount = 0;
                itemAmount = 0;
            }
            $scope.data.vatAmount = (itemAmount + costAmount) * $scope.data.vat / 100;
            $scope.data.totalAmount = costAmount + itemAmount + parseInt($scope.data.vatAmount);
           
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
                for (var j = 0; j < $scope.searchResult.length; j++) {
                    let item = $scope.searchResult[j];
                    item.isSelected = false;
                }
            }
        }
    };


    //
    //init
    //
    $scope.initController();
}]);
