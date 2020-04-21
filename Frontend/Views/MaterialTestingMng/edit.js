// formatData
// using for auto-complate support quicksearch.
function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.label,
            description: item.label
        }
    });
}

(function () {
    'use strict';

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', materialTestingController);
    materialTestingController.$inject = ['$scope', 'dataService'];

    function materialTestingController($scope, materialTestingService) {
        $scope.data = [];
        $scope.searchString = null;
        $scope.IDX = 0;
        $scope.getIDX = 0;
        $scope.newid = 0;

        $scope.materialTestStandards = [];


        // variables for event quick-search with client.      
        $scope.ngRequestClient = {
            url: context.serviceUrl + 'getClient',
            token: context.token,
        };
        $scope.client = {
            id: null,
            label: null,
            description: null
        };

        // variables for event quick-search with Material Option.      
        $scope.ngRequestMaterialOption = {
            url: context.serviceUrl + 'getMaterialOption',
            token: context.token,
        };
        $scope.materialOption = {
            id: null,
            label: null,
            description: null
        };

        $scope.event = {
            init: init,
            get: get,
            update: update,
            del: del,
            selectMaterialOption: selectMaterialOption,
            selectClient: selectClient,

            addTestfile: addTestfile,
            editTestfile: editTestfile,
            updateTestfile: updateTestfile,
            removeTestfile: removeTestfile,
            addTestScanFile: addTestScanFile,

            addTestStandard: addTestStandard,
            removeTestStandard: removeTestStandard,
        };

        $scope.method = {
            getNewID: getNewID,
            gettdfileIndex: gettdfileIndex,
            rendertdfile: rendertdfile,

        };

        function init() {
            materialTestingService.serviceUrl = context.serviceUrl;
            materialTestingService.supportServiceUrl = context.supportServiceUrl;
            materialTestingService.token = context.token;

            $scope.event.get();
        };

        function get() {
            materialTestingService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.materialTestStandards = data.data.supportMaterialTestStandards;
                    jQuery('#content').show();

                    $('#client').autocomplete({
                        source: function (request, response) {

                            $.ajax({
                                cache: false,
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json',
                                    'Authorization': 'Bearer ' + context.token
                                },
                                type: "POST",
                                data: JSON.stringify({
                                    filters: {
                                        searchQuery: request.term
                                    }
                                }),
                                dataType: 'json',
                                url: context.supportServiceUrl + 'getClient',
                                beforeSend: function () {

                                    jsHelper.loadingSwitch(true);
                                },
                                success: function (data) {

                                    jsHelper.loadingSwitch(false);

                                    response(data.data);
                                },
                                error: function (xhr, ajaxOptions, thrownError) {

                                    jsHelper.loadingSwitch(false);
                                    errorCallBack(xhr.responseJSON.exceptionMessage);
                                }
                            });
                        },
                        minLength: 3,
                        select: function (event, ui) {
                            $scope.data.clientID = ui.item.id;
                            $scope.data.clientName = ui.item.label;
                            console.log(ui.item.email);
                            $scope.$apply();
                        }
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function update() {
            if ($scope.editForm.$valid) {
                materialTestingService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            debugger
                            window.location = context.refreshUrl + data.data.materialTestReportID;
                            $scope.data = data.data;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            } else {
                jsHelper.showMessage('warning', context.msgValid);
            }
        };

        function del(id) {
            materialTestingService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function selectMaterialOption(materialOption) {
            $scope.data.materialOptionID = materialOption.id,
                $scope.data.materialOptionUD = materialOption.value,
                $scope.data.materialOptionNM = materialOption.description,
                $scope.$apply();
        };

        function selectClient(client) {
            $scope.data.clientID = client.id,
                $scope.data.clientUD = client.value,
                $scope.data.clientNM = client.description,
                $scope.$apply();
        };

        /*
         Test File
         */
        function addTestfile() {
            var newTDfile = {
                materialTestReportFileID: $scope.method.getNewID(),
                scanFile: null,
                remark: null,
                scanFileLocation: null,
                friendlyName: null,
                scanHasChange: false,
                scanNewFile: null
            };
            $scope.event.editTestfile(newTDfile);
        };

        function editTestfile(item) {
            $scope.currenTDfile = item;
            jQuery("#testFileForm").modal();
        };

        function updateTestfile(currenTDfile) {
            $scope.data.materialTestReportFileDTOs.push(currenTDfile);
            var index = $scope.data.materialTestReportFileDTOs.indexOf(currenTDfile);
            $scope.data.materialTestReportFileDTOs[index] = currenTDfile;
            jQuery("#testFileForm").modal('hide');
        };

        function removeTestfile(item) {
            if (confirm("Are you Sure Delete this Data ?")) {
                $scope.data.materialTestReportFileDTOs.splice($scope.data.materialTestReportFileDTOs.indexOf(item), 1)
            }
        };

        function addTestScanFile() {
            masterUploader.multiSelect = false;
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
        };

        function addTestStandard() {
            var selectedOption = jQuery('#materialStandard').select2('data');
            var isExist = false;
            angular.forEach($scope.data.materialTestStandardDTOs, function (value, key) {
                if (value.materialTestStandardID === selectedOption.id) {
                    isExist = true;
                }
            }, null);
            if (!isExist) {
                var creatMaterial = {
                    materialTestUsingMaterialStandardID: $scope.method.getNewID(),
                    materialTestStandardID: selectedOption.id,
                    testStandardNM: selectedOption.text
                };

                $scope.data.materialTestStandardDTOs.push(creatMaterial);
            }
        };

        function removeTestStandard(item) {
            if (confirm("Are you Sure Delete this Data ?")) {
                $scope.data.materialTestStandardDTOs.splice($scope.data.materialTestStandardDTOs.indexOf(item), 1)
            }
        };

        //tdfile
        function getNewID() {
            $scope.newid--;
            return $scope.newid;
        };
        function gettdfileIndex(id) {
            var isExist = false;
            var index = 0;
            for (var index = 0; index < $scope.data.materialTestReportFileDTOs.length; index++) {
                if ($scope.data.materialTestReportFileDTOs[index].materialTestReportFileID = id) {
                    isExist = true;
                    break;
                }
                if (!isExist) {
                    return -1;
                } else {
                    return index;
                }
            }
        };
        function rendertdfile() {
            $scope.tdfile = [];
            angular.forEach($scope.data.materialTestReportFileDTOs, function (value, key) {
                if ($scope.tdfile.indexOf(value.materialTestReportFileID) < 0) {
                    $scope.tdfile.push(value.materialTestReportFileID);
                }
            }, null);
        };

        $scope.event.init();
    };
})();