(function () {
    'use strict';

    angular.module('tilsoftApp').controller('calendarController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
        //
        // property
        //
        $scope.viewmodel = dataService.getLocalData(dataService.dataKey);

        //
        // event
        //
        $scope.event = {
            init: function () {
                $('#content').show();
                $('#ribbon .breadcrumb').html('').append('<li>Leave Calendar</li>');
                $('#page-title').html('').append('LEAVE CALENDAR');
                $scope.method._renderCalendar();
                $scope.viewmodel = dataService.getLocalData(dataService.dataKey);

                //
                // load support data if needed
                //
                if ($scope.viewmodel.supportData.loadedStatus.indexOf('calendar') < 0) {
                    // load location
                    dataService.getSupportData(
                        function (data) {
                            $scope.viewmodel.supportData.locations = data.data.companyDTOs;
                            $scope.viewmodel.supportData.locations.push({
                                companyID: 5,
                                companyUD: '0000000005',
                                companyNM: 'Work out of Office',
                                isSelected: true,
                                annualLeaveTypeID: 5
                            });
                            angular.forEach($scope.viewmodel.supportData.locations, function (item) {
                                item.isSelected = true;
                            });
                            $scope.viewmodel.supportData.loadedStatus.push('calendar'); // mark as support data loaded for calendar form
                        },
                        function (error) {
                        }
                    );
                }

                //
                // loading init data
                //
                $('#calendar').fullCalendar('gotoDate', $scope.viewmodel.calendarStatus.currentMonth);
                $scope.method._loadCalendarData();
            },
            prev: function () {
                $('.fc-prev-button').click();
                $scope.method._loadCalendarData();
            },
            next: function () {
                $('.fc-next-button').click();
                $scope.method._loadCalendarData();
            },
            applyFilter: function () {
                $scope.viewmodel.data = [];
                $('#calendar').fullCalendar('removeEvents');
                debugger;
                $scope.viewmodel.filters.locations = [];
                angular.forEach($scope.viewmodel.supportData.locations, function (item) {
                    if (item.isSelected && item.companyID != 5) {
                        $scope.viewmodel.filters.locations.push(item.companyID);
                    }
                    else if (item.isSelected && item.companyID == 5) {
                        $scope.viewmodel.filters.locations.push(item.annualLeaveTypeID);
                    }
                });

                $scope.method._loadCalendarData();
            },
            clearFilter: function () {
                $scope.viewmodel.data = [];
                $('#calendar').fullCalendar('removeEvents');
                $scope.viewmodel.filters = {
                    locations: []
                };
                angular.forEach($scope.viewmodel.supportData.locations, function (item) {
                    item.isSelected = true;
                    $scope.viewmodel.filters.locations.push(item.companyID);
                });

                //
                // clear UI filter
                //
                $('#cboFactoryFilter').val(null).trigger('change');

                $scope.method._loadCalendarData();
            },
            locationSelect: function (item) {
                //if (item.companyID == 4) { // 4: location = factory (hack)
                //    $scope.viewmodel.filters.factories = [];

                //    //
                //    // clear UI filter
                //    //
                //    $('#cboFactoryFilter').val(null).trigger('change');
                //}
            }
        };
        $scope.$on('$destroy', function () {
            dataService.setLocalData(dataService.dataKey, $scope.viewmodel);
        });

        //
        // method
        //
        $scope.method = {
            getLocationClass: function (id) {
                return _getItemClass(id);
            },
            //
            // private method
            //
            //_refreshCalendar: function () {
            //    $('#calendar').fullCalendar('removeEvents');
            //    angular.forEach($scope.viewmodel.data, function (item) {
            //        $('#calendar').fullCalendar('renderEvent', {
            //            id: item.appointmentID,
            //            description: '<b>' + item.factoryRawMaterialOfficialNM + '</b><br/>' + item.description + '<br/><i>' + item.ownerNM + '</i>',
            //            start: item.startTime,
            //            end: item.endTime,
            //            icon: (item.totalAttachedFile > 0) ? '' : '',
            //            userID: item.userID,
            //            className: _getItemClass(item.companyID)
            //        }, true);
            //    });
            //},
            _loadCalendarData: function () {
                // check if data is loaded: calendar contain item with startdate month and year = getDate month and year
                var isExists = false;
                var mDate = new Date($("#calendar").fullCalendar('getDate'));
                $scope.viewmodel.calendarStatus.currentMonth = mDate;
                //angular.forEach($scope.viewmodel.data, function (item) {
                //    if (item.startTime.getUTCMonth() === mDate.getUTCMonth() && item.startTime.getUTCFullYear() === mDate.getUTCFullYear()) {
                //        isExists = true;
                //    }
                //});
                //if (!isExists) {
                //    $scope.method._loadData(mDate.getUTCMonth() + 1, mDate.getUTCFullYear());
                //}
                $scope.method._loadData(mDate.getUTCMonth() + 1, mDate.getUTCFullYear());
            },
            _loadData: function (m, y) {
                //
                // store filter in cookies
                //
                $scope.viewmodel.data = [];
                $scope.viewmodel.filters.month = m;
                $scope.viewmodel.filters.year = y;
                dataService.searchFilter.filters = $scope.viewmodel.filters;
                dataService.search(
                    function (data) {
                        $('#calendar').fullCalendar('removeEvents');
                        angular.forEach(data.data.data, function (item) {
                            debugger;
                            if (item.annualLeaveTypeID != 5) {
                                $('#calendar').fullCalendar('renderEvent', {
                                    id: item.annualLeaveTrackingID,
                                    title: item.subject,
                                    start: jsHelper.stringToDate(item.startTime),
                                    end: jsHelper.stringToDate(item.endTime),
                                    icon: (item.totalAttachedFile > 0) ? 'fa-file-o' : '',
                                    userID: item.userID,
                                    companyID: item.companyID,
                                    className: _getItemClass(item.companyID),
                                    employeeNM: item.employeeNM,
                                    remark: item.remark,
                                    managerRemark: item.managerRemark
                                }, true);
                            }
                            else {
                                $('#calendar').fullCalendar('renderEvent', {
                                    id: item.annualLeaveTrackingID,
                                    title: item.subject,
                                    start: jsHelper.stringToDate(item.startTime),
                                    end: jsHelper.stringToDate(item.endTime),
                                    icon: (item.totalAttachedFile > 0) ? 'fa-file-o' : '',
                                    userID: item.userID,
                                    companyID: item.companyID,
                                    className: _getItemClass(item.annualLeaveTypeID),
                                    employeeNM: item.employeeNM,
                                    remark: item.remark,
                                    managerRemark: item.managerRemark
                                }, true);
                            }
                        });
                    },
                    function (error) {
                    }
                );
            },
            _renderCalendar: function () {
                $('#calendar').fullCalendar({
                    header: {
                        left: 'title',
                        center: 'month,agendaWeek,agendaDay',
                        right: 'prev,today,next'
                    },
                    editable: false,
                    droppable: false,
                    weekNumbers: true,
                    weekNumberCalculation: 'iso',
                    eventClick: function (event, jsEvent, view) {
                        // alert(window.location);
                        var url = window.location.href;
                        var arr = url.split("/");
                        var result = arr[0] + "//" + arr[2];

                        window.location = result + '/AnnualLeaveMng/Edit/' + event.id;


                        //if (event.userID == context.userID) {
                        //    window.location = '#/event/edit/' + event.id;
                        //}
                        //else {
                        //    window.location = '#/event/view/' + event.id;
                        //}
                    },
                    eventRender: function (event, element, icon) {
                        element.find('.fc-title').html("<span class='ultra-light'>" + event.employeeNM + "</span>");
                        element.find('.fc-time').remove();
                        if (event.remark != null)
                            element.find('.fc-title').append("<br/><span class='ultra-light'>" + event.remark + "</span>");
                        if (event.managerRemark != null)
                            element.find('.fc-title').append("<br/><span class='ultra-light'>" + event.managerRemark + "</span>");

                        //element.attr('title', event.remark);

                        //if (event.companyID == 5) {
                        //    element.find('.fc-title').html("<span class='ultra-light'>" + event.employeeNM + "</span>");
                        //    element.find('.fc-time').remove();
                        //}
                        //else {
                        //    element.attr('title', event.remark);
                        //    //if (event.ownerName != "" && event.factoryRawMaterialOfficialNM != "") {
                        //    //    element.find('.fc-title').append("<br/><span class='ultra-light'>" + event.factoryRawMaterialOfficialNM + "</span><br/><span class='ultra-light'>(" + event.ownerName + ")</span>");
                        //    //}
                        //    //if (!event.icon == "") {
                        //    //    element.find('.fc-title').append("<i class='air air-top-right fa " + event.icon + " '></i>");
                        //    //}
                        //}
                    },
                    windowResize: function (event, ui) {
                        $('#calendar').fullCalendar('render');
                    }
                });

                /* hide default buttons */
                $('.fc-right, .fc-center').hide();
            }
        };

        //
        // bootstrap
        //
        $scope.event.init();
    }]);
})();


var _getItemClass = function (location) {
    switch (location) {
        case 1:
            return 'bg-color-blue';
        case 2:
            return 'bg-color-orange';
        case 3:
            return 'bg-color-greenLight';
        case 4:
            return 'bg-color-red';
        case 5:
            return 'bg-color-yellow-custom';
        default:
            return '';
    }
}