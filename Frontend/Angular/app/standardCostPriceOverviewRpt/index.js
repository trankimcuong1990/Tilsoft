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
    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', standardCostPriceOverviewRptController);
    standardCostPriceOverviewRptController.$inject = ['$scope', '$cookieStore', 'dataService', '$timeout'];

    function standardCostPriceOverviewRptController($scope, $cookieStore, sCPOService, $timeout) {
        $scope.data = [];
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.pageIndex = 1;
        $scope.defaultFactory = [];
        $scope.oldprice = 0;
        $scope.product = 0;
        $scope.costPrice = 0;
        $scope.indexs = 0;
        $scope.loadItem = [];
        $scope.isSetFactory = 0;

        $scope.filters = {
            articleCode: "",
            description: "",
            factoryUD: "",
            season:""
        };

        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    sCPOService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                sCPOService.searchFilter.sortedDirection = sortedDirection;
                sCPOService.searchFilter.pageIndex = $scope.pageIndex = 1;
                sCPOService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };

        $scope.event = {
            init: init,
            search: search,
            refresh: refresh,
            clear: clear,
            exportExcel: exportExcel,
            edit: edit,
            cancel: cancel,
            setDefaultFactory: setDefaultFactory,
            editValueCostPrice: editValueCostPrice,
            updateValueCostPrice: updateValueCostPrice,
            cancelEditValueCostPrice: cancelEditValueCostPrice,
            onUnitValueCostPrice: onUnitValueCostPrice
        };

        function init() {
            sCPOService.searchFilter.pageSize = context.pageSize;
            sCPOService.serviceUrl = context.serviceUrl;
            sCPOService.token = context.token;
            $scope.event.search();
        }
         //
        //Load Data
        //
        function search(isLoadMore) {
            //jQuery('#content').show();
            $cookieStore.put(context.cookieID, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;
            sCPOService.searchFilter.filters = $scope.filters;
            sCPOService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);  //
                    $scope.seasons = data.data.sdata; // call Season
                    $scope.totalPage = Math.ceil(data.totalRows / sCPOService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    for (var i = 0; i < $scope.data.length; i++) {
                        var item = $scope.data[i];
                        item.isEditValueCostPrice = false;
                    }
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
        }
        //
        //refresh page
        //
        function refresh() {
            $scope.data = [];
            $scope.pageIndex = 1
            sCPOService.searchFilter.pageIndex = $scope.pageIndex = 1;
            $scope.event.search();
        }
         //
        //clear Search
       //
        function clear() {
            $scope.filters = {
                articleCode: '',
                description: '',
                factoryUD: '',
                season:''
            };
            $scope.event.refresh();
        }

        //
       //Popup
      //
        function edit(item, index) {
            //$('#moreProduct').modal();
            sCPOService.getDataForPopupWithFilters(
                {
                    filters: {
                        articleCode: item.articleCode
                    }
                },
                function (data) {
                    $scope.priceDetail = data.data.data;
                    //angular.forEach($scope.priceDetail, function (item) {
                    //    for (var i = 0; i < $scope.defaultFactory.length; i++) {
                    //        $scope.productID = $scope.defaultFactory.productID[i];
                    //        if (item.productID === $scope.productID) {
                    //            item.defaultFactoryID = $scope.defaultFactory.defaultFactoryID[i];
                    //        }
                    //    }
                    //});
                    $scope.loadItem = item;
                    $scope.indexs = index;
                    $scope.$apply();
                    $('#moreProduct').modal();
                },
                function (error) {
                });
        };

        function cancel() {    
            $timeout(function () {
                $scope.event.refresh();
                $scope.isSetFactory = 0;
            }, 0);  
            $('#moreProduct').modal('hide');
        };


        //
        //Popup
        //
        function setDefaultFactory(gItem) {
            if (confirm('Are you sure set default for this Factory?')) {
                sCPOService.setDefaultFactory(
                    {
                        filters: {
                            factoryID: gItem.factoryID,
                            productID: gItem.productID
                        }
                    },
                    function (data) {
                        var index = $scope.priceDetail.indexOf(gItem);
                        $scope.priceDetail[index].defaultFactoryID = data.data.defaultFactoryID;
                        $scope.$apply();
                    },
                    function (error) {
                    });
                $timeout(function () {
                    $scope.event.edit($scope.loadItem, $scope.indexs);
                    $scope.isSetFactory += 1;
                }, 0);        
            }             
        };
        //
        //  Export ExCel
        //
        function exportExcel() {
            //$event.preventDefault();
            sCPOService.exportExcel(
                {
                    filters: {
                        UserID: $scope.filters.UserID,
                        ArticleCode: $scope.filters.ArticleCode,
                        Description: $scope.filters.Description,
                        Season: $scope.filters.Season
                    }
            },
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        //
        // Event Edit CostPrice
        //

        function editValueCostPrice(item) {
            item.isEditValueCostPrice = true;
        };

        function cancelEditValueCostPrice(item) {
            item.isEditValueCostPrice = false;
        };

        function updateValueCostPrice(item) {
            var postedData = [];
            if (item.isCostPriceChanged !== undefined && item.isCostPriceChanged == true) {
                postedData.push({
                    productID: item.productID,
                    costPrice: item.costPrice

                });
            }

            sCPOService.updateProductPrice(
                postedData,
                function (data) {
                    if (data.data) {                       
                        angular.forEach($scope.data, function (item) {
                            item.isCostPriceChanged = false;
                        });                       
                    }                  
                },
                function (error) {

                }
            );
            item.isEditValueCostPrice = false;
        };
        function onUnitValueCostPrice(item) {
            item.isCostPriceChanged = true;
        }

        //Event Init

        $scope.event.init();

        function createservices() {
            sCPOService.searchFilter.pageSize = context.pageSize;
            sCPOService.serviceUrl = context.serviceUrl;
            sCPOService.token = context.token;
        };
    };
})();