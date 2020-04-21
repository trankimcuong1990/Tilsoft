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
    $scope.clients = [];
    $scope.poLs = [];
    $scope.poDs = [];
    $scope.currencies = [];
    $scope.clientCostItemDTOs = [];
    $scope.clientConditionItemDTOs = [];
    $scope.clientCostTypes = [];
    $scope.clientCostChargeTypes = [];
    $scope.paymentTerms = [];
    $scope.newID = -100;
    $scope.printOptionValue = 1;
    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.clients = data.data.clients;
                    $scope.currencies = data.data.currencies;
                    $scope.poLs = data.data.poLs;
                    $scope.poDs = data.data.poDs;
                    $scope.clientCostTypes = data.data.clientCostTypes;
                    $scope.clientCostItemDTOs = data.data.clientCostItemDTOs;
                    $scope.clientConditionItemDTOs = data.data.clientConditionItemDTOs;
                    $scope.clientAdditionalItemDTOs = data.data.clientAdditionalItemDTOs;
                    $scope.clientCostChargeTypes = data.data.clientCostChargeTypes;
                    $scope.paymentTerms = data.data.paymentTerms;
                    if (context.isCopy == "true") {
                        context.id = 0;
                        $scope.data.clientOfferID = null;
                        $scope.data.validFrom = null;
                        $scope.data.validTo = null;
                        angular.forEach($scope.data.clientOfferCostDetailDTOs, function (item) {
                            item.clientOfferCostDetailID = $scope.method.getNewID();
                        });
                        angular.forEach($scope.data.clientOfferConditionDetailDTOs, function (item) {
                            item.clientOfferConditionDetailID = $scope.method.getNewID();
                        });
                    }
                    $scope.$apply();
                    console.log(data);
                    jQuery('#content').show();
                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                    //add one line to make sure user can input data wihtout click any button add
                    if ($scope.data.clientOfferCostDetailDTOs.length == 0) {
                        $scope.data.clientOfferCostDetailDTOs.push({
                            clientOfferCostDetailID: $scope.method.getNewID()
                        });
                    }
                    if ($scope.data.clientOfferConditionDetailDTOs.length == 0) {
                        $scope.data.clientOfferConditionDetailDTOs.push({
                            clientOfferConditionDetailID: $scope.method.getNewID()
                        });
                    }

                    //add three line by default when create new client offer
                    if (context.id == 0) {
                        angular.forEach($scope.clientAdditionalItemDTOs, function (item) {
                            $scope.data.clientOfferAdditionalDetailDTOs.push({
                                clientOfferAdditionalDetailID: $scope.method.getNewID(),
                                clientAdditionalItemID: item.clientAdditionalItemID,
                                clientAdditionalItemNM: item.clientAdditionalItemNM,
                                cost20DC: 0,
                                cost40DC: 0,
                                cost40HC: 0
                            });
                        });
                        console.log($scope.data.clientOfferAdditionalDetailDTOs);
                    };

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
                            $scope.method.refresh(data.data.clientOfferID);
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
                    $scope.data.clientOfferID,
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
            $scope.data.clientOfferCostDetailDTOs.splice(index, 1);
        },

        removeConditionDetail: function ($event, index) {
            $event.preventDefault();
            $scope.data.clientOfferConditionDetailDTOs.splice(index, 1);
        },

        addNewCostDetailLine: function ($event, index, item) {
            $event.preventDefault();
            if (index + 1 == $scope.data.clientOfferCostDetailDTOs.length) {
                var clientOfferCostDetailID = $scope.method.getNewID()
                if (item.clientCostItemID != null || item.currency != null || item.cost20DC != null || item.cost40DC != null || item.cost40HC != null || item.polid != null || item.podid != null || item.remark != null) {
                    $scope.data.clientOfferCostDetailDTOs.push({
                        clientOfferCostDetailID: clientOfferCostDetailID
                    });
                }
                $timeout(function () {
                    $scope.method.assignSelect2_Cost(clientOfferCostDetailID);
                });
            }
        },

        addNewConditionDetailLine: function ($event, index, item) {
            $event.preventDefault();
            if (index + 1 == $scope.data.clientOfferConditionDetailDTOs.length) {
                var clientOfferConditionDetailID = $scope.method.getNewID();
                if (item.clientConditionItemID != null) {
                    $scope.data.clientOfferConditionDetailDTOs.push({
                        clientOfferConditionDetailID: clientOfferConditionDetailID
                    });
                }
                $timeout(function () {
                    $scope.method.assignSelect2_Condition(clientOfferConditionDetailID);
                });
            }
        },

        addNewAdditionalDetailLine: function ($event, index, item) {
            $event.preventDefault();
            if (index + 1 == $scope.data.clientOfferAdditionalDetailDTOs.length) {
                var clientOfferAdditionalDetailID = $scope.method.getNewID();
                if (item.clientAdditionalItemID != null || item.condition20DC != null || item.condition40DC != null || item.condition40HC != null) {
                    $scope.data.clientOfferAdditionalDetailDTOs.push({
                        clientOfferAdditionalDetailID: clientOfferAdditionalDetailID
                    });
                }
                $timeout(function () {
                    $scope.method.assignSelect2_Condition(clientOfferConditionDetailID);
                });
            }
        },

        clientCostItemChange: function (item) {
            var x = $scope.clientCostItemDTOs.filter(function (o) { return o.clientCostItemID == item.clientCostItemID });
            if (x != null && x.length > 0) {
                item.currency = x.currency;
                $timeout(function () {
                    $("#currency" + item.clientOfferCostDetailID).trigger('change');
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
        printout: function () {
            jsonService.getClientOfferPrintout(
                $scope.data.clientOfferID,
                $scope.printOptionValue,
                function (data) {
                    window.location = context.reportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        },

        showPrintOptionForm: function () {
            $scope.printOptions = [];
            $scope.printOptions.push({
                printOptionValue: 1,
                printOptionText: "Print EuroFar Template"
            });
            $scope.printOptions.push({
                printOptionValue: 2,
                printOptionText: "Print OrangePine Template"
            });

            $('#frmPrintOption').modal('show');
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
            $("#clientID").select2();
            angular.forEach($scope.data.clientOfferConditionDetailDTOs, function (item) {
                $scope.method.assignSelect2_Condition(item.clientOfferConditionDetailID);
            });
            angular.forEach($scope.data.clientOfferCostDetailDTOs, function (item) {
                $scope.method.assignSelect2_Cost(item.clientOfferCostDetailID);
            });
        }

    };
    //
    //edit or addd client cost item
    //
    $scope.clientCostItemForm = {
        editingItem: null,

        clientCostItemID: null,
        data: null,
        event: {
            load: function (clientCostItemID, currentItem) {
                $scope.clientCostItemForm.editingItem = currentItem;

                var id = (clientCostItemID == null ? 0 : clientCostItemID);
                $scope.clientCostItemForm.clientCostItemID = id;
                $scope.clientCostItemForm.event.getClientCostItem(id);
            },
            getClientCostItem: function (id) {
                jsonService.getClientCostItem(
                    id,
                    function (data) {
                        $scope.clientCostItemForm.data = data.data;
                        $('#frmClientCostItemInfo').modal('show');
                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.clientCostItemForm.data = null;
                        $scope.$apply();
                    }
                );
            },

            newCostItem: function () {
                $scope.clientCostItemForm.clientCostItemID = 0;
                $scope.clientCostItemForm.event.getClientCostItem(0);
            },

            deleteCostItem: function (id) {
                console.log(id)
                jsonService.deleteClientCostItem(id,
                    function (data) {
                        if (data.message.type == 2) {
                            bootbox.alert('Can not delete this cost.');
                        }
                        else {
                            //delete item
                            var j = -1;
                            for (var i = 0; i < $scope.clientCostItemDTOs.length; i++) {
                                if ($scope.clientCostItemDTOs[i].clientCostItemID == id) {
                                    j = i;
                                    break;
                                }
                            }
                            if (j >= 0) {
                                $scope.clientCostItemDTOs.splice(j, 1);
                            }
                        }
                        //empty current item
                        if ($scope.clientCostItemForm.data != null && $scope.clientCostItemForm.data.clientCostItemID == id) {
                            $scope.clientCostItemForm.data = null;
                        }
                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            },

            selectItem: function (item) {
                $scope.clientCostItemForm.clientCostItemID = item.clientCostItemID;
                $scope.clientCostItemForm.data = item;
            },
            updateClientCostItem: function () {
                if ($scope.clientCostItemForm.data.clientCostItemNM == null) {
                    bootbox.alert('Please fill in cost name');
                    return;
                }
                else if ($scope.clientCostItemForm.data.clientCostTypeID == null) {
                    bootbox.alert('Please fill in cost type');
                    return;
                }
                else if ($scope.clientCostItemForm.data.clientCostChargeTypeID == null) {
                    bootbox.alert('Please fill in charge type');
                    return;
                }
                else if ($scope.clientCostItemForm.data.currency == null) {
                    bootbox.alert('Please fill in currency');
                    return;
                }
                jsonService.updateClientCostItem(
                    $scope.clientCostItemForm.clientCostItemID,
                    $scope.clientCostItemForm.data,
                    function (data) {
                        if (data.message.type === 0) {
                            //adjust data for editing node
                            var newItemData = data.data;
                            if ($scope.clientCostItemForm.clientCostItemID > 0) {
                                var x = $scope.clientCostItemDTOs.filter(function (o) { return o.clientCostItemID == $scope.clientCostItemForm.clientCostItemID })[0];
                                x.clientCostItemNM = newItemData.clientCostItemNM;
                                x.clientCostTypeID = newItemData.clientCostTypeID;
                                x.clientCostChargeTypeID = newItemData.clientCostChargeTypeID,
                                x.currency = newItemData.currency;
                                x.isDefault = newItemData.isDefault;
                                x.clientCostTypeNM = $scope.clientCostTypes.filter(function (o) { return o.clientCostTypeID == newItemData.clientCostTypeID })[0].clientCostTypeNM;
                                x.clientCostChargeTypeNM = $scope.clientCostChargeTypes.filter(function (o) { return o.clientCostChargeTypeID == newItemData.clientCostChargeTypeID })[0].clientCostChargeTypeNM;                                
                                x.poLname = $scope.poLs.filter(function (o) { return o.polid == newItemData.polid })[0].poLname;
                            }
                            else {
                                $scope.clientCostItemDTOs.push({
                                    clientCostItemID: newItemData.clientCostItemID,
                                    clientCostItemNM: newItemData.clientCostItemNM,
                                    clientCostTypeID: newItemData.clientCostTypeID,
                                    clientCostChargeTypeID: newItemData.clientCostChargeTypeID,
                                    currency: newItemData.currency,
                                    isDefault: newItemData.isDefault,
                                    clientCostTypeNM: $scope.clientCostTypes.filter(function (o) { return o.transportCostTypeID == newItemData.clientCostTypeID })[0].transportCostTypeNM,
                                    clientCostChargeTypeNM: $scope.clientCostChargeTypes.filter(function (o) { return o.transportCostChargeTypeID == newItemData.clientCostChargeTypeID })[0].transportCostChargeTypeNM,
                                    poLname: $scope.poLs.filter(function (o) { return o.polid == newItemData.polid })[0].poLname,
                                });
                            }
                            $scope.clientCostItemForm.clientCostItemID = newItemData.clientCostItemID;
                            $scope.clientCostItemForm.data = newItemData;
                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },

            cancel: function () {
                $('#frmClientCostItemInfo').modal('hide');
            },

            ok: function () {
                if ($scope.clientCostItemForm.data != null) {
                    $scope.clientCostItemForm.editingItem.clientCostItemID = $scope.clientCostItemForm.data.clientCostItemID;
                    $scope.clientCostItemForm.editingItem.currency = $scope.clientCostItemForm.data.currency;
                    $timeout(function () {
                        $("#costitem" + $scope.clientCostItemForm.editingItem.clientOfferCostDetailID).trigger('change');
                        $("#currency" + $scope.clientCostItemForm.editingItem.clientOfferCostDetailID).trigger('change');
                    });
                }
                $('#frmClientCostItemInfo').modal('hide');
            },
        }
    }

    //
    //edit or addd client condition item
    //
    $scope.clientConditionItemForm = {
        editingItem: null,

        clientConditionItemID: null,
        data: null,
        event: {
            load: function (clientConditionItemID, currentItem) {
                $scope.clientConditionItemForm.editingItem = currentItem;

                var id = (clientConditionItemID == null ? 0 : clientConditionItemID);
                $scope.clientConditionItemForm.clientConditionItemID = id;
                $scope.clientConditionItemForm.event.getClientConditionItem(id);
            },
            getClientConditionItem: function (id) {
                jsonService.getClientConditionItem(
                    id,
                    function (data) {
                        $scope.clientConditionItemForm.data = data.data;
                        $('#frmClientConditionItemInfo').modal('show');
                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.clientConditionItemForm.data = null;
                        $scope.$apply();
                    }
                );
            },

            newConditionItem: function () {
                $scope.clientConditionItemForm.clientConditionItemID = 0;
                $scope.clientConditionItemForm.event.getClientConditionItem(0);
            },

            deleteConditionItem: function (id) {
                jsonService.deleteClientConditionItem(id,
                    function (data) {
                        if (data.message.type == 2) {
                            bootbox.alert('Can not delete this condition.');
                        }
                        else {
                            //delete item
                            var j = -1;
                            for (var i = 0; i < $scope.clientConditionItemDTOs.length; i++) {
                                if ($scope.clientConditionItemDTOs[i].clientConditionItemID == id) {
                                    j = i;
                                    break;
                                }
                            }
                            if (j >= 0) {
                                $scope.clientConditionItemDTOs.splice(j, 1);
                            }
                            //empty current item
                            if ($scope.clientConditionItemForm.data != null && $scope.clientConditionItemForm.data.clientConditionItemID == id) {
                                $scope.clientConditionItemForm.data = null;
                            }
                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            },

            selectItem: function (item) {
                $scope.clientConditionItemForm.clientConditionItemID = item.clientConditionItemID;
                $scope.clientConditionItemForm.data = item;
            },

            updateClientConditionItem: function () {
                jsonService.updateClientConditionItem(
                    $scope.clientConditionItemForm.clientConditionItemID,
                    $scope.clientConditionItemForm.data,
                    function (data) {
                        if (data.message.type === 0) {
                            var newItemData = data.data;
                            if ($scope.clientConditionItemForm.clientConditionItemID > 0) {
                                var x = $scope.clientConditionItemDTOs.filter(function (o) { return o.clientConditionItemID == $scope.clientConditionItemForm.clientConditionItemID })[0];
                                x.clientConditionItemNM = newItemData.clientConditionItemNM;
                                x.isDefault = newItemData.isDefault;
                            }
                            else {
                                $scope.clientConditionItemDTOs.push({
                                    clientConditionItemID: newItemData.clientConditionItemID,
                                    clientConditionItemNM: newItemData.clientConditionItemNM,
                                    isDefault: newItemData.isDefault,
                                });
                            }
                        }
                        $scope.clientConditionItemForm.clientConditionItemID = newItemData.clientConditionItemID;
                        $scope.clientConditionItemForm.data = newItemData;
                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },

            cancel: function () {
                $('#frmClientConditionItemInfo').modal('hide');
            },

            ok: function () {
                if ($scope.clientConditionItemForm.data != null) {
                    $scope.clientConditionItemForm.editingItem.clientConditionItemID = $scope.clientConditionItemForm.data.clientConditionItemID;
                    $timeout(function () {
                        $("#conditionitem" + $scope.clientConditionItemForm.editingItem.clientOfferConditionDetailID).trigger('change');
                    });
                }
                $('#frmClientConditionItemInfo').modal('hide');
            }
        }
    }

    //
    // init
    //
    $scope.event.load();
}]);