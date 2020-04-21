//jQuery("#content").show();

(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive']).controller('tilsoftController', MRPRptController);
    MRPRptController.$inject = ['$scope', 'dataService'];

    function MRPRptController($scope, dataService) {
        $scope.data = [];
        $scope.newID = -1;
        //$scope.dateRange = [{ date: '01/08/2018' }, { date: '02/08/2018' }, { date: '03/08/2018' }, { date: '04/08/2018' }, { date: '05/08/2018' }, { date: '06/08/2018' },
        //{ date: '07/08/2018' }, { date: '08/08/2018' }, { date: '09/08/2018' }, { date: '10/08/2018' }, { date: '11/08/2018' }, { date: '12/08/2018' },
        //{ date: '13/08/2018' }, { date: '14/08/2018' }, { date: '15/08/2018' }, { date: '16/08/2018' }, { date: '17/08/2018' }, { date: '18/08/2018' }];

        $scope.dateRange = [];
        $scope.purchaseOrderArr = [];
        $scope.purchaseRequestArr = [];
        $scope.projectOnHandTempArr = [];
        $scope.projectOnHandArr = [];
        $scope.planProjectOnHandArr = [];
        $scope.planProjectOnHandTempArr = [];
        $scope.planOderRelaseTempArr = [];
        $scope.planOderRelaseArr = [];
        $scope.projectOnHandArrPlan = [];


        $scope.event = {
            get: get,
            remove: remove,
            update: update,
            showPlan: showPlan,
            showActual: showActual,
            getDate: getDate,
            getDates: getDates


        };

        $scope.method = {
            refresh: refresh,
            getNewID: getNewID
        };


        //Function Start

        function get() {
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
            dataService.supportServiceUrl = context.supportServiceUrl;
            dataService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.purchaseOrderDetail = data.data.data.purchaseOrderDetailMRPs;
                    $scope.purchaseRequestDetail = data.data.data.purchaseRequestDetailMRPs;
                    $scope.isshow = 1;
                    jQuery('#content').show();
                    $scope.event.getDate();

                    //Get Arr PurchaseOrderArr
                    for (var i = 0; i < $scope.dateRange.length; i++) {
                        var itemDate = $scope.dateRange[i];

                        if ($scope.purchaseOrderDetail.length !== 0) {

                            for (var j = 0; j < $scope.purchaseOrderDetail.length; j++) {
                                var itemOrder = $scope.purchaseOrderDetail[j];

                                var itemOrder2 = {
                                    plannedETA: itemDate.date,
                                    plannedReceivingQnt: null
                                };

                                if (itemDate.date === itemOrder.plannedETA) {
                                    itemOrder2.plannedReceivingQnt = itemOrder.plannedReceivingQnt;
                                    $scope.purchaseOrderArr.push(itemOrder2);
                                    break;
                                } else {
                                    if (j === ($scope.purchaseOrderDetail.length - 1)) {
                                        $scope.purchaseOrderArr.push(itemOrder2);
                                        break;
                                    }
                                }
                            }
                        } else {
                            itemOrder2 = {
                                plannedETA: itemDate.date,
                                plannedReceivingQnt: null
                            };

                            $scope.purchaseOrderArr.push(itemOrder2);
                        }


                    }
                    //Get PurchaseRequest Arr
                    for (var ij = 0; ij < $scope.dateRange.length; ij++) {
                        var itemDate2 = $scope.dateRange[ij];

                        if ($scope.purchaseRequestDetail.length !== 0) {
                            for (var l = 0; l < $scope.purchaseRequestDetail.length; l++) {
                                var itemRequest = $scope.purchaseRequestDetail[l];

                                var itemRequest2 = {

                                    eta: itemDate2.date,
                                    requestQnt: null
                                };
                                if (itemDate2.date === itemRequest.eta) {
                                    itemRequest2.requestQnt = itemRequest.requestQnt;
                                    $scope.purchaseRequestArr.push(itemRequest2);
                                    break;
                                } else {
                                    if (l === ($scope.purchaseRequestDetail.length - 1)) {
                                        $scope.purchaseRequestArr.push(itemRequest2);
                                        break;
                                    }
                                }
                            }
                        } else {
                            itemRequest2 = {
                                eta: itemDate2.date,
                                requestQnt: null
                            };
                            $scope.purchaseRequestArr.push(itemRequest2);
                        }

                    }

                    //Get ProjectOnHanTempdArr 
                    for (var ii = 0; ii < $scope.dateRange.length; ii++) {
                        var itemPr = $scope.dateRange[ii];

                        for (var jj = 0; jj < $scope.purchaseOrderArr.length; jj++) {
                            var itemOrderPr = $scope.purchaseOrderArr[jj];
                            if (itemPr.date === itemOrderPr.plannedETA) {
                                var itemOderQnt = (itemOrderPr.plannedReceivingQnt === null ? 0 : parseFloat(itemOrderPr.plannedReceivingQnt));
                                break;
                            }
                        }

                        for (var k = 0; k < $scope.purchaseRequestArr.length; k++) {
                            var itemRequestPr = $scope.purchaseRequestArr[k];
                            if (itemPr.date === itemRequestPr.eta) {
                                var itemRequestQnt = (itemRequestPr.requestQnt === null ? 0 : parseFloat(itemRequestPr.requestQnt));
                                break;
                            }
                        }

                        var projectItem = {
                            date: itemPr.date,
                            itemOderQnt: itemOderQnt,
                            itemRequestQnt: itemRequestQnt
                        };

                        $scope.projectOnHandTempArr.push(projectItem);
                    }

                    //Get ProjectOnHandArr 
                    for (var ix = 0; ix < $scope.projectOnHandTempArr.length; ix++) {

                        var index = 0;
                        var stockQntBefor = $scope.data.projectOnHand;
                        var prItem = $scope.projectOnHandTempArr[ix];
                        var stockQntAffter;
                        var stockQnt;
                        var planStockQntAffter;
                        var itemx;
                        if (ix === 0) {
                            stockQntAffter = parseFloat(stockQntBefor) + (prItem.itemOderQnt === null ? 0 : parseFloat(prItem.itemOderQnt)) - (prItem.itemRequestQnt === null ? 0 : parseFloat(prItem.itemRequestQnt));

                        } else {
                            stockQntAffter = parseFloat(stockQntAffter) + (prItem.itemOderQnt === null ? 0 : parseFloat(prItem.itemOderQnt)) - (prItem.itemRequestQnt === null ? 0 : parseFloat(prItem.itemRequestQnt));

                        }

                        var prData = {
                            stockQnt: stockQntAffter,
                            date: prItem.date
                            //planStockQnt: planStockQntAffter
                        };

                        $scope.projectOnHandArr.push(prData);
                        index++;
                    }

                    //Get ProjectOnHandArr for Plan
                    for (var ik = 0; ik < $scope.projectOnHandTempArr.length; ik++) {
                        var index2 = 0;
                        var stockQntBefor2 = $scope.data.projectOnHand;
                        var prItem2 = $scope.projectOnHandTempArr[ik];
                        var stockQntAffter2;
                        var minQntx = $scope.data.minQnt;
                        //var stockQnt;
                        //var planStockQntAffter;
                        //var itemx;

                        if (ik === 0) {
                            stockQntAffter2 = parseFloat(stockQntBefor2) + (prItem2.itemOderQnt === null ? 0 : parseFloat(prItem2.itemOderQnt)) - (prItem2.itemRequestQnt === null ? 0 : parseFloat(prItem2.itemRequestQnt));

                        } else {
                            if (prItem2.itemOderQnt !== 0 || prItem2.itemRequestQnt !== 0) {
                                stockQntAffter2 = parseFloat(stockQntAffter2) + (prItem2.itemOderQnt === null ? 0 : parseFloat(prItem2.itemOderQnt)) - (prItem2.itemRequestQnt === null ? 0 : parseFloat(prItem2.itemRequestQnt));
                            }
                            if (stockQntAffter2 < 0) {
                                stockQntAffter2 = 0;
                            }
                            if (stockQntAffter2 === 0) {
                                stockQntAffter2 = parseFloat(prItem2.itemOderQnt);
                            }
                            //if (stockQntAffter2 !== 0) {
                            //    stockQntAffter2 = stockQntAffter2;
                            //}
                        }

                        var prData2 = {
                            stockQnt: stockQntAffter2,
                            date: prItem2.date
                            //planStockQnt: planStockQntAffter
                        };

                        $scope.projectOnHandArrPlan.push(prData2);
                        index2++;
                    }

                    //Get Plan Project On Hand Arr
                    for (var n = 0; n < $scope.projectOnHandArrPlan.length; n++) {
                        var planItemOnHand = $scope.projectOnHandArrPlan[n];
                        var minQnt = $scope.data.minQnt;

                        if (planItemOnHand.stockQnt < minQnt) {
                            var planItem = {
                                date: planItemOnHand.date,
                                qnt: parseFloat(minQnt)
                            };
                            $scope.planProjectOnHandArr.push(planItem);
                        } else {
                            var planItem2 = {
                                date: planItemOnHand.date,
                                qnt: parseFloat(planItemOnHand.stockQnt)
                            };
                            $scope.planProjectOnHandArr.push(planItem2);
                        }
                    }

                    //Get temp Project On Hand
                    for (var e = 0; e < $scope.dateRange.length; e++) {
                        var planTempItem = $scope.dateRange[e];
                        for (var o = 0; o < $scope.purchaseRequestArr.length; o++) {
                            var itemRequestTemp = $scope.purchaseRequestArr[o];
                            if (planTempItem.date === itemRequestTemp.eta) {
                                var itemRequestQntTemp = (itemRequestTemp.requestQnt === null ? 0 : parseFloat(itemRequestTemp.requestQnt));
                                break;
                            }
                        }
                        for (var p = 0; p < $scope.planProjectOnHandArr.length; p++) {
                            var planProjectItemTemp = $scope.planProjectOnHandArr[p];
                            if (planTempItem.date === planProjectItemTemp.date) {
                                var itemPlanQntTemp = (planProjectItemTemp.qnt === null ? 0 : parseFloat(planProjectItemTemp.qnt));
                                break;
                            }
                        }

                        for (var h = 0; h < $scope.projectOnHandArr.length; h++) {
                            var itemProjectOnHandtemp = $scope.projectOnHandArr[h];
                            var stockQnttempOld;
                            if (planTempItem.date === itemProjectOnHandtemp.date) {
                                if (stockQnttempOld === stockQnttemp) {
                                    stockQnttemp = 0;
                                } else {
                                    var stockQnttemp = (itemProjectOnHandtemp.stockQnt === null ? 0 : parseFloat(itemProjectOnHandtemp.stockQnt));
                                    stockQnttempOld = stockQnttemp;
                                    break;
                                }

                            }
                        }
                        var planItemTemp = {
                            date: planTempItem.date,
                            itemRequestQntTemps: itemRequestQntTemp,
                            itemPlanQntTemps: itemPlanQntTemp,
                            stockQnt: stockQnttemp
                        };
                        $scope.planProjectOnHandTempArr.push(planItemTemp);
                    }

                    //Get Planned Order Release
                    for (var v = 0; v < $scope.planProjectOnHandTempArr.length; v++) {
                        var planOderReleaseItem = $scope.planProjectOnHandTempArr[v];
                        var planMinQnt = ($scope.data.minQnt === null ? 0 : $scope.data.minQnt);


                        if (planOderReleaseItem.stockQnt < planMinQnt && planOderReleaseItem.itemRequestQntTemps > 0) {
                            var planOderReleaseQntx = (parseFloat(planOderReleaseItem.itemRequestQntTemps) - parseFloat($scope.planProjectOnHandTempArr[v - 1].itemPlanQntTemps)) + parseFloat(planMinQnt);
                        } else {
                            planOderReleaseQntx = 0;
                        }

                        var xItem = {
                            planOderReleaseQnttemp: planOderReleaseQntx,
                            dateTime: planOderReleaseItem.date,
                            planOderReleaseQnt: null,
                            plannedOrderReceipts: null
                        };
                        $scope.planOderRelaseArr.push(xItem);
                    }

                    // Get Planned Order Releases
                    for (var xx = 0; xx < $scope.planOderRelaseArr.length; xx++) {
                        var xxlItem = $scope.planOderRelaseArr[xx];
                        var leadTimeReleases = $scope.data.leadTime;

                        if (xxlItem.planOderReleaseQnttemp !== 0) {
                            var prxxlItem = $scope.planOderRelaseArr[xx - leadTimeReleases - 1];
                            prxxlItem.planOderReleaseQnt = xxlItem.planOderReleaseQnttemp;
                        }
                    }
                    //Get Planned Order Receipts
                    for (var xxx = 0; xxx < $scope.planOderRelaseArr.length; xxx++) {
                        var xxxItem = $scope.planOderRelaseArr[xxx];
                        var leadTimeReceipts = $scope.data.leadTime;

                        if (xxxItem.planOderReleaseQnt !== 0) {
                            var plxxxItem = $scope.planOderRelaseArr[xxx + leadTimeReceipts];
                            plxxxItem.plannedOrderReceipts = xxxItem.planOderReleaseQnt;
                        }
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        }

        function remove(id) {
            dataService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    //not thing to do
                });
        }

        function update($event) {
            for (var i = 0; i < $scope.data.specificationPackingDTOs.length; i++) {
                var item = $scope.data.specificationPackingDTOs[i];

                item.productSpecificationPackingID = $scope.method.getNewID();
            }

            dataService.update(
                context.id,
                $scope.data,
                function (data) {
                    jsHelper.processMessage(data.message);
                    window.location = context.refreshUrl + data.data.productSpecificationID;
                    $scope.data = data.data;
                },
                function (error) {
                    //do not need do anything
                });
        }

        function showActual() {
            $scope.isshow = 1;
        }
        function showPlan(qnt, leadTime) {
            if (qnt === null || leadTime === null) {
                bootbox.alert('The current minQnt is null or LeadTime is null, please add information to display the plan');
                return;
            }
            $scope.isshow = 2;
        }

        function getDates(startDate, endDate) {
            var dateRanges = [];
            var currentDate = startDate;

            function addDays(days) {
                var date = new Date(this.valueOf());
                date.setDate(date.getDate() + days);
                return date;
            }

            while (currentDate <= endDate) {
                dateRanges.push(currentDate);
                currentDate = addDays.call(currentDate, 1);
            }
            return dateRanges;
        }

        function getDate() {
            var fromDate = context.fromDate;
            var toDate = context.toDate;
            $scope.dateRanges = [];
            //get day && month && year		
            var frday = parseInt(fromDate.substr(0, 2));
            var frmm = parseInt(fromDate.substr(3, 2));
            var fryyyy = parseInt(fromDate.substr(6, 4));

            var frmmNew = (frmm - 1);

            var tday = parseInt(toDate.substr(0, 2));
            var tmm = parseInt(toDate.substr(3, 2));
            var tyyyy = parseInt(toDate.substr(6, 4));

            var tmmNew = (tmm - 1);

            var dateRanges = getDates(new Date(fryyyy, frmmNew, frday), new Date(tyyyy, tmmNew, tday));

            dateRanges.forEach(function (date) {
                var dd = date.getDate();
                var mm = date.getMonth() + 1;
                var yy = date.getFullYear();
                if (dd < 10) { dd = '0' + dd }
                if (mm < 10) { mm = '0' + mm }

                var currenDate = dd + '/' + mm + '/' + yy;

                var dateList = {
                    date: currenDate
                }
                $scope.dateRange.push(dateList);
            });
        }
        //Start Method
        function refresh() {
            window.location = context.refreshUrl + id;
        }

        function getNewID() {
            $scope.newID--;
            return $scope.newID;
        }

        $scope.event.get();

        function createservices() {
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
            dataService.supportServiceUrl = context.supportServiceUrl;

        }
    }
})();