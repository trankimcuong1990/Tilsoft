function formatModel(data) {
    return $.map(data.data, function (item) {
        if (item !== null) {
            return {
                id: item.modelID,
                label: item.modelUD,
                description: item.modelNM,
                qnt40HC: item.qnt40HC,

                productID: item.productID,
                productArticleCode: item.articleCode,
                productDescription: item.description
            };
        }
    });
};
function formatSample(data) {
    return $.map(data.data, function (item) {
        if (item !== null) {
            return {
                id: item.productID,
                label: item.articleCode,
                description: item.description,
                modelID: item.modelID,
                modelUD: item.modelUD,
                modelNM: item.modelNM,
                qnt40HC: item.qnt40HC
            };
        }
    });
};
function formatClient(data) {
    return $.map(data.data, function (item) {
        if (item !== null) {
            return {
                id: item.clientID,
                label: item.clientUD,
                description: ''
            };
        }
    });
};
function formatProduction(data) {
    return $.map(data.data, function (item) {
        if (item !== null) {
            return {
                id: item.productionItemID,
                label: item.productionItemNM,
                description: item.productionItemUD,
                price: item.price,
                unit: item.unitNM,
                friendlyName: item.friendlyName,
                fileLocation: item.fileLocation,
                thumbnailLocation: item.thumbnailLocation,
                productionItemVNNM: item.productionItemVNNM
            };
        }
    });
};
function formatProduct(data) {
    return $.map(data.data, function (item) {
        if (item !== null) {
            return {
                id: item.productID,
                label: item.articleCode,
                description: item.description,
                modelID: item.modelID,
                modelUD: item.modelUD,
                modelNM: item.modelNM,
                qnt40HC: item.qnt40HC
            };
        }
    });
};
function formatCategory(data) {
    return $.map(data.data, function (item) {
        if (item !== null) {
            return {
                id: item.productBreakDownDefaultCategoryID,
                label: item.productBreakDownDefaultCategoryNM,
                description: '',
                productBreakDownCalculationTypeID: item.productBreakDownCalculationTypeID,
                unit: item.unit,
                unitPrice: item.unitPrice,
                optionTotalID: item.optionTotalID,
                optionToGetPriceID: item.optionToGetPriceID,
                quantityPercent: item.quantityPercent,
                percentWastage: item.percentWastage
            };
        }
    });
};

