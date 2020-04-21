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

var quickSearchResultGridClient = jQuery('#quickSearchResultGrid-client').scrollableTable2(
            'quicksearchClient.filters.pageIndex',
            'quicksearchClient.totalPage',
            function (currentPage) {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    scope.quicksearchClient.filters.pageIndex = currentPage;
                    scope.quicksearchClient.method.search();
                });
            },
            function (sortedBy, sortedDirection) {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    scope.quicksearchClient.filters.sortedDirection = sortedDirection;
                    scope.quicksearchClient.filters.pageIndex = 1;
                    scope.quicksearchClient.filters.sortedBy = sortedBy;
                    scope.quicksearchClient.method.search();
                });
            }
        );

var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.filters = {
        ArticleCode: '',
        Description: '',
    };
    $scope.pageIndex = 1;
    $scope.totalPage = 0;

    $scope.data = null;

    $scope.client = {
        clientID : null,
        clientUD : null,
        clientNM : null
    }

    //
    //total function
    //
    $scope.method = {
        calTotalFTS: function () {
            if ($scope.data == null) return;
            var total = parseInt('0');
            angular.forEach($scope.data, function (item) {
                total += (JSON.stringify(item.ftsQnt) == 'null' ? parseInt('0') : parseInt(item.ftsQnt));
            }, total);
            return total;
        },

        calTotalFTSQntIn40HC: function () {
            if ($scope.data == null) return;
            var total = parseFloat('0');
            angular.forEach($scope.data, function (item) {
                total += (JSON.stringify(item.ftsQntIn40HC) == 'null' ? parseFloat('0') : parseFloat(item.ftsQntIn40HC));
            }, total);
            return total;
        },

        calTotalFTSSelected: function () {
            if ($scope.selectedItemPrintForm.selectedItem == null) return;
            var total = parseInt('0');
            angular.forEach($scope.selectedItemPrintForm.selectedItem, function (item) {
                total += (JSON.stringify(item.ftsQnt) == 'null' ? parseInt('0') : parseInt(item.ftsQnt));
            }, total);
            return total;
        },

        calTotalFTSQntIn40HCSelected: function () {
            if ($scope.selectedItemPrintForm.selectedItem == null) return;
            var total = parseFloat('0');
            angular.forEach($scope.selectedItemPrintForm.selectedItem, function (item) {
                total += (JSON.stringify(item.ftsQntIn40HC) == 'null' ? parseFloat('0') : parseFloat(item.ftsQntIn40HC));
            }, total);
            return total;
        }
    }


    //
    // event
    //
    $scope.event = {
        reload: function () {
            $scope.pageIndex = 1;
            jsonService.searchFilter.pageIndex = 1;
            jsonService.searchFilter.sortedBy = 'DisplayOrder';
            jsonService.searchFilter.sortedDirection = 'DESC';
            $scope.event.search();
        },
        search: function () {
            jsonService.searchFilter.filters = $scope.filters;
            jsonService.search(
                function (data) {
                    //get list selected item before
                    var catch_selectedItems = angular.copy($scope.selectedItemPrintForm.selectedItem);

                    //refresh search data
                    $scope.data = data.data.data;

                    $scope.totalFTSQnt = data.data.totalFTSQnt;
                    $scope.totalFTSQntIn40HC = data.data.totalFTSQntIn40HC;

                    //reset selected status for item that selected item before
                    angular.forEach($scope.data, function (item, i) {
                        var x = catch_selectedItems.filter(function (o) { return o.keyID == item.keyID });
                        if (x != null && x.length > 0)
                        {
                            item.isSelected = true;
                        }
                    });

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
        },

        printFreeToSaleOverview: function () {
            jsonService.getFreeToSaleOverview(true,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type != 2) {
                            window.location = context.reportUrl + data.data + '.xlsm';
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
        },

        addToPrint: function ($event, item) {
            $event.preventDefault();
            item.isSelected = true;
            if ($scope.selectedItemPrintForm.selectedItem.filter(function (o) { return o.keyID == item.keyID }).length > 0) return;
            $scope.selectedItemPrintForm.selectedItem.push(item);
        },

        selectedItemToPrint: function ($event, item) {
            if (item.isSelected) {
                if ($scope.selectedItemPrintForm.selectedItem.filter(function (o) { return o.keyID == item.keyID }).length > 0) return;
                $scope.selectedItemPrintForm.selectedItem.push(item);
            }
            else {
                var j = -1;
                for (var i = 0; i < $scope.selectedItemPrintForm.selectedItem.length; i++) {
                    if ($scope.selectedItemPrintForm.selectedItem[i].keyID == item.keyID) {
                        j = i;
                        break;
                    }
                }
                if (j >= 0) {
                    $scope.selectedItemPrintForm.selectedItem.splice(j, 1);
                }
            }
        },

        real_ref_Selected: function (item) {
            //call from stock list service
            jsonService.serviceUrl = context.backEndUrl + 'api/reportstocklist/';
            isMatchedImage = (item.isMatchedImage == 1 ? true : false);
            jsonService.matchedImageProduct(item.goodsID, isMatchedImage,
                function (data) {
                    if (data.message.type != 2) {
                        if (isMatchedImage) {
                            document.getElementById(item.keyID).src = '';
                            //document.getElementById('selected-' + item.keyID).src = '';
                            item.selectedThumbnailImage = item.productThumbnailImage;
                            item.selectedFullImage = item.productFullImage;
                        }
                        else {
                            item.selectedThumbnailImage = item.modelThumbnailImage;
                            item.selectedFullImage = item.modelFullImage;
                        }
                        $scope.$apply();
                    }
                    else {
                        jsHelper.processMessage(data.message);
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
            //return call service for current module
            jsonService.serviceUrl = context.backEndUrl + 'api/reportfreetosaleoverview/';
        },
    }


    $scope.selectedItemPrintForm = {

        selectedItem : [],
        filters: {
            KeyIDs: '',
            ClientID : null
        },
        event: {
            load: function ($event) {
                $event.preventDefault();

                $scope.selectedItemPrintForm.selectedItem = [];
                angular.forEach($scope.data, function (item, i) {
                    if (item.isSelected)
                    {
                        $scope.selectedItemPrintForm.selectedItem.push(item);
                    }
                });
                jsHelper.showPopup('selectedItemPrintForm', function () { });
            },

            selectClient: function ($event) {
                $event.preventDefault();
                $('#frmSelectClient').modal('show');
            },

            print: function ($event) {
                $event.preventDefault();
                //get item id param to print 
                $scope.selectedItemPrintForm.filters.KeyIDs = '';
                for (i = 0; i < $scope.selectedItemPrintForm.selectedItem.length; i++)
                {
                    $scope.selectedItemPrintForm.filters.KeyIDs += $scope.selectedItemPrintForm.selectedItem[i].keyID + ',';
                }
                //get client id param to print 
                $scope.selectedItemPrintForm.filters.ClientID = $scope.client.clientID;

                //print
                $scope.selectedItemPrintForm.method.printReport($event);
            },
            cancel: function ($event) {
                $event.preventDefault();
                jsHelper.hidePopup('selectedItemPrintForm', function () { });
            },
            remove: function ($event,index,item) {
                $event.preventDefault();
                item.isSelected = false;
                $scope.selectedItemPrintForm.selectedItem.splice(index, 1);
            },
        },

        method: {
            printReport: function ($event) {
                $event.preventDefault();
                jsonService.getFreeToSaleSelected($scope.selectedItemPrintForm.filters,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type != 2) {
                            window.location = context.reportUrl + data.data + '.xlsm';
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            },
        }
    }

    //quick search client
    var quicksearchClientTimer = null;
    $scope.quicksearchClient = {
        data: null,
        filters: {
            filters: {
                searchQuery: ''
            },
            sortedBy: 'ClientNM',
            sortedDirection: 'ASC',
            pageSize: 20,
            pageIndex: 1
        },
        totalPage: 0,

        popupformID: 'popup-client',
        gridSearchResultID: 'quickSearchResultGrid-client',
        searchQueryBoxID: 'quickSearchBox-client',

        method: {
            search: function () {
                supportService.quicksearchClient(
                    $scope.quicksearchClient.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quicksearchClient.data = data.data;
                            $scope.quicksearchClient.totalPage = Math.ceil(data.totalRows / $scope.quicksearchClient.filters.pageSize);
                            quickSearchResultGridClient.updateLayout();
                            $scope.$apply();
                            jQuery('#' + $scope.quicksearchClient.popupformID).show();
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
                if ($event.keyCode == 13) {
                    $scope.quicksearchClient.event.searchClick();
                }
                else if (jQuery('#' + $scope.quicksearchClient.searchQueryBoxID).val().length >= 3) {
                    clearTimeout(quicksearchClientTimer);
                    quicksearchClientTimer = setTimeout(
                        function () {
                            $scope.quicksearchClient.event.searchClick();
                        },
                        500);
                }
            },

            searchClick: function () {
                $scope.quicksearchClient.filters.filters.searchQuery = jQuery('#' + $scope.quicksearchClient.searchQueryBoxID).val();
                $scope.quicksearchClient.filters.pageIndex = 1;
                $scope.quicksearchClient.method.search();
            },

            close: function ($event) {
                jQuery('#' + $scope.quicksearchClient.popupformID).hide();
                jQuery('#' + $scope.quicksearchClient.searchQueryBoxID).val('');
                $scope.quicksearchClient.data = null;
                $event.preventDefault();
            },
            itemSelected: function ($event, id) {
                jQuery.each($scope.quicksearchClient.data, function () {
                    var client = $scope.quicksearchClient.data.filter(function (o) { return o.clientID == id });
                    $scope.client.clientID = id;
                    $scope.client.clientUD = client[0].clientUD;
                    $scope.client.clientNM = client[0].clientNM;
                });
                $scope.quicksearchClient.event.close($event);
            }
        }
    }


    $scope.event.reload();
}]);