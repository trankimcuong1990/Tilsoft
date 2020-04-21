jQuery('#grdItem').scrollableTable(
    function (currentPage) {
    },
    function (sortedBy, sortedDirection) {
    }
);
jQuery('#grdSearchResult').scrollableTable(
    function (currentPage) {
    },
    function (sortedBy, sortedDirection) {
    }
);

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ngSanitize']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;
    $scope.AvailableReports = null;
    $scope.inspectionTypes = null;
    $scope.packagingTypes = null;
    $scope.currentImage = null;
    $scope.items = null;
    $scope.newid = 0;
    $scope.qcReportDocumentTypes = null; // qc report document type | tran.cuong | 20180228

    $scope.filter = {
        factoryOrderDetailID: context.factoryOrderDetailID,
        id: context.id
    };
    $scope.seasons = null;
    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                context.factoryOrderDetailID,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.availableReports = data.data.availableReports;
                    $scope.inspectionTypes = data.data.inspectionTypes;
                    $scope.packagingTypes = data.data.packagingTypes;
                    $scope.qcReportImageTitles = data.data.qcReportImageTitles;
                    $scope.qcReportDocumentTypes = data.data.qcReportDocumentTypes; // qc report document type | tran.cuong | 20180228

                    //console.log(data);

                    context.factoryOrderDetailID = $scope.data.factoryID;
                    $scope.filter = {
                        factoryOrderDetailID: context.factoryOrderDetailID,
                        id: context.id
                    };

                    jQuery('#content').show();
                    if (context.id === 0) {
                        for (i = 1; i <= 3; i++) {
                            switch (i) {
                                case 1:
                                    name = 'Overal Dimension (L x W x H)';
                                    unit = 'mm';
                                    break;
                                case 2:
                                    name = 'Net Weight';
                                    unit = 'kg';
                                    break;
                                case 3:
                                    name = 'Moisture content';
                                    unit = '%';
                                    break;
                            }

                            $scope.data.qcReportDetails.push({
                                qcReportDetailID: $scope.method.getNewID(),
                                name: name,
                                unit: unit
                            });
                        }
                    }
                    $scope.$apply();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.qcReportID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', context.error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', errMsg);
            }
        },
        delete: function () {
            if (confirm('Are you sure ?')) {
                jsonService.delete(context.id,
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
        //
        // file functions
        //
        //changeFile: function () {
        //    fileUploader2.callback = function () {
        //        $scope.$apply(function () {
        //            $scope.data.friendlyName = fileUploader2.selectedFileName;
        //            $scope.data.fileLocation = fileUploader2.selectedFileUrl;
        //            $scope.data.attachedFile_NewFile = fileUploader2.selectedFileName;
        //            $scope.data.attachedFile_HasChanged = true;
        //        });
        //    };
        //    fileUploader2.open();
        //},
        //removeFile: function () {
        //    $scope.data.friendlyName = '';
        //    $scope.data.fileLocation = '';
        //    $scope.data.attachedFile_NewFile = '';
        //    $scope.data.attachedFile_HasChanged = true;
        //},
        //downloadFile: function () {
        //    if ($scope.data.fileLocation) {
        //        window.open($scope.data.fileLocation);
        //    }
        //},
        print: function () {
            jsonService.getReport(
                context.id,
                function (data) {
                    if (data.message.type == 0) {
                        window.location = context.backendReportUrl + data.data;
                    }
                    else {
                        jsHelper.processMessageEx(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        //
        // IMAGE
        //
        addImage: function () {
            var newImage = {
                qcReportImageID: $scope.method.getNewID(),
                fileUD: null,
                description: null,
                friendlyName: null,
                thumbnailLocation: null,
                fileLocation: null,
                hasChange: false,
                newfile: null
            };
            $scope.event.editImage(newImage);
        },
        editImage: function (item) {
            $scope.currentImage = JSON.parse(JSON.stringify(item));
            jQuery("#imageForm").modal();
        },
        updateImage: function () {
            var index = $scope.method.getImageIndex($scope.currentImage.qcReportImageID)
            if (index >= 0) {
                $scope.data.qcReportImages[index] = $scope.currentImage;
            }
            else {
                $scope.data.qcReportImages.push($scope.currentImage);
            }
            jQuery("#imageForm").modal('hide');

            $scope.method.renderImage();
            //console.log($scope.currentDrawing);
        },

        removeImage: function (item) {
            if (confirm('Are you sure ?')) {
                $scope.data.qcReportImages.splice($scope.data.qcReportImages.indexOf(item), 1);
            }
        },

        addPreviewImage: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        // TO DO LIST
                        scope.currentImage.hasChange = true;
                        scope.currentImage.thumbnailLocation = img.fileURL;
                        scope.currentImage.fileLocation = img.fileURL;
                        scope.currentImage.newFile = img.filename;
                    }, null);
                });
            };
            masterUploader.open();
        },

        //
        // QC Repor Detail
        //
        addDetail: function ($event) {
            $event.preventDefault();
            $scope.data.qcReportDetails.push({
                qcReportDetailID: $scope.method.getNewID(),
            });
        },
        removeDetail: function ($event, index) {
            $event.preventDefault();
            if (confirm('Are you sure ?')) {
                $scope.data.qcReportDetails.splice(index, 1);
            }
        },

        //
        // QC Report Defect
        //
        addDefect: function ($event) {
            $event.preventDefault();
            $scope.data.qcReportDefects.push({
                qcReportDefectID: $scope.method.getNewID(),
            });
        },
        removeDefect: function ($event, index) {
            $event.preventDefault();
            if (confirm('Are you sure ?')) {
                $scope.data.qcReportDefects.splice(index, 1);
            }
        },
        generateExcel: function (id) {
            var qcReportId = id;
            jsonService.getExcelData(
                qcReportId,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        //
        // IMAGE TITLE
        //
        addTitle: function () {
            var newImageTitle = {
                qcReportImageTitleID: $scope.method.getNewID(),
                qcReportImageTitleNM: null
            };
            $scope.newImageTitle = JSON.parse(JSON.stringify(newImageTitle));
            jQuery("#imageTitleForm").modal();
        },
        updateTitle: function () {
            if ($scope.newImageTitle.qcReportImageTitleNM) {
                $scope.qcReportImageTitles.push($scope.newImageTitle);
                //console.log($scope.qcReportImageTitles[$scope.qcReportImageTitles.length - 1]);
                $scope.currentImage.description = $scope.qcReportImageTitles[$scope.qcReportImageTitles.length - 1].qcReportImageTitleNM;
                //$scope.$apply();
                jQuery("#imageTitleForm").modal('hide');
            } else {
                alert('Remark could not be null!');
                return;
            }
            //$scope.$apply();
        },

        // event add document files | tran.cuong | 20180227 | start
        addDocumentFiles: function () {
            userFileMng.isMultiSelectActivated = true;
            userFileMng.autoResizeImage = false;

            userFileMng.callback = function () {
                scope.$apply(function () {
                    var d = new Date();
                    var n = d.getMilliseconds();

                    angular.forEach(userFileMng.selectedFiles, function (value, key) {
                        if ($scope.data.qcReportDocuments === null) {
                            $scope.data.qcReportDocuments = [];
                        }

                        $scope.data.qcReportDocuments.push({
                            qcReportDocumentID: scope.method.getNewID(),
                            qcReportDocumentUD: null,
                            qcReportDocumentTypeID: null,
                            remark: null,
                            
                            friendlyName: value.fileName,
                            fileLocation: (value.filePath.indexOf('?') < 0) ? value.filePath + '?' + n : value.filePath + n,
                            qcReportDocument_HasChange: true,
                            qcReportDocument_NewFile: value.fileName,
                        });
                        //scope.data.plcImages.push({
                        //    plcImageID: scope.method.getNewID(),
                        //    imageTypeID: type,
                        //    imageFile: '',
                        //    thumbnailLocation: (value.thumbnailPath.indexOf('?') < 0) ? value.thumbnailPath + '?' + n : value.thumbnailPath + n,
                        //    fileLocation: (value.filePath.indexOf('?') < 0) ? value.filePath + '?' + n : value.filePath + n,
                        //    imageFile_HasChange: true,
                        //    imageFile_NewFile: value.fileName
                        //});
                    }, null);
                });
            };

            userFileMng.open();
        },
        removeDocumentFiles: function (item) {
            var index = $scope.data.qcReportDocuments.indexOf(item);
            $scope.data.qcReportDocuments.splice(index, 1);
        }
        // event add document files | tran.cuong | 20180227 | end
    };

    //
    // method
    //
    $scope.method = {
        getNewID: function () {
            $scope.newid--;
            return $scope.newid;
        },
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        },
        //Image
        getImageIndex: function (id) {
            var isExist = false;
            var index = 0;
            for (var index = 0; index < $scope.data.qcReportImages.length; index++) {
                if ($scope.data.qcReportImages[index].qcReportImageID == id) {
                    isExist = true;
                    break;
                }
            }

            if (!isExist) {
                return -1;
            }
            else {
                return index;
            }
        },
        renderImage: function () {
            $scope.images = [];
            angular.forEach($scope.data.qcReportImages, function (value, key) {
                if ($scope.images.indexOf(value.draw) < 0) {
                    $scope.images.push(value.draw);
                }
            }, null);
        },
    };

    // on data binding change
    $scope.$watch('selectAll', function () {
        angular.forEach($scope.items, function (value, key) {
            value.isSelected = $scope.selectAll;
        }, null);
    });

    //
    // init
    //    
    $scope.event.init();
}]);