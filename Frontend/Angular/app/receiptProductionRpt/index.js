$('.search-filter').keypress(
    function (event) {
        if (event.keyCode === 13) {
            var scope = angular.element(jQuery("body")).scope();

            if (scope.filters.workOrderID === undefined || scope.filters.workOrderID === null) {
                scope.receiptProductionRpts = [];

                scope.totalRows = 0;
                scope.totalPage = 0;
                scope.pageIndex = 1;

                scope.$apply();

                return false;
            }

            scope.event.refreshDataWithFilter();
        }
    }
);

(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', ReceiptProductionRptController);
    ReceiptProductionRptController.$inject = ['$scope', 'dataService'];

    function ReceiptProductionRptController($scope, ReceiptProductionRptService) {
        // properties.
        $scope.workCenters = [];
        $scope.productionTeams = [];
        $scope.factoryWarehouses = [];
        $scope.receiptProductionRpts = [];
        $scope.receivingNote = [];

        $scope.totalRows = 0;
        $scope.totalPage = 0;
        $scope.pageIndex = 1;

        $scope.isNullProductionTeam = false;
        $scope.selectedWorkOrderUD = null;

        // filters.
        $scope.filters = {
            workOrderUD: null,
            workOrderID: null,
            workCenterID: null,
            productionTeamID: null
        };

        // declare name event.
        $scope.event = {
            getInitData: getInitData,
            getDataWithFilter: getDataWithFilter,
            refreshDataWithFilter: refreshDataWithFilter,
            updateReceivingNote: updateReceivingNote,
            changeWorkOrder: changeWorkOrder
        };

        // declare name method.
        $scope.method = {
            getDataReceivingNote: getDataReceivingNote,
            checkLeastOneRowUpdate: checkLeastOneRowUpdate
        };

        // declare gridHandler.
        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex = $scope.pageIndex + 1;

                    ReceiptProductionRptService.searchFilter.pageIndex = $scope.pageIndex;

                    $scope.event.getDataWithFilter(true);
                }
            },
            sort: function () {
                $scope.productionItemIssue = [];

                ReceiptProductionRptService.searchFilter.sortedDirection = sortedDirection;
                ReceiptProductionRptService.searchFilter.pageIndex = $scope.pageIndex = 1;
                ReceiptProductionRptService.searchFilter.sortedBy = sortedBy;

                $scope.event.getDataWithFilter();
            },
            isTriggered: false
        };

        // default call event getInitData.
        $scope.event.getInitData();

        // define event getInitData.
        function getInitData() {
            ReceiptProductionRptService.serviceUrl = context.serviceUrl;
            ReceiptProductionRptService.token = context.token;
            ReceiptProductionRptService.searchFilter.pageSize = context.pageSize;

            ReceiptProductionRptService.getInit(
                function (data) {
                    $scope.workCenters = data.data.workCenters;
                    $scope.productionTeams = data.data.productionTeams;
                    $scope.factoryWarehouses = data.data.factoryWarehouses;
                    
                    $('#workOrder').autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                cache: false,
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json',
                                    'Authorization': 'Bearer ' + context.token
                                },
                                type: 'POST',
                                data: JSON.stringify({
                                    filters: {
                                        searchQuery: request.term
                                    }
                                }),
                                dataType: 'json',
                                url: context.serviceUrl + 'getWorkOrder',
                                beforeSend: function () {
                                    jsHelper.loadingSwitch(true);
                                },
                                success: function (data) {
                                    jsHelper.loadingSwitch(false);

                                    response(data.data);
                                },
                                error: function (xhr, ajaxOptions, throwError) {
                                    jsHelper.loadingSwitch(false);

                                    errorCallBack(xhr.responseJSON.exceptionMessage);
                                }
                            });
                        },
                        minLength: 3,
                        select: function (event, ui) {
                            $scope.filters.workOrderID = ui.item.id;
                            $scope.selectedWorkOrderUD = $scope.filters.workOrderUD = ui.item.value;

                            $scope.$apply();
                        }
                    });

                    $('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                })
        };

        // define event getDataWithFilter.
        function getDataWithFilter(isLoadMore) {
            $scope.gridHandler.isTriggered = false;

            ReceiptProductionRptService.searchFilter.filters = $scope.filters;

            ReceiptProductionRptService.search(
                function (data) {
                    Array.prototype.push.apply($scope.receiptProductionRpts, data.data.receiptProductions);
                    
                    $scope.totalPage = Math.ceil(data.totalRows / ReceiptProductionRptService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    
                    angular.forEach($scope.receiptProductionRpts, function (main, key) {
                        angular.forEach(main.receiptProductionDetails, function (child, key) {
                            child.historyReceiptProductionTeams = [];
                            child.totalIssuedQnt = 0;

                            angular.forEach(data.data.receiptProductionQuantities, function (history, key) {
                                if (history.bomid === child.bomid && history.workOrderID === child.workOrderID && history.workCenterID === main.workCenterID && history.productionItemID === child.productionItemID) {
                                    child.totalIssuedQnt = child.totalIssuedQnt + history.totalQnt;
                                    child.historyReceiptProductionTeams.push(history);
                                }
                            })
                        })
                    })
                   
                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }

                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        // define event refreshDataWithFilter.
        function refreshDataWithFilter() {
            $scope.receiptProductionRpts = [];

            ReceiptProductionRptService.searchFilter.pageIndex = $scope.pageIndex = 1;

            $scope.event.getDataWithFilter();
        };

        // define event updateReceivingNote.
        function updateReceivingNote() {
            $scope.receivingNote = [];

            if ($scope.filters.productionTeamID === null) {
                $scope.isNullProductionTeam = true;
                return false;
            }

            var checkLeastRows = $scope.method.checkLeastOneRowUpdate();
            if (!checkLeastRows) {
                jsHelper.showMessage('warning', context.messageNoItems);
                return false;
            }

            $scope.method.getDataReceivingNote();
            
            ReceiptProductionRptService.update(
                    1,
                    $scope.receivingNote,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            $scope.isNullProductionTeam = false;

                            $scope.event.refreshDataWithFilter();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
        };

        // define event changeWorkOrder.
        function changeWorkOrder() {
            if ($scope.selectedWorkOrderUD !== $scope.filters.workOrderUD) {
                $scope.filters.workOrderID = null;
            }
        };

        // define method getDataReceivingNote.
        function getDataReceivingNote() {
            var receivingNote = {
                receivingNoteID: -1,
                fromProductionTeamID: $scope.filters.productionTeamID,
                workOrderID: $scope.filters.workOrderID,
                viewName: 'Team2Warehouse',
                opSequenceDetailID: null,

                receivingNoteDetails: []
            };

            angular.forEach($scope.receiptProductionRpts, function (value, key) {
                var opSequenceDetailID = value.workCenterID;
                
                if (value.receiptProductionDetails.length > 0) {
                    var j = -1;

                    angular.forEach(value.receiptProductionDetails, function (value, key) {
                        if (value.receiptProductionQnt !== undefined && value.receiptProductionQnt !== null && value.receiptProductionQnt !== '') {
                            receivingNote.opSequenceDetailID = opSequenceDetailID;

                            receivingNote.receivingNoteDetails.push({
                                receivingNoteDetailID: j,
                                productionItemID: value.productionItemID,
                                quantity: value.receiptProductionQnt,
                                toFactoryWarehouseID: value.toFactoryWarehouseID,
                                bomid: value.bomid
                            });

                            j = j - 1;
                        }
                    });
                }
            });

            $scope.receivingNote.push(receivingNote);
        };

        // define method checkLeastOneRowUpdate.
        function checkLeastOneRowUpdate() {
            var isHaveItems = false;

            angular.forEach($scope.receiptProductionRpts, function (value, key) {
                if (value.receiptProductionDetails.length > 0) {
                    angular.forEach(value.receiptProductionDetails, function (value, key) {
                        if (value.receiptProductionQnt !== undefined && value.receiptProductionQnt !== null) {
                            isHaveItems = true;
                            return isHaveItems;
                        }
                    });
                }
            });
            
            return isHaveItems;
        };
    }
})();
