var saleOrderDetailGrid = jQuery('#saleOrderDetail').scrollableTable(null, function (sortedBy, sortedDirection) {
    var scope = angular.element(jQuery('body')).scope();
    datasource = scope.saleorder.saleOrderData.saleOrderDetails;

    if (sortedDirection === 'asc') {
        datasource.sort(function (a, b) {
            return a[sortedBy] > b[sortedBy] ? 1 : -1;
        });
    }
    else if (sortedDirection === 'desc') {
        datasource.sort(function (a, b) {
            return a[sortedBy] < b[sortedBy] ? 1 : -1;
        });
    }
    scope.$apply();
});

var saleOrderDetailSparepartGrid = jQuery('#saleOrderDetailSparepart').scrollableTable(null, function (sortedBy, sortedDirection) {
    var scope = angular.element(jQuery('body')).scope();
    datasource = scope.saleorder.saleOrderData.saleOrderDetailSpareparts;
    if (sortedDirection === 'asc') {
        datasource.sort(function (a, b) {
            return a[sortedBy] > b[sortedBy] ? 1 : -1;
        });
    }
    else if (sortedDirection === 'desc') {
        datasource.sort(function (a, b) {
            return a[sortedBy] < b[sortedBy] ? 1 : -1;
        });
    }
    scope.$apply();
});
(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', saleOrderController)
        .filter('saleOrderDetail_Filter', function () {
            return function (saleOrderDetails, articleCode, description, quantity, v4id) {
                var items = {
                    articleCode: articleCode,
                    description: description,
                    quantity: quantity,
                    v4id: v4id,
                    out: []
                }
                angular.forEach(saleOrderDetails, function (value, key) {
                    var pushed = true;

                    if (this.articleCode !== '') {
                        pushed = pushed && (value.articleCode.toUpperCase().indexOf(this.articleCode.toUpperCase()) >= 0);
                    }

                    if (this.description !== '') {
                        pushed = pushed && (value.description.toUpperCase().indexOf(this.description.toUpperCase()) >= 0);
                    }

                    if (pushed) {
                        this.out.push(value);
                    }
                }, items);
                return items.out;
            };
        });
    saleOrderController.$inject = ['$scope', '$timeout', 'dataService'];

    function saleOrderController($scope, $timeout, saleOrderMngService) {
        $scope.saleorder = null;
        $scope.selected_detail_extends = null; //store data when select one row on grid detail

        $scope.offerLine = null;
        $scope.isOfferLineLoaded = false;
        $scope.offerLineSparepart = null;
        $scope.isOfferLineSparepartLoaded = false;

        $scope.articleCode_filter = '';
        $scope.description_filter = '';
        $scope.quantity_filter = '';
        $scope.v4id_filter = '';
        $scope.newId = 0;

        $scope.support = {
            orderTypes: [
                { id: 'FACTORY', name: 'FACTORY' },
                { id: 'WAREHOUSE', name: 'WAREHOUSE' }
            ],
        };

        saleOrderMngService.serviceUrl = context.serviceUrl;
        saleOrderMngService.token = context.token;

        $scope.event = {
            checkHasAddedProduct: checkHasAddedProduct,
            checkHasAddedSparepart: checkHasAddedSparepart,
            clearDetail: clearDetail,
            loaddata: loaddata,
            deletedata: deletedata,
            removeProduct: removeProduct,
            removeAllProduct: removeAllProduct,
            removeSparepart: removeSparepart,
            removeAllSparepart: removeAllSparepart,
            frmAddProduct_onOpen: frmAddProduct_onOpen,
            frmAddProduct_onOK: frmAddProduct_onOK,
            frmAddProduct_selectAll: frmAddProduct_selectAll,
            frmAddProduct_unSelectAll: frmAddProduct_unSelectAll,
            frmAddSparepart_onOpen: frmAddSparepart_onOpen,
            frmAddSparepart_onOK: frmAddSparepart_onOK,
            frmAddSparepart_selectAll: frmAddSparepart_selectAll,
            frmAddSparepart_unSelectAll: frmAddSparepart_unSelectAll,
            quantityContainerRequire: quantityContainerRequire,
            ldS_DeliveryDate_Require: ldS_DeliveryDate_Require,
            update: update,
            confirmdata: confirmdata,
            revise: revise,
            printPI: printPI,
            ispiReceivedChecked: ispiReceivedChecked,
            //changeSignedPIFile: changeSignedPIFile,
            //removeSignedPIFile: removeSignedPIFile,
            //uploadSignedPIFile: uploadSignedPIFile,
            //changeClientPOFile: changeClientPOFile,
            //removeClientPOFile: removeClientPOFile,
            //uploadClientPOFile: uploadClientPOFile,
            orderQntKeyUp: orderQntKeyUp,
            addDescription: addDescription,
            removeDescription: removeDescription,
            insertDescription: insertDescription,
            viewDetailExtend: viewDetailExtend,
            selectedDelivery: selectedDelivery,
            selectedPaymentTerm: selectedPaymentTerm,
            sortAtClient: sortAtClient,
            getPrintTemplate: getPrintTemplate
        };

        $scope.method = {
            getNewId: getNewId,
            calTotalAmount: calTotalAmount,
            calTotalAmountSparepart: calTotalAmountSparepart,
            calTotalAmountPI: calTotalAmountPI
        };

        function checkHasAddedProduct(offerLineID) {
            var check = 0;
            if ($scope.saleorder.saleOrderData.saleOrderDetails != null && $scope.saleorder.saleOrderData.saleOrderDetails.length > 0) {
                angular.forEach($scope.saleorder.saleOrderData.saleOrderDetails, function (item) {
                    if (item.offerLineID === offerLineID) {
                        check = check + 1;
                    }
                });
            }
            if (check > 0) {
                return true;
            }
            else {
                return false;
            }
        };

        function checkHasAddedSparepart(offerLineSparepartID) {
            var check = 0;
            if ($scope.saleorder.saleOrderData.saleOrderDetailSpareparts != null && $scope.saleorder.saleOrderData.saleOrderDetailSpareparts.length > 0) {
                angular.forEach($scope.saleorder.saleOrderData.saleOrderDetailSpareparts, function (item) {
                    if (item.offerLineSparepartID === offerLineSparepartID) {
                        check = check + 1;
                    }
                });
            }
            if (check > 0) {
                return true;
            }
            else {
                return false;
            }
        };

        function clearDetail(orderType) {
            $scope.saleorder.saleOrderData.saleOrderDetails = [];
            $scope.saleorder.saleOrderData.saleOrderDetailSpareparts = [];

            $scope.event.loaddata(orderType);
        };

        function removeProduct(item) {
            $scope.saleorder.saleOrderData.saleOrderDetails.splice($scope.saleorder.saleOrderData.saleOrderDetails.indexOf(item), 1);
        };
        function removeAllProduct() {
            var tobeRemoved = $scope.saleorder.saleOrderData.saleOrderDetails.filter(o => !o.isFactoryOrderCreated);
            angular.forEach(angular.copy(tobeRemoved), function (item) {
                $scope.saleorder.saleOrderData.saleOrderDetails.splice($scope.saleorder.saleOrderData.saleOrderDetails.indexOf(item), 1);
            });
        };

        function removeSparepart(item) {
            $scope.saleorder.saleOrderData.saleOrderDetailSpareparts.splice($scope.saleorder.saleOrderData.saleOrderDetailSpareparts.indexOf(item), 1);
        };
        function removeAllSparepart() {
            var tobeRemoved = $scope.saleorder.saleOrderData.saleOrderDetailSpareparts.filter(o => !o.isFactoryOrderCreated);
            angular.forEach(angular.copy(tobeRemoved), function (item) {
                $scope.saleorder.saleOrderData.saleOrderDetailSpareparts.splice($scope.saleorder.saleOrderData.saleOrderDetailSpareparts.indexOf(item), 1);
            });
        };

        function getNewId() {
            $scope.newId--;
            return $scope.newId;
        };

        function frmAddProduct_onOpen() {
            //if (!$scope.isOfferLineLoaded) {
            var orderType = $scope.saleorder.saleOrderData.orderType;
            //if ($scope.saleorder.saleOrderData.saleOrderID > 0 && $scope.saleorder.saleOrderData.orderType) {
            //    orderType = $scope.saleorder.saleOrderData.orderType;
            //}
            saleOrderMngService.getOfferLine(
                context.offerID,
                orderType,
                function (data) {
                    if (data.message.type === 0) {
                        $scope.offerLine = data.data;
                        $scope.isOfferLineLoaded = true;
                        $scope.$apply();
                        $('#frmAddProduct').modal('show');
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.$apply();
                }
            );
            //}
            //else {
            //    angular.forEach($scope.offerLine, function (item) {
            //        item.isSelected = false;
            //    });
            //    $('#frmAddProduct').modal('show');
            //}
        };
        function frmAddProduct_onOK() {
            angular.forEach($scope.offerLine.filter(o => o.isSelected), function (item) {
                if ($scope.saleorder.saleOrderData.saleOrderDetails.filter(o => o.offerLineID === item.offerLineID).length === 0) {
                    var itemCloned = angular.copy(item);
                    itemCloned.saleOrderDetailID = $scope.method.getNewId();
                    $scope.saleorder.saleOrderData.saleOrderDetails.push(itemCloned);
                }
            });
        };
        function frmAddProduct_selectAll() {
            angular.forEach($scope.offerLine, function (item) {
                item.isSelected = true;
            });
        };
        function frmAddProduct_unSelectAll() {
            angular.forEach($scope.offerLine, function (item) {
                item.isSelected = false;
            });
        };

        function frmAddSparepart_onOpen() {
            //if (!$scope.isOfferLineSparepartLoaded) {
            var orderType = $scope.saleorder.saleOrderData.orderType;
            //if ($scope.saleorder.saleOrderData.saleOrderID > 0 && $scope.saleorder.saleOrderData.orderType) {
            //    orderType = $scope.saleorder.saleOrderData.orderType;
            //}

            saleOrderMngService.getOfferLineSparepart(
                context.offerID,
                orderType,
                function (data) {
                    if (data.message.type === 0) {
                        $scope.offerLineSparepart = data.data;
                        $scope.isOfferLineSparepartLoaded = true;
                        $scope.$apply();
                        $('#frmAddSparepart').modal('show');
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.$apply();
                }
            );
            //}
            //else {
            //    angular.forEach($scope.offerLineSparepart, function (item) {
            //        item.isSelected = false;
            //    });
            //    $('#frmAddSparepart').modal('show');
            //}
        };
        function frmAddSparepart_onOK() {
            angular.forEach($scope.offerLineSparepart.filter(o => o.isSelected), function (item) {
                if ($scope.saleorder.saleOrderData.saleOrderDetailSpareparts.filter(o => o.offerLineSparepartID === item.offerLineSparepartID).length === 0) {
                    var itemCloned = angular.copy(item);
                    itemCloned.saleOrderDetailSparepartID = $scope.method.getNewId();
                    $scope.saleorder.saleOrderData.saleOrderDetailSpareparts.push(itemCloned);
                }
            });
        };
        function frmAddSparepart_selectAll() {
            angular.forEach($scope.offerLineSparepart, function (item) {
                item.isSelected = true;
            });
        };
        function frmAddSparepart_unSelectAll() {
            angular.forEach($scope.offerLineSparepart, function (item) {
                item.isSelected = false;
            });
        };

        function calTotalAmount() {
            if ($scope.saleorder === null) return;
            var total = 0;
            angular.forEach($scope.saleorder.saleOrderData.saleOrderDetails, function (item) {
                total = total + item.unitPrice * item.orderQnt;
            })
            return total;
        };

        function calTotalAmountSparepart() {
            if ($scope.saleorder === null) return;
            var total = 0;
            angular.forEach($scope.saleorder.saleOrderData.saleOrderDetailSpareparts, function (item) {
                total = total + item.unitPrice * item.orderQnt;
            })
            return total;
        };

        function calTotalAmountPI() {
            if ($scope.saleorder === null) return;
            var total = 0;
            angular.forEach($scope.saleorder.saleOrderData.saleOrderExtends, function (item) {
                total = total + item.totalAmount;
            })
            return total + $scope.method.calTotalAmount() + $scope.method.calTotalAmountSparepart();
        };

        function quantityContainerRequire() {
            if ($scope.saleorder === null) return;
            return ($scope.saleorder.saleOrderData.quantity20DC === null || $scope.saleorder.saleOrderData.quantity20DC === undefined || $scope.saleorder.saleOrderData.quantity20DC === '' || $scope.saleorder.saleOrderData.quantity20DC === undefined)
                && ($scope.saleorder.saleOrderData.quantity40DC === null || $scope.saleorder.saleOrderData.quantity40DC === undefined || $scope.saleorder.saleOrderData.quantity40DC === '' || $scope.saleorder.saleOrderData.quantity40DC === undefined)
                && ($scope.saleorder.saleOrderData.quantity40HC === null || $scope.saleorder.saleOrderData.quantity40HC === undefined || $scope.saleorder.saleOrderData.quantity40HC === '' || $scope.saleorder.saleOrderData.quantity40HC === undefined)
                && ($scope.saleorder.saleOrderData.lessThanContainerLoad === null || $scope.saleorder.saleOrderData.lessThanContainerLoad === undefined || $scope.saleorder.saleOrderData.lessThanContainerLoad === '' || $scope.saleorder.saleOrderData.lessThanContainerLoad === undefined);
        };

        function ldS_DeliveryDate_Require() {
            if ($scope.saleorder === null) return;
            return ($scope.saleorder.saleOrderData.ldsFormated === null || $scope.saleorder.saleOrderData.ldsFormated === undefined || $scope.saleorder.saleOrderData.ldsFormated === '' || $scope.saleorder.saleOrderData.ldsFormated === '')
                && ($scope.saleorder.saleOrderData.deliveryDateFormated === null || $scope.saleorder.saleOrderData.deliveryDateFormated === undefined || $scope.saleorder.saleOrderData.deliveryDateFormated === '' || $scope.saleorder.saleOrderData.deliveryDateFormated === undefined);
        };

        function loaddata(orderType) {
            saleOrderMngService.load(
                context.id,
                context.offerID,
                orderType,
                function (data) {
                    $scope.saleorder = data.data;
                    //$scope.saleorder.saleOrderData.saleOrderDetails = data.data.saleOrderData.saleOrderDetails;
                    //if (context.id === "0" || context.id === null) {
                    //    $scope.saleorder.saleOrderData.orderType = "FACTORY";
                    //    $scope.saleorder.saleOrderData.season = $scope.saleorder.saleOrderData.offerSeason;
                    //}

                    var total = 0;
                    for (var i = 0; i < $scope.saleorder.saleOrderData.saleOrderDetails.length; i++) {
                        var item = $scope.saleorder.saleOrderData.saleOrderDetails[i];
                        total += (item.unitPrice * item.orderQnt);
                    }
                    $scope.total = total;

                    $scope.$apply();

                    jQuery('#content').show();
                    // monitor changes
                    $scope.$watch('saleorder', function () {
                        jQuery('#changeNotification').html('<i class="fa fa-save blink_me"></i>');
                    });

                    //setTimeout(function () { runAllForms(); }, 200);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                    $scope.saleorder = null;
                    $scope.$apply();
                }
            );
        };

        function update($event) {
            $event.preventDefault();
            if ($scope.editForm.$valid) {
                saleOrderMngService.update(context.id, $scope.saleorder.saleOrderData,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        console.log(data);
                        if (data.message.type === 0) {
                            window.location = context.refreshUrl + data.data.saleOrderData.saleOrderID + '/' + data.data.saleOrderData.offerID;
                            //location.reload();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', 'Input required');
            }
        };

        function deletedata($event) {
            $event.preventDefault();

            if (confirm('Are you sure ?')) {
                saleOrderMngService.delete(context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type === 0) {
                            window.location = '@Url.Action("Index", "SaleOrderMng")';
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        };

        function confirmdata($event, id, withoutsigned) {
            $event.preventDefault();

            if (confirm('Are you sure want to confirm ' + (withoutsigned ? ' without signed ?' : '?'))) {
                saleOrderMngService.confirm(context.id, $scope.saleorder.saleOrderData, withoutsigned,
                    function (data) {
                        $scope.saleorder.saleOrderData.trackingStatusNM = data.data.trackingStatusNM;
                        $scope.saleorder.saleOrderData.concurrencyFlag_String = data.data.concurrencyFlag_String;
                        jsHelper.processMessage(data.message);
                        //$scope.$apply();
                        if (data.message.type === 0) {
                            location.reload();
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        };

        function revise($event, id) {
            $event.preventDefault();

            if (confirm('Are you sure want to revise ?')) {
                saleOrderMngService.revise(context.id, $scope.saleorder.saleOrderData,
                    function (data) {
                        //$scope.saleorder.saleOrderData.trackingStatusNM = data.data.trackingStatusNM;
                        //$scope.saleorder.saleOrderData.concurrencyFlag_String = data.data.concurrencyFlag_String;
                        //jsHelper.processMessage(data.message);
                        //$scope.$apply();
                        location.reload();
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        };

        function getPrintTemplate($event) {
            $('#reportTemplate').modal('show');
        };

        function printPI($event, reportName) {
            $event.preventDefault();
            saleOrderMngService.printPI(context.id, reportName,
                function (data) {
                    window.open(context.backendReportUrl + data.data);
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function ispiReceivedChecked() {

            if ($scope.saleorder.saleOrderData.ispiReceived === true) {
                var date = new Date();
                var pad = '00';
                $scope.saleorder.saleOrderData.piReceivedDateFormated = pad.substring(0, pad.length - date.getDate().toString().length) + date.getDate().toString() + '/' + pad.substring(0, pad.length - (date.getMonth() + 1).toString().length) + (date.getMonth() + 1).toString() + '/' + date.getFullYear();
                $scope.saleorder.saleOrderData.piReceivedBy = context.currentUser;
            }
            else {
                $scope.saleorder.saleOrderData.piReceivedDateFormated = '';
                $scope.saleorder.saleOrderData.piReceivedBy = "";
            }
        };

        $scope.changeSignedPIFile = function ($event, controlID) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.saleorder.saleOrderData.signedPIFileURL = img.fileURL;
                        scope.saleorder.saleOrderData.signedPIFileFriendlyName = img.filename;
                        scope.saleorder.saleOrderData.newSignedPIFile = img.filename;
                        //call server to update file
                        scope.uploadSignedPIFile();
                    }, null);
                });
            };
            masterUploader.open();
        };

        $scope.removeSignedPIFile = function ($event, controlID) {
            $scope.saleorder.saleOrderData.signedPIFileURL = '';
            $scope.saleorder.saleOrderData.signedPIFileFriendlyName = '';
            $scope.saleorder.saleOrderData.newSignedPIFile = '';
            //call server to update file
            scope.uploadSignedPIFile();
        };

        $scope.uploadSignedPIFile = function () {
            saleOrderMngService.uploadSignedPIFile(
                $scope.saleorder.saleOrderData.saleOrderID,
                $scope.saleorder.saleOrderData.newSignedPIFile,
                $scope.saleorder.saleOrderData.signedPIFile,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type !== 2) {
                        $scope.saleorder.saleOrderData.signedPIFile = data.data.signedPIFile;
                        $scope.saleorder.saleOrderData.concurrencyFlag_String = data.data.concurrencyFlag_String;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        $scope.changeClientPOFile = function ($event, controlID) {
            $event.preventDefault();
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        scope.saleorder.saleOrderData.clientPOFileURL = img.fileURL;
                        scope.saleorder.saleOrderData.clientPOFriendlyName = img.filename;
                        scope.saleorder.saleOrderData.newClientPOFile = img.filename;
                        //call server to update file
                        scope.uploadClientPOFile();
                    }, null);
                });
            };
            masterUploader.open();
        };

        $scope.removeClientPOFile = function ($event, controlID) {
            $scope.saleorder.saleOrderData.clientPOFileURL = '';
            $scope.saleorder.saleOrderData.clientPOFriendlyName = '';
            $scope.saleorder.saleOrderData.newClientPOFile = '';
            //call server to update file
            scope.uploadClientPOFile();
        };

        $scope.uploadClientPOFile = function () {
            saleOrderMngService.uploadClientPOFile(
                $scope.saleorder.saleOrderData.saleOrderID,
                $scope.saleorder.saleOrderData.newClientPOFile,
                $scope.saleorder.saleOrderData.clientPOFile,
                function (data) {
                    jsHelper.processMessage(data.message);
                    if (data.message.type !== 2) {
                        $scope.saleorder.saleOrderData.clientPOFile = data.data.clientPOFile;
                        $scope.saleorder.saleOrderData.concurrencyFlag_String = data.data.concurrencyFlag_String;
                    }
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        };

        function orderQntKeyUp($event, index) {
            $event.preventDefault();
            if ($event.keyCode === 13) {
                $("#" + (index + 1)).focus();
                $("#" + (index + 1)).select();
            }
        };



        //
        //ADD/INSERT/REMOVE TOP-BOTTOM DESCRIPTION FOR SALEORDER
        //

        function addDescription($event, position) {
            $event.preventDefault();

            var descrip = $scope.saleorder.saleOrderData.saleOrderExtends;
            var max_rowIndex = 1;
            if (descrip.length > 0) {
                descrip.sort(function (a, b) { return b.rowIndex - a.rowIndex });
                max_rowIndex = descrip[0].rowIndex + 1;
            }
            $scope.saleorder.saleOrderData.saleOrderExtends.push({
                saleOrderExtendID: max_rowIndex * (-1),
                rowIndex: max_rowIndex,
                position: position
            });
        };

        function removeDescription($event, id) {
            $event.preventDefault();

            var isExist = false;
            for (var j = 0; j < $scope.saleorder.saleOrderData.saleOrderExtends.length; j++) {
                if ($scope.saleorder.saleOrderData.saleOrderExtends[j].saleOrderExtendID === id) {
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                $scope.saleorder.saleOrderData.saleOrderExtends.splice(j, 1);
            }
        };

        function insertDescription($event, position, rowIndex) {
            $event.preventDefault();
            var descrip = $scope.saleorder.saleOrderData.saleOrderExtends;
            for (var i = 0; i < descrip.length; i++) {
                if ($scope.saleorder.saleOrderData.saleOrderExtends[i].rowIndex > rowIndex) {
                    $scope.saleorder.saleOrderData.saleOrderExtends[i].rowIndex = $scope.saleorder.saleOrderData.saleOrderExtends[i].rowIndex + 1;
                    if ($scope.saleorder.saleOrderData.saleOrderExtends[i].saleOrderExtendID < 0) {
                        $scope.saleorder.saleOrderData.saleOrderExtends[i].saleOrderExtendID = $scope.saleorder.saleOrderData.saleOrderExtends[i].saleOrderExtendID - 1;
                    }
                }
            }
            $scope.saleorder.saleOrderData.saleOrderExtends.push({
                saleOrderExtendID: (rowIndex + 1) * (-1),
                rowIndex: rowIndex + 1,
                position: position

            });
        };


        //
        // ADD DESCRIPTION FOR EVERY PRODUCT LINE
        //
        function viewDetailExtend($event, saleOrderDetailID) {
            $event.preventDefault();
            $scope.detailExtendForm.event.load(saleOrderDetailID);
        };


        //
        //PROCESS EVENT WHEN SELECT PaymenTerm AND DeliveryTerm
        //
        function selectedDelivery(id) {
            $($scope.saleorder.deliveryTerms).each(function () {
                if (this.deliveryTermID === id) {
                    $scope.saleorder.saleOrderData.deliveryTypeNM = this.deliveryTypeNM;
                    $scope.$apply();
                }
            });
        };

        function selectedPaymentTerm(id) {
            $($scope.saleorder.paymentTerms).each(function () {
                if (this.paymentTermID === id) {
                    $scope.saleorder.saleOrderData.paymentTypeNM = this.paymentTypeNM;
                    $scope.$apply();
                }
            });
        };

        //
        // SORT GRID DETAIL AT CLIENT
        //

        function sortAtClient($event) {
            $event.preventDefault();
            colName = jQuery($event.target).data('colname');
            tableId = jQuery($event.target).data('tableid')

            datasource = $scope.saleorder.saleOrderData.saleOrderDetails;

            if (jQuery($event.target).hasClass('sorting_asc')) {
                datasource.sort(function (a, b) {
                    return a[colName] > b[colName] ? 1 : -1;
                });
            }
            else if (jQuery($event.target).hasClass('sorting_desc') || jQuery($event.target).hasClass('sorting')) {
                datasource.sort(function (a, b) {
                    return a[colName] < b[colName] ? 1 : -1;
                });
            }
        };

        //
        // ADD PRODUCT LINE DESCRIPTION FORM
        //
        $scope.detailExtendForm = {
            selected_detail_extends: null,
            event: {
                load: function (saleOrderDetailID) {
                    $("#" + saleOrderDetailID).toggle();
                    //$("#icon-detail-description-" + saleOrderDetailID).toggleClass("fa-minus-square-o");
                    if ($("#icon-detail-description-" + saleOrderDetailID).hasClass('fa-plus-square-o')) {
                        $("#icon-detail-description-" + saleOrderDetailID).removeClass('fa-plus-square-o');
                        $("#icon-detail-description-" + saleOrderDetailID).addClass('fa-minus-square-o');
                    }
                    else if ($("#icon-detail-description-" + saleOrderDetailID).hasClass('fa-minus-square-o')) {
                        $("#icon-detail-description-" + saleOrderDetailID).removeClass('fa-minus-square-o');
                        $("#icon-detail-description-" + saleOrderDetailID).addClass('fa-plus-square-o');
                    }
                },

                add: function ($event, saleOrderDetailID) {
                    $event.preventDefault();
                    $scope.detailExtendForm.method.add(saleOrderDetailID);
                },

                remove: function ($event, saleOrderDetailID, saleOrderDetailExtendID) {
                    $event.preventDefault();
                    $scope.detailExtendForm.method.remove(saleOrderDetailID, saleOrderDetailExtendID);
                },

                insert: function ($event, saleOrderDetailID, rowIndex) {
                    $event.preventDefault();
                    $scope.detailExtendForm.method.insert(saleOrderDetailID, rowIndex);
                },
            },
            method: {
                add: function (saleOrderDetailID) {
                    var order_details = $scope.saleorder.saleOrderData.saleOrderDetails.filter(function (o) { return o.saleOrderDetailID === saleOrderDetailID });
                    if (order_details[0].saleOrderDetailExtends === null) {
                        order_details[0].saleOrderDetailExtends = [];
                    }
                    var arr = order_details[0].saleOrderDetailExtends;

                    var max_rowIndex = 1;
                    if (arr.length > 0) {
                        arr.sort(function (a, b) { return b.rowIndex - a.rowIndex });
                        max_rowIndex = arr[0].rowIndex + 1;
                    }
                    arr.push({
                        saleOrderDetailExtendID: max_rowIndex * (-1),
                        rowIndex: max_rowIndex,
                    });
                    $scope.$apply();
                },
                remove: function (saleOrderDetailID, saleOrderDetailExtendID) {
                    var order_details = $scope.saleorder.saleOrderData.saleOrderDetails.filter(function (o) { return o.saleOrderDetailID === saleOrderDetailID });
                    var arr = order_details[0].saleOrderDetailExtends;
                    var isExist = false;
                    for (var j = 0; j < arr.length; j++) {

                        if (arr[j].saleOrderDetailExtendID === saleOrderDetailExtendID) {
                            isExist = true;
                            break;
                        }
                    }
                    if (isExist) {
                        arr.splice(j, 1);
                    }
                    $scope.$apply();
                },

                insert: function (saleOrderDetailID, rowIndex) {
                    var order_details = $scope.saleorder.saleOrderData.saleOrderDetails.filter(function (o) { return o.saleOrderDetailID === saleOrderDetailID });
                    if (order_details[0].saleOrderDetailExtends === null) {
                        order_details[0].saleOrderDetailExtends = [];
                    }
                    var arr = order_details[0].saleOrderDetailExtends;
                    for (var i = 0; i < arr.length; i++) {
                        if (arr[i].rowIndex > rowIndex) {
                            arr[i].rowIndex = arr[i].rowIndex + 1;
                            if (arr[i].saleOrderDetailExtendID < 0) {
                                arr[i].saleOrderDetailExtendID = arr[i].saleOrderDetailExtendID - 1;
                            }
                        }
                    }
                    arr.push({
                        saleOrderDetailExtendID: (rowIndex + 1) * (-1),
                        rowIndex: rowIndex + 1,
                    });
                }
            }

        };

        $scope.returnOrderForm = {
            loadingPlanData: null,
            event: {
                load: function ($event) {
                    $event.preventDefault();
                    jsHelper.showPopup('returnOrderFormMain', function () { });
                },

                openAddGoodsForm: function ($event) {
                    $event.preventDefault();
                    jsHelper.hidePopup('returnOrderFormMain', function () { });
                    $scope.returnOrderForm.method.getLoadingPlan($scope.saleorder.saleOrderData.saleOrderID);
                },

                addGoods: function ($event) {
                    $event.preventDefault();

                    data = $scope.saleorder.saleOrderData;
                    //add product again
                    angular.forEach($scope.returnOrderForm.loadingPlanData, function (value, key) {
                        jQuery.each(value.loadingPlanDetails, function (i, val) {
                            if (val.isSelected) {
                                data.saleOrderProductReturns.push({
                                    saleOrderProductReturnID: -1,
                                    saleOrderDetailID: val.saleOrderDetailID,
                                    loadingPlanDetailID: val.loadingPlanDetailID,

                                    loadingPlanUD: val.loadingPlanUD,
                                    blNo: val.blNo,
                                    containerNo: val.containerNo,
                                    factoryOrderUD: val.factoryOrderUD,
                                    factoryUD: val.factoryUD,
                                    articleCode: val.articleCode,
                                    description: val.description,
                                    orderQnt: val.returnQnt,

                                    loadingPlanID: val.loadingPlanID,
                                    productID: val.productID,
                                    offerLineID: val.offerLineID,
                                    modelID: val.modelID,
                                    materialID: val.materialID,
                                    materialTypeID: val.materialTypeID,
                                    materialColorID: val.materialColorID,
                                    frameMaterialID: val.frameMaterialID,
                                    frameMaterialColorID: val.frameMaterialColorID,
                                    subMaterialID: val.subMaterialID,
                                    subMaterialColorID: val.subMaterialColorID,
                                    seatCushionID: val.seatCushionID,
                                    backCushionID: val.backCushionID,
                                    cushionColorID: val.cushionColorID,
                                    fSCTypeID: val.fscTypeID,
                                    fSCPercentID: val.fscPercentID,

                                    remaingReturnQnt: val.remaingReturnQnt,
                                });
                            }
                        });
                    }, data)

                    //add sparepart again
                    angular.forEach($scope.returnOrderForm.loadingPlanData, function (value, key) {
                        jQuery.each(value.loadingPlanSparepartDetails, function (i, val) {
                            if (val.isSelected) {
                                data.saleOrderSparepartReturns.push({
                                    saleOrderSparepartReturnID: -1,
                                    saleOrderDetailSparepartID: val.saleOrderDetailSparepartID,
                                    loadingPlanSparepartDetailID: val.loadingPlanSparepartDetailID,

                                    loadingPlanUD: val.loadingPlanUD,
                                    blNo: val.blNo,
                                    containerNo: val.containerNo,
                                    factoryOrderUD: val.factoryOrderUD,
                                    factoryUD: val.factoryUD,
                                    articleCode: val.articleCode,
                                    description: val.description,
                                    orderQnt: val.returnQnt,

                                    loadingPlanID: val.loadingPlanID,
                                    sparepartID: val.sparepartID,
                                    offerLineSparepartID: val.offerLineSparepartID,
                                    modelID: val.modelID,
                                    partID: val.partID,
                                });
                            }
                        });
                    }, data)

                    jsHelper.hidePopup('returnOrderForm', function () { });
                    jsHelper.showPopup('returnOrderFormMain', function () { });
                },

                cancel: function ($event) {
                    $event.preventDefault();
                    jsHelper.hidePopup('returnOrderFormMain', function () { });
                    jsHelper.hidePopup('returnOrderForm', function () { });
                },

                confirmReturnData: function () {
                    if (confirm('Are you sure you want to return goods?\nSystem will automatic created order for these goods return for Eurofar client and automatic to import to warehouse')) {
                        var data = angular.copy($scope.saleorder.saleOrderData);
                        data.saleOrderDetails = [];
                        data.SaleOrderExtends = [];
                        data.SaleOrderDetailSpareparts = [];

                        $scope.returnOrderForm.method.createReturnData(data);
                    };
                },
            },
            method: {
                getLoadingPlan: function (saleOrderID) {
                    saleOrderMngService.getLoadingPlan(saleOrderID,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.returnOrderForm.loadingPlanData = data.data;
                                $scope.$apply();
                                jsHelper.showPopup('returnOrderForm', function () { });
                            }
                            else if (data.message.type === 2) {
                                jsHelper.processMessage(data.message);
                                $scope.returnOrderForm.loadingPlanData = null;
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                            $scope.returnOrderForm.loadingPlanData = null;
                        }
                    );
                },

                createReturnData: function (data) {
                    saleOrderMngService.createReturnData(data,
                        function (data) {
                            jsHelper.processMessage(data.message);
                            if (data.message.type === 0) { //success
                                jsHelper.hidePopup('returnOrderFormMain', function () {
                                    //reset id
                                    for (i = 0; i < $scope.saleorder.saleOrderData.saleOrderProductReturns.length; i++) {
                                        $scope.saleorder.saleOrderData.saleOrderProductReturns[i].saleOrderProductReturnID = 1;
                                    }
                                    for (i = 0; i < $scope.saleorder.saleOrderData.saleOrderSparepartReturns.length; i++) {
                                        $scope.saleorder.saleOrderData.saleOrderSparepartReturns[i].saleOrderSparepartReturnID = 1;
                                    }
                                });
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                }


            }
        };

        $scope.returnGoodsForm = {
            loadingPlanData: null,
            event: {
                load: function ($event) {
                    $event.preventDefault();
                    jsHelper.showPopup('returnGoodsForm', function () { });
                    $scope.returnGoodsForm.method.getLoadingPlan2($scope.saleorder.saleOrderData.saleOrderID);
                },
                cancel: function ($event) {
                    $event.preventDefault();
                    jsHelper.hidePopup('returnGoodsForm', function () { });
                },
                ok: function ($event) {
                    $event.preventDefault();
                    if (confirm('Are you sure you want to return goods?\nSystem will automate to import to warehouse')) {
                        $scope.returnGoodsForm.method.createReturnData2($scope.returnGoodsForm.loadingPlanData);
                    };
                },
                selectedCont: function ($event, isSelected, loadingPlanID) {
                    var loadingPlan = $scope.returnGoodsForm.loadingPlanData.filter(function (o) { return o.loadingPlanID === loadingPlanID });
                    for (var i = 0; i < loadingPlan[0].loadingPlanDetail2s.length; i++) {
                        if (isSelected) {
                            loadingPlan[0].loadingPlanDetail2s[i].returnQnt = loadingPlan[0].loadingPlanDetail2s[i].remaingReturnQnt;
                        }
                        else {
                            loadingPlan[0].loadingPlanDetail2s[i].returnQnt = null;
                        }
                    }
                    for (i = 0; i < loadingPlan[0].loadingPlanSparepartDetail2s.length; i++) {
                        if (isSelected) {
                            loadingPlan[0].loadingPlanSparepartDetail2s[i].returnQnt = loadingPlan[0].loadingPlanSparepartDetail2s[i].remaingReturnQnt;
                        }
                        else {
                            loadingPlan[0].loadingPlanSparepartDetail2s[i].returnQnt = null;
                        }
                    }
                },
            },
            method: {
                getLoadingPlan2: function (saleOrderID) {
                    saleOrderMngService.getLoadingPlan2(saleOrderID,
                        function (data) {
                            if (data.message.type === 0) {
                                $scope.returnGoodsForm.loadingPlanData = data.data;
                                $scope.$apply();
                                jsHelper.showPopup('returnGoodsForm', function () { });
                            }
                            else if (data.message.type === 2) {
                                jsHelper.processMessage(data.message);
                                $scope.returnGoodsForm.loadingPlanData = null;
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                            $scope.returnGoodsForm.loadingPlanData = null;
                        }
                    );
                },
                createReturnData2: function (data) {
                    saleOrderMngService.createReturnData2(data,
                        function (data) {
                            jsHelper.processMessage(data.message);
                            if (data.message.type === 0) { //success
                                jsHelper.hidePopup('returnGoodsForm', function () { });
                            }
                        },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
                }
            }
        };

        $timeout(function () {
            $scope.event.loaddata();
        }, 0);
    };
})();