(function() {
    'use strict';

    //
    // APP START
    //
    angular
        .module('tilsoftApp', ['widgets'])
        .controller('tilsoftController', ['$scope', PLCController]);
    

    function PLCController($scope) {
        //
        // property
        //
        $scope.data = null;
        $scope.filters = {
            ModelUD: '',
            ModelNM: '',
            UpdatorName: '',
            Season: '',
            IsStandard: '',
            ProductTypeID: '',
            ProductGroupID: '',
            HasCushion: '',
            HasFrameMaterial: '',
            HasSubMaterial: '',
            IsConfirmed: ''
        };
        $scope.seasons = null;
        $scope.pageIndex = 1;
        $scope.totalPage = 0;
        $scope.totalRows = 0;

        //
        // event
        //
        $scope.event = {
            reload: reload,
            search: search,
            delete: deleteItem,
            init: init,
            print: print
        };

        //
        // init
        //
        $scope.event.init();


        //
        // method
        //
        function reload() {
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = $scope.pageIndex;
            $scope.event.search();
        }
        
        function search() {
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    $scope.data = data.data.data;
                    injectShowLinks($scope.data);
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.totalRows = data.totalRows;
                    $scope.$apply();

                    if (data.totalRows < jsonService.searchFilter.pageSize) {
                        jQuery('#searchResult').find('ul').hide();
                        //jQuery('#searchResult').find('span.more-info').hide();
                    }
                    else {
                        jQuery('#searchResult').find('ul').show();
                    }
                    jQuery('#searchResult').find('span.more-info').css('display', 'inline-block');

                    jQuery('#content').show();
                    searchResultGrid.updateLayout();
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.totalRows = 0;
                    $scope.$apply();
                }
            );

            function injectShowLinks(data) {
                if (data === null || typeof (data) === "undefined") return;

                data.forEach(function(item) {
                    item.showRatorLink = item.ratorID !== null && item.ratorName !== null;
                    item.showUpdatorLink = item.updatorID !== null && item.updatorName !== null;
                    item.showConfirmerLink = item.confirmerID !== null && item.confirmerName !== null;
                });
            }
        }

        function deleteItem(id) {
            if (confirm('Are you sure ?')) {
                jsonService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].plcid == data.data) {
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
        }

        function init() {
            jsonService.getsearchfilter(
                function (data) {
                    $scope.seasons = data.data.seasons;
                    $scope.$apply();

                    $scope.event.search();
                },
                function (error) {
                    $scope.seasons = null;
                    $scope.$apply();
                }
            );
        }

        function print() {
            var selectedIDs = '';

            angular.forEach($scope.data, function (value, key) {
                if (value.isSelected) {
                    if (selectedIDs != '') {
                        selectedIDs += ',' + value.plcid;
                    }
                    else {
                        selectedIDs += '' + value.plcid;
                    }
                }
            }, null);

            if (selectedIDs != '') {
                jsonService.getReport(
                    selectedIDs,
                    function (data) {
                        window.location = context.backendReportUrl + data.data + ".xlsm";

                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                alert('Please selected PLCs to print');
            }
        }
    }

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
            scope.$apply(function () {
                scope.pageIndex = currentPage;
                jsonService.searchFilter.pageIndex = scope.pageIndex;
                scope.event.search();
            });
        },
        function (sortedBy, sortedDirection) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                jsonService.searchFilter.sortedDirection = sortedDirection;
                scope.pageIndex = 1;
                jsonService.searchFilter.pageIndex = scope.pageIndex;
                jsonService.searchFilter.sortedBy = sortedBy;
                scope.event.search();
            });
        }
    );

})();

