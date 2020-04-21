jQuery('#grdLoadingPlan').scrollableTable2();

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ngSanitize', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', '$filter', function ($scope, $timeout, $filter) {
    //
    // property
    //
    $scope.data = null;

    $scope.poLs = null;
    $scope.poDs = null;
    $scope.movementTerms = null;
    $scope.deliveryTerms = null;
    $scope.oceanFreights = null;
    $scope.forwarders = null;

    // data support for set default
    $scope.defaults = [];
    $scope.defaultSelected = null;
    $scope.detailID = -1;

    //
    // grid handler
    //
    $scope.gridHandler = {
    };


    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                context.supplierId,
                context.clientId,
                context.season,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.poLs = data.data.poLs;
                    $scope.poDs = data.data.poDs;
                    $scope.movementTerms = data.data.movementTerms;
                    $scope.deliveryTerms = data.data.deliveryTerms;
                    $scope.oceanFreights = data.data.oceanFreights;
                    $scope.forwarders = data.data.forwarders;
                    $scope.data.viewTypeID = 1;
                    $scope.$apply();

                    jQuery('#content').show();

                    // watch functions
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                    $scope.$watch('data.forwarderID', function (newValue, oldValue) {
                        if (oldValue != newValue) {
                            $scope.method.setForwarderInfo(newValue);
                        }
                    });
                    $scope.$watch('data.eta', function (newValue, oldValue) {
                        if (oldValue != newValue) {
                            $scope.method.setETA2(newValue);
                        }
                    });

                    $("#forwarderBox").select2();
                    $("#polBox").select2();
                    $("#podBox").select2();
                    $("#deliveryTermBox").select2();
                    $("#movementTermBox").select2();
                    $("#occeanFreightBox").select2();

                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.poLs = null;
                    $scope.poDs = null;
                    $scope.movementTerms = null;
                    $scope.deliveryTerms = null;
                    $scope.oceanFreights = null;
                    $scope.forwarders = null;
                    $scope.$apply();
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                $scope.data.shipedDate = $scope.data.etd;
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.bookingID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', context.error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', context.errMsg);
            }
        },
        approve: function () {
            // check shiped date.
            if ($scope.data.shipedDate === null || $scope.data.shipedDate === '') {
                jsHelper.showMessage('warning', 'Please input Shiped Date in B/L Information.');
                return false;
            }

            if (confirm('Booking can not be edited after confirm, continue to confirm the booking ?')) {
                jsonService.approve(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.bookingID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', context.error);
                    }
                );
            }
        },
        reset: function () {
            if (confirm('Reset the booking ?')) {
                jsonService.reset(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.bookingID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', context.error);
                    }
                );
            }
        },
        confirmETA: function (type) {
            if (confirm('Confirm ETA ' + (type == 1 ? '' : '2') + '?')) {
                jsonService.confirmETA(
                    context.id,
                    $scope.data,
                    type,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.bookingID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', context.error);
                    }
                );
            }
        },

        confirmAllLoadingEvt: function () {
            jsonService.confirmAllLoading(
                context.id,
                $scope.data,
                function (data) {
                    jsHelper.processMessageEx(data.message);
                    if (data.message.type === 0) {
                        $scope.method.refresh(data.data.bookingID);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', context.error);
                }
            );
        },

        delete: function () {
            if (confirm('Are you sure ?')) {
                jsonService.delete(context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        window.location = context.backUrl;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        //
        // file functions
        //
        changeFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.friendlyName = fileUploader2.selectedFriendlyName;
                    $scope.data.fileLocation = fileUploader2.selectedFileUrl;
                    $scope.data.BLFile_NewFile = fileUploader2.selectedFileName;
                    $scope.data.BLFile_HasChanged = true;
                });
            };
            fileUploader2.open();
        },
        removeFile: function () {
            $scope.data.friendlyName = '';
            $scope.data.fileLocation = '';
            $scope.data.BLFile_NewFile = '';
            $scope.data.BLFile_HasChanged = true;
        },
        downloadFile: function () {
            if ($scope.data.fileLocation) {
                window.open($scope.data.fileLocation);
            }
        },
        changeEUTRFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.eutrFriendlyName = fileUploader2.selectedFriendlyName;
                    $scope.data.eutrFileLocation = fileUploader2.selectedFileUrl;
                    $scope.data.eutrFile_NewFile = fileUploader2.selectedFileName;
                    $scope.data.eutrFile_HasChange = true;
                });
            };
            fileUploader2.open();
        },
        removeEUTRFile: function () {
            $scope.data.eutrFriendlyName = '';
            $scope.data.eutrFileLocation = '';
            $scope.data.eutrFile_NewFile = '';
            $scope.data.eutrFile_HasChange = true;
        },
        downloadEUTRFile: function () {
            if ($scope.data.eutrFileLocation) {
                window.open($scope.data.eutrFileLocation);
            }
        },

        changeCOFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.coFriendlyName = fileUploader2.selectedFriendlyName;
                    $scope.data.coFileLocation = fileUploader2.selectedFileUrl;
                    $scope.data.coFile_NewFile = fileUploader2.selectedFileName;
                    $scope.data.coFile_HasChange = true;
                });
            };
            fileUploader2.open();
        },
        removeCOFile: function () {
            $scope.data.coFriendlyName = '';
            $scope.data.coFileLocation = '';
            $scope.data.coFile_NewFile = '';
            $scope.data.coFile_HasChange = true;
        },
        downloadCOFile: function () {
            if ($scope.data.coFileLocation) {
                window.open($scope.data.coFileLocation);
            }
        },

        changeFumigationFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.fumigationFriendlyName = fileUploader2.selectedFriendlyName;
                    $scope.data.fumigationFileLocation = fileUploader2.selectedFileUrl;
                    $scope.data.fumigationFile_NewFile = fileUploader2.selectedFileName;
                    $scope.data.fumigationFile_HasChange = true;
                });
            };
            fileUploader2.open();
        },
        removeFumigationFile: function () {
            $scope.data.fumigationFriendlyName = '';
            $scope.data.fumigationFileLocation = '';
            $scope.data.fumigationFile_NewFile = '';
            $scope.data.fumigationFile_HasChange = true;
        },
        downloadFumigationFile: function () {
            if ($scope.data.coFileLocation) {
                window.open($scope.data.fumigationFileLocation);
            }
        },

        changeOtherFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.otherFriendlyName = fileUploader2.selectedFriendlyName;
                    $scope.data.otherFileLocation = fileUploader2.selectedFileUrl;
                    $scope.data.otherFile_NewFile = fileUploader2.selectedFileName;
                    $scope.data.otherFile_HasChange = true;
                });
            };
            fileUploader2.open();
        },
        removeOtherFile: function () {
            $scope.data.otherFriendlyName = '';
            $scope.data.otherFileLocation = '';
            $scope.data.otherFile_NewFile = '';
            $scope.data.otherFile_HasChange = true;
        },
        downloadOtherFile: function () {
            if ($scope.data.coFileLocation) {
                window.open($scope.data.otherFileLocation);
            }
        },

        changeConfirmationFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.confirmationFriendlyName = fileUploader2.selectedFriendlyName;
                    $scope.data.confirmationFileLocation = fileUploader2.selectedFileUrl;
                    $scope.data.confirmationFile_NewFile = fileUploader2.selectedFileName;
                    $scope.data.confirmationFile_HasChange = true;
                });
            };
            fileUploader2.open();
        },
        removeConfirmationFile: function () {
            $scope.data.confirmationFriendlyName = '';
            $scope.data.confirmationFileLocation = '';
            $scope.data.confirmationFile_NewFile = '';
            $scope.data.confirmationFile_HasChange = true;
        },
        downloadConfirmationFile: function () {
            if ($scope.data.coFileLocation) {
                window.open($scope.data.confirmationFileLocation);
            }
        },

        //changeBookingConfirmFile: function () {
        //    fileUploader2.callback = function () {
        //        scope.$apply(function () {
        //            $scope.data.bookingConfirmationFriendlyName = fileUploader2.selectedFriendlyName;
        //            $scope.data.bookingConfirmationFileLocation = fileUploader2.selectedFileUrl;
        //            $scope.data.bookingConfirmationFile_NewFile = fileUploader2.selectedFileName;
        //            $scope.data.bookingConfirmationFile_HasChange = true;
        //        });
        //    };
        //    fileUploader2.open();
        //},
        //removeBookingConfirmFile: function () {
        //    $scope.data.bookingConfirmationFriendlyName = '';
        //    $scope.data.bookingConfirmationFileLocation = '';
        //    $scope.data.bookingConfirmationFile_NewFile = '';
        //    $scope.data.bookingConfirmationFile_HasChange = true;
        //},
        //downloadBookingConfirmFile: function () {
        //    if ($scope.data.bookingConfirmationFileLocation) {
        //        window.open($scope.data.bookingConfirmationFileLocation);
        //    }
        //},

        print: function () {
            jsonService.print(
                context.id,
                function (data) {
                    window.location = context.backendReportUrl + data.data + '.xlsm';
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        setDefaultShippingInstruction: function () {
            if (confirm('Would you like to set default consignee, notify and POD of Shipping Instruction for Booking?')) {
                jsonService.setDefault(
                    context.clientId,
                    function (data) {
                        $scope.defaults = data.data;
                        jQuery('#shippingInstruction').modal();

                        if ($scope.defaultSelected !== null) {
                            angular.forEach($scope.defaults, function (value, key) {
                                if (value.primaryID === $scope.defaultSelected.primaryID) {
                                    var index = $scope.defaults.indexOf(value);
                                    $scope.defaults[index] = $scope.defaultSelected;
                                }
                            });
                        }

                        console.log($scope.defaults);
                        $scope.$apply();
                    },
                    function (error) {
                    });
            }
        },
        selectItem: function (item) {
            $scope.method.clearSelected();

            item.isSelected = '1';
            $scope.defaultSelected = item;
        },
        acceptItem: function () {
            jQuery('#shippingInstruction').modal('hide');

            $scope.data.notifyParty1Info = $scope.defaultSelected.notifyInfo;
            $scope.data.consigneeInfo = $scope.defaultSelected.consigneeInfo;
            $scope.data.podid = $scope.defaultSelected.podid;
        },
        cancelItem: function () {
            jQuery('#shippingInstruction').modal('hide');
        }
    };

    $scope.showPrintOption = function () {
        $('#frmPrintOption').modal('show');
    };

    $scope.printOption = function (purchasingInvoiceID, packingListID, bookingID, optionID) {
        if (optionID === undefined) {
            jsHelper.showMessage('warning', 'Please select to option.');
            return false;
        }
        if (parseInt(optionID) === 1) {
            if (purchasingInvoiceID === null) {
                $scope.printLoadingPlan(bookingID);
            }
            else {
                $scope.print(purchasingInvoiceID);
            }
        }
        if (parseInt(optionID) === 2) {
            if (packingListID === null) {
                $scope.printLoadingPlan_PL(bookingID);
            }
            else {
                $scope.printPackingList(packingListID);
            }
        }
    };

    $scope.print = function (purchasingInvoiceID) {
        if (purchasingInvoiceID === null) {
            bootbox.alert("Invoice did not make yet");
            return;
        }
        purchasingInvoiceService.getReport(purchasingInvoiceID,
            function (data) {
                jsHelper.processMessage(data.message);
                if (data.message.type === 0) {
                    window.location = context.backendReportUrl + data.data + '.xlsm';
                }
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            }
        );
    };

    $scope.printLoadingPlan = function (bookingID) {
        jsonService.printLoadingPlan(bookingID,
            function (data) {
                jsHelper.processMessage(data.message);
                if (data.message.type === 0) {
                    window.location = context.backendReportUrl + data.data + '.xlsm';
                }
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            }
        );
    };


    $scope.printPackingList = function (packingListID) {
        var companyID = $scope.data.companyID;
        var reportOption = -1;
        //find option to print
        if (companyID === 13) {
            reportOption = 1;
        }
        else if (companyID === 4) {
            reportOption = 2;
        }
        //print
        packingListService.getReport(packingListID, reportOption,
            function (data) {
                window.location = context.backendReportUrl + data.data;
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            }
        );
    };

    $scope.printLoadingPlan_PL = function (bookingID) {
        var companyID = $scope.data.companyID;
        var reportOption = -1;
        if (companyID === 13) {
            reportOption = 1;
        }
        else if (companyID === 4) {
            reportOption = 2;
        }
        jsonService.printLoadingPlan_PL(
            bookingID, 
            reportOption,
            function (data) {
                jsHelper.processMessage(data.message);
                if (data.message.type === 0) {
                    window.location = context.backendReportUrl + data.data;
                }
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            }
        );
    };

    //
    // method
    //
    $scope.method = {
        createDetailID: function () {
        return $scope.detailID = $scope.detailID - 1;
        },
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        },
        setForwarderInfo: function (id) {
            if ($scope.data != null) {
                if (id != null && id != 'undefined') {
                    angular.forEach($scope.forwarders, function (value, key) {
                        if (value.forwarderID == id) {
                            $scope.data.forwarderInfo = value.forwarderNM + "/n" + value.address;
                        }
                    }, null);
                }
                else {
                    $scope.data.forwarderInfo = '';
                }
            }
        },
        setETA2: function (value) {
            if ($scope.data != null) {
                if (!$scope.data.etA2) {
                    $scope.data.etA2 = value;
                }
            }
        },
        formatRemark: function (remark) {
            if (remark == null) {
                return '';
            }
            return remark.replace(/(?:\r\n|\r|\n)/g, '<br />');
        },
        clearSelected: function () {
            angular.forEach($scope.defaults, function (value, key) {
                if (value.isSelected !== undefined && value.isSelected !== null && value.isSelected === '1') {
                    value.isSelected = null;
                }
            });
        }
    };

    //
    //
    //
    $scope.factoryOrder = {
        data: [],
        isSelected: false,
        filterFactoryOrder: {
            clientID: null,
            season: null,
            pi: '', 
            bookingID: null
        },
        viewTypes: [
            { id: 1, name: 'View P/I' },
            { id: 2, name: 'View Detail' }
        ],
        getFactoryOrder: function () {
            if ($scope.data.bookingID === 0) {
                $scope.factoryOrder.filterFactoryOrder.bookingID = null;
            }
            else {
                if ($scope.data.bookingID > 0) {
                    $scope.factoryOrder.filterFactoryOrder.bookingID = $scope.data.bookingID;
                }
            }
            $scope.factoryOrder.filterFactoryOrder.clientID = $scope.data.clientID;
            $scope.factoryOrder.filterFactoryOrder.season = $scope.data.season;
            jsonService.getFactoryOrder(
                $scope.factoryOrder.filterFactoryOrder.clientID,
                $scope.factoryOrder.filterFactoryOrder.season,
                $scope.factoryOrder.filterFactoryOrder.pi,
                $scope.factoryOrder.filterFactoryOrder.bookingID,
                function (data) {
                    $scope.factoryOrder.data = data.data;
                    for (var i = $scope.factoryOrder.data.factoryOrderDTOs.length - 1; i >= 0; i--) {
                        var item = $scope.factoryOrder.data.factoryOrderDTOs[i];
                        var check = false;
                        for (var j = 0; j < $scope.data.bookingPlanFactoryOrderDTOs.length; j++) {
                            var sItem = $scope.data.bookingPlanFactoryOrderDTOs[j];
                            if (item.factoryOrderID === sItem.factoryOrderID) {
                                check = true;
                                break;
                            }
                        }
                        if (check === true) {
                            var index = $scope.factoryOrder.data.factoryOrderDTOs.indexOf(item);
                            $scope.factoryOrder.data.factoryOrderDTOs.splice(index, 1);
                        }
                    }
                    $timeout(function () {
                        jQuery("#frmFactoryOrder").modal('show');
                    }, 0);
                },
                function (error) {
                });
        },
        selectFactoryOrder: function () {
            for (var i = 0; i < $scope.factoryOrder.data.factoryOrderDTOs.length; i++) {
                var item = $scope.factoryOrder.data.factoryOrderDTOs[i];
                if (item.isSelected) {
                    var sADetailItem = {
                        factoryOrderID: item.factoryOrderID,
                        plannedQnt: item.orderQnt,
                        orderQnt: item.orderQnt,
                        factoryOrderUD: item.factoryOrderUD,
                        proformaInvoiceNo: item.proformaInvoiceNo,
                        lds: item.lds,
                        factoryUD: item.factoryUD
                    };
                    $scope.data.bookingPlanFactoryOrderDTOs.push(sADetailItem);
                    angular.forEach($scope.factoryOrder.data.factoryOrderDetailDTOs, function (sItem) {
                        if (item.factoryOrderID === sItem.factoryOrderID) {
                            var sDetailItem = {
                                bookingPlanFactoryOrderDetailID: $scope.method.createDetailID(),
                                factoryOrderDetailID: sItem.factoryOrderDetailID,
                                plannedQnt: sItem.planQnt,
                                orderQnt: sItem.planQnt,
                                factoryOrderID: sItem.factoryOrderID,
                                factoryOrderUD: sItem.factoryOrderUD,
                                proformaInvoiceNo: sItem.proformaInvoiceNo,
                                lds: sItem.lds,
                                factoryUD: sItem.factoryUD,
                                articleCode: sItem.articleCode,
                                description: sItem.description,
                                shippedQnt: sItem.shippedQnt,
                                typeName: 'Product'
                            };
                            $scope.data.bookingPlanFactoryOrderDetailDTOs.push(sDetailItem);
                        }
                    });
                    angular.forEach($scope.factoryOrder.data.factoryOrderSampleDetailDTOs, function (sItem) {
                        if (item.factoryOrderID === sItem.factoryOrderID) {
                            var sDetailItem = {
                                bookingPlanFactoryOrderSampleDetailID: $scope.method.createDetailID(),
                                factoryOrderSampleDetailID: sItem.factoryOrderSampleDetailID,
                                plannedQnt: sItem.planQnt,
                                orderQnt: sItem.planQnt,
                                factoryOrderID: sItem.factoryOrderID,
                                factoryOrderUD: sItem.factoryOrderUD,
                                proformaInvoiceNo: sItem.proformaInvoiceNo,
                                lds: sItem.lds,
                                factoryUD: sItem.factoryUD,
                                articleCode: sItem.articleCode,
                                description: sItem.description,
                                shippedQnt: sItem.shippedQnt,
                                typeName: 'Sample'
                            };
                            $scope.data.bookingPlanFactoryOrderSampleDetailDTOs.push(sDetailItem);
                        }
                    });
                    angular.forEach($scope.factoryOrder.data.factoryOrderSparepartDetailDTOs, function (sItem) {
                        if (item.factoryOrderID === sItem.factoryOrderID) {
                            var sDetailItem = {
                                bookingPlanFactoryOrderSparepartDetailID: $scope.method.createDetailID(),
                                factoryOrderSparepartDetailID: sItem.factoryOrderSparepartDetailID,
                                plannedQnt: sItem.planQnt,
                                orderQnt: sItem.planQnt,
                                factoryOrderID: sItem.factoryOrderID,
                                factoryOrderUD: sItem.factoryOrderUD,
                                proformaInvoiceNo: sItem.proformaInvoiceNo,
                                lds: sItem.lds,
                                factoryUD: sItem.factoryUD,
                                articleCode: sItem.articleCode,
                                description: sItem.description,
                                shippedQnt: sItem.shippedQnt,
                                typeName: 'Sparepart'
                            };
                            $scope.data.bookingPlanFactoryOrderSparepartDetailDTOs.push(sDetailItem);
                        }
                    });
                }
            }

            jQuery("#frmFactoryOrder").modal('hide');
        },
        removeFactoryOrder: function (subItem) {
            for (var i = $scope.data.bookingPlanFactoryOrderDetailDTOs.length - 1; i >= 0; i--)
            {
                var item = $scope.data.bookingPlanFactoryOrderDetailDTOs[i];
                if (item.factoryOrderID === subItem.factoryOrderID) {
                    var index = $scope.data.bookingPlanFactoryOrderDetailDTOs.indexOf(item);
                    $scope.data.bookingPlanFactoryOrderDetailDTOs.splice(index, 1);
                }
            }

            for (var j = $scope.data.bookingPlanFactoryOrderSampleDetailDTOs.length - 1; j >= 0; j--) {
                var item1 = $scope.data.bookingPlanFactoryOrderSampleDetailDTOs[j];
                if (item1.factoryOrderID === subItem.factoryOrderID) {
                    var index1 = $scope.data.bookingPlanFactoryOrderSampleDetailDTOs.indexOf(item1);
                    $scope.data.bookingPlanFactoryOrderSampleDetailDTOs.splice(index1, 1);
                }
            }

            for (var z = $scope.data.bookingPlanFactoryOrderSparepartDetailDTOs.length - 1; z >= 0; z--) {
                var item12 = $scope.data.bookingPlanFactoryOrderSparepartDetailDTOs[z];
                if (item12.factoryOrderID === subItem.factoryOrderID) {
                    var index12 = $scope.data.bookingPlanFactoryOrderSparepartDetailDTOs.indexOf(item12);
                    $scope.data.bookingPlanFactoryOrderSparepartDetailDTOs.splice(index12, 1);
                }
            }

            //angular.forEach($scope.data.bookingPlanFactoryOrderDetailDTOs, function (item) {
            //    if (item.factoryOrderID === subItem.factoryOrderID) {
            //        var index = $scope.data.bookingPlanFactoryOrderDetailDTOs.indexOf(item);
            //        $scope.data.bookingPlanFactoryOrderDetailDTOs.splice(index, 1);
            //    }
            //});
            //angular.forEach($scope.data.bookingPlanFactoryOrderSampleDetailDTOs, function (item) {
            //    if (item.factoryOrderID === subItem.factoryOrderID) {
            //        var index1 = $scope.data.bookingPlanFactoryOrderSampleDetailDTOs.indexOf(item);
            //        $scope.data.bookingPlanFactoryOrderSampleDetailDTOs.splice(index1, 1);
            //    }
            //});
            //angular.forEach($scope.data.bookingPlanFactoryOrderSparepartDetailDTOs, function (item) {
            //    if (item.factoryOrderID === subItem.factoryOrderID) {
            //        var index2 = $scope.data.bookingPlanFactoryOrderSparepartDetailDTOs.indexOf(item);
            //        $scope.data.bookingPlanFactoryOrderSparepartDetailDTOs.splice(index2, 1);
            //    }
            //});
            var index3 = $scope.data.bookingPlanFactoryOrderDTOs.indexOf(subItem);
            $scope.data.bookingPlanFactoryOrderDTOs.splice(index3, 1);
        },
        changeSelectedAll: function () {
            if ($scope.factoryOrder.isSelected) {
                angular.forEach($scope.factoryOrder.data.factoryOrderDTOs, function (item) {
                    item.isSelected = true;
                });
                angular.forEach($scope.factoryOrder.data.factoryOrderDetailDTOs, function (item) {
                    item.isSelected = true;
                });
                angular.forEach($scope.factoryOrder.data.factoryOrderSampleDetailDTOs, function (item) {
                    item.isSelected = true;
                });
                angular.forEach($scope.factoryOrder.data.factoryOrderSparepartDetailDTOs, function (item) {
                    item.isSelected = true;
                });
            }
            else {
                angular.forEach($scope.factoryOrder.data.factoryOrderDTOs, function (item) {
                    item.isSelected = false;
                });
                angular.forEach($scope.factoryOrder.data.factoryOrderDetailDTOs, function (item) {
                    item.isSelected = false;
                });
                angular.forEach($scope.factoryOrder.data.factoryOrderSampleDetailDTOs, function (item) {
                    item.isSelected = false;
                });
                angular.forEach($scope.factoryOrder.data.factoryOrderSparepartDetailDTOs, function (item) {
                    item.isSelected = false;
                });
            }
        }
    };

    //
    // init
    //    
    $scope.event.init();
}]);