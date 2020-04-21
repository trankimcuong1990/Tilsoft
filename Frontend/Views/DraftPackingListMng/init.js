//
// SUPPORT
//
$('.search-filter').keypress(function (e) {
    if (e.keyCode == 13) {
        var scope = angular.element(jQuery('body')).scope();
        scope.event.search();
    }
});
(function () {
    'use strict';

    angular.module('tilsoftApp', ['ngCookies', 'avs-directives']).controller('tilsoftController', DraftPackingListController);
    DraftPackingListController.$inject = ['$scope', '$cookieStore', 'dataService', '$timeout'];

    function DraftPackingListController($scope, $cookieStore, dataService, $timeout) {
        $scope.data = [];
        $scope.pageIndex = 1;
        $scope.totalPage = 0;
        $scope.filters = {
            invoiceNo: ''
        };

        $scope.event = {
            init: init,
            search: search
        };

        function init() {
            dataService.searchFilter.pageSize = context.pageSize;
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
            $scope.event.search();
            jQuery('#content').show();
        };

        function search() {
            dataService.searchFilter.filters = $scope.filters;
            dataService.search_initInfo(
                function (data) {
                    $scope.data = data.data;
                    $scope.$apply();
                },
                function (error) {
                    $scope.data = null;
                }
            );
        }

        $scope.event.init();
    };
})();