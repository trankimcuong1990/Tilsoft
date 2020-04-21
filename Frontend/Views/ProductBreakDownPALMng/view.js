(function () {
    'use strict';

    angular.module('tilsoftApp', ['avs-directives']).controller('tilsoftController', productBreakDownPALController);
    productBreakDownPALController.$inject = ['$scope', '$timeout', 'dataService'];

    function productBreakDownPALController($scope, $timeout, productBreakDownPALService) {
        $scope.data = null;
        $scope.dataDetail = null;
        $scope.openSubForm = false;

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

                if ($scope.data === null) {
                    return 0;
                }

                if ($scope.event.getTotalAmountPAL() === null || $scope.event.getTotalAmountPAL() === 0 || $scope.data.seasonSpec === null || $scope.data.seasonSpec === 0) {
                    return 0;
                }

                return parseFloat($scope.event.getTotalAmountPAL()) * (100 + parseFloat($scope.data.seasonSpec)) / 100;
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
            getTotalAmountPAL2: function (item) {
                if (item.quantity === null || item.quantity === "") {
                    return item.amount = null;
                }

                if (item.unitPrice === null || item.unitPrice === "") {
                    return item.amount = null;
                }

                return item.amount = parseFloat(item.quantity) * parseFloat(item.unitPrice);
            },
            downloadImage: function (item) {
                if (item.fileLocation) {
                    window.open(item.fileLocation);
                }
            },
            getFormDetail: function (item) {
                $scope.dataDetail = item;
                $scope.openSubForm = true;
            },
            getAmountPAL: function (detailData, categoryType) {
                detailData.unitPrice = (detailData.unitPrice !== undefined && detailData.unitPrice !== null && detailData.unitPrice !== "") ? detailData.unitPrice : null;
                detailData.quantity = (detailData.quantity !== undefined && detailData.quantity !== null && detailData.quantity !== "") ? detailData.quantity : null;

                if (detailData.unitPrice !== null && detailData.quantity !== null) {
                    detailData.amount = parseFloat(detailData.unitPrice) * parseFloat(detailData.quantity);
                }

                $scope.method.getTotalPriceDetailPAL(categoryType);
                $scope.method.getTotalQntDetailPAL(categoryType);
                $scope.method.getTotalAmountDetailPAL(categoryType);
            },
            closeFormDetail: function (detailData) {
                $scope.openSubForm = false;
                if (!detailData.isSave) {
                    detailData.productBreakDownCategoryTypePAL = [];
                }
            },
            getTotalPricePALExchange: function () {
                var priceAmount = parseFloat($scope.event.getTotalPricePAL());
                var exchangeRate = 1;

                if ($scope.data !== null && $scope.data.exchangeRate !== undefined && $scope.data.exchangeRate !== null && $scope.data.exchangeRate !== '') {
                    exchangeRate = parseFloat($scope.data.exchangeRate);
                    return priceAmount / exchangeRate;
                }
            }
        };

        $scope.method = {
            getNewID: function () {
                return ($scope.newID - 1);
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

                $scope.dataDetail.unitPrice = totalAmount;
                return totalAmount;
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
                if (item.quantity === null || item.quantity === '') {
                    item.quantity = 1;
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
            }
        };

        $timeout(function () {
            $scope.event.init();
        }, 0);
    };
})();