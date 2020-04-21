
(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives', 'ui.bootstrap', 'furnindo-directive']).controller('tilsoftController', purchaseQuotationMngController);
    purchaseQuotationMngController.$inject = ['$scope', 'dataService', '$timeout'];

    function purchaseQuotationMngController($scope, purchaseQuotationService, $timeout) {
        $scope.data = [];
        $scope.newID = -1;
        $scope.supportDeliveryTerms = [];
        $scope.supportPaymentTerms = [];
        $scope.supportFactoryRawMaterials = [];

        $scope.gridHandler = {
            loadMore: function () {
            },
            sort: function (sortedBy, sortedDirection) {
            },
            isTriggered: false
        };

        $scope.event = {
            init: init,
            get: get,
            remove: remove,
            update: update,
            removeLine: removeLine,
            addNewLine: addNewLine,
            downloadFile: downloadFile,
            removeFile: removeFile,
            changeFile: changeFile,
            approve: approve,
            cancel: cancel
        };

        $scope.method = {
            refresh: refresh,
            getNewID: getNewID,
            assignAutoCompleteProductionItem: assignAutoCompleteProductionItem
        };

        //For curency
        $scope.supportCurency = [
            { sname: 'VND', name: 'VND' },
            { sname: 'USD', name: 'USD' }
        ];

        function init() {
            purchaseQuotationService.serviceUrl = context.serviceUrl;
            purchaseQuotationService.token = context.token;
            purchaseQuotationService.supportServiceUrl = context.supportServiceUrl;
            purchaseQuotationService.getInit(
                function (data) {
                    $scope.supportDeliveryTerms = data.data.supportDeliveryTermDTOs;
                    $scope.supportPaymentTerms = data.data.supportPaymentTermDTOs;
                    $scope.supportFactoryRawMaterials = data.data.supportFactoryRawMaterialDTOs;

                    $scope.event.get();
                },
                function (error) {
                });         
        };

        function get() {
            purchaseQuotationService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;                  

                    jQuery('#content').show();
                    $scope.method.assignAutoCompleteProductionItem();
                },
                function (error) {
                });
        };

        function remove(id) {
            purchaseQuotationService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type === 0) {
                        window.location = context.backUrl;
                    }
                },
                function (error) {
                });
        };

        function update() {
            if ($scope.editForm.$valid) {
                // Get value validFrom and validTo
                $scope.data.validFrom = jQuery('#validFrom').val();
                $scope.data.validTo = jQuery('#validTo').val();
                var count = 0;
                if ($scope.data.purchaseQuotationDetailDTOs.length > 0) {
                    angular.forEach($scope.data.purchaseQuotationDetailDTOs, function (item) {
                        if (item.productionItemID === null || item.productionItemID === undefined) {
                            count = count + 1;
                        }
                    });
                }
                if (count === 0) {
                    purchaseQuotationService.update(
                        context.id,
                        $scope.data,
                        function (data) {
                            jsHelper.processMessage(data.message);

                            if (data.message.type === 0) {
                                window.location = context.refreshUrl + data.data.purchaseQuotationID;
                                $scope.data = data.data;
                            }
                        },
                        function (error) {
                        });
                }
                else {
                    jsHelper.showMessage('warning', count + ' item must input product');
                }
                
            } else {
                jsHelper.showMessage('warning', context.msgValid);
            }
        };

        function removeLine(item) {
            var index = $scope.data.purchaseQuotationDetailDTOs.indexOf(item);
            $scope.data.purchaseQuotationDetailDTOs.splice(index, 1);
        };

        function addNewLine() {
            $scope.data.purchaseQuotationDetailDTOs.push({
                purchaseQuotationDetailID: $scope.method.getNewID()
            });
            $timeout(function () {
                return $scope.method.assignAutoCompleteProductionItem();
            });
        };

        //file functions   
        function changeFile() {
            fileUploader2.callback = function () {
                scope.$apply(function () {
                    $scope.data.friendlyName = fileUploader2.selectedFriendlyName;
                    $scope.data.fileLocation = fileUploader2.selectedFileUrl;
                    $scope.data.file_NewFile = fileUploader2.selectedFileName;
                    $scope.data.file_HasChange = true;
                });
            };
            fileUploader2.open();
        };

        function removeFile() {
            $scope.data.friendlyName = '';
            $scope.data.fileLocation = '';
            $scope.data.file_NewFile = '';
            $scope.data.file_HasChange = true;
        };

        function downloadFile() {
            if ($scope.data.fileLocation) {
                window.open($scope.data.fileLocation);
            }
        };

        function approve() {
            purchaseQuotationService.approve(
                context.id,
                $scope.data,
                function (data) {
                    jsHelper.processMessage(data.message);

                    window.location = context.refreshUrl + data.data.purchaseQuotationID;
                    $scope.data = data.data;
                },
                function (error) {
                });
        };

        function cancel() {
            if (confirm('Are you sure Cancel ?')) {
                purchaseQuotationService.cancel(
                    context.id,
                    $scope.data,
                    function (data) {
                        if (data.message.type === 2) {
                            jsHelper.showMessage('warning', data.message.message);
                        } else {
                            window.location = context.refreshUrl + data.data.purchaseQuotationID;
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            };
        };


        /**
         * Method
         */
        function refresh() {
            window.location = context.refreshUrl + id
        };

        function getNewID() {
            $scope.newID--;
            return $scope.newID;
        };

        //Search Product
        function assignAutoCompleteProductionItem() {
            angular.forEach($scope.data.purchaseQuotationDetailDTOs, function (item) {
                $("#productItem" + item.purchaseQuotationDetailID).autocomplete({
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
                                    searchQuery: request.term,
                                }
                            }),
                            dataType: 'json',
                            url: context.getProductionItemUrl,
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
                    minLength: 2,
                    select: function (event, ui) {
                        var index = $scope.data.purchaseQuotationDetailDTOs.indexOf(item);

                        $scope.data.purchaseQuotationDetailDTOs[index].productionItemID = ui.item.id;
                        $scope.data.purchaseQuotationDetailDTOs[index].productionItemNM = ui.item.value;
                        $scope.data.purchaseQuotationDetailDTOs[index].productionItemUD = ui.item.productionItemUD;
                        $scope.data.purchaseQuotationDetailDTOs[index].unitNM = ui.item.unitNM;
                        $scope.data.purchaseQuotationDetailDTOs[index].remark = ui.item.remark;
                        $scope.data.purchaseQuotationDetailDTOs[index].orderQnt = ui.item.orderQnt;
                        $scope.data.purchaseQuotationDetailDTOs[index].unitPrice = ui.item.unitPrice;
                        $scope.data.purchaseQuotationDetailDTOs[index].productionItemTypeID = ui.item.productionItemTypeID;
                        $scope.data.purchaseQuotationDetailDTOs[index].frameWeight = ui.item.frameWeight;
                        $scope.data.purchaseQuotationDetailDTOs[index].isHasWeight = ui.item.isHasWeight;

                        $scope.$apply();
                    }
                });
            });
        };

        $scope.event.init();

        function createservices() {
            purchaseQuotationService.serviceUrl = context.serviceUrl;
            purchaseQuotationService.token = context.token;
        };
    };
})();