(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', productBreakDownPALController);
    productBreakDownPALController.$inject = ['$scope', '$timeout', 'dataService'];

    function productBreakDownPALController($scope, $timeout, productBreakDownPALService) {
        $scope.data = null;
        $scope.dataDetail = null;
        $scope.openSubForm = false;
        $scope.modelID = '';
        $scope.keepAmountDetail = 0;
        $scope.newID = 0;
        $scope.fileName = 'ProductBreakDownDetail_Template.xlsx';

        $scope.autocompleteModel = {
            model: {
                id: null,
                label: null,
                description: null,
                qnt40HC: null,
                productID: null,
                productArticleCode: null,
                productDescription: null
            },
            api: {
                url: context.serviceUrl + 'getmodel',
                token: context.token
            }
        };
        $scope.autocompleteSample = {
            sample: {
                id: null,
                label: null,
                description: null,
                modelID: null,
                modelUD: null,
                modelNM: null,
                qnt40HC: null,
            },
            api: {
                url: context.serviceUrl + 'getsample',
                token: context.token
            }
        };
        $scope.autocompleteClient = {
            client: {
                id: null,
                label: null,
                description: null
            },
            api: {
                url: context.serviceUrl + 'getclient',
                token: context.token
            }
        };
        $scope.autocompleteProduction = {
            productionItem: {
                id: null,
                label: null,
                description: null,
                price: null,
                unit: null,
                friendlyName: null,
                fileLocation: null,
                thumbnailLocation: null,
                productionItemVNNM: null
            },
            api: {
                url: context.serviceUrl + 'getproduction',
                token: context.token
            }
        };
        $scope.autocompleteProduct = {
            product: {
                id: null,
                label: null,
                description: null,
                modelID: null,
                modelUD: null,
                modelNM: null,
                qnt40HC: null,
            },
            api: {
                url: context.serviceUrl + 'getproduct',
                token: context.token
            }
        };
        $scope.autocompleteCategory = {
            category: {
                id: null,
                label: null,
                description: null,
                productBreakDownCalculationTypeID: null,
                unit: null,
                unitPrice: null,
                optionTotalID: null,
                optionToGetPriceID: null,
                quantityPercent: null,
                percentWastage: null
            },
            api: {
                url: context.serviceUrl + 'getcategory2',
                token: context.token
            }
        };

        $scope.support = {
            currency: [{ currencyID: 1, currencyNM: 'EUR' }, { currencyID: 2, currencyNM: 'USD' }],
            unitAmount: [
                { unitID: 1, unitNM: 'KG', typeID: 1 },
                { unitID: 2, unitNM: 'PCS', typeID: 1 },
                { unitID: 3, unitNM: 'M2', typeID: 1 },
                { unitID: 4, unitNM: 'CONT', typeID: 1 },
            ],
            unitPercent: [
                { unitID: 5, unitNM: '%', typeID: 2 }
            ]
        };

        $scope.event = {
            init: function () {
                productBreakDownPALService.serviceUrl = context.serviceUrl;
                productBreakDownPALService.token = context.token;

                $scope.event.load();
            },
            load: function () {
                productBreakDownPALService.load(
                    context.id,
                    context.modelID,
                    context.sampleProductID,
                    function (data) {
                        $scope.data = data.data.productBreakDownPAL;

                        jQuery('#content').show();
                    },
                    function (error) {
                    });
            },
            backUrl: function () {
                window.location = context.backUrl;
            },
            selectModel: function (model) {
                if (model !== null) {
                    $scope.data.modelID = model.id;
                    $scope.data.modelUD = model.label;
                    $scope.data.modelNM = model.description;
                    $scope.data.qnt40HC = model.qnt40HC;

                    $scope.modelID = model.id.toString();
                    jQuery('#supportModelID').blur();
                    $scope.$apply();
                }
            },
            selectSample: function (sample) {
                if (sample !== null) {
                    $scope.data.sampleProductID = sample.id;
                    $scope.data.sampleProductUD = sample.label;
                    $scope.data.articleDescription = sample.description;

                    if ($scope.data.modelID === null) {
                        $scope.data.modelID = sample.modelID;
                    }

                    if ($scope.data.modelUD === null || $scope.data.modelUD === '') {
                        $scope.data.modelUD = sample.modelUD;
                    }

                    if ($scope.data.modelNM === null || $scope.data.modelNM === '') {
                        $scope.data.modelNM = sample.modelNM;
                    }

                    if ($scope.data.qnt40HC === null || $scope.data.qnt40HC === '') {
                        $scope.data.qnt40HC = sample.qnt40HC;
                    }

                    jQuery('#supportSampleProductID').blur();
                    $scope.$apply();
                }
            },
            selectClient: function (client) {
                if (client !== null) {
                    $scope.data.clientID = client.id;
                    jQuery('#supportClientID').blur();
                    $scope.$apply();
                }
            },
            selectProduction: function (production, itemDetail) {
                if (production !== null) {
                    itemDetail.unitNM = production.unit;
                    itemDetail.unitPrice = production.price;
                    itemDetail.productBreakDownDetailNM = production.description;

                    itemDetail.productionItemID = production.id;
                    itemDetail.productionItemUD = production.description;
                    itemDetail.productionItemNM = production.label;
                    itemDetail.friendlyName = production.friendlyName;
                    itemDetail.fileLocation = production.fileLocation;
                    itemDetail.thumbnailLocation = production.thumbnailLocation;

                    itemDetail.productionItemVNNM = production.productionItemVNNM;
                    itemDetail.productBreakDownDetailVNNM = production.productionItemVNNM;

                    $scope.$apply();
                }
            },
            selectProduct: function (product) {
                if (product !== null) {
                    $scope.data.productID = product.id;
                    $scope.data.articleCode = product.label;
                    $scope.data.description = product.description;
                    $scope.data.modelID = product.modelID;
                    $scope.data.modelUD = product.modelUD;
                    $scope.data.modelNM = product.modelNM;
                    $scope.data.qnt40HC = product.qnt40HC;

                    //to make sure do not update product from default of model
                    $scope.data.modelDefaultOption = null;

                    jQuery('#supportProductID').blur();
                    $scope.$apply();
                }
            },
            selectCategoryAmount: function (category, item) {
                for (var i = 0; i < $scope.data.productBreakDownCategoryAmountPAL.length; i++) {
                    var itemAmount = $scope.data.productBreakDownCategoryAmountPAL[i];

                    if (itemAmount.productBreakDownCategoryNM === category.label) {
                        item.productBreakDownCategoryNM = '';
                        category.label = '';
                        category.value = '';

                        $scope.$apply();
                        return;
                    }
                }

                item.productBreakDownCategoryNM = category.label;
                item.productBreakDownCalculationTypeID = category.productBreakDownCalculationTypeID;
                item.productBreakDownCalculationTypeNM = 'Amount';
                item.quantity = 0;
                item.unitPrice = category.unitPrice;
                item.optionTotalID = category.optionTotalID;
                item.optionToGetPriceID = category.optionToGetPriceID;
                item.quantityPercent = category.quantityPercent;
                item.percentWastage = category.percentWastage;

                $scope.$apply();
            },
            selectCategoryPercent: function (category, item) {
                for (var i = 0; i < $scope.data.productBreakDownCategoryPercentPAL.length; i++) {
                    var itemPercent = $scope.data.productBreakDownCategoryPercentPAL[i];

                    if (itemPercent.productBreakDownCategoryNM === category.label) {
                        item.productBreakDownCategoryNM = '';
                        category.label = '';
                        category.value = '';

                        $scope.$apply();
                        return;
                    }
                }

                item.productBreakDownCategoryNM = category.label;
                item.productBreakDownCalculationTypeID = category.productBreakDownCalculationTypeID;
                item.productBreakDownCalculationTypeNM = 'Percent(%)';
                item.quantity = category.quantityPercent;
                item.optionTotalID = category.optionTotalID;
                item.optionToGetPriceID = category.optionToGetPriceID;
                item.quantityPercent = category.quantityPercent;

                $scope.$apply();
            },
            getSubTotalAmountPAL: function () {
                var totalAmount = 0;

                if ($scope.data === undefined || $scope.data === null) {
                    return totalAmount;
                }

                for (var i = 0; i < $scope.data.productBreakDownCategoryAmountPAL.length; i++) {
                    var amount = $scope.data.productBreakDownCategoryAmountPAL[i];

                    if (amount.amount === null || amount.amount === "") {
                        continue;
                    }

                    totalAmount = parseFloat(totalAmount) + parseFloat(amount.amount);
                }

                for (var i = 0; i < $scope.data.productBreakDownCategoryPercentPAL.length; i++) {
                    var percent = $scope.data.productBreakDownCategoryPercentPAL[i];

                    percent.unitPrice = totalAmount;
                }

                return totalAmount;
            },
            getSubTotalPercentPAL: function () {
                var totalPercent = 0;

                if ($scope.data === undefined || $scope.data === null) {
                    return totalPercent;
                }

                for (var i = 0; i < $scope.data.productBreakDownCategoryPercentPAL.length; i++) {
                    var percent = $scope.data.productBreakDownCategoryPercentPAL[i];

                    if (percent.amount === null || percent.amount === "") {
                        continue;
                    }

                    totalPercent = parseFloat(totalPercent) + parseFloat(percent.amount);
                }

                return totalPercent;
            },
            getTotalAmountPAL: function () {
                return parseFloat($scope.event.getSubTotalAmountPAL()) + parseFloat($scope.event.getSubTotalPercentPAL());
            },
            getTotalPricePAL: function () {
                var priceAmount = 0;
                var exchangeRate = 1;

                if ($scope.data === null) {
                    return 0;
                }

                if ($scope.event.getTotalAmountPAL() === null || $scope.event.getTotalAmountPAL() === 0 || $scope.data.seasonSpec === null || $scope.data.seasonSpec === 0) {
                    return 0;
                }

                if ($scope.data.exchangeRate !== null && $scope.data.exchangeRate !== '') {
                    exchangeRate = parseFloat($scope.data.exchangeRate);
                }

                return (parseFloat($scope.event.getTotalAmountPAL()) * (100 + parseFloat($scope.data.seasonSpec)) / 100);
            },
            getTotalAmountQntPAL: function () {
                var totalAmountQnt = 0;

                if ($scope.data === undefined || $scope.data === null) {
                    return totalAmountQnt;
                }

                for (var i = 0; i < $scope.data.productBreakDownCategoryAmountPAL.length; i++) {
                    var amount = $scope.data.productBreakDownCategoryAmountPAL[i];

                    if (amount.quantity === null || amount.quantity === "") {
                        continue;
                    }

                    totalAmountQnt = parseFloat(totalAmountQnt) + parseFloat(amount.quantity);
                }

                return totalAmountQnt;
            },
            getTotalAmountPricePAL: function () {
                var totalAmountPrice = 0;

                if ($scope.data === undefined || $scope.data === null) {
                    return totalAmountPrice;
                }

                for (var i = 0; i < $scope.data.productBreakDownCategoryAmountPAL.length; i++) {
                    var amount = $scope.data.productBreakDownCategoryAmountPAL[i];

                    if (amount.unitPrice === null || amount.unitPrice === "") {
                        continue;
                    }

                    totalAmountPrice = parseFloat(totalAmountPrice) + parseFloat(amount.unitPrice);
                }

                return totalAmountPrice;
            },
            getTotalAmountDetailPAL: function () {
                var totalAmountDetail = 0;

                if ($scope.data === undefined || $scope.data === null) {
                    return totalAmountDetail;
                }

                for (var i = 0; i < $scope.data.productBreakDownCategoryAmountPAL.length; i++) {
                    var amount = $scope.data.productBreakDownCategoryAmountPAL[i];

                    if (amount.amount === null || amount.amount === "") {
                        continue;
                    }

                    totalAmountDetail = parseFloat(totalAmountDetail) + parseFloat(amount.amount);
                }

                for (var i = 0; i < $scope.data.productBreakDownCategoryPercentPAL.length; i++) {
                    var percent = $scope.data.productBreakDownCategoryPercentPAL[i];
                    percent.unitPrice = totalAmountDetail;
                }

                return totalAmountDetail;
            },
            getTotalPercentQntPAL: function () {
                var totalPercentQnt = 0;

                if ($scope.data === undefined || $scope.data === null) {
                    return totalPercentQnt;
                }

                for (var i = 0; i < $scope.data.productBreakDownCategoryPercentPAL.length; i++) {
                    var percent = $scope.data.productBreakDownCategoryPercentPAL[i];

                    if (percent.quantity === null || percent.quantity === "") {
                        continue;
                    }

                    totalPercentQnt = parseFloat(totalPercentQnt) + parseFloat(percent.quantity);
                }

                return totalPercentQnt;
            },
            getTotalPercentDetailPAL: function () {
                var totalPercentDetail = 0;

                if ($scope.data === undefined || $scope.data === null) {
                    return totalPercentDetail;
                }

                for (var i = 0; i < $scope.data.productBreakDownCategoryPercentPAL.length; i++) {
                    var percent = $scope.data.productBreakDownCategoryPercentPAL[i];

                    if (percent.amount === null || percent.amount === "") {
                        continue;
                    }

                    totalPercentDetail = parseFloat(totalPercentDetail) + parseFloat(percent.amount);
                }

                return totalPercentDetail;
            },
            addCategoryAmount: function () {
                var newItem = {
                    productBreakDownCategoryID: $scope.method.getNewID(),
                    productBreakDownCategoryNM: null,
                    quantity: null,
                    unitPrice: null,
                    amount: null,
                    remark: null,
                    productBreakDownCalculationTypeID: 1,
                    productBreakDownCalculationTypeNM: 'Amount',
                    productBreakDownCategoryImagePAL: [],
                    productBreakDownCategoryTypePAL: [],
                    displayOrder: $scope.data.productBreakDownCategoryAmountPAL.length + 1,
                    isSave: false
                };

                $scope.data.productBreakDownCategoryAmountPAL.push(newItem);
                $scope.totalRowsAmount = $scope.totalRowsAmount + 1;
            },
            addCategoryPercent: function () {
                var newItem = {
                    productBreakDownCategoryID: $scope.method.getNewID(),
                    productBreakDownCategoryNM: null,
                    quantity: null,
                    unitPrice: null,
                    amount: null,
                    remark: null,
                    productBreakDownCalculationTypeID: 2,
                    productBreakDownCalculationTypeNM: 'Percent(%)',
                    productBreakDownCategoryImagePAL: [],
                    productBreakDownCategoryTypePAL: [],
                    displayOrder: $scope.data.productBreakDownCategoryPercentPAL.length + 1,
                    isSave: false
                };

                $scope.data.productBreakDownCategoryPercentPAL.push(newItem);
                $scope.totalRowsPercent = $scope.totalRowsPercent + 1;
            },
            removeCategoryAmount: function (amount) {
                var index = $scope.data.productBreakDownCategoryAmountPAL.indexOf(amount);

                $scope.data.productBreakDownCategoryAmountPAL.splice(index, 1);
                $scope.totalRowsAmount = $scope.data.productBreakDownCategoryAmountPAL.length;

                var amountindex = 0;
                for (var i = 0; i < $scope.data.productBreakDownCategoryAmountPAL.length; i++) {
                    var amount = $scope.data.productBreakDownCategoryAmountPAL[i];
                    amountindex = amountindex + 1;
                    amount.index = amountindex;
                }
            },
            removeCategoryPercent: function (percent) {
                var index = $scope.data.productBreakDownCategoryPercentPAL.indexOf(percent);

                $scope.data.productBreakDownCategoryPercentPAL.splice(index, 1);
                $scope.totalRowsPercent = $scope.data.productBreakDownCategoryPercentPAL.length;

                var percentindex = 0;
                for (var i = 0; i < $scope.data.productBreakDownCategoryPercentPAL.length; i++) {
                    var percent = $scope.data.productBreakDownCategoryPercentPAL[i];
                    percentindex = percentindex + 1;
                    percent.index = percentindex;
                }
            },
            fillCategoryAmount: function () {
                if ($scope.data.productBreakDownCategoryAmountPAL.length > 0) {
                    bootbox.confirm({
                        message: 'Do you want to delete data ready?',
                        buttons: {
                            confirm: {
                                label: 'Yes',
                                className: 'btn-success'
                            },
                            cancel: {
                                label: 'No',
                                className: 'btn-danger'
                            }
                        },
                        callback: function (result) {
                            if (result) {
                                $scope.method.fillCategoryAmount();
                            }
                        }
                    })
                } else {
                    $scope.method.fillCategoryAmount();
                }
            },
            fillCategoryPercent: function () {
                if ($scope.data.productBreakDownCategoryPercentPAL.length > 0) {
                    bootbox.confirm({
                        message: 'Do you want to delete data ready?',
                        buttons: {
                            confirm: {
                                label: 'Yes',
                                className: 'btn-success'
                            },
                            cancel: {
                                label: 'No',
                                className: 'btn-danger'
                            }
                        },
                        callback: function (result) {
                            if (result) {
                                $scope.method.fillCategoryPercent();
                            }
                        }
                    })
                } else {
                    $scope.method.fillCategoryPercent();
                }
            },
            getTotalAmountPAL2: function (item, typeID) {
                if (typeID == 2) {
                    if (item.quantityPercent === null || item.quantityPercent === "") {
                        return item.amount = null;
                    }

                    if (item.unitPrice === null || item.unitPrice === "") {
                        return item.amount = null;
                    }

                    return item.amount = parseFloat(item.quantityPercent) * parseFloat(item.unitPrice);
                } else {
                    if (item.quantity === null || item.quantity === "") {
                        return item.amount = null;
                    }

                    if (item.unitPrice === null || item.unitPrice === "") {
                        return item.amount = null;
                    }

                    return item.amount = parseFloat(item.quantity) * parseFloat(item.unitPrice);
                }
            },
            addImage: function (item) {
                var newImage = {
                    productBreakDownCategoryImageID: $scope.method.getNewID(),
                    productBreakDownCategoryID: item.productBreakDownCategoryID,
                    fileUD: null,
                    friendlyName: null,
                    fileLocation: null,
                    thumbnailLocation: null,
                    hasChange: true,
                    newFile: null
                };

                item.productBreakDownCategoryImagePAL.push(newImage);
            },
            downloadImage: function (item) {
                if (item.fileLocation) {
                    window.open(item.fileLocation);
                }
            },
            changeImage: function (item) {
                fileUploader2.callback = function () {
                    scope.$apply(function () {
                        item.friendlyName = fileUploader2.selectedFriendlyName;
                        item.fileLocation = fileUploader2.selectedFileUrl;
                        item.newFile = fileUploader2.selectedFileName;
                        item.hasChange = true;
                    });
                };

                fileUploader2.open();
            },
            removeImage: function (item, list) {
                var index = list.indexOf(item);
                list.splice(index, 1);
            },
            getFormDetail: function (item) {
                $scope.dataDetail = item;
                if ($scope.dataDetail.productBreakDownCategoryTypePAL.length > 0) {
                    $scope.dataDetail.isSave = true;
                }
                $scope.openSubForm = true;
            },
            addCategoryTypePAL: function () {
                var newItem = {
                    productBreakDownCategoryTypeID: $scope.method.getNewID(),
                    productBreakDownCategoryID: $scope.dataDetail.productBreakDownCategoryID,
                    productBreakDownCategoryTypeNM: null,
                    productBreakDownDetailPAL: [],
                }

                $scope.dataDetail.productBreakDownCategoryTypePAL.unshift(newItem);
                $scope.event.addDetailPAL(newItem);
            },
            removeCategoryTypePAL: function (removeItem) {
                var removeIndex = $scope.dataDetail.productBreakDownCategoryTypePAL.indexOf(removeItem);
                $scope.dataDetail.productBreakDownCategoryTypePAL.splice(removeIndex, 1);
            },
            downloadTemplateDetailPAL: function () {
                window.open(context.servicePath + $scope.fileName);
            },
            uploadTemplateDetailPAL: function (detail) {
                if (detail.productBreakDownCategoryTypePAL.length > 0) {
                    bootbox.confirm({
                        message: 'Do you want to delete data ready?',
                        buttons: {
                            confirm: {
                                label: 'Yes',
                                className: 'btn-success'
                            },
                            cancel: {
                                label: 'No',
                                className: 'btn-danger'
                            }
                        },
                        callback: function (result) {
                            if (result) {
                                $scope.method.uploadTemplateDetail(detail);
                            }
                        }
                    })
                } else {
                    $scope.method.uploadTemplateDetail(detail);
                }
            },
            saveFormDetail: function (detailData) {
                $scope.openSubForm = false;

                detailData.isSave = true;

                var totalQuantity = 0;
                var totalAmount = 0;
                // reset value to calculate
                $scope.keepAmountDetail = 0;

                var typeToGet = detailData.optionTotalID;
                var typeToPrice = detailData.optionToGetPriceID;

                for (var i = 0; i < detailData.productBreakDownCategoryTypePAL.length; i++) {
                    var categoryType = detailData.productBreakDownCategoryTypePAL[i];

                    for (var j = 0; j < categoryType.productBreakDownDetailPAL.length; j++) {
                        var detail = categoryType.productBreakDownDetailPAL[j];

                        if (typeToGet === 1 || typeToPrice !== null) {
                            if (detail.amount !== null && detail.amount !== "") {
                                totalQuantity = parseFloat(totalQuantity) + parseFloat(detail.amount);
                            }
                        } else if (typeToGet === 2) {
                            if (detail.weight !== null && detail.weight !== "") {
                                totalQuantity = parseFloat(totalQuantity) + parseFloat(detail.weight);
                            }
                        } else if (typeToGet === 3) {
                            if (detail.volume !== null && detail.volume !== "") {
                                totalQuantity = parseFloat(totalQuantity) + parseFloat(detail.volume);
                            }
                        } else if (typeToGet === 4) {
                            if (detail.acreage !== null && detail.acreage !== "") {
                                totalQuantity = parseFloat(totalQuantity) + parseFloat(detail.acreage);
                            }
                        }

                        // get total amount set unit price out-side.
                        $scope.keepAmountDetail = $scope.keepAmountDetail + parseFloat(detail.amount);
                    }
                }

                if (detailData.isSave) {
                    detailData.unitPrice = $scope.keepAmountDetail;
                }

                if (totalQuantity !== 0) {
                    if (detailData.quantity === null || detailData.quantity === "") {
                        if (detailData.optionTotalID !== null) {
                            detailData.quantity = totalQuantity;
                        }

                        if (detailData.optionToGetPriceID !== null) {
                            detailData.unitPrice = totalQuantity;
                        }
                    }
                }

                $scope.event.getTotalAmountPAL2(detailData);
            },
            addDetailPAL: function (dataCategoryType) {
                var newDetail = {
                    productBreakDownDetailID: $scope.method.getNewID(),
                    productBreakDownCategoryTypeID: dataCategoryType.productBreakDownCategoryTypeID,
                    productBreakDownDetailNM: null,
                    quantity: null,
                    unitPrice: null,
                    amount: null,
                    dimensionL: null,
                    dimensionW: null,
                    dimensionH: null,
                    profile: null,
                    weight: null,
                    volume: null,
                    acreage: null,
                    remark: null,
                    index: null,
                    unit: null,
                    displayOrder: dataCategoryType.productBreakDownDetailPAL.length + 1,

                    productionItemID: null,
                    productionItemUD: null,
                    productionItemNM: null,
                    unitNM: null,
                    friendlyName: null,
                    fileLocation: null,
                    thumbnailLocation: null
                }

                dataCategoryType.productBreakDownDetailPAL.push(newDetail);
            },
            removeDetailPAL: function (dataCategoryType, detailItem) {
                var index = dataCategoryType.productBreakDownDetailPAL.indexOf(detailItem);
                dataCategoryType.productBreakDownDetailPAL.splice(index, 1);
            },
            getAmountPAL: function (detailData, categoryType) {
                
                detailData.unitPrice = (detailData.unitPrice !== undefined && detailData.unitPrice !== null && detailData.unitPrice !== "") ? detailData.unitPrice : null;
                detailData.quantity = (detailData.quantity !== undefined && detailData.quantity !== null && detailData.quantity !== "") ? detailData.quantity : null;

                if ($scope.data.qnt40HC != null && $scope.data.qnt40HC != '' && $scope.data.qnt40HC != 0 && detailData.unitID == 4) {
                    if (detailData.unitPrice != null) {
                        detailData.unitPrice = parseFloat(detailData.unitPrice) / parseFloat($scope.data.qnt40HC);
                    }
                }

                if (detailData.unitPrice !== null && detailData.quantity !== null) {
                    detailData.amount = parseFloat(detailData.unitPrice) * parseFloat(detailData.quantity);
                }

                $scope.method.getTotalPriceDetailPAL(categoryType);
                $scope.method.getTotalQntDetailPAL(categoryType);
                $scope.method.getTotalAmountDetailPAL(categoryType);
            },
            update: function () {
                if ($scope.data.productBreakDownCategoryPAL === null) {
                    $scope.data.productBreakDownCategoryPAL = [];
                }

                if ($scope.data.productBreakDownCategoryAmountPAL.length > 0) {
                    for (var i = 0; i < $scope.data.productBreakDownCategoryAmountPAL.length; i++) {
                        var item = $scope.data.productBreakDownCategoryAmountPAL[i];
                        $scope.data.productBreakDownCategoryPAL.push(item);
                    }
                }

                if ($scope.data.productBreakDownCategoryPercentPAL.length > 0) {
                    for (i = 0; i < $scope.data.productBreakDownCategoryPercentPAL.length; i++) {
                        item = $scope.data.productBreakDownCategoryPercentPAL[i];
                        $scope.data.productBreakDownCategoryPAL.push(item);
                    }
                }

                productBreakDownPALService.update(
                    context.id,
                    $scope.data,
                    function (data) {
                        if (data.message.type === 0) {
                            jsHelper.processMessage(data.message);
                            window.location = context.refreshUrl + data.data.productBreakDownPAL.productBreakDownID;
                        }
                    },
                    function (error) {
                    });
            },
            delete: function () {
                productBreakDownPALService.delete(
                    $scope.data.productBreakDownID,
                    null,
                    function (data) {
                        if (data.message.type === 0) {
                            jsHelper.processMessage(data.message);
                            window.location = context.backUrl;
                        }
                    },
                    function (error) {
                    });
            },
            changeModel: function () {
                if ($scope.data.modelID !== null) {
                    $scope.data.modelID = null;
                    $scope.data.modelNM = null;

                    if ($scope.data.productID !== null) {
                        $scope.data.productID = null;
                        $scope.data.articleCode = null;
                        $scope.data.description = null;
                    }

                    $scope.modelID = '';
                }

                if ($scope.data.modelUD === '') {
                    $scope.data.modelUD = null;
                }
            },
            changeSample: function () {
                if ($scope.data.sampleProductID !== null) {
                    $scope.data.sampleProductID = null;
                    $scope.data.articleDescription = null;
                    $scope.data.modelID = null;
                    $scope.data.modelNM = null;
                    $scope.data.modelUD = null;
                }

                if ($scope.data.sampleProductUD === '') {
                    $scope.data.sampleProductUD = null;
                }
            },
            changeClient: function () {
                if ($scope.data.clientID !== null) {
                    $scope.data.clientID = null;
                }

                if ($scope.data.clientUD === '') {
                    $scope.data.clientUD = null;
                }
            },
            changeProduct: function () {
                if ($scope.data.productID !== null) {
                    $scope.data.productID = null;
                    $scope.data.description = null;
                }

                if ($scope.data.articleCode === '') {
                    $scope.data.articleCode = null;
                }
            },
            approve: function () {
                productBreakDownPALService.approve(
                    $scope.data.productBreakDownID,
                    $scope.data,
                    function (data) {
                        if (data.message.type === 0) {
                            jsHelper.processMessage(data.message);
                            window.location = context.viewUrl + data.data.productBreakDownPAL.productBreakDownID;
                        } else if (data.message.type === 1) {
                            jsHelper.showMessage('warning', data.message.message);
                        } else if (data.message.type === 2) {
                            jsHelper.showMessage('warning', data.message.message);
                        }
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error.data.exceptionMessage);
                    });
            },
            closeFormDetail: function (detailData) {
                $scope.openSubForm = false;
                if (!detailData.isSave) {
                    detailData.productBreakDownCategoryTypePAL = [];
                }
            },
            changeCurrency: function (id) {
                if (id !== undefined && id !== null) {
                    for (var i = 0; i < $scope.support.currency.length; i++) {
                        if ($scope.support.currency[i].currencyID === id) {
                            return $scope.data.currencyNM = $scope.support.currency[i].currencyNM;
                        }
                    }
                }
            },
            getTotalPricePALExchange: function () {
                var priceAmount = parseFloat($scope.event.getTotalPricePAL());
                var exchangeRate = 1;

                if ($scope.data !== null && $scope.data.exchangeRate !== undefined && $scope.data.exchangeRate !== null && $scope.data.exchangeRate !== '') {
                    exchangeRate = parseFloat($scope.data.exchangeRate);
                    return priceAmount / exchangeRate;
                }
            },
            getTotalAmountWithExchangeRate: function (exchangeRate) {
                var totalAmount = $scope.event.getTotalAmountDetailPAL();
                return totalAmount / parseFloat(exchangeRate);
            },
            getTotalPercentWithExchangeRate: function (exchangeRate) {
                var totalPercent = $scope.event.getTotalPercentDetailPAL();
                return totalPercent / parseFloat(exchangeRate);
            },
            getTotalAmountWithUSD: function () {
                if ($scope.data != null) {
                    var totalAmountVND = parseFloat($scope.event.getSubTotalAmountPAL()) + parseFloat($scope.event.getSubTotalPercentPAL());
                    if ($scope.data.exchangeRate == null || $scope.data.exchangeRate == '') {
                        $scope.data.exchangeRate = 1;
                    }
                    return totalAmountVND / $scope.data.exchangeRate;
                }
            },
            copy: function () {
                window.open(context.copyUrl + 0 + '?oldID=' + $scope.data.productBreakDownID, '_blank');
            },

            changeProductBreakDownDetailNM(detailItem) {
                if (detailItem.productBreakDownDetailNM != $scope.autocompleteProduction.productionItem.label) {
                    detailItem.productionItemID = null;
                    detailItem.thumbnailLocation = null;
                    detailItem.fileLocation = null;
                }
            },

            getModelDefaultOption: function (modelID) {
                //get model default option
                productBreakDownPALService.getModelDefaultOption(
                    modelID,
                    function (data) {
                        $scope.data.modelDefaultOption = data.data;

                        //show default info
                        $scope.data.articleCode = data.data.articleCode;
                        $scope.data.description = data.data.description;                        
                    },
                    function (error) {
                        $scope.data.modelDefaultOption = null;
                    }
                );
            }
        };

        $scope.method = {
            getNewID: function () {
                return ($scope.newID - 1);
            },
            fillCategoryAmount: function () {
                $scope.data.productBreakDownCategoryAmountPAL = [];

                productBreakDownPALService.fill(
                    1,
                    function (data) {
                        var amountindex = 0;

                        for (var i = 0; i < data.data.length; i++) {
                            var item = data.data[i];
                            if (item.unitPrice == null || item.unitPrice == '' || item.unitPrice == 0) {
                                continue;
                            }

                            item.productBreakDownCategoryID = $scope.method.getNewID();

                            amountindex = amountindex + 1;
                            item.displayOrder = amountindex;

                            $scope.data.productBreakDownCategoryAmountPAL.push(item);

                            var index = $scope.data.productBreakDownCategoryAmountPAL.indexOf(item);
                            $scope.data.productBreakDownCategoryAmountPAL[index].quantity = item.quantityPercent;
                        }

                        $scope.totalRowsAmount = $scope.data.productBreakDownCategoryAmountPAL.length;
                    },
                    function (error) {
                    });
            },
            fillCategoryPercent: function () {
                $scope.data.productBreakDownCategoryPercentPAL = [];

                productBreakDownPALService.fill(
                    2,
                    function (data) {
                        var percentindex = 0;

                        for (var i = 0; i < data.data.length; i++) {
                            var item = data.data[i];

                            item.productBreakDownCategoryID = $scope.method.getNewID();

                            percentindex = percentindex + 1;
                            item.displayOrder = percentindex;

                            item.unitPrice = null;
                            var totalAmountPAL = $scope.event.getTotalAmountDetailPAL();
                            if (totalAmountPAL !== null && totalAmountPAL !== "") {
                                item.unitPrice = totalAmountPAL;
                            }

                            if (item.quantity === null || item.quantity === '') {
                                item.quantity = 0;
                            }

                            if (item.unitPrice === null || item.unitPrice === '') {
                                item.unitPrice = 0;
                            }

                            item.amount = parseFloat(item.quantity) * parseFloat(item.unitPrice);

                            $scope.data.productBreakDownCategoryPercentPAL.push(item);

                            var index = $scope.data.productBreakDownCategoryPercentPAL.indexOf(item);
                            $scope.data.productBreakDownCategoryPercentPAL[index].quantity = item.quantityPercent;
                        }

                        $scope.totalRowsPercent = $scope.data.productBreakDownCategoryPercentPAL.length;
                    },
                    function (error) {
                    });
            },
            uploadTemplateDetail: function (detail) {
                detail.productBreakDownCategoryTypePAL = [];

                var input = document.createElement("input");

                input.setAttribute("type", "file");
                input.setAttribute("accept", ".xlsx");

                input.addEventListener("change", function (e) {
                    //get file content
                    var file = e.target.files[0];
                    //
                    var f = e.target.files[0];
                    var reader = new FileReader();
                    //var name = f.name;
                    reader.onload = function (e) {
                        var data = e.target.result;

                        var result;
                        var workbook = XLSX.read(data, { type: 'binary' });

                        var sheet_name_list = workbook.SheetNames;
                        sheet_name_list.forEach(function (y) { /* iterate through sheets */
                            //Convert the cell value to Json
                            var roa = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                            if (roa.length > 0) {
                                result = roa;
                            }
                        });

                        var productBreakDownDetailID;
                        var index;

                        for (var i = 0; i < result.length; i++) {
                            if (i >= 0) {
                                var ele = result[i];

                                // category type
                                if (ele.No === undefined || ele.No === null) {
                                    var newCategoryType = {
                                        productBreakDownCategoryTypeID: $scope.method.getNewID(),
                                        productBreakDownCategoryID: detail.productBreakDownCategoryID,
                                        productBreakDownCategoryTypeNM: (ele.ProductBreakDownDetailName === undefined || ele.ProductBreakDownDetailName === null) ? '' : ele.ProductBreakDownDetailName,
                                        productBreakDownDetailPAL: [],
                                    }

                                    productBreakDownDetailID = newCategoryType.productBreakDownCategoryTypeID;

                                    detail.productBreakDownCategoryTypePAL.push(newCategoryType);

                                    index = detail.productBreakDownCategoryTypePAL.indexOf(newCategoryType);
                                } else {
                                    var newDetail = {
                                        productBreakDownDetailID: $scope.method.getNewID(),
                                        productBreakDownCategoryTypeID: productBreakDownDetailID,
                                        productBreakDownDetailNM: (ele.ProductBreakDownDetailName === undefined || ele.ProductBreakDownDetailName === null) ? '' : ele.ProductBreakDownDetailName,
                                        quantity: $scope.method.convertFloat(ele.Quantity),
                                        unitPrice: $scope.method.convertFloat(ele.UnitPrice),
                                        amount: $scope.method.convertFloat(ele.Quantity) * $scope.method.convertFloat(ele.UnitPrice),
                                        dimensionL: $scope.method.convertFloat(ele.DimensionL),
                                        dimensionW: $scope.method.convertFloat(ele.DimensionW),
                                        dimensionH: $scope.method.convertFloat(ele.DimensionH),
                                        profile: (ele.Profile === undefined || ele.Profile === null) ? '' : ele.Profile,
                                        weight: $scope.method.convertFloat(ele.Weight),
                                        volume: $scope.method.convertFloat(ele.Volume),
                                        acreage: $scope.method.convertFloat(ele.Acreage),
                                        remark: (ele.Remark === undefined || ele.Remark === null) ? '' : ele.Remark,
                                        displayOrder: (ele.No === undefined || ele.No === null) ? '0' : ele.No,
                                        unit: (ele.Unit === undefined || ele.Unit === null) ? '' : ele.Unit
                                    }

                                    detail.productBreakDownCategoryTypePAL[index].productBreakDownDetailPAL.push(newDetail);
                                }

                                $scope.$apply();
                            }
                        }
                    };

                    reader.readAsArrayBuffer(f);
                }, false);

                input.click();
            },
            convertFloat: function (data) {
                if (data !== undefined) {
                    var convertTemp = data.replace(',', '');
                    var parseString = parseFloat(data);

                    if (parseString.toString() === 'NaN') {
                        return null;
                    }

                    return parseString;
                } else {
                    return null;
                }
            },
            getTotalQntDetailPAL: function (categoryType) {
                var totalQuantity = 0;

                for (var i = 0; i < categoryType.productBreakDownDetailPAL.length; i++) {
                    var itemDetail = categoryType.productBreakDownDetailPAL[i];

                    if (itemDetail.quantity !== null && itemDetail.quantity !== "") {
                        totalQuantity = parseFloat(totalQuantity) + parseFloat(itemDetail.quantity);
                    }
                }

                return (totalQuantity === 0) ? null : totalQuantity;
            },
            getTotalPriceDetailPAL: function (categoryType) {
                var totalPrice = 0;

                for (var i = 0; i < categoryType.productBreakDownDetailPAL.length; i++) {
                    var itemDetail = categoryType.productBreakDownDetailPAL[i];

                    if (itemDetail.unitPrice !== null && itemDetail.unitPrice !== "") {
                        totalPrice = parseFloat(totalPrice) + parseFloat(itemDetail.unitPrice);
                    }
                }

                return (totalPrice === 0) ? null : totalPrice;
            },
            getTotalAmountDetailPAL: function (categoryType) {
                var totalAmount = 0;
                var isNotNull = false;

                for (var i = 0; i < categoryType.productBreakDownDetailPAL.length; i++) {
                    var itemDetail = categoryType.productBreakDownDetailPAL[i];

                    if (itemDetail.amount !== null && itemDetail.amount !== "") {
                        isNotNull = true;
                        totalAmount = parseFloat(totalAmount) + parseFloat(itemDetail.amount);
                    }
                }

                if (totalAmount == 0) {
                    return null;
                }

                return totalAmount;
            },
            getTotalAmountDetailPALUSD: function (categoryType) {
                if ($scope.data != null) {
                    var totalAmountVND = $scope.method.getTotalAmountDetailPAL(categoryType);

                    if ($scope.data.exchangeRate == null || $scope.data.exchangeRate == '') {
                        $scope.data.exchangeRate = 1;
                    }

                    if (totalAmountVND == null || totalAmountVND == 0) {
                        return null;
                    }

                    return totalAmountVND / $scope.data.exchangeRate;
                }
            },
            getTotalWeightDetailPAL: function (categoryType) {
                var totalWeight = 0;

                for (var i = 0; i < categoryType.productBreakDownDetailPAL.length; i++) {
                    var itemDetail = categoryType.productBreakDownDetailPAL[i];

                    if (itemDetail.weight !== null && itemDetail.weight !== "") {
                        totalWeight = parseFloat(totalWeight) + parseFloat(itemDetail.weight);
                    }
                }

                return (totalWeight === 0) ? null : totalWeight;
            },
            getTotalVolumneDetailPAL: function (categoryType) {
                var totalVolumne = 0;

                for (var i = 0; i < categoryType.productBreakDownDetailPAL.length; i++) {
                    var itemDetail = categoryType.productBreakDownDetailPAL[i];

                    if (itemDetail.volume !== null && itemDetail.volume !== "") {
                        totalVolumne = parseFloat(totalVolumne) + parseFloat(itemDetail.volume);
                    }
                }

                return (totalVolumne === 0) ? null : totalVolumne;
            },
            getTotalAcreageDetailPAL: function (categoryType) {
                var totalAcreage = 0;

                for (var i = 0; i < categoryType.productBreakDownDetailPAL.length; i++) {
                    var itemDetail = categoryType.productBreakDownDetailPAL[i];

                    if (itemDetail.acreage !== null && itemDetail.acreage !== "") {
                        totalAcreage = parseFloat(totalAcreage) + parseFloat(itemDetail.acreage);
                    }
                }

                return (totalAcreage === 0) ? null : totalAcreage;
            },
            getBaseAmount(item, typeID) {
                item.quantity = 1;
                if (typeID == 2) {
                    item.quantity = item.quantityPercent;
                }

                if (item.unitPrice === null || item.unitPrice === '') {
                    item.unitPrice = 0;
                }

                item.amount = parseFloat(item.quantity) * parseFloat(item.unitPrice);

                if (typeID === 1) {
                    if (item.percentWastage !== undefined && item.percentWastage !== null) {
                        item.amount = parseFloat(item.quantity) * parseFloat(item.unitPrice) * (1 + parseFloat(item.percentWastage) / 100);
                    }
                }

                if (typeID === 2) {
                    item.amount = item.amount / 100;
                }

                return item.amount;
            },
            getBaseAmountWithExchangeRate(item, typeID, exchangeRate) {
                var amountWithType = $scope.method.getBaseAmount(item, typeID);

                return amountWithType / parseFloat(exchangeRate);
            },
            getPriceUSDInDetailMaterialItem: function (detailItem, exchangeRate) {
                if (detailItem.unitPrice != null && detailItem.unitPrice != '') {
                    var result = parseFloat(detailItem.unitPrice) / parseFloat(exchangeRate);
                    return result;
                }
                return null;
            }
        };

        $timeout(function () {
            $scope.event.init();
        }, 0);
    };
})();