//
// SUPPORT
//
jQuery('#main-form').keypress(function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
        return false;
    }
});

//
// APP START
//
var tilsoftApp = angular.module('tilsoftApp', ['ui.grid', 'ui.grid.resizeColumns', 'ui.grid.pinning', 'ui.grid.edit', 'ui.grid.cellNav']);
tilsoftApp.controller('tilsoftController', ['$scope', function ($scope) {
    //
    // property
    //
    $scope.data = null;
    $scope.paymentTerms = null;
    $scope.deliveryTerms = null;
    $scope.offerDirections = null;
    $scope.priceDifferences = null;
    $scope.quotationStatuses = null;
    $scope.newid = -1000;
    $scope.newOfferDirection = null;

    $scope.selectedQuotationDetail = null;
    $scope.selectedQuotationDetailUpdateValue = {
        salePrice: null,
        priceDifferenceCode: null,
        statusID: null
    }
    $scope.applyDifferenceCodeForm = {
        data: {
            priceDifferenceCode: null
        }
    }

    //
    // grid config
    //    
    $scope.grdQuotationDetail = {
        enableSorting: false,
        enableColumnResizing: true,
        enableFiltering: true,
        columnDefs: [
            { name: 'Action', width: 200, enableHiding: false, enableSorting: false, enableColumnMenu: false, cellClass: 'cell-align-center', cellTemplate: '<a class="btn btn-primary btn-xs font-11" href="javascript:void(0)" title="Confirm" ng-click="grid.appScope.event.editQuotationDetail(row.entity)"><i class="fa fa-pencil"></i> Edit</a> <a class="btn btn-primary btn-xs font-11" href="javascript:void(0)" title="Confirm" ng-click="grid.appScope.event.confirmQuotationDetail(row.entity)"><i class="fa fa-thumbs-o-up"></i> Confirm</a> <a class="btn btn-danger btn-xs font-11" href="javascript:void(0)" title="Confirm" ng-click="grid.appScope.event.resetQuotationDetail(row.entity)"><i class="fa fa-thumbs-o-down"></i> Reset</a>' },
            { name: 'Status', field: 'quotationStatusNM', width: 100, enableHiding: false },
            { name: 'Art.Code', field: 'articleCode', width: 100, enableHiding: false },
            { name: 'Description', field: 'description', width: 250, enableHiding: false },
            { name: 'Order', field: 'factoryOrderUD', width: 100, enableHiding: false },
            { name: 'LDS', field: 'lds', width: 100, enableHiding: false },
            { name: 'Client', field: 'clientUD', width: 100, enableHiding: false },
            { name: 'Qnt', field: 'orderQnt', width: 100, enableHiding: false },
            { name: 'Diff.Code', field: 'priceDifferenceCode', width: 100, enableHiding: false },
            { name: 'Old Price', field: 'oldPrice', width: 100, enableHiding: false },
            { name: 'Purchasing', field: 'purchasingPrice', width: 100, enableHiding: false },
            { name: 'Sale', field: 'salePrice', width: 100, enableHiding: false },
            { name: 'Target', field: 'targetPrice', width: 100, enableHiding: false },
            { name: 'Remark', field: 'remark', width: 300, enableHiding: false },
            { name: 'Status Updated By', field: 'updatorName', width: 150, enableHiding: false, cellTemplate: '<a ng-show="(row.entity.statusUpdatedBy!=null && row.entity.updatorName2!=null)" href="' + context.viewUrl + '/{{row.entity.statusUpdatedBy}}" da data-featherlight="iframe" data-featherlight-iframe-allowfullscreen="true" data-featherlight-iframe-width="800" data-featherlight-iframe-height="600"><i class="fa fa-user"></i> {{row.entity.updatorName2}}</a><span ng-show="(row.entity.statusUpdatedBy!=null && row.entity.updatorName2==null && row.entity.updatorName!=null)">{{row.entity.updatorName}}</span>' },
            { name: 'Status Updated Date', field: 'statusUpdatedDate', width: 150, enableHiding: false }
        ],
        data: null
    };

    $scope.grdQuotationOffer = {
        enableSorting: false,
        enableColumnResizing: true,
        columnDefs: [
            { name: 'Direction', field: 'quotationOfferDirectionNM', width: 150, enableHiding: false },
            { name: 'Version', field: 'quotationOfferVersion', width: 150, enableHiding: false },
            { name: 'Offer Date', field: 'quotationOfferDate', width: 150, enableHiding: false },
            { name: 'Updated By', field: 'updatorName', width: 150, enableHiding: false, cellTemplate: '<a ng-show="(row.entity.updatedBy!=null && row.entity.updatorName2!=null)" href="' + context.viewUrl + '/{{row.entity.updatedBy}}" da data-featherlight="iframe" data-featherlight-iframe-allowfullscreen="true" data-featherlight-iframe-width="800" data-featherlight-iframe-height="600"><i class="fa fa-user"></i> {{row.entity.updatorName2}}</a><span ng-show="(row.entity.updatedBy!=null && row.entity.updatorName2==null && row.entity.updatorName!=null)">{{row.entity.updatorName}}</span>' },
            { name: 'Updated Date', field: 'updatedDate', enableHiding: false }
        ],
        onRegisterApi: function (gridApi) {
            gridApi.cellNav.on.navigate($scope, function (newRowCol, oldRowCol) {
                $scope.grdQuotationOfferDetail.data = newRowCol.row.entity.quotationOfferDetails;
            });
        },
        data: null
    };

    $scope.grdQuotationOfferDetail = {
        enableSorting: false,
        enableColumnResizing: true,
        columnDefs: [
            { name: 'Art.Code', field: 'articleCode', width: 150, enableHiding: false, enableCellEdit: false },
            { name: 'Description', field: 'description', width: 150, enableHiding: false, enableCellEdit: false },
            { name: 'Price', field: 'price', width: 150, enableHiding: false, enableCellEditOnFocus: true },
            { name: 'Remark', field: 'remark', enableHiding: false, enableCellEditOnFocus: true }
        ],
        onRegisterApi: function (gridApi) {
            gridApi.edit.on.afterCellEdit($scope, function (rowEntity, colDef, newValue, oldValue) {
                if (colDef.field == 'price') {
                    if (newValue == oldValue) { return; }

                    var param = {
                        isConfirmed: false,
                        row: rowEntity,
                        maxVersion: -1,
                        quotationDetailRow: null,
                        quotationOfferRow: null
                    }
                    // check if the price is already confirmed and if this is not the lates offer
                    angular.forEach($scope.grdQuotationDetail.data, function (value, key) {
                        if (value.quotationDetailID == this.row.quotationDetailID) {
                            if (value.statusID == 3 || value.statusID == 2) {
                                this.isConfirmed = true;
                            }
                            this.quotationDetailRow = value;
                        }
                    }, param);
                    if (param.isConfirmed) {
                        alert('Can not change price of a confirmed product!');
                        rowEntity.price = oldValue;
                        return;
                    }

                    // check if this is the latest offer
                    angular.forEach($scope.grdQuotationOffer.data, function (value, key) {
                        if (this.maxVersion < value.quotationOfferVersion) {
                            this.maxVersion = value.quotationOfferVersion;
                        }
                        if (value.quotationOfferID == this.row.quotationOfferID) {
                            this.quotationOfferRow = value;
                        }
                    }, param);
                    if (param.maxVersion > param.quotationOfferRow.quotationOfferVersion) {
                        alert('User can only change price of the latest offer !');
                        rowEntity.price = oldValue;
                        return;
                    }

                    // all condition are met, proceed to calculation
                    if (param.quotationOfferRow.quotationOfferDirectionID == 1) { // factory to furnindo
                        param.quotationDetailRow.purchasingPrice = newValue;
                        param.quotationDetailRow.salePrice = '';
                        if (param.quotationDetailRow.priceDifferenceRate && typeof param.quotationDetailRow.priceDifferenceRate != 'undefined') {
                            param.quotationDetailRow.salePrice = jsHelper.round(param.quotationDetailRow.purchasingPrice * (1 + jsHelper.round(param.quotationDetailRow.priceDifferenceRate, 4)), 2);
                        }
                    }
                    else {
                        if (param.quotationOfferRow.quotationOfferDirectionID == 2) { // furnido to factory
                            param.quotationDetailRow.targetPrice = newValue;
                        }
                    }

                    // confirm if target price match with sale price
                    if (param.quotationDetailRow.salePrice == param.quotationDetailRow.targetPrice) {
                        param.quotationDetailRow.statusID = 3;
                        param.quotationDetailRow.statusUpdatedBy = context.currentUserId;
                        param.quotationDetailRow.statusUpdatedDate = context.currentDate;
                        param.quotationDetailRow.updatorName = context.currentUserNm;
                        param.quotationDetailRow.updatorName2 = context.currentUserNm;
                        param.quotationDetailRow.quotationStatusNM = 'CONFIRMED';
                        alert('Price for item: ' + param.quotationDetailRow.description + ' - Client: ' + param.quotationDetailRow.clientUD + ' has been confirmed');
                    }
                }
            });
        },
        data: null
    };


    //
    // event
    //
    $scope.event = {
        init: function () {
            jsonService.load(
                context.id,
                context.factoryid,
                context.season,
                context.orders,
                function (data) {
                    $scope.data = data.data.data;
                    $scope.paymentTerms = data.data.paymentTerms;
                    $scope.deliveryTerms = data.data.deliveryTerms;
                    $scope.offerDirections = data.data.offerDirections;
                    $scope.priceDifferences = data.data.priceDifferences;
                    $scope.quotationStatuses = data.data.quotationStatuses;
                    $scope.grdQuotationDetail.data = $scope.data.quotationDetails;
                    $scope.grdQuotationOffer.data = $scope.data.quotationOffers;
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
                    $scope.paymentTerms = null;
                    $scope.deliveryTerms = null;
                    $scope.offerDirections = null;
                    $scope.priceDifferences = null;
                    $scope.quotationStatuses = null;
                    $scope.grdQuotationDetail.data = null;
                    $scope.grdQuotationOffer.data = null;
                    $scope.grdQuotationOfferDetail.data = null;
                    $scope.$apply();
                }
            );
        },
        update: function ($event) {
            $event.preventDefault();
            if ($scope.editForm.$valid) {
                jsonService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            $scope.method.refresh(data.data.quotationID);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
            else {
                jsHelper.showMessage('warning', errMsg);
            }
        },
        delete: function ($event) {
            $event.preventDefault();

            if (confirm('Are you sure ?')) {
                jsonService.delete(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        if (data.message.type == 0) {
                            window.location = context.backUrl;
                        }
                    },
                        function (error) {
                            jsHelper.showMessage('warning', error);
                        }
                    );
            }
        },
        editQuotationDetail: function (obj) {
            $scope.selectedQuotationDetail = obj;
            $scope.selectedQuotationDetailUpdateValue = {
                salePrice: null,
                priceDifferenceCode: obj.priceDifferenceCode,
                statusID: obj.statusID
            }
            jQuery('#frmEditQuotation').modal('show');
        },
        updateQuotationDetail: function () {
            $scope.selectedQuotationDetail.priceDifferenceCode = $scope.selectedQuotationDetailUpdateValue.priceDifferenceCode;
            $scope.selectedQuotationDetail.statusID = $scope.selectedQuotationDetailUpdateValue.statusID;

            var tmpIndex = -1;
            tmpIndex = jsHelper.getArrayIndex($scope.quotationStatuses, 'quotationStatusID', $scope.selectedQuotationDetailUpdateValue.statusID);
            if (tmpIndex >= 0) {
                $scope.selectedQuotationDetail.quotationStatusNM = $scope.quotationStatuses[tmpIndex].quotationStatusNM;
            }

            angular.forEach($scope.priceDifferences, function (value, key) {
                if (value.priceDifferenceUD == this.priceDifferenceCode) {
                    this.priceDifferenceRate = value.rate;
                    this.salePrice = jsHelper.round(this.purchasingPrice * (1 + jsHelper.round(value.rate, 4)), 2);
                }
            }, $scope.selectedQuotationDetail);
            if ($scope.selectedQuotationDetailUpdateValue.salePrice != null) {
                $scope.selectedQuotationDetail.salePrice = $scope.selectedQuotationDetailUpdateValue.salePrice;
            }
            $scope.selectedQuotationDetail = null;
            jQuery('#frmEditQuotation').modal('hide');
        },
        confirmQuotationDetail: function (obj) {
            if (obj.salePrice != obj.targetPrice || typeof obj.salePrice == 'undefined' || typeof obj.targetPrice == 'undefined' || obj.salePrice == null || obj.targetPrice == null) {
                alert('The item price can not be confirmed: sale price and target price do not matched or invalid');
                return;
            }
            else {
                if (confirm('Mark as confirmed ?')) {
                    obj.statusID = 3;
                    obj.statusUpdatedBy = context.currentUserId;
                    obj.statusUpdatedDate = context.currentDate;
                    obj.updatorName = context.currentUserNm;
                    obj.updatorName2 = context.currentUserNm;
                    obj.quotationStatusNM = 'CONFIRMED';
                }
            }
        },
        resetQuotationDetail: function (obj) {
            if (confirm('Reset confirm status ?')) {
                obj.statusID = 1;
                obj.statusUpdatedBy = context.currentUserId;
                obj.statusUpdatedDate = context.currentDate;
                obj.updatorName = context.currentUserNm;
                obj.updatorName2 = context.currentUserNm;
                obj.quotationStatusNM = 'PENDING';
            }
        },
        openApplyDifferenceCodeForm: function () {
            $scope.applyDifferenceCodeForm.data.priceDifferenceCode = null;
            jQuery('#frmApplyDifferenceCodeAllItem').modal('show');
        },
        onApplyDifferenceCodeFormOK: function () {
            if ($scope.applyDifferenceCodeForm.data.priceDifferenceCode != null) {
                $scope.method.applyDifferenceCode($scope.applyDifferenceCodeForm.data.priceDifferenceCode);
                jQuery('#frmApplyDifferenceCodeAllItem').modal('hide');
            }
            else {
                alert('Please select difference code to apply for all items');
                return;
            }
        },
        onGetFactoryQuotation: function () {
            jsonService.getFactoryQuotationReport(
                $scope.data.quotationID,
                function (data) {
                    window.open(context.backendReportUrl + data.data + '.xlsm');
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        onGetEurofarQuotation: function () {
            jsonService.getEurofarQuotationReport(
                $scope.data.quotationID,
                function (data) {
                    window.open(context.backendReportUrl + data.data + '.xlsm');
                },
                function (error) {
                    jsHelper.showMessage('warning', error);
                }
            );
        },
        onImportFactoryPrice: function () {

            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        jsonService.getXMLContent(
                            img.filename,
                            function (data) {
                                // parsing xml file
                                var xmlData = [];
                                var content = data.data.replace(/ns2:/g, ''); // replace all ns2: with empty string
                                jQuery(jQuery.parseXML(content)).find('QuotationMng_FactoryQuotationReportData_View').each(function () {
                                    if ($(this).find('Price').length > 0) {
                                        xmlData.push({
                                            quotationDetailID: parseInt($(this).find('QuotationDetailID').text()),
                                            price: parseFloat($(this).find('Price').text()),
                                            remark: $(this).find('Remark').text()
                                        });
                                    }
                                });

                                // add offer
                                var offerDirectionID = 1; // constant value for factory -> eurofar
                                var offerDirectionNM = '';
                                angular.forEach($scope.offerDirections, function (value, key) {
                                    if (value.quotationOfferDirectionID == offerDirectionID) {
                                        offerDirectionNM = value.quotationOfferDirectionNM;
                                    }
                                }, offerDirectionNM);

                                var offer = {
                                    quotationOfferID: $scope.method.getNewID(),
                                    quotationID: context.id,
                                    quotationOfferVersion: $scope.grdQuotationOffer.data.length + 1,
                                    quotationOfferDirectionID: offerDirectionID,
                                    quotationOfferDate: context.currentDate,
                                    updatedBy: context.currentUserId,
                                    updatedDate: context.currentDate,
                                    quotationOfferDirectionNM: offerDirectionNM,
                                    updatorName: context.currentUserNm,
                                    updatorName2: context.currentUserNm,
                                    quotationOfferDetails: []
                                }

                                // add offer detail
                                var param = {
                                    id: null,
                                    price: null,
                                    remark: null
                                }
                                var salePrice = null;
                                angular.forEach($scope.grdQuotationDetail.data, function (value, key) {
                                    if (value.statusID == 1 || value.statusID == 4) {
                                        salePrice = null

                                        param.id = value.quotationDetailID;
                                        param.price = null;
                                        param.remark = null;
                                        angular.forEach(xmlData, function (value, key) {
                                            if (value.quotationDetailID == this.id) {
                                                this.price = value.price;
                                                this.remark = value.remark;
                                            }
                                        }, param);

                                        if (param.price != null) {
                                            // all condition are met, proceed to calculation
                                            value.salePrice = null;
                                            value.purchasingPrice = param.price;
                                            value.remark = param.remark;
                                            value.statusID = 1; // pending
                                            value.quotationStatusNM = 'PENDING';
                                            if (value.priceDifferenceRate !== null && value.priceDifferenceRate !== '') {
                                                value.salePrice = jsHelper.round(value.purchasingPrice * (1 + jsHelper.round(value.priceDifferenceRate, 4)), 2);
                                            }

                                            // confirm if target price match with sale price
                                            if (value.salePrice !== null && value.salePrice == value.targetPrice) {
                                                value.statusID = 3;
                                                value.statusUpdatedBy = context.currentUserId;
                                                value.statusUpdatedDate = context.currentDate;
                                                value.updatorName = context.currentUserNm;
                                                value.updatorName2 = context.currentUserNm;
                                                value.quotationStatusNM = 'CONFIRMED';
                                            }

                                            this.quotationOfferDetails.push({
                                                quotationOfferDetailID: $scope.method.getNewID(),
                                                quotationOfferID: this.quotationOfferID,
                                                quotationDetailID: value.quotationDetailID,
                                                price: param.price,
                                                remark: param.remark,
                                                lds: value.lds,
                                                clientUD: value.clientUD,
                                                factoryOrderUD: value.factoryOrderUD,
                                                articleCode: value.articleCode,
                                                description: value.description
                                            });
                                        }
                                    }
                                }, offer);

                                // add to grid
                                $scope.grdQuotationOffer.data.push(offer);
                                $scope.grdQuotationOfferDetail.data = offer.quotationOfferDetails;
                                $scope.$apply();
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error);
                            }
                        );
                    }, null);
                });
            };
            masterUploader.open();
        },
        onImportEurofarPrice: function () {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        jsonService.getXMLContent(
                            img.filename,
                            function (data) {
                                // parsing xml file
                                var xmlData = [];
                                var content = data.data.replace(/ns2:/g, ''); // replace all ns2: with empty string
                                jQuery(jQuery.parseXML(content)).find('QuotationMng_EurofarQuotationReportData_View').each(function () {
                                    if ($(this).find('Price').length > 0) {
                                        xmlData.push({
                                            quotationDetailID: parseInt($(this).find('QuotationDetailID').text()),
                                            price: parseFloat($(this).find('Price').text()),
                                            remark: $(this).find('Remark').text()
                                        });
                                    }
                                });

                                // add offer
                                var offerDirectionID = 2; // constant value for eurofar -> factory
                                var offerDirectionNM = '';
                                angular.forEach($scope.offerDirections, function (value, key) {
                                    if (value.quotationOfferDirectionID == offerDirectionID) {
                                        offerDirectionNM = value.quotationOfferDirectionNM;
                                    }
                                }, null);

                                var offer = {
                                    quotationOfferID: $scope.method.getNewID(),
                                    quotationID: context.id,
                                    quotationOfferVersion: $scope.grdQuotationOffer.data.length + 1,
                                    quotationOfferDirectionID: offerDirectionID,
                                    quotationOfferDate: context.currentDate,
                                    updatedBy: context.currentUserId,
                                    updatedDate: context.currentDate,
                                    quotationOfferDirectionNM: offerDirectionNM,
                                    updatorName: context.currentUserNm,
                                    updatorName2: context.currentUserNm,
                                    quotationOfferDetails: []
                                }

                                // add offer detail
                                var param = {
                                    id: null,
                                    price: null,
                                    remark: null
                                }
                                var salePrice = null;

                                angular.forEach($scope.grdQuotationDetail.data, function (value, key) {
                                    if (value.statusID != 3) {
                                        param.id = value.quotationDetailID;
                                        param.price = null;
                                        param.remark = null;

                                        angular.forEach(xmlData, function (value, key) {
                                            if (value.quotationDetailID == this.id) {
                                                this.price = value.price;
                                                this.remark = value.remark;
                                            }
                                        }, param);

                                        if (param.price != null) {
                                            // all condition are met, proceed to calculation
                                            value.targetPrice = param.price;
                                            value.remark = param.remark;

                                            // confirm if target price match with sale price
                                            if (value.salePrice !== null && value.salePrice == value.targetPrice) {
                                                value.statusID = 3;
                                                value.statusUpdatedBy = context.currentUserId;
                                                value.statusUpdatedDate = context.currentDate;
                                                value.updatorName = context.currentUserNm;
                                                value.updatorName2 = context.currentUserNm;
                                                value.quotationStatusNM = 'CONFIRMED';
                                            }

                                            this.quotationOfferDetails.push({
                                                quotationOfferDetailID: $scope.method.getNewID(),
                                                quotationOfferID: this.quotationOfferID,
                                                quotationDetailID: value.quotationDetailID,
                                                price: param.price,
                                                remark: param.remark,
                                                lds: value.lds,
                                                clientUD: value.clientUD,
                                                factoryOrderUD: value.factoryOrderUD,
                                                articleCode: value.articleCode,
                                                description: value.description
                                            });
                                        }
                                    }
                                }, offer);

                                // add to grid
                                $scope.grdQuotationOffer.data.push(offer);
                                $scope.grdQuotationOfferDetail.data = offer.quotationOfferDetails;
                                $scope.$apply();
                            },
                            function (error) {
                                jsHelper.showMessage('warning', error);
                            }
                        );
                    }, null);
                });
            };
            masterUploader.open();
        }
    };

    //
    // method
    //
    $scope.method = {
        refresh: function (id) {
            jsHelper.loadingSwitch(true);
            window.location = context.refreshUrl + id + '?f=0&s=&o=[]';
        },
        getNewID: function () {
            $scope.newid--;
            return $scope.newid;
        },
        applyDifferenceCode: function (differenceCode) {
            if (confirm('Apply difference code ' + differenceCode + ' for all item in the quotation ?')) {
                var param = {
                    code: differenceCode,
                    rate: null
                }
                angular.forEach($scope.priceDifferences, function (value, key) {
                    if (value.priceDifferenceUD == this.code) {
                        this.rate = value.rate;
                    }
                }, param);
                angular.forEach($scope.grdQuotationDetail.data, function (value, key) {
                    if (value.statusID != 3) {
                        value.salePrice = jsHelper.round(value.purchasingPrice * (1 + jsHelper.round(param.rate, 4)), 2);
                        value.priceDifferenceRate = this.rate;
                        value.priceDifferenceCode = this.code;
                    }
                }, param);
            }
        }
    };

    //
    // init
    //
    $scope.event.init();
}]);