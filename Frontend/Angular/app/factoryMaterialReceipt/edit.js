//var quickSearchResultGridMaterial = jQuery('#quickSearchResultGrid_Material').scrollableTable2(
//    'quickSearchMaterial.filters.pageIndex',
//    'quickSearchMaterial.totalPage',
//    function (currentPage) {
//        var scope = angular.element(jQuery('body')).scope();
//        scope.$apply(function () {
//            scope.quickSearchMaterial.filters.pageIndex = currentPage;
//            scope.quickSearchMaterial.method.search();
//        });
//    },
//    function (sortedBy, sortedDirection) {
//        var scope = angular.element(jQuery('body')).scope();
//        scope.$apply(function () {
//            scope.quickSearchMaterial.filters.sortedDirection = sortedDirection;
//            scope.quickSearchMaterial.filters.pageIndex = 1;
//            scope.quickSearchMaterial.filters.sortedBy = sortedBy;
//            scope.quickSearchMaterial.method.search();
//        });
//    }
//);

var quickSearchResultGridMaterial = jQuery('#quickSearchResultGrid_Material').scrollableTable(
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quickSearchMaterial.filters.pageIndex = currentPage;
            scope.quickSearchMaterial.method.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.pageIndex = 1;
            scope.quickSearchMaterial.filters.pageIndex = 1;
            scope.quickSearchMaterial.filters.sortedBy = sortedBy;
            scope.quickSearchMaterial.filters.sortedDirection = sortedDirection;
            scope.quickSearchMaterial.method.search();
        });
    }
);


var grdSearchStockFree = jQuery('#grdSearchStockFree').scrollableTable(
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.searchStockFreeForm.filters.pageIndex = currentPage;
            scope.searchStockFreeForm.method.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.pageIndex = 1;
            scope.searchStockFreeForm.filters.pageIndex = 1;
            scope.searchStockFreeForm.filters.sortedBy = sortedBy;
            scope.searchStockFreeForm.filters.sortedDirection = sortedDirection;
            scope.searchStockFreeForm.method.search();
        });
    }
);



var grdNeedToImportByOrder = jQuery('#grdNeedToImportByOrder').scrollableTable(
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.searchToImportByOrder.filters.pageIndex = currentPage;
            scope.searchToImportByOrder.method.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.pageIndex = 1;
            scope.searchToImportByOrder.filters.pageIndex = 1;
            scope.searchToImportByOrder.filters.sortedBy = sortedBy;
            scope.searchToImportByOrder.filters.sortedDirection = sortedDirection;
            scope.searchToImportByOrder.method.search();
        });
    }
);

var grdStockRemain = jQuery('#grdStockRemain').scrollableTable(
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quickSearchStockRemain.filters.pageIndex = currentPage;
            scope.quickSearchStockRemain.method.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.pageIndex = 1;
            scope.quickSearchStockRemain.filters.pageIndex = 1;
            scope.quickSearchStockRemain.filters.sortedBy = sortedBy;
            scope.quickSearchStockRemain.filters.sortedDirection = sortedDirection;
            scope.quickSearchStockRemain.method.search();
        });
    }
);


