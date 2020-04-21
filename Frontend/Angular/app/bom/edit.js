$(document).keypress(function (e) {
    if ($(e.target).is('input, textarea')) {
        return;
    }
    if (e.which === 83) { // 83: S
        var scope = angular.element(jQuery('body')).scope();
        scope.event.update();
    }
});
var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'furnindo-directive']);
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

    $scope.viewType = 'list';
    $scope.datePrices = [];
    $scope.supportYear = [];
    $scope.supportMonth = [];
    $scope.selection = {
        monthValue: null,
        yearValue: null
    };
    $scope.canApprove = context.canApprove;

    $scope.gridHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
        },
        isTriggered: false
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getInit(
                function (data) {
                    $scope.datePrices = data.data.datePrices;
                    for (var i = 0; i < $scope.datePrices.length; i++) {
                        var x = $scope.datePrices.length - 1;
                        if (i === x) {
                            $scope.selection.monthValue = $scope.datePrices[i].month;
                            $scope.selection.yearValue = $scope.datePrices[i].year;
                        }
                    }
                    $scope.event.load();
                },
                function (error) {
                }
            );
        },

        load: function () {
            jsonService.getBOM(
                context.id, context.workOrderID,
                function (data) {
                    if (data.message.type === 2) {
                        jsHelper.showMessage('warning', data.message.message);
                    } else {
                        $scope.data = data.data.data;
                        $scope.opSequenceDetails = data.data.opSequenceDetails;
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

                        //show in list format
                        $scope.event.viewBOM_ListFormat();
                        $scope.method.getYear();
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

        update: function () {
            //$event.preventDefault();
            if ($scope.editForm.$valid) {
                jsonService.update(
                    $scope.data.bomid,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            $scope.method.refresh(data.data.bomid);
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

        addNode: function (currentNote) {
            currentNote.bomdtOs.push({
                bomid: $scope.method.getNewID(),
                workOrderID: $scope.data.workOrderID,
                opSequenceID: $scope.data.opSequenceID,
                productionItemID: null,
                parentBOMID: currentNote.bomid,
                opSequenceDetailID: null,
                description: currentNote.description + '.' + (currentNote.bomdtOs.length + 1),
                revisionNumber: $scope.data.revisionNumber,
                isActive: $scope.data.isActive,
                isDeleted: false,
                qtyByUnit: null,
                bomdtOs: [],
                isEditing: false,
                visibleCRUD: true,
                displayIndex: $scope.method.getNextDisplayIndex(currentNote),
                productionItemTypeDisplayIndex: 1,
                productionItemTypeID: null,
            });
        },

        deleteNode: function (currentNote) {
            if (currentNote.bomid < 0) {
                $scope.selectedNote = null;
                $scope.method.getNoteByID(currentNote.parentBOMID, $scope.data);
                //delete node
                var j = -1;
                for (var i = 0; i < $scope.selectedNote.bomdtOs.length; i++) {
                    if ($scope.selectedNote.bomdtOs[i].bomid == currentNote.bomid) {
                        j = i;
                        break;
                    }
                }
                if (j >= 0) {
                    $scope.selectedNote.bomdtOs.splice(j, 1);
                }
            }
            else {
                $scope.method.setNoteIsDeleted(currentNote);
            }
        },

        editNote: function (currentNode) {
            //keep the node is editing
            currentNode.isEditing = true;
            //create a copy of node to edit
            var copyOfcurrentNode = angular.copy(currentNode);
            //call object to opration edit node
            $scope.editNodeForm.event.load(currentNode, copyOfcurrentNode);
        },

        addNodeByBOMID: function (bomid) {
            $scope.editNodeForm_ListViewFormat.event.load(bomid, true);
        },

        editNodeByBOMID: function (bomid) {
            $scope.editNodeForm_ListViewFormat.event.load(bomid, false);
        },

        deleteNodeByBOMID: function (bomid) {
            //get node by id
            $scope.selectedNote = null;
            $scope.method.getNoteByID(bomid, $scope.data);
            var currentNote = $scope.selectedNote;
            $scope.event.deleteNode(currentNote);
            $scope.event.viewBOM_ListFormat();
        },

        createImportTemplateGeneral: function () {
            $('#frmImportTemplate').modal('hide');
            jsonService.createImportTemplateGeneral(
                $scope.data.workOrderID,
                function (data) {
                    if (data.message.type == 2) {
                        jsHelper.processMessage(data.message);
                        return;
                    }
                    window.open(context.reportUrl + data.data, '');
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        createImportTemplate: function () {
            $('#frmImportTemplate').modal('hide');
            jsonService.createImportTemplate(
                $scope.data.workOrderID,
                function (data) {
                    if (data.message.type == 2) {
                        jsHelper.processMessage(data.message);
                        return;
                    }
                    window.open(context.reportUrl + data.data, '');
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        createBOMTemplateData: function (workOrderID, copyFromProductionProcessID) {
            //alert confirm
            bootbox.confirm({
                message: "All current BOM will be deleted and system will auto create BOM base on OP Sequence. Are you sure you want to created ?",
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
                        //reset bom
                        angular.forEach($scope.data.bomdtOs, function (bItem) {
                            $scope.method.setNoteIsDeleted(bItem);
                        });

                        //generate BOM
                        jsonService.createBOMTemplateData(
                            workOrderID, copyFromProductionProcessID,
                            function (data) {
                                if (data.message.type == 2) {
                                    jsHelper.processMessage(data.message);
                                    return;
                                }

                                //Clear DTO
                                $scope.data.bomdtOs.length = 0;
                                //$scope.data.bomdtOs = [];

                                //create BOM
                                var workOrder = data.data.workOrder;
                                var pieces = data.data.pieces;
                                var workCenters = data.data.workCenters;
                                var productionItems = data.data.productionItems;
                                var bomSources = data.data.bomSources;
                                var bomStandardSources = data.data.bomStandardSources

                                var pieceNo = 1;
                                var materialFinishing = [];

                                angular.forEach(pieces, function (piece) {
                                    //piece info
                                    var productionItem = $scope.method.getProductionItemByCode(productionItems, piece.pieceArticleCode);
                                    var productionItemID = productionItem.productionItemID;
                                    var productionItemUD = productionItem.productionItemUD;
                                    var productionItemNM = productionItem.productionItemNM;
                                    var productionItemTypeID = productionItem.productionItemTypeID;
                                    var productionItemTypeNM = productionItem.productionItemTypeNM;
                                    var productionItemUnitDTOs = productionItem.productionItemUnitDTOs;

                                    //unit info
                                    var unitID = productionItem.unitID;
                                    var unitNM = productionItem.unitNM;
                                    var conversionFactor = 1;
                                    //create piece node
                                    var piceNode = {
                                        bomid: $scope.method.getNewID(),
                                        workOrderID: $scope.data.workOrderID,
                                        opSequenceID: $scope.data.opSequenceID,
                                        productionItemID: productionItemID,
                                        unitID: unitID,
                                        parentBOMID: $scope.data.bomid,
                                        opSequenceDetailID: null,
                                        revisionNumber: $scope.data.revisionNumber,
                                        isActive: $scope.data.isActive,
                                        isDeleted: false,
                                        qtyByUnit: piece.quantity,
                                        bomdtOs: [],
                                        isEditing: false,
                                        visibleCRUD: true,
                                        displayIndex: 1,
                                        productionItemTypeDisplayIndex: 1,
                                        productionItemTypeID: productionItemTypeID,
                                        workOrderQnt: $scope.data.workOrderQnt,

                                        productionItemUD: piece.pieceArticleCode,
                                        productionItemNM: piece.pieceDescription,
                                        productionItemTypeNM: productionItemTypeNM,
                                        productionItemUnitDTOs: productionItemUnitDTOs,
                                        unitNM: unitNM,
                                        conversionFactor: conversionFactor,
                                    }
                                    $scope.data.bomdtOs.push(piceNode);

                                    //fetch workcenter to create component
                                    workCenters = workCenters.sort(function (a, b) { return b.sequenceIndexNumber - a.sequenceIndexNumber });
                                    var finalWorkCenterNM = workCenters[0].workCenterNM;

                                    angular.forEach(workCenters, function (workCenter) {
                                        var workCenterNM = workCenter.workCenterNM;
                                        var productionUD = '';
                                        var parentNode = null;
                                        //get productionUD base on workCenter
                                        if (workCenterNM == finalWorkCenterNM) { //workCenter final in the OP Sequence (example PACKING in FRAME-->PAINT-->WEAVING-->FINISHED-->PACKING)
                                            productionUD = $scope.method.getProductionUDByWorkCenter(piece.pieceArticleCode, finalWorkCenterNM); //piece.pieceArticleCode;
                                            //get parent node
                                            parentNode = piceNode;
                                        }
                                        else {
                                            if (workCenterNM !== 'FINISHING') {
                                                productionUD = $scope.method.getProductionUDByWorkCenter(piece.pieceArticleCode, workCenterNM);//workOrder.workOrderUD + '-' + piece.pieceArticleCode + '-' + workCenterNM;
                                                //get parent node
                                                var filterWorkCenter = workCenters.filter(function (o) { return o.sequenceIndexNumber == (workCenter.sequenceIndexNumber + 1) });
                                                var nextWorkCenter = null;
                                                //var nextWorkCenter = workCenters.filter(function (o) { return o.sequenceIndexNumber == (workCenter.sequenceIndexNumber + 1) });
                                                if (filterWorkCenter.length === 0 || filterWorkCenter[0].workCenterNM === 'FINISHING') {
                                                    nextWorkCenter = workCenters.filter(function (o) { return o.sequenceIndexNumber == (workCenter.sequenceIndexNumber + 2) })[0].workCenterNM;
                                                } else {
                                                    nextWorkCenter = filterWorkCenter[0].workCenterNM;
                                                }

                                                $scope.selectedNote = null;
                                                $scope.importFromExcel.getNodeInPieceBySequence(nextWorkCenter, pieceNo, $scope.data);
                                                parentNode = $scope.selectedNote;
                                            }
                                        }

                                        if (workCenterNM !== 'FINISHING') {
                                            //production item info
                                            var productionItem = $scope.method.getProductionItemByCode(productionItems, productionUD);
                                            var productionItemID = productionItem.productionItemID;
                                            var productionItemUD = productionItem.productionItemUD;
                                            if (workCenterNM === 'WEAVING') {
                                                productionItemUD = piece.pieceArticleCode.substring(0, 15) + '-WEAVING';
                                            }
                                            if (workCenterNM === 'ASSEMBLY') {
                                                productionItemUD = piece.pieceArticleCode.substring(0, 22) + '-ASSEMBLY';
                                            }

                                            var productionItemNM = productionItem.productionItemNM;
                                            var productionItemTypeID = productionItem.productionItemTypeID;
                                            var productionItemTypeNM = productionItem.productionItemTypeNM;
                                            var productionItemUnitDTOs = productionItem.productionItemUnitDTOs;

                                            //unit info
                                            var unitID = productionItem.unitID;
                                            var unitNM = productionItem.unitNM;
                                            var conversionFactor = 1;

                                            //create component node
                                            var componentNode = {
                                                //need save fields
                                                bomid: $scope.method.getNewID(),
                                                workOrderID: $scope.data.workOrderID,
                                                opSequenceID: $scope.data.opSequenceID,
                                                productionItemID: productionItemID,
                                                unitID: unitID,
                                                //parentBOMID: $scope.data.bomid,
                                                opSequenceDetailID: workCenter.opSequenceDetailID,
                                                revisionNumber: $scope.data.revisionNumber,
                                                isActive: $scope.data.isActive,
                                                isDeleted: false,
                                                qtyByUnit: piece.quantity,
                                                bomdtOs: [],
                                                isEditing: false,
                                                visibleCRUD: true,
                                                displayIndex: 1,
                                                productionItemTypeDisplayIndex: 1,
                                                productionItemTypeID: productionItemTypeID,
                                                productionTeamID: $scope.importFromExcel.getProductionTeamIDByWorkCenterName(workCenterNM),
                                                workOrderQnt: $scope.data.workOrderQnt,
                                                isExceptionAtConfirmFrameStatus: $scope.importFromExcel.getIsExceptionAtConfirmFrameStatusByWorkCenterName(workCenterNM),

                                                //need show fields
                                                productionItemUD: productionItemUD,
                                                productionItemNM: productionItemNM,
                                                productionItemTypeNM: productionItemTypeNM,
                                                productionItemUnitDTOs: productionItemUnitDTOs,
                                                unitNM: unitNM,
                                                conversionFactor: conversionFactor,
                                                opSequenceDetailNM: workCenterNM,
                                                productionTeamNM: $scope.importFromExcel.getProductionTeamNameByWorkCenterName(workCenterNM),
                                                piece: pieceNo // this field does not in BOM but we need to add to BOM to service for this function                                            
                                            }

                                            //mock to parent node
                                            componentNode.parentBOMID = parentNode.bomid;
                                            parentNode.bomdtOs.push(componentNode);
                                        }

                                        // Add material node
                                        if (copyFromProductionProcessID !== null) { // Copy material from BOM Standard
                                            // Get component in BOM by workCenter and Piece
                                            var componentSources = bomStandardSources.filter(function (o) { return o.workCenterNM == workCenterNM && o.productionItemUD.indexOf(piece.pieceUD) >= 0 });

                                            // Add material of finishing
                                            if (materialFinishing.length > 0) {
                                                angular.forEach(materialFinishing, function (materialItem) {
                                                    componentNode.bomdtOs.push({
                                                        bomid: $scope.method.getNewID(),
                                                        workOrderID: $scope.data.workOrderID,
                                                        opSequenceID: $scope.data.opSequenceID,
                                                        productionItemID: materialItem.productionItemID,
                                                        unitID: materialItem.unitID,
                                                        parentBOMID: componentNode.bomid,
                                                        opSequenceDetailID: null,
                                                        description: null,
                                                        revisionNumber: $scope.data.revisionNumber,
                                                        isActive: $scope.data.isActive,
                                                        isDeleted: false,
                                                        quantity: materialItem.quantity,
                                                        qtyByUnit: materialItem.qtyByUnit,
                                                        bomdtOs: [],
                                                        isEditing: false,
                                                        visibleCRUD: true,
                                                        displayIndex: 1,
                                                        productionItemTypeDisplayIndex: 1,
                                                        productionItemTypeID: materialItem.productionItemTypeID,
                                                        workOrderQnt: $scope.data.workOrderQnt,
                                                        wastagePercent: $scope.data.workOrderWastagePercent,
                                                        isExceptionAtConfirmFrameStatus: $scope.importFromExcel.getIsExceptionAtConfirmFrameStatusByWorkCenterName(workCenterNM),
                                                        productionItemUD: materialItem.productionItemUD,
                                                        productionItemNM: materialItem.productionItemNM,
                                                        productionItemTypeNM: materialItem.productionItemTypeNM,
                                                        productionItemUnitDTOs: materialItem.productionItemUnitDTOs,
                                                        unitNM: materialItem.unitNM,
                                                        conversionFactor: materialItem.conversionFactor,
                                                        wastagePercent: materialItem.wastagePercent
                                                    });
                                                });

                                                console.log(componentNode.bomdtOs);

                                                materialFinishing = [];
                                            }

                                            if (componentSources.length > 0) {
                                                var bomStandardID = componentSources[0].bomStandardID;

                                                // Find material belong component
                                                angular.forEach(bomStandardSources, function (bomSourceItem) {
                                                    if (workCenterNM !== 'FINISHING') {
                                                        if (bomSourceItem.parentBOMStandardID == bomStandardID && bomSourceItem.productionItemTypeID == 2) { //2:Material
                                                            componentNode.bomdtOs.push({
                                                                bomid: $scope.method.getNewID(),
                                                                workOrderID: $scope.data.workOrderID,
                                                                opSequenceID: $scope.data.opSequenceID,
                                                                productionItemID: bomSourceItem.productionItemID,
                                                                unitID: bomSourceItem.unitID,
                                                                parentBOMID: componentNode.bomid,
                                                                opSequenceDetailID: null,
                                                                description: null,
                                                                revisionNumber: $scope.data.revisionNumber,
                                                                isActive: $scope.data.isActive,
                                                                isDeleted: false,
                                                                quantity: bomSourceItem.quantity,
                                                                qtyByUnit: bomSourceItem.qtyByUnit,
                                                                bomdtOs: [],
                                                                isEditing: false,
                                                                visibleCRUD: true,
                                                                displayIndex: 1,
                                                                productionItemTypeDisplayIndex: 1,
                                                                productionItemTypeID: bomSourceItem.productionItemTypeID,
                                                                workOrderQnt: $scope.data.workOrderQnt,
                                                                wastagePercent: $scope.data.workOrderWastagePercent,
                                                                isExceptionAtConfirmFrameStatus: $scope.importFromExcel.getIsExceptionAtConfirmFrameStatusByWorkCenterName(workCenterNM),
                                                                productionItemUD: bomSourceItem.productionItemUD,
                                                                productionItemNM: bomSourceItem.productionItemNM,
                                                                productionItemTypeNM: bomSourceItem.productionItemTypeNM,
                                                                productionItemUnitDTOs: bomSourceItem.productionItemUnitDTOs,
                                                                unitNM: bomSourceItem.unitNM,
                                                                conversionFactor: bomSourceItem.conversionFactor,
                                                                wastagePercent: bomSourceItem.wastagePercent
                                                            });
                                                        }
                                                    } else {
                                                        if (bomSourceItem.parentBOMStandardID == bomStandardID && bomSourceItem.productionItemTypeID == 2) { //2:Material
                                                            materialFinishing.push({
                                                                bomid: $scope.method.getNewID(),
                                                                workOrderID: $scope.data.workOrderID,
                                                                opSequenceID: $scope.data.opSequenceID,
                                                                productionItemID: bomSourceItem.productionItemID,
                                                                unitID: bomSourceItem.unitID,
                                                                parentBOMID: null,
                                                                opSequenceDetailID: null,
                                                                description: null,
                                                                revisionNumber: $scope.data.revisionNumber,
                                                                isActive: $scope.data.isActive,
                                                                isDeleted: false,
                                                                quantity: bomSourceItem.quantity,
                                                                qtyByUnit: bomSourceItem.qtyByUnit,
                                                                bomdtOs: [],
                                                                isEditing: false,
                                                                visibleCRUD: true,
                                                                displayIndex: 1,
                                                                productionItemTypeDisplayIndex: 1,
                                                                productionItemTypeID: bomSourceItem.productionItemTypeID,
                                                                workOrderQnt: $scope.data.workOrderQnt,
                                                                wastagePercent: $scope.data.workOrderWastagePercent,
                                                                isExceptionAtConfirmFrameStatus: $scope.importFromExcel.getIsExceptionAtConfirmFrameStatusByWorkCenterName(workCenterNM),
                                                                productionItemUD: bomSourceItem.productionItemUD,
                                                                productionItemNM: bomSourceItem.productionItemNM,
                                                                productionItemTypeNM: bomSourceItem.productionItemTypeNM,
                                                                productionItemUnitDTOs: bomSourceItem.productionItemUnitDTOs,
                                                                unitNM: bomSourceItem.unitNM,
                                                                conversionFactor: bomSourceItem.conversionFactor,
                                                                wastagePercent: bomSourceItem.wastagePercent
                                                            });
                                                        }
                                                    }
                                                });

                                                console.log(materialFinishing);
                                            }
                                        }
                                    });

                                    //mark piece no for every piece
                                    pieceNo = pieceNo + 1;
                                });

                                $scope.$apply();
                                //show in list format
                                $scope.event.viewBOM_ListFormat();

                            },
                            function (error) {
                                jsHelper.showMessage('warning', error);
                            }
                        );
                    }
                }
            });

        },

        //viewBOM_ListFormat() {
        //    var bomList = {
        //        productItem: null,
        //        pieces: [],
        //    };
        //    //get product
        //    var productItem = angular.copy($scope.data);
        //    productItem.bomdtOs = [];///minnimize data
        //    bomList.productItem = productItem;

        //    //get piece
        //    var pieceNo = parseInt(1);
        //    angular.forEach($scope.data.bomdtOs, function (pItem) {
        //        var pieceItem = angular.copy(pItem);
        //        pieceItem.relation = '' + pieceNo;
        //        pieceItem.bomdtOs = [];//minnimize data
        //        //add pice to bom list
        //        bomList.pieces.push(pieceItem);

        //        //read op sequence & get component
        //        var componentNo = parseInt(1);
        //        var opSequenceDetails = $scope.opSequenceDetails.filter(function (o) { return o.opSequenceID == $scope.data.opSequenceID }).sort(function (a, b) { return b.sequenceIndexNumber - a.sequenceIndexNumber });

        //        angular.forEach(opSequenceDetails, function (opsItem) {
        //            //get node by op sequence detail
        //            $scope.selectedNote = null;
        //            $scope.method.getNodeByOPSequenceDetail(opsItem.opSequenceDetailNM, pItem); // after this function then node by sequence detail will be save in $scope.selectedNode

        //            //get component
        //            var component = angular.copy($scope.selectedNote);
        //            component.relation = '' + pieceNo + '.' + componentNo;
        //            component.bomdtOs = []; //minnimize data
        //            bomList.pieces.push(component);

        //            //get material 
        //            var materialNo = parseInt(1);
        //            angular.forEach($scope.selectedNote.bomdtOs, function (materialItem) {
        //                if (materialItem.productionItemTypeID == 2) {
        //                    var material = angular.copy(materialItem);
        //                    material.relation = '' + pieceNo + '.' + componentNo + '.' + materialNo;
        //                    bomList.pieces.push(material);
        //                    materialNo++;
        //                }                                                
        //            });
        //            componentNo++;
        //        });

        //        pieceNo++;
        //    });
        //    $scope.bomList = bomList;
        //    $scope.viewType = 'list';
        //},

        viewBOM_ListFormat: function () {
            $scope.viewType = 'list'
            $scope.bomList = [];
            $scope.method.parseBOMDataToBOMList($scope.data, $scope.bomList);
            $scope.bomList = $scope.bomList.filter(o => o.parentBOMID !== null && !o.isDeleted);
        },

        viewBOM_TreeFormat: function () {
            $scope.viewType = 'tree';
        },

        setDefaultWorkOrder: function () {
            jsonService.setDefaultWorkOrder(
                $scope.data.workOrderID,
                function (data) {
                    if (data.message.type != 2) {
                        bootbox.alert("BOM has just set default success");
                    }
                    else {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.$apply();
                }
            );
        },

        showExportExcel: function () {
            $('#frmExportExcelBom').modal('show');
        },

        showImportTemplate: function () {
            $('#frmImportTemplate').modal('show');
        },

        exportBOMToExcel: function () {
            if ($scope.selection.yearValue === null || $scope.selection.yearValue === '' || $scope.selection.yearValue === undefined) {
                jsHelper.showMessage('warning', 'Please select a Year.');
                return false;
            }
            if ($scope.selection.monthValue === null || $scope.selection.monthValue === '' || $scope.selection.monthValue === undefined) {
                jsHelper.showMessage('warning', 'Please select a Month.');
                return false;
            }
            var param = {
                year: $scope.selection.yearValue,
                month: $scope.selection.monthValue
            }
            jsonService.exportBomToExcel(
                context.id, param,
                function (data) {
                    window.open(context.reportUrl + data.data, '');
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            )
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            window.location = context.refreshUrl + id;
        },

        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },

        getNoteByID: function (id, data) {
            if (data.bomid == id) {
                $scope.selectedNote = data;
            }
            if (!$scope.selectedNote) {
                var i = 0;
                for (i = 0; i < data.bomdtOs.length; i++) {
                    $scope.method.getNoteByID(id, data.bomdtOs[i]);
                }
            }
        },

        setNoteIsDeleted: function (bomNode) {
            bomNode.isDeleted = true;
            var i = 0;
            for (i = 0; i < bomNode.bomdtOs.length; i++) {
                $scope.method.setNoteIsDeleted(bomNode.bomdtOs[i]);
            }
        },

        fetchBOM: function (data) {
            if (data.bomdtOs.length > 0) {
                var i = 0;
                for (i = 0; i < data.bomdtOs.length; i++) {
                    $scope.method.fetchBOM(data.bomdtOs[i]);
                }
            }
        },

        assignAutoCompleteProductionItem: function (currentNote) {
            $("#productItem" + currentNote.bomid).autocomplete({
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
                    currentNote.productionItemID = ui.item.id;
                    currentNote.unit = ui.item.unit;
                    currentNote.unitID = ui.item.unitID;
                    currentNote.unitNM = ui.item.unitNM;
                    currentNote.productionItemUnitDTOs = ui.item.productionItemUnits;
                    currentNote.productionItemTypeID = ui.item.productionItemTypeID;
                    $scope.$apply();
                }
            });
        },

        enableButtonCRUD: function (bom, value) {
            bom.visibleCRUD = value;
            var i = 0;
            for (i = 0; i < bom.bomdtOs.length; i++) {
                $scope.method.enableButtonCRUD(bom.bomdtOs[i], value);
            }
        },

        getNextDisplayIndex(currentNode) {
            nextDisplayIndex = 1;
            if (currentNode.bomdtOs.length > 0) {
                currentNode.bomdtOs.sort(function (a, b) { return b.displayIndex - a.displayIndex });
                nextDisplayIndex = currentNode.bomdtOs[0].displayIndex + 1;
            }
            return nextDisplayIndex;
        },

        upBOMToNewVersion: function (bom) {
            bom.bomid = -bom.bomid;
            if (bom.parentBOMID != null) bom.parentBOMID = -bom.parentBOMID;
            bom.revisionNumber = bom.revisionNumber + 1;
            bom.visibleCRUD = true;
            for (var i = 0; i < bom.bomdtOs.length; i++) {
                $scope.method.upBOMToNewVersion(bom.bomdtOs[i]);
            }
        },

        setNewID: function (bom) {
            if (bom.bomid > (-1) * $scope.newID) {
                $scope.newID = -bom.bomid;
            }
            for (var i = 0; i < bom.bomdtOs.length; i++) {
                $scope.method.setNewID(bom.bomdtOs[i]);
            }
        },

        getNodeByOPSequenceDetail: function (opSequenceDetailNM, data) {
            if (data.opSequenceDetailNM == opSequenceDetailNM) {
                $scope.selectedNote = data;
            }
            if (!$scope.selectedNote) {
                var i = 0;
                for (i = 0; i < data.bomdtOs.length; i++) {
                    $scope.method.getNodeByOPSequenceDetail(opSequenceDetailNM, data.bomdtOs[i]);
                }
            }
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

        getWorkCenterNMByID: function (opSequenceDetailID) {
            var result = '';
            if (opSequenceDetailID !== null) {
                var ops = $scope.opSequenceDetails.filter(o => o.opSequenceDetailID === opSequenceDetailID);
                if (ops !== null && ops.length > 0) {
                    result = ops[0].workCenterNM;
                }
            }
            return result;
        },

        getProductionItemTypeNMByID: function (productionItemTypeID) {
            var result = '';
            if (productionItemTypeID !== null) {
                var types = $scope.productionItemTypes.filter(o => o.productionItemTypeID === productionItemTypeID);
                if (types !== null && types.length > 0) {
                    result = types[0].productionItemTypeNM;
                }
            }
            return result;
        },


        getProductionTeamNMByID: function (productionTeamID) {
            var result = '';
            if (productionTeamID !== null) {
                var teams = $scope.productionTeams.filter(o => o.productionTeamID === productionTeamID);
                if (teams !== null && teams.length > 0) {
                    result = teams[0].productionTeamNM;
                }
            }
            return result;
        },

        getUnitByID: function (productionItemUnits, unitID) {
            var result = {
                productionItemID: null,
                unitID: null,
                unitNM: null,
                conversionFactor: null,
            }
            if (productionItemUnits !== null && unitID !== null) {
                var units = productionItemUnits.filter(o => o.unitID === unitID);
                if (units !== null && units.length > 0) {
                    result = units[0];
                }
            }
            return result;
        },

        getUnitByName: function (productionItemID, productionItems, unitNM) {
            var result = {
                productionItemID: null,
                unitID: null,
                unitNM: null,
                conversionFactor: null,
            }
            if (productionItemID !== null && productionItems !== null && unitNM !== null) {
                var productionItem = productionItems.filter(x => x.productionItemID === productionItemID);
                if (productionItem !== null && productionItem.length > 0) {
                    var productionItemUnitDTOs = productionItem[0].productionItemUnitDTOs;
                    var units = productionItemUnitDTOs.filter(o => o.unitNM === unitNM);
                    if (units !== null && units.length > 0) {
                        result = units[0];
                    }
                }
            }
            return result;
        },

        getProductionItemByCode: function (productionItems, productionItemUD) {
            var result = {
                productionItemID: null,
                productionItemUD: null,
                productionItemNM: null,
                productionItemTypeID: null,
                unitID: null,
                unitNM: null,
                productionItemUnitDTOs: null,
            };
            if (productionItems !== null && productionItemUD !== null) {
                var x = productionItems.filter(o => o.productionItemUD == productionItemUD);
                if (x !== null && x.length > 0) {
                    result = x[0];
                }
            }
            return result;
        },

        getYear: function () {
            $scope.supportYear = [];
            var x = parseInt(0);
            for (var i = 0; i < $scope.datePrices.length; i++) {
                if (x !== parseInt($scope.datePrices[i].year)) {
                    var currentItem = { 'yearID': parseInt($scope.datePrices[i].year), 'yearValue': parseInt($scope.datePrices[i].year) };
                    $scope.supportYear.push(currentItem);
                    x = parseInt($scope.datePrices[i].year);
                }
            }
            $scope.method.getMonth();
        },
        getMonth: function () {
            $scope.supportMonth = [];
            for (var i = 0; i < $scope.datePrices.length; i++) {
                if ($scope.datePrices[i].year === $scope.selection.yearValue) {
                    var currentItem = { 'monthID': $scope.datePrices[i].month, 'monthValue': $scope.datePrices[i].month };
                    $scope.supportMonth.push(currentItem);
                }
            }
        },

        getProductionUDByWorkCenter: function (articleCode, workCenterNM) {
            if (workCenterNM == 'FRAME') {
                return articleCode.substring(0, 6) + '-FRAME';
            }

            if (workCenterNM == 'PAINTED') {
                return articleCode.substring(0, 8) + '-PAINTED';
            }

            if (workCenterNM == 'WEAVING') {
                return articleCode.substring(0, 15) + '-WEAVING';
            }

            if (workCenterNM == 'FINISHING') {
                return articleCode.substring(0, 22) + '-FINISHING';
            }

            if (workCenterNM == 'ASSEMBLY') {
                return articleCode.substring(0, 22) + '-ASSEMBLY';
            }

            if (workCenterNM == 'PACKING') {
                return articleCode;
            }

            return '';
        },

        getDisabledDeleted: function (pItem) {
            var className = '';
            if (pItem.workOrderStatusID === 3 || pItem.workOrderStatusID === 4) {
                className = 'disabled';
            }
            else if (pItem.workOrderStatusID === 2 || pItem.workOrderStatusID === 5) {
                if (!$scope.canApprove) {
                    className = 'disabled';
                }
                else if (pItem.hasArising) {
                    className = 'disabled';
                }
            }
            return className;           
        }
    };

    //
    //edit node (Tree Format)
    //
    $scope.editNodeForm = {
        currentNode: null,
        copyOfcurrentNode: null,
        event: {
            load: function (currentNode, copyOfcurrentNode) {
                $scope.editNodeForm.currentNode = currentNode;
                $scope.editNodeForm.copyOfcurrentNode = copyOfcurrentNode;
                //assign autocomplete for the control of node
                $scope.method.assignAutoCompleteProductionItem($scope.editNodeForm.copyOfcurrentNode);
                //disable CRUD buttons
                $scope.method.enableButtonCRUD($scope.data, false);
            },
            cancel: function () {
                $scope.editNodeForm.currentNode.isEditing = false;
                $scope.method.enableButtonCRUD($scope.data, true);
            },
            ok: function () {
                var xCopy = $scope.editNodeForm.copyOfcurrentNode;
                //update value for currentNode
                $scope.editNodeForm.currentNode.productionItemID = xCopy.productionItemID;
                $scope.editNodeForm.currentNode.productionItemUD = xCopy.productionItemUD;
                $scope.editNodeForm.currentNode.productionItemNM = xCopy.productionItemNM;
                $scope.editNodeForm.currentNode.unit = xCopy.unit;
                $scope.editNodeForm.currentNode.unitID = xCopy.unitID;
                $scope.editNodeForm.currentNode.unitNM = xCopy.unitNM;
                $scope.editNodeForm.currentNode.productionItemUnitDTOs = xCopy.productionItemUnitDTOs;
                $scope.editNodeForm.currentNode.productThumbnailImageUrl = xCopy.productThumbnailImageUrl;
                $scope.editNodeForm.currentNode.productFullSizeImageUrl = xCopy.productFullSizeImageUrl;
                $scope.editNodeForm.currentNode.productionItemTypeID = xCopy.productionItemTypeID;

                if (xCopy.opSequenceDetailID != null) $scope.editNodeForm.currentNode.opSequenceDetailNM = $scope.opSequenceDetails.filter(function (o) { return o.opSequenceDetailID == xCopy.opSequenceDetailID })[0].opSequenceDetailNM;
                $scope.editNodeForm.currentNode.qtyByUnit = xCopy.qtyByUnit;
                $scope.editNodeForm.currentNode.opSequenceDetailID = xCopy.opSequenceDetailID;
                //set editing status is false
                $scope.editNodeForm.currentNode.isEditing = false;
                $scope.method.enableButtonCRUD($scope.data, true);
            },
        }
    }

    //
    //edit node (List Format)
    //
    $scope.editNodeForm_ListViewFormat = {
        currentNode: null,
        copyOfcurrentNode: null,
        isAddNew: null,
        event: {
            load: function (currentBOMID, isAddNew) {
                //get form status
                $scope.editNodeForm_ListViewFormat.isAddNew = isAddNew;
                //get node by bomid
                $scope.selectedNote = null;
                $scope.method.getNoteByID(currentBOMID, $scope.data);
                $scope.editNodeForm_ListViewFormat.currentNode = $scope.selectedNote;

                if (isAddNew) {
                    $scope.editNodeForm_ListViewFormat.copyOfcurrentNode = {
                        bomid: $scope.method.getNewID(),
                        workOrderID: $scope.data.workOrderID,
                        opSequenceID: $scope.data.opSequenceID,
                        productionItemID: null,
                        parentBOMID: $scope.editNodeForm_ListViewFormat.currentNode.bomid,
                        opSequenceDetailID: null,
                        description: null,
                        revisionNumber: $scope.data.revisionNumber,
                        isActive: $scope.data.isActive,
                        isDeleted: false,
                        qtyByUnit: null,
                        bomdtOs: [],
                        isEditing: false,
                        visibleCRUD: true,
                        displayIndex: $scope.method.getNextDisplayIndex($scope.editNodeForm_ListViewFormat.currentNode),
                        productionItemTypeDisplayIndex: 1,
                        productionItemTypeID: null,

                        productionItemTypeNM: '',
                        opSequenceDetailNM: '',
                        workOrderQnt: $scope.data.workOrderQnt,
                        wastagePercent: $scope.data.workOrderWastagePercent,
                    }
                }
                else {
                    $scope.editNodeForm_ListViewFormat.copyOfcurrentNode = angular.copy($scope.editNodeForm_ListViewFormat.currentNode);
                }
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
                        $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.productionItemID = ui.item.id;
                        $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.productionItemUD = ui.item.productionItemUD;
                        $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.productionItemNM = ui.item.productionItemNM;
                        $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.unit = ui.item.unit;
                        $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.unitID = ui.item.unitID;
                        $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.unitNM = ui.item.unitNM;
                        $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.productionItemUnitDTOs = ui.item.productionItemUnits;
                        $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.productionItemTypeID = ui.item.productionItemTypeID;
                        $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.productionItemTypeNM = ui.item.productionItemTypeNM;
                        $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.wastagePercent = ui.item.wastagePercent;
                        $scope.$apply();
                    }
                });

                $('#frmEditNodeForm_ListViewFormat').modal('show');
            },
            cancel: function () {
                $('#frmEditNodeForm_ListViewFormat').modal('hide');
            },
            ok: function () {
                var xCopy = $scope.editNodeForm_ListViewFormat.copyOfcurrentNode;
                if ($scope.editNodeForm_ListViewFormat.isAddNew) {
                    xCopy.opSequenceDetailNM = $scope.method.getWorkCenterNMByID(xCopy.opSequenceDetailID);
                    xCopy.productionTeamNM = $scope.method.getProductionTeamNMByID(xCopy.productionTeamID);
                    xCopy.unitNM = $scope.method.getUnitByID(xCopy.productionItemUnitDTOs, xCopy.unitID).unitNM,
                        xCopy.conversionFactor = $scope.method.getUnitByID(xCopy.productionItemUnitDTOs, xCopy.unitID).conversionFactor,

                        $scope.editNodeForm_ListViewFormat.currentNode.bomdtOs.push(xCopy);
                }
                else {
                    //update value for currentNode
                    $scope.editNodeForm_ListViewFormat.currentNode.productionItemID = xCopy.productionItemID;
                    $scope.editNodeForm_ListViewFormat.currentNode.productionItemUD = xCopy.productionItemUD;
                    $scope.editNodeForm_ListViewFormat.currentNode.productionItemNM = xCopy.productionItemNM;
                    $scope.editNodeForm_ListViewFormat.currentNode.unit = xCopy.unit;
                    $scope.editNodeForm_ListViewFormat.currentNode.unitID = xCopy.unitID;
                    $scope.editNodeForm_ListViewFormat.currentNode.unitNM = $scope.method.getUnitByID(xCopy.productionItemUnitDTOs, xCopy.unitID).unitNM,
                        $scope.editNodeForm_ListViewFormat.currentNode.conversionFactor = $scope.method.getUnitByID(xCopy.productionItemUnitDTOs, xCopy.unitID).conversionFactor,
                        $scope.editNodeForm_ListViewFormat.currentNode.productionItemUnitDTOs = xCopy.productionItemUnitDTOs;

                    $scope.editNodeForm_ListViewFormat.currentNode.productThumbnailImageUrl = xCopy.productThumbnailImageUrl;
                    $scope.editNodeForm_ListViewFormat.currentNode.productFullSizeImageUrl = xCopy.productFullSizeImageUrl;
                    $scope.editNodeForm_ListViewFormat.currentNode.productionItemTypeID = xCopy.productionItemTypeID;

                    $scope.editNodeForm_ListViewFormat.currentNode.opSequenceDetailNM = $scope.method.getWorkCenterNMByID(xCopy.opSequenceDetailID),
                        $scope.editNodeForm_ListViewFormat.currentNode.qtyByUnit = xCopy.qtyByUnit;
                    $scope.editNodeForm_ListViewFormat.currentNode.opSequenceDetailID = xCopy.opSequenceDetailID;
                    $scope.editNodeForm_ListViewFormat.currentNode.productionTeamID = xCopy.productionTeamID;
                    $scope.editNodeForm_ListViewFormat.currentNode.operatingTime = xCopy.operatingTime;
                    $scope.editNodeForm_ListViewFormat.currentNode.defaultCost = xCopy.defaultCost;
                    $scope.editNodeForm_ListViewFormat.currentNode.wastagePercent = xCopy.wastagePercent;
                    $scope.editNodeForm_ListViewFormat.currentNode.productionTeamNM = $scope.method.getProductionTeamNMByID(xCopy.productionTeamID),
                        $scope.editNodeForm_ListViewFormat.currentNode.description = xCopy.description;
                }
                $('#frmEditNodeForm_ListViewFormat').modal('hide');
                //view in list format
                $scope.event.viewBOM_ListFormat();
            },

            workCenterSelected: function (id) {
                var x = $scope.opSequenceDetails.filter(function (o) { return o.opSequenceDetailID == id });
                $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.operatingTime = x[0].operatingTime;
                $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.defaultCost = x[0].defaultCost;
                $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.workCenterID = x[0].workCenterID;
            },

            productionTeamSelected: function (id) {
                var x = $scope.productionTeams.filter(function (o) { return o.productionTeamID == id });
                if (x[0].operatingTime != null) {
                    $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.operatingTime = x[0].operatingTime;
                }
            },
        }
    }

    //
    //edit or addd production item
    //
    $scope.productionItemForm = {
        editingNotde: null,

        productionItemID: null,
        data: null,
        event: {
            load: function (productionItemID, copyOfcurrentNode) {
                $scope.productionItemForm.editingNotde = copyOfcurrentNode;
                $scope.productionItemForm.productionItemID = productionItemID;
                $scope.productionItemForm.event.getProductionItem(productionItemID);
            },

            getProductionItem: function (id) {
                jsonService.getProductionItem(
                    id,
                    function (data) {
                        $scope.productionItemForm.data = data.data;
                        $('#frmProductInfo').modal('show');
                        $scope.$apply();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                        $scope.productionItemForm.data = null;
                        $scope.$apply();
                    }
                );
            },
            updateProductionItem: function () {
                jsonService.updateProductionItem(
                    $scope.productionItemForm.productionItemID,
                    $scope.productionItemForm.data,
                    function (data) {
                        if (data.message.type === 0) {
                            //adjust data for editing node
                            var productItemData = data.data;
                            $scope.productionItemForm.editingNotde.productionItemID = productItemData.productionItemID;
                            $scope.productionItemForm.editingNotde.productionItemUD = productItemData.productionItemUD;
                            $scope.productionItemForm.editingNotde.productionItemNM = productItemData.productionItemNM;
                            $scope.productionItemForm.editingNotde.unit = productItemData.unit;
                            $scope.productionItemForm.editingNotde.unitID = productItemData.unitID;
                            $scope.productionItemForm.editingNotde.unitNM = productItemData.unitNM;
                            $scope.productionItemForm.editingNotde.productThumbnailImageUrl = productItemData.thumbnailLocation;
                            $scope.productionItemForm.editingNotde.productFullSizeImageUrl = productItemData.fileLocation;
                            $scope.$apply();
                            $('#frmProductInfo').modal('hide');
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },

            cancel: function () {
                $('#frmProductInfo').modal('hide');
            },

            changeImage: function ($event) {
                $event.preventDefault();
                masterUploader.multiSelect = true;
                masterUploader.imageOnly = true;
                masterUploader.callback = function () {
                    var scope = angular.element(jQuery('body')).scope();
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            scope.productionItemForm.data.thumbnailLocation = img.fileURL;
                            scope.productionItemForm.data.productImage_NewFile = img.filename;
                            scope.productionItemForm.data.productImage_HasChange = true;
                        }, null);
                    });
                };
                masterUploader.open();
            },
            removeImage: function ($event) {
                $scope.data.showroomItemThumbnailImage = '';
                $scope.data.imageFile_NewFile = '';
                $scope.data.imageFile_HasChange = true;
            },

        }
    }

    //
    //import from excel BOM file
    //
    $scope.importFromExcel = {
        load: function () {
            var input = document.createElement("input");
            input.setAttribute("type", "file");
            input.setAttribute("accept", ".xlsx");
            input.addEventListener("change", function (e) {
                //get file content
                var file = e.target.files[0];
                //
                var f = e.target.files[0];
                var reader = new FileReader();
                //var name = f.name;
                reader.onload = function (e) {
                    var data = e.target.result;

                    var result;
                    var workbook = XLSX.read(data, { type: 'binary' });

                    var sheet_name_list = workbook.SheetNames;
                    sheet_name_list.forEach(function (y) { /* iterate through sheets */
                        //Convert the cell value to Json
                        var roa = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                        if (roa.length > 0) {
                            result = roa;
                        }
                    });
                    //console.log(result);
                    /*
                    /* check product of workOrder is match with product of excel file(root row)
                    */
                    var articleCodeImported = result[0].Code;
                    if ($scope.data.productArticleCode != articleCodeImported) {
                        bootbox.alert('Import failure. ArticleCode of product on excel file is not matched with ArticleCode of product on WorkOrder. Please check excel file again.');
                        return;
                    }
                    /*
                     *  Check Piece must be a number
                     */
                    for (var i = 0; i < result.length; i++) {
                        var item = result[i];
                        var number;
                        if (i >= 1) {
                            number = parseInt(item.Piece);
                            if (isNaN(number)) {
                                //bootbox.alert('Hey! Please check the excel file, column Piece does not contain letters. ');
                                jsHelper.showMessage('warning', 'Please check the excel file, column Piece does not contain letters. (Kiểm tra lại file excel, cột Piece không được chứa chữ cái)');
                                return;
                            }
                        }
                    }

                    /*
                    /* BEGIN TO IMPORT
                    */

                    /*
                    /* get list product and import data
                    */

                    //find the end of WorkCenter of Sequence & attach to result
                    var opSequenceDetails = $scope.opSequenceDetails.filter(function (o) { return o.opSequenceID == $scope.data.opSequenceID }).sort(function (a, b) { return b.sequenceIndexNumber - a.sequenceIndexNumber });
                    var workCenterEnd = opSequenceDetails[0].opSequenceDetailNM;
                    angular.forEach(result, function (bItem) {
                        //find the end of workcenter in sequence
                        if (bItem.Sequence == workCenterEnd) {
                            bItem.isEndOfSequence = true;
                        }
                        else {
                            bItem.isEndOfSequence = false;
                        }

                        //assign default factory warehouse for every workcenter
                        bItem.defaultFactoryWarehouseID = $scope.importFromExcel.getDefaultFactoryWarehouseIDByWorkCenterName(bItem.Sequence);
                    })
                    //debugger
                    //ceate product item before import bom
                    jsonService.getListProductionItem(
                        result,
                        function (data) {
                            var productionItems = data.data;
                            /*
                                import product after get list product succcess
                            */

                            if (data.message.type === 2) { //error: not find material in system
                                bootbox.alert(data.message.message);
                                return;
                            }

                            //reset bom
                            //$scope.data.bomdtOs = [];
                            angular.forEach($scope.data.bomdtOs, function (bItem) {
                                $scope.method.setNoteIsDeleted(bItem);
                            });

                            //get list piece
                            var pieces = [];
                            var indexs = [];
                            angular.forEach(result, function (item) {
                                if (item.Piece != undefined) {
                                    if (indexs.indexOf(item.Piece) < 0) {
                                        indexs.push(item.Piece);
                                        pieces.push({
                                            piece: item.Piece,
                                            code: item.Code,
                                            unit: item.Unit,
                                            description: item.Description,
                                            productionItemTypeNM: item.Type,
                                        });
                                    }
                                }
                            });
                            //create nodes
                            angular.forEach(pieces, function (piece) {
                                //production item info
                                var productionItem = $scope.method.getProductionItemByCode(productionItems, piece.code);
                                var productionItemID = productionItem.productionItemID;
                                var productionItemUD = productionItem.productionItemUD;
                                var productionItemNM = productionItem.productionItemNM;
                                var productionItemTypeID = productionItem.productionItemTypeID;
                                var productionItemTypeNM = productionItem.productionItemTypeNM;
                                var productionItemUnitDTOs = productionItem.productionItemUnitDTOs;

                                //unit info
                                //var unitItem = $scope.method.getUnitByName(productionItemID, productionItems, piece.unit);
                                //var unitID = unitItem.unitID;
                                //var unitNM = unitItem.unitNM;
                                //var conversionFactor = unitItem.conversionFactor;

                                var unitID = 25;
                                var unitNM = 'PCS';
                                var conversionFactor = 1;

                                var piceNode = {
                                    //need save fields
                                    bomid: $scope.method.getNewID(),
                                    workOrderID: $scope.data.workOrderID,
                                    opSequenceID: $scope.data.opSequenceID,
                                    productionItemID: productionItemID,
                                    unitID: unitID,
                                    parentBOMID: $scope.data.bomid,
                                    opSequenceDetailID: null,
                                    description: null,
                                    revisionNumber: $scope.data.revisionNumber,
                                    isActive: $scope.data.isActive,
                                    isDeleted: false,
                                    qtyByUnit: 1,
                                    bomdtOs: [],
                                    isEditing: false,
                                    visibleCRUD: true,
                                    displayIndex: 1,
                                    productionItemTypeDisplayIndex: 1,
                                    productionItemTypeID: productionItemTypeID,
                                    workOrderQnt: $scope.data.workOrderQnt,

                                    //need show fields
                                    productionItemUD: productionItemUD,
                                    productionItemNM: productionItemNM,
                                    productionItemTypeNM: productionItemTypeNM,
                                    productionItemUnitDTOs: productionItemUnitDTOs,
                                    unitNM: unitNM,
                                    conversionFactor: conversionFactor, //very important
                                }
                                $scope.data.bomdtOs.push(piceNode);
                                //create component, material item
                                var opSequenceDetails = $scope.opSequenceDetails.sort(function (a, b) { return b.sequenceIndexNumber - a.sequenceIndexNumber });
                                angular.forEach(opSequenceDetails, function (sequenceItem) {
                                    var workCenterNM = sequenceItem.workCenterNM;

                                    angular.forEach(result, function (excelProductItem) {
                                        if (excelProductItem.Piece == piece.piece && excelProductItem.Sequence == workCenterNM) {
                                            //production item info
                                            var productionItem = $scope.method.getProductionItemByCode(productionItems, excelProductItem.Code);
                                            var productionItemID = productionItem.productionItemID;
                                            var productionItemUD = productionItem.productionItemUD;
                                            var productionItemNM = productionItem.productionItemNM;
                                            var productionItemTypeID = productionItem.productionItemTypeID;
                                            var productionItemTypeNM = productionItem.productionItemTypeNM;
                                            var productionItemUnitDTOs = productionItem.productionItemUnitDTOs;

                                            //unit info
                                            //var unitItem = $scope.method.getUnitByName(productionItemID, productionItems, excelProductItem.Unit);
                                            //var unitID = unitItem.unitID;
                                            //var unitNM = unitItem.unitNM;
                                            //var conversionFactor = unitItem.conversionFactor;
                                            var unitID = 25;
                                            var unitNM = 'PCS';
                                            var conversionFactor = 1;

                                            var componentNode = {
                                                //need save fields
                                                bomid: $scope.method.getNewID(),
                                                workOrderID: $scope.data.workOrderID,
                                                opSequenceID: $scope.data.opSequenceID,
                                                productionItemID: productionItemID,
                                                unitID: unitID,
                                                //parentBOMID: $scope.data.bomid,
                                                opSequenceDetailID: $scope.importFromExcel.getOPSequenceDetailID(opSequenceDetails, workCenterNM),
                                                description: null,
                                                revisionNumber: $scope.data.revisionNumber,
                                                isActive: $scope.data.isActive,
                                                isDeleted: false,
                                                qtyByUnit: excelProductItem.Norm,
                                                bomdtOs: [],
                                                isEditing: false,
                                                visibleCRUD: true,
                                                displayIndex: 1,
                                                productionItemTypeDisplayIndex: 1,
                                                productionItemTypeID: productionItemTypeID,
                                                productionTeamID: $scope.importFromExcel.getProductionTeamIDByWorkCenterName(excelProductItem.Sequence),
                                                workOrderQnt: $scope.data.workOrderQnt,
                                                isExceptionAtConfirmFrameStatus: $scope.importFromExcel.getIsExceptionAtConfirmFrameStatusByWorkCenterName(workCenterNM),

                                                //need show field
                                                productionItemUD: productionItemUD,
                                                productionItemNM: productionItemNM,
                                                productionItemTypeNM: productionItemTypeNM,
                                                productionItemUnitDTOs: productionItemUnitDTOs,
                                                unitNM: unitNM,
                                                conversionFactor: conversionFactor,
                                                opSequenceDetailNM: excelProductItem.Sequence,
                                                productionTeamNM: $scope.importFromExcel.getProductionTeamNameByWorkCenterName(excelProductItem.Sequence),
                                                piece: excelProductItem.Piece // this field does not in BOM but we need to add to BOM to service for import from BOM excel file                                                
                                            }
                                            if (excelProductItem.Type == 'Component') {
                                                //get parent node to push component in
                                                var nextSequence = $scope.importFromExcel.getNextSequence(opSequenceDetails, workCenterNM);
                                                if (nextSequence == '') { // is end of sequence
                                                    componentNode.parentBOMID = piceNode.bomid;
                                                    piceNode.bomdtOs.push(componentNode);
                                                }
                                                else {
                                                    $scope.selectedNote = null;
                                                    $scope.importFromExcel.getNodeInPieceBySequence(nextSequence, excelProductItem.Piece, $scope.data);
                                                    var parentComponentNode = $scope.selectedNote;

                                                    componentNode.parentBOMID = parentComponentNode.bomid;
                                                    parentComponentNode.bomdtOs.push(componentNode);
                                                }
                                                //add material node
                                                angular.forEach(result, function (excelMaterialItem) {
                                                    if (excelMaterialItem.Type == 'Material' && excelMaterialItem.Sequence == excelProductItem.Sequence && excelMaterialItem.Piece == excelProductItem.Piece) {
                                                        //production item info
                                                        var productionItem = $scope.method.getProductionItemByCode(productionItems, excelMaterialItem.Code);
                                                        var productionItemID = productionItem.productionItemID;
                                                        var productionItemUD = productionItem.productionItemUD;
                                                        var productionItemNM = productionItem.productionItemNM;
                                                        var productionItemTypeID = productionItem.productionItemTypeID;
                                                        var productionItemTypeNM = productionItem.productionItemTypeNM;
                                                        var productionItemUnitDTOs = productionItem.productionItemUnitDTOs;

                                                        //unit info
                                                        var unitItem = $scope.method.getUnitByName(productionItemID, productionItems, excelMaterialItem.Unit);
                                                        var unitID = unitItem.unitID;
                                                        var unitNM = unitItem.unitNM;
                                                        var conversionFactor = unitItem.conversionFactor;

                                                        componentNode.bomdtOs.push({
                                                            //need save fields
                                                            bomid: $scope.method.getNewID(),
                                                            workOrderID: $scope.data.workOrderID,
                                                            opSequenceID: $scope.data.opSequenceID,
                                                            productionItemID: productionItemID,
                                                            unitID: unitID,
                                                            parentBOMID: componentNode.bomid,
                                                            opSequenceDetailID: null,
                                                            description: null,
                                                            revisionNumber: $scope.data.revisionNumber,
                                                            isActive: $scope.data.isActive,
                                                            isDeleted: false,
                                                            qtyByUnit: excelMaterialItem.Norm,
                                                            bomdtOs: [],
                                                            isEditing: false,
                                                            visibleCRUD: true,
                                                            displayIndex: 1,
                                                            productionItemTypeDisplayIndex: 1,
                                                            productionItemTypeID: productionItemTypeID,
                                                            workOrderQnt: $scope.data.workOrderQnt,
                                                            wastagePercent: $scope.data.workOrderWastagePercent,
                                                            isExceptionAtConfirmFrameStatus: $scope.importFromExcel.getIsExceptionAtConfirmFrameStatusByWorkCenterName(workCenterNM),

                                                            //need show field
                                                            productionItemUD: productionItemUD,
                                                            productionItemNM: productionItemNM,
                                                            productionItemTypeNM: productionItemTypeNM,
                                                            productionItemUnitDTOs: productionItemUnitDTOs,
                                                            unitNM: unitNM,
                                                            conversionFactor: conversionFactor,
                                                            wastagePercent: productionItem.wastagePercent
                                                        });
                                                    }
                                                });
                                            }
                                        }
                                    });
                                });

                            });
                            $scope.$apply();
                            //show in list format
                            $scope.event.viewBOM_ListFormat();
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                };
                reader.readAsArrayBuffer(f);

            }, false);
            input.click();
        },

        importMaterialForPackingWorkCenter: function () {
            debugger;
            var input = document.createElement("input");
            input.setAttribute("type", "file");
            input.setAttribute("accept", ".xlsx");
            input.addEventListener("change", function (e) {
                alert("ggg");
                //get file content
                var file = e.target.files[0];
                //
                var f = e.target.files[0];
                var reader = new FileReader();
                //var name = f.name;
                reader.onload = function (e) {
                    var data = e.target.result;

                    var result;
                    var workbook = XLSX.read(data, { type: 'binary' });

                    var sheet_name_list = workbook.SheetNames;
                    sheet_name_list.forEach(function (y) { /* iterate through sheets */
                        //Convert the cell value to Json
                        var roa = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                        if (roa.length > 0) {
                            result = roa;
                        }
                    });

                    /*
                    /* BEGIN TO IMPORT
                    */

                    /*
                    /* get list product and import data
                    */

                    //ceate product item before import bom
                    jsonService.getListProductionItem(
                        result,
                        function (data) {
                            var productionItems = data.data;

                            //get list piece
                            var pieces = [];
                            var indexs = [];
                            angular.forEach(result, function (item) {
                                if (item.Piece != undefined) {
                                    if (indexs.indexOf(item.Piece) < 0) {
                                        indexs.push(item.Piece);
                                        pieces.push({
                                            pieceNo: item.Piece,
                                            code: item.Code,
                                        });
                                    }
                                }
                            });
                            //get workCenter exception
                            var opSequenceDetails = $scope.opSequenceDetails.sort(function (a, b) { return b.sequenceIndexNumber - a.sequenceIndexNumber });
                            var workCenterNM = opSequenceDetails.filter(o => o.isExceptionAtConfirmFrameStatus)[0].workCenterNM;

                            angular.forEach(pieces, function (pieceItem) {
                                var pieceNode = $scope.data.bomdtOs.filter(o => o.productionItemUD === pieceItem.code);
                                if (pieceNode !== null && pieceNode.length > 0) {

                                    //get exception node
                                    $scope.selectedNote = null;
                                    $scope.importFromExcel.getNodeByWorkCenterInParentNode(workCenterNM, pieceNode[0]);
                                    var exceptionNode = $scope.selectedNote;

                                    //push material to exception node
                                    var marterialItems = result.filter(o => o.Piece === pieceItem.pieceNo && o.Type === 'Material');
                                    angular.forEach(marterialItems, function (materialItem) {
                                        //production item info
                                        var productionItem = $scope.method.getProductionItemByCode(productionItems, materialItem.Code);
                                        var productionItemID = productionItem.productionItemID;
                                        var productionItemUD = productionItem.productionItemUD;
                                        var productionItemNM = productionItem.productionItemNM;
                                        var productionItemTypeID = productionItem.productionItemTypeID;
                                        var productionItemTypeNM = productionItem.productionItemTypeNM;
                                        var productionItemUnitDTOs = productionItem.productionItemUnitDTOs;

                                        //unit info
                                        var unitItem = $scope.method.getUnitByName(productionItemID, productionItems, materialItem.Unit);
                                        var unitID = unitItem.unitID;
                                        var unitNM = unitItem.unitNM;
                                        var conversionFactor = unitItem.conversionFactor;

                                        //get material node in component (in this case component node is exception node, ex:PACKING)
                                        var x = exceptionNode.bomdtOs.filter(o => o.productionItemUD === productionItemUD);
                                        if (x !== null && x.length > 0) {
                                            x[0].qtyByUnit = materialItem.Norm;
                                            x[0].unitID = unitID;
                                            x[0].unitNM = unitNM;
                                            x[0].conversionFactor = conversionFactor;
                                        }
                                        else {
                                            exceptionNode.bomdtOs.push({
                                                //need save fields
                                                bomid: $scope.method.getNewID(),
                                                workOrderID: $scope.data.workOrderID,
                                                opSequenceID: $scope.data.opSequenceID,
                                                productionItemID: productionItemID,
                                                productionItemTypeID: productionItemTypeID,
                                                unitID: unitID,
                                                parentBOMID: exceptionNode.bomid,
                                                opSequenceDetailID: null,
                                                description: null,
                                                revisionNumber: $scope.data.revisionNumber,
                                                isActive: $scope.data.isActive,
                                                isDeleted: false,
                                                qtyByUnit: materialItem.Norm,
                                                bomdtOs: [],
                                                isEditing: false,
                                                visibleCRUD: true,
                                                displayIndex: 1,
                                                productionItemTypeDisplayIndex: 1,
                                                workOrderQnt: $scope.data.workOrderQnt,
                                                wastagePercent: $scope.data.workOrderWastagePercent,

                                                //show fields
                                                productionItemUD: productionItemUD,
                                                productionItemNM: productionItemNM,
                                                productionItemTypeNM: productionItemTypeNM,
                                                productionItemUnitDTOs: productionItemUnitDTOs,
                                                unitNM: unitNM,
                                                conversionFactor: conversionFactor,
                                            });
                                        }
                                    });
                                }
                            });
                            $scope.$apply();
                            //show in list format
                            $scope.event.viewBOM_ListFormat();
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                };
                reader.readAsArrayBuffer(f);

            }, false);
            input.click();
        },

        getNextSequence: function (opSequences, sequence) {
            var index = 0;
            angular.forEach(opSequences, function (item) {
                if (item.opSequenceDetailNM == sequence) {
                    index = item.sequenceIndexNumber;
                }
            });
            var x = opSequences.filter(function (o) { return o.sequenceIndexNumber == (index + 1) });
            if (x != null && x.length > 0) {
                return x[0].opSequenceDetailNM;
            }
            else {
                return '';
            }
        },

        getNodeInPieceBySequence: function (opSequenceDetailNM, piece, data) {
            if (data.opSequenceDetailNM == opSequenceDetailNM && data.piece == piece) {
                $scope.selectedNote = data;
            }
            if (!$scope.selectedNote) {
                var i = 0;
                for (i = 0; i < data.bomdtOs.length; i++) {
                    $scope.importFromExcel.getNodeInPieceBySequence(opSequenceDetailNM, piece, data.bomdtOs[i]);
                }
            }
        },

        getProductionItemIDByCode(productionItems, code) {
            for (var i = 0; i < productionItems.length; i++) {
                if (productionItems[i].productionItemUD == code) {
                    return productionItems[i].productionItemID;
                }
            }
        },

        getProductionItemNMByCode(productionItems, code) {
            for (var i = 0; i < productionItems.length; i++) {
                if (productionItems[i].productionItemUD == code) {
                    return productionItems[i].productionItemNM;
                }
            }
        },

        getProductionItemUnitByCode(productionItems, code) {
            for (var i = 0; i < productionItems.length; i++) {
                if (productionItems[i].productionItemUD == code) {
                    return productionItems[i].unit;
                }
            }
        },

        getProductionItemTypeByCode(productionItems, code) {
            for (var i = 0; i < productionItems.length; i++) {
                if (productionItems[i].productionItemUD == code) {
                    return productionItems[i].productionItemTypeID;
                }
            }
        },

        getOPSequenceDetailID: function (opSequences, sequence) {
            for (var i = 0; i < opSequences.length; i++) {
                if (opSequences[i].opSequenceDetailNM == sequence) {
                    return opSequences[i].opSequenceDetailID;
                }
            }
        },

        getProductionTeamIDByWorkCenterName: function (workCenterName) {
            /*
                note that opSequenceDetailNM is same as workCenterNM
                in SQL we write: workCenter.workCenterNM AS opSequenceDetailNM
            */
            var result = null;
            var x = $scope.opSequenceDetails.filter(function (o) { return o.opSequenceDetailNM == workCenterName });
            if (x != null && x.length > 0) {
                var workCenterID = x[0].workCenterID;
                var teams = $scope.productionTeams.filter(function (o) { return o.workCenterID == workCenterID && o.isDefault == true });
                if (teams != null && teams.length > 0) {
                    result = teams[0].productionTeamID;
                }
            }
            return result;
        },

        getProductionTeamNameByWorkCenterName: function (workCenterName) {
            /*
                note that opSequenceDetailNM is same as workCenterNM
                in SQL we write: workCenter.workCenterNM AS opSequenceDetailNM
            */
            var result = '';
            var x = $scope.opSequenceDetails.filter(function (o) { return o.opSequenceDetailNM == workCenterName });
            if (x != null && x.length > 0) {
                var workCenterID = x[0].workCenterID;
                var teams = $scope.productionTeams.filter(function (o) { return o.workCenterID == workCenterID && o.isDefault == true });
                if (teams != null && teams.length > 0) {
                    result = teams[0].productionTeamNM;
                }
            }
            return result;
        },

        getDefaultFactoryWarehouseIDByWorkCenterName: function (workCenterName) {
            /*
                note that opSequenceDetailNM is same as workCenterNM
                in SQL we write: workCenter.workCenterNM AS opSequenceDetailNM
            */
            var result = null;
            var x = $scope.opSequenceDetails.filter(function (o) { return o.opSequenceDetailNM == workCenterName });
            if (x != null && x.length > 0) {
                result = x[0].defaultFactoryWarehouseID;
            }
            return result;
        },

        getIsExceptionAtConfirmFrameStatusByWorkCenterName: function (workCenterName) {
            var result = null;
            var x = $scope.opSequenceDetails.filter(function (o) { return o.opSequenceDetailNM == workCenterName });
            if (x != null && x.length > 0) {
                result = x[0].isExceptionAtConfirmFrameStatus;
            }
            return result;
        },

        getNodeByWorkCenterInParentNode: function (workCenterNM, parentNode) {
            if (parentNode.opSequenceDetailNM == workCenterNM) {
                $scope.selectedNote = parentNode;
            }
            for (var i = 0; i < parentNode.bomdtOs.length; i++) {
                $scope.importFromExcel.getNodeByWorkCenterInParentNode(workCenterNM, parentNode.bomdtOs[i]);
            }
        },
    },

    //
    //show workOrders of BOM are same model and production process of BOM Standard is same productID
    //
    $scope.productionProcessForm = {
            productionProcesses: [],
            event: {
                load: function () {
                    jsonService.getProductionProcessFormData(
                        $scope.data.modelID,
                        function (data) {
                            if (data.message.type != 2) {
                                //get production process that contain BOM Standard
                                $scope.productionProcessForm.productionProcesses = data.data.productionProcesses;

                                //apply data
                                $scope.$apply();
                                $('#frmProductionProcess').modal('show');
                            }
                            else {
                                jsHelper.processMessage(data.message);
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                            $scope.$apply();
                        }
                    );
                },
                cancel: function () {
                    $('#frmProductionProcess').modal('hide');
                },
                ok: function () {
                    $('#frmProductionProcess').modal('hide');
                },

                copyBOM: function (workOrderID, copyFromProductionProcessID) {
                    $('#frmProductionProcess').modal('hide');
                    $scope.event.createBOMTemplateData(workOrderID, copyFromProductionProcessID)
                }
            }
        }

    //
    // init
    //
    $scope.event.init();
}]);