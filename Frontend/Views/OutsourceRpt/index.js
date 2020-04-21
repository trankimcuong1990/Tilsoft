(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', outsourceRptController);
    outsourceRptController.$inject = ['$scope', '$timeout', 'dataService'];

    function outsourceRptController($scope, $timeout, outsourceRptService) {
        $scope.productionTeams = [];
        $scope.workOrderStatuses = [];

        $scope.includeMaterial = true;
        $scope.includeComponent = true;

        $scope.outsourceWorkOrder = [];
        $scope.outsourceProductionItem = [];
        $scope.outsourceDocumentNote = [];
        $scope.totalRows = 0;

        $scope.filters = {
            productionTeamID: null,
            clientUD: '',
            proformaInvoiceNo: '',
            modelUD: '',
            modelNM: '',
            workOrderUD: '',
            workOrderStatusID: null,
            productionItemTypeIDs: '',
        };

        $scope.event = {
            initialize: initialize,
            search: search,
            openProductionItem: openProductionItem,
            openDocumentNote: openDocumentNote,
            exportOutsourceReport: exportOutsourceReport,
        };

        function initialize() {
            outsourceRptService.serviceUrl = context.serviceUrl;
            outsourceRptService.token = context.token;

            outsourceRptService.getInit(
                function (data) {
                    $scope.productionTeams = data.data.productionTeam;
                    $scope.workOrderStatuses = data.data.workOrderStatus;

                    jQuery('#content').show();
                },
                function (error) {
                });
        };

        function search(isLoadMore) {
            outsourceRptService.searchFilter.pageSize = context.pageSize;
            outsourceRptService.searchFilter.filters = $scope.filters;

            $scope.filters.productionItemTypeIDs = '';

            if ($scope.includeComponent) {
                $scope.filters.productionItemTypeIDs = $scope.filters.productionItemTypeIDs + '1';
            }

            if ($scope.includeMaterial) {
                if ($scope.filters.productionItemTypeIDs !== '') {
                    $scope.filters.productionItemTypeIDs = $scope.filters.productionItemTypeIDs + ', ';
                }
                $scope.filters.productionItemTypeIDs = $scope.filters.productionItemTypeIDs + '2';
            }

            outsourceRptService.search(
                function (data) {
                    $scope.outsourceWorkOrder = data.data.outsourceWorkOrder;
                    $scope.outsourceProductionItem = data.data.outsourceProductionItem;
                    $scope.outsourceDocumentNote = data.data.outsourceDocumentNote;
                    $scope.totalRows = data.totalRows;

                    angular.forEach($scope.outsourceWorkOrder, function (workOrder) {
                        workOrder.isOpenProductionItem = false;
                    });
                },
                function (error) {
                });
        };

        function openProductionItem(workOrder) {
            if (workOrder !== null) {
                if (!workOrder.isOpenProductionItem) {
                    workOrder.isOpenProductionItem = true;

                    outsourceRptService.getProductionItem(
                        {
                            filters: {
                                productionTeamID: $scope.filters.productionTeamID,
                                clientUD: $scope.filters.clientUD,
                                proformaInvoiceNo: $scope.filters.proformaInvoiceNo,
                                modelUD: $scope.filters.modelUD,
                                modelNM: $scope.filters.modelNM,
                                workOrderUD: workOrder.workOrderUD,
                                workOrderStatusID: $scope.filters.workOrderStatusID,
                                productionItemTypeIDs: $scope.filters.productionItemTypeIDs,
                            }
                        },
                        function (data) {
                            angular.forEach(data.data, function (productionItem) {
                                $scope.outsourceProductionItem.push(productionItem);
                            });

                            angular.forEach($scope.outsourceProductionItem, function (productionItem) {
                                productionItem.isOpenDocumentNote = false;
                            });

                            $scope.$apply();
                        },
                        function (error) {
                        });
                } else {
                    workOrder.isOpenProductionItem = false;

                    for (var i = ($scope.outsourceProductionItem.length - 1) ; i >= 0; i--) {
                        var productionItem = $scope.outsourceProductionItem[i];
                        if (productionItem.workOrderID === workOrder.workOrderID) {
                            var index = $scope.outsourceProductionItem.indexOf(productionItem);
                            $scope.outsourceProductionItem.splice(index, 1);
                        }
                    }
                }
            }
        };

        function openDocumentNote(workOrder, productionItem) {
            if (workOrder !== null && productionItem !== null) {
                if (!productionItem.isOpenDocumentNote) {
                    productionItem.isOpenDocumentNote = true;

                    outsourceRptService.getDocumentNote(
                        {
                            filters: {
                                productionTeamID: $scope.filters.productionTeamID,
                                clientUD: $scope.filters.clientUD,
                                proformaInvoiceNo: $scope.filters.proformaInvoiceNo,
                                modelUD: $scope.filters.modelUD,
                                modelNM: $scope.filters.modelNM,
                                workOrderUD: workOrder.workOrderUD,
                                workOrderStatusID: $scope.filters.workOrderStatusID,
                                productionItemTypeIDs: $scope.filters.productionItemTypeIDs,
                                productionItemID: productionItem.productionItemID,
                            }
                        },
                        function (data) {
                            angular.forEach(data.data, function (documentNote) {
                                $scope.outsourceDocumentNote.push(documentNote);
                            });

                            $scope.$apply();
                        },
                        function (error) {
                        });
                } else {
                    productionItem.isOpenDocumentNote = false;

                    for (var i = ($scope.outsourceDocumentNote.length - 1) ; i >= 0; i--) {
                        var documentNote = $scope.outsourceDocumentNote[i];
                        if (documentNote.workOrderID === workOrder.workOrderID && documentNote.productionItemID === productionItem.productionItemID) {
                            var index = $scope.outsourceDocumentNote.indexOf(documentNote);
                            $scope.outsourceDocumentNote.splice(index, 1);
                        }
                    }
                }
            }
        };

        function exportOutsourceReport() {
            $scope.filters.productionItemTypeIDs = '';

            if ($scope.includeComponent) {
                $scope.filters.productionItemTypeIDs = $scope.filters.productionItemTypeIDs + '1';
            }

            if ($scope.includeMaterial) {
                if ($scope.filters.productionItemTypeIDs !== '') {
                    $scope.filters.productionItemTypeIDs = $scope.filters.productionItemTypeIDs + ', ';
                }
                $scope.filters.productionItemTypeIDs = $scope.filters.productionItemTypeIDs + '2';
            }

            outsourceRptService.exportOutsourceReport(
                {
                    filters: {
                        productionTeamID: $scope.filters.productionTeamID,
                        clientUD: $scope.filters.clientUD,
                        proformaInvoiceNo: $scope.filters.proformaInvoiceNo,
                        modelUD: $scope.filters.modelUD,
                        modelNM: $scope.filters.modelNM,
                        workOrderStatusID: $scope.filters.workOrderStatusID,
                        productionItemTypeIDs: $scope.filters.productionItemTypeIDs,
                    },
                },
                function (data) {
                    if (data.message.type == 0) {
                        window.location = context.backendReportUrl + data.data;
                    } else {
                        jsHelper.showMessage('warning', data.message.message);
                    }
                },
                function (error) {
                });
        };

        $scope.method = {
            getTotalDelivering: getTotalDelivering,
            getTotalReceiving: getTotalReceiving,
            getTotalEnding: getTotalEnding,
        };

        function getTotalDelivering() {
            var totalDelivering = 0;

            angular.forEach($scope.outsourceWorkOrder, function (workOrder) {
                totalDelivering = totalDelivering + workOrder.totalDelivering;
            });

            return totalDelivering;
        };

        function getTotalReceiving() {
            var totalReceiving = 0;

            angular.forEach($scope.outsourceWorkOrder, function (workOrder) {
                totalReceiving = totalReceiving + workOrder.totalReceiving;
            });

            return totalReceiving;
        };

        function getTotalEnding() {
            var totalEnding = 0;

            angular.forEach($scope.outsourceWorkOrder, function (workOrder) {
                totalEnding = totalEnding + (workOrder.totalDelivering - workOrder.totalReceiving);
            });

            return totalEnding;
        };

        $timeout(function () {
            $scope.event.initialize();
        }, 0);
    };
})();