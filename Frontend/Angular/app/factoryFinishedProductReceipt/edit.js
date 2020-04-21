var grdComponentSearchResult = jQuery('#grdComponentSearchResult').scrollableTable(
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.pageIndex = currentPage;
            jsonService.searchFilter.pageIndex = currentPage;
            scope.event.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = 1;
            jsonService.searchFilter.sortedBy = sortedBy;
            jsonService.searchFilter.sortedDirection = sortedDirection;
            scope.event.search();
        });
    }
);

var grdComponentSearchResult_WithoutOrders = jQuery('#grdComponentSearchResult_WithoutOrders').scrollableTable(
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.searchComponentScreen_WithoutOrders.filters.pageIndex = currentPage;
            jsonService.searchFilter.pageIndex = currentPage;
            scope.searchComponentScreen_WithoutOrders.method.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.searchComponentScreen_WithoutOrders.filters.pageIndex = 1;
            jsonService.searchFilter.pageIndex = 1;
            jsonService.searchFilter.sortedBy = sortedBy;
            jsonService.searchFilter.sortedDirection = sortedDirection;
            scope.searchComponentScreen_WithoutOrders.method.search();
        });
    }
);


var grdReceiptDetail = jQuery('#grdReceiptDetail').scrollableTable(null,
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        datasource = scope.data.factoryFinishedProductReceiptDetails;
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
    }
);
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
    $scope.factorySteps = null;
    $scope.newID = -1;
    $scope.receiptTypes = [{ receiptTypeID: 1, receiptTypeName: 'Import' }, { receiptTypeID: 2, receiptTypeName: 'Export' }];
    $scope.baseOnTypes = [{ baseOnTypeID: 1, baseOnTypeName: 'Free, without orders' }, { baseOnTypeID: 2, baseOnTypeName: 'Base on orders' }]; // import export by order or free : 1 : Free, 2: Order
    $scope.inProgressOrBuyDirectlies = [{ inProgressOrBuyDirectlyID: 1, inProgressOrBuyDirectlyName: 'In production progress' }, { inProgressOrBuyDirectlyID: 2, inProgressOrBuyDirectlyName: 'Buy directly from supplier' }];

    $scope.selectedReceiptTypeID = 1;
    $scope.selectedBaseOnTypeID = 1;
    $scope.selectedInProgressOrBuyDirectlyID = 1;

    $scope.selectedTeamItem = null;
    $scope.selectedStepItem = null;
    $scope.selectdGoodsProcedureItem = null;

    $scope.ui = {
        screenName: 'receipt',
        title: 'General'
    }
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
                    $scope.factorySteps = data.data.factorySteps;
                    $scope.$apply();
                    jQuery('#content').show();
                    //
                    if (context.id == 0)
                    {
                        $scope.data.receiptTypeID = $scope.selectedReceiptTypeID;
                        $scope.data.productTypeID = $scope.productTypeID;

                        $scope.$apply();
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
                            $scope.method.refresh(data.data.factoryFinishedProductReceiptID);
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
            $scope.data.factoryFinishedProductReceiptDetails.splice(index, 1);
        },
        onNextButtonClick: function () {

            if ($scope.selectedReceiptTypeID == 1) {//import
                if ($scope.selectedInProgressOrBuyDirectlyID == 1) { // if in-progress we must check TeamID
                    if ($scope.selectedTeamItem == null) {
                        bootbox.alert({ message: 'Team is empty, you should select team', size: 'small' });
                        return;
                    }
                }
            }
            else if ($scope.selectedReceiptTypeID == 2) {//export
                if ($scope.selectedTeamItem == null) {
                    bootbox.alert({ message: 'Team is empty, you should select team', size: 'small' });
                    return;
                }
            }

            if ($scope.selectedStepItem == null) {
                bootbox.alert({ message: 'Step is empty, you should select step', size: 'small' });
                return;
            }

            if ($scope.selectdGoodsProcedureItem == null) {
                bootbox.alert({ message: 'Procedure is empty, you should select procedure', size: 'small' });
                return;
            }

            //receipt type
            $scope.data.receiptTypeID = $scope.selectedReceiptTypeID;
            $scope.data.receiptTypeText = $scope.receiptTypes.filter(function (o) { return o.receiptTypeID == $scope.selectedReceiptTypeID })[0].receiptTypeName;

            //base on type (base on free or order)
            $scope.data.baseOnTypeID = $scope.selectedBaseOnTypeID;
            $scope.data.baseOnTypeText = $scope.baseOnTypes.filter(function (o) { return o.baseOnTypeID == $scope.selectedBaseOnTypeID })[0].baseOnTypeName;

            //in production progress or buy directly from supplier
            $scope.data.inProgressOrBuyDirectlyID = $scope.selectedInProgressOrBuyDirectlyID;
            $scope.data.inProgressOrBuyDirectlyText = $scope.inProgressOrBuyDirectlies.filter(function (o) { return o.inProgressOrBuyDirectlyID == $scope.selectedInProgressOrBuyDirectlyID })[0].inProgressOrBuyDirectlyName;

            //team
            if ($scope.selectedReceiptTypeID == 1)
            {
                if ($scope.selectedInProgressOrBuyDirectlyID == 1) { // if in-progress we must check TeamID and assign TeamID
                    $scope.data.importFromTeamID = $scope.selectedTeamItem.factoryTeamID;
                    $scope.data.factoryTeamNM = $scope.selectedTeamItem.factoryTeamNM;
                }
            }
            else if ($scope.selectedReceiptTypeID == 2)
            {
                $scope.data.exportToTeamID = $scope.selectedTeamItem.factoryTeamID;
                $scope.data.factoryTeamNM = $scope.selectedTeamItem.factoryTeamNM;
            }
            //step
            $scope.data.factoryStepID = $scope.selectedStepItem.factoryStepID;
            $scope.data.factoryStepNM = $scope.selectedStepItem.factoryStepNM;

            //goods procedure
            $scope.data.factoryGoodsProcedureID = $scope.selectdGoodsProcedureItem.factoryGoodsProcedureID;
            $scope.data.factoryGoodsProcedureNM = $scope.selectdGoodsProcedureItem.factoryGoodsProcedureNM;

            jsHelper.hidePopup('receiptTypeForm', function () { });
        },
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
        }
    },


    //search component to impot/export (containt RAP Orders)
    $scope.searchComponentScreen = {
        data: null,
        filters: {
            filters: {
                factoryFinishedProductUD: '',
                factoryFinishedProductNM: '',
                articleCode: '',
                description: '',
                clientUD: '',
                proformaInvoiceNo: '',
            },
            sortedBy: 'factoryFinishedProductUD',
            sortedDirection: 'ASC',
            pageSize: 20,
            pageIndex: 1
        },
        totalPage: 0,

        method: {
            search: function () {

                if ($scope.data.receiptTypeID == 1) //Import Receipt
                {

                    if ($scope.data.inProgressOrBuyDirectlyID == 1) // In production progress (search product is in production progress)
                    {
                        /*
                        Find all components that exported to selected team at step before
                        */

                        $scope.searchComponentScreen.filters.filters["importFromTeamID"] = $scope.data.importFromTeamID;
                        $scope.searchComponentScreen.filters.filters["factoryStepID"] = $scope.data.factoryStepID;
                        $scope.searchComponentScreen.filters.filters["factoryGoodsProcedureID"] = $scope.data.factoryGoodsProcedureID;

                        // search component base on team (team was selected by user at init)
                        jsonService.searchComponentNeedToImport(
                            $scope.searchComponentScreen.filters,
                            function (data) {
                                if (data.message.type == 0) {
                                    $scope.searchComponentScreen.data = data.data.data;
                                    var totalRows = data.data.totalRows;

                                    $scope.searchComponentScreen.totalPage = Math.ceil(totalRows / $scope.searchComponentScreen.filters.pageSize);
                                    grdComponentSearchResult.updateLayout();
                                    $scope.$apply();
                                }
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error);
                            }
                        );
                    }
                    else if ($scope.data.inProgressOrBuyDirectlyID == 2) // Buy directly from supplier (search component without in production progress)
                    {
                        jsonService.searchComponentNeedToImportOrdersBuyDirectlies(
                            $scope.searchComponentScreen.filters,
                            function (data) {
                                if (data.message.type == 0) {
                                    $scope.searchComponentScreen.data = data.data.data;
                                    var totalRows = data.data.totalRows;

                                    $scope.searchComponentScreen.totalPage = Math.ceil(totalRows / $scope.searchComponentScreen.filters.pageSize);
                                    grdComponentSearchResult.updateLayout();
                                    $scope.$apply();
                                }
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error);
                            }
                        );
                    }                    
                }
                else if ($scope.data.receiptTypeID == 2) //Export Receipt
                {
                    /*
                        Find all components that next step is same as selected step (note that every team is specify by many step, so user need select one any step to export product
                        this is describled in table FactoryTeamStep,
                    */

                    //set selected step
                    $scope.searchComponentScreen.filters.filters["factoryStepID"] = $scope.data.factoryStepID;
                    $scope.searchComponentScreen.filters.filters["factoryGoodsProcedureID"] = $scope.data.factoryGoodsProcedureID;

                    //search component base on selected step (selected step is know when user select team)
                    jsonService.searchComponentNeedToExport(
                        $scope.searchComponentScreen.filters,
                        function (data) {
                            if (data.message.type == 0) {
                                $scope.searchComponentScreen.data = data.data.data;
                                var totalRows = data.data.totalRows;

                                $scope.searchComponentScreen.totalPage = Math.ceil(totalRows / $scope.searchComponentScreen.filters.pageSize);
                                grdComponentSearchResult.updateLayout();
                                $scope.$apply();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                }
            }
        },
        event: {
            openScreen: function ($event) {
                $event.preventDefault();
                $scope.ui.screenName = 'component-search-result';
                if ($scope.data.receiptTypeID == 1)
                {
                    $scope.ui.title = 'Select component to import'
                }
                else if ($scope.data.receiptTypeID == 2)
                {
                    $scope.ui.title = 'Select component to export'
                }                
            },
            search: function ($event) {
                $event.preventDefault();
                $scope.searchComponentScreen.filters.pageIndex = 1;
                $scope.searchComponentScreen.method.search();
            },
            close: function ($event) {
                $event.preventDefault();
                $scope.ui.screenName = 'receipt';
                $scope.ui.title = 'General'
            },
            itemSelected: function ($event) {
                $event.preventDefault();                
                angular.forEach($scope.searchComponentScreen.data, function (componentItem) {
                    if (componentItem.isSelected) {
                        if ($scope.data.factoryFinishedProductReceiptDetails.filter(function (o) { return o.factoryOrderDetailID == componentItem.factoryOrderDetailID && o.factoryFinishedProductID == componentItem.factoryFinishedProductID && o.factoryStepID == componentItem.factoryStepID && o.factoryStepPrevID == componentItem.factoryStepPrevID && o.factoryStepNextID == componentItem.factoryStepNextID && o.factoryFinishedProductOrderNormID == componentItem.factoryFinishedProductOrderNormID }).length == 0) {
                            $scope.data.factoryFinishedProductReceiptDetails.push({
                                factoryFinishedProductReceiptDetailID: $scope.method.getNewID(),

                                factoryOrderDetailID: componentItem.factoryOrderDetailID,
                                factoryFinishedProductOrderNormID: componentItem.factoryFinishedProductOrderNormID,
                                factoryFinishedProductID: componentItem.factoryFinishedProductID,
                                fromGoodsProcedureID: componentItem.fromGoodsProcedureID,

                                articleCode: componentItem.articleCode,
                                description: componentItem.description,
                                factoryUD: componentItem.factoryUD,
                                lds: componentItem.lds,
                                proformaInvoiceNo: componentItem.proformaInvoiceNo,
                                clientUD: componentItem.clientUD,
                                factoryFinishedProductUD: componentItem.factoryFinishedProductUD,
                                factoryFinishedProductNM: componentItem.factoryFinishedProductNM,
                                //quantity: componentItem.quantity,
                                unitPrice: componentItem.unitPrice,
                                stockQnt: componentItem.quantity,
                            });
                        }
                    }
                });
                $scope.searchComponentScreen.event.close($event);
            }
        }
    }

    //search component to impot/export (Free, without RAP Order)
    $scope.searchComponentScreen_WithoutOrders = {
        data: null,
        filters: {
            filters: {
                factoryFinishedProductUD: '',
                factoryFinishedProductNM: '',
                articleCode: '',
                description: '',
            },
            sortedBy: 'Description',
            sortedDirection: 'ASC',
            pageSize: 20,
            pageIndex: 1
        },
        totalPage: 0,

        method: {
            search: function () {
                if ($scope.data.receiptTypeID == 1) //Import Receipt
                {
                    if ($scope.data.inProgressOrBuyDirectlyID == 1) // In production progress (search product is in production progress)
                    {
                        /*
                        Find all components that exported to selected team at step before
                        */
                        $scope.searchComponentScreen_WithoutOrders.filters.filters["importFromTeamID"] = $scope.data.importFromTeamID;
                        $scope.searchComponentScreen_WithoutOrders.filters.filters["factoryStepID"] = $scope.data.factoryStepID;
                        $scope.searchComponentScreen_WithoutOrders.filters.filters["factoryGoodsProcedureID"] = $scope.data.factoryGoodsProcedureID;

                        // search component base on team (team was selected by user at init)
                        jsonService.searchComponentNeedToImportWithoutOrdesInProgress(
                            $scope.searchComponentScreen_WithoutOrders.filters,
                            function (data) {
                                if (data.message.type == 0) {
                                    $scope.searchComponentScreen_WithoutOrders.data = data.data.data;
                                    var totalRows = data.data.totalRows;

                                    $scope.searchComponentScreen_WithoutOrders.totalPage = Math.ceil(totalRows / $scope.searchComponentScreen_WithoutOrders.filters.pageSize);
                                    grdComponentSearchResult_WithoutOrders.updateLayout();
                                    $scope.$apply();
                                }
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error);
                            }
                        );
                    }
                    else if ($scope.data.inProgressOrBuyDirectlyID == 2) // Buy directly from supplier (search component without in production progress)
                    {
                        jsonService.searchComponentNeedToImportWithoutOrdesBuyDirectly(
                            $scope.searchComponentScreen_WithoutOrders.filters,
                            function (data) {
                                if (data.message.type == 0) {
                                    $scope.searchComponentScreen_WithoutOrders.data = data.data.data;
                                    var totalRows = data.data.totalRows;

                                    $scope.searchComponentScreen_WithoutOrders.totalPage = Math.ceil(totalRows / $scope.searchComponentScreen_WithoutOrders.filters.pageSize);
                                    grdComponentSearchResult_WithoutOrders.updateLayout();
                                    $scope.$apply();
                                }
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error);
                            }
                        );
                    }
                }
                else if ($scope.data.receiptTypeID == 2) //Export Receipt
                {
                    /*
                        Find all components that next step is same as selected step (note that every team is specify by many step, so user need select one any step to export product
                        this is describled in table FactoryTeamStep,
                    */

                    //set selected step
                    $scope.searchComponentScreen_WithoutOrders.filters.filters["factoryStepID"] = $scope.data.factoryStepID;
                    $scope.searchComponentScreen_WithoutOrders.filters.filters["factoryGoodsProcedureID"] = $scope.data.factoryGoodsProcedureID;

                    //search component base on selected step (selected step is know when user select team)
                    jsonService.searchComponentNeedToExportWithoutOrdesInProgress(
                        $scope.searchComponentScreen_WithoutOrders.filters,
                        function (data) {
                            if (data.message.type == 0) {
                                $scope.searchComponentScreen_WithoutOrders.data = data.data.data;
                                var totalRows = data.data.totalRows;

                                $scope.searchComponentScreen_WithoutOrders.totalPage = Math.ceil(totalRows / $scope.searchComponentScreen_WithoutOrders.filters.pageSize);
                                grdComponentSearchResult_WithoutOrders.updateLayout();
                                $scope.$apply();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                }
            }
        },
        event: {
            openScreen: function ($event) {
                $event.preventDefault();
                $scope.ui.screenName = 'component-search-result-without-orders';
                if ($scope.data.receiptTypeID == 1) {
                    $scope.ui.title = 'Select component to import'
                }
                else if ($scope.data.receiptTypeID == 2) {
                    $scope.ui.title = 'Select component to export'
                }
            },
            search: function ($event) {
                $event.preventDefault();
                $scope.searchComponentScreen_WithoutOrders.filters.pageIndex = 1;
                $scope.searchComponentScreen_WithoutOrders.method.search();
            },
            close: function ($event) {
                $event.preventDefault();
                $scope.ui.screenName = 'receipt';
                $scope.ui.title = 'General'
            },
            itemSelected: function ($event) {
                $event.preventDefault();
                angular.forEach($scope.searchComponentScreen_WithoutOrders.data, function (componentItem) {
                    if (componentItem.isSelected) {
                        if ($scope.data.factoryFinishedProductReceiptDetails.filter(function (o) { return o.factoryFinishedProductID == componentItem.factoryFinishedProductID && o.productID == componentItem.productID }).length == 0) {
                            $scope.data.factoryFinishedProductReceiptDetails.push({
                                factoryFinishedProductReceiptDetailID: $scope.method.getNewID(),
                                factoryFinishedProductID: componentItem.factoryFinishedProductID,
                                productID: componentItem.productID,
                                factoryMaterialNormID: componentItem.factoryMaterialNormID, // in case material transfer to produce at first step of progress we have to save this filed so we can tracking how many material was transfer to make compomnent at first step (ex ALU tranfer to make a FRAME, 100Kg ALU # 10 KHUNG)
                                fromGoodsProcedureID : componentItem.fromGoodsProcedureID, // this field is only effect when receipt is export. We can use it to tracking transfer goodsProcedure from this A to B

                                articleCode: componentItem.articleCode,
                                description: componentItem.description,
                                factoryFinishedProductUD: componentItem.factoryFinishedProductUD,
                                factoryFinishedProductNM: componentItem.factoryFinishedProductNM,
                                //
                                quantity: componentItem.quantity,
                            });
                        }
                    }
                });
                $scope.searchComponentScreen_WithoutOrders.event.close($event);
            }
        }
    }
    //
    // init
    //
    $scope.event.load();
}]);