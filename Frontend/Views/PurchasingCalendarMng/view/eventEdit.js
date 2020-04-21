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
            purchasingCalendarAppointmentID: 0,
            purchasingCalendarAppointmentUD: '',
            factoryRawMaterialID: null,
            userID: context.userID,
            personInChargeID: 0,
            personInChargeNM: '',
            subject: '',
            meetingLocationID: null,          
            startDate: '',
            startTime: '',
            endDate: '',
            endTime: '',
            remindDate: '',
            remindTime: '',
            description: '',
            meetingMinute: '',
            factoryRawMaterialUD: '',
            factoryRawMaterialOfficialNM: '',
            ownerNM: context.userName,
            meetingLocationNM: '',         
            managerUserID: null,
            employeeID: null,
            employeeNM: '',
            purchasingCalendarAppointmentAttachedFileDTOs: [],
            purchasingCalendarUserDTOs: []
        };

        //
        // event
        //
        $scope.event = {
            init: function () {
                $('#content').hide();
                $('#ribbon .breadcrumb').html('').append('<li><a href="#/">Purchasing Calendar</a></li><li>Event</li>');
                $('#page-title').html('').append('<a href="#/">Purchasing Calendar</a> <span>> Event</span>');
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
                            $scope.viewmodel.supportData.meetingLocationDTOs = [];
                            angular.forEach($scope.viewmodel.supportData.locations, function (item) {
                                if (item.meetingLocationID < 5) {
                                    $scope.viewmodel.supportData.meetingLocationDTOs.push({ meetingLocationID: item.meetingLocationID, meetingLocationNM: item.meetingLocationNM });
                                }
                            });

                            $scope.viewmodel.supportData.loadedStatus.push('editEvent'); // mark as support data loaded for calendar form
                        },
                        function (error) {
                        }
                    );
                }
                else {
                    $scope.viewmodel.supportData.meetingLocationDTOs = [];
                    angular.forEach($scope.viewmodel.supportData.locations, function (item) {
                        $scope.viewmodel.supportData.meetingLocationDTOs.push({ meetingLocationID: item.meetingLocationID, meetingLocationNM: item.meetingLocationNM });
                    });
                }
                if ($scope.viewmodel.supportData.loadedStatus.indexOf('calendar') < 0) {
                    // load location & factories
                    dataService.getSupportData(
                        'calendar',
                        function (data) {
                            $scope.viewmodel.supportData.locations = data.data.meetingLocationDTOs;
                            angular.forEach($scope.viewmodel.supportData.locations, function (item) {
                                item.isSelected = true;
                            });
                         
                            $scope.viewmodel.supportData.employeeDepartmentDTOs = data.data.employeeDepartmentDTOs;
                            $scope.viewmodel.supportData.meetingLocationDTOs = [];
                            angular.forEach($scope.viewmodel.supportData.locations, function (item) {
                                if (item.meetingLocationID < 5) {
                                    $scope.viewmodel.supportData.meetingLocationDTOs.push({ meetingLocationID: item.meetingLocationID, meetingLocationNM: item.meetingLocationNM });
                                }
                            });

                            $scope.viewmodel.supportData.loadedStatus.push('calendar'); // mark as support data loaded for calendar form
                        },
                        function (error) {
                        }
                    );
                }
                else {
                    $scope.viewmodel.supportData.meetingLocationDTOs = [];
                    angular.forEach($scope.viewmodel.supportData.locations, function (item) {
                        $scope.viewmodel.supportData.meetingLocationDTOs.push({ meetingLocationID: item.meetingLocationID, meetingLocationNM: item.meetingLocationNM });
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

                            $('#clientAutoComplete').val(data.data.data.factoryRawMaterialOfficialNM);
                            $('#content').show();

                            // monitor changes
                            $scope.$watch('data', function () {
                                jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                            });
                            $scope.$watch('data.meetingLocationID', function () {
                               
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
                            url: context.serviceUrl + 'quicksearchfactoryrawmaterial?query=' + request.term,
                            headers: {
                                'Accept': 'application/json',
                                'Content-Type': 'application/json',
                                'Authorization': 'Bearer ' + context.token
                            },
                            dataType: "json",
                            success: function (data) {
                                response($.map(data, function (value, key) {
                                    return {
                                        id: value.factoryRawMaterialID,
                                        label: value.factoryRawMaterialOfficialNM,
                                        value: value.factoryRawMaterialOfficialNM,
                                        description: 'Code: ' + value.factoryRawMaterialUD,
                                        code: value.factoryRawMaterialUD
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
                            $scope.data.factoryRawMaterialID = ui.item.id;
                            $scope.data.factoryRawMaterialOfficialNM = ui.item.label;
                            $scope.data.factoryRawMaterialUD = ui.item.code;
                        });
                    },
                    change: function (event, ui) {
                        if (!ui.item) {
                            scope.$apply(function () {
                                $scope.data.factoryRawMaterialID = null;
                                $scope.data.factoryRawMaterialOfficialNM = '';
                                $scope.data.factoryRawMaterialUD = '';
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
                fileUploader2.callback = function () {
                    scope.$apply(function () {
                        $scope.data.purchasingCalendarAppointmentAttachedFileDTOs.push({
                            purchasingCalendarAppointmentAttachedFileID: dataService.getIncrementId(),
                            fileUD: '',
                            purchasingCalendarAppointmentAttachedFileTypeID: 1,
                            description: '',
                            friendlyName: fileUploader2.selectedFriendlyName,
                            fileLocation: fileUploader2.selectedFileUrl,
                            newFile: fileUploader2.selectedFileName,
                            hasChanged: true
                        });
                    });
                };
                fileUploader2.open();
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
                        $scope.data.purchasingCalendarUserDTOs.push({
                            purchasingCalendarUserID: $scope.method.getNewID(),
                            employeeID: value.employeeID,
                            employeeNM: value.employeeNM,
                        });
                    }
                }, null);
                jQuery('#showemployee').modal('hide');
            },
            removeManager(item) {
                $scope.data.purchasingCalendarUserDTOs.splice($scope.data.purchasingCalendarUserDTOs.indexOf(item), 1);
            },
            removeFile: function (item) {
                $scope.data.purchasingCalendarAppointmentAttachedFileDTOs.splice($scope.data.purchasingCalendarAppointmentAttachedFileDTOs.indexOf(item), 1);
            },
            update: function () {
                if ($scope.editForm.$valid) {
                    dataService.update(
                        $scope.data.purchasingCalendarAppointmentID,
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
                    $scope.data.purchasingCalendarAppointmentID,
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
