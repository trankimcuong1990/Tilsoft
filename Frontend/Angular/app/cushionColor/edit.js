//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.seasons = null;
    $scope.cushionTypes = null;

    $scope.newID = 0;
    $scope.totalRows = 0;

    $scope.gridHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
        },
        isTriggered: false
    };

    //
    // event
    //
    $scope.event = {
        init: function () {
        jsonService.load(
            context.id,
            function (data) {
                $scope.data = data.data.data;
                $scope.seasons = data.data.seasons;
                $scope.cushionTypes = data.data.cushionTypes;
                $scope.totalRows = $scope.data.cushionColorTestReports.length;

                $scope.$apply();

                jQuery('#content').show();
                    
                // monitor changes
                $scope.$watch('data', function () {
                    jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                });
            },
            function (error) {
                jsHelper.showMessage('warning', error);

                $scope.data = null;
                $scope.seasons = null;
                $scope.cushionTypes = null;
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
                        debugger;
                        $scope.method.refresh(data.data.cushionColorID);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
            }
            else {
                jsHelper.showMessage('warning', context.errMsg);
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
                            window.location = context.backURL;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
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

        //Test report 1
        changeFile1: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.fileLocation1 = img.fileURL;
                        scope.data.friendlyName1 = img.filename;
                        scope.data.testReportFile_NewFile1 = img.filename;
                        scope.data.testReportFile_HasChange1 = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeFile1: function () {
            $scope.data.friendlyName1 = '';
            $scope.data.fileLocation1 = '';
            $scope.data.testReportFile_NewFile1 = '';
            $scope.data.testReportFile_HasChange1 = true;
        },
        downloadFile1: function () {
            if ($scope.data.fileLocation1) {
                window.open($scope.data.fileLocation1);
            }
        },

        //Test report 2
        changeFile2: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.fileLocation2 = img.fileURL;
                        scope.data.friendlyName2 = img.filename;
                        scope.data.testReportFile_NewFile2 = img.filename;
                        scope.data.testReportFile_HasChange2 = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeFile2: function () {
            $scope.data.friendlyName2 = '';
            $scope.data.fileLocation2 = '';
            $scope.data.testReportFile_NewFile2 = '';
            $scope.data.testReportFile_HasChange2 = true;
        },
        downloadFile2: function () {
            if ($scope.data.fileLocation2) {
                window.open($scope.data.fileLocation2);
            }
        },

        //Test report 3
        changeFile3: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.fileLocation3 = img.fileURL;
                        scope.data.friendlyName3 = img.filename;
                        scope.data.testReportFile_NewFile3 = img.filename;
                        scope.data.testReportFile_HasChange3 = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeFile3: function () {
            $scope.data.friendlyName3 = '';
            $scope.data.fileLocation3 = '';
            $scope.data.testReportFile_NewFile3 = '';
            $scope.data.testReportFile_HasChange3 = true;
        },
        downloadFile3: function () {
            if ($scope.data.fileLocation3) {
                window.open($scope.data.fileLocation3);
            }
        },
        addCushionColorTestReport: function () {
            var cushionColorTestReport = {
                cushionColorTestReportID: $scope.method.getNewID(),
                friendlyName: null,
                fileLocation: null,
                file_NewFile: null,
                file_HasChange: null,
                remark: null
            };

            $scope.data.cushionColorTestReports.push(cushionColorTestReport);
            $scope.totalRows = $scope.data.cushionColorTestReports.length;
        },
        removeCushionColorTestReport: function (item) {
            var index = $scope.data.cushionColorTestReports.indexOf(item);

            $scope.data.cushionColorTestReports.splice(index, 1);
            $scope.totalRows = $scope.data.cushionColorTestReports.length;
        },
        addFileTestReport: function (item) {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    item.friendlyName = fileUploader2.selectedFriendlyName;
                    item.fileLocation = fileUploader2.selectedFileUrl;
                    item.file_NewFile = fileUploader2.selectedFileName;
                    item.file_HasChange = true;
                });
            };
            fileUploader2.open();
        },
        removeFileTestReport: function (item) {
            item.friendlyName = null;
            item.fileLocation = null;
            item.file_NewFile = null;
            item.file_HasChange = true;
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshURL + id;
        },
        getNewID: function () {
            $scope.newID = $scope.newID - 1;
            return $scope.newID;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);