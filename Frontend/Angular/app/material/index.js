jQuery('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

var tilsoftApp = angular.module('tilsoftApp', ['ngCookies', 'avs-directives'])
    .controller('tilsoftController', ['$scope', '$cookieStore', 'dataService', function ($scope, $cookieStore, dataService) {

        dataService.searchFilter.pageSize = context.pageSize;
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;

        $scope.filters = {
            materialUd: '',
            materialNm: ''
        };

        $scope.defaultFilter = angular.copy($scope.filters);
        $scope.isDefaultFilter = true;

        var cookieValue = $cookieStore.get(context.cookieId);
        if (cookieValue)
            $scope.filters = cookieValue;

        $scope.data = [];

        $scope.materialUd = null;
        $scope.materialNm = null;

        $scope.pageIndex = 1;
        $scope.totalPage = 0;
        $scope.totalRows = 0;

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

        $scope.event = {
            search: function (isLoadMore) {
                $cookieStore.put(context.cookieId, $scope.filters);
                $scope.isDefaultFilter = angular.equals($scope.filters, $scope.defaultFilter);
                //console.log($scope.filters);
                //console.log($scope.defaultFilter);

                $scope.gridHandler.isTriggered = false;
                dataService.searchFilter.filters = $scope.filters;
                debugger;
                dataService.search(
                    function (data) {
                        Array.prototype.push.apply($scope.data, data.data.data);

                        $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;

                        jQuery('#content').show();

                        if (!isLoadMore)
                            $scope.gridHandler.goTop();

                        $scope.gridHandler.isTriggered = true;
                    },
                    function (error) {
                        $scope.data = null;
                        $scope.pageIndex = 1;
                        $scope.totalPage = 0;
                        $scope.totalRows = 0;
                    });
            },
            init: function () {
                dataService.search(
                    function (data) {
                        $scope.materialUd = data.data.MaterialUD;
                        $scope.materialNm = data.data.MaterialNM;
                        $scope.event.search();
                    },
                    function (error) {
                        $scope.materialUd = null;
                        $scope.materialNm = null;
                    });
            },
            reload: function () {
                $scope.data = [];
                $scope.pageIndex = 1;
                dataService.searchFilter.pageIndex = $scope.pageIndex;
                $scope.event.search();
            },
            delete: function (id) {
                dataService.delete(
                    id,
                    null,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].materialID == data.data) {
                                    j = i;
                                    break;
                                }
                            }
                            if (j >= 0) {
                                $scope.data.splice(j, 1);
                                $scope.totalRows--;
                            }
                        }
                    },
                    function (error) {
                        jsHelper.showMessage("warning", error);
                    });
            },
            clearFilter: function () {
                $scope.filters = {
                    materialUd: '',
                    materialNm: ''
                };
                $scope.event.reload();
            }
        }

        $scope.event.init();

    }]);