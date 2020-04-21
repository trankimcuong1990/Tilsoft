(function () {
    'use strict';

    angular.module('tilsoftApp').controller('eventVisitController', ['$scope', '$routeParams', 'dataService', function ($scope, $routeParams, dataService) {
        //
        // property
        //
        $scope.viewmodel = {};
        $scope.data = {
            scmAppointmentID: 0,
            scmAppointmentUD: '',
            clientID: 70560, // eurofar
            userID: null,
            personInChargeID: 0,
            personInChargeNM: '',
            subject: 'TRIP TO VN',
            meetingLocationID: 5,
            factoryID: null,
            startDate: '',
            startTime: '',
            endDate: '',
            endTime: '',
            remindDate: '',
            remindTime: '',
            flightDetail: '',
            description: '',
            meetingMinute: '',
            clientUD: '',
            clientNM: '',
            ownerNM: context.userName,
            meetingLocationNM: '',
            factoryUD: '',

            scmAppointmentAttachedFileDTOs: []
        };

        //
        // event
        //
        $scope.event = {
            init: function () {
                $('#content').hide();
                $('#ribbon .breadcrumb').html('').append('<li><a href="#/">SCM Agenda</a></li><li>Event</li>');
                $('#page-title').html('').append('<a href="#/">SCM Agenda</a> <span>> Event</span>');
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
                            $scope.viewmodel.supportData.scmAppointmentAttachedFileTypes = data.data.scmAppointmentAttachedFileTypes;
                            $scope.viewmodel.supportData.sales = data.data.sales;
                            $scope.viewmodel.supportData.loadedStatus.push('editEvent'); // mark as support data loaded for calendar form
                        },
                        function (error) {
                        }
                    );
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
                            $scope.viewmodel.supportData.loadedStatus.push('calendar'); // mark as support data loaded for calendar form
                        },
                        function (error) {
                        }
                    );
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
                            debugger;
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
                    $("html, body").animate({ scrollTop: 0 }, 500);
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
            },
            addFile: function () {
                masterUploader.multiSelect = false;
                masterUploader.imageOnly = false;
                masterUploader.callback = function () {
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            $scope.data.scmAppointmentAttachedFileDTOs.push({
                                scmAppointmentAttachedFileID: dataService.getIncrementId(),
                                fileUD: '',
                                scmAppointmentAttachedFileTypeID: 1,
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
            removeFile: function (item) {
                $scope.data.scmAppointmentAttachedFileDTOs.splice($scope.data.scmAppointmentAttachedFileDTOs.indexOf(item), 1);
            },
        }

        //
        // method
        //
        $scope.method = {
            getProfileViewUrl: function () {
                return context.profileViewUrl;
            }
        }

        //
        // bootstrap
        //
        $scope.event.init();
    }]);
})();