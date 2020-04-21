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
                $('#ribbon .breadcrumb').html('').append('<li>Sales Agenda</li>');
                $('#page-title').html('').append('Purchasing Calendar');
                $scope.method._renderCalendar();
                $scope.viewmodel = dataService.getLocalData(dataService.dataKey);

                //
                // load support data if needed
                //
                if ($scope.viewmodel.supportData.loadedStatus.indexOf('calendar') < 0) {
                    // load location
                    dataService.getSupportData(
                        'calendar',
                        function (data) {
                            $scope.viewmodel.supportData.locations = data.data.meetingLocationDTOs;
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

                $scope.viewmodel.filters.locations = [];
                angular.forEach($scope.viewmodel.supportData.locations, function (item) {
                    if (item.isSelected) {
                        $scope.viewmodel.filters.locations.push(item.meetingLocationID);
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
                    $scope.viewmodel.filters.locations.push(item.meetingLocationID);
                });

                //
                // clear UI filter
                //
                $('#cboFactoryFilter').val(null).trigger('change');

                $scope.method._loadCalendarData();
            },
            locationSelect: function (item) {
                //if (item.meetingLocationID == 4) { // 4: location = factory (hack)
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
            //            className: _getItemClass(item.meetingLocationID)
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
                            $('#calendar').fullCalendar('renderEvent', {
                                id: item.purchasingCalendarAppointmentID,
                                title: item.subject,
                                description: item.description,
                                start: jsHelper.stringToDate(item.startTime),
                                end: jsHelper.stringToDate(item.endTime),
                                icon: (item.totalAttachedFile > 0) ? 'fa-file-o' : '',
                                userID: item.userID,
                                meetingLocationID: item.meetingLocationID,
                                className: _getItemClass(item.meetingLocationID),
                                factoryRawMaterialOfficialNM: item.factoryRawMaterialOfficialNM,
                                ownerName: item.ownerNM
                            }, true);
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
                        if (event.userID == context.userID) {
                            window.location = '#/event/edit/' + event.id;
                        }
                        else {
                            window.location = '#/event/view/' + event.id;
                        }
                    },
                    eventRender: function (event, element, icon) {
                        if (event.meetingLocationID == 5) {
                            element.find('.fc-title').html("<span class='ultra-light'>" + event.ownerName + "</span>");
                            element.find('.fc-time').remove();
                        }
                        else {
                            element.attr('title', event.description);
                            if (event.ownerName != "" && event.factoryRawMaterialOfficialNM != "") {
                                element.find('.fc-title').append("<br/><span class='ultra-light'>" + event.factoryRawMaterialOfficialNM + "</span><br/><span class='ultra-light'>(" + event.ownerName + ")</span>");
                            }
                            if (!event.icon == "") {
                                element.find('.fc-title').append("<i class='air air-top-right fa " + event.icon + " '></i>");
                            }
                        }
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
        default:
            return '';
    }
}