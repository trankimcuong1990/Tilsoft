jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode === 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});
(function () {
    'use strict';   

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
            fromDate: '',
            toDate: '',           
            factoryID: null,
            complianceProcessUD: '',
            complianceCertificateTypeID: null,
            auditStatusID: null,
            clientUD: '',
            clientNM:''
        };

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;
        $scope.pageIndex = 1;
        $scope.totalPage = 0;
        $scope.totalRows = 0;
        $scope.factoryDTOs = [];
        $scope.clientDTOs = [];
        $scope.complianceCertificateTypeDTOs = [];
        $scope.auditStatusDTOs = [];

        //
        // events
        //
        $scope.event = {
            init: function () {
                $scope.event.reload();
            },
            reload: function () {
                $scope.data = [];
                $scope.pageIndex = 1;
                dataService.searchFilter.pageIndex = $scope.pageIndex;
                dataService.searchFilter.sortedBy = 'ExpiredDate';
                $scope.event.search();
            },
            search: function (isLoadMore) {  

                //
                // store filter in cookies
                //
                $cookieStore.put(context.cookieId, $scope.filters);
                $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
                debugger;
                $scope.gridHandler.isTriggered = false;
                dataService.searchFilter.filters = $scope.filters;
                dataService.search(
                    function (data) {
                        Array.prototype.push.apply($scope.data, data.data.data);
                        $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;
                        $scope.factoryDTOs = data.data.factoryDTOs;
                        $scope.clientDTOs = data.data.clientDTOs;
                        $scope.complianceCertificateTypeDTOs = data.data.complianceCertificateTypeDTOs;
                        $scope.auditStatusDTOs = data.data.auditStatusDTOs;
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
                    fromDate: '',
                    toDate: '',
                    factoryID: null,
                    complianceProcessUD: '',
                    complianceCertificateTypeID: null,
                    auditStatusID: null,
                    clientUD: '',
                    clientNM:''
                };
                $scope.event.reload();
            },
            //delete: function (item) {
            //    dataService.delete(
            //        item.id,
            //        null,
            //        function (data) {
            //            if (data.message.type === 0) {
            //                $scope.data.splice($scope.data.indexOf(item), 1);
            //                $scope.totalRows--;
            //            }
            //        },
            //        function (error) {
            //        }
            //    );
            //}
        };

        //
        // angular init
        //
        $timeout(function () {
            $scope.event.init();
        }, 0);
    }]);

})();