(function () {
    'use strict';
    //
    // SUPPORT
    //
    $('.search-filter').keypress(function (e) {
        if (e.keyCode === 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.reload();
        }
    });
    var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);
    tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
        dataService.searchFilter.pageSize = context.pageSize;
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;

        $scope.reportData = [];
        $scope.pagePosition = {
            gridProductTop: null,
            gridProductLeft: null
        };
        $scope.pageIndex = 1;

        //----------
        $scope.searchFilter = {
            filters: {},
            pageSize: 25,
            pageIndex: 1
        };
        //
        // grid handler
        //
        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    $scope.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.reportData;
                $scope.pageIndex = 1;
                $scope.searchFilter.pageIndex = $scope.pageIndex;
                $scope.event.search();
            },
            isTriggered: false,
            getScrollPosistion: function (topValue, leftValue) {
                $scope.pagePosition.gridProductTop = null;
                $scope.pagePosition.gridProductLeft = null;
            }
        };
        //
        // property
        //
        $scope.dataModel = [];

        $scope.support = {
            clients: [],
            productionTeams: []
        };
        $scope.filters = {
            clientID: null,
            productionTeamID: null,
            fromDate: '',
            toDate: '',
            isOutSource: true,
            workCenterID: 9
        };
        //
        // events
        //
        $scope.event = {
            init: function () {
                dataService.getInitReport(
                    function (data) {
                        $scope.support.clients = data.data.clients;
                        $scope.support.productionTeams = data.data.productionTeams;
                    },
                    function (err) {

                    }
                );
                jQuery('#content').show();
                //$scope.event.reload();
            },

            reload: function () {
                $scope.reportData = [];
                $scope.searchFilter.pageIndex = 1;
                $scope.searchFilter.pageSize = 25;
                $scope.event.search();
            },

            search: function (isLoadMore) {
                //
                // store filter in cookies
                //
                if ($scope.filters.productionTeamID === '') { $scope.filters.productionTeamID = null; }
                if ($scope.filters.clientID === '') { $scope.filters.clientID = null; }
                if ($scope.filters.fromDate === null || $scope.filters.fromDate === '') { jsHelper.showMessage('warning', 'select from date first'); return; }
                if ($scope.filters.toDate === null || $scope.filters.toDate === '') { jsHelper.showMessage('warning', 'select to date first'); return; }
                $scope.searchFilter.filters = $scope.filters;
                dataService.getReportData(
                    $scope.searchFilter,
                    function (data) {
                        Array.prototype.push.apply($scope.reportData, data.data.reportDataDTOs);
                        $scope.totalPage = Math.ceil(data.data.totalRows / $scope.searchFilter.pageSize);
                        $scope.totalRows = data.data.totalRows;
                        jQuery('#content').show();

                        if (!isLoadMore) {
                            $scope.gridHandler.goTop();
                        }
                        $scope.gridHandler.isTriggered = true;
                    },
                    function (error) {
                        $scope.reportData = null;
                    }
                );
            },

            gerExcelReport: function () {
                //
                // store filter in cookies
                //
                if ($scope.filters.productionTeamID === '') { $scope.filters.productionTeamID = null; }
                if ($scope.filters.clientID === '') { $scope.filters.clientID = null; }
                if ($scope.filters.fromDate === null || $scope.filters.fromDate === '') { jsHelper.showMessage('warning', 'select from date first'); return; }
                if ($scope.filters.toDate === null || $scope.filters.toDate === '') { jsHelper.showMessage('warning', 'select to date first'); return; }
                dataService.getExcelReportData(
                    $scope.filters,
                    function (data) {
                        window.location = context.backendReportUrl + data.data;
                    },
                    function (error) {
                        
                    }
                );
            },

            clearFilter: function () {
                $scope.filters = {
                    clientID: null,
                    productionTeamID: null,
                    fromDate: '',
                    toDate: '',
                    isOutSource: true
                };
            },

            getReportDetail: function (item) {
                if ($scope.filters.productionTeamID === '') { $scope.filters.productionTeamID = null; }
                if ($scope.filters.clientID === '') { $scope.filters.clientID = null; }
                if ($scope.filters.fromDate === null || $scope.filters.fromDate === '') { jsHelper.showMessage('warning', 'select from date first'); return; }
                if ($scope.filters.toDate === null || $scope.filters.toDate === '') { jsHelper.showMessage('warning', 'select to date first'); return; }

                var param = {};
                param.clientID = $scope.filters.clientID;
                param.productionTeamID = $scope.filters.productionTeamID;
                param.fromDate = $scope.filters.fromDate;
                param.toDate = $scope.filters.toDate;
                param.isOutSource = $scope.filters.isOutSource;
                param.workCenterID = $scope.filters.workCenterID;
                param.selectedModelID = item.modelID;
                param.selectedProductionTeamID = item.productionTeamID;
                param.selectedClientID = item.clientID;
                param.selectedWorkCenterID = item.workCenterID;       


                //start load data detail
                if (item.isShowDetail == undefined) {
                    item.isShowDetail = false;
                }
                if (item.isShowDetail) {
                    item.isShowDetail = !item.isShowDetail;
                    return;
                }

                dataService.getReportDataDetail(
                    param,
                    function (data) {
                        item.isShowDetail = !item.isShowDetail;                        
                        item.reportDataDetailDTOs = [];
                        angular.forEach(data.data, function (dItem) {
                            item.reportDataDetailDTOs.push(dItem);
                        });                                           
                    },
                    function (error) {
                        $scope.reportData = null;
                    }
                );
            }
        };
        //
        // method
        //
        $scope.method = {

        };
        //
        // angular init
        //
        $timeout(function () {
            $scope.event.init();
        }, 0);
    }]);

})();