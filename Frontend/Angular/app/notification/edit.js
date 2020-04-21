var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', function ($scope, $timeout) {
    //
    // property
    //
    $scope.newID = -1;
    $scope.newEmployeeDetailID = -1;
    $scope.newNotificationGroupStatusID = -1;
    $scope.userId = null;
    $scope.moduleStatusData = [];
    $scope.enableAddUser = false;
    $scope.modules = [];
    $scope.filters = {
        SearchUser: '',
        email:''
    };

    //
    // event
    //
    $scope.event = {
        load: function () {
            //debugger;
            jsonService.load(
                context.id,
                function (data) {
                    //debugger;
                    $scope.data = data.data.data;
                    console.log($scope.data);
                    $scope.modules = data.data.modules;
                    if (context.id > 0 && $scope.data.moduleID !== null) {
                        $scope.event.loadModuleStatus($scope.data.moduleID);
                    }
                    $scope.$apply();

                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                    $('#quickSearchBoxUser').autocomplete({
                        source: function (request, response) {

                            $.ajax({
                                cache: false,
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json',
                                    'Authorization': 'Bearer ' + context.token
                                },
                                type: "POST",
                                data: JSON.stringify({
                                    filters: {
                                        searchQuery: request.term
                                    }
                                }),
                                dataType: 'json',
                                url: context.supportServiceUrl + 'getsearchuser',
                                beforeSend: function () {

                                    jsHelper.loadingSwitch(true);
                                },
                                success: function (data) {

                                    jsHelper.loadingSwitch(false);

                                    response(data.data);
                                },
                                error: function (xhr, ajaxOptions, thrownError) {

                                    jsHelper.loadingSwitch(false);
                                    errorCallBack(xhr.responseJSON.exceptionMessage);
                                }
                            });
                        },
                        minLength: 3,
                        select: function (event, ui) {
                            $scope.userId = ui.item.id;
                            $scope.filters.SearchUser = ui.item.label;
                            $scope.filters.email = ui.item.employeeEmail;
                            $scope.enableAddUser = true;

                            $scope.$apply();
                        }
                    });


                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.data = null;
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
                        if (data.message.type === 0) {
                            $scope.method.refresh(data.data.notificationGroupID);
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
                notificationService.delete(
                    context.id,
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
        },
        add: function () {
            if (jQuery('#quickSearchBoxUser').val() === null || jQuery('#quickSearchBoxUser').val() === '') {
                return false;
            }

            $scope.data.notificationGroupMemberData.push({
                notificationGroupMemberID: $scope.detailID--,
                userID: $scope.userId,
                userName: $scope.filters.SearchUser,
                remark: '',
                email: $scope.filters.email
            });

            $scope.enableAddUser = false;

            $scope.userId = null;
            $scope.filters.SearchUser = '';
            $scope.filters.email = '';
        },
        remove: function (item) {
            var index = $scope.data.notificationGroupMemberData.indexOf(item);
            $scope.data.notificationGroupMemberData.splice(index, 1);
        },
        changeSearchUser: function () {
            $scope.enableAddUser = false;
        },
        loadModuleStatus: function (moduleID) {
            jsonService.loadModuleStatus(
                moduleID,
                context.id,
                function (data) {
                    if (context.id !== 0) {
                        $scope.data.notificationGroupStatuses = data.data.notificationGroupStatusDTOs;
                    }
                    else {
                        $scope.data.notificationGroupStatuses = [];
                        angular.forEach(data.data.moduleStatusDTOs, function (item) {
                            var notificationGroupStatus = {
                                notificationGroupStatusID: $scope.method.getNewNotificationGroupStatusID(),
                                notificationGroupID: $scope.data.notificationGroupID,
                                moduleStatusID: item.moduleStatusID,
                                isSelected: false,
                                statusNM: item.statusNM,
                                email: item.employeeEmail,
                            };
                            if (!$scope.method.checkNotificationStatus(notificationGroupStatus.moduleStatusID)) {
                                $scope.data.notificationGroupStatuses.push(notificationGroupStatus);
                            }
                        });
                    }
                    $scope.$apply();
                },
                function (error) {
                });
        },

        openModuleStatus: function () {
            jQuery("#frmModuleStatus").modal();
            $scope.moduleStatusData.moduleStatusID = 0;
            $scope.moduleStatusData.moduleID = $scope.data.moduleID;
        },

        saveModuleStatus: function ($event) {
            if ($scope.moduleStatusData.statusUD === '' || $scope.moduleStatusData.statusUD === null || $scope.moduleStatusData.statusUD === undefined) {
                jsHelper.showMessage('warning', "Vui lòng nhập Mã Trạng Thái");
                $scope.labelText = "Vui lòng nhập Mã Trạng Thái";
                return false;
            }
            else {
                if ($scope.moduleStatusData.statusNM === '' || $scope.moduleStatusData.statusNM === null || $scope.moduleStatusData.statusNM === undefined) {
                    jsHelper.showMessage('warning', "Vui lòng nhập Tên Trạng Thái");
                    $scope.labelText = "Vui lòng nhập Tên Trạng Thái";
                    return false;
                }
                else {
                    if ($scope.moduleStatusData.moduleID === '' || $scope.moduleStatusData.moduleID === null || $scope.moduleStatusData.moduleID === undefined) {
                        jsHelper.showMessage('warning', "Vui lòng chọn phân hệ");
                        $scope.labelText = "Vui lòng nhập chọn phân hệ";
                        return false;
                    }
                    else {
                        jsonService.saveModuleStatus(
                            $scope.moduleStatusData.moduleStatusID,
                            $scope.moduleStatusData.moduleID,
                            $scope.moduleStatusData.statusUD,
                            $scope.moduleStatusData.statusNM,
                            function (data) {
                                if (data.message.type === 0) {
                                    $scope.event.loadModuleStatus($scope.data.moduleID);
                                    jQuery("#frmModuleStatus").modal("hide");
                                    $scope.labelText = null;
                                    $scope.moduleStatusData = [];
                                }
                                else {
                                    $scope.labelText = data.message.message;
                                }
                                $scope.$apply();
                                jsHelper.processMessage(data.message);
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error);
                            }
                        );
                    }
                }
            }
        },
        clearModuleStatus: function () {
            $scope.moduleStatusData = [];
        }
    },

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
        },
        getNewNotificationGroupStatusID: function () {
            return $scope.newNotificationGroupStatusID--;
        },
        checkNotificationStatus: function (moduleStatusID) {
            var isChecked = false;

            angular.forEach($scope.data.notificationGroupStatuses, function (notificationStatus) {
                if (notificationStatus.moduleStatusID === moduleStatusID) {
                    isChecked = true;
                }
            });

            return isChecked;
        }
    },




    ////quick search product
    //searchProductTimer = null;


    //$scope.quickSearchUser = {
    //    data: null,
    //    filters: {
    //        filters: {
    //            searchQuery: '',
    //        },
    //        sortedBy: 'Description',
    //        sortedDirection: 'ASC',
    //        pageSize: 20,
    //        pageIndex: 1
    //    },
    //    totalPage: 0,

    //    popupformID: 'popup-product',

    //    //searchQueryBoxID: 'quickSearchBox-user',


    //    method: {
    //        search: function () {
    //            jsonService.quickSearchUser(
    //                $scope.quickSearchProduct.filters,
    //                function (data) {
    //                    if (data.message.type === 0) {
    //                        $scope.quickSearchUser.data = data.data;
    //                        $scope.quickSearchUser.totalPage = Math.ceil(data.totalRows / $scope.quickSearchUser.filters.pageSize);
    //                        quickSearchResultGridProduct.updateLayout();
    //                        $scope.$apply();

    //                    }
    //                },
    //                function (error) {
    //                    jsHelper.showMessage('warning', error);
    //                }
    //            );
    //        }
    //    },
    //    event: {
    //        searchBoxKeyUp: function ($event) {
    //            $scope.quickSearchUser.filters.filters.searchQuery = jQuery('#quickSearchBoxUser').val();
    //            jQuery.ajax({
    //                cache: false,
    //                headers: {
    //                    'Accept': 'application/json',
    //                    'Content-Type': 'application/json',
    //                    'Authorization': 'Bearer ' + this.token
    //                },
    //                type: "POST",
    //                dataType: 'json',
    //                data: JSON.stringify(this.searchFilter),
    //                url: this.serviceUrl + 'gets',
    //                beforeSend: function () {
    //                    jsHelper.loadingSwitch(true);
    //                },
    //                success: function (data) {
    //                    jsHelper.loadingSwitch(false);
    //                    successCallBack(data);
    //                },
    //                error: function (xhr, ajaxOptions, thrownError) {
    //                    jsHelper.loadingSwitch(false);
    //                    errorCallBack(xhr.responseJSON.exceptionMessage);
    //                }
    //            });

    //        },


    //    }
    //}
    //
    // init
    //
    $scope.detailID = -1;
    $scope.notificationGroupUser = [];


    $scope.add = function () {


        //if ($scope.filters.SearchUser === null) {
        //    $scope.data.opSequenceDetails = [];
        //}

        $scope.data.notificationGroupMemberData.push({
            notificationGroupMemberID: $scope.detailID--,
            userID: $scope.userId,
            userName: $scope.filters.SearchUser,
            remark: '',
            email: $scope.filters.email
        });

        $scope.userId = null;
        $scope.filters.SearchUser = '';
        $scope.filters.email = '';
        // alert($scope.filters.SearchUser);
    };

    $scope.removeItem = function (index) {


        $scope.data.notificationGroupMemberData.splice(index, 1);
    };


    $scope.event.load();
}]);


//$('#quickSearchBoxUser').autocomplete({
//    source: function (request, response) {
//        $.ajax({
//            cache: false,
//            headers: {
//                'Accept': 'application/json',
//                'Content-Type': 'application/json',
//                'Authorization': 'Bearer ' + jsonService.token
//            },
//            type: 'POST',
//            data: JSON.stringify({ filters: { searchQuery: request.term } }),
//            dataType: 'json',
//            url: jsonService.serviceUrl + 'getsearchuser',
//            beforeSend: function () {
//            },
//            success: function (data) {
//                response(data.data);
//            },
//            error: function (xhr, ajaxOptions, thrownError) {
//                errorCallBack(xhr.responseJSON.exceptionMessage);
//            }
//        });
//    },
//    minLength: 3,
//    select: function (event, ui) {
//        $scope.filters.ClientUD = ui.item.label;
//        $scope.$apply();
//    }
//});