
//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.list = null;
    $scope.data = null;
    $scope.selectedEvent = null;
    $scope.meetingLocations = null;
    $scope.timeRange = null;

    $scope.filters = {};
    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    $scope.list = data.data.data;
                    $scope.meetingLocations = data.data.meetingLocations;
                    $scope.timeRange = data.data.timeRange;

                    $scope.$apply();
                    jQuery('#content').show();

                    var calendarData = [];
                    var tmpitem = null;
                    angular.forEach($scope.list, function (value, key) {
                        tmpitem = JSON.parse(JSON.stringify(value));
                        tmpitem.start = jsHelper.stringToDate(tmpitem.start_string);
                        tmpitem.end = jsHelper.stringToDate(tmpitem.end_string);
                        tmpitem.id = value.appointmentID;
                        tmpitem.className = $scope.method.getAppointmentClass(value.meetingLocationID);
                        this.push(tmpitem);
                    }, calendarData);

                    var hdr = {
                        left: 'title',
                        center: 'month,agendaWeek,agendaDay',
                        right: 'prev,today,next'
                    };

                    /* calendar declaration */
                    jQuery('#calendar').fullCalendar({
                        header: hdr,                        
                        buttonText: {
                            prev: '<i class="fa fa-chevron-left"></i>',
                            next: '<i class="fa fa-chevron-right"></i>'
                        },
                        editable: false,
                        droppable: false,
                        select: function (start, end, allDay) {
                            var title = prompt('Event Title:');
                            if (title) {
                                calendar.fullCalendar('renderEvent', {
                                    title: title,
                                    start: start,
                                    end: end,
                                    allDay: allDay
                                }, true // make the event "stick"
                                );
                            }
                            calendar.fullCalendar('unselect');
                        },
                        eventClick: function (event, jsEvent, view) {
                            scope.method.edit(event);
                        },
                        events: calendarData,
                        eventRender: function (event, element, icon) {
                            if (event.ownerName != "" && event.clientNM != "") {
                                element.find('.fc-event-title').append("<br/><span class='ultra-light'>" + event.clientNM + "</span><br/><span class='ultra-light'>(" + event.ownerName + ")</span>");
                            }
                            if (!event.icon == "") {
                                element.find('.fc-event-title').append("<i class='air air-top-right fa " + event.icon +
                                    " '></i>");
                            }
                        },
                        windowResize: function (event, ui) {
                            jQuery('#calendar').fullCalendar('render');
                        }
                    });

                    /* hide default buttons */
                    jQuery('.fc-header-right, .fc-header-center').hide();


                    jQuery('#calendar-buttons #btn-prev').click(function () {
                        jQuery('.fc-button-prev').click();
                        return false;
                    });

                    jQuery('#calendar-buttons #btn-next').click(function () {
                        jQuery('.fc-button-next').click();
                        return false;
                    });
                },
                function (error) {
                    $scope.list = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                    $scope.$apply();
                }
            );
        },
        update: function () {
            if (!$scope.eventForm.$valid) {
                alert(context.errMsg);
                return;
            }
            if ($scope.data.clientID == null) {
                alert(context.errMsg);
                return;
            }

            if ($scope.data.appointmentID == 0) {
                // send update command
                jsonService.update(
                    $scope.data.appointmentID,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            // refresh calendar
                            var eventSource = [];
                            eventSource.push({
                                appointmentID: data.data.appointmentID,
                                appointmentUD: data.data.appointmentUD,
                                clientID: data.data.clientID,
                                clientNM: data.data.clientNM,
                                userID: data.data.userID,
                                subject: data.data.subject,
                                meetingLocationID: data.data.meetingLocationID,
                                meetingLocationNM: data.data.meetingLocationNM,
                                startTime_Date: data.data.startTime_Date,
                                startTime_Time: data.data.startTime_Time,
                                endTime_Date: data.data.endTime_Date,
                                endTime_Time: data.data.endTime_Time,
                                remindTime_Date: data.data.remindTime_Date,
                                remindTime_Time: data.data.remindTime_Time,
                                description: data.data.description,
                                ownerName: context.userName,
                                id: data.data.appointmentID,
                                title: data.data.subject,
                                start: jsHelper.stringToDate(data.data.startTime_Date + ' ' + data.data.startTime_Time),
                                end: jsHelper.stringToDate(data.data.endTime_Date + ' ' + data.data.endTime_Time),
                                icon: (data.data.meetingLocationID == 3 ? 'fa-share' : 'fa-reply'),
                                className: $scope.method.getAppointmentClass(data.data.meetingLocationID)
                            });
                            jQuery('#calendar').fullCalendar('addEventSource', eventSource);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                // send update command
                jsonService.update(
                    $scope.data.appointmentID,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            // refresh calendar
                            $scope.selectedEvent.appointmentID = data.data.appointmentID;
                            $scope.selectedEvent.appointmentUD = data.data.appointmentUD;
                            $scope.selectedEvent.clientID = data.data.clientID;
                            $scope.selectedEvent.clientNM = data.data.clientNM;
                            $scope.selectedEvent.userID = data.data.userID;
                            $scope.selectedEvent.subject = data.data.subject;
                            $scope.selectedEvent.meetingLocationID = data.data.meetingLocationID;
                            $scope.selectedEvent.meetingLocationNM = data.data.meetingLocationNM;
                            $scope.selectedEvent.startTime_Date = data.data.startTime_Date;
                            $scope.selectedEvent.startTime_Time = data.data.startTime_Time;
                            $scope.selectedEvent.endTime_Date = data.data.endTime_Date;
                            $scope.selectedEvent.endTime_Time = data.data.endTime_Time;
                            $scope.selectedEvent.remindTime_Date = data.data.remindTime_Date;
                            $scope.selectedEvent.remindTime_Time = data.data.remindTime_Time;
                            $scope.selectedEvent.description = data.data.description;
                            $scope.selectedEvent.ownerName = data.data.ownerName;
                            $scope.selectedEvent.id = data.data.appointmentID;
                            $scope.selectedEvent.title = data.data.subject;
                            $scope.selectedEvent.start = jsHelper.stringToDate(data.data.startTime_Date + ' ' + data.data.startTime_Time);
                            $scope.selectedEvent.end = jsHelper.stringToDate(data.data.endTime_Date + ' ' + data.data.endTime_Time);
                            $scope.selectedEvent.icon = (data.data.meetingLocationID == 3 ? 'fa-share' : 'fa-reply');
                            $scope.selectedEvent.className = $scope.method.getAppointmentClass(data.data.meetingLocationID);


                            jQuery('#calendar').fullCalendar('updateEvent', $scope.selectedEvent);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );               
            }

            $scope.data = null;
            jQuery('#eventForm').modal('hide');
        },
        delete: function () {
            if (confirm('Are you sure?')) {
                // send delete command
                jsonService.delete(
                    $scope.selectedEvent.appointmentID,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            jQuery('#calendar').fullCalendar('removeEvents', $scope.selectedEvent.id);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }

            $scope.data = null;
            jQuery('#eventForm').modal('hide');
        },
        add: function () {
            $scope.data = {
                appointmentID: 0,
                appointmentUD: '',
                clientID: null,
                clientNM: null,
                userID: context.userId,
                subject: '',
                meetingLocationID: null,
                meetingLocationNM: null,
                startTime_Date: null,
                startTime_Time: null,
                endTime_Date: null,
                endTime_Time: null,
                remindTime_Date: null,
                remindTime_Time: null,
                description: null,
                ownerName: context.userName
            };

            jQuery('#eventForm').modal();
            jQuery('#client-autocomplete').val('');
        }
    }

    //
    // method
    //
    $scope.method = {
        edit: function (item) {
            $scope.selectedEvent = item;
            $scope.data = {
                appointmentID: item.appointmentID,
                appointmentUD: item.appointmentUD,
                clientID: item.clientID,
                clientNM: item.clientNM,
                userID: item.userID,
                subject: item.subject,
                meetingLocationID: item.meetingLocationID,
                meetingLocationNM: item.meetingLocationNM,
                startTime_Date: item.startTime_Date,
                startTime_Time: item.startTime_Time,
                endTime_Date: item.endTime_Date,
                endTime_Time: item.endTime_Time,
                remindTime_Date: item.remindTime_Date,
                remindTime_Time: item.remindTime_Time,
                description: item.description,
                ownerName: item.ownerName
            };

            $scope.$apply();
            jQuery('#clientTextBox').show();
            jQuery('#clientSearchBox').hide();
            jQuery('#eventForm').modal();
            jQuery('#client-autocomplete').val(item.clientNM);
        },
        getAppointmentClass: function(locationId){
            switch(locationId){
                case 1: 
                    return 'bg-color-blue';
        
                case 2:
                    return 'bg-color-orange';

                case 3:
                    return 'bg-color-greenLight';

                case 4:
                    return 'bg-color-red';

                default:
                    return '';
            }
        }
    }

    //
    // init
    //
    $scope.event.init();
}]);

