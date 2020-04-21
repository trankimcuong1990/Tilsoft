jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives', 'furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$cookieStore', function ($scope, $cookieStore) {

    $scope.data = null;
    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;
    
    $scope.filters = {
        factoryID: null,
        season: '',
        weekNo: null,
        statusDate : '',
        updatedDate: '',
        updatorName: '',
    };

    $scope.factories = [];
    $scope.seasons = [];
    $scope.weekSeasons = [];

    $scope.initForm = {
        factoryID: '',
        season: context.currentSeason,
    }

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
            jsonService.searchFilter.sortedBy = 'factoryUD';
            jsonService.searchFilter.sortedDirection = 'DESC';

            //search data
            $scope.event.search();           
        },
        search: function (isLoadMore) {
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.factories = data.data.factories;
                    $scope.seasons = data.data.seasons;
                    $scope.weekSeasons = data.data.weekSeasons;
                    //get total row
                    $scope.totalRows = data.totalRows;
                    //cal total page
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.$apply();
                    $("#factoryID").select2();
                    $("#factoryID_Init").select2();
                    $("#season").select2();
                    $("#season_Init").select2();
                    $("#weekNo").select2();
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
                factoryID: null,
                season: '',
                weekNo: null,
                statusDate : '',
                updatedDate: '',
                updatorName: '',
            };
            $scope.event.reload();
        },
        delete: function (id, $event) {
            $event.preventDefault();
            if (confirm('Are you sure you want to delete ?')) {
                jsonService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].factoryProductionStatusID == data.data) {
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

        createNew: function ($event) {
            $event.preventDefault();
            if ($scope.initForm.factoryID == '') {
                bootbox.alert("Factory is empty. You should select factory");
                return;
            }
            if ($scope.initForm.season == '') {
                bootbox.alert("Season is empty. You should select season");
                return;
            }
            window.open(context.editUrl + '?factoryID=' + $scope.initForm.factoryID + '&season=' + $scope.initForm.season, '');
            $('#frmInit').modal('hide');
        },

        openInitForm: function ($event) {
            $event.preventDefault();
            $('#frmInit').modal('show');
        }

    }

    $scope.getWeekDayRange = function (weekNo) {
        for (var i = 0; i < $scope.weekSeasons.length; i++) {
            var item = $scope.weekSeasons[i];
            if (item.weekNo == weekNo) {
                return item.weekNo + ' (' + item.startDate + ' - ' + item.endDate + ')'
            }
        }
    }

    $scope.event.reload();
}]);