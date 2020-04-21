(function() {
    'use strict';

    //
    // APP START
    //
    angular
        .module('tilsoftApp', ['widgets'])
        .controller('tilsoftController', ['$scope', ForwarderInvoiceController]);

    function ForwarderInvoiceController($scope) {
        //
        // property
        //
        $scope.data = null;
        $scope.filters = {
            BookingID: '',
            InvoiceNo: '',
            AccountNo: ''
        };
        $scope.pageIndex = 1;
        $scope.totalPage = 0;

        //
        // event
        //
        $scope.event = {
            reload: reload,
            search: search,
            delete: deleteItem,
            download: download,
            init: init
        };


        //
        // init
        //
        $scope.event.init();


        //
        // private methods
        //
        function reload() {
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.search();
        }

        function search() {
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    $scope.data = data.data.data;
                    injectShowLinks($scope.data);
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.$apply();

                    if (data.totalRows < jsonService.searchFilter.pageSize) {
                        jQuery('#searchResult').find('ul').hide();
                    }
                    else {
                        jQuery('#searchResult').find('ul').show();
                    }

                    jQuery('#content').show();
                    searchResultGrid.updateLayout();
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.$apply();
                }
            );

            function injectShowLinks(data) {
                if (data === null || typeof (data) === "undefined") return;

                data.forEach(function (item) {
                    item.showUpdatorLink = item.updatedBy !== null && item.fullName !== null;
                });
            }
        }

        function deleteItem(id) {
            if (confirm('Are you sure ?')) {
                jsonService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].forwarderInvoiceID == data.data) {
                                    j = i;
                                    break;
                                }
                            }

                            if (j >= 0) {
                                $scope.data.splice(j, 1);
                            }

                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        }

        function download(url) {
            window.open(url);
        }

        function init() {
            $scope.event.search();
        }
        
    }

    //
    // SUPPORT
    //
    jQuery('.search-filter').keypress(function (e) {
        if (e.keyCode == 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.reload();
        }
    });

    var searchResultGrid = jQuery('#searchResult').scrollableTable(
        function (currentPage) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                scope.pageIndex = currentPage;
                jsonService.searchFilter.pageIndex = scope.pageIndex;
                scope.event.search();
            });
        },
        function (sortedBy, sortedDirection) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                jsonService.searchFilter.sortedDirection = sortedDirection;
                scope.pageIndex = 1;
                jsonService.searchFilter.pageIndex = scope.pageIndex;
                jsonService.searchFilter.sortedBy = sortedBy;
                scope.event.search();
            });
        }
    );
})();
