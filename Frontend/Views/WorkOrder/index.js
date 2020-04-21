jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives', 'furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', function ($scope, $cookieStore) {

    $scope.data = null;
    $scope.workCenters = [];
    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;
    $scope.workOrderStatuses = [];
    $scope.materialGroup = [];
    $scope.weaving = [];
    $scope.totalWorkOrderQnt = 0;
    $scope.filter = {
        weavingStatus: '',
    };
    $scope.listWorkOrder = null;

    $scope.filters = {
        workOrderUD: '',
        productArticleCode: '',
        productDescription: '',
        modelUD: '',        
        clientUD: '',
        proformaInvoiceNo: '',
        createdDate: '',
        isCreatedBOM: 'any',
        createdDateBOM: '',
        isConfirmed: 'any',
        startDate: '',
        finishDate: '',
        updatedDate: '',
        workOrderStatusID: null,
        workOrderPreOrderUD: '',
        productionItemGroupID: null,
        hasPurchaseRequest: 'any'
    };

    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;

    var cookieValue = $cookieStore.get(context.cookieID);
    if (cookieValue) {
        $scope.filters = cookieValue;
    }

    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
            if ($scope.pageIndex < $scope.totalPage) {
                $scope.pageIndex++;
                jsonService.searchFilter.pageIndex = $scope.pageIndex;
                $scope.event.search(true);
            }
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.data = [];
            jsonService.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = scope.pageIndex;
            jsonService.searchFilter.sortedBy = sortedBy;
            $scope.event.search(true);
        },
        isTriggered: false
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getWorkCenter(
                function (data) {
                    $scope.workCenters = data.data;
                    $scope.event.reload();                  
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );  
        },

        reload: function () {
            //reset main data
            $scope.data = [];

            //assign pager for search result
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = 1;
            jsonService.searchFilter.sortedBy = 'WorkOrderUD';
            jsonService.searchFilter.sortedDirection = 'DESC';

            //search data
            $scope.event.search();
        },
        search: function (isLoadMore) {
            $cookieStore.put(context.cookieID, $scope.filters);

            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            $scope.gridHandler.isTriggered = false;

            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                   
                    Array.prototype.push.apply($scope.data, data.data.data);
                    //support list
                    $scope.workOrderStatuses = data.data.workOrderStatuses;

                    $scope.materialGroup = data.data.productionItemGroupDTOs
                    //get total row
                    $scope.totalRows = data.totalRows;
                    $scope.totalWorkOrderQnt = data.data.totalWorkOrderQnt;
                    //cal total page
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.weaving = data.data.weavingStatus;
                    for (var i = 0; i < $scope.data.length; i++) {
                        var data1 = $scope.data[i];
                        for (var j = 0; j < $scope.weaving.length; j++) {
                            var weaving1 = $scope.weaving[j];
                            if (data1.workOrderID === weaving1.workOrderID) {
                                if (weaving1.totalDeliveryQnt === 0) {
                                    data1.weavingstatus_col = 'Not Issue';
                                }
                                else {
                                    if (weaving1.totalDeliveryQnt >= data1.quantity) {
                                        data1.weavingstatus_col = 'Finish';
                                    }
                                    else {
                                        data1.weavingstatus_col = 'In Production';
                                    }
                                }
                            }
                        }
                    }
                    $scope.method.loadDataWeavingStatus();
                    $scope.$apply();
                    jQuery('#content').show();

                    //gridHandler
                    $scope.gridHandler.refresh();
                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    $scope.data = null;
                    $scope.weaving = [];
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.$apply();
                }
            );
        },
        clearFilter: function () {
            $scope.filters = {
                workOrderUD: '',
                productArticleCode: '',
                productDescription: '',
                modelUD: '',
                clientUD: '',
                proformaInvoiceNo: '',
                createdDate: '',
                isCreatedBOM: 'any',
                createdDateBOM: '',
                isConfirmed: 'any',
                startDate: '',
                finishDate: '',
                updatedDate: '',
                workOrderStatusID: null,
                checkallStatus: false,
                workOrderPreOrderUD: ''
            };
            $scope.filter = {
                weavingStatus: ''
            };
            $scope.event.reload();
        },
        delete: function (id, pos, $event) {
            $event.preventDefault();

            if (confirm('Are you sure you want to delete work order ?')) {
                jsonService.delete(
                    id,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            if (pos >= 0) {
                                $scope.data.splice(pos, 1);
                                $scope.totalRows--;
                                $scope.$apply();
                            }
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
        },

        deliveryNoteFromWarehouseToTeam: function () {
            var workOrderIDs = '';
            var isConfirmed = true;
            angular.forEach($scope.data, function (item) {
                if (item.isSelected) {
                    workOrderIDs += item.workOrderID + ',';
                    isConfirmed = isConfirmed & item.isConfirmed;
                }
            });

            if (!isConfirmed) {
                bootbox.alert('All workorders that you are selected need to be confirmed. Please confirm workorder before delivery & receiving');
                return;
            }

            if (workOrderIDs.length > 1) {
                workOrderIDs = workOrderIDs.substring(0, workOrderIDs.length - 1);
            }
            else {
                bootbox.alert('You have to select work order');
                return;
            }
            window.open(context.deliveryNoteUrl + '0?workOrderIDs=' + workOrderIDs, '');
        },

        receivingNoteFromTeamToWarehouse: function () {
            var workOrderIDs = '';
            var isConfirmed = true;
            angular.forEach($scope.data, function (item) {
                if (item.isSelected) {
                    workOrderIDs += item.workOrderID + ',';
                    isConfirmed = isConfirmed & item.isConfirmed;
                }
            });

            if (!isConfirmed) {
                bootbox.alert('All workorders that you are selected need to be confirmed. Please confirm workorder before delivery & receiving');
                return;
            }

            if (workOrderIDs.length > 1) {
                workOrderIDs = workOrderIDs.substring(0, workOrderIDs.length - 1);
            }
            else {
                bootbox.alert('You have to select work order');
                return;
            }
            window.open(context.receivingNoteUrl + '0?workOrderIDs=' + workOrderIDs, '');
        },

        purchaseRequest: function () {
            var workOrderIDs = '';
            var isConfirmed = true;
            angular.forEach($scope.data, function (item) {
                if (item.isSelected) {
                    workOrderIDs += item.workOrderID + ',';
                    isConfirmed = isConfirmed & item.isConfirmed;
                }
            });

            if (!isConfirmed) {
                bootbox.alert('All workorders that you are selected need to be confirmed. Please confirm workorder before request');
                return;
            }

            if (workOrderIDs.length > 1) {
                workOrderIDs = workOrderIDs.substring(0, workOrderIDs.length - 1);
            }
            else {
                bootbox.alert('You have to select work order');
                return;
            }
            window.open(context.purchaseRequestUrl + '0?workOrderIDs=' + workOrderIDs, '');
        },

        exportWorkOrder: function () {
            var workOrderIDs = '';   
            $scope.listWorkOrder = null;
            angular.forEach($scope.data, function (item) {
                if (item.isSelected) {
                    if (workOrderIDs !== '') {
                        workOrderIDs += ',';
                    }
                    workOrderIDs += item.workOrderID;
                }
            });

            if (workOrderIDs.length >= 1) {
                $scope.listWorkOrder = workOrderIDs;
            }
            else {
                bootbox.alert('You have to select work order');
                return;
            }
            $("#frmExportExcelBom").modal('show');
        },

        exportBOMToExcel: function () {
            var workCenterIDs = '';
            angular.forEach($scope.workCenters, function (item) {
                if (item.isSelected) {
                    if (workCenterIDs !== '') {
                        workCenterIDs += ',';
                    }
                    workCenterIDs += item.workCenterID;
                }
            });

            if (workCenterIDs.length >= 1) {
                workCenterIDs = workCenterIDs.substring(0, workCenterIDs.length);               
            }
            else {
                bootbox.alert('You have to select work order');
                return;
            }
            jsonService.exportExcel(
                $scope.listWorkOrder,
                workCenterIDs,
                function (data) {
                    if (data.message.type === 0) {
                        window.location = context.reportUrl + data.data;
                    }
                    else {
                        jsHelper.processMessageEx(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        checkAll: function (item) {
            if (item === true) {
                angular.forEach($scope.data, function (items) {
                    items.isSelected = true;
                });
            } else {
                angular.forEach($scope.data, function (items) {
                    items.isSelected = false;
                });
            }
            
        }

    };

    //
    //method
    //
    $scope.method = {
        getCSSByWorkOrderStatus: function (woItem) {
            workOrderStatusCSS = '';
            if (woItem.workOrderStatusID == 3) {
                workOrderStatusCSS = 'finished';
            }
            else if (woItem.hasTransaction) {
                workOrderStatusCSS = 'has-transaction';
            }
            return workOrderStatusCSS;
        },
        loadDataWeavingStatus: function () {
            if ($scope.filter.weavingStatus === 'Finish') {
                $scope.method.loadStatus('Finish');
            }
            else {
                if ($scope.filter.weavingStatus === 'Not Issue') {
                    $scope.method.loadStatus('Not Issue');
                }
                else {
                    if ($scope.filter.weavingStatus === 'In Production') {
                        $scope.method.loadStatus('In Production');
                    }
                    else {
                        if ($scope.filter.weavingStatus === '') {
                            $scope.data = $scope.data;
                        }
                        else {
                            $scope.data = [];
                        }
                    }
                }
            }
                    
        },
        loadStatus: function (status) {
            var subData = [];
            for (var i = 0; i < $scope.data.length; i++) {
                var a = $scope.data[i];
                if (a.weavingstatus_col === status) {
                    subData.push(a);
                }
            }
            $scope.data = subData;
        }
    }

    $scope.event.init();
}]);