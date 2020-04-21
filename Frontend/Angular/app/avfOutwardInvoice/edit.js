//
//APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['customFilters', 'furnindo-directive']);

tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    $scope.dataContainer = null;
    $scope.seasons = null;
    $scope.newID = -1;

    $scope.event = {
        init: function () {
            avfOutwardInvoiceService.load(
                context.id,
                function (data) {
                    $scope.dataContainer = data.data.data;
                    $scope.seasons = data.data.seasons;
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                    if (data.message.type == 2) {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.datdataContainer = null;
                    $scope.$apply();
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                avfOutwardInvoiceService.update(
                    context.id,
                    $scope.dataContainer,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.avfOutwardInvoiceID);
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
        delete: function () {
            if (confirm('Are you sure ?')) {
                avfOutwardInvoiceService.delete(
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

        changeFile: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.dataContainer.fileLocation = img.fileURL;
                        scope.dataContainer.friendlyName = img.filename;
                        scope.dataContainer.pdfFileScan_NewFile = img.filename;
                        scope.dataContainer.pdfFileScan_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeFile: function () {
            $scope.dataContainer.friendlyName = '';
            $scope.dataContainer.fileLocation = '';
            $scope.dataContainer.pdfFileScan_NewFile = '';
            $scope.dataContainer.pdfFileScan_HasChange = true;
        },
        downloadFile: function () {
            if ($scope.dataContainer.fileLocation) {
                window.open($scope.dataContainer.fileLocation);
            }
        },

        addDetail: function ($event) {
            $event.preventDefault();
            $scope.dataContainer.avfOutwardInvoiceDetails.push({
                avfOutwardInvoiceDetailID: $scope.method.getNewID(),
            });
        },
        removeDetail: function ($event, index) {
            $event.preventDefault();
            $scope.dataContainer.avfOutwardInvoiceDetails.splice(index, 1);
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
        },
        getTotal: function () {
            var result = 0;
            if ($scope.dataContainer != null) {
                angular.forEach($scope.dataContainer.avfOutwardInvoiceDetails, function (value, key) {
                    result += parseFloat(value.amount);
                }, null);
            }
            return result;
        }
    }
    //
    // init
    //
    $scope.event.init();
}]);