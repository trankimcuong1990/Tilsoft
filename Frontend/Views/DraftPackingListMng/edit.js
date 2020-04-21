(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', draftPackingListController);
    draftPackingListController.$inject = ['$scope', 'dataService', '$timeout'];

    function draftPackingListController($scope, dataService, $timeout) {
        $scope.data = [];
        $scope.data.draftPackingListData = [];
        jQuery('#content').show();

        $scope.event = {
            init: init,
            load: load,
            update: update,
            getDraftPackingListPrintOut: getDraftPackingListPrintOut,
            remove: remove
        };

        function init() {
            $scope.event.load();
        };

        function load() {
            dataService.serviceUrl = context.serviceUrl;
            dataService.supportServiceUrl = context.supportServiceUrl;
            dataService.token = context.token;
            var param = context.pID;
            dataService.load(
                context.id,
                param,
                function (data) {
                    $scope.data = data.data;
                    $scope.data.draftPackingListData = data.data.draftPackingListData;
                    $scope.data.draftPackingListData.draftPackingListUD = $scope.data.draftPackingListData.invoiceNo;
                    if ($scope.data.draftPackingListData.packingListDate === null) {
                        var today = new Date();
                        var dd = today.getDate();
                        var mm = today.getMonth() + 1; //January is 0!

                        var yyyy = today.getFullYear();
                        if (dd < 10) {
                            dd = '0' + dd;
                        }
                        if (mm < 10) {
                            mm = '0' + mm;
                        }
                        var today = dd + '/' + mm + '/' + yyyy;
                        $scope.data.draftPackingListData.packingListDate = today;
                    }
                    //show to page
                    jQuery('#content').show();
                    $scope.$watch('data', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    $scope.data = null;
                }
            );
        };

        function update($event) {
            $event.preventDefault();
            if ($scope.editForm.$valid) {
                if (context.pID > 0) {
                    for (var i = 0; i < $scope.data.draftPackingListData.draftPackingListDetail.length; i++) {
                        $scope.data.draftPackingListData.draftPackingListDetail[i].draftPackingListDetailID = -1;
                    }
                    for (var j = 0; j < $scope.data.draftPackingListData.draftPackingListSparepartDetail.length; j++) {
                        $scope.data.draftPackingListData.draftPackingListSparepartDetail[j].draftPackingListDetailID = -1;
                    }
                    $scope.data.draftPackingListData.draftPackingListID = -1;
                }
                dataService.update(context.id, $scope.data.draftPackingListData,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            //window.location = '@Url.Action("Edit", "DraftPackingList", new { id = UrlParameter.Optional })/' + data.data.packingListID;
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
        };

        function remove(item) {
            dataService.delete(
                item,
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
        };

        function getDraftPackingListPrintOut(id) {
            dataService.getDraftPackingListPrintOut(
                id,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type !== 2) {
                        window.open(context.backendReportUrl + data.data, '');
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };


        //
        // method
        //
        $scope.method = {
            calTotalCBMs: calTotalCBMs,
            calTotalKGs: calTotalKGs,
            calTotalNettWeight: calTotalNettWeight,
            calTotalPKGs: calTotalPKGs,
            calTotalQuantity: calTotalQuantity,
            calTotalQuantitySparepart: calTotalQuantitySparepart,
            calTotalPKGsSparepart: calTotalPKGsSparepart,
            calTotalNettWeightSparepart: calTotalNettWeightSparepart,
            calTotalKGsSparepart: calTotalKGsSparepart,
            calTotalCBMsSparepart: calTotalCBMsSparepart,
        };


        //Calculate Products
        function calTotalQuantity() {
            if ($scope.data === null) return;
            var total = 0;
            angular.forEach($scope.data.draftPackingListData.draftPackingListDetail, function (item) {
                if (item.quantity !== null && !isNaN(parseInt(item.quantity))) {
                    total = total + parseInt(item.quantity);
                }
            })
            return total;
        };

        function calTotalPKGs() {
            if ($scope.data === null) return;
            var total = 0;
            angular.forEach($scope.data.draftPackingListData.draftPackingListDetail, function (item) {
                if (item.pkGs !== null && !isNaN(parseInt(item.pkGs))) {
                    total = total + parseInt(item.pkGs);
                }
            })
            return total;
        };

        function calTotalNettWeight() {
            if ($scope.data === null) return;
            var total = 0;
            angular.forEach($scope.data.draftPackingListData.draftPackingListDetail, function (item) {
                if (item.nettWeight !== null && !isNaN(parseFloat(item.nettWeight))) {
                    total = total + parseFloat(item.nettWeight);
                }
            })
            return total;
        };

        function calTotalKGs() {
            if ($scope.data === null) return;
            var total = 0;
            angular.forEach($scope.data.draftPackingListData.draftPackingListDetail, function (item) {
                if (item.kGs !== null && !isNaN(parseFloat(item.kGs))) {
                    total = total + parseFloat(item.kGs);
                }
            })
            return total;
        };

        function calTotalCBMs() {
            if ($scope.data === null) return;
            var total = 0;
            angular.forEach($scope.data.draftPackingListData.draftPackingListDetail, function (item) {
                if (item.cbMs !== null && !isNaN(parseFloat(item.cbMs))) {
                    total = total + parseFloat(item.cbMs);
                }
            })
            return total;
        };

        //Calculate Sparepart
        function calTotalQuantitySparepart() {
            if ($scope.data === null) return;
            var total = 0;
            angular.forEach($scope.data.draftPackingListData.draftPackingListSparepartDetail, function (item) {
                if (item.quantity !== null && !isNaN(parseInt(item.quantity))) {
                    total = total + parseInt(item.quantity);
                }
            })
            return total;
        };

        function calTotalPKGsSparepart() {
            if ($scope.data === null) return;
            var total = 0;
            angular.forEach($scope.data.draftPackingListData.draftPackingListSparepartDetail, function (item) {
                if (item.pkGs !== null && !isNaN(parseInt(item.pkGs))) {
                    total = total + parseInt(item.pkGs);
                }
            })
            return total;
        };

        function calTotalNettWeightSparepart() {
            if ($scope.data === null) return;
            var total = 0;
            angular.forEach($scope.data.draftPackingListData.draftPackingListSparepartDetail, function (item) {
                if (item.nettWeight !== null && !isNaN(parseFloat(item.nettWeight))) {
                    total = total + parseFloat(item.nettWeight);
                }
            })
            return total;
        };

        function calTotalKGsSparepart() {
            if ($scope.data === null) return;
            var total = 0;
            angular.forEach($scope.data.draftPackingListData.draftPackingListSparepartDetail, function (item) {
                if (item.kGs !== null && !isNaN(parseFloat(item.kGs))) {
                    total = total + parseFloat(item.kGs);
                }
            })
            return total;
        };

        function calTotalCBMsSparepart() {
            if ($scope.data === null) return;
            var total = 0;
            angular.forEach($scope.data.draftPackingListData.draftPackingListSparepartDetail, function (item) {
                if (item.cbMs !== null && !isNaN(parseFloat(item.cbMs))) {
                    total = total + parseFloat(item.cbMs);
                }
            })
            return total;
        };

        $scope.event.load();
    }
})();