$("#pricingID").select2({
    closeOnSelect: true,
});

$("#qualityID").select2({
    closeOnSelect: true,
});


$("#directorID").select2({
    closeOnSelect: true,
});


$("#managerID").select2({
    closeOnSelect: true,
});


$("#sampleID").select2({
    closeOnSelect: true,
});

function formatProductionItem(data) {
    return $.map(data.data, function (item) {
        return {
            description: item.productionItemUD,
            id: item.productionItemID,
            label: item.productionItemNM,
            value: item.productionItemNM,
            productionItemID: item.productionItemID,
            productionItemUD: item.productionItemUD,
            productionItemNM: item.productionItemNM,
            units: item.units,
            unitID: item.unitID,
            qty: item.qty,
            price: item.price,
        };
    });
}
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ui.select2', 'ng-currency', 'avs-directives', 'ngCookies']);
tilsoftApp.filter("trustUrl", ['$sce', function ($sce) {
    return function (recordingUrl) {
        return $sce.trustAsResourceUrl(recordingUrl);
    };
}]);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', '$timeout', function ($scope, dataService, $timeout) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    $scope.data = null;

    $scope.newID = -1;
    $scope.employees = null;
    $scope.suppliers = null;
    $scope.locations = null;

    $scope.keyRawMaterials = null;
    $scope.employees = null;
    $scope.supplierPaymentTerms = null;
    $scope.subSupplierContracts = null;
    $scope.subsupplierDocumentTypes = null;
    $scope.productGroupDTOs = null;


    $scope.pricingSelected = [];
    $scope.qualitySelected = [];
    $scope.poDetailTemp = [];
    $scope.materialPriceTemp = [];
    $scope.directorSelected = [];
    $scope.managerSelected = [];
    $scope.sampleSeleted = [];
    //$scope.units = [];
    $scope.materialsPrices = [];
    $scope.materialPriceHistories = [];
    $scope.currentCard = [];
    $scope.appointmentDTO = [];


    $scope.newKeyRawMaterial = {
        keyRawMaterialID: '',
        KeyRawMaterialNM: ''
    };

    $scope.certificateTypes = [
        { 'typeID': 1, 'typeNM': 'BSCI' },
        { 'typeID': 2, 'typeNM': 'ISO-9001 / ISO-14001' },
        { 'typeID': 3, 'typeNM': 'QMS' },
        { 'typeID': 4, 'typeNM': 'SR' },
        { 'typeID': 5, 'typeNM': 'CTPAT' },
        { 'typeID': 6, 'typeNM': 'FSC' }
    ];


    $scope.productionItemBox = {

        request: {

            url: context.getProductionItemUrl,
            token: context.token
        },
        data: {
            productionItemID: null,
            productionItemUD: null,
            productionItemNM: null,
            unitID: null,
            qty: null,
            price: null,
            units: null,
        }
    };
    $scope.event = {
        addContactQuickInfoSupplier: function () {
            var item = {
                supplierContactQuickInfoId: $scope.method.getNewID()
            };
            $scope.data.supplierContactQuickInfos.push(item);
        },

        removeContactQuickInfo: function (item) {
            var index = $scope.data.supplierContactQuickInfos.indexOf(item);
            $scope.data.supplierContactQuickInfos.splice(index, 1);
        },


        init: function () {
            dataService.load(
                context.id,
                null,
                function (data) {

                    $scope.data = data.data.data;
                    $scope.locations = data.data.locations;
                    $scope.keyRawMaterials = data.data.keyRawMaterials;
                    $scope.employees = data.data.employees;
                    $scope.supplierPaymentTerms = data.data.supplierPaymentTerms;
                    $scope.subsupplierDocumentTypes = data.data.subSupplierDocumentTypeDTOs;
                    debugger;
                    $scope.productGroupDTOs = data.data.productGroupDTOs;
                    $scope.productItemFactoryRawMaterials = data.data.productItemFactoryRawMaterials;
                    $scope.statusMaterialPrices = data.data.statusMaterialPrices;
                    $scope.appointmentDTOs = data.data.appointmentDTO;
                    $scope.users = data.data.usersDTO;
                    for (var j = 0;j< $scope.appointmentDTOs.length; j++) {
                        var item2 = $scope.appointmentDTOs[j];
                        if ($scope.data.factoryRawMaterialID == item2.factoryRawMaterialID) {
                            $scope.appointmentDTO.push(item2);
                        }
                    }
                    
                    //$scope.units = data.data.units;
                    //$scope.materialPriceHistories = data.data.materialsPrices.materialPriceHistories;
                    //angular.forEach($scope.productGroupDTOs, function (item) {
                    //    if ($scope.data.factoryRawMaterialProductGroupDTOs.filter(s => s.productGroupID == item.productGroupID).length > 0) {
                    //        item.isChecked = true;
                    //    }
                    //});

                    var x = [];
                    angular.forEach($scope.data.factoryRawMaterialPricingPersons, function (item) {
                        x.push({ "id": item.employeeID, "text": item.employeeNM });
                        $scope.pricingSelected.push(item.employeeID);
                    })
                    $('#pricingID').select2('data', x);


                    var y = [];
                    angular.forEach($scope.data.factoryRawMaterialQualityPersons, function (item) {
                        y.push({ "id": item.employeeID, "text": item.employeeNM });
                        $scope.qualitySelected.push(item.employeeID);
                    })
                    $('#qualityID').select2('data', y);

                    var l = [];
                    angular.forEach($scope.data.supplierDirectors, function (item) {
                        l.push({ "id": item.employeeID, "text": item.employeeNM });
                        $scope.directorSelected.push(item.employeeID);
                    })
                    $('#directorID').select2('data', l);

                    var z = [];
                    angular.forEach($scope.data.supplierManagers, function (item) {
                        z.push({ "id": item.employeeID, "text": item.employeeNM, "selected": item.isReceivePriceRequest });
                        $scope.managerSelected.push(item.employeeID);
                    })
                    $('#managerID').select2('data', z);

                    var h = [];
                    angular.forEach($scope.data.supplierSampleTechnicals, function (item) {
                        z.push({ "id": item.employeeID, "text": item.employeeNM });
                        $scope.sampleSeleted.push(item.employeeID);
                    })
                    $('#sampleID').select2('data', h);

                    jQuery('#content').show();

                    var arr = [context.CurrentSeason, context.PrevSeason1, context.PrevSeason2, context.PrevSeason3];

                    if ($scope.data.factoryRawMaterialTurnovers.length == 0) {
                        for (i = 0; i < arr.length; i++) {
                            $scope.data.factoryRawMaterialTurnovers.push({
                                factoryRawMaterialTurnoverID: $scope.method.getNewID(),
                                factoryRawMaterialID: context.id,
                                season: arr[i],
                                amount: 0
                            });
                        }
                    } else {
                        var existingSeason = [];
                        angular.forEach($scope.data.factoryRawMaterialTurnovers, function (item) {
                            existingSeason.push(item.season);
                        })
                        for (i = 0; i < arr.length; i++) {
                            if (jQuery.inArray(arr[i], existingSeason) == -1) {
                                $scope.data.factoryRawMaterialTurnovers.push({
                                    factoryRawMaterialTurnoverID: $scope.method.getNewID(),
                                    factoryRawMaterialID: context.id,
                                    season: arr[i],
                                    amount: 0
                                });
                            }
                        }
                    }
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.locations = null;
                    $scope.keyRawMaterials = null;
                    $scope.supplierPaymentTerms = null;
                }
            );
        },


        update: function () {
            angular.forEach($scope.data.materialsPrices, function (item) {
                if (item.productionItemNM == null) {
                    alert('Input Valid');
                    this.addnewMaterial();
                }
                if (item.statusID < 0) {
                    alert('Input Valid');
                    this.addnewMaterial();
                }
                if (item.price === null) {
                    alert('Input Valid');
                    this.addnewMaterial();
                }
            })
            debugger;
            angular.forEach($scope.data.materialsPrices, function (item) {
                if (item.materialsPriceID < 0) {
                    var datahistory = {
                        materialHistoryID: $scope.method.getNewID(),
                        price: item.price,
                        materialsPriceID: item.materialsPriceID,
                        validFrom: item.validFrom,
                        qty: item.qty,
                        remark: item.remark,
                        statusID: item.statusID,
                        updatedDate: new Date(),
                        attachFileURL: item.attachFileURL,
                        attachFileFriendlyName: item.attachFileFriendlyName                        
                    }
                    item.materialPriceHistories.push(datahistory);
                }
                   
            })
            if ($scope.editForm.$valid) {
                dataService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryRawMaterialID);
                        }
                    },
                    function (error) {

                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Invalid input data, please check');
            }
            angular.forEach($scope.productGroupDTOs, function (item) {
                if (item.isChecked) {
                    // insert new
                    if ($scope.data.factoryRawMaterialProductGroupDTOs.filter(s => s.productGroupID == item.productGroupID).length == 0) {
                        $scope.data.factoryRawMaterialProductGroupDTOs.push({
                            productGroupID: item.productGroupID
                        })
                    }
                }
                else {
                    //remove if unchecked
                    if ($scope.data.factoryRawMaterialProductGroupDTOs.filter(s => s.productGroupID == item.productGroupID).length > 0) {
                        angular.forEach($scope.data.factoryRawMaterialProductGroupDTOs.filter(s => s.productGroupID == item.productGroupID), function (itemFactoryProductGroup) {
                            $scope.data.factoryRawMaterialProductGroupDTOs.splice($scope.data.factoryRawMaterialProductGroupDTOs.indexOf(itemFactoryProductGroup), 1);
                        })
                    }
                }

            });
        },

        openHictoryPrice: function (materialhistory) {
            debugger;
            $timeout(function () {
                $scope.materialsPrices = materialhistory;
            }, 0);
            jQuery("#frmHictoryPrice").modal();
        },

        changePrice: function (item) {
            debugger;
            if (item.oldPrice !== item.price || item.oldQty !== item.qty || item.oldStatusID !== item.statusID || item.oldValidFrom !== item.validFrom || item.oldRemark !== item.remark || item.oldAttachFile !== item.attachFile) {
                item.isChange = true;
            }
        },
        selectedProductionItem: function (data, item) { 
            if (data !== null) {
                var isExist = $scope.method.isExistProductionItemProduction(data.productionItemID);

                if (!isExist) {
                    item.productionItemID = data.productionItemID;
                    item.productionItemUD = data.productionItemUD;
                    item.productionItemNM = data.productionItemNM;
                    item.unitID = data.unitID;
                    item.units = data.units;
                } else {
                    jsHelper.showMessage('warning', 'ProductionItem ' + data.productionItemNM + ' is existed!');
                    return;
                }
            }
            $scope.$apply();
        },

        deleteItem: function () {

            dataService.delete(
                context.id,
                null,
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
        },

        edit: function (item) {
            item.isEditItem = true;
        },


        ChangeNameproduct: function (item) {

            var a = document.getElementById("productionname").value;
            angular.forEach($scope.data.materialsPrices, function (data) {
                if (item.productionItemID == data.productionItemID) {
                    if (a != data.productionItemNM) {
                        item.productionItemUD = null;
                        item.units = null;

                    }
                }
                this.addnewMaterial();
            });
        },

        //
        // BUSINESS CARD
        //

        addCard: function () {
            var newCard = {
                factoryRawMaterialBusinessCardID: $scope.method.getNewID(),
                factoryRawMaterialID: 0,
                frontFileUD: null,
                behindFileUD: null,
                description: null,
                remark: null,

                frontFriendlyName: null,
                frontThumbnailLocation: null,
                frontFileLocation: null,
                frontHasChange: false,
                frontNewFile: null,

                behindFriendlyName: null,
                behindThumbnailLocation: null,
                behindFileLocation: null,
                behindHasChange: false,
                behindNewFile: null,
                controllerID: null,
                controllerName: null,
                otherName: null

            };
            $scope.event.editCard(newCard);
        },
        editCard: function (item) {
            $scope.currentCard = JSON.parse(JSON.stringify(item));
            jQuery("#cardForm").modal();
        },
        removeCard: function (item) {
            if (confirm('Are you sure ?')) {
                $scope.data.factoryRawMaterialBusinessCardDTO.splice($scope.data.factoryRawMaterialBusinessCardDTO.indexOf(item), 1);
            }
        },
        updateCard: function () {
            debugger;
            var index = $scope.method.getCardIndex($scope.currentCard.factoryRawMaterialBusinessCardID);
            if (index >= 0) {
                $scope.data.factoryRawMaterialBusinessCardDTO[index] = $scope.currentCard;
            }
            else {
                debugger;
                $scope.data.factoryRawMaterialBusinessCardDTO.push($scope.currentCard);
            }
            jQuery("#cardForm").modal('hide');

            $scope.method.renderCard();
        },
        addPreviewCardImage: function (pos) {
            if (pos === 1) {
                masterUploader.multiSelect = true;
                masterUploader.imageOnly = false;
                masterUploader.callback = function () {
                    var scope = angular.element(jQuery('body')).scope();
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            // TO DO LIST
                            scope.currentCard.frontHasChange = true;
                            scope.currentCard.frontThumbnailLocation = img.fileURL;
                            scope.currentCard.frontFileLocation = img.fileURL;
                            scope.currentCard.frontNewFile = img.filename;
                        }, null);
                    });
                };
                masterUploader.open();
            }
            if (pos === 2) {
                masterUploader.multiSelect = true;
                masterUploader.imageOnly = false;
                masterUploader.callback = function () {
                    var scope = angular.element(jQuery('body')).scope();
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            // TO DO LIST
                            scope.currentCard.behindHasChange = true;
                            scope.currentCard.behindThumbnailLocation = img.fileURL;
                            scope.currentCard.behindFileLocation = img.fileURL;
                            scope.currentCard.behindNewFile = img.filename;
                        }, null);
                    });
                };
                masterUploader.open();
            }
        },

        changeController: function (controllerID) {
            if ($scope.currentCard.controllerID) {
                angular.forEach($scope.users, function (item) {
                    if (item.userID == $scope.currentCard.controllerID) {
                        $scope.currentCard.controllerName = item.employeeNM + ' (' + item.internalCompanyNM + ')';
                    }
                });
            }
            else {
                $scope.currentCard.controllerName = '';
            }
        },
        //upload video
        uploadVideo: function () {
            //return;

            userFileMng.callback = function () {
                scope.$apply(function () {
                    // TO DO LIST
                    angular.forEach(userFileMng.selectedFiles, function (value, key) {
                        debugger;
                        scope.data.factoryRawMaterialGalleryDTO.push({
                            factoryRawMaterialGalleryID: scope.method.getNewID(),
                            factoryRawMaterialGalleryUD: '',
                            factoryRawMaterialID: context.id,
                            factoryGalleryHasChange: true,
                            factoryGalleryNewFile: value.fileName,
                            factoryGalleryThumbnail: context.backendUrl + 'avi.mp4'
                        });
                    });
                });
            };
            userFileMng.open();
        },

        removeGalleryItem: function (id) {
            //return;
            if (confirm('Are you sure ?')) {
                isExist = false;
                for (j = 0; j < $scope.data.factoryRawMaterialGalleryDTO.length; j++) {
                    if ($scope.data.factoryRawMaterialGalleryDTO[j].factoryRawMaterialGalleryID == id) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    $scope.data.factoryRawMaterialGalleryDTO.splice(j, 1);
                }
            }
        },
    
        changeLogo: function ($event) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.logoFileLocation = img.fileURL;
                        scope.data.logoThumbnailLocation = img.fileURL;
                        scope.data.logoFile_NewFile = img.filename;
                        scope.data.logoFile_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeLogo: function ($event) {
            $scope.data.logoFileLocation = '';
            $scope.data.logoThumbnailLocation = '';
            $scope.data.logoFile_NewFile = '';
            $scope.data.logoFile_HasChange = true;
        },


        addPricing: function () {
            jQuery('#pricingForm').modal('show');
        },


        addQuality: function () {
            jQuery('#qualityForm').modal('show');
        },

        addDirector: function () {
            jQuery('#directorForm').modal('show');
        },

        addManager: function () {
            jQuery('#managerForm').modal('show');
        },


        addSample: function () {
            jQuery('#sampleForm').modal('show');
        },

        updatePricing: function (item) {
            $scope.data.factoryRawMaterialPricingPersons = [];
            var selectedPricing = $('#pricingID').select2('data');
            angular.forEach(selectedPricing, function (item) {
                $scope.data.factoryRawMaterialPricingPersons.push({
                    factoryRawMaterialPricingPersonID: $scope.method.getNewID(),
                    employeeID: item.id,
                    employeeNM: item.text
                });
            });
            jQuery('#pricingForm').modal('hide');
        },

        updateQuality: function (item) {
            $scope.data.factoryRawMaterialQualityPersons = [];
            var selectedQuality = $('#qualityID').select2('data');
            angular.forEach(selectedQuality, function (item) {
                $scope.data.factoryRawMaterialQualityPersons.push({
                    factoryRawMaterialQualityPersonID: $scope.method.getNewID(),
                    employeeID: item.id,
                    employeeNM: item.text
                });
            });
            jQuery('#qualityForm').modal('hide');
        },

        updateDirector: function (item) {

            var directorSelected = $('#directorID').select2('data');
            angular.forEach(directorSelected, function (item) {
                $scope.data.supplierDirectors.push({
                    supplierDirectorID: $scope.method.getNewID(),
                    employeeID: item.id,
                    employeeNM: item.text,
                    isReceivePriceRequest: item.selected
                });
            });
            jQuery('#directorForm').modal('hide');
        },

        updateManager: function (item) {

            var selectedManager = $('#managerID').select2('data');
            angular.forEach(selectedManager, function (item) {
                $scope.data.supplierManagers.push({
                    supplierManagerID: $scope.method.getNewID(),
                    employeeID: item.id,
                    employeeNM: item.text,
                    isReceivePriceRequest: item.selected
                });
            });
            jQuery('#managerForm').modal('hide');
        },

        updateSample: function (item) {

            var sampleSelected = $('#sampleID').select2('data');
            angular.forEach(sampleSelected, function (item) {
                $scope.data.supplierSampleTechnicals.push({
                    supplierSampleTechnicalID: $scope.method.getNewID(),
                    employeeID: item.id,
                    employeeNM: item.text,
                    isReceivePriceRequest: item.selected
                });
            });
            jQuery('#sampleForm').modal('hide');
        },


        removePricing: function (item) {
            $scope.data.factoryRawMaterialPricingPersons.splice($scope.data.factoryRawMaterialPricingPersons.indexOf(item), 1);
        },

        removeQuality: function (item) {
            $scope.data.factoryRawMaterialQualityPersons.splice($scope.data.factoryRawMaterialQualityPersons.indexOf(item), 1);
        },


        addTest: function ($event) {
            $event.preventDefault();
            $scope.data.factoryRawMaterialTests.push({
                factoryRawMaterialTestID: $scope.method.getNewID(),
            });
        },
        removeTest: function ($event, index) {
            $event.preventDefault();
            if (confirm('Are you sure ?')) {
                $scope.data.factoryRawMaterialTests.splice(index, 1);
            }
        },


        addPaymentTerm: function ($event) {
            $event.preventDefault();
            $scope.data.factoryRawPaymentTerms.push({
                factoryRawMaterialPaymentTermID: $scope.method.getNewID(),
            });
        },
        removePaymentTerm: function ($event, index) {
            $event.preventDefault();
            if (confirm('Are you sure ?')) {
                $scope.data.factoryRawPaymentTerms.splice(index, 1);
            }
        },

        existSupplier: function (item) {
            for (var i = 0; i < $scope.data.factoryRawPaymentTerms.length; i++) {
                var factoryRawPayment = $scope.data.factoryRawPaymentTerms[i];
                if (item.supplierPaymentTermID === null) {
                    return;
                }
                else {
                    if (factoryRawPayment.supplierPaymentTermID == item.supplierPaymentTermID && factoryRawPayment.factoryRawMaterialPaymentTermID !== item.factoryRawMaterialPaymentTermID) {
                        bootbox.alert('This Supplier Payment Term already exist in list Payment Term.');
                        item.supplierPaymentTermID = null;
                    }
                }
            }
        },

        addCertificate: function ($event) {
            $event.preventDefault();
            $scope.data.factoryRawMaterialCertificates.push({
                factoryRawMaterialCertificateID: $scope.method.getNewID(),
            });
        },
        removeCertificate: function ($event, index) {
            $event.preventDefault();
            if (confirm('Are you sure ?')) {
                $scope.data.factoryRawMaterialCertificates.splice(index, 1);
            }
        },
        changeCertificateFile: function ($event, controlID, typeID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        var arr = scope.data.factoryRawMaterialCertificates.filter(function (o) { return o.factoryRawMaterialCertificateID == controlID });
                        $(arr).each(function () {
                            this.certificateFileUrl = img.fileURL;
                            this.certificateFileFriendlyName = img.filename;
                            this.certificateFileHasChange = true;
                            this.newCertificateFile = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeCertificateFile: function ($event, controlID, typeID) {
            var arr = scope.data.factoryRawMaterialCertificates.filter(function (o) { return o.factoryRawMaterialCertificateID == controlID });
            $(arr).each(function () {
                this.certificateFileUrl = '';
                this.certificateFileFriendlyName = '';
                this.certificateFileHasChange = true;
                this.newCertificateFile = '';
            });
        },
        changeAttachFile: function ($event, controlID) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        var arr = scope.data.materialsPrices.filter(function (o) { return o.materialsPriceID == controlID });
                        $(arr).each(function () {
                            this.attachFileURL = img.fileURL;
                            this.attachFileFriendlyName = img.filename;
                            this.attachFileHasChange = true;
                            this.newAttachFile = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },

        removeAttachFile: function ($event, controlID) {
            var arr = $scope.data.materialsPrices.filter(function (o) { return o.materialsPriceID == controlID });
            $(arr).each(function () {
                this.attachFileURL = '';
                this.attachFileFriendlyName = '';
                this.attachFileHasChange = true;
                this.newAttachFile = '';
            });
        },
        ChangeRawList: function () {
            $scope.tmpKeyRawMaterials = angular.copy($scope.keyRawMaterials);
            jQuery('#newKeyForm').modal();
        },
        addRaw: function ($event) {
            $event.preventDefault();
            $scope.tmpKeyRawMaterials.push({
                keyRawMaterialID: $scope.method.getNewID(),
                keyRawMaterialNM: ''
            });
        },
        removeRaw: function ($event, index, item) {
            $event.preventDefault();
            if (item.keyRawMaterialID > 0) {
                if (confirm('Are you sure ?')) {
                    dataService.removeRaw(
                      item,
                      function (data) {
                          if (data.data == 0) {
                              alert('Option was selected! Can not be deleted!');
                              return false;
                          } else {
                              $scope.tmpKeyRawMaterials.splice(index, 1);
                          }
                      },
                      function (error) {
                          jsHelper.showMessage('warning', error);
                      }
                  );
                }
            } else {
                $scope.tmpKeyRawMaterials.splice(index, 1);
            }
        },
        updateRawList: function (item) {
            dataService.updateRawList(
                item,
                function (data) {
                    var result = data.data;
                    $scope.keyRawMaterials = result;
                    jQuery('#newKeyForm').modal('hide');
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        addImage: function () {
            var newImage = {
                factoryImageID: $scope.method.getNewID(),
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
            var index = $scope.method.getImageIndex($scope.currentImage.factoryRawMaterialImageID)
            if (index >= 0) {
                $scope.data.factoryRawMaterialImages[index] = $scope.currentImage;
            }
            else {
                $scope.data.factoryRawMaterialImages.push($scope.currentImage);
            }
            jQuery("#imageForm").modal('hide');

            $scope.method.renderImage();
        },

        removeImage: function (item) {
            if (confirm('Are you sure ?')) {
                $scope.data.factoryRawMaterialImages.splice($scope.data.factoryRawMaterialImages.indexOf(item), 1);
            }
        },

        addPreviewImage: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.currentImage.hasChange = true;
                        scope.currentImage.thumbnailLocation = img.fileURL;
                        scope.currentImage.fileLocation = img.fileURL;
                        scope.currentImage.newFile = img.filename;
                    }, null);
                });
            };
            masterUploader.open();
        },

        addDocument: function ($event) {
            $event.preventDefault();

            var arr = $scope.data.subSupplierContracts.filter(function (o) { return o.subSupplierContractID < 0 });
            max_id = -1;

            if (arr.length > 0) {
                arr.sort(function (a, b) { return a.subSupplierContractID - b.subSupplierContractID });
                max_id = arr[0].subSupplierContractID - 1;
            }
            $scope.data.subSupplierContracts.push({
                subSupplierContractID: max_id,
            });
        },

        removeMaterial: function ($event, materialsPriceID) {
            $event.preventDefault();
            isExist = false;
            for (j = 0; j < $scope.data.materialsPrices.length; j++) {
                if ($scope.data.materialsPrices[j].materialsPriceID == materialsPriceID) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.data.materialsPrices.splice(j, 1);
            }
        },

        cannelMaterial: function ($event, materialsPriceID) {
            $event.preventDefault();
            isExist = false;
            for (j = 0; j < $scope.data.materialsPrices.length; j++) {
                if ($scope.data.materialsPrices[j].materialsPriceID == materialsPriceID) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.data.materialsPrices.splice(j, 1);
            }
        },

        addnewMaterial: function ($event, item) {
            debugger;
            var date = new Date();
            $scope.data.materialsPrices.push({
                materialsPriceID: $scope.method.getNewID(),
                statusID: 1,
                isEditItem: true,
                isEditProductNM: true,
                isEditProductUD: true,
                isEdiuUnit: true,
                productionItemUD: null,
                productionItemNM: null,
                qty: 1,
                productionItemTypeNM: 'Material',
                unitID: null,
                price: null,
                validFrom: date.getDate() + '/' +(date.getMonth()+1)+ '/' + date.getFullYear(),
                materialPriceHistories: [],
            });

        },
        removeContract: function ($event, subSupplierContractID) {
            $event.preventDefault();
            isExist = false;
            for (j = 0; j < $scope.data.subSupplierContracts.length; j++) {
                if ($scope.data.subSupplierContracts[j].subSupplierContractID == subSupplierContractID) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.data.subSupplierContracts.splice(j, 1);
            }
        }
    };


    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        },
        getImageIndex: function (id) {
            var isExist = false;
            var index = 0;
            for (var index = 0; index < $scope.data.factoryRawMaterialImages.length; index++) {
                if ($scope.data.factoryRawMaterialImages[index].factoryRawMaterialImageID == id) {
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
            angular.forEach($scope.data.factoryRawMaterialImages, function (value, key) {
                if ($scope.images.indexOf(value.draw) < 0) {
                    $scope.images.push(value.draw);
                }
            }, null);
        },
        isExistProductionItemProduction: function (productionItemID) {
            var isExist = false;
            if ($scope.data != null) {
                if ($scope.data.materialsPrices.length == 0) {
                    return isExist;
                }

                angular.forEach($scope.data.materialsPrices, function (item) {
                    if (item.productionItemID == productionItemID) {
                        isExist = true;
                    }
                });
                return isExist;
            }
        },
        //Card
        getCardIndex: function (id) {
            debugger;
            var isExist = false;
            var cardIndex = 0;
            if ($scope.data.factoryRawMaterialBusinessCardDTO !== null) {
                for (cardIndex; cardIndex < $scope.data.factoryRawMaterialBusinessCardDTO.length; cardIndex++) {
                    if ($scope.data.factoryRawMaterialBusinessCardDTO[cardIndex].factoryRawMaterialBusinessCardID === id) {
                        isExist = true;
                        break;
                    }
                }

                if (!isExist) {
                    return -1;
                }
                else {
                    return cardIndex;
                }
            }
            else {
                return -1;
            }
        },
        renderCard: function () {
            $scope.cards = [];
            angular.forEach($scope.data.factoryRawMaterialBusinessCardDTO, function (value, key) {
                if ($scope.cards.indexOf(value.draw) < 0) {
                    $scope.cards.push(value.draw);
                }
            }, null);
        }

    };

    $scope.changeFile = function ($event, controlID) {
        $event.preventDefault();
        userFileMng.isMultiSelectActivated = false;
        userFileMng.autoResizeImage = false;
        userFileMng.callback = function () {
            scope.$apply(function () {
                var d = new Date();
                var n = d.getMilliseconds();

                if (userFileMng.selectedFiles.length > 0) {
                    console.log(userFileMng.selectedFiles[0]);
                    var arr = $scope.data.subSupplierContracts.filter(function (o) { return o.subSupplierContractID == controlID });
                    $(arr).each(function () {
                        this.contractFileURL = (userFileMng.selectedFiles[0].filePath.indexOf('?') < 0) ? userFileMng.selectedFiles[0].filePath + '?' + n : userFileMng.selectedFiles[0].filePath + n;
                        this.contractFileFriendlyName = userFileMng.selectedFiles[0].fileName;
                        this.contractFileHasChange = true;
                        this.newContractFile = userFileMng.selectedFiles[0].fileName;
                    });
                }
            });
        };
        userFileMng.open();
    },

        $scope.removeFile = function ($event, controlID) {
            var arr = $scope.data.subSupplierContracts.filter(function (o) { return o.subSupplierContractID == controlID });
            $(arr).each(function () {
                this.contractFileURL = '';
                this.contractFileFriendlyName = '';
                this.contractFileHasChange = true;
                this.newContractFile = '';
            });
        }


    $scope.event.init();
}]);
