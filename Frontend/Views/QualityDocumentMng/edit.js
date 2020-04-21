(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'furnindo-directive', 'ngCookies']).controller('tilsoftController', qualityDocumentMngController);
    qualityDocumentMngController.$inject = ['$scope', 'dataService'];

    function qualityDocumentMngController($scope, dataService) {
        $scope.data = [];
        $scope.supportQualityDocumentType = [];


        $scope.event = {
            init: init,
            get: get,
            remove: remove,
            update: update,
            downloadFile: downloadFile,
            removeFile: removeFile,
            changeFile: changeFile

        };

        function init() {
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
            dataService.supportServiceUrl = context.supportServiceUrl;

            $scope.event.get();
        };

        function get() {
            dataService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.supportQualityDocumentType = data.data.supportQualityDocuments;
                    jQuery('#content').show();
                },
                function (error) {
                   
                });
        };

        function remove(id) {
            debugger;
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
                    
                });
        };

        function update() {
            if ($scope.editForm.$valid) {
                dataService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.qualityDocumentID;
                            $scope.data = data.data;
                        }
                    },
                    function (error) {
                        
                    });
            } else {
                jsHelper.showMessage('warning', context.msgValid);
            }
        };


        //file functions   
        function changeFile() {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.friendlyName = fileUploader2.selectedFriendlyName;
                    $scope.data.fileLocation = fileUploader2.selectedFileUrl;
                    $scope.data.file_NewFile = fileUploader2.selectedFileName;
                    $scope.data.file_HasChange = true;
                });
            };
            fileUploader2.open();
        };

        function removeFile() {
            $scope.data.friendlyName = '';
            $scope.data.fileLocation = '';
            $scope.data.file_NewFile = '';
            $scope.data.file_HasChange = true;
        };

        function downloadFile() {
            if ($scope.data.fileLocation) {
                window.open($scope.data.fileLocation);
            }
        };


        $scope.event.init();

        function createservices() {
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;

        };
    };
})();