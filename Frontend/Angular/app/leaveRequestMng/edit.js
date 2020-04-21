//
//APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['customFilters', 'furnindo-directive']);

tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    $scope.dataContainer = null;
    $scope.newID = -1;
    $scope.leaveTypes = null;
    $scope.leaveRequestTimes = null;
    $scope.managers = null;
    $scope.directors = null;

    $scope.event = {
        init: function () {
            leaveRequestMngService.load(
                context.id,
                function (data) {
                    $scope.dataContainer = data.data.data;
                    $scope.leaveTypes = data.data.leaveTypes;
                    $scope.leaveRequestTimes = data.data.leaveRequestTimes;
                    $scope.managers = data.data.managers;
                    $scope.directors = data.data.directors;
                   
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                    if (data.message.type == 2) {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.datdataContainer = null;
                    $scope.$apply();
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                leaveRequestMngService.update(
                    context.id,
                    $scope.dataContainer,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.leaveRequestID);
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
        delete: function () {
            if (confirm('Are you sure ?')) {
                leaveRequestMngService.delete(
                    context.id,
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
        }
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
        }
    };

    //quick seach employee form
    var quickSearchEmployeeGrid = jQuery('#quickSearchEmployeeTable').scrollableTable(
        function (currentPage) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                scope.quickSearchEmployee.filters.pageIndex = currentPage;
                scope.quickSearchEmployee.method.searchEmployee();
            });
        },
        function (sortedBy, sortedDirection) {
            //do nothing
        }
    );
    searchEmployeeTimer = null;
    $scope.quickSearchEmployee = {
        data: null,
        filters: {
            filters: {
                searchQuery: ''
            },
            sortedBy: 'employeeUD',
            sortedDirection: 'ASC',
            pageSize: 10,
            pageIndex: 1
        },
        totalPage: 0,

        method: {
            searchEmployee: function () {
                leaveRequestMngService.quickSearchEmployee(
                    $scope.quickSearchEmployee.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quickSearchEmployee.data = data.data;
                            $scope.quickSearchEmployee.totalPage = Math.ceil(data.totalRows / $scope.quickSearchEmployee.filters.pageSize);
                            quickSearchEmployeeGrid.updateLayout();
                            $scope.$apply();
                            jQuery('#employee-popup').show();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },

        event: {
            searchBoxKeyUp: function () {
                if (jQuery('#txtEmployee').val().length >= 3) {
                    clearTimeout(searchEmployeeTimer);
                    searchEmployeeTimer = setTimeout(
                        function () {
                            $scope.quickSearchEmployee.filters.filters.searchQuery = jQuery('#txtEmployee').val();
                            $scope.quickSearchEmployee.filters.pageIndex = 1;
                            $scope.quickSearchEmployee.method.searchEmployee();
                        },
                        500);
                }
            },
            close: function ($event, clearSearchBox) {
                jQuery('#employee-popup').hide();
                if (clearSearchBox) {
                    $scope.dataContainer.employeeID = null;
                    $scope.dataContainer.employeeUD = null;

                    $scope.dataContainer.employeeNM = null;
                }
                $scope.quickSearchEmployee.data = null;
                $event.preventDefault();
            },
            ok: function ($event, id) {
                $scope.dataContainer.requesterID = id;
                
                jQuery.each($scope.quickSearchEmployee.data, function () {
                    if (this.employeeID == id) {
                        $scope.dataContainer.requesterID = this.employeeID;
                        $scope.dataContainer.requesterUD = this.employeeUD;
                        $scope.dataContainer.requesterNM = this.employeeNM;
                    }
                });
                $scope.quickSearchEmployee.event.close($event, false);
            }
        }
    }
    //
    // init
    //
    $scope.event.init();

    //$("#fromDate").datepicker();
    $("#fromDate").on("change", function () {
        var tmpFromDate = $(this).val();
        var tmpToDate = $('#toDate').val();
        var fromDateArray = tmpFromDate.split('/');
        var toDateArray = tmpToDate.split('/');
        
        var fromDate = new Date(fromDateArray[1] + '/' + fromDateArray[0] + '/' + fromDateArray[2]);
        var toDate = new Date(toDateArray[1] + '/' + toDateArray[0] + '/' + toDateArray[2]);

        var timeDiff = Math.abs(toDate.getTime() - fromDate.getTime());

        var leaveTime = $('#leaveTime').val();
        var totalDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
        if (toDate.getTime() == fromDate.getTime()) {
            totalDays = totalDays + 1;
        }else{
            totalDays = totalDays;
        }
        var input = $('#totalDays');
        input.val(totalDays);
        input.trigger('input');
    });

    $("#toDate").on("change", function () {
        var tmpFromDate = $('#fromDate').val();
        var tmpToDate = $(this).val();
        var fromDateArray = tmpFromDate.split('/');
        var toDateArray = tmpToDate.split('/');

        var fromDate = new Date(fromDateArray[1] + '/' + fromDateArray[0] + '/' + fromDateArray[2]);
        var toDate = new Date(toDateArray[1] + '/' + toDateArray[0] + '/' + toDateArray[2]);

        var timeDiff = Math.abs(toDate.getTime() - fromDate.getTime());

        var leaveTime = $('#leaveTime').val();
        var totalDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
        if (toDate.getTime() == fromDate.getTime()) {
            totalDays = totalDays + 1;
        }else {
            totalDays = totalDays;
        }
        var input = $('#totalDays');
        input.val(totalDays);
        input.trigger('input');
    });
}]);