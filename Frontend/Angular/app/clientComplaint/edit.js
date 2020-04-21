jQuery("#selectedUsersId").select2({
    closeOnSelect: true
});

function initFactoryOrderItemsGrid() {
    return jQuery('#quickSearchFactoryOrderItemsGrid').scrollableTable(
        function (currentPage) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                scope.quickSearchFactoryOrderVM.filters.pageIndex = currentPage;
                scope.quickSearchFactoryOrderVM.method.search();
            });
        },
        function (sortedBy, sortedDirection) {
            var scope = angular.element(jQuery('body')).scope();
            scope.$apply(function () {
                scope.pageIndex = 1;
                scope.quickSearchFactoryOrderVM.filters.pageIndex = 1;
                scope.quickSearchFactoryOrderVM.filters.sortedBy = sortedBy;
                scope.quickSearchFactoryOrderVM.filters.sortedDirection = sortedDirection;
                scope.quickSearchFactoryOrderVM.method.search();
            });
        }
    );
}



(function () {
    angular
        .module("tilsoftApp")
        .controller("tilsoftController", ["$scope", "dataService", ClientComplaintEditController]);

    function ClientComplaintEditController($scope, dataService) {
        //#region properties
        $scope.ui = {
            currentForm: 'main-form',
            title: ''
        };
        $scope.CONSTS = {
            statusOrderVietnam: "Put In Production",
            shipStatus: {
                shipped: "Shipped",
                notshipped: "Not Shipped",
                loaded: "Loaded"
            }
        };
    

        $scope.dataContainer = {};
        $scope.data = {};

        //$scope.data = {
        //    season: null,
        //    clientID: null,
        //    client: null,
        //    dateReceived: null,
        //    clientUD: null,
        //    clientNM: null,
        //    saleNM: null,
        //    contactPerson: null
        //};
        $scope.newId = -1;
        $scope.selectedUserIds = [];
        $scope.selectedComplaintItemsId = [];

        /////////////////////////

        $scope.event = {
            init: init,
            update: update,
            approve: approve,
            reset: reset,
            exportExcelItem: function (event, clientComplaintItemID) {
                dataService.exportExcelClientComplaintItem(clientComplaintItemID,
                       function (data) {
                           if (data.message.type === 0) {
                               var result = data.data;
                               window.location = context.reportUrl + data.data;
                               $scope.$apply();
                           }
                       },
                       function (error) {
                           jsHelper.showMessage('warning', error);
                       }
                   );
            },


            // add clientComplaintUsers
            updateSelectedUsers: function (items) {
                var selectedUsers = $('#selectedUsersId').select2('data');

                $scope.data.clientComplaintUsers = [];
                angular.forEach(selectedUsers, function (item) {
                    $scope.data.clientComplaintUsers.push({
                        clientComplaintUserID: $scope.method.getNewId(),
                        clientComplaintID: context.id,
                        employeeID: item.id,
                        employeeNM: item.text
                    });
                });
                this.hideAddUserDialog();
            },
            showAddUserDialog: function () {
                jQuery('#addUserDialog').modal('show');
            },
            hideAddUserDialog: function() {
                jQuery('#addUserDialog').modal('hide');
            },
            removeUser: function (event, index) {
                var selectedUsers = $('#selectedUsersId').select2('data');

                selectedUsers.splice(index, 1);
                $('#selectedUsersId').select2('data', selectedUsers);

                $scope.data.clientComplaintUsers.splice(index, 1);
                $scope.selectedUserIds.splice(index, 1);
            },
            // add clientComplaintItems
            approveItem: function (event, index) {
                var currentItem = $scope.data.clientComplaintItems[index];

                currentItem.approvedBy = context.userId;
                currentItem.approverFullname = context.userFullname;
            },
            removeItem: function (event, index) {
                $scope.data.clientComplaintItems.splice(index, 1);
            },
            editItem: function($event, clientComplaintItemId) {
                $event.preventDefault();

                toggleFormRow(clientComplaintItemId);

                function toggleFormRow(clientComplaintItemId) {
                    var rowIsCollapsed = $("#icon-edit-" + clientComplaintItemId).hasClass('fa-pencil'),
                        rowEle = $("#" + clientComplaintItemId);
                    
                    if (rowIsCollapsed) {
                        hideAllFormRows();
                        turnOffAllEditIcons();
                        registerDatePickers();
                    }

                    rowEle.toggle();
                    toggleEditIcon(clientComplaintItemId);

                    function toggleEditIcon(clientComplaintItemId) {
                        var editIcon = $("#icon-edit-" + clientComplaintItemId),
                            isCollapsed = editIcon.hasClass('fa-pencil');

                        if (isCollapsed)
                            editIcon.removeClass('fa-pencil').addClass('fa-pencil-square-o');
                        else 
                            editIcon.removeClass('fa-pencil-square-o').addClass('fa-pencil');
                    }
                    
                    function turnOffAllEditIcons() {
                        $("i[id^='icon-edit-'")
                            .removeClass('fa-pencil-square-o')
                            .addClass('fa-pencil');
                    }

                    function hideAllFormRows() {
                        $('tr[id]').hide();
                    }
                }
            },
            
            // clientcomplaintitem communication
            addClientCommunication: function (event) {
                $scope.data.clientComplaintCommunications = $scope.data.clientComplaintCommunications || [];
      
                $scope.data.clientComplaintCommunications.push({
                    clientComplaintCommunicationID: $scope.method.getNewId(),
                    clientComplaintItemID: $scope.data.clientComplaintID,
                    messageText: "",
                    creatorID: context.userId,
                    creatorFullName: context.userFullname,
                    createdDate: moment().format('DD/MM/YYYY HH:mm:ss')
                });
            },
            removeClientCommunication: function (index) {
                $scope.data.clientComplaintCommunications.splice(index, 1);
            },

            // clientcomplaintitem communication
            addClientComplaintItemImage: function (parentItem) {
                masterUploader.multiSelect = true;
                masterUploader.imageOnly = true;
                masterUploader.callback = function () {
                    parentItem.clientComplaintItemImages = parentItem.clientComplaintItemImages || [];
                    scope.$apply(function () {
                        angular.forEach(masterUploader.selectedFiles, function (img) {
                            parentItem.clientComplaintItemImages.push({
                                clientComplaintItemImageID: $scope.method.getNewId(),
                                imageFile_HasChange: true,
                                imageFile_NewFile: img.filename,
                                thumbnailLocation: img.fileURL,
                                fileLocation: img.fileURL
                            });
                        }, null);
                    });
                };
                masterUploader.open();
            },
            removeClientComplaintItemImage: function (parentItem, $index) {
                parentItem.clientComplaintItemImages.splice($index, 1);
            },
            deleteItem: function() {
                //$event.preventDefault();
                dataService.delete(
                    context.id,
                    null,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = context.backUrl;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        };

        $scope.method = {
            refresh: refresh,
            getNewId: function () {
                $scope.newId--;
                return $scope.newId;
            },
            setUI: function () {
                $scope.ui.currentForm = 'main-form';
                $scope.ui.title = 'General Information';
                $scope.$apply();
            },
            showForm: function(formName, formTitle) {
                $scope.ui.currentForm = formName;
                $scope.ui.title = formTitle;
                //$scope.$apply();
            },
            updateShipmentStatus: updateShipmentStatus
        };

        $scope.quickSearchFactoryOrderVM = {
            data: null,
            filters: {
                filters: {
                    searchQuery: '',
                    clientId: $scope.data.clientID,
                    season: $scope.data.season
                },
                sortedBy: 'FactoryUD',
                sortedDirection: 'ASC',
                pageSize: 20,
                pageIndex: 1
            },
            totalPage: 0,

            method: {
                search: function () {
                    // get paramters for filtering
                    $scope.quickSearchFactoryOrderVM.filters.filters.clientId = $scope.data.clientID;
                    $scope.quickSearchFactoryOrderVM.filters.filters.season = $scope.data.season;
                                                                                                       
                    dataService.quickSearchFactoryOrderDetails(
                        $scope.quickSearchFactoryOrderVM.filters,
                        function(data) {
                            if (data.message.type === 0) {
                                // remove seleted items out off the result
                                $scope.quickSearchFactoryOrderVM.data = $scope.quickSearchFactoryOrderVM.method.removeItems(data.data.data, $scope.data.clientComplaintItems);
                                var totalRows = data.data.totalRows;

                                $scope.quickSearchFactoryOrderVM.data.forEach(function (item) {
                                    if (item.isShipped)
                                        item.statusShipment = $scope.CONSTS.shipStatus.shipped;
                                    else if (item.isLoaded)
                                        item.statusShipment = $scope.CONSTS.shipStatus.loaded;
                                    else item.statusShipment = "";
                                });

                                $scope.quickSearchFactoryOrderVM.totalPage = Math.ceil(totalRows / $scope.quickSearchFactoryOrderVM.filters.pageSize);

                                var quickSearchFactoryOrderItemsGrid = initFactoryOrderItemsGrid();

                                // hide paging nav items
                                if (data.totalRows < $scope.quickSearchFactoryOrderVM.filters.pageSize) {
                                    jQuery('#quickSearchFactoryOrderItemsGrid').find('ul').hide();
                                }
                                else {
                                    jQuery('#quickSearchFactoryOrderItemsGrid').find('ul').show();
                                }

                                // update scrollbar
                                quickSearchFactoryOrderItemsGrid.updateLayout();
                                $scope.$apply();
                            }
                        },
                        function(error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                },
                removeItems: function (sourceList, substractList) {
                    // gate keeper checking
                    var results =[];
                    if (sourceList === null ||
                        typeof (sourceList) === "undefined") return results;
                    else if (substractList === null ||
                            typeof (substractList) === "undefined") return sourceList;

                    // filter results
                    sourceList.forEach(function (item) {
                        var foundItem = _.find(substractList, { 'factoryOrderDetailID': item.factoryOrderDetailID });

                        if (!foundItem) {
                            results.push(item);
                        }
                    });

                    return results;
                }
            },
            event: {
                openQuickSearchForm: function($event) {
                    $event.preventDefault();
                    $scope.method.showForm('quicksearch-factory-form', 'Select factory order items to complain');
                    //$scope.quickSearchFactoryOrderItemsGrid = initFactoryOrderItemsGrid();
                },
                search: function($event) {
                    $event.preventDefault();
                    $scope.quickSearchFactoryOrderVM.filters.pageIndex = 1;
                    $scope.quickSearchFactoryOrderVM.method.search();
                },
                closeQuickSearchForm: function($event) {
                    $event.preventDefault();
                    $scope.method.showForm('main-form', '');

                    // reset search data
                    $scope.quickSearchFactoryOrderVM.data = null;
                },
                itemSelected: function ($event) {
                    var selectedItems = _.filter($scope.quickSearchFactoryOrderVM.data, { 'isSelected': true });
                    $scope.data.clientComplaintItems = $scope.data.clientComplaintItems || [];

                    angular.forEach(selectedItems, function (item) {
                        $scope.data.clientComplaintItems.push({
                            clientComplaintItemID: $scope.method.getNewId(),
                            factoryOrderDetailID: item.factoryOrderDetailID,
                            loadingPlanDetailID: item.loadingPlanDetailID,
                            clientComplaintID: $scope.data.clientComplaintID,
                            approvedBy: null,
                            /* for displayings */
                            statusShipment: item.statusShipment,
                            articleCode: item.articleCode,
                            proformaInvoiceNo: item.proformaInvoiceNo,
                            factoryOrderDetailDescription: item.description,
                            factoryUD: item.factoryUd,
                            factoryOrderUD: item.factoryOrderUD,
                            lds: item.lds,
                            originalSellingPrice: item.originalSellingPrice,
                            totalSaleValue: item.totalSaleValue
                        });
                    });

                    $scope.quickSearchFactoryOrderVM.event.closeQuickSearchForm($event);
                },
                canSelect: function () {
                    return _.some($scope.quickSearchFactoryOrderVM.data, { 'isSelected': true });
                }
            }
        };

        //quick seach client form
        var quickSearchPIsGrid = jQuery('#quickSearchPIsTable').scrollableTable(
            function (currentPage) {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    scope.quickSearchPIsVM.filters.pageIndex = currentPage;
                    scope.quickSearchPIsVM.method.searchPIs();
                });
            },
            function (sortedBy, sortedDirection) {
                //do nothing
            }
        );
        var quickSearchPIsGrid2 = jQuery('#quickSearchPIsTable2').scrollableTable(
            function (currentPage) {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    scope.quickSearchPIsVM.filters.pageIndex = currentPage;
                    scope.quickSearchPIsVM.method.searchPIs();
                });
            },
            function (sortedBy, sortedDirection) {
                //do nothing
            }
        );
        
        $scope.quickSearchGrid = {};
        $scope.quickSearchGrid['PINumber-popup2'] = quickSearchPIsGrid2;
        $scope.quickSearchGrid['PINumber-popup'] = quickSearchPIsGrid;
        quickSearchPIsTimer = null;

        $scope.quickSearchPIsVM = {
            data: null,
            popupElementId: '',
            filters: {
                filters: {
                    searchQuery: '',
                    clientId: context.clientId
                },
                sortedBy: 'lds',
                sortedDirection: 'DESC',
                pageSize: 10,
                pageIndex: 1
            },
            totalPage: 0,

            method: {
                searchPIs: function () {
                    dataService.quickSearchPIs(
                        $scope.quickSearchPIsVM.filters,
                        function(data) {
                            if (data.message.type === 0) {
                                $scope.quickSearchPIsVM.data = data.data.data;
                                
                                $scope.quickSearchPIsVM.totalPage = Math.ceil(data.totalRows / $scope.quickSearchPIsVM.filters.pageSize);
                                $scope.quickSearchGrid[$scope.quickSearchPIsVM.popupElementId].updateLayout();
                                $scope.$apply();

                                jQuery('#' + $scope.quickSearchPIsVM.popupElementId).show();
                            }
                        },
                        function(error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                },
                getShipmentStatus: function (proformaInvoice, clientID) {
                    dataService.getShipmentStatusOfPISolution({
                             filters: {
                                 proformaInvoice: proformaInvoice,
                                 clientID: clientID
                             }
                         },
                        function (data) {
                            
                            if (data.message.type === 0) {
                                var result = data.data;
                                if (result.isShipped) {
                                    $scope.data.statusShipment = $scope.CONSTS.shipStatus.shipped;
                                } else {
                                    $scope.data.statusShipment = $scope.CONSTS.shipStatus.notshipped;
                                }
                                //debugger;
                                // clear text order status VN
                                if ($scope.data.statusShipment !== null && $scope.data.statusShipment !== '') {
                                    $scope.data.statusOrderVietnam = '';
                                }

                                $scope.$apply();
                            }
                        },
                        function (error) {
                            
                            jsHelper.showMessage('warning', error);
                        }
                    );
                }
                
            },

            event: {
                searchBoxKeyUp: function ($event, popupElementId) {
                    var inputValue = $event.target.value;
                    $scope.quickSearchPIsVM.popupElementId = popupElementId;

                    if (inputValue.length >= 3) {
                        clearTimeout(quickSearchPIsTimer);
                        quickSearchPIsTimer = setTimeout(
                            function () {
                                $scope.quickSearchPIsVM.filters.filters.searchQuery = inputValue;
                                $scope.quickSearchPIsVM.filters.pageIndex = 1;
                                $scope.quickSearchPIsVM.method.searchPIs();
                            },
                            200);
                    }
                },
                close: function ($event, clearSearchBox) {
                    jQuery('#' + $scope.quickSearchPIsVM.popupElementId).hide();
                    if (clearSearchBox) {
                        $scope.dataContainer.createdPINumberSolution = null;
                    }
                    $scope.quickSearchPIsVM.data = null;
                    $event.preventDefault();
                },
                ok: function ($event, pi) {
                    $scope.dataContainer.createdPINumberSolution = pi.proformaInvoiceNo;
                    $scope.data.createdPINumberSolution = pi.proformaInvoiceNo;

                    if ($scope.data.createdPINumberSolution) {
                        $scope.data.statusOrderVietnam = $scope.CONSTS.statusOrderVietnam;
                    } else {
                        $scope.data.statusOrderVietnam = "";
                    }

                    $scope.quickSearchPIsVM.method.getShipmentStatus(pi.proformaInvoiceNo, $scope.data.clientID);
                    $scope.quickSearchPIsVM.event.close($event, false);
                    
                }
            }
        };
        //#endregion properties

        //#region private methods

        function init() {
            
            initStates();

            dataService.load(
                context.id,
                context.clientId,
                context.season,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.employees = data.data.employees;
                    $scope.complaintTypes = data.data.complaintTypes;
                    $scope.complaintStatuses = data.data.complaintStatuses;
                    //debugger;
                    $scope.method.updateShipmentStatus();
                   
                    var selectedUsers = [];
                    angular.forEach($scope.data.clientComplaintUsers, function(item) {
                        selectedUsers.push({ "id": item.employeeID, "text": item.employeeNM });
                        $scope.selectedUserIds.push(item.employeeID);
                    });
                    $('#selectedUsersId').select2('data', selectedUsers);


                    $scope.$apply();
                    jQuery('#content').show();
                    registerWatches();                    

                    function registerWatches() {
                        $scope.$watch('data', function () {
                            jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                        });
                    }
                },
                function (error) {
                    
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        }

        function update($event) {
            $event.preventDefault();

            if ($scope.editForm.$valid) {
                dataService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            var newId = data.data.data.clientComplaintID;
                            var createdNew = context.id === 0 && newId > 0;

                            if (createdNew) {
                                var url = window.location.href || "";
                                context.id = newId;
                                url = url.replace("Edit/0?", "Edit/" + newId + "?");
                                window.location = url;
                                return;
                            }
                            jQuery('#changeNotification').html('');
                            location.reload();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            } else {
                jsHelper.showMessage('warning', context.errMsg);
            }
        }

        function approve($event) {
            $event.preventDefault();

            if (confirm('Client complaint can not be edited after confirm, do you want to continue?')) {
                dataService.approve(
                    context.id,
                    $scope.data,
                    function (data) {
                        if (data.message.type === 0) {
                            jsHelper.loadingSwitch(true);
                            window.location.reload(false);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', context.error);
                    }
                );
            }
           
        }

        function reset($event) {
            $event.preventDefault();

            if (confirm('Reset the complaint approval status?')) {
                dataService.reset(
                    context.id,
                    function (data) {
                        console.log(data);
                        if (data.message.type === 0) {
                            jsHelper.loadingSwitch(true);
                            window.location.reload(false);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', context.error);
                    }
                );
            }
        }

        function refresh(id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        }

        function updateShipmentStatus($event) {
            if (!$scope.data.createdPINumberSolution) {
                $scope.data.statusOrderVietnam = $scope.CONSTS.statusOrderVietnam;
            } else {
                $scope.data.statusOrderVietnam = "";
            }
            if ($scope.data.createdPINumberSolution !== null && $scope.data.createdPINumberSolution !== '') {
                $scope.quickSearchPIsVM.method.getShipmentStatus($scope.data.createdPINumberSolution, $scope.data.clientID);
            }
        }

        function initStates() {
            dataService.searchFilter.pageSize = context.pageSize;
            dataService.serviceUrl = context.serviceUrl;
            dataService.supportServiceUrl = context.supportServiceUrl;
            dataService.token = context.token;
        }

        // setup date time picker
        function registerDatePickers() {
            $('.datepicker').each(function () {

                var $this = $(this),
                    dataDateFormat = $this.attr('data-dateformat') || 'dd.mm.yy';

                $this.removeClass('hasDatepicker').datepicker({
                    dateFormat: dataDateFormat,
                    prevText: '<i class="fa fa-chevron-left"></i>',
                    nextText: '<i class="fa fa-chevron-right"></i>'
                });

                //clear memory reference
                $this = null;
            });

        }


        //#endregion private methods

        $('#txtPINumber').change(function () {
            if ($('#txtPINumber').val() === null || $('#txtPINumber').val() === '') {
                $scope.data.statusOrderVietnam = $scope.CONSTS.statusOrderVietnam;
                $scope.data.statusShipment = '';
                $scope.dataContainer.createdPINumberSolution = null;
                $scope.data.createdPINumberSolution = null;
            }
        });

        //#region init

        $scope.event.init();
        
        //#endregion init
    }
})();