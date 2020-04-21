//
// SUPPORT
//
jQuery('#main-form').keypress(function (e) {
    if (e.keyCode === 13) {
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
    if (e.keyCode === 13) {
        jQuery("#description").val(jQuery("#description").val() + "\n");
    }
});

jQuery("#comments").keydown(function (e) {
    if (e.keyCode === 13) {
        jQuery("#comments").val(jQuery("#comments").val() + "\n");
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'furnindo-directive', 'customFilters', 'ui']);
tilsoftApp.filter("trustUrl", ['$sce', function ($sce) {
    return function (recordingUrl) {
        return $sce.trustAsResourceUrl(recordingUrl);
    };
}]);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', function ($scope, $timeout) {
    //
    // property
    //
    $scope.data = null;    
    $scope.productTypes = null;
    $scope.productGroups = null;
    $scope.packagingMethods = null;
    $scope.checkListGroupDTO = null;
    $scope.modelCheckListGroupDTO = null;
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
    $scope.productBreakDowns = null;
    $scope.factoryDTOs = null;
    $scope.clientSpecialPackagingMethods = null;
    $scope.defaultFactoryID = null;
    $scope.seasonModelDefaultOption = jsHelper.getCurrentSeason();
    $scope.nullHandler = {
        loadMore: function () {
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.$apply(function () {
                $scope.orderByColumn = (sortedDirection === 'desc' ? '-' : '') + sortedBy;
            });
        },
        isTriggered: false
    };

    $scope.collections = ["ALU - LINE", "NAUTICA OUTDOOR", "AZZURA", "TRENDLINE", "KENSINGTON", "COLOURS"];

    //Td File
    $scope.currenTDfile = null;
    $scope.tdfiles = [];

    $scope.currenAIfile = null;
    $scope.aifiles = [];

    $scope.modelIssueStrongPoint = null;
    $scope.modelIssueWeakPoint = null;
    $scope.modelIssueTracking = null;
    $scope.modelIssueTrack = null;
    $scope.modelIssueTrackImagesError = [];
    $scope.modelIssueTrackImagesFinish = [];

    $scope.editData = [];
    $scope.detailFactoryDefault = [];
    $scope.supportUsers = [];
    $scope.supportStatus = [];

    $scope.isEditQCIssue = false;

    $scope.isEditData = false;
    $scope.supportTrackingStatus = null;
    $scope.supportTrackingType = null;

    var isFirstQCIssue = false;

    // price setting
    $scope.materialTypes = [];
    $scope.productElements = [];
    $scope.productElementOptions = [];
    $scope.season = jsHelper.getCurrentSeason();
    $scope.seasonDefaultOption = jsHelper.getCurrentSeason();
    $scope.modelPriceConfigurationDefault = [];
    $scope.isPriceEnabled = false;
    $scope.factoryID = null;
    $scope.factoryUD = null;
  

    $scope.canCreate = context.canCreate;

    $scope.loadAbilityTypes = [
        { 'loadAbilityTypeID': 1, 'loadAbilityTypeNM': 'ACTUAL' },
        { 'loadAbilityTypeID': 2, 'loadAbilityTypeNM': 'INDICATED' }
    ];

    $scope.clientFilter = {
        'season': jsHelper.getCurrentSeason(),
        'clientID': 0
    };

    // Approve
    $scope.canApprove = false;

    $scope.loadingState = {
        sample: false,
        product: false,
        productBreakDown: false,
        modelDefaultOption: false,
        client: false,
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
                    if ($scope.data.modelID > 0 && $scope.data.modelDefaultFactories.length > 0) {
                        if ($scope.data.modelDefaultFactories.filter(o => o.isDefault).length > 0) {
                            $scope.defaultFactoryID = $scope.data.modelDefaultFactories.filter(o => o.isDefault)[0].factoryID;
                        }
                        else {
                            $scope.data.modelDefaultFactories[0].isDefault = true;
                            $scope.defaultFactoryID = $scope.data.modelDefaultFactories[0].factoryID;
                        }
                    }
                    console.log(data);
                    $scope.clients = data.data.clients;
                    $scope.imageGalleries = data.data.data.imageGalleries;
                    $scope.imageGalleryVersions = data.data.data.imageGalleryVersions;
                    $scope.productTypes = data.data.productTypes;
                    $scope.productGroups = data.data.productGroups;
                    $scope.packagingMethods = data.data.packagingMethods;
                    $scope.checkListGroupDTO = data.data.checkListGroupDTO;
                    $scope.seasons = data.data.seasons;
                    $scope.manufacturerCountries = data.data.manufacturerCountries;
                    $scope.modelStatuses = data.data.modelStatuses;                 
                    $scope.productCategories = data.data.productCategories;
                    $scope.factories = data.data.factories;
                    $scope.modelID = data.data.data.modelID;
                    $scope.galleryItemTypes = data.data.galleryItemTypes;
                    $scope.data.testReports = data.data.data.testReports;
                    $scope.data.tdfiles = data.data.data.tdfiles;
                    $scope.aifiles = data.data.data.aiFiles; //ai file
                    $scope.productElements = data.data.productElements;
                    $scope.productElementOptions = data.data.productElementOptions;
                    $scope.materialTypes = data.data.materialTypes;
                    $scope.modelPriceConfigurationDefault = data.data.modelPriceConfigurationDefault;
                    $scope.isPriceEnabled = data.data.isPriceEnabled;
                    $scope.testReports = data.data.data.testReports;
                    $scope.tdfiles = data.data.data.tdfiles;
                    $scope.factoryDTOs = data.data.factoryDTOs;
                    $scope.clientSpecialPackagingMethods = data.data.clientSpecialPackagingMethods;
                    //console.log($scope.factoryDTOs);
                    //$scope.sampleProductViews = data.data.data.sampleProductViews;
                    //$scope.productsViews = data.data.data.productsViews;

                    // cuong.tran
                    $scope.modelIssueStrongPoint = data.data.data.modelIssueStrongPoint;
                    $scope.modelIssueWeakPoint = data.data.data.modelIssueWeakPoint;
                    $scope.modelIssueTracking = data.data.data.modelIssueTracks;
                    // cuong.tran

                    $scope.supportTrackingStatus = data.data.supportQCTrackingStatus;
                    $scope.supportTrackingType = data.data.supportQCTrackingType;

                    //console.log($scope.data.modelPriceConfigurations);
                    $scope.canApprove = data.data.canApprove;

                    //bind default selected value for FactoryID
                    if ($scope.data !==  null && $scope.data.modelDefaultFactories !==  null) {
                        angular.forEach($scope.data.modelDefaultFactories.filter(s => s.IsDefault), function (item) {
                            $scope.factoryID = item.factoryID;
                            $scope.factoryUD = item.factoryUD;
                        });
                    }                    

                    //bind configPrice factoryBreakdown
                    $scope.data.configPriceDTOs = data.data.configPriceDTOs;

                    $scope.factoryID = $scope.method.getDefaultFactory();
                    $scope.event.bindFactoryID($scope.factoryID);

                    $scope.$apply();

                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                    //setTimeout(function () { pageSetUp(); }, 500);
                    $timeout(function () { pageSetUp(); }, 500);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.productTypes = null;
                    $scope.productGroups = null;
                    $scope.packagingMethods = null;
                    $scope.checkListGroupDTO = null;
                    $scope.seasons = null;
                    $scope.manufacturerCountries = null;
                    $scope.modelStatuses = null;
                    $scope.productCategories = null;
                    $scope.factories = null;
                    $scope.productElements = null;
                    $scope.productElementOptions = null;
                    $scope.materialTypes = null;
                    $scope.modelPriceConfigurationDefault = null;
                    $scope.isPriceEnabled = false;
                    $scope.$apply();
                }
            );

            $scope.method.bindPurchasingPrices();
        },
        update: function ($event) {
            $event.preventDefault();
            if ($scope.editForm.$valid) {
                if ($scope.data.modelDefaultFactories.length > 0) {
                    var factoryID = null;
                    if (!$scope.defaultFactoryID) {
                        $scope.data.modelDefaultFactories[0].isDefault = true;
                        factoryID = $scope.data.modelDefaultFactories[0].factoryID;
                    }
                    else {
                        if ($scope.data.modelDefaultFactories.filter(o => o.factoryID === $scope.defaultFactoryID).length > 0) {
                            $scope.data.modelDefaultFactories.filter(o => o.factoryID === $scope.defaultFactoryID)[0].isDefault = true;
                            factoryID = $scope.data.modelDefaultFactories.filter(o => o.factoryID === $scope.defaultFactoryID)[0].factoryID;
                        }
                        else {
                            $scope.data.modelDefaultFactories[0].isDefault = true;
                            factoryID = $scope.data.modelDefaultFactories.filter(o => o.factoryID === $scope.defaultFactoryID)[0].factoryID;
                        }
                    }

                    angular.forEach($scope.data.modelDefaultFactories.filter(o => o.factoryID !== factoryID), function (item) {
                        item.isDefault = false;
                    });
                }

                // get season default options
                var seasonDefaultOption = jQuery('#seasonDefaultOption').val();
                if (seasonDefaultOption) {
                    seasonDefaultOption = seasonDefaultOption.substring(7, seasonDefaultOption.length);
                    $scope.data.seasonDefaultOption = seasonDefaultOption;
                }

                //remove Purchasing Fix Price have FactoryID is null
                angular.forEach($scope.data.modelPurchasingFixPriceConfigurationDTOs.filter(s => s.factoryID === null), function (item) {
                    $scope.data.modelPurchasingFixPriceConfigurationDTOs.splice($scope.data.modelPurchasingFixPriceConfigurationDTOs.indexOf(item), 1);
                });

                //remove Purchasing Price have FactoryID is null
                angular.forEach($scope.data.modelPurchasingPriceConfigurationDTOs.filter(s => s.factoryID === null), function (item) {
                    $scope.data.modelPurchasingPriceConfigurationDTOs.splice($scope.data.modelPurchasingPriceConfigurationDTOs.indexOf(item), 1);
                });

                //remove Purchasing Price Status have FactoryID is null               
                angular.forEach($scope.data.modelPurchasingPriceStatusDTOs.filter(s => s.factoryID === null), function (item) {
                    $scope.data.modelPurchasingPriceStatusDTOs.splice($scope.data.modelPurchasingPriceStatusDTOs.indexOf(item), 1);
                });



                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
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
                        if (data.message.type === 0) {
                            window.location = context.backUrl;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },

        add: function (factoryID, $event) {

            $event.preventDefault();

            if (factoryID === null) {
                alert("Please choose a factory !!");
                return false;
            }

            if ($scope.data.modelDefaultFactories === null || $scope.data.modelDefaultFactories.length === 0) {
                $scope.data.modelDefaultFactories.push({
                    modelDefaultFactoryID: $scope.method.getNewID(),
                    modelID: $scope.data.modelID,
                    factoryID: factoryID,
                    factoryUD: $scope.method.getFactoryUD(factoryID),
                    isDefault: null,
                });
            } else {
                if (!$scope.method.checkFactoryInModelFactories(factoryID)) {
                    $scope.data.modelDefaultFactories.push({
                        modelDefaultFactoryID: $scope.method.getNewID(),
                        modelID: $scope.data.modelID,
                        factoryID: factoryID,
                        factoryUD: $scope.method.getFactoryUD(factoryID),
                        isDefault: null,
                    });
                }
            }

            $scope.data.defaultFactoryID = null;

            $scope.method.bindModelPurchasingPrice();
            $scope.method.bindPurchasingPrices();
        },

        removed: function ($event, index) {

            $event.preventDefault();

            //remove Purchasing Fix Price have FactoryID is removed
            angular.forEach($scope.data.modelPurchasingFixPriceConfigurationDTOs.filter(s => s.factoryID === $scope.data.modelDefaultFactories[index].factoryID), function (item) {
                $scope.data.modelPurchasingFixPriceConfigurationDTOs.splice($scope.data.modelPurchasingFixPriceConfigurationDTOs.indexOf(item), 1);
            });

            //remove Purchasing Price have FactoryID is removed
            angular.forEach($scope.data.modelPurchasingPriceConfigurationDTOs.filter(s => s.factoryID === $scope.data.modelDefaultFactories[index].factoryID), function (item) {
                $scope.data.modelPurchasingPriceConfigurationDTOs.splice($scope.data.modelPurchasingPriceConfigurationDTOs.indexOf(item), 1);
            });

            //remove Purchasing Price Status have FactoryID is removed
            angular.forEach($scope.data.modelPurchasingPriceStatusDTOs.filter(s => s.factoryID === $scope.data.modelDefaultFactories[index].factoryID), function (item) {
                $scope.data.modelPurchasingPriceStatusDTOs.splice($scope.data.modelPurchasingPriceStatusDTOs.indexOf(item), 1);
            });

            //remove model default option factory have FactoryID is removed
            angular.forEach($scope.data.modelDefaultFactories.filter(s => s.factoryID === $scope.data.modelDefaultFactories[index].factoryID), function (item) {
                $scope.data.modelDefaultFactories.splice($scope.data.modelPurchasingPriceStatusDTOs.indexOf(item), 1);
            });

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
                if ($scope.data.pieces[j].modelPieceID === id) {
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
                if ($scope.data.subMaterialOptions[j].modelSubMaterialOptionID === id) {
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
                if ($scope.data.spareparts[j].modelSparepartID === id) {
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
                if ($scope.data.packagingMethodOptions[j].modelPackagingMethodOptionID === id) {
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
        //checklist
        removeCheckList: function ($event, id) {
            if ($event !== null) {
                $event.preventDefault();
            }
            isExist = false;
            for (j = 0; j < $scope.data.modelCheckListGroupDTO.length; j++) {
                if ($scope.data.modelCheckListGroupDTO[j].modelCheckListGroupID === id) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.data.modelCheckListGroupDTO.splice(j, 1);
            }
        },
        addCheckList: function ($event) {
            if ($event !== null) {
                $event.preventDefault();
            }
            var item = {
                modelCheckListGroupID: $scope.method.getNewID(),
                checkListGroupID: null,
                isActive: null,
                remark: null
            };
            $scope.data.modelCheckListGroupDTO.push({
                modelCheckListGroupID: $scope.method.getNewID(),
                checkListGroupID: '',
                isActive: 0,
                remark: ''
            });
        },
        //selectedOptionCheckList: function ($event) {
        //    if ($event !== null || $scope.data.modelCheckListGroupDTO.modelCheckListGroupID === null) {
        //        $event.preventDefault();
        //    }
        //    $scope.data.modelCheckListGroupDTO.checkListGroupUD.select2({
        //        checkListGroupNM: $scope.data.modelCheckListGroupDTO.checkListGroupNM,
        //    });
        //    $event.show();
        //},  
        confirmFrame: function ($event, id) {

        },
        confirmMaterial: function ($event, id) {

        },
        confirmPackaging: function ($event, id) {

        },
        confirmCheckListGroup: function ($event, id) {

        },
        uploadFreescan: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
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
            masterUploader.imageOnly = false;
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
            masterUploader.imageOnly = false;
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
                    if ($scope.data.imageGalleries[j].imageGalleryID === id) {
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
                if (value.galleryItemTypeID === this) {
                    value.isDefault = false;
                }
            }, type);

            isExist = false;
            for (j = 0; j < $scope.data.imageGalleries.length; j++) {
                if ($scope.data.imageGalleries[j].imageGalleryID === id) {
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


        bindFactoryID: function (factoryID) {
            $scope.factoryID = factoryID;
            //$scope.factoryUD = factory.factoryUD;
           
            angular.forEach($scope.data.modelFixPriceConfigurations.filter(s => s.season === $scope.season), function (item) {
               
                if ($scope.data.modelPurchasingFixPriceConfigurationDTOs.filter(s => s.factoryID === $scope.factoryID
                    && s.season === $scope.season
                    && s.materialTypeID === item.materialTypeID).length === 0) {
                    $scope.data.modelPurchasingFixPriceConfigurationDTOs.push({
                        modelPurchasingFixPriceConfigurationID: $scope.method.getNewID(),
                        season: $scope.season,
                        materialTypeID: item.materialTypeID,
                        uSDAmount: null,
                        materialTypeNM: item.materialTypeNM,
                        factoryID: $scope.factoryID
                    });
                };
            });

            //rebind modelPurchasingPriceConfiguration
            $scope.method.bindModelPurchasingPrice();
            $scope.method.bindPurchasingPrices();
            $scope.method.emptyPurchasingPrices();
        },
        bindSeason: function (season) {
            $scope.season = season;

            if ($scope.data.modelPurchasingFixPriceConfigurationDTOs.filter(s => s.factoryID === $scope.factoryID && s.season === $scope.season).length === 0) {
                angular.forEach($scope.data.modelFixPriceConfigurations.filter(s => s.season === $scope.season), function (item) {
                    $scope.data.modelPurchasingFixPriceConfigurationDTOs.push({
                        modelPurchasingFixPriceConfigurationID: $scope.method.getNewID(),
                        season: $scope.season,
                        materialTypeID: item.materialTypeID,
                        uSDAmount: null,
                        materialTypeNM: item.materialTypeNM,
                        factoryID: $scope.factoryID
                    });
                });
            };

            //rebind modelPurchasingPriceConfiguration
            $scope.method.bindModelPurchasingPrice();
            $scope.method.bindPurchasingPrices();
            $scope.method.emptyPurchasingPrices()
        },
        bindPurchasingPrice: function (optionset, item) {
            angular.forEach($scope.data.modelPurchasingPriceConfigurationDTOs.filter(s => s.season === optionset.season
                && s.productElementID === optionset.productElementID
                && s.factoryID === $scope.factoryID), function (itemModelPurchasingPriceConfiguration) {

                    angular.forEach(itemModelPurchasingPriceConfiguration.modelPurchasingPriceConfigurationDetailDTOs.filter(
                        s => s.optionID === item.optionID), function (itemDetail) {
                            itemDetail.percentValue = item.purchasingPercentValue;
                            itemDetail.usdAmount = item.purchasingUSDAmount;
                        });
                });
        },
        confirmPrice: function () {
            //insert new
            if ($scope.data.modelPriceStatusDTOs.filter(s => s.season === $scope.season).length === 0) {
                $scope.data.modelPriceStatusDTOs.push({
                    season: $scope.season,
                    isConfirmed: true
                });
            }
            //update true
            else if ($scope.data.modelPriceStatusDTOs.filter(s => s.season === $scope.season && !s.isConfirmed).length > 0) {
                angular.forEach($scope.data.modelPriceStatusDTOs.filter(s => s.season === $scope.season && !s.isConfirmed), function (item) {
                    item.isConfirmed = true;
                });
            }
            //update false
            else {
                angular.forEach($scope.data.modelPriceStatusDTOs.filter(s => s.season === $scope.season && s.isConfirmed), function (item) {
                    item.isConfirmed = false;
                });
            }
        },
        confirmPurchasingPrice: function () {
            //insert new
            if ($scope.data.modelPurchasingPriceStatusDTOs.filter(s => s.season === $scope.season && s.factoryID === $scope.factoryID).length === 0) {
                if ($scope.data.modelPurchasingPriceStatusDTOs.filter(s => s.season === $scope.season && s.factoryID === $scope.factoryID && s.isConfirmed).length === 0) {
                    $scope.data.modelPurchasingPriceStatusDTOs.push({
                        factoryID: $scope.factoryID,
                        season: $scope.season,
                        isConfirmed: true
                    });
                }
            }
            //update true
            else if ($scope.data.modelPurchasingPriceStatusDTOs.filter(s => s.season === $scope.season && s.factoryID === $scope.factoryID && !s.isConfirmed).length > 0) {
                angular.forEach($scope.data.modelPurchasingPriceStatusDTOs.filter(s => s.season === $scope.season && s.factoryID === $scope.factoryID && !s.isConfirmed), function (item) {
                    item.isConfirmed = true;
                });
            }
            //update false
            else if ($scope.data.modelPurchasingPriceStatusDTOs.filter(s => s.season === $scope.season && s.factoryID === $scope.factoryID && s.isConfirmed).length > 0) {
                angular.forEach($scope.data.modelPurchasingPriceStatusDTOs.filter(s => s.season === $scope.season && s.factoryID === $scope.factoryID && s.isConfirmed), function (item) {
                    item.isConfirmed = false;
                });
            }
        },

        getClientSaleData: function () {
            jsonService.getClientData(
                $scope.clientFilter.season,
                $scope.clientFilter.clientID,
                context.id,
                function (data) {
                    $scope.clientData = data.data;
                    $scope.$apply();
                },
                function (error) {

                });
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

        //AI File
        addAIfile: function () {
            var newAIfile = {
                aiFileID: $scope.method.getNewID(),
                scanFile: null,
                remark: null,
                scanFileLocation: null,
                friendlyName: null,
                scanHasChange: false,
                scanNewFile: null,
                method: null
            };
            $scope.event.editAIfile(newAIfile)
        },
        editAIfile: function (item) {
            $scope.currenAIfile = JSON.parse(JSON.stringify(item));
            jQuery("#aiForm").modal();
        },
        updateaifile: function () {

            var index = $scope.method.getaifileIndex($scope.currenAIfile.aiFileID)
            if (index >= 0) {
                $scope.aifiles[index] = $scope.currenAIfile;
            }
            else {
                $scope.aifiles.push($scope.currenAIfile)
            }

            jQuery("#aiForm").modal('hide');
            $scope.method.renderaifile();
        },
        removeaifile: function (item) {
            if (confirm("Are you Sure Delete this Data ?")) {
                $scope.aifiles.splice($scope.aifiles.indexOf(item), 1)
            }
        },
        addaiScanFile: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.currenAIfile.scanHasChange = true;
                        scope.currenAIfile.scanFileLocation = img.fileURL;
                        scope.currenAIfile.friendlyName = img.filename;
                        scope.currenAIfile.scanNewFile = img.filename;
                    }, null);
                });
            };
            masterUploader.open();
        },

        editQCIssue: function ($event, id, mdlId) {
            $event.preventDefault();
            $scope.isEditData = true;

            if (id === null) {
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
            if (isFirstQCIssue === false) {
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
            masterUploader.imageOnly = false;
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
            masterUploader.imageOnly = false;
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
                if ($scope.editData.issueDateFormatted === null || $scope.editData.issueDateFormatted === "") {
                    jsHelper.showMessage("warning", "Please inut Issue Date !!");
                    return false;
                }
            }

            var isValid = $scope.method.checkIsValid();

            if ($scope.editData.typeID !== 6) {
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
                            optionNM: item.optionNM,
                            purchasingUSDAmount: parseFloat(item.purchasingUSDAmount),
                            PurchasingPercentValue: parseFloat(item.PurchasingPercentValue)
                        });
                    }, null);
                    $scope.data.modelPriceConfigurations.push({
                        modelPriceConfigurationID: $scope.method.getNewID(),
                        season: $scope.season,
                        updatedBy: null,
                        updatedDate: null,
                        employeeNM: '',
                        factoryID: $scope.factoryID,
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
                    materialTypeNM: selectedOption.text
                });

                // add Purchasing Fix Price          
                if ($scope.factoryID !==  null) {
                    $scope.data.modelPurchasingFixPriceConfigurationDTOs.push({
                        modelPurchasingFixPriceConfigurationID: $scope.method.getNewID(),
                        season: $scope.season,
                        materialTypeID: selectedOption.id,
                        uSDAmount: null,
                        materialTypeNM: selectedOption.text,
                        factoryID: $scope.factoryID
                    });
                };

                $('#fixPriceOption').val(null).trigger('change');

            }
        },
        removeFixPriceOption: function (item) {
            angular.forEach($scope.data.modelPurchasingFixPriceConfigurationDTOs, function (itemPurchasingFixPriceConfigurationDTO) {
                if (itemPurchasingFixPriceConfigurationDTO.season === item.season && itemPurchasingFixPriceConfigurationDTO.materialTypeID === item.materialTypeID) {
                    $scope.data.modelPurchasingFixPriceConfigurationDTOs.splice($scope.data.modelPurchasingFixPriceConfigurationDTOs.indexOf(itemPurchasingFixPriceConfigurationDTO), 1);
                };
            });

            $scope.data.modelFixPriceConfigurations.splice($scope.data.modelFixPriceConfigurations.indexOf(item), 1);
        },
        addOptionConfigure: function (optionSet) {
            var selectedOption = $('#productElementID-' + optionSet.productElementID).select2('data');
            var isExist = false;
            angular.forEach(optionSet.modelPriceConfigurationDetails, function (value, key) {
                if (parseInt(value.optionID) === parseInt(selectedOption.id)) {
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
                    purchasingPercentValue: null,
                    purchasingUSDAmount: null,
                });

                $scope.method.bindModelPurchasingPrice();


                $('#productElementID-' + optionSet.productElementID).val(null).trigger('change');
            }
            else {
                alert('Option already exists!');
            }
        },
        removeOptionConfigure: function (optionset, item) {
            angular.forEach($scope.data.modelPurchasingPriceConfigurationDTOs.filter(s => s.productElementID === optionset.productElementID
                && s.season === optionset.season), function (itemModelPurchasingPriceConfigurationDTO) {

                    angular.forEach(itemModelPurchasingPriceConfigurationDTO.modelPurchasingPriceConfigurationDetailDTOs.filter(
                        s => s.optionID === item.optionID), function (detail) {
                            itemModelPurchasingPriceConfigurationDTO.modelPurchasingPriceConfigurationDetailDTOs.splice(
                                itemModelPurchasingPriceConfigurationDTO.modelPurchasingPriceConfigurationDetailDTOs.indexOf(detail), 1);
                        });
                });

            optionset.modelPriceConfigurationDetails.splice(optionset.modelPriceConfigurationDetails.indexOf(item), 1);
        },


        //
        // seft calculation price
        //
        calcPrice: function (optionSet) {
            jsonService.selfCalculationPrice(
                context.id,
                $scope.season,
                optionSet.modelPriceConfigurationDetails,
                function (data) {
                   
                    if (data.message.type === 0) {
                        
                        angular.forEach(data.data, function (item) {
                           
                            angular.forEach(optionSet.modelPriceConfigurationDetails, function (modelPriceConfigurationDetail) {
                                //alert("item.optionID: " + item.optionID);
                                //alert("modelPriceConfigurationDetail.optionID: " + modelPriceConfigurationDetail.optionID);
                                //alert("item.purchasingUSDAmount: " + item.purchasingUSDAmount);

                                if (item.optionID === modelPriceConfigurationDetail.optionID && item.purchasingUSDAmount > 0) {
                                    //alert("Ok: " + item.purchasingUSDAmount);
                                    modelPriceConfigurationDetail.purchasingUSDAmount = item.purchasingUSDAmount;                                  
                                }
                            });
                        });
                        $scope.$apply();
                        //jsHelper.processMessage(data.message);
                        //optionSet.modelPriceConfigurationDetails = data.data;
                  
                        //return true;
                    }

                    else {
                     
                        jsHelper.processMessage(data.message);
                        window.location = context.refreshUrl + $scope.data.modelID;
                    }
                },
                function () {
                });
        },

        // Customize add image
        downloadFile: function () {

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
                for (var i = 0; i < $scope.data.modelIssueTracks.length; i++) {
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

                case '#tabProductBreakDownCategory':
                    if ($scope.loadingState.productBreakDown) return;
                    jsonService.getProductBreakDownData(
                        context.id,
                        function (data) {
                            $scope.productBreakDowns = data.data.data.productBreakDowns;
                            $scope.loadingState.productBreakDown = true;

                            $scope.$apply();
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                            $scope.$apply();
                        }
                    );
                    break;
                //1234
                case '#tabDefaultOption':

                    if ($scope.loadingState.modelDefaultOption) return;

                    seasonDefaultOption = jQuery('#seasonDefaultOption').val();

                    if (seasonDefaultOption === undefined) {
                        seasonDefaultOption = $scope.season;

                    } else
                        seasonDefaultOption = seasonDefaultOption.substring(7, seasonDefaultOption.length);

                    jsonService.getModelDefaultOptionBySeason(
                        $scope.data.modelID,
                        seasonDefaultOption,
                        function (data) {
                            $scope.data.modelDefaultOptionDTOs = data.data;
                            $scope.$apply();
                            $scope.loadingState.modelDefaultOption = true;
                        },
                        function (error) {

                        });

                    break;
                case '#tabClient':
                    if ($scope.loadingState.client) return;
                    jsonService.getClientData(
                        $scope.clientFilter.season,
                        $scope.clientFilter.clientID,
                        context.id,
                        function (data) {
                            $scope.clientData = data.data;
                            $scope.$apply();
                            $scope.loadingState.client = true;
                        },
                        function (error) {

                        });
            }
        },
        clearClientSpecialPackagingMethodID: function (item) {
            if (item.packagingMethodID !== 11) {
                item.clientSpecialPackagingMethodID = null;
            }
        },

        // Get default option by season 
        getDefaultOptionBySeason: function (id, seasonDefaultOption) {


            seasonDefaultOption = jQuery('#seasonDefaultOption').val();
            seasonDefaultOption = seasonDefaultOption.substring(7, seasonDefaultOption.length);
            jsonService.getModelDefaultOptionBySeason(
                id,
                seasonDefaultOption,
                function (data) {
                    $scope.data.modelDefaultOptionDTOs = [];

                    angular.forEach(data.data, function (item) {
                        if (item !== null) {
                            $scope.data.modelDefaultOptionDTOs.push(item);

                        }
                    });

                    $scope.$apply();
                },
                function (error) {

                });
        },

        // Get default option by previous season
        getDefaultOptionByPreviousSeason: function (id, seasonDefaultOption) {

            seasonDefaultOption = jQuery('#seasonDefaultOption').val();
            seasonDefaultOption = seasonDefaultOption.substring(7, seasonDefaultOption.length);
            jsonService.getModelDefaultOptionByPreviousSeason(
                id,
                seasonDefaultOption,
                function (data) {
                    $scope.data.modelDefaultOptionDTOs = [];

                    if (data !==  null) {
                        if (data.data.length === 0) {
                            $scope.defaultOptionForm.showEditProductWinzard(id, seasonDefaultOption);
                        }

                        else {
                            angular.forEach(data.data, function (item) {
                                if (item !== null) {

                                    $scope.data.modelDefaultOptionDTOs.push(item);

                                    angular.forEach($scope.data.modelDefaultOptionDTOs, function (item) {
                                        item.modelDefaultOptionID = 0;
                                        item.season = seasonDefaultOption;
                                        item.isConfirmed = false;
                                    });

                                    $scope.$apply();
                                }
                            });
                        }
                    }
                },
                function (error) {

                });
        },

        loadDefaultOptions: function (modelID, seasonDefaultOption) {
            seasonDefaultOption = jQuery('#seasonDefaultOption').val();
            if (seasonDefaultOption === undefined) {
                seasonDefaultOption = $scope.season;

            } else
                seasonDefaultOption = seasonDefaultOption.substring(7, seasonDefaultOption.length);

            jsonService.getModelDefaultOptionBySeason(
                modelID,
                seasonDefaultOption,
                function (data) {
                    $scope.data.modelDefaultOptionDTOs = data.data;
                    $scope.$apply();
                },
                function (error) {

                });
        },

        approve: function approve(id, packagingMethodID) {
           
            if ((id === undefined) || (id === 0)) {
                bootbox.alert("Please save data before confirmed !!!");
                return false;
            }

            angular.forEach($scope.data.modelDefaultOptionDTOs, function (item) {
                if (item.packagingMethodID === null) {
                    bootbox.alert("Packaging Method can not null !!!");
                    return false;
                } else {
                    jsonService.approveData(
                        id,
                        packagingMethodID,
                        $scope.data,
                        function (data) {
                            jsHelper.processMessage(data.message);
                            window.location = context.refreshUrl + $scope.data.modelID;
                        },
                        function () {
                        });
                }
            });
        },

        reset: function reset(id) {

            jsonService.resetData(
                id,
                $scope.data,
                function (data) {

                    if (data.message.type === 2) {
                        jsHelper.processMessage(data.message);
                        return false;
                    }

                    else {
                        jsHelper.processMessage(data.message);
                        window.location = context.refreshUrl + $scope.data.modelID;
                    }
                },
                function () {
                });
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
        pageSetup: function () {
            $timeout(function () { pageSetUp(); }, 500);
        },
        getNewID: function () {
            $scope.newid--;
            return $scope.newid;
        },
        getImageIndex: function (id) {
            var isExist = false;
            var index = 0;
            for (index = 0; index < $scope.imageGalleries.length; index++) {
                if ($scope.imageGalleries[index].imageGalleryID === id) {
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
            for (index = 0; index < $scope.data.testReports.length; index++) {
                if ($scope.data.testReports[index].testReportID === id) {
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
            for (index = 0; index < $scope.data.tdfiles.length; index++) {
                if ($scope.data.tdfiles[index].tdFileID === id) {
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

        //aifile
        getaifileIndex: function (id) {
            var isExist = false;
            var index = 0;
            for (index = 0; index < $scope.aifiles.length; index++) {
                if ($scope.aifiles[index].aiFileID === id) {
                    isExist = true;
                    break;
                }
            }
            if (!isExist) {
                return -1;
            } else {
                return index;
            }
        },
        renderaifile: function () {

            $scope.aifile = [];
            angular.forEach($scope.aifiles, function (value, key) {
                if ($scope.aifile.indexOf(value.aifile) < 0) {
                    $scope.aifile.push(value.aifile);
                }
            }, null);
        },

        checkIsValid: function () {
            var scope = $scope.editData;

            if (scope.description === null)
                return false;

            if (scope.statusBy === 0)
                return false;

            //if (scope.followUp === 0)
            //    return false;

            //if (scope.modelIssueTrackImagesError === null || scope.modelIssueTrackImagesError.length === 0)
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
        },
        factoryChange: function (factory) {
            $scope.data.defaultFactoryID = factory.id;
            $scope.data.defaultFactoryUD = factory.label;
        },
        getFactoryUD: function (factoryID) {
            for (var i = 0; i < $scope.factoryDTOs.length; i++) {
                var factoryItem = $scope.factoryDTOs[i];
                if (factoryItem.id === factoryID) {
                    return factoryItem.label;
                }
            }
        },
        checkFactoryInModelFactories: function (factoryID) {
            var isChecked = false;

            angular.forEach($scope.data.modelDefaultFactories, function (item) {
                if (item.factoryID === factoryID) {
                    isChecked = true;
                }
            });

            return isChecked;
        },
        bindModelPurchasingPrice: function () {
            //rebind modelPurchasingPriceConfiguration
            angular.forEach($scope.data.modelDefaultFactories, function (itemDefaultFactory) {
                angular.forEach($scope.data.modelPriceConfigurations.filter(s => s.season === $scope.season), function (item) {

                    if ($scope.data.modelPurchasingPriceConfigurationDTOs.filter(s => s.factoryID === itemDefaultFactory.factoryID
                        && s.season === $scope.season
                        && s.productElementID === item.productElementID).length === 0) {

                        $scope.data.modelPurchasingPriceConfigurationDTOs.push({
                            factoryID: itemDefaultFactory.factoryID,
                            season: $scope.season,
                            productElementID: item.productElementID,
                            modelPurchasingPriceConfigurationDetailDTOs: []
                        });
                    };

                    angular.forEach($scope.data.modelPurchasingPriceConfigurationDTOs.filter(s => s.factoryID === itemDefaultFactory.factoryID
                        && s.season === $scope.season), function (itemPurchasingPriceConfiguration) {

                            if (itemPurchasingPriceConfiguration.productElementID === item.productElementID) {

                                angular.forEach(item.modelPriceConfigurationDetails, function (itemPriceConfigurationDetail) {

                                    if (itemPurchasingPriceConfiguration.modelPurchasingPriceConfigurationDetailDTOs.filter(
                                        s => s.optionID === itemPriceConfigurationDetail.optionID).length === 0) {
                                        itemPurchasingPriceConfiguration.modelPurchasingPriceConfigurationDetailDTOs.push({
                                            modelPurchasingPriceConfigurationID: itemPurchasingPriceConfiguration.modelPurchasingPriceConfigurationID,
                                            optionID: itemPriceConfigurationDetail.optionID,
                                            //percentValue: itemPriceConfigurationDetail.purchasingPercentValue,
                                            //usdAmount: itemPriceConfigurationDetail.purchasingUsdAmount
                                            percentValue: null,
                                            usdAmount: null
                                        });
                                    }
                                });
                            }
                        });
                });
            });
        },
        bindPurchasingPrices: function () {
            //rebind purchasing price into girdview
            if ($scope.factoryID !==  null) {
                angular.forEach($scope.data.modelPurchasingPriceConfigurationDTOs.filter(s => s.factoryID === $scope.factoryID
                    && s.season === $scope.season), function (itemPurchasingPrice) {
                        angular.forEach(itemPurchasingPrice.modelPurchasingPriceConfigurationDetailDTOs, function (itemPurchasingPriceDetail) {

                            angular.forEach($scope.data.modelPriceConfigurations.filter(s => s.season === $scope.season
                                && s.productElementID === itemPurchasingPrice.productElementID), function (itemPrice) {

                                    angular.forEach(itemPrice.modelPriceConfigurationDetails, function (itemPriceDetail) {

                                        if (itemPriceDetail.optionID === itemPurchasingPriceDetail.optionID) {
                                            itemPriceDetail.purchasingPercentValue = itemPurchasingPriceDetail.percentValue;
                                            itemPriceDetail.purchasingUSDAmount = itemPurchasingPriceDetail.usdAmount;
                                        };

                                    });
                                });
                        });
                    });
            };
        },
        emptyPurchasingPrices: function () {
            //remove Price when combox factory is null
            if ($scope.factoryID === null) {
                angular.forEach($scope.data.modelPriceConfigurations, function (itemPriceConfiguration) {
                    angular.forEach(itemPriceConfiguration.modelPriceConfigurationDetails, function (itemDetail) {

                        itemDetail.purchasingPercentValue = null;
                        itemDetail.purchasingUSDAmount = null;
                    });

                });
            }
        },
        checkConfirmPrice: function () {
            if ($scope.data.modelPriceStatusDTOs.filter(s => s.season === $scope.season && s.isConfirmed).length > 0) {
                return true;
            }
            return false;
        },
        checkConfirmPurchasingPrice: function () {
            if ($scope.data.modelPurchasingPriceStatusDTOs.filter(s => s.season === $scope.season && s.factoryID === $scope.factoryID && s.isConfirmed).length > 0) {
                return true;
            }
            return false;
        },
        getDefaultFactory: function () {
            var factoryID = null;
            angular.forEach($scope.data.modelDefaultFactories.filter(s => s.isDefault), function (item) {
                factoryID = item.factoryID
            });
            return factoryID;
        }
    };
    //AutoComplete------------------------------------------------
    $scope.generalCtrlMethod = {
        getnewID: function () {
            $scope.getID--;
            return $scope.getID;
        },
        autocompleteSupplier: function () {
            $('#supplierAutoComplete').autocomplete({
                source: function (request, response) {
                    jQuery.ajax({
                        url: jsonService.serviceUrl + 'query-check?param=' + request.term,
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json',
                            'Authorization': 'Bearer ' + jsonService.token
                        },
                        dataType: "json",
                        type: "POST",
                        success: function (data) {
                            response($.map(data, function (value, key) {
                                return {
                                    id: value.checkListGroupID,
                                    label: value.checkListGroupNM,
                                    value: value.checkListGroupNM,
                                    description: 'Code: ' + value.checkListGroupUD,
                                    code: value.checkListGroupUD
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
                        $scope.data.checkListGroupID = ui.item.id;
                        $scope.data.checkListGroupNM = ui.item.label;
                        $scope.data.checkListGroupUD = ui.item.code;
                    });
                },
                change: function (event, ui) {
                    if (!ui.item) {
                        scope.$apply(function () {
                            $scope.data.checkListGroupID = null;
                            $scope.data.checkListGroupNM = '';
                            $scope.data.checkListGroupUD = '';
                        });
                        $('#supplierAutoComplete').val('');
                    }
                }
            }).data("ui-autocomplete")._renderItem = function (ul, item) {
                return jQuery("<li>")
                    .data('item.autocomplete', item)
                    .append('<a href="javascript:void(0)"><strong style="text-decoration: underline; text-transform: uppercase;">' + item.label + '</strong><br>' + item.description + '</a>')
                    .appendTo(ul);
            };
        },
        autocompleteSupplierDetail: function () {
            angular.forEach($scope.data.modelCheckListGroupDTO, function (item) {
                $("#supplierAutoCompleteOther" + item.modelCheckListGroupID).autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            cache: false,
                            headers: {
                                'Accept': 'application/json',
                                'Content-Type': 'application/json',
                                'Authorization': 'Bearer ' + jsonService.token
                            },
                            type: "POST",
                            dataType: 'JSON',
                            url: jsonService.serviceUrl + 'query-check?param=' + request.term,
                            success: function (data) {
                                response($.map(data, function (value, key) {
                                    return {
                                        id: value.checkListGroupID,
                                        label: value.checkListGroupNM,
                                        value: value.checkListGroupUD,
                                        description: 'code: ' + value.checkListGroupUD
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
                            item.checkListGroupID = ui.item.id;
                            item.checkListGroupNM = ui.item.label;
                            item.checkListGroupUD = ui.item.value;
                        });
                    },
                    change: function (event, ui) {
                        if (!ui.item) {
                            scope.$apply(function () {
                                item.checkListGroupID = null;
                                item.checkListGroupNM = '';
                                item.checkListGroupUD = '';
                            });
                            $('#supplierAutoCompleteOther').val('');
                        }
                    }

                }).data("ui-autocomplete")._renderItem = function (ul, item) {
                    return jQuery("<li>")
                        .data('item.autocomplete', item)
                        .append('<a href="javascript:void(0)"><strong style="text-decoration: underline; text-transform: uppercase;">' + item.label + '</strong><br>' + item.description + '</a>')
                        .appendTo(ul);
                };
            });

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
                        if (data.message.type === 0) {
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
                if (e.keyCode === 13) {
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
                            if (value.pieceModelID === this.id) {
                                this.isOK = false;
                            }
                        }, param);

                        if (param.isOK) {
                            $scope.data.pieces.push({
                                modelPieceID: $scope.method.getNewID(),
                                pieceModelID: this.modelID,
                                modelUD: this.modelUD,
                                rowIndex: this.rowIndex,
                                modelNM: this.modelNM,
                                productTypeNM: this.productTypeNM,
                                quantity: 1,
                                overallDimL: this.overallDimL,
                                overallDimH: this.overallDimH,
                                overallDimW: this.overallDimW
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
            },
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
                        if (data.message.type === 0) {
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
                if (e.keyCode === 13) {
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
                            if (value.subMaterialOptionID === this.id) {
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

    // Setup model default option
    $scope.defaultOptionForm = {
        showEditProductWinzard: function (modelID, seasonDefaultOption) {

            seasonDefaultOption = jQuery('#seasonDefaultOption').val();
            seasonDefaultOption = seasonDefaultOption.substring(7, seasonDefaultOption.length);
            var productData = {};
            if ($scope.data.modelDefaultOptionDTOs.length === 0) {
                productWizard.open({

                    articleCode: '',
                    description: '',
                    modelID: modelID,
                    materialID: null,
                    materialTypeID: null,
                    materialColorID: null,
                    frameMaterialID: null,
                    frameMaterialColorID: null,
                    subMaterialID: null,
                    subMaterialColorID: null,
                    backCushionID: null,
                    seatCushionID: null,
                    cushionColorID: null,
                    fSCTypeID: null,
                    fSCPercentID: null,
                    useFSCLabel: '',
                    manufacturerCountryID: null,
                    packagingMethodID: null,
                    materialText: '',
                    frameText: '',
                    subMaterialText: '',
                    cushionText: '',
                    fscText: '',
                    packagingText: 'NONE',
                    cushionDescription: null,
                    displayCushionDescription: false,
                    displayPackagingMethod: true,
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
                    season: seasonDefaultOption,

                    currency: '',
                    discountPercent: null,
                    discountAmount: null,
                    salePriceCalculated: null,
                    increasePercent: null,

                    onFinish: function (data) {

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

                        //text
                        productData.materialText = data.materialText;

                        if (data.frameText === "") {
                            data.frameText = 'NONE';
                        }
                        else {
                            productData.frameText = data.frameText;
                        }

                        productData.subMaterialText = data.subMaterialText;
                        productData.cushionText = data.cushionText;
                        productData.fscText = data.fscText;

                        if (data.fscText === "") {
                            data.fscText = 'NONE';
                        }
                        else {
                            productData.fscText = data.fscText;
                        }
                        //image
                        productData.materialThumbnailLocation = data.materialImage;
                        productData.frameMaterialThumbnailLocation = data.frameImage;
                        productData.subMaterialColorThumbnailLocation = data.subMaterialImage;
                        productData.backCushionThumbnailLocation = data.backCushionImage;
                        productData.seatCushionThumbnailLocation = data.seatCushionImage;
                        productData.cushionColorThumbnailLocation = data.cushionColorImage;
                        productData.increasePercent = data.increasePercent;

                        if (data !==  null) {
                            $scope.data.modelDefaultOptionDTOs.push({
                                modelDefaultOptionID: data.modelDefaultOptionID,
                                articleCode: data.articleCode,
                                backCushionID: data.backCushionID,
                                backCushionThumbnailLocation: data.backCushionImage,
                                clientSpecialPackagingMethod: data.clientSpecialPackagingMethod,
                                confirmedBy: data.confirmedBy,
                                confirmedDate: data.confirmedDate,
                                cushionColorID: data.cushionColorID,
                                cushionColorThumbnailLocation: data.cushionColorImage,
                                cushionText: data.cushionText,
                                description: data.description,
                                frameMaterialColorID: data.frameMaterialColorID,
                                frameMaterialID: data.frameMaterialID,
                                frameMaterialThumbnailLocation: data.frameImage,
                                frameText: data.frameText,
                                fscPercentID: data.fscPercentID,
                                fscText: data.fscText,
                                fscTypeID: data.fscTypeID,
                                isConfirmed: data.isConfirmed,
                                materialColorID: data.materialColorID,
                                materialID: data.materialID,
                                materialText: data.materialText,
                                materialThumbnailLocation: data.materialImage,
                                materialTypeID: data.materialTypeID,
                                modelID: data.modelID,
                                packagingMethodID: data.packagingMethodID,
                                remark: data.remark,
                                season: seasonDefaultOption,
                                seatCushionID: data.seatCushionID,
                                seatCushionThumbnailLocation: data.seatCushionImage,
                                subMaterialColorID: data.subMaterialColorID,
                                subMaterialColorThumbnailLocation: data.subMaterialImage,
                                subMaterialID: data.subMaterialID,
                                subMaterialText: data.subMaterialText,
                                updatedBy: data.updatedBy,
                                updatedDate: data.updatedDate,
                                packagingText: data.packagingText
                            });
                        }
                    }
                });
            }
            else {
                angular.forEach($scope.data.modelDefaultOptionDTOs, function (item) {
                    angular.copy(item, productData);
                    productWizard.open({
                        modelDefaultOptionID: productData.modelDefaultOptionID,
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
                        packagingText: 'NONE',
                        cushionDescription: productData.cushionRemark,
                        displayCushionDescription: false,
                        displayPackagingMethod: true,
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
                        season: seasonDefaultOption,

                        currency: '',
                        discountPercent: null,
                        discountAmount: null,
                        salePriceCalculated: null,
                        increasePercent: null,

                        onFinish: function (data) {

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

                            //text
                            productData.materialText = data.materialText;
                            if (data.frameText === "") {
                                data.frameText = 'NONE';
                            }
                            else {
                                productData.frameText = data.frameText;
                            }
                            productData.subMaterialText = data.subMaterialText;
                            productData.cushionText = data.cushionText;

                            if (data.fscText === "") {
                                data.fscText = 'NONE';
                            }
                            else {
                                productData.fscText = data.fscText;
                            }

                            //image
                            productData.materialThumbnailLocation = data.materialImage;
                            productData.frameMaterialThumbnailLocation = data.frameImage;
                            productData.subMaterialColorThumbnailLocation = data.subMaterialImage;
                            productData.backCushionThumbnailLocation = data.backCushionImage;
                            productData.seatCushionThumbnailLocation = data.seatCushionImage;
                            productData.cushionColorThumbnailLocation = data.cushionColorImage;
                            productData.increasePercent = data.increasePercent;
                            productData.packagingText = data.packagingMethodText;

                            if (data !==  null) {
                                if ($scope.data.modelDefaultOptionDTOs.length > 0) {
                                    for (i = 0; i < $scope.data.modelDefaultOptionDTOs.length; i++) {
                                        var item = $scope.data.modelDefaultOptionDTOs[i];
                                        if (item.season === seasonDefaultOption) {

                                            item.modelDefaultOptionID = data.modelDefaultOptionID,
                                                item.articleCode = data.articleCode,
                                                item.backCushionID = data.backCushionID,
                                                item.backCushionThumbnailLocation = data.backCushionImage,
                                                item.clientSpecialPackagingMethod = data.clientSpecialPackagingMethod,
                                                item.confirmedBy = data.confirmedBy,
                                                item.confirmedDate = data.confirmedDate,
                                                item.cushionColorID = data.cushionColorID,
                                                item.cushionColorThumbnailLocation = data.cushionColorImage,
                                                item.cushionText = data.cushionText,
                                                item.description = data.description,
                                                item.frameMaterialColorID = data.frameMaterialColorID,
                                                item.frameMaterialID = data.frameMaterialID,
                                                item.frameMaterialThumbnailLocation = data.frameImage,
                                                item.frameText = data.frameText,
                                                item.fscPercentID = data.fscPercentID,
                                                item.fscText = data.fscText,
                                                item.fscTypeID = data.fscTypeID,
                                                item.isConfirmed = data.isConfirmed,
                                                item.materialColorID = data.materialColorID,
                                                item.materialID = data.materialID,
                                                item.materialText = data.materialText,
                                                item.materialThumbnailLocation = data.materialImage,
                                                item.materialTypeID = data.materialTypeID,
                                                item.modelDefaultOptionID = data.modelDefaultOptionID,
                                                item.modelID = data.modelID,
                                                item.packagingMethodID = data.packagingMethodID,
                                                item.remark = data.remark,
                                                item.season = seasonDefaultOption,
                                                item.seatCushionID = data.seatCushionID,
                                                item.seatCushionThumbnailLocation = data.seatCushionImage,
                                                item.subMaterialColorID = data.subMaterialColorID,
                                                item.subMaterialColorThumbnailLocation = data.subMaterialImage,
                                                item.subMaterialID = data.subMaterialID,
                                                item.subMaterialText = data.subMaterialText,
                                                item.updatedBy = data.updatedBy,
                                                item.updatedDate = data.updatedDate,
                                                item.packagingText = data.packagingMethodText
                                        }
                                    }
                                }
                            }
                        }
                    });
                });
            }
        },
    }

    $scope.event.init();
}]);