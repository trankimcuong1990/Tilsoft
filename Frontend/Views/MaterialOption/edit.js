var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.materials = null;
    $scope.materialTypes = null;
    $scope.materialColors = null;
    $scope.testingFile = [];
    $scope.seasons = null;
    $scope.startListener = false;

    $scope.newTestReportID = 0;
    $scope.isEditing = false;

    $scope.dataTestReport = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;

                    $scope.materials = data.data.materials;
                    $scope.materialTypes = data.data.materialTypes;
                    $scope.materialColors = data.data.materialColors;
                    $scope.seasons = data.data.seasons;

                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                    $scope.startListener = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.materials = null;
                    $scope.materialTypes = null;
                    $scope.materialColors = null;
                    $scope.seasons = null;
                    $scope.$apply();
                }
            );
        },
        update: function ($event) {
            $event.preventDefault();

            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.materialOptionID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', context.errorMsg);
            }
        },
        delete: function ($event) {
            $event.preventDefault();

            if (confirm('Are you sure ?')) {
                jsonService.delete(
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
        },
        onOptionChange: function () {
            if (!$scope.startListener) {
                return;
            }

            var optionCode = '';
            var optionName = '';

            // material
            if ($scope.data.materialID > 0) {
                for (j = 0; j < $scope.materials.length; j++) {
                    if ($scope.materials[j].materialID == $scope.data.materialID) {
                        optionCode += $scope.materials[j].materialUD;
                        optionName += $scope.materials[j].materialNM;
                        break;
                    }
                }
            }

            // type
            if ($scope.data.materialTypeID > 0) {
                for (j = 0; j < $scope.materialTypes.length; j++) {
                    if ($scope.materialTypes[j].materialTypeID == $scope.data.materialTypeID) {
                        optionCode += $scope.materialTypes[j].materialTypeUD;
                        optionName += ' ' + $scope.materialTypes[j].materialTypeNM;

                        break;
                    }
                }
            }

            // color
            if ($scope.data.materialColorID > 0) {
                for (j = 0; j < $scope.materialColors.length; j++) {
                    if ($scope.materialColors[j].materialColorID == $scope.data.materialColorID) {
                        optionCode += $scope.materialColors[j].materialColorUD;
                        optionName += ' ' + $scope.materialColors[j].materialColorNM;
                        break;
                    }
                }
            }

            $scope.data.materialOptionUD = optionCode;
            $scope.data.materialOptionNM = optionName;
        },
        changeImage: function ($event) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.imageFile_DisplayUrl = img.fileURL;
                        scope.data.imageFile_NewFile = img.filename;
                        scope.data.imageFile_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeImage: function ($event) {
            $scope.data.imageFile_DisplayUrl = '';
            $scope.data.imageFile_NewFile = '';
            $scope.data.imageFile_HasChange = true;
        },
        addTestReport: function () {
            $scope.isEditing = true;

            $scope.dataTestReport = {
                materialOptionTestReportID: $scope.method.getNewTestReportID(),
                remark: null,
            };
        },
        cancelTestReport: function () {
            $scope.isEditing = false;
        },
        downloadTestReport: function () {
            if ($scope.dataTestReport.fileLocation) {
                window.open($scope.dataTestReport.fileLocation);
            }
        },
        changeTestReport: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.dataTestReport.fileLocation = img.fileURL;
                        scope.dataTestReport.friendlyName = img.filename;
                        scope.dataTestReport.testReportNewFile = img.filename;
                        scope.dataTestReport.testReportHasChange = true;
                         //scope.dataTestReport.fileUD = scope.dataTestReport.friendlyName;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeTestReport: function () {
            $scope.dataTestReport.friendlyName = '';
            $scope.dataTestReport.fileLocation = '';
            $scope.dataTestReport.testReportNewFile = '';
            $scope.dataTestReport.testReportHasChange = false;
        },
        saveTestReport: function () {
            if ($scope.data.materialOptionTestReports === null) {
                $scope.data.materialOptionTestReports = [];
            }

            if ($scope.dataTestReport.materialOptionTestReportID < 0) {
                if ($scope.data.materialOptionTestReports.length > 0) {
                    var lastIdx = $scope.data.materialOptionTestReports.length - 1;

                    if ($scope.data.materialOptionTestReports[lastIdx].materialOptionTestReportID !== $scope.dataTestReport.materialOptionTestReportID) {
                        $scope.data.materialOptionTestReports.push($scope.dataTestReport);
                    }
                } else {
                    $scope.data.materialOptionTestReports.push($scope.dataTestReport);
                }
            } else {
                for (var i = 0; i < $scope.data.materialOptionTestReports.length ; i++) {
                    var ele = $scope.data.materialOptionTestReports[i];
                    ele = $scope.dataTestReport;
                }
            }

            $scope.isEditing = false;
        },
        editTestReport: function (item) {
            $scope.dataTestReport = item;
            $scope.isEditing = true;
        },
        deleteTestReport: function (item) {
            var index = $scope.data.materialOptionTestReports.indexOf(item);
            $scope.data.materialOptionTestReports.splice(index, 1);
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
        getNewTestReportID: function () {
            $scope.newTestReportID--;
            return $scope.newTestReportID;
        },
    };

    //
    // init
    //
    $scope.event.init();
}]);