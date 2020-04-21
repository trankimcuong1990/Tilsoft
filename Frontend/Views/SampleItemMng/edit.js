var tilsoftApp = angular.module('tilsoftApp', ['ngSanitize', 'avs-directives', 'furnindo-directive', 'customFilters', 'ui']);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', function ($scope, $timeout){   


    //
    // property
    //
    $scope.data = null   
    $scope.sampleProductStatuses = null;
    $scope.factoryBreakdown = null;
    $scope.currentProgress = null;
    $scope.currentQARemark = null;

    $scope.newid = 0;
    //
    // event
    //
    $scope.event = {
        init: function () {        
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;                 
                    if ($scope.data != null) {                    
                        $scope.factoryBreakdown = $scope.data.factoryBreakdownDTO;
                        $scope.sampleProductStatuses = data.data.sampleProductStatuses;
                    }

                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });                
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null; 
                    $scope.factoryBreakdown = null;

                    $scope.$apply();
                }
            );
        },
        update: function ($event) {
            $event.preventDefault();

            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.sampleProductID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', '@Frontend.Properties.Resources.INPUT_VALIDATION_FAILED');
            }
        },

        // Progress
        addProgress: function () {
            $scope.currentProgress = {
                sampleProgressID: $scope.method.getNewID(),
                sampleProductID: $scope.data.sampleProductID,
                version: '',
                remark: '',
                updatorName: 'Yourself',
                updatedDate: 'Just Now',
                sampleProgressImageDTOs: []
            };
            $scope.event.editProgress($scope.currentProgress);
        },
        editProgress: function (item) {
            $scope.currentProgress = JSON.parse(JSON.stringify(item));
            jQuery('#frmEditProgress').modal('show');
        },
        updateProgress: function () {
            var isValid = true;
            if (!$scope.currentProgress.version) {
                isValid = false;
            }

            if (isValid) {
                jsonService.updateProgress(
                    $scope.currentProgress.sampleProgressID,
                    $scope.currentProgress,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            index = jsHelper.getArrayIndex($scope.data.sampleProgressDTOs, 'sampleProgressID', data.data.sampleProgressID);
                            if (index >= 0) {
                                $scope.data.sampleProgressDTOs[index] = JSON.parse(JSON.stringify(data.data));
                            }
                            else {
                                $scope.data.sampleProgressDTOs.push(JSON.parse(JSON.stringify(data.data)));
                            }
                            jQuery('#frmEditProgress').modal('hide');       
                            $scope.method.refresh(context.id);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                alert('Validation failed, please check the input form for error!');
            }
        },
        removeProgress: function (item) {
            jsonService.deleteProgress(
                item.sampleProgressID,
                function (data) {
                    $scope.data.sampleProgressDTOs.splice($scope.data.sampleProgressDTOs.indexOf(item), 1);
                    $scope.method.refresh(context.id);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        addProgressImage: function () {
            imageMultipleUploader.callback = function () {
                scope.$apply(function () {
                    angular.forEach(imageMultipleUploader.selectedFiles, function (value, key) {
                        scope.currentProgress.sampleProgressImageDTOs.push({
                            sampleProgressImageID: $scope.method.getNewID(),
                            fileUD: '',
                            thumbnailLocation: value.fileURL,
                            fileLocation: value.fileURL,
                            hasChanged: true,
                            newFile: value.filename
                        });
                    }, null);
                });
            };
            imageMultipleUploader.onhide = function () {
                jQuery('#frmEditProgress').modal('show');
            };
            jQuery('#frmEditProgress').modal('hide');
            imageMultipleUploader.open();
        },
        removeProgressImage: function (image) {
            $scope.currentProgress.sampleProgressImageDTOs.splice($scope.currentProgress.sampleProgressImageDTOs.indexOf(image), 1);
        },       

        // QA remark       
        addQARemark: function () {
            $scope.currentQARemark = {
                sampleQARemarkID: $scope.method.getNewID(),
                sampleProductID: $scope.data.sampleProductID,
                remark: '',
                updatedDate: 'Just Now',
                updatorName: 'Yourself',
                sampleQARemarkImageDTOs: []
            };
            $scope.event.editQARemark($scope.currentQARemark);
        },
        editQARemark: function (item) {
            $scope.currentQARemark = JSON.parse(JSON.stringify(item));
            jQuery('#frmEditQARemark').modal('show');
        },
        updateQARemark: function () {
            console.log($scope.currentQARemark);

            jsonService.updateQARemark(
                $scope.currentQARemark.sampleQARemarkID,
                $scope.currentQARemark,
                function (data) {
                    index = jsHelper.getArrayIndex($scope.data.sampleQARemarkDTOs, 'sampleQARemarkID', data.data.sampleQARemarkID);
                    if (index >= 0) {
                        $scope.data.sampleQARemarkDTOs[index] = JSON.parse(JSON.stringify(data.data));
                    }
                    else {
                        $scope.data.sampleQARemarkDTOs.push(JSON.parse(JSON.stringify(data.data)));
                    }
                    jQuery('#frmEditQARemark').modal('hide');
                    $scope.method.refresh(context.id);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        removeQARemark: function (item) {
            jsonService.deleteQARemark(
                item.sampleQARemarkID,
                function (data) {
                    $scope.data.sampleQARemarkDTOs.splice($scope.data.sampleQARemarkDTOs.indexOf(item), 1);
                    $scope.method.refresh(context.id);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        addQARemarkImage: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.currentQARemark.sampleQARemarkImageDTOs.push({
                        sampleQARemarkImageID: $scope.method.getNewID(),
                        fileUD: '',
                        thumbnailLocation: '',
                        fileLocation: fileUploader2.selectedFileUrl,
                        hasChanged: true,
                        newFile: fileUploader2.selectedFileName,
                        friendlyName: fileUploader2.selectedFriendlyName
                    });
                });
            };
            fileUploader2.onhide = function () {
                jQuery('#frmQARemark').modal('show');
            };
            jQuery('#frmQARemark').modal('hide');
            fileUploader2.open();
        },
        removeQARemarkImage: function (image) {
            $scope.currentQARemark.sampleQARemarkImageDTOs.splice($scope.currentQARemark.sampleQARemarkImageDTOs.indexOf(image), 1);
        },   
        setProductStatus: function (id, product) {
            index = jsHelper.getArrayIndex($scope.sampleProductStatuses, 'sampleProductStatusID', id);
            if (index >= 0) {
                product.sampleProductStatusID = id;
                product.sampleProductStatusNM = $scope.sampleProductStatuses[index].sampleProductStatusNM;

                if (id == 4) { // FINISHED
                    $scope.event.openFinishProductForm(product);

                }
                else {
                    jsonService.changeProductStatus(
                        product.sampleProductID,
                        id,
                        function (data) {
                            jsHelper.processMessageEx(data.message);
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                }
            }
            else {
                product.sampleProductStatusID = null;
                product.sampleProductStatusNM = '';
            }
        },
        openFinishProductForm: function (product) {
            $scope.data = JSON.parse(JSON.stringify(product));
            jQuery('#frmFinished').modal('show');
        },
        uploadFinishProduct: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.data.finishedImage = fileUploader2.selectedFileName;
                    scope.data.finishedImageFileLocation = fileUploader2.selectedFileUrl;
                });
            };
            fileUploader2.onhide = function () {
                jQuery('#frmFinished').modal('show');
            };
            jQuery('#frmFinished').modal('hide');
            fileUploader2.open();
        },
        setProductFinishedStatus: function () {
            if (!$scope.data.finishedImage) {
                alert('Please upload the finished image!');
                return;
            }
          
            jsonService.updateFinishStatus(
                $scope.data.sampleProductID,
                $scope.data.finishedImage,
                function (data) {
                    jsHelper.processMessageEx(data.message);
                    if (data.message.type == 0) {
                       // window.location = context.refreshUrl + context.id + '?param=pi:' + $scope.data.sampleProductID + ',a:999';
                        $scope.method.refresh(context.id);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    if (!error.data.data && error.data.message.message === 'Invalid indicated price!') {
                        if (confirm(error.data.message.message + '. Do you want to create Factory Breakdown?')) {
                            window.location = context.redirectUrl + '/' + 0 + '?sampleProductId=' + $scope.data.sampleProductID;
                        }
                    }
                }
            );            
        },
        inputFactoryBreakdown: function (sampleProductID) {
            window.location = context.redirectUrl + '/' + 0 + '?sampleProductId=' + sampleProductID;
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = id;
        },
        getNewID: function () {
            $scope.newid--;
            return $scope.newid;
        },      
        formatRemark: function (remark) {
            if (remark == null) {
                return '';
            }
            return remark.replace(/(?:\r\n|\r|\n)/g, '<br />');
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);