jQuery('#main-form').keypress(function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});
var grdPieceSearchResult = jQuery('#grdPieceSearchResult').scrollableTable2(
    'quicksearchPiece.filters.pageIndex',
    'quicksearchPiece.totalPage',
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quicksearchPiece.filters.pageIndex = currentPage;
            scope.quicksearchPiece.method.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quicksearchPiece.filters.sortedDirection = sortedDirection;
            scope.quicksearchPiece.filters.pageIndex = 1;
            scope.quicksearchPiece.filters.sortedBy = sortedBy;
            scope.quicksearchPiece.method.search();
        });
    }
);

var grdSubMaterialSearchResult = jQuery('#grdSubMaterialSearchResult').scrollableTable2(
    'quicksearchSubMaterial.filters.pageIndex',
    'quicksearchSubMaterial.totalPage',
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quicksearchSubMaterial.filters.pageIndex = currentPage;
            scope.quicksearchSubMaterial.method.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quicksearchSubMaterial.filters.sortedDirection = sortedDirection;
            scope.quicksearchSubMaterial.filters.pageIndex = 1;
            scope.quicksearchSubMaterial.filters.sortedBy = sortedBy;
            scope.quicksearchSubMaterial.method.search();
        });
    }
);

jQuery("#description").keydown(function (e) {
    if (e.keyCode == 13) {
        jQuery("#description").val(jQuery("#description").val() + "\n");
    }
});

jQuery("#comments").keydown(function (e) {
    if (e.keyCode == 13) {
        jQuery("#comments").val(jQuery("#comments").val() + "\n");
    }
});

