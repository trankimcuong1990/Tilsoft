//
// SUPPORT
//
jQuery('#main-form').keypress(function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngSanitize', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', '$http', 'jsonService', function ($scope, $filter, $http, jsonService) {
    jsonService.serviceUrl = context.serviceUrl;
    jsonService.token = context.token;
    jsonService.printUrl = context.printUrl;

    // base
    jsonService.load = function (id, param, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        if (param == null) {
            param = {};
        }

        $http({
            method: 'POST',
            url: this.serviceUrl + 'getdetail?id=' + id,
            data: JSON.stringify(param),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    // item info
    jsonService.updateItem = function (id, data, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'updateitem?id=' + id,
            data: JSON.stringify(data),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    // progress
    jsonService.updateProgress = function (id, data, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'updateprogress?id=' + id,
            data: JSON.stringify(data),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };
    jsonService.deleteProgress = function (id, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'deleteprogress?id=' + id,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    // remark
    jsonService.updateRemark = function (id, data, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'updateremark?id=' + id,
            data: JSON.stringify(data),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };
    jsonService.deleteRemark = function (id, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'deleteremark?id=' + id,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    // QA remark
    jsonService.updateQARemark = function (id, data, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'updateqaremark?id=' + id,
            data: JSON.stringify(data),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };
    jsonService.deleteQARemark = function (id, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'deleteqaremark?id=' + id,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    // technical drawing
    jsonService.updateTechnicalDrawing = function (id, data, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'updatetechnicaldrawing?id=' + id,
            data: JSON.stringify(data),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };
    jsonService.approveTechnicalDrawing = function (id, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'approvetechnicaldrawing?id=' + id,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };
    jsonService.resetTechnicalDrawing = function (id, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'resettechnicaldrawing?id=' + id,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };
    jsonService.deleteTechnicalDrawing = function (id, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'deletetechnicaldrawing?id=' + id,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    // product status
    jsonService.changeProductStatus = function (id, statusId, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'updateproductstatus?id=' + id + '&statusId=' + statusId,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    jsonService.updateFinishStatus = function (id, file, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.serviceUrl + 'updateproductstatus2?id=' + id + '&statusId=4&file=' + file,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type == 0) {
                //jsHelper.method.showMessage(context.TEXT_UPDATE_SUCCESS, 0);
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    // export to excel
    jsonService.getExcelPrint = function (iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.printUrl + 'getexcelprint?id=' + context.id,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);
            iSuccessCallback(response.data);
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    }

    // export to excel with qrcode
    jsonService.getExcelBarcode = function (data, qntBarcode, iSuccessCallback, iErrorCallback) {
        jsHelper.loadingSwitch(true);
        $http({
            method: 'POST',
            url: this.printUrl + 'getexcelbarcode?param=' + data + '&qnt=' + qntBarcode,
            data: JSON.stringify(data),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            }
        }).then(function successCallback(response) {
            jsHelper.loadingSwitch(false);

            if (response.data.message.type === 0) {
                iSuccessCallback(response.data);
            }
            else {
                jsHelper.showMessage('warning', response.data.message.message);
                console.log(response.data.message.message);
                iErrorCallback(response);
            }
        }, function errorCallback(response) {
            jsHelper.loadingSwitch(false);
            jsHelper.showMessage('warning', response.data.message);
            iErrorCallback(response);
        });
    };

    //
    // property
    //
    $scope.data = null;
    $scope.sampleProductStatuses = null;

    $scope.currentProduct = null;
    $scope.currentProgress = null;
    $scope.currentRemark = null;
    $scope.currentQARemark = null;
    $scope.currentTechnicalDrawing = null;
    $scope.currentTechnicalDrawingFile = null;
    $scope.sampleRemark = [];
    $scope.sampleProductMinute = [];

    $scope.newid = 0;
    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                {},
                function (data) {
                    $scope.data = data.data.data; console.log($scope.data);
                    $scope.sampleProductStatuses = data.data.sampleProductStatuses;
                    jQuery('#content').show();

                    // go to page section
                    if (context.initParam) {
                        index = jsHelper.getArrayIndex($scope.data.sampleProducts, 'sampleProductID', context.initParam.pi);
                        if (index >= 0) {
                            switch (context.initParam.a) {
                                case 4:
                                    $scope.event.openQARemark($scope.data.sampleProducts[index]);
                                    if (context.initParam.data) {
                                        $scope.currentQARemark = {
                                            sampleQARemarkID: $scope.method.getNewID(),
                                            sampleProductID: $scope.currentProduct.sampleProductID,
                                            remark: jQuery.base64.atob(context.initParam.data, true),
                                            updatedDate: 'Just Now',
                                            updatorName: 'Yourself',
                                            sampleQARemarkImages: []
                                        };
                                        $scope.event.updateQARemark();
                                    }
                                    break;

                                case 3:
                                    $scope.event.openRemark($scope.data.sampleProducts[index]);
                                    if (context.initParam.data) {
                                        $scope.currentRemark = {
                                            sampleRemarkID: $scope.method.getNewID(),
                                            sampleProductID: $scope.currentProduct.sampleProductID,
                                            remark: jQuery.base64.atob(context.initParam.data, true),
                                            updatedDate: 'Just Now',
                                            updatorName: 'Yourself',
                                            sampleRemarkImages: []
                                        };
                                        $scope.event.updateRemark();
                                    }
                                    break;

                                case 2:
                                    $scope.event.openTechnicalDrawing($scope.data.sampleProducts[index]);
                                    break;

                                case 1:
                                    $scope.event.openProgress($scope.data.sampleProducts[index]);
                                    break;

                                case 0:
                                    $scope.event.openProductInfo($scope.data.sampleProducts[index]);
                                    break;

                                case -1:
                                    setTimeout(function () { $scope.method.goToSection('product-' + context.initParam.pi); }, 500)
                                    break;
                            }
                        }
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.sampleProductStatuses = null;
                }
            );
        },

        print: function () {
            jsonService.getExcelPrint(
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        export: function () {
            var sampleProductIDs = '', qntBarcodes = '';
            var i = 0;

            angular.forEach($scope.data.sampleProducts, function (value, key) {
                if (value.isExport !== undefined && value.isExport !== null && value.isExport) {
                    if (i > 0) {
                        sampleProductIDs = sampleProductIDs + ',';
                        qntBarcodes = qntBarcodes + ',';
                    }

                    sampleProductIDs = sampleProductIDs + value.sampleProductID;
                    if (value.qntBarcode === undefined || value.qntBarcode === null || value.qntBarcode === '') {
                        qntBarcodes = qntBarcodes + '1';
                    } else {
                        qntBarcodes = qntBarcodes + value.qntBarcode;
                    }

                    i++;
                }
            });

            if (sampleProductIDs === undefined || sampleProductIDs === null || sampleProductIDs === '') {
                jsHelper.showMessage('warning', 'Please select one least sample to print.');
                return false;
            }

            jsonService.getExcelBarcode(
                sampleProductIDs,
                qntBarcodes,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        },

        // product info
        openProductInfo: function (item) {
            console.log(item);
            $scope.currentProduct = JSON.parse(JSON.stringify(item));
            jsHelper.showPopup('frmProduct', function () { });
            window.scrollTo(0, 0);
        },
        updateProductInfo: function () {
            jsonService.updateItem(
                $scope.currentProduct.sampleProductID,
                $scope.currentProduct,
                function (data) {
                    jsHelper.processMessageEx(data.message);
                    if (data.message.type == 0) {
                        // reflect change
                        index = jsHelper.getArrayIndex($scope.data.sampleProducts, 'sampleProductID', $scope.currentProduct.sampleProductID);
                        if (index >= 0) {
                            $scope.data.sampleProducts[index] = JSON.parse(JSON.stringify($scope.currentProduct));
                        }

                        $scope.event.closeProductInfo();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        closeProductInfo: function () {
            jsHelper.hidePopup('frmProduct', function () { });
            $scope.method.goToSection('product-' + $scope.currentProduct.sampleProductID);
        },

        // progress
        openProgress: function (product) {
            $scope.currentProduct = product;
            jsHelper.showPopup('frmProgress', function () { });
            window.scrollTo(0, 0);
        },
        addProgress: function () {
            $scope.currentProgress = {
                sampleProgressID: $scope.method.getNewID(),
                sampleProductID: $scope.currentProduct.sampleProductID,
                version: '',
                remark: '',
                updatorName: 'Yourself',
                updatedDate: 'Just Now',
                sampleProgressImages: []
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
                            index = jsHelper.getArrayIndex($scope.currentProduct.sampleProgresses, 'sampleProgressID', data.data.sampleProgressID);
                            if (index >= 0) {
                                $scope.currentProduct.sampleProgresses[index] = JSON.parse(JSON.stringify(data.data));
                            }
                            else {
                                $scope.currentProduct.sampleProgresses.push(JSON.parse(JSON.stringify(data.data)));
                            }
                            jQuery('#frmEditProgress').modal('hide');
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
                    $scope.currentProduct.sampleProgresses.splice($scope.currentProduct.sampleProgresses.indexOf(item), 1);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        addProgressImage: function () {
            //imageMultipleUploader.callback = function () {
            //    scope.$apply(function () {
            //        angular.forEach(imageMultipleUploader.selectedFiles, function (value, key) {
            //            scope.currentProgress.sampleProgressImages.push({
            //                sampleProgressImageID: $scope.method.getNewID(),
            //                fileUD: '',
            //                thumbnailLocation: value.fileURL,
            //                fileLocation: value.fileURL,
            //                hasChanged: true,
            //                newFile: value.filename
            //            });
            //        }, null);
            //    });
            //};
            //imageMultipleUploader.onhide = function () {
            //    jQuery('#frmEditProgress').modal('show');
            //};
            //jQuery('#frmEditProgress').modal('hide');
            //imageMultipleUploader.open();

            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.currentProgress.sampleProgressImages.push({
                            sampleProgressImageID: $scope.method.getNewID(),
                            thumbnailLocation: img.fileURL,
                            fileLocation: img.fileURL,
                            hasChanged: true,
                            newFile: img.filename
                        });
                    }, null);
                    jQuery('#frmEditProgress').modal('show');
                });
            };
            jQuery('#frmEditProgress').modal('hide');
            masterUploader.open();
        },
        removeProgressImage: function (image) {
            $scope.currentProgress.sampleProgressImages.splice($scope.currentProgress.sampleProgressImages.indexOf(image), 1);
        },
        closeProgress: function () {
            jsHelper.hidePopup('frmProgress', function () { });
            $scope.method.goToSection('product-' + $scope.currentProduct.sampleProductID);
        },

        // remark
        openRemark: function (product) {
            $scope.currentProduct = product;
            jsHelper.showPopup('frmRemark', function () { });
            window.scrollTo(0, 0);
        },
        addRemark: function () {
            $scope.currentRemark = {
                sampleRemarkID: $scope.method.getNewID(),
                sampleProductID: $scope.currentProduct.sampleProductID,
                remark: '',
                updatedDate: 'Just Now',
                updatorName: 'Yourself',
                sampleRemarkImages: []
            };
            $scope.event.editRemark($scope.currentRemark);
        },
        editRemark: function (item) {
            $scope.currentRemark = JSON.parse(JSON.stringify(item));
            jQuery('#frmEditRemark').modal('show');
        },
        updateRemark: function () {
            jsonService.updateRemark(
                $scope.currentRemark.sampleRemarkID,
                $scope.currentRemark,
                function (data) {
                    index = jsHelper.getArrayIndex($scope.currentProduct.sampleRemarks, 'sampleRemarkID', data.data.sampleRemarkID);
                    if (index >= 0) {
                        $scope.currentProduct.sampleRemarks[index] = JSON.parse(JSON.stringify(data.data));
                    }
                    else {
                        $scope.currentProduct.sampleRemarks.push(JSON.parse(JSON.stringify(data.data)));
                    }
                    jQuery('#frmEditRemark').modal('hide');
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        removeRemark: function (item) {
            jsonService.deleteRemark(
                item.sampleRemarkID,
                function (data) {
                    $scope.currentProduct.sampleRemarks.splice($scope.currentProduct.sampleRemarks.indexOf(item), 1);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        addRemarkImage: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.currentRemark.sampleRemarkImages.push({
                        sampleRemarkImageID: $scope.method.getNewID(),
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
                jQuery('#frmRemark').modal('show');
            };
            jQuery('#frmRemark').modal('hide');
            fileUploader2.open();
        },
        removeRemarkImage: function (image) {
            $scope.currentRemark.sampleRemarkImages.splice($scope.currentRemark.sampleRemarkImages.indexOf(image), 1);
        },
        closeRemark: function () {
            jsHelper.hidePopup('frmRemark', function () { });
            $scope.method.goToSection('product-' + $scope.currentProduct.sampleProductID);
        },


        // QA remark
        openQARemark: function (product) {
            $scope.currentProduct = product;
            jsHelper.showPopup('frmQARemark', function () { });
            window.scrollTo(0, 0);
        },
        addQARemark: function () {
            $scope.currentQARemark = {
                sampleQARemarkID: $scope.method.getNewID(),
                sampleProductID: $scope.currentProduct.sampleProductID,
                remark: '',
                updatedDate: 'Just Now',
                updatorName: 'Yourself',
                sampleQARemarkImages: []
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
                    index = jsHelper.getArrayIndex($scope.currentProduct.sampleQARemarks, 'sampleQARemarkID', data.data.sampleQARemarkID);
                    if (index >= 0) {
                        $scope.currentProduct.sampleQARemarks[index] = JSON.parse(JSON.stringify(data.data));
                    }
                    else {
                        $scope.currentProduct.sampleQARemarks.push(JSON.parse(JSON.stringify(data.data)));
                    }
                    jQuery('#frmEditQARemark').modal('hide');
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
                    $scope.currentProduct.sampleQARemarks.splice($scope.currentProduct.sampleQARemarks.indexOf(item), 1);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        addQARemarkImage: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.currentQARemark.sampleQARemarkImages.push({
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
            $scope.currentQARemark.sampleQARemarkImages.splice($scope.currentQARemark.sampleQARemarkImages.indexOf(image), 1);
        },
        closeQARemark: function () {
            jsHelper.hidePopup('frmQARemark', function () { });
            $scope.method.goToSection('product-' + $scope.currentProduct.sampleProductID);
        },

        // technical drawing
        openTechnicalDrawing: function (product) {
            $scope.currentProduct = product;
            jsHelper.showPopup('frmTechnicalDrawing', function () { });
            window.scrollTo(0, 0);
        },
        addTechnicalDrawing: function () {
            $scope.currentTechnicalDrawing = {
                sampleTechnicalDrawingID: $scope.method.getNewID(),
                sampleProductID: $scope.currentProduct.sampleProductID,
                description: '',
                isConfirmed: false,
                confirmerName: '',
                confirmedDate: '',
                sampleTechnicalDrawingFiles: []
            };
            $scope.event.editTechnicalDrawing($scope.currentTechnicalDrawing);
        },
        editTechnicalDrawing: function (item) {
            $scope.currentTechnicalDrawing = JSON.parse(JSON.stringify(item));
            jQuery('#frmEditTechnicalDrawing').modal('show');
        },
        updateTechnicalDrawing: function () {
            jsonService.updateTechnicalDrawing(
                $scope.currentTechnicalDrawing.sampleTechnicalDrawingID,
                $scope.currentTechnicalDrawing,
                function (data) {
                    jsHelper.processMessageEx(data.message);
                    if (data.message.type == 0) {
                        index = jsHelper.getArrayIndex($scope.currentProduct.sampleTechnicalDrawings, 'sampleTechnicalDrawingID', data.data.sampleTechnicalDrawingID);
                        if (index >= 0) {
                            $scope.currentProduct.sampleTechnicalDrawings[index] = JSON.parse(JSON.stringify(data.data));
                        }
                        else {
                            $scope.currentProduct.sampleTechnicalDrawings.push(JSON.parse(JSON.stringify(data.data)));
                        }
                        jQuery('#frmEditTechnicalDrawing').modal('hide');
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        removeTechnicalDrawing: function (item) {
            if (item.sampleTechnicalDrawingID > 0 && confirm('Delete the technical drawing ?')) {
                jsonService.deleteTechnicalDrawing(
                    item.sampleTechnicalDrawingID,
                    function (data) {
                        $scope.currentProduct.sampleTechnicalDrawings.splice($scope.currentProduct.sampleTechnicalDrawings.indexOf(item), 1);
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        addTechnicalDrawingFile: function () {
            $scope.currentTechnicalDrawingFile = {
                sampleTechnicalDrawingFileID: $scope.method.getNewID(),
                isActived: false,
                remark: '',
                previewFileUD: false,
                sourceFileUD: '',
                updatedBy: '',
                updatedDate: 'Just now',
                updatorName: 'Yourself',
                sourceFileFriendlyName: '',
                sourceFileLocation: '',
                previewFileFriendlyName: '',
                previewFileLocation: '',
                sourceFileHasChanged: false,
                sourceFileNewFile: '',
                previewFileHasChanged: false,
                previewFileNewFile: ''
            };
            $scope.event.editTechnicalDrawingFile($scope.currentTechnicalDrawingFile);
        },
        editTechnicalDrawingFile: function (item) {
            $scope.currentTechnicalDrawingFile = JSON.parse(JSON.stringify(item));
            jQuery('#frmEditTechnicalDrawing').modal('hide');
            setTimeout(function () { jQuery('#frmEditTechnicalDrawingFile').modal('show'); }, 500)
        },
        updateTechnicalDrawingFile: function () {
            if ($scope.currentTechnicalDrawingFile.isActived) {
                angular.forEach($scope.currentTechnicalDrawing.sampleTechnicalDrawingFiles, function (value, key) {
                    value.isActived = false;
                }, null);
            }

            index = jsHelper.getArrayIndex($scope.currentTechnicalDrawing.sampleTechnicalDrawingFiles, 'sampleTechnicalDrawingFileID', $scope.currentTechnicalDrawingFile.sampleTechnicalDrawingFileID);
            if (index >= 0) {
                $scope.currentTechnicalDrawing.sampleTechnicalDrawingFiles[index] = JSON.parse(JSON.stringify($scope.currentTechnicalDrawingFile));
            }
            else {
                $scope.currentTechnicalDrawing.sampleTechnicalDrawingFiles.push(JSON.parse(JSON.stringify($scope.currentTechnicalDrawingFile)));
            }
            jQuery('#frmEditTechnicalDrawingFile').modal('hide');
            setTimeout(function () { jQuery('#frmEditTechnicalDrawing').modal('show'); }, 500)
        },
        cancelTechnicalDrawingFile: function () {
            jQuery('#frmEditTechnicalDrawingFile').modal('hide');
            setTimeout(function () { jQuery('#frmEditTechnicalDrawing').modal('show'); }, 500)
        },
        removeTechnicalDrawingFile: function (item) {
            $scope.currentTechnicalDrawing.sampleTechnicalDrawingFiles.splice($scope.currentTechnicalDrawing.sampleTechnicalDrawingFiles.indexOf(item), 1);
        },
        addTechnicalDrawingSourceFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.currentTechnicalDrawingFile.sourceFileHasChanged = true;
                    scope.currentTechnicalDrawingFile.sourceFileNewFile = fileUploader2.selectedFileName;
                    scope.currentTechnicalDrawingFile.sourceFileLocation = fileUploader2.selectedFileUrl;
                    scope.currentTechnicalDrawingFile.sourceFileFriendlyName = fileUploader2.selectedFriendlyName;
                });
            };
            fileUploader2.onhide = function () {
                jQuery('#frmEditTechnicalDrawingFile').modal('show');
            };
            jQuery('#frmEditTechnicalDrawingFile').modal('hide');
            fileUploader2.open();
        },
        removeTechnicalDrawingSourceFile: function () {
            $scope.currentTechnicalDrawingFile.sourceFileHasChanged = true;
            $scope.currentTechnicalDrawingFile.sourceFileNewFile = '';
            $scope.currentTechnicalDrawingFile.sourceFileLocation = '';
            $scope.currentTechnicalDrawingFile.sourceFileFriendlyName = '';
        },
        addTechnicalDrawingPreviewFile: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.currentTechnicalDrawingFile.previewFileHasChanged = true;
                    scope.currentTechnicalDrawingFile.previewFileNewFile = fileUploader2.selectedFileName;
                    scope.currentTechnicalDrawingFile.previewFileLocation = fileUploader2.selectedFileUrl;
                    scope.currentTechnicalDrawingFile.previewFileFriendlyName = fileUploader2.selectedFriendlyName;
                });
            };
            fileUploader2.onhide = function () {
                jQuery('#frmEditTechnicalDrawingFile').modal('show');
            };
            jQuery('#frmEditTechnicalDrawingFile').modal('hide');
            fileUploader2.open();
        },
        removeTechnicalDrawingPreviewFile: function () {
            $scope.currentTechnicalDrawingFile.previewFileHasChanged = true;
            $scope.currentTechnicalDrawingFile.previewFileNewFile = '';
            $scope.currentTechnicalDrawingFile.previewFileLocation = '';
            $scope.currentTechnicalDrawingFile.previewFileFriendlyName = '';
        },
        toggleTechnicalDrawingArchive: function (item) {
            jQuery('#archive-' + item.sampleTechnicalDrawingID).toggle();
        },
        approveTechnicalDrawing: function (item) {
            if (item.sampleTechnicalDrawingID > 0 && confirm('Approve the TD?')) {
                jsonService.approveTechnicalDrawing(
                    item.sampleTechnicalDrawingID,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            window.location = context.refreshUrl + context.id + '?param=pi:' + item.sampleProductID + ',a:2';
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        resetTechnicalDrawing: function (item) {
            if (item.sampleTechnicalDrawingID > 0 && confirm('Reset the TD to un-confirmed')) {
                jsonService.resetTechnicalDrawing(
                    item.sampleTechnicalDrawingID,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            window.location = context.refreshUrl + context.id + '?param=pi:' + item.sampleProductID + ',a:2';
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        closeTechnicalDrawing: function () {
            jsHelper.hidePopup('frmTechnicalDrawing', function () { });
            $scope.method.goToSection('product-' + $scope.currentProduct.sampleProductID);
        },

        // product status
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
            $scope.currentProduct = JSON.parse(JSON.stringify(product));
            jQuery('#frmFinished').modal('show');
        },
        uploadFinishProduct: function () {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    scope.currentProduct.finishedImage = fileUploader2.selectedFileName;
                    scope.currentProduct.finishedImageFileLocation = fileUploader2.selectedFileUrl;
                });
            };
            fileUploader2.onhide = function () {
                jQuery('#frmFinished').modal('show');
            };
            jQuery('#frmFinished').modal('hide');
            fileUploader2.open();
        },
        setProductFinishedStatus: function () {
            if (!$scope.currentProduct.finishedImage) {
                alert('Please upload the finished image!');
                return;
            }

            jsonService.updateFinishStatus(
                $scope.currentProduct.sampleProductID,
                $scope.currentProduct.finishedImage,
                function (data) {
                    jsHelper.processMessageEx(data.message);
                    if (data.message.type == 0) {
                        window.location = context.refreshUrl + context.id + '?param=pi:' + $scope.currentProduct.sampleProductID + ',a:999';
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    if (!error.data.data && error.data.message.message === 'Invalid indicated price!') {
                        if (confirm(error.data.message.message + '. Do you want to create Factory Breakdown?')) {
                            window.location = context.redirectUrl + '/' + 0 + '?sampleProductId=' + $scope.currentProduct.sampleProductID;
                        }
                    }
                }
            );
        },
        inputFactoryBreakdown: function (sampleProductID) {
            window.location = context.redirectUrl + '/' + 0 + '?sampleProductId=' + sampleProductID;
        },

        //popup Sample Remark
        openSampleRemark: function (sampleRemarkID) {
            angular.forEach($scope.data.sampleProducts, function (item) {
                angular.forEach(item.sampleRemarks, function (sItem) {
                    if (sItem.sampleRemarkID === sampleRemarkID) {
                        $scope.sampleRemark = sItem;
                    }
                });
            });

            jQuery("#frmSampleRemark").modal();
        },

        //popup Sample Product Minute
        openSampleProductMinute: function (sampleProductMinuteID) {
            angular.forEach($scope.data.sampleProducts, function (item) {
                angular.forEach(item.sampleProductMinutes, function (sItem) {
                    if (sItem.sampleProductMinuteID === sampleProductMinuteID) {
                        $scope.sampleProductMinute = sItem;
                    }
                });
            });

            jQuery("#frmSampleProductMinute").modal();
        },
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
            $scope.newid--;
            return $scope.newid;
        },
        formatRemark: function (remark) {
            if (remark == null) {
                return '';
            }
            return remark.replace(/(?:\r\n|\r|\n)/g, '<br />');
        },
        goToSection: function (id) {
            window.location.hash = id;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);