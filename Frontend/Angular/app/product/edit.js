//
// SUPPORT
//
jQuery('#main-form').keypress(function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['customFilters', 'ui', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.manufacturerCountries = null;
    $scope.packagingMethodOptions = null;
    $scope.seasons = null;
    $scope.evenTrigger = false;
    $scope.packages = null;
    $scope.newID = -1;

    $scope.typeToGenerateEANCodes = [{ id: 1, name: 'Auto' }, { id: 2, name: 'Manual' }];
    $scope.isManualEANCode = true;
    $scope.eanCodeManual = '';
    $scope.productECommerceSpec = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                context.modelid,
                function (data) {
                    $scope.seasons = data.data.seasons;
                    $scope.packagingMethodOptions = data.data.modelPackagingMethodOption2DTOs;
                    $scope.packages = data.data.packages;
                    $scope.data = data.data.data;
                    if ($scope.data.productECommerceSpecs !== null && $scope.data.productECommerceSpecs.length > 0) {
                        $scope.productECommerceSpec = $scope.data.productECommerceSpecs[0];
                    }
                    $scope.materials = data.data.materials;
                    $scope.frameMaterials = data.data.frameMaterials;
                    $scope.materialTypes = data.data.materialTypes;
                    $scope.fscTypes = data.data.fscTypes;
                    $scope.materialColors = data.data.materialColors;
                    $scope.frameMaterialColors = data.data.frameMaterialColors;
                    $scope.cushionColors = data.data.cushionColors;
                    $scope.cushionTypes = data.data.cushionTypes;

                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                    $scope.evenTrigger = true;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.seasons = null;
                    $scope.packagingMethodOptions = null;
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
                            $scope.method.refresh(data.data.productID);
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
                            window.location = context.backUrl;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },
        approve: function ($event) {
            $event.preventDefault();
            if (confirm('Approve the current product ?')) {
                jsonService.approve(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.productID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        reset: function ($event) {
            $event.preventDefault();
            if (confirm('Reset the current product to pending status?')) {
                jsonService.reset(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.productID);
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
        openProductWizard: function () {
            productWizard.fscLabelVisible = false;
            productWizard.open({
                articleCode: scope.data.articleCode,
                description: scope.data.description,
                modelID: scope.data.modelID,
                materialID: scope.data.materialID,
                materialTypeID: scope.data.materialTypeID,
                materialColorID: scope.data.materialColorID,
                frameMaterialID: scope.data.frameMaterialID,
                frameMaterialColorID: scope.data.frameMaterialColorID,
                subMaterialID: scope.data.subMaterialID,
                subMaterialColorID: scope.data.subMaterialColorID,
                cushionID: scope.data.cushionID,
                cushionColorID: scope.data.cushionColorID,
                backCushionID: scope.data.backCushionID,
                seatCushionID: scope.data.seatCushionID,
                fSCTypeID: scope.data.fscTypeID,
                fSCPercentID: scope.data.fscPercentID,
                useFSCLabel: false,
                manufacturerCountryID: scope.data.manufacturerCountryID,
                packagingMethodID: null,
                materialText: '',
                frameText: '',
                subMaterialText: '',
                cushionText: '',
                packagingMethodText: '',
                cushionDescription: '',
                fscText: '',
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
                onFinish: function (data) {
                    scope.data.articleCode = data.articleCode;
                    scope.data.description = data.description;
                    scope.data.materialID = data.materialID;
                    scope.data.materialTypeID = data.materialTypeID;
                    scope.data.materialColorID = data.materialColorID;
                    scope.data.frameMaterialID = data.frameMaterialID;
                    scope.data.frameMaterialColorID = data.frameMaterialColorID;
                    scope.data.subMaterialID = data.subMaterialID;
                    scope.data.subMaterialColorID = data.subMaterialColorID;
                    scope.data.cushionID = data.cushionID;
                    scope.data.cushionColorID = data.cushionColorID;
                    scope.data.backCushionID = data.backCushionID;
                    scope.data.seatCushionID = data.seatCushionID;
                    scope.data.fscTypeID = data.fSCTypeID;
                    scope.data.fscPercentID = data.fSCPercentID;
                    scope.data.manufacturerCountryID = data.manufacturerCountryID;

                    scope.data.materialText = data.materialText;
                    scope.data.frameText = data.frameText;
                    scope.data.subMaterialText = data.subMaterialText;
                    scope.data.cushionText = data.cushionText;
                    scope.data.fscText = data.fscText;
                }
            });
        },
        addPackagingMethod: function () {
            $scope.data.loadAbilities.push({
                productLoadAbilityID: $scope.method.getNewID(),
                packagingMethodID: '',
                qnt20DC: '',
                qnt40DC: '',
                qnt40HC: '',
                comment: '',
                grossWeight: '',
                qntInBox: '',
                isConfirmed: '',
                updatorName: '',
                updatedDate: '',
                confirmerName: '',
                confirmedDate: ''
            });
        },
        removePackagingMethod: function (id) {
            isExist = false;
            for (j = 0; j < $scope.data.loadAbilities.length; j++) {
                if ($scope.data.loadAbilities[j].productLoadAbilityID == id) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.data.loadAbilities.splice(j, 1);
            }
        },
        
        //
        //package: OLD EAN CODE FEATURE
        //

        //addPackage: function () {
        //    $scope.data.productPackages.push({
        //        productPackageID: $scope.method.getNewID()
        //    });                
        //},

        //removePackage: function (id) {
        //    isExist = false;
        //    for (j = 0; j < $scope.data.productPackages.length; j++) {
        //        if ($scope.data.productPackages[j].productPackageID == id) {
        //            isExist = true;
        //            break;
        //        }
        //    }
        //    if (isExist) {
        //        $scope.data.productPackages.splice(j, 1);
        //    }
        //},

        //generatePreparingDataPieceEANCode: function () {
        //    jsonService.update(
        //        context.id,
        //        $scope.data,
        //        function (data) {
        //            if (data.message.type == 0) {
        //                jsonService.generatePreparingDataPieceEANCode(
        //                    context.id,
        //                    function (data) {
        //                        jsHelper.processMessage(data.message);
        //                        if (data.message.type == 0) {
        //                            $scope.method.refresh(context.id);
        //                        }
        //                    },
        //                    function (error) {
        //                        jsHelper.showMessage('warning', error);
        //                    }
        //                );
        //            }
        //            else {
        //                jsHelper.processMessage(data.message);
        //            }
        //        },
        //        function (error) {
        //            jsHelper.showMessage('warning', error);
        //        }
        //    );            
        //},

        //generateEANCode: function () {

        //    jsonService.update(
        //        context.id,
        //        $scope.data,
        //        function (data) {
        //            if (data.message.type == 0) {
                        
        //                jsonService.generateEANCode(
        //                    context.id,
        //                    function (data) {
        //                        jsHelper.processMessage(data.message);
        //                        if (data.message.type == 0) {
        //                            $scope.method.refresh(context.id);
        //                        }
        //                    },
        //                    function (error) {
        //                        jsHelper.showMessage('warning', error);
        //                    }
        //                );
        //            }
        //            else {
        //                jsHelper.processMessage(data.message);
        //            }
        //        },
        //        function (error) {
        //            jsHelper.showMessage('warning', error);
        //        }
        //    );            
        //},

        //generateEANCodeForSET: function () {
        //    jsonService.generateEANCodeForSET(
        //        context.id,
        //        function (data) {
        //            jsHelper.processMessage(data.message);
        //            if (data.message.type == 0) {
        //                $scope.method.refresh(context.id);
        //            }
        //        },
        //        function (error) {
        //            jsHelper.showMessage('warning', error);
        //        }
        //    );
        //},

        //resetEANCodeForSET: function () {
        //    jsonService.resetEANCodeForSET(
        //        context.id,
        //        function (data) {
        //            jsHelper.processMessage(data.message);
        //            if (data.message.type == 0) {
        //                $scope.method.refresh(context.id);
        //            }
        //        },
        //        function (error) {
        //            jsHelper.showMessage('warning', error);
        //        }
        //    );
        //},

        //generatePieceCode: function () {
        //    jsonService.update(
        //        context.id,
        //        $scope.data,
        //        function (data) {
        //            if (data.message.type == 0) {
        //                jsonService.generatePieceCode(
        //                    context.id,
        //                    function (data) {
        //                        jsHelper.processMessage(data.message);
        //                        if (data.message.type == 0) {
        //                            $scope.method.refresh(context.id);
        //                        }
        //                    },
        //                    function (error) {
        //                        jsHelper.showMessage('warning', error);
        //                    }
        //                );
        //            }
        //            else {
        //                jsHelper.processMessage(data.message);
        //            }
        //        },
        //        function (error) {
        //            jsHelper.showMessage('warning', error);
        //        }
        //    );            
        //},


        //
        //EAN CODE NEW FEATURE
        //
        
        //ean code new feature
        createSetEanCode_Auto: function (productID) {
            jsonService.createSetEanCode(
                productID,'',
                function (data) {                    
                    if (data.message.type == 0) {
                        if ($scope.data.productSetEANCodes == null) $scope.data.productSetEANCodes = [];
                        $scope.data.productSetEANCodes.push({
                            productSetEANCodeID: data.data.productSetEANCodeID,
                            productID: context.id,
                            eanCode: data.data.eanCode,
                            eanCodeIndex: data.data.eanCodeIndex,
                        });
                        $scope.$apply();
                    }
                    else {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        createSetEanCode_Manual: function (productID,eanCode) {
            if (eanCode == '')
            {
                bootbox.alert({size:'small', message : 'Please enter ean code'});
                return;
            }
            else if (!$scope.editForm.$valid) {
                bootbox.alert({ size: 'small', message: 'Invalid ean code' });
                return;
            }
            jsonService.createSetEanCode(
                productID, eanCode,
                function (data) {
                    if (data.message.type == 0) {
                        if ($scope.data.productSetEANCodes == null) $scope.data.productSetEANCodes = [];
                        $scope.data.productSetEANCodes.push({
                            productSetEANCodeID: data.data.productSetEANCodeID,
                            productID: context.id,
                            eanCode: data.data.eanCode,
                            eanCodeIndex: data.data.eanCodeIndex,                            
                        });
                        //reset ean manual ean code
                        $scope.eanCodeManual = '';
                        $scope.$apply();
                    }
                    else {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        createColli: function (productSetEANCodeItem) {
            jsonService.createColli(
                productSetEANCodeItem.productSetEANCodeID,
                function (data) {
                    if (data.message.type == 0) {
                        if (productSetEANCodeItem.productCollis == null) productSetEANCodeItem.productCollis = [];
                        productSetEANCodeItem.productCollis.push({
                            productColliID: data.data.productColliID,
                            productSetEANCodeID: productSetEANCodeItem.productSetEANCodeID,
                            eanCode: data.data.eanCode,
                            colliIndex: data.data.colliIndex,
                        });
                        $scope.$apply();
                    }
                    else {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        createColliPiece: function (colliItem) {
            jsonService.createColliPiece(
                colliItem.productColliID,
                function (data) {
                    if (data.message.type == 0) {
                        if (colliItem.productColliPieces == null) colliItem.productColliPieces = [];
                        colliItem.productColliPieces.push({
                            productColliPieceID: data.data.productColliPieceID,
                            productColliID: colliItem.productColliID,
                            pieceID: data.data.pieceID,
                            colli: data.data.colli,
                            pcs: data.data.pcs,
                            piceDescription: data.data.piceDescription,
                            colliPieceDescription: data.data.colliPieceDescription,
                        });
                        $scope.$apply();
                    }
                    else {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        //ean code new feature (delete)
        deleteSetEanCode: function (id,index) {
            jsonService.deleteSetEanCode(
                id,
                function (data) {
                    if (data.message.type == 0) {
                        $scope.data.productSetEANCodes.splice(index, 1);
                        $scope.$apply();
                    }
                    else {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        deleteColli: function (productSetEANCodeItem, id, index) {
            jsonService.deleteColli(
                id,
                function (data) {
                    if (data.message.type == 0) {
                        productSetEANCodeItem.productCollis.splice(index, 1);
                        $scope.$apply();
                    }
                    else {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        deleteColliPiece: function (colliItem, id, index) {
            jsonService.deleteColliPiece(
                id,
                function (data) {
                    if (data.message.type == 0) {
                        colliItem.productColliPieces.splice(index, 1);
                        $scope.$apply();
                    }
                    else {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        //update ean code new feature
        updateColliPiece: function (item) {
            jsonService.updateColliPiece(
                item,
                function (data) {
                    if (data.message.type == 0) {
                        $scope.$apply();
                    }
                    else {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        createEANCodeManual: function () {
            $scope.isManualEANCode = true;
        },


        updateColli: function (item) {
            jsonService.updateColli(
                item.productColliID,
                item,
                function (data) {
                    if (data.message.type == 0) {
                        $scope.$apply();
                    }
                    else {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            window.location = context.refreshUrl + id + '?m=0';
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);