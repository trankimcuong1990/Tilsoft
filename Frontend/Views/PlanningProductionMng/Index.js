(function () {
    'use strict';

    //
    // SUPPORT
    //
    $('.search-filter').keypress(function (e) {
        if (e.keyCode == 13) {
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
        // grid handler
        //
        $scope.gridHandler = {
            loadMore: function () {
                if ($scope.pageIndex < $scope.totalPage) {
                    $scope.pageIndex++;
                    dataService.searchFilter.pageIndex = $scope.pageIndex;
                    $scope.event.search(true);
                }
            },
            sort: function (sortedBy, sortedDirection) {
                $scope.data = [];
                dataService.searchFilter.sortedDirection = sortedDirection;
                $scope.pageIndex = 1;
                dataService.searchFilter.pageIndex = scope.pageIndex;
                dataService.searchFilter.sortedBy = sortedBy;
                $scope.event.search();
            },
            isTriggered: false
        };

        //
        // property
        //
        $scope.data = [];
        $scope.filters = {
            workOrderUD: '',
            productArticleCode: '',
            productDescription: '',
            clientUD: '',
            proformaInvoiceNo: '',
            hasBOM: 'any',
            workOrderStatusID: null,
            startDate: '',
            finishDate: '',            
        };
        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;
        $scope.pageIndex = 1;
        $scope.totalPage = 0;
        $scope.totalRows = 0;

        //
        // events
        //
        $scope.event = {
            init: function () {
                //dataService.getSearchFilter(
                //    function (data) {
                //        $scope.event.search();
                //    },
                //    function (error) {
                //        // reset data
                //    }
                //);
                $scope.event.reload();
            },
            reload: function () {
                $scope.data = [];
                $scope.pageIndex = 1;
                dataService.searchFilter.pageIndex = $scope.pageIndex;
                dataService.searchFilter.sortedBy = 'WorkOrderUD';
                $scope.event.search();
            },
            search: function (isLoadMore) {
                //
                // store filter in cookies
                //
                $cookieStore.put(context.cookieId, $scope.filters);
                $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

                $scope.gridHandler.isTriggered = false;
                dataService.searchFilter.filters = $scope.filters;
                dataService.search(
                    function (data) {
                        Array.prototype.push.apply($scope.data, data.data.data);
                        $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;
                        jQuery('#content').show();

                        if (!isLoadMore) {
                            $scope.gridHandler.goTop();
                        }
                        $scope.gridHandler.isTriggered = true;
                    },
                    function (error) {
                        $scope.data = null;
                        $scope.pageIndex = 1;
                        $scope.totalPage = 0;
                        $scope.totalRows = 0;
                    }
                );
            },
            clearFilter: function () {
                $scope.filters = {
                    UserUD: '',
                    EmployeeNM: ''
                };
                $scope.event.reload();
            },
            delete: function (item) {
                dataService.delete(
                    item.id, // id of item
                    null,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.data.splice($scope.data.indexOf(item), 1);
                            $scope.totalRows--;
                        }
                    },
                    function (error) {
                    }
                );
            },
            print: function (item) {
                dataService.print(
                    item.id, // id of item
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            // redirect to excel temp file after print out or perform other action
                            window.location = context.backendReportUrl + data.data;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.message.message);
                    }
                );
            }
        }

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