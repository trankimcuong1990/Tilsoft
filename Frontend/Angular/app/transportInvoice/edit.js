$(document).keypress(function (e) {
    if ($(e.target).is('input, textarea')) {
        return;
    }
    if (e.which === 83) { // 83: S
        var scope = angular.element(jQuery('body')).scope();
        scope.event.update();
    }
});

var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive','avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', function ($scope, $timeout) {
    //
    // property
    //
    $scope.data = null;
    $scope.currencies = [];
    $scope.transportCostItemDTOs = [];
    $scope.transportCostTypes = [];
    $scope.transportCostChargeTypes = [];
    $scope.loadingPlanDTOs = [];
    $scope.initCostItemDTOs = [];
    $scope.newID = -100;
    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.getTransportInvoice(
                context.id,context.bookingID,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.currencies = data.data.currencies;
                    $scope.transportCostItemDTOs = data.data.transportCostItemDTOs;
                    $scope.transportCostTypes = data.data.transportCostTypes;
                    $scope.loadingPlanDTOs = data.data.loadingPlanDTOs;
                    $scope.transportCostChargeTypes = data.data.transportCostChargeTypes;
                    $scope.initCostItemDTOs = data.data.initCostItems;

                    if (context.isCopy=="true") {
                        context.id = 0;
                        $scope.data.transportInvoiceID = null;
                        angular.forEach($scope.data.transportInvoiceDetailDTOs, function (item) {
                            item.transportInvoiceDetailID = $scope.method.getNewID();
                            angular.forEach(item.transportInvoiceContainerDetailDTOs, function (sItem) {
                                sItem.transportInvoiceContainerDetailID = $scope.method.getNewID();
                            });
                        });
                    }
                    if (context.id == 0) {
                        $scope.data.bookingID = context.bookingID;
                    }
                    $scope.$apply();
                    jQuery('#content').show();
                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                    //assign select2
                    $scope.method.assignSelect2ForAllCostItem();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                //check currency is consistancy
                for (var i = 0; i < $scope.data.transportInvoiceDetailDTOs.length; i++) {
                    var item = $scope.data.transportInvoiceDetailDTOs[i];
                    if (item.currency != $scope.data.currency) {
                        bootbox.alert('Currency must be same for every cost item');
                        return;
                    }
                }
                //update data
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.transportInvoiceID);
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
                    $scope.data.transportInvoiceID,
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

        removeCostDetail: function ($event, index) {
            $event.preventDefault();
            $scope.data.transportInvoiceDetailDTOs.splice(index, 1);
        },

        removeCont: function ($event, index,item) {
            $event.preventDefault();
            item.transportInvoiceContainerDetailDTOs.splice(index, 1);
        },

        addCont: function ($event, item) {
            $event.preventDefault();
            if (item.transportInvoiceContainerDetailDTOs == null) item.transportInvoiceContainerDetailDTOs = [];
            item.transportInvoiceContainerDetailDTOs.push({
                transportInvoiceContainerDetailID: $scope.method.getNewID(),
                amount: null
            });
            $timeout(function () {
                angular.forEach(item.transportInvoiceContainerDetailDTOs, function (cItem) {
                    $scope.method.assignSelect2ForContainerItem(cItem.transportInvoiceContainerDetailID);
                });
            });
        },

        //addNewCostDetailLine: function ($event, index, item) {
        //    $event.preventDefault();
        //    if (index + 1 == $scope.data.transportInvoiceDetailDTOs.length) {
        //        var transportInvoiceDetailID = $scope.method.getNewID()
        //        if (item.transportCostItemID != null || item.remark != null || item.subTotalAmount != null) {
        //            $scope.data.transportInvoiceDetailDTOs.push({
        //                transportInvoiceDetailID: transportInvoiceDetailID,
        //                transportInvoiceContainerDetailDTOs : []
        //            });
        //        }
        //        $timeout(function () {
        //            $scope.method.assignSelect2ForCostItem(transportInvoiceDetailID);
        //        });
        //    }     
        //},

        addCostItem: function ($event) {
            $event.preventDefault();
            var transportInvoiceDetailID = $scope.method.getNewID()
            $scope.data.transportInvoiceDetailDTOs.push({
                transportInvoiceDetailID: transportInvoiceDetailID,
                subTotalAmount: null,
                transportInvoiceContainerDetailDTOs: []
            });
            $timeout(function () {
                $scope.method.assignSelect2ForCostItem(transportInvoiceDetailID);
            });
        },

        transportCostItemChange: function (item) {
            var x = $scope.transportCostItemDTOs.filter(function (o) { return o.transportCostItemID == item.transportCostItemID })[0];
            item.currency = x.currency;
            var transportCostChargeTypeID = x.transportCostChargeTypeID;
            if (transportCostChargeTypeID == 2) {
                angular.forEach($scope.loadingPlanDTOs, function (loadingItem) {
                    var x = item.transportInvoiceContainerDetailDTOs.filter(function (o) { return o.loadingPlanID == loadingItem.loadingPlanID });
                    if (x == null || x.length == 0)
                    {
                        item.transportInvoiceContainerDetailDTOs.push({
                            transportInvoiceContainerDetailID: $scope.method.getNewID(),
                            loadingPlanID: loadingItem.loadingPlanID,
                            containerNo: loadingItem.containerNo,
                            amount: null
                        });
                    }
                });
                $timeout(function () {
                    angular.forEach(item.transportInvoiceContainerDetailDTOs, function (cItem) {
                        $scope.method.assignSelect2ForContainerItem(cItem.transportInvoiceContainerDetailID);
                    });
                });
                
            }
        },

        changeAmountOfCont: function (item) {
            var total = parseFloat(0);
            angular.forEach(item.transportInvoiceContainerDetailDTOs, function (cItem) {
                total += parseFloat((cItem.amount == null ? 0 : cItem.amount));
                console.log(cItem.amount);
            })
            item.subTotalAmount = total;
        },

        setInvoiceStatus: function (statusID) {
            var statusText = "";
            if (statusID == 2) {
                statusText = 'VERIFY';
            }
            else if (statusID == 3) {
                statusText = 'PAID';
            }
            bootbox.confirm({
                message: "Are you sure you want to set status invoice as " + statusText,
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
                        jsonService.setInvoiceStatus(
                            statusID,
                            $scope.data.transportInvoiceID,
                            function (data) {                   
                                jsHelper.processMessage(data.message);
                                if (data.message.type == 0) {
                                    $scope.data.invoiceStatusID = statusID;
                                    $scope.data.invoiceStatusText = statusText;
                                    $scope.$apply();
                                }
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error);
                            }
                        );
                    }
                }
            });
            
        },

        selectedCurrency: function (currency) {
            $scope.data.transportInvoiceDetailDTOs = [];
            angular.forEach($scope.initCostItemDTOs, function (item) {
                if (item.currency == currency) {
                    var detail = {
                        transportInvoiceDetailID: $scope.method.getNewID(),
                        transportCostItemID: item.transportCostItemID,
                        currency: item.currency,
                        transportCostChargeTypeID: item.transportCostChargeTypeID,
                        transportInvoiceContainerDetailDTOs: [],
                    }
                    if (item.transportCostChargeTypeID == 1) { //charge base on BL
                        detail.offerPrice = item.cost40HC;
                        detail.subTotalAmount = detail.offerPrice;
                    }
                    else if (item.transportCostChargeTypeID == 2) { //charge base on container
                        var totalSubTotalAmount = parseFloat(0);
                        angular.forEach($scope.loadingPlanDTOs, function (loadingPlanItem) {
                            var x = {
                                transportInvoiceContainerDetailID: $scope.method.getNewID(),
                                loadingPlanID: loadingPlanItem.loadingPlanID,
                                containerNo: loadingPlanItem.containerNo,
                                containerTypeNM: loadingPlanItem.containerTypeNM,
                            };
                            var offerPrice = null;
                            var amount = null;
                            if (loadingPlanItem.containerTypeID == 1) { //cont 20 DC
                                offerPrice = item.cost20DC;
                                amount = offerPrice;
                            }
                            else if (loadingPlanItem.containerTypeID == 2) { //cont 40DC
                                offerPrice = item.cost40DC;
                                amount = offerPrice;
                            }
                            else if (loadingPlanItem.containerTypeID == 3) { //cont 40HC
                                offerPrice = item.cost40HC;
                                amount = offerPrice;
                            }
                            x.offerPrice = offerPrice;
                            x.amount = amount;
                            detail.transportInvoiceContainerDetailDTOs.push(x);
                            totalSubTotalAmount += parseFloat(amount);
                        });
                        detail.offerPrice = totalSubTotalAmount;
                        detail.subTotalAmount = totalSubTotalAmount;

                    }
                    $scope.data.transportInvoiceDetailDTOs.push(detail);
                }
            });
            //assign select2
            $timeout(function () {
                $scope.method.assignSelect2ForAllCostItem();
            });
        }

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
        assignSelect2ForCostItem: function (transportInvoiceDetailID) {
            $("#costitem" + transportInvoiceDetailID).select2();
        },
        assignSelect2ForContainerItem: function (transportInvoiceContainerDetailID) {
            $("#loadingplan" + transportInvoiceContainerDetailID).select2();
        },
        assignSelect2ForAllCostItem: function () {
            angular.forEach($scope.data.transportInvoiceDetailDTOs, function (item) {
                $scope.method.assignSelect2ForCostItem(item.transportInvoiceDetailID);
                angular.forEach(item.transportInvoiceContainerDetailDTOs, function (cItem) {
                    $scope.method.assignSelect2ForContainerItem(cItem.transportInvoiceContainerDetailID);
                })
            });
        },

        getTotalAmountOfCont: function (cItem) {
            var total = parseFloat(0);
            //total += parseFloat(cItem.offerPrice == null ? 0 : cItem.offerPrice);
            total += parseFloat(cItem.alreadyInvoiced == null ? 0 : cItem.alreadyInvoiced);
            total += parseFloat(cItem.amount == null ? 0 : cItem.amount);
            return total;
        },

        getTotalAmountOfCostItem: function (item) {
            var total = parseFloat(0);
            //total += parseFloat(item.offerPrice == null ? 0 : item.offerPrice);
            total += parseFloat(item.alreadyInvoiced == null ? 0 : item.alreadyInvoiced);
            total += parseFloat(item.subTotalAmount == null ? 0 : item.subTotalAmount);
            return total;
        },

        getTotalInvoice: function () {
            var total = parseFloat(0);
            if ($scope.data != null && $scope.data.transportInvoiceDetailDTOs) {
                angular.forEach($scope.data.transportInvoiceDetailDTOs, function (item) {
                    total += parseFloat(item.subTotalAmount == null ? 0 : item.subTotalAmount);
                });
            }
            return total;
        },        

        getTotalOfferPrice: function (item) {
            var total = parseFloat(0);
            if (item.transportInvoiceContainerDetailDTOs) {
                angular.forEach(item.transportInvoiceContainerDetailDTOs, function (cItem) {
                    total += parseFloat(cItem.offerPrice == null ? 0 : cItem.offerPrice);
                });
            }
            return total;
        },

    };
    //
    //edit or addd transport cost item
    //
    $scope.transportCostItemForm = {
        editingItem: null,

        transportCostItemID: null,
        data: null,
        event: {
            load: function (transportCostItemID, currentItem) {
                $scope.transportCostItemForm.editingItem = currentItem;

                var id = (transportCostItemID == null ? 0 : transportCostItemID);
                $scope.transportCostItemForm.transportCostItemID = id;
                $scope.transportCostItemForm.event.getTransportCostItem(id);
            },
            getTransportCostItem: function (id) {
                jsonService.getTransportCostItem(
                    context.transportOfferAPIServiceUrl,
                    id,
                    function (data) {
                        $scope.transportCostItemForm.data = data.data;
                        $('#frmTransportCostItemInfo').modal('show');
                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.transportCostItemForm.data = null;
                        $scope.$apply();
                    }
                );
            },

            newCostItem: function () {
                $scope.transportCostItemForm.transportCostItemID = 0;
                $scope.transportCostItemForm.event.getTransportCostItem(0);
            },

            deleteCostItem: function (id) {
                jsonService.deleteTransportCostItem(context.transportOfferAPIServiceUrl, id,
                    function (data) {
                        if (data.message.type == 2) {
                            bootbox.alert('Can not delete this cost.');
                        }
                        else {
                            //delete item
                            var j = -1;
                            for (var i = 0; i < $scope.transportCostItemDTOs.length; i++) {
                                if ($scope.transportCostItemDTOs[i].transportCostItemID == id) {
                                    j = i;
                                    break;
                                }
                            }
                            if (j >= 0) {
                                $scope.transportCostItemDTOs.splice(j, 1);
                            }
                        }
                        //empty current item
                        if ($scope.transportCostItemForm.data != null && $scope.transportCostItemForm.data.transportCostItemID == id) {
                            $scope.transportCostItemForm.data = null;
                        }
                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            },

            selectItem: function (item) {
                $scope.transportCostItemForm.transportCostItemID = item.transportCostItemID;
                $scope.transportCostItemForm.data = item;
            },
            updateTransportCostItem: function () {
                if ($scope.transportCostItemForm.data.transportCostItemNM == null) {
                    bootbox.alert('Please fill in cost name');
                    return;
                }
                else if ($scope.transportCostItemForm.data.transportCostTypeID == null) {
                    bootbox.alert('Please fill in cost type');
                    return;
                }
                else if ($scope.transportCostItemForm.data.transportCostChargeTypeID == null) {
                    bootbox.alert('Please fill in charge type');
                    return;
                }
                else if ($scope.transportCostItemForm.data.currency == null) {
                    bootbox.alert('Please fill in currency');
                    return;
                }
                jsonService.updateTransportCostItem(
                    context.transportOfferAPIServiceUrl,
                    $scope.transportCostItemForm.transportCostItemID,
                    $scope.transportCostItemForm.data,
                    function (data) {
                        if (data.message.type === 0) {
                            //adjust data for editing node
                            var newItemData = data.data;
                            if ($scope.transportCostItemForm.transportCostItemID > 0) {
                                var x = $scope.transportCostItemDTOs.filter(function (o) { return o.transportCostItemID == $scope.transportCostItemForm.transportCostItemID })[0];
                                x.transportCostItemNM = newItemData.transportCostItemNM;
                                x.transportCostTypeID = newItemData.transportCostTypeID;
                                x.transportCostChargeTypeID = newItemData.transportCostChargeTypeID,
                                x.currency = newItemData.currency;
                                x.isDefault = newItemData.isDefault;
                                x.transportCostTypeNM = $scope.transportCostTypes.filter(function (o) { return o.transportCostTypeID == newItemData.transportCostTypeID })[0].transportCostTypeNM;
                                x.transportCostChargeTypeNM = $scope.transportCostChargeTypes.filter(function (o) { return o.transportCostChargeTypeID == newItemData.transportCostChargeTypeID })[0].transportCostChargeTypeNM;
                            }
                            else {
                                $scope.transportCostItemDTOs.push({
                                    transportCostItemID: newItemData.transportCostItemID,
                                    transportCostItemNM: newItemData.transportCostItemNM,
                                    transportCostTypeID: newItemData.transportCostTypeID,
                                    transportCostChargeTypeID: newItemData.transportCostChargeTypeID,
                                    currency: newItemData.currency,
                                    isDefault: newItemData.isDefault,
                                    transportCostTypeNM: $scope.transportCostTypes.filter(function (o) { return o.transportCostTypeID == newItemData.transportCostTypeID })[0].transportCostTypeNM,
                                    transportCostChargeTypeNM: $scope.transportCostChargeTypes.filter(function (o) { return o.transportCostChargeTypeID == newItemData.transportCostChargeTypeID })[0].transportCostChargeTypeNM,
                                });
                            }
                            $scope.transportCostItemForm.transportCostItemID = newItemData.transportCostItemID;
                            $scope.transportCostItemForm.data = newItemData;
                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },

            cancel: function () {
                $('#frmTransportCostItemInfo').modal('hide');
            },

            ok: function () {
                if ($scope.transportCostItemForm.data != null) {
                    $scope.transportCostItemForm.editingItem.transportCostItemID = $scope.transportCostItemForm.data.transportCostItemID;
                    $scope.transportCostItemForm.editingItem.currency = $scope.transportCostItemForm.data.currency;
                    $timeout(function () {
                        $("#costitem" + $scope.transportCostItemForm.editingItem.transportInvoiceDetailID).trigger('change');
                    });
                }
                $('#frmTransportCostItemInfo').modal('hide');
            },
        }
    }

    //
    // init
    //
    $scope.event.load();
}]);