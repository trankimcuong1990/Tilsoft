var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {

    //
    // init service
    //
    dataService.serviceUrl = context.serviceUrl;
    dataService.supportServiceUrl = context.supportServiceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;
    $scope.opSequenceDetails = [];
    $scope.productionItemTypes = [];
    $scope.productionTeams = [];
    $scope.newID = -1;
    $scope.selectedNote = [];
    $scope.dateProductionPrices = [];
    $scope.supportYear = [];
    $scope.supportMonth = [];
    $scope.selection = {
        monthValue: null,
        yearValue: null,
        isLocked: null
    };
    $scope.listLock = [
        { isLocked: true, name: 'Yes' },
        { isLocked: false, name: 'No' }
    ];

    $scope.currency = [
        { value: 'USD', name: 'USD' },
        { value: 'EUR', name: 'EUR' },
    ];
    $scope.currencySelect = "USD";

    //Set date value
    var currentdate = new Date();
    var day = currentdate.getDate();
    if (day < 10) {
        day = '0' + day;
    }
    var month = currentdate.getMonth();
    if (month < 10) {
        month = '0' + month;
    }
    $scope.today = day + "/" + month + "/" + currentdate.getFullYear();

    $scope.seasonSpec = 10;
    $scope.priceAmountVND = 0;
    $scope.priceAmountOther = 0;
    // exchangeRate changed
    $scope.exchangeRateChange = function () {
        if (!isNaN($scope.exchangeRate)) {
            $scope.priceAmountOther = ($scope.priceAmountVND / $scope.exchangeRate).toFixed(2);
        }
        else {
            $scope.priceAmountOther = 0;
        }

        if ($scope.exchangeRate == "") {
            $scope.priceAmountOther = 0;
        }
    }
    //seasonSpec change
    $scope.seasonSpecChange = function () {
        $scope.priceAmountVND = ($scope.TotalAmount + ($scope.TotalAmount * ($scope.seasonSpec / 100))).toFixed(2);
        if ($scope.exchangeRate != null && $scope.exchangeRate.toString() != "")
            $scope.priceAmountOther = ($scope.priceAmountVND / $scope.exchangeRate).toFixed(2);
        else {
            $scope.priceAmountOther = 0;
        }
    }

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.getInit(
                function (data) {
                    $scope.dateProductionPrices = data.data.dateProductionPrices;
                    for (var i = 0; i < $scope.dateProductionPrices.length; i++) {
                        var x = $scope.dateProductionPrices.length - 1;
                        if (i === x) {
                            $scope.selection.monthValue = $scope.dateProductionPrices[i].month;
                            $scope.selection.yearValue = $scope.dateProductionPrices[i].year;
                            $scope.selection.isLocked = false;
                        }
                    }
                    $scope.event.load();
                },
                function (error) {
                }
            );
        },
        load: function () {
            if ($scope.selection.yearValue === null || $scope.selection.yearValue === '' || $scope.selection.yearValue === undefined) {
                jsHelper.showMessage('warning', 'Please select a Year.');
                return false;
            }
            if ($scope.selection.monthValue === null || $scope.selection.monthValue === '' || $scope.selection.monthValue === undefined) {
                jsHelper.showMessage('warning', 'Please select a Month.');
                return false;
            }
            function DateTemp(dateStr) {
                var parts = dateStr.split("/")
                return new Date(parts[2], parts[1] - 1, parts[0])
            }
            var param = {
                productionProcessID: context.productionProcessID,
                year: DateTemp($scope.today).getFullYear(),
                month: (DateTemp($scope.today).getMonth())+1,
                isLocked: $scope.selection.isLocked,
            }
            dataService.getViewData(
                context.id, param,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.opSequenceDetails = data.data.opSequenceDetails;
                    $scope.productionItemTypes = data.data.productionItemTypes;
                    $scope.productionTeams = data.data.productionTeams;
                    if (context.id == 0) {
                        $scope.data.bomStandardID = $scope.method.getNewID();//allway rootNoted is negative in case create new
                        $scope.data.displayIndex = 1;
                    }
                    //show in list format
                    $scope.event.viewBOM_ListFormat();
                    $scope.method.getYear();

                    jQuery('#content').show();
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    $scope.data = [];
                }
            );
        },

        createImportTemplate: function () {
            dataService.createImportTemplate(
                $scope.data.productionProcessID,
                function (data) {
                    window.open(context.backendReportUrl + data.data, '');
                },
                function (error) {
                    //do nothing
                }
            );
        },

        exportBOMStandardToExcel: function () {
            dataService.exportBOMStandardToExcel(
                $scope.data.bomStandardID,
                function (data) {
                    window.open(context.backendReportUrl + data.data, '');
                },
                function (error) {
                    //do nothing
                }
            );
        },

        createBOMTemplateData: function (copyFrompPoductionProcessID) {
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
                        angular.forEach($scope.data.bomStandardViewDTOs, function (bItem) {
                            $scope.method.setNoteIsDeleted(bItem);
                        });

                        //generate BOM
                        dataService.createBOMTemplateData(
                            $scope.data.productionProcessID, copyFrompPoductionProcessID,
                            function (data) {
                                //create BOM
                                var productionProcess = data.data.productionProcess;
                                var pieces = data.data.pieces;
                                var workCenters = data.data.workCenters;
                                var productionItems = data.data.productionItems;
                                var bomSources = data.data.bomStandardSources;

                                var pieceNo = 1;
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
                                        //to be save info
                                        bomStandardID: $scope.method.getNewID(),
                                        productionProcessID: $scope.data.productionProcessID,
                                        productionItemID: productionItemID,
                                        unitID: unitID,
                                        parentBOMStandardID: $scope.data.bomStandardID,
                                        opSequenceDetailID: null,
                                        isDeleted: false,
                                        qtyByUnit: piece.quantity,
                                        bomStandardViewDTOs: [],
                                        displayIndex: 1,
                                        //sub info                                        
                                        opSequenceID: $scope.data.opSequenceID,
                                        productionItemUD: piece.pieceArticleCode,
                                        productionItemNM: piece.pieceDescription,
                                        productionItemTypeID: productionItemTypeID,
                                        productionItemTypeNM: productionItemTypeNM,
                                        productionItemUnitDTOs: productionItemUnitDTOs,
                                        unitNM: unitNM,
                                        conversionFactor: conversionFactor,
                                    }
                                    $scope.data.bomStandardViewDTOs.push(piceNode);

                                    //fetch workcenter to create component
                                    workCenters = workCenters.sort(function (a, b) { return b.sequenceIndexNumber - a.sequenceIndexNumber });
                                    var finalWorkCenterNM = workCenters[0].workCenterNM;

                                    angular.forEach(workCenters, function (workCenter) {
                                        var workCenterNM = workCenter.workCenterNM;
                                        var productionUD = '';
                                        var parentNode = null;
                                        //get productionUD base on workCenter
                                        if (workCenterNM == finalWorkCenterNM) { //workCenter final in the OP Sequence (example PACKING in FRAME-->PAINT-->WEAVING-->FINISHED-->PACKING)
                                            productionUD = piece.pieceArticleCode;
                                            //get parent node
                                            parentNode = piceNode;
                                        }
                                        else {
                                            productionUD = productionProcess.productionProcessID + '-' + piece.pieceArticleCode + '-' + workCenterNM;
                                            //get parent node
                                            var nextWorkCenter = workCenters.filter(function (o) { return o.sequenceIndexNumber == (workCenter.sequenceIndexNumber + 1) })[0].workCenterNM;
                                            $scope.selectedNote = null;
                                            $scope.importFromExcel.getNodeInPieceBySequence(nextWorkCenter, pieceNo, $scope.data);
                                            parentNode = $scope.selectedNote;
                                        }

                                        //production item info
                                        var productionItem = $scope.method.getProductionItemByCode(productionItems, productionUD);
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

                                        //create component node
                                        var componentNode = {
                                            //tobe save info
                                            bomStandardID: $scope.method.getNewID(),
                                            productionProcessID: $scope.data.productionProcessID,
                                            productionItemID: productionItemID,
                                            unitID: unitID,
                                            //parentBOMStandardID: $scope.data.bomStandardID,
                                            opSequenceDetailID: workCenter.opSequenceDetailID,
                                            isDeleted: false,
                                            qtyByUnit: piece.quantity,
                                            bomStandardViewDTOs: [],
                                            displayIndex: 1,
                                            productionTeamID: $scope.importFromExcel.getProductionTeamIDByWorkCenterName(workCenterNM),
                                            //sub info
                                            productionItemUD: productionUD,
                                            productionItemNM: productionItemNM,
                                            productionItemTypeNM: productionItemTypeNM,
                                            productionItemTypeID: productionItemTypeID,
                                            productionItemUnitDTOs: productionItemUnitDTOs,
                                            unitNM: unitNM,
                                            conversionFactor: conversionFactor,
                                            opSequenceID: $scope.data.opSequenceID,
                                            workCenterNM: workCenterNM,
                                            productionTeamNM: $scope.importFromExcel.getProductionTeamNameByWorkCenterName(workCenterNM),
                                            piece: pieceNo // this field does not in BOM but we need to add to BOM to service for this function                                            
                                        }

                                        //mock to parent node
                                        componentNode.parentBOMStandardID = parentNode.bomStandardID;
                                        parentNode.bomStandardViewDTOs.push(componentNode);

                                        //add material node
                                        var componentSources = bomSources.filter(function (o) { return o.workCenterNM == workCenterNM && o.productionItemUD.indexOf(piece.pieceUD) >= 0 });
                                        if (componentSources.length > 0) {
                                            var bomStandardID = componentSources[0].bomStandardID;
                                            angular.forEach(bomSources, function (bomSourceItem) {
                                                if (bomSourceItem.parentBOMStandardID == bomStandardID && bomSourceItem.productionItemTypeID == 2) { //2:Material
                                                    console.log(bomSourceItem);
                                                    componentNode.bomStandardViewDTOs.push({
                                                        //tobe save  info
                                                        bomStandardID: $scope.method.getNewID(),
                                                        productionProcessID: $scope.data.productionProcessID,
                                                        productionItemID: bomSourceItem.productionItemID,
                                                        unitID: bomSourceItem.unitID,
                                                        parentBOMStandardID: componentNode.bomStandardID,
                                                        opSequenceDetailID: null,
                                                        description: null,
                                                        isDeleted: false,
                                                        quantity: bomSourceItem.quantity,
                                                        qtyByUnit: bomSourceItem.qtyByUnit,
                                                        bomStandardViewDTOs: [],
                                                        displayIndex: 1,
                                                        //sub info                                                        
                                                        opSequenceID: $scope.data.opSequenceID,
                                                        productionItemUD: bomSourceItem.productionItemUD,
                                                        productionItemNM: bomSourceItem.productionItemNM,
                                                        productionItemTypeID: bomSourceItem.productionItemTypeID,
                                                        productionItemTypeNM: bomSourceItem.productionItemTypeNM,
                                                        productionItemUnitDTOs: bomSourceItem.productionItemUnitDTOs,
                                                        unitNM: bomSourceItem.unitNM,
                                                        conversionFactor: bomSourceItem.conversionFactor,
                                                    });
                                                }
                                            });
                                        }
                                    });

                                    //mark piece no for every piece
                                    pieceNo = pieceNo + 1;
                                });

                                //show in list format
                                $scope.event.viewBOM_ListFormat();

                            },
                            function (error) {
                                //do nothing
                            }
                        );
                    }
                }
            });

        },

        viewBOM_ListFormat: function () {
            $scope.bomList = [];
            $scope.method.parseBOMDataToBOMList($scope.data, $scope.bomList);
            $scope.bomList = $scope.bomList.filter(o => o.parentBOMStandardID !== null && !o.isDeleted);
            $scope.method.totalCostEveryWorkCenter($scope.bomList);
        },

        setBOMStandardDefault: function () {
            dataService.setBOMStandardDefault(
                $scope.data.productionProcessID,
                function (data) {
                    bootbox.alert("BOM has just set default success");
                },
                function (error) {
                    //do nothing
                }
            );
        },
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        },

        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },

        getNoteByID: function (id, data) {
            if (data.bomStandardID == id) {
                $scope.selectedNote = data;
            }
            if (!$scope.selectedNote) {
                var i = 0;
                for (i = 0; i < data.bomStandardViewDTOs.length; i++) {
                    $scope.method.getNoteByID(id, data.bomStandardViewDTOs[i]);
                }
            }
        },

        setNoteIsDeleted: function (bomNode) {
            bomNode.isDeleted = true;
            var i = 0;
            for (i = 0; i < bomNode.bomStandardViewDTOs.length; i++) {
                $scope.method.setNoteIsDeleted(bomNode.bomStandardViewDTOs[i]);
            }
        },

        fetchBOM: function (data) {
            if (data.bomStandardViewDTOs.length > 0) {
                var i = 0;
                for (i = 0; i < data.bomStandardViewDTOs.length; i++) {
                    $scope.method.fetchBOM(data.bomStandardViewDTOs[i]);
                }
            }
        },


        setNewID: function (bom) {
            if (bom.bomStandardID > (-1) * $scope.newID) {
                $scope.newID = -bom.bomStandardID;
            }
            for (var i = 0; i < bom.bomStandardViewDTOs.length; i++) {
                $scope.method.setNewID(bom.bomStandardViewDTOs[i]);
            }
        },

        getNodeInPieceByWorkcenter: function (workCenterNM, piece, resultNode) {
            if (piece.workCenterNM == workCenterNM) {
                resultNode.push(piece);
            }
            for (var i = 0; i < piece.bomStandardViewDTOs.length; i++) {
                $scope.method.getNodeInPieceByWorkcenter(workCenterNM, piece.bomStandardViewDTOs[i], resultNode);
            }
        },

        parseBOMDataToBOMList: function (bomData, result) {
            if (bomData != null && bomData.bomStandardViewDTOs != null) {
                //push to list result
                result.push(bomData);

                //it just only assign piece index for child of root node
                if (bomData.parentBOMStandardID == null) {
                    for (var i = 0; i < bomData.bomStandardViewDTOs.length; i++) {
                        bomData.bomStandardViewDTOs[i].pieceIndex = i + 1;
                    }
                }

                //assign number if child bom item for every child bom so we can sort to make sure code read from materil to component (material don't have chidl bom anymore and component can have child bom)
                angular.forEach(bomData.bomStandardViewDTOs, function (item) {
                    item.countChildBOM = item.bomStandardViewDTOs.length;
                    if (item.pieceIndex == undefined) {
                        item.pieceIndex = bomData.pieceIndex;
                    }
                });

                //sort base on number item of child bom
                var bomSorted = bomData.bomStandardViewDTOs.sort(function (a, b) { return a.countChildBOM - b.countChildBOM });
                angular.forEach(bomSorted, function (item) {
                    $scope.method.parseBOMDataToBOMList(item, result);
                });
            }
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
                var x = productionItems.filter(o => o.productionItemUD === productionItemUD);
                if (x !== null && x.length > 0) {
                    result = x[0];
                }
            }
            return result;
        },

        getYear: function () {
            $scope.supportYear = [];
            var x = parseInt(0);
            for (var i = 0; i < $scope.dateProductionPrices.length; i++) {
                if (x !== parseInt($scope.dateProductionPrices[i].year)) {
                    var currentItem = { 'yearID': parseInt($scope.dateProductionPrices[i].year), 'yearValue': parseInt($scope.dateProductionPrices[i].year) };
                    $scope.supportYear.push(currentItem);
                    x = parseInt($scope.dateProductionPrices[i].year);
                }
            }
            $scope.method.getMonth();
        },
        getMonth: function () {
            $scope.supportMonth = [];
            for (var i = 0; i < $scope.dateProductionPrices.length; i++) {
                if ($scope.dateProductionPrices[i].year === $scope.selection.yearValue) {
                    var currentItem = { 'monthID': $scope.dateProductionPrices[i].month, 'monthValue': $scope.dateProductionPrices[i].month };
                    $scope.supportMonth.push(currentItem);
                }
            }
        },

        totalCostEveryWorkCenter: function (data) {
            //price
            var Price = 0;

            for (var i = data.length - 1; i > 0; i--) {
                var item = data[i];
                if (item.productionItemTypeID === 3) {//Piece
                    item.price = Price;
                    $scope.TotalAmount = Price;
                    $scope.priceAmountVND = ($scope.TotalAmount + ($scope.TotalAmount * ($scope.seasonSpec / 100))).toFixed(2);
                    if ($scope.exchangeRate != null && $scope.exchangeRate != '')
                        $scope.priceAmountOther = ($scope.priceAmountVND / $scope.exchangeRate).toFixed(2);

                    if (item.workCenterID === null) {
                        Price = 0;
                    }
                }
                if (item.productionItemTypeID === 2) { //Material
                    Price += item.price;
                }
                if (item.productionItemTypeID === 1) { //Component
                    item.price = Price;
                }
            }
        }
    };

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
                console.log($scope.selectedNote);
                $scope.editNodeForm_ListViewFormat.currentNode = $scope.selectedNote;

                if (isAddNew) {
                    $scope.editNodeForm_ListViewFormat.copyOfcurrentNode = {
                        bomStandardID: $scope.method.getNewID(),
                        productionProcessID: $scope.data.productionProcessID,
                        opSequenceID: $scope.data.opSequenceID,
                        productionItemID: null,
                        unitID: null,
                        parentBOMStandardID: $scope.editNodeForm_ListViewFormat.currentNode.bomStandardID,
                        opSequenceDetailID: null,
                        description: null,
                        isDeleted: false,
                        quantity: null,
                        qtyByUnit: null,
                        price: null,
                        totalPrice: null,
                        bomStandardViewDTOs: [],
                        productionItemTypeID: null,
                        productionItemTypeNM: '',
                        workCenterNM: '',
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
                    if (xCopy.opSequenceDetailID != null) {
                        xCopy.workCenterNM = $scope.opSequenceDetails.filter(function (o) { return o.opSequenceDetailID == xCopy.opSequenceDetailID })[0].workCenterNM;
                    }
                    if (xCopy.productionTeamID != null) {
                        $scope.editNodeForm_ListViewFormat.currentNode.productionTeamNM = $scope.productionTeams.filter(function (o) { return o.productionTeamID == xCopy.productionTeamID })[0].productionTeamNM;
                    }
                    xCopy.unitNM = $scope.method.getUnitByID(xCopy.productionItemUnitDTOs, xCopy.unitID).unitNM,
                        xCopy.conversionFactor = $scope.method.getUnitByID(xCopy.productionItemUnitDTOs, xCopy.unitID).conversionFactor,
                        $scope.editNodeForm_ListViewFormat.currentNode.bomStandardViewDTOs.push(xCopy);
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
                    $scope.editNodeForm_ListViewFormat.currentNode.productionItemTypeID = xCopy.productionItemTypeID;
                    $scope.editNodeForm_ListViewFormat.currentNode.productionItemTypeNM = xCopy.productionItemTypeNM;

                    if (xCopy.opSequenceDetailID != null) $scope.editNodeForm_ListViewFormat.currentNode.workCenterNM = $scope.opSequenceDetails.filter(function (o) { return o.opSequenceDetailID == xCopy.opSequenceDetailID })[0].workCenterNM;
                    $scope.editNodeForm_ListViewFormat.currentNode.qtyByUnit = xCopy.qtyByUnit;
                    $scope.editNodeForm_ListViewFormat.currentNode.opSequenceDetailID = xCopy.opSequenceDetailID;
                    $scope.editNodeForm_ListViewFormat.currentNode.productionTeamID = xCopy.productionTeamID;
                    $scope.editNodeForm_ListViewFormat.currentNode.operatingTime = xCopy.operatingTime;
                    $scope.editNodeForm_ListViewFormat.currentNode.defaultCost = xCopy.defaultCost;
                    if (xCopy.productionTeamID != null) $scope.editNodeForm_ListViewFormat.currentNode.productionTeamNM = $scope.productionTeams.filter(function (o) { return o.productionTeamID == xCopy.productionTeamID })[0].productionTeamNM;
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

                    /*
                    /* check product of production process is match with product of excel file(root row)
                    */
                    var articleCodeImported = result[0].Code;
                    if ($scope.data.productArticleCode != articleCodeImported) {
                        bootbox.alert('Import failure. ArticleCode of product on excel file is not matched with ArticleCode of product on a production process. Please check excel file again.');
                        return;
                    }

                    /*
                    /* BEGIN TO IMPORT
                    */

                    /*
                    /* get list product and import data
                    */

                    //find the end of WorkCenter of Sequence & attach to result
                    var opSequenceDetails = $scope.opSequenceDetails.sort(function (a, b) { return b.sequenceIndexNumber - a.sequenceIndexNumber });
                    var workCenterEnd = opSequenceDetails[0].workCenterNM;
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

                    //ceate product item before import bom
                    dataService.getListProductionItem(
                        result,
                        function (data) {
                            var productionItems = data.data;
                            /*
                                import product after get list product succcess
                            */

                            //if (data.message.type === 2) { //error: not find material in system
                            //    bootbox.alert(data.message.message);
                            //    return;
                            //}

                            //reset bom
                            angular.forEach($scope.data.bomStandardViewDTOs, function (bItem) {
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

                                //create piece node
                                var piceNode = {
                                    //to be save info
                                    bomStandardID: $scope.method.getNewID(),
                                    productionProcessID: $scope.data.productionProcessID,
                                    productionItemID: productionItemID,
                                    unitID: unitID,
                                    parentBOMStandardID: $scope.data.bomStandardID,
                                    opSequenceDetailID: null,
                                    isDeleted: false,
                                    qtyByUnit: 1,
                                    bomStandardViewDTOs: [],
                                    displayIndex: 1,

                                    //sub info
                                    productionItemTypeID: productionItemTypeID,
                                    productionItemTypeNM: productionItemTypeNM,
                                    productionItemUD: productionItemUD,
                                    productionItemNM: productionItemNM,
                                    productionItemUnitDTOs: productionItemUnitDTOs,
                                    unitNM: unitNM,
                                    conversionFactor: conversionFactor, //very important
                                    opSequenceID: $scope.data.opSequenceID,
                                }
                                $scope.data.bomStandardViewDTOs.push(piceNode);
                                //create component, material item
                                var opSequenceDetails = $scope.opSequenceDetails.sort(function (a, b) { return b.sequenceIndexNumber - a.sequenceIndexNumber });
                                angular.forEach(opSequenceDetails, function (sequenceItem) {
                                    angular.forEach(result, function (excelProductItem) {
                                        if (excelProductItem.Piece == piece.piece && excelProductItem.Sequence == sequenceItem.workCenterNM) {
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
                                                //to be save info
                                                bomStandardID: $scope.method.getNewID(),
                                                productionProcessID: $scope.data.productionProcessID,
                                                productionItemID: productionItemID,
                                                unitID: unitID,
                                                //parentBOMStandardID: $scope.data.parentBOMStandardID,
                                                opSequenceDetailID: $scope.importFromExcel.getOPSequenceDetailID(opSequenceDetails, sequenceItem.workCenterNM),
                                                isDeleted: false,
                                                qtyByUnit: excelProductItem.Norm,
                                                bomStandardViewDTOs: [],
                                                displayIndex: 1,
                                                productionTeamID: $scope.importFromExcel.getProductionTeamIDByWorkCenterName(excelProductItem.Sequence),

                                                //view info
                                                productionItemTypeID: productionItemTypeID,
                                                productionItemTypeNM: productionItemTypeNM,
                                                productionItemUD: productionItemUD,
                                                productionItemNM: productionItemNM,
                                                productionItemUnitDTOs: productionItemUnitDTOs,
                                                unitNM: unitNM,
                                                conversionFactor: conversionFactor, //very important
                                                opSequenceID: $scope.data.opSequenceID,
                                                workCenterNM: excelProductItem.Sequence,
                                                productionTeamNM: $scope.importFromExcel.getProductionTeamNameByWorkCenterName(excelProductItem.Sequence),
                                                piece: excelProductItem.Piece // this field does not in BOM but we need to add to BOM to service for import from BOM excel file                                                
                                            }
                                            if (excelProductItem.Type == 'Component') {
                                                //get parent node to push component in
                                                var nextSequence = $scope.importFromExcel.getNextSequence(opSequenceDetails, sequenceItem.workCenterNM);
                                                if (nextSequence == '') { // is end of sequence
                                                    componentNode.parentBOMStandardID = piceNode.bomStandardID;
                                                    piceNode.bomStandardViewDTOs.push(componentNode);
                                                }
                                                else {
                                                    $scope.selectedNote = null;
                                                    $scope.importFromExcel.getNodeInPieceBySequence(nextSequence, excelProductItem.Piece, $scope.data);
                                                    var parentComponentNode = $scope.selectedNote;

                                                    componentNode.parentBOMStandardID = parentComponentNode.bomStandardID;
                                                    parentComponentNode.bomStandardViewDTOs.push(componentNode);
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

                                                        componentNode.bomStandardViewDTOs.push({
                                                            //to be save info
                                                            bomStandardID: $scope.method.getNewID(),
                                                            productionProcessID: $scope.data.productionProcessID,
                                                            productionItemID: productionItemID,
                                                            unitID: unitID,
                                                            parentBOMStandardID: componentNode.bomStandardID,
                                                            opSequenceDetailID: null,
                                                            isDeleted: false,
                                                            qtyByUnit: excelMaterialItem.Norm,
                                                            bomStandardViewDTOs: [],
                                                            displayIndex: 1,

                                                            //sub info
                                                            productionItemTypeID: productionItemTypeID,
                                                            productionItemTypeNM: productionItemTypeNM,
                                                            productionItemUD: productionItemUD,
                                                            productionItemNM: productionItemNM,
                                                            productionItemUnitDTOs: productionItemUnitDTOs,
                                                            unitNM: unitNM,
                                                            conversionFactor: conversionFactor,
                                                            opSequenceID: $scope.data.opSequenceID,
                                                        });
                                                    }
                                                });
                                            }
                                        }

                                    });
                                });

                            });
                            //show in list format
                            $scope.event.viewBOM_ListFormat();
                        },
                        function (error) {
                            console.log(error);
                            jsHelper.showMessage('warning', error);
                        }
                    );
                };
                reader.readAsArrayBuffer(f);

            }, false);
            input.click();
        },

        getNextSequence: function (opSequences, workCenterNM) {
            var index = 0;
            angular.forEach(opSequences, function (item) {
                if (item.workCenterNM == workCenterNM) {
                    index = item.sequenceIndexNumber;
                }
            });
            var x = opSequences.filter(function (o) { return o.sequenceIndexNumber == (index + 1) });
            if (x != null && x.length > 0) {
                return x[0].workCenterNM;
            }
            else {
                return '';
            }
        },

        getNodeInPieceBySequence: function (workCenterNM, piece, data) {
            if (data.workCenterNM == workCenterNM && data.piece == piece) {
                $scope.selectedNote = data;
            }
            if (!$scope.selectedNote) {
                for (var i = 0; i < data.bomStandardViewDTOs.length; i++) {
                    $scope.importFromExcel.getNodeInPieceBySequence(workCenterNM, piece, data.bomStandardViewDTOs[i]);
                }
            }
        },

        getOPSequenceDetailID: function (opSequenceDetails, workCenterNM) {
            for (var i = 0; i < opSequenceDetails.length; i++) {
                if (opSequenceDetails[i].workCenterNM == workCenterNM) {
                    return opSequenceDetails[i].opSequenceDetailID;
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

        getProductionTeamIDByWorkCenterName: function (workCenterName) {
            /*
                note that opSequenceDetailNM is same as workCenterNM
                in SQL we write: workCenter.workCenterNM AS opSequenceDetailNM
            */
            var result = null;
            var x = $scope.opSequenceDetails.filter(function (o) { return o.workCenterNM == workCenterName });
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
            var x = $scope.opSequenceDetails.filter(function (o) { return o.workCenterNM == workCenterName });
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
            var x = $scope.opSequenceDetails.filter(function (o) { return o.workCenterNM == workCenterName });
            if (x != null && x.length > 0) {
                result = x[0].defaultFactoryWarehouseID;
            }
            return result;
        }

    },

        //
        //show production proces from are same model
        //
        $scope.productionProcessForm = {
            productionProcesses: [],
            event: {
                load: function () {

                    dataService.getProductionProcessByModel(
                        $scope.data.modelID,
                        function (data) {
                            $scope.productionProcessForm.productionProcesses = data.data;
                            $scope.productionProcessForm.productionProcesses = $scope.productionProcessForm.productionProcesses.filter(function (o) { return o.productionProcessID != $scope.data.productionProcessID && o.hasBOM });
                            $('#frmProductionProcess').modal('show');
                        },
                        function (error) {
                            //do nothing
                        }
                    );
                },
                cancel: function () {
                    $('#frmProductionProcess').modal('hide');
                },
                ok: function () {
                    $('#frmProductionProcess').modal('hide');
                },

                copyBOM: function (copyFromProductionProcessID) {
                    $('#frmProductionProcess').modal('hide');
                    $scope.event.createBOMTemplateData(copyFromProductionProcessID)
                }
            }
        }

    //
    // init
    //
    $scope.event.init();
}]);