var tilsoftApp = angular.module('tilsoftApp', []);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.getReportData(
                context.season,
                function (data) {
                    $scope.data = data.data;

                    $scope.$apply();
                    jQuery('#content').show();
                },
                function (error) {
                    $scope.data = null;
                    $scope.$apply();
                }
            );
        }
    }

    //
    // method
    //
    $scope.method = {
        // all material commercial invoice
        getAllMaterialTotalAmount: function () {
            var total = 0;
            if ($scope.data != null && $scope.data.allMaterials != null) {
                angular.forEach($scope.data.allMaterials, function (value, key) {
                    total += value.totalAmount;
                }, null);
            }
            return total;
        },
        getAllMaterialTotalCont: function () {
            var total = 0;
            if ($scope.data != null && $scope.data.allMaterials != null) {
                angular.forEach($scope.data.allMaterials, function (value, key) {
                    total += value.totalCont;
                }, null);
            }
            return total;
        },
        getAllMaterialPercent: function (item) {
            return item.totalAmount * 100 / $scope.method.getAllMaterialTotalAmount();
        },

        // wood material commercial invoice
        getWoodTotalAmount: function () {
            var total = 0;
            if ($scope.data != null && $scope.data.woods != null) {
                angular.forEach($scope.data.woods, function (value, key) {
                    total += value.totalAmount;
                }, null);
            }
            return total;
        },
        getWoodTotalCont: function () {
            var total = 0;
            if ($scope.data != null && $scope.data.woods != null) {
                angular.forEach($scope.data.woods, function (value, key) {
                    total += value.totalCont;
                }, null);
            }
            return total;
        },
        getWoodPercent: function (item) {
            return item.totalAmount * 100 / $scope.method.getWoodTotalAmount();s
        },

        // all material proforma invoice
        getAllMaterialTotalAmount_ProformaInvoice: function () {
            var total = 0;
            if ($scope.data != null && $scope.data.proformaInvoiceAllMaterials != null) {
                angular.forEach($scope.data.proformaInvoiceAllMaterials, function (value, key) {
                    total += value.totalAmount;
                }, null);
            }
            return total;
        },
        getAllMaterialTotalCont_ProformaInvoice: function () {
            var total = 0;
            if ($scope.data != null && $scope.data.proformaInvoiceAllMaterials != null) {
                angular.forEach($scope.data.proformaInvoiceAllMaterials, function (value, key) {
                    total += value.totalCont;
                }, null);
            }
            return total;
        },
        getAllMaterialPercent_ProformaInvoice: function (item) {
            return item.totalAmount * 100 / $scope.method.getAllMaterialTotalAmount_ProformaInvoice();
        },

        // wood material commercial invoice
        getWoodTotalAmount_ProformaInvoice: function () {
            var total = 0;
            if ($scope.data != null && $scope.data.proformaInvoiceWoods != null) {
                angular.forEach($scope.data.proformaInvoiceWoods, function (value, key) {
                    total += value.totalAmount;
                }, null);
            }
            return total;
        },
        getWoodTotalCont_ProformaInvoice: function () {
            var total = 0;
            if ($scope.data != null && $scope.data.proformaInvoiceWoods != null) {
                angular.forEach($scope.data.proformaInvoiceWoods, function (value, key) {
                    total += value.totalCont;
                }, null);
            }
            return total;
        },
        getWoodPercent_ProformaInvoice: function (item) {
            return item.totalAmount * 100 / $scope.method.getWoodTotalAmount_ProformaInvoice(); s
        },

    }


    //
    // init
    //
    $scope.event.init();
}]);