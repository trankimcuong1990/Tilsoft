(function () {
    'use strict';


    angular
        .module('tilsoftApp', ['widgets'])
        .controller('tilsoftController', ['$scope', FactoryMaterialReceiptController]);

    function FactoryMaterialReceiptController($scope) {
        //
        // property
        //
        $scope.filters = {
            receiptNo: '',
            deliverName: '',
            deliverAddress: '',
            receiverName: '',
            receiverAddress: '',
            stockName :''
        };
        $scope.pageIndex = 1;
        $scope.totalPage = 0;

        $scope.data = null;

        //
        // event
        //
        $scope.event = {
            reload: reload,
            search: search,
            delete: deleteItem
        };

        //
        // init
        //
        $scope.event.reload();

        //
        // private methods
        //
        function reload() {
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = 1;
            jsonService.searchFilter.sortedBy = 'receiptNo';
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
                        jQuery('#grdSearchResult').find('ul').hide();
                    }
                    else {
                        jQuery('#grdSearchResult').find('ul').show();
                    }
                    jQuery('#content').show();

                    if (data.message.type == 2) {
                        jsHelper.processMessage(data.message);
                    }

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
                    item.showCreatorLink = item.creatorID !== null && item.creatorName !== null;
                });
            }
        }

        function deleteItem(id, $event) {
            $event.preventDefault();
            if (confirm('Are you sure you want to delete ?')) {
                jsonService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].factoryMaterialReceiptID == data.data) {
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

    var searchResultGrid = jQuery('#grdSearchResult').scrollableTable(
        function (currentPage) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                scope.pageIndex = currentPage;
                jsonService.searchFilter.pageIndex = currentPage;
                scope.event.search();
            });
        },
        function (sortedBy, sortedDirection) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                scope.pageIndex = 1;
                jsonService.searchFilter.pageIndex = 1;
                jsonService.searchFilter.sortedBy = sortedBy;
                jsonService.searchFilter.sortedDirection = sortedDirection;
                scope.event.search();
            });
        }
    );

})();

