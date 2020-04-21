//
// SUPPORT
//
jQuery('#main-form').keypress(function(e){
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;
    $scope.plcImageTypes = null;
    $scope.newid = 0;

    //
    // event
    //
    $scope.event = {
        init:function(){
            jsonService.load(
                context.id,
                context.factoryid,
                context.season,
                context.factoryOrderID,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.plcImageTypes = data.data.plcImageTypes;
                    $scope.$apply();
                    jQuery('#content').show();

                    // monitor changes
                    $scope.$watch('data', function() {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });
                },
                function (error) {
                    jsHelper.showMessage('warning', error);

                    $scope.data = null;
                    $scope.plcImageTypes = null;
                    $scope.$apply();
                }
            );
        },
        update: function($event){
            $event.preventDefault();

            if($scope.editForm.$valid)
            {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if(data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryProformaInvoiceID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else
            {
                jsHelper.showMessage('warning', errMsg);
            }
        },
        delete: function($event){
            $event.preventDefault();

            if (confirm('Are you sure ?')) {
                jsonService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if(data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },
        approve: function () {
            if (confirm('Approve the current proforma invoice ? (data can not be changed after approval)')) {
                jsonService.approve(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryProformaInvoiceID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },
        reset: function () {
            if (confirm('Reset approval status for the current proforma invoice ?')) {
                jsonService.reset(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.factoryProformaInvoiceID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },

        //
        // file functions
        //
        changeFile: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.fileLocation = img.fileURL;
                        scope.data.friendlyName = img.filename;
                        scope.data.attachedFile_NewFile = img.filename;
                        scope.data.attachedFile_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeFile: function () {
            $scope.data.friendlyName = '';
            $scope.data.fileLocation = '';
            $scope.data.attachedFile_NewFile = '';
            $scope.data.attachedFile_HasChange = true;
        },
        downloadFile: function () {
            if ($scope.data.fileLocation) {
                window.open($scope.data.fileLocation);
            }
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function(id){
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl+id;
        },
        getNewID: function(){
            $scope.newid--;
            return $scope.newid;
        },
        getTotalAmount: function () {
            if ($scope.data == null) {
                return 0;
            }

            var param = { total: 0 };
            angular.forEach($scope.data.details, function (value, key) {
                if (value.unitPrice != null && typeof value.unitPrice != 'undefined') {
                    this.total += jsHelper.round(parseInt(value.orderQnt) * parseFloat(value.unitPrice), 2);
                }
            }, param);
            return $filter('currency')(param.total, '$', 2);
        },
        getAmount: function (item) {
            if (item.unitPrice != null && typeof item.unitPrice != 'undefined') {
                return $filter('currency')(jsHelper.round(parseInt(item.orderQnt) * parseFloat(item.unitPrice), 2), '$ ', 2);
            }
            
            return 0;
        },
        getQuotePrice: function (item) {
            if (item.purchasingPrice != null && typeof item.purchasingPrice != 'undefined') {
                return $filter('currency')(item.purchasingPrice, '$', 2);
            }
            return '';
        },
        getQuantity: function (item) {
            if (item.orderQnt != null && typeof item.orderQnt != 'undefined') {
                return $filter('currency')(item.orderQnt, '', 0);
            }
            return '';
        },
        getTotalQuantity: function () {
            if ($scope.data == null) {
                return 0;
            }

            var param = { total: 0 };
            angular.forEach($scope.data.details, function (value, key) {
                this.total += parseInt(value.orderQnt);
            }, param);

            return $filter('currency')(param.total, '', 0);
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);