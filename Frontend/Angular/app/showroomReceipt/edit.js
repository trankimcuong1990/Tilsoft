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

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.users = null;
    $scope.showrooms = null;
    $scope.seasons = null;
    $scope.newID = -1;

    quickSearchShowroomItemTimer = null;
    quicksearchClientTimer = null;
    quicksearchShowroomAreaTimer = null;

    $scope.searchBy = 1; // normal
    

    //
    // event
    //
    $scope.event = {
        load: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.users = data.data.users;
                    $scope.showrooms = data.data.showrooms;
                    $scope.seasons = data.data.seasons;
                    $scope.$apply();
                    jQuery('#content').show();

                    if (context.id == 0)
                    {
                        $scope.data.receiptTypeID = context.receiptTypeID;
                        $scope.data.receiptTypeText = context.receiptTypeText;
                        $scope.$apply();
                    }
                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.users = null;
                    $scope.showrooms = null;;
                    $scope.$apply();
                }
            );
        },
        update: function ($event) {
            $event.preventDefault();

            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.showroomReceiptID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', context.errMsg);
            }
        },
        delete: function ($event) {
            $event.preventDefault();

            if (confirm('Are you sure you want to delete ?')) {
                jsonService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },
        removeProduct: function ($event, index) {
            $event.preventDefault();
            $scope.data.showroomReceiptDetails.splice(index, 1);
        },

        removeArea: function ($event, index, showroomReceiptDetailID) {
            $event.preventDefault();
            var receiptDetail = $scope.data.showroomReceiptDetails.filter(function (o) { return o.showroomReceiptDetailID == showroomReceiptDetailID })[0];
            receiptDetail.showroomReceiptAreaDetails.splice(index, 1);
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            window.location = context.refreshUrl + id  
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        }
    };

    //
    //support quick search showroom item
    //
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

        popupformID : 'popup-showroomItem',
        gridSearchResultID: 'quickSearchResultGrid-showroomItem',
        searchQueryBoxID : 'quickSearchBox-showroomItem',
        
        method: {
            search: function (callBackSuccess) {
                supportService.quicksearchShowroomItem(
                    $scope.quicksearchShowroomItem.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quicksearchShowroomItem.data = data.data;
                            $scope.quicksearchShowroomItem.totalPage = Math.ceil(data.totalRows / $scope.quicksearchShowroomItem.filters.pageSize);
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
                }
                else // search normal
                { 
                    if ($event.keyCode == 13) {
                        $scope.quicksearchShowroomItem.event.searchClick();
                    }
                    else if (jQuery('#' + $scope.quicksearchShowroomItem.searchQueryBoxID).val().length >= 3) {
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
                $scope.quicksearchShowroomItem.filters.filters.searchQuery = jQuery('#' + $scope.quicksearchShowroomItem.searchQueryBoxID).val();
                $scope.quicksearchShowroomItem.filters.pageIndex = 1;
                $scope.quicksearchShowroomItem.method.search(function () {
                    if (callBackSuccess != null)
                    {
                        callBackSuccess();
                    }
                    else
                    {
                        jQuery('#' + $scope.quicksearchShowroomItem.popupformID).show();
                    }
                });
            },

            close: function ($event) {
                jQuery('#' + $scope.quicksearchShowroomItem.popupformID).hide();
                jQuery('#' + $scope.quicksearchShowroomItem.searchQueryBoxID).val('');
                $scope.quicksearchShowroomItem.data = null;
                $event.preventDefault();
            },
            itemSelected: function ($event, id) {
                jQuery.each($scope.quicksearchShowroomItem.data, function () {
                    if (this.isSelected) {
                        //check is exist in list showrooom item detail
                        var showroomItemID = this.showroomItemID;
                        var checkItem = $scope.data.showroomReceiptDetails.filter(function (o) {return o.showroomItemID == showroomItemID });
                        // if not found this product in list detail then push to list detail
                        if (checkItem == null || checkItem.length == 0)
                        {
                            $scope.data.showroomReceiptDetails.push({
                                showroomReceiptDetailID: $scope.method.getNewID(),
                                showroomItemID: this.showroomItemID,
                                articleCode: this.articleCode,
                                description: this.description
                            });
                        }
                    }
                });
                $scope.quicksearchShowroomItem.event.close($event);
            },

            barcodeScanItem: function () {
                if ($scope.quicksearchShowroomItem.data.length > 0 && jQuery('#' + $scope.quicksearchShowroomItem.searchQueryBoxID).val().length > 0)
                {
                    detailID = $scope.method.getNewID();
                    //take first item
                    var result = $scope.quicksearchShowroomItem.data[0];
                    //check is exist in list showrooom item detail
                    var checkItem = $scope.data.showroomReceiptDetails.filter(function (o) { return o.showroomItemID == result.showroomItemID });
                    // if not found this product in list detail then push to list detail
                    if (checkItem == null || checkItem.length == 0)
                    {
                        $scope.data.showroomReceiptDetails.push({
                            showroomReceiptDetailID: detailID,
                            showroomItemID: result.showroomItemID,
                            articleCode: result.articleCode,
                            description: result.description,
                        });
                    }
                }
                $scope.$apply();
                jQuery('#' + $scope.quicksearchShowroomItem.searchQueryBoxID).val('');
                $('#quickSearchBox-showroomarea' + detailID).focus();
            }
        }
    }


    //quick search client
    $scope.quicksearchClient = {
        data: null,
        filters: {
            filters: {
                searchQuery: ''
            },
            sortedBy: 'ClientUD',
            sortedDirection: 'ASC',
            pageSize: 10,
            pageIndex: 1
        },
        totalPage: 0,

        popupformID: 'popup-client',
        gridSearchResultID: 'quickSearchResultGrid-client',
        //searchQueryBoxID: 'quickSearchBox-showroomItem',

        showroomReceiptDetailID : null,

        method: {
            search: function (callBack) {
                supportService.quicksearchClient(
                    $scope.quicksearchClient.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quicksearchClient.data = data.data;
                            $scope.quicksearchClient.totalPage = Math.ceil(data.totalRows / $scope.quicksearchClient.filters.pageSize);
                            quickSearchResultGridShowroomItem.updateLayout();
                            $scope.$apply();

                            if (callBack != null) callBack();                            
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        event: {
            searchClick: function ($event, showroomReceiptDetailID, searchValue) {
                //mark the id of row that is editing
                $scope.quicksearchClient.showroomReceiptDetailID = showroomReceiptDetailID;
                //filter for quicksearch
                $scope.quicksearchClient.filters.filters.searchQuery = (searchValue == null ? '' : searchValue);
                $scope.quicksearchClient.filters.pageIndex = 1;
                //search client
                $scope.quicksearchClient.method.search(function () {
                    jQuery("#receipt_detail_id" + $scope.quicksearchClient.showroomReceiptDetailID).append(jQuery('#' + $scope.quicksearchClient.popupformID));
                    jQuery('#' + $scope.quicksearchClient.popupformID).show();
                });
            },

            searchBoxKeyUp: function ($event, showroomReceiptDetailID, searchValue) {

                if ($event.keyCode == 13) {
                    $scope.quicksearchClient.event.searchClick($event, showroomReceiptDetailID, searchValue);
                }
                else if (searchValue.length >= 3) {
                    clearTimeout(quicksearchClientTimer);
                    quicksearchClientTimer = setTimeout(
                        function () {
                            $scope.quicksearchClient.event.searchClick($event, showroomReceiptDetailID, searchValue);
                        },
                        500
                    );
                }
            },

            close: function ($event) {
                jQuery('#' + $scope.quicksearchClient.popupformID).hide();
                $scope.quicksearchClient.data = null;
                $event.preventDefault();
            },
            itemSelected: function ($event, client) {
                var receiptDetail = $scope.data.showroomReceiptDetails.filter(function (o) { return o.showroomReceiptDetailID == $scope.quicksearchClient.showroomReceiptDetailID })[0];
                receiptDetail.clientID = client.clientID;
                receiptDetail.clientUD = client.clientUD;
                $scope.quicksearchClient.event.close($event);
            }
        }
    }


    //quick search showroom area
    $scope.quicksearchShowroomArea = {
        data: null,
        filters: {
            filters: {
                searchQuery: ''
            },
        },

        popupformID: 'popup-showroomarea',
        gridSearchResultID: 'quickSearchResultGrid-showroomarea',
        searchQueryBoxID: 'quickSearchBox-showroomarea',

        showroomReceiptDetailID: null,
        showroomItemID : null,

        method: {
            search: function (successCallBack) {
                if (context.receiptTypeID == 1) //Import receipt
                {
                    supportService.quicksearchShowroomArea(
                        $scope.quicksearchShowroomArea.filters,
                        function (data) {
                            if (data.message.type == 0) {
                                $scope.quicksearchShowroomArea.data = data.data;
                                $scope.$apply();

                                if (successCallBack != null) successCallBack();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                }
                else if (context.receiptTypeID == 2) //Export receipt
                {
                    supportService.quicksearchShowroomAreaByPhysicalQnt(
                        $scope.quicksearchShowroomArea.filters,
                        function (data) {
                            if (data.message.type == 0) {
                                $scope.quicksearchShowroomArea.data = data.data;
                                $scope.quicksearchShowroomArea.data = $scope.quicksearchShowroomArea.data.filter(function (o) { return o.showroomItemID == $scope.quicksearchShowroomArea.showroomItemID });
                                $scope.$apply();

                                if (successCallBack != null) successCallBack();
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                }
                
            }
        },
        event: {
            searchBoxKeyUp: function ($event, showroomReceiptDetailID, showroomItemID) {
                //mark the id of row that is editing
                $scope.quicksearchShowroomArea.showroomReceiptDetailID = showroomReceiptDetailID;
                $scope.quicksearchShowroomArea.showroomItemID = showroomItemID;
                $scope.quicksearchShowroomArea.searchQueryBoxID = 'quickSearchBox-showroomarea' + showroomReceiptDetailID;

                if ($scope.searchBy == 2) // search by barcode
                {
                    if ($event.keyCode == 13) {
                        $scope.quicksearchShowroomArea.filters.filters.searchQuery = jQuery('#' + $scope.quicksearchShowroomArea.searchQueryBoxID).val();
                        $scope.quicksearchShowroomArea.method.search(function () {
                            $scope.quicksearchShowroomArea.event.barcodeScanItem();
                            $scope.$apply();
                        });
                    }
                }
                else // search normal
                { 
                    if ($event.keyCode == 13) {
                        $scope.quicksearchShowroomArea.filters.filters.searchQuery = jQuery('#' + $scope.quicksearchShowroomArea.searchQueryBoxID).val();
                        $scope.quicksearchShowroomArea.method.search(function () {
                            jQuery("#area_receipt_detail_id" + $scope.quicksearchShowroomArea.showroomReceiptDetailID).append(jQuery('#' + $scope.quicksearchShowroomArea.popupformID));
                            jQuery('#' + $scope.quicksearchShowroomArea.popupformID).show();
                        });
                    }
                    else if (jQuery('#' + $scope.quicksearchShowroomArea.searchQueryBoxID).val().length >= 3) {
                        clearTimeout(quicksearchShowroomAreaTimer);
                        quicksearchShowroomAreaTimer = setTimeout(
                            function () {
                                $scope.quicksearchShowroomArea.filters.filters.searchQuery = jQuery('#' + $scope.quicksearchShowroomArea.searchQueryBoxID).val();
                                $scope.quicksearchShowroomArea.method.search(function () {
                                    jQuery("#area_receipt_detail_id" + $scope.quicksearchShowroomArea.showroomReceiptDetailID).append(jQuery('#' + $scope.quicksearchShowroomArea.popupformID));
                                    jQuery('#' + $scope.quicksearchShowroomArea.popupformID).show();
                                });
                            },
                            500
                        );
                    }
                }
            },

            close: function ($event) {
                jQuery('#' + $scope.quicksearchShowroomArea.popupformID).hide();
                jQuery('#' + $scope.quicksearchShowroomArea.searchQueryBoxID).val('');
                $scope.quicksearchShowroomArea.data = null;
                $event.preventDefault();
            },

            itemSelected: function ($event, showroomArea) {
                //get current row that is editing
                var receiptDetail = $scope.data.showroomReceiptDetails.filter(function (o) { return o.showroomReceiptDetailID == $scope.quicksearchShowroomArea.showroomReceiptDetailID })[0];
                if (receiptDetail.showroomReceiptAreaDetails == null) receiptDetail.showroomReceiptAreaDetails = [];
                //check is exist in list area
                var checkArea = receiptDetail.showroomReceiptAreaDetails.filter(function (o) { return o.showroomAreaID == showroomArea.showroomAreaID });
                //if not found area in current list area detail then push to list area detail
                if (checkArea == null || checkArea.length == 0) {
                    receiptDetail.showroomReceiptAreaDetails.push({
                        showroomReceiptAreaDetailID: $scope.method.getNewID(),
                        showroomAreaID: showroomArea.showroomAreaID,
                        showroomAreaUD: showroomArea.showroomAreaUD,
                        quantity: 1
                    });
                }
                else {
                    //increase then current quantity to 1
                    checkArea[0].quantity = checkArea[0].quantity + 1; 
                }
                $scope.quicksearchShowroomArea.event.close($event);
            },

            barcodeScanItem: function () {
                if ($scope.quicksearchShowroomArea.data.length > 0 && jQuery('#' + $scope.quicksearchShowroomArea.searchQueryBoxID).val().length > 0) {
                    //take the first area to push to list area detail
                    var showroomArea = $scope.quicksearchShowroomArea.data[0];
                    //get current row that is editing
                    var receiptDetail = $scope.data.showroomReceiptDetails.filter(function (o) { return o.showroomReceiptDetailID == $scope.quicksearchShowroomArea.showroomReceiptDetailID })[0];
                    if (receiptDetail.showroomReceiptAreaDetails == null) receiptDetail.showroomReceiptAreaDetails = [];
                    //check is exist in list area
                    var checkArea = receiptDetail.showroomReceiptAreaDetails.filter(function (o) { return o.showroomAreaID == showroomArea.showroomAreaID });
                    //if not found area in current list area detail then push to list area detail
                    if (checkArea == null || checkArea.length == 0) {
                        receiptDetail.showroomReceiptAreaDetails.push({
                            showroomReceiptAreaDetailID: $scope.method.getNewID(),
                            showroomAreaID: showroomArea.showroomAreaID,
                            showroomAreaUD: showroomArea.showroomAreaUD,
                            quantity: 1
                        });
                    }
                    else {
                        //increase then current quantity to 1
                        checkArea[0].quantity = checkArea[0].quantity + 1;
                    }
                    $scope.$apply()
                    jQuery('#' + $scope.quicksearchShowroomArea.searchQueryBoxID).val('');
                    jQuery('#' + $scope.quicksearchShowroomItem.searchQueryBoxID).focus();
                }
            }
        }
    }

    //
    // init
    //
    $scope.event.load();
}]);