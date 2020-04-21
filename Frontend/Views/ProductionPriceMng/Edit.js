//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$timeout', 'dataService', function ($scope, $timeout, dataService) {

    //
    // init service
    //
    dataService.serviceUrl = context.serviceUrl;
    dataService.supportServiceUrl = context.supportServiceUrl;
    dataService.token = context.token;

    //
    // property
    //
    $scope.data = null;
    $scope.receiptByProductionItems = []; //tracking receipt for every item

    //
    // event
    //
    $scope.event = {
        init: init,
        load: load,
        update: update,
        print: print,        
        calculateAvgPrice: calculateAvgPrice,
        lockPrice: lockPrice,
        getReceiptByProductionItem: getReceiptByProductionItem,
        applyPriceOnReceipt: applyPriceOnReceipt
    }

    //
    // method
    //
    $scope.method = {
        refresh: refresh,
    };

    //
    //init form
    //
    $scope.initForm = {
        productionItemTypes: [],
        months: [],
        years:[],
        productionItemTypeID: null,
        month: null,
        year: null,
        load: function () {
            dataService.getSearchFilter(
                function (data) {
                    $scope.initForm.productionItemTypes = data.data.productionItemTypes;
                    $scope.initForm.months = data.data.months;
                    $scope.initForm.years = data.data.years;
                    jQuery('#content').show();
                    $('#frmInitForm').modal('show');
                },
                function (error) {
                    $scope.productionItemTypes = [];
                }
            );           
        },
        ok: function () {
            if ($scope.initForm.productionItemTypeID === null) {
                bootbox.alert('You have to select type');
                return;
            }
            if ($scope.initForm.month === null) {
                bootbox.alert('You have to select month');
                return;
            }
            if ($scope.initForm.year === null) {
                bootbox.alert('You have to select year');
                return;
            }             
            $('#frmInitForm').modal('hide');

            //load edit form
            $scope.event.load();
        },
    }


    //
    // init
    //
    $scope.event.init();

    //
    // implement function for event
    //
    function init() {
        if (context.id === 0) {
            $scope.initForm.load();
        }
        else {
            $scope.event.load();
        }
                     
    }

    function load() {        
        var param = {
            productionItemTypeID: $scope.initForm.productionItemTypeID,
            month: $scope.initForm.month,
            year: $scope.initForm.year,
        }
        dataService.load(
            context.id,
            param,
            function (data) {
                $scope.data = data.data.data;
                jQuery('#content').show();

                if (context.id === 0) {
                    $scope.method.refresh($scope.data.productionPriceID);
                }          
            },
            function (error) {
                $scope.data = null;
            }
        );
    }

    function update(typeID) {
        if ($scope.editForm.$valid) {
            $scope.data.updateTypeID = typeID;

            dataService.update(
                context.id,
                $scope.data,
                function (data) {
                    $scope.method.refresh(data.data.productionPriceID);
                },
                function (error) {
                    //do need do nothing
                }
            );
        }
        else {
            jsHelper.showMessage('warning', context.errMsg);
        }
    }

    function print() {

    }

    function calculateAvgPrice() {
        dataService.calculateAvgPrice(
            $scope.data.productionItemTypeID,
            $scope.data.month,
            $scope.data.year,
            function (data) {
                $scope.method.refresh(data.data);
            },
            function (error) {
                // do nothing
            });
    }

    function lockPrice(isLocked) {
        dataService.lockPrice(
            context.id,
            isLocked,
            function (data) {
                jsHelper.processMessage(data.message);
                $scope.data.isLocked = isLocked;
            },
            function (error) {
                // do nothing
            });
    }

    function getReceiptByProductionItem(productionItemID, month, year) {
        dataService.getReceiptByProductionItem(
            productionItemID,
            month,
            year,
            function (data) {
                $scope.receiptByProductionItems = data.data;
                $('#frmReceiptByProductionItem').modal('show');
            },
            function (error) {
                // do nothing
            });
    }

    function applyPriceOnReceipt() {
        dataService.applyPriceOnReceipt(
            $scope.data.productionPriceID,
            function (data) {
                jsHelper.processMessage(data.message);                
            },
            function (error) {
                // do nothing
            });
    }

    //
    // implement function for method 
    //
    function refresh(id) {
        jsHelper.loadingSwitch(true);
        window.location = context.refreshUrl + id;
    }

    
    
}]);