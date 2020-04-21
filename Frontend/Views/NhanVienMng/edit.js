(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
    angular.module('tilsoftApp').controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;

        //
        // scope data
        //
        $scope.data = null;
        $scope.phongBans = [];

        //
        // events
        //
        $scope.event = {
            init: function () {
                dataService.load(
                    context.id,
                    {},
                    function (data) {
                        if (data.message.type === 0) {
                            // map data when load success
                            $scope.data = data.data.data;
                            $scope.phongBans = data.data.phongBanDTOs;

                            $('#content').show();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                        $scope.data = null;
                        $scope.phongBans = [];
                    }
                );
            },
            update: function () {
                if ($scope.frmEdit.$valid) { // check if form is valid
                    dataService.update(
                        context.id,
                        $scope.data,
                        function (data) {
                            //console.log(data);
                            //return;

                            jsHelper.processMessage(data.message);
                            if (data.message.type === 0) {
                                // reload page with new id
                                $scope.method.refresh(data.data.nhanVienID);
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error.message.message);
                        }
                    );
                }
                else {
                    jsHelper.showMessage('warning', context.errMsg);
                }
            },
            delete: function () {
                dataService.delete(
                    context.id,
                    null,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            // return to index page after delete
                            window.location = context.backUrl;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            },
            addNPT: function () {
                $scope.data.nguoiPhuThuocDTOs.push({
                    nguoiPhuThuocID: dataService.getIncrementId(),
                    nguoiPhuThuocNM: '',
                    ngaySinh: '',
                    quanHe: ''
                });

                $('.datepicker').each(function () {

                    var $this = $(this),
                        dataDateFormat = $this.attr('data-dateformat') || 'dd.mm.yy';

                    $this.datepicker({
                        dateFormat: dataDateFormat,
                        prevText: '<i class="fa fa-chevron-left"></i>',
                        nextText: '<i class="fa fa-chevron-right"></i>',
                    });

                    //clear memory reference
                    $this = null;
                });
            },
            delNPT: function (item) {
                $scope.data.nguoiPhuThuocDTOs.splice($scope.data.nguoiPhuThuocDTOs.indexOf(item), 1);
            },

            uploadImage: function () {
                fileUploader2.callback = function () {
                    scope.$apply(function () {
                        scope.data.thumbnailLocation = fileUploader2.selectedFileUrl;
                        scope.data.fileLocation = fileUploader2.selectedFileUrl;
                        scope.data.newPhoto = fileUploader2.selectedFileName;
                        scope.data.photoHasChanged = true;
                    });
                };
                fileUploader2.open();
            }
            //approve: function () {
            //    if (confirm('Approve current data?')) {
            //        dataService.approve(
            //            context.id,
            //            $scope.data,
            //            function (data) {
            //                jsHelper.processMessage(data.message);
            //                if (data.message.type === 0) {
            //                    // reload page with new id
            //                    $scope.method.refresh(data.data);
            //                }
            //            },
            //            function (error) {
            //                jsHelper.showMessage('warning', error.message.message);
            //            }
            //        );
            //    }
            //},
            //reset: function () {
            //    if (confirm('Reset current data?')) {
            //        dataService.reset(
            //            context.id,
            //            $scope.data,
            //            function (data) {
            //                jsHelper.processMessage(data.message);
            //                if (data.message.type === 0) {
            //                    // reload page with new id
            //                    $scope.method.refresh(data.data);
            //                }
            //            },
            //            function (error) {
            //                jsHelper.showMessage('warning', error.message.message);
            //            }
            //        );
            //    }
            //},
            //print: function () {
            //    dataService.print(
            //        context.id,
            //        function (data) {
            //            jsHelper.processMessage(data.message);
            //            if (data.message.type === 0) {
            //                // redirect to excel temp file after print out or perform other action
            //                window.location = context.backendReportUrl + data.data;
            //            }
            //        },
            //        function (error) {
            //            jsHelper.showMessage('warning', error.message.message);
            //        }
            //    );
            //},

            // image/file processing
            //addImage: function (type) {
            //    userFileMng.isMultiSelectActivated = true;
            //    userFileMng.autoResizeImage = false;
            //    userFileMng.callback = function () {
            //        scope.$apply(function () {
            //            var d = new Date();
            //            var n = d.getMilliseconds();

            //            angular.forEach(userFileMng.selectedFiles, function (value, key) {
            //                scope.data.plcImages.push({
            //                    plcImageID: scope.method.getNewID(),
            //                    imageTypeID: type,
            //                    imageFile: '',
            //                    thumbnailLocation: (value.thumbnailPath.indexOf('?') < 0) ? value.thumbnailPath + '?' + n : value.thumbnailPath + n,
            //                    fileLocation: (value.filePath.indexOf('?') < 0) ? value.filePath + '?' + n : value.filePath + n,
            //                    imageFile_HasChange: true,
            //                    imageFile_NewFile: value.fileName
            //                });
            //            }, null);
            //        });
            //    };
            //    userFileMng.open();
            //},
            //removeImage: function (item) {
            //    $scope.data.plcImages.splice($scope.data.plcImages.indexOf(item), 1);
            //}
        }

        //
        // method
        //
        $scope.method = {
            refresh: function (id) {
                jsHelper.loadingSwitch(true);
                window.location = context.refreshUrl + id;
            }
        };


        //
        // angular init
        //
        $timeout(function () {
            $scope.event.init();
        }, 0);
    }]);

})();