//
// SUPPORT
//
jQuery('.search-filter').keypress(function(e){
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
        Season: context.autoSeason ? context.autoSeason : jsHelper.getCurrentSeason(),
        Remark: '',
        FactoryUD: '',
        Description: context.autoArtCode ? context.autoArtCode : '',
        PackagingMethodNM: '',
        UserID: context.userId
    };

    //
    // get filter from cookies
    //

    $scope.data = [];
    $scope.histories = [];
    $scope.seasons = jsHelper.getSeasons();

    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;

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
                    $('#content').show();

                    // refresh the grid
                    $timeout(function () {
                        jsHelper.refreshAvsScroll();
                    }, 100);

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                }
            );
        },
        init: function () {
            $scope.event.search();
        },
        addOtherFactory: function (item) {
            $scope.frmSelectFactoryObject.method.openForm(
                function (item, factoryID, factoryUD) {
                    var newItem = angular.copy(item);
                    newItem.updatorName = 'yourself';
                    newItem.updatedDate = 'just now';
                    newItem.updatedBy = context.userId;
                    newItem.estimatedPrice = null;
                    newItem.remark = null;
                    newItem.newEstimatedPrice = null;
                    newItem.newRemark = null;
                    newItem.estimatedPurchasingPriceID = dataService.getIncrementId();
                    newItem.factoryID = factoryID;
                    newItem.factoryUD = factoryUD;
                    $scope.data.unshift(newItem);
                    $('#pnlDetailArea').scrollTop(0);
                },
                item
            );
        },
        update: function () {
            var toBeUpdatedData = $scope.data.filter(o=> o.newEstimatedPrice || o.newRemark || o.estimatedPurchasingPriceID <= 0);

            if (toBeUpdatedData.length > 0) {
                dataService.updateBatch(
                    toBeUpdatedData,
                    function (data) {
                        if (data.message.type === 0) {
                            angular.forEach(toBeUpdatedData, function (item) {
                                if (item.newEstimatedPrice) item.estimatedPrice = item.newEstimatedPrice;
                                if (item.newRemark) item.remark = item.newRemark;
                                item.updatedBy = context.userId;
                                item.updatorName = 'yourself';
                                item.updatedDate = 'just now';
                                item.isHistoryLoaded = false;

                                item.newEstimatedPrice = null;
                                item.newRemark = null;
                            });
                        }
                    },
                    function (error) {
                        console.log(error);
                    }
                );
            }
        },
        getHistory: function (item) {
            if (item.isHistoryLoaded) {
                $scope.histories = item.historyDTOs;
            }
            else {
                dataService.getHistory(
                    item.estimatedPurchasingPriceID,
                    function (data) {
                        if (data.message.type === 0) {
                            $scope.histories = data.data;
                            item.historyDTOs = data.data;
                            item.isHistoryLoaded = true;                            
                        }
                    },
                    function (error) {
                        console.log(error);
                        $scope.histories = [];
                        item.historyDTOs = []
                        item.isHistoryLoaded = false;
                    }
                );
            }  
            $('#frmHistory').modal('show');
        }
    },
    //
    // init
    //
    $scope.event.init();


    //
    // frmSelectFactory
    //
    $scope.frmSelectFactoryObject = {
        data: {
            factoryID: null
        },
        supportData: {
            factories: null
        },
        beforeCloseCallBack: null,
        beforeCloseCallBackParam: null,
        event: {
            onOK: function () {
                if ($scope.frmSelectFactoryObject.beforeCloseCallBack && $scope.frmSelectFactoryObject.beforeCloseCallBackParam) {
                    $scope.frmSelectFactoryObject.beforeCloseCallBack(
                        $scope.frmSelectFactoryObject.beforeCloseCallBackParam,
                        $scope.frmSelectFactoryObject.data.factoryID,
                        $scope.frmSelectFactoryObject.supportData.factories.filter(o=>o.factoryID === $scope.frmSelectFactoryObject.data.factoryID)[0].factoryUD
                    );
                }

                $scope.frmSelectFactoryObject.method.closeForm();
            },
            onCancel: function () {
                $scope.frmSelectFactoryObject.method.closeForm();
            }
        },
        method: {
            closeForm: function () {
                $scope.frmSelectFactoryObject.data.factoryID = null;
                $scope.frmSelectFactoryObject.beforeCloseCallBack = null;
                $scope.frmSelectFactoryObject.beforeCloseCallBackParam = null;
                $('#frmSelectFactory').modal('hide');
            },
            openForm: function (beforeCloseCallBack, beforeCloseCallBackParam) {
                $scope.frmSelectFactoryObject.beforeCloseCallBack = beforeCloseCallBack;
                $scope.frmSelectFactoryObject.beforeCloseCallBackParam = beforeCloseCallBackParam;
                if (!$scope.frmSelectFactoryObject.supportData.factories) {
                    // query data
                    dataService.getFactories(
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.frmSelectFactoryObject.supportData.factories = data.data.factoryDTOs;
                            }
                        },
                        function (error) {
                            console.log(error);
                        }
                    );
                }
                $('#frmSelectFactory').modal('show');
            }
        }
    };
}]);