//
// SUPPORT
//
//jQuery('#main-form').keypress(function (e) {
//    if (e.keyCode == 13) {
//        e.preventDefault();
//        return false;
//    }
//});

//
// APP START
//
jQuery('#grdProduct').scrollableTable2();
jQuery('#grdSparepart').scrollableTable2();

var tilsoftApp = angular.module('tilsoftApp', ['furnindo-directive']);
tilsoftApp.controller('tilsoftController', ['$scope', '$filter', function ($scope, $filter) {
    //
    // property
    //
    $scope.data = null;
    $scope.containerTypes = null;

    $scope.product = {
        productId: null,
        product: null
    };

    $scope.sparepart = {
        sparepartId: null,
        sparepart: null
    };

    $scope.newid = 0;

    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                context.bookingId,
                context.factoryId,
                context.parentId,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.containerTypes = data.data.containerTypes;
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
                    $scope.containerTypes = null;
                    $scope.$apply();
                }
            );
        },
        update: function () {
            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessageEx(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.loadingPlanID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', context.error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', errMsg);
            }
        },
        delete: function () {
            if (confirm('Are you sure ?')) {
                jsonService.delete(context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        window.location = context.backUrl;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        },

        // product
        addProduct: function () {
            jQuery('#productDetail').modal('hide');

            if ($scope.method.productExists($scope.product.product.factoryOrderDetailID)) {
                alert('Selected product is already exists in the current loading plan');
            }
            else {
                $scope.data.loadingPlanDetails.push({
                    loadingPlanDetailID: $scope.method.getNewID(),
                    factoryOrderDetailID: $scope.product.product.factoryOrderDetailID,
                    quantity: 0,
                    remark: '',
                    factoryOrderUD: $scope.product.product.factoryOrderUD,
                    lds: $scope.product.product.lds,
                    clientUD: $scope.product.product.clientUD,
                    factoryUD: $scope.product.product.factoryUD,
                    articleCode: $scope.product.product.articleCode,
                    description: $scope.product.product.description,
                    qnt40HC: $scope.product.product.qnt40HC,
                    orderQnt: $scope.product.product.orderQnt,
                    totalLoaded: $scope.product.product.totalLoaded
                });
            }

            $scope.product.productId = null;
            $scope.product.product = null;
            jQuery('#product-autocomplete').val('');
        },
        removeProduct: function (item) {
            $scope.data.loadingPlanDetails.splice($scope.data.loadingPlanDetails.indexOf(item), 1);
        },

        // sparepart
        addSparepart: function () {
            jQuery('#sparepartDetail').modal('hide');
            if ($scope.method.sparepartExists($scope.sparepart.sparepart.factoryOrderSparepartDetailID)) {
                alert('Selected sparepart is already exists in the current loading plan');
            }
            else {
                $scope.data.loadingPlanSparepartDetails.push({
                    loadingPlanSparepartDetailID: $scope.method.getNewID(),
                    factoryOrderSparepartDetailID: $scope.sparepart.sparepart.factoryOrderSparepartDetailID,
                    quantity: 0,
                    remark: '',
                    factoryOrderUD: $scope.sparepart.sparepart.factoryOrderUD,
                    lds: $scope.sparepart.sparepart.lds,
                    clientUD: $scope.sparepart.sparepart.clientUD,
                    factoryUD: $scope.sparepart.sparepart.factoryUD,
                    articleCode: $scope.sparepart.sparepart.articleCode,
                    description: $scope.sparepart.sparepart.description,
                    qnt40HC: $scope.sparepart.sparepart.qnt40HC,
                    orderQnt: $scope.sparepart.sparepart.orderQnt,
                    totalLoaded: $scope.sparepart.sparepart.totalLoaded
                });
            }           

            $scope.sparepart.sparepartId = null;
            $scope.sparepart.sparepart = null;
            jQuery('#sparepart-autocomplete').val('');
        },
        removeSparepart: function (item) {
            $scope.data.loadingPlanSparepartDetails.splice($scope.data.loadingPlanSparepartDetails.indexOf(item), 1);
        },

        // image
        changeProductPicture1Image: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.productPicture1_DisplayUrl = img.fileURL;
                        scope.data.productPicture1_OriginalUrl = img.fileURL;
                        scope.data.productPicture1_NewFile = img.filename;
                        scope.data.productPicture1_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeProductPicture1Image: function () {
            $scope.data.productPicture1_DisplayUrl = '';
            scope.data.productPicture1_OriginalUrl = '';
            $scope.data.productPicture1_NewFile = '';
            $scope.data.productPicture1_HasChange = true;
        },
        changeProductPicture2Image: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.productPicture2_DisplayUrl = img.fileURL;
                        scope.data.productPicture2_OriginalUrl = img.fileURL;
                        scope.data.productPicture2_NewFile = img.filename;
                        scope.data.productPicture2_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeProductPicture2Image: function () {
            $scope.data.productPicture2_DisplayUrl = '';
            scope.data.productPicture2_OriginalUrl = '';
            $scope.data.productPicture2_NewFile = '';
            $scope.data.productPicture2_HasChange = true;
        },
        changeContainer1Image: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.containerPicture1_DisplayUrl = img.fileURL;
                        scope.data.containerPicture1_OriginalUrl = img.fileURL;
                        scope.data.containerPicture1_NewFile = img.filename;
                        scope.data.containerPicture1_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeContainer1Image: function () {
            $scope.data.containerPicture1_DisplayUrl = '';
            scope.data.containerPicture1_OriginalUrl = '';
            $scope.data.containerPicture1_NewFile = '';
            $scope.data.containerPicture1_HasChange = true;
        },
        changeContainer2Image: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.data.containerPicture2_DisplayUrl = img.fileURL;
                        scope.data.containerPicture2_OriginalUrl = img.fileURL;
                        scope.data.containerPicture2_NewFile = img.filename;
                        scope.data.containerPicture2_HasChange = true;
                    }, null);
                });
            };
            masterUploader.open();
        },
        removeContainer2Image: function () {
            $scope.data.containerPicture2_DisplayUrl = '';
            scope.data.containerPicture2_OriginalUrl = '';
            $scope.data.containerPicture2_NewFile = '';
            $scope.data.containerPicture2_HasChange = true;
        }
    };

    //
    // method
    //
    $scope.method = {
        getNewID: function(){
            $scope.newid--;
            return $scope.newid;
        },
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id;
        },
        productExists: function (factoryOrderDetailID) {
            var result = false;
            angular.forEach(scope.data.loadingPlanDetails, function (value, key) {
                if (value.factoryOrderDetailID == factoryOrderDetailID) {
                    result = true;
                }
            }, null);            
            return result;
        },
        sparepartExists: function (factoryOrderSparepartDetailID) {
            var result = false;
            angular.forEach(scope.data.loadingPlanSparepartDetails, function (value, key) {
                if (value.factoryOrderSparepartDetailID == factoryOrderSparepartDetailID) {
                    result = true;
                }
            }, null);
            return result;
        }
    };

    //
    // init
    //    
    $scope.event.init();
}]);

