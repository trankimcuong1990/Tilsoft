//
//APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.factories = null;
    $scope.lpStatuses = null;
    $scope.currentRemark = null;
    $scope.options = [];

    $scope.newid = -1;
    //
    // event
    //
    $scope.event = {

        autoFile: function (chkPosition, status) {
            if (status === 1) {
                jsonService.autoFile(
                    context.id,
                    chkPosition,
                    $scope.data,
                    function (data) {
                        var myPosition = chkPosition - 5;
                        var optionXFile = "option" + myPosition.toString() + "File";
                        var optionXFileUrl = "option" + myPosition.toString() + "FileUrl";
                        var optionXFriendlyName = "option" + myPosition.toString() + "FriendlyName";
                        var optionXFilePI = "option" + myPosition.toString() + "FilePI";

                        var dataTemp = data.data.data;
                        angular.forEach($scope.data.labelingPackagingDetails, function (item) {
                            var myItem = dataTemp.labelingPackagingDetails.filter(o => o.labelingPackagingDetailID === item.labelingPackagingDetailID);
                            if (myItem.length > 0 && myItem[optionXFile] !== null) {
                                if (item[optionXFile] === null || item[0][optionXFile] === '') {
                                    item[optionXFileUrl] = myItem[0][optionXFileUrl];
                                    item[optionXFriendlyName] = myItem[0][optionXFriendlyName];
                                    item[optionXFile] = myItem[0][optionXFile];
                                    item[optionXFilePI] = myItem[0][optionXFilePI];
                                }
                            }
                        });
                        $scope.$apply();
                        //$scope.method.refresh(context.id);
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },

        autoUpdate: function ($event) {
            $event.preventDefault();

            if ($scope.editForm.$valid) {
                jsonService.autoupdate(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            // Approve notification
                            if ($scope.data.lpStatusID == 6) {
                                $scope.method.backUrl();
                            } else {
                                $scope.method.refresh(data.data.labelingPackagingID);
                            }

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

        init: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.lpStatuses = data.data.lpStatuses;
                    $scope.data.labelingPackagingRemarks = data.data.data.labelingPackagingRemarks;
                    $scope.options = data.data.options;

                    if ($scope.data.lpStatusID == 6) {
                        alert('This LP has been aprroved. PLease check in LP Overview!');
                        return false;
                    }

                    if ($scope.data !== null) {
                        $scope.event.showOptions();
                    }
                    $scope.$apply();
                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.factories = null;
                    $scope.$apply();
                }
            );
        },

        update: function ($event) {
            $event.preventDefault();

            if ($scope.data.lpStatusID == 6) {
                if (!confirm('Are you sure you want to approve this LP ?')) {
                    return false;
                }
            }


            // Force to fill rejected reason
            for (var i = 0; i < $scope.data.labelingPackagingRejections.length; i++) {
                var item = $scope.data.labelingPackagingRejections[i];

                var rejectedReason = item.labelingPackagingRejectionComment;

                if ($scope.data.lpStatusID === 5 && rejectedReason === null) {
                    alert('Reasons For Rejection must be fill in !');

                    jQuery('#rejectComment').addClass('state-error');

                    return false;
                } else {
                    jQuery('#rejectComment').removeClass('state-error');
                }
            }

            // Get LP status name
            $scope.method.getLPStatusName($scope.data.lpStatusID);

            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            // Approve notification
                            if ($scope.data.lpStatusID == 6) {
                                $scope.method.backUrl();
                            } else {
                                $scope.method.refresh(data.data.labelingPackagingID);
                            }

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

        delete: function ($event) {
            $event.preventDefault();
            if (confirm('Are you sure ?')) {
                jsonService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            window.location = context.backURL;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },

        generateExcel: function (id) {
            var labelingPackagingId = id;
            jsonService.getExcelData(
                labelingPackagingId,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        // Other File
        addOtherFile: function () {
            userFileMng.isMultiSelectActivated = true;
            userFileMng.autoResizeImage = false;

            userFileMng.callback = function () {
                scope.$apply(function () {
                    var d = new Date();
                    var n = d.getMilliseconds();

                    angular.forEach(userFileMng.selectedFiles, function (value, key) {
                        $scope.data.labelingPackagingOtherFiles.push({
                            labelingPackagingOtherFileID: scope.method.getNewID(),
                            remark: null,

                            otherFileFriendlyName: value.fileName,
                            otherFileUrl: (value.filePath.indexOf('?') < 0) ? value.filePath + '?' + n : value.filePath + n,
                            otherFileHasChange: true,
                            newOtherFile: value.fileName,
                        });
                    }, null);
                });
            };

            userFileMng.open();
        },

        removeOtherFile: function (item) {
            var index = $scope.data.labelingPackagingOtherFiles.indexOf(item);
            $scope.data.labelingPackagingOtherFiles.splice(index, 1);
        },

        // Reject Comment File
        changeRejectCommentFile: function ($event, controlID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.rejectCommentFile_FileUrl = img.fileURL;
                        scope.data.rejectCommentFile_FriendlyName = img.filename;
                        scope.data.rejectCommentFileHasChange = true;
                        scope.data.newRejectCommentFile = img.filename;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeRejectCommentFile: function ($event, controlID) {
            $scope.data.rejectCommentFile_FileUrl = '';
            $scope.data.rejectCommentFile_FriendlyName = '';
            $scope.data.rejectCommentFileHasChange = true;
            $scope.data.newRejectCommentFile = '';
        },

        // Hangtag
        changeHangtag: function ($event, controlID, typeID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        switch (typeID) {
                            case 1:
                                var arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
                                break;
                            case 2:
                                var arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID == controlID });
                                break;
                        }
                        $(arr).each(function () {
                            this.hangTagFileUrl = img.fileURL;
                            this.hangTagFriendlyName = img.filename;
                            this.hangTagFileHasChange = true;
                            this.newHangTagFile = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeHangtag: function ($event, controlID, typeID) {
            switch (typeID) {
                case 1:
                    var arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
                    break;
                case 2:
                    var arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID == controlID });
                    break;
            }
            $(arr).each(function () {
                this.hangTagFileUrl = '';
                this.hangTagFriendlyName = '';
                this.hangTagFileHasChange = true;
                this.newHangTagFile = '';
                this.hangTagFile = '';
                this.hangTagFilePI = '';
            });
        },

        // Box/shippingmark	
        changeBoxAndShippingmark: function ($event, controlID, typeID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        switch (typeID) {
                            case 1:
                                var arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
                                break;
                            case 2:
                                var arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID == controlID });
                                break;
                        }
                        $(arr).each(function () {
                            this.boxShippingMarkFileUrl = img.fileURL;
                            this.boxShippingMarkFriendlyName = img.filename;
                            this.boxShippingMarkFileHasChange = true;
                            this.newBoxShippingMarkFile = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeBoxAndShippingmark: function ($event, controlID, typeID) {
            switch (typeID) {
                case 1:
                    var arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
                    break;
                case 2:
                    var arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID == controlID });
                    break;
            }
            $(arr).each(function () {
                this.boxShippingMarkFileUrl = '';
                this.boxShippingMarkFriendlyName = '';
                this.boxShippingMarkFileHasChange = true;
                this.newBoxShippingMarkFile = '';
                this.boxShippingMarkFile = '';
                this.boxShippingMarkFilePI = '';
            });
        },

        // Brass label
        changeBrassLabel: function ($event, controlID, typeID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        switch (typeID) {
                            case 1:
                                var arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
                                break;
                            case 2:
                                var arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID == controlID });
                                break;
                        }
                        $(arr).each(function () {
                            this.brassLabelFileUrl = img.fileURL;
                            this.brassLabelFriendlyName = img.filename;
                            this.brassLabelFileHasChange = true;
                            this.newBrassLabelFile = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeBrassLabel: function ($event, controlID, typeID) {
            switch (typeID) {
                case 1:
                    var arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
                    break;
                case 2:
                    var arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID == controlID });
                    break;
            }
            $(arr).each(function () {
                this.brassLabelFileUrl = '';
                this.brassLabelFriendlyName = '';
                this.brassLabelFileHasChange = true;
                this.newBrassLabelFile = '';
                this.brassLabelFile = '';
                this.brassLabelFilePI = '';
            });
        },

        // AI
        changeAi: function ($event, controlID, typeID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        switch (typeID) {
                            case 1:
                                var arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
                                break;
                            case 2:
                                var arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID == controlID });
                                break;
                        }
                        $(arr).each(function () {
                            this.aiFileUrl = img.fileURL;
                            this.aiFriendlyName = img.filename;
                            this.aiFileHasChange = true;
                            this.newAiFile = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeAi: function ($event, controlID, typeID) {
            switch (typeID) {
                case 1:
                    var arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
                    break;
                case 2:
                    var arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID == controlID });
                    break;
            }
            $(arr).each(function () {
                this.aiFriendlyName = '';
                this.aiFileHasChange = true;
                this.newAiFile = '';
                this.aiFile = '';
                this.aiFilePI = '';
            });
        },

        // Wash Cushion Label
        changeWashCushionLabel: function ($event, controlID, typeID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        switch (typeID) {
                            case 1:
                                var arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
                                break;
                            case 2:
                                var arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID == controlID });
                                break;
                        }
                        $(arr).each(function () {
                            this.washCushionLabelFileUrl = img.fileURL;
                            this.washCushionLabelFriendlyName = img.filename;
                            this.washCushionLabelFileHasChange = true;
                            this.newWashCushionLabelFile = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeWashCushionLabel: function ($event, controlID, typeID) {
            switch (typeID) {
                case 1:
                    var arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
                    break;
                case 2:
                    var arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID == controlID });
                    break;
            }
            $(arr).each(function () {
                this.washCushionLabelFriendlyName = '';
                this.washCushionLabelFileHasChange = true;
                this.newWashCushionLabelFile = '';
                this.washCushionLabelFile = '';
                this.washCushionLabelFilePI = '';
            });
        },

        // FSC Label
        changeFSCLabel: function ($event, controlID, typeID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        switch (typeID) {
                            case 1:
                                var arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
                                break;
                            case 2:
                                var arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID == controlID });
                                break;
                        }
                        $(arr).each(function () {
                            this.fscLabelFileUrl = img.fileURL;
                            this.fscLabelFriendlyName = img.filename;
                            this.fscLabelFileHasChange = true;
                            this.newFSCLabelFile = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeFSCLabel: function ($event, controlID, typeID) {
            switch (typeID) {
                case 1:
                    var arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
                    break;
                case 2:
                    var arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID == controlID });
                    break;
            }
            $(arr).each(function () {
                this.fscLabelFriendlyName = '';
                this.fscLabelFileHasChange = true;
                this.newFSCLabelFile = '';
                this.fscLabelFile = '';
            });
        },

        //Option 1
        changeOption1: function ($event, controlID, typeID) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    var arr;
                    switch (typeID) {
                        case 1:
                            arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                            break;
                        case 2:
                            arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                            break;
                    }
                    $(arr).each(function () {
                        this.option1FileUrl = img.fileURL;
                        this.option1FriendlyName = img.filename;
                        this.option1FileHasChange = true;
                        this.newOption1File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },
        removeOption1: function ($event, controlID, typeID) {
            var arr;
            switch (typeID) {
                case 1:
                    arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                    break;
                case 2:
                    arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                    break;
            }
            $(arr).each(function () {
                this.option1FriendlyName = '';
                this.option1FileHasChange = true;
                this.newOption1File = '';
            });
        },

        //Option 2
        changeOption2: function ($event, controlID, typeID) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    var arr;
                    switch (typeID) {
                        case 1:
                            arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                            break;
                        case 2:
                            arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                            break;
                    }
                    $(arr).each(function () {
                        this.option2FileUrl = img.fileURL;
                        this.option2FriendlyName = img.filename;
                        this.option2FileHasChange = true;
                        this.newOption2File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },
        removeOption2: function ($event, controlID, typeID) {
            var arr;
            switch (typeID) {
                case 1:
                    arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                    break;
                case 2:
                    arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                    break;
            }
            $(arr).each(function () {
                this.option2FriendlyName = '';
                this.option2FileHasChange = true;
                this.newOption2File = '';
            });
        },

        //Option 3
        changeOption3: function ($event, controlID, typeID) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    var arr;
                    switch (typeID) {
                        case 1:
                            arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                            break;
                        case 2:
                            arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                            break;
                    }
                    $(arr).each(function () {
                        this.option3FileUrl = img.fileURL;
                        this.option3FriendlyName = img.filename;
                        this.option3FileHasChange = true;
                        this.newOption3File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },
        removeOption3: function ($event, controlID, typeID) {
            var arr;
            switch (typeID) {
                case 1:
                    arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID });
                    break;
                case 2:
                    arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID });
                    break;
            }
            $(arr).each(function () {
                this.option3FriendlyName = '';
                this.option3FileHasChange = true;
                this.newOption3File = '';
            });
        },

        //Option 4
        changeOption4: function ($event, controlID, typeID) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    var arr;
                    switch (typeID) {
                        case 1:
                            arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                            break;
                        case 2:
                            arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                            break;
                    }
                    $(arr).each(function () {
                        this.option4FileUrl = img.fileURL;
                        this.option4FriendlyName = img.filename;
                        this.option4FileHasChange = true;
                        this.newOption4File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },
        removeOption4: function ($event, controlID, typeID) {
            var arr;
            switch (typeID) {
                case 1:
                    arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                    break;
                case 2:
                    arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                    break;
            }
            $(arr).each(function () {
                this.option4FriendlyName = '';
                this.option4FileHasChange = true;
                this.newOption4File = '';
            });
        },

        //Option 5
        changeOption5: function ($event, controlID, typeID) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    var arr;
                    switch (typeID) {
                        case 1:
                            arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                            break;
                        case 2:
                            arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                            break;
                    }
                    $(arr).each(function () {
                        this.option5FileUrl = img.fileURL;
                        this.option5FriendlyName = img.filename;
                        this.option5FileHasChange = true;
                        this.newOption5File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },
        removeOption5: function ($event, controlID, typeID) {
            var arr;
            switch (typeID) {
                case 1:
                    arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                    break;
                case 2:
                    arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                    break;
            }
            $(arr).each(function () {
                this.option5FriendlyName = '';
                this.option5FileHasChange = true;
                this.newOption5File = '';
            });
        },

        //Option 6
        changeOption6: function ($event, controlID, typeID) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    var arr;
                    switch (typeID) {
                        case 1:
                            arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                            break;
                        case 2:
                            arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                            break;
                    }
                    $(arr).each(function () {
                        this.option6FileUrl = img.fileURL;
                        this.option6FriendlyName = img.filename;
                        this.option6FileHasChange = true;
                        this.newOption6File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },
        removeOption6: function ($event, controlID, typeID) {
            var arr;
            switch (typeID) {
                case 1:
                    arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                    break;
                case 2:
                    arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                    break;
            }
            $(arr).each(function () {
                this.option6FriendlyName = '';
                this.option6FileHasChange = true;
                this.newOption6File = '';
            });
        },

        //Option 7
        changeOption7: function ($event, controlID, typeID) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    var arr;
                    switch (typeID) {
                        case 1:
                            arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                            break;
                        case 2:
                            arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                            break;
                    }
                    $(arr).each(function () {
                        this.option7FileUrl = img.fileURL;
                        this.option7FriendlyName = img.filename;
                        this.option7FileHasChange = true;
                        this.newOption7File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },
        removeOption7: function ($event, controlID, typeID) {
            var arr;
            switch (typeID) {
                case 1:
                    arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                    break;
                case 2:
                    arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                    break;
            }
            $(arr).each(function () {
                this.option7FriendlyName = '';
                this.option7FileHasChange = true;
                this.newOption7File = '';
            });
        },

        //Option 8
        changeOption8: function ($event, controlID, typeID) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    var arr;
                    switch (typeID) {
                        case 1:
                            arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                            break;
                        case 2:
                            arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                            break;
                    }
                    $(arr).each(function () {
                        this.option8FileUrl = img.fileURL;
                        this.option8FriendlyName = img.filename;
                        this.option8FileHasChange = true;
                        this.newOption8File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },
        removeOption8: function ($event, controlID, typeID) {
            var arr;
            switch (typeID) {
                case 1:
                    arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                    break;
                case 2:
                    arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                    break;
            }
            $(arr).each(function () {
                this.option8FriendlyName = '';
                this.option8FileHasChange = true;
                this.newOption8File = '';
            });
        },

        //Option 9
        changeOption9: function ($event, controlID, typeID) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    var arr;
                    switch (typeID) {
                        case 1:
                            arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                            break;
                        case 2:
                            arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                            break;
                    }
                    $(arr).each(function () {
                        this.option9FileUrl = img.fileURL;
                        this.option9FriendlyName = img.filename;
                        this.option9FileHasChange = true;
                        this.newOption9File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },
        removeOption9: function ($event, controlID, typeID) {
            var arr;
            switch (typeID) {
                case 1:
                    arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                    break;
                case 2:
                    arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                    break;
            }
            $(arr).each(function () {
                this.option9FriendlyName = '';
                this.option9FileHasChange = true;
                this.newOption9File = '';
            });
        },

        //Option 10
        changeOption10: function ($event, controlID, typeID) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    var arr;
                    switch (typeID) {
                        case 1:
                            arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                            break;
                        case 2:
                            arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                            break;
                    }
                    $(arr).each(function () {
                        this.option10FileUrl = img.fileURL;
                        this.option10FriendlyName = img.filename;
                        this.option10FileHasChange = true;
                        this.newOption10File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },
        removeOption10: function ($event, controlID, typeID) {
            var arr;
            switch (typeID) {
                case 1:
                    arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                    break;
                case 2:
                    arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                    break;
            }
            $(arr).each(function () {
                this.option10FriendlyName = '';
                this.option10FileHasChange = true;
                this.newOption10File = '';
            });
        },

        //Option 11
        changeOption11: function ($event, controlID, typeID) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    var arr;
                    switch (typeID) {
                        case 1:
                            arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                            break;
                        case 2:
                            arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                            break;
                    }
                    $(arr).each(function () {
                        this.option11FileUrl = img.fileURL;
                        this.option11FriendlyName = img.filename;
                        this.option11FileHasChange = true;
                        this.newOption11File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },
        removeOption11: function ($event, controlID, typeID) {
            var arr;
            switch (typeID) {
                case 1:
                    arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                    break;
                case 2:
                    arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                    break;
            }
            $(arr).each(function () {
                this.option11FriendlyName = '';
                this.option11FileHasChange = true;
                this.newOption11File = '';
            });
        },

        //Option 12
        changeOption12: function ($event, controlID, typeID) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    var arr;
                    switch (typeID) {
                        case 1:
                            arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                            break;
                        case 2:
                            arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                            break;
                    }
                    $(arr).each(function () {
                        this.option12FileUrl = img.fileURL;
                        this.option12FriendlyName = img.filename;
                        this.option12FileHasChange = true;
                        this.newOption12File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },
        removeOption12: function ($event, controlID, typeID) {
            var arr;
            switch (typeID) {
                case 1:
                    arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                    break;
                case 2:
                    arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                    break;
            }
            $(arr).each(function () {
                this.option12FriendlyName = '';
                this.option12FileHasChange = true;
                this.newOption12File = '';
            });
        },

        //Option 13
        changeOption13: function ($event, controlID, typeID) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    var arr;
                    switch (typeID) {
                        case 1:
                            arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                            break;
                        case 2:
                            arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                            break;
                    }
                    $(arr).each(function () {
                        this.option13FileUrl = img.fileURL;
                        this.option13FriendlyName = img.filename;
                        this.option13FileHasChange = true;
                        this.newOption13File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },
        removeOption13: function ($event, controlID, typeID) {
            var arr;
            switch (typeID) {
                case 1:
                    arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                    break;
                case 2:
                    arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                    break;
            }
            $(arr).each(function () {
                this.option13FriendlyName = '';
                this.option13FileHasChange = true;
                this.newOption13File = '';
            });
        },

        //Option 14
        changeOption14: function ($event, controlID, typeID) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    var arr;
                    switch (typeID) {
                        case 1:
                            arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                            break;
                        case 2:
                            arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                            break;
                    }
                    $(arr).each(function () {
                        this.option14FileUrl = img.fileURL;
                        this.option14FriendlyName = img.filename;
                        this.option14FileHasChange = true;
                        this.newOption14File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },
        removeOption14: function ($event, controlID, typeID) {
            var arr;
            switch (typeID) {
                case 1:
                    arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                    break;
                case 2:
                    arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                    break;
            }
            $(arr).each(function () {
                this.option14FriendlyName = '';
                this.option14FileHasChange = true;
                this.newOption14File = '';
            });
        },

        //Option 15
        changeOption15: function ($event, controlID, typeID) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    var arr;
                    switch (typeID) {
                        case 1:
                            arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                            break;
                        case 2:
                            arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                            break;
                    }
                    $(arr).each(function () {
                        this.option15FileUrl = img.fileURL;
                        this.option15FriendlyName = img.filename;
                        this.option15FileHasChange = true;
                        this.newOption15File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },
        removeOption15: function ($event, controlID, typeID) {
            var arr;
            switch (typeID) {
                case 1:
                    arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID === controlID; });
                    break;
                case 2:
                    arr = scope.data.labelingPackagingSparepartDetails.filter(function (o) { return o.labelingPackagingSparepartDetailID === controlID; });
                    break;
            }
            $(arr).each(function () {
                this.option15FriendlyName = '';
                this.option15FileHasChange = true;
                this.newOption15File = '';
            });
        },

        //
        // Upload all
        //

        // AIOHangTag
        changAIOHangTagFile: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    scope.data.aioHangTagFile_FileUrl = img.fileURL;
                    scope.data.aioHangTagFile_FriendlyName = img.filename;
                    scope.data.aioHangTagFileHasChange = true;
                    scope.data.newAIOHangTagFile = img.filename;

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.hangTagFileUrl = img.fileURL;
                        this.hangTagFriendlyName = img.filename;
                        this.hangTagFileHasChange = true;
                        this.newHangTagFile = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        // AIOBoxShipping
        changAIOBoxShippingMarkFile: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    scope.data.aioBoxShippingMarkFile_FileUrl = img.fileURL;
                    scope.data.aioBoxShippingMarkFile_FriendlyName = img.filename;
                    scope.data.aioBoxShippingMarkFileHasChange = true;
                    scope.data.newAIOBoxShippingMarkFile = img.filename;

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.boxShippingMarkFileUrl = img.fileURL;
                        this.boxShippingMarkFriendlyName = img.filename;
                        this.boxShippingMarkFileHasChange = true;
                        this.newBoxShippingMarkFile = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        // AIOBrassLabel
        changAIOBrassLabelFile: function (id) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    scope.data.aioBrassLabelFile_FileUrl = img.fileURL;
                    scope.data.aioBrassLabelFile_FriendlyName = img.filename;
                    scope.data.aioBrassLabelFileHasChange = true;
                    scope.data.newAIOBrassLabelFile = img.filename;

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.brassLabelFileUrl = img.fileURL;
                        this.brassLabelFriendlyName = img.filename;
                        this.brassLabelFileHasChange = true;
                        this.newBrassLabelFile = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        // AIO AI
        changAIOAIFile: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    scope.data.aioaiFile_FileUrl = img.fileURL;
                    scope.data.aioaiFile_FriendlyName = img.filename;
                    scope.data.aioaiFileHasChange = true;
                    scope.data.newAIOAIFile = img.filename;

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.aiFileUrl = img.fileURL;
                        this.aiFriendlyName = img.filename;
                        this.aiFileHasChange = true;
                        this.newAiFile = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        // AIO Wash Cushion Label
        changAIOWashCushionLabelFile: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    scope.data.aioWashCushionLabelFile_FileUrl = img.fileURL;
                    scope.data.aioWashCushionLabelFile_FriendlyName = img.filename;
                    scope.data.aioWashCushionLabelFileHasChange = true;
                    scope.data.newAIOWashCushionLabelFile = img.filename;

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.washCushionLabelFileUrl = img.fileURL;
                        this.washCushionLabelFriendlyName = img.filename;
                        this.washCushionLabelFileHasChange = true;
                        this.newWashCushionLabelFile = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        // AIO FSC Label
        changAIOFSCLabelFile: function (id) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    scope.data.aiofscLabelFile_FileUrl = img.fileURL;
                    scope.data.aiofscLabelFile_FriendlyName = img.filename;
                    scope.data.aiofscLabelFileHasChange = true;
                    scope.data.newAIOFSCLabelFile = img.filename;

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.fscLabelFileUrl = img.fileURL;
                        this.fscLabelFriendlyName = img.filename;
                        this.fscLabelFileHasChange = true;
                        this.newFSCLabelFile = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        //Option AIO 1
        changAIOOption1File: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    scope.data.aioOption1File_FileUrl = img.fileURL;
                    scope.data.aioOption1File_FriendlyName = img.filename;
                    scope.data.aioOption1FileHasChange = true;
                    scope.data.newAIOOption1File = img.filename;

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.option1FileUrl = img.fileURL;
                        this.option1FriendlyName = img.filename;
                        this.option1FileHasChange = true;
                        this.newOption1File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        //Option AIO 2
        changAIOOption2File: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    scope.data.aioOption2File_FileUrl = img.fileURL;
                    scope.data.aioOption2File_FriendlyName = img.filename;
                    scope.data.aioOption2FileHasChange = true;
                    scope.data.newAIOOption2File = img.filename;

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.option2FileUrl = img.fileURL;
                        this.option2FriendlyName = img.filename;
                        this.option2FileHasChange = true;
                        this.newOption2File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        //Option AIO 3
        changAIOOption3File: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];
                    scope.data.aioOption3File_FileUrl = img.fileURL;
                    scope.data.aioOption3File_FriendlyName = img.filename;
                    scope.data.aioOption3FileHasChange = true;
                    scope.data.newAIOOption3File = img.filename;

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.option3FileUrl = img.fileURL;
                        this.option3FriendlyName = img.filename;
                        this.option3FileHasChange = true;
                        this.newOption3File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        //Option AIO 4
        changAIOOption4File: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.option4FileUrl = img.fileURL;
                        this.option4FriendlyName = img.filename;
                        this.option4FileHasChange = true;
                        this.newOption4File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        //Option AIO 5
        changAIOOption5File: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.option5FileUrl = img.fileURL;
                        this.option5FriendlyName = img.filename;
                        this.option5FileHasChange = true;
                        this.newOption5File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        //Option AIO 6
        changAIOOption6File: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.option6FileUrl = img.fileURL;
                        this.option6FriendlyName = img.filename;
                        this.option6FileHasChange = true;
                        this.newOption6File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        //Option AIO 7
        changAIOOption7File: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.option7FileUrl = img.fileURL;
                        this.option7FriendlyName = img.filename;
                        this.option7FileHasChange = true;
                        this.newOption7File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        //Option AIO 8
        changAIOOption8File: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.option8FileUrl = img.fileURL;
                        this.option8FriendlyName = img.filename;
                        this.option8FileHasChange = true;
                        this.newOption8File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        //Option AIO 9
        changAIOOption9File: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.option9FileUrl = img.fileURL;
                        this.option9FriendlyName = img.filename;
                        this.option9FileHasChange = true;
                        this.newOption9File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        //Option AIO 10
        changAIOOption10File: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.option10FileUrl = img.fileURL;
                        this.option10FriendlyName = img.filename;
                        this.option10FileHasChange = true;
                        this.newOption10File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        //Option AIO 11
        changAIOOption11File: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.option11FileUrl = img.fileURL;
                        this.option11FriendlyName = img.filename;
                        this.option11FileHasChange = true;
                        this.newOption11File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        //Option AIO 12
        changAIOOption12File: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.option12FileUrl = img.fileURL;
                        this.option12FriendlyName = img.filename;
                        this.option12FileHasChange = true;
                        this.newOption12File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        //Option AIO 13
        changAIOOption13File: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.option13FileUrl = img.fileURL;
                        this.option13FriendlyName = img.filename;
                        this.option13FileHasChange = true;
                        this.newOption13File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        //Option AIO 14
        changAIOOption14File: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.option14FileUrl = img.fileURL;
                        this.option14FriendlyName = img.filename;
                        this.option14FileHasChange = true;
                        this.newOption14File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        //Option AIO 15
        changAIOOption15File: function (id) {
            masterUploader.multiSelect = false;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    var img = masterUploader.selectedFiles[0];

                    switch (id) {
                        case 1:
                            var details = scope.data.labelingPackagingDetails;
                            break;
                        case 2:
                            var details = scope.data.labelingPackagingSparepartDetails;
                            break;
                    }

                    $(details).each(function () {
                        this.option15FileUrl = img.fileURL;
                        this.option15FriendlyName = img.filename;
                        this.option15FileHasChange = true;
                        this.newOption15File = img.filename;
                    });
                });
            };
            masterUploader.open();
        },

        // Remark
        addRemark: function ($event) {
            $scope.data.labelingPackagingRemarks.push({
                labelingPackagingRemarkID: $scope.method.getNewID(),
                remark: null,
                remarkFileUrl: null,
                remarkFriendlyName: null,
                remarkFileHasChange: true,
                newRemarkFile: null
            });
            //$scope.$apply();
        },

        removeRemark: function ($event, id) {
            isExist = false;
            for (j = 0; j < $scope.data.labelingPackagingRemarks.length; j++) {
                if ($scope.data.labelingPackagingRemarks[j].labelingPackagingRemarkID == id) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.data.labelingPackagingRemarks.splice(j, 1);
            }
        },

        changeRemarkFile: function ($event, controlID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        var arr = scope.data.labelingPackagingRemarks.filter(function (o) { return o.labelingPackagingRemarkID == controlID });
                        //console.log(arr);
                        $(arr).each(function () {
                            this.remarkFileUrl = img.fileURL;
                            this.remarkFriendlyName = img.filename;
                            this.remarkFileHasChange = true;
                            this.newRemarkFile = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },

        removeRemarkFile: function ($event, controlID) {
            var arr = $scope.data.labelingPackagingRemarks.filter(function (o) { return o.labelingPackagingRemarkID == controlID });
            $(arr).each(function () {
                this.remarkFileUrl = '';
                this.remarkFriendlyName = '';
                this.remarkFileHasChange = true;
                this.newRemarkFile = '';
            });
        },

        remarkText: function ($event, item) {
            $scope.$watch(item, function () {
                item.RemarkTextHasChange = true;
            });
        },

        // Remark popup
        changeRemark: function () {
            jQuery('#remarkForm').modal('show');
        },

        // Rejections
        addRejections: function addRejections() {
            $scope.data.labelingPackagingRejections.push({
                labelingPackagingRejectionID: $scope.method.getNewID(),
                labelingPackagingRejectionComment: null,
                labelingPackagingRejectionFriendlyName: null,
                labelingPackagingRejectionFileLocation: null,
                labelingPackagingRejectionThumbnailLocation: null,
                labelingPackagingRejectionFileHasChange: true,
                labelingPackagingRejectionFileNew: null,
            });
        },

        changeRejections: function changeRejections($event, controlID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        var arr = scope.data.labelingPackagingRejections.filter(function (o) { return o.labelingPackagingRejectionID == controlID });
                        $(arr).each(function () {
                            this.labelingPackagingRejectionFileLocation = img.fileURL;
                            this.labelingPackagingRejectionFriendlyName = img.filename;
                            this.labelingPackagingRejectionFileHasChange = true;
                            this.labelingPackagingRejectionFileNew = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },

        removeRejections: function removeRejections($event, item) {
            var index = $scope.data.labelingPackagingRejections.indexOf(item);
            $scope.data.labelingPackagingRejections.splice(index, 1);
        },

        removeRejectionFile: function ($event, controlID) {
            var arr = $scope.data.labelingPackagingRejections.filter(function (o) { return o.labelingPackagingRejectionID == controlID });
            $(arr).each(function () {
                this.labelingPackagingRejectionFileLocation = null;
                this.labelingPackagingRejectionFriendlyName = null;
                this.labelingPackagingRejectionFileHasChange = true;
                this.labelingPackagingRejectionFileNew = null;
            });
        },

        // Send notification from team netherland to team vietnam
        sendNotificationNL2VN: function (factoryID, clientUD, proformaInvoiceNo, factoryUD) {
            $scope.method.getLPStatusName($scope.data.lpStatusID);

            jsonService.sendNotificationNL2VN(
                factoryID,
                clientUD,
                proformaInvoiceNo,
                factoryUD,
                $scope.data.lpStatusNM,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {
                        $scope.method.refresh($scope.data.labelingPackagingID);
                    }
                },
                function (error) {
                });
        },

        addOtherOption: function () {
            if ($scope.data !== null) {
                for (var i = 1; i <= 15; i++) {
                    var optionX = "#Option" + i.toString();
                    //var optionXLabel = "option" + i.toString() + "Label";
                    if ($(optionX).hasClass('displaynone')) {
                        $(optionX).removeClass("displaynone");
                        break;
                    }
                }
            }
        },

        removeOption: function (position) {
            var optionX = "#Option" + position.toString();
            var optionXLabel = "option" + position.toString() + "Label";
            var optionXStatusID = "option" + position.toString() + "StatusID";
            //Remove
            $scope.data[optionXLabel] = null;
            $scope.data[optionXStatusID] = null;
            $(optionX).addClass("displaynone");
        },

        selectedApporve: function () {
            if ($scope.data.lpStatusID === 6) {
                $scope.data.isSaveConfig = true;
            }
        },

        //Show options
        showOptions: function () {
            for (var i = 1; i <= 15; i++) {
                var optionX = "#Option" + i.toString();
                var optionXLabel = "option" + i.toString() + "Label";
                if ($scope.data[optionXLabel] === undefined) {
                    return;
                }
                if ($scope.data[optionXLabel] !== null && $scope.data[optionXLabel] !== "") {
                    $(optionX).removeClass("displaynone");
                }
            }
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshURL + id;
        },

        getNewID: function () {
            $scope.newid--;
            return $scope.newid;
        },

        backUrl: function () {
            jsHelper.loadingSwitch(true);
            window.location = context.backURL;
        },

        getLPStatusName: function (lpStatusID) {
            for (var i = 0; i < $scope.lpStatuses.length; i++) {
                var item = $scope.lpStatuses[i];
                if (item.lpStatusID === lpStatusID) {
                    $scope.data.lpStatusNM = item.lpStatusNM;
                    return;
                }
            }
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);