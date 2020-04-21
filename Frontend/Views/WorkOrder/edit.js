var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'confirmDialogFactory', function ($scope, $timeout, confirmDialogFactory) {
    //
    // property
    //
    $scope.data = null;
    $scope.newID = -1;
    $scope.supportOPSequence = [];
    $scope.workOrderTypes = [];
    $scope.workOrderStatuses = [];
    $scope.canReset = (context.reset != 'disabled');
    $scope.filterWorkOrder = {
        workOrderUD: ''
    };
    $scope.listPreOrder = [];
    $scope.workOrderReportDTOs = [];
    $scope.workOrderReportHeaderDTOs = [];
    $scope.workOrderReportContentDTOs = [];
    $scope.itemNotBOMDTOs = [];
    $scope.workOrderType = {
        _MAKE_TO_ORDER: 1,
        _MAKE_TO_SAMPLE: 3,
        _PRE_ORDER: 4,
        _MAKE_TO_SPAREPART: 5,
    };
    $scope.workCenterToBlock = null;

    $scope.isEdit = true;
    $scope.isView = true;

    $scope.infoModel = '';

    $scope.opSequenceDetails = [];

    //$scope.gridHandler = {
    //    loadMore: function () {
    //        $scope.event.load();
    //    },
    //    sort: function (sortedBy, sortedDirection) {
    //        $scope.event.load();
    //    },
    //    isTriggered: false
    //};

    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
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
    $scope.nullHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.$apply(function () {
                $scope.orderByColumn = (sortedDirection === 'desc' ? '-' : '') + sortedBy;
            });
        },
        isTriggered: false
    };

    $scope.tabManagement = {
        preOrderWorkOrderManagements: [],
        workOrderBaseOnManangements: [],
        historyTransferPreOrders: [],
    };

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

        getTotalProduce: function () {
            var total = parseInt(0);
            if ($scope.data != null && $scope.data.workOrderDetailDTOs != null) {
                angular.forEach($scope.data.workOrderDetailDTOs, function (item) {
                    total += (item.quantity == null || item.quantity == '' ? parseInt(0) : parseInt(item.quantity));
                });
            }
            return total;
        },

        getWorkOrderStatusNM: function (id) {
            var name = '';
            angular.forEach($scope.workOrderStatuses, function (item) {
                if (item.workOrderStatusID == id) {
                    name = item.workOrderStatusNM;
                }
            });
            return angular.lowercase(name);
        },

        parseBOMDataToBOMList: function (bomData, result) {
         
            if (bomData != null && bomData.bomdtOs != null) {
                //push to list result
                result.push(bomData);

                //it just only assign piece index for child of root node
                if (bomData.parentBOMID == null) {
                    for (var i = 0; i < bomData.bomdtOs.length; i++) {
                        bomData.bomdtOs[i].pieceIndex = i + 1;
                    }
                }

                //assign number if child bom item for every child bom so we can sort to make sure code read from materil to component (material don't have chidl bom anymore and component can have child bom)
                angular.forEach(bomData.bomdtOs, function (item) {
                    item.countChildBOM = item.bomdtOs.length;
                    if (item.pieceIndex == undefined) {
                        item.pieceIndex = bomData.pieceIndex;
                    }
                });

                //sort base on number item of child bom
                var bomSorted = bomData.bomdtOs.sort(function (a, b) { return a.countChildBOM - b.countChildBOM });
                angular.forEach(bomSorted, function (item) {
                    $scope.method.parseBOMDataToBOMList(item, result);
                });
            }
        },

        clickChangeWorkOrder: function (workOrderID) {
            window.open(context.workOrderUrl + workOrderID);
        },

        isCheckedOneWorkCenter: function (workOrderOPSequences) {
            var isValidation = false;
            angular.forEach(workOrderOPSequences, function (item) {
                if (!item.isDisabled && item.isActived) {
                    isValidation = true;
                }
            });
            return isValidation;
        },

        isEnableWorkOrderSequence: function (opSequenceDetails) {
            var cntEnable = 0;

            angular.forEach(opSequenceDetails, function (item) {
                if (item.isDisabled) {
                    cntEnable += 1;
                }
            });

            return (cntEnable == opSequenceDetails.length);
        }
    };

    $scope.transferWorkOrder = {
        data: {
            transferWorkOrderID: $scope.method.getNewID(),
            workOrderID: null,
            preOrderWorkOrderID: null,
            workOrderUD: null,
            proformaInvoiceNo: null,
            lds: null,
            productionQuantity: null,
            transferPreOrderToWorkOrders: [],
        },
        event: {
            savePreOrder2WorkOrder: function (transfer, workOrderID, preOrderWorkOrderID) {
                jQuery('#errorComponent').html('');
                jQuery('#errorComponent').hide();

                // Comparision pre-order quantity and work-order quantity with transfer quantity.
                var errorComponents = null;
                angular.forEach(transfer.transferWorkOrderDetails, function (item) {
                    if (item.transferQuantity === 0 || item.preOrderWorkOrderQuantity < item.transferQuantity || item.workOrderQuantity < item.transferQuantity) {
                        if (errorComponents !== null) {
                            errorComponents += ', ';
                        }

                        errorComponents = '';
                        errorComponents += item.productionItemUD;
                    }
                });

                if (errorComponents !== null) {
                    jQuery('#errorComponent').html('Error data in component: ' + errorComponents);
                    jQuery('#errorComponent').css('display', 'block');
                    jQuery('#notificationMessage').hide();
                    return;
                }

                jsonService.updateTransferWorkOrder(
                    transfer.transferWorkOrderID,
                    transfer,
                    function (data) {
                        if (!data.data) {
                            jQuery('#errorComponent').html(data.message.message);
                            jQuery('#errorComponent').css('display', 'block');
                            jQuery('#notificationMessage').hide();

                            return;
                        }

                        jQuery('#frmTransferWO').modal('hide');

                        // Reload again
                        jQuery('#preOrderInformation').hide();
                        jQuery('#preOrderInformationLoading').show();

                        jQuery('#workOrderBasePreOrder').hide();
                        jQuery('#workOrderBasePreOrderLoading').show();

                        jQuery('#historyTransferWorkOrder').hide();
                        jQuery('#historyTransferWorkOrderLoading').show();

                        $scope.event.openTabManagement(workOrderID, preOrderWorkOrderID);
                    },
                    function (error) {
                    });
            },
            close: function (workOrderID, preOrderWorkOrderID) {
                jQuery('#frmTransferWO').modal('hide');

                jQuery('#preOrderInformation').hide();
                jQuery('#preOrderInformationLoading').show();

                jQuery('#workOrderBasePreOrder').hide();
                jQuery('#workOrderBasePreOrderLoading').show();

                jQuery('#historyTransferWorkOrder').hide();
                jQuery('#historyTransferWorkOrderLoading').show();

                $scope.event.openTabManagement(workOrderID, preOrderWorkOrderID);
            }
        },
    };
   
    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.getWorkOrderWithBranchID(
                context.id,
                context.branchID,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.supportOPSequence = data.data.supportOPSequence;
                    $scope.workOrderTypes = data.data.workOrderTypes;
                    $scope.workOrderStatuses = data.data.workOrderStatuses;
                    $scope.sampleProducts = data.data.sampleProductDTOs;
                    $scope.listPreOrder = data.data.workOrderListPreOrderDTOs;
                    $scope.workOrderReportDTOs = data.data.workOrderReportDTOs;                   
                    $scope.workOrderReportHeaderDTOs = data.data.workOrderReportHeaderDTOs;
                    $scope.workOrderReportContentDTOs = data.data.workOrderReportContentDTOs;
                    $scope.itemNotBOMDTOs = data.data.itemNotBOMDTOs;

                    if ($scope.data.workOrderID > 0) {
                        $scope.opSequenceDetails = data.data.opSequenceDetails;

                        angular.forEach($scope.opSequenceDetails, function (item) {
                            angular.forEach($scope.data.workOrderOPSequences, function (sItem) {
                                if (item.opSequenceDetailID === sItem.opSequenceDetailID) {
                                    item.isActived = sItem.isActived;
                                    item.isBlocked = sItem.isBlocked;
                                }
                            });
                        });
                    }
                  
                    //parse bom to bom list format
                    $scope.bomData = [];
                    $scope.method.parseBOMDataToBOMList(data.data.bomData, $scope.bomData);
                    angular.forEach($scope.itemNotBOMDTOs, function (item) {
                        var addItem = {
                            workOrderID: item.workOrderID,
                            productionItemID: item.productionItemID,
                            productionItemUD: item.productionItemUD,
                            productionItemNM: item.productionItemNM,
                            unitNM: item.unitNM,
                            workCenterID: item.workCenterID,
                            workCenterNM: item.workCenterNM,
                            totalDeliveryQnt: item.deliveryQty,
                            totalReceivedQnt: item.receivingQty,
                            branchID: null,
                            parentBOMID: 0,
                            productionItemTypeID: item.productionItemTypeID,
                            productionItemTypeNM: item.productionItemTypeNM
                        };
                        $scope.bomData.push(addItem);
                    });
                    

                    var isValidate = false;
                    for (var i = ($scope.bomData.length - 1); i >= 0; i--) {
                        var item = $scope.bomData[i];

                        if (item !== null) {
                            if (item.delta !== null && item.wastagePercent !== null && item.delta > item.wastagePercent) {
                                isValidate = true;
                            }
                        }

                        if (isValidate && item.productionItemTypeID === 1) {
                            $scope.workCenterToBlock = item.workCenterNM;
                            break;
                        }
                    }

                    angular.forEach($scope.opSequenceDetails, function (opsItem) {
                        if (opsItem.workCenterNM === $scope.workCenterToBlock) {
                            opsItem.isDisabledBlock = true;
                        } else {
                            opsItem.isDisabledBlock = false;
                        }
                    });
                   
                    if ($scope.data.modelUD != null) {
                        $scope.infoModel = $scope.data.modelUD + '(' + $scope.data.modelNM + ')';
                    }

                    //apply data
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                    $('#client').autocomplete({
                        source: function (request, response) {

                            $.ajax({
                                cache: false,
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json',
                                    'Authorization': 'Bearer ' + context.token
                                },
                                type: "POST",
                                data: JSON.stringify({
                                    filters: {
                                        searchQuery: request.term
                                    }
                                }),
                                dataType: 'json',
                                url: context.supportServiceUrl + 'getClient',
                                beforeSend: function () {

                                    jsHelper.loadingSwitch(true);
                                },
                                success: function (data) {

                                    jsHelper.loadingSwitch(false);

                                    response(data.data);
                                },
                                error: function (xhr, ajaxOptions, thrownError) {

                                    jsHelper.loadingSwitch(false);
                                    errorCallBack(xhr.responseJSON.exceptionMessage);
                                }
                            });
                        },
                        minLength: 3,
                        select: function (event, ui) {
                            $scope.data.clientID = ui.item.id;
                            $scope.data.clientUD = ui.item.label;

                            //reset saleorder & workOrderDetail
                            $scope.data.saleOrderID = null;
                            $scope.data.proformaInvoiceNo = '';
                            $scope.data.workOrderDetailDTOs = [];
                            $scope.$apply();
                        }
                    });

                    $('#product').autocomplete({
                        source: function (request, response) {

                            $.ajax({
                                cache: false,
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json',
                                    'Authorization': 'Bearer ' + context.token
                                },
                                type: "POST",
                                data: JSON.stringify({
                                    filters: {
                                        searchQuery: request.term
                                    }
                                }),
                                dataType: 'json',
                                url: jsonService.serviceUrl + 'getProduct',
                                beforeSend: function () {
                                    jsHelper.loadingSwitch(true);
                                },
                                success: function (data) {
                                    jsHelper.loadingSwitch(false);
                                    response(data.data);
                                },
                                error: function (xhr, ajaxOptions, thrownError) {
                                    jsHelper.loadingSwitch(false);
                                    errorCallBack(xhr.responseJSON.exceptionMessage);
                                }
                            });
                        },
                        minLength: 3,
                        select: function (event, ui) {
                            $scope.data.productID = ui.item.productID;
                            $scope.data.productArticleCode = ui.item.articleCode;
                            $scope.data.modelID = ui.item.modelID;
                            $scope.data.modelUD = ui.item.modelUD;
                            $scope.data.modelNM = ui.item.modelNM;
                            $scope.$apply();
                        }
                    });

                    $('#saleOrder').autocomplete({
                        source: function (request, response) {

                            $.ajax({
                                cache: false,
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json',
                                    'Authorization': 'Bearer ' + context.token
                                },
                                type: "POST",
                                data: JSON.stringify({
                                    filters: {
                                        searchQuery: request.term,
                                        clientID: $scope.data.clientID
                                    }
                                }),
                                dataType: 'json',
                                url: jsonService.serviceUrl + 'getSaleOrder',
                                beforeSend: function () {
                                    jsHelper.loadingSwitch(true);
                                },
                                success: function (data) {
                                    jsHelper.loadingSwitch(false);
                                    response(data.data);
                                },
                                error: function (xhr, ajaxOptions, thrownError) {
                                    jsHelper.loadingSwitch(false);
                                    errorCallBack(xhr.responseJSON.exceptionMessage);
                                }
                            });
                        },
                        minLength: 2,
                        select: function (event, ui) {
                            $scope.data.saleOrderID = ui.item.saleOrderID;
                            $scope.data.proformaInvoiceNo = ui.item.proformaInvoiceNo;
                            $scope.$apply();
                            debugger;
                            //reset  workOrderDetail
                            $scope.data.workOrderDetailDTOs = [];

                            //get factory order detail item
                            $scope.factoryOrderDetails = [];
                            var inputData = {
                                filters: {
                                    clientID: $scope.data.clientID,
                                    saleOrderID: $scope.data.saleOrderID,
                                    modelID: $scope.data.modelID,
                                }
                            }
                            jsonService.getFactoryOrderDetail(
                                inputData,
                                function (data) {
                                    $scope.factoryOrderDetails = data.data;
                                    $scope.data.workOrderDetailDTOs = [];
                                    $scope.$apply();
                                },
                                function (error) {
                                    jsHelper.showMessage('warning', error);
                                }
                            );
                        }
                    });

                    $('#sampleOrder').autocomplete({
                        source: function (request, response) {

                            $.ajax({
                                cache: false,
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json',
                                    'Authorization': 'Bearer ' + context.token
                                },
                                type: "POST",
                                data: JSON.stringify({
                                    searchQuery: request.term,
                                }),
                                dataType: 'json',
                                url: jsonService.serviceUrl + 'get-sample-order',
                                beforeSend: function () {
                                    jsHelper.loadingSwitch(true);
                                },
                                success: function (data) {
                                    jsHelper.loadingSwitch(false);
                                    response(data.data);
                                },
                                error: function (xhr, ajaxOptions, thrownError) {
                                    jsHelper.loadingSwitch(false);
                                    errorCallBack(xhr.responseJSON.exceptionMessage);
                                }
                            });
                        },
                        minLength: 2,
                        select: function (event, ui) {
                            $scope.data.sampleOrderID = ui.item.sampleOrderID;
                            $scope.sampleProducts = ui.item.sampleProductDTOs
                            $scope.$apply();
                        }
                    });

                    $('#pre-order-client').autocomplete({
                        source: function (request, response) {

                            $.ajax({
                                cache: false,
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json',
                                    'Authorization': 'Bearer ' + context.token
                                },
                                type: "POST",
                                data: JSON.stringify({
                                    filters: {
                                        searchQuery: request.term
                                    }
                                }),
                                dataType: 'json',
                                url: context.supportServiceUrl + 'getClient',
                                beforeSend: function () {

                                    jsHelper.loadingSwitch(true);
                                },
                                success: function (data) {

                                    jsHelper.loadingSwitch(false);

                                    response(data.data);
                                },
                                error: function (xhr, ajaxOptions, thrownError) {

                                    jsHelper.loadingSwitch(false);
                                    errorCallBack(xhr.responseJSON.exceptionMessage);
                                }
                            });
                        },
                        minLength: 3,
                        select: function (event, ui) {
                            $scope.data.clientID = ui.item.id;
                            $scope.data.clientUD = ui.item.label;
                            $scope.$apply();
                        }
                    });

                    //assing confirm dialog for combobx set status
                    confirmDialogFactory.setSelected('workOrderStatusID', $scope.data.workOrderStatusID);
                    $scope.$on('popupCloseEvent_workOrderStatusID', function (event, data) {
                        $scope.data.workOrderStatusID = data.selectedItem;
                        if (data.result) {
                            jsonService.setWorkOrderStatus(
                                context.id,
                                $scope.data.workOrderStatusID,
                                function (data) {
                                    jsHelper.processMessage(data.message);
                                    $scope.data.workOrderStatusID = data.data;
                                    $scope.$apply();
                                    if (data.message.type == 0 && data.data == 2) { //workOrderStatusID = 2 : Confimred
                                        $scope.method.refresh(context.id);
                                    }
                                },
                                function (error) {
                                    jsHelper.showMessage('warning', error);
                                });
                        }
                    });

                    //assing confirm dialog for combobx OP Sequence
                    confirmDialogFactory.setSelected('opSequenceID', $scope.data.opSequenceID);
                    $scope.$on('popupCloseEvent_opSequenceID', function (event, data) {
                        $scope.data.opSequenceID = data.selectedItem;
                        if (data.result) {

                            //declare param
                            var param = {
                                workOrderID: $scope.data.workOrderID,
                                opSequenceID: $scope.data.opSequenceID
                            }

                            //change status
                            jsonService.changeOPSequence(
                                param,
                                function (data) {
                                    if (data.message.type === 0) {
                                        jsHelper.processMessage(data.message);
                                        $scope.data.bomid = 0;
                                    }
                                    else {//error
                                    }
                                    $scope.$apply();
                                },
                                function (error) {
                                    jsHelper.showMessage('warning', error);
                                });
                        }
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

            // Pre-data sequence with WorkOrder
            $scope.data.workOrderOPSequences = [];
            angular.forEach($scope.opSequenceDetails, function (item) {
                if (!item.isDisabled && item.isActived) {
                    var workOrderOPSequence = {
                        workOrderOPSequenceID: $scope.method.getNewID(),
                        workOrderID: $scope.data.workOrderID,
                        opSequenceDetailID: item.opSequenceDetailID,
                        workCenterID: item.workCenterID,
                        workCenterUD: item.workCenterUD,
                        workCenterNM: item.workCenterNM,
                        isActived: item.isActived,
                        isBlocked: item.isBlocked,
                    };

                    $scope.data.workOrderOPSequences.push(workOrderOPSequence);
                }

                if ($scope.data.workOrderTypeID == 1) {
                    if (!item.isDisabled && !item.isActived) {
                        var workOrderOPSequence = {
                            workOrderOPSequenceID: $scope.method.getNewID(),
                            workOrderID: $scope.data.workOrderID,
                            opSequenceDetailID: item.opSequenceDetailID,
                            workCenterID: item.workCenterID,
                            workCenterUD: item.workCenterUD,
                            workCenterNM: item.workCenterNM,
                            isActived: item.isActived,
                            isBlocked: item.isBlocked,
                        };

                        $scope.data.workOrderOPSequences.push(workOrderOPSequence);
                    }
                }
            });

            // Checked OPSequence must be selected one more
            if (!$scope.method.isCheckedOneWorkCenter($scope.data.workOrderOPSequences)) {
                jsHelper.showMessage('warning', 'Please select one value in OPSequence(must be)');
                return;
            }

            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            $scope.method.refresh(data.data.workOrderID);
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

            if (confirm('Are you sure you want to delete work order ?')) {
                jsonService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.backUrl;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },

        setWorkOrderStatus: function (workOrderStatusID) {
            if (context.id > 0) {
                bootbox.confirm({
                    message: 'Are you sure you want to confirm',
                    buttons: {
                        confirm: {
                            label: 'Yes',
                            className: 'btn-success'
                        },
                        cancel: {
                            label: 'No',
                            className: 'btn-danger'
                        }
                    },
                    callback: function (result) {
                        if (result) {
                        }
                    }
                });
            }
        },

        selectFactoryOrderDetailItem: function (item) {
            $scope.data.workOrderDetailDTOs = [];
            angular.forEach($scope.factoryOrderDetails, function (fItem) {
                if (fItem.articleCode == item.articleCode) {
                    $scope.data.workOrderDetailDTOs.push({
                        workOrderDetailID: $scope.method.getNewID(),
                        factoryOrderDetailID: fItem.factoryOrderDetailID,
                        productID: fItem.productID,
                        articleCode: fItem.articleCode,
                        description: fItem.description,
                        orderQnt: fItem.orderQnt,
                        lds: fItem.lds,
                        factoryUD: fItem.factoryUD,
                        modelID: fItem.modelID,
                    });
                }
            });
            $scope.$apply
        },

        selectWorkOrderType: function () {
            if ($scope.data.workOrderTypeID === $scope.workOrderType._MAKE_TO_ORDER || $scope.data.workOrderTypeID === $scope.workOrderType._PRE_ORDER) {
                bootbox.confirm({
                    message: "Do you want to make from PRE-ORDER ?",
                    buttons: {
                        confirm: {
                            label: 'Yes',
                            className: 'btn-success'
                        },
                        cancel: {
                            label: 'No',
                            className: 'btn-danger'
                        }
                    },
                    callback: function (result) {
                        if (result) {
                            $scope.preOrderWorkOrderForm.load();
                        }
                    }
                });
            } else {
                $scope.data.clientID = null;
                $scope.data.saleOrderID = null;
                $scope.data.workOrderDetailDTOs = [];
                $scope.data.clientUD = '';
                $scope.data.proformaInvoiceNo = '';
                $scope.data.opSequenceDetailID = null;
            }
        },

        openWorkoderStatus: function () {
            bootbox.confirm({
                message: "Do you want set status to Confirm All ? ",
                buttons: {
                    confirm: {
                        label: 'Yes',
                        className: 'btn-success'
                    },
                    cancel: {
                        label: 'No',
                        className: 'btn-danger'
                    }
                },
                callback: function (result) {
                    if (result) {
                        jsonService.openWorkOrderStatus(
                            context.id,
                            function (data) {
                                jsHelper.processMessage(data.message);
                                $scope.event.load();
                            },
                            function (error) {
                            }
                        );
                    }
                }
            });
        },

        changeOPSequence: function (opSequenceID, bomID, preWorkOrderID) {
            if (bomID == null || bomID == 0) {
                jsonService.getOPSequenceDetail(
                    opSequenceID,
                    preWorkOrderID,
                    function (data) {
                        $scope.opSequenceDetails = data.data;

                        angular.forEach($scope.supportOPSequence, function (item) {
                            if (item.opSequenceID == opSequenceID) {
                                $scope.data.opSequenceNM = item.opSequenceNM;
                            }
                        });

                        if ($scope.data.workOrderTypeID == 1 && preWorkOrderID == null) {
                            // To-do
                            angular.forEach(
                                $scope.opSequenceDetails,
                                function (item) {
                                    if (!item.isDisabled) {
                                        item.isActived = true;
                                    }
                                });
                        }

                        jQuery('#frmOPSequenceDetail').modal();

                        $scope.$apply();
                    },
                    function (error) {
                    });
            } else {
            }
        },

        checkAllActive: function () {
            var isCountChecked = 0;
            angular.forEach($scope.opSequenceDetails, function (item) {
                if (item.isActived == null || !item.isActived && !item.isDisabled) {
                    item.isActived = true;
                    isCountChecked++;
                }

                if (isCountChecked == 0 && item.isActived && !item.isDisabled) {
                    item.isActived = false;
                }
            });
        },

        checkItemActive: function (item, workOrder) {
            if (item.isActived) {
                if (workOrder.workOrderTypeID == 1) {
                    for (var i = 0; i < $scope.opSequenceDetails.length; i++) {
                        var sItem = $scope.opSequenceDetails[i];

                        if (!sItem.isDisabled) {
                            sItem.isActived = true;
                        }
                    }
                } else if (workOrder.workOrderTypeID == 4) {
                    var isCheckedPrevious = false;

                    for (var i = $scope.opSequenceDetails.length - 1; i >= 0; i--) {
                        var sItem = $scope.opSequenceDetails[i];

                        if (sItem.isActived) {
                            isCheckedPrevious = true;
                            continue;
                        }

                        if (isCheckedPrevious) {
                            sItem.isActived = true;
                        }
                    }
                }
            } else {
                if (workOrder.workOrderTypeID == 1) {
                    var index = $scope.opSequenceDetails.indexOf(item);
                    if ((index + 1) == $scope.opSequenceDetails.length) {
                        for (var i = ($scope.opSequenceDetails.length - 1); i >= 0; i--) {
                            var sItem = $scope.opSequenceDetails[i];
                            if (!sItem.isDisabled) {
                                sItem.isActived = false;
                            }
                        }
                    }
                }
            }
        },

        saveWorkOrderOPSequence: function (workOrder, opSequenceDetails) {
            var param = {
                workOrderID: workOrder.workOrderID,
                opSequenceID: workOrder.opSequenceID
            };

            jsonService.changeOPSequence(
                param,
                function (data) {
                    workOrder.workOrderOPSequences = []; // Reset data in workOrderOPSequences
                    workOrder.bomid = 0;
                    angular.forEach(opSequenceDetails, function (item) {
                        if (item.isActived && !item.isDisabled) {
                            var workOrderOPSequence = {
                                workOrderOPSequenceID: $scope.method.getNewID(),
                                workOrderID: workOrder.workOrderID,
                                opSequenceDetailID: item.opSequenceDetailID,
                                workCenterID: item.workCenterID,
                                workCenterUD: item.workCenterUD,
                                workCenterNM: item.workCenterNM,
                                isActived: item.isActived
                            };

                            workOrder.workOrderOPSequences.push(workOrderOPSequence);
                        }
                    });

                    if (workOrder.workOrderTypeID == 1) { // Create work order
                        angular.forEach(opSequenceDetails, function (item) {
                            if (!item.isActived && !item.isDisabled) {
                                item.isActived = true;

                                var workOrderOPSequence = {
                                    workOrderOPSequenceID: $scope.method.getNewID(),
                                    workOrderID: workOrder.workOrderID,
                                    opSequenceDetailID: item.opSequenceDetailID,
                                    workCenterID: item.workCenterID,
                                    workCenterUD: item.workCenterUD,
                                    workCenterNM: item.workCenterNM,
                                    isActived: item.isActived
                                };

                                workOrder.workOrderOPSequences.push(workOrderOPSequence);
                            }
                        });
                    }

                    // Close pop-up select frmOPSequenceDetail
                    jQuery('#frmOPSequenceDetail').modal('hide');
                },
                function (error) {
                });
        },

        openWorkOrderOPSequence: function (workOrder, opSequenceDetails) {
            if (workOrder.workOrderOPSequences.length > 0) {
                angular.forEach(workOrder.workOrderOPSequences, function (item) {
                    angular.forEach(opSequenceDetails, function (sItem) {
                        if (item.opSequenceDetailID == sItem.opSequenceDetailID) {
                            sItem.isActived = item.isActived;
                        }
                    });
                });

                var isEnableAll = $scope.method.isEnableWorkOrderSequence($scope.opSequenceDetails);
                if (isEnableAll) {
                    angular.forEach($scope.opSequenceDetails, function (item) {
                        item.isDisabled = false;
                    });
                }
            } else {
                jsonService.getOPSequenceDetail(
                    workOrder.opSequenceID,
                    workOrder.preOrderWorkOrderID,
                    function (data) {
                        $scope.opSequenceDetails = data.data;

                        var isEnableAll = $scope.method.isEnableWorkOrderSequence($scope.opSequenceDetails);
                        if (isEnableAll) {
                            angular.forEach($scope.opSequenceDetails, function (item) {
                                item.isDisabled = false;
                            });
                        }

                        angular.forEach($scope.opSequenceDetails, function (item) {
                            item.isActived = true;
                        });

                        $scope.$apply();
                    },
                    function (error) {
                    });
            }

            jQuery('#frmOPSequenceDetail').modal();
        },

        openTransferWO: function (id, workOrderID, preOrderWorkOrderID, isEdit, isView) {
            jQuery('#errorComponent').html(null);
            jQuery('#errorComponent').css('display', 'none');

            jsonService.getTrasferWorkOrder(
                id,
                workOrderID,
                preOrderWorkOrderID,
                function (data) {
                    $scope.transferWorkOrder.data = data.data;

                    $scope.isEdit = isEdit;
                    $scope.isView = isView;

                    $scope.$apply();

                    jQuery('#frmTransferWO').modal();
                },
                function (error) {
                });
            //if (item !== null) {$scope.transferWorkOrder.data.workOrderID = item.workOrderID;
            //    $scope.transferWorkOrder.data.preOrderWorkOrderID = item.preOrderWorkOrderID;
            //    $scope.transferWorkOrder.data.workOrderUD = item.workOrderUD;
            //    $scope.transferWorkOrder.data.proformaInvoiceNo = item.proformaInvoiceNo;
            //    $scope.transferWorkOrder.data.lds = item.lds;
            //    $scope.transferWorkOrder.data.productionQuantity = item.productionQuantity;

            //    $scope.transferWorkOrder.data.transferPreOrderToWorkOrders = [];
            //    angular.forEach(item.preOrderWorkOrderBaseOnManagements, function (subItem) {
            //        if (subItem !== null) {
            //            var realItem = {};
            //            angular.copy(subItem, realItem);
            //            // clear transfer quantity
            //            realItem.transferQuantity = null;
            //            $scope.transferWorkOrder.data.transferPreOrderToWorkOrders.push(realItem);
            //        }
            //    });
            //}

            //jQuery('#frmTransferWO').modal();
        },

        openTabManagement: function (workOrderID, preOrderWorkOrderID) {
            // Get PreOrder WorkOrder
            jsonService.getPreOrderInformation(
                workOrderID,
                function (data) {
                    $scope.tabManagement.preOrderWorkOrderManagements = data.data;

                    jQuery('#preOrderInformationLoading').hide();
                    jQuery('#preOrderInformation').show();

                    $scope.$apply();
                },
                function (error) {
                });

            jsonService.getWorkOrderBaseOn(
                0,
                workOrderID,
                function (data) {
                    $scope.tabManagement.workOrderBaseOnManangements = data.data;

                    jQuery('#workOrderBasePreOrderLoading').hide();
                    jQuery('#workOrderBasePreOrder').show();

                    $scope.$apply();
                },
                function (error) {
                });

            jsonService.getHistoryTransferPreOrder(
                0,
                workOrderID,
                function (data) {
                    $scope.tabManagement.historyTransferPreOrders = data.data;

                    jQuery('#historyTransferWorkOrderLoading').hide();
                    jQuery('#historyTransferWorkOrder').show();

                    $scope.$apply();
                },
                function (error) {
                });
        },
        openMaterial: function (workOrder, opSequenceDetails) {
            if (workOrder.workOrderOPSequences.length > 0) {
                angular.forEach(workOrder.workOrderOPSequences, function (item) {
                    angular.forEach(opSequenceDetails, function (sItem) {
                        if (item.opSequenceDetailID == sItem.opSequenceDetailID) {
                            sItem.isActived = item.isActived;
                        }
                    });
                });

                var isEnableAll = $scope.method.isEnableWorkOrderSequence($scope.opSequenceDetails);
                if (isEnableAll) {
                    angular.forEach($scope.opSequenceDetails, function (item) {
                        item.isDisabled = false;
                    });
                }
            } else {
                jsonService.getOPSequenceDetail(
                    workOrder.opSequenceID,
                    workOrder.preOrderWorkOrderID,
                    function (data) {
                        $scope.opSequenceDetails = data.data;

                        var isEnableAll = $scope.method.isEnableWorkOrderSequence($scope.opSequenceDetails);
                        if (isEnableAll) {
                            angular.forEach($scope.opSequenceDetails, function (item) {
                                item.isDisabled = false;
                            });
                        }

                        angular.forEach($scope.opSequenceDetails, function (item) {
                            item.isActived = true;
                        });

                        $scope.$apply();
                    },
                    function (error) {
                    });
            }

            jQuery('#frmBlockMaterial').modal();
        },

    };

    //
    //select workorder which is 'PRE-ORDER' to make workOrder with type is 'MAKE TO ORDER'
    //
    $scope.preOrderWorkOrderForm = {
        data: [],
        load: function () {
         
            jsonService.getPreOrderWorkOrder(
                $scope.filterWorkOrder.workOrderUD,
                function (data) {
                    $scope.preOrderWorkOrderForm.data = data.data;
                    $scope.$apply();
                    $("#frmPreOrderWorkOrder").modal('show');
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        selectedWorkOrder: function (item) {
            $scope.data.workOrderDetailDTOs = [];
            $scope.data.preOrderWorkOrderID = item.workOrderID;
            $scope.data.opSequenceID = item.opSequenceID;
            $scope.data.modelID = item.modelID;

            $scope.data.opSequenceNM = item.opSequenceNM;
            $scope.data.modelUD = item.modelUD;
            $scope.data.modelNM = item.modelNM;

            if ($scope.data.modelUD != null) {
                $scope.infoModel = $scope.data.modelUD;
                if ($scope.data.modelNM !== null && $scope.data.modelNM !== '') {
                    $scope.infoModel += ' (' + $scope.data.modelNM + ')';
                }
            }

            $("#frmPreOrderWorkOrder").modal('hide');
        }
    }

    //
    // init
    //
    $scope.event.load();
}]);