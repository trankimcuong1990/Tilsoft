function formatData(data) {
    return $.map(data.data, function (item) {
        //console.log(item)
        return {
            id: item.id,
            label: item.value,
            description: item.label,
            clientNM: item.clientNM,
            vatNo: item.vatNo,
            telephone: item.telephone,
            fax: item.fax
        }
    });
}
(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', draftCommercialInvoiceMngController);
    draftCommercialInvoiceMngController.$inject = ['$scope', 'dataService', '$timeout'];

    function draftCommercialInvoiceMngController($scope, draftCommercialInvoiceMngService, $timeout) {
        // variable
        $scope.data = [];
        //$scope.clients = [];
        $scope.seasons = [];
        $scope.deliveryTerms = [];
        $scope.paymentTerms = [];
        $scope.turnOverLedgerAccounts = [];
        $scope.saleOrderDetails = [];
        $scope.saleOrderDetailSpareparts = [];
        $scope.viewName = "init";
        $scope.newid = 0;
        $scope.checkSalerOrder = null;

        $scope.filter = {
            proformaInvoiceNo: '',
            articleCode: ''
        };
        // event
        $scope.event = {
            get: get,
            update: update,
            deleted: deleted,
            getClient: getClient,
            goNext: goNext,
            goBack: goBack,
            addFobProduct: addFobProduct,
            addFobSparepart: addFobSparepart,
            addSaleOrderDetail: addSaleOrderDetail,
            addSaleOrderDetailSparepart: addSaleOrderDetailSparepart,
            viewDetailDescription: viewDetailDescription,
            addDescription: addDescription,
            confirmInvoice: confirmInvoice,
            selectDeliveryTerm: selectDeliveryTerm,
            selectPaymentTerm: selectPaymentTerm, 
            getDraftInvoicePrintOut: getDraftInvoicePrintOut,
            loadSaleOrder: loadSaleOrder,
            checkAllSaleOrder: checkAllSaleOrder, 
            cancel: cancel
        };
        // variables for event quick-search
        $scope.ngInitClient = null;
        $scope.ngItemClient = {
            id: null,
            label: null,
            description: null
        };
        $scope.ngRequestClient = {
            url: context.serviceUrl + 'getclient',
            token: context.token,
        };
        $scope.ngSelectedClient = {
            id: null,
            label: null,
            description: null
        };

        $scope.currency = [
            { currencyValue: 'USD', currencyText: 'USD' },
            { currencyValue: 'EUR', currencyText: 'EUR' }
        ];
        $scope.vatPercents = [
            { vatPercentValue: 0, vatPercentText: '0%' },
            { vatPercentValue: 21, vatPercentText: '21%' }
        ];

        function get() {
            draftCommercialInvoiceMngService.serviceUrl = context.serviceUrl;
            draftCommercialInvoiceMngService.token = context.token;
            draftCommercialInvoiceMngService.load(
                context.id,
                null,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.seasons = data.data.seasons;
                    $scope.deliveryTerms = data.data.deliveryTerms;
                    $scope.paymentTerms = data.data.paymentTerms;
                    $scope.turnOverLedgerAccounts = data.data.turnOverLedgerAccounts;
                    if (context.id > 0) {
                        $scope.viewName = "edit";
                    }
                    else {
                        if (context.id == null) {
                            $scope.viewName = "init";
                        }
                    }
                    jQuery('#content').show();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function update() {
            //if (jQuery('#outsourcingCostNM').val() === null || jQuery('#outsourcingCostNM').val() === '') {
            //    jsHelper.showMessage('warning', 'Please input to Cost Name.');
            //    return false;
            //}

            //if (jQuery('#outsourcingCostNMEN').val() === null || jQuery('#outsourcingCostNMEN').val() === '') {
            //    jsHelper.showMessage('warning', 'Please input to Cost Name EN.');
            //    return false;
            //}

            //if ($scope.data.workCenterID === null || $scope.data.workCenterID === '') {
            //    jsHelper.showMessage('warning', 'Please select to Material Group.');
            //    return false;
            //}

            //if ($scope.data.isAdditionalCost === null || $scope.data.isAdditionalCost === '') {
            //    jsHelper.showMessage('warning', 'Please select to Additional Cost.');
            //    return false;
            //}

            draftCommercialInvoiceMngService.update(
                context.id,
                $scope.data,
                function (data) {
                    jsHelper.processMessage(data.message);

                    if (data.message.type === 0) {
                        window.location = context.refreshUrl + data.data.draftCommercialInvoiceID;
                        $scope.data = data.data;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function deleted(id) {
            draftCommercialInvoiceMngService.delete(
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

        function getClient(itemClient) {
            if (itemClient !== undefined && itemClient !== null) {
                //assing purchase request
                $scope.data.clientID = itemClient.id;
                $scope.data.clientUD = itemClient.value;
                $scope.data.clientNM = itemClient.clientNM;
                $scope.data.vatNo = itemClient.vatNo;
                $scope.data.telephone = itemClient.telephone;
                $scope.data.fax = itemClient.fax;
            }
        };

        function goNext(season) {
            $scope.viewName = "edit";
            $scope.data.season = season;
            //$scope.data.factoryRawMaterialShortNM = $scope.method.getFactoryRawMaterialOfficialNM($scope.data.factoryRawMaterialID);
        };
        function goBack() {
            window.location = context.backUrl;
            $scope.viewName = "init";
        };

        function addFobProduct() {
            $scope.checkSalerOrder = false;
            draftCommercialInvoiceMngService.getSaleOrder(
                $scope.data.clientID,
                0,
                $scope.data.season,
                $scope.filter.proformaInvoiceNo,
                $scope.filter.articleCode,
                function (data) {                   
                    $scope.saleOrderDetails = data.data;               
                    jQuery('#frmSaleOrderDetail').modal();
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };
        function addFobSparepart() {
            $scope.checkSalerOrder = false;
            draftCommercialInvoiceMngService.getSaleOrder(
                $scope.data.clientID,
                1,
                $scope.data.season,
                $scope.filter.proformaInvoiceNo,
                $scope.filter.articleCode,  
                function (data) {
                    $timeout(function () {
                        $scope.saleOrderDetailSpareparts = data.data;
                    }, 0);
                    jQuery('#frmSaleOrderDetailSparepart').modal();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                });
        };

        function addSaleOrderDetailSparepart() {
            for (var i = 0; i < $scope.saleOrderDetailSpareparts.length; i++) {
                var item = $scope.saleOrderDetailSpareparts[i];
                if (item.check === true) {
                    var addItem = {
                        draftCommercialInvoiceDetailID: $scope.method.getNewID(),
                        saleOrderDetailSparepartID: item.saleOrderDetailSparepartID,
                        quantity: item.quantity,
                        unitPrice: item.unitPrice,
                        articleCodeSparepart: item.articleCode,
                        descriptionSparepart: item.description,
                        proformaInvoiceNoSparepart: item.proformaInvoiceNo,
                        draftCommercialInvoiceDetailDescriptions: []
                    };
                    $scope.data.draftCommercialInvoiceDetails.push(addItem);
                }
            }
            $scope.filter = {
                proformaInvoiceNo: '',
                articleCode: ''
            };
            jQuery('#frmSaleOrderDetailSparepart').modal('hide');
        };

        function addSaleOrderDetail() {
            for (var i = 0; i < $scope.saleOrderDetails.length; i++) {
                var item = $scope.saleOrderDetails[i];
                if (item.check === true) {
                    var addItem = {
                        draftCommercialInvoiceDetailID: $scope.method.getNewID(),
                        saleOrderDetailID: item.saleOrderDetailID,
                        quantity: item.quantity,
                        unitPrice: item.unitPrice,
                        articleCode: item.articleCode,
                        description: item.description,
                        proformaInvoiceNo: item.proformaInvoiceNo,
                        draftCommercialInvoiceDetailDescriptions: []
                    };
                    $scope.data.draftCommercialInvoiceDetails.push(addItem);
                }
            }
            $scope.filter = {
                proformaInvoiceNo: '',
                articleCode: ''
            };
            jQuery('#frmSaleOrderDetail').modal('hide');
        };

        function viewDetailDescription(draftCommercialInvoiceDetailID) {
            $("#" + draftCommercialInvoiceDetailID).toggle();
            if ($("#icon-detail-description-" + draftCommercialInvoiceDetailID).hasClass('fa-plus-square-o')) {
                $("#icon-detail-description-" + draftCommercialInvoiceDetailID).removeClass('fa-plus-square-o');
                $("#icon-detail-description-" + draftCommercialInvoiceDetailID).addClass('fa-minus-square-o');
            }
            else if ($("#icon-detail-description-" + draftCommercialInvoiceDetailID).hasClass('fa-minus-square-o')) {
                $("#icon-detail-description-" + draftCommercialInvoiceDetailID).removeClass('fa-minus-square-o');
                $("#icon-detail-description-" + draftCommercialInvoiceDetailID).addClass('fa-plus-square-o');
            }
        };

        function addDescription($event, position) {
            $event.preventDefault();

            var descrip = $scope.data.draftCommercialInvoiceDescriptions;
            var max_rowIndex = 1;
            if (descrip.length > 0) {
                descrip.sort(function (a, b) { return b.rowIndex - a.rowIndex });
                max_rowIndex = descrip[0].rowIndex + 1;
            }
            $scope.data.draftCommercialInvoiceDescriptions.push({
                draftCommercialInvoiceDescriptionID: max_rowIndex * (-1),
                rowIndex: max_rowIndex,
                position: position
            });
            setTimeout(function () { runAllForms() }, 200);
        };
        function confirmInvoice() {
            if (confirm('Are you sure ?')) {
                draftCommercialInvoiceMngService.approve(
                    context.id,
                    $scope.data,
                    function (data) {
                        window.location = context.refreshUrl + data.data.draftCommercialInvoiceID;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    });
            }
        };

        function selectPaymentTerm(paymentTerm) {

            angular.forEach($scope.paymentTerms, function (item) {
                if (item.paymentTermID === paymentTerm) {
                    $scope.data.paymentTerm = item.paymentTermNM;
                }
            });  
        };
        function selectDeliveryTerm(deliveryTerm) {
            angular.forEach($scope.deliveryTerms, function (item) {
                if (item.deliveryTermID === deliveryTerm) {
                    $scope.data.deliveryTerm = item.deliveryTermNM;
                }
            });            
        };

        function getDraftInvoicePrintOut(id) {
            draftCommercialInvoiceMngService.getDraftCommercialPrintout(
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

        function loadSaleOrder(check) {
            var isCheck = parseInt(check);
            draftCommercialInvoiceMngService.getSaleOrder(
                $scope.data.clientID,
                isCheck,
                $scope.data.season,
                $scope.filter.proformaInvoiceNo,
                $scope.filter.articleCode,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (isCheck === 0) {
                        $scope.saleOrderDetails = data.data; 
                    }
                    if (isCheck === 1) {
                        $scope.saleOrderDetailSpareparts = data.data;
                    }     
                    $scope.$apply();
                },
                function (erorr) {
                    jsHelper.showMessage('warning', erorr);
                }
            );
        };

        function checkAllSaleOrder(detail, selectSaleOrder) {
            if ($scope.checkSalerOrder === true) {
                if (selectSaleOrder === 0) {//saleOrderDetail
                    $scope.saleOrderDetails = [];
                    for (var i = 0; i < detail.length; i++) {
                        var item = detail[i];
                        item.check = true;
                        $scope.saleOrderDetails.push(item);
                    }
                }
                if (selectSaleOrder === 1) {//saleOrderDetailSparepart
                    $scope.saleOrderDetailSpareparts = [];
                    for (var z = 0; z < detail.length; z++) {
                        var item_1 = detail[z];
                        item_1.check = true;
                        $scope.saleOrderDetailSpareparts.push(item_1);
                    }
                }
            }
            else {
                if (selectSaleOrder === 0) {//saleOrderDetail
                    $scope.saleOrderDetails = [];
                    for (var j = 0; j < detail.length; j++) {
                        var item_2 = detail[j];
                        item_2.check = false;
                        $scope.saleOrderDetails.push(item_2);
                    }
                }
                if (selectSaleOrder === 1) {//saleOrderDetailSparepart
                    $scope.saleOrderDetailSpareparts = [];
                    for (var x = 0; x < detail.length; x++) {
                        var item_3 = detail[x];
                        item_3.check = false;
                        $scope.saleOrderDetailSpareparts.push(item_3);
                    }
                }
            }
           
        };

        function cancel() {
            $scope.filter = {
                proformaInvoiceNo: '',
                articleCode: ''
            };
        }
        //
        // METHOD
        //
        $scope.method = {
            getNewID: function () {
            $scope.newid--;
            return $scope.newid;
            },
            addLine: function (draftCommercialInvoiceDetailID) {
                var arr = $scope.data.draftCommercialInvoiceDetails.filter(function (o) { return o.draftCommercialInvoiceDetailID === draftCommercialInvoiceDetailID });
                if (arr[0].draftCommercialInvoiceDetailDescriptions === null) {
                    arr[0].draftCommercialInvoiceDetailDescriptions = [];
                }
                var arr_detaildescription = arr[0].draftCommercialInvoiceDetailDescriptions;

                var max_rowIndex = 1;
                if (arr_detaildescription.length > 0) {
                    arr_detaildescription.sort(function (a, b) { return b.rowIndex - a.rowIndex });
                    max_rowIndex = arr_detaildescription[0].rowIndex + 1;
                }
                arr_detaildescription.push({
                    draftCommercialInvoiceDetailDescriptionID: max_rowIndex * (-1),
                    rowIndex: max_rowIndex
                });
            },

            addClientInfo: function (draftCommercialInvoiceDetailID, saleOrderDetailId, saleOrderDetailSparepartID) {
                var arr = $scope.data.draftCommercialInvoiceDetails.filter(function (o) { return o.draftCommercialInvoiceDetailID == draftCommercialInvoiceDetailID });
                if (arr[0].draftCommercialInvoiceDetailDescriptions === null) {
                    arr[0].draftCommercialInvoiceDetailDescriptions = [];
                }
                
                if (saleOrderDetailSparepartID !== null && saleOrderDetailSparepartID !== 0) {
                    var arr_detaildescription = arr[0].draftCommercialInvoiceDetailDescriptions;

                    var max_rowIndex = 1;
                    if (arr_detaildescription.length > 0) {
                        arr_detaildescription.sort(function (a, b) { return b.rowIndex - a.rowIndex });
                        max_rowIndex = arr_detaildescription[0].rowIndex + 1;
                    }
                    var insertvalue = '';
                    //insert client info
                    for (var i = 1; i <= 4; i++) {
                        switch (i) {
                            case 1:
                                insertvalue = 'CLIENT ARTICLE CODE : ' + (arr[0].clientArticleCodeSparepart == null ? '' : arr[0].clientArticleCodeSparepart);
                                break;
                            case 2:
                                insertvalue = 'CLIENT ATICLE NAME : ' + (arr[0].clientArticleNameSparepart == null ? '' : arr[0].clientArticleNameSparepart);
                                break;
                            case 3:
                                insertvalue = 'CLIENT ORDER NUMBER : ' + (arr[0].clientOrderNumberSparepart == null ? '' : arr[0].clientOrderNumberSparepart);
                                break;
                            case 4:
                                insertvalue = 'CLIENT EAN CODE : ' + (arr[0].clientEANCodeSparepart == null ? '' : arr[0].clientEANCodeSparepart);
                                break;
                        }
                        arr_detaildescription.push({
                            draftCommercialInvoiceDetailDescriptionID: max_rowIndex * (-1),
                            rowIndex: max_rowIndex,
                            description: insertvalue
                        });

                        max_rowIndex++;
                    }                   
                }

                if (saleOrderDetailId !== null && saleOrderDetailId !== 0) {
                    var arr_detaildescription1 = arr[0].draftCommercialInvoiceDetailDescriptions;

                    var max_rowIndex1 = 1;
                    if (arr_detaildescription1.length > 0) {
                        arr_detaildescription1.sort(function (a, b) { return b.rowIndex - a.rowIndex });
                        max_rowIndex1 = arr_detaildescription1[0].rowIndex + 1;
                    }
                    var insertvalu1 = '';
                    //insert client info
                    for (var j = 1; j <= 4; j++) {
                        switch (j) {
                            case 1:
                                insertvalu1 = 'CLIENT ARTICLE CODE : ' + (arr[0].clientArticleCode == null ? '' : arr[0].clientArticleCode);
                                break;
                            case 2:
                                insertvalu1 = 'CLIENT ATICLE NAME : ' + (arr[0].clientArticleName == null ? '' : arr[0].clientArticleName);
                                break;
                            case 3:
                                insertvalu1 = 'CLIENT ORDER NUMBER : ' + (arr[0].clientOrderNumber == null ? '' : arr[0].clientOrderNumber);
                                break;
                            case 4:
                                insertvalu1 = 'CLIENT EAN CODE : ' + (arr[0].clientEANCode == null ? '' : arr[0].clientEANCode);
                                break;
                        }
                        arr_detaildescription1.push({
                            draftCommercialInvoiceDetailDescriptionID: max_rowIndex1 * (-1),
                            rowIndex: max_rowIndex1,
                            description: insertvalu1
                        });

                        max_rowIndex1++;
                    }                  
                }
            },

            removeLine: function (draftCommercialInvoiceDetailID, draftCommercialInvoiceDetailDescriptionID) {
                var arr = $scope.data.draftCommercialInvoiceDetails.filter(function (o) { return o.draftCommercialInvoiceDetailID == draftCommercialInvoiceDetailID });
                var arr_detaildescription = arr[0].draftCommercialInvoiceDetailDescriptions;

                var isExist = false;
                for (var j = 0; j < arr_detaildescription.length; j++) {
                    if (arr_detaildescription[j].draftCommercialInvoiceDetailDescriptionID == draftCommercialInvoiceDetailDescriptionID) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    arr_detaildescription.splice(j, 1);
                }
                $scope.$apply();
            },

            insertLine: function (draftCommercialInvoiceDetailID, rowIndex) {
                var arr = $scope.data.draftCommercialInvoiceDetails.filter(function (o) { return o.draftCommercialInvoiceDetailID == draftCommercialInvoiceDetailID });
                if (arr[0].draftCommercialInvoiceDetailDescriptions == null) {
                    arr[0].draftCommercialInvoiceDetailDescriptions = [];
                }
                var arr_detaildescription = arr[0].draftCommercialInvoiceDetailDescriptions;

                for (var i = 0; i < arr_detaildescription.length; i++) {
                    if (arr_detaildescription[i].rowIndex > rowIndex) {
                        arr_detaildescription[i].rowIndex = arr_detaildescription[i].rowIndex + 1;
                        if (arr_detaildescription[i].draftCommercialInvoiceDetailDescriptionID < 0) {
                            arr_detaildescription[i].draftCommercialInvoiceDetailDescriptionID = arr_detaildescription[i].draftCommercialInvoiceDetailDescriptionID - 1;
                        }
                    }
                }
                arr_detaildescription.push({
                    draftCommercialInvoiceDetailDescriptionID: (rowIndex + 1) * (-1),
                    rowIndex: rowIndex + 1,
                });
            },

            removeFobProduct: function (draftCommercialInvoiceDetailID) {

                var arr = $scope.data.draftCommercialInvoiceDetails;
                var isExist = false;

                for (var j = 0; j < arr.length; j++) {
                    if (arr[j].draftCommercialInvoiceDetailID == draftCommercialInvoiceDetailID) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    arr.splice(j, 1);
                }
            },

            removeFobSparepart: function (draftCommercialInvoiceDetailID) {

                var arr = $scope.data.draftCommercialInvoiceDetails;
                var isExist = false;

                for (var j = 0; j < arr.length; j++) {
                    if (arr[j].draftCommercialInvoiceDetailID == draftCommercialInvoiceDetailID) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    arr.splice(j, 1);
                }
            },

           calTotalAmount: function () {
               if ($scope.data == null) return;
                var total = 0;
               angular.forEach($scope.data.draftCommercialInvoiceDetails, function (item) {
                   if (item.saleOrderDetailID !== null) {
                       total = total + item.unitPrice * item.quantity;
                   }
               });
                return total;
            },

             calTotalQuantity: function () {
                 if ($scope.data == null) return;
                var total = 0;
                 angular.forEach($scope.data.draftCommercialInvoiceDetails, function (item) {
                     if (item.saleOrderDetailID !== null) {
                         total = total + item.quantity;
                     }
                 });
                return total;
            },

            calTotalQuantitySparepart: function () {
                if ($scope.data == null) return;
                var total = 0;
                angular.forEach($scope.data.draftCommercialInvoiceDetails, function (item) {
                    if (item.saleOrderDetailSparepartID !== null) {
                        total = total + item.quantity;
                    }
                });
                return total;
            },

            calTotalAmountSparepart: function () {
                if ($scope.data == null) return;
                var total = 0;
                angular.forEach($scope.data.draftCommercialInvoiceDetails, function (item) {
                    if (item.saleOrderDetailSparepartID !== null) {
                        total = total + item.unitPrice * item.quantity;
                    }
                });
                return total;
            },

            netRecal: function () {
                var total = 0;
                angular.forEach($scope.data.draftCommercialInvoiceDetails, function (item) {
                    total = total + item.unitPrice * item.quantity;
                });
                $scope.data.netAmount = total;
                return total;
            },

            vatRecal: function () {
                $scope.data.vatAmount = $scope.data.netAmount * ($scope.data.vatRate == null || $scope.data.vatRate == 'undefined' ? 0 : $scope.data.vatRate) / 100;
                var amount = 0;
                amount = $scope.data.vatAmount;
                return amount;
            },

            totalRecal: function () {
                $scope.data.TotalAmount = $scope.data.netAmount + $scope.data.vatAmount;
                var amount = 0;
                amount = $scope.data.TotalAmount;
                return amount;
            },
            removeDescription: function (id) {

                isExist = false;
                for (var j = 0; j < $scope.data.draftCommercialInvoiceDescriptions.length; j++) {
                    if ($scope.data.draftCommercialInvoiceDescriptions[j].draftCommercialInvoiceDescriptionID == id) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist) {
                    $scope.data.draftCommercialInvoiceDescriptions.splice(j, 1);
                }
            },

            insertDescription: function (position, rowIndex) {
                var descrip = $scope.data.draftCommercialInvoiceDescriptions;
                for (var i = 0; i < descrip.length; i++) {
                    if ($scope.data.draftCommercialInvoiceDescriptions[i].rowIndex > rowIndex) {
                        $scope.data.draftCommercialInvoiceDescriptions[i].rowIndex = $scope.data.draftCommercialInvoiceDescriptions[i].rowIndex + 1;
                        if ($scope.data.draftCommercialInvoiceDescriptions[i].draftCommercialInvoiceDescriptionID < 0) {
                            $scope.data.draftCommercialInvoiceDescriptions[i].draftCommercialInvoiceDescriptionID = $scope.data.draftCommercialInvoiceDescriptions[i].draftCommercialInvoiceDescriptionID - 1;
                        }
                    }
                }
                $scope.data.draftCommercialInvoiceDescriptions.push({
                    draftCommercialInvoiceDescriptionID: (rowIndex + 1) * (-1),
                    rowIndex: rowIndex + 1,
                    position: position

                });
            }
        };
        // default event
        $scope.event.get();
    }
})();