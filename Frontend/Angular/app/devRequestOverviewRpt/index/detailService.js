angular.module('tilsoftApp').service('detailService', ['$rootScope', 'dataService', function ($rootScope, dataService) {
    var detailServicePointer = this;

    this.data = null;
    this.person = null;
    this.parentScope = null;

    this.openForm = function (dev) {
        this.person = dev;
        dataService.getDetail(
            this.parentScope.filters.year,
            dev.userID,
            function (data) {
                detailServicePointer.data = data.data;
                jsHelper.showPopup('rptDetail', function () { });

                // re-generate chart
                var requests = [];
                var estimate = {
                    name: 'Estimated Hour Spend',
                    data: []
                };
                var actual = {
                    name: 'Actual Hour Spend',
                    data: []
                };
                angular.forEach(detailServicePointer.data.resolvedRequestByPersons, function (item) {
                    requests.push(item.devRequestID + ': ' + item.title);
                    estimate.data.push(item.estWorkingHour);
                    actual.data.push(item.actualWorkingHour);
                }, null);
                Highcharts.chart('chartDetail', {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: 'Resolved Request Status'
                    },
                    subtitle: {
                        text: 'Compare between estimated hour with actual hour'
                    },
                    xAxis: {
                        categories: requests,
                        title: {
                            text: null
                        }
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Working hours',
                            align: 'high'
                        },
                        labels: {
                            overflow: 'justify'
                        }
                    },
                    tooltip: {
                        valueSuffix: ' hours'
                    },
                    plotOptions: {
                        bar: {
                            dataLabels: {
                                enabled: true
                            }
                        }
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'top',
                        x: -40,
                        y: 80,
                        floating: true,
                        borderWidth: 1,
                        backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                        shadow: true
                    },
                    credits: {
                        enabled: false
                    },
                    series: [estimate, actual]
                });

                // render calendar
                jQuery('#calendarPanel').html('');
                var calendarID = 'calendar' + dataService.getIncrementId();
                jQuery('#calendarPanel').html('<div id="' + calendarID + '"></div>');
                var calendarData = [];
                angular.forEach(detailServicePointer.data.planingByPersons, function (item) {
                    calendarData.push({
                        id: item.devRequestID,
                        title: item.devRequestID + ': ' + item.title,
                        start: jsHelper.stringToDate(item.start_string),
                        end: jsHelper.stringToDate(item.end_string),
                        color: detailServicePointer.getRandomColor()
                    });
                }, null);
                
                console.log(calendarData);

                jQuery('#' + calendarID).fullCalendar({
                    header: {
                        left: 'title',
                        center: 'month,agendaWeek,agendaDay',
                        right: 'prev,today,next'
                    },
                    displayEventTime: false,
                    buttonText: {
                        prev: '<i class="fa fa-chevron-left"></i>',
                        next: '<i class="fa fa-chevron-right"></i>'
                    },
                    editable: false,
                    droppable: false,
                    events: calendarData,
                    windowResize: function (event, ui) {
                        jQuery('#' + calendarID).fullCalendar('render');
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

            }
        );
    };

    this.closeForm = function () {
        jsHelper.hidePopup('rptDetail', function () { });
    };

    this.getRandomColor = function(){
        var letters = '0123456789ABCDEF';
        var color = '#';
        for (var i = 0; i < 6; i++) {
            color += letters[Math.floor(Math.random() * 16)];
        }
        return color;
    }
}]);