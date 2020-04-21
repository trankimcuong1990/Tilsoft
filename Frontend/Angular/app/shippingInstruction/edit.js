//
// SUPPORT
//
jQuery('#main-form').find('input').keypress(function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});
jQuery('#main-form').find('select').keypress(function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.clients = null;
    $scope.poDs = null;
    $scope.countries = null;
    $scope.forwarders = null;
    $scope.notifyTypes = null;
    $scope.consigneeTypes = null;

    $scope.otherAddress = '[Client Name]\n[Address]\n[Post Code]\n[Country]';
    $scope.eurofarAddress = 'EUROFAR INTERNATIONAL B.V.\nBEELAERTS VAN BLOKLANDSTRAAT 14\n5042 PM TILBURG\nTHE NETHERLANDS';

    $scope.newID = -1000;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.clients = data.data.clients;
                    $scope.poDs = data.data.poDs;
                    $scope.countries = data.data.countries;
                    $scope.forwarders = data.data.forwarders;
                    $scope.notifyTypes = data.data.notifyTypes;
                    $scope.consigneeTypes = data.data.consigneeTypes;
                    $scope.$apply();
                    jQuery('#content').show();

                    //select 2
                    $("#podBox").select2();
                    $("#countryBox").select2();
                    $("#forwarderBox").select2();
                    $("#notifyTypeBox").select2();
                    $("#consigneeTypeBox").select2();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.clients = null;
                    $scope.poDs = null;
                    $scope.countries = null;
                    $scope.forwarders = null;
                    $scope.notifyTypes = null;
                    $scope.consigneeTypes = null;
                    $scope.packagingMethodOptions = null;
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
                            $scope.method.refresh(data.data.shippingInstructionID);
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

            if (confirm('Are you sure ?')) {
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
        approve: function ($event) {
            $event.preventDefault();
            if (confirm('Approve the current shipping instruction ?')) {
                jsonService.approve(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.shippingInstructionID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        reset: function ($event) {
            $event.preventDefault();
            if (confirm('Reset the current shipping instruction to pending status?')) {
                jsonService.reset(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.shippingInstructionID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        onNotifyTypeChange: function () {
            if ($scope.data.notifyTypeID == null || typeof $scope.data.notifyTypeID == 'undefined' || $scope.data.notifyTypeID == '') {
                return;
            }

            switch ($scope.data.notifyTypeID)
            {
                case 1: // client address
                    if ($scope.data.clientID == null || typeof $scope.data.clientID == 'undefined' || $scope.data.clientID == '') {
                        $scope.data.notifyInfo = '';
                    }
                    else {
                        angular.forEach($scope.clients, function (value, key) {
                            if (this.clientID == value.clientID) {
                                this.notifyInfo = value.clientNM + "\n" + value.clientAddress;
                            }
                        }, $scope.data);
                    }

                    break;

                case 2: // eurofar address
                    $scope.data.notifyInfo = $scope.eurofarAddress;
                    break;

                case 3: // other
                    $scope.data.notifyInfo = $scope.otherAddress;
                    break;
            }
        },
        onForwarderChange: function () {
            if ($scope.data.forwarderID == null || typeof $scope.data.forwarderID == 'undefined' || $scope.data.forwarderID == '') {
                return;
            }

            angular.forEach($scope.forwarders, function (value, key) {
                if (this.forwarderID == value.forwarderID) {
                    this.forwarderInfo = value.forwarderNM + "\n" + value.address;
                }
            }, $scope.data);
        },
        onConsigneeTypeChange: function () {
            if ($scope.data.consigneeTypeID == null || typeof $scope.data.consigneeTypeID == 'undefined' || $scope.data.consigneeTypeID == '') {
                return;
            }

            switch ($scope.data.consigneeTypeID) {
                case 1: // to order
                    $scope.data.consigneeInfo = 'TO ORDER';
                    break;

                case 2: // client address
                    if ($scope.data.clientID == null || typeof $scope.data.clientID == 'undefined' || $scope.data.clientID == '') {
                        $scope.data.consigneeInfo = '';
                    }
                    else {
                        angular.forEach($scope.clients, function (value, key) {
                            if (this.clientID == value.clientID) {
                                this.consigneeInfo = value.clientNM + "\n" + value.clientAddress;
                            }
                        }, $scope.data);
                    }

                    break;

                case 3: // eurofar address
                    $scope.data.consigneeInfo = $scope.eurofarAddress;
                    break;

                case 4: // other
                    $scope.data.consigneeInfo = $scope.otherAddress;
                    break;
            }
        },
        getDefault: function () {
            jsonService.getDefault(
                $scope.data.clientID,
                function (data) {
                    if (data.data && data.data.shippingInstructionID) {
                        $scope.data.podid = data.data.podid;
                        $scope.data.podRemark = data.data.podRemark;
                        $scope.data.countryID = data.data.countryID;
                        $scope.data.forwarderID = data.data.forwarderID;
                        $scope.data.forwarderInfo = data.data.forwarderInfo;
                        $scope.data.notifyTypeID = data.data.notifyTypeID;
                        $scope.data.notifyInfo = data.data.notifyInfo;
                        $scope.data.consigneeTypeID = data.data.consigneeTypeID;
                        $scope.data.consigneeInfo = data.data.consigneeInfo;
                        $scope.data.podName = data.data.podName;
                        $scope.data.clientCountryNM = data.data.clientCountryNM;
                        $scope.data.clientAddress = data.data.clientAddress;
                        $scope.data.forwarderNM = data.data.forwarderNM;
                        $scope.data.notifyTypeNM = data.data.notifyTypeNM;

                        $scope.$apply();
                    }
                    else {
                        alert('Default shipping instruction for this client not found !');
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        getCountryFromPOD: function (podID) {
            jsonService.getCountryFromPOD(
                podID,
                function (data) {
                    $scope.data.countryID = data.data.clientCountryID;
                    $scope.data.clientCountryNM = data.data.clientCountryNM;
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            window.location = context.refreshUrl + id;
        },
        getNewID: function () {
            $scope.newID--;
            return $scope.newID;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);