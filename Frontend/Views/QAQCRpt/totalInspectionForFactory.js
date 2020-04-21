(function () {
    'use strict';

    var tilsoftApp = angular.module('tilsoftApp', ['avs-directives']);
    tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {
        dataService.searchFilter.pageSize = context.pageSize;
        dataService.serviceUrl = context.serviceUrl;
        dataService.token = context.token;
        //
        // grid handler
        //
        $scope.gridHandler = {

        };
        //
        // property
        //
        $scope.reportData = [];
        $scope.support = {
            clients: [],
            facories: [],
            productGroups: []
        };
        $scope.filters = {
            clientID: null,
            factoryID: null,
            productGroupID: null,
            fromDate: '',
            toDate: ''
        };
        //
        // events
        //
        $scope.event = {
            init: function () {
                dataService.getInitData(
                    function (data) {
                        $scope.support.clients = data.data.clients;
                        $scope.support.facories = data.data.factories;
                        $scope.support.productGroups = data.data.productGroups;
                    },
                    function (err) {

                    }
                );
                jQuery('#content').show();
            },

            getData: function () {
                //
                // store filter in cookies
                //
                if ($scope.filters.factoryID === '') { $scope.filters.factoryID = null; }
                if ($scope.filters.clientID === '') { $scope.filters.clientID = null; }
                if ($scope.filters.fromDate === null || $scope.filters.fromDate === '') { jsHelper.showMessage('warning', 'select from date first'); return; }
                if ($scope.filters.toDate === null || $scope.filters.toDate === '') { jsHelper.showMessage('warning', 'select to date first'); return; }
                $scope.reportData = [];
                dataService.getTotalInspectionForFactory(
                    $scope.filters,
                    function (data) {
                        $scope.reportData = data.data;
                    },
                    function (error) {
                        $scope.reportData = null;
                    }
                );
            },

            clearFilter: function () {
                $scope.filters = {
                    clientID: null,
                    factoryID: null,
                    productGroupID: null,
                    fromDate: '',
                    toDate: ''
                };
            }

        };
        //
        // method
        //
        $scope.method = {

        };
        //
        // angular init
        //
        $timeout(function () {
            $scope.event.init();
        }, 0);
    }]);

})();