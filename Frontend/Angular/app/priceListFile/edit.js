//
// APP START
//

var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'ui.select2', 'ng-currency']);
tilsoftApp.controller('tilsoftController', ['$scope', 'dataService', function ($scope, dataService) {
    dataService.searchFilter.pageSize = context.pageSize;
    dataService.serviceUrl = context.serviceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;
    $scope.newID = -1;
    $scope.seasons = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            dataService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.seasons = data.data.seasons;
                    //console.log(data);
                    jQuery('#content').show();
                    console.log(data);

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });


                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.seasons = null;
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                dataService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.priceListFileID);
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

        //PDF
        changePDFFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.pdfFriendlyName = fileUploader2.selectedFriendlyName;
                    $scope.data.pdfFileLocation = fileUploader2.selectedFileUrl;
                    $scope.data.newPDFFile = fileUploader2.selectedFileName;
                    $scope.data.pdfFileLocation_HasChange = true;
                });
            };
            fileUploader2.open();
        },
        removePDFFile: function () {
            $scope.data.pdfFriendlyName = '';
            $scope.data.pdfFileLocation = '';
            $scope.data.newPDFFile = '';
            $scope.data.pdfFileLocation_HasChange = true;
        },
        //Excel
        changeExcelFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.excelFriendlyName = fileUploader2.selectedFriendlyName;
                    $scope.data.excelFileLocation = fileUploader2.selectedFileUrl;
                    $scope.data.newExcelFile = fileUploader2.selectedFileName;
                    $scope.data.excelFileLocation_HasChange = true;
                });
            };
            fileUploader2.open();
        },
        removeExcelFile: function () {
            $scope.data.excelFriendlyName = '';
            $scope.data.excelFileLocation = '';
            $scope.data.newExcelFile = '';
            $scope.data.excelFileLocation_HasChange = true;
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