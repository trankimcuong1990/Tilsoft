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
        invoiceNo: '',
        invoiceDate: '',
        blNo: '',
        clientUD: '',
        clientNM:'',
        forwarderNM: '',
        currency: '',
        refNo: '',
        invoiceStatusText: '',
        statustorName: '',
        statusUpdatedDate: '',
    };
    $scope.defaultFilter = angular.copy($scope.filters);
    $scope.isDefaultFilter = true;

    $scope.seasons = [];
    $scope.initForm = {
        season: context.currentSeason,
        bookingID: null,
        blNo:''
    }
    $scope.currentForm = 'searchResult';
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
            jsonService.searchFilter.sortedBy = 'forwarderNM';
            jsonService.searchFilter.sortedDirection = 'DESC';

            //search data
            $scope.event.search();
            $scope.method.assignAutoCompleteProductionBooking();
        },
        search: function (isLoadMore) {
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.forwarders = data.data.forwarders;
                    //get total row
                    $scope.totalRows = data.totalRows;
                    //cal total page
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.$apply();
                    $("#forwarderID").select2();
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
                receivingNoteUD: '',
                receivingNoteDate: '',
                purchasingOrderNo: '',
                productionTeamNM: '',
                factoryWarehouseNM: '',
                workOrderUD: '',
                workOrderDescription: '',
                description: '',
                updatorName: '',
                updatedDate: '',
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
                                if ($scope.data[i].transportOfferID == data.data) {
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
    }

    $scope.initForm = {
        load: function ($event) {
            $event.preventDefault();
            $scope.seasons = jsHelper.getSeasons();
            $scope.initForm.season = context.currentSeason;
            $scope.currentForm = 'initForm';
        },
        ok: function () {
            if ($scope.initForm.season == null) {
                bootbox.alert("You have to select season");
                return;
            }
            if ($scope.initForm.bookingID == null) {
                bootbox.alert("You have to select BL no");
                return;
            }
            window.open(context.editUrl + '?bookingID=' + $scope.initForm.bookingID, '');
            $scope.currentForm = 'searchResult';
        },
        cancel: function () {
            $scope.currentForm = 'searchResult';
        }
    }


    $scope.method = {
        assignAutoCompleteProductionBooking: function () {
            $("#booking").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        cache: false,
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json',
                            'Authorization': 'Bearer ' + context.token
                        },
                        type: "POST",
                        data: JSON.stringify({
                            filters: {
                                searchQuery: request.term,
                                season: $scope.initForm.season
                            }
                        }),
                        dataType: 'json',
                        url: context.getBookingUrl,
                        beforeSend: function () {
                            jsHelper.loadingSwitch(true);
                        },
                        success: function (data) {
                            jsHelper.loadingSwitch(false);
                            response(data.data);
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            jsHelper.loadingSwitch(false);
                            errorCallBack(xhr.responseJSON.exceptionMessage);
                        }
                    });
                },
                minLength: 2,
                select: function (event, ui) {
                    $scope.initForm.bookingID = ui.item.id;
                    $scope.$apply();
                }
            });
        },
    }

    $scope.event.reload();
}]);