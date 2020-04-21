//jQuery("#content").show();

(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive']).controller('tilsoftController', specificationOfProductMngController);
    specificationOfProductMngController.$inject = ['$scope', 'dataService'];

    function specificationOfProductMngController($scope, dataService) {
        $scope.data = [];
        $scope.newID = -1;
        $scope.packingSpecification = [];

        //Weaving File
        $scope.currenweavingfile = null;
        $scope.weavingfiles = [];
        $scope.specOfProductList = [];
        $scope.specOfProductCopy = [];

        $scope.event = {
            get: get,
            remove: remove,
            update: update,

            //Event Weaving File
            addWeavingFile: addWeavingFile,
            editWeavingFile: editWeavingFile,
            updatedWeavingFile: updatedWeavingFile,
            removedWeavingFile: removedWeavingFile,
            addWeavingScanFile: addWeavingScanFile,

            //Event Upload Image
            uploadSpecificationImage: uploadSpecificationImage,
            removeSpecificationImage: removeSpecificationImage,
            uploadSpecificationCushionImage: uploadSpecificationCushionImage,
            removeSpecificationCushionImage: removeSpecificationCushionImage,

            //ExportExcel
            exportExcel: exportExcel,
            getListSpec: getListSpec,
            cancel: cancel,
            copySpec: copySpec,
            fillData: fillData

        };

        $scope.method = {
            refresh: refresh,
            getNewID: getNewID,
        };


        //Function Start

        function get() {
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
            dataService.supportServiceUrl = context.supportServiceUrl;

            dataService.getDataWithSample(
                context.id,
                context.sampleProductID,
                context.productID,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.packingSpecification = data.data.packingSpecificationDTOs;
                    jQuery('#content').show();

                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function remove(id) {
            dataService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    //not thing to do
                });
        };

        function update($event) {
            for (var i = 0; i < $scope.data.specificationPackingDTOs.length; i++) {
                var item = $scope.data.specificationPackingDTOs[i];

                //if (item.isSelected) {
                item.productSpecificationPackingID = $scope.method.getNewID();
                //}
                //var newItem = {
                //    productSpecificationPackingID: $scope.method.getNewID(),
                //    packingSpecificationID: item.packingSpecificationID,
                //    isSelected: item.isSelected
                //}

                //$scope.data.specificationPackingDTOs.push(newItem);

            }

            dataService.update(
                context.id,
                $scope.data,
                function (data) {
                    jsHelper.processMessage(data.message);
                    window.location = context.refreshUrl + data.data.productSpecificationID;
                    $scope.data = data.data;
                },
                function (error) {
                    //do not need do anything
                });
        };

        //Processing Upload Weaving File
        function addWeavingFile() {
            var newWeavingFile = {
                productSpecificationWeavingFileID: $scope.method.getNewID(),
                scanFile: null,
                scanFileLocation: null,
                friendlyName: null,
                scanHasChange: false,
                scanNewFile: null,
            };

            $scope.event.editWeavingFile(newWeavingFile)
            //$scope.data.specificationWeavingFileDTOs.push(newWeavingFile);
        };

        function editWeavingFile(item) {
            $scope.currenweavingfile = item;
            jQuery("#weavingFileForm").modal();
        };

        function updatedWeavingFile(item) {
            var index = $scope.data.specificationWeavingFileDTOs.indexOf(item);
            $scope.data.specificationWeavingFileDTOs[index] = item;
            $scope.data.specificationWeavingFileDTOs.push(item);
            jQuery("#weavingFileForm").modal('hide');

        };

        function removedWeavingFile(item) {
            if (confirm("Are you Sure Delete this Data ?")) {
                $scope.data.specificationWeavingFileDTOs.splice($scope.data.specificationWeavingFileDTOs.indexOf(item), 1)
            }
        };

        function addWeavingScanFile() {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.currenweavingfile.scanHasChange = true;
                        scope.currenweavingfile.scanFileLocation = img.fileURL;
                        scope.currenweavingfile.friendlyName = img.filename;
                        scope.currenweavingfile.scanNewFile = img.filename;
                    }, null);
                });
            };
            masterUploader.open();
        };
        //End Upload WeavingFile

        //Upload Image SpecificationImage
        function uploadSpecificationImage() {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        // TO DO LIST
                        scope.data.specificationImageDTOs.push({
                            productSpecificationImageID: scope.method.getNewID(),
                            fileUD: '',
                            scanHasChange: true,
                            scanNewFile: img.filename,
                            scanFileLocation: img.fileURL
                        });
                    }, null);
                });
            };
            masterUploader.open();
        };

        function removeSpecificationImage(item) {
            if (confirm("Are you Sure Delete this Image ?")) {
                $scope.data.specificationImageDTOs.splice($scope.data.specificationImageDTOs.indexOf(item), 1)
            }
        };

        //Upload Image SpecificationCushionImage
        function uploadSpecificationCushionImage() {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        // TO DO LIST
                        scope.data.specificationCushionImageDTOs.push({
                            productSpecificationCushionImageID: scope.method.getNewID(),
                            fileUD: '',
                            scanHasChange: true,
                            scanNewFile: img.filename,
                            scanFileLocation: img.fileURL
                        });
                    }, null);
                });
            };
            masterUploader.open();
        };

        function removeSpecificationCushionImage(item) {
            if (confirm("Are you Sure Delete this Image ?")) {
                $scope.data.specificationCushionImageDTOs.splice($scope.data.specificationCushionImageDTOs.indexOf(item), 1)
            }
        };

        //  Export ExCel
        function exportExcel() {
            dataService.exportExcel(
                $scope.data.productSpecificationID,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        // Get list Spec
        function getListSpec() {
            dataService.getListSpec(
                $scope.data.modelID,
                function (data) {
                    $scope.specOfProductList = data.data.data;
                    $scope.$apply();
                    $('#moreSpec').modal();
                },
                function (error) {
                    //jsHelper.showMessage('warning', error);
                });
        };

        function cancel() {
            $('#moreSpec').modal('hide');
        };

        //copy spec
        function copySpec(productSpecificationID) {
            dataService.copySpec(
                productSpecificationID,
                function (data) {
                    $scope.specOfProductCopy = data.data;
                    //$scope.$apply();

                    $scope.event.fillData();
                    $scope.$apply();
                    $('#moreSpec').modal('hide');
                },

                function (error) {

                });
        };

        function fillData() {
            debugger
            $scope.data.productOverallDimL = $scope.specOfProductCopy.productOverallDimL,
                $scope.data.productOverallDimW = $scope.specOfProductCopy.productOverallDimW,
                $scope.data.productOverallDimH = $scope.specOfProductCopy.productOverallDimH,
                $scope.data.productOverallWeight = $scope.specOfProductCopy.productOverallWeight,
                $scope.data.frameTDCode = $scope.specOfProductCopy.frameTDCode,
                $scope.data.frameDimensionL = $scope.specOfProductCopy.frameDimensionL,
                $scope.data.frameDimensionW = $scope.specOfProductCopy.frameDimensionW,
                $scope.data.frameDimensionH = $scope.specOfProductCopy.frameDimensionH,
                $scope.data.frameMaterial = $scope.specOfProductCopy.frameMaterial,
                $scope.data.frameColor = $scope.specOfProductCopy.frameColor,
                $scope.data.frameColorCode = $scope.specOfProductCopy.frameColorCode,
                $scope.data.frameWelding = $scope.specOfProductCopy.frameWelding,
                $scope.data.frameWeight = $scope.specOfProductCopy.frameWeight,
                $scope.data.frameCoatingColor = $scope.specOfProductCopy.frameCoatingColor,
                $scope.data.woodenPartTDCode = $scope.specOfProductCopy.woodenPartTDCode,
                $scope.data.woodenPartDimensionL = $scope.specOfProductCopy.woodenPartDimensionL,
                $scope.data.woodenPartDimensionW = $scope.specOfProductCopy.woodenPartDimensionW,
                $scope.data.woodenPartDimensionH = $scope.specOfProductCopy.woodenPartDimensionH,
                $scope.data.woodenPartSpecies = $scope.specOfProductCopy.woodenPartSpecies,
                $scope.data.woodenPartColor = $scope.specOfProductCopy.woodenPartColor,
                $scope.data.woodenPartSnail = $scope.specOfProductCopy.woodenPartSnail,
                $scope.data.wickerColorCode = $scope.specOfProductCopy.wickerColorCode,
                $scope.data.wickerType = $scope.specOfProductCopy.wickerType,
                $scope.data.wickerColor = $scope.specOfProductCopy.wickerColor,
                $scope.data.wickerWeight = $scope.specOfProductCopy.wickerWeight,
                $scope.data.weavingMethod = $scope.specOfProductCopy.weavingMethod,
                $scope.data.textilenDimensionL = $scope.specOfProductCopy.textilenDimensionL,
                $scope.data.textilenDimensionW = $scope.specOfProductCopy.textilenDimensionW,
                $scope.data.textilenColor = $scope.specOfProductCopy.textilenColor,
                $scope.data.cushionColor = $scope.specOfProductCopy.cushionColor,
                $scope.data.cushionWeight = $scope.specOfProductCopy.cushionWeight,
                $scope.data.cushionDimensionL = $scope.specOfProductCopy.cushionDimensionL,
                $scope.data.cushionDimensionW = $scope.specOfProductCopy.cushionDimensionW,
                $scope.data.cushionDimensionH = $scope.specOfProductCopy.cushionDimensionH,
                $scope.data.cushionLine = $scope.specOfProductCopy.cushionLine,
                $scope.data.cushionWashingLabelDimL = $scope.specOfProductCopy.cushionWashingLabelDimL,
                $scope.data.cushionWashingLabelDimW = $scope.specOfProductCopy.cushionWashingLabelDimW,
                $scope.data.cartonBoxTDCode = $scope.specOfProductCopy.cartonBoxTDCode,
                $scope.data.cartonBoxType = $scope.specOfProductCopy.cartonBoxType,
                $scope.data.cartonBoxDimensionL = $scope.specOfProductCopy.cartonBoxDimensionL,
                $scope.data.cartonBoxDimensionW = $scope.specOfProductCopy.cartonBoxDimensionW,
                $scope.data.cartonBoxDimensionH = $scope.specOfProductCopy.cartonBoxDimensionH,
                $scope.data.hardwareBoltQuantity = $scope.specOfProductCopy.hardwareBoltQuantity,
                $scope.data.hardwareBoltDimension = $scope.specOfProductCopy.hardwareBoltDimension,
                $scope.data.hardwareKeyDimensionL = $scope.specOfProductCopy.hardwareKeyDimensionL,
                $scope.data.hardwareKeyDimensionW = $scope.specOfProductCopy.hardwareKeyDimensionW,
                $scope.data.hardwareKeyThickness = $scope.specOfProductCopy.hardwareKeyThickness,
                $scope.data.hardwareRing = $scope.specOfProductCopy.hardwareRing,
                $scope.data.hardwareSpring = $scope.specOfProductCopy.hardwareSpring,
                $scope.data.hardwareConnector = $scope.specOfProductCopy.hardwareConnector,
                $scope.data.hardwareLeveller = $scope.specOfProductCopy.hardwareLeveller,
                $scope.data.assemblyInstructionCode = $scope.specOfProductCopy.assemblyInstructionCode,
                $scope.data.otherDescription = $scope.specOfProductCopy.otherDescription,
                $scope.data.remark = $scope.specOfProductCopy.remark,

                //$scope.data.specificationCushionImageDTOs = $scope.specOfProductCopy.specificationCushionImageDTOs,
                //angular.forEach($scope.data.specificationCushionImageDTOs, function (item) {
                //    item.productSpecificationCushionImageID = $scope.method.getNewID(),
                //        item.fileUD = "",
                //        item.scanHasChange = true,
                //        item.productSpecificationID = null

                //});

                //$scope.data.specificationImageDTOs = $scope.specOfProductCopy.specificationImageDTOs,
                //    angular.forEach($scope.data.specificationImageDTOs, function (item3) {
                //         item3.productSpecificationImageID = $scope.method.getNewID(),
                //        item3.scanHasChange = true
                //});

                $scope.data.specificationPackingDTOs = $scope.specOfProductCopy.specificationPackingDTOs,
                angular.forEach($scope.data.specificationPackingDTOs, function (item) {
                    item.productSpecificationPackingID = 0,
                        item.productSpecificationID = null
                });

            //$scope.data.specificationWeavingFileDTOs = $scope.specOfProductCopy.specificationWeavingFileDTOs,
            //    angular.forEach($scope.data.specificationWeavingFileDTOs, function (item4) {
            //        item4.productSpecificationWeavingFileID = $scope.method.getNewID(),
            //            item4.scanHasChange = true
            //    });

            //$scope.data.specificationWoodenartDTOs = $scope.specOfProductCopy.specificationWoodenartDTOs,
            //    angular.forEach($scope.data.specificationWoodenartDTOs, function (item2) {
            //        item2.productSpecificationWoodenPartID = $scope.method.getNewID(),
            //            item2.productSpecificationID = null
            //    });
            // $scope.data.packingSpecificationDTOs = $scope.specOfProductCopy.packingSpecificationDTOs


        };

        //Start Method
        function refresh() {
            window.location = context.refreshUrl + id
        };

        function getNewID() {
            $scope.newID--;
            return $scope.newID;
        };

        $scope.event.get();

        function createservices() {
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
            dataService.supportServiceUrl = context.supportServiceUrl;

        };
    };
})();