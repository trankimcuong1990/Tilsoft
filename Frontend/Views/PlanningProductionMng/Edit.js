function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            description: '',
            id: item.productionTeamID,
            label: item.productionTeamNM,
            value: item.productionTeamNM,

            productionTeamID: item.productionTeamID,
            productionTeamUD: item.productionTeamUD,
            productionTeamNM: item.productionTeamNM,
            workCenterID: item.workCenterID,
            isDefault: item.isDefault,
        }
    });
}

(function () {
    'use strict';
    //
    // APP START
    //
    var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'furnindo-directive']);

    tilsoftApp.filter('arisingTeamFilter', function () {
        return function (arisingProductionTeamDTOs, planningProductionTeamDTOs) {
            var result = [];
            angular.forEach(arisingProductionTeamDTOs, function (item) {
                var teamItem = planningProductionTeamDTOs.filter(o => o.productionTeamID === item.productionTeamID);
                if (teamItem.length === 0) {
                    result.push(item);
                }
            });
            return result;
        }
    });


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
        $scope.workOrder = null;
        $scope.workCenters = [];

        //grid handle
        $scope.gridHandler = {
            loadMore: function () {
                // Do nothing
            },
            sort: function (sortedBy, sortedDirection) {
                // Do nothing
            },
            isTriggered: false
        };

        //
        // event
        //
        $scope.event = {
            init: function () {
                if (context.id == 0) {
                    jsHelper.loadingSwitch(true);
                    dataService.createPlanningProductionFromBOM(context.bomID)
                        .then(data => {
                            var planningProductionID = data.data.data;
                            $scope.method.refresh(planningProductionID);
                        })
                        .catch(error => {
                            jsHelper.showMessage('warning', response.data.message);
                            jsHelper.loadingSwitch(false);
                        });
                }
                else {
                    $scope.event.load();
                }       
            },

            load: function () {
                var param = {};
                dataService.load(
                    context.id,
                    param,
                    function (data) {
                        $scope.data = data.data.data;
                        $scope.workOrder = data.data.workOrder;
                        $scope.workCenters = data.data.workCenters

                        //view list planning production
                        $scope.event.viewListPlanningProduction();
                    
                        //show to page
                        jQuery('#content').show();
                        $scope.$watch('data', function () {
                            jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                        });
                    },
                    function (error) {
                        $scope.data = null;
                    }
                );
            },

            update: function () {
                //update workorder
                $scope.data.workOrderStartDate = $scope.workOrder.startDate;
                $scope.data.workOrderFinishDate = $scope.workOrder.finishDate;

                //update data
                if ($scope.editForm.$valid) {
                    dataService.update(
                        context.id,
                        $scope.data,
                        function (data) {
                            $scope.method.refresh(data.data.planningProductionID);
                        },
                        function (error) {
                            //do need do nothing
                        }
                    );
                }
                else {
                    jsHelper.showMessage('warning', context.errMsg);
                }
            }, 

            viewListPlanningProduction: function () {
                $scope.data_listFormated = [];
                $scope.method.parseTreeDataToList($scope.data, $scope.data_listFormated);
                $scope.data_listFormated = $scope.data_listFormated.filter(o => o.parentPlanningProductionID !== null && !o.isDeleted);
            },

            changeWorkOrderFinishDate: function () {
                $scope.method.setStartDateAndFinishDate($scope.data, $scope.workOrder);
                $scope.event.viewListPlanningProduction();
            },

            addNodeByPlanningProductionID: function (planningProductionID) {
                $scope.editNodeForm_ListViewFormat.event.load(planningProductionID, true);
            },

            editNodeByPlanningProductionID: function (planningProductionID) {
                $scope.editNodeForm_ListViewFormat.event.load(planningProductionID, false);
            },

            deleteNode: function (deleteNode) {
                if (deleteNode.planningProductionID > 0 && deleteNode.planningProductionTeamDTOs.length > 0) {
                    bootbox.alert("You have to delete team before delete");
                    return;
                }
                //get is node is same workcenter
                var allDeleteNodes = $scope.method.getNodesByWorkCenterID(deleteNode.workCenterID, $scope.data);
                for (var i = 0; i < allDeleteNodes.length; i++) {
                    var tobeDeletedNode = allDeleteNodes[i];

                    //get parent node of node which is deleting
                    var parentNode = $scope.method.getNodeByID(tobeDeletedNode.parentPlanningProductionID, $scope.data);

                    //get child node of deleting node
                    var childNodes = tobeDeletedNode.planningProductionDTOs;
                    if (childNodes.length > 0) {
                        var childNode = childNodes[0];
                        childNode.parentPlanningProductionID = parentNode.planningProductionID;

                        //push this child node to parent node
                        parentNode.planningProductionDTOs.push(childNode);

                        //release 
                        tobeDeletedNode.planningProductionDTOs = [];
                    }

                    //delete node
                    tobeDeletedNode.planningProductionTeamDTOs = [];
                    tobeDeletedNode.isDeleted = true;
                }
                //view in list format
                $scope.event.viewListPlanningProduction();
            },


        }

        //
        // method
        //
        $scope.method = {
            refresh: function (id) {
                jsHelper.loadingSwitch(true);
                window.location = context.refreshUrl + id;
            },

            parseTreeDataToList: function (data, result) {
                //debugger;
                if (data != null && data.planningProductionDTOs != null) {
                    //push to list result
                    result.push(data);

                    //assign piece index for node which are pieces
                    if (data.parentPlanningProductionID == null) {
                        for (var i = 0; i < data.planningProductionDTOs.length; i++) {
                            data.planningProductionDTOs[i].pieceIndex = i + 1;
                        }
                    }

                    //assign piece index for nodes which are child of piece (component node)
                    angular.forEach(data.planningProductionDTOs, function (item) {
                        if (item.pieceIndex == undefined) {
                            item.pieceIndex = data.pieceIndex;
                        }
                    });

                    //continue for next node
                    angular.forEach(data.planningProductionDTOs, function (item) {
                        $scope.method.parseTreeDataToList(item, result);
                    });
                }
            },

            convertoToDate: function (ddmmyyyy) {
                var day = parseInt(ddmmyyyy.substr(0, 2));
                var mm = parseInt(ddmmyyyy.substr(3, 2));
                var yyyy = parseInt(ddmmyyyy.substr(6, 4));
                return new Date(yyyy, mm-1, day);
            },

            formatDateDDMMYYYY: function (dateObj) {
                var pad = function (n) {
                    return n < 10 ? "0" + n : n;
                }
                return pad(dateObj.getDate()) + "/" + pad(dateObj.getMonth() + 1) + "/" + dateObj.getFullYear();
            },

            setStartDateAndFinishDate(data, workOrder) {
                if (data.workCenterNM) {
                    var woFinishDate = $scope.method.convertoToDate(workOrder.finishDate);
                    var woStartDate = $scope.method.convertoToDate(workOrder.startDate);
                    var planningTime = (data.planningTime === null ? 0 : parseInt(data.planningTime));
                    var workingTime = (data.workingTime === null ? 0 : parseInt(data.workingTime));
                    var diffDays = parseInt((woFinishDate - woStartDate) / (1000 * 60 * 60 * 24)); 
                    data.startDate = dataService.dateAdd(woFinishDate, -(parseInt(diffDays * planningTime / 60)));
                    data.finishDate = dataService.dateAdd($scope.method.convertoToDate(data.startDate), +(parseInt(diffDays * workingTime / 60)));
                }
                angular.forEach(data.planningProductionDTOs, function (item) {
                    $scope.method.setStartDateAndFinishDate(item, workOrder)
                });
            },

            _getNodeByID: function (planningProductionID, data, resultNodes) {
                if (data.planningProductionID == planningProductionID) {
                    resultNodes.push(data);
                }
                if (resultNodes.length == 0) {
                    for (var i = 0; i < data.planningProductionDTOs.length; i++) {
                        $scope.method._getNodeByID(planningProductionID, data.planningProductionDTOs[i], resultNodes);
                    }
                }
            },

            getNodeByID: function (planningProductionID, data) {
                var resultNode = null;
                var resultNodes = [];
                $scope.method._getNodeByID(planningProductionID, data, resultNodes);
                if (resultNodes.length > 0) {
                    resultNode = resultNodes[0];
                }
                return resultNode;             
            },

            getWorkCenterNMByID: function (workCenterID) {
                var result = '';
                var x = $scope.workCenters.filter(o => o.workCenterID === workCenterID);
                if (x.length > 0) {
                    result = x[0].workCenterNM;
                }
                return result;
            },

            _getPieceNode: function (_currentNode, _data, _pieceNodes, _resultNodes) {
                var x = _pieceNodes.filter(o => o.planningProductionID === _currentNode.planningProductionID);
                if (x !== null && x.length > 0) {
                    _resultNodes.push(_currentNode);
                }
                if (_resultNodes.length == 0) {
                    var parentNode = $scope.method.getNodeByID(_currentNode.parentPlanningProductionID, _data);
                    $scope.method._getPieceNode(parentNode, _data, _pieceNodes, _resultNodes);
                }
            },

            getPieceNode: function (data, currentNode) {                
                var pieceNodes = data.planningProductionDTOs;
                var resultNodes = [];
                $scope.method._getPieceNode(currentNode, data, pieceNodes, resultNodes);
                if (resultNodes.length > 0) {
                    return resultNodes[0];
                }
                else return null;
            },

            _getNodesByWorkCenterID: function (workCenterID, data, resultNodes) {
                if (data.workCenterID == workCenterID) {
                    resultNodes.push(data);
                }
                for (var i = 0; i < data.planningProductionDTOs.length; i++) {
                    $scope.method._getNodesByWorkCenterID(workCenterID, data.planningProductionDTOs[i], resultNodes);
                }
            },

            getNodesByWorkCenterID: function (workCenterID, data) {
                var resultNodes = [];
                $scope.method._getNodesByWorkCenterID(workCenterID, data, resultNodes);
                return resultNodes;
            },

            getTotalPlanOfNode: function (planningProductionNode) {
                var total = parseFloat(0);
                angular.forEach(planningProductionNode.planningProductionTeamDTOs, function (item) {
                    total += (item.planQnt == null ? parseFloat(0) : parseFloat(item.planQnt));
                });
                return total;
            },

            getArisingItemByProductionTeam: function (currentNode, productionTeamID) {
                var arsingItem = {
                    receivedQnt: 0,
                    producedQnt: 0,
                }
                var x = currentNode.arisingProductionTeamDTOs.filter(s => s.productionTeamID === productionTeamID);
                if (x.length > 0) {
                    arsingItem.receivedQnt = x[0].receivedQnt;
                    arsingItem.producedQnt = x[0].producedQnt;
                }
                return arsingItem                
            }
        }

        $scope.editNodeForm_ListViewFormat = {
            currentNode: null,
            copyOfcurrentNode: null,
            isAddNew: null,

            //search team property
            searchTeamQuery: '',            
            productionTeamAPI: {
                url: dataService.serviceUrl + 'get-production-team',
                token: dataService.token,
            },
            productionTeam: {
                productionTeamID: null,
                productionTeamUD: null,
                productionTeamNM: null,
                workCenterID: null,
                isDefault: null,
            },

            //event
            event: {
                load: function (currentPlanningProductionID, isAddNew) {
                    //get form status
                    $scope.editNodeForm_ListViewFormat.isAddNew = isAddNew;
                    //get node by planningProductionID
                    $scope.editNodeForm_ListViewFormat.currentNode = $scope.method.getNodeByID(currentPlanningProductionID, $scope.data);

                    if (isAddNew) {
                        $scope.editNodeForm_ListViewFormat.copyOfcurrentNode = {
                            planningProductionID: null,
                            parentPlanningProductionID: null,
                            startDate: null,
                            finishDate: null,
                            bomid: null,
                            workCenterID: null,
                            productionItemID: null,
                            isDeleted: false,  
                            planningProductionDTOs: [],
                            planningProductionTeamDTOs: [],
                            productionItemUD: '',
                            productionItemNM: '',
                            productionItemTypeNM: '',
                            workOrderID: $scope.data.workOrderID,
                        }
                    }
                    else {
                        $scope.editNodeForm_ListViewFormat.copyOfcurrentNode = angular.copy($scope.editNodeForm_ListViewFormat.currentNode);
                    }
                    $('#frmEditNodeForm_ListViewFormat').modal('show');
                },

                addProductionTeam: function (selectedData) {
                    $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.planningProductionTeamDTOs.push({
                        planningProductionTeamID: dataService.getIncrementId(),
                        productionTeamID: selectedData.productionTeamID,
                        productionTeamUD: selectedData.productionTeamUD,
                        productionTeamNM: selectedData.productionTeamNM,
                    });

                    //reset seach query
                    $scope.editNodeForm_ListViewFormat.searchTeamQuery = '';
                },

                removeProductionTeam: function (teamItem) {
                    var index = $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.planningProductionTeamDTOs.indexOf(teamItem);
                    $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.planningProductionTeamDTOs.splice(index, 1);
                },

                selectedWorkCenter: function () {
                    var workCenter = $scope.workCenters.filter(o => o.workCenterID == $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.workCenterID)[0];
                    var planningTime = (workCenter.planningTime === null ? 0 : workCenter.planningTime);
                    var workingTime = (workCenter.workingTime === null ? 0 : workCenter.workingTime);

                    var finishDate_s = $scope.method.convertoToDate($scope.workOrder.finishDate);
                    var finishDate_f = $scope.method.convertoToDate($scope.workOrder.finishDate);

                    var _startDate = finishDate_s.setDate(finishDate_s.getDate() - planningTime - workingTime);                    
                    var _finishDate = finishDate_f.setDate(finishDate_f.getDate() - planningTime);

                    //conver date to string
                    $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.startDate = $scope.method.formatDateDDMMYYYY(new Date(_startDate));
                    $scope.editNodeForm_ListViewFormat.copyOfcurrentNode.finishDate = $scope.method.formatDateDDMMYYYY(new Date(_finishDate));                    
                },

                cancel: function () {
                    $('#frmEditNodeForm_ListViewFormat').modal('hide');
                },

                ok: function () {
                    if ($scope.editNodeForm_ListViewFormat.isAddNew) { //add new node to current node
                        //var newNode = $scope.editNodeForm_ListViewFormat.copyOfcurrentNode;

                        ////assign workcenter name for new node
                        //newNode.workCenterNM = (newNode.bomid ? newNode.workCenterNM : $scope.method.getWorkCenterNMByID(newNode.workCenterID));
                        ////assign productionItem for new node
                        //var _currentNode = $scope.editNodeForm_ListViewFormat.currentNode;
                        //var pieceNode = $scope.method.getPieceNode($scope.data, _currentNode);
                        //newNode.productionItemUD = $scope.workOrder.workOrderUD + "-" + pieceNode.productionItemUD + "-" + newNode.workCenterNM;
                        //newNode.productionItemNM = pieceNode.productionItemNM + "-" + newNode.workCenterNM;
                        //newNode.productionItemTypeNM = 'Component';

                        ////assign new node to currentNode & assign chidl node of current node to new node
                        //if ($scope.editNodeForm_ListViewFormat.currentNode.planningProductionDTOs.length > 0) {
                        //    //get child nodes of current node
                        //    var childNode = angular.copy($scope.editNodeForm_ListViewFormat.currentNode.planningProductionDTOs[0]);
                        //    childNode.parentPlanningProductionID = newNode.planningProductionID;

                        //    //push child node of current node to new node
                        //    newNode.planningProductionDTOs.push(childNode);

                        //    //release child node of current node
                        //    $scope.editNodeForm_ListViewFormat.currentNode.planningProductionDTOs = [];
                        //}
                        ////push new node to current node
                        //$scope.editNodeForm_ListViewFormat.currentNode.planningProductionDTOs.push(newNode);

                        var nodesByWorkCenter = $scope.method.getNodesByWorkCenterID($scope.editNodeForm_ListViewFormat.currentNode.workCenterID, $scope.data);
                        for (var i = 0; i < nodesByWorkCenter.length; i++) {
                            var _currentNode = nodesByWorkCenter[i];
                            var _newNode = angular.copy($scope.editNodeForm_ListViewFormat.copyOfcurrentNode);

                            //assign workcenter info
                            _newNode.workCenterNM = (_newNode.bomid ? _newNode.workCenterNM : $scope.method.getWorkCenterNMByID(_newNode.workCenterID));
                            if (!_newNode.bomid) {
                                var wc = $scope.workCenters.filter(o => o.workCenterID == _newNode.workCenterID)[0];
                                _newNode.planningTime = wc.planningTime;
                                _newNode.workingTime = wc.workingTime;
                            }

                            //asign id and parent id
                            _newNode.planningProductionID = dataService.getIncrementId();
                            _newNode.parentPlanningProductionID = _currentNode.planningProductionID;

                            //assign productionItem for new node
                            var pieceNode = $scope.method.getPieceNode($scope.data, _currentNode);
                            _newNode.productionItemUD = $scope.workOrder.workOrderUD + "-" + pieceNode.productionItemUD + "-" + _newNode.workCenterNM;
                            _newNode.productionItemNM = pieceNode.productionItemNM + "-" + _newNode.workCenterNM;
                            _newNode.productionItemTypeNM = 'Component';

                            //insert to selecting node
                            $scope.editNodeForm_ListViewFormat.method.insertNewNode(_newNode, _currentNode);
                        }                        
                    }
                    else { //edit current node
                        var editingNode = $scope.editNodeForm_ListViewFormat.copyOfcurrentNode;
                        //update value for editing node
                        editingNode.workCenterNM = (editingNode.bomid ? editingNode.workCenterNM : $scope.method.getWorkCenterNMByID(editingNode.workCenterID));
                        if (!editingNode.bomid) {
                            var wc = $scope.workCenters.filter(o => o.workCenterID == editingNode.workCenterID)[0];
                            editingNode.planningTime = wc.planningTime;
                            editingNode.workingTime = wc.workingTime;
                        }
                        //assign productionItem
                        var pieceNode = $scope.method.getPieceNode($scope.data, editingNode);
                        editingNode.productionItemUD = $scope.workOrder.workOrderUD + "-" + pieceNode.productionItemUD + "-" + editingNode.workCenterNM;
                        editingNode.productionItemNM = pieceNode.productionItemNM + "-" + editingNode.workCenterNM;
                        editingNode.productionItemTypeNM = 'Component';

                        //update current node
                        $scope.editNodeForm_ListViewFormat.currentNode.workCenterID = editingNode.workCenterID;   
                        $scope.editNodeForm_ListViewFormat.currentNode.workCenterNM = editingNode.workCenterNM;
                        $scope.editNodeForm_ListViewFormat.currentNode.startDate = editingNode.startDate;
                        $scope.editNodeForm_ListViewFormat.currentNode.finishDate = editingNode.finishDate;
                        $scope.editNodeForm_ListViewFormat.currentNode.planningTime = editingNode.planningTime;
                        $scope.editNodeForm_ListViewFormat.currentNode.workingTime = editingNode.workingTime;
                        $scope.editNodeForm_ListViewFormat.currentNode.productionItemUD = editingNode.productionItemUD;
                        $scope.editNodeForm_ListViewFormat.currentNode.productionItemNM = editingNode.productionItemNM;
                        $scope.editNodeForm_ListViewFormat.currentNode.productionItemTypeNM = editingNode.productionItemTypeNM;

                        //production team
                        $scope.editNodeForm_ListViewFormat.currentNode.planningProductionTeamDTOs = [];
                        angular.forEach(editingNode.planningProductionTeamDTOs, function (tItem) {
                            $scope.editNodeForm_ListViewFormat.currentNode.planningProductionTeamDTOs.push(tItem);
                        });
                                                
                    }
                    $('#frmEditNodeForm_ListViewFormat').modal('hide');

                    //view in list format
                    $scope.event.viewListPlanningProduction();
                },

                
            },

            method: {
                //insert new node to current node
                //  we will make new node is child of current node 
                //  and make new node is parent of nodes which are child of current node
                insertNewNode: function (newNode, currentNode) {
                    //assign new node to currentNode & assign chidl node of current node to new node
                    if (currentNode.planningProductionDTOs.length > 0) {
                        //get child nodes of current node
                        var childNode = angular.copy(currentNode.planningProductionDTOs[0]); //we take 1 node because the it alway have 1 node or 0 node
                        childNode.parentPlanningProductionID = newNode.planningProductionID;

                        //push child node of current node to new node
                        newNode.planningProductionDTOs.push(childNode);

                        //release child node of current node
                        currentNode.planningProductionDTOs = [];
                    }
                    //push new node to current node
                    currentNode.planningProductionDTOs.push(newNode);
                }
            },
        }
       
        $timeout(function () {
            $scope.event.init();
        }, 0);


    }]);
})();

