﻿tilsoftApp.controller('_SearchSparepartForm', ['$scope', '$timeout', '$cookieStore', 'dataService', function ($scope, $timeout, $cookieStore, dataService) {
    //
    //init controller
    //
    $scope.initController = function () {
        //
        //tracking position
        //
        $scope.pagePosition.pageYOffset = window.pageYOffset;
        window.scrollTo(0, 0);

        //
        // init service
        //
        dataService.serviceUrl = context.serviceUrl;
        dataService.supportServiceUrl = context.supportServiceUrl;
        dataService.token = context.token;

        //
        //        
        jQuery('#content').show();

        //
        //        
        $scope.event.init();
    };

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
            $scope.searchResult = [];
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
    $scope.searchResult = [];
    $scope.filters = {
        modelUD: '',
        modelNM: '',
        season: '',
        articleCode: '',
        description: ''
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
            $scope.searchResult = [];
            $scope.pageIndex = 1;
            dataService.searchFilter.pageIndex = $scope.pageIndex;
            dataService.searchFilter.sortedBy = 'Season';
            dataService.searchFilter.sortedDirection = 'DESC';
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
            dataService.searchSparepart(
                function (data) {
                    Array.prototype.push.apply($scope.searchResult, data.data.data);
                    $scope.totalPage = Math.ceil(data.data.totalRows / dataService.searchFilter.pageSize);
                    $scope.totalRows = data.data.totalRows;
                    jQuery('#content').show();

                    if (!isLoadMore) {
                        $scope.gridHandler.goTop();
                    }
                    $scope.gridHandler.isTriggered = true;
                },
                function (error) {
                    $scope.searchResult = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                }
            );
        },
        clearFilter: function () {
            $scope.filters = {
                modelUD: '',
                modelNM: '',
                season: '',
                articleCode: '',
                description: ''
            };
            $scope.event.reload();
        },
        ok: function () {
            var selectedItems = $scope.searchResult.filter(o => o.isSelected === true);
            angular.forEach(selectedItems, function (item) {
                $scope.data.offerSeasonDetailDTOs.push({
                    offerSeasonDetailID: dataService.getIncrementId(),
                    sparepartID: item.sparepartID,
                    modelID: item.modelID,
                    partID: item.partID,

                    modelUD: item.modelUD,
                    modelNM: item.modelNM,
                    articleCode: item.articleCode,
                    description: item.description,
                    isEditing: true,
                    isClientSelected: true
                });
            });

        }
    };


    //
    //init
    //
    $scope.initController();
}]);
