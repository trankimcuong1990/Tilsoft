(function () {
    'use strict';
    //
    // APP START
    //
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
        $scope.workOrderInfo = null;
        $scope.plannings = null;
        $scope.durationDates = [];
        $scope.arisingByDates = [];
        $scope.statisticsByDates = [];
        $scope.isShowTeam = false;

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
                dataService.getGrantChart(
                    context.id,
                    function (data) {
                        $scope.workOrder = data.data.workOrderInfo;
                        $scope.plannings = data.data.plannings;
                        //$scope.assignedTeams = data.data.assignedTeams;
                        $scope.arisingByDates = data.data.arisingByDates;
                        $scope.statisticsByDates = data.data.statisticsByDates;

                        //get duration date
                        var startDate = $scope.method.convertoToDate($scope.workOrder.startDate);
                        var finishDate = $scope.method.convertoToDate($scope.workOrder.finishDate);
                        $scope.durationDates = $scope.method.getDates(startDate, finishDate);

                        //show to page
                        jQuery('#content').show();
                    },
                    function (error) {
                        $scope.data = null;
                    }
                );
            },

            showHideTeam: function ($event) {
                $scope.isShowTeam = !$scope.isShowTeam;
                $event.currentTarget.innerHTML = $scope.isShowTeam ? "Hide Team" : "Show Team";
                $event.currentTarget.className = $scope.isShowTeam ? "btn btn-warning btn-xs m-bottom-15" : "btn btn-primary btn-xs m-bottom-15";                
            }

        };

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

            getDates: function (startDate, endDate) {
                var dates = [],
                currentDate = startDate;
                var addDays = function (days) {
                    var date = new Date(this.valueOf());
                    date.setDate(date.getDate() + days);
                    return date;
                };
                while (currentDate <= endDate) {
                    dates.push($scope.method.formatDateDDMMYYYY(currentDate));
                    currentDate = addDays.call(currentDate, 1);
                }
                return dates;
            },

            formatDateDDMMYYYY: function (dateObj) {
                var pad = function (n) {
                    return n < 10 ? "0" + n : n;
                }
                return pad(dateObj.getDate()) + "/" + pad(dateObj.getMonth() + 1) + "/" + dateObj.getFullYear();
            },
            
            getArisingExcludeTeam: function (workOrderID, workCenterID, productionItemID, arisingDate) {
                var total = parseFloat(0);
                angular.forEach($scope.arisingByDates, function (item) {
                    if (item.workOrderID === workOrderID && item.workCenterID === workCenterID && item.productionItemID === productionItemID && item.arisingDate === arisingDate) {
                        total += parseFloat(item.producedQnt);
                    }
                });
                return total;
            },

            getArisingIncludeTeam: function (workOrderID, workCenterID, productionItemID, productionTeamID, arisingDate) {
                var total = parseFloat(0);
                angular.forEach($scope.arisingByDates, function (item) {
                    if (item.workOrderID === workOrderID && item.workCenterID === workCenterID && item.productionItemID === productionItemID && item.productionTeamID === productionTeamID && item.arisingDate === arisingDate) {
                        total += parseFloat(item.producedQnt);
                    }
                });
                return total;
            },

            getStatisticsExcludeTeam: function (workOrderID, workCenterID, productionItemID, producedDate) {
                var total = parseFloat(0);
                angular.forEach($scope.statisticsByDates, function (item) {
                    if (item.workOrderID === workOrderID && item.workCenterID === workCenterID && item.productionItemID === productionItemID && item.producedDate === producedDate) {
                        total += parseFloat(item.producedQnt);
                    }
                });
                return total;
            },

            getStatisticsIncludeTeam: function (workOrderID, workCenterID, productionItemID, productionTeamID, producedDate) {
                var total = parseFloat(0);
                angular.forEach($scope.statisticsByDates, function (item) {
                    if (item.workOrderID === workOrderID && item.workCenterID === workCenterID && item.productionItemID === productionItemID && item.productionTeamID === productionTeamID && item.producedDate === producedDate) {
                        total += parseFloat(item.producedQnt);
                    }
                });
                return total;
            },

            getProducedQnt: function (planningItem, durationDate) {
                var producedQnt = parseFloat(0);
                if (planningItem.hasBOM) {
                    if (planningItem.groupIndex === 1) {
                        producedQnt = $scope.method.getArisingExcludeTeam(planningItem.workOrderID, planningItem.workCenterID, planningItem.productionItemID, durationDate);
                    }
                    else if (planningItem.groupIndex === 2) {
                        producedQnt =  $scope.method.getArisingIncludeTeam(planningItem.workOrderID, planningItem.workCenterID, planningItem.productionItemID, planningItem.productionTeamID, durationDate);
                    }
                }
                else {
                    if (planningItem.groupIndex === 1) {
                        producedQnt = $scope.method.getStatisticsExcludeTeam(planningItem.workOrderID, planningItem.workCenterID, planningItem.productionItemID, durationDate);
                    }
                    else if (planningItem.groupIndex === 2) {
                        producedQnt = $scope.method.getStatisticsIncludeTeam(planningItem.workOrderID, planningItem.workCenterID, planningItem.productionItemID, planningItem.productionTeamID, durationDate);
                    }
                }
                return producedQnt;
            },

            getStatusClass: function (planningItem, durationDateItem) {                
                var currentDate = new Date();
                var finishDate = $scope.method.convertoToDate($scope.workOrder.finishDate);

                var _startDate = $scope.method.convertoToDate(planningItem.startDate);
                var _finishDate = $scope.method.convertoToDate(planningItem.finishDate);
                var _durationDate = $scope.method.convertoToDate(durationDateItem);
                //var producedQnt = $scope.method.getProducedQnt(planningItem, durationDateItem);
                var classStatus = "";
                
                if (durationDateItem === $scope.method.formatDateDDMMYYYY(currentDate)) {
                    classStatus = "current-date";
                }
                else if (_startDate <= _durationDate && _durationDate <= _finishDate) {
                    if (planningItem.producedQnt >= planningItem.planQnt) {
                        classStatus = "enought";
                    }
                    else if (currentDate > finishDate) {
                        classStatus = "over-time";
                    }
                    else if (planningItem.producedQnt === 0) {
                        classStatus = "not-arising";
                    }
                    else if (planningItem.planQnt > planningItem.producedQnt) {
                        classStatus = "not-enought";
                    }        
                }
                return classStatus;
            },

            getGroupClass: function (planningItem) {
                var classStatus = "";
                if (planningItem.workCenterIndex === 1) {
                    classStatus = "work-center";
                }
                else if (planningItem.groupIndex === 1) {
                    classStatus = "component";
                }
                else if (planningItem.groupIndex === 2) {
                    classStatus = "team";
                }
                return classStatus;
            },

            getSelectedRowClass: function (planningItem) {
                var classStatus = "";
                if (planningItem.isSelected) {
                    classStatus = "selected-row";
                }
                return classStatus;
            },

            getShowHideTeamStatus: function (planningItem, isShowTeam) {
                var result = true;
                if (planningItem.groupIndex==2) {
                    result = isShowTeam;
                }
                return result;
            }

        }

        $timeout(function () {
            $scope.event.init();
        }, 0);


    }]);
})();

