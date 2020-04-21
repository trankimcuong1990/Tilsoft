// formatData
// using for auto-complate support quicksearch.
function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.value,
            description: item.value
        }
    });
}

(function () {
    'use strict';

    angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives', 'ui.select2', 'ngCookies']).controller('tilsoftController', cushionTestingController);
    cushionTestingController.$inject = ['$scope', 'dataService'];

    function cushionTestingController($scope, cushionTestingService) {
        $scope.data = [];
        $scope.searchString = null;
        $scope.IDX = 0;
        $scope.getIDX = 0;
        $scope.newid = 0;

        $scope.cushionTestStandards = [];

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
        $scope.ngRequestCushionColor = {
            url: context.serviceUrl + 'getCushionColor',
            token: context.token,
        };
        $scope.cushionColor = {
            id: null,
            label: null,
            description: null
        };

        $scope.event = {
            init: init,
            get: get,
            update: update,
            del: del,
            selectCushionColor: selectCushionColor,
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
            getNewID: getNewID

        };

        function init() {
            cushionTestingService.serviceUrl = context.serviceUrl;
            cushionTestingService.supportServiceUrl = context.supportServiceUrl;
            cushionTestingService.token = context.token;

            $scope.event.get();
        };

        function get() {
            cushionTestingService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.cushionTestStandards = data.data.supportCushionTestStandards;
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
                cushionTestingService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            debugger
                            window.location = context.refreshUrl + data.data.cushionTestReportID;
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
            cushionTestingService.delete(
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

        function selectCushionColor(cushionColor) {
            $scope.data.cushionColorID = cushionColor.id,
                $scope.data.cushionColorUD = cushionColor.value,
                $scope.data.cushionColorNM = cushionColor.description,
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
                cushionTestReportFileID: $scope.method.getNewID(),
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
            $scope.currentTestfile = item;
            jQuery("#testFileForm").modal();
        };

        function updateTestfile(currentTestfile) {
            $scope.data.cushionTestReportFileDTOs.push(currentTestfile);
            var index = $scope.data.cushionTestReportFileDTOs.indexOf(currentTestfile);
            $scope.data.cushionTestReportFileDTOs[index] = currentTestfile;
            jQuery("#testFileForm").modal('hide');
        };

        function removeTestfile(item) {
            if (confirm("Are you Sure Delete this Data ?")) {
                $scope.data.cushionTestReportFileDTOs.splice($scope.data.cushionTestReportFileDTOs.indexOf(item), 1)
            }
        };

        function addTestScanFile() {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.currentTestfile.scanHasChange = true;
                        scope.currentTestfile.scanFileLocation = img.fileURL;
                        scope.currentTestfile.friendlyName = img.filename;
                        scope.currentTestfile.scanNewFile = img.filename;
                    }, null);
                });
            };
            masterUploader.open();
        };

        function addTestStandard() {
            var selectedOption = jQuery('#cushionStandard').select2('data');
            var isExist = false;
            angular.forEach($scope.data.cushionTestStandardDTOs, function (value, key) {
                if (value.cushionTestStandardID === selectedOption.id) {
                    isExist = true;
                }
            }, null);
            if (!isExist) {
                var creatMaterial = {
                    cushionTestReportUsingCushionStandardID: $scope.method.getNewID(),
                    cushionTestStandardID: selectedOption.id,
                    testStandardNM: selectedOption.text
                };

                $scope.data.cushionTestStandardDTOs.push(creatMaterial);
            }
        };

        function removeTestStandard(item) {
            if (confirm("Are you Sure Delete this Data ?")) {
                $scope.data.cushionTestStandardDTOs.splice($scope.data.cushionTestStandardDTOs.indexOf(item), 1)
            }
        };

        //testfile
        function getNewID() {
            $scope.newid--;
            return $scope.newid;
        };

        $scope.event.init();
    };
})();