//
// JQUERY EXTENSION DECLARATION
//
jQuery(document).ready(function () {
    jQuery('#btnSwitch').click(function () {
        jQuery('#clientTextBox').hide();
        jQuery('#clientSearchBox').show();
        scope.data.clientID = null;
        scope.data.clientNM = null;
        scope.$apply();
        jQuery('#client-autocomplete').val('');
    });

    jQuery('#client-autocomplete').devbridgeAutocomplete({
        serviceUrl: jsonService.serviceUrl + 'quicksearchclient',
        dataType: 'json',
        minChars: 3,
        ajaxSettings: {
            cache: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jsonService.token
            },
            type: "GET"
        },
        onSelect: function (suggestion) {
            scope.data.clientID = suggestion.data.clientID;
            scope.data.clientNM = suggestion.data.clientNM;
            scope.$apply();

            jQuery('#clientTextBox').show();
            jQuery('#clientSearchBox').hide();
        },
        transformResult: function (response) {
            return {
                suggestions: jQuery.map(response, function (item) {
                    return { value: item.clientNM, data: item };
                })
            };
        },
        onInvalidateSelection: function () {
            scope.data.clientID = null;
            scope.data.clientNM = null;
            scope.$apply();
            jQuery('#client-autocomplete').val('');
        },
        showNoSuggestionNotice: true,
        noCache: true,
        onSearchStart: function (query) {
            jsHelper.loadingSwitch(true);
        },
        onSearchComplete: function (query, suggestions) {
            jsHelper.loadingSwitch(false);
        }
    });
});