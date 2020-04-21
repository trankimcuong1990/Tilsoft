function formatData(data) {
    return $.map(data.data, function (item) {
        if (item !== null) {
            return {
                id: item.id,
                label: item.value,
                description: item.label,

                modelID: item.modelID,
                modelUD: item.modelUD,
                modelNM: item.modelNM,

                factoryID: item.factoryID,
                factoryUD: item.factoryUD,
                factoryNM: item.factoryName
            }
        }
    });
}

function formatClient(data) {
    return $.map(data.data, function (item) {
        if (item !== null) {
            return {
                id: item.clientID,
                label: item.clientUD,
                description: ''
            }
        }
    });
};

(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', productBreakDownController);
    productBreakDownController.$inject = ['$scope', '$timeout', 'dataService'];

    function productBreakDownController($scope, $timeout, productBreakDownService) {
        $scope.data = null;
        $scope.productBreakDownCategoryTypes = [];

        $scope.dataDetail = null;
        $scope.productBreakDownCategoryType = [];

        $scope.fileName = 'ProductBreakDownDetail_Template.xlsx';

        $scope.headerFactory = [];
        $scope.headerTotal = [];

        $scope.newID = 0;
        $scope.newDetailID = 0;
        $scope.totalRowsAmount = 0;
        $scope.totalRowsPercent = 0;
        $scope.totalDetailRows = 0;

        $scope.totalQnt = 0;
        $scope.totalUnitPrice = 0;
        $scope.totalAmount = 0;

        $scope.subTotalAmount = 0;
        $scope.subTotalPercent = 0;

        $scope.width = 1645;
        $scope.openSubForm = false;

        $scope.gridHandler = {
            loadMore: function () {
            },
            sort: function (sortedBy, sortedDirection) {
            },
            isTriggered: false
        };

        $scope.event = {
            init: init,
            load: load,
            updated: updated,
            deleted: deleted,
            getProductBreakDownDetail: getProductBreakDownDetail,

            addProductBreakDownDetail: addProductBreakDownDetail,
            removeProductBreakDownDetail: removeProductBreakDownDetail,
            downloadProductBreakDownDetailTemplate: downloadProductBreakDownDetailTemplate,
            uploadProductBreakDownDetailTemplate: uploadProductBreakDownDetailTemplate,

            selectModel: selectModel,

            approve: function () {
                productBreakDownService.approve(
                    context.id,
                    $scope.data,
                    function (data) {
                        window.location = context.refreshUrl + data.data.productBreakDownID;
                        $scope.data = data.data;
                    },
                    function (error) {
                        // do nothing
                    })
            },

            selectSampleProduct: selectSampleProduct,

            addProductBreakDownCategoryAmount: addProductBreakDownCategoryAmount,
            removeProductBreakDownCategoryAmount: removeProductBreakDownCategoryAmount,
            fillProductBreakDownCategoryAmount: fillProductBreakDownCategoryAmount,

            addProductBreakDownCategoryPercent: addProductBreakDownCategoryPercent,
            removeProductBreakDownCategoryPercent: removeProductBreakDownCategoryPercent,
            fillProductBreakDownCategoryPercent: fillProductBreakDownCategoryPercent,

            changeImageFile: changeImageFile,
            removeImageFile: removeImageFile,

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

                item.productBreakDownCategoryImage.push(newImage);
            },
            changeImage: function (image) {
                fileUploader2.callback = function () {
                    scope.$apply(function () {
                        image.friendlyName = fileUploader2.selectedFriendlyName;
                        image.fileLocation = fileUploader2.selectedFileUrl;
                        image.newFile = fileUploader2.selectedFileName;
                        image.hasChange = true;
                    });
                };

                fileUploader2.open();
            },
            removeImage: function (image, list) {
                var index = list.indexOf(image);
                list.splice(index, 1);
            },
            downloadImage: function (image) {
                if (image.fileLocation) {
                    window.open(image.fileLocation);
                }
            },

            addProductBreakDownCategoryType: function () {
                var newItem = {
                    productBreakDownCategoryTypeID: $scope.method.getNewID(),
                    productBreakDownCategoryID: $scope.dataDetail.productBreakDownCategoryID,
                    productBreakDownCategoryTypeNM: null,
                    productBreakDownDetail: [],
                }

                $scope.dataDetail.productBreakDownCategoryType.unshift(newItem);
            },
            removeProductBreakDownCategoryType: function (removeItem) {
                var removeIndex = $scope.dataDetail.productBreakDownCategoryType.indexOf(removeItem);
                $scope.dataDetail.productBreakDownCategoryType.splice(removeIndex, 1);
            },

            addProductBreakDownDetail: function (dataCategoryType) {
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
                    displayOrder: dataCategoryType.productBreakDownDetail.length + 1
                }

                dataCategoryType.productBreakDownDetail.push(newDetail);
            },
            removeProductBreakDownDetail: function (dataCategoryType, detailItem) {
                var index = dataCategoryType.productBreakDownDetail.indexOf(detailItem);
                dataCategoryType.productBreakDownDetail.splice(index, 1);
            },

            getAmount: function (detailData, categoryType) {
                detailData.unitPrice = (detailData.unitPrice !== undefined && detailData.unitPrice !== null && detailData.unitPrice !== "") ? detailData.unitPrice : null;
                detailData.quantity = (detailData.quantity !== undefined && detailData.quantity !== null && detailData.quantity !== "") ? detailData.quantity : null;

                if (detailData.unitPrice !== null && detailData.quantity !== null) {
                    detailData.amount = parseFloat(detailData.unitPrice) * parseFloat(detailData.quantity);
                }

                $scope.method.getTotalPriceDetail(categoryType);
                $scope.method.getTotalQuantityDetail(categoryType);
                $scope.method.getTotalAmountDetail(categoryType);
            },

            // Issues 1360: Add features
            closeProductBreakDownDetail: function (detailData) {
                $scope.openSubForm = false;

                var totalQuantity = 0;
                var totalAmount = 0;

                var typeToGet = detailData.optionTotalID;
                var typeToPrice = detailData.optionToGetPriceID;

                for (var i = 0; i < detailData.productBreakDownCategoryType.length; i++) {
                    var categoryType = detailData.productBreakDownCategoryType[i];

                    for (var j = 0; j < categoryType.productBreakDownDetail.length; j++) {
                        var detail = categoryType.productBreakDownDetail[j];

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
                    }
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

                $scope.event.getAmountForGroupAmount(detailData);
            },

            getAmountForGroupAmount: function (amount) {
                if (amount.quantity === null || amount.quantity === "") {
                    return amount.amount = null;
                }

                if (amount.unitPrice === null || amount.unitPrice === "") {
                    return amount.amount = null;
                }

                return amount.amount = parseFloat(amount.quantity) * parseFloat(amount.unitPrice);
            },
            getSubTotalQuantityForGroupAmount: function () {
                var subTotalQuantityForGroupAmount = 0;

                if ($scope.data === undefined || $scope.data === null) {
                    return subTotalQuantityForGroupAmount;
                }

                for (var i = 0; i < $scope.data.productBreakDownCategoryAmount.length; i++) {
                    var amount = $scope.data.productBreakDownCategoryAmount[i];

                    if (amount.quantity === null || amount.quantity === "") {
                        continue;
                    }

                    subTotalQuantityForGroupAmount = parseFloat(subTotalQuantityForGroupAmount) + parseFloat(amount.quantity);
                }

                return subTotalQuantityForGroupAmount;
            },
            getSubTotalUnitPriceForGroupAmount: function () {
                var subTotalUnitPriceForGroupAmount = 0;

                if ($scope.data === undefined || $scope.data === null) {
                    return subTotalUnitPriceForGroupAmount;
                }

                for (var i = 0; i < $scope.data.productBreakDownCategoryAmount.length; i++) {
                    var amount = $scope.data.productBreakDownCategoryAmount[i];

                    if (amount.unitPrice === null || amount.unitPrice === "") {
                        continue;
                    }

                    subTotalUnitPriceForGroupAmount = parseFloat(subTotalUnitPriceForGroupAmount) + parseFloat(amount.unitPrice);
                }

                return subTotalUnitPriceForGroupAmount;
            },
            getSubTotalAmountForGroupAmount: function () {
                var subTotalAmountForGroupAmount = 0;

                if ($scope.data === undefined || $scope.data === null) {
                    return subTotalAmountForGroupAmount;
                }

                for (var i = 0; i < $scope.data.productBreakDownCategoryAmount.length; i++) {
                    var amount = $scope.data.productBreakDownCategoryAmount[i];

                    if (amount.amount === null || amount.amount === "") {
                        continue;
                    }

                    subTotalAmountForGroupAmount = parseFloat(subTotalAmountForGroupAmount) + parseFloat(amount.amount);
                }

                for (var i = 0; i < $scope.data.productBreakDownCategoryPercent.length; i++) {
                    var percent = $scope.data.productBreakDownCategoryPercent[i];
                    percent.unitPrice = subTotalAmountForGroupAmount;
                }

                return subTotalAmountForGroupAmount;
            },

            getSubTotalQuantityForGroupPercent: function () {
                var subTotalQuantityForGroupPercent = 0;

                if ($scope.data === undefined || $scope.data === null) {
                    return subTotalQuantityForGroupPercent;
                }

                for (var i = 0; i < $scope.data.productBreakDownCategoryPercent.length; i++) {
                    var percent = $scope.data.productBreakDownCategoryPercent[i];

                    if (percent.quantity === null || percent.quantity === "") {
                        continue;
                    }

                    subTotalQuantityForGroupPercent = parseFloat(subTotalQuantityForGroupPercent) + parseFloat(percent.quantity);
                }

                return subTotalQuantityForGroupPercent;
            },
            getSubTotalAmountForGroupPercent: function () {
                var subTotalAmountForGroupPercent = 0;

                if ($scope.data === undefined || $scope.data === null) {
                    return subTotalAmountForGroupPercent;
                }

                for (var i = 0; i < $scope.data.productBreakDownCategoryPercent.length; i++) {
                    var percent = $scope.data.productBreakDownCategoryPercent[i];

                    if (percent.amount === null || percent.amount === "") {
                        continue;
                    }

                    subTotalAmountForGroupPercent = parseFloat(subTotalAmountForGroupPercent) + parseFloat(percent.amount);
                }

                return subTotalAmountForGroupPercent;
            },
            getAmountForGroupPercent: function (percent) {
                if (percent.quantity === null || percent.quantity === "") {
                    return percent.amount = null;
                }

                if (percent.unitPrice === null || percent.unitPrice === "") {
                    return percent.amount = null;
                }

                return percent.amount = parseFloat(percent.quantity) * parseFloat(percent.unitPrice) / 100;
            },
            getAmountForGroupPercentAutofill: function (percent) {
                if (percent.quantity === null || percent.quantity === "") {
                    return percent.amount = null;
                }

                if (percent.unitPrice === null || percent.unitPrice === "") {
                    return percent.amount = null;
                }

                return percent.amount = parseFloat(percent.quantity) * parseFloat(percent.unitPrice) / 100;
            },

            // Issues 1360: Add features
            getTotalAmountProductBreakDown: function () {
                return parseFloat($scope.event.getSubTotalAmountForGroupAmount()) + parseFloat($scope.event.getSubTotalAmountForGroupPercent());
            },
            getPriceAmountProductBreakDown: function () {
                var priceAmount = 0;

                // data null
                if ($scope.data === null) {
                    return 0;
                }

                // total amount or specific season (%)
                if ($scope.event.getTotalAmountProductBreakDown() === null || $scope.event.getTotalAmountProductBreakDown() === 0 || $scope.data.seasonSpec === null || $scope.data.seasonSpec === 0) {
                    return 0;
                }

                // calculate price amount
                return parseFloat($scope.event.getTotalAmountProductBreakDown()) * (100 + parseFloat($scope.data.seasonSpec)) / 100;
            },

            selectClient: function (item) {
                $scope.data.clientID = item.id;
            }
        }

        //Search Model Varible
        $scope.model = {
            id: null,
            label: null,
            description: null,

            factoryID: null,
            factoryUD: null,
            factoryNM: null
        };
        $scope.requestModel = {
            url: context.serviceUrl + 'quicksearchmodel',
            token: context.token,
        };

        $scope.sampleProduct = {
            id: null,
            label: null,
            description: null,

            modelID: null,
            modelUD: null,
            modelNM: null,

            factoryID: null,
            factoryUD: null,
            factoryNM: null
        };
        $scope.requestSampleProduct = {
            url: context.serviceUrl + 'quicksearchsampleproduct',
            token: context.token,
        };

        $scope.client = {
            id: null,
            label: null,
            description: ''
        };
        $scope.requestClient = {
            url: context.serviceUrl + 'getsupportclient',
            token: context.token
        };

        $scope.method = {
            getNewID: getNewID,
            getNewDetailID: getNewDetailID,
            //fillproductBreakDownCategory: fillproductBreakDownCategory,
            uploadProductBreakDownDetails: uploadProductBreakDownDetails,
            convertString: convertString,
            convertFloat: convertFloat,

            totalQuantity: totalQuantity,
            totalUnitPrice: totalUnitPrice,
            totalAmount: totalAmount,
            calculateAmount: calculateAmount,
            autofillUnitPrice: autofillUnitPrice,

            fillproductBreakDownCategoryAmount: fillproductBreakDownCategoryAmount,
            fillproductBreakDownCategoryPercent: fillproductBreakDownCategoryPercent,

            totalEleAmount: totalEleAmount,
            subtotalQntAmount: subtotalQntAmount,
            subtotalUPAmount: subtotalUPAmount,
            subtotalAAmount: subtotalAAmount,

            totalElePercent: totalElePercent,
            subtotalQntPercent: subtotalQntPercent,
            subtotalUPPercent: subtotalUPPercent,
            subtotalAPercent: subtotalAPercent,

            elePercentUP: elePercentUP,
            changeSubAmount: changeSubAmount,
            getPriceAmount: getPriceAmount,

            // get detail price, quantity, amount, dimensionL, dimensionW, dimensionH, weight, volumne, acreage
            getTotalPriceDetail: function (categoryType) {
                var totalPrice = 0;
                for (var i = 0; i < categoryType.productBreakDownDetail.length; i++) {
                    var itemDetail = categoryType.productBreakDownDetail[i];
                    if (itemDetail.unitPrice !== null && itemDetail.unitPrice !== "") {
                        totalPrice = parseFloat(totalPrice) + parseFloat(itemDetail.unitPrice);
                    }
                }
                return (totalPrice === 0) ? null : totalPrice;
            },
            getTotalQuantityDetail: function (categoryType) {
                var totalQuantity = 0;
                for (var i = 0; i < categoryType.productBreakDownDetail.length; i++) {
                    var itemDetail = categoryType.productBreakDownDetail[i];
                    if (itemDetail.quantity !== null && itemDetail.quantity !== "") {
                        totalQuantity = parseFloat(totalQuantity) + parseFloat(itemDetail.quantity);
                    }
                }
                return (totalQuantity === 0) ? null : totalQuantity;
            },
            getTotalAmountDetail: function (categoryType) {
                var totalAmount = 0;
                var isNotNull = false;

                for (var i = 0; i < categoryType.productBreakDownDetail.length; i++) {
                    var itemDetail = categoryType.productBreakDownDetail[i];
                    if (itemDetail.amount !== null && itemDetail.amount !== "") {
                        isNotNull = true;
                        totalAmount = parseFloat(totalAmount) + parseFloat(itemDetail.amount);
                    }
                }

                return (totalAmount === 0 && !isNotNull) ? null : totalAmount;
            },
            getTotalWeightDetail: function (categoryType) {
                var totalWeight = 0;
                for (var i = 0; i < categoryType.productBreakDownDetail.length; i++) {
                    var itemDetail = categoryType.productBreakDownDetail[i];
                    if (itemDetail.weight !== null && itemDetail.weight !== "") {
                        totalWeight = parseFloat(totalWeight) + parseFloat(itemDetail.weight);
                    }
                }
                return (totalWeight === 0) ? null : totalWeight;
            },
            getTotalVolumneDetail: function (categoryType) {
                var totalVolumne = 0;
                for (var i = 0; i < categoryType.productBreakDownDetail.length; i++) {
                    var itemDetail = categoryType.productBreakDownDetail[i];
                    if (itemDetail.volume !== null && itemDetail.volume !== "") {
                        totalVolumne = parseFloat(totalVolumne) + parseFloat(itemDetail.volume);
                    }
                }
                return (totalVolumne === 0) ? null : totalVolumne;
            },
            getTotalAcreageDetail: function (categoryType) {
                var totalAcreage = 0;
                for (var i = 0; i < categoryType.productBreakDownDetail.length; i++) {
                    var itemDetail = categoryType.productBreakDownDetail[i];
                    if (itemDetail.acreage !== null && itemDetail.acreage !== "") {
                        totalAcreage = parseFloat(totalAcreage) + parseFloat(itemDetail.acreage);
                    }
                }
                return (totalAcreage === 0) ? null : totalAcreage;
            }
        }

        function init() {
            productBreakDownService.serviceUrl = context.serviceUrl;
            productBreakDownService.token = context.token;

            $scope.event.load();
        }

        function load() {
            productBreakDownService.getDataWithModel(
                context.id,
                context.modelID,
                context.sampleProductID,
                function (data) {
                    $scope.data = data.data.mainData;
                    jQuery('#content').show();
                },
                function (error) {
                });
        }

        function updated($event) {
            $event.preventDefault();

            if ($scope.data.productBreakDownCategory === null) {
                $scope.data.productBreakDownCategory = [];
            }

            if ($scope.data.productBreakDownCategoryAmount.length > 0) {
                for (var i = 0; i < $scope.data.productBreakDownCategoryAmount.length; i++) {
                    var item = $scope.data.productBreakDownCategoryAmount[i];
                    $scope.data.productBreakDownCategory.push(item);
                }
            }

            if ($scope.data.productBreakDownCategoryPercent.length > 0) {
                for (i = 0; i < $scope.data.productBreakDownCategoryPercent.length; i++) {
                    item = $scope.data.productBreakDownCategoryPercent[i];
                    $scope.data.productBreakDownCategory.push(item);
                }
            }

            productBreakDownService.update(
                context.id,
                $scope.data,
                function (data) {
                    jsHelper.processMessage(data.message);

                    window.location = context.refreshUrl + data.data.productBreakDownID;

                    $scope.data = data.data;
                },
                function (error) {
                    //do not need do anything
                });
        };

        function deleted(id) {
            productBreakDownService.delete(
                id,
                null,
                function (data) {
                    jsHelper.processMessage(data.message);
                    window.location = context.backUrl;
                },
                function (error) {
                    //do not need do anything
                }
            );
        };

        function getProductBreakDownDetail(item) {
            $scope.dataDetail = item;
            $scope.openSubForm = true;
            //jQuery('#frmAddProductBreakDownDetail').modal();
        }

        function addProductBreakDownCategory() {
            var newItem = {
                productBreakDownCategoryID: $scope.method.getNewID(),
                productBreakDownCategoryNM: null,
                quantity: null,
                unitPrice: null,
                amount: null,
                remark: null,
                productBreakDownCalculationTypeID: null,
                productBreakDownCalculationTypeNM: null,
                productBreakDownCategoryImage: [],
                productBreakDownCategoryType: []
            };

            $scope.data.productBreakDownCategory.push(newItem);
            $scope.totalRows = $scope.totalRows + 1;
        }

        function addProductBreakDownCategoryAmount() {
            var newItem = {
                productBreakDownCategoryID: $scope.method.getNewID(),
                productBreakDownCategoryNM: null,
                quantity: null,
                unitPrice: null,
                amount: null,
                remark: null,
                productBreakDownCalculationTypeID: 1,
                productBreakDownCalculationTypeNM: 'Amount',
                productBreakDownCategoryImage: [],
                productBreakDownCategoryType: [],
                displayOrder: $scope.data.productBreakDownCategoryAmount.length + 1,
            };

            $scope.data.productBreakDownCategoryAmount.push(newItem);
            $scope.totalRowsAmount = $scope.totalRowsAmount + 1;
        }

        function removeProductBreakDownCategoryAmount(item) {
            var index = $scope.data.productBreakDownCategoryAmount.indexOf(item);
            $scope.data.productBreakDownCategoryAmount.splice(index, 1);

            $scope.totalRowsAmount = $scope.data.productBreakDownCategoryAmount.length;

            // update index again
            var amountindex = 0;
            for (var i = 0; i < $scope.data.productBreakDownCategoryAmount.length; i++) {
                var amount = $scope.data.productBreakDownCategoryAmount[i];

                amountindex = amountindex + 1;
                amount.index = amountindex;
            }
        }

        function fillProductBreakDownCategoryAmount() {
            if ($scope.data.productBreakDownCategoryAmount.length > 0) {
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
                            $scope.method.fillproductBreakDownCategoryAmount();
                        }
                    }
                })
            } else {
                $scope.method.fillproductBreakDownCategoryAmount();
            }
        }

        function addProductBreakDownCategoryPercent() {
            var newItem = {
                productBreakDownCategoryID: $scope.method.getNewID(),
                productBreakDownCategoryNM: null,
                quantity: null,
                unitPrice: null,
                amount: null,
                remark: null,
                productBreakDownCalculationTypeID: 2,
                productBreakDownCalculationTypeNM: 'Percent(%)',
                productBreakDownCategoryImage: [],
                productBreakDownCategoryType: [],
                displayOrder: $scope.data.productBreakDownCategoryPercent.length + 1
            };

            $scope.data.productBreakDownCategoryPercent.push(newItem);
            $scope.totalRowsPercent = $scope.totalRowsPercent + 1;
        }

        function removeProductBreakDownCategoryPercent(item) {
            var index = $scope.data.productBreakDownCategoryPercent.indexOf(item);
            $scope.data.productBreakDownCategoryPercent.splice(index, 1);

            $scope.totalRowsPercent = $scope.data.productBreakDownCategoryPercent.length;

            // update index again
            var percentindex = 0;
            for (var i = 0; i < $scope.data.productBreakDownCategoryPercent.length; i++) {
                var percent = $scope.data.productBreakDownCategoryPercent[i];

                percentindex = percentindex + 1;
                percent.index = percentindex;
            }
        }

        function fillProductBreakDownCategoryPercent() {
            if ($scope.data.productBreakDownCategoryPercent.length > 0) {
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
                            $scope.method.fillproductBreakDownCategoryPercent();
                        }
                    }
                })
            } else {
                $scope.method.fillproductBreakDownCategoryPercent();
            }
        }

        function addProductBreakDownDetail(dataDetail) {
            var newItemDetail = {
                productBreakDownDetailID: $scope.method.getNewDetailID(),
                productBreakDownCategoryID: dataDetail.productBreakDownCategoryID,
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
                remark: null
            }

            $scope.dataDetail.productBreakDownCategoryType.push(newItemDetail);
            $scope.totalDetailRows = $scope.dataDetail.productBreakDownCategoryType.length;
        }

        function removeProductBreakDownDetail(subItem) {
            var subIndex = $scope.dataDetail.productBreakDownCategoryType.indexOf(subItem);
            $scope.dataDetail.productBreakDownCategoryType.splice(subIndex, 1);
            $scope.totalDetailRows = $scope.dataDetail.productBreakDownCategoryType.length;
        }

        function downloadProductBreakDownDetailTemplate() {
            window.open(context.servicePath + $scope.fileName);
        }

        function uploadProductBreakDownDetailTemplate(dataDetail) {
            if (dataDetail.productBreakDownCategoryType.length > 0) {
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
                            $scope.method.uploadProductBreakDownDetails(dataDetail);
                        }
                    }
                })
            } else {
                $scope.method.uploadProductBreakDownDetails(dataDetail);
            }
        }

        function getNewID() {
            $scope.newID = $scope.newID - 1;
            return $scope.newID;
        }

        function getNewDetailID() {
            $scope.newDetailID = $scope.newDetailID - 1;
            return $scope.newDetailID;
        }

        function uploadProductBreakDownDetails(dataDetail) {
            dataDetail.productBreakDownCategoryType = [];
            //debugger
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
                                    productBreakDownCategoryID: dataDetail.productBreakDownCategoryID,
                                    productBreakDownCategoryTypeNM: (ele.ProductBreakDownDetailName === undefined || ele.ProductBreakDownDetailName === null) ? '' : ele.ProductBreakDownDetailName,
                                    productBreakDownDetail: [],
                                }

                                productBreakDownDetailID = newCategoryType.productBreakDownCategoryTypeID;

                                dataDetail.productBreakDownCategoryType.push(newCategoryType);

                                index = dataDetail.productBreakDownCategoryType.indexOf(newCategoryType);
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

                                dataDetail.productBreakDownCategoryType[index].productBreakDownDetail.push(newDetail);
                            }

                            $scope.$apply();
                        }
                    }
                };

                reader.readAsArrayBuffer(f);
            }, false);

            input.click();
        }

        function selectModel(model) {
            if (model !== null) {
                $scope.data.modelID = model.id;
                $scope.data.modelUD = model.label;
                $scope.data.modelNM = model.description;

                $scope.$apply();
            }
        }

        function convertString(data) {

        }

        function convertFloat(data) {
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
        }

        function approveTechnicalDrawing($event) {
            $event.preventDefault();
            if (context.id > 0 && confirm('Approve the Product Break Down?')) {
                productBreakDownService.approveProductBreakDown(
                    context.id,
                    function (data) {
                        jsHelper.processMessage(data.message);
                        debugger;
                        window.location = context.refreshUrl + data.data.productBreakDownID;
                        $scope.data = data.data;
                    },
                    function (error) {
                        jsHelper.showMessage('warning', error);
                    }
                );
            }
        }

        function selectSampleProduct(sampleProduct) {
            if (sampleProduct !== null) {
                $scope.data.sampleProductID = sampleProduct.id;
                $scope.data.sampleProductUD = sampleProduct.label;
                $scope.data.articleDescription = sampleProduct.description;

                if ($scope.data.modelID === null) {
                    $scope.data.modelID = sampleProduct.modelID;
                }

                if ($scope.data.modelUD === null) {
                    $scope.data.modelUD = sampleProduct.modelUD;
                }

                if ($scope.data.modelNM === null) {
                    $scope.data.modelNM = sampleProduct.modelNM;
                }

                $scope.$apply();
            }
        }

        function totalQuantity() {
            $scope.totalQnt = 0;

            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.productBreakDownCategory.length; i++) {
                    var item = $scope.data.productBreakDownCategory[i];

                    if (item.quantity !== null) {
                        $scope.totalQnt = parseFloat($scope.totalQnt) + parseFloat(item.quantity);
                    }
                }
            }

            return $scope.totalQnt;
        }

        function totalUnitPrice() {
            $scope.totalUnitPrice = 0;

            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.productBreakDownCategory.length; i++) {
                    var item = $scope.data.productBreakDownCategory[i];

                    if (item.unitPrice !== null) {
                        $scope.totalUnitPrice = parseFloat($scope.totalUnitPrice) + parseFloat(item.unitPrice);
                    }
                }
            }

            return $scope.totalUnitPrice;
        }

        function totalAmount() {
            return $scope.subTotalAmount + $scope.subTotalPercent;
        }

        function calculateAmount(item) {
            if (item.quantity !== null && item.unitPrice !== null) {
                item.amount = item.quantity * item.unitPrice;
            }

            return item.amount;
        }

        function autofillUnitPrice(item) {
            if (item.quantity !== null && item.productBreakDownCalculationTypeID === 1) {
                item.unitPrice = $scope.totalAmount * item.quantity / 100;
                return item.unitPrice;
            }

            return item.unitPrice;
        }

        function fillproductBreakDownCategoryAmount() {
            $scope.data.productBreakDownCategoryAmount = [];

            productBreakDownService.autofillProductBreakDownCategory(
                1,
                function (data) {
                    var amountindex = 0;

                    for (var i = 0; i < data.data.length; i++) {
                        var item = data.data[i];

                        item.productBreakDownCategoryID = $scope.method.getNewID();

                        amountindex = amountindex + 1;
                        item.displayOrder = amountindex;

                        $scope.data.productBreakDownCategoryAmount.push(item);

                        // Issue 1360
                        var index = $scope.data.productBreakDownCategoryAmount.indexOf(item);
                        $scope.data.productBreakDownCategoryAmount[index].quantity = item.quantityPercent;
                    }

                    $scope.totalRowsAmount = $scope.data.productBreakDownCategoryAmount.length;
                },
                function (error) {
                    // do nothing
                })
        }

        function fillproductBreakDownCategoryPercent() {
            $scope.data.productBreakDownCategoryPercent = [];

            productBreakDownService.autofillProductBreakDownCategory(
                2,
                function (data) {
                    var percentindex = 0;

                    for (var i = 0; i < data.data.length; i++) {
                        var item = data.data[i];

                        item.productBreakDownCategoryID = $scope.method.getNewID();

                        percentindex = percentindex + 1;
                        item.displayOrder = percentindex;
                        // progress unit price
                        item.unitPrice = null;
                        var subTotalAmountForGroupAmount = scope.event.getSubTotalAmountForGroupAmount();
                        if (subTotalAmountForGroupAmount !== null && subTotalAmountForGroupAmount !== "") {
                            item.unitPrice = subTotalAmountForGroupAmount;
                        }

                        $scope.data.productBreakDownCategoryPercent.push(item);

                        // Issue 1360
                        var index = $scope.data.productBreakDownCategoryPercent.indexOf(item);
                        $scope.data.productBreakDownCategoryPercent[index].quantity = item.quantityPercent;
                    }

                    $scope.totalRowsPercent = $scope.data.productBreakDownCategoryPercent.length;
                },
                function (error) {
                    // do nothing
                })
        }

        function totalEleAmount(item) {
            if (item.quantity !== null && item.unitPrice !== null) {
                item.amount = item.quantity * item.unitPrice;
            }
            return item.amount;
        }

        function subtotalQntAmount() {
            var subSumQnt = null;
            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.productBreakDownCategoryAmount.length; i++) {
                    var item = $scope.data.productBreakDownCategoryAmount[i];
                    if (item.quantity !== null && item.quantity !== '') {
                        if (subSumQnt === null) {
                            subSumQnt = parseFloat(item.quantity);
                        } else {
                            subSumQnt = subSumQnt + parseFloat(item.quantity);
                        }
                    }
                }
                $scope.method.changeSubAmount();
            }
            return subSumQnt;
        }

        function subtotalUPAmount() {
            var subSumUP = null;
            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.productBreakDownCategoryAmount.length; i++) {
                    var item = $scope.data.productBreakDownCategoryAmount[i];
                    if (item.unitPrice !== null && item.unitPrice !== '') {
                        if (subSumUP === null) {
                            subSumUP = parseFloat(item.unitPrice);
                        } else {
                            subSumUP = subSumUP + parseFloat(item.unitPrice);
                        }
                    }
                }
                $scope.method.changeSubAmount();
            }
            return subSumUP;
        }

        function subtotalAAmount() {
            var subSumA = null;
            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.productBreakDownCategoryAmount.length; i++) {
                    var item = $scope.data.productBreakDownCategoryAmount[i];
                    if (item.amount !== null) {
                        if (subSumA === null) {
                            subSumA = parseFloat(item.amount);
                        } else {
                            subSumA = subSumA + parseFloat(item.amount);
                        }
                    }
                }
            }
            $scope.subTotalAmount = subSumA;
            return subSumA;
        }

        function subtotalQntPercent() {
            var subSumQnt = null;
            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.productBreakDownCategoryPercent.length; i++) {
                    var item = $scope.data.productBreakDownCategoryPercent[i];
                    if (item.quantity !== null) {
                        if (subSumQnt === null) {
                            if (item.quantity === '') {
                                subSumQnt = 0;
                            } else {
                                subSumQnt = parseFloat(item.quantity);
                            }
                        } else {
                            subSumQnt = subSumQnt + parseFloat(item.quantity);
                        }
                    }
                }
            }
            return subSumQnt;
        }

        function subtotalUPPercent() {
            var subSumUP = null;
            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.productBreakDownCategoryPercent.length; i++) {
                    var item = $scope.data.productBreakDownCategoryPercent[i];
                    if (item.unitPrice !== null) {
                        if (subSumUP === null) {
                            subSumUP = parseFloat(item.unitPrice);
                        } else {
                            subSumUP = subSumUP + parseFloat(item.unitPrice);
                        }
                    }
                }
            }
            return subSumUP;
        }

        function subtotalAPercent() {
            var subSumA = null;
            if ($scope.data !== null) {
                for (var i = 0; i < $scope.data.productBreakDownCategoryPercent.length; i++) {
                    var item = $scope.data.productBreakDownCategoryPercent[i];
                    if (item.amount !== null) {
                        if (subSumA === null) {
                            subSumA = parseFloat(item.amount);
                        } else {
                            subSumA = subSumA + parseFloat(item.amount);
                        }
                    }
                }
            }
            $scope.subTotalPercent = subSumA;
            return subSumA;
        }

        function elePercentUP(item) {
        }

        function totalElePercent(item) {
            if (item.quantity !== null && item.unitPrice !== null) {
                item.amount = item.quantity * item.unitPrice;
            }
            return item.amount;
        }

        function changeSubAmount() {
            if ($scope.subTotalAmount !== 0) {
                for (var i = 0; i < $scope.data.productBreakDownCategoryPercent.length; i++) {
                    var item = $scope.data.productBreakDownCategoryPercent[i];
                    item.unitPrice = $scope.subTotalAmount * item.quantity / 100;
                }
            }
        }

        function getPriceAmount() {
            var totalAmount = $scope.method.totalAmount();

            if ($scope.data === null) {
                return 0;
            }

            // case season spec not declare or null value
            if (totalAmount === null || $scope.data.seasonSpec === undefined || $scope.data.seasonSpec === null) {
                return 0;
            }

            // calculate price amount with formular
            return parseFloat(totalAmount.toString()) * (100 + parseFloat($scope.data.seasonSpec)) / 100;
        }

        function changeImageFile($event, controlID, typeID) {
            masterUploader.multiSelect = true;
            masterUploader.imageOnly = false;
            masterUploader.callback = function () {
                var scope = angular.element(jQuery('body')).scope();
                scope.$apply(function () {
                    angular.forEach(masterUploader.selectedFiles, function (img) {
                        switch (typeID) {
                            case 1:
                                var arr = scope.data.productBreakDownCategoryAmount.filter(function (o) { return o.productBreakDownCategoryID == controlID });
                                break;
                            case 2:
                                var arr = scope.data.productBreakDownCategoryPercent.filter(function (o) { return o.productBreakDownCategoryID == controlID });
                                break;
                        }

                        $(arr).each(function () {
                            this.imageFileUrl = img.fileURL;
                            this.imageFileFriendlyName = img.filename;
                            this.imageFileHasChange = true;
                            this.newImageFile = img.filename;
                        });
                    }, null);
                });
            };
            masterUploader.open();
        }

        function removeImageFile($event, controlID, typeID) {
            switch (typeID) {
                case 1:
                    var arr = scope.data.productBreakDownCategoryAmount.filter(function (o) { return o.productBreakDownCategoryID == controlID });
                    break;
                case 2:
                    var arr = scope.data.productBreakDownCategoryPercent.filter(function (o) { return o.productBreakDownCategoryID == controlID });
                    break;
            }

            $(arr).each(function () {
                this.imageFileUrl = '';
                this.imageFileFriendlyName = '';
                this.imageFileHasChange = true;
                this.newImageFile = '';
            });
        }

        $scope.event.init();
    }
})();