var grdFactory = jQuery('#grdFactory').scrollableTable(null, null);

var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.deliveryTerms = null;
    $scope.paymentTerms = null;
    $scope.manufacturerCountries = null;
    $scope.companies = null;
    $scope.getID = -1;
    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.deliveryTerms = data.data.deliveryTerms;
                    $scope.paymentTerms = data.data.paymentTerms;
                    $scope.manufacturerCountries = data.data.manufacturerCountries;
                    $scope.companies = data.data.companies;
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                    grdFactory.updateLayout();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.deliveryTerms = null;
                    $scope.paymentTerms = null;
                    $scope.manufacturerCountries = null;
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
                            $scope.method.refresh(data.data.supplierID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', '@Frontend.Properties.Resources.INPUT_VALIDATION_FAILED');
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
                            window.location = '@Url.Action("Index", "Supplier")';
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },

        addBankAccount: function () {
            var item = {
                supplierBankID: $scope.method.getnewID(),
                bankNM: '',
                accountNM: '',
                bankCode: '',
                remark: ''
            };
            $scope.data.supplierBanks.push(item);
        },

        removeBankAccount: function (item) {
            var index = $scope.data.supplierBanks.indexOf(item);
            $scope.data.supplierBanks.splice(index, 1);
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = id;
        },
        getnewID: function () {
            $scope.getID--;
            return $scope.getID;
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);