var grdReceiptDetail = jQuery('#grdReceiptDetail').scrollableTable(null,
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        datasource = scope.data.factoryMaterialReceiptDetails;
        if (sortedDirection == 'asc') {
            datasource.sort(function (a, b) {
                return a[sortedBy] > b[sortedBy] ? 1 : -1;
            });
        }
        else if (sortedDirection == 'desc') {
            datasource.sort(function (a, b) {
                return a[sortedBy] < b[sortedBy] ? 1 : -1;
            });
        }
        scope.$apply();
    });

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.seasons = null;
    $scope.factoryTeams = null;
    $scope.factoryAreas = null;
    $scope.newID = -1;
    $scope.receiptTypes = [{ receiptTypeID: 1, receiptTypeName: 'Input' }, { receiptTypeID: 2, receiptTypeName: 'Output' }];
    $scope.baseOnTypes = [{ baseOnTypeID: 1, baseOnTypeName: 'Free, without orders' }, { baseOnTypeID: 2, baseOnTypeName: 'Base on orders' }],

    $scope.ui = {
        currentForm: '',
        title: ''
    }

    $scope.selectedTeamItem = null;
    $scope.selectedStepItem = null;
    $scope.selectdGoodsProcedureItem = null;
    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.seasons = data.data.seasons;
                    $scope.factoryTeams = data.data.factoryTeams;
                    $scope.factoryAreas = data.data.factoryAreas;
                    $scope.$apply();
                    jQuery('#content').show();
                    $scope.method.setUI();
                    //
                    if (context.id == 0) {
                        jsHelper.showPopup('receiptTypeForm', function () { });
                    }

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },
        update: function ($event) {
            $event.preventDefault();

            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryMaterialReceiptID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', context.errMsg);
            }
        },
        delete: function ($event) {
            $event.preventDefault();

            if (confirm('Are you sure you want to delete ?')) {
                jsonService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },
        removeMaterial: function ($event, index) {
            $event.preventDefault();
            $scope.data.factoryMaterialReceiptDetails.splice(index, 1);
        },
        onNextButtonClick: function () {            
            if ($scope.data.receiptTypeID == 2)
            {
                console.log($scope.data.factoryTeamID);
                if ($scope.selectedTeamItem == null) {
                    bootbox.alert({ message: 'You have to select team to export material', size: 'small' });
                    return;
                }

                if ($scope.selectedStepItem == null) {
                    bootbox.alert({ message: 'You have to select step to export material', size: 'small' });
                    return;
                }

                if ($scope.selectdGoodsProcedureItem == null) {
                    bootbox.alert({ message: 'You have to good procedure step to export material', size: 'small' });
                    return;
                }
                //team
                $scope.data.factoryTeamID = $scope.selectedTeamItem.factoryTeamID;
                $scope.data.factoryTeamNM = $scope.selectedTeamItem.factoryTeamNM;
                //step
                $scope.data.factoryStepID = $scope.selectedStepItem.factoryStepID;
                $scope.data.factoryStepNM = $scope.selectedStepItem.factoryStepNM;
                //goods procedure
                $scope.data.factoryGoodsProcedureID = $scope.selectdGoodsProcedureItem.factoryGoodsProcedureID;
                $scope.data.factoryGoodsProcedureNM = $scope.selectdGoodsProcedureItem.factoryGoodsProcedureNM;
            }

            jsHelper.hidePopup('receiptTypeForm', function () {
                $scope.method.setUI();
            });
        },

        getReceiptPrintout: function () {
            jsonService.getReceiptPrintout(
                context.id,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type != 2) {
                        window.open(context.reportUrl + data.data, '');
                    }
                },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
        }

    },

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            window.location = context.refreshUrl + id
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },
        getReceiptTypeName: function (id) {
            if ($scope.receiptTypes == null) return '';
            name = '';
            for (i = 0; i < $scope.receiptTypes.length; i++) // nen dung for thay vi angular.forEach doi voi truong hop tim gia tri (tim thay la lap tuc exit vong for, angular.forEach ko exit duoc)
            {
                item = $scope.receiptTypes[i];
                if (item.receiptTypeID == id) {
                    name = item.receiptTypeName;
                    break;
                }
            }
            return name;
        },
        getFactoryTeamName: function (id) {
            if ($scope.factoryTeams == null) return '';
            name = '';
            for (i = 0; i < $scope.factoryTeams.length; i++) // nen dung for thay vi angular.forEach doi voi truong hop tim gia tri (tim thay la lap tuc exit vong for, angular.forEach ko exit duoc)
            {
                item = $scope.factoryTeams[i];
                if (item.factoryTeamID == id) {
                    name = item.factoryTeamNM;
                    break;
                }
            }
            return name;
        },
        setUI: function () {
            if ($scope.data.receiptTypeID == 1) {
                $scope.ui.currentForm = 'import-receipt';
                $scope.ui.title = 'Input receipt';
            }
            else if ($scope.data.receiptTypeID == 2) {
                $scope.ui.currentForm = 'export-receipt';
                $scope.ui.title = 'Output receipt';
            }
            $scope.$apply();
        }
    },


    //quick search material to import (free import)
    $scope.quickSearchMaterial = {
        data: null,
        filters: {
            filters: {
                searchQuery: ''
            },
            sortedBy: 'FactoryMaterialNM',
            sortedDirection: 'ASC',
            pageSize: 20,
            pageIndex: 1
        },
        totalPage: 0,

        method: {
            search: function () {
                supportService.quickSearchFactoryMaterial(
                    $scope.quickSearchMaterial.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quickSearchMaterial.data = data.data.data;
                            var totalRows = data.data.totalRows;

                            $scope.quickSearchMaterial.totalPage = Math.ceil(totalRows / $scope.quickSearchMaterial.filters.pageSize);
                            quickSearchResultGridMaterial.updateLayout();
                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        event: {
            openScreen: function ($event) {
                $event.preventDefault();
                $scope.ui.currentForm = 'search-free-material';
                $scope.ui.title = 'Select material to output'
            },
            search: function ($event) {
                $event.preventDefault();
                $scope.quickSearchMaterial.filters.pageIndex = 1;
                $scope.quickSearchMaterial.method.search();
            },
            close: function ($event) {
                $event.preventDefault();
                $scope.method.setUI();
            },
            itemSelected: function ($event) {
                angular.forEach($scope.quickSearchMaterial.data, function (material_item) {
                    if (material_item.isSelected) {
                        $scope.data.factoryMaterialReceiptDetails.push({
                            factoryMaterialReceiptDetailID: $scope.method.getNewID(),
                            factoryMaterialID: material_item.factoryMaterialID,
                            unitID: material_item.unitID,
                            factoryMaterialUD: material_item.factoryMaterialUD,
                            factoryMaterialNM: material_item.factoryMaterialNM,
                            unitNM: material_item.unitNM,
                        });
                    }
                });
                $scope.quickSearchMaterial.event.close($event);
            }
        }
    },

    //search material to import by order
    $scope.searchToImportByOrder = {
        data: null,
        filters: {
            filters: {
                factoryMaterialUD: '',
                factoryMaterialNM: '',
                factoryFinishedProductUD: '',
                factoryFinishedProductNM: '',
                articleCode: '',
                description: '',
                clientUD: '',
                proformaInvoiceNo: '',
            },
            sortedBy: 'FactoryMaterialNM',
            sortedDirection: 'ASC',
            pageSize: 20,
            pageIndex: 1
        },
        totalPage: 0,

        method: {
            search: function () {
                jsonService.searchToImportByOrder(
                    $scope.searchToImportByOrder.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.searchToImportByOrder.data = data.data.data;
                            var totalRows = data.data.totalRows;

                            $scope.searchToImportByOrder.totalPage = Math.ceil(totalRows / $scope.searchToImportByOrder.filters.pageSize);
                            grdStockRemain.updateLayout();
                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        event: {
            openScreen: function ($event) {
                $event.preventDefault();
                $scope.ui.currentForm = 'search-material-to-import-by-order';
                $scope.ui.title = 'Select material to import'
            },

            search: function ($event) {
                $event.preventDefault();
                $scope.searchToImportByOrder.filters.pageIndex = 1;
                $scope.searchToImportByOrder.method.search();
            },

            close: function ($event) {
                $event.preventDefault();
                $scope.method.setUI();
            },
            itemSelected: function ($event) {
                $event.preventDefault();

                angular.forEach($scope.searchToImportByOrder.data, function (stock_remain_item) {
                    if (stock_remain_item.isSelected) {
                        var exportItem = {
                            factoryMaterialReceiptDetailID: $scope.method.getNewID(),
                            factoryOrderDetailID: stock_remain_item.factoryOrderDetailID,
                            factoryFinishedProductID: stock_remain_item.factoryFinishedProductID,
                            factoryMaterialID: stock_remain_item.factoryMaterialID,
                            factoryMaterialOrderNormID: stock_remain_item.factoryMaterialOrderNormID,
                            factoryAreaID: stock_remain_item.factoryAreaID,

                            totalStockQnt: stock_remain_item.totalStockQnt,
                            factoryMaterialUD: stock_remain_item.factoryMaterialUD,
                            factoryMaterialNM: stock_remain_item.factoryMaterialNM,
                            factoryAreaNM: stock_remain_item.factoryAreaNM,
                            factoryUD: stock_remain_item.factoryUD,
                            lds: stock_remain_item.lds,
                            proformaInvoiceNo: stock_remain_item.proformaInvoiceNo,
                            clientUD: stock_remain_item.clientUD,
                            articleCode: stock_remain_item.articleCode,
                            description: stock_remain_item.description,
                            factoryFinishedProductUD: stock_remain_item.factoryFinishedProductUD,
                            factoryFinishedProductNM: stock_remain_item.factoryFinishedProductNM,
                            unitNM: stock_remain_item.unitNM,
                            normValue: stock_remain_item.normValue,
                            orderQnt: stock_remain_item.orderQnt,
                        }
                        if ($scope.data.factoryMaterialReceiptDetails.filter(function (o) { return o.factoryOrderDetailID == stock_remain_item.factoryOrderDetailID && o.factoryFinishedProductID == stock_remain_item.factoryFinishedProductID && o.factoryMaterialID == stock_remain_item.factoryMaterialID && o.factoryMaterialOrderNormID == stock_remain_item.factoryMaterialOrderNormID }).length == 0) {
                            $scope.data.factoryMaterialReceiptDetails.push(exportItem);
                        }
                    }
                });
                $scope.searchToImportByOrder.event.close($event);
            }
        }
    }

    //search material from free stock to export
    $scope.searchStockFreeForm = {
        data: null,
        filters: {
            filters: {
                factoryMaterialUD: '',
                factoryMaterialNM: '',

                factoryMaterialID : null,
                factoryAreaID: null,
                factoryTeamID: null
            },
            sortedBy: 'FactoryMaterialNM',
            sortedDirection: 'ASC',
            pageSize: 20,
            pageIndex: 1
        },
        totalPage: 0,
        exportParentItem: null, // this varible is use in case user export item to a parent export item

        method: {
            search: function () {
                //set filter
                if ($scope.searchStockFreeForm.exportParentItem != null)
                {
                    $scope.searchStockFreeForm.filters.filters.factoryMaterialID = $scope.searchStockFreeForm.exportParentItem.factoryMaterialID;
                    $scope.searchStockFreeForm.filters.filters.factoryAreaID = $scope.searchStockFreeForm.exportParentItem.factoryAreaID;
                }
                $scope.searchStockFreeForm.filters.filters.factoryTeamID = $scope.data.factoryTeamID;
                //search
                jsonService.searchStockFree(
                    $scope.searchStockFreeForm.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.searchStockFreeForm.data = data.data.data;
                            var totalRows = data.data.totalRows;

                            $scope.searchStockFreeForm.totalPage = Math.ceil(totalRows / $scope.searchStockFreeForm.filters.pageSize);
                            grdSearchStockFree.updateLayout();
                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        event: {
            openScreen: function ($event) {
                $event.preventDefault();
                $scope.ui.currentForm = 'search-material-to-export-from-free-stock';
                $scope.ui.title = 'Select material from free stock to export'
            },
            search: function ($event) {
                $event.preventDefault();
                $scope.searchStockFreeForm.filters.pageIndex = 1;
                $scope.searchStockFreeForm.method.search();
            },
            close: function ($event) {
                $event.preventDefault();
                $scope.method.setUI();
            },
            itemSelected: function ($event) {
                angular.forEach($scope.searchStockFreeForm.data, function (material_item) {
                    if (material_item.isSelected) {
                        var exportItem = {
                            factoryMaterialReceiptDetailID: $scope.method.getNewID(),
                            factoryMaterialID: material_item.factoryMaterialID,
                            factoryAreaID: material_item.factoryAreaID,
                            totalStockQnt: material_item.totalStockQnt,
                            factoryMaterialUD: material_item.factoryMaterialUD,
                            factoryMaterialNM: material_item.factoryMaterialNM,
                            factoryAreaNM: material_item.factoryAreaNM,
                            unitNM: material_item.unitNM,

                            //parent item
                            factoryMaterialReceiptDetails: []
                        }
                        if ($scope.searchStockFreeForm.exportParentItem == null) {
                            if ($scope.data.factoryMaterialReceiptDetails.filter(function (o) { return o.factoryMaterialID == material_item.factoryMaterialID && o.factoryAreaID == material_item.factoryAreaID }).length == 0) {
                                $scope.data.factoryMaterialReceiptDetails.push(exportItem);
                            }
                        }
                        else {
                            if ($scope.searchStockFreeForm.exportParentItem.factoryMaterialReceiptDetails.filter(function (o) { return o.factoryMaterialID == material_item.factoryMaterialID && o.factoryAreaID == material_item.factoryAreaID }).length == 0) {
                                exportItem.isFreeStock = true;
                                $scope.searchStockFreeForm.exportParentItem.factoryMaterialReceiptDetails.push(exportItem);
                            }
                        }
                    }
                });
                $scope.searchStockFreeForm.event.close($event);
            }
        }
    },


    //search stock material by orders to export
    $scope.quickSearchStockRemain = {
        data: null,
        filters: {
            filters: {
                factoryMaterialUD: '',
                factoryMaterialNM: '',
                factoryFinishedProductUD: '',
                factoryFinishedProductNM: '',
                articleCode: '',
                description: '',
                clientUD: '',
                proformaInvoiceNo: '',

                factoryMaterialID: null,
                factoryAreaID: null,
                factoryTeamID: null
            },
            sortedBy: 'FactoryMaterialNM',
            sortedDirection: 'ASC',
            pageSize: 20,
            pageIndex: 1
        },
        totalPage: 0,

        exportParentItem: null, // this varible is use in case user export item to a parent export item

        method: {
            search: function () {
                //set filter
                if ($scope.quickSearchStockRemain.exportParentItem != null) {
                    $scope.quickSearchStockRemain.filters.filters.factoryMaterialID = $scope.quickSearchStockRemain.exportParentItem.factoryMaterialID;
                    $scope.quickSearchStockRemain.filters.filters.factoryAreaID = $scope.quickSearchStockRemain.exportParentItem.factoryAreaID;
                }
                $scope.quickSearchStockRemain.filters.filters.factoryTeamID = $scope.data.factoryTeamID;

                //search
                jsonService.quickSearchStockRemain(
                    $scope.quickSearchStockRemain.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quickSearchStockRemain.data = data.data.data;
                            var totalRows = data.data.totalRows;

                            $scope.quickSearchStockRemain.totalPage = Math.ceil(totalRows / $scope.quickSearchStockRemain.filters.pageSize);
                            grdStockRemain.updateLayout();
                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        event: {
            openScreen: function ($event) {
                $event.preventDefault();
                $scope.ui.currentForm = 'search-stock-remain';
                $scope.ui.title = 'Select material to make export receipt'
            },

            search: function ($event) {
                $event.preventDefault();
                $scope.quickSearchStockRemain.filters.pageIndex = 1;
                $scope.quickSearchStockRemain.method.search();
            },

            close: function ($event) {
                $event.preventDefault();
                $scope.method.setUI();
            },
            itemSelected: function ($event) {
                $event.preventDefault();

                angular.forEach($scope.quickSearchStockRemain.data, function (stock_remain_item) {
                    if (stock_remain_item.isSelected) {
                        var exportItem = {
                            factoryMaterialReceiptDetailID: $scope.method.getNewID(),
                            factoryOrderDetailID: stock_remain_item.factoryOrderDetailID,
                            factoryFinishedProductID: stock_remain_item.factoryFinishedProductID,
                            factoryMaterialID: stock_remain_item.factoryMaterialID,
                            factoryMaterialOrderNormID: stock_remain_item.factoryMaterialOrderNormID,
                            factoryAreaID: stock_remain_item.factoryAreaID,

                            factoryMaterialUD: stock_remain_item.factoryMaterialUD,
                            factoryMaterialNM: stock_remain_item.factoryMaterialNM,
                            factoryAreaNM: stock_remain_item.factoryAreaNM,
                            factoryUD: stock_remain_item.factoryUD,
                            lds: stock_remain_item.lds,
                            proformaInvoiceNo: stock_remain_item.proformaInvoiceNo,
                            clientUD: stock_remain_item.clientUD,
                            articleCode: stock_remain_item.articleCode,
                            description: stock_remain_item.description,
                            factoryFinishedProductUD: stock_remain_item.factoryFinishedProductUD,
                            factoryFinishedProductNM: stock_remain_item.factoryFinishedProductNM,

                            totalStockQnt: stock_remain_item.totalStockQnt,
                            unitNM: stock_remain_item.unitNM,
                            normValue: stock_remain_item.normValue,
                            orderQnt: stock_remain_item.orderQnt,

                            //parent item
                            factoryMaterialReceiptDetails : []
                        }

                        if ($scope.quickSearchStockRemain.exportParentItem == null) {
                            if ($scope.data.factoryMaterialReceiptDetails.filter(function (o) { return o.factoryOrderDetailID == stock_remain_item.factoryOrderDetailID && o.factoryFinishedProductID == stock_remain_item.factoryFinishedProductID && o.factoryMaterialID == stock_remain_item.factoryMaterialID && o.factoryMaterialOrderNormID == stock_remain_item.factoryMaterialOrderNormID && o.factoryAreaID == stock_remain_item.factoryAreaID }).length == 0) {
                                $scope.data.factoryMaterialReceiptDetails.push(exportItem);
                            }
                        }
                        else {
                            if ($scope.quickSearchStockRemain.exportParentItem.factoryOrderDetailID != exportItem.factoryOrderDetailID && $scope.quickSearchStockRemain.exportParentItem.factoryFinishedProductID != exportItem.factoryFinishedProductID && $scope.quickSearchStockRemain.exportParentItem.factoryMaterialOrderNormID != exportItem.factoryMaterialOrderNormID)
                            {
                                if ($scope.quickSearchStockRemain.exportParentItem.factoryMaterialReceiptDetails.filter(function (o) { return o.factoryOrderDetailID == stock_remain_item.factoryOrderDetailID && o.factoryFinishedProductID == stock_remain_item.factoryFinishedProductID && o.factoryMaterialID == stock_remain_item.factoryMaterialID && o.factoryMaterialOrderNormID == stock_remain_item.factoryMaterialOrderNormID && o.factoryAreaID == stock_remain_item.factoryAreaID }).length == 0) {
                                    exportItem.isFreeStock = false;
                                    $scope.quickSearchStockRemain.exportParentItem.factoryMaterialReceiptDetails.push(exportItem);
                                }
                            }                            
                        }
                    }
                });
                $scope.quickSearchStockRemain.event.close($event);
            }
        }
    }

    //search product from which source to export
    $scope.searchProductFrom = {        
        baseOnTypeID: 2,

        exportParentItem: null, // this varible is use in case user export item to a parent export item
        event: {
            openScreen: function ($event, exportParentItem) {
                $event.preventDefault();

                $scope.searchProductFrom.exportParentItem = exportParentItem;
                $scope.ui.currentForm = 'search-product-from';
                $scope.ui.title = 'Select material to make export receipt'
            },

            gotoNextStep: function ($event) {
                $event.preventDefault();
                if ($scope.searchProductFrom.baseOnTypeID == 1) {
                    $scope.searchStockFreeForm.exportParentItem = $scope.searchProductFrom.exportParentItem;
                    $scope.searchStockFreeForm.event.openScreen($event);
                }
                else if ($scope.searchProductFrom.baseOnTypeID == 2) {
                    $scope.quickSearchStockRemain.exportParentItem = $scope.searchProductFrom.exportParentItem;
                    $scope.quickSearchStockRemain.event.openScreen($event);
                }
            }
        }
    }


    //
    // init
    //
    $scope.event.load();
}]);