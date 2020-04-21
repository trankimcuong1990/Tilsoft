(function () {
    'use strict';

    angular.module('tilsoftApp').controller('eventEditController', ['$scope', '$routeParams', 'dataService', function ($scope, $routeParams, dataService) {
        //
        // property
        //
        $scope.employeeDepartmentDTOs = null;
        $scope.viewmodel = {};
        $scope.newID = -1;
        $scope.data = {
            appointmentID: 0,
            appointmentUD: '',
            clientID: null,
            userID: context.userID,
            personInChargeID: 0,
            personInChargeNM: '',
            subject: '',
            meetingLocationID: null,
            factoryID: null,
            startDate: '',
            startTime: '',
            endDate: '',
            endTime: '',
            remindDate: '',
            remindTime: '',
            description: '',
            meetingMinute: '',
            clientUD: '',
            clientNM: '',
            ownerNM: context.userName,
            meetingLocationNM: '',
            factoryUD: '',
            managerUserID: null,
            employeeID: null,
            employeeNM: '',
            appointmentAttachedFileDTOs: [],
            agendaMngUsers: []
        };

        //
        // event
        //
        $scope.event = {
            init: function () {
                $('#content').hide();
                $('#ribbon .breadcrumb').html('').append('<li><a href="#/">Sales Agenda</a></li><li>Event</li>');
                $('#page-title').html('').append('<a href="#/">Sales Agenda</a> <span>> Event</span>');
                $scope.viewmodel = dataService.getLocalData(dataService.dataKey);

                //
                // load support data if needed
                //
                if ($scope.viewmodel.supportData.loadedStatus.indexOf('editEvent') < 0) {
                    // load location & factories
                    dataService.getSupportData(
                        'editEvent',
                        function (data) {
                            $scope.viewmodel.supportData.timeRange = data.data.timeRange;
                            $scope.viewmodel.supportData.users = data.data.users;
                            $scope.viewmodel.supportData.appointmentAttachedFileTypes = data.data.appointmentAttachedFileTypes;
                            $scope.viewmodel.supportData.sales = data.data.sales;
                            $scope.viewmodel.supportData.employeeDepartmentDTOs = data.data.employeeDepartmentDTOs;
                            $scope.viewmodel.supportData.meetingLocations = [];
                            angular.forEach($scope.viewmodel.supportData.locations, function (item) {
                                if (item.meetingLocationID < 5) {
                                    $scope.viewmodel.supportData.meetingLocations.push({ meetingLocationID: item.meetingLocationID, meetingLocationNM: item.meetingLocationNM });
                                }
                            });

                            $scope.viewmodel.supportData.loadedStatus.push('editEvent'); // mark as support data loaded for calendar form
                        },
                        function (error) {
                        }
                    );
                }
                else {
                    $scope.viewmodel.supportData.meetingLocations = [];
                    angular.forEach($scope.viewmodel.supportData.locations, function (item) {
                        if (item.meetingLocationID < 5) {
                            $scope.viewmodel.supportData.meetingLocations.push({ meetingLocationID: item.meetingLocationID, meetingLocationNM: item.meetingLocationNM });
                        }
                    });
                }
                if ($scope.viewmodel.supportData.loadedStatus.indexOf('calendar') < 0) {
                    // load location & factories
                    dataService.getSupportData(
                        'calendar',
                        function (data) {
                            $scope.viewmodel.supportData.locations = data.data.meetingLocations;
                            angular.forEach($scope.viewmodel.supportData.locations, function (item) {
                                item.isSelected = true;
                            });
                            $scope.viewmodel.supportData.factories = data.data.factories;
                            $scope.viewmodel.supportData.employeeDepartmentDTOs = data.data.employeeDepartmentDTOs;
                            $scope.viewmodel.supportData.meetingLocations = [];
                            angular.forEach($scope.viewmodel.supportData.locations, function (item) {
                                if (item.meetingLocationID < 5) {
                                    $scope.viewmodel.supportData.meetingLocations.push({ meetingLocationID: item.meetingLocationID, meetingLocationNM: item.meetingLocationNM });
                                }
                            });

                            $scope.viewmodel.supportData.loadedStatus.push('calendar'); // mark as support data loaded for calendar form
                        },
                        function (error) {
                        }
                    );
                }
                else {
                    $scope.viewmodel.supportData.meetingLocations = [];
                    angular.forEach($scope.viewmodel.supportData.locations, function (item) {
                        if (item.meetingLocationID < 5) {
                            $scope.viewmodel.supportData.meetingLocations.push({ meetingLocationID: item.meetingLocationID, meetingLocationNM: item.meetingLocationNM });
                        }
                    });
                }



                //
                // load event data
                //
                var id = $routeParams.id;
                if (id > 0) {
                    dataService.load(
                        id,
                        null,
                        function (data) {
                            $scope.data = data.data.data;

                            $('#clientAutoComplete').val(data.data.data.clientNM);
                            $('#content').show();

                            // monitor changes
                            $scope.$watch('data', function () {
                                jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                            });
                            $scope.$watch('data.meetingLocationID', function () {
                                if ($scope.data.meetingLocationID != 4) {
                                    $scope.data.factoryID = null;
                                    $scope.data.factoryUD = null;
                                }
                            });
                            $scope.$watch('data.personInChargeID', function () {
                                if ($scope.data.personInChargeID != 0) {
                                    $scope.data.personInChargeNM = '';
                                }
                            });
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                            $scope.data = null;
                        }
                    );
                }

                $('#content').show();
                runAllForms();

                //
                // ajax auto complete
                //
                $('#clientAutoComplete').autocomplete({
                    source: function (request, response) {
                        jQuery.ajax({
                            url: context.serviceUrl + 'quicksearchclient?query=' + request.term,
                            headers: {
                                'Accept': 'application/json',
                                'Content-Type': 'application/json',
                                'Authorization': 'Bearer ' + context.token
                            },
                            dataType: "json",
                            success: function (data) {
                                response($.map(data, function (value, key) {
                                    return {
                                        id: value.clientID,
                                        label: value.clientNM,
                                        value: value.clientNM,
                                        description: 'Code: ' + value.clientUD,
                                        code: value.clientUD
                                    };
                                }));
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                console.log(jqXHR);
                                console.log(textStatus);
                                console.log(errorThrown);
                            }
                        });
                    },
                    minLength: 3,
                    select: function (event, ui) {
                        scope.$apply(function () {
                            $scope.data.clientID = ui.item.id;
                            $scope.data.clientNM = ui.item.label;
                            $scope.data.clientUD = ui.item.code;
                        });
                    },
                    change: function (event, ui) {
                        if (!ui.item) {
                            scope.$apply(function () {
                                $scope.data.clientID = null;
                                $scope.data.clientNM = '';
                                $scope.data.clientUD = '';
                            });
                            $('#clientAutoComplete').val('');
                        }
                    },
                })
                .data("ui-autocomplete")._renderItem = function (ul, item) {
                    return jQuery("<li>")
                        .data('item.autocomplete', item)
                        .append('<a href="javascript:void(0)"><strong style="text-decoration: underline; text-transform: uppercase;">' + item.label + '</strong><br>' + item.description + '</a>')
                        .appendTo(ul);
                };
            },

            addFile: function () {
                masterUploader.multiSelect = false;
                masterUploader.imageOnly = false;
                masterUploader.callback = function () {
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            $scope.data.appointmentAttachedFileDTOs.push({
                                appointmentAttachedFileID: dataService.getIncrementId(),
                                fileUD: '',
                                appointmentAttachedFileTypeID: 1,
                                description: '',
                                friendlyName: img.friendlyName,
                                fileLocation: img.fileURL,
                                newFile: img.filename,
                                hasChanged: true
                            });
                        }, null);

                    });
                };
                masterUploader.open();
            },
            addManager: function () {
                angular.forEach($scope.viewmodel.supportData.employeeDepartmentDTOs, function (value, key) {
                    value.isSelected = false;
                }, null);
                jQuery('#showemployee').modal('show');
            },
            updateManger: function (item) {
                angular.forEach($scope.viewmodel.supportData.employeeDepartmentDTOs, function (value, key) {
                    if (value.isSelected) {
                        $scope.data.agendaMngUsers.push({
                            managerUserID: $scope.method.getNewID(),
                            employeeID: value.employeeID,
                            employeeNM: value.employeeNM,
                        });
                    }
                }, null);
                jQuery('#showemployee').modal('hide');
            },
            removeManager(item) {
                $scope.data.agendaMngUsers.splice($scope.data.agendaMngUsers.indexOf(item), 1);
            },
            removeFile: function (item) {
                $scope.data.appointmentAttachedFileDTOs.splice($scope.data.appointmentAttachedFileDTOs.indexOf(item), 1);
            },
            update: function () {
                if ($scope.editForm.$valid) {
                    dataService.update(
                        $scope.data.appointmentID,
                        $scope.data,
                        function (data) {
                            jsHelper.processMessage(data.message);
                            if (data.message.type == 0) {
                                $('.alert .close').click();
                                window.location = '#/';
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                }
                else {
                    jsHelper.showMessage('warning', 'Invalid input data, please check');
                    console.log($scope.editForm.$error);
                }
            },
            delete: function () {
                dataService.delete(
                    $scope.data.appointmentID,
                    null,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $('.alert .close').click();
                            window.location = '#/';
                        }
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
            getProfileViewUrl: function () {
                return context.profileViewUrl;
            },
            getClientViewUrl: function () {
                return context.clientViewUrl;
            },
            getNewID: function () {
                $scope.newID--;
                return $scope.newID;
            },

        }

        //
        // bootstrap
        //
        $scope.event.init();
    }]);
})();