var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'customFilters', 'ui'])
tilsoftApp.filter("trustUrl", ['$sce', function ($sce) {
    return function (recordingUrl) {
        return $sce.trustAsResourceUrl(recordingUrl);
    };
}]);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
        $scope.data = null;
        $scope.productTypes = null;
        $scope.productGroups = null;
        $scope.packagingMethods = null;
        $scope.manufacturerCountries = null;
        $scope.modelStatuses = null;
        $scope.seasons = null;
        $scope.productCategories = null;
        $scope.factories = null;
        $scope.imageGalleries = null;
        $scope.imageGalleryVersions = null;
        $scope.galleryItemTypes = null;
        $scope.currentGallery = null;
        $scope.newid = 0;
        $scope.modelID = null;
        $scope.currentReport = null;
        $scope.testReports = [];
        $scope.productsViews = null;
        $scope.sampleProductViews = null;

        $scope.canCreate = context.canCreate;
        $scope.isPriceEnabled = false;
        $scope.canApprove = false;

        $scope.loadAbilityTypes = [
            { 'loadAbilityTypeID': 1, 'loadAbilityTypeNM': 'ACTUAL' },
            { 'loadAbilityTypeID': 2, 'loadAbilityTypeNM': 'INDICATED' }
        ];

        $scope.loadingState = {
            sample: false,
            product: false
        };

        $scope.event = {
            init: function () {
                jsonService.serviceUrl = context.serviceUrl;
                jsonService.token = context.token;

                jsonService.getInitDataCreateModel(
                    function (data) {
                        $scope.productTypes = data.data.productTypes;
                        $scope.productGroups = data.data.productGroups;
                        $scope.packagingMethods = data.data.packagingMethods;
                        $scope.seasons = data.data.seasons;
                        $scope.manufacturerCountries = data.data.manufacturerCountries;
                        $scope.modelStatuses = data.data.modelStatuses;
                        $scope.productCategories = data.data.productCategories;
                        $scope.factories = data.data.factories;
                        $scope.galleryItemTypes = data.data.galleryItemTypes;
                        $scope.productElements = data.data.productElements;
                        $scope.productElementOptions = data.data.productElementOptions;
                        $scope.materialTypes = data.data.materialTypes;

                        $scope.supportTrackingStatus = data.data.supportQCTrackingStatus;
                        $scope.supportTrackingType = data.data.supportQCTrackingType;

                        $scope.event.load();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            },
            load: function () {
                jsonService.getDataCreateModel(
                    context.id,
                    context.sampleProductID,
                    function (data) {
                        $scope.data = data.data.data;
                        $scope.imageGalleries = data.data.data.imageGalleries;
                        $scope.imageGalleryVersions = data.data.data.imageGalleryVersions;
                        $scope.data.testReports = data.data.data.testReports;
                        $scope.data.tdfiles = data.data.data.tdfiles;
                        $scope.modelPriceConfigurationDefault = data.data.modelPriceConfigurationDefault;
                        $scope.canApprove = data.data.canApprove;
                        $scope.isPriceEnabled = data.data.isPriceEnabled;

                        $scope.$apply();

                        jQuery('#content').show();

                        $scope.$watch('data', function () {
                            jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                        });

                        setTimeout(function () { pageSetUp(); }, 500);
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
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
                                $scope.method.refresh(data.data.modelID);
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                }
                else {
                    jsHelper.showMessage('warning', 'Invalid input data, please check');
                }
            },
            delete: function ($event) {
                $event.preventDefault();

                if (confirm('Put item to archive ?')) {
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
            addPart: function () {
                $scope.data.spareparts.push({
                    modelSparepartID: $scope.method.getNewID(),
                    partUD: '--',
                    description: ''
                });
            },
            removePiece: function ($event, id) {
                if ($event !== null) {
                    $event.preventDefault();
                }

                isExist = false;
                for (j = 0; j < $scope.data.pieces.length; j++) {
                    if ($scope.data.pieces[j].modelPieceID == id) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    $scope.data.pieces.splice(j, 1);
                }
            },
            removeSubMaterial: function ($event, id) {
                if ($event !== null) {
                    $event.preventDefault();
                }

                isExist = false;
                for (j = 0; j < $scope.data.subMaterialOptions.length; j++) {
                    if ($scope.data.subMaterialOptions[j].modelSubMaterialOptionID == id) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    $scope.data.subMaterialOptions.splice(j, 1);
                }
            },
            removePart: function ($event, id) {
                if ($event !== null) {
                    $event.preventDefault();
                }

                isExist = false;
                for (j = 0; j < $scope.data.spareparts.length; j++) {
                    if ($scope.data.spareparts[j].modelSparepartID == id) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    $scope.data.spareparts.splice(j, 1);
                }
            },
            removePackaging: function ($event, id) {
                if ($event !== null) {
                    $event.preventDefault();
                }

                isExist = false;
                for (j = 0; j < $scope.data.packagingMethodOptions.length; j++) {
                    if ($scope.data.packagingMethodOptions[j].modelPackagingMethodOptionID == id) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    $scope.data.packagingMethodOptions.splice(j, 1);
                }
            },
            addPackaging: function ($event) {
                if ($event !== null) {
                    $event.preventDefault();
                }

                $scope.data.packagingMethodOptions.push({
                    modelPackagingMethodOptionID: $scope.method.getNewID(),
                    packagingMethodID: '',
                    isDefault: 0,
                    isConfirmed: 0
                });
            },
            confirmFrame: function ($event, id) {

            },
            confirmMaterial: function ($event, id) {

            },
            confirmPackaging: function ($event, id) {

            },
            uploadFreescan: function () {
                masterUploader.multiSelect = true;
                masterUploader.imageOnly = true;
                masterUploader.callback = function () {
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            // TO DO LIST
                            scope.data.imageGalleries.push({
                                imageGalleryID: scope.method.getNewID(),
                                fileUD: '',
                                galleryItemTypeID: 1,
                                isDefault: false,
                                fileHasChange: true,
                                newFile: img.filename,
                                seasonTypeID: 1,
                                thumbnailLocation: img.fileURL
                            });
                        }, null);
                    });
                };
                masterUploader.open();
            },


            uploadGarden: function () {
                //return;
                masterUploader.multiSelect = true;
                masterUploader.imageOnly = true;
                masterUploader.callback = function () {
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            // TO DO LIST
                            scope.data.imageGalleries.push({
                                imageGalleryID: scope.method.getNewID(),
                                fileUD: '',
                                galleryItemTypeID: 2,
                                isDefault: false,
                                fileHasChange: true,
                                newFile: img.filename,
                                seasonTypeID: 1,
                                thumbnailLocation: img.fileURL
                            });
                        }, null);
                    });
                };
                masterUploader.open();
            },
            uploadVideo: function () {
                //return;
                masterUploader.multiSelect = true;
                masterUploader.imageOnly = true;
                masterUploader.callback = function () {
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            // TO DO LIST
                            scope.data.imageGalleries.push({
                                imageGalleryID: scope.method.getNewID(),
                                fileUD: '',
                                galleryItemTypeID: 3,
                                isDefault: false,
                                fileHasChange: true,
                                newFile: img.filename,
                                seasonTypeID: 1,
                                thumbnailLocation: context.backendUrl + 'avi.png'
                            });
                        }, null);
                    });
                };
                masterUploader.open();
            },
            removeGalleryItem: function (id) {
                //return;
                if (confirm('Are you sure ?')) {
                    isExist = false;
                    for (j = 0; j < $scope.data.imageGalleries.length; j++) {
                        if ($scope.data.imageGalleries[j].imageGalleryID == id) {
                            isExist = true;
                            break;
                        }
                    }
                    if (isExist) {
                        $scope.data.imageGalleries.splice(j, 1);
                    }
                }
            },
            setDefaultItem: function (id, type) {
                //return;

                angular.forEach($scope.data.imageGalleries, function (value, key) {
                    if (value.galleryItemTypeID == this) {
                        value.isDefault = false;
                    }
                }, type);

                isExist = false;
                for (j = 0; j < $scope.data.imageGalleries.length; j++) {
                    if ($scope.data.imageGalleries[j].imageGalleryID == id) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    $scope.data.imageGalleries[j].isDefault = true;
                }
            },
            editImage: function (item) {
                scope.currentImage = JSON.parse(JSON.stringify(item));
                jQuery("#imageForm").modal();
            },
            updateImage: function () {
                var index = scope.method.getImageIndex(scope.currentImage.imageGalleryID)
                if (index >= 0) {
                    scope.data.imageGalleries[index] = scope.currentImage;
                }
                jQuery("#imageForm").modal('hide');
            },
            toggleWarehouseConnectSelection: function (item) {
                if (item.isUsedForWarehouseConnect) {
                    item.isUsedForWarehouseConnect = false;
                }
                else {
                    item.isUsedForWarehouseConnect = true;
                }
            },

            //
            //Test Report
            //
            addReport: function () {
                var newReport = {
                    testReportID: $scope.method.getNewID(),
                    scanFile: null,
                    remark: null,
                    friendlyName: null,
                    scanFileLocation: null,
                    testDate: null,
                    scanHasChange: false,
                    scanNewFile: null
                };
                $scope.event.editReport(newReport);
            },
            editReport: function (item) {
                $scope.currentReport = JSON.parse(JSON.stringify(item));
                //console.log($scope.currentReport);
                jQuery("#reportForm").modal();
            },
            updateReport: function () {
                var index = $scope.method.getReportIndex($scope.currentReport.testReportID)
                if (index >= 0) {
                    $scope.data.testReports[index] = $scope.currentReport;
                }
                else {
                    $scope.data.testReports.push($scope.currentReport);
                }
                jQuery("#reportForm").modal('hide');

                $scope.method.renderReport();
                //console.log($scope.currentReport);
            },

            removeReport: function (item) {
                if (confirm('Are you sure ?')) {
                    $scope.data.testReports.splice($scope.data.testReports.indexOf(item), 1);
                }
            },

            addReportScanFile: function () {
                masterUploader.multiSelect = true;
                masterUploader.imageOnly = false;
                masterUploader.callback = function () {
                    var scope = angular.element(jQuery('body')).scope();
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            // TO DO LIST
                            scope.currentReport.scanHasChange = true;
                            scope.currentReport.scanFileLocation = img.fileURL;
                            scope.currentReport.scanNewFile = img.filename;
                            scope.currentReport.friendlyName = img.filename;
                        }, null);
                    });
                };
                masterUploader.open();
            },

            /*
             TD File
             */
            addTDfile: function () {
                var newTDfile = {
                    tdFileID: $scope.method.getNewID(),
                    scanFile: null,
                    remark: null,
                    scanFileLocation: null,
                    date: null,
                    friendlyName: null,
                    scanHasChange: false,
                    scanNewFile: null,
                    title: null
                };
                $scope.event.editTDfile(newTDfile)
            },

            editTDfile: function (item) {
                $scope.currenTDfile = JSON.parse(JSON.stringify(item));
                jQuery("#tdForm").modal();
            },

            updatetdfile: function () {
                debugger
                var index = $scope.method.gettdfileIndex($scope.currenTDfile.tdFileID)
                if (index >= 0) {
                    $scope.data.tdfiles[index] = $scope.currenTDfile;
                }
                else {
                    $scope.data.tdfiles.push($scope.currenTDfile)
                }

                jQuery("#tdForm").modal('hide');
                $scope.method.rendertdfile();
            },

            removetdfile: function (item) {
                if (confirm("Are you Sure Delete this Data ?")) {
                    $scope.data.tdfiles.splice($scope.data.tdfiles.indexOf(item), 1)
                }
            },

            addtdScanFile: function () {
                masterUploader.multiSelect = true;
                masterUploader.imageOnly = false;
                masterUploader.callback = function () {
                    var scope = angular.element(jQuery('body')).scope();
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            scope.currenTDfile.scanHasChange = true;
                            scope.currenTDfile.scanFileLocation = img.fileURL;
                            scope.currenTDfile.friendlyName = img.filename;
                            scope.currenTDfile.scanNewFile = img.filename;
                        }, null);
                    });
                };
                masterUploader.open();
            },

            editQCIssue: function ($event, id, mdlId) {
                $event.preventDefault();
                $scope.isEditData = true;

                if (id == null) {
                    id = 0;
                }

                jsonService.initTracking(
                    id,
                    mdlId,
                    function (data) {
                        $scope.editData = data.data.data;

                        $scope.supportTrackingStatus = data.data.supportQCTrackingStatus;
                        $scope.supportTrackingType = data.data.supportQCTrackingType;

                        $scope.$apply();
                    },
                    function (error) {
                    });
            },
            backQCIssue: function ($event) {
                $event.preventDefault();
                $scope.isEditData = false;
            },
            initTabQCIssued: function () {
                //console.log(isFirstQCIssue);
                if (isFirstQCIssue == false) {
                    jsonService.loadTabQCIssued(
                        context.id,
                        function (data) {
                            $scope.modelIssue = data.data.modelIssue;
                            $scope.modelIssueTracking = data.data.modelIssueTrackings;
                            $scope.$apply();

                            isFirstQCIssue = true;
                        },
                        function (error) {
                            isFirstQCIssue = false;
                        });
                }
            },
            addImageErrorModelIssueTracking: function () {
                masterUploader.multiSelect = true;
                masterUploader.imageOnly = true;
                masterUploader.callback = function () {
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            $scope.modelIssueTrackImagesError.push({
                                modelIssueTrackImageErrorID: $scope.method.getNewID(),
                                imageFile_HasChange: true,
                                imageFile_NewFile: img.filename,
                                thumbnailLocation: img.fileURL,
                                fileLocation: img.fileURL
                            });
                        }, null);
                    });
                };
                masterUploader.open();
            },
            addImageCompleteModelIssueTracking: function () {
                masterUploader.multiSelect = true;
                masterUploader.imageOnly = true;
                masterUploader.callback = function () {
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            $scope.modelIssueTrackImagesFinish.push({
                                modelIssueTrackImageFinishID: $scope.method.getNewID(),
                                imageFile_HasChange: true,
                                imageFile_NewFile: img.filename,
                                thumbnailLocation: img.fileURL,
                                fileLocation: img.fileURL
                            });
                        }, null);
                    });
                };
                masterUploader.open();
            },
            removeImageErrorModelIssueTracking: function ($index) {
                $scope.modelIssueTrackImagesError.splice($index, 1);
            },
            removeImageFinishModelIssueTracking: function ($index) {
                $scope.modelIssueTrackImagesFinish.splice($index, 1);
            },
            updateDetailTracking: function ($event, id, idIssue) {
                $event.preventDefault();

                if ($scope.editData.modelIssueTrackImagesFinish.length > 0) {
                    //console.log($scope.editData.issueDateFormatted);
                    if ($scope.editData.issueDateFormatted == null || $scope.editData.issueDateFormatted == "") {
                        jsHelper.showMessage("warning", "Please inut Issue Date !!");
                        return false;
                    }
                }

                var isValid = $scope.method.checkIsValid();

                if ($scope.editData.typeID != 6) {
                    $scope.editData.otherContent = null;
                }
                //console.log($scope.editData)

                if (isValid) {
                    jsonService.saveDetailTracking(
                    id,
                    idIssue,
                    $scope.editData,
                    function (data) {
                        $scope.isEditData = false;
                        jsHelper.processMessage(data.message);
                        $scope.event.init();
                    },
                    function (error) {
                        jsHelper.showMessage("warning", error);
                    });
                } else {
                    jsHelper.showMessage('warning', 'Invalid input data, please check');
                }
            },
            deleteDetailTracking: function ($event, id) {
                $event.preventDefault();
                jsonService.deleteDetailTracking(
                    id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        $scope.event.init();
                    },
                    function (error) {
                        jsHelper.showMessage("warning", error);
                    });
            },

            //
            // price configure
            //
            getDefaultPriceSetting: function () {
                // clear current setting
                angular.forEach(angular.copy($scope.data.modelPriceConfigurations), function (value, key) {
                    if (value.season === $scope.season) {
                        $scope.data.modelPriceConfigurations.splice($scope.data.modelPriceConfigurations.indexOf(value), 1);
                    }
                }, null);

                // add default setting
                angular.forEach($scope.modelPriceConfigurationDefault, function (value, key) {
                    if (value.season === $scope.season) {
                        var details = [];
                        angular.forEach(value.modelPriceConfigurationDetails, function (item, itemKey) {
                            details.push({
                                modelPriceConfigurationDetailID: $scope.method.getNewID(),
                                optionID: item.optionID,
                                percentValue: parseFloat(item.percentValue),
                                usdAmount: parseFloat(item.usdAmount),
                                eurAmount: parseFloat(item.eurAmount),
                                optionNM: item.optionNM
                            });
                        }, null);
                        $scope.data.modelPriceConfigurations.push({
                            modelPriceConfigurationID: $scope.method.getNewID(),
                            season: $scope.season,
                            updatedBy: null,
                            updatedDate: null,
                            employeeNM: '',
                            productElementID: value.productElementID,
                            productElementNM: value.productElementNM,
                            modelPriceConfigurationDetails: details
                        });
                    }
                }, null);
            },
            addFixPriceOption: function () {
                var selectedOption = jQuery('#fixPriceOption').select2('data');
                var isExist = false;
                angular.forEach($scope.data.modelFixPriceConfigurations, function (value, key) {
                    if (value.materialTypeID === selectedOption.id) {
                        isExist = true;
                    }
                }, null);
                if (!isExist) {
                    $scope.data.modelFixPriceConfigurations.push({
                        modelFixPriceConfigurationID: $scope.method.getNewID(),
                        season: $scope.season,
                        materialTypeID: selectedOption.id,
                        uSDAmount: null,
                        eURAmount: null,
                        materialTypeNM: selectedOption.text,
                    });
                }
            },
            removeFixPriceOption: function (item) {
                $scope.data.modelFixPriceConfigurations.splice($scope.data.modelFixPriceConfigurations.indexOf(item), 1);
            },
            addOptionConfigure: function (optionSet) {
                var selectedOption = jQuery('#productElementID-' + optionSet.productElementID).select2('data');
                var isExist = false;
                angular.forEach(optionSet.modelPriceConfigurationDetails, function (value, key) {
                    if (value.optionID === selectedOption.id) {
                        isExist = true;
                    }
                }, null);
                if (!isExist) {
                    optionSet.modelPriceConfigurationDetails.push({
                        modelPriceConfigurationDetailID: $scope.method.getNewID(),
                        optionID: selectedOption.id,
                        percentValue: null,
                        uSDAmount: null,
                        eURAmount: null,
                        optionNM: selectedOption.text,
                    });
                }
            },
            removeOptionConfigure: function (optionset, item) {
                optionset.modelPriceConfigurationDetails.splice(optionset.modelPriceConfigurationDetails.indexOf(item), 1);
            },

            // Customize add image
            downloadFile: function () {
                //debugger;
                if ($scope.editData.fileLocation) {
                    window.open($scope.editData.fileLocation);
                }
            },
            changeFile: function () {
                masterUploader.multiSelect = true;
                masterUploader.imageOnly = false;
                masterUploader.callback = function () {
                    var scope = angular.element(jQuery('body')).scope();
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            scope.editData.fileLocation = img.fileURL;
                            scope.editData.friendlyName = img.filename;
                            scope.editData.qualityReport_NewFile = img.filename;
                            scope.editData.qualityReport_HasChange = true;

                            scope.editData.qualityReport = scope.editData.friendlyName;
                        }, null);
                    });
                };
                masterUploader.open();
            },
            removeFile: function () {
                //debugger;
                $scope.editData.friendlyName = '';
                $scope.editData.fileLocation = '';
                $scope.editData.qualityReport_NewFile = '';
                $scope.editData.qualityReport_HasChange = true;
            },

            // New tracking
            addTracking: function () {
                $scope.editData = {
                    modelIssueTrackID: $scope.method.getNewID(),
                    description: null,
                    issueDateFormatted: null,
                    statusBy: null,
                    statusName: '',
                    typeID: null,
                    typeName: '',
                    otherContent: null,
                    comment: null,
                    qualityReport: null
                };

                $scope.isEditData = true;
            },

            // Edit tracking
            editTracking: function (item) {
                $scope.editData = item;
                //debugger;
                $scope.isEditData = true;
            },

            // Remove tracking
            removeTracking: function (item) {
                var index = $scope.data.modelIssueTracks.indexOf(item);
                $scope.data.modelIssueTracks.splice(index, 1);
            },

            // Save tracking
            saveTracking: function () {
                if ($scope.data.modelIssueTracks === null) {
                    $scope.data.modelIssueTracks = [];
                }

                if ($scope.editData.modelIssueTrackID < 0) {
                    if ($scope.data.modelIssueTracks.length > 0) {
                        //debugger;
                        var lastIndex = $scope.data.modelIssueTracks.length - 1;

                        $scope.editData.statusName = $scope.method.getStatusName($scope.editData.statusBy);
                        $scope.editData.typeName = $scope.method.getTypeName($scope.editData.typeID);

                        if ($scope.data.modelIssueTracks[lastIndex].modelIssueTrackID !== $scope.editData.modelIssueTrackID) {
                            $scope.data.modelIssueTracks.push($scope.editData);
                        }
                    } else {
                        $scope.editData.statusName = $scope.method.getStatusName($scope.editData.statusBy);
                        $scope.editData.typeName = $scope.method.getTypeName($scope.editData.typeID);

                        $scope.data.modelIssueTracks.push($scope.editData);
                    }
                } else {
                    for (var i = 0; i < $scope.data.modelIssueTracks.length ; i++) {
                        var ele = $scope.data.modelIssueTracks[i];

                        if ($scope.editData.modelIssueTrackID === ele.modelIssueTrackID) {
                            ele.comment = $scope.editData.comment;
                            ele.description = $scope.editData.description;
                            ele.fileLocation = $scope.editData.fileLocation;
                            ele.qualityReport = $scope.editData.qualityReport;
                            ele.issueDateFormatted = $scope.editData.issueDateFormatted;
                            ele.followUp = $scope.editData.followUp;
                            ele.employeeName = $scope.editData.employeeName;
                            ele.fullName = $scope.editData.fullName;
                            ele.statusName = $scope.method.getStatusName($scope.editData.statusBy);
                            ele.typeName = $scope.method.getTypeName($scope.editData.typeID);
                        }
                    }
                }

                $scope.isEditData = false;
            },

            // Set default Factory selected
            setDefaultFactory: function (item) {
                angular.forEach($scope.data.modelDefaultFactories, function (value, key) {
                    value.isDefaultFactory = false;
                });

                item.isDefaultFactory = true;
                $scope.data.defaultFactoryID = item.factoryID;
                $scope.data.defaultFactoryUD = item.factoryUD;
            },

            getDefaultFactoryDetail: function (modelID, factoryID) {
                jsonService.getModelDefaultFactoryDetail(
                    modelID,
                    factoryID,
                    function (data) {
                        $scope.detailFactoryDefault = data.data;

                        $scope.$apply();

                        $('#frmDefaultFactoryModel').modal();
                    },
                    function (error) {
                    });
            },

            //For LazyLoad
            getLinkedData: function (param) {
                switch (param) {
                    case '#tabSample':
                        if ($scope.loadingState.sample) return;

                        jsonService.getSampleProductData(
                          context.id,
                          function (data) {
                              $scope.sampleProductViews = data.data.data.sampleProductViews;
                              $scope.loadingState.sample = true;

                              $scope.$apply();
                          },
                          function (error) {
                              jsHelper.showMessage('warning', error);
                              $scope.$apply();
                          }
                        );
                        break;

                    case '#tabProducts':
                        if ($scope.loadingState.product) return;

                        jsonService.getProductData(
                            context.id,
                            function (data) {
                                $scope.productsViews = data.data.data.productsViews;
                                $scope.loadingState.product = true;

                                $scope.$apply();
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error);
                                $scope.$apply();
                            }
                        );
                        break;
                }
            },
        };

        //
        // method
        //
        $scope.method = {
            refresh: function (id) {
                jsHelper.loadingSwitch(true);
                window.location = context.refreshUrl + id;
            },
            getNewID: function () {
                $scope.newid--;
                return $scope.newid;
            },
            getImageIndex: function (id) {
                var isExist = false;
                var index = 0;
                for (var index = 0; index < $scope.imageGalleries.length; index++) {
                    if ($scope.imageGalleries[index].imageGalleryID == id) {
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

            //Report
            getReportIndex: function (id) {
                var isExist = false;
                var index = 0;
                for (var index = 0; index < $scope.data.testReports.length; index++) {
                    if ($scope.data.testReports[index].testReportID == id) {
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
            renderReport: function () {
                $scope.reports = [];
                angular.forEach($scope.data.testReports, function (value, key) {
                    if ($scope.reports.indexOf(value.report) < 0) {
                        $scope.reports.push(value.report);
                    }
                }, null);
            },

            //tdfile
            gettdfileIndex: function (id) {
                var isExist = false;
                var index = 0;
                for (var index = 0; index < $scope.data.tdfiles.length; index++) {
                    if ($scope.data.tdfiles[index].tdFileID = id) {
                        isExist = true;
                        break;
                    }
                    if (!isExist) {
                        return -1;
                    } else {
                        return index;
                    }
                }
            },
            rendertdfile: function () {
                $scope.tdfile = [];
                angular.forEach($scope.data.tdfiles, function (value, key) {
                    if ($scope.tdfile.indexOf(value.tdfile) < 0) {
                        $scope.tdfile.push(value.tdfile);
                    }
                }, null);
            },

            checkIsValid: function () {
                var scope = $scope.editData;

                if (scope.description == null)
                    return false;

                if (scope.statusBy == 0)
                    return false;

                //if (scope.followUp == 0)
                //    return false;

                //if (scope.modelIssueTrackImagesError == null || scope.modelIssueTrackImagesError.length == 0)
                //    return false;

                return true;
            },

            // Quality notice
            getStatusName: function (id) {
                for (var i = 0; i < $scope.supportTrackingStatus.length; i++) {
                    if ($scope.supportTrackingStatus[i].trackingStatusID === id) {
                        return $scope.supportTrackingStatus[i].trackingStatusNM;
                    }
                }
            },
            getTypeName: function (id) {
                for (var i = 0; i < $scope.supportTrackingType.length; i++) {
                    if ($scope.supportTrackingType[i].trackingTypeID === id) {
                        return $scope.supportTrackingType[i].trackingTypeNM;
                    }
                }
            }
        };

        //
        // search piece controller
        //
        $scope.quicksearchPiece = {
            data: null,
            selectValue: false,
            filters: {
                filters: {
                    searchQuery: '',
                    modelID: context.id
                },
                sortedBy: 'ModelNM',
                sortedDirection: 'ASC',
                pageSize: 20,
                pageIndex: 1
            },
            totalPage: 0,
            method: {
                search: function () {
                    $scope.quicksearchPiece.filters.filters.searchQuery = jQuery('#quicksearchBoxPiece').val();
                    jsonService.quicksearchPiece(
                        $scope.quicksearchPiece.filters,
                        function (data) {
                            if (data.message.type == 0) {
                                $scope.quicksearchPiece.data = data.data;
                                $scope.quicksearchPiece.totalPage = Math.ceil(data.totalRows / $scope.quicksearchPiece.filters.pageSize);
                                grdPieceSearchResult.updateLayout();
                                $scope.$apply();
                                //console.log($scope.quicksearchPiece.data);
                                jQuery('#piece-popup').show();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                }
            },
            event: {
                searchBoxKeyPress: function (e) {
                    if (e.keyCode == 13) {
                        $scope.quicksearchPiece.method.search();
                    }
                },
                close: function ($event) {
                    jQuery('#piece-popup').hide();
                    jQuery('#quicksearchBoxPiece').val('');
                    $scope.quicksearchPiece.data = null;
                    $event.preventDefault();
                },
                ok: function ($event) {
                    jQuery.each($scope.quicksearchPiece.data, function () {
                        if (this.isSelected) {
                            // check for existance
                            param = {
                                id: this.modelID,
                                isOK: true
                            }
                            angular.forEach($scope.data.pieces, function (value, key) {
                                if (value.pieceModelID == this.id) {
                                    this.isOK = false;
                                }
                            }, param);

                            if (param.isOK) {
                                $scope.data.pieces.push({
                                    modelPieceID: $scope.method.getNewID(),
                                    pieceModelID: this.modelID,
                                    modelUD: this.modelUD,
                                    modelNM: this.modelNM,
                                    productTypeNM: this.productTypeNM,
                                    quantity: 1
                                });
                            }
                        }
                    });
                    grdPieceSearchResult.updateLayout();
                    $scope.quicksearchPiece.event.close($event);
                },
                selectAll: function () {
                    angular.forEach($scope.quicksearchPiece.data, function (value, key) {
                        value.isSelected = $scope.quicksearchPiece.selectValue;
                    }, null);
                }
            }
        }

        //
        // search sub material option controller
        //
        $scope.quicksearchSubMaterial = {
            data: null,
            selectValue: false,
            filters: {
                filters: {
                    searchQuery: ''
                },
                sortedBy: 'SubMaterialOptionNM',
                sortedDirection: 'ASC',
                pageSize: 20,
                pageIndex: 1
            },
            totalPage: 0,
            method: {
                search: function () {
                    $scope.quicksearchSubMaterial.filters.filters.searchQuery = jQuery('#quicksearchBoxSubMaterial').val();
                    jsonService.quicksearchSubMaterialOption(
                        $scope.quicksearchSubMaterial.filters,
                        function (data) {
                            if (data.message.type == 0) {
                                $scope.quicksearchSubMaterial.data = data.data;
                                $scope.quicksearchSubMaterial.totalPage = Math.ceil(data.totalRows / $scope.quicksearchSubMaterial.filters.pageSize);
                                grdSubMaterialSearchResult.updateLayout();
                                $scope.$apply();
                                jQuery('#SubMaterial-popup').show();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                }
            },
            event: {
                searchBoxKeyPress: function (e) {
                    if (e.keyCode == 13) {
                        $scope.quicksearchSubMaterial.method.search();
                    }
                },
                close: function ($event) {
                    jQuery('#SubMaterial-popup').hide();
                    jQuery('#quicksearchBoxSubMaterial').val('');
                    $scope.quicksearchSubMaterial.data = null;
                    $scope.quicksearchSubMaterial.selectValue = false;
                    $event.preventDefault();
                },
                ok: function ($event) {
                    jQuery.each($scope.quicksearchSubMaterial.data, function () {
                        if (this.isSelected) {
                            // check for existance
                            param = {
                                id: this.subMaterialOptionID,
                                isOK: true
                            }
                            angular.forEach($scope.data.subMaterialOptions, function (value, key) {
                                if (value.subMaterialOptionID == this.id) {
                                    this.isOK = false;
                                }
                            }, param);

                            if (param.isOK) {
                                $scope.data.subMaterialOptions.push({
                                    modelSubMaterialOptionID: $scope.method.getNewID(),
                                    subMaterialOptionID: this.subMaterialOptionID,
                                    subMaterialOptionUD: this.subMaterialOptionUD,
                                    subMaterialOptionNM: this.subMaterialOptionNM,
                                    imageFile_DisplayUrl: this.imageFile_DisplayUrl,
                                    isStandard: this.isStandard,
                                    season: this.season,
                                    isEnabled: true
                                });
                            }
                        }
                    });
                    grdSubMaterialSearchResult.updateLayout();
                    $scope.quicksearchSubMaterial.event.close($event);
                },
                selectAll: function () {
                    angular.forEach($scope.quicksearchSubMaterial.data, function (value, key) {
                        value.isSelected = $scope.quicksearchSubMaterial.selectValue;
                    }, null);
                }
            }
        }

        $scope.defaultOptionForm = {
            showEditProductWinzard: function () {
                var productData = $scope.data;
                //open winzard
                productWizard.open({
                    articleCode: productData.articleCode,
                    description: productData.description,
                    modelID: productData.modelID,
                    materialID: productData.materialID,
                    materialTypeID: productData.materialTypeID,
                    materialColorID: productData.materialColorID,
                    frameMaterialID: productData.frameMaterialID,
                    frameMaterialColorID: productData.frameMaterialColorID,
                    subMaterialID: productData.subMaterialID,
                    subMaterialColorID: productData.subMaterialColorID,
                    backCushionID: productData.backCushionID,
                    seatCushionID: productData.seatCushionID,
                    cushionColorID: productData.cushionColorID,
                    fSCTypeID: productData.fscTypeID,
                    fSCPercentID: productData.fscPercentID,
                    useFSCLabel: productData.useFSCLabel,
                    manufacturerCountryID: productData.manufacturerCountryID,
                    packagingMethodID: productData.packagingMethodID,
                    materialText: '',
                    frameText: '',
                    subMaterialText: '',
                    cushionText: '',
                    fscText: '',
                    packagingMethodText: '',
                    cushionDescription: productData.cushionRemark,
                    displayCushionDescription: false,
                    displayPackagingMethod: false,
                    modelImage: '',
                    frameImage: '',
                    materialImage: '',
                    subMaterialImage: '',
                    cushionImage: '',
                    cushionColorImage: '',
                    backCushionImage: '',
                    seatCushionImage: '',

                    showPricingOption: false,
                    pricingOption: [],
                    season: '',
                    currency: '',
                    discountPercent: null,
                    discountAmount: null,
                    salePriceCalculated: null,
                    onFinish: function (data) {
                        //read return data
                        //productData.articleCode = data.articleCode;
                        //productData.description = data.description + (data.cushionDescription == '' || data.cushionDescription == null ? '' : '(' + data.cushionDescription + ')');
                        //productData.modelID = data.modelID;
                        productData.materialID = data.materialID;
                        productData.materialTypeID = data.materialTypeID;
                        productData.materialColorID = data.materialColorID;
                        productData.frameMaterialID = data.frameMaterialID;
                        productData.frameMaterialColorID = data.frameMaterialColorID;
                        productData.subMaterialID = data.subMaterialID;
                        productData.subMaterialColorID = data.subMaterialColorID;
                        productData.backCushionID = data.backCushionID;
                        productData.seatCushionID = data.seatCushionID;
                        productData.cushionColorID = data.cushionColorID;
                        productData.fscTypeID = data.fSCTypeID;
                        productData.fscPercentID = data.fSCPercentID;
                        productData.manufacturerCountryID = data.manufacturerCountryID;
                        //productData.cushionDescription = data.cushionDescription;
                        //productData.cushionRemark = data.cushionDescription;
                        //productData.packagingMethodID = data.packagingMethodID;
                        //productData.useFSCLabel = data.useFSCLabel;

                        //text
                        productData.materialText = data.materialText;
                        productData.frameText = data.frameText;
                        productData.subMaterialText = data.subMaterialText;
                        productData.cushionText = data.cushionText;
                        productData.fscText = data.fscText;

                        //image
                        productData.materialThumbnailLocation = data.materialImage;
                        productData.frameMaterialThumbnailLocation = data.frameImage;
                        productData.subMaterialColorThumbnailLocation = data.subMaterialImage;
                        productData.backCushionThumbnailLocation = data.backCushionImage;
                        productData.seatCushionThumbnailLocation = data.seatCushionImage;
                        productData.cushionColorThumbnailLocation = data.cushionColorImage;

                        //mark to finish edited
                        //$scope.editOfferLineForm.offerLineData.isEditing = true;

                        //price option
                        //productData.offerLinePriceOptions = data.pricingOption;
                        //productData.discountPercent = data.discountPercent;
                        //productData.discountAmount = data.discountAmount;
                        //productData.salePriceCalculated = data.salePriceCalculated;
                    }
                });

                console.log($scope.data);

            },
        }

        $scope.event.init();
    }]);