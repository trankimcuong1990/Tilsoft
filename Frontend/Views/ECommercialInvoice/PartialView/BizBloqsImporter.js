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
                //
                //check invoice, invoiceline is object or array
                //
                var addInvoice = function (invoiceItem, tilsoftInvoiceItem) {
                    tilsoftInvoiceItem.invoiceLines = [];
                    if (angular.isArray(invoiceItem.InvoiceLine)) {
                        angular.forEach(invoiceItem.InvoiceLine, function (lItem) {
                            var tilsoftInvoiceLineItem = {};
                            $scope.bizBloqs.method.mappingInvoiceLine(lItem, tilsoftInvoiceLineItem);
                            tilsoftInvoiceItem.invoiceLines.push(tilsoftInvoiceLineItem);
                        });
                    }
                    else {
                        var tilsoftInvoiceLineItem = {};
                        $scope.bizBloqs.method.mappingInvoiceLine(invoiceItem.InvoiceLine, tilsoftInvoiceLineItem);
                        tilsoftInvoiceItem.invoiceLines.push(tilsoftInvoiceLineItem);
                    }
                    $scope.bizBloqs.method.mappingInvoice(invoiceItem, tilsoftInvoiceItem);
                };
                var tilsoftInvoices = [];
                if (angular.isArray(invoiceData.Invoice)) {
                    for (var i = 0; i < invoiceData.Invoice.length; i++) {
                        var invoiceItem = invoiceData.Invoice[i];
                        var tilsoftInvoiceItem = {};
                        addInvoice(invoiceItem, tilsoftInvoiceItem);
                        tilsoftInvoices.push(tilsoftInvoiceItem);
                    }
                }
                else {
                    var tilsoftInvoiceItem1 = {};
                    addInvoice(invoiceData.Invoice, tilsoftInvoiceItem1);
                    tilsoftInvoices.push(tilsoftInvoiceItem1);
                }
                $scope.bizBloqs.bizBloqInvoiceData = tilsoftInvoices;

                //show form
                $scope.forms.currentForm = $scope.forms.bizBloqsImporter;
                $scope.$apply();
            });
        },

        importInvoice: function () {
            eCommercialInvoiceService.bizBloqsImportData(
                $scope.bizBloqs.bizBloqInvoiceData,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type == 2) {                        
                        angular.forEach(data.data, function (item) {
                            var x = $scope.bizBloqs.bizBloqInvoiceData.filter(o => o.invoiceNo == item.invoiceNo)[0];
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
            input.setAttribute("accept", ".xml");
            input.addEventListener("change", function (e) {
                //get file content
                var file = e.target.files[0];
                var reader = new FileReader();
                reader.onload = function () {
                    var text = reader.result;
                    //parse to Json
                    var jsonObj = xmlToJson.parse(text);
                    var invoiceData = jsonObj.eExact.Invoices;
                    //return data
                    successCallBack(invoiceData);
                };
                reader.readAsText(file);
            }, false);
            input.click();
        },

        mappingInvoice: function (bizBloqsInvoiceItem, tilsoftInvoiceItem) {
            tilsoftInvoiceItem.isSelected = true;
            tilsoftInvoiceItem.isError = false;
            tilsoftInvoiceItem.errorList = [];
            tilsoftInvoiceItem.invoiceNo = bizBloqsInvoiceItem.invoicenumber;
            tilsoftInvoiceItem.invoiceDate = bizBloqsInvoiceItem.InvoiceDate;
            tilsoftInvoiceItem.orderNo = bizBloqsInvoiceItem.ordernumber;
            tilsoftInvoiceItem.clientUD = String(bizBloqsInvoiceItem.InvoiceTo.code).padStart(6,'0');
            tilsoftInvoiceItem.clientNM = bizBloqsInvoiceItem.InvoiceTo.Name;
            tilsoftInvoiceItem.accountNo = null;
            tilsoftInvoiceItem.vatRate = bizBloqsInvoiceItem.EntryDiscount.Percentage;
            tilsoftInvoiceItem.currency = bizBloqsInvoiceItem.ForeignAmount.Currency.code;
            tilsoftInvoiceItem.deliveryTerm = null;
            tilsoftInvoiceItem.paymentTerm = bizBloqsInvoiceItem.PaymentCondition.Description;
        },

        mappingInvoiceLine: function (bizBloqsInvoiceLineItem, tilsoftInvoiceLineItem) {
            tilsoftInvoiceLineItem.articleCode = bizBloqsInvoiceLineItem.Item.code;
            tilsoftInvoiceLineItem.description = bizBloqsInvoiceLineItem.Item.Description; 
            tilsoftInvoiceLineItem.quantity = bizBloqsInvoiceLineItem.Quantity;  
            tilsoftInvoiceLineItem.unitPrice = bizBloqsInvoiceLineItem.UnitPrice.Value;
            tilsoftInvoiceLineItem.currency = bizBloqsInvoiceLineItem.UnitPrice.Currency.code;
            tilsoftInvoiceLineItem.vatPercent = bizBloqsInvoiceLineItem.UnitPrice.VATPercentage;
            tilsoftInvoiceLineItem.accountNo = bizBloqsInvoiceLineItem.GLAccount.code;
            tilsoftInvoiceLineItem.discountPercentage = bizBloqsInvoiceLineItem.DiscountPercentage;            
        }
    };

    //
    // listen to parent scope
    //
    $rootScope.$on('bizBloqForm', function () {
        $scope.bizBloqs.event.import();
    });
    
}]);