var quickSearchResultGridMaterial = jQuery('#quickSearchResultGrid-material').scrollableTable2(
    'quickSearchMaterial.filters.pageIndex',
    'quickSearchMaterial.totalPage',
    function (currentPage) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quickSearchMaterial.filters.pageIndex = currentPage;
            scope.quickSearchMaterial.method.search();
        });
    },
    function (sortedBy, sortedDirection) {
        var scope = angular.element(jQuery('body')).scope();
        scope.$apply(function () {
            scope.quickSearchMaterial.filters.sortedDirection = sortedDirection;
            scope.quickSearchMaterial.filters.pageIndex = 1;
            scope.quickSearchMaterial.filters.sortedBy = sortedBy;
            scope.quickSearchMaterial.method.search();
        });
    }
);

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.rapData = [];
    $scope.estimatedMaterialData = [];
    $scope.factoryAreas = null;
    $scope.importReceipt = {
        factoryAreaID : null,
    }

    //calculate sum function
    $scope.calTotalMaterial = function (factoryMaterialItem) {
        var total = parseFloat('0');
        angular.forEach(factoryMaterialItem.factoryOrderDetails, function (mItem) {
            total += parseFloat(mItem.amountQnt == '' || mItem.amountQnt == null ? 0 : mItem.amountQnt);
        })
        return total;
    }

    //
    // event
    //
    $scope.event = {
        load: function () {

            jsonService.getSupportData(
                function (data) {
                    jQuery('#content').show();
                    $scope.factoryAreas = data.data.factoryAreas;
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        },
        
        removeMaterial: function ($event, index) {
            $event.preventDefault();
            $scope.rapData.splice(index, 1);
        },

        showEstimatedMaterialForm: function ($event) {
            $event.preventDefault();
            jsHelper.showPopup('estimatedMaterialForm', function () {
                jQuery('#sparks').show();
                jQuery('#btnEstimate').hide();
                jQuery('#btnImport').show();
                jQuery('#btnBack').show();
                //get list selected factory order
                var factoryOrderIds = '';
                angular.forEach($scope.rapData, function (item) {
                    factoryOrderIds += item.factoryOrderID + ';';
                });
                //estima material
                jsonService.estimatedMaterial(
                    factoryOrderIds,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.estimatedMaterialData = data.data;
                            $scope.$apply();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            });
        },

        back: function ($event) {
            jsHelper.hidePopup('estimatedMaterialForm', function () {
                jQuery('#btnEstimate').show();
                jQuery('#btnImport').hide();
                jQuery('#btnBack').hide();
            });
        },

        showImportForm: function ($event) {
            $event.preventDefault();
            $('#frmImport').modal('show');
        },

        importMaterial: function ($event) {
            $event.preventDefault();

            bootbox.confirm({
                message: 'System will import all product that you are selected to import to stock. Are you sure you want to import?',
                size: 'medium',
                buttons: {
                    confirm: {
                        label: 'Yes',
                        className: 'btn-primary'
                    },
                    cancel: {
                        label: 'No',
                        className: 'btn-danger'
                    }
                },
                callback: function (result) {
                    if (result) {
                        //set all product to stock that user are selected
                        angular.forEach($scope.estimatedMaterialData, function (item) {
                            item.factoryAreaID = $scope.importReceipt.factoryAreaID;
                        });
                        //call to server to import product to stock
                        jsonService.importMaterial(
                            $scope.estimatedMaterialData,
                            function (data) {
                                jsHelper.processMessage(data.message);
                                if (data.message.type == 0) {
                                    window.open(context.editUrl + data.data, '');
                                    $('#frmImport').modal('hide');
                                }
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error);
                            }
                        );
                    }
                }
            });
        },

        toggleAll: function (item) {
            angular.forEach(item.factoryOrderDetails, function (subItem) {
                subItem.isSelected = item.isSelected;
                subItem.amountQnt = subItem.isSelected ? subItem.orderQnt * subItem.normValue : 0;
            })
        },

        itemToggled: function (fOrder,item) {
            fOrder.amountQnt = fOrder.isSelected ? fOrder.orderQnt * fOrder.normValue : 0;
            item.isSelected = item.factoryOrderDetails.every(function (subItem) {
                return subItem.isSelected;
            })
        }


    },

    //quick search material
    searchMaterialTimer = null;
    $scope.quickSearchMaterial = {
        data: null,
        filters: {
            filters: {
                searchQuery: ''
            },
            sortedBy: 'FactoryOrderUD',
            sortedDirection: 'ASC',
            pageSize: 10,
            pageIndex: 1
        },
        totalPage: 0,

        popupformID: 'popup-material',
        gridSearchResultID: 'quickSearchResultGrid-material',
        searchQueryBoxID: 'quickSearchBox-material',

        method: {
            search: function () {
                supportService.quickSearchFactoryOrder(
                    $scope.quickSearchMaterial.filters,
                    function (data) {
                        if (data.message.type == 0) {
                            $scope.quickSearchMaterial.data = data.data.data;
                            var totalRows = data.data.totalRows;

                            $scope.quickSearchMaterial.totalPage = Math.ceil(totalRows / $scope.quickSearchMaterial.filters.pageSize);
                            quickSearchResultGridMaterial.updateLayout();
                            $scope.$apply();
                            jQuery('#' + $scope.quickSearchMaterial.popupformID).show();
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
                    $scope.quickSearchMaterial.event.searchClick();
                }
                else if (jQuery('#' + $scope.quickSearchMaterial.searchQueryBoxID).val().length >= 3) {
                    clearTimeout(searchMaterialTimer);
                    searchMaterialTimer = setTimeout(
                        function () {
                            $scope.quickSearchMaterial.event.searchClick();
                        },
                        500);
                }
            },

            searchClick: function () {
                $scope.quickSearchMaterial.filters.filters.searchQuery = jQuery('#' + $scope.quickSearchMaterial.searchQueryBoxID).val();
                $scope.quickSearchMaterial.filters.pageIndex = 1;
                $scope.quickSearchMaterial.method.search();
            },

            close: function ($event) {
                jQuery('#' + $scope.quickSearchMaterial.popupformID).hide();
                jQuery('#' + $scope.quickSearchMaterial.searchQueryBoxID).val('');
                $scope.quickSearchMaterial.data = null;
                $event.preventDefault();
            },
            ok: function ($event) {
                $event.preventDefault();
                angular.forEach($scope.quickSearchMaterial.data, function (item) {
                    if (item.isSelected)
                    {
                        checkExistData = $scope.rapData.filter(function (o) { return o.factoryOrderID == item.factoryOrderID });
                        if (checkExistData == null || checkExistData.length == 0)
                        {
                            $scope.rapData.push(item);
                        }
                    }
                });
                $scope.quickSearchMaterial.event.close($event);
            }
        }
    }



    //
    // init
    //
    $scope.event.load();
}]);