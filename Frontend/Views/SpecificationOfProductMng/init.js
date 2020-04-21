//jQuery("#content").show();

(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap',]).controller('tilsoftController', specificationOfProductMngController);
    specificationOfProductMngController.$inject = ['$scope', 'dataService'];

    function specificationOfProductMngController($scope, dataService) {
        $scope.totalRows = 0;
        $scope.data = [];
        $scope.data.productID = null;

        $scope.factories = null;

        $scope.event = {
            init: init,
            goNext: goNext,
            goBack: goBack,
        }

        function goBack() {
            window.location = context.backURL;
        }

        function goNext() {
            if (!$scope.data.sampleProductID || $scope.data.sampleProductID === '' || typeof ($scope.data.sampleProductID) === 'undefined') {
                alert('Sample is invalid !');
                return;
            }

            window.location = context.nextURL + '&sampleProductID=' + $scope.data.sampleProductID + '&productID=' + $scope.data.productID;
        }

        function init() {
            //setInitStates();
            dataService.serviceUrl = context.serviceUrl;
            dataService.token = context.token;
            dataService.supportServiceUrl = context.supportServiceUrl;

            dataService.getInit(
                function (data) {
                    $scope.factories = data.data.factories;
                    jQuery('#content').show();
                },
                function (error) {

                    //$scope.$apply();
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

        //#region quickSearchSample
        var quickSearchSampleGrid = jQuery("#quickSearchSampleTable").scrollableTable(
            function (currentPage) {
                var scope = $scope.method.getBodyScope();
                scope.$apply(function () {
                    scope.quickSearchSample.filters.pageIndex = currentPage;
                    scope.quickSearchSample.method.searchClient();
                });
            },
            function (sortedBy, sortedDirection) {
                //do nothing
            }
        );

        //SearchSample
        $scope.searchSampleTimer = null;
        $scope.quickSearchSample = {
            data: [],
            filters: {
                filters: {
                    searchQuery: "",
                },
                sortedBy: "OptionUD",
                sortedDirection: "ASC",
                pageSize: 10,
                pageIndex: 1
            },
            totalPage: 0,

            method: {
                searchSample: function () {
                    dataService.quickSearchSample(
                        $scope.quickSearchSample.filters.filters.searchQuery,
                        $scope.factoryID,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.quickSearchSample.data = data.data.data;
                                $scope.quickSearchSample.totalPage = Math.ceil(data.data.data.totalRows / $scope.quickSearchSample.filters.pageSize);
                                
                                quickSearchSampleGrid.updateLayout();
                                $scope.$apply();
                                jQuery('#sample-popup').show();
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
                    if (jQuery("#txtSample").val().length >= 3) {
                        clearTimeout($scope.searchSampleTimer);
                        $scope.searchSampleTimer = setTimeout(
                            function () {
                                $scope.quickSearchSample.filters.filters.searchQuery = jQuery("#txtSample").val();
                                $scope.quickSearchSample.filters.pageIndex = 1;
                                $scope.quickSearchSample.method.searchSample();
                            },
                            500);
                    }
                },
                close: function ($event, clearSearchBox) {
                    jQuery("#sample-popup").hide();
                    if (clearSearchBox) {
                        $scope.data.sampleProductID = null;
                        $scope.data.sampleProductUD = null;
                        $scope.data.articleDescription = null;
                    }
                    $scope.quickSearchSample.data = null;
                    $event.preventDefault();
                },
                ok: function ($event, id) {
                    jQuery.each($scope.quickSearchSample.data, function () {
                        if (this.sampleProductID === id) {
                            $scope.data.sampleProductID = this.optionID;
                            $scope.data.sampleProductUD = this.optionUD;
                            $scope.data.articleDescription = this.optionNM;
                            $scope.data.clientID = this.clientID;
                            $scope.data.clientUD = this.clientUD;
                            $scope.data.sampleOrderUD = this.sampleOrderUD;

                        }
                    });
                    $scope.quickSearchSample.event.close($event, false);
                }
            }
        }

        //Init
        $scope.event.init();


    };
})();
