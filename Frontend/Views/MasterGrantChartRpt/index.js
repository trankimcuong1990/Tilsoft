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

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
    angular.module('tilsoftApp').controller('tilsoftController', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
        dataService.searchFilter.pageSize = context.pageSize;
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;

        //
        // property
        //
        $scope.data = [];
        $scope.weekInfo = [];
        $scope.statuses = ['FINISHED', 'OVER SHEDULE', 'IN PRODUCTION', 'OPEN'];
        $scope.workCenters = [];
        $scope.filters = {
            StartDateFrom: '',
            StartDateTo: '',
            FinishedDateFrom: '',
            FinishedDateTo: '',
            WorkOrderUD: '',
            ClientUD: '',
            ProformaInvoiceNo: '',
            WorkCenterID: '',
            WorkOrderStatus: ''
        };
        $scope.screen = {
            _WORKORDER: 1,
            _WORKORDER_DETAIL: 2,
        }
        $scope.currentScreen = $scope.screen._WORKORDER;
        //
        // events
        //
        $scope.event = {
            init: function () {
                dataService.getInit(
                    function (data) {
                        if (data.message.type === 0) {
                            $scope.workCenters = data.data.workCenterDTOs;
                        }                        
                        $('#content').show();
                    },
                    function (error) {
                        $scope.workCenters = [];
                    }
                );                
            },
            reload: function () {
                $scope.data = [];
                $scope.event.search();
            },
            search: function () {
                dataService.searchFilter.filters = $scope.filters;
                dataService.search(
                    function (data) {
                        if (data.message.type === 0) {
                            $scope.data = data.data.data;
                            $scope.weekInfo = data.data.weekInfoDTOs;
                        }
                    },
                    function (error) {
                        $scope.data = [];
                        $scope.weekInfo = [];
                        $scope.workCenters = [];
                    }
                );
            }
        }

        //
        // method
        //
        $scope.method = {
            generateBackground: function (wItem, item) {
                var weekIndexToNumber = 0;
                if ((wItem.weekIndex + '').length === 1) {
                    weekIndexToNumber = parseInt(wItem.season.substring(wItem.season.length - 4, wItem.season.length) + '0' + wItem.weekIndex);
                }
                else {
                    weekIndexToNumber = parseInt(wItem.season.substring(wItem.season.length - 4, wItem.season.length) + wItem.weekIndex);
                }

                var weekStartIndexToNumber = 0;
                if ((item.weekStartIndex + '').length === 1) {
                    weekStartIndexToNumber = parseInt(item.seasonStart.substring(item.seasonStart.length - 4, item.seasonStart.length) + '0' + item.weekStartIndex);
                }
                else {
                    weekStartIndexToNumber = parseInt(item.seasonStart.substring(item.seasonStart.length - 4, item.seasonStart.length) + item.weekStartIndex);
                }

                var weekEndIndexToNumber = 0;
                if ((item.weekEndIndex + '').length === 1) {
                    weekEndIndexToNumber = parseInt(item.seasonEnd.substring(item.seasonEnd.length - 4, item.seasonEnd.length) + '0' + item.weekEndIndex);
                }
                else {
                    weekEndIndexToNumber = parseInt(item.seasonEnd.substring(item.seasonEnd.length - 4, item.seasonEnd.length) + item.weekEndIndex);
                }

                if (weekIndexToNumber >= weekStartIndexToNumber && weekIndexToNumber <= weekEndIndexToNumber) {
                    var result = 'background-color: ';
                    switch (item.workOrderStatus) {
                        case 'FINISHED':
                            result = result + '#00ff00';
                            break;

                        case 'OVER SHEDULE':
                            result = result + '#ff0000';
                            break;

                        case 'IN PRODUCTION':
                            result = result + '#390cf8';
                            break;

                        case 'OPEN':
                            result = result + '#9bc2e6';
                            break;
                    }
                    return result;
                }
                return '';
            }
        };


        //
        //workOrder detail screen
        //
        $scope.workOrderDetailSreen = {
            data: [],
            workOrderSelected: null,
            load: function (item) {
                $scope.workOrderDetailSreen.workOrderSelected = item;
                dataService.getWorkOrderProductionDetail(item.workOrderID,
                    (data) => {
                        $scope.workOrderDetailSreen.data = data.data;
                        $scope.currentScreen = $scope.screen._WORKORDER_DETAIL;
                    },
                    (error) => {
                        $scope.workOrderDetailSreen.data = [];
                    });                
            },

            close: function () {
                $scope.currentScreen = $scope.screen._WORKORDER;
            }
        }

        //
        // angular init
        //
        $timeout(function () {
            $scope.event.init();
        }, 0);
    }]);

})();