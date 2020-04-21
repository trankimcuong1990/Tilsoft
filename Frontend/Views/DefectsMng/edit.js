(function () {
    "use strict";

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', DefectsController);
    DefectsController.$inject = ['$scope', 'dataService'];

    function DefectsController($scope, defectService) {
        $scope.data = [];
        $scope.defetcsDTO = [];

        $scope.event = {
            activepage: activepage,
            updatedata: updatedata,
            deletedata: deletedata,
            getInit: getInit,
            addImage: addImage,
            addFile: addFile,
            removeImage: removeImage
            };
        $scope.event.getInit();
        $scope.event.activepage();

        function getInit() {
            createservices();          
            defectService.getInit(
                function (data) {

                    $scope.defectsGroups = data.data.defectsGroups;
                    $scope.typeOfDefectDTO = data.data.typeOfDefectDTO;
                    jQuery("#content").show();
                },
                function (error) {
                    $scope.defectsGroups = [];
                    $scope.typeOfDefectDTO = [];
                });


        }

        function activepage() {
            createservices();
            defectService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.defectsDTO;
                    jQuery("#content").show();
                },
                function (error) {
                    $scope.data = [];
                });
        }

        function updatedata($event) {
            $event.preventDefault();
            if ($scope.editForm.$valid) {
                defectService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.defectsDTO.defectID;
                            $scope.data = data.data;
                        }
                    },
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (context.id === 0) {
                            //refresh page
                            var id = data.data.defectID;
                            window.location = context.refreshUrl + id + '#/';
                        }
                        else {
                            //refresh data
                            $scope.data = data.data;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage("warning", error);
                    });
            } else {
                jsHelper.showMessage("warning", context.msgValid);
            }
        }

        function deletedata(id) {
            defectService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    jsHelper.showMessage("warning", error);
                });
        }

        function createservices() {
            defectService.serviceUrl = context.serviceUrl;
            defectService.token = context.token;
        }

        /*
         Image
         */
        //removeFile: function () {
        //    $scope.data.friendlyName = '';
        //    $scope.data.fileLocation = '';
        //    $scope.data.BLFile_NewFile = '';
        //    $scope.data.BLFile_HasChanged = true;
        //},

        function addImage() {
            var tempID = null;
            tempID = defectService.getIncrementId();
           $scope.data.defectsImageDTOs.push({
                defectImageID: tempID,
                fileUD: '',
                remark: '',
                fileLocation: '',
                thumbnailLocation: '',
                friendlyName: '',
                scanFileLocation: '',
                scanHasChange: null,
                scanNewFile: ''
            });
            addFile(tempID);
        }

        function addFile(id) {
            console.log(id);
            if ($scope.data !== null && $scope.data.defectsImageDTOs !== null) {
                angular.forEach($scope.data.defectsImageDTOs, function (item) {
                    if (item.defectImageID === id) {
                        fileUploader2.callback = function () {
                            scope.$apply(function () {
                                item.friendlyName = fileUploader2.selectedFriendlyName;
                                item.fileLocation = fileUploader2.selectedFileUrl;
                                item.thumbnailLocation = fileUploader2.selectedFileUrl;
                                item.scanNewFile = fileUploader2.selectedFileName;
                                item.scanHasChange = true;
                            });
                        };
                        fileUploader2.open();
                    }
                });
            }
        }

        function removeImage(item) {
            $scope.data.defectsImageDTOs.splice($scope.data.defectsImageDTOs.indexOf(item), 1);
        }
    }

})();
