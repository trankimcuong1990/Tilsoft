//
// SUPPORT
//
jQuery('#main-form').keypress(function(e){
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngSanitize', 'ui.select2']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;
    $scope.seasons = null;
    $scope.clients = null;
    $scope.sampleRequestTypes = null;
    $scope.samplePurposes = null;
    $scope.sampleTransportTypes = null;
    $scope.models = null;
    $scope.materials = null;
    $scope.materialTypes = null;
    $scope.materialColors = null;
    $scope.frameMaterials = null;
    $scope.frameMaterialColors = null;
    $scope.subMaterials = null;
    $scope.subMaterialColors = null;
    $scope.backCushions = null;
    $scope.seatCushions = null;
    $scope.cushionColors = null;
    $scope.users = null;
    $scope.factories = null;

    $scope.newid = 0;

    $scope.currentProduct = null;
    $scope.currentProgress = null;
    $scope.currentRemark = null;
    $scope.currentDrawing = null;
    $scope.currentRef = null;
    $scope.currentMinute = null

    //
    // event
    //
    $scope.event = {
        init:function(){
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.seasons = data.data.seasons;
                    $scope.clients = data.data.clients;
                    $scope.sampleRequestTypes = data.data.sampleRequestTypes;
                    $scope.samplePurposes = data.data.samplePurposes;
                    $scope.sampleTransportTypes = data.data.sampleTransportTypes;
                    $scope.models = data.data.models;
                    $scope.materials = data.data.materials;
                    $scope.materialTypes = data.data.materialTypes;
                    $scope.materialColors = data.data.materialColors;
                    $scope.frameMaterials = data.data.frameMaterials;
                    $scope.frameMaterialColors = data.data.frameMaterialColors;
                    $scope.subMaterials = data.data.subMaterials;
                    $scope.subMaterialColors = data.data.subMaterialColors;
                    $scope.backCushions = data.data.backCushions;
                    $scope.seatCushions = data.data.seatCushions;
                    $scope.cushionColors = data.data.cushionColors;
                    $scope.users = data.data.users;
                    $scope.factories = data.data.factories;

                    // init select 2 for client box
                    jQuery('#clientSelectBox').select2({
                        ajax: {
                            url: jsonService.supportServiceUrl + 'select2client',
                            dataType: 'json',
                            type: "GET",
                            quietMillis: 150,
                            data: function (params) {
                                return {
                                    query: params
                                };
                            },
                            results: function (data) {
                                return {
                                    results: jQuery.map(data, function (item) {
                                        return {
                                            text: item.clientUD,
                                            id: item.clientID
                                        }
                                    })
                                };
                            }
                        },
                        cache: true,
                        initSelection: function (item, callback) {
                            if ($scope.data.sampleOrderID > 0) {
                                callback({ id: $scope.data.clientID, text: $scope.data.clientUD });
                            }
                        },
                        minimumInputLength: 3
                    });

                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function() {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.seasons = null;
                    $scope.clients = null;
                    $scope.sampleRequestTypes = null;
                    $scope.samplePurposes = null;
                    $scope.sampleTransportTypes = null;
                    $scope.models = null;
                    $scope.materials = null;
                    $scope.materialTypes = null;
                    $scope.materialColors = null;
                    $scope.frameMaterials = null;
                    $scope.frameMaterialColors = null;
                    $scope.subMaterials = null;
                    $scope.subMaterialColors = null;
                    $scope.backCushions = null;
                    $scope.seatCushions = null;
                    $scope.cushionColors = null;
                    $scope.users = null;
                    $scope.factories = null;
                    $scope.$apply();
                }
            );
        },
        update: function(){
            if($scope.editForm.$valid)
            {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if(data.message.type == 0) {
                            $scope.method.refresh(data.data.sampleOrderID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else
            {
                jsHelper.showMessage('warning', errMsg);
            }
        },

        //
        // order
        // 
        updateOrder: function () {
            if ($scope.editForm.$valid) {
                jsonService.updateOrder(
                    context.id,
                    $scope.data,
                    function (data) {
                        if (context.id == 0) {
                            jsHelper.processMessageEx(data.message, "Sample Order updated");
                            if (data.message.type == 0) {
                                $scope.method.refresh(data.data.sampleOrderID);
                            }
                        }
                        else {
                            jsHelper.processMessageEx(data.message, "Sample Order updated");
                            $scope.data = data.data;
                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', errMsg);
            }
        },
        refreshOrder: function () {
            jsonService.getOrder(
                   context.id,
                   function (data) {
                       if (data.message.type != 0) {
                           jsHelper.processMessageEx(data.message);
                       }
                       $scope.data = data.data;
                       $scope.$apply();
                   },
                   function (error) {
                       jsHelper.showMessage('warning', error);
                   }
               );
        },
        backToOrder: function () {
            var index = $scope.method.getProductIndex($scope.currentProduct.sampleProductID);
            if (index < 0) {
                $scope.data.sampleProducts.push($scope.currentProduct);
            }
            else {
                $scope.data.sampleProducts[index] = JSON.parse(JSON.stringify($scope.currentProduct));
            }

            jsHelper.hidePopup('detailPanel', function () { });
        },
        changeOrderStatus: function (statusId) {
            jsonService.changeOrderStatus(
                   context.id,
                   statusId,
                   $scope.data,                   
                   function (data) {
                        if (context.id == 0) {
                            jsHelper.processMessageEx(data.message, "Sample Order updated");
                            if (data.message.type == 0) {
                                $scope.method.refresh(data.data.sampleOrderID);
                            }
                        }
                        else {
                            jsHelper.processMessageEx(data.message, "Sample Order updated");
                            $scope.data = data.data;
                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
               );
        },

        // product
        addProduct: function () {
            $scope.currentProduct = {
                sampleProductID: $scope.method.getNewID(),
                sampleOrderID: context.id,
                requestTypeID: null,
                articleDescription: null,
                overallDimL: null,
                overallDimW: null,
                overallDimH: null,
                modelID: null,
                frameMaterialID: null,
                frameMaterialColorID: null,
                materialID: null,
                materialTypeID: null,
                materialColorID: null,
                subMaterialID: null,
                subMaterialColorID: null,
                seatCushionID: null,
                seatCushionThickness: null,
                seatCushionSpecification: null,
                backCushionID: null,
                backCushionThickness: null,
                backCushionSpecification: null,
                cushionColorID: null,
                displayIndex: 0,
                quantity:0,
                remark: null,
                isConfirmed: null,
                modelNM: null,
                frameMaterialNM: null,
                frameMaterialColorNM: null,
                materialNM: null,
                materialTypeNM: null,
                materialColorNM: null,
                subMaterialNM: null,
                subMaterialColorNM: null,
                seatCushionNM: null,
                backCushionNM: null,
                cushionColorNM: null,
                requestTypeNM: null,
                nlSuggestedFactoryID: null,
                nlSuggestedFactoryRemark: null,
                vnSuggestedFactoryID: null,
                vnSuggestedFactoryRemark: null,
                factoryDeadline: null,
                nlSuggestedFactoryUD: null,
                vnSuggestedFactoryUD: null,                
                thumbnailLocation: null,
                fileLocation: null,
                sampleProductMinuteImages: [],
                sampleReferenceImages: [],
                sampleProgresses: [],
                sampleRemarks: [],
                sampleTechnicalDrawings: [],
                netWeight: null,
                grossWeight: null,
                loadAbility20DC: null,
                loadAbility40DC: null,
                loadAbility40HC: null
            };
            $scope.event.editProduct($scope.currentProduct);
        },
        editProduct: function (item) {
            $scope.currentProduct = JSON.parse(JSON.stringify(item));

            // init select 2 for popup model box
            jQuery('#popupModelSelectBox').select2({
                ajax: {
                    url: jsonService.supportServiceUrl + 'select2model',
                    dataType: 'json',
                    type: "GET",
                    quietMillis: 150,
                    data: function (params) {
                        return {
                            query: params
                        };
                    },
                    results: function (data) {
                        $scope.models = [];
                        return {
                            results: jQuery.map(data, function (item) {
                                $scope.models.push(item);
                                return {
                                    text: item.modelNM,
                                    id: item.modelID
                                }
                            })
                        };
                    }
                },
                cache: true,
                initSelection: function (item, callback) {
                    if ($scope.currentProduct != null && $scope.currentProduct.modelID != null) {
                        callback({ id: $scope.currentProduct.modelID, text: $scope.currentProduct.modelNM });
                    }
                },
                minimumInputLength: 3
            });
            jQuery('#popupModelSelectBox').on("select2-selecting", function (e) {
                for (var index = 0; index < $scope.models.length; index++) {
                    if ($scope.models[index].modelID == e.choice.id) {
                        // add dimension auto matically
                        $scope.currentProduct.overallDimL = $scope.models[index].overallDimL;
                        $scope.currentProduct.overallDimW = $scope.models[index].overallDimW;
                        $scope.currentProduct.overallDimH = $scope.models[index].overallDimH;

                        // add reference image
                        $scope.currentRef = {
                            sampleReferenceImageID: $scope.method.getNewID(),
                            sampleProductID: $scope.currentProduct.sampleProductID,
                            fileUD: '',
                            bringInBy: '',
                            bringInDate: '',
                            hasChange: true,
                            newFile: null,
                            thumbnailLocation: context.backendFileUrl + $scope.models[index].fileLocation,
                            fileLocation: context.backendFileUrl + $scope.models[index].fileLocation,
                            isDefault: true
                        };
                        $scope.event.updateReferenceImage();

                        break;
                    }
                }
            });

            jsHelper.showPopup('detailPanel', function () { });
        },
        updateProduct: function () {
            jsonService.updateProduct(
                   $scope.currentProduct.sampleProductID,
                   $scope.currentProduct,
                   function (data) {
                       if (data.message.type != 0) {
                           jsHelper.processMessageEx(data.message);
                       }
                       jsHelper.processMessageEx(data.message, "Sample Product updated");
                       $scope.currentProduct = data.data;
                       $scope.$apply();
                   },
                   function (error) {
                       jsHelper.showMessage('warning', error);
                   }
               );
        },
        changeProductStatus: function (statusId) {
            jsonService.changeProductStatus(
                   $scope.currentProduct.sampleProductID,
                   statusId,
                   $scope.currentProduct,                   
                   function (data) {
                       if (data.message.type != 0) {
                           jsHelper.processMessageEx(data.message);
                       }
                       jsHelper.processMessageEx(data.message, "Sample Product updated");
                       $scope.currentProduct = data.data;
                       $scope.$apply();
                   },
                   function (error) {
                       jsHelper.showMessage('warning', error);
                   }
               );
        },
        removeProduct: function (item) {
            if (confirm('Delete the selected product ?')) {
                jsonService.deleteProduct(
                   item.sampleProductID,
                   function (data) {
                       if (data.message.type != 0) {
                           jsHelper.processMessageEx(data.message);
                       }

                       $scope.data.sampleProducts.splice($scope.data.sampleProducts.indexOf(item), 1);
                       $scope.$apply();
                   },
                   function (error) {
                       jsHelper.showMessage('warning', error);
                   }
               );
            }            
        },
        refreshProduct: function () {
            if ($scope.currentProduct.sampleProductID > 0) {
                jsonService.getProduct(
                   $scope.currentProduct.sampleProductID,
                   function (data) {
                       if (data.message.type != 0) {
                           jsHelper.processMessageEx(data.message);
                       }
                       $scope.currentProduct = data.data;
                       $scope.$apply();
                   },
                   function (error) {
                       jsHelper.showMessage('warning', error);
                   }
               );
            }
            else {
                $scope.currentProduct = {
                    sampleProductID: $scope.method.getNewID(),
                    requestTypeID: null,
                    articleDescription: null,
                    overallDimL: null,
                    overallDimW: null,
                    overallDimH: null,
                    modelID: null,
                    frameMaterialID: null,
                    frameMaterialColorID: null,
                    materialID: null,
                    materialTypeID: null,
                    materialColorID: null,
                    subMaterialID: null,
                    subMaterialColorID: null,
                    seatCushionID: null,
                    seatCushionThickness: null,
                    seatCushionSpecification: null,
                    backCushionID: null,
                    backCushionThickness: null,
                    backCushionSpecification: null,
                    cushionColorID: null,
                    remark: null,
                    isConfirmed: null,
                    modelNM: null,
                    frameMaterialNM: null,
                    frameMaterialColorNM: null,
                    materialNM: null,
                    materialTypeNM: null,
                    materialColorNM: null,
                    subMaterialNM: null,
                    subMaterialColorNM: null,
                    seatCushionNM: null,
                    backCushionNM: null,
                    cushionColorNM: null,
                    requestTypeNM: null,
                    nlSuggestedFactoryID: null,
                    nlSuggestedFactoryRemark: null,
                    vnSuggestedFactoryID: null,
                    vnSuggestedFactoryRemark: null,
                    factoryDeadline: null,
                    nlSuggestedFactoryUD: null,
                    vnSuggestedFactoryUD: null,
                    thumbnailLocation: null,
                    fileLocation: null,
                    sampleProductMinuteImages: [],
                    sampleReferenceImages: [],
                    sampleProgresses: [],
                    sampleRemarks: [],
                    sampleTechnicalDrawings: []
                };
            }
        },

        addMinuteImage: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        // TO DO LIST
                        scope.currentProduct.sampleProductMinuteImages.push({
                            sampleProductMinuteImageID: scope.method.getNewID(),
                            fileUD: '',
                            hasChange: true,
                            newFile: img.filename,
                            thumbnailLocation: img.fileURL,
                            fileLocation: img.fileURL
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeMinuteImage: function ($index) {
            $scope.currentProduct.sampleProductMinuteImages.splice($index, 1);
        },

        // reference image
        addReferenceImage: function () {
            $scope.currentRef = {
                sampleReferenceImageID: $scope.method.getNewID(),
                sampleProductID: $scope.currentProduct.sampleProductID,
                fileUD: '',
                bringInBy: '',
                bringInDate: '',
                hasChange: true,
                newFile: null,
                thumbnailLocation: context.backendUrl + 'no-image.jpg',
                fileLocation: context.backendUrl + 'no-image.jpg',
                isDefault: false
            };
            $scope.event.editReferenceImage(-1);
        },
        editReferenceImage: function ($index) {
            if ($index >= 0) {
                $scope.currentRef = JSON.parse(JSON.stringify($scope.currentProduct.sampleReferenceImages[$index]));
            }

            jQuery("#referenceImageForm").modal();
        },
        updateReferenceImage: function () {
            jsonService.updateReferenceImage(
                  $scope.currentRef.sampleReferenceImageID,
                  $scope.currentRef,
                  function (data) {
                      if (data.message.type != 0) {
                          jsHelper.processMessageEx(data.message);
                      }
                      $scope.currentRef = data.data;

                      if ($scope.currentRef.isDefault) {
                          for (var index = 0; index < $scope.currentProduct.sampleReferenceImages.length; index++) {
                              $scope.currentProduct.sampleReferenceImages[index].isDefault = false;
                          }
                          $scope.currentProduct.thumbnailLocation = $scope.currentRef.thumbnailLocation;
                          $scope.currentProduct.fileLocation = $scope.currentRef.fileLocation;
                      }

                      var index = $scope.method.getReferenceImageIndex($scope.currentRef.sampleReferenceImageID);
                      if (index < 0) {
                          $scope.currentProduct.sampleReferenceImages.push($scope.currentRef);
                      }
                      else {
                          $scope.currentProduct.sampleReferenceImages[index] = JSON.parse(JSON.stringify($scope.currentRef));
                      }

                      $('#referenceImageForm').modal('hide');

                      $scope.$apply();
                  },
                  function (error) {
                      jsHelper.showMessage('warning', error);
                  }
              );           
        },
        refreshReferenceImage: function () {
            if ($scope.currentRef.sampleReferenceImageID > 0) {
                jsonService.getReferenceImage(
                   $scope.currentRef.sampleReferenceImageID,
                   function (data) {
                       if (data.message.type != 0) {
                           jsHelper.processMessageEx(data.message);
                       }
                       $scope.currentRef = data.data;
                       $scope.$apply();
                   },
                   function (error) {
                       jsHelper.showMessage('warning', error);
                   }
               );
            }
            else {
                $scope.currentRef = {
                    sampleReferenceImageID: scope.method.getNewID(),
                    fileUD: '',
                    bringInBy: '',
                    bringInDate: '',
                    hasChange: true,
                    newFile: img.filename,
                    thumbnailLocation: img.fileURL,
                    fileLocation: img.fileURL,
                    isDefault: false
                };
            }
        },
        removeReferenceImage: function ($index) {
            if (confirm('Delete the reference image ?')) {
                jsonService.deleteReferenceImage(
                  $scope.currentProduct.sampleReferenceImages[$index].sampleReferenceImageID,
                  function (data) {
                      if (data.message.type != 0) {
                          jsHelper.processMessageEx(data.message);
                      }

                      if ($scope.currentProduct.sampleReferenceImages[$index].isDefault) {
                          $scope.currentProduct.thumbnailLocation = null;
                          $scope.currentProduct.fileLocation = null;
                      }

                      $scope.currentProduct.sampleReferenceImages.splice($index, 1);
                      $scope.$apply();
                  },
                  function (error) {
                      jsHelper.showMessage('warning', error);
                  }
              );
            }
        },
        changeReferenceImage: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.currentRef.hasChange = true;
                        scope.currentRef.newFile = img.filename;
                        scope.currentRef.thumbnailLocation = img.fileURL;
                        scope.currentRef.fileLocation = img.fileURL;
                    }, null);
                });
            };
            masterUploader.open();
        },

        // progress
        addProgress: function () {
            // set current progress
            $scope.currentProgress = {
                sampleProgressID: $scope.method.getNewID(),
                sampleProductID: $scope.currentProduct.sampleProductID,
                version: null,
                remark: null,
                updatorName: 'Yourself',
                updatedDate: 'Just Now',
                sampleProgressImages: [],
                sampleRemarks: []
            };
            $scope.event.editProgress($scope.currentProgress);
        },
        editProgress: function (item) {
            $scope.currentProgress = JSON.parse(JSON.stringify(item));
            jQuery("#progressForm").modal();
        },
        updateProgress: function () {
            jsonService.updateProgress(
                  $scope.currentProgress.sampleProgressID,
                  $scope.currentProgress,
                  function (data) {
                      if (data.message.type != 0) {
                          jsHelper.processMessageEx(data.message);
                      }
                      $scope.currentProgress = data.data;

                      var index = $scope.method.getProgressIndex($scope.currentProgress.sampleProgressID);
                      if (index < 0) {
                          $scope.currentProduct.sampleProgresses.push($scope.currentProgress);
                      }
                      else {
                          $scope.currentProduct.sampleProgresses[index] = JSON.parse(JSON.stringify($scope.currentProgress));
                      }
                      $('#progressForm').modal('hide');

                      $scope.$apply();
                  },
                  function (error) {
                      jsHelper.showMessage('warning', error);
                  }
              );
        },
        removeProgress: function (item) {
            if (confirm('Delete the selected progress?')) {
                jsonService.deleteProgress(
                  item.sampleProgressID,
                  function (data) {
                      if (data.message.type != 0) {
                          jsHelper.processMessageEx(data.message);
                      }

                      $scope.currentProduct.sampleProgresses.splice($scope.currentProduct.sampleProgresses.indexOf(item), 1);
                      $scope.$apply();
                  },
                  function (error) {
                      jsHelper.showMessage('warning', error);
                  }
              );
            }
        },
        addProgressImage: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        // TO DO LIST
                        scope.currentProgress.sampleProgressImages.push({
                            sampleProgressImageID: scope.method.getNewID(),
                            fileUD: '',
                            hasChange: true,
                            newFile: img.filename,
                            thumbnailLocation: img.fileURL,
                            fileLocation: img.fileURL
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeProgressImage: function ($index) {
            $scope.currentProgress.sampleProgressImages.splice($index, 1);
        },

        // progress remark
        addProgressRemark: function (progressId) {
            $scope.currentRemark = {
                sampleRemarkID: $scope.method.getNewID(),
                sampleProgressID: progressId,
                sampleTechnicalDrawingID: null,
                sampleProductID: null,
                updatedDate: 'Just Now',
                updatorName: 'Yourself',
                remark: null,
                sampleRemarkImages: []
            };

            $scope.event.editProgressRemark($scope.currentRemark);
        },
        editProgressRemark: function (item) {
            $scope.currentRemark = JSON.parse(JSON.stringify(item));
            jQuery("#remarkForm").modal();
        },
        removeProgressRemark: function (item, progress) {            
            if (confirm('Delete the selected remark?')) {
                jsonService.deleteRemark(
                  item.sampleRemarkID,
                  function (data) {
                      if (data.message.type != 0) {
                          jsHelper.processMessageEx(data.message);
                      }

                      progress.sampleRemarks.splice(progress.sampleRemarks.indexOf(item), 1);
                      $scope.$apply();
                  },
                  function (error) {
                      jsHelper.showMessage('warning', error);
                  }
              );
            }
        },
        
        // drawing
        addDrawing: function () {
            // set current drawing
            $scope.currentDrawing = {
                sampleTechnicalDrawingID: $scope.method.getNewID(),
                sampleProductID: $scope.currentProduct.sampleProductID,
                version: null,
                sourceFileUD: null,
                previewFileUD: null,
                authorID: null,
                remark: null,
                updatorName: 'Yourself',
                updatedDate: 'Just Now',
                friendlyName: null,
                sourceFileLocation: null,
                thumbnailLocation: null,
                fileLocation: null,
                sampleRemarks: []
            };
            $scope.event.editDrawing($scope.currentDrawing);
        },
        editDrawing: function (item) {
            $scope.currentDrawing = JSON.parse(JSON.stringify(item));
            jQuery("#drawingForm").modal();
        },
        updateDrawing: function () {
            jsonService.updateDrawing(
                  $scope.currentDrawing.sampleTechnicalDrawingID,
                  $scope.currentDrawing,
                  function (data) {
                      if (data.message.type != 0) {
                          jsHelper.processMessageEx(data.message);
                      }
                      $scope.currentDrawing = data.data;

                      var index = $scope.method.getDrawingIndex($scope.currentDrawing.sampleTechnicalDrawingID);
                      if (index < 0) {
                          $scope.currentProduct.sampleTechnicalDrawings.push($scope.currentDrawing);
                      }
                      else {
                          $scope.currentProduct.sampleTechnicalDrawings[index] = JSON.parse(JSON.stringify($scope.currentDrawing));
                      }
                      $('#drawingForm').modal('hide');

                      $scope.$apply();
                  },
                  function (error) {
                      jsHelper.showMessage('warning', error);
                  }
              );            
        },
        removeDrawing: function (item) {
            if (confirm('Delete the selected technical drawing?')) {
                jsonService.deleteDrawing(
                  item.sampleTechnicalDrawingID,
                  function (data) {
                      if (data.message.type != 0) {
                          jsHelper.processMessageEx(data.message);
                      }

                      $scope.currentProduct.sampleTechnicalDrawings.splice($scope.currentProduct.sampleTechnicalDrawings.indexOf(item), 1);
                      $scope.$apply();
                  },
                  function (error) {
                      jsHelper.showMessage('warning', error);
                  }
              );
            }
        },
        addDrawingPreviewImage: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        // TO DO LIST
                        scope.currentDrawing.hasChange = true;
                        scope.currentDrawing.thumbnailLocation = img.fileURL;
                        scope.currentDrawing.fileLocation = img.fileURL;
                        scope.currentDrawing.newFile = img.filename;
                    }, null);
                });
            };
            masterUploader.open();
        },
        addDrawingSource: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        // TO DO LIST
                        scope.currentDrawing.sourceHasChange = true;
                        scope.currentDrawing.sourceFileLocation = img.fileURL;
                        scope.currentDrawing.friendlyName = img.filename;
                        scope.currentDrawing.sourceNewFile = img.filename;
                    }, null);
                });
            };
            masterUploader.open();
        },

        // drawing remark
        addDrawingRemark: function (drawingId) {
            $scope.currentRemark = {
                sampleRemarkID: $scope.method.getNewID(),
                sampleProgressID: null,
                sampleTechnicalDrawingID: drawingId,
                sampleProductID: null,
                updatedDate: 'Just Now',
                updatorName: 'Yourself',
                remark: null,
                sampleRemarkImages: []
            };
            $scope.event.editDrawingRemark($scope.currentRemark);
        },
        editDrawingRemark: function (item) {
            $scope.currentRemark = JSON.parse(JSON.stringify(item));
            jQuery("#remarkForm").modal();
        },
        removeDrawingRemark: function (item, drawing) {
            if (confirm('Delete the selected remark?')) {
                jsonService.deleteRemark(
                  item.sampleRemarkID,
                  function (data) {
                      if (data.message.type != 0) {
                          jsHelper.processMessageEx(data.message);
                      }

                      drawing.sampleRemarks.splice(drawing.sampleRemarks.indexOf(item), 1);
                      $scope.$apply();
                  },
                  function (error) {
                      jsHelper.showMessage('warning', error);
                  }
              );
            }
        },

        // product remark
        addProductRemark: function () {
            $scope.currentRemark = {
                sampleRemarkID: $scope.method.getNewID(),
                sampleProgressID: null,
                sampleTechnicalDrawingID: null,
                sampleProductID: $scope.currentProduct.sampleProductID,
                updatedDate: 'Just Now',
                updatorName: 'Yourself',
                remark: null,
                sampleRemarkImages: []
            };

            $scope.event.editProductRemark($scope.currentRemark);
        },
        editProductRemark: function (item) {
            $scope.currentRemark = JSON.parse(JSON.stringify(item));
            jQuery("#remarkForm").modal();
        },
        removeProductRemark: function (item) {            
            if (confirm('Delete the selected remark?')) {
                jsonService.deleteRemark(
                  item.sampleRemarkID,
                  function (data) {
                      if (data.message.type != 0) {
                          jsHelper.processMessageEx(data.message);
                      }

                      $scope.currentProduct.sampleRemarks.splice($scope.currentProduct.sampleRemarks.indexOf(item), 1);
                      $scope.$apply();
                  },
                  function (error) {
                      jsHelper.showMessage('warning', error);
                  }
              );
            }
        },

        // remark
        updateRemark: function () {
            jsonService.updateRemark(
                  $scope.currentRemark.sampleRemarkID,
                  $scope.currentRemark,
                  function (data) {
                      if (data.message.type != 0) {
                          jsHelper.processMessageEx(data.message);
                      }
                      $scope.currentRemark = data.data;

                      if ($scope.currentRemark.sampleProgressID != null) {
                          var progressIndex = $scope.method.getProgressIndex($scope.currentRemark.sampleProgressID);
                          index = $scope.method.getProgressRemarkIndex($scope.currentRemark.sampleRemarkID, $scope.currentRemark.sampleProgressID);
                          if (index >= 0) {
                              $scope.currentProduct.sampleProgresses[progressIndex].sampleRemarks[index] = JSON.parse(JSON.stringify($scope.currentRemark));
                          }
                          else {
                              $scope.currentProduct.sampleProgresses[progressIndex].sampleRemarks.push(JSON.parse(JSON.stringify($scope.currentRemark)));
                          }
                      }

                      if ($scope.currentRemark.sampleTechnicalDrawingID != null) {
                          var drawingId = $scope.method.getDrawingIndex($scope.currentRemark.sampleTechnicalDrawingID);
                          index = $scope.method.getDrawingRemarkIndex($scope.currentRemark.sampleRemarkID, $scope.currentRemark.sampleTechnicalDrawingID);
                          if (index >= 0) {
                              $scope.currentProduct.sampleTechnicalDrawings[drawingId].sampleRemarks[index] = JSON.parse(JSON.stringify($scope.currentRemark));
                          }
                          else {
                              $scope.currentProduct.sampleTechnicalDrawings[drawingId].sampleRemarks.push(JSON.parse(JSON.stringify($scope.currentRemark)));
                          }
                      }

                      if ($scope.currentRemark.sampleProductID != null) {
                          index = $scope.method.getProductRemarkIndex($scope.currentRemark.sampleRemarkID);
                          if (index >= 0) {
                              $scope.currentProduct.sampleRemarks[index] = JSON.parse(JSON.stringify($scope.currentRemark));
                          }
                          else {
                              $scope.currentProduct.sampleRemarks.push(JSON.parse(JSON.stringify($scope.currentRemark)));
                          }
                      }

                      $('#remarkForm').modal('hide');

                      $scope.$apply();
                  },
                  function (error) {
                      jsHelper.showMessage('warning', error);
                  }
              );
        },
        addRemarkImage: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        // TO DO LIST
                        scope.currentRemark.sampleRemarkImages.push({
                            sampleRemarksImageID: scope.method.getNewID(),
                            fileUD: '',
                            hasChange: true,
                            newFile: img.filename,
                            thumbnailFileLocation: img.fileURL,
                            originalFileLocation: img.fileURL
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeRemarkImage: function ($index) {
            $scope.currentRemark.sampleRemarkImages.splice($index, 1);
        },

        // minute
        addMinute: function () {
            // set current minute
            $scope.currentMinute = {
                sampleProductMinuteID: $scope.method.getNewID(),
                sampleProductID: $scope.currentProduct.sampleProductID,
                meetingMinute: '',
                updatorName: 'Yourself',
                updatedDate: 'Just Now',
                sampleProductMinuteImages: []
            };
            $scope.event.editMinute($scope.currentMinute);
        },
        editMinute: function (item) {
            $scope.currentMinute = JSON.parse(JSON.stringify(item));
            jQuery("#minuteForm").modal();
        },
        updateMinute: function () {
            jsonService.updateMinute(
                  $scope.currentMinute.sampleProductMinuteID,
                  $scope.currentMinute,
                  function (data) {
                      if (data.message.type != 0) {
                          jsHelper.processMessageEx(data.message);
                      }
                      $scope.currentMinute = data.data;
                      
                      var index = $scope.method.getMinuteIndex($scope.currentMinute.sampleProductMinuteID);
                      if (index < 0) {
                          $scope.currentProduct.sampleProductMinutes.push($scope.currentMinute);
                      }
                      else {
                          $scope.currentProduct.sampleProductMinutes[index] = JSON.parse(JSON.stringify($scope.currentMinute));
                      }
                      $('#minuteForm').modal('hide');
                      jsHelper.processMessageEx(data.message, "Minute updated");

                      $scope.$apply();
                  },
                  function (error) {
                      jsHelper.showMessage('warning', error);
                  }
              );
        },
        removeMinute: function (item) {
            if (confirm('Delete the selected minute?')) {
                jsonService.deleteMinute(
                  item.sampleProductMinuteID,
                  function (data) {
                      if (data.message.type != 0) {
                          jsHelper.processMessageEx(data.message);
                      }

                      $scope.currentProduct.sampleProductMinutes.splice($scope.currentProduct.sampleProductMinutes.indexOf(item), 1);
                      $scope.$apply();
                  },
                  function (error) {
                      jsHelper.showMessage('warning', error);
                  }
              );
            }
        },
        addMinuteImage: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        // TO DO LIST
                        scope.currentMinute.sampleProductMinuteImages.push({
                            sampleProductMinuteImageID: scope.method.getNewID(),
                            fileUD: '',
                            hasChange: true,
                            newFile: img.filename,
                            thumbnailLocation: img.fileURL,
                            fileLocation: img.fileURL
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeMinuteImage: function ($index) {
            $scope.currentMinute.sampleProductMinuteImages.splice($index, 1);
        },

        // report
        getMinuteReport: function () {
            jsonService.getMinuteReport(
                context.id,
                function (data) {
                    window.location = context.backendReportUrl + data.data + '.xlsm';

                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        getSampleReport: function () {
            jsonService.getSampleReport(
                context.id,
                function (data) {
                    window.location = context.backendReportUrl + data.data + '.xlsm';

                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        resetModel: function () {
            $scope.currentProduct.modelID = null;
            jQuery('#popupModelSelectBox').select2('val', null);
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function(id){
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl+id;
        },
        getNewID: function(){
            $scope.newid--;
            return $scope.newid;
        },
        formatRemark: function (remark) {
            if (remark == null) {
                return '';
            }
            return remark.replace(/(?:\r\n|\r|\n)/g, '<br />');
        },
        isProgressExist: function (id) {
            var isExist = false;
            var index = 0;
            for (var index = 0; index < $scope.currentProduct.sampleProgresses.length; index++) {
                if ($scope.currentProduct.sampleProgresses[index].sampleProgressID == id) {
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
        getProgressIndex: function (id) {
            var isExist = false;
            var index = 0;
            for (var index = 0; index < $scope.currentProduct.sampleProgresses.length; index++) {
                if ($scope.currentProduct.sampleProgresses[index].sampleProgressID == id) {
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
        getProgressRemarkIndex: function (id, progressId) {
            var isExist = false;
            var index = 0;
            var progressIndex = $scope.method.getProgressIndex(progressId);
            if (progressIndex >= 0) {
                for (var index = 0; index < $scope.currentProduct.sampleProgresses[progressIndex].sampleRemarks.length; index++) {
                    if ($scope.currentProduct.sampleProgresses[progressIndex].sampleRemarks[index].sampleRemarkID == id) {
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
            }
            return -1;
        },
        getDrawingIndex: function (id) {
            var isExist = false;
            var index = 0;
            for (var index = 0; index < $scope.currentProduct.sampleTechnicalDrawings.length; index++) {
                if ($scope.currentProduct.sampleTechnicalDrawings[index].sampleTechnicalDrawingID == id) {
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
        getDrawingRemarkIndex: function (id, drawingId) {
            var isExist = false;
            var index = 0;
            var drawingIndex = $scope.method.getDrawingIndex(drawingId);
            if (drawingIndex >= 0) {
                for (var index = 0; index < $scope.currentProduct.sampleTechnicalDrawings[drawingIndex].sampleRemarks.length; index++) {
                    if ($scope.currentProduct.sampleTechnicalDrawings[drawingIndex].sampleRemarks[index].sampleRemarkID == id) {
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
            }
            return -1;
        },
        getProductRemarkIndex: function (id) {
            var isExist = false;
            var index = 0;
            for (var index = 0; index < $scope.currentProduct.sampleRemarks.length; index++) {
                if ($scope.currentProduct.sampleRemarks[index].sampleRemarkID == id) {
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
        getReferenceImageIndex: function (id) {
            var isExist = false;
            var index = 0;
            for (var index = 0; index < $scope.currentProduct.sampleReferenceImages.length; index++) {
                if ($scope.currentProduct.sampleReferenceImages[index].sampleReferenceImageID == id) {
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
        getProductIndex: function (id) {
            var isExist = false;
            var index = 0;
            for (var index = 0; index < $scope.data.sampleProducts.length; index++) {
                if ($scope.data.sampleProducts[index].sampleProductID == id) {
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
        getMinuteIndex: function (id) {
            var isExist = false;
            var index = 0;
            for (var index = 0; index < $scope.currentProduct.sampleProductMinutes.length; index++) {
                if ($scope.currentProduct.sampleProductMinutes[index].sampleProductMinuteID == id) {
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
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);