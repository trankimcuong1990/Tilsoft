$(document).keypress(function (e) {
    if ($(e.target).is('input, textarea')) {
        return;
    }
    if (e.which === 83) { // 83: S
        var scope = angular.element(jQuery('body')).scope();
        scope.event.update();
    }
});

var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', function ($scope, $timeout) {
    //
    // property
    //
    $scope.data = null;
    $scope.forwarders = [];
    $scope.poLs = [];
    $scope.poDs = [];
    $scope.currencies = [];
    $scope.transportCostItemDTOs = [];
    $scope.transportConditionItemDTOs = [];
    $scope.transportCostTypes = [];
    $scope.transportCostChargeTypes = [];
    $scope.newID = -100;
    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.forwarders = data.data.forwarders;
                    $scope.currencies = data.data.currencies;
                    $scope.poLs = data.data.poLs;
                    $scope.poDs = data.data.poDs;
                    $scope.transportCostTypes = data.data.transportCostTypes;
                    $scope.transportCostItemDTOs = data.data.transportCostItemDTOs;
                    $scope.transportConditionItemDTOs = data.data.transportConditionItemDTOs;
                    $scope.transportCostChargeTypes = data.data.transportCostChargeTypes;
                    if (context.isCopy=="true") {
                        context.id = 0;
                        $scope.data.transportOfferID = null;
                        $scope.data.validFrom = null;
                        $scope.data.validTo = null;
                        angular.forEach($scope.data.transportOfferCostDetailDTOs, function (item) {
                            item.transportOfferCostDetailID = $scope.method.getNewID();
                        });
                        angular.forEach($scope.data.transportOfferConditionDetailDTOs, function (item) {
                            item.transportOfferConditionDetailID = $scope.method.getNewID();
                        });
                    }
                    $scope.$apply();
                    jQuery('#content').show();
                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                    //add one line to make sure user can input data wihtout click any button add
                    if ($scope.data.transportOfferCostDetailDTOs.length == 0) {
                        $scope.data.transportOfferCostDetailDTOs.push({
                            transportOfferCostDetailID: $scope.method.getNewID()
                        });
                    }                    
                    if ($scope.data.transportOfferConditionDetailDTOs.length == 0) {
                        $scope.data.transportOfferConditionDetailDTOs.push({
                            transportOfferConditionDetailID: $scope.method.getNewID()
                        });
                    }
                    $scope.$apply();
                    //assign select2
                    $scope.method.assignSelect2();
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
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.transportOfferID);
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
                    $scope.data.transportOfferID,
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
            $scope.data.transportOfferCostDetailDTOs.splice(index, 1);
        },

        removeConditionDetail: function ($event, index) {
            $event.preventDefault();
            $scope.data.transportOfferConditionDetailDTOs.splice(index, 1);
        },

        addNewCostDetailLine: function ($event, index, item) {
            $event.preventDefault();
            if (index + 1 == $scope.data.transportOfferCostDetailDTOs.length) {
                var transportOfferCostDetailID=$scope.method.getNewID()
                if (item.transportCostItemID != null || item.currency != null || item.cost20DC != null || item.cost40DC != null || item.cost40HC != null || item.polid != null || item.podid != null || item.remark != null) {
                    $scope.data.transportOfferCostDetailDTOs.push({
                        transportOfferCostDetailID: transportOfferCostDetailID
                    });
                }
                $timeout(function () {
                    $scope.method.assignSelect2_Cost(transportOfferCostDetailID);
                });
            }     
        },

        addNewConditionDetailLine: function ($event, index, item) {
            $event.preventDefault();
            if (index + 1 == $scope.data.transportOfferConditionDetailDTOs.length) {
                var transportOfferConditionDetailID = $scope.method.getNewID();
                if (item.transportConditionItemID != null || item.condition20DC != null || item.condition40DC != null || item.condition40HC != null || item.polid != null || item.remark != null) {
                    $scope.data.transportOfferConditionDetailDTOs.push({
                        transportOfferConditionDetailID: transportOfferConditionDetailID
                    });
                }
                $timeout(function () {
                    $scope.method.assignSelect2_Condition(transportOfferConditionDetailID);
                });
            }
        },

        transportCostItemChange: function (item) {
            var x = $scope.transportCostItemDTOs.filter(function (o) { return o.transportCostItemID == item.transportCostItemID });
            if (x != null && x.length > 0) {
                item.currency = x.currency;
                $timeout(function () {
                    $("#currency" + item.transportOfferCostDetailID).trigger('change');
                });
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
                    $scope.data.file_NewFile = fileUploader2.selectedFileName;
                    $scope.data.file_HasChange = true;
                });
            };
            fileUploader2.open();
        },
        removeFile: function () {
            $scope.data.friendlyName = '';
            $scope.data.fileLocation = '';
            $scope.data.file_NewFile = '';
            $scope.data.file_HasChange = true;
        },
        downloadFile: function () {
            if ($scope.data.fileLocation) {
                window.open($scope.data.fileLocation);
            }
        },

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
        assignSelect2_Cost: function (rowID) {
            $("#costitem" + rowID).select2();
            $("#currency" + rowID).select2();
            $("#pol" + rowID).select2();
            $("#pod" + rowID).select2();           
        },

        assignSelect2_Condition: function (rowID) {
            $("#conditionitem" + rowID).select2();
            $("#polcondition" + rowID).select2();
            $("#podcondition" + rowID).select2();
        },

        assignSelect2: function () {
            $("#forwarderID").select2();
            angular.forEach($scope.data.transportOfferConditionDetailDTOs, function (item) {
                $scope.method.assignSelect2_Condition(item.transportOfferConditionDetailID);
            });
            angular.forEach($scope.data.transportOfferCostDetailDTOs, function (item) {
                $scope.method.assignSelect2_Cost(item.transportOfferCostDetailID);
            });
        }

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
                jsonService.deleteTransportCostItem(id,
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
                        if ($scope.transportCostItemForm.data!=null && $scope.transportCostItemForm.data.transportCostItemID == id) {
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
                                x.transportCostTypeNM=$scope.transportCostTypes.filter(function (o) { return o.transportCostTypeID == newItemData.transportCostTypeID })[0].transportCostTypeNM;
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
                if ($scope.transportCostItemForm.data!=null) {
                    $scope.transportCostItemForm.editingItem.transportCostItemID = $scope.transportCostItemForm.data.transportCostItemID;
                    $scope.transportCostItemForm.editingItem.currency = $scope.transportCostItemForm.data.currency;
                    $timeout(function () {
                        $("#costitem" + $scope.transportCostItemForm.editingItem.transportOfferCostDetailID).trigger('change');
                        $("#currency" + $scope.transportCostItemForm.editingItem.transportOfferCostDetailID).trigger('change');
                    });
                }
                $('#frmTransportCostItemInfo').modal('hide');
            },
        }
    }

    //
    //edit or addd transport condition item
    //
    $scope.transportConditionItemForm = {
        editingItem: null,

        transportConditionItemID: null,
        data: null,
        event: {
            load: function (transportConditionItemID, currentItem) {
                $scope.transportConditionItemForm.editingItem = currentItem;
                
                var id = (transportConditionItemID == null ? 0 : transportConditionItemID);
                $scope.transportConditionItemForm.transportConditionItemID = id;
                $scope.transportConditionItemForm.event.getTransportConditionItem(id);
            },
            getTransportConditionItem: function (id) {
                jsonService.getTransportConditionItem(
                    id,
                    function (data) {
                        $scope.transportConditionItemForm.data = data.data;
                        $('#frmTransportConditionItemInfo').modal('show');
                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.transportConditionItemForm.data = null;
                        $scope.$apply();
                    }
                );
            },

            newConditionItem: function () {
                $scope.transportConditionItemForm.transportConditionItemID = 0;
                $scope.transportConditionItemForm.event.getTransportConditionItem(0);
            },

            deleteConditionItem: function (id) {
                jsonService.deleteTransportConditionItem(id,
                    function (data) {
                        if (data.message.type == 2) {
                            bootbox.alert('Can not delete this condition.');
                        }
                        else {
                            //delete item
                            var j = -1;
                            for (var i = 0; i < $scope.transportConditionItemDTOs.length; i++) {
                                if ($scope.transportConditionItemDTOs[i].transportConditionItemID == id) {
                                    j = i;
                                    break;
                                }
                            }
                            if (j >= 0) {
                                $scope.transportConditionItemDTOs.splice(j, 1);
                            }
                            //empty current item
                            if ($scope.transportConditionItemForm.data != null && $scope.transportConditionItemForm.data.transportConditionItemID == id) {
                                $scope.transportConditionItemForm.data = null;
                            }
                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            },

            selectItem: function (item) {
                $scope.transportConditionItemForm.transportConditionItemID = item.transportConditionItemID;
                $scope.transportConditionItemForm.data = item;
            },

            updateTransportConditionItem: function () {
                jsonService.updateTransportConditionItem(
                    $scope.transportConditionItemForm.transportConditionItemID,
                    $scope.transportConditionItemForm.data,
                    function (data) {
                        if (data.message.type === 0) {
                            var newItemData = data.data;
                            if ($scope.transportConditionItemForm.transportConditionItemID > 0) {
                                var x = $scope.transportConditionItemDTOs.filter(function (o) { return o.transportConditionItemID == $scope.transportConditionItemForm.transportConditionItemID })[0];
                                x.transportConditionItemNM = newItemData.transportConditionItemNM;
                                x.isDefault = newItemData.isDefault;
                            }
                            else {
                                $scope.transportConditionItemDTOs.push({
                                    transportConditionItemID: newItemData.transportConditionItemID,
                                    transportConditionItemNM: newItemData.transportConditionItemNM,
                                    isDefault: newItemData.isDefault,
                                });
                            }                            
                        }
                        $scope.transportConditionItemForm.transportConditionItemID = newItemData.transportConditionItemID;
                        $scope.transportConditionItemForm.data = newItemData;
                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },

            cancel: function () {
                $('#frmTransportConditionItemInfo').modal('hide');
            },

            ok: function () {
                if ($scope.transportConditionItemForm.data != null) {
                    $scope.transportConditionItemForm.editingItem.transportConditionItemID = $scope.transportConditionItemForm.data.transportConditionItemID;
                    $timeout(function () {
                        $("#conditionitem" + $scope.transportConditionItemForm.editingItem.transportOfferConditionDetailID).trigger('change');
                    });
                }
                $('#frmTransportConditionItemInfo').modal('hide');
            }
        }
    }

    //
    // init
    //
    $scope.event.load();
}]);