//
// JQUERY EXTENSION DECLARATION
//
jQuery(document).ready(function () {
    jQuery('#product-autocomplete').val('');
    jQuery('#sparepart-autocomplete').val('');

    jQuery('#product-autocomplete').devbridgeAutocomplete({
        serviceUrl: jsonService.serviceUrl + 'quicksearchfactoryorderdetail',
        dataType: 'json',
        minChars: 3,
        ajaxSettings: {
            cache: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jsonService.token
            },
            type: "GET"
        },
        params: { factoryid: function () { return scope.data.factoryID; } },
        onSelect: function (suggestion) {
            scope.product.productId = suggestion.data.factoryOrderDetailID;
            scope.product.product = suggestion.data;
            scope.$apply();
        },
        transformResult: function (response) {
            return {
                suggestions: jQuery.map(response, function (item) {
                    return { value: item.itemDescription, data: item };
                })
            };
        },
        onInvalidateSelection: function () {
            scope.product.productId = null;
            scope.product.product = null;
            scope.$apply();
            jQuery('#product-autocomplete').val('');
        },
        showNoSuggestionNotice: true,
        noCache: true,
        onSearchStart: function (query) {
            jsHelper.loadingSwitch(true);
        },
        onSearchComplete: function (query, suggestions) {
            jsHelper.loadingSwitch(false);
        }
    });

    jQuery('#sparepart-autocomplete').devbridgeAutocomplete({
        serviceUrl: jsonService.serviceUrl + 'quicksearchfactoryordersparepartdetail',
        dataType: 'json',
        minChars: 3,
        ajaxSettings: {
            cache: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jsonService.token
            },
            type: "GET"
        },
        params: { factoryid: function () { return scope.data.factoryID; } },
        onSelect: function (suggestion) {
            scope.sparepart.sparepartId = suggestion.data.factoryOrderSparepartDetailID;
            scope.sparepart.sparepart = suggestion.data;
            scope.$apply();
        },
        transformResult: function (response) {
            return {
                suggestions: jQuery.map(response, function (item) {
                    return { value: item.itemDescription, data: item };
                })
            };
        },
        onInvalidateSelection: function () {
            scope.sparepart.sparepartId = null;
            scope.sparepart.sparepart = null;
            scope.$apply();
            jQuery('#sparepart-autocomplete').val('');
        },
        showNoSuggestionNotice: true,
        noCache: true,
        onSearchStart: function (query) {
            jsHelper.loadingSwitch(true);
        },
        onSearchComplete: function (query, suggestions) {
            jsHelper.loadingSwitch(false);
        }
    });
});