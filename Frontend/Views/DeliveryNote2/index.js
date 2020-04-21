jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives', 'furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', 'searchDetailDeliveryNote', function ($scope, $cookieStore, searchDetailDeliveryNote) {

    $scope.data = null;
    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;
    
    $scope.filters = {
        deliveryNoteUD: '',
        deliveryNoteDate: '',
        workCenterNM: '',
        fromProductionTeamNM: '',
        toProductionTeamNM: '',
        workOrderUD: '',
        modelUD: '',
        modelNM: '',
        productArticleCode: '',
        productDescription: '',
        description: '',
        updatedDate: '',
        updatorName: '',
        factoryWarehouseID: null,
        fromDeliveryNoteDate: '',
        toDeliveryNoteDate: '',
        statusTypeID: null,
        fromUpdatedDate: '',
        toUpdatedDate: '',
        workCenterID: null,
        fromProductionTeamID: null,


    };

    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;
    //
    // grid handler
    //
    $scope.gridHandler = {
        loadMore: function () {
            if ($scope.pageIndex < $scope.totalPage) {
                $scope.pageIndex++;
                jsonService.searchFilter.pageIndex = $scope.pageIndex;
                $scope.event.search(true);
            }
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.data = [];
            jsonService.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = scope.pageIndex;
            jsonService.searchFilter.sortedBy = sortedBy;
            $scope.event.search(true);
        },
        isTriggered: false
    };

    //
    // event
    //
    $scope.event = {
        reload: function () {
            //reset main data
            $scope.data = [];

            //assign pager for search result
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = 1;
            jsonService.searchFilter.sortedBy = 'deliveryNoteUD';
            jsonService.searchFilter.sortedDirection = 'DESC';

            //search data
            $scope.event.search();
        },
        search: function (isLoadMore) {
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
            //if ($scope.filters.deliveryNoteDate != null && $scope.filters.deliveryNoteDate != "") {
            //    if ($scope.filters.deliveryNoteDate.indexOf('/') < 2) {
            //        $scope.filters.deliveryNoteDate = "0" + $scope.filters.deliveryNoteDate;
            //    }
            //    $scope.filters.deliveryNoteDate = $scope.filters.deliveryNoteDate.split("/").reverse().join("-");
            //}
            if ($scope.filters.updatedDate != null && $scope.filters.updatedDate != "") {
                if ($scope.filters.updatedDate.indexOf('/') < 2) {
                    $scope.filters.updatedDate = "0" + $scope.filters.updatedDate;
                }
                $scope.filters.updatedDate = $scope.filters.updatedDate.split("/").reverse().join("-");
            }
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    //$scope.filters.deliveryNoteDate = $scope.filters.deliveryNoteDate.split("-").reverse().join("/");
                    $scope.filters.updatedDate = $scope.filters.updatedDate.split("-").reverse().join("/");
                    $scope.factoryWareHouseList = data.data.supportFactoryWareHouseLists;
                    $scope.statusTypeDTOs = data.data.statusTypes;
                    Array.prototype.push.apply($scope.data, data.data.data);
                    //get total row
                    $scope.totalRows = data.totalRows;
                    //cal total page
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.$apply();
                    jQuery('#content').show();

                    //gridHandler
                    $scope.gridHandler.refresh();
                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.$apply();
                }
            );
        },
        clearFilter: function () {
            $scope.filters = {
                deliveryNoteUD: '',
                deliveryNoteDate: '',
                workCenterNM: '',
                fromProductionTeamNM: '',
                toProductionTeamNM: '',
                workOrderUD: '',
                modelUD: '',
                modelNM: '',
                productArticleCode: '',
                productDescription: '',
                description: '',
                updatedDate: '',
                updatorName: '',
                factoryWarehouseID: null,
                fromDeliveryNoteDate: '',
                toDeliveryNoteDate: '',
                statusTypeID: null,
                fromUpdatedDate: '',
                toUpdatedDate: '',
                workCenterID: null,
                fromProductionTeamID: null
            };
            $scope.event.reload();
        },
        delete: function (id, $event) {
            $event.preventDefault();
            if (confirm('Are you sure you want to delete delivery note ?')) {
                jsonService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].deliveryNoteID == data.data) {
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
        },

        deleteWithPermission: function (id, $event, createdBy) {
            $event.preventDefault();
            //jsHelper.showMessage('warning', 'cannot delete.');
            if (confirm('Are you sure you want to delete delivery note ?')) {
                jsonService.deleteWithPermission(id,
                    createdBy,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].deliveryNoteID == data.data) {
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
        },
        exportExcel: function () {
            jsonService.getListDeliveryNote(
                function (data) {
                    if (data.message.type === 0) {
                        window.location = context.reportUrl + data.data;
                    }
                    else {
                        jsHelper.processMessageEx(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        reloadDetail: function () {
            //reset main data
            $scope.data = [];

            //assign pager for search result
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = 1;
            jsonService.searchFilter.sortedBy = 'deliveryNoteUD';
            jsonService.searchFilter.sortedDirection = 'DESC';
            jQuery("#getSearchDetail").modal('hide');

            //search data
            $scope.event.search();
        },
        closeDetail: function () {
            $scope.searchDetail.checkSearchDetail = false;
        }
    };


    $scope.getModel = function (item) {
        var index = [];
        var result = [];
        angular.forEach(item.workOrderSearchDTOs, function (cItem) {
            if (cItem.modelUD != null && cItem.modelUD != '' && index.indexOf(cItem.modelUD) < 0) {
                index.push(cItem.modelUD);
                result.push(cItem);
            }
        });
        return result;
    };

    //
    // search detail
    //      
    $scope.searchDetail = {
        productionTeamData: null,
        workCenterData: null,
        checkSearchDetail: false
        //getFactoryBreakdown: function (sampleProductID) {

        //}
    };
    $scope.popupSearchDetailDeliveryNote = searchDetailDeliveryNote;
    $scope.$on('searchDetailDeliveryNote.productionTeam', function (event, args) {
        $scope.searchDetail.productionTeamData = args;
    });
    $scope.$on('searchDetailDeliveryNote.workCenter', function (event, args) {
        $scope.searchDetail.workCenterData = args;
    });
    $scope.$on('searchDetailDeliveryNote.checkDetail', function (event, args) {
        $scope.searchDetail.checkSearchDetail = args;
    });


    $scope.event.reload();
}]);