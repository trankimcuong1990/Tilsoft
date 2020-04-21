modelApp.controller('BizBloqImporterController', ['$scope', '$rootScope', '$timeout', '$filter', function ($scope, $rootScope, $timeout, $filter) {
    $scope.bizBloqs = {};
    //
    // property
    //
    $scope.bizBloqs.bizBloqInvoiceData = [];

    var bizBloqsGrid = jQuery('#bizBloqsInvoice').scrollableTable(
        function (currentPage) {
        },
        function (sortedBy, sortedDirection) {
        }
    );

    //
    // events
    //
    $scope.bizBloqs.event = {
        import: function () {
            $scope.bizBloqs.method.getImportData(function (invoiceData) {
                console.log(invoiceData);
                tilsoftInvoices = [];
                var bizBloqInvoices = invoiceData.filter(o => o.Header === 'H');
                angular.forEach(bizBloqInvoices, function (bizItem) {
                    //invoice item
                    var tilsoftItem = {};
                    tilsoftItem.invoiceLines = [];
                    $scope.bizBloqs.method.mappingInvoice(bizItem, tilsoftItem);
                    tilsoftInvoices.push(tilsoftItem);

                    //invoice line item
                    var invoiceLines = invoiceData.filter(o => o.Header !== 'H' && o.Ordernumber === bizItem.Ordernumber);
                    angular.forEach(invoiceLines, function (bizLineItem) {
                        var tilsoftLineItem = {};
                        $scope.bizBloqs.method.mappingInvoiceLine(bizLineItem, tilsoftLineItem);
                        tilsoftItem.invoiceLines.push(tilsoftLineItem);
                    });
                });
                console.log(tilsoftInvoices);
                $scope.bizBloqs.bizBloqInvoiceData = tilsoftInvoices;
                //show form
                $scope.forms.currentForm = $scope.forms.bizBloqsImporter;
                $scope.$apply();
            });
        },

        importInvoice: function () {
            $scope.bizBloqs.apiService.bizBloqsImportData(
                bizBloqsContext.serviceUrl,
                bizBloqsContext.token,
                $scope.bizBloqs.bizBloqInvoiceData,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 2) {                        
                        angular.forEach(data.data, function (item) {
                            var x = $scope.bizBloqs.bizBloqInvoiceData.filter(o => o.orderNumber == item.orderNumber)[0];
                            x.isError = true;
                            x.isSelected = false;
                            x.errorList.push(item.remark);
                        });
                    }
                    $scope.$apply();
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        }
    };

    $scope.bizBloqs.method = {
        getImportData: function (successCallBack) {
            var input = document.createElement("input");
            input.setAttribute("type", "file");
            input.setAttribute("accept", ".csv");
            input.addEventListener("change", function (e) {
                //get file content
                var file = e.target.files[0];
                Papa.parse(file, {
                    delimiter: "",
                    //header: true,
                    skipEmptyLines: true,
                    complete: function (results) {
                        //adjust source data (replace ' by string empty)
                        var bizBloqsItems = [];
                        var bizBloqsItem = {};
                        var hearder = results.data[0];
                        for (var i = 0; i < results.data.length; i++) {
                            if (i > 0) {
                                var item = results.data[i];
                                bizBloqsItem = {};
                                for (var j = 0; j < hearder.length; j++) {
                                    var fieldName = hearder[j].replace(':', '');
                                    fieldName = fieldName.replace(' ', '');
                                    bizBloqsItem[fieldName] = item[j].replace(/'/g, '');      
                                }
                                bizBloqsItems.push(bizBloqsItem);
                            }
                        }                        
                        successCallBack(bizBloqsItems);
                    }
                });
            }, false);
            input.click();
        },

        mappingInvoice: function (bizBloqsInvoiceItem, tilsoftInvoiceItem) {
            tilsoftInvoiceItem.isSelected = true;
            tilsoftInvoiceItem.isError = false;
            tilsoftInvoiceItem.errorList = [];
            tilsoftInvoiceItem.currency = bizBloqsInvoiceItem.Currency;
            tilsoftInvoiceItem.clientUD = String(bizBloqsInvoiceItem.InvoicetoCode).padStart(6, '0');
            tilsoftInvoiceItem.discountPercentage = bizBloqsInvoiceItem.OrderDiscountPercentage === '' ? 0 : parseFloat(bizBloqsInvoiceItem.OrderDiscountPercentage);
            tilsoftInvoiceItem.orderDate = bizBloqsInvoiceItem.Orderdate;
            tilsoftInvoiceItem.orderNumber = bizBloqsInvoiceItem.Ordernumber;
            tilsoftInvoiceItem.vatPercentage = bizBloqsInvoiceItem.VATPercentage === '' ? 0 : parseFloat(bizBloqsInvoiceItem.VATPercentage);
        },

        mappingInvoiceLine: function (bizBloqsInvoiceLineItem, tilsoftInvoiceLineItem) {
            tilsoftInvoiceLineItem.articleCode = bizBloqsInvoiceLineItem.Item;
            tilsoftInvoiceLineItem.netPrice = parseFloat(bizBloqsInvoiceLineItem.Netprice.replace(',','.'));
            tilsoftInvoiceLineItem.quantity = parseFloat(bizBloqsInvoiceLineItem.Quantity);
            tilsoftInvoiceLineItem.unitPrice = parseFloat(bizBloqsInvoiceLineItem.Unitprice.replace(',', '.'));
        },               
    };

    $scope.bizBloqs.apiService = {
        bizBloqsImportData: function (serviceUrl, token, bizBloqsInvoice, successCallBack, errorCallBack) {
            jQuery.ajax({
                cache: false,
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + token
                },
                type: "POST",
                dataType: "json",
                data: JSON.stringify(bizBloqsInvoice),
                url: serviceUrl + 'bizbloqs-import-invoice',
                beforeSend: function () {
                    jsHelper.loadingSwitch(true);
                },
                success: function (data) {
                    jsHelper.loadingSwitch(false);
                    successCallBack(data);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    jsHelper.loadingSwitch(false);
                    errorCallBack(xhr.responseJSON.exceptionMessage);
                }
            });
        }
    };

    //
    // listen to parent scope
    //
    $rootScope.$on('bizBloqForm', function () {
        $scope.bizBloqs.event.import();
    });
    
}]);