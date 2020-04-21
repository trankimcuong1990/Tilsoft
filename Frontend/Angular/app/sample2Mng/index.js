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

        scope.pageIndex = currentPage;
        scope.event.search();
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.sort(sortedBy, sortedDirection);
    }
);



//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', '$http', '$cookieStore', 'jsonService', function ($scope, $filter, $http, $cookieStore, jsonService) {
    jsonService.searchFilter.pageSize = context.pageSize;
    jsonService.serviceUrl = context.serviceUrl;
    jsonService.token = context.token;
    jsonService.getexcelsampleorder = function (sampleOrderUD, season, clientUD, clientNM, purposeID, transportTypeID, sampleOrderStatusID, factoryID, sampleItemCode, sampleItemName, accountManager, _successCallback, errorCallBack) {
        debugger;
        jQuery.ajax({
            cache: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + this.token
            },
            type: "POST",
            url: this.serviceUrl + 'exportexcelsampleorder?sampleOrderUD=' + sampleOrderUD + '&season=' + season + 
                '&clientUD=' + clientUD + '&clientNM=' + clientNM + '&purposeID=' + purposeID + '&transportTypeID=' + transportTypeID +
                '&sampleOrderStatusID=' + sampleOrderStatusID + '&factoryID=' + factoryID + '&sampleItemCode=' + sampleItemCode + '&sampleItemName=' + sampleItemName + '&accountManager=' + accountManager,
            beforeSend: function () {
                jsHelper.loadingSwitch(true);
            },
            success: function (data) {
                jsHelper.loadingSwitch(false);
                _successCallback(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                jsHelper.loadingSwitch(false);
                errorCallBack(xhr.responseJSON.exceptionMessage);
            }
        });
    }
    //
    // property
    //
    $scope.data = [];
    $scope.filters = {
        SampleOrderUD: '',
        Season: '',
        ClientUD: '',
        ClientNM: '',
        PurposeID: '',
        TransportTypeID: '',
        SampleOrderStatusID: 1,
        SampleItemCode: '',
        SampleItemName: '',
        FactoryID: '',
        AccountManagerID: ''
    };
    $scope.seasons = null;
    $scope.samplePurposes = null;
    $scope.sampleTransportTypes = null;
    $scope.sampleOrderStatuses = null;
    $scope.factorys = null;
    $scope.accountManagers = null;
    $scope.sampleProductNewDevelopmentNumber = null;
    $scope.sampleProductNoNewDevelopmentNumber = null;
    $scope.sampleProductTableFinishNumber = null;
    $scope.sampleProductTableInProgressNumber = null;
    $scope.sampleProductdFromExistModelNumber = null;

    $scope.selectedModel = {
        id: null,
        data: null
    }

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
            $scope.event.search();
        },
        isTriggered: false
    };

    $scope.pageIndex = 1;
    $scope.totalPage = 0;
    $scope.totalRows = 0;


    //
    // event
    //
    $scope.event = {
        reload: function () {
            $scope.pageIndex = 1;
            $scope.data = [];
            jsonService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.search();
        },
        sort: function (sortedBy, sortedDirection) {
            $scope.data = [];
            jsonService.searchFilter.sortedDirection = sortedDirection;
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = scope.pageIndex;
            jsonService.searchFilter.sortedBy = sortedBy;
            $scope.event.search();
        },
        search: function (isLoadMore) {
            //
            // store filter in cookies
            //
            $cookieStore.put(context.cookieId, $scope.filters);
            $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);

            $scope.gridHandler.isTriggered = false;
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    Array.prototype.push.apply($scope.data, data.data.data);
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                    jQuery('#content').show();
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                }
            );
        },
        delete: function (id, code) {
            jsonService.delete(id, code,
                function (data) {
                    jsHelper.processMessage(data.message);

                    if (data.message.type == 0) {
                        var j = -1;
                        for (var i = 0; i < $scope.data.length; i++) {
                            if ($scope.data[i].sampleID == data.data) {
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
                    jsHelper.showMessage('warning', error.data.message.message);
                }
            );
        },

        getExcelSamplerOrder: function () {
            debugger;
            jsonService.getexcelsampleorder(

                $scope.filters.SampleOrderUD,
                $scope.filters.Season,
                $scope.filters.ClientUD,
                $scope.filters.ClientNM,
                $scope.filters.PurposeID,
                $scope.filters.TransportTypeID,
                $scope.filters.SampleOrderStatusID,
                $scope.filters.FactoryID,
                $scope.filters.SampleItemCode,
                $scope.filters.SampleItemName,
                $scope.filters.AccountManagerID,
                function (data) {
                    window.location = context.backendReportUrl + data.data;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            )
        },

        init: function () {
            jsonService.searchFilter.sortedBy = 'SampleOrderStatusID';
            jsonService.searchFilter.sortedDirection = 'ASC';
            jsonService.getSearchFilter(
                function (data) {
                    $scope.seasons = data.data.seasons;
                    $scope.samplePurposes = data.data.samplePurposes;
                    $scope.sampleTransportTypes = data.data.sampleTransportTypes;
                    $scope.sampleOrderStatuses = data.data.sampleOrderStatuses;
                    $scope.factorys = data.data.factorys;
                    $scope.accountManagers = data.data.accountManagerDTOs;

                    $scope.event.search();
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.samplePurposes = null;
                    $scope.sampleTransportTypes = null;
                    $scope.sampleOrderStatuses = null;
                    $scope.factorys = null;
                }
            );
        }

    }

    //
    // method
    //
    $scope.method = {
        total: function () {
            var result = 0;
            $scope.sampleProductNewDevelopmentNumber = 0;
            $scope.sampleProductNoNewDevelopmentNumber = 0;
            $scope.sampleProductTableFinishNumber = 0;
            $scope.sampleProductTableInProgressNumber = 0;
            $scope.sampleProductdFromExistModelNumber = 0;
            if ($scope.data.length !== 0) {
                $scope.sampleProductNewDevelopmentNumber = 0;
                $scope.sampleProductNoNewDevelopmentNumber = 0;
                $scope.sampleProductTableFinishNumber = 0;
                $scope.sampleProductTableInProgressNumber = 0;
                angular.forEach($scope.data, function (item) {
                    result += item.sampleProductTotalNumber;
                    $scope.sampleProductNewDevelopmentNumber += item.sampleProductNewDevelopmentNumber;
                    $scope.sampleProductNoNewDevelopmentNumber += item.sampleProductNoNewDevelopmentNumber;
                    $scope.sampleProductTableFinishNumber += item.sampleProductTableFinishNumber;
                    $scope.sampleProductTableInProgressNumber += item.sampleProductTableInProgressNumber;
                    $scope.sampleProductdFromExistModelNumber += item.sampleProductdFromExistModelNumber;
                });
            }
            return result;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);


