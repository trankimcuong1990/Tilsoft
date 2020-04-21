//
// SUPPORT
//
//jQuery('#main-form').keypress(function (e) {
//    if (e.keyCode == 13) {
//        e.preventDefault();
//        return false;
//    }
//});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = [];
    $scope.plcImageTypes = null;
    $scope.currentImage = null;
    $scope.newid = 0;
    

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                context.bookingId,
                context.factoryId,
                context.offerLineId,
                context.offerLineSampleProductId,
                context.offerLineSparepartId,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.plcImageTypes = data.data.plcImageTypes;
                    $scope.$apply();
                    jQuery('#content').show();
                    if ($scope.data.plcImages.length > 0) {
                        $scope.event.checkOverral();
                    }
                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.plcImageTypes = null;
                    $scope.$apply();
                }
            );
        },
        update: function () {
            var itemImg = $scope.data.plcImages.filter(o => o.imageTypeID === 15);
            if (itemImg.length <= 0) {
                jsHelper.showMessage('warning', "Please add Overral Image !!!");
                return;
            }
            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.plcid);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', context.error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', errMsg);
            }
        },
        delete: function () {
            if (confirm('Are you sure ?')) {
                jsonService.delete(context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        window.location = context.backUrl;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        addImage: function (type) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.plcImages.push({
                            plcImageID: scope.method.getNewID(),
                            imageTypeID: type,
                            imageFile: '',
                            thumbnailLocation: img.fileURL,
                            fileLocation: img.fileURL,
                            imageFile_HasChange: true,
                            imageFile_NewFile: img.filename
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },

        checkOverral: function () {
            let check = false;
            if ($scope.data.length !== 0)
            {
                for (var i = 0; i < $scope.data.plcImages.length; i++) {
                    var item = $scope.data.plcImages[i];
                    if (item.imageTypeID === 15) {
                        check = true;
                        break;
                    }
                }
                if (check === true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        },
        
        addImageOverral: function (type) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    scope.data.plcImages.push({
                        plcImageID: scope.method.getNewID(),
                        imageTypeID: type,
                        imageFile: '',
                        thumbnailLocation: img.fileURL,
                        fileLocation: img.fileURL,
                        imageFile_HasChange: true,
                        imageFile_NewFile: img.filename
                    });
                });
            };
            masterUploader.open();
        },
        removeImage: function (item) {
            $scope.data.plcImages.splice($scope.data.plcImages.indexOf(item), 1);
            $scope.event.checkOverral();
        },

        // move form function
        openMoveForm: function (item) {
            $scope.currentImage = JSON.parse(JSON.stringify(item));
            jQuery('#moveForm').modal();
        },
        moveFormOK: function () {
            var updateItem = $scope.currentImage;
            angular.forEach($scope.data.plcImages, function (item) {
                if (item.plcImageID === updateItem.plcImageID) {
                    item.imageTypeID = updateItem.imageTypeID;
                }
            }, updateItem);
            $scope.currentImage = null;

            jQuery('#moveForm').modal('hide');
        },
        print: function () {
            jsonService.getReport(
                context.id,
                function (data) {
                    if (data.message.type === 0) {
                        window.location = context.backendReportUrl + data.data;
                    }
                    else {
                        jsHelper.processMessageEx(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    };

    //
    // method
    //
    $scope.method = {
        getNewID: function(){
            $scope.newid--;
            return $scope.newid;
        },
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        },
        getPLCImage: function (id) {
            var result = null;
            angular.forEach($scope.data.plcImages, function (value, key) {
                if (value.plcImageID === id) {
                    result = value;
                }
            }, result);
            return result;
        }
    };

    //
    // init
    //    
    $scope.event.init();
}]);