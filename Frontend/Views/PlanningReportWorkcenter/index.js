(function () {
    'use strict';

    //
    // SUPPORT
    //
    $('.search-filter').keypress(function (e) {
        if (e.keyCode === 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.reload();
        }
    });

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
    angular.module('tilsoftApp').controller('tilsoftController', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
        dataService.searchFilter.pageSize = context.pageSize;
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;
        //
        // property
        //
        $scope.data = [];
        $scope.weekInfo = [];
        $scope.ReceivingDetail = [];
        $scope.ReceivingSetDetail = [];
        $scope.WorkCenterStatus = [];
        $scope.statuses = ['FINISHED', 'OVER SHEDULE', 'IN PRODUCTION', 'OPEN'];
        $scope.workCenters = [];
        $scope.dataDisplay = [];
        $scope.filters = {
            StartDateFrom: '',
            StartDateTo: '',
            FinishedDateFrom: '',
            FinishedDateTo: '',
            WorkOrderUD: '',
            ClientUD: '',
            ProformaInvoiceNo: '',
            WorkCenterID: 7,
            WorkOrderStatus: ''
        };
        $scope.screen = {
            _WORKORDER: 1,
            _WORKORDER_DETAIL: 2,
        }
        $scope.currentScreen = $scope.screen._WORKORDER;

        //
        // private function defined
        //
        $scope.showSetDetail = function ($event, workOrderID) {
            $event.preventDefault();
            $("." + workOrderID).toggle();
        }

        //
        // events
        //
        $scope.event = {
            init: function () {
                dataService.getInit(
                    function (data) {
                        if (data.message.type === 0) {
                            $scope.workCenters = data.data.workCenterDTOs;
                        }
                        $('#content').show();
                    },
                    function (error) {
                        $scope.workCenters = [];
                    }
                );
            },
            reload: function () {
                $scope.data = [];
                $scope.event.search();
            },
            search: function () {
                $scope.dataDisplay = [];
                dataService.searchFilter.filters = $scope.filters;
                dataService.search(
                    function (data) {
                        if (data.message.type === 0) {
                            $scope.data = data.data.data;
                            $scope.weekInfo = data.data.weekInfoDTOs;
                            $scope.ReceivingDetail = data.data.receivingDetailDTOs;
                            $scope.ReceivingSetDetail = data.data.receivingSetDetailDTOs;
                            $scope.WorkCenterStatus = data.data.workcenterStatus;
                            $scope.MaterialStatus = data.data.materialStatus;
                            //get week info double
                            $scope.weekInfo_x2 = $scope.method.getWeekInfo_x2();
                            //$scope.data.WorkCenterStatusFilter = $scope.method.checkWorkCenterStatus($scope.data);
                        }
                    },
                    function (error) {
                        $scope.data = [];
                        $scope.weekInfo = [];
                        $scope.workCenters = [];
                        $scope.ReceivingDetail = [];
                        $scope.ReceivingSetDetail = [];
                        $scope.WorkCenterStatus = [];
                        $scope.MaterialStatus = [];
                    }
                );
            },
        }

        //
        // method
        //
        $scope.method = {
            convertoToDate: function (ddmmyyyy) {
                var day = parseInt(ddmmyyyy.substr(0, 2));
                var mm = parseInt(ddmmyyyy.substr(3, 2));
                var yyyy = parseInt(ddmmyyyy.substr(6, 4));
                return new Date(yyyy, mm - 1, day);
            },
            formatDateDDMMYYYY: function (dateObj) {
                var pad = function (n) {
                    return n < 10 ? "0" + n : n;
                }
                return pad(dateObj.getDate()) + "/" + pad(dateObj.getMonth() + 1) + "/" + dateObj.getFullYear();
            },


            getWeekInfo_x2: function () {
                var result = [];
                angular.forEach($scope.weekInfo, function (item) {
                    result.push({
                        colName: 'Plan',
                        weekInfoID: item.weekInfoID
                    });
                    result.push({
                        colName: 'Real',
                        weekInfoID: item.weekInfoID
                    });
                });
                return result;
            },

            getPlanQnt: function (workOrderID, weekInfoID) {
                var result = parseFloat(0);
                for (var i = 0; i < $scope.data.length; i++) {
                    var aItem = $scope.data[i];
                    if (aItem.workOrderID === workOrderID && aItem.weekInfoID === weekInfoID) {
                        result = aItem.planQnt;
                    }
                }
                if (result === 0 || result === " ") {
                    result = " ";
                }
                return result;
            },

            getRealQnt: function (workOrderID, weekInfoID) {
                var result = parseFloat(0);
                for (var i = 0; i < $scope.ReceivingDetail.length; i++) {
                    var rItem = $scope.ReceivingDetail[i];
                    if (rItem.workOrderID === workOrderID && rItem.weekInfoID === weekInfoID) {
                        result = rItem.totalReceiving;
                    }
                }
                if (result === 0 || result === " ") {
                    result = " ";
                }
                return result;
            },

            getProductQuantity: function (item) {
                var result = parseFloat(0);
                for (var i = 0; i < $scope.ReceivingDetail.length; i++) {
                    var rItem = $scope.ReceivingDetail[i];
                    if (rItem.workOrderID === item.workOrderID) {
                        result += rItem.totalReceiving;
                    }
                }
                item.remain = item.quantity - result;
                return result;
            },

            checkMaterialStatus: function (item, id) {
                for (var i = 1; i < $scope.MaterialStatus.length; i++) {
                    var rItem = $scope.MaterialStatus[i];
                    if (item.workOrderID == rItem.workOrderID) {
                        if ((rItem.storageMaterial != null && rItem.storageMaterial != "") && (rItem.needMaterial != null && rItem.needMaterial != "")) {
                            if (id == 15) {
                                if (rItem.storageMaterial < rItem.needMaterial && rItem.storageMaterial > 0) {
                                    item.woodStatus = "DO";
                                    return "DO";
                                } else if (rItem.storageMaterial >= rItem.needMaterial) {
                                    item.woodStatus = "OK";
                                    return "OK";
                                }
                                else {
                                    item.woodStatus = "NOT";
                                    return "NOT";
                                }
                            }
                            if (id == 17) {
                                if (rItem.storageMaterial < rItem.needMaterial && rItem.storageMaterial > 0) {
                                    item.cartonStatus = "DO";
                                    return "DO";
                                } else if (rItem.storageMaterial >= rItem.needMaterial) {
                                    item.cartonStatus = "OK";
                                    return "OK";
                                }
                                else {
                                    item.cartonStatus = "NOT";
                                    return "NOT";
                                }
                            }
                            if (id == 18 || id == 19 || id == 40) {
                                if (rItem.storageMaterial < rItem.needMaterial && rItem.storageMaterial > 0) {
                                    item.cushionStatus = "DO";
                                    return "DO";
                                } else if (rItem.storageMaterial >= rItem.needMaterial) {
                                    item.cushionStatus = "OK";
                                    return "OK";
                                }
                                else {
                                    cushionStatus = "NOT";
                                    return "NOT";
                                }
                            }
                        }
                        else {
                            item.woodStatus = "NOT";
                            item.cartonStatus = "NOT";
                            item.cushionStatus = "NOT";
                            return "NOT";
                        }
                    }
                }
                item.woodStatus = "NO";
                item.cartonStatus = "NO";
                item.cushionStatus = "NO";
                return "NO";
            },

            checkWorkCenterStatus: function (item, id) {
                var currentDate = new Date();
                for (var i = 0; i < $scope.WorkCenterStatus.length; i++) {
                    var rItem = $scope.WorkCenterStatus[i];
                    if (rItem.finishDate != undefined) {
                        if (item.workOrderID == rItem.workOrderID && rItem.workCenterID == id) {
                            if (id == 7) {
                                if (rItem.totalReceiving >= item.planQnt) {
                                    for (var j = 0; j < $scope.WorkCenterStatus.length; j++) {
                                        var tempItem = $scope.WorkCenterStatus[j];
                                        if (tempItem.finishDate != undefined) {
                                            if (rItem.workOrderID == tempItem.workOrderID && tempItem.workCenterID == 8) {
                                                if (tempItem.totalReceiving >= item.planQnt) {
                                                    item.frameStatus = "OK";
                                                    return "OK";
                                                }
                                                else if (tempItem.totalReceiving < item.planQnt && currentDate > $scope.method.convertoToDate(tempItem.finishDate)) {
                                                    item.frameStatus = "OVER";
                                                    return "OVER";
                                                }
                                                else if (tempItem.totalReceiving < item.planQnt && tempItem.totalReceiving > 0) {
                                                    item.frameStatus = "DO";
                                                    return "DO";
                                                }
                                                else if (tempItem.totalReceiving == null || tempItem.totalReceiving == "") {
                                                    item.frameStatus = "NOT";
                                                    return "NOT";
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (rItem.totalReceiving < item.planQnt && currentDate > $scope.method.convertoToDate(rItem.finishDate)) {
                                    item.frameStatus = "OVER";
                                    return "OVER";
                                }
                                else if (rItem.totalReceiving < item.planQnt && rItem.totalReceiving > 0) {
                                    item.frameStatus = "DO";
                                    return "DO";
                                }
                                else {
                                    item.frameStatus = "NOT";
                                    return "NOT";
                                }
                            }
                            else if (id == 9) {
                                if (rItem.totalReceiving >= item.planQnt) {
                                    item.weavingStatus = "OK";
                                    return "OK";
                                }
                                else if (rItem.totalReceiving < item.planQnt && currentDate > $scope.method.convertoToDate(rItem.finishDate)) {
                                    item.weavingStatus = "OVER";
                                    return "OVER";
                                }
                                else if (rItem.totalReceiving < item.planQnt && rItem.totalReceiving > 0) {
                                    item.weavingStatus = "DO";
                                    return "DO";
                                }
                                else {
                                    item.weavingStatus = "NOT";
                                    return "NOT";
                                }
                            }
                            else if (id == 11) {
                                if (rItem.totalReceiving >= item.planQnt) {
                                    item.packingStatus = "OK";
                                    return "OK";
                                }
                                else if (rItem.totalReceiving < item.planQnt && currentDate > $scope.method.convertoToDate(rItem.finishDate)) {
                                    item.packingStatus = "OVER";
                                    return "OVER";
                                }
                                else if (rItem.totalReceiving < item.planQnt && rItem.totalReceiving > 0) {
                                    item.packingStatus = "DO";
                                    return "DO";
                                }
                                else {
                                    item.packingStatus = "NOT";
                                    return "NOT";
                                }
                            }
                        }
                    }

                }
                item.frameStatus = "NO";
                item.weavingStatus = "NO";
                item.packingStatus = "NO";
                return "NO";
            },
        };

        //
        // angular init
        //
        $timeout(function () {
            $scope.event.init();
        }, 0);
    }]);
})();