jQuery('.search-filter').keypress(function(event)
{
    if (event.keyCode === 13) 
    {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.reload();
    }
});

angular
    .module('tilsoftApp', ['ngCookies', 'avs-directives'])
    .controller('tilsoftController', priceConfigurationController);

priceConfigurationController.$inject = ['$scope', '$cookieStore', 'dataService'];

function priceConfigurationController($scope, $cookieStore, priceConfigurationService) {

    $scope.data = [];
    $scope.productElements = [];
    $scope.seasons = [];

    $scope.filters = {
        ProductElement: '',
        Season: ''
    };

    $scope.totalPage = 0;
    $scope.totalRows = 0;
    $scope.totalIndex = 1;

    $scope.gridHandler = {
        loadMore: function () {
            if ($scope.pageIndex < $scope.totalPage) {
                $scope.pageIndex++;

                priceConfigurationService.searchFilter.pageIndex = $scope.pageIndex;

                $scope.method.search(true);
            }
        },
        sort: function () {
            $scope.data = [];

            priceConfigurationService.searchFilter.sortedDirection = sortedDirection;
            priceConfigurationService.searchFilter.pageIndex = $scope.pageIndex = 1;
            priceConfigurationService.searchFilter.sortedBy = sortedBy;

            $scope.method.search();
        },
        isTriggered: false
    };

    $scope.event = {
        actived: actived,
        reloaded: reloaded,
        deleted: deleted,
        edited: edited
    };

    $scope.method = {
        init: init,
        search: search
    };

    $scope.event.actived();

    function actived() {
        $scope.method.init();
        $scope.method.search();
    }

    function reloaded() {
        $scope.data = [];

        priceConfigurationService.searchFilter.pageIndex = $scope.pageIndex = 1;

        $scope.method.search();
    }

    function deleted($event, item, $index) {
        priceConfigurationService.deleted(
            1,
            item.season,
            function (data) {
                jsHelper.processMessage(data.message);

                if (data.message.type === 0) {
                    if ($index >= 0) {
                        $scope.data.splice($index, 1);
                        $scope.$apply();
                        $scope.totalRows--;
                    }
                }
            },
            function (error) {
                jsHelper.showMessage('warning', error);
            });
    }

    function init() {
        priceConfigurationService.searchFilter.pageSize = context.pageSize;
        priceConfigurationService.serviceUrl = context.serviceUrl;
        priceConfigurationService.token = context.token;
    }

    function search(isLoadMore) {
        $cookieStore.put(context.cookieId, $scope.filters);

        priceConfigurationService.searchFilter.filters = $scope.filters;

        priceConfigurationService.search(
            function (data) {
                Array.prototype.push.apply($scope.data, data.data.data); console.log($scope.data);
                Array.prototype.push.apply($scope.seasons, data.data.supportSeason);

                $scope.totalPage = Math.ceil(data.totalRows / priceConfigurationService.searchFilter.pageSize);
                $scope.totalRows = data.totalRows;

                jQuery('#content').show();

                if (isLoadMore === false) {
                    $scope.gridHandler.goTop();
                }

                $scope.gridHandler.isTriggered = true;
            },
            function (error) {
            }
        );
    }

    function edited($event, id, season) {
        $event.preventDefault();

        window.location = context.editURL + id + '?s=' + season;
    }
}