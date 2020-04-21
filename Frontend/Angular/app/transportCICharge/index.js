jQuery('.search-filter').keypress(function (event) {
    if (event.keyCode === 13) {
        var scope = angular.element(jQuery("body")).scope();
        scope.event.reload();
    }
})

angular
    .module('tilsoftApp', ['ngCookies', 'avs-directives'])
    .controller('tilsoftController', ['$scope', '$cookieStore', 'dataService',
        function ($scope, $cookieStore, dataService) {
            // declare variable
            $scope.data = [];
            $scope.filters = {
                ClientNM: '',
                BookingUD: '',
                EurofarInvoiceNr: ''
            }
            $scope.totalPage = 0;
            $scope.totalRows = 0;
            $scope.totalIndex = 1;

            // define, proccess gridHandler
            $scope.gridHandler = {
                loadMore: function () {
                    if ($scope.pageIndex < $scope.totalPage) {
                        $scope.pageIndex++;
                        dataService.searchFilter.pageIndex = $scope.pageIndex;
                        $scope.method.search(true);
                    }
                },
                sort: function (sortedBy, sortedDirection) {
                    $scope.data = [];
                    dataService.searchFilter.sortedDirection = sortedDirection;
                    dataService.searchFilter.pageIndex = $scope.pageIndex = 1;
                    dataService.searchFilter.sortedBy = sortedBy;
                    $scope.method.search();
                },
                isTriggered: false
            }

            // declare event, method
            $scope.event = {
                active: active,
                reload: reload,
                delete: deleteItem
            }
            $scope.method = {
                init: init,
                search: search
            }

            $scope.event.active();

            // define event
            function active() {
                $scope.method.init();
                $scope.method.search();
            }
            function reload() {
                $scope.data = [];
                dataService.searchFilter.pageIndex = $scope.pageIndex = 1;
                $scope.method.search();
            }
            function deleteItem(id) {
                dataService.delete(
                    id,
                    null,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].transportCIChargeID === data.data) {
                                    j = i;
                                    break;
                                }
                            }
                            if (j >= 0) {
                                $scope.data.splice(j, 1);
                                $scope.$apply();
                                $scope.totalRows--;
                            }
                        }
                    },
                    function (error) {
                        jsHelper.showMessage("warning", error);
                    });
            }

            // define method
            function init() {
                dataService.searchFilter.pageSize = context.pageSize;
                dataService.serviceUrl = context.serviceUrl;
                dataService.token = context.token;
            }
            function search(isLoadMore) {
                $cookieStore.put(context.cookieId, $scope.filters);
                $scope.gridHandler.isTriggered = false;
                dataService.searchFilter.filters = $scope.filters;
                dataService.search(
                    function (data) {
                        Array.prototype.push.apply($scope.data, data.data.data);console.log($scope.data)

                        $scope.totalPage = Math.ceil(data.totalRows / dataService.searchFilter.pageSize);
                        $scope.totalRows = data.totalRows;

                        jQuery('#content').show();

                        if (isLoadMore === false) {
                            $scope.gridHandler.goTop();
                        }

                        $scope.gridHandler.isTriggered = true;
                    },
                    function (error) {
                    });
            }
        }]);