(function () {
    'use strict';

    angular.module('tilsoftApp').controller('eventViewController', ['$scope', '$routeParams', 'dataService', '$http', function ($scope, $routeParams, dataService, $http) {
        $scope.viewmodel = {};
        $scope.data = null;
        $scope.employeeDepartmentDTOs = null;
        $scope.newID = -1;


        $scope.event = {
            init: function () {
                $('#content').hide();
                $('#ribbon .breadcrumb').html('').append('<li><a href="#/">SCM Agenda</a></li><li>Event</li>');
                $('#page-title').html('').append('<a href="#/">SCM Agenda</a> <span>> Event</span>');

                $scope.viewmodel = dataService.getLocalData(dataService.dataKey);

                // fix get support appointment attached file type.
                if ($scope.viewmodel.supportData.loadedStatus.indexOf('editEvent') < 0) {
                    // load data from server
                    dataService.getSupportData(
                        'editEvent',
                        function (data) {
                            $scope.viewmodel.supportData.timeRange = data.data.timeRange;
                            $scope.viewmodel.supportData.users = data.data.users;
                            $scope.viewmodel.supportData.scmAppointmentAttachedFileTypes = data.data.scmAppointmentAttachedFileTypeDTOs;
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

                var id = $routeParams.id;
                if (id > 0) {
                    dataService.load(
                        id,
                        null,
                        function (data) {
                            $scope.data = data.data.data;
                            $scope.employeeDepartmentDTOs = [];
                            angular.forEach(data.data.employeeDepartmentDTOs, function (value, key) {
                                $scope.employeeDepartmentDTOs.push({
                                    employeeID: value.employeeID,
                                    employeeNM: value.employeeNM,
                                    isSelected: false,
                                });
                            }, null);

                            $('#content').show();
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                            $scope.data = null;
                        }
                    );
                }
                else {
                    window.location = '#/';
                }
            },
            addFile: function () {
                fileUploader2.callback = function () {
                    scope.$apply(function () {
                        $scope.data.scmAppointmentAttachedFileDTOs.push({
                            scmAppointmentAttachedFileID: dataService.getIncrementId(),
                            fileUD: '',
                            scmAppointmentAttachedFileTypeID: 1,
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
                angular.forEach($scope.employeeDepartmentDTOs, function (value, key) {
                    value.isSelected = false;
                }, null);
                jQuery('#showemployee').modal('show');
            },
            updateManger: function (item) {
                angular.forEach($scope.employeeDepartmentDTOs, function (value, key) {
                    if (value.isSelected) {
                        $scope.data.scmAppointmentUserDTOs.push({
                            scmAppointmentUserID: $scope.method.getNewID(),
                            employeeID: value.employeeID,
                            employeeNM: value.employeeNM,
                        });
                    }
                }, null);
                jQuery('#showemployee').modal('hide');
            },
            removeManager(item) {
                $scope.data.scmAppointmentUserDTOs.splice($scope.data.scmAppointmentUserDTOs.indexOf(item), 1);
            },
            removeFile: function (item) {
                $scope.data.scmAppointmentAttachedFileDTOs.splice($scope.data.scmAppointmentAttachedFileDTOs.indexOf(item), 1);
            },
            update: function () {
                dataService.update(
                    $scope.data.scmAppointmentID,
                        $scope.data,
                        function (data) {
                            jsHelper.processMessage(data.message);
                            if (data.message.type == 0) {
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

        $scope.frmQCReport = {
            printQCReport: function (item) {
                dataService.printQCReport(item.qcReportID,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type !== 2) {
                            window.location = context.backendReportUrl + data.data;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        }

        //
        // bootstrap
        //
        $scope.event.init();
    }]);
})();
