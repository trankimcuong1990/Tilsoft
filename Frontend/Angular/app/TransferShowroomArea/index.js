(function () {

    angular
        .module('tilsoftApp', ['widgets'])
        .controller('tilsoftController', ['$scope', TransferShowroomAreaController]);

    function TransferShowroomAreaController($scope) {
        //
        // property
        //
        $scope.filters = {
            ArticleCode: '',
            Description: ''
        };
        $scope.pageIndex = 1;
        $scope.totalPage = 0;

        $scope.data = null;

        $scope.searchBy = 1; // normal
        $scope.transferType = 2; // 1 : transfer normal, 2 : Reset and Re-arrange area

        $scope.transferData = {
            showroomItemID: null,
            fromShowroomAreaID: null,
            toShowroomAreaID: null,
            quantity: null,

            articleCode: '',
            description: '',
            fromAreaUD: null,
            toAreaUD: null,
        };

        $scope.listTransferData = [];

        //
        // event
        //
        $scope.event = {
            reload: reload,
            search: search,
            removeTranferItem: removeTranferItem
        };

        $scope.method = {
            transfer: transfer,
            transferMultiItem: transferMultiItem
        };

        //
        // init
        //
        $scope.event.reload();


        //
        //support quick search showroom item
        //
        var quickSearchShowroomItemTimer = null;
        $scope.quicksearchShowroomItem = {
            data: null,
            filters: {
                filters: {
                    searchQuery: ''
                },
                sortedBy: 'Description',
                sortedDirection: 'ASC',
                pageSize: 20,
                pageIndex: 1
            },
            totalPage: 0,

            popupformID: 'popup-showroomItem',
            gridSearchResultID: 'quickSearchResultGrid-showroomItem',
            searchQueryBoxID: 'quickSearchBox-showroomItem',

            method: {
                search: function (callBackSuccess) {
                    supportService.quicksearchShowroomItem(
                        $scope.quicksearchShowroomItem.filters,
                        function (data) {
                            if (data.message.type == 0) {
                                $scope.quicksearchShowroomItem.data = data.data;
                                $scope.quicksearchShowroomItem.totalPage = Math.ceil(data.totalRows /
                                    $scope.quicksearchShowroomItem.filters.pageSize);
                                quickSearchResultGridShowroomItem.updateLayout();
                                $scope.$apply();

                                if (callBackSuccess != null) callBackSuccess();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                }
            },
            event: {
                searchBoxKeyUp: function ($event) {
                    if ($scope.searchBy == 2) { // search by barcode
                        if ($event.keyCode == 13) {
                            $scope.quicksearchShowroomItem.event.searchClick(function () {
                                $scope.quicksearchShowroomItem.event.barcodeScanItem();
                                $scope.$apply();
                            });
                        }
                    } else // search normal
                    {
                        if ($event.keyCode == 13) {
                            $scope.quicksearchShowroomItem.event.searchClick();
                        } else if (jQuery('#' + $scope.quicksearchShowroomItem.searchQueryBoxID).val().length >= 3) {
                            clearTimeout(quickSearchShowroomItemTimer);
                            quickSearchShowroomItemTimer = setTimeout(
                                function () {
                                    $scope.quicksearchShowroomItem.event.searchClick();
                                },
                                500);
                        }
                    }
                },

                searchClick: function (callBackSuccess) {
                    $scope.quicksearchShowroomItem.filters.filters.searchQuery =
                        jQuery('#' + $scope.quicksearchShowroomItem.searchQueryBoxID).val();
                    $scope.quicksearchShowroomItem.filters.pageIndex = 1;
                    $scope.quicksearchShowroomItem.method.search(function () {
                        if (callBackSuccess != null) {
                            callBackSuccess();
                        } else {
                            jQuery('#' + $scope.quicksearchShowroomItem.popupformID).show();
                        }
                    });
                },

                close: function ($event) {
                    $event.preventDefault();
                    jQuery('#' + $scope.quicksearchShowroomItem.popupformID).hide();
                    jQuery('#' + $scope.quicksearchShowroomItem.searchQueryBoxID).val('');
                    $scope.quicksearchShowroomItem.data = null;
                },

                itemSelected: function ($event, item) {
                    $event.preventDefault();

                    if ($scope.transferType == 1) //transfer
                    {
                        $scope.transferData.showroomItemID = item.showroomItemID;
                        $scope.transferData.articleCode = item.articleCode;
                        $scope.transferData.description = item.description;

                        //reset shworoom area from
                        $scope.transferData.fromShowroomAreaID = null;
                        $scope.transferData.fromAreaUD = null;
                    } else if ($scope.transferType == 2) { //rest and re-arrange area

                        //get one from area
                        var fromAreaData = [];
                        supportService.quicksearchShowroomAreaByPhysicalQnt(
                            $scope.quicksearchShowroomAreaFrom.filters,
                            function (data) {
                                if (data.message.type == 0) {
                                    fromAreaData = data.data;
                                    fromAreaData = fromAreaData.filter(function (o) {
                                        return o.showroomItemID == item.showroomItemID;
                                    });
                                    if (fromAreaData.length > 0) {
                                        //push to list to transfer
                                        $scope.listTransferData.push({
                                            showroomItemID: item.showroomItemID,
                                            fromShowroomAreaID: fromAreaData[0].showroomAreaID,
                                            toShowroomAreaID: null,
                                            quantity: 1,

                                            articleCode: item.articleCode,
                                            description: item.description,
                                            fromAreaUD: fromAreaData[0].showroomAreaUD,
                                            toAreaUD: null,
                                        });
                                        $scope.$apply();
                                    }
                                }
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error);
                            }
                        );
                    }
                    $scope.quicksearchShowroomItem.event.close($event);
                },

                barcodeScanItem: function () {
                    if ($scope.quicksearchShowroomItem.data.length > 0 &&
                        jQuery('#' + $scope.quicksearchShowroomItem.searchQueryBoxID).val().length > 0) {
                        var item = $scope.quicksearchShowroomItem.data[0];

                        if ($scope.transferType == 1) //transfer
                        {
                            $scope.transferData.showroomItemID = item.showroomItemID;
                            $scope.transferData.articleCode = item.articleCode;
                            $scope.transferData.description = item.description;

                            //reset shworoom area from
                            $scope.transferData.fromShowroomAreaID = null;
                            $scope.transferData.fromAreaUD = null;
                        } else if ($scope.transferType == 2) { //rest and re-arrange area

                            var fromAreaData = [];
                            supportService.quicksearchShowroomAreaByPhysicalQnt(
                                $scope.quicksearchShowroomAreaFrom.filters,
                                function (data) {
                                    if (data.message.type == 0) {
                                        fromAreaData = data.data;
                                        fromAreaData = fromAreaData.filter(function (o) {
                                            return o.showroomItemID == item.showroomItemID;
                                        });
                                        if (fromAreaData.length > 0) {
                                            //push to list to transfer
                                            $scope.listTransferData.push({
                                                showroomItemID: item.showroomItemID,
                                                fromShowroomAreaID: fromAreaData[0].showroomAreaID,
                                                toShowroomAreaID: null,
                                                quantity: 1,

                                                articleCode: item.articleCode,
                                                description: item.description,
                                                fromAreaUD: fromAreaData[0].showroomAreaUD,
                                                toAreaUD: null,
                                            });
                                            $scope.$apply();
                                        }
                                    }
                                },
                                function (error) {
                                    jsHelper.showMessage('warning', error);
                                }
                            );
                        }
                        jQuery('#' + $scope.quicksearchShowroomItem.searchQueryBoxID).val('');
                        $scope.quicksearchShowroomItem.data = null;
                    }
                }
            }
        };

        //
        //support quick search Area From
        //
        var quicksearchShowroomAreaFromTimer = null;
        $scope.quicksearchShowroomAreaFrom = {
            data: null,
            filters: {
                filters: {
                    searchQuery: ''
                }
            },

            popupformID: 'popup-showroomAreaFrom',
            gridSearchResultID: 'quickSearchResultGrid-showroomAreaFrom',
            searchQueryBoxID: 'quickSearchBox-showroomAreaFrom',

            method: {
                search: function (callBackSuccess) {
                    supportService.quicksearchShowroomAreaByPhysicalQnt(
                        $scope.quicksearchShowroomAreaFrom.filters,
                        function (data) {
                            if (data.message.type == 0) {
                                $scope.quicksearchShowroomAreaFrom.data = data.data;
                                $scope.quicksearchShowroomAreaFrom.data =
                                    $scope.quicksearchShowroomAreaFrom.data.filter(function (o) {
                                        return o.showroomItemID == $scope.transferData.showroomItemID;
                                    });
                                $scope.$apply();

                                if (callBackSuccess != null) callBackSuccess();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                }
            },
            event: {
                searchBoxKeyUp: function ($event) {
                    if ($scope.searchBy == 2) { // search by barcode
                        if ($event.keyCode == 13) {
                            $scope.quicksearchShowroomAreaFrom.event.searchClick(function () {
                                $scope.quicksearchShowroomAreaFrom.event.barcodeScanItem();
                                $scope.$apply();
                            });
                        }
                    } else // search normal
                    {
                        if ($event.keyCode == 13) {
                            $scope.quicksearchShowroomAreaFrom.event.searchClick();
                        } else if (jQuery('#' + $scope.quicksearchShowroomAreaFrom.searchQueryBoxID).val().length >=
                            3) {
                            clearTimeout(quicksearchShowroomAreaFromTimer);
                            quicksearchShowroomAreaFromTimer = setTimeout(
                                function () {
                                    $scope.quicksearchShowroomAreaFrom.event.searchClick();
                                },
                                500);
                        }
                    }
                },

                searchClick: function (callBackSuccess) {
                    $scope.quicksearchShowroomAreaFrom.filters.filters.searchQuery =
                        jQuery('#' + $scope.quicksearchShowroomAreaFrom.searchQueryBoxID).val();
                    $scope.quicksearchShowroomAreaFrom.method.search(function () {
                        if (callBackSuccess != null) {
                            callBackSuccess();
                        } else {
                            jQuery('#' + $scope.quicksearchShowroomAreaFrom.popupformID).show();
                        }
                    });
                },

                close: function ($event, isReset) {
                    $event.preventDefault();
                    jQuery('#' + $scope.quicksearchShowroomAreaFrom.popupformID).hide();
                    jQuery('#' + $scope.quicksearchShowroomAreaFrom.searchQueryBoxID).val('');
                    $scope.quicksearchShowroomAreaFrom.data = null;
                },

                itemSelected: function ($event, item) {
                    $event.preventDefault();
                    $scope.transferData.fromShowroomAreaID = item.showroomAreaID;
                    $scope.transferData.fromAreaUD = item.showroomAreaUD;
                    $scope.quicksearchShowroomAreaFrom.event.close($event);
                },

                barcodeScanItem: function () {
                    if ($scope.quicksearchShowroomAreaFrom.data.length > 0 &&
                        jQuery('#' + $scope.quicksearchShowroomAreaFrom.searchQueryBoxID).val().length > 0) {
                        var item = $scope.quicksearchShowroomAreaFrom.data[0];

                        $scope.transferData.fromShowroomAreaID = item.showroomAreaID;
                        $scope.transferData.fromAreaUD = item.showroomAreaUD;
                        $scope.$apply();
                    }
                    jQuery('#' + $scope.quicksearchShowroomAreaFrom.searchQueryBoxID).val('');
                    $scope.quicksearchShowroomAreaFrom.data = null;
                }
            }
        };

        //
        //support quick search Area To
        //
        var quicksearchShowroomAreaToTimer = null;
        $scope.quicksearchShowroomAreaTo = {
            data: null,
            filters: {
                filters: {
                    searchQuery: ''
                }
            },

            popupformID: 'popup-showroomAreaTo',
            gridSearchResultID: 'quickSearchResultGrid-showroomAreaTo',
            searchQueryBoxID: 'quickSearchBox-showroomAreaTo',

            method: {
                search: function (callBackSuccess) {
                    supportService.quicksearchShowroomArea(
                        $scope.quicksearchShowroomAreaTo.filters,
                        function (data) {
                            if (data.message.type == 0) {
                                $scope.quicksearchShowroomAreaTo.data = data.data;
                                $scope.$apply();

                                if (callBackSuccess != null) callBackSuccess();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                }
            },
            event: {
                searchBoxKeyUp: function ($event) {
                    if ($scope.searchBy == 2) { // search by barcode
                        if ($event.keyCode == 13) {
                            $scope.quicksearchShowroomAreaTo.event.searchClick(function () {
                                $scope.quicksearchShowroomAreaTo.event.barcodeScanItem();
                                $scope.$apply();
                            });
                        }
                    } else // search normal
                    {
                        if ($event.keyCode == 13) {
                            $scope.quicksearchShowroomAreaTo.event.searchClick();
                        } else if (jQuery('#' + $scope.quicksearchShowroomAreaTo.searchQueryBoxID).val().length >= 3) {
                            clearTimeout(quicksearchShowroomAreaToTimer);
                            quicksearchShowroomAreaToTimer = setTimeout(
                                function () {
                                    $scope.quicksearchShowroomAreaTo.event.searchClick();
                                },
                                500);
                        }
                    }
                },

                searchClick: function (callBackSuccess) {
                    $scope.quicksearchShowroomAreaTo.filters.filters.searchQuery =
                        jQuery('#' + $scope.quicksearchShowroomAreaTo.searchQueryBoxID).val();
                    $scope.quicksearchShowroomAreaTo.method.search(function () {
                        if (callBackSuccess != null) {
                            callBackSuccess();
                        } else {
                            jQuery('#' + $scope.quicksearchShowroomAreaTo.popupformID).show();
                        }
                    });
                },

                close: function ($event) {
                    $event.preventDefault();
                    jQuery('#' + $scope.quicksearchShowroomAreaTo.popupformID).hide();
                    jQuery('#' + $scope.quicksearchShowroomAreaTo.searchQueryBoxID).val('');
                    $scope.quicksearchShowroomAreaTo.data = null;
                },

                itemSelected: function ($event, item) {
                    $event.preventDefault();
                    console.log(item);
                    if ($scope.transferType == 1) {
                        $scope.transferData.toShowroomAreaID = item.showroomAreaID;
                        $scope.transferData.toAreaUD = item.showroomAreaUD;
                    } else if ($scope.transferType == 2) {
                        angular.forEach($scope.listTransferData,
                            function (transferItem) {
                                transferItem.toShowroomAreaID = item.showroomAreaID;
                                transferItem.toAreaUD = item.showroomAreaUD
                            });
                    }
                    $scope.quicksearchShowroomAreaTo.event.close($event);
                },

                barcodeScanItem: function () {
                    if ($scope.quicksearchShowroomAreaTo.data.length > 0 &&
                        jQuery('#' + $scope.quicksearchShowroomAreaTo.searchQueryBoxID).val().length > 0) {
                        var item = $scope.quicksearchShowroomAreaTo.data[0];

                        if ($scope.transferType == 1) {
                            $scope.transferData.toShowroomAreaID = item.showroomAreaID;
                            $scope.transferData.toAreaUD = item.showroomAreaUD;
                        } else if ($scope.transferType == 2) {
                            angular.forEach($scope.listTransferData,
                                function (transferItem) {
                                    transferItem.toShowroomAreaID = item.showroomAreaID;
                                    transferItem.toAreaUD = item.showroomAreaUD
                                });
                        }
                        $scope.quicksearchShowroomAreaTo.event.close($event);
                    }
                    jQuery('#' + $scope.quicksearchShowroomAreaTo.searchQueryBoxID).val('');
                    $scope.quicksearchShowroomAreaTo.data = null;
                }
            }
        };


        //
        // private methods
        //

        function reload() {
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = 1;
            jsonService.searchFilter.sortedBy = 'TransferDate';
            jsonService.searchFilter.sortedDirection = 'DESC';
            $scope.event.search();
        }

        function search() {
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    $scope.data = data.data;
                    injectShowLinks($scope.data);
                    $scope.totalPage = Math.ceil(data.totalRows / jsonService.searchFilter.pageSize);
                    $scope.$apply();

                    if (data.totalRows < jsonService.searchFilter.pageSize) {
                        jQuery('#grdSearchResult').find('ul').hide();
                    }
                    else {
                        jQuery('#grdSearchResult').find('ul').show();
                    }
                    jQuery('#content').show();
                    searchResultGrid.updateLayout();
                },
                function (error) {
                    $scope.data = null;
                    $scope.pageIndex = 1;
                    $scope.totalPage = 0;
                    $scope.$apply();
                }
            );

            function injectShowLinks(data) {
                if (data === null || typeof (data) === "undefined") return;

                data.forEach(function (item) {
                    item.showTransferLink = item.transferID !== null && item.transferName !== null;
                });
            }
        }

        function removeTranferItem($event, index) {
            $event.preventDefault();
            $scope.listTransferData.splice(index, 1);
        }

        function transfer($event) {
            $event.preventDefault();

            if ($scope.searchForm.$valid) {
                jsonService.update(0, $scope.transferData,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            $scope.data.push(data.data);
                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
            else {
                jsHelper.showMessage('warning', context.errMsg);
            }
        }

        function transferMultiItem($event) {
            $event.preventDefault();

            jsonService.transferMultiItem($scope.listTransferData,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 0) {
                        $scope.listTransferData = [];
                        $scope.event.reload();
                        $scope.$apply();
                    }
                    else if (data.message.type == 2) {
                        //alert('error');
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
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

    var searchResultGrid = jQuery('#grdSearchResult').scrollableTable(
        function (currentPage) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                scope.pageIndex = currentPage;
                jsonService.searchFilter.pageIndex = currentPage;
                scope.event.search();
            });
        },
        function (sortedBy, sortedDirection) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                scope.pageIndex = 1;
                jsonService.searchFilter.pageIndex = 1;
                jsonService.searchFilter.sortedBy = sortedBy;
                jsonService.searchFilter.sortedDirection = sortedDirection;
                scope.event.search();
            });
        }
    );

    var quickSearchResultGridShowroomItem = jQuery('#quickSearchResultGrid-showroomItem').scrollableTable2(
        'quicksearchShowroomItem.filters.pageIndex',
        'quicksearchShowroomItem.totalPage',
        function (currentPage) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                scope.quicksearchShowroomItem.filters.pageIndex = currentPage;
                scope.quicksearchShowroomItem.method.search();
            });
        },
        function (sortedBy, sortedDirection) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                scope.quicksearchShowroomItem.filters.sortedDirection = sortedDirection;
                scope.quicksearchShowroomItem.filters.pageIndex = 1;
                scope.quicksearchShowroomItem.filters.sortedBy = sortedBy;
                scope.quicksearchShowroomItem.method.search();
            });
        }
    );

})();

