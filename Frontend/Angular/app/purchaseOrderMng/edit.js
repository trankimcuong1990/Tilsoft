function formatData(data) {
    return $.map(data.data, function (item) {
        //console.log(item)
        return {
            id: item.id,
            label: item.value,
            description: item.label
        }
    });
}

(function () {
    'use strict';

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', purchaseOrderController);
    purchaseOrderController.$inject = ['$scope', 'dataService', '$timeout'];

    function purchaseOrderController($scope, purchaseOrderService, $timeout) {
        $scope.data = null;
        $scope.searchString = null;
        $scope.viewName = "init";
        $scope.factoryRawMaterials = [];
        $scope.isChecked = false;
        $scope.supplierPaymentTerms = [];
        $scope.supplierPaymentTermLists = [];

        $scope.initSubSupplierID = null;
        $scope.purchaseOrderDetail = [];
        $scope.seasons = [];

        $scope.filter = {
            productionItemUD: '',
            productionItemNM: ''        
        };

        //total value
        $scope.totalAmountValue = 0;

        $scope.totalpage = 0;
        $scope.totalrows = 0;
        $scope.pageindex = 1;

        $scope.supplierID = null;

        // variables for event quick-search
        $scope.ngInitPurchaseRequest = null;
        $scope.ngItemPurchaseRequest = {
            id: null,
            label: null,
            description: null
        };
        $scope.ngRequestPurchaseRequest = {
            url: context.serviceUrl + 'getPurchaseRequest',
            token: context.token,
        };
        $scope.ngSelectedPurchaseRequest = {
            id: null,
            label: null,
            description: null
        };

        $scope.spCurrency = [
            { sname: 'VND', name: 'VND' },
            { sname: 'USD', name: 'USD' }
        ];

        $scope.spcheckPercentVAT = [
            { id: 0, name: '0%' },
            { id: 5, name: '5%' },
            { id: 10, name: '10%' }

        ];

        $scope.gridHandler = {
            loadMore: function () {
            },
            sort: function (sortedBy, sortedDirection) {
            },
            isTriggered: false
        };

        $scope.event = {
            init: init,
            load: load,
            get: get,
            remove: remove,
            update: update,
            del: del,
            goNext: goNext,
            getPurchaseRequest: getPurchaseRequest,
            getList: getList,

            //file
            changeFile: changeFile,
            removeFile: removeFile,
            downloadFile: downloadFile,

            approve: approve,
            cancel: cancel,
            finish: finish,
            revise: revise,
            excelPurchaseOrderReport: excelPurchaseOrderReport,
            getPurchaseOrderPrintOut: getPurchaseOrderPrintOut,
            changePrice: changePrice,
            changETA: changETA,

            openViewDetail: function (purchaseOrderDetail) {
                $scope.purchaseOrderDetail = purchaseOrderDetail;
                jQuery("#frmWorkOrderDetailView").modal();
            },

            changeRequestQntByWorkOrder: function (item) {
                var s = parseFloat(0);
                angular.forEach(item.purchaseOrderWorkOrderDetailDTOs, function (wItem) {
                    s += (wItem.orderQnt === null ? 0 : parseFloat(wItem.orderQnt));
                });             
                if (s <= item.orderQnt) {
                    jQuery("#frmWorkOrderDetailView").modal("hide");
                }
                else {
                    bootbox.alert("Total WorkOrderQnt must be less than or equal to OrderQnt");
                }
                //item.orderQnt = s;
                //$scope.method.changeReceivingPlanQnt(item);
            },

            changeQntPurchaseOrderDetail: function (item) {               
                $scope.method.changeReceivingPlanQnt(item);
                $scope.method.changePuchaseOrderWorkOrderQnt(item);
                
            },

            changeOrderQnt: function (purchaseOrderDetail) {
                var s = parseFloat(0);
                angular.forEach(purchaseOrderDetail.purchaseOrderDetailReceivingPlanDtos, function (wItem) {
                    s += (wItem.plannedReceivingQnt === null ? 0 : parseFloat(wItem.plannedReceivingQnt));
                });

                if (s !== parseFloat(purchaseOrderDetail.orderQnt)) {
                    bootbox.alert("Total Quantity of Receving Plan must be equal to Order Quantity");
                }
                else {
                    for (var i = 0; i < purchaseOrderDetail.purchaseOrderDetailReceivingPlanDtos.length; i++) {
                        var z = purchaseOrderDetail.purchaseOrderDetailReceivingPlanDtos[i];
                        if (z.plannedETA === null || z.plannedETA === '') {
                            bootbox.alert("You have to input date for column Plan ETA");
                            return false;
                        }
                    }
                    jQuery("#frmReceivingPlanView").modal("hide");
                }
            },  
            

        };

        $scope.method = {
            isChecking: isChecking,
            getFactoryRawMaterialOfficialNM: getFactoryRawMaterialOfficialNM,
            getFactoryRawMaterialUD: getFactoryRawMaterialUD,
            addValuePurchaseOrderDetail: addValuePurchaseOrderDetail,
            changePuchaseOrderWorkOrderQnt: function (item) {
                var inputQnt = item.orderQnt;
                for (var i = 0; i < item.purchaseOrderWorkOrderDetailDTOs.length; i++) {
                    var wItem = item.purchaseOrderWorkOrderDetailDTOs[i];
                    if (inputQnt <= wItem.totalNorm) {
                        wItem.orderQnt = inputQnt;
                        inputQnt = inputQnt - wItem.orderQnt;
                    }
                    else {
                        wItem.orderQnt = wItem.totalNorm;
                        inputQnt = inputQnt - wItem.totalNorm;
                    }
                }    
            },

            changeReceivingPlanQnt(item) {
                for (var i = 0; i < item.purchaseOrderDetailReceivingPlanDtos.length; i++) {
                    var receivingPlan = item.purchaseOrderDetailReceivingPlanDtos[i];
                    //if (receivingPlan.purchaseOrderDetailID == item.purchaseOrderDetailID) {
                    //    item.purchaseOrderDetailReceivingPlanDtos[i].plannedReceivingQnt = item.orderQnt;
                    //}
                    if (i === 0) {
                        item.purchaseOrderDetailReceivingPlanDtos[i].plannedReceivingQnt = item.orderQnt;
                    }
                    else {
                        item.purchaseOrderDetailReceivingPlanDtos[i].plannedReceivingQnt = 0;
                    }
                }
            },
            totalAmountPO: function () {
                var total = 0;
                debugger;
                if ($scope.data!=null) {
                    for (var i = 0; i < $scope.data.purchaseOrderDetailDTOs.length; i++) {
                        var item = $scope.data.purchaseOrderDetailDTOs[i];
                        total = total + (item.unitPrice * item.orderQnt);
                    }
                    return total;
                }
            }
        };

        //
        //implement function for event
        //
        function init() {
            purchaseOrderService.serviceUrl = context.serviceUrl;
            purchaseOrderService.supportServiceUrl = context.supportServiceUrl;
            purchaseOrderService.token = context.token;
        };

        function load() {
            purchaseOrderService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;

                    /// fixed error season created purchase order
                    if (context.id == 0) {
                        $scope.data.season = jsHelper.getCurrentSeason();
                    }

                    $scope.supplierPaymentTerms = data.data.supplierPaymentTermDtos;
                    
                    //call init form incase create
                    if (context.id === 0) {
                        purchaseOrderService.getInit(
                            function (data) {
                                $scope.factoryRawMaterials = data.data.subSupplier;
                                $scope.seasons = data.data.seasons;
                                $scope.viewName = "init";
                                jQuery('#content').show();
                            },
                            function (error) {
                                // do nothing
                            });
                    }
                    else {
                        purchaseOrderService.getInit(
                            function (data) {
                                $scope.seasons = data.data.seasons;
                            },
                            function (error) {
                                // do nothing
                            });
                        $scope.viewName = "edit";  
                        $scope.supplierPaymentTermLists = $scope.supplierPaymentTerms;
                        jQuery('#content').show();
                    }
                },
                function (error) {
                    // do nothing
                });
        };

        function get() {
        };

        function remove($event, item) {
            //$event.preventDefault();
            var confirmedMsg = 'Delete ' + item.productionItemUD + ' ?'
            if (confirm(confirmedMsg) === true) {
                var index = $scope.data.purchaseOrderDetailDTOs.indexOf(item);
                $scope.data.purchaseOrderDetailDTOs.splice(index, 1);
                $scope.totalRowsScan = $scope.data.purchaseOrderDetailDTOs.length;
            }

        };

        function update() {
            if (jQuery('#purchaseOrderDate').val() === null || jQuery('#purchaseOrderDate').val() === '') {
                jsHelper.showMessage('warning', 'Please input Purchase Order Date.');
                return false;
            }

            if (jQuery('#eta').val() === null || jQuery('#eta').val() === '') {
                jsHelper.showMessage('warning', 'Please input ETA.');
                return false;
            }

            if ((jQuery('#paymentTerm').val() === null || jQuery('#paymentTerm').val() === '') && $scope.data.purchaseOrderStatusID === 1) {
                jsHelper.showMessage('warning', 'Please input Payment Period.');
                return false;
            }

            /// get value #eta.
            $scope.data.eta = jQuery('#eta').val();
           
            for (var i = 0; i < $scope.data.purchaseOrderDetailDTOs.length; i++) {
                var item = $scope.data.purchaseOrderDetailDTOs[i];
                if (item.eta === null || item.eta === '') {
                    jsHelper.showMessage('warning', 'Please input ETA for product ' + item.productionItemNM);
                    return;
                } 
            }

            if ($scope.editForm.$valid) {
                // set PurchaseRequestID for data to insert.
                if ($scope.ngItemPurchaseRequest !== null) {
                    if ($scope.ngItemPurchaseRequest.id !== undefined && $scope.ngItemPurchaseRequest.id !== null) {
                        $scope.data.purchaseRequestID = $scope.ngItemPurchaseRequest.id;
                    } else {
                        $scope.data.purchaseRequestID = $scope.data.purchaseRequestID;
                    }
                }

                purchaseOrderService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.purchaseOrderID;
                            $scope.data = data.data;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            } else {
                jsHelper.showMessage('warning', context.msgValid);
            }
        };

        function del(id) {
            purchaseOrderService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function goNext($event, factoryRawMaterialID) {
            $scope.viewName = "edit";
            $scope.data.factoryRawMaterialUD = scope.method.getFactoryRawMaterialUD($scope.data.factoryRawMaterialID);  
            $scope.data.factoryRawMaterialShortNM = $scope.method.getFactoryRawMaterialOfficialNM($scope.data.factoryRawMaterialID);  
            $scope.event.getList();
        };

        function getPurchaseRequest(itemPurchaseRequest, factoryRawMaterialID, purchaseOrderDate) {
            if (itemPurchaseRequest !== undefined && itemPurchaseRequest !== null) {
                //assing purchase request
                $scope.data.purchaseRequestID = itemPurchaseRequest.id;

                //get purchase request detail by purchase request
                purchaseOrderService.getPurchaseOrderDetailByPurchaseRequestID(
                    itemPurchaseRequest.id,
                    factoryRawMaterialID,
                    purchaseOrderDate,
                    function (data) {
                        var purchaseOrderDetailID = -1;
                        var purchaseOrderWorkOrderDetailID = -1;

                        for (var i = 0; i < data.data.length; i++) {
                            var purchaseOrderDetail = data.data[i];

                            $scope.method.addValuePurchaseOrderDetail(purchaseOrderDetail, purchaseOrderDetailID, purchaseOrderWorkOrderDetailID);
                        }

                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
        };

        //file functions   
        function changeFile() {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.friendlyName = fileUploader2.selectedFriendlyName;
                    $scope.data.fileLocation = fileUploader2.selectedFileUrl;
                    $scope.data.file_NewFile = fileUploader2.selectedFileName;
                    $scope.data.file_HasChange = true;
                });
            };
            fileUploader2.open();
        }

        function removeFile() {
            $scope.data.friendlyName = '';
            $scope.data.fileLocation = '';
            $scope.data.file_NewFile = '';
            $scope.data.file_HasChange = true;
        }

        function downloadFile() {
            if ($scope.data.fileLocation) {
                window.open($scope.data.fileLocation);
            }
        }

        function approve() {
            if (confirm('Are you sure ?')) {
                purchaseOrderService.approve(
                    context.id,
                    $scope.data,
                    function (data) {
                        window.location = context.refreshUrl + data.data.purchaseOrderID;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
        }

        function cancel() {
            if (confirm('Are you sure Cancel ?')) {
                purchaseOrderService.cancel(
                    context.id,
                    $scope.data,
                    function (data) {
                        if (data.message.type === 2) {
                            jsHelper.showMessage('warning', data.message.message);
                        } else {
                            window.location = context.refreshUrl + data.data.purchaseOrderID;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
        };

        function finish() {
            if (confirm('Are you sure Finish ?')) {
                purchaseOrderService.finish(
                    context.id,
                    $scope.data,
                    function (data) { 
                        if (data.message.type === 2) {
                            jsHelper.showMessage('warning', data.message.message);
                        } else {
                            window.location = context.refreshUrl + data.data.purchaseOrderID;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
        };

        function revise() {
            if (confirm('Are you sure Revise ?')) {
                purchaseOrderService.revise(
                    context.id,
                    $scope.data,
                    function (data) {
                        if (data.message.type === 2) {
                            jsHelper.showMessage('warning', data.message.message);
                        } else {
                            window.location = context.refreshUrl + data.data.purchaseOrderID;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
        };

        function excelPurchaseOrderReport(purchaseOrderID) {
            purchaseOrderService.excelPurchaseOrderReport(
                purchaseOrderID,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type !== 2) {
                        window.location = context.backendReportUrl + data.data;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function getPurchaseOrderPrintOut(purchaseOrderID) {
            purchaseOrderService.getPurchaseOrderPrintout(
                purchaseOrderID,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type !== 2) {
                        window.open(context.backendReportUrl + data.data, '');
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function changePrice(item) {
            if (item.oldPrice !== item.unitPrice) {
                item.isChangePrice = true;
            }
        }

        function changETA(item) {
            for (var i = 0; i < item.purchaseOrderDetailReceivingPlanDtos.length; i++) {
                var x = item.purchaseOrderDetailReceivingPlanDtos[i];
                if (x.purchaseOrderDetailID === item.purchaseOrderDetailID) {
                    item.purchaseOrderDetailReceivingPlanDtos[i].plannedETA = item.eta;
                }
            }
        }

        //
        //implement function for method
        //
        function isChecking(item) {
            for (var i = 0; i < $scope.data.purchaseOrderDetailDTOs.length; i++) {
                var param = $scope.data.purchaseOrderDetailDTOs[i];
                if (param.productionItemID === item.productionItemID) {
                    $scope.isChecked = true;
                    break;
                }
            }
        }

        function isExistWO(woList, item) {
            var rs = false;

            for (var j = 0; j < woList.length; j++) {
                var woItem = woList[j];

                if (woItem.workOrderID === item.workOrderID) {
                    rs = true;
                    return rs;
                }
            }

            return rs;
        }

        function getFactoryRawMaterialOfficialNM(factoryRawMaterialID) {
            var result = '';
            if ($scope.factoryRawMaterials === null) return ''
            angular.forEach($scope.factoryRawMaterials, function (item) {
                if (item.factoryRawMaterialID === factoryRawMaterialID) {
                    result = item.factoryRawMaterialShortNM;
                }
            });
            return result;
        }

        function getFactoryRawMaterialUD(factoryRawMaterialID) {
            var result = '';
            if ($scope.factoryRawMaterials === null) return ''
            angular.forEach($scope.factoryRawMaterials, function (item) {
                if (item.factoryRawMaterialID === factoryRawMaterialID) {
                    result = item.factoryRawMaterialUD;
                }
            });
            return result;
        }

        function addValuePurchaseOrderDetail(purchaseOrderDetail, purchaseOrderDetailID, purchaseOrderWorkOrderDetailID) {
            if ($scope.data.purchaseOrderDetailDTOs.length === 0) {
                // default add one row value into purchase order detail.
                $scope.data.purchaseOrderDetailDTOs.push({
                    purchaseOrderDetailID: purchaseOrderDetailID,
                    productionItemID: purchaseOrderDetail.productionItemID,
                    productionItemUD: purchaseOrderDetail.productionItemUD,
                    productionItemNM: purchaseOrderDetail.productionItemNM,
                    purchaseRequestDetailID: purchaseOrderDetail.purchaseRequestDetailID,
                    requestQnt: purchaseOrderDetail.requestQnt,
                    unit: purchaseOrderDetail.unit,
                    unitPrice: purchaseOrderDetail.unitPrice,
                    orderQnt: purchaseOrderDetail.orderQnt,
                    stockQnt: purchaseOrderDetail.stockQnt,
                    remark: null,
                    eta: $scope.data.eta,
                    purchaseOrderWorkOrderDetailDTOs: [],
                    purchaseOrderDetailReceivingPlanDtos: [],
                });

                // default add all rows value into purchase order work order detail.
                for (var j = 0; j < purchaseOrderDetail.purchaseRequestWorkOrderDetails.length; j++) {
                    var purchaseRequestWorkOrderDetailApproval = purchaseOrderDetail.purchaseRequestWorkOrderDetails[j];

                    $scope.data.purchaseOrderDetailDTOs[0].purchaseOrderWorkOrderDetailDTOs.push({
                        purchaseOrderWorkOrderDetailID: purchaseOrderWorkOrderDetailID,
                        workOrderID: purchaseRequestWorkOrderDetailApproval.workOrderID,
                        workOrderUD: purchaseRequestWorkOrderDetailApproval.workOrderUD,
                        requestedQnt: purchaseRequestWorkOrderDetailApproval.requestedQnt,
                        orderQnt: '',
                        totalNorm: purchaseRequestWorkOrderDetailApproval.totalNorm,
                        finishDate: purchaseRequestWorkOrderDetailApproval.finishDate
                    });
                    purchaseOrderWorkOrderDetailID = purchaseOrderWorkOrderDetailID - 1;                   
                }
                $scope.data.purchaseOrderDetailDTOs[0].purchaseOrderDetailReceivingPlanDtos.push({
                    purchaseOrderDetailReceivingPlanID: purchaseOrderService.getIncrementId(),
                    purchaseOrderDetailID: purchaseOrderDetailID,
                    plannedETA: $scope.data.eta,
                    plannedReceivingQnt: purchaseOrderDetail.orderQnt,
                    remark: ''
                });
                $scope.method.changePuchaseOrderWorkOrderQnt($scope.data.purchaseOrderDetailDTOs[0]);
                return;
            }

            // check purchase order detail exist or not exist.
            if (!existPurchaseOrderDetails(purchaseOrderDetail)) {
                // add purchase order detail.
                $scope.data.purchaseOrderDetailDTOs.push({
                    purchaseOrderDetailID: purchaseOrderDetailID,
                    productionItemID: purchaseOrderDetail.productionItemID,
                    productionItemUD: purchaseOrderDetail.productionItemUD,
                    productionItemNM: purchaseOrderDetail.productionItemNM,
                    purchaseRequestDetailID: purchaseOrderDetail.purchaseRequestDetailID,
                    requestQnt: purchaseOrderDetail.requestQnt,
                    unit: purchaseOrderDetail.unit,
                    unitPrice: purchaseOrderDetail.unitPrice,
                    orderQnt: purchaseOrderDetail.orderQnt,
                    stockQnt: purchaseOrderDetail.stockQnt,
                    remark: null,
                    eta: $scope.data.eta,
                    purchaseOrderWorkOrderDetailDTOs: [],
                    purchaseOrderDetailReceivingPlanDtos: [],
                });

                purchaseOrderWorkOrderDetailID = -1;
            }
            // loop purchase order detail after push.
            for (var i = 0; i < $scope.data.purchaseOrderDetailDTOs.length; i++) {
                var purchaseOrderDetailM = $scope.data.purchaseOrderDetailDTOs[i];

                if (purchaseOrderDetailM.productionItemID === purchaseOrderDetail.productionItemID) {
                    for (var j = 0; j < purchaseOrderDetail.purchaseRequestWorkOrderDetails.length; j++) {
                        var purchaseRequestWorkOrderDetail = purchaseOrderDetail.purchaseRequestWorkOrderDetails[j];

                        if (!existPurchaseOrderWorkOrderDetails(purchaseRequestWorkOrderDetail, purchaseOrderDetailM.purchaseOrderWorkOrderDetailDTOs)) {
                            // add purchase order work order detail.
                            $scope.data.purchaseOrderDetailDTOs[i].purchaseOrderWorkOrderDetailDTOs.push({
                                purchaseOrderWorkOrderDetailID: purchaseOrderWorkOrderDetailID,
                                workOrderID: purchaseRequestWorkOrderDetail.workOrderID,
                                workOrderUD: purchaseRequestWorkOrderDetail.workOrderUD,
                                requestedQnt: purchaseRequestWorkOrderDetail.requestedQnt,
                                orderQnt: '',
                                totalNorm: purchaseRequestWorkOrderDetail.totalNorm,
                                finishDate: purchaseRequestWorkOrderDetail.finishDate
                            });                           
                            purchaseOrderWorkOrderDetailID--;
                        }
                    }
                    $scope.method.changePuchaseOrderWorkOrderQnt(purchaseOrderDetailM);
                    purchaseOrderDetailM.purchaseOrderDetailReceivingPlanDtos.push({
                        purchaseOrderDetailReceivingPlanID: purchaseOrderService.getIncrementId(),
                        purchaseOrderDetailID: purchaseOrderDetailM.purchaseOrderDetailID,
                        plannedETA: purchaseOrderDetailM.eta,
                        plannedReceivingQnt: purchaseOrderDetailM.orderQnt,
                        remark: ''
                    });
                }            
            }

        };

        function existPurchaseOrderDetails(item) {
            for (var i = 0; i < $scope.data.purchaseOrderDetailDTOs.length; i++) {
                if ($scope.data.purchaseOrderDetailDTOs[i].productionItemID === item.productionItemID) {
                    return true;
                }
            }

            return false;
        };

        function existPurchaseOrderWorkOrderDetails(item, list) {
            for (var i = 0; i < list.length; i++) {
                if (list[i].workOrderID === item.workOrderID) {
                    return true;
                }
            }

            return false;
        };

        //custom 
        function getList() {
            angular.forEach($scope.supplierPaymentTerms, function (item) {
                if ($scope.data.factoryRawMaterialID === item.factoryRawMaterialID) {
                    var arr = {
                        supplierPaymentTermID: item.supplierPaymentTermID,
                        supplierPaymentTermNM: item.supplierPaymentTermNM,
                        factoryRawMaterialID: item.factoryRawMaterialID
                    };
                    $scope.supplierPaymentTermLists.push(arr);
                }
            });
        }
        //
        //add item form
        //
        $scope.addItemForm = {
            purchaseRequestID: null,
            factoryRawMaterialID: null,
            typePQAndPR: null,
            data: [],
            loadApprovePrice: function () {
                if ($scope.data.eta === null || $scope.data.eta === '') {
                    bootbox.alert("You have to fill-in eta before add item");
                    return;
                }

                //inject init value form
                $scope.addItemForm.purchaseRequestID = $scope.data.purchaseRequestID;
                $scope.addItemForm.factoryRawMaterialID = $scope.data.factoryRawMaterialID;

                //get data from purchase request
                purchaseOrderService.getPurchaseOrderDetailByPurchaseRequestID(
                    $scope.addItemForm.purchaseRequestID,
                    $scope.addItemForm.factoryRawMaterialID,
                    function (data) {
                        $scope.addItemForm.data = data.data;
                        $scope.addItemForm.typePQAndPR = data.data.typePQAndPR;
                        jQuery("#purchaseOrderDetailForm").modal();
                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            },

            loadDefaultPrice: function (purchaseOrderDate) {
                if ($scope.data.eta === null || $scope.data.eta === '') {
                    bootbox.alert("You have to fill-in eta before add item");
                    return;
                }

                //inject init value form
                $scope.addItemForm.factoryRawMaterialID = $scope.data.factoryRawMaterialID;

                //get data from purchase request
                purchaseOrderService.getPurchaseQuotationDetail(
                    $scope.addItemForm.factoryRawMaterialID,
                    purchaseOrderDate,
                    function (data) {
                        debugger;
                        $scope.addItemForm.data = data.data.purchaseQuotationAndSupplierDtos;
                        $scope.addItemForm.typePQAndPR = data.data.typePQAndPR;

                        jQuery("#purchaseOrderDetailForm").modal();
                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            },

            selectedApprovedItem: function () {
                debugger;
                angular.forEach($scope.addItemForm.data, function (item) {
                    if (item.checkVal) {
                        var addingItem = {
                            //tobe saved info
                            purchaseOrderDetailID: purchaseOrderService.getIncrementId(),
                            purchaseRequestDetailID: item.purchaseRequestDetailID,
                            productionItemID: item.productionItemID,
                            purchaseQuotationDetailID: null,
                            orderQnt: item.orderQnt,
                            unitPrice: item.unitPrice,
                            eta: $scope.data.eta,

                            //sub info
                            requestQnt: item.requestQnt,
                            productionItemUD: item.productionItemUD,
                            productionItemNM: item.productionItemNM,
                            unitNM: item.unitNM,
                            stockQnt: item.stockQnt,
                            typeName: item.typeName,
                            friendlyName: item.friendlyName,
                            fileLocation: item.fileLocation,
                            factoryRawMaterialID: item.factoryRawMaterialID,
                            purchaseOrderDetailReceivingPlanDtos: [],
                            purchaseOrderWorkOrderDetailDTOs: [],
                        }
                        addingItem.purchaseOrderDetailReceivingPlanDtos.push({
                            purchaseOrderDetailReceivingPlanID: purchaseOrderService.getIncrementId(),
                            purchaseOrderDetailID: addingItem.purchaseOrderDetailID,
                            plannedETA: addingItem.eta,
                            plannedReceivingQnt: item.orderQnt,
                            remark: ''
                        });
                        angular.forEach(item.purchaseRequestWorkOrderDetails, function (wItem) {
                            addingItem.purchaseOrderWorkOrderDetailDTOs.push({
                                purchaseOrderWorkOrderDetailID: purchaseOrderService.getIncrementId(),
                                workOrderID: wItem.workOrderID,
                                requestedQnt: wItem.requestedQnt,
                                workOrderUD: wItem.workOrderUD,
                                totalNorm: wItem.totalNorm,
                                orderQnt: '',
                                finishDate: wItem.finishDate
                            });
                        })

                        //check item is exist and add to dto detail
                        var x = $scope.data.purchaseOrderDetailDTOs.filter(o => o.productionItemID === item.productionItemID);
                        if (x === null || x.length === 0) {
                            $scope.data.purchaseOrderDetailDTOs.push(addingItem);
                            $scope.method.changePuchaseOrderWorkOrderQnt(addingItem);
                        }
                        else {
                            bootbox.alert("This item is existed in system");
                        }

                    }
                });

               

            },

            selectedDefaultItem: function () {
                angular.forEach($scope.addItemForm.data, function (item) {
                    if (item.checkVal) {
                        var addingItem = {
                            //tobe saved info
                            purchaseOrderDetailID: purchaseOrderService.getIncrementId(),
                            purchaseRequestDetailID: null,
                            productionItemID: item.productionItemID,
                            purchaseQuotationDetailID: item.purchaseQuotationDetailID,
                            materialHistoryID: item.materialHistoryID,
                            materialsPriceID: item.materialsPriceID,
                            orderQnt: item.orderQnt,
                            unitPrice: item.unitPrice,
                            eta: $scope.data.eta,

                            //sub info
                            productionItemUD: item.productionItemUD,
                            productionItemNM: item.productionItemNM,
                            unitNM: item.unitNM,
                            stockQnt: item.stockQnt,
                            typeName: item.typeName,
                            friendlyName: item.friendlyName,
                            fileLocation: item.fileLocation,
                            factoryRawMaterialID: item.factoryRawMaterialID,
                            purchaseOrderDetailReceivingPlanDtos: [],
                        }
                        debugger;
                        addingItem.purchaseOrderDetailReceivingPlanDtos.push({
                            purchaseOrderDetailReceivingPlanID: purchaseOrderService.getIncrementId(),
                            purchaseOrderDetailID: addingItem.purchaseOrderDetailID,
                            plannedETA: addingItem.eta,
                            plannedReceivingQnt: item.orderQnt,
                            remark: ''
                        });

                        //check item is exist and add to dto detail
                        var x = $scope.data.purchaseOrderDetailDTOs.filter(o => o.productionItemID === item.productionItemID);
                        if (x === null || x.length === 0) {
                            debugger;
                            $scope.data.purchaseOrderDetailDTOs.push(addingItem);
                        }
                        else {
                            bootbox.alert("This item is existed in system");
                        }

                    }
                });               
            },

            openReceivingPlan: function (purchaseOrderDetail) {
                $timeout(function () {
                    $scope.purchaseOrderDetail = purchaseOrderDetail;
                }, 0);

                jQuery("#frmReceivingPlanView").modal();
            },

            changePlannedDate: function (purchaseOrderDetail) {
                for (var i = 0; i < purchaseOrderDetail.purchaseOrderDetailReceivingPlanDtos.length; i++) {
                    var x = purchaseOrderDetail.purchaseOrderDetailReceivingPlanDtos[i];
                    if (x.purchaseOrderDetailReceivingPlanID < 0 && x.plannedETA === '') {
                        purchaseOrderDetail.purchaseOrderDetailReceivingPlanDtos[i].plannedETA = purchaseOrderDetail.eTA;
                    }
                }
            },

            addReceivingPlan: function () {
                var item = {
                    purchaseOrderDetailReceivingPlanID: purchaseOrderService.getIncrementId(),
                    purchaseOrderDetailID: purchaseOrderService.getIncrementId(),
                    plannedETA: '',
                    plannedReceivingQnt: null,
                    remark: ''
                };
                $scope.purchaseOrderDetail.purchaseOrderDetailReceivingPlanDtos.push(item);
            },

            openHictoryPrice: function (purchaseOrderDetail) {
                $timeout(function () {
                    $scope.purchaseOrderDetail = purchaseOrderDetail;
                }, 0);
                jQuery("#frmHictoryPrice").modal();
            },

            removeReceivingPlan: function ($event, purchaseOrderDetail, purchaseOrderReceivingPlan) {             
                var index = purchaseOrderDetail.purchaseOrderDetailReceivingPlanDtos.indexOf(purchaseOrderReceivingPlan);
                purchaseOrderDetail.purchaseOrderDetailReceivingPlanDtos.splice(index, 1);
            },

        };

        //
        //init form
        //
        $scope.event.init();
        $scope.event.load();
    };
})();