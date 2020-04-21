(function () {
    'use strict';

    //
    // AP START
    //
    angular
        .module('tilsoftApp', ['widgets'])
        .controller('tilsoftController', ['$scope', AVFPurchasingInvoiceController]);

    function AVFPurchasingInvoiceController($scope) {
        //
        // property
        //
        $scope.selection = {
            from: null,
            to: null
        };
        $scope.data = null;
        $scope.ledgerAccounts = null;
        $scope.filters = {
            SuppplierUD: '',
            InvoiceNo: ''
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
            init: init,
            generateExcel: generateExcel
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
            if ($scope.filters.SuppplierUD == null) {
                $scope.filters.SuppplierUD = '';
            }

            if ($scope.filters.InvoiceNo == null) {
                $scope.filters.InvoiceNo = '';
            }

            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    $scope.data = data.data.data;
                    injectShowLinks($scope.data);
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    $scope.$apply();

                    if (data.totalRows < jsonService.searchFilter.pageSize) {
                        jQuery('#searchResult').find('ul').hide();
                        jQuery('#searchResult').find('span.more-info').hide();
                    }
                    else {
                        jQuery('#searchResult').find('ul').show();
                        jQuery('#searchResult').find('span.more-info').css('display', 'inline-block');
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
                    item.showUpdatorLink = item.updatorID !== null && item.updatorName !== null;
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
                                if ($scope.data[i].avfPurchasingInvoiceID == data.data) {
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

        function generateExcel() {
            if (!$scope.selection.from) {
                alert('Please select date!');
                return false;
            }
            if (!$scope.selection.to) {
                alert('Please select date!');
                return false;
            }
            jsonService.getExcelReport(
                $scope.selection.from,
                $scope.selection.to,
                function (data) {
                    window.location = context.backendReportUrl + data.data + '.xlsm';
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    }

    //
    // Support
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


