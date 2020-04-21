$(document).keypress(function (e) {
    if ($(e.target).is('input, textarea')) {
        return;
    }
    if (e.which === 83) { // 83: S
        var scope = angular.element(jQuery('body')).scope();
        scope.event.update();
    }
});
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', function ($scope, $timeout) {
    //
    // property
    //
    $scope.data = null;
    $scope.opSequenceDetails = null;
    $scope.productionItemTypes = null;
    $scope.productionTeams = null;
    $scope.newID = -1;
    $scope.selectedNote = null;
    $scope.Cost = [];

    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.getBOM(
                context.id, context.workOrderID,
                function (data) {
                    $scope.data = data.data.data;
                    console.log(data.data);
                    $scope.Cost = data.data.workOrderCosts;
                    $scope.productionItemTypes = data.data.productionItemTypes;
                    $scope.productionTeams = data.data.productionTeams;
                    if (context.id == 0) {
                        $scope.data.bomid = $scope.method.getNewID();//allway rootNoted is negative in case create new
                        $scope.data.description = "BOM";
                        $scope.data.revisionNumber = 1;
                        $scope.data.isActive = true;
                        $scope.data.visibleCRUD = true;
                        $scope.data.displayIndex = 1;
                        $scope.data.isCreatingNewVersionOfBOM = false;
                    }
                    $scope.$apply();
                  
                    jQuery('#content').show();
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
        delete: function (item) {
            var index = $scope.Cost.indexOf(item);
            $scope.Cost.splice(index, 1); 
        },
        update: function (item) {
            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.workOrderID,
                    $scope.Cost,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        //if (data.message.type === 0) {
                        //    $scope.method.refresh(data.data.bomid);
                        //}
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
        edit: function (item) {
            $scope.editNodeForm_ListViewFormat.event.load(item);
        },
        addNode: function ($event) {
            $event.preventDefault();
            $scope.editNodeForm_ListViewFormat.event.load(null);
            $('#frmEditNodeForm_ListViewFormat').modal('show');

        },                        
    }; 
    //
    //edit node (List Format)
    //
    $scope.editNodeForm_ListViewFormat = {
        copyOfcurrentNode: null,
        currentnode:null,
        isNew:false,
        event: {
            load: function (item) {
                if (item == null) {
                    $scope.editNodeForm_ListViewFormat.copyOfcurrentNode = null,
                    $scope.editNodeForm_ListViewFormat.isNew = true,
                    //assign autocomplete production item text box
                    $("#productItem").autocomplete({
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
                                    }
                                }),
                                dataType: 'JSON',
                                url: context.getProductionItemUrl,
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
                            $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.workOrderCostID = $scope.method.getNewID();
                            $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.workOrderID = context.workOrderID;
                            $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.productionItemID = ui.item.productionItemID;
                            $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.productionItemUD = ui.item.productionItemUD;
                            $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.productionItemNM = ui.item.productionItemNM;
                            $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.unit = ui.item.unit;
                            $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.unitID = ui.item.unitID;
                            $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.unitNM = ui.item.unitNM;
                            $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.productionItemTypeID = ui.item.productionItemTypeID;
                            $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.productionItemTypeNM = ui.item.productionItemTypeNM;
                            $scope.$apply();
                        }
                    });
                }
                else {
                    $scope.editNodeForm_ListViewFormat.copyOfcurrentNode = angular.copy(item);
                    $scope.editNodeForm_ListViewFormat.isNew = false;
                    $scope.editNodeForm_ListViewFormat.currentnode = item;
                }
                $('#frmEditNodeForm_ListViewFormat').modal('show');
            },
            cancel: function () {
                $('#frmEditNodeForm_ListViewFormat').modal('hide');
            },
            ok: function () {
                $('#frmEditNodeForm_ListViewFormat').modal('hide');
                if ($scope.editNodeForm_ListViewFormat.isNew == true)
                    $scope.Cost.push($scope.editNodeForm_ListViewFormat.copyOfcurrentNode);
                else {
                    $scope.editNodeForm_ListViewFormat.currentnode.workOrderCostID = $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.workOrderCostID;
                    $scope.editNodeForm_ListViewFormat.currentnode.workOrderID = $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.workOrderID;
                    $scope.editNodeForm_ListViewFormat.currentnode.productionItemID = $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.productionItemID;
                    $scope.editNodeForm_ListViewFormat.currentnode.productionItemUD = $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.productionItemUD;
                    $scope.editNodeForm_ListViewFormat.currentnode.productionItemNM = $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.productionItemNM
                    $scope.editNodeForm_ListViewFormat.currentnode.unit = $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.unit;
                    $scope.editNodeForm_ListViewFormat.currentnode.unitID = $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.unitID;
                    $scope.editNodeForm_ListViewFormat.currentnode.unitNM = $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.unitNM;
                    $scope.editNodeForm_ListViewFormat.currentnode.productionItemTypeID = $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.productionItemTypeID;
                    $scope.editNodeForm_ListViewFormat.currentnode.productionItemTypeNM = $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.productionItemTypeNM;
                    $scope.editNodeForm_ListViewFormat.currentnode.qty = $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.qty;
                    $scope.editNodeForm_ListViewFormat.currentnode.costPrice = $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.costPrice;

                }
            },
        }
    };
    $scope.method = {
        refresh: function (id) {
            window.location = context.refreshUrl + id;
        },

        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },
    };
    $scope.getTotal = function () {
        var total = 0;
        for (var i = 0; i < $scope.Cost.length; i++) {
            var product = $scope.Cost[i];
            total += (product.costPrice * product.qty);
        }
        return total;
    }
    //
    // init
    //
    $scope.event.load();
}]);