(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.select2']).controller('tilsoftController', annualLeaveController);
    annualLeaveController.$inject = ['$scope', 'dataService'];

    function annualLeaveController($scope, annualLeaveService) {
        $scope.data = [];
        $scope.employeeDTOs = [];
        $scope.annualLeaveTypeDTOs = [];
        $scope.annualLeaveStatusDTOs = [];
        $scope.toTimeRange = [];
        $scope.fromTimeRange = [];
        $scope.morningFromTime = null;
        $scope.morningToTime = null;
        $scope.afternoonFromTime = null;
        $scope.afternoonToTime = null;
        $scope.availableTime = 0;
      
        $scope.newID = -1;

        $scope.event = {
            initAnnualLeave: initAnnualLeave,
            saveAnnualLeave: saveAnnualLeave,
            deleteAnnualLeave: deleteAnnualLeave,
            removeWarehouse: removeWarehouse,
            addWarehouse: addWarehouse,
            calRequestedTimeOff: calRequestedTimeOff,
            approve: approve,
            reject: reject,
            cancel: cancel,
            bindAvailableTime: bindAvailableTime
        };

        // Init page Work Center
        function initAnnualLeave() {
            annualLeaveService.serviceUrl = context.serviceUrl;
            annualLeaveService.token = context.token;

            annualLeaveService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.annualLeaveTrackingDTO;
                    $scope.employeeDTOs = data.data.employeeDTOs;
                    $scope.annualLeaveTypeDTOs = data.data.annualLeaveTypeDTOs;
                    $scope.annualLeaveStatusDTOs = data.data.annualLeaveStatusDTOs;
                    $scope.fromTimeRange = data.data.fromTimeRange;
                    $scope.toTimeRange = data.data.toTimeRange;
                    $scope.morningFromTime = $scope.fromTimeRange[0];
                    $scope.morningToTime = $scope.toTimeRange[0]; 
                    $scope.afternoonFromTime = $scope.fromTimeRange[1];
                    $scope.afternoonToTime = $scope.toTimeRange[1];
                    
                    //nếu là mới lúc nào status cũng pending 
                    if (context.id == 0) {
                        $scope.data.statusID = 1;
                        $scope.data.affectedYear = new Date().getFullYear();
                    }
                    else {
                        $scope.availableTime = $scope.data.totalAvailableDay - $scope.data.totalDaysOff;
                    }

                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        // Save a Work Center
        function saveAnnualLeave() {
            if ($scope.editForm.$valid) {
                var isValid = true
                var messageError = "";
                angular.forEach($scope.data.annualLeaveTrackingDetailDTOs, function (item) {
                    if (item.leaveTypeID == null) {
                        isValid = false;
                        messageError = "Invalid input data, please check leave type";
                    }     

                    if (jsHelper.stringToDate(item.fromDate).getFullYear() != $scope.data.affectedYear
                        || jsHelper.stringToDate(item.toDate).getFullYear() != $scope.data.affectedYear) {
                        isValid = false;
                        messageError = "Invalid input data, please check leave date";
                    }
                });

                if (isValid) {
                    annualLeaveService.update(
                        context.id,
                        $scope.data,
                        function (data) {
                            jsHelper.processMessage(data.message);

                            if (data.message.type === 0) {
                                window.location = context.refreshUrl + data.data.annualLeaveTrackingDTO.annualLeaveTrackingID;
                                $scope.data = data.data.annualLeaveTrackingDTO;
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.data.message.message);
                        }
                    );
                }
                else
                    jsHelper.showMessage('warning', messageError);
                
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        };
        function approve() {
            if ($scope.editForm.$valid) {
                $scope.data.statusID = 3;
                annualLeaveService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.annualLeaveTrackingDTO.annualLeaveTrackingID;
                            $scope.data = data.data.annualLeaveTrackingDTO;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.message.message);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        };
        function reject() {
            if ($scope.editForm.$valid) {
                $scope.data.statusID = 4;
                annualLeaveService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.annualLeaveTrackingDTO.annualLeaveTrackingID;
                            $scope.data = data.data.annualLeaveTrackingDTO;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.message.message);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        };
        function cancel() {
            if ($scope.editForm.$valid) {
                $scope.data.statusID = 2;
                annualLeaveService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.annualLeaveTrackingDTO.annualLeaveTrackingID;
                            $scope.data = data.data.annualLeaveTrackingDTO;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.message.message);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
        };
        // Delete a Work Center
        function deleteAnnualLeave() {
            annualLeaveService.delete(
                context.id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);

                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    var contentMessage = '';
                    jsHelper.showMessage('warning', contentMessage);
                }
            );
        };

        //Warehouse
        function removeWarehouse($event, id) {
            if ($event !== null) {
                $event.preventDefault();
            }
            var isExist = false;
            for (var j = 0; j < $scope.data.annualLeaveTrackingDetailDTOs.length; j++) {
                if ($scope.data.annualLeaveTrackingDetailDTOs[j].annualLeaveTrackingDetailID === id) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.data.annualLeaveTrackingDetailDTOs.splice(j, 1);
            }
        };

        function addWarehouse($event) {
            if ($event !== null) {
                $event.preventDefault();
            }         

            $scope.data.annualLeaveTrackingDetailDTOs.push({
                annualLeaveTrackingDetailID: $scope.method.getNewID(),                
                fromTime: $scope.morningFromTime,
                toTime: $scope.afternoonToTime               
            });
        };
      
        function bindAvailableTime(employeeID) {
            for (var j = 0; j < $scope.employeeDTOs.length; j++) {
                var employeeDTO = $scope.employeeDTOs[j];
                if (employeeDTO.employeeID == employeeID) {            
                    $scope.availableTime = employeeDTO.totalAvailableTime - employeeDTO.totalDaysOff;
                    break;
                }
            }
        };
        
        function calRequestedTimeOff() {
            var totalWeekends = 0;
            for (var j = 0; j < $scope.data.annualLeaveTrackingDetailDTOs.length; j++) {
                var daysOff = 0;

                var itemDetail = $scope.data.annualLeaveTrackingDetailDTOs[j];

                var fromdate = jsHelper.stringToDate(itemDetail.fromDate);
                var todate = jsHelper.stringToDate(itemDetail.toDate);
                //var fromdate = jsHelper.stringToDate(item.fromDate + ' ' + item.fromTime + ':00');
                //var todate = jsHelper.stringToDate(item.toDate + ' ' + item.toTime + ':00');
                if (todate < fromdate) return; //avoid infinite loop;
              
                for (var i = fromdate; i <= todate; i.setDate(i.getDate() + 1)) {
                    if (i.getDay() != 0 && i.getDay() != 6) daysOff++; // trừ thứ 7 chủ nhật
                };          

                //kiểm tra nếu là nữa ngày thì trừ bớt
                if ((itemDetail.fromTime == $scope.morningFromTime && itemDetail.toTime == $scope.morningToTime)
                    || (itemDetail.fromTime == $scope.afternoonFromTime && itemDetail.toTime == $scope.afternoonToTime))
                    daysOff = daysOff - 0.5;
                else if (itemDetail.fromTime == $scope.afternoonFromTime && itemDetail.toTime == $scope.morningToTime)
                    daysOff = daysOff - 1;                    

                $scope.data.annualLeaveTrackingDetailDTOs[j].numberOfDays = daysOff;
                //// To calculate the no. of days between two dates 
                //var dayOff = temp / (1000 * 3600 * 24);
                ////alert(dayOff);          

                var totalWeekends = totalWeekends + daysOff;
               
            }
            $scope.data.requestedTimeOff = totalWeekends;  
            $scope.data.affectedYear = todate.getFullYear();           
        };
        

        $scope.method = {
            getNewID: function () {
                $scope.newID--;
                return $scope.newID;
            }
        };

        initAnnualLeave();
    };
})();