var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', function ($scope, $timeout) {
    jsonService.searchFilter.pageSize = context.pageSize;
    jsonService.serviceUrl = context.serviceUrl;
    jsonService.token = context.token;
    jsonService.supportServiceUrl = context.supportServiceUrl
    //
    // property
    //
    $scope.data = null;
    $scope.paymentTypes = null;
    $scope.paymentMethods = null;
    $scope.activeStatuses = null;
    /// event
    $scope.event = {

        init: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.paymentTypes = data.data.paymentTypes;
                    $scope.paymentMethods = data.data.paymentMethods;
                    $scope.activeStatuses = data.data.activeStatuses;
                    $scope.data = data.data.data;
                    console.log($scope.data);
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.paymentTypes = null;
                    $scope.paymentMethods = null;
                    $scope.$apply();
                }
            )
        },
        /// update
        update: function ($event) {
            $event.preventDefault();
            if (jQuery('#paymentTermNM').val() === null || jQuery('#paymentTermNM').val() === '') {
                jsHelper.showMessage('warning', 'Please input Name.');
                return false;
            }
            if ($scope.data.paymentMethod !== null && $scope.data.paymentMethod !== '' && $scope.data.paymentMethod !== 'LC') {
                if ($scope.data.paymentMethod === 'DP-PERCENT' && $scope.data.downValue === null) {
                    if (jQuery('#downValue').val() === null || jQuery('#downValue').val() === '') {
                        jsHelper.showMessage('warning', 'Please input DownValue.');
                        return false;
                    }
                }               
            }
            if ($scope.data.paymentMethod !== 'DP-PERCENT') {
                $scope.data.downValue = null;
            }
            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.paymentTermID);
                            
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', context.validationErrMsg);
            }
        },

        ///
        onOptionChange: function (checkChange) {
            var optionCode = '';
            var optionName = '';

            // payment type
            if ($scope.data.paymentTypeID > 0) {
                for (j = 0; j < $scope.paymentTypes.length; j++) {
                    if ($scope.paymentTypes[j].paymentTypeID == $scope.data.paymentTypeID) {
                        optionCode += $scope.paymentTypes[j].paymentTypeID;
                        optionName += $scope.paymentTypes[j].paymentTypeNM;
                        break;
                    }
                }
            }

            // payment method
            if ($scope.data.paymentMethod !== '' || $scope.data.paymentMethod !== null) {
                for (j = 0; j < $scope.paymentMethods.length; j++) {
                    if ($scope.paymentMethods[j].paymentMethodID == $scope.data.paymentMethodID) {
                        optionCode += $scope.paymentMethods[j].paymentMethodID;
                        optionName += ' ' + $scope.paymentMethods[j].paymentMethodNM;

                        break;
                    }
                }
            }

            $scope.data.paymentTermID = optionCode;
            $scope.data.paymentTermNM = optionName;  
            if ($scope.data.paymentTypeID === 4) {
                $scope.data.paymentMethod = 'LC';
            }
            if ($scope.data.paymentTypeID !== 4) {
                if (checkChange !== 1) {
                    $scope.data.paymentMethod = '';
                }              
            }
        },


    };
    //
    // method
    //
    $scope.method = {       
        refresh: function (id) {
            jsHelper.loadingSwitch(true);         
            //window.location = '@Url.Action("Edit", "PaymentTerm", new { id = UrlParameter.Optional })/' + id;
            window.location = context.refreshUrl + id;
        }
    };

    //
    // init
    //
    $scope.event.init();

}]);