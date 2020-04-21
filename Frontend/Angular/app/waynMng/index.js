
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
    $scope.fileScan = null;
    $scope.excelData = [];

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
                    $scope.list.employeeLists = data.data.data.employeeLists;
                    $scope.fileScan = data.data.fileScan;
                    $scope.$apply();
                    jQuery('#content').show();

                    var calendarData = [];
                    var tmpitem = null;
                    angular.forEach($scope.list, function (value, key) {
                        tmpitem = JSON.parse(JSON.stringify(value));
                        tmpitem.start = jsHelper.stringToDate(tmpitem.start_string);
                        tmpitem.end = jsHelper.stringToDate(tmpitem.end_string);
                        tmpitem.id = value.waynid;
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
                                    allDay: true
                                }, true // make the event "stick"
                                );
                            }
                            calendar.fullCalendar('unselect');
                        },
                        eventClick: function (event, jsEvent, view) {
                            $scope.selectedEvent = event;
                            scope.method.getDetail(event.workingDate);
                        },
                        events: calendarData,
                        eventRender: function (event, element, icon) {
                            //console.log(event);
                            element.find('.fc-event-title').append("<br/><span class='ultra-light'>Attendace Time On: </span><br/><span>" + event.workingDate + "</span><br/><span class='ultra-light'>Updated By " + event.updatorName + "</span>");
                            element.find('.fc-event-time').hide();
                        },
                        windowResize: function (event, ui) {
                            jQuery('#calendar').fullCalendar('render');
                        }
                    });

                    /* hide default buttons */
                    jQuery('.fc-header-right, .fc-header-center').hide();
                    jQuery('.fc-event-time').hide();

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
        updateList: function (item) {
            //console.log(item);
            //load data
            jsonService.updateList(item,
                function (data) {
                    $scope.$apply();
                },
                function (error) {
                    $scope.$apply();
                }
            );
        },
        updateNewList: function () {
            var pickedDate = jQuery('#pickedDate').val();

            if (!pickedDate) {
                alert('Please choose date !');
                return;
            }
            var available = true;
            angular.forEach($scope.list, function (value, key) {
                var checkDate = value.workingDate.replace(/-/g, "/");
                if (checkDate == pickedDate)
                {
                    alert('Please choose another date !');
                    available = false;
                }
            });
            if (available == true)
            {
                //load data
                var excelList = $scope.excelData;
                jsonService.updateNewList(excelList, pickedDate,
                    function (data) {
                        $scope.$apply();
                    },
                    function (error) {
                        $scope.$apply();
                    }
                );
            }
        },
        delete: function (item) {
            if (confirm('Are you sure?')) {
                // send delete command
                jsonService.deleteList(item,
                function (data) {
                    $scope.$apply();
                },
                function (error) {
                    $scope.$apply();
                }
            );
            }

            $scope.data = null;
            jQuery('#eventForm').modal('hide');
        },
        add: function () {
            jQuery('#newEventForm').modal();
        },
        changeFile: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.fileScan.fileLocation = img.fileURL;
                        scope.fileScan.friendlyName = img.filename;
                        scope.fileScan.fileScan_NewFile = img.filename;
                        scope.fileScan.fileScan_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        //downloadFile: function () {
        //    if ($scope.fileScan.fileLocation) {
        //        window.open($scope.fileScan.fileLocation);
        //    }
        //},
        removeFile: function () {
            $scope.fileScan.friendlyName = '';
            $scope.fileScan.fileLocation = '';
            $scope.fileScan.fileScan_NewFile = '';
            $scope.fileScan.fileScan_HasChange = true;
        },
        generateExcel: function (date) {
            jsonService.getExcelReport(
                date,
                function (data) {
                    window.location = context.backendReportUrl + data.data + '.xlsm';
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    }

    //
    // method
    //
    $scope.method = {
        getDetail: function (date) {
            //load data
            jsonService.getDetail(date,
                function (data) {
                    $scope.list.employeeLists = data;
                    //console.log($scope.list.employeeLists);
                    $scope.$apply();
                    jQuery('#eventForm').modal();
                },
                function (error) {
                    $scope.$apply();
                }
            );
        }
    }

    //
    // init
    //
    $scope.event.init();

   function handleFile(e) {
     //Get the files from Upload control
        var files = e.target.files;
        var i, f;
     //Loop through files
        for (i = 0, f = files[i]; i != files.length; ++i) {
            var reader = new FileReader();
            var name = f.name;
            reader.onload = function (e) {
                var data = e.target.result;

                var result;
                var workbook = XLSX.read(data, { type: 'binary' });
                
                var sheet_name_list = workbook.SheetNames;
                sheet_name_list.forEach(function (y) { /* iterate through sheets */
                    //Convert the cell value to Json
                    var roa = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                    if (roa.length > 0) {
                        result = roa;
                    }
                });
                $scope.excelData = result;
            };
            reader.readAsArrayBuffer(f);
        }
    }

    //Change event to dropdownlist
    $('#files').change(handleFile);
}]);