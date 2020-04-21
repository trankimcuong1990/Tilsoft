(function() {
    'use strict';

    angular
        .module('tilsoftApp', ['widgets'])
        .controller('tilsoftController', ['$scope', PackingListController]);


    function PackingListController($scope) {
        
        //#region properties
        $scope.packingList = null;
        $scope.pageIndex = 1;
        $scope.totalPage = 0;
        $scope.filters = {
            PackingListUD: '',
            InvoiceNo: '',
            BLNo: '',
            ClientUD: '',
            ClientNM: '',
            ContainerNo: ''
        };
        //#endregion properties

        //#region events & methods
        $scope.search = search;
        $scope.delete = deleteItem;
        //#endregion events & methods

        //#region init
        $scope.search();
        //#endregion init

        //#region private methods
        function search() {
            packingListService.searchFilter.filters = $scope.filters;
            packingListService.search(
                function (data) {
                    $scope.packingList = data.data;
                    injectShowLinks($scope.packingList);
                    $scope.totalPage = Math.ceil(data.totalRows / packingListService.searchFilter.pageSize);
                    $scope.$apply();

                    if (data.totalRows < packingListService.searchFilter.pageSize) {
                        jQuery('#mainresultTable').find('ul').hide();
                    }
                    else {
                        jQuery('#mainresultTable').find('ul').show();
                    }

                    jQuery('#content').show();

                },
                function (error) {
                    $scope.packingList = null;
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

        function deleteItem(id) {
            if (confirm('Are you sure ?')) {
                packingListService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.packingList.length; i++) {
                                if ($scope.packingList[i].packingListID == data.data) {
                                    j = i;
                                    break;
                                }
                            }

                            if (j >= 0) {
                                $scope.packingList.splice(j, 1);
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
        //#endregion private methods
    }

    
    //#region support
    jQuery('.search-filter').keypress(function (e) {
        if (e.keyCode == 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.search();
        }
    });

    var searchGrid = jQuery('#searchTableContent').scrollableTable(
        function (currentPage) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                scope.pageIndex = currentPage;
                packingListService.searchFilter.pageIndex = scope.pageIndex;
                scope.search();
            });
        },
        function (sortedBy, sortedDirection) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                packingListService.searchFilter.sortedDirection = sortedDirection;
                scope.pageIndex = 1;
                packingListService.searchFilter.pageIndex = scope.pageIndex;
                packingListService.searchFilter.sortedBy = sortedBy;
                scope.search();
            });
        }
    );
    //#endregion support

})();