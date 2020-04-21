//import { debug } from "util";

(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'ui.select2', 'furnindo-directive']).controller('tilsoftController', qcReportMngController);
    qcReportMngController.$inject = ['$scope', 'dataService'];

    function qcReportMngController($scope, qcReportMngService) {
        // variable
        $scope.data = [];
        $scope.qcReportSectionInspection = [];
        $scope.qcReportSectionSpecific = [];
        $scope.inspectionStages = [];
        $scope.inspectionResults = [];
        $scope.summaryResults = [];
        $scope.packagingMethods = [];
        $scope.testEnvironmentCategorys = [];
        $scope.newid = 0;
        $scope.totalCritical = 0;
        $scope.totalMajor = 0;
        $scope.totalMinor = 0;

        $scope.qcReportDefectID = -1;

        $scope.listConformity = [
            { isConformed: false, name: 'No' },
            { isConformed: true, name: 'Yes' }
        ];
        // event
        $scope.event = {
            get: get,
            update: update,
            deleted: deleted,
            getInspection: getInspection,
            getSpecific: getSpecific,
            addDefect: addDefect,
            removeDefect: removeDefect,
            addImage: addImage,
            changeImage: changeImage,
            removeImage: removeImage,
            addQCReportImage: addQCReportImage,
            changeQCReportImage: changeQCReportImage,
            removeQCReportImage: removeQCReportImage,
            clickSelectInpectionResult: clickSelectInpectionResult,
            printQCReport: printQCReport,
            addReportFile: addReportFile,
            removeReportFile: removeReportFile,
            printQCReportpdf: printQCReportpdf
        };

        $scope.method = {
            getNewID: getNewID,
            isCheckDescriptionInspectionSummary: function (newItem) {
                for (var j = 0; j < $scope.data.qcReportSummaryDTOs.length; j++) {
                    var subItem = $scope.data.qcReportSummaryDTOs[j];
                    if (subItem.description === newItem.description) {
                        return false;
                    }
                }

                return true;
            },
            isCheckDescriptionSpecific: function (newItem) {
                for (var j = 0; j < $scope.data.qcReportDetailDTOs.length; j++) {
                    var subItem = $scope.data.qcReportDetailDTOs[j];
                    if (subItem.name === newItem.name) {

                        return false;
                    }
                }
                return true;
            },
            calculateAmount: function () {
                var amountCritical = 0;
                var amountMajor = 0;
                var amountMinor = 0;
                angular.forEach($scope.data.qcReportDefectDTOs, function (value, key) {

                    //set total critical
                    if (value.critical !== null && value.critical !== '') {
                        amountCritical = amountCritical + parseFloat(value.critical);
                    }
                    else {
                        amountCritical = amountCritical + 0;
                    }

                    //set total major
                    if (value.major !== null && value.major !== '') {
                        amountMajor = amountMajor + parseFloat(value.major);
                    }
                    else {
                        amountMajor = amountMajor + 0;
                    }

                    //set total minor
                    if (value.minor !== null && value.minor !== '') {
                        amountMinor = amountMinor + parseFloat(value.minor);
                    }
                    else {
                        amountMinor = amountMinor + 0;
                    }
                })

                $scope.totalCritical = amountCritical;
                $scope.totalMajor = amountMajor;
                $scope.totalMinor = amountMinor;
            }
        };

        function get() {
            qcReportMngService.serviceUrl = context.serviceUrl;
            qcReportMngService.token = context.token;

            qcReportMngService.getData(
                context.id,
                context.saleOrderDetailID,
                context.factoryID,
                context.itemFactoryOrderLink,
                context.clientID,
                context.articleCode,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.inspectionStages = data.data.supportInspectionStages;
                    $scope.inspectionResults = data.data.supportInspectionResults;
                    $scope.summaryResults = data.data.supportSummaryResults;
                    $scope.packagingMethods = data.data.supportPackagingMethods;
                    $scope.testEnvironmentCategorys = data.data.qcReportTestEnvironmentCategoryDTOs;


                    //Inspection result
                    for (var i = 0; i < $scope.inspectionResults.length; i++) {
                        var item = $scope.inspectionResults[i];

                        item.isInspectionResult = false;
                        if (item.inspectionResultID === scope.data.inspectionResultID) {
                            item.isInspectionResult = true;
                        }
                    }

                    $scope.event.getInspection();

                    $scope.method.calculateAmount();
                    $scope.$apply();
                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function update() {
            if ($scope.data.qcReportFactoryOrderDetailDTOs.length > 0) {
                var check = 0;
                for (var i = 0; i < $scope.data.qcReportFactoryOrderDetailDTOs.length; i++) {
                    var item = $scope.data.qcReportFactoryOrderDetailDTOs[i];
                    if (item.isSelected) {
                        check = 1;
                        break;
                    }
                }
                if (check === 0) {
                    jsHelper.showMessage('warning', 'Please check one or much item in tab PI.');
                    return false;
                }
            }
            if ($scope.editForm.$valid) {
                qcReportMngService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.qcReportID;
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

        function deleted(id) {
            qcReportMngService.delete(
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

        function getInspection() {
            qcReportMngService.getInspection(
                'Inspection Summary',
                function (data) {
                    if (data.message.type === 0) {
                        $scope.qcReportSectionInspection = data.data;
                        // fill data for qcReportSummary
                        if ($scope.data.qcReportSummaryDTOs.length === 0) {
                            $scope.data.qcReportSummaryDTOs = [];
                        }
                        else {
                            var newid = -1;
                            for (var i = 0; i < $scope.qcReportSectionInspection.length; i++) {
                                var item = $scope.qcReportSectionInspection[i];
                                var newItem = {
                                    qcReportSummaryID: newid,
                                    qcReportID: null,
                                    description: item.description,
                                    qcReportSummaryResultID: null,
                                    remark: null
                                };
                                var isPushed = $scope.method.isCheckDescriptionInspectionSummary(newItem);
                                if (isPushed) {
                                    var newItem_1 = {
                                        qcReportSummaryID: newid,
                                        qcReportID: null,
                                        description: newItem.description,
                                        qcReportSummaryResultID: null,
                                        remark: null
                                    };

                                    $scope.data.qcReportSummaryDTOs.push(newItem_1);
                                }
                                newid--;
                            }
                            $scope.event.getSpecific();
                            return;
                        }
                        var newid = -1;

                        for (var i = 0; i < $scope.qcReportSectionInspection.length; i++) {
                            var item = $scope.qcReportSectionInspection[i];
                            var newItem = {
                                qcReportSummaryID: newid,
                                qcReportID: null,
                                description: item.description,
                                qcReportSummaryResultID: null,
                                remark: null
                            };

                            $scope.data.qcReportSummaryDTOs.push(newItem);
                            newid--;
                        }
                        $scope.event.getSpecific();
                        $scope.$apply();
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function getSpecific() {
            qcReportMngService.getInspection(
                'Specific Data Measurement',
                function (data) {
                    if (data.message.type === 0) {
                        $scope.qcReportSectionSpecific = data.data;

                        // fill data for qcReportDetail
                        if ($scope.data.qcReportDetailDTOs.length === 0) {
                            $scope.data.qcReportDetailDTOs = [];
                        }
                        else {
                            var newid = -1;
                            for (var i = 0; i < $scope.qcReportSectionSpecific.length; i++) {
                                var item = $scope.qcReportSectionSpecific[i];
                                var newItem = {
                                    qcReportDetailID: newid,
                                    rowIndex: null,
                                    qcReportID: null,
                                    name: item.description,
                                    unit: null,
                                    specification: null,
                                    actual: null,
                                    tolerance: null
                                };
                                var isPushed = $scope.method.isCheckDescriptionSpecific(newItem);
                                if (isPushed) {
                                    var newItem_1 = {
                                        qcReportDetailID: newid,
                                        rowIndex: null,
                                        qcReportID: null,
                                        name: newItem.name,
                                        unit: null,
                                        specification: null,
                                        actual: null,
                                        tolerance: null
                                    };

                                    $scope.data.qcReportDetailDTOs.push(newItem_1);

                                }
                                newid--;
                            }
                            return;
                        }

                        var newid = -1;

                        for (var i = 0; i < $scope.qcReportSectionSpecific.length; i++) {
                            var item = $scope.qcReportSectionSpecific[i];
                            var newItem = {
                                qcReportDetailID: newid,
                                rowIndex: null,
                                qcReportID: null,
                                name: item.description,
                                unit: null,
                                specification: null,
                                actual: null,
                                tolerance: null
                            };

                            $scope.data.qcReportDetailDTOs.push(newItem);
                            newid--;
                        }
                    }

                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );

        };

        function addReportFile() {
            userFileMng.callback = function () {
                scope.$apply(function () {
                    scope.data.reportScanNewFile = userFileMng.selectedFiles[0].fileName;
                    scope.data.reportFriendlyName = userFileMng.selectedFiles[0].fileName;
                    scope.data.reportScanHasChange = true;
                });
            };
            userFileMng.open();
        };

        function removeReportFile() {
            if (confirm('Are you sure ?')) {
                scope.data.reportScanNewFile = null;
                scope.data.reportFriendlyName = null;
                scope.data.reportScanHasChange = true;
            }
        }

        //function clickTab(tabSelected) {
        //    switch (tabSelected) {
        //        case 1: //
        //            if ($scope.data.qcReportSummaryDTOs.length === 0) {
        //                $scope.event.getInspection();
        //            }
        //            break;
        //        case 3: //
        //            if ($scope.data.qcReportDetailDTOs.length === 0) {
        //                $scope.event.getSpecific();
        //            }

        //            for (var i = 0; i < $scope.inspectionResults.length; i++) {
        //                var item = $scope.inspectionResults[i];

        //                item.isInspectionResult = false;
        //                if (item.inspectionResultID === scope.data.inspectionResultID) {
        //                    item.isInspectionResult = true;
        //                }
        //            }

        //            break;
        //    }
        //};

        function addDefect($event) {
            $event.preventDefault();
            if ($scope.data.qcReportDefectDTOs === null) {
                $scope.data.qcReportDefectDTOs = [];
            }

            $scope.data.qcReportDefectDTOs.push({
                qcReportDefectID: $scope.qcReportDefectID--,
                description: '',
                critical: null,
                major: null,
                minor: null,
                remark: ''
            });
        };

        function removeDefect($event, index) {
            $event.preventDefault();
            $scope.data.qcReportDefectDTOs.splice(index, 1);
        };

        function addImage() {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.thumbnailLocation = img.fileURL;
                        scope.data.scanNewFile = img.filename;
                        scope.data.scanHasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        };

        function changeImage() {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.thumbnailLocation = img.fileURL;
                        scope.data.scanNewFile = img.filename;
                        scope.data.scanHasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        };

        function removeImage() {
            if (confirm('Are you sure ?')) {
                $scope.data.thumbnailLocation = '';
                $scope.data.scanNewFile = '';
                $scope.data.scanHasChange = true;
            }
        };

        function addQCReportImage() {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.qcReportImageDTOs.push({
                            qcReportImageID: scope.method.getNewID(),
                            imageTitle: '',
                            thumbnailLocation: img.fileURL,
                            scanHasChange: true,
                            scanNewFile: img.filename
                        });
                    }, null);
                });
            };
            masterUploader.open();
        };

        function changeQCReportImage(id) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = true;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        var arr = scope.data.qcReportImageDTOs.filter(function (o) { return o.qcReportImageID === id });
                        $(arr).each(function () {
                            this.thumbnailLocation = img.fileURL;
                            this.scanHasChange = true;
                            this.scanNewFile = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        };

        function removeQCReportImage(id) {
            if (confirm('Are you sure ?')) {
                var isExist = false;
                for (var j = 0; j < $scope.data.qcReportImageDTOs.length; j++) {
                    if ($scope.data.qcReportImageDTOs[j].qcReportImageID === id) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    $scope.data.qcReportImageDTOs.splice(j, 1);
                }
            }
        };

        function clickSelectInpectionResult(item) {
            for (var i = 0; i < $scope.inspectionResults.length; i++) {
                var eleItem = $scope.inspectionResults[i];
                if (item.inspectionResultID !== eleItem.inspectionResultID) {
                    eleItem.isInspectionResult = false;
                }
                if (eleItem.isInspectionResult === true) {
                    $scope.data.inspectionResultID = eleItem.inspectionResultID;
                }
            }
        };

        function printQCReport(qcReportID) {
            qcReportMngService.printQCReport(qcReportID,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type !== 2) {
                        window.location = context.backendReportUrl + data.data;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function printQCReportpdf(qcReportID) {
            qcReportMngService.printQCReportpdf(
                qcReportID,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type !== 2) {
                        window.open(context.backendReportUrl + data.data);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }

        function getNewID() {
            $scope.newid--;
            return $scope.newid;
        };
        // default event
        $scope.event.get();
    }
})();