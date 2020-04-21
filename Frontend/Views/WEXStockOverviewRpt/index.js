//
// SUPPORT
//
jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', '$timeout', 'dataService', function ($scope, $cookieStore, $timeout, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.filters = {
        ArticleCode: '',
        SubEANCode: '',
        Description: '',
        ProductTypeNM: '',
        NoImage: false,
        ItemSourceID: null
    };
    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;

    $scope.isEditSellingPrice = false;
    $scope.isEditPurchasingPrice = false;
    $scope.isEditObsolete = false;
    $scope.isEditValueObsolesence = false;
    // variables to update data.
    $scope.productIDs = '';
    $scope.modelIDs = '';
    $scope.articleCodes = '';
    $scope.eanCodes = '';
    $scope.sellPrices = '';


    // variables for purchasing price
    $scope.productIDPs = '';
    $scope.purchasingPrices = '';

    $scope.wexItemSource = [{ itemSourceID: "1", name: "WEX" }, { itemSourceID: "2", name: "OTHER SOURCE" }];

    //
    // get filter from cookies
    //
    var cookieValue = $cookieStore.get(context.cookieId);
    if (cookieValue) {
        $scope.filters = cookieValue;
    }

    $scope.data = [];

    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;

    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
            //if ($scope.pageIndex < $scope.totalPage) {
            //    $scope.pageIndex++;
            //    dataService.searchFilter.pageIndex = $scope.pageIndex;
            //    $scope.event.search(true);
            //}
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

    //
    // event
    //
    $scope.event = {
        reload: function () {
            $scope.data = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex;
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
            dataService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;

                    $timeout(function () {
                        jsHelper.refreshAvsScroll();
                    }, 100);
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                }
            );
        },
        clearFilter: function () {
            $scope.filters = {
                ArticleCode: '',
                SubEANCode: '',
                Description: '',
                ProductTypeNM: '',
                ItemSourceID: ''
            };
            $scope.event.reload();
        },
        init: function () {
            $scope.event.search();
        },
        exportExcel: function () {
            dataService.exportExcel(
                {
                    filters: $scope.filters,
                    sortedBy: 'StockQnt',
                    sortedDirection: 'DESC',
                    pageSize: 20,
                    pageIndex: 1
                },
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type !== 2) {
                        window.open(context.backendReportUrl + data.data);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        // update selling price task
        editSellingPrice: function () {
            $scope.isEditSellingPrice = true;
        },
        cancelEditSellingPrice: function () {
            $scope.isEditSellingPrice = false;
        },
        updateSellingPrice: function () {
            $scope.method.getDataToUpdate();
            var postedData = [];
            angular.forEach($scope.data, function (item) {
                if (item.isSellingPriceChanged) {
                    postedData.push({
                        productID: item.productID,
                        price: item.unitPrice
                    });
                }
            });
            dataService.updateSellingPrice(
                postedData,
                function (data) {
                    if (data.data) {
                        $scope.isEditSellingPrice = false;
                        angular.forEach($scope.data, function (item) {
                            item.isSellingPriceChanged = false;
                        });
                    }
                },
                function (error) {
                }
            );
        },
        onUnitPriceChange: function (item) {
            item.isSellingPriceChanged = true;
        },


        //Update purchasing Price 

        editPurchasingPrice: function () {
            $scope.isEditPurchasingPrice = true;
        },

        cancelEditPurchasingPrice: function () {
            $scope.isEditPurchasingPrice = false;
        },

        updatePurchasingPrice: function () {
            $scope.method.getDataForPurchasingPrice();
            var postedData = [];

            angular.forEach($scope.data, function (item) {
                if (item.isPurchasingPriceChanged) {
                    postedData.push({
                        productID: item.productID,
                        price: item.vvpPrice

                    });
                }
            });

            dataService.updatePurchasingPrice(
                postedData,
                function (data) {
                    if (data.data) {
                        $scope.isEditPurchasingPrice = false;
                        angular.forEach($scope.data, function (item) {
                            item.isPurchasingPriceChanged = false;
                        });
                    }
                },
                function (error) {

                }
            );
        },

        onUnitPurchasingChange: function (item) {
            if (item.vvpPrice !== null) {
                item.isPurchasingPriceChanged = true;
            } else {
                item.vvpPrice = null;
            }
            
        },

        //Update % obsolete

        editObsolete: function () {
            $scope.isEditObsolete = true;
        },

        cancelEditObsolete: function () {
            $scope.isEditObsolete = false;
        },

        updateObsolete: function () {
            var postedData = [];

            angular.forEach($scope.data, function (item) {
                if (item.isObsoleteChange) {
                    postedData.push({
                        productID: item.productID,
                        obsolete: item.obsolete

                    });
                }
            });

            dataService.updateObsolete(
                postedData,
                function (data) {
                    if (data.data) {
                        $scope.isEditObsolete = false;
                        angular.forEach($scope.data, function (item) {
                            item.isObsoleteChange = false;
                        });
                    }
                },
                function (error) {

                }
            );
        },

        onUnitObsolete: function (item) {
            if (item.obsolete !== "") {
                item.isObsoleteChange = true;
            } else {
                item.obsolete = null;
            }
            
        },

        //Update

        editValueObsolescence: function () {
            $scope.isEditValueObsolescence = true;
        },

        cancelEditValueObsolescence: function () {
            $scope.isEditValueObsolescence = false;
        },

        updateValueObsolescence: function () {
            var postedData = [];

            angular.forEach($scope.data, function (item) {
                if (item.isValueObsolescenceChange) {
                    postedData.push({
                        productID: item.productID,
                        valueObsolescence: item.valueObsolescence

                    });
                }
            });

            dataService.updateValueObsolescence(
                postedData,
                function (data) {
                    if (data.data) {
                        $scope.isEditValueObsolescence = false;
                        angular.forEach($scope.data, function (item) {
                            item.isValueObsolescenceChange = false;
                        });
                    }
                },
                function (error) {

                }
            );
        },

        onUnitValueObsolescence: function (item) {
            if (item.valueObsolescence !== "") {
                item.isValueObsolescenceChange = true;
            } else {
                item.valueObsolescence = null;
            }
            
        },

        //
        // Event Edit CostPrice
        //

        editValueSalePrice:function (item) {
            item.isEditValueSalePrice = true;
        },

        cancelEditValueSalePrice: function(item) {
            item.isEditValueSalePrice = false;
        },

        updateValueSalePrice: function(item) {
            var postedData = [];
            if (item.isSalePriceChanged !== undefined && item.isSalePriceChanged === true) {
                postedData.push({
                    productID: item.productID,
                    costPrice: item.costPrice

                });
            }

            dataService.updateProductPrice(
                postedData,
                function (data) {
                    if (data.data) {
                        angular.forEach($scope.data, function (item) {
                            item.isSalePriceChanged = false;
                        });
                    }
                },
                function (error) {

                }
            );
            item.isEditValueSalePrice = false;
        },

        onUnitValueSalePrice: function(item) {
            item.isSalePriceChanged = true;
        },

        updateEnableStatus: function (enabled) {
            var postedData = $scope.data.filter(o => o.isSelected);
            if (postedData.length === 0) {
                alert('No item has been selected!');
                return;
            }
            angular.forEach(postedData, function (item) {
                item.isEnabled = enabled;
            });

            dataService.updateEnableStatus(
                postedData,
                function (data) {
                    if (data.message.type === 0) {
                        angular.forEach($scope.data, function (item) {
                            item.isSelected = false;
                        });
                    }
                    else {
                        console.log(data);
                    }
                },
                function (error) {
                }
            );
        }

    };
    //
    // method
    //
    $scope.method = {
        getTotalStockQnt: function () {
            if ($scope.data) {
                var total = 0;
                angular.forEach($scope.data, function (item) {
                    if (item.stockQnt !== null) {
                        total += parseInt(item.stockQnt);
                    }
                });

                return total;
            }

            return 0;
        },
        getTotalFTSQnt: function () {
            if ($scope.data) {
                var total = 0;
                angular.forEach($scope.data, function (item) {
                    if (item.freeToSaleQnt !== null) {
                        total += parseInt(item.freeToSaleQnt);
                    }
                });

                return total;
            }

            return 0;
        },
        getTotalStockQnt40HC: function () {
            if ($scope.data) {
                var total = 0;
                angular.forEach($scope.data, function (item) {
                    if (item.stockQnt !== null && item.qnt40HC !== null && item.qnt40HC > 0) {
                        total += jsHelper.round(parseFloat(item.stockQnt) / parseFloat(item.qnt40HC), 2);
                    }
                });

                return total;
            }

            return 0;
        },
        getTotalSellingAmount: function () {
            if ($scope.data) {
                var total = 0;
                angular.forEach($scope.data, function (item) {
                    if (item.stockQnt !== null && item.unitPrice !== null) {
                        total += jsHelper.round(parseFloat(item.stockQnt) * parseFloat(item.unitPrice), 2);
                    }
                });

                return total;
            }

            return 0;
        },
        getTotalPurchasingAmount: function () {
            if ($scope.data) {
                var total = 0;
                var subTotal = 0;
                angular.forEach($scope.data, function (item) {
                    subTotal = 0;
                    var costPrice = item.salePrice !== null && item.salePrice !== 0 ? item.salePrice : item.costPrice !== null && item.costPrice !== 0 ? item.costPrice : 0;
                    var stockQnt = item.stockQnt !== null ? item.stockQnt : 0;

                    if (item.valueObsolescence) {
                        subTotal += item.valueObsolescence * stockQnt;
                    }
                    else if (item.obsolete) {
                        subTotal += costPrice * (1 - jsHelper.round(item.obsolete / 100, 2)) * stockQnt;
                    }
                    else {
                        subTotal += costPrice * stockQnt;
                    }

                    if (subTotal) {
                        total += subTotal;
                    }
                });

                return total;
            }

            return 0;
        },
        getDataToUpdate: function () {
            var i = 0;

            angular.forEach($scope.data, function (value, key) {
                if (i > 0) {
                    $scope.productIDs = $scope.productIDs + ',';
                    //$scope.modelIDs = $scope.modelIDs + ',';
                    //$scope.articleCodes = $scope.articleCodes + ',';
                    //$scope.eanCodes = $scope.eanCodes + ',';
                    $scope.sellPrices = $scope.sellPrices + ',';
                }

                $scope.productIDs = $scope.productIDs + value.productID;
                //$scope.modelIDs = $scope.modelIDs + value.modelID;
                //$scope.articleCodes = $scope.articleCodes + value.articleCode;
                //$scope.eanCodes = $scope.eanCodes + value.eanCode;
                $scope.sellPrices = $scope.sellPrices + value.unitPrice;

                i = i + 1;
            });
        },

        getDataForPurchasingPrice: function () {
            var j = 0;

            angular.forEach($scope.data, function (value, key) {
                if (j > 0) {
                    $scope.productIDPs = $scope.productIDPs + ',';
                    $scope.purchasingPrices = $scope.purchasingPrices + ',';
                }

                $scope.productIDPs = $scope.productIDPs + value.productIDP;
                $scope.purchasingPrices = $scope.purchasingPrices + value.purchasingPrice;

                j = j + 1;
            });
        },

        getValueIncludeObsolesence: function (item) {
            var result = parseFloat(0);
            var costPrice = item.salePrice !== null && item.salePrice !== 0 ? item.salePrice : item.costPrice !== null && item.costPrice !== 0 ? item.costPrice : 0;
            var stockQnt = item.stockQnt !== null ? item.stockQnt : 0;

            if (item.valueObsolescence) {
                result = item.valueObsolescence * stockQnt;
            }
            else if (item.obsolete) {
                result = costPrice * (1 - jsHelper.round(item.obsolete / 100, 2)) * stockQnt;
            }
            else {
                result = costPrice * stockQnt;
            }
            if (result) {

                return result;
            }
            return parseFloat(0);
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);