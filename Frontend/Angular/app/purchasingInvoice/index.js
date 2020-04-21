(function () {
    'use strict';

    angular
        .module('tilsoftApp', ['widgets'])
        .controller('tilsoftController', ['$scope', PurchasingInvoiceController]);

    function PurchasingInvoiceController($scope) {
        //#region properties
        $scope.data = null;
        $scope.allData = null;
        $scope.supportData = null;
        $scope.totalAmount = 0;
        $scope.totalFactoryAmount = 0;
        $scope.userOfficeID = -1;
        $scope.isSelectedAll = false;

        $scope.pageIndex = 1;
        $scope.totalPage = 0;
        $scope.filters = {
            Season: context.season,
            InvoiceNo: '',
            ProformaInvoiceNo: '',
            ClientUD: '',
            ContainerNo: '',
            BLNo: '',
            PurchasingCreditNoteNo: '',
            SupplierID: '',
            FactoryInvoiceNo: '',
            CompanyID: '',
            ConfirmAll: '',
            UpdateName:''
        };
        //#endregion properties

        
        //#region events & methods
        $scope.event = {
            search: eventSearch,
            toggleSelectAll: function() {
                if ($scope.isSelectedAll === true) {
                    angular.forEach($scope.data, function (item) {
                        item.isSelected = $scope.isSelectedAll;
                    });
                }
                else {
                    angular.forEach($scope.data, function (item) {
                        item.isSelected = $scope.isSelectedAll;
                    });
                }
            }
        };

        $scope.search = search;

        $scope.delete = deleteItem;

        $scope.getSearchSupport = getSearchSupport;

        $scope.exportExactOnlineSoftware = exportExactOnlineSoftware;

        $scope.resetExportExact = resetExportExact;
        //#endregion events & methods

        
        //#region init page
        $scope.getSearchSupport();        
        //#endregion init page

        
        //#region private methods
        function eventSearch() {
            purchasingInvoiceService.searchFilter.filters = $scope.filters;
            $scope.search();
        }

        function search() {
            purchasingInvoiceService.search(
                function (data) {
                    $("#supplierID").select2();
                    $scope.data = data.data.data;
                    $scope.allData = data.data.allData;
                    injectShowLinks($scope.data);
                    $scope.totalAmount = data.data.totalAmount;
                    $scope.totalFactoryAmount = data.data.totalFactoryAmount;
                    $scope.userOfficeID = data.data.userOfficeID;

                    $scope.totalPage = Math.ceil(data.totalRows / purchasingInvoiceService.searchFilter.pageSize);
                    $scope.$apply();

                    if (data.totalRows < purchasingInvoiceService.searchFilter.pageSize) {
                        jQuery('#searchTableContent').find('ul').hide();
                    }
                    else {
                        jQuery('#searchTableContent').find('ul').show();
                    }

                    jQuery('#content').show();
                    searchGrid.updateLayout();

                    if (data.message.type == 2) {
                        jsHelper.processMessage(data.message);
                    }
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
                    item.showUpdatorLink = item.updatorID !== null && item.updatorName !== null;
                    item.showCreatorLink = item.creatorID !== null && item.creatorName !== null;
                });
            }
        }

        function deleteItem(id, $event) {
            $event.preventDefault();
            if (confirm('Are you sure you want to delete?')) {
                purchasingInvoiceService.delete(id,
                    function (data) {
                        jsHelper.processMessage(data.message);

                        if (data.message.type == 0) {
                            var j = -1;
                            for (var i = 0; i < $scope.data.length; i++) {
                                if ($scope.data[i].purchasingInvoiceID == id) {
                                    j = i;
                                    break;
                                }
                            }

                            if (j >= 0) {
                                $scope.models.splice(j, 1);
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

        function getSearchSupport() {
            purchasingInvoiceService.getSearchSupport(
                function (data) {
                    $scope.supportData = data.data;
                    $scope.$apply();

                    $scope.event.search();
                },
                function (error) {
                    $scope.supportData = null;
                    $scope.$apply();
                }
            );
        }

        function exportExactOnlineSoftware($event) {
            debugger;
            //FIXME: should not issue GLOBAL vars.
            $event.preventDefault();
            var ids = '';
            var iDs = [];
            var items = [];
            if ($scope.isSelectedAll === true) {
                angular.forEach($scope.allData, function (item) {
                    if (!item.isExpotedToExact) {
                        ids += item.purchasingInvoiceID + ',';
                        iDs.push(item.purchasingInvoiceID);
                        items.push(item);
                    }
                });
            }
            else {
                angular.forEach($scope.data, function (item) {
                    if (item.isSelected && !item.isExpotedToExact) {
                        ids += item.purchasingInvoiceID + ',';
                        iDs.push(item.purchasingInvoiceID);
                        items.push(item);
                    }
                });
            }
            
            if (iDs.length > 0) {
                purchasingInvoiceService.exportExactOnlineSoftware(ids
                    , function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type != 2) {
                            window.open(context.reportUrl + data.data + '.xlsm');
                            //confirm mark as exported
                            bootbox.confirm({
                                message: '<h4><span class="text-success"><i class="fa fa-check"></i>Exported invoices success !!!</span></h4></br>Do you want to mark invoices as exported to excact online?',
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
                                        purchasingInvoiceService.markAsExportedToExact(iDs
                                            , function (data) {
                                                if (data) {
                                                    bootbox.alert('<span class="text-success">Marked invoices as exported to exact online success</span>');
                                                    angular.forEach(items, function (item) {
                                                        item.isSelected = false;
                                                        item.isExpotedToExact = true;
                                                    });
                                                    $scope.$apply();
                                                }
                                                else {
                                                    bootbox.alert('<span class="text-danger">Error, can not mark invoice as exported to exact online</span>');
                                                }
                                            }
                                            , function (error) {
                                                bootbox.alert('<span class="text-danger">Error, can not mark invoice as exported to exact online</span>');
                                            });
                                    }
                                }
                            });
                        }
                    }
                    , function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
            else {
                bootbox.alert("Select an invoice that has not been exported before export exact online file");
            }
        }

        function resetExportExact($event) {
            $event.preventDefault();
            var iDs = [];
            angular.forEach($scope.data, function (item) {
                if (item.isSelected && item.isExpotedToExact) {
                    iDs.push(item.purchasingInvoiceID);
                }
            });
            if (iDs.length > 0) {
                purchasingInvoiceService.resetExportExact(
                    iDs
                    , function (data) {
                        if (data) {
                            $scope.search();
                        }
                    }
                    , function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                bootbox.alert("Select an invoice that has been exported before reset export exact");
            }
        }

        
        //#endregion private methods

    }

    //#region support
    jQuery('.search-filter').keypress(function (e) {
        if (e.keyCode == 13) {
            var scope = angular.element(jQuery('body')).scope();
            scope.event.search();
        }
    });

    var searchGrid = jQuery('#searchTableContent').scrollableTable(
        function (currentPage) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                scope.pageIndex = currentPage;
                purchasingInvoiceService.searchFilter.pageIndex = scope.pageIndex;
                scope.search();
            });
        },
        function (sortedBy, sortedDirection) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                purchasingInvoiceService.searchFilter.sortedDirection = sortedDirection;
                scope.pageIndex = 1;
                purchasingInvoiceService.searchFilter.pageIndex = scope.pageIndex;
                purchasingInvoiceService.searchFilter.sortedBy = sortedBy;
                scope.search();
            });
        }
    );
    //#endregion support
})();