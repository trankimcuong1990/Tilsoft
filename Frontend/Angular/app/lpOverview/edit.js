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

    $scope.selectAll = {
        checked: true,
        checkedSparepart: true
    };

    $scope.newid = -1;
    //
    // event
    //

    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                function (data) {
                    console.log(data);
                    $scope.data = data.data.data;
                    $scope.lpStatuses = data.data.lpStatuses;
                    $scope.data.labelingPackagingRemarks = data.data.data.labelingPackagingRemarks;

                    for (var i = 0; i < $scope.data.labelingPackagingDetails.length; i++) {
                        $scope.data.labelingPackagingDetails[i].isSelected = true;
                    }
                    for (var j = 0; j < $scope.data.labelingPackagingSparepartDetails.length; j++) {
                        $scope.data.labelingPackagingSparepartDetails[j].isSelected = true;
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
                    $scope.factories = null;
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
                            $scope.method.refresh(data.data.labelingPackagingID);
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
        revise: function ($event) {
            $event.preventDefault();
            if (confirm('Revise this LP?')) {
                jsonService.revise(
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

        generateExcelNL: function (id) {
            var labelingPackagingId = id;
            jsonService.getExcelDataNL(
                labelingPackagingId,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        generateExcelv2: function (id) {

            //Product
            var ListLPDetails = '';
            angular.forEach($scope.data.labelingPackagingDetails, function (item) {
                if (item.isSelected) {
                    if (ListLPDetails !== '') {
                        ListLPDetails += ',';
                    }
                    ListLPDetails += item.labelingPackagingDetailID;
                }
            });
            //
            //Sparepart
            var ListLPSparepartDetails = '';
            angular.forEach($scope.data.labelingPackagingSparepartDetails, function (item) {
                if (item.isSelected) {
                    if (ListLPSparepartDetails !== '') {
                        ListLPSparepartDetails += ',';
                    }
                    ListLPSparepartDetails += item.labelingPackagingSparepartDetailID;
                }
            });
            //
            if (ListLPDetails.length >= 1) {
                ListLPDetails = ListLPDetails.substring(0, ListLPDetails.length);
            }
            else {
                ListLPDetails = "0";
            }

            if (ListLPSparepartDetails.length >= 1) {
                ListLPSparepartDetails = ListLPSparepartDetails.substring(0, ListLPSparepartDetails.length);
            }
            else {
                ListLPSparepartDetails = "0";
            }

            if (ListLPDetails == "0" && ListLPSparepartDetails == "0") {
                bootbox.alert('You have to select Product/Sparepart before print');
                return;
            }

            var labelingPackagingId = id;
            jsonService.getExcelDataV2(
                labelingPackagingId,
                ListLPDetails,
                ListLPSparepartDetails,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },

        selectAll: function () {
            for (var i = 0; i < $scope.data.labelingPackagingDetails.length; i++) {
                $scope.data.labelingPackagingDetails[i].isSelected = $scope.selectAll.checked;
            }
        },

        selectAllSparepart: function () {
            for (var j = 0; j < $scope.data.labelingPackagingSparepartDetails.length; j++) {
                $scope.data.labelingPackagingSparepartDetails[j].isSelected = $scope.selectAll.checkedSparepart;
            }
        },
        //
        // Other File
        //
        changeOtherFile: function ($event, controlID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.otherFileFileUrl = img.fileURL;
                        scope.data.otherFileFriendlyName = img.filename;
                        scope.data.otherFileHasChange = true;
                        scope.data.newOtherFile = img.filename;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeOtherFile: function ($event, controlID) {
            $scope.data.otherFileUrl = '';
            $scope.data.otherFileFriendlyName = '';
            $scope.data.otherFileHasChange = true;
            $scope.data.newOtherFile = '';
        },

        //
        // Hangtag
        //
        changeHangtag: function ($event, controlID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        var arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
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
        removeHangtag: function ($event, controlID) {
            var arr = $scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
            $(arr).each(function () {
                this.hangTagFileUrl = '';
                this.hangTagFriendlyName = '';
                this.hangTagFileHasChange = true;
                this.newHangTagFile = '';
            });
        },

        //
        // Box/shippingmark	
        //
        changeBoxAndShippingmark: function ($event, controlID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        var arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
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
        removeBoxAndShippingmark: function ($event, controlID) {
            var arr = $scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
            $(arr).each(function () {
                this.boxShippingMarkFileUrl = '';
                this.boxShippingMarkFriendlyName = '';
                this.boxShippingMarkFileHasChange = true;
                this.newBoxShippingMarkFile = '';
            });
        },

        //
        // Brass label
        //
        changeBrassLabel: function ($event, controlID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        var arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
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
        removeBrassLabel: function ($event, controlID) {
            var arr = $scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
            $(arr).each(function () {
                this.brassLabelFileUrl = '';
                this.brassLabelFriendlyName = '';
                this.brassLabelFileHasChange = true;
                this.newBrassLabelFile = '';
            });
        },

        //
        // AI
        //
        changeAi: function ($event, controlID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        var arr = scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
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
        removeAi: function ($event, controlID) {
            var arr = $scope.data.labelingPackagingDetails.filter(function (o) { return o.labelingPackagingDetailID == controlID });
            $(arr).each(function () {
                this.aiFriendlyName = '';
                this.aiFileHasChange = true;
                this.newAiFile = '';
            });
        },

        //
        // Upload all
        //

        // AIOHangTag
        changgAIOHangTagFile: function ($event) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.AIOHangTagFile_FileUrl = img.fileURL;
                        scope.data.AIOHangTagFile_FriendlyName = img.filename;
                        scope.data.AIOHangTagFileHasChange = true;
                        scope.data.newAIOHangTagFile = img.filename;

                        var details = scope.data.labelingPackagingDetails;
                        $(details).each(function () {
                            this.hangTagFileUrl = img.fileURL;
                            this.hangTagFriendlyName = img.filename;
                            this.newHangTagFile = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },
        // AIOBoxShipping
        changgAIOBoxShippingMarkFile: function ($event) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.AIOBoxShippingMarkFile_FileUrl = img.fileURL;
                        scope.data.AIOBoxShippingMarkFile_FriendlyName = img.filename;
                        scope.data.AIOBoxShippingMarkFileHasChange = true;
                        scope.data.newAIOBoxShippingMarkFile = img.filename;

                        var details = scope.data.labelingPackagingDetails;
                        $(details).each(function () {
                            this.boxShippingMarkFileUrl = img.fileURL;
                            this.boxShippingMarkFriendlyName = img.filename;
                            this.newBoxShippingMarkFile = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },
        // AIOBrassLabel
        changgAIOBrassLabelFile: function ($event) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.AIOBrassLabelFile_FileUrl = img.fileURL;
                        scope.data.AIOBrassLabelFile_FriendlyName = img.filename;
                        scope.data.AIOBrassLabelFileHasChange = true;
                        scope.data.newAIOBrassLabelFile = img.filename;

                        var details = scope.data.labelingPackagingDetails;
                        $(details).each(function () {
                            this.brassLabelFileUrl = img.fileURL;
                            this.brassLabelFriendlyName = img.filename;
                            this.newBrassLabelFile = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },
        // AIO AI
        changgAIOAIFile: function ($event) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.AIOAIFile_FileUrl = img.fileURL;
                        scope.data.AIOAIFile_FriendlyName = img.filename;
                        scope.data.AIOAIFileHasChange = true;
                        scope.data.newAIOAIFile = img.filename;

                        var details = scope.data.labelingPackagingDetails;
                        $(details).each(function () {
                            this.aiFileUrl = img.fileURL;
                            this.aiFriendlyName = img.filename;
                            this.newAiFile = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        },

        //
        // Remark
        //
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
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);