(function () {
    'use strict';

    angular
        .module('tilsoftApp')
        .controller('tilsoftController', ['$scope', 'dataService', ClientComplaintInitController]);

    function ClientComplaintInitController($scope, dataService) {
        //#region public members

        $scope.data = {
            season: jsHelper.getCurrentSeason(),
            clientID: null,
            client: null
        };
        $scope.seasons = null;
        $scope.complaintTypes = null;
        $scope.complaintStatuses = null;
        $scope.dataContainer = null;

        $scope.event = {
            init: init,
            goNext: goNext,
            goBack: goBack
        }

        //#endregion public members

        //#region private methods
        function setInitStates() {
            dataService.serviceUrl = context.serviceUrl;
            dataService.supportServiceUrl = context.supportServiceUrl;
            dataService.token = context.token;
        }

        function goBack() {
            window.location = context.backURL;
        }

        function goNext() {
            if (!$scope.data.clientID || $scope.data.clientID === '' || typeof ($scope.data.clientID) === 'undefined') {
                alert('Client is invalid !');
                return;
            }
            if (!$scope.data.season || $scope.data.season === '' || typeof ($scope.data.season) === 'undefined') {
                alert('Season is invalid !');
                return;
            }

            window.location = context.nextURL + 'clientId=' + $scope.data.clientID + '&season=' + $scope.data.season;
        }

        function init() {
            setInitStates();

            dataService.getInit(
                function (data) {
                    $scope.seasons = data.data.seasons;
                    jQuery('#content').show();
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.$apply();
                    jsHelper.showMessage('warning', error);
                }
            );

            initJQueryExtensions();
        }

        function initJQueryExtensions() {
            jQuery(document).ready(function () {
                //TODO: register 3rd Jquery controls here   
            });
        }

        //#region quickSearchClient
        var quickSearchClientGrid = jQuery("#quickSearchClientTable").scrollableTable(
            function (currentPage) {
                var scope = $scope.method.getBodyScope();
                scope.$apply(function () {
                    scope.quickSearchClient.filters.pageIndex = currentPage;
                    scope.quickSearchClient.method.searchClient();
                });
            },
            function (sortedBy, sortedDirection) {
                //do nothing
            }
        );

        $scope.searchClientTimer = null;
        $scope.quickSearchClient = {
            data: null,
            filters: {
                filters: {
                    searchQuery: ""
                },
                sortedBy: "ClientNM",
                sortedDirection: "ASC",
                pageSize: 10,
                pageIndex: 1
            },
            totalPage: 0,

            method: {
                searchClient: function () {
                    dataService.quickSearchClient(
                        $scope.quickSearchClient.filters,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.quickSearchClient.data = data.data;
                                $scope.quickSearchClient.totalPage = Math.ceil(data.totalRows / $scope.quickSearchClient.filters.pageSize);

                                quickSearchClientGrid.updateLayout();
                                $scope.$apply();
                                jQuery('#client-popup').show();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                }
            },

            event: {
                searchBoxKeyUp: function () {
                    if (jQuery("#txtClient").val().length >= 3) {
                        clearTimeout($scope.searchClientTimer);
                        $scope.searchClientTimer = setTimeout(
                            function () {
                                $scope.quickSearchClient.filters.filters.searchQuery = jQuery("#txtClient").val();
                                $scope.quickSearchClient.filters.pageIndex = 1;
                                $scope.quickSearchClient.method.searchClient();
                            },
                            500);
                    }
                },
                close: function ($event, clearSearchBox) {
                    jQuery("#client-popup").hide();
                    if (clearSearchBox) {
                        $scope.data.clientID = null;
                        $scope.data.clientUD = null;
                        $scope.data.clientNM = null;
                    }
                    $scope.quickSearchClient.data = null;
                    $event.preventDefault();
                },
                ok: function ($event, id) {
                    jQuery.each($scope.quickSearchClient.data, function () {
                        if (this.clientID === id) {
                            $scope.data.clientID = this.clientID;
                            $scope.data.clientUD = this.clientUD;
                            $scope.data.clientNM = this.clientNM;

                            //$scope.filters.searchQuery = this.clientUD;
                        }
                    });
                    $scope.quickSearchClient.event.close($event, false);
                }
            }
        }
        //#endregion quickSearchClient

        //#endregion private methods

        //#region init

        $scope.event.init();

        //#endregion init
    }
})();
