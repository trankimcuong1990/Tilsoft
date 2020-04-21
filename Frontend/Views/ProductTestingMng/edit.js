function formatData(data) {
    return $.map(data.data, function (item) {
        return {
            id: item.id,
            label: item.value,
            description: item.label
        }
    });
}

(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive']).controller('tilsoftController', productTestingMngController);
    productTestingMngController.$inject = ['$scope', 'dataService'];

    function productTestingMngController($scope, dataService) {
        $scope.data = [];
        $scope.newID = -1;
        $scope.supportPTS = [];
      
        //Test File
        $scope.currenTestfile = null;
        $scope.testfiles = [];

        // variables for event quick-search with client.
        $scope.model = {
            id: null,
            label: null,
            description: null
        };
        $scope.requestModel = {
            url: context.supportServiceUrl + 'getModel2',
            token: context.token,
        };

        $scope.event = {
            //init: init,
            get: get,
            remove: remove,
            update: update,
            addTestfile: addTestfile,
            editTestfile: editTestfile,
            selectModel: selectModel,
            addTestScanFile: addTestScanFile,
            updatedTestfile: updatedTestfile,
            removedTestfile: removedTestfile,
            addTestStandard: addTestStandard,
            removeTestStandard: removeTestStandard

        };

        $scope.method = {
            refresh: refresh,
            getNewID: getNewID,
        };

        //function init() {

        //};

        //Function Start

        function get() {
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
            dataService.supportServiceUrl = context.supportServiceUrl;

            dataService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.supportPTS = data.data.supportProductTestStandards;
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

        function remove(id) { debugger
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

        function update() {
            if ($scope.editForm.$valid) {
                dataService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                                                  debugger
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.productTestID;
                            $scope.data = data.data;
                        }
                    },
                    function (error) {
                        //not thing to do
                    });
            } else {
                jsHelper.showMessage('warning', context.msgValid);
            }
        };

        function selectModel(model) {
            $scope.data.modelID = model.id;
            $scope.data.modelUD = model.label;
            $scope.data.modelNM = model.description;
            $scope.$apply();
        };

        //Testfile
        function addTestfile() {
            var newTestfile = {
                productTestFileID: $scope.method.getNewID(),
                scanFile: null,
                remark: null,
                scanFileLocation: null,
                friendlyName: null,
                scanHasChange: false,
                scanNewFile: null,
            };

            $scope.event.editTestfile(newTestfile)
        };

        function editTestfile(item) {
            $scope.currenTestfile = item;
            jQuery( "#testForm" ).modal();
        };

        function updatedTestfile(item) {
            $scope.data.productTestFileDTOs.push(item);
            var index = $scope.data.productTestFileDTOs.indexOf(item);
            $scope.data.productTestFileDTOs[index] = item;

            jQuery( "#testForm" ).modal( 'hide' );
        };

        function removedTestfile( item ) {
            if ( confirm( "Are you Sure Delete this Data ?" ) ) {
                $scope.data.productTestFileDTOs.splice($scope.data.productTestFileDTOs.indexOf(item), 1)
            }
        };

        function addTestScanFile() {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.currenTestfile.scanHasChange = true;
                        scope.currenTestfile.scanFileLocation = img.fileURL;
                        scope.currenTestfile.friendlyName = img.filename;
                        scope.currenTestfile.scanNewFile = img.filename;
                    }, null);
                });
            };
            masterUploader.open();
        };


        function addTestStandard() {
            debugger;
            var selectedOption = jQuery('#testStandard').select2('data');
            var isExist = false;
            angular.forEach($scope.data.productTestStandardDTOs, function (value, key) {
                if (value.productTestStandardID === selectedOption.id) {
                    isExist = true;
                }
            }, null);
            if (!isExist) {
                var newTestStandard = {
                    productTestUsingTestStandardID: $scope.method.getNewID(),
                    productTestStandardID: selectedOption.id,
                    testStandardNM: selectedOption.text,
                };
                $scope.data.productTestStandardDTOs.push(newTestStandard);
            }
        };
       function removeTestStandard(item) {
           $scope.data.productTestStandardDTOs.splice($scope.data.productTestStandardDTOs.indexOf(item), 1);
        }

        jQuery('#client').blur(function (e) {
            if (jQuery('#client').val() === null || jQuery('#client').val() === '') {
                $scope.data.clientID = null;
            }
